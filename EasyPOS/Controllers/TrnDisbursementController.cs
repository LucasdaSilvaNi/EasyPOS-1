using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Controllers
{
    class TrnDisbursementController
    {
        // ============
        // Data Context
        // ============
        public Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

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

        // =================
        // Disbursement List
        // =================
        public List<Entities.TrnDisbursementEntity> ListDisbursement(DateTime dateFilter, String filter)
        {
            var disbursements = from d in db.TrnDisbursements
                                where d.DisbursementDate == dateFilter
                                && d.DisbursementNumber.Contains(filter)
                                select new Entities.TrnDisbursementEntity
                                {
                                    Id = d.Id,
                                    PeriodId = d.PeriodId,
                                    DisbursementDate = d.DisbursementDate.ToShortDateString(),
                                    DisbursementNumber = d.DisbursementNumber,
                                    DisbursementType = d.DisbursementType,
                                    AccountId = d.AccountId,
                                    Account = d.MstAccount.Account,
                                    Amount = d.Amount,
                                    PayTypeId = d.PayTypeId,
                                    PayType = d.MstPayType.PayType,
                                    TerminalId = d.TerminalId,
                                    Terminal = d.MstTerminal.Terminal,
                                    Remarks = d.Remarks,
                                    IsRefund = d.IsRefund,
                                    RefundSalesId = d.RefundSalesId,
                                    RefundSalesNumber = d.TrnSale.SalesNumber,
                                    StockInId = d.StockInId,
                                    PreparedBy = d.PreparedBy,
                                    CheckedBy = d.CheckedBy,
                                    ApprovedBy = d.ApprovedBy,
                                    IsLocked = d.IsLocked,
                                    EntryUserId = d.EntryUserId,
                                    EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                    UpdateUserId = d.UpdateUserId,
                                    UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                                    Amount1000 = d.Amount1000,
                                    Amount500 = d.Amount500,
                                    Amount200 = d.Amount200,
                                    Amount100 = d.Amount100,
                                    Amount50 = d.Amount50,
                                    Amount20 = d.Amount20,
                                    Amount10 = d.Amount10,
                                    Amount5 = d.Amount5,
                                    Amount1 = d.Amount1,
                                    Amount025 = d.Amount025,
                                    Amount010 = d.Amount010,
                                    Amount005 = d.Amount005,
                                    Amount001 = d.Amount001,
                                    Payee = d.Payee
                                };

            return disbursements.OrderByDescending(d => d.Id).ToList();
        }

        // ===================
        // Disbursement Detail
        // ===================
        public Entities.TrnDisbursementEntity DetailDisbursement(Int32 id)
        {
            var disbursement = from d in db.TrnDisbursements
                               where d.Id == id
                               select new Entities.TrnDisbursementEntity
                               {
                                   Id = d.Id,
                                   PeriodId = d.PeriodId,
                                   DisbursementDate = d.DisbursementDate.ToShortDateString(),
                                   DisbursementNumber = d.DisbursementNumber,
                                   DisbursementType = d.DisbursementType,
                                   AccountId = d.AccountId,
                                   Account = d.MstAccount.Account,
                                   Amount = d.Amount,
                                   PayTypeId = d.PayTypeId,
                                   PayType = d.MstPayType.PayType,
                                   TerminalId = d.TerminalId,
                                   Terminal = d.MstTerminal.Terminal,
                                   Remarks = d.Remarks,
                                   IsRefund = d.IsRefund,
                                   RefundSalesId = d.RefundSalesId,
                                   RefundSalesNumber = d.TrnSale.SalesNumber,
                                   StockInId = d.StockInId,
                                   PreparedBy = d.PreparedBy,
                                   CheckedBy = d.CheckedBy,
                                   ApprovedBy = d.ApprovedBy,
                                   IsLocked = d.IsLocked,
                                   EntryUserId = d.EntryUserId,
                                   EntryUsername = d.MstUser3.UserName,
                                   EntryUserUsername = d.MstUser3.FullName,
                                   EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                   EntryTime = d.EntryDateTime.ToShortTimeString(),
                                   UpdateUserId = d.UpdateUserId,
                                   UpdateUsername = d.MstUser4.UserName,
                                   UpdateUserUsername = d.MstUser4.FullName,
                                   UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                                   UpdateTime = d.UpdateDateTime.ToShortTimeString(),
                                   Amount1000 = d.Amount1000,
                                   Amount500 = d.Amount500,
                                   Amount200 = d.Amount200,
                                   Amount100 = d.Amount100,
                                   Amount50 = d.Amount50,
                                   Amount20 = d.Amount20,
                                   Amount10 = d.Amount10,
                                   Amount5 = d.Amount5,
                                   Amount1 = d.Amount1,
                                   Amount025 = d.Amount025,
                                   Amount010 = d.Amount010,
                                   Amount005 = d.Amount005,
                                   Amount001 = d.Amount001,
                                   Payee = d.Payee
                               };

            return disbursement.FirstOrDefault();
        }

        // ========================
        // Dropdown List - Terminal
        // ========================
        public List<Entities.MstTerminalEntity> DropdownListDisbursementTerminal()
        {
            var terminals = from d in db.MstTerminals
                            select new Entities.MstTerminalEntity
                            {
                                Id = d.Id,
                                Terminal = d.Terminal
                            };

            return terminals.ToList();
        }

        // ================================
        // Dropdown List - Remmittance Type
        // ================================
        public List<String> DropdownListDisbursementRemittanceType()
        {
            return new List<String>()
            {
                "DEBIT", "CREDIT"
            };
        }

        // =======================
        // Dropdown List - PayType
        // =======================
        public List<Entities.MstPayTypeEntity> DropdownListDisbursementPayType()
        {
            var payTypes = from d in db.MstPayTypes
                           select new Entities.MstPayTypeEntity
                           {
                               Id = d.Id,
                               PayType = d.PayType
                           };

            return payTypes.ToList();
        }

        // =======================
        // Dropdown List - Account
        // =======================
        public List<Entities.MstAccountEntity> DropdownListDisbursementAccount()
        {
            var accounts = from d in db.MstAccounts
                           select new Entities.MstAccountEntity
                           {
                               Id = d.Id,
                               Account = d.Account
                           };

            return accounts.ToList();
        }

        // ===============================
        // Dropdown List - Return Stock-In
        // ===============================
        public List<Entities.TrnStockInEntity> DropdownListDisbursementReturnStockIn()
        {
            var stockIns = from d in db.TrnStockIns
                           where d.IsReturn == true
                           && d.IsLocked == true
                           select new Entities.TrnStockInEntity
                           {
                               Id = d.Id,
                               StockInNumber = d.StockInNumber
                           };

            return stockIns.ToList();
        }

        // ====================
        // Dropdown List - User
        // ====================
        public List<Entities.MstUserEntity> DropdownListDisbursementUser()
        {
            var users = from d in db.MstUsers
                        select new Entities.MstUserEntity
                        {
                            Id = d.Id,
                            FullName = d.FullName
                        };

            return users.ToList();
        }

        // ================
        // Add Disbursement
        // ================
        public String[] AddDisbursement()
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

                var account = from d in db.MstAccounts select d;
                if (account.Any() == false)
                {
                    return new String[] { "Account not found.", "0" };
                }

                var payType = from d in db.MstPayTypes select d;
                if (payType.Any() == false)
                {
                    return new String[] { "Paytype not found.", "0" };
                }

                var terminal = from d in db.MstTerminals where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().TerminalId) select d;
                if (terminal.Any() == false)
                {
                    return new String[] { "Terminal not found.", "0" };
                }

                DateTime currentDate = DateTime.Today;
                if (Modules.SysCurrentModule.GetCurrentSettings().IsLoginDate == true)
                {
                    currentDate = Convert.ToDateTime(Modules.SysCurrentModule.GetCurrentSettings().CurrentDate);
                }

                String disbursementNumber = "0000000001";
                var lastDisbursement = from d in db.TrnDisbursements.OrderByDescending(d => d.Id) select d;
                if (lastDisbursement.Any())
                {
                    Int32 newDisbursementNumber = Convert.ToInt32(lastDisbursement.FirstOrDefault().DisbursementNumber) + 1;
                    disbursementNumber = FillLeadingZeroes(newDisbursementNumber, 10);
                }

                Data.TrnDisbursement newDisbursement = new Data.TrnDisbursement()
                {
                    PeriodId = period.FirstOrDefault().Id,
                    DisbursementDate = currentDate,
                    DisbursementNumber = disbursementNumber,
                    DisbursementType = "",
                    AccountId = account.FirstOrDefault().Id,
                    Amount = 0,
                    PayTypeId = payType.FirstOrDefault().Id,
                    TerminalId = terminal.FirstOrDefault().Id,
                    Remarks = "",
                    IsRefund = false,
                    RefundSalesId = null,
                    StockInId = null,
                    PreparedBy = currentUserLogin.FirstOrDefault().Id,
                    CheckedBy = currentUserLogin.FirstOrDefault().Id,
                    ApprovedBy = currentUserLogin.FirstOrDefault().Id,
                    IsLocked = false,
                    EntryUserId = currentUserLogin.FirstOrDefault().Id,
                    EntryDateTime = DateTime.Now,
                    UpdateUserId = currentUserLogin.FirstOrDefault().Id,
                    UpdateDateTime = DateTime.Now,
                    Amount1000 = null,
                    Amount500 = null,
                    Amount200 = null,
                    Amount100 = null,
                    Amount50 = null,
                    Amount20 = null,
                    Amount10 = null,
                    Amount5 = null,
                    Amount1 = null,
                    Amount025 = null,
                    Amount010 = null,
                    Amount005 = null,
                    Amount001 = null,
                    Payee = ""
                };

                db.TrnDisbursements.InsertOnSubmit(newDisbursement);
                db.SubmitChanges();

                String newObject = Modules.SysAuditTrailModule.GetObjectString(newDisbursement);

                Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                {
                    UserId = currentUserLogin.FirstOrDefault().Id,
                    AuditDate = DateTime.Now,
                    TableInformation = "TrnDisbursement",
                    RecordInformation = "",
                    FormInformation = newObject,
                    ActionInformation = "AddDisbursement"
                };
                Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                return new String[] { "", newDisbursement.Id.ToString() };
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // =================
        // Lock Disbursement
        // =================
        public String[] LockDisbursement(Int32 id, Entities.TrnDisbursementEntity objDisbursement)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var payType = from d in db.MstPayTypes where d.Id == objDisbursement.PayTypeId select d;
                if (payType.Any() == false)
                {
                    return new String[] { "Paytype not found.", "0" };
                }

                Int32? stockInId = null;

                if (objDisbursement.StockInId != null)
                {
                    var stockIn = from d in db.TrnStockIns
                                  where d.Id == objDisbursement.StockInId
                                  select d;

                    if (stockIn.Any())
                    {
                        stockInId = stockIn.FirstOrDefault().Id;
                    }
                }

                var checkedByUser = from d in db.MstUsers
                                    where d.Id == objDisbursement.CheckedBy
                                    && d.IsLocked == true
                                    select d;

                if (checkedByUser.Any() == false)
                {
                    return new String[] { "Checked by user not found.", "0" };
                }

                var approvedByUser = from d in db.MstUsers
                                     where d.Id == objDisbursement.ApprovedBy
                                     && d.IsLocked == true
                                     select d;

                if (approvedByUser.Any() == false)
                {
                    return new String[] { "Approved by user not found.", "0" };
                }

                var disbursement = from d in db.TrnDisbursements
                                   where d.Id == id
                                   select d;

                if (disbursement.Any())
                {
                    if (disbursement.FirstOrDefault().IsLocked)
                    {
                        return new String[] { "Already locked.", "0" };
                    }

                    Int32? refundSalesId = null;
                    if (objDisbursement.IsRefund == true)
                    {
                        var sales = from d in db.TrnSales
                                    where d.SalesNumber == objDisbursement.RefundSalesNumber
                                    && d.IsLocked == true
                                    && d.IsReturned == true
                                    && d.ReturnApplication == "Return"
                                    select d;

                        if (sales.Any() == false)
                        {
                            return new String[] { "Sales not found.", "0" };
                        }

                        refundSalesId = sales.FirstOrDefault().Id;

                        var updateSales = sales.FirstOrDefault();
                        updateSales.ReturnApplication = "Refund";
                        updateSales.UpdateUserId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId);
                        updateSales.UpdateDateTime = DateTime.Now;
                        db.SubmitChanges();
                    }

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(disbursement.FirstOrDefault());

                    var lockDisbursement = disbursement.FirstOrDefault();
                    lockDisbursement.DisbursementType = objDisbursement.DisbursementType;
                    lockDisbursement.PayTypeId = payType.FirstOrDefault().Id;
                    lockDisbursement.Remarks = objDisbursement.Remarks;
                    lockDisbursement.Amount = objDisbursement.Amount;
                    lockDisbursement.IsRefund = objDisbursement.IsRefund;
                    lockDisbursement.RefundSalesId = refundSalesId;
                    lockDisbursement.CheckedBy = objDisbursement.CheckedBy;
                    lockDisbursement.ApprovedBy = objDisbursement.ApprovedBy;
                    lockDisbursement.IsLocked = true;
                    lockDisbursement.UpdateUserId = currentUserLogin.FirstOrDefault().Id;
                    lockDisbursement.UpdateDateTime = DateTime.Now;
                    lockDisbursement.Amount1000 = objDisbursement.Amount1000;
                    lockDisbursement.Amount500 = objDisbursement.Amount500;
                    lockDisbursement.Amount200 = objDisbursement.Amount200;
                    lockDisbursement.Amount100 = objDisbursement.Amount100;
                    lockDisbursement.Amount50 = objDisbursement.Amount50;
                    lockDisbursement.Amount20 = objDisbursement.Amount20;
                    lockDisbursement.Amount10 = objDisbursement.Amount10;
                    lockDisbursement.Amount5 = objDisbursement.Amount5;
                    lockDisbursement.Amount1 = objDisbursement.Amount1;
                    lockDisbursement.Amount025 = objDisbursement.Amount025;
                    lockDisbursement.Amount010 = objDisbursement.Amount010;
                    lockDisbursement.Amount005 = objDisbursement.Amount005;
                    lockDisbursement.Amount001 = objDisbursement.Amount001;
                    lockDisbursement.Payee = objDisbursement.Payee;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(disbursement.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "TrnDisbursement",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "LockDisbursement"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Disbursement not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ===================
        // Unlock Disbursement
        // ===================
        public String[] UnlockDisbursement(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var disbursement = from d in db.TrnDisbursements
                                   where d.Id == id
                                   select d;

                if (disbursement.Any())
                {
                    if (disbursement.FirstOrDefault().IsLocked == false)
                    {
                        return new String[] { "Already unlocked.", "0" };
                    }

                    if (disbursement.FirstOrDefault().IsRefund == true && disbursement.FirstOrDefault().RefundSalesId != null)
                    {
                        var sales = from d in db.TrnSales
                                    where d.Id == disbursement.FirstOrDefault().RefundSalesId
                                    && d.IsLocked == true
                                    && d.IsReturned == true
                                    && d.ReturnApplication == "Refund"
                                    select d;

                        if (sales.Any())
                        {
                            var updateSales = sales.FirstOrDefault();
                            updateSales.ReturnApplication = "Return";
                            db.SubmitChanges();
                        }
                    }

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(disbursement.FirstOrDefault());

                    var unlockDisbursement = disbursement.FirstOrDefault();
                    unlockDisbursement.IsLocked = false;
                    unlockDisbursement.UpdateUserId = currentUserLogin.FirstOrDefault().Id;
                    unlockDisbursement.UpdateDateTime = DateTime.Now;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(disbursement.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "TrnDisbursement",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "UnlockDisbursement"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Disbursement not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ===================
        // Delete Disbursement
        // ===================
        public String[] DeleteDisbursement(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var disbursement = from d in db.TrnDisbursements
                                   where d.Id == id
                                   select d;

                if (disbursement.Any())
                {
                    if (disbursement.FirstOrDefault().IsLocked)
                    {
                        return new String[] { "Disbursement is locked", "0" };
                    }

                    var deleteDisbursement = disbursement.FirstOrDefault();
                    db.TrnDisbursements.DeleteOnSubmit(deleteDisbursement);

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(disbursement.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "TrnDisbursement",
                        RecordInformation = oldObject,
                        FormInformation = "",
                        ActionInformation = "DeleteDisbursement"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    db.SubmitChanges();

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Disbursement not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ===========================
        // Get Sales Detail for Refund
        // ===========================
        public Entities.TrnSalesEntity GetSalesDetail(String orderReturnNumber)
        {
            var sale = from d in db.TrnSales
                       where d.SalesNumber == orderReturnNumber
                       && d.IsLocked == true
                       && d.IsReturned == true
                       && d.ReturnApplication == "Return"
                       select new Entities.TrnSalesEntity
                       {
                           Id = d.Id,
                           Customer = d.MstCustomer.Customer,
                           Amount = d.Amount
                       };

            return sale.FirstOrDefault();
        }

        // ======
        // Refund
        // ======
        public String[] Refund(Entities.TrnDisbursementEntity objDisbursement)
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

                var account = from d in db.MstAccounts select d;
                if (account.Any() == false)
                {
                    return new String[] { "Account not found.", "0" };
                }

                var payType = from d in db.MstPayTypes
                              where d.PayType == "Cash"
                              select d;

                if (payType.Any() == false)
                {
                    return new String[] { "Pay type cash not found.", "0" };
                }

                var terminal = from d in db.MstTerminals where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().TerminalId) select d;
                if (terminal.Any() == false)
                {
                    return new String[] { "Terminal not found.", "0" };
                }

                DateTime currentDate = DateTime.Today;
                if (Modules.SysCurrentModule.GetCurrentSettings().IsLoginDate == true)
                {
                    currentDate = Convert.ToDateTime(Modules.SysCurrentModule.GetCurrentSettings().CurrentDate);
                }

                var sales = from d in db.TrnSales
                            where d.Id == objDisbursement.RefundSalesId
                            && d.IsLocked == true
                            && d.IsReturned == true
                            && d.ReturnApplication == "Return"
                            select d;

                if (sales.Any() == false)
                {
                    return new String[] { "Sales not found.", "0" };
                }

                String disbursementNumber = "0000000001";
                var lastDisbursement = from d in db.TrnDisbursements.OrderByDescending(d => d.Id) select d;
                if (lastDisbursement.Any())
                {
                    Int32 newDisbursementNumber = Convert.ToInt32(lastDisbursement.FirstOrDefault().DisbursementNumber) + 1;
                    disbursementNumber = FillLeadingZeroes(newDisbursementNumber, 10);
                }

                Data.TrnDisbursement newDisbursement = new Data.TrnDisbursement()
                {
                    PeriodId = period.FirstOrDefault().Id,
                    DisbursementDate = currentDate,
                    DisbursementNumber = disbursementNumber,
                    DisbursementType = "CREDIT",
                    AccountId = account.FirstOrDefault().Id,
                    Amount = sales.FirstOrDefault().Amount * -1,
                    PayTypeId = payType.FirstOrDefault().Id,
                    TerminalId = terminal.FirstOrDefault().Id,
                    Remarks = "Refund",
                    IsRefund = true,
                    RefundSalesId = sales.FirstOrDefault().Id,
                    StockInId = null,
                    PreparedBy = currentUserLogin.FirstOrDefault().Id,
                    CheckedBy = currentUserLogin.FirstOrDefault().Id,
                    ApprovedBy = currentUserLogin.FirstOrDefault().Id,
                    IsLocked = true,
                    EntryUserId = currentUserLogin.FirstOrDefault().Id,
                    EntryDateTime = DateTime.Now,
                    UpdateUserId = currentUserLogin.FirstOrDefault().Id,
                    UpdateDateTime = DateTime.Now,
                    Amount1000 = null,
                    Amount500 = null,
                    Amount200 = null,
                    Amount100 = null,
                    Amount50 = null,
                    Amount20 = null,
                    Amount10 = null,
                    Amount5 = null,
                    Amount1 = null,
                    Amount025 = null,
                    Amount010 = null,
                    Amount005 = null,
                    Amount001 = null,
                    Payee = sales.FirstOrDefault().MstCustomer.Customer
                };

                db.TrnDisbursements.InsertOnSubmit(newDisbursement);
                db.SubmitChanges();

                var updateSales = sales.FirstOrDefault();
                updateSales.ReturnApplication = "Refund";
                db.SubmitChanges();

                String newObject = Modules.SysAuditTrailModule.GetObjectString(newDisbursement);

                Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                {
                    UserId = currentUserLogin.FirstOrDefault().Id,
                    AuditDate = DateTime.Now,
                    TableInformation = "TrnDisbursement",
                    RecordInformation = "",
                    FormInformation = newObject,
                    ActionInformation = "AddDisbursement"
                };
                Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                return new String[] { "", newDisbursement.Id.ToString() };
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

    }
}
