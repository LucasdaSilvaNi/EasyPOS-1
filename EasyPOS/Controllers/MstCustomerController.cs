using EasyPOS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Controllers
{
    class MstCustomerController
    {
        // ============
        // Data Context
        // ============
        public Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

        // =============
        // Customer List
        // =============
        public List<Entities.MstCustomerEntity> ListCustomer(String filter)
        {
            var customers = from d in db.MstCustomers
                            where d.Customer.Contains(filter)
                            || d.CustomerCode.Contains(filter)
                            || d.ContactNumber.Contains(filter)
                            || d.Address.Contains(filter)
                            select new Entities.MstCustomerEntity
                            {
                                Id = d.Id,
                                Customer = d.Customer,
                                Address = d.Address,
                                ContactPerson = d.ContactPerson,
                                ContactNumber = d.ContactNumber,
                                CreditLimit = d.CreditLimit,
                                TermId = d.TermId,
                                TIN = d.TIN,
                                WithReward = d.WithReward,
                                RewardNumber = d.RewardNumber,
                                RewardConversion = d.RewardConversion,
                                AvailableReward = d.AvailableReward,
                                AccountId = d.AccountId,
                                EntryUserId = d.EntryUserId,
                                EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                UpdateUserId = d.UpdateUserId,
                                UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                                IsLocked = d.IsLocked,
                                DefaultPriceDescription = d.DefaultPriceDescription,
                                CustomerCode = d.CustomerCode,
                                BusinessStyle = d.BusinessStyle,
                                LoadAmount = d.LoadAmount
                            };

            return customers.OrderByDescending(d => d.Id).ToList();
        }

        // ===============
        // Customer Detail
        // ===============
        public Entities.MstCustomerEntity DetailCustomer(Int32 id)
        {
            var customer = from d in db.MstCustomers
                           where d.Id == id
                           select new Entities.MstCustomerEntity
                           {
                               Id = d.Id,
                               Customer = d.Customer,
                               Address = d.Address,
                               ContactPerson = d.ContactPerson,
                               ContactNumber = d.ContactNumber,
                               CreditLimit = d.CreditLimit,
                               TermId = d.TermId,
                               TIN = d.TIN,
                               WithReward = d.WithReward,
                               RewardNumber = d.RewardNumber,
                               RewardConversion = d.RewardConversion,
                               AvailableReward = d.AvailableReward,
                               AccountId = d.AccountId,
                               EntryUserId = d.EntryUserId,
                               EntryUserUserName = d.MstUser.FullName,
                               EntryDateTime = d.EntryDateTime.ToShortDateString(),
                               EntryTime = d.EntryDateTime.ToShortTimeString(),
                               UpdateUserId = d.UpdateUserId,
                               UpdatedUserUserName = d.MstUser1.FullName,
                               UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                               UpdateTime = d.UpdateDateTime.ToShortTimeString(),
                               IsLocked = d.IsLocked,
                               DefaultPriceDescription = d.DefaultPriceDescription,
                               CustomerCode = d.CustomerCode,
                               BusinessStyle = d.BusinessStyle,
                               LoadAmount = d.LoadAmount
                           };

            return customer.FirstOrDefault();
        }

        // =================================
        // Customer Detail Per Customer Code
        // =================================
        public Entities.MstCustomerEntity DetailCustomerPerCustomerCode(String customerCode)
        {
            var customer = from d in db.MstCustomers
                           where d.CustomerCode == customerCode
                           && d.IsLocked == true
                           select new Entities.MstCustomerEntity
                           {
                               Id = d.Id,
                               Customer = d.Customer,
                               Address = d.Address,
                               ContactPerson = d.ContactPerson,
                               ContactNumber = d.ContactNumber,
                               CreditLimit = d.CreditLimit,
                               TermId = d.TermId,
                               TIN = d.TIN,
                               WithReward = d.WithReward,
                               RewardNumber = d.RewardNumber,
                               RewardConversion = d.RewardConversion,
                               AvailableReward = d.AvailableReward,
                               AccountId = d.AccountId,
                               EntryUserId = d.EntryUserId,
                               EntryDateTime = d.EntryDateTime.ToShortDateString(),
                               UpdateUserId = d.UpdateUserId,
                               UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                               IsLocked = d.IsLocked,
                               DefaultPriceDescription = d.DefaultPriceDescription,
                               CustomerCode = d.CustomerCode,
                               BusinessStyle = d.BusinessStyle,
                               LoadAmount = d.LoadAmount
                           };

            return customer.FirstOrDefault();
        }

        // ====================
        // Dropdown List - Term
        // ====================
        public List<Entities.MstTermEntity> DropdownListCustomerTerm()
        {
            var units = from d in db.MstTerms
                        select new Entities.MstTermEntity
                        {
                            Id = d.Id,
                            Term = d.Term
                        };

            return units.ToList();
        }

        // ============
        // Add Customer
        // ============
        public String[] AddCustomer()
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var term = from d in db.MstTerms select d;
                if (term.Any() == false)
                {
                    return new String[] { "Term not found.", "0" };
                }

                var account = from d in db.MstAccounts where d.Account == "Accounts Receivable - Sales" select d;
                if (account.Any() == false)
                {
                    return new String[] { "Account not found.", "0" };
                }

                Data.MstCustomer newCustomer = new Data.MstCustomer()
                {
                    Customer = "",
                    Address = "",
                    ContactPerson = "",
                    ContactNumber = "",
                    CreditLimit = 0,
                    TermId = term.FirstOrDefault().Id,
                    TIN = "",
                    WithReward = false,
                    RewardNumber = null,
                    RewardConversion = 0,
                    AvailableReward = 0,
                    AccountId = account.FirstOrDefault().Id,
                    EntryUserId = currentUserLogin.FirstOrDefault().Id,
                    EntryDateTime = DateTime.Now,
                    UpdateUserId = currentUserLogin.FirstOrDefault().Id,
                    UpdateDateTime = DateTime.Now,
                    IsLocked = false,
                    DefaultPriceDescription = null,
                    CustomerCode = null,
                    BusinessStyle = "",
                    LoadAmount = 0
                };

                db.MstCustomers.InsertOnSubmit(newCustomer);
                db.SubmitChanges();

                String newObject = Modules.SysAuditTrailModule.GetObjectString(newCustomer);

                Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                {
                    UserId = currentUserLogin.FirstOrDefault().Id,
                    AuditDate = DateTime.Now,
                    TableInformation = "MstCustomer",
                    RecordInformation = "",
                    FormInformation = newObject,
                    ActionInformation = "AddCustomer"
                };
                Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                return new String[] { "", newCustomer.Id.ToString() };
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // =============
        // Lock Customer
        // =============
        public String[] LockCustomer(Int32 id, Entities.MstCustomerEntity objCustomer)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var customerCode = from d in db.MstCustomers
                                   where d.CustomerCode == objCustomer.CustomerCode
                                   && d.IsLocked == true
                                   select d;

                if (customerCode.Any())
                {
                    return new String[] { "Customer Code already exist.", "0" };
                }

                var customerName = from d in db.MstCustomers
                                   where d.Customer == objCustomer.Customer
                                   && d.IsLocked == true
                                   select d;

                if (customerName.Any())
                {
                    return new String[] { "Customer already exist.", "0" };
                }

                var customer = from d in db.MstCustomers
                               where d.Id == id
                               select d;

                if (customer.Any())
                {
                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(customer.FirstOrDefault());

                    var lockCustomer = customer.FirstOrDefault();
                    lockCustomer.Customer = objCustomer.Customer;
                    lockCustomer.Address = objCustomer.Address;
                    lockCustomer.ContactPerson = objCustomer.ContactPerson;
                    lockCustomer.ContactNumber = objCustomer.ContactNumber;
                    lockCustomer.CreditLimit = Convert.ToDecimal(objCustomer.CreditLimit);
                    lockCustomer.TermId = objCustomer.TermId;
                    lockCustomer.TIN = objCustomer.TIN;
                    lockCustomer.WithReward = objCustomer.WithReward;
                    lockCustomer.RewardNumber = objCustomer.RewardNumber;
                    lockCustomer.RewardConversion = objCustomer.RewardConversion;
                    lockCustomer.AvailableReward = objCustomer.AvailableReward;
                    lockCustomer.AccountId = 64;
                    lockCustomer.UpdateUserId = currentUserLogin.FirstOrDefault().Id;
                    lockCustomer.UpdateDateTime = DateTime.Now;
                    lockCustomer.IsLocked = true;
                    lockCustomer.DefaultPriceDescription = objCustomer.DefaultPriceDescription;
                    lockCustomer.CustomerCode = objCustomer.CustomerCode;
                    lockCustomer.BusinessStyle = objCustomer.BusinessStyle;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(customer.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "MstCustomer",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "LockCustomer"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "" };
                }
                else
                {
                    return new String[] { "Customer not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ===============
        // Unlock Customer
        // ===============
        public String[] UnlockCustomer(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var customer = from d in db.MstCustomers
                               where d.Id == id
                               select d;

                if (customer.Any())
                {
                    if (customer.FirstOrDefault().CustomerCode.Equals("0000000001"))
                    {
                        return new String[] { "Unlock not allowed.", "0" };
                    }

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(customer.FirstOrDefault());

                    var unLockCustomer = customer.FirstOrDefault();
                    unLockCustomer.UpdateUserId = currentUserLogin.FirstOrDefault().Id;
                    unLockCustomer.UpdateDateTime = DateTime.Now;
                    unLockCustomer.IsLocked = false;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(customer.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "MstCustomer",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "UnlockCustomer"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);
                }

                return new String[] { "", "" };
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ===============
        // Delete Customer
        // ===============
        public String[] DeleteCustomer(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var customer = from d in db.MstCustomers
                               where d.Id == id
                               select d;

                if (customer.Any())
                {
                    if (customer.FirstOrDefault().CustomerCode.Equals("0000000001"))
                    {
                        return new String[] { "Delete not allowed.", "0" };
                    }

                    if (customer.FirstOrDefault().IsLocked == false)
                    {
                        var deleteCustomer = customer.FirstOrDefault();
                        db.MstCustomers.DeleteOnSubmit(deleteCustomer);

                        String oldObject = Modules.SysAuditTrailModule.GetObjectString(customer.FirstOrDefault());

                        Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                        {
                            UserId = currentUserLogin.FirstOrDefault().Id,
                            AuditDate = DateTime.Now,
                            TableInformation = "MstCustomer",
                            RecordInformation = oldObject,
                            FormInformation = "",
                            ActionInformation = "DeleteCustomer"
                        };
                        Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                        db.SubmitChanges();

                        return new String[] { "", "" };
                    }
                    else
                    {
                        return new String[] { "Customer is Locked", "0" };
                    }
                }
                else
                {
                    return new String[] { "Customer not found", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ===============
        // Import Customer
        // ===============
        public String[] ImportCustomer(List<MstCustomerEntity> objcustomerList)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }
                var term = from d in db.MstTerms select d;
                if (term.Any() == false)
                {
                    return new String[] { "Term not found.", "0" };
                }

                var account = from d in db.MstAccounts where d.Account == "Accounts Receivable - Sales" select d;
                if (account.Any() == false)
                {
                    return new String[] { "Account not found.", "0" };
                }
                if (objcustomerList.Any())
                {
                    foreach(var obj in objcustomerList)
                    {
                        Data.MstCustomer newCustomer = new Data.MstCustomer()
                        {
                            Customer = obj.Customer,
                            Address = obj.Address,
                            ContactPerson = "",
                            ContactNumber = obj.ContactNumber,
                            CreditLimit = 0,
                            TermId = term.FirstOrDefault().Id,
                            TIN = "",
                            WithReward = false,
                            RewardNumber = null,
                            RewardConversion = 0,
                            AvailableReward = 0,
                            AccountId = account.FirstOrDefault().Id,
                            EntryUserId = currentUserLogin.FirstOrDefault().Id,
                            EntryDateTime = DateTime.Today,
                            UpdateUserId = currentUserLogin.FirstOrDefault().Id,
                            UpdateDateTime = DateTime.Today,
                            IsLocked = false,
                            DefaultPriceDescription = null,
                            CustomerCode = obj.CustomerCode,
                            BusinessStyle = obj.BusinessStyle,
                            LoadAmount = 0
                        };

                        db.MstCustomers.InsertOnSubmit(newCustomer);
                        db.SubmitChanges();
                    }
                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Data source is empty.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

    }
}
