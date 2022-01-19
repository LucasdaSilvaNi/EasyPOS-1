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
    class EasyPOSTrnSalesReturnController
    {
        // ====
        // Data
        // ====
        private Data.easyposdbDataContext posdb = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

        public Forms.Software.SysSettings.SysSettingsForm sysSettingsForm;

        // ===========
        // Constructor
        // ===========
        public EasyPOSTrnSalesReturnController(Forms.Software.SysSettings.SysSettingsForm form)
        {
            sysSettingsForm = form;
        }

        // =================
        // Sync Sales Return
        // =================
        public async void SyncSalesReturn(String apiUrlHost, String branchCode, String userCode)
        {
            await GetSalesReturn(apiUrlHost, branchCode, userCode);
        }

        // ================
        // Get Sales Return
        // ================
        public Task GetSalesReturn(String apiUrlHost, String branchCode, String userCode)
        {
            try
            {
                var salesReturn = from d in posdb.TrnSales
                                  where d.IsLocked == true
                                  && d.IsTendered == false
                                  && d.IsCancelled == false
                                  && d.IsReturned == true
                                  && d.PostCode == null
                                  select d;

                if (salesReturn.Any())
                {
                    List<Entities.EasyPOSTrnSalesLines> listSalesLines = new List<Entities.EasyPOSTrnSalesLines>();
                    foreach (var salesLine in salesReturn.FirstOrDefault().TrnSalesLines)
                    {
                        listSalesLines.Add(new Entities.EasyPOSTrnSalesLines()
                        {
                            ItemManualArticleCode = salesLine.MstItem.BarCode,
                            Particulars = salesLine.MstItem.ItemDescription,
                            Unit = salesLine.MstUnit.Unit,
                            Quantity = salesLine.Quantity,
                            Price = salesLine.Price,
                            Discount = salesLine.MstDiscount.Discount,
                            DiscountAmount = salesLine.DiscountAmount,
                            NetPrice = salesLine.NetPrice,
                            Amount = salesLine.Amount,
                            VAT = salesLine.MstTax.Tax,
                            SalesItemTimeStamp = salesLine.SalesLineTimeStamp.ToString("MM/dd/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture)
                        });
                    }

                    var salesData = new Entities.EasyPOSTrnSales()
                    {
                        SIDate = salesReturn.FirstOrDefault().SalesDate.ToShortDateString(),
                        BranchCode = branchCode,
                        CustomerManualArticleCode = salesReturn.FirstOrDefault().MstCustomer.CustomerCode,
                        CreatedBy = userCode,
                        Term = salesReturn.FirstOrDefault().MstTerm.Term,
                        DocumentReference = "",
                        ManualSINumber = "RT-" + salesReturn.FirstOrDefault().SalesNumber,
                        Remarks = "From EasyPOS \n"
                                + "\nTerminal: " + salesReturn.FirstOrDefault().MstTerminal.Terminal
                                + "\nUser: " + salesReturn.FirstOrDefault().MstUser.UserName,
                        ManualSONumber = salesReturn.FirstOrDefault().ManualInvoiceNumber,
                        ListPOSIntegrationTrnSalesInvoiceItem = listSalesLines.ToList()
                    };

                    String json = new JavaScriptSerializer().Serialize(salesData);

                    sysSettingsForm.logMessages("Sending Sales Return...\r\n\n");
                    sysSettingsForm.logMessages("Sales Return Number: " + salesData.ManualSINumber + "\r\n\n");
                    sysSettingsForm.logMessages("Sales Return Date: " + salesData.SIDate + "\r\n\n");
                    sysSettingsForm.logMessages("Amount: " + salesData.ListPOSIntegrationTrnSalesInvoiceItem.Sum(d => d.Amount).ToString("#,##0.00") + "\r\n\n");

                    SendSalesReturn(apiUrlHost, json, salesReturn.FirstOrDefault().Id);
                }

                return Task.FromResult("");
            }
            catch (Exception e)
            {
                sysSettingsForm.logMessages("Sales Return Error: " + e.Message + "\r\n\n");
                sysSettingsForm.logMessages("Time Stamp: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "\r\n\n");
                sysSettingsForm.logMessages("\r\n\n");

                return Task.FromResult("");
            }
        }

        // =================
        // Send Sales Return
        // =================
        public void SendSalesReturn(String apiUrlHost, String json, Int32 salesReturnId)
        {
            try
            {
                // ============
                // Http Request
                // ============
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://" + apiUrlHost + "/api/add/POSIntegration/salesInvoice");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                // ====
                // Data
                // ====
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    Entities.EasyPOSTrnSales collection = new JavaScriptSerializer().Deserialize<Entities.EasyPOSTrnSales>(json);
                    streamWriter.Write(new JavaScriptSerializer().Serialize(collection));
                }

                // ================
                // Process response
                // ================
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    if (result != null)
                    {
                        var sales = from d in posdb.TrnSales
                                    where d.Id == salesReturnId
                                    select d;

                        if (sales.Any())
                        {
                            var updateSales = sales.FirstOrDefault();
                            updateSales.PostCode = result.Replace("\"", "");
                            posdb.SubmitChanges();
                        }

                        sysSettingsForm.logMessages("Send Succesful!" + "\r\n\n");
                        sysSettingsForm.logMessages("Time Stamp: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "\r\n\n");
                        sysSettingsForm.logMessages("\r\n\n");
                    }
                }
            }
            catch (WebException we)
            {
                var resp = new StreamReader(we.Response.GetResponseStream()).ReadToEnd();

                sysSettingsForm.logMessages(resp.Replace("\"", "") + "\r\n\n");
                sysSettingsForm.logMessages("Time Stamp: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "\r\n\n");
                sysSettingsForm.logMessages("\r\n\n");
            }
        }
    }
}