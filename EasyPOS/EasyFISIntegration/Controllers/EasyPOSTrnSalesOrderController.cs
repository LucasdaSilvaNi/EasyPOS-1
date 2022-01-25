using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace EasyPOS.EasyFISIntegration.Controllers
{
    class EasyPOSTrnSalesOrderController
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
        public EasyPOSTrnSalesOrderController(Forms.Software.SysSettings.SysSettingsForm form, String actDate)
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

        // ================
        // Sync Sales Order
        // ================
        public async void SyncSalesOrder(String apiUrlHost, String branchCode)
        {
            await GetSalesOrder(apiUrlHost, branchCode);
        }

        // ===============
        // Get Sales Order
        // ===============
        public Task GetSalesOrder(String apiUrlHost, String branchCode)
        {
            try
            {
                DateTime dateTimeToday = DateTime.Now;
                String salesOrderDate = Convert.ToDateTime(activityDate).ToString("MM-dd-yyyy", CultureInfo.InvariantCulture);

                // ============
                // Http Request
                // ============
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://" + apiUrlHost + "/api/get/POSIntegration/salesOrder/" + salesOrderDate + "/" + branchCode);
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
                    List<Entities.EasyPOSTrnSalesOrder> salesOrderLists = (List<Entities.EasyPOSTrnSalesOrder>)js.Deserialize(result, typeof(List<Entities.EasyPOSTrnSalesOrder>));

                    if (salesOrderLists.Any())
                    {
                        var period = from d in posdb.MstPeriods
                                     where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentPeriodId)
                                     select d;

                        if (!period.Any())
                        {
                            return Task.FromResult("Period not found.");
                        }

                        var terminal = from d in posdb.MstTerminals
                                       where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().TerminalId)
                                       select d;

                        if (!terminal.Any())
                        {
                            return Task.FromResult("Terminal not found.");
                        }

                        var user = from d in posdb.MstUsers
                                   where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId)
                                   select d;

                        if (!user.Any())
                        {
                            return Task.FromResult("User not found.");
                        }

                        foreach (var salesOrder in salesOrderLists)
                        {
                            var currentSales = from d in posdb.TrnSales
                                               where d.ManualInvoiceNumber == salesOrder.DocumentReference
                                               select d;

                            if (!currentSales.Any())
                            {
                                sysSettingsForm.logMessages("Saving Sales Order...\r\n\n");
                                sysSettingsForm.logMessages("SO Number: SO-" + salesOrder.BranchCode + "-" + salesOrder.ManualSONumber + "\r\n\n");

                                var customer = from d in posdb.MstCustomers
                                               where d.CustomerCode == salesOrder.CustomerCode
                                               select d;

                                if (customer.Any())
                                {
                                    String salesNumber = "0000000001";
                                    var lastSales = from d in posdb.TrnSales.OrderByDescending(d => d.Id) select d;
                                    if (lastSales.Any())
                                    {
                                        Int32 newSalesNumber = Convert.ToInt32(lastSales.FirstOrDefault().SalesNumber) + 1;
                                        salesNumber = FillLeadingZeroes(newSalesNumber, 10);
                                    }

                                    Data.TrnSale newSales = new Data.TrnSale
                                    {
                                        PeriodId = period.FirstOrDefault().Id,
                                        SalesDate = Convert.ToDateTime(salesOrder.SODate),
                                        SalesNumber = salesNumber,
                                        ManualInvoiceNumber = salesOrder.DocumentReference,
                                        CollectionNumber = null,
                                        Amount = 0,
                                        TableId = null,
                                        CustomerId = customer.FirstOrDefault().Id,
                                        AccountId = customer.FirstOrDefault().AccountId,
                                        TermId = customer.FirstOrDefault().TermId,
                                        DiscountId = null,
                                        SeniorCitizenId = "",
                                        SeniorCitizenName = "",
                                        SeniorCitizenAge = null,
                                        Remarks = salesOrder.Remarks,
                                        SalesAgent = user.FirstOrDefault().Id,
                                        TerminalId = terminal.FirstOrDefault().Id,
                                        PreparedBy = user.FirstOrDefault().Id,
                                        CheckedBy = user.FirstOrDefault().Id,
                                        ApprovedBy = user.FirstOrDefault().Id,
                                        IsLocked = false,
                                        IsTendered = false,
                                        IsCancelled = false,
                                        IsDispatched = false,
                                        DeliveryDriver = "",
                                        PaidAmount = 0,
                                        CreditAmount = 0,
                                        DebitAmount = 0,
                                        BalanceAmount = 0,
                                        EntryUserId = user.FirstOrDefault().Id,
                                        EntryDateTime = DateTime.Now,
                                        UpdateUserId = user.FirstOrDefault().Id,
                                        UpdateDateTime = DateTime.Now,
                                        Pax = null,
                                        TableStatus = 0,
                                    };

                                    posdb.TrnSales.InsertOnSubmit(newSales);
                                    posdb.SubmitChanges();

                                    Decimal amount = 0;

                                    if (salesOrder.POSIntegrationTrnSalesOrderItems.Any())
                                    {
                                        foreach (var item in salesOrder.POSIntegrationTrnSalesOrderItems.ToList())
                                        {
                                            var currentItem = from d in posdb.MstItems
                                                              where d.BarCode.Equals(item.ItemBarcode)
                                                              && d.MstUnit.Unit.Equals(item.Unit)
                                                              select d;

                                            if (currentItem.Any())
                                            {
                                                Int32 discountId = 0;

                                                var discount = from d in posdb.MstDiscounts
                                                               where d.Discount == item.Discount
                                                               select d;

                                                if (!discount.Any())
                                                {
                                                    var variableDiscount = from d in posdb.MstDiscounts
                                                                           where d.Discount == "Variable Discount"
                                                                           select d;

                                                    if (variableDiscount.Any())
                                                    {
                                                        discountId = variableDiscount.FirstOrDefault().Id;
                                                    }
                                                }
                                                else
                                                {
                                                    discountId = discount.FirstOrDefault().Id;
                                                }

                                                var tax = from d in posdb.MstTaxes
                                                          where d.Tax == item.VAT
                                                          select d;

                                                if (tax.Any())
                                                {
                                                    Decimal taxAmount = 0;
                                                    if (tax.FirstOrDefault().Rate > 0)
                                                    {
                                                        taxAmount = (item.Price * item.Quantity) / (1 + (tax.FirstOrDefault().Rate / 100)) * (tax.FirstOrDefault().Rate / 100);
                                                    }

                                                    Data.TrnSalesLine newSalesLine = new Data.TrnSalesLine
                                                    {
                                                        SalesId = newSales.Id,
                                                        ItemId = currentItem.FirstOrDefault().Id,
                                                        UnitId = currentItem.FirstOrDefault().UnitId,
                                                        Price = item.Price,
                                                        DiscountId = discount.FirstOrDefault().Id,
                                                        DiscountRate = discount.FirstOrDefault().DiscountRate,
                                                        DiscountAmount = item.DiscountAmount,
                                                        NetPrice = item.NetPrice,
                                                        Quantity = item.Quantity,
                                                        Amount = item.Amount,
                                                        TaxId = tax.FirstOrDefault().Id,
                                                        TaxRate = tax.FirstOrDefault().Rate,
                                                        TaxAmount = taxAmount,
                                                        SalesAccountId = 159,
                                                        AssetAccountId = 255,
                                                        CostAccountId = 238,
                                                        TaxAccountId = 87,
                                                        SalesLineTimeStamp = DateTime.Now,
                                                        UserId = user.FirstOrDefault().Id,
                                                        Preparation = "",
                                                        IsPrepared = false,
                                                        Price1 = 0,
                                                        Price2 = 0,
                                                        Price2LessTax = 0,
                                                        PriceSplitPercentage = 0,
                                                    };

                                                    posdb.TrnSalesLines.InsertOnSubmit(newSalesLine);
                                                    posdb.SubmitChanges();

                                                    amount += item.Amount;

                                                    sysSettingsForm.logMessages(" > " + currentItem.FirstOrDefault().ItemDescription + " * " + item.Quantity.ToString("#,##0.00") + "\r\n\n");
                                                }
                                            }
                                        }
                                    }

                                    var sales = from d in posdb.TrnSales
                                                where d.Id == newSales.Id
                                                select d;

                                    if (sales.Any())
                                    {
                                        var updateSales = sales.FirstOrDefault();
                                        updateSales.Amount = amount;

                                        posdb.SubmitChanges();
                                    }

                                    sysSettingsForm.logMessages("Save Successful!\r\n\n");
                                    sysSettingsForm.logMessages("Time Stamp: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "\r\n\n");
                                    sysSettingsForm.logMessages("\r\n\n");
                                }
                                else
                                {
                                    sysSettingsForm.logMessages("Save Failed!\r\n\n");
                                    sysSettingsForm.logMessages("Error: Customer Not found.\r\n\n");
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
                sysSettingsForm.logMessages("Sales Order Error: " + e.Message + "\r\n\n");
                sysSettingsForm.logMessages("Time Stamp: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "\r\n\n");
                sysSettingsForm.logMessages("\r\n\n");

                return Task.FromResult("");
            }
        }
    }
}
