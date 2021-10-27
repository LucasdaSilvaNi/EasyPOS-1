using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Controllers
{
    class TrnCollectionController
    {
        // ============
        // Data Context
        // ============
        public Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());
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
        // ================================
        // Dropdown List Collection Number
        // ================================
        public List<Entities.TrnCollectionEntity> DropdownListCollectionNumber()
        {
            var collection = from d in db.TrnCollections
                             select new Entities.TrnCollectionEntity
                             {
                                 CollectionNumber = d.CollectionNumber
                             };

            return collection.ToList();
        }

        // ================================
        // Dropdown List Period
        // ================================
        public List<Entities.MstPeriodEntity> DropdownListCollectionPeriodId()
        {
            var period = from d in db.MstPeriods
                         select new Entities.MstPeriodEntity
                         {
                             Id = d.Id,
                             Period = d.Period
                         };

            return period.ToList();
        }
        // ================================
        // Dropdown List Manual OR Number
        // ================================
        public List<Entities.TrnCollectionEntity> DropdownListCollectionManualORNumber()
        {
            var ManualORNumber = from d in db.TrnCollections
                                 select new Entities.TrnCollectionEntity
                                 {
                                     ManualORNumber = d.ManualORNumber
                                 };

            return ManualORNumber.ToList();
        }
        // ================================
        // Dropdown List Terminal
        // ================================
        public List<Entities.MstTerminalEntity> DropdownListCollectionTerminalId()
        {
            var Terminal = from d in db.MstTerminals
                           select new Entities.MstTerminalEntity
                           {
                               Id = d.Id,
                               Terminal = d.Terminal
                           };

            return Terminal.ToList();
        }
        // ================================
        // Dropdown List Customer
        // ================================
        public List<Entities.MstCustomerEntity> DropdownListCollectionCustomer()
        {
            var customer = from d in db.MstCustomers
                           select new Entities.MstCustomerEntity
                           {
                               Id = d.Id,
                               Customer = d.Customer
                           };

            return customer.ToList();
        }

        // ==========================
        // Dropdown List Sales Number
        // ==========================
        public List<Entities.TrnSalesEntity> DropdownListCollectionSalesNumberByCustomer(Int32 customerId, Int32 terminalId)
        {
            var salesNumber = from d in db.TrnSales
                              where d.CustomerId == customerId
                              && d.TerminalId == terminalId
                              && d.IsLocked == true
                              && d.BalanceAmount > 0 
                              select new Entities.TrnSalesEntity
                              {
                                  Id = d.Id,
                                  SalesNumber = d.SalesNumber,
                                  Amount = d.Amount,
                                  PaidAmount = d.PaidAmount,
                                  BalanceAmount = d.BalanceAmount
                              };

            return salesNumber.ToList();
        }

        // ================================
        // Dropdown List Sales Number
        // ================================
        public List<Entities.TrnSalesEntity> DropdownListCollectionSalesBalanceAmount()
        {
            var salesBalance = from d in db.TrnSales
                               select new Entities.TrnSalesEntity
                               {
                                   BalanceAmount = d.BalanceAmount
                               };

            return salesBalance.ToList();
        }
        // ================================
        // Dropdown List Amount
        // ================================
        public List<Entities.TrnSalesEntity> DropdownListCollectionSalesAmount()
        {
            var Amount = from d in db.TrnSales
                         select new Entities.TrnSalesEntity
                         {
                             Amount = d.Amount
                         };

            return Amount.ToList();
        }
        // ====================
        // Dropdown List - User
        // ====================
        public List<Entities.MstUserEntity> DropdownListCollectionUser()
        {
            var users = from d in db.MstUsers
                        select new Entities.MstUserEntity
                        {
                            Id = d.Id,
                            FullName = d.FullName
                        };

            return users.ToList();
        }
        // ===================
        // List Collection
        // ===================
        public List<Entities.TrnCollectionEntity> ListCollection(DateTime dateFilter, String filter)
        {
            var collection = from d in db.TrnCollections
                             where d.CollectionDate == dateFilter
                             && d.CollectionNumber.Contains(filter)
                             select new Entities.TrnCollectionEntity
                             {
                                 Id = d.Id,
                                 PeriodId = d.PeriodId,
                                 CollectionDate = d.CollectionDate.ToShortDateString(),
                                 CollectionNumber = d.CollectionNumber,
                                 TerminalId = d.TerminalId,
                                 Terminal = d.MstTerminal.Terminal,
                                 CancelledCollectionNumber = d.CancelledCollectionNumber,
                                 ManualORNumber = d.ManualORNumber,
                                 CustomerId = d.CustomerId,
                                 Customer = d.MstCustomer.Customer,
                                 CustomerCode = d.MstCustomer.CustomerCode,
                                 Remarks = d.Remarks,
                                 SalesId = d.SalesId,
                                 SalesBalanceAmount = d.SalesBalanceAmount,
                                 Amount = d.Amount,
                                 TenderAmount = d.TenderAmount,
                                 ChangeAmount = d.ChangeAmount,
                                 PreparedBy = d.PreparedBy,
                                 CheckedBy = d.CheckedBy,
                                 ApprovedBy = d.ApprovedBy,
                                 IsLocked = d.IsLocked,
                                 EntryUserId = d.EntryUserId,
                                 EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                 UpdateUserId = d.UpdateUserId,
                                 UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                             };
            return collection.OrderByDescending(d => d.Id).ToList();
        }
        // ========================
        // Detail Collection Detail
        // ========================
        public Entities.TrnCollectionEntity DetailCollection(Int32 id)
        {
            var collections = from d in db.TrnCollections
                              where d.Id == id
                              select new Entities.TrnCollectionEntity
                              {
                                  Id = d.Id,
                                  PeriodId = d.PeriodId,
                                  CollectionDate = d.CollectionDate.ToShortDateString(),
                                  CollectionNumber = d.CollectionNumber,
                                  TerminalId = d.TerminalId,
                                  Terminal = d.MstTerminal.Terminal,
                                  CancelledCollectionNumber = d.CancelledCollectionNumber,
                                  ManualORNumber = d.ManualORNumber,
                                  CustomerId = d.CustomerId,
                                  Customer = d.MstCustomer.Customer,
                                  CustomerCode = d.MstCustomer.CustomerCode,
                                  Remarks = d.Remarks,
                                  SalesId = d.SalesId,
                                  SalesBalanceAmount = d.SalesBalanceAmount,
                                  Amount = d.Amount,
                                  TenderAmount = d.TenderAmount,
                                  ChangeAmount = d.ChangeAmount,
                                  PreparedBy = d.PreparedBy,
                                  CheckedBy = d.CheckedBy,
                                  ApprovedBy = d.ApprovedBy,
                                  IsLocked = d.IsLocked,
                                  EntryUserId = d.EntryUserId,
                                  EntryUserName = d.MstUser3.FullName,
                                  EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                  EntryTime = d.EntryDateTime.ToShortTimeString(),
                                  UpdateUserId = d.UpdateUserId,
                                  UpdatedUserName = d.MstUser4.FullName,
                                  UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                                  UpdateTime = d.UpdateDateTime.ToShortTimeString()
                              };
            return collections.FirstOrDefault();
        }

        // ==================
        // Add Collection
        // ==================
        public String[] AddCollection()
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var period = from d in db.MstPeriods select d;
                if (period.Any() == false)
                {
                    return new String[] { "Periods not found.", "0" };
                }
                var terminal = from d in db.MstTerminals select d;
                if (terminal.Any() == false)
                {
                    return new String[] { "Terminal not found.", "0" };
                }

                var customer = from d in db.MstCustomers select d;
                if (customer.Any() == false)
                {
                    return new String[] { "Customer not found.", "0" };
                }
                var sales = from d in db.TrnSales select d;
                if (sales.Any() == false)
                {
                    return new String[] { "Customer not found.", "0" };

                }
                DateTime currentDate = DateTime.Today;
                if (Modules.SysCurrentModule.GetCurrentSettings().IsLoginDate == true)
                {
                    currentDate = Convert.ToDateTime(Modules.SysCurrentModule.GetCurrentSettings().CurrentDate);
                }

                String CollectionNumber = "0000000001";
                var lastCollection = from d in db.TrnCollections.OrderByDescending(d => d.Id) select d;
                if (lastCollection.Any())
                {
                    Int32 newCollection = Convert.ToInt32(lastCollection.FirstOrDefault().CollectionNumber) + 1;
                    CollectionNumber = FillLeadingZeroes(newCollection, 10);
                }
                String CancelledCollectionNumber = "0000000001";
                var lastCancelledCollection = from d in db.TrnCollections.OrderByDescending(d => d.Id) select d;
                if (lastCancelledCollection.Any())
                {
                    Int32 newCancelledCollection = Convert.ToInt32(lastCancelledCollection.FirstOrDefault().CollectionNumber) + 1;
                    CancelledCollectionNumber = FillLeadingZeroes(newCancelledCollection, 10);
                }

                Data.TrnCollection newCollections = new Data.TrnCollection()
                {
                    PeriodId = period.FirstOrDefault().Id,
                    CollectionDate = currentDate,
                    CollectionNumber = CollectionNumber,
                    TerminalId = terminal.FirstOrDefault().Id,
                    CancelledCollectionNumber = "",
                    ManualORNumber = "",
                    CustomerId = customer.FirstOrDefault().Id,
                    Remarks = "",
                    SalesId = sales.FirstOrDefault().Id,
                    SalesBalanceAmount = 0,
                    Amount = 0,
                    TenderAmount = 0,
                    ChangeAmount = 0,
                    PreparedBy = currentUserLogin.FirstOrDefault().Id,
                    CheckedBy = currentUserLogin.FirstOrDefault().Id,
                    ApprovedBy = currentUserLogin.FirstOrDefault().Id,
                    IsCancelled = false,
                    IsLocked = false,
                    EntryUserId = currentUserLogin.FirstOrDefault().Id,
                    EntryDateTime = DateTime.Now,
                    UpdateUserId = currentUserLogin.FirstOrDefault().Id,
                    UpdateDateTime = DateTime.Now,
                };
                db.TrnCollections.InsertOnSubmit(newCollections);
                db.SubmitChanges();

                String newObject = Modules.SysAuditTrailModule.GetObjectString(newCollections);

                Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                {
                    UserId = currentUserLogin.FirstOrDefault().Id,
                    AuditDate = DateTime.Now,
                    TableInformation = "TrnCollection",
                    RecordInformation = "",
                    FormInformation = newObject,
                    ActionInformation = "AddCollection"
                };
                Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                return new String[] { "", newCollections.Id.ToString() };
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // =====================
        // Delete Collection
        // =====================
        public String[] DeleteCollection(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var collection = from d in db.TrnCollections
                                 where d.Id == id
                                 select d;

                if (collection.Any())
                {
                    if (collection.FirstOrDefault().IsLocked)
                    {
                        return new String[] { "Purchase Order is locked", "0" };
                    }

                    var deletecollection = collection.FirstOrDefault();
                    db.TrnCollections.DeleteOnSubmit(deletecollection);

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(collection.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "TrnStockIn",
                        RecordInformation = oldObject,
                        FormInformation = "",
                        ActionInformation = "DeleteStockIn"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    db.SubmitChanges();

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Collection not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }
        // ===================
        // Lock Collection
        // ===================
        public String[] LockCollection(Int32 id, Entities.TrnCollectionEntity objCollection)
        {
            try
            {

                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var period = from d in db.MstPeriods
                             where d.Id == objCollection.PeriodId
                             select d;

                if (period.Any() == false)
                {
                    return new String[] { "Period not found.", "0" };
                }

                var terminal = from d in db.MstTerminals
                               where d.Id == Convert.ToInt32(objCollection.TerminalId)
                               select d;

                if (terminal.Any() == false)
                {
                    return new String[] { "Terminal Id not found.", "0" };
                }

                var customer = from d in db.MstCustomers
                               where d.Id == Convert.ToInt32(objCollection.CustomerId)
                               select d;
                if (customer.Any() == false)
                {
                    return new String[] { "Customer Id not found.", "0" };
                }

                if (objCollection.SalesId != null)
                {
                    var salesNumber = from d in db.TrnSales
                                      where d.Id == objCollection.SalesId
                                      select d;

                    if (salesNumber.Any() == false)
                    {
                        return new String[] { "Sales Number not found.", "0" };
                    }
                }

                var preparedByUser = from d in db.MstUsers
                                     where d.Id == objCollection.PreparedBy
                                     && d.IsLocked == true
                                     select d;

                if (preparedByUser.Any() == false)
                {
                    return new String[] { "Prepared by user not found.", "0" };
                }
                var checkedByUser = from d in db.MstUsers
                                    where d.Id == objCollection.CheckedBy
                                    && d.IsLocked == true
                                    select d;

                if (checkedByUser.Any() == false)
                {
                    return new String[] { "Checked by user not found.", "0" };
                }

                var approvedByUser = from d in db.MstUsers
                                     where d.Id == objCollection.ApprovedBy
                                     && d.IsLocked == true
                                     select d;

                if (approvedByUser.Any() == false)
                {
                    return new String[] { "Approved by user not found.", "0" };
                }

                var collection = from d in db.TrnCollections
                                 where d.Id == id
                                 select d;

                if (collection.Any())
                {
                    if (collection.FirstOrDefault().IsLocked)
                    {
                        return new String[] { "Already locked.", "0" };
                    }

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(collection.FirstOrDefault());

                    var lockCollection = collection.FirstOrDefault();
                    lockCollection.PeriodId = objCollection.PeriodId;
                    lockCollection.CollectionDate = Convert.ToDateTime(objCollection.CollectionDate);
                    lockCollection.ManualORNumber = objCollection.ManualORNumber;
                    lockCollection.TerminalId = objCollection.TerminalId;
                    lockCollection.CustomerId = objCollection.CustomerId;
                    lockCollection.SalesId = objCollection.SalesId;
                    lockCollection.SalesBalanceAmount = objCollection.SalesBalanceAmount;
                    lockCollection.Amount = objCollection.Amount;
                    lockCollection.Remarks = objCollection.Remarks;
                    lockCollection.PreparedBy = objCollection.PreparedBy;
                    lockCollection.CheckedBy = objCollection.CheckedBy;
                    lockCollection.ApprovedBy = objCollection.ApprovedBy;
                    lockCollection.IsCancelled = false; ;
                    lockCollection.IsLocked = true;
                    lockCollection.UpdateUserId = currentUserLogin.FirstOrDefault().Id;
                    lockCollection.UpdateDateTime = DateTime.Now;
                    db.SubmitChanges();

                    UpdateAccountsReceivable(objCollection.SalesId);

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(collection.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "TrnCollection",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "LockCollectionLine"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Collection not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        public void UpdateAccountsReceivable(Int32? salesId)
        {
            if (salesId != null)
            {
                var sales = from d in db.TrnSales
                            where d.Id == salesId
                            select d;

                if (sales.Any())
                {
                    Decimal totalCollectionLineAmount = 0;

                    var collectionLines = from d in db.TrnCollectionLines
                                          where d.TrnCollection.SalesId == salesId
                                          && d.TrnCollection.IsLocked == true
                                          select d;

                    if (collectionLines.Any())
                    {
                        totalCollectionLineAmount = collectionLines.Sum(d => d.Amount);
                    }

                    var updateSales = sales.FirstOrDefault();
                    updateSales.PaidAmount = totalCollectionLineAmount;
                    updateSales.BalanceAmount = sales.FirstOrDefault().Amount - totalCollectionLineAmount;
                    db.SubmitChanges();
                }
            }
        }

        // =================
        // Unlock Collection
        // =================
        public String[] UnlockCollection(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var collection = from d in db.TrnCollections
                                 where d.Id == id
                                 select d;

                if (collection.Any())
                {
                    if (collection.FirstOrDefault().IsLocked == false)
                    {
                        return new String[] { "Already unlocked.", "0" };
                    }

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(collection.FirstOrDefault());

                    var unlockCollection = collection.FirstOrDefault();
                    unlockCollection.IsLocked = false;
                    unlockCollection.UpdateUserId = currentUserLogin.FirstOrDefault().Id;
                    unlockCollection.UpdateDateTime = DateTime.Now;
                    db.SubmitChanges();

                    UpdateAccountsReceivable(collection.FirstOrDefault().SalesId);

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(collection.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "TrnCollectionLine",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "UnlockCollection"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Collection not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

    }
}
