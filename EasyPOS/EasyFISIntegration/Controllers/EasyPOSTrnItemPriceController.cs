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
    class EasyPOSTrnItemPriceController
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
        public EasyPOSTrnItemPriceController(Forms.Software.SysSettings.SysSettingsForm form, String actDate)
        {
            sysSettingsForm = form;
            activityDate = actDate;
        }

        // ===============
        // Sync Item Price
        // ===============
        public async void SyncItemPrice(String apiUrlHost, String branchCode)
        {
            await GetItemPrice(apiUrlHost, branchCode);
        }

        // ==============
        // Get Item Price
        // ==============
        public Task GetItemPrice(String apiUrlHost, String branchCode)
        {
            try
            {
                DateTime dateTimeToday = DateTime.Now;
                String currentDate = Convert.ToDateTime(activityDate).ToString("MM-dd-yyyy", CultureInfo.InvariantCulture);

                // ============
                // Http Request
                // ============
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://" + apiUrlHost + "/api/get/POSIntegration/itemPrice/" + branchCode + "/" + currentDate);
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
                    List<Entities.EasyPOSTrnArticlePrice> itemPriceLists = (List<Entities.EasyPOSTrnArticlePrice>)js.Deserialize(result, typeof(List<Entities.EasyPOSTrnArticlePrice>));

                    if (itemPriceLists.Any())
                    {
                        foreach (var itemPrice in itemPriceLists)
                        {
                            var currentItem = from d in posdb.MstItems where d.BarCode.Equals(itemPrice.ItemCode) && d.IsLocked == true select d;
                            if (currentItem.Any())
                            {
                                var currentItemPrice = from d in posdb.MstItemPrices where d.ItemId == currentItem.FirstOrDefault().Id && d.PriceDescription.Equals("IP-" + itemPrice.BranchCode + "-" + itemPrice.IPNumber + " (" + itemPrice.IPDate + ")") select d;
                                if (!currentItemPrice.Any())
                                {
                                    sysSettingsForm.logMessages("Saving Item Price...\r\n\n");
                                    sysSettingsForm.logMessages("IP Number (Price Description): " + "IP - " + itemPrice.BranchCode + " - " + itemPrice.IPNumber + "(" + itemPrice.IPDate + ")\r\n\n");

                                    Data.MstItemPrice newPrice = new Data.MstItemPrice()
                                    {
                                        ItemId = currentItem.FirstOrDefault().Id,
                                        PriceDescription = "IP-" + itemPrice.BranchCode + "-" + itemPrice.IPNumber + " (" + itemPrice.IPDate + ")",
                                        Price = itemPrice.Price,
                                        TriggerQuantity = itemPrice.TriggerQuantity
                                    };

                                    posdb.MstItemPrices.InsertOnSubmit(newPrice);

                                    var updateCurrentItem = currentItem.FirstOrDefault();
                                    updateCurrentItem.Price = itemPrice.Price;
                                    posdb.SubmitChanges();

                                    sysSettingsForm.logMessages("Save Successful!\r\n\n");
                                    sysSettingsForm.logMessages("Time Stamp: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "\r\n\n");
                                    sysSettingsForm.logMessages("\r\n\n");
                                }
                            }
                            else
                            {
                                sysSettingsForm.logMessages("Item Price Integration Failed!\r\n\n");
                                sysSettingsForm.logMessages("IP Number (Price Description): " + "IP - " + itemPrice.BranchCode + " - " + itemPrice.IPNumber + "(" + itemPrice.IPDate + ")\r\n\n");
                                sysSettingsForm.logMessages("Error: Item Description Not Found.\r\n\n");
                                sysSettingsForm.logMessages("Time Stamp: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "\r\n\n");
                                sysSettingsForm.logMessages("\r\n\n");
                            }
                        }
                    }
                }

                return Task.FromResult("");
            }
            catch (Exception e)
            {
                sysSettingsForm.logMessages("Item Price Integration Failed!\r\n\n");
                sysSettingsForm.logMessages("Error: " + e.Message + "\r\n\n");
                sysSettingsForm.logMessages("Time Stamp: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "\r\n\n");
                sysSettingsForm.logMessages("\r\n\n");

                return Task.FromResult("");
            }
        }
    }
}