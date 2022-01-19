using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace EasyPOS.EasyFISIntegration.Controllers
{
    class EasyPOSTrnStockOutController
    {
        // ====
        // Data
        // ====
        private Data.easyposdbDataContext posdb = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

        public Forms.Software.SysSettings.SysSettingsForm sysSettingsForm;
        public String activityDate;

        // ===========
        // Constructor
        // ===========
        public EasyPOSTrnStockOutController(Forms.Software.SysSettings.SysSettingsForm form, String actDate)
        {
            sysSettingsForm = form;
            activityDate = actDate;
        }

        // ===================
        // Fill Leading Zeroes
        // ===================
        public String FillLeadingZeroes(Int32 number, Int32 length)
        {
            var result = number.ToString();
            var pad = length - result.Length;
            while (pad > 0)
            {
                result = '0' + result;
                pad--;
            }

            return result;
        }

        // ==============
        // Sync Stock Out
        // ==============
        public async void SyncStockOut(String apiUrlHost, String branchCode)
        {
            await GetStockOut(apiUrlHost, branchCode);
        }

        // =============
        // Get Stock Out
        // =============
        public Task GetStockOut(String apiUrlHost, String branchCode)
        {
            try
            {
                DateTime dateTimeToday = DateTime.Now;
                String stockOutDate = Convert.ToDateTime(activityDate).ToString("MM-dd-yyyy", CultureInfo.InvariantCulture);

                // ============
                // Http Request
                // ============
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://" + apiUrlHost + "/api/get/POSIntegration/stockOut/" + stockOutDate + "/" + branchCode);
                httpWebRequest.Method = "GET";
                httpWebRequest.Accept = "application/json";

                // ================
                // Process Response
                // ================
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    List<Entities.EasyPOSTrnStockOut> stockOuts = (List<Entities.EasyPOSTrnStockOut>)js.Deserialize(result, typeof(List<Entities.EasyPOSTrnStockOut>));

                    if (stockOuts.Any())
                    {
                        foreach (var stockOut in stockOuts)
                        {
                            var currentStockOut = from d in posdb.TrnStockOuts
                                                  where d.Remarks == "OT-" + stockOut.BranchCode + "-" + stockOut.OTNumber
                                                  && d.IsLocked == true
                                                  select d;

                            if (!currentStockOut.Any())
                            {
                                sysSettingsForm.logMessages("Saving Stock-Out...\r\n\n");
                                sysSettingsForm.logMessages("OT Number: OT-" + stockOut.BranchCode + "-" + stockOut.OTNumber + "\r\n\n");

                                var defaultPeriod = from d in posdb.MstPeriods select d;
                                var defaultSettings = from d in posdb.IntCloudSettings select d;

                                String stockOutNumber = "0000000001";
                                var lastStockOut = from d in posdb.TrnStockOuts.OrderByDescending(d => d.Id) select d;
                                if (lastStockOut.Any())
                                {
                                    Int32 newStockOutNumber = Convert.ToInt32(lastStockOut.FirstOrDefault().StockOutNumber) + 1;
                                    stockOutNumber = FillLeadingZeroes(newStockOutNumber, 10);
                                }

                                var accounts = from d in posdb.MstAccounts select d;
                                if (accounts.Any())
                                {
                                    Data.TrnStockOut newStockOut = new Data.TrnStockOut
                                    {
                                        PeriodId = defaultPeriod.FirstOrDefault().Id,
                                        StockOutDate = Convert.ToDateTime(stockOut.OTDate),
                                        StockOutNumber = stockOutNumber,
                                        ManualStockOutNumber = null,
                                        AccountId = accounts.FirstOrDefault().Id,
                                        Remarks = "OT-" + stockOut.BranchCode + "-" + stockOut.OTNumber,
                                        PreparedBy = defaultSettings.FirstOrDefault().PostUserId,
                                        CheckedBy = defaultSettings.FirstOrDefault().PostUserId,
                                        ApprovedBy = defaultSettings.FirstOrDefault().PostUserId,
                                        IsLocked = true,
                                        EntryUserId = defaultSettings.FirstOrDefault().PostUserId,
                                        EntryDateTime = DateTime.Now,
                                        UpdateUserId = defaultSettings.FirstOrDefault().PostUserId,
                                        UpdateDateTime = DateTime.Now,
                                    };

                                    posdb.TrnStockOuts.InsertOnSubmit(newStockOut);
                                    posdb.SubmitChanges();

                                    if (stockOut.ListPOSIntegrationTrnStockOutItem.Any())
                                    {
                                        foreach (var item in stockOut.ListPOSIntegrationTrnStockOutItem.ToList())
                                        {
                                            var currentItem = from d in posdb.MstItems where d.BarCode.Equals(item.ItemCode) && d.MstUnit.Unit.Equals(item.Unit) select d;
                                            if (currentItem.Any())
                                            {
                                                Data.TrnStockOutLine newStockOutLine = new Data.TrnStockOutLine
                                                {
                                                    StockOutId = newStockOut.Id,
                                                    ItemId = currentItem.FirstOrDefault().Id,
                                                    UnitId = currentItem.FirstOrDefault().UnitId,
                                                    Quantity = item.Quantity,
                                                    Cost = item.Cost,
                                                    Amount = item.Amount,
                                                    AssetAccountId = currentItem.FirstOrDefault().AssetAccountId,
                                                };

                                                posdb.TrnStockOutLines.InsertOnSubmit(newStockOutLine);

                                                var updateItem = currentItem.FirstOrDefault();
                                                updateItem.OnhandQuantity = currentItem.FirstOrDefault().OnhandQuantity - Convert.ToDecimal(item.Quantity);

                                                posdb.SubmitChanges();

                                                sysSettingsForm.logMessages(" > " + currentItem.FirstOrDefault().ItemDescription + " * " + item.Quantity.ToString("#,##0.00") + "\r\n\n");
                                            }
                                        }
                                    }

                                    sysSettingsForm.logMessages("Save Successful!\r\n\n");
                                    sysSettingsForm.logMessages("Time Stamp: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "\r\n\n");
                                    sysSettingsForm.logMessages("\r\n\n");
                                }
                                else
                                {
                                    sysSettingsForm.logMessages("Save Failed!\r\n\n");
                                    sysSettingsForm.logMessages("Error: Default Account is Empty.\r\n\n");
                                    sysSettingsForm.logMessages("Time Stamp: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "\r\n\n");
                                    sysSettingsForm.logMessages("\r\n\n");
                                }
                            }
                        }
                    }
                }

                return Task.FromResult("");
            }
            catch (Exception e)
            {
                sysSettingsForm.logMessages("Stock-Out Integration Failed!\r\n\n");
                sysSettingsForm.logMessages("Error: " + e.Message + "\r\n\n");
                sysSettingsForm.logMessages("Time Stamp: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "\r\n\n");
                sysSettingsForm.logMessages("\r\n\n");

                return Task.FromResult("");
            }
        }
    }
}