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
    class EasyPOSMstCustomerController
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
        public EasyPOSMstCustomerController(Forms.Software.SysSettings.SysSettingsForm form, String actDate)
        {
            sysSettingsForm = form;
            activityDate = actDate;
        }

        // =============
        // Sync Customer
        // =============
        public async void SyncCustomer(String apiUrlHost)
        {
            await GetCustomer(apiUrlHost);
        }

        // ============
        // Get Customer
        // ============
        public Task GetCustomer(String apiUrlHost)
        {
            try
            {
                DateTime dateTimeToday = DateTime.Now;
                String currentDate = Convert.ToDateTime(activityDate).ToString("MM-dd-yyyy", CultureInfo.InvariantCulture);

                // ============
                // Http Request
                // ============
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://" + apiUrlHost + "/api/get/POSIntegration/customer/" + currentDate);
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

                    List<Entities.EasyPOSMstCustomer> customerLists = (List<Entities.EasyPOSMstCustomer>)js.Deserialize(result, typeof(List<Entities.EasyPOSMstCustomer>));

                    if (customerLists.Any())
                    {
                        foreach (var customer in customerLists)
                        {
                            var terms = from d in posdb.MstTerms where d.Term.Equals(customer.Term) select d;
                            if (terms.Any())
                            {
                                var defaultSettings = from d in posdb.IntCloudSettings select d;

                                var currentCustomer = from d in posdb.MstCustomers where d.CustomerCode.Equals(customer.ManualArticleCode) && d.CustomerCode != null && d.IsLocked == true select d;
                                if (currentCustomer.Any())
                                {
                                    Boolean foundChanges = false;

                                    if (!foundChanges)
                                    {
                                        if (!currentCustomer.FirstOrDefault().Customer.Equals(customer.Article))
                                        {
                                            foundChanges = true;
                                        }
                                    }

                                    if (!foundChanges)
                                    {
                                        if (!currentCustomer.FirstOrDefault().Address.Equals(customer.Address))
                                        {
                                            foundChanges = true;
                                        }
                                    }

                                    if (!foundChanges)
                                    {
                                        if (!currentCustomer.FirstOrDefault().ContactPerson.Equals(customer.ContactPerson))
                                        {
                                            foundChanges = true;
                                        }
                                    }

                                    if (!foundChanges)
                                    {
                                        if (!currentCustomer.FirstOrDefault().ContactNumber.Equals(customer.ContactNumber))
                                        {
                                            foundChanges = true;
                                        }
                                    }

                                    if (!foundChanges)
                                    {
                                        if (!currentCustomer.FirstOrDefault().MstTerm.Term.Equals(customer.Term))
                                        {
                                            foundChanges = true;
                                        }
                                    }

                                    if (!foundChanges)
                                    {
                                        if (!currentCustomer.FirstOrDefault().TIN.Equals(customer.TaxNumber))
                                        {
                                            foundChanges = true;
                                        }
                                    }

                                    if (!foundChanges)
                                    {
                                        if (Convert.ToDecimal(currentCustomer.FirstOrDefault().CreditLimit) != Convert.ToDecimal(customer.CreditLimit))
                                        {
                                            foundChanges = true;
                                        }
                                    }

                                    if (foundChanges)
                                    {
                                        sysSettingsForm.logMessages("Updating Customer...\r\n\n");
                                        sysSettingsForm.logMessages("Customer Code: " + currentCustomer.FirstOrDefault().CustomerCode + "\r\n\n");
                                        sysSettingsForm.logMessages("Customer: " + currentCustomer.FirstOrDefault().Customer + "\r\n\n");

                                        var updateCustomer = currentCustomer.FirstOrDefault();
                                        updateCustomer.Customer = customer.Article;
                                        updateCustomer.Address = customer.Address;
                                        updateCustomer.ContactPerson = customer.ContactPerson;
                                        updateCustomer.ContactNumber = customer.ContactNumber;
                                        updateCustomer.CreditLimit = customer.CreditLimit;
                                        updateCustomer.TermId = terms.FirstOrDefault().Id;
                                        updateCustomer.TIN = customer.TaxNumber;
                                        updateCustomer.UpdateUserId = defaultSettings.FirstOrDefault().PostUserId;
                                        updateCustomer.UpdateDateTime = DateTime.Now;
                                        updateCustomer.CustomerCode = customer.ManualArticleCode;
                                        posdb.SubmitChanges();

                                        sysSettingsForm.logMessages("Update Successful!\r\n\n");
                                        sysSettingsForm.logMessages("Time Stamp: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "\r\n\n");
                                        sysSettingsForm.logMessages("\r\n\n");
                                    }
                                }
                                else
                                {
                                    sysSettingsForm.logMessages("Saving Customer...\r\n\n");
                                    sysSettingsForm.logMessages("Customer Code: " + customer.ManualArticleCode + "\r\n\n");
                                    sysSettingsForm.logMessages("Customer: " + customer.Article + "\r\n\n");

                                    Data.MstCustomer newCustomer = new Data.MstCustomer
                                    {
                                        Customer = customer.Article,
                                        Address = customer.Address,
                                        ContactPerson = customer.ContactPerson,
                                        ContactNumber = customer.ContactNumber,
                                        CreditLimit = customer.CreditLimit,
                                        TermId = terms.FirstOrDefault().Id,
                                        TIN = customer.TaxNumber,
                                        WithReward = false,
                                        RewardNumber = null,
                                        RewardConversion = 4,
                                        AccountId = 64,
                                        EntryUserId = defaultSettings.FirstOrDefault().PostUserId,
                                        EntryDateTime = DateTime.Now,
                                        UpdateUserId = defaultSettings.FirstOrDefault().PostUserId,
                                        UpdateDateTime = DateTime.Now,
                                        IsLocked = true,
                                        DefaultPriceDescription = null,
                                        CustomerCode = customer.ManualArticleCode,
                                    };

                                    posdb.MstCustomers.InsertOnSubmit(newCustomer);
                                    posdb.SubmitChanges();

                                    sysSettingsForm.logMessages("Save Successful!\r\n\n");
                                    sysSettingsForm.logMessages("Time Stamp: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "\r\n\n");
                                    sysSettingsForm.logMessages("\r\n\n");
                                }
                            }
                            else
                            {
                                sysSettingsForm.logMessages("Customer Integration Failed!\r\n\n");
                                sysSettingsForm.logMessages("Customer Code: " + customer.ManualArticleCode + "\r\n\n");
                                sysSettingsForm.logMessages("Customer: " + customer.Article + "\r\n\n");
                                sysSettingsForm.logMessages("Error: Term Mismatch.\r\n\n");
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
                sysSettingsForm.logMessages("Customer Integration Failed!\r\n\n");
                sysSettingsForm.logMessages("Error: " + e.Message + "\r\n\n");
                sysSettingsForm.logMessages("Time Stamp: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "\r\n\n");
                sysSettingsForm.logMessages("\r\n\n");

                return Task.FromResult("");
            }
        }
    }
}