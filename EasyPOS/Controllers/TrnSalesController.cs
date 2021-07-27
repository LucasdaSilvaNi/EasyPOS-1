using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace EasyPOS.Controllers
{
    class TrnSalesController
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

        // ======================
        // Dropdown List Terminal
        // ======================
        public List<Entities.MstTerminalEntity> DropdownListTerminal()
        {
            var terminals = from d in db.MstTerminals
                            select new Entities.MstTerminalEntity
                            {
                                Id = d.Id,
                                Terminal = "Terminal: " + d.Terminal
                            };

            return terminals.ToList();
        }

        // ==============
        // POS List Sales 
        // ==============
        public List<Entities.TrnSalesEntity> POSTTouchListSales(DateTime dateTime, Int32 terminalId, String filter)
        {
            var sales = from d in db.TrnSales
                        where d.SalesDate == dateTime
                        && d.TerminalId == terminalId
                        && (d.SalesNumber.Contains(filter)
                        || d.ManualInvoiceNumber.Contains(filter)
                        || d.CollectionNumber.Contains(filter)
                        || d.MstTable.TableCode.Contains(filter)
                        || d.MstCustomer.CustomerCode.Contains(filter)
                        || d.MstCustomer.Customer.Contains(filter)
                        || d.MstUser5.UserName.Contains(filter)
                        || d.Remarks.Contains(filter)
                        || d.DeliveryDriver.Contains(filter))
                        select new Entities.TrnSalesEntity
                        {
                            Id = d.Id,
                            PeriodId = d.PeriodId,
                            Period = d.MstPeriod.Period,
                            SalesDate = d.SalesDate.ToShortDateString(),
                            SalesNumber = d.SalesNumber,
                            ManualInvoiceNumber = d.ManualInvoiceNumber,
                            CollectionNumber = d.CollectionNumber,
                            Amount = d.Amount,
                            TableId = d.TableId,
                            Table = d.TableId != null ? d.MstTable.TableCode : "",
                            TableStatus = d.TableStatus,
                            CustomerId = d.CustomerId,
                            CustomerCode = d.MstCustomer.CustomerCode,
                            Customer = d.MstCustomer.Customer,
                            CustomerAddress = d.MstCustomer.Address,
                            AccountId = d.AccountId,
                            TermId = d.TermId,
                            Term = d.MstTerm.Term,
                            DiscountId = d.DiscountId,
                            SeniorCitizenId = d.SeniorCitizenId,
                            SeniorCitizenName = d.SeniorCitizenName,
                            SeniorCitizenAge = d.SeniorCitizenAge,
                            Remarks = d.Remarks,
                            SalesAgent = d.SalesAgent,
                            SalesAgentUserName = d.SalesAgent != null ? d.MstUser5.UserName : "",
                            TerminalId = d.TerminalId,
                            Terminal = d.MstTerminal.Terminal,
                            DeliveryId = d.DeliveryId,
                            DeliveryDriver = d.DeliveryDriver,
                            PreparedBy = d.PreparedBy,
                            PreparedByUserName = d.MstUser.FullName,
                            CheckedBy = d.CheckedBy,
                            CheckedByUserName = d.MstUser1.FullName,
                            ApprovedBy = d.ApprovedBy,
                            ApprovedByUserName = d.MstUser2.FullName,
                            IsLocked = d.IsLocked,
                            IsTendered = d.IsTendered,
                            IsCancelled = d.IsCancelled,
                            IsDispatched = d.IsDispatched,
                            IsReturned = d.IsReturned,
                            ReturnApplication = d.ReturnApplication,
                            PaidAmount = d.PaidAmount,
                            CreditAmount = d.CreditAmount,
                            DebitAmount = d.DebitAmount,
                            BalanceAmount = d.BalanceAmount,
                            EntryUserId = d.EntryUserId,
                            EntryUserName = d.MstUser3.FullName,
                            EntryDateTime = d.EntryDateTime.ToShortDateString(),
                            UpdateUserId = d.UpdateUserId,
                            UpdatedUserName = d.MstUser4.FullName,
                            UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                            Pax = d.Pax,
                            PostCode = d.PostCode
                        };

            return sales.OrderByDescending(d => d.Id).ToList();
        }

        // ==========
        // List Sales 
        // ==========
        public List<Entities.TrnSalesEntity> ListSales(DateTime dateTime, Int32 terminalId, String filter, String selectedIsLocked)
        {
            if (selectedIsLocked == "All")
            {
                var sales = from d in db.TrnSales
                            where d.SalesDate == dateTime
                            && d.TerminalId == terminalId
                            && (d.SalesNumber.Contains(filter)
                            || d.MstCustomer.CustomerCode.Contains(filter)
                            || d.MstCustomer.Customer.Contains(filter)
                            || d.MstUser5.UserName.Contains(filter)
                            || d.Remarks.Contains(filter)
                            || d.CollectionNumber.Contains(filter))
                            select new Entities.TrnSalesEntity
                            {
                                Id = d.Id,
                                PeriodId = d.PeriodId,
                                Period = d.MstPeriod.Period,
                                SalesDate = d.SalesDate.ToShortDateString(),
                                SalesNumber = d.SalesNumber,
                                ManualInvoiceNumber = d.ManualInvoiceNumber,
                                CollectionNumber = d.CollectionNumber,
                                Amount = d.Amount,
                                TableId = d.TableId,
                                Table = d.TableId != null ? d.MstTable.TableCode : "",
                                TableStatus = d.TableStatus,
                                CustomerId = d.CustomerId,
                                CustomerCode = d.MstCustomer.CustomerCode,
                                Customer = d.MstCustomer.Customer,
                                CustomerAddress = d.MstCustomer.Address,
                                AccountId = d.AccountId,
                                TermId = d.TermId,
                                Term = d.MstTerm.Term,
                                DiscountId = d.DiscountId,
                                SeniorCitizenId = d.SeniorCitizenId,
                                SeniorCitizenName = d.SeniorCitizenName,
                                SeniorCitizenAge = d.SeniorCitizenAge,
                                Remarks = d.Remarks,
                                SalesAgent = d.SalesAgent,
                                SalesAgentUserName = d.SalesAgent != null ? d.MstUser5.UserName : "",
                                TerminalId = d.TerminalId,
                                Terminal = d.MstTerminal.Terminal,
                                DeliveryId = d.DeliveryId,
                                DeliveryDriver = d.DeliveryDriver,
                                PreparedBy = d.PreparedBy,
                                PreparedByUserName = d.MstUser.FullName,
                                CheckedBy = d.CheckedBy,
                                CheckedByUserName = d.MstUser1.FullName,
                                ApprovedBy = d.ApprovedBy,
                                ApprovedByUserName = d.MstUser2.FullName,
                                IsLocked = d.IsLocked,
                                IsTendered = d.IsTendered,
                                IsCancelled = d.IsCancelled,
                                IsDispatched = d.IsDispatched,
                                IsReturned = d.IsReturned,
                                ReturnApplication = d.ReturnApplication,
                                PaidAmount = d.PaidAmount,
                                CreditAmount = d.CreditAmount,
                                DebitAmount = d.DebitAmount,
                                BalanceAmount = d.BalanceAmount,
                                EntryUserId = d.EntryUserId,
                                EntryUserName = d.MstUser3.FullName,
                                EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                UpdateUserId = d.UpdateUserId,
                                UpdatedUserName = d.MstUser4.FullName,
                                UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                                Pax = d.Pax,
                                PostCode = d.PostCode
                            };

                return sales.OrderByDescending(d => d.Id).ToList();
            }
            else if (selectedIsLocked == "Tendered")
            {
                var sales = from d in db.TrnSales
                            where d.SalesDate == dateTime
                            && d.TerminalId == terminalId
                            && d.IsTendered == true
                            && (d.SalesNumber.Contains(filter)
                            || d.MstCustomer.CustomerCode.Contains(filter)
                            || d.MstCustomer.Customer.Contains(filter)
                            || d.MstUser5.UserName.Contains(filter)
                            || d.Remarks.Contains(filter)
                            || d.CollectionNumber.Contains(filter))
                            select new Entities.TrnSalesEntity
                            {
                                Id = d.Id,
                                PeriodId = d.PeriodId,
                                Period = d.MstPeriod.Period,
                                SalesDate = d.SalesDate.ToShortDateString(),
                                SalesNumber = d.SalesNumber,
                                ManualInvoiceNumber = d.ManualInvoiceNumber,
                                CollectionNumber = d.CollectionNumber,
                                Amount = d.Amount,
                                TableId = d.TableId,
                                Table = d.TableId != null ? d.MstTable.TableCode : "",
                                TableStatus = d.TableStatus,
                                CustomerId = d.CustomerId,
                                CustomerCode = d.MstCustomer.CustomerCode,
                                Customer = d.MstCustomer.Customer,
                                CustomerAddress = d.MstCustomer.Address,
                                AccountId = d.AccountId,
                                TermId = d.TermId,
                                Term = d.MstTerm.Term,
                                DiscountId = d.DiscountId,
                                SeniorCitizenId = d.SeniorCitizenId,
                                SeniorCitizenName = d.SeniorCitizenName,
                                SeniorCitizenAge = d.SeniorCitizenAge,
                                Remarks = d.Remarks,
                                SalesAgent = d.SalesAgent,
                                SalesAgentUserName = d.SalesAgent != null ? d.MstUser5.UserName : "",
                                TerminalId = d.TerminalId,
                                Terminal = d.MstTerminal.Terminal,
                                DeliveryId = d.DeliveryId,
                                DeliveryDriver = d.DeliveryDriver,
                                PreparedBy = d.PreparedBy,
                                PreparedByUserName = d.MstUser.FullName,
                                CheckedBy = d.CheckedBy,
                                CheckedByUserName = d.MstUser1.FullName,
                                ApprovedBy = d.ApprovedBy,
                                ApprovedByUserName = d.MstUser2.FullName,
                                IsLocked = d.IsLocked,
                                IsTendered = d.IsTendered,
                                IsCancelled = d.IsCancelled,
                                IsDispatched = d.IsDispatched,
                                IsReturned = d.IsReturned,
                                ReturnApplication = d.ReturnApplication,
                                PaidAmount = d.PaidAmount,
                                CreditAmount = d.CreditAmount,
                                DebitAmount = d.DebitAmount,
                                BalanceAmount = d.BalanceAmount,
                                EntryUserId = d.EntryUserId,
                                EntryUserName = d.MstUser3.FullName,
                                EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                UpdateUserId = d.UpdateUserId,
                                UpdatedUserName = d.MstUser4.FullName,
                                UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                                Pax = d.Pax,
                                PostCode = d.PostCode
                            };

                return sales.OrderByDescending(d => d.Id).ToList();
            }
            else if (selectedIsLocked == "Cancelled")
            {
                var sales = from d in db.TrnSales
                            where d.SalesDate == dateTime
                            && d.TerminalId == terminalId
                            && d.IsCancelled == true
                            && (d.SalesNumber.Contains(filter)
                            || d.MstCustomer.CustomerCode.Contains(filter)
                            || d.MstCustomer.Customer.Contains(filter)
                            || d.MstUser5.UserName.Contains(filter)
                            || d.Remarks.Contains(filter)
                            || d.CollectionNumber.Contains(filter))
                            select new Entities.TrnSalesEntity
                            {
                                Id = d.Id,
                                PeriodId = d.PeriodId,
                                Period = d.MstPeriod.Period,
                                SalesDate = d.SalesDate.ToShortDateString(),
                                SalesNumber = d.SalesNumber,
                                ManualInvoiceNumber = d.ManualInvoiceNumber,
                                CollectionNumber = d.CollectionNumber,
                                Amount = d.Amount,
                                TableId = d.TableId,
                                Table = d.TableId != null ? d.MstTable.TableCode : "",
                                TableStatus = d.TableStatus,
                                CustomerId = d.CustomerId,
                                CustomerCode = d.MstCustomer.CustomerCode,
                                Customer = d.MstCustomer.Customer,
                                CustomerAddress = d.MstCustomer.Address,
                                AccountId = d.AccountId,
                                TermId = d.TermId,
                                Term = d.MstTerm.Term,
                                DiscountId = d.DiscountId,
                                SeniorCitizenId = d.SeniorCitizenId,
                                SeniorCitizenName = d.SeniorCitizenName,
                                SeniorCitizenAge = d.SeniorCitizenAge,
                                Remarks = d.Remarks,
                                SalesAgent = d.SalesAgent,
                                SalesAgentUserName = d.SalesAgent != null ? d.MstUser5.UserName : "",
                                TerminalId = d.TerminalId,
                                Terminal = d.MstTerminal.Terminal,
                                DeliveryId = d.DeliveryId,
                                DeliveryDriver = d.DeliveryDriver,
                                PreparedBy = d.PreparedBy,
                                PreparedByUserName = d.MstUser.FullName,
                                CheckedBy = d.CheckedBy,
                                CheckedByUserName = d.MstUser1.FullName,
                                ApprovedBy = d.ApprovedBy,
                                ApprovedByUserName = d.MstUser2.FullName,
                                IsLocked = d.IsLocked,
                                IsTendered = d.IsTendered,
                                IsCancelled = d.IsCancelled,
                                IsDispatched = d.IsDispatched,
                                IsReturned = d.IsReturned,
                                ReturnApplication = d.ReturnApplication,
                                PaidAmount = d.PaidAmount,
                                CreditAmount = d.CreditAmount,
                                DebitAmount = d.DebitAmount,
                                BalanceAmount = d.BalanceAmount,
                                EntryUserId = d.EntryUserId,
                                EntryUserName = d.MstUser3.FullName,
                                EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                UpdateUserId = d.UpdateUserId,
                                UpdatedUserName = d.MstUser4.FullName,
                                UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                                Pax = d.Pax,
                                PostCode = d.PostCode
                            };

                return sales.OrderByDescending(d => d.Id).ToList();
            }
            else
            {
                Boolean isLocked = false;

                if (selectedIsLocked == "Locked")
                {
                    isLocked = true;
                }

                var sales = from d in db.TrnSales
                            where d.SalesDate == dateTime
                            && d.TerminalId == terminalId
                            && d.IsLocked == isLocked
                            && (d.SalesNumber.Contains(filter)
                            || d.MstCustomer.CustomerCode.Contains(filter)
                            || d.MstCustomer.Customer.Contains(filter)
                            || d.MstUser5.UserName.Contains(filter)
                            || d.Remarks.Contains(filter)
                            || d.CollectionNumber.Contains(filter))
                            select new Entities.TrnSalesEntity
                            {
                                Id = d.Id,
                                PeriodId = d.PeriodId,
                                Period = d.MstPeriod.Period,
                                SalesDate = d.SalesDate.ToShortDateString(),
                                SalesNumber = d.SalesNumber,
                                ManualInvoiceNumber = d.ManualInvoiceNumber,
                                CollectionNumber = d.CollectionNumber,
                                Amount = d.Amount,
                                TableId = d.TableId,
                                Table = d.TableId != null ? d.MstTable.TableCode : "",
                                TableStatus = d.TableStatus,
                                CustomerId = d.CustomerId,
                                CustomerCode = d.MstCustomer.CustomerCode,
                                Customer = d.MstCustomer.Customer,
                                CustomerAddress = d.MstCustomer.Address,
                                AccountId = d.AccountId,
                                TermId = d.TermId,
                                Term = d.MstTerm.Term,
                                DiscountId = d.DiscountId,
                                SeniorCitizenId = d.SeniorCitizenId,
                                SeniorCitizenName = d.SeniorCitizenName,
                                SeniorCitizenAge = d.SeniorCitizenAge,
                                Remarks = d.Remarks,
                                SalesAgent = d.SalesAgent,
                                SalesAgentUserName = d.SalesAgent != null ? d.MstUser5.UserName : "",
                                TerminalId = d.TerminalId,
                                Terminal = d.MstTerminal.Terminal,
                                DeliveryId = d.DeliveryId,
                                DeliveryDriver = d.DeliveryDriver,
                                PreparedBy = d.PreparedBy,
                                PreparedByUserName = d.MstUser.FullName,
                                CheckedBy = d.CheckedBy,
                                CheckedByUserName = d.MstUser1.FullName,
                                ApprovedBy = d.ApprovedBy,
                                ApprovedByUserName = d.MstUser2.FullName,
                                IsLocked = d.IsLocked,
                                IsTendered = d.IsTendered,
                                IsCancelled = d.IsCancelled,
                                IsDispatched = d.IsDispatched,
                                IsReturned = d.IsReturned,
                                ReturnApplication = d.ReturnApplication,
                                PaidAmount = d.PaidAmount,
                                CreditAmount = d.CreditAmount,
                                DebitAmount = d.DebitAmount,
                                BalanceAmount = d.BalanceAmount,
                                EntryUserId = d.EntryUserId,
                                EntryUserName = d.MstUser3.FullName,
                                EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                UpdateUserId = d.UpdateUserId,
                                UpdatedUserName = d.MstUser4.FullName,
                                UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                                Pax = d.Pax,
                                PostCode = d.PostCode
                            };

                return sales.OrderByDescending(d => d.Id).ToList();
            }
        }

        // ============
        // Detail Sales 
        // ============
        public Entities.TrnSalesEntity DetailSales(Int32 id)
        {
            var sales = from d in db.TrnSales
                        where d.Id == id
                        select new Entities.TrnSalesEntity
                        {
                            Id = d.Id,
                            PeriodId = d.PeriodId,
                            Period = d.MstPeriod.Period,
                            SalesDate = d.SalesDate.ToShortDateString(),
                            SalesNumber = d.SalesNumber,
                            ManualInvoiceNumber = d.ManualInvoiceNumber,
                            CollectionNumber = d.CollectionNumber,
                            Amount = d.Amount,
                            TableId = d.TableId,
                            Table = d.TableId != null ? d.MstTable.TableCode : "",
                            TableStatus = d.TableStatus,
                            CustomerId = d.CustomerId,
                            CustomerCode = d.MstCustomer.CustomerCode,
                            Customer = d.MstCustomer.Customer,
                            CustomerAddress = d.MstCustomer.Address,
                            AccountId = d.AccountId,
                            TermId = d.TermId,
                            Term = d.MstTerm.Term,
                            DiscountId = d.DiscountId,
                            SeniorCitizenId = d.SeniorCitizenId,
                            SeniorCitizenName = d.SeniorCitizenName,
                            SeniorCitizenAge = d.SeniorCitizenAge,
                            Remarks = d.Remarks,
                            SalesAgent = d.SalesAgent,
                            SalesAgentUserName = d.SalesAgent != null ? d.MstUser5.UserName : "",
                            TerminalId = d.TerminalId,
                            Terminal = d.MstTerminal.Terminal,
                            DeliveryId = d.DeliveryId,
                            DeliveryDriver = d.DeliveryDriver,
                            PreparedBy = d.PreparedBy,
                            PreparedByUserName = d.MstUser.FullName,
                            CheckedBy = d.CheckedBy,
                            CheckedByUserName = d.MstUser1.FullName,
                            ApprovedBy = d.ApprovedBy,
                            ApprovedByUserName = d.MstUser2.FullName,
                            IsLocked = d.IsLocked,
                            IsTendered = d.IsTendered,
                            IsCancelled = d.IsCancelled,
                            IsDispatched = d.IsDispatched,
                            IsReturned = d.IsReturned,
                            ReturnApplication = d.ReturnApplication,
                            PaidAmount = d.PaidAmount,
                            CreditAmount = d.CreditAmount,
                            DebitAmount = d.DebitAmount,
                            BalanceAmount = d.BalanceAmount,
                            EntryUserId = d.EntryUserId,
                            EntryUserName = d.MstUser3.FullName,
                            EntryDateTime = d.EntryDateTime.ToShortDateString(),
                            UpdateUserId = d.UpdateUserId,
                            UpdatedUserName = d.MstUser4.FullName,
                            UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                            Pax = d.Pax,
                            PostCode = d.PostCode
                        };

            return sales.FirstOrDefault();
        }

        // =========
        // Add Sales 
        // =========
        public String[] AddSales(String tableCode, Int32 customerId)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var period = from d in db.MstPeriods where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentPeriodId) select d;
                if (period.Any() == false)
                {
                    return new String[] { "Period not found.", "0" };
                }

                Int32? tableId = null;
                if (tableCode != "")
                {
                    var table = from d in db.MstTables
                                where d.TableCode == tableCode
                                select d;

                    if (table.Any() == false)
                    {
                        return new String[] { "Table not found.", "0" };
                    }

                    tableId = table.FirstOrDefault().Id;
                }

                var customer = from d in db.MstCustomers where d.Id == customerId select d;
                if (customer.Any() == false)
                {
                    return new String[] { "Customer not found.", "0" };
                }

                var terminal = from d in db.MstTerminals where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().TerminalId) select d;
                if (terminal.Any() == false)
                {
                    return new String[] { "Terminal not found.", "0" };
                }

                var user = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (user.Any() == false)
                {
                    return new String[] { "User not found.", "0" };
                }

                String salesNumber = "0000000001";

                while (true)
                {
                    var lastSales = from d in db.TrnSales.OrderByDescending(d => d.Id) select d;
                    if (lastSales.Any())
                    {
                        Int32 newSalesNumber = Convert.ToInt32(lastSales.FirstOrDefault().SalesNumber) + 1;
                        salesNumber = FillLeadingZeroes(newSalesNumber, 10);
                    }

                    var existingSales = from d in db.TrnSales
                                        where d.SalesNumber == salesNumber
                                        select d;

                    if (existingSales.Any() == false)
                    {
                        break;
                    }
                }

                DateTime currentDate = DateTime.Today;
                if (Modules.SysCurrentModule.GetCurrentSettings().IsLoginDate == true)
                {
                    currentDate = Convert.ToDateTime(Modules.SysCurrentModule.GetCurrentSettings().CurrentDate);
                }

                Data.TrnSale newSales = new Data.TrnSale()
                {
                    PeriodId = period.FirstOrDefault().Id,
                    SalesDate = currentDate,
                    SalesNumber = salesNumber,
                    ManualInvoiceNumber = terminal.FirstOrDefault().Terminal + "-" + salesNumber,
                    CollectionNumber = null,
                    Amount = 0,
                    TableId = tableId,
                    TableStatus = 0,
                    CustomerId = customer.FirstOrDefault().Id,
                    AccountId = customer.FirstOrDefault().AccountId,
                    TermId = customer.FirstOrDefault().TermId,
                    DiscountId = null,
                    SeniorCitizenId = "",
                    SeniorCitizenName = "",
                    SeniorCitizenAge = null,
                    Remarks = "",
                    SalesAgent = user.FirstOrDefault().Id,
                    TerminalId = terminal.FirstOrDefault().Id,
                    DeliveryId = null,
                    DeliveryDriver = "",
                    PreparedBy = user.FirstOrDefault().Id,
                    CheckedBy = user.FirstOrDefault().Id,
                    ApprovedBy = user.FirstOrDefault().Id,
                    IsLocked = false,
                    IsTendered = false,
                    IsCancelled = false,
                    IsDispatched = false,
                    IsReturned = false,
                    ReturnApplication = "",
                    PaidAmount = 0,
                    CreditAmount = 0,
                    DebitAmount = 0,
                    BalanceAmount = 0,
                    EntryUserId = user.FirstOrDefault().Id,
                    EntryDateTime = DateTime.Now,
                    UpdateUserId = user.FirstOrDefault().Id,
                    UpdateDateTime = DateTime.Now,
                    Pax = null,
                    PostCode = null
                };

                db.TrnSales.InsertOnSubmit(newSales);
                db.SubmitChanges();

                String newObject = Modules.SysAuditTrailModule.GetObjectString(newSales);

                Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                {
                    UserId = currentUserLogin.FirstOrDefault().Id,
                    AuditDate = DateTime.Now,
                    TableInformation = "TrnSale",
                    RecordInformation = "",
                    FormInformation = newObject,
                    ActionInformation = "AddSales"
                };
                Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                return new String[] { "", newSales.Id.ToString() };
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // =====================
        // Tender List Pay Types 
        // =====================
        public List<Entities.MstPayTypeEntity> TenderListPayType()
        {
            var payTypes = from d in db.MstPayTypes
                           select new Entities.MstPayTypeEntity
                           {
                               Id = d.Id,
                               PayTypeCode = d.PayTypeCode,
                               PayType = d.PayType,
                               SortNumber = d.SortNumber
                           };

            return payTypes.OrderBy(d => d.SortNumber).ToList();
        }

        // ============
        // Tender Sales
        // ============
        public String[] TenderSales(Int32 salesId, Entities.TrnCollectionEntity objCollection)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var currentSales = from d in db.TrnSales
                                   where d.Id == salesId
                                   select d;

                if (currentSales.Any() == false)
                {
                    return new String[] { "Sales not found.", "0" };
                }
                else
                {
                    if (Modules.SysCurrentModule.GetCurrentSettings().AllowNegativeInventory == false)
                    {
                        Boolean isNegativeInventory = false;
                        String negativeInventoryItem = "";

                        if (currentSales.FirstOrDefault().TrnSalesLines.Where(d => d.MstItem.IsInventory == true).Any())
                        {
                            var groupedSalesLines = from d in currentSales.FirstOrDefault().TrnSalesLines.Where(d => d.MstItem.IsInventory == true)
                                                    group d by d.MstItem into g
                                                    select g;

                            foreach (var salesLine in groupedSalesLines.ToList())
                            {
                                negativeInventoryItem = salesLine.Key.ItemDescription;

                                if (salesLine.Key.OnhandQuantity < 0)
                                {
                                    isNegativeInventory = true;
                                    break;
                                }
                                //else
                                //{
                                //    if (salesLine.Key.OnhandQuantity <= salesLine.Sum(d => d.Quantity))
                                //    {
                                //        isNegativeInventory = true;
                                //        break;
                                //    }
                                //}
                            }
                        }

                        if (isNegativeInventory == true)
                        {
                            return new String[] { "Negative inventory item found. " + negativeInventoryItem, "0" };
                        }

                        Boolean isNegativeInventoryComponent = false;
                        String negativeInventoryComponentItem = "";

                        if (currentSales.FirstOrDefault().TrnSalesLines.Where(d => d.MstItem.IsInventory == false).Any())
                        {
                            var groupedSalesLines = from d in currentSales.FirstOrDefault().TrnSalesLines.Where(d => d.MstItem.IsInventory == false)
                                                    group d by d.MstItem into g
                                                    select g;

                            foreach (var salesLine in groupedSalesLines.ToList())
                            {
                                if (salesLine.Key.MstItemComponents.Any())
                                {
                                    foreach (var component in salesLine.Key.MstItemComponents)
                                    {
                                        negativeInventoryComponentItem = component.MstItem1.ItemDescription;

                                        if (component.MstItem1.OnhandQuantity <= 0)
                                        {
                                            isNegativeInventoryComponent = true;
                                            break;
                                        }
                                        else
                                        {
                                            if (component.MstItem1.OnhandQuantity < (salesLine.Sum(d => d.Quantity) * component.Quantity))
                                            {
                                                isNegativeInventoryComponent = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (isNegativeInventoryComponent == true)
                        {
                            return new String[] { "Negative inventory component item found. " + negativeInventoryComponentItem, "0" };
                        }
                    }
                }

                var user = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (user.Any() == false)
                {
                    return new String[] { "User not found.", "0" };
                }

                if (user.Any() == false)
                {
                    return new String[] { "User are not found.", "0" };
                }

                var collectedSales = from d in db.TrnCollections
                                     where d.SalesId == salesId
                                     && d.IsLocked == true
                                     select d;

                if (collectedSales.Any())
                {
                    return new String[] { "Sales already collected.", "0" };
                }

                String collectionNumber = "0000000001";
                var lastCollection = from d in db.TrnCollections.OrderByDescending(d => d.Id)
                                     where d.TerminalId == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().TerminalId)
                                     select d;

                if (lastCollection.Any())
                {
                    Int32 newCollectionNumber = Convert.ToInt32(lastCollection.FirstOrDefault().CollectionNumber) + 1;
                    collectionNumber = FillLeadingZeroes(newCollectionNumber, 10);
                }

                DateTime currentDate = DateTime.Today;
                if (Modules.SysCurrentModule.GetCurrentSettings().IsLoginDate == true)
                {
                    currentDate = Convert.ToDateTime(Modules.SysCurrentModule.GetCurrentSettings().CurrentDate);
                }


                if (objCollection.CollectionLines.Any())
                {
                    foreach (var collectionLine in objCollection.CollectionLines)
                    {
                        var payType = from d in db.MstPayTypes
                                      where d.Id == collectionLine.PayTypeId
                                      select d;

                        if (payType.Any())
                        {
                            if (payType.FirstOrDefault().PayTypeCode == "REWARDS")
                            {
                                if (currentSales.FirstOrDefault().MstCustomer.AvailableReward < collectionLine.Amount)
                                {
                                    return new String[] { "Invalid reward", "0" };
                                }
                                else
                                {
                                    if (currentSales.FirstOrDefault().MstCustomer.WithReward == true)
                                    {
                                        Decimal rewardConversion = currentSales.FirstOrDefault().MstCustomer.RewardConversion;
                                        if (rewardConversion != 0)
                                        {
                                            var customer = from d in db.MstCustomers
                                                           where d.Id == currentSales.FirstOrDefault().CustomerId
                                                           select d;

                                            if (customer.Any())
                                            {
                                                Decimal existingReward = customer.FirstOrDefault().AvailableReward;

                                                var updateRewards = customer.FirstOrDefault();
                                                updateRewards.AvailableReward = existingReward - collectionLine.Amount;
                                                db.SubmitChanges();
                                            }
                                        }
                                    }
                                }

                                break;
                            }
                        }
                    }
                }

                Data.TrnCollection newCollection = new Data.TrnCollection
                {
                    PeriodId = currentSales.FirstOrDefault().PeriodId,
                    CollectionDate = currentDate,
                    CollectionNumber = collectionNumber,
                    TerminalId = currentSales.FirstOrDefault().TerminalId,
                    ManualORNumber = currentSales.FirstOrDefault().MstTerminal.Terminal + "-" + collectionNumber,
                    CustomerId = currentSales.FirstOrDefault().CustomerId,
                    Remarks = currentSales.FirstOrDefault().Remarks,
                    SalesId = currentSales.FirstOrDefault().Id,
                    SalesBalanceAmount = currentSales.FirstOrDefault().BalanceAmount,
                    Amount = 0,
                    TenderAmount = objCollection.TenderAmount,
                    ChangeAmount = objCollection.ChangeAmount,
                    PreparedBy = user.FirstOrDefault().Id,
                    CheckedBy = user.FirstOrDefault().Id,
                    ApprovedBy = user.FirstOrDefault().Id,
                    IsCancelled = false,
                    IsLocked = false,
                    EntryUserId = user.FirstOrDefault().Id,
                    EntryDateTime = DateTime.Now,
                    UpdateUserId = user.FirstOrDefault().Id,
                    UpdateDateTime = DateTime.Now
                };

                db.TrnCollections.InsertOnSubmit(newCollection);
                db.SubmitChanges();

                String newObject = Modules.SysAuditTrailModule.GetObjectString(newCollection);

                Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                {
                    UserId = currentUserLogin.FirstOrDefault().Id,
                    AuditDate = DateTime.Now,
                    TableInformation = "TrnCollection",
                    RecordInformation = "",
                    FormInformation = newObject,
                    ActionInformation = "TenderSales"
                };
                Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                if (objCollection.CollectionLines.Any())
                {
                    List<Data.TrnCollectionLine> newCollectionLines = new List<Data.TrnCollectionLine>();
                    foreach (var collectionLine in objCollection.CollectionLines)
                    {
                        var payType = from d in db.MstPayTypes
                                      where d.Id == collectionLine.PayTypeId
                                      && d.AccountId != null
                                      select d;

                        if (payType.Any())
                        {
                            DateTime? checkDate = null;
                            if (String.IsNullOrEmpty(collectionLine.CheckDate) == false)
                            {
                                checkDate = Convert.ToDateTime(collectionLine.CheckDate);
                            }

                            newCollectionLines.Add(new Data.TrnCollectionLine()
                            {
                                CollectionId = newCollection.Id,
                                Amount = collectionLine.Amount,
                                PayTypeId = collectionLine.PayTypeId,
                                CheckNumber = collectionLine.CheckNumber,
                                CheckDate = checkDate,
                                CheckBank = collectionLine.CheckBank,
                                CreditCardVerificationCode = collectionLine.CreditCardVerificationCode,
                                CreditCardNumber = collectionLine.CreditCardNumber,
                                CreditCardType = collectionLine.CreditCardType,
                                CreditCardBank = collectionLine.CreditCardBank,
                                GiftCertificateNumber = collectionLine.GiftCertificateNumber,
                                OtherInformation = collectionLine.OtherInformation,
                                SalesReturnSalesId = collectionLine.SalesReturnSalesId,
                                StockInId = null,
                                AccountId = Convert.ToInt32(payType.FirstOrDefault().AccountId),
                                CreditCardReferenceNumber = collectionLine.CreditCardReferenceNumber,
                                CreditCardHolderName = collectionLine.CreditCardHolderName,
                                CreditCardExpiry = collectionLine.CreditCardExpiry
                            });
                        }
                    }

                    db.TrnCollectionLines.InsertAllOnSubmit(newCollectionLines);
                    db.SubmitChanges();
                }

                Decimal salesAmount = currentSales.FirstOrDefault().Amount;
                Decimal paidAmount = 0;

                var collection = from d in db.TrnCollections
                                 where d.Id == newCollection.Id
                                 select d;

                if (collection.Any())
                {
                    Decimal totalCollectionLineAmount = 0;
                    var collectionLines = from d in db.TrnCollectionLines
                                          where d.CollectionId == collection.FirstOrDefault().Id
                                          select d;

                    if (collectionLines.Any())
                    {
                        totalCollectionLineAmount = collectionLines.Sum(d => d.Amount);
                    }

                    // change amount = tender amount - sales amount

                    var lockCollection = collection.FirstOrDefault();
                    lockCollection.ChangeAmount = collection.FirstOrDefault().TenderAmount - salesAmount;
                    lockCollection.Amount = totalCollectionLineAmount - collection.FirstOrDefault().ChangeAmount;
                    lockCollection.IsLocked = true;
                    db.SubmitChanges();

                    paidAmount = totalCollectionLineAmount;
                }

                String oldObject3 = Modules.SysAuditTrailModule.GetObjectString(currentSales.FirstOrDefault());

                var lockSales = currentSales.FirstOrDefault();
                lockSales.CollectionNumber = collection.FirstOrDefault().CollectionNumber;
                lockSales.PaidAmount = paidAmount;
                lockSales.BalanceAmount = salesAmount - paidAmount;
                lockSales.IsLocked = true;
                lockSales.IsTendered = true;
                lockSales.UpdateUserId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId);
                lockSales.UpdateDateTime = DateTime.Now;
                db.SubmitChanges();

                Modules.TrnInventoryModule trnInventoryModule = new Modules.TrnInventoryModule();
                trnInventoryModule.UpdateSalesInventory(currentSales.FirstOrDefault().Id);

                String newObject3 = Modules.SysAuditTrailModule.GetObjectString(currentSales.FirstOrDefault());

                Entities.SysAuditTrailEntity newAuditTrail3 = new Entities.SysAuditTrailEntity()
                {
                    UserId = currentUserLogin.FirstOrDefault().Id,
                    AuditDate = DateTime.Now,
                    TableInformation = "TrnSales",
                    RecordInformation = oldObject3,
                    FormInformation = newObject3,
                    ActionInformation = "TenderSales"
                };
                Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail3);

                if (currentSales.FirstOrDefault().MstCustomer.WithReward == true)
                {
                    Decimal rewardConversion = currentSales.FirstOrDefault().MstCustomer.RewardConversion;
                    if (rewardConversion != 0)
                    {
                        var customer = from d in db.MstCustomers
                                       where d.Id == currentSales.FirstOrDefault().CustomerId
                                       select d;

                        if (customer.Any())
                        {
                            Decimal availableReward = currentSales.FirstOrDefault().Amount / rewardConversion;
                            Decimal existingReward = customer.FirstOrDefault().AvailableReward;

                            var updateRewards = customer.FirstOrDefault();
                            updateRewards.AvailableReward = existingReward + availableReward;
                            db.SubmitChanges();
                        }
                    }
                }

                if (Modules.SysCurrentModule.GetCurrentSettings().EnableEasyShopIntegration == true)
                {
                    EasyShopAlreadyPaid(currentSales.FirstOrDefault().ManualInvoiceNumber);
                }

                return new String[] { "", newCollection.Id.ToString() };
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ======================================
        // Tender Sales - Dropdown List Customers
        // ======================================
        public List<Entities.MstCustomerEntity> TenderSalesDropdownListCustomer()
        {
            var customers = from d in db.MstCustomers
                            where d.IsLocked == true
                            select new Entities.MstCustomerEntity
                            {
                                Id = d.Id,
                                Customer = d.Customer,
                                TermId = d.TermId,
                                CustomerCode = d.CustomerCode,
                                ContactNumber = d.ContactNumber,
                                ContactPerson = d.ContactPerson,
                                Address = d.Address,
                                AvailableReward = d.AvailableReward
                            };

            return customers.OrderBy(d => d.Customer).ToList();
        }

        // ==================================
        // Tender Sales - Dropdown List Terms
        // ==================================
        public List<Entities.MstTermEntity> TenderSalesDropdownListTerm()
        {
            var terms = from d in db.MstTerms
                        select new Entities.MstTermEntity
                        {
                            Id = d.Id,
                            Term = d.Term
                        };

            return terms.OrderBy(d => d.Term).ToList();
        }

        // ==================================
        // Tender Sales - Dropdown List Users
        // ==================================
        public List<Entities.MstUserEntity> TenderSalesDropdownListUser()
        {
            var users = from d in db.MstUsers
                        where d.IsLocked == true
                        select new Entities.MstUserEntity
                        {
                            Id = d.Id,
                            FullName = d.FullName
                        };

            return users.OrderBy(d => d.FullName).ToList();
        }

        // ===========================
        // Tender Sales - Update Sales
        // ===========================
        public String[] TenderUpdateSales(Int32 salesId, Entities.TrnSalesEntity objSalesEntity)
        {
            try
            {
                var sales = from d in db.TrnSales
                            where d.Id == salesId
                            select d;

                if (sales.Any())
                {
                    var updateSales = sales.FirstOrDefault();
                    updateSales.CustomerId = objSalesEntity.CustomerId;
                    updateSales.TermId = objSalesEntity.TermId;
                    updateSales.Remarks = objSalesEntity.Remarks;
                    updateSales.SalesAgent = objSalesEntity.SalesAgent;
                    updateSales.UpdateUserId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId);
                    updateSales.UpdateDateTime = DateTime.Now;
                    db.SubmitChanges();

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Sales not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ============
        // Delete Sales 
        // ============
        public String[] DeleteSales(Int32 salesId)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var sales = from d in db.TrnSales
                            where d.Id == salesId
                            select d;

                if (sales.Any())
                {
                    if (sales.FirstOrDefault().IsTendered)
                    {
                        return new String[] { "Already tendered.", "0" };
                    }

                    if (sales.FirstOrDefault().IsLocked)
                    {
                        return new String[] { "Already locked.", "0" };
                    }

                    db.TrnSales.DeleteOnSubmit(sales.FirstOrDefault());

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(sales.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "TrnSales",
                        RecordInformation = oldObject,
                        FormInformation = "",
                        ActionInformation = "DeleteSales"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    db.SubmitChanges();

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Sales not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ============
        // Cancel Sales
        // ============
        public String[] CancelSales(Int32 salesId, String cancelRemarks)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var sales = from d in db.TrnSales
                            where d.Id == salesId
                            && d.IsLocked == true
                            select d;

                if (sales.Any())
                {
                    if (sales.FirstOrDefault().IsCancelled)
                    {
                        return new String[] { "Already cancelled.", "0" };
                    }

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(sales.FirstOrDefault());

                    var cancelSales = sales.FirstOrDefault();
                    cancelSales.IsCancelled = true;
                    cancelSales.UpdateUserId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId);
                    cancelSales.UpdateDateTime = DateTime.Now;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(sales.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "TrnSales",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "CancelSales"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    var collection = from d in db.TrnCollections
                                     where d.SalesId == salesId
                                     && d.IsLocked == true
                                     select d;

                    if (collection.Any())
                    {
                        if (collection.FirstOrDefault().IsCancelled)
                        {
                            return new String[] { "Already cancelled.", "0" };
                        }

                        String cancelledCollectionNumber = "0000000001";
                        var lastCancelledCollection = from d in db.TrnCollections.OrderByDescending(d => d.Id)
                                                      where d.TerminalId == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().TerminalId)
                                                      && d.IsCancelled == true
                                                      select d;

                        if (lastCancelledCollection.Any())
                        {
                            Int32 newCancelledCollectionNumber = Convert.ToInt32(lastCancelledCollection.FirstOrDefault().CancelledCollectionNumber) + 1;
                            cancelledCollectionNumber = FillLeadingZeroes(newCancelledCollectionNumber, 10);
                        }

                        String oldObject2 = Modules.SysAuditTrailModule.GetObjectString(collection.FirstOrDefault());

                        var cancelCollection = collection.FirstOrDefault();
                        cancelCollection.CancelledCollectionNumber = cancelledCollectionNumber;
                        cancelCollection.Remarks = cancelRemarks;
                        cancelCollection.IsCancelled = true;
                        cancelCollection.UpdateUserId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId);
                        cancelCollection.UpdateDateTime = DateTime.Now;
                        db.SubmitChanges();

                        String newObject2 = Modules.SysAuditTrailModule.GetObjectString(collection.FirstOrDefault());

                        Entities.SysAuditTrailEntity newAuditTrail2 = new Entities.SysAuditTrailEntity()
                        {
                            UserId = currentUserLogin.FirstOrDefault().Id,
                            AuditDate = DateTime.Now,
                            TableInformation = "TrnCollection",
                            RecordInformation = oldObject2,
                            FormInformation = newObject2,
                            ActionInformation = "CancelSales"
                        };
                        Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail2);

                        return new String[] { "", "1" };
                    }
                    else
                    {
                        return new String[] { "Collection not found.", "0" };
                    }
                }
                else
                {
                    return new String[] { "Sales not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ===============
        // Get Last Change
        // ===============
        public Decimal GetLastChange(Int32 terminalId)
        {
            Decimal lastChange = 0;

            var lastCollection = from d in db.TrnCollections
                                 where d.TerminalId == terminalId
                                 select d;

            if (lastCollection.Any())
            {
                lastChange = lastCollection.OrderByDescending(d => d.Id).FirstOrDefault().ChangeAmount;
            }

            return lastChange;
        }

        // =================
        // Get Collection Id
        // =================
        public Int32 GetCollectionId(Int32 salesId)
        {
            Int32 collectionId = 0;

            var collection = from d in db.TrnCollections
                             where d.SalesId == salesId
                             select d;

            if (collection.Any())
            {
                collectionId = collection.FirstOrDefault().Id;
            }

            return collectionId;
        }

        // ====================
        // Check Tendered Sales
        // ====================
        public Boolean IsSalesTendered(Int32 salesId)
        {
            Boolean isTendered = false;

            var collection = from d in db.TrnCollections
                             where d.SalesId == salesId
                             && d.IsLocked == true
                             && d.IsCancelled == false
                             select d;

            if (collection.Any())
            {
                isTendered = true;
            }

            return isTendered;
        }

        // ======================
        // Dropdown List Discount
        // ======================
        public List<Entities.MstDiscountEntity> DropdownListDiscount()
        {
            var discounts = from d in db.MstDiscounts
                            where d.IsLocked == true
                            select new Entities.MstDiscountEntity
                            {
                                Id = d.Id,
                                Discount = d.Discount,
                                DiscountRate = d.DiscountRate,
                            };

            return discounts.ToList();
        }

        // ==============
        // Discount Sales
        // ==============
        public String[] DiscountSales(Int32 salesId, Entities.TrnSalesEntity objSalesEntity, Int32? itemId)
        {
            try
            {
                var sales = from d in db.TrnSales
                            where d.Id == salesId
                            select d;

                if (sales.Any())
                {
                    var updateSales = sales.FirstOrDefault();
                    updateSales.DiscountId = objSalesEntity.DiscountId;
                    updateSales.SeniorCitizenId = objSalesEntity.SeniorCitizenId;
                    updateSales.SeniorCitizenName = objSalesEntity.SeniorCitizenName;
                    updateSales.SeniorCitizenAge = objSalesEntity.SeniorCitizenAge;
                    updateSales.UpdateUserId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId);
                    updateSales.UpdateDateTime = DateTime.Now;
                    updateSales.Pax = objSalesEntity.Pax;
                    updateSales.DiscountedPax = objSalesEntity.DiscountedPax;
                    db.SubmitChanges();

                    Decimal discountRate = objSalesEntity.DiscountRate;

                    var discount = from d in db.MstDiscounts
                                   where d.Id == objSalesEntity.DiscountId
                                   select d;

                    IQueryable<Data.TrnSalesLine> salesLines = from d in db.TrnSalesLines
                                                               where d.SalesId == salesId
                                                               select d;

                    if (itemId != null)
                    {
                        salesLines = from d in db.TrnSalesLines
                                     where d.SalesId == salesId
                                     && d.ItemId == itemId
                                     select d;
                    }

                    if (salesLines.Any())
                    {
                        foreach (var salesLine in salesLines)
                        {
                            Decimal quantity = salesLine.Quantity;

                            Decimal pax = Convert.ToDecimal(objSalesEntity.Pax != null ? objSalesEntity.Pax : 0);
                            Decimal discountedPax = Convert.ToDecimal(objSalesEntity.DiscountedPax != null ? objSalesEntity.DiscountedPax : 0);
                            Decimal withoutDiscountedPax = 0;

                            if (pax > discountedPax)
                            {
                                withoutDiscountedPax = pax - discountedPax;
                            }

                            Decimal pricePerPax = salesLine.Price / pax;
                            Decimal pricePerPaxVatExempt = 0;
                            Decimal discountAmountPerPax = 0;

                            Decimal netPrice = 0;
                            Decimal amount = 0;

                            Decimal taxAmount = 0;

                            if (discount.FirstOrDefault().IsVatExempt == true)
                            {
                                if (salesLine.MstItem.MstTax1.Rate > 0)
                                {
                                    Decimal VATAmountPerPax = (pricePerPax / (1 + (salesLine.MstItem.MstTax1.Rate / 100))) * (salesLine.MstItem.MstTax1.Rate / 100);

                                    pricePerPaxVatExempt = pricePerPax - VATAmountPerPax;

                                    discountAmountPerPax = pricePerPaxVatExempt * (discountRate / 100);

                                    Decimal netPriceDiscountedPax = (pricePerPaxVatExempt - discountAmountPerPax) * discountedPax;
                                    Decimal netPriceWithoutDiscountedPax = pricePerPax * withoutDiscountedPax;

                                    netPrice = netPriceDiscountedPax + netPriceWithoutDiscountedPax;

                                    amount = netPrice * quantity;
                                }
                                else
                                {
                                    discountAmountPerPax = pricePerPax * (discountRate / 100);

                                    Decimal netPriceDiscountedPax = (pricePerPax - discountAmountPerPax) * discountedPax;
                                    Decimal netPriceWithoutDiscountedPax = pricePerPax * withoutDiscountedPax;

                                    netPrice = netPriceDiscountedPax + netPriceWithoutDiscountedPax;

                                    amount = netPrice * quantity;
                                }

                                salesLine.DiscountId = discount.FirstOrDefault().Id;
                                salesLine.DiscountRate = discountRate;
                                salesLine.DiscountAmount = discountAmountPerPax * discountedPax;
                                salesLine.NetPrice = netPrice;
                                salesLine.Amount = amount;
                                salesLine.TaxId = 18;
                                salesLine.TaxRate = 0;
                                salesLine.TaxAmount = 0;
                            }
                            else
                            {
                                Decimal taxRate = salesLine.TaxRate;

                                if (taxRate > 0)
                                {
                                    discountAmountPerPax = pricePerPax * (discountRate / 100);

                                    if (withoutDiscountedPax != 0)
                                    {
                                        netPrice = (pricePerPax - discountAmountPerPax) * withoutDiscountedPax;
                                    }
                                    else
                                    {
                                        netPrice = (pricePerPax - discountAmountPerPax);
                                    }

                                    amount = netPrice * quantity;

                                    taxAmount = amount / (1 + (taxRate / 100)) * (taxRate / 100);
                                }
                                else
                                {
                                    discountAmountPerPax = pricePerPax * (discountRate / 100);

                                    if (withoutDiscountedPax != 0)
                                    {
                                        netPrice = (pricePerPax - discountAmountPerPax) * withoutDiscountedPax;
                                    }
                                    else
                                    {
                                        netPrice = (pricePerPax - discountAmountPerPax);
                                    }

                                    amount = netPrice * quantity;

                                    taxAmount = 0;
                                }

                                salesLine.DiscountId = discount.FirstOrDefault().Id;
                                salesLine.DiscountRate = discountRate;
                                salesLine.DiscountAmount = withoutDiscountedPax > 0 ? discountAmountPerPax * withoutDiscountedPax : discountAmountPerPax;
                                salesLine.NetPrice = netPrice;
                                salesLine.Amount = amount;
                                salesLine.TaxAmount = taxAmount;
                            }

                            db.SubmitChanges();
                        }
                    }

                    if (discount.FirstOrDefault().Id == 2)
                    {
                        updateSales.DiscountId = null;
                        updateSales.SeniorCitizenId = "";
                        updateSales.SeniorCitizenName = "";
                        updateSales.SeniorCitizenAge = null;
                    }

                    updateSales.Amount = sales.FirstOrDefault().TrnSalesLines.Any() ? sales.FirstOrDefault().TrnSalesLines.Sum(d => d.Amount) : 0;
                    db.SubmitChanges();

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Sales not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // =====================
        // Discount Detail Sales 
        // =====================
        public Entities.TrnSalesEntity DiscountDetailSales(Int32 id)
        {
            var sales = from d in db.TrnSales
                        where d.Id == id
                        select new Entities.TrnSalesEntity
                        {
                            DiscountId = d.DiscountId,
                            SeniorCitizenId = d.SeniorCitizenId,
                            SeniorCitizenName = d.SeniorCitizenName,
                            SeniorCitizenAge = d.SeniorCitizenAge
                        };

            return sales.FirstOrDefault();
        }

        // ===================================
        // Can Cancel Collection Previous Date
        // ===================================
        public Boolean CanCancelCollection(Int32 salesId)
        {
            var collection = from d in db.TrnCollections
                             where d.SalesId == salesId
                             select d;

            if (collection.Any())
            {
                var lastCollection = from d in db.TrnCollections.OrderByDescending(d => d.Id)
                                     where d.IsLocked == true
                                     //&& d.CollectionDate == DateTime.Today
                                     select d;



                if (lastCollection.Any())
                {
                    if (lastCollection.FirstOrDefault().CollectionDate.Date == collection.FirstOrDefault().CollectionDate)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        // ===================================
        // Allow Cancel Collection Previous Date
        // ===================================
        public Boolean AllowCancelCollection(Int32 salesId)
        {
            var collection = from d in db.TrnCollections
                             where d.SalesId == salesId
                             select d;

            if (collection.Any())
            {
                var lastCollection = from d in db.TrnCollections.OrderByDescending(d => d.Id)
                                     where d.IsLocked == true
                                     select d;

                if (lastCollection.Any())
                {
                    if (lastCollection.FirstOrDefault().CollectionDate.Date == collection.FirstOrDefault().CollectionDate)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        // ==========
        // Lock Sales
        // ==========
        public String[] LockSales(Int32 salesId, Entities.TrnSalesEntity objSales)
        {
            try
            {
                var sales = from d in db.TrnSales
                            where d.Id == salesId
                            select d;

                if (sales.Any())
                {
                    if (Modules.SysCurrentModule.GetCurrentSettings().AllowNegativeInventory == false)
                    {
                        Boolean isNegativeInventory = false;
                        String negativeInventoryItem = "";

                        if (sales.FirstOrDefault().TrnSalesLines.Where(d => d.MstItem.IsInventory == true).Any())
                        {
                            var groupedSalesLines = from d in sales.FirstOrDefault().TrnSalesLines.Where(d => d.MstItem.IsInventory == true)
                                                    group d by d.MstItem into g
                                                    select g;

                            foreach (var salesLine in groupedSalesLines.ToList())
                            {
                                negativeInventoryItem = salesLine.Key.ItemDescription;

                                if (salesLine.Key.OnhandQuantity <= -1)
                                {
                                    isNegativeInventory = true;
                                    break;
                                }
                                else
                                {
                                    if (salesLine.Key.OnhandQuantity < salesLine.Sum(d => d.Quantity))
                                    {
                                        isNegativeInventory = true;
                                        break;
                                    }
                                }
                            }
                        }

                        if (isNegativeInventory == true)
                        {
                            return new String[] { "Negative inventory item found. " + negativeInventoryItem, "0" };
                        }
                    }

                    var customerSales = from d in db.TrnSales
                                        where d.CustomerId == objSales.CustomerId
                                        select d;

                    if (customerSales.Any())
                    {
                        Decimal customerTerms = customerSales.FirstOrDefault().MstCustomer.MstTerm.NumberOfDays;

                        var previousSales = from d in customerSales
                                            where d.SalesDate < sales.FirstOrDefault().SalesDate
                                            && d.BalanceAmount > 0
                                            select d;

                        if (previousSales.Any())
                        {
                            if (customerTerms != 0)
                            {
                                var start_sales_date = previousSales.OrderBy(d => d.SalesDate).FirstOrDefault().SalesDate;
                                var end_sales_date = sales.FirstOrDefault().SalesDate;

                                var totalDays = (end_sales_date - start_sales_date).TotalDays;

                                if (Convert.ToDecimal(totalDays) > customerTerms)
                                {
                                    return new String[] { "Please settle the previous balance first.", "0" };
                                }
                            }
                        }
                    }

                    var lockSales = sales.FirstOrDefault();
                    lockSales.CustomerId = objSales.CustomerId;
                    lockSales.TermId = objSales.TermId;
                    lockSales.Remarks = objSales.Remarks;
                    lockSales.SalesAgent = objSales.SalesAgent;
                    lockSales.IsLocked = true;
                    lockSales.BalanceAmount = objSales.Amount;
                    lockSales.UpdateUserId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId);
                    lockSales.UpdateDateTime = DateTime.Now;
                    db.SubmitChanges();

                    Modules.TrnInventoryModule trnInventoryModule = new Modules.TrnInventoryModule();
                    trnInventoryModule.UpdateSalesInventory(salesId);

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Sales not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ============
        // Unlock Sales
        // ============
        public String[] UnlockSales(Int32 salesId)
        {
            try
            {
                var sales = from d in db.TrnSales
                            where d.Id == salesId
                            select d;

                if (sales.Any())
                {
                    var updateSales = sales.FirstOrDefault();
                    updateSales.IsLocked = false;
                    updateSales.UpdateUserId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId);
                    updateSales.UpdateDateTime = DateTime.Now;
                    db.SubmitChanges();

                    Modules.TrnInventoryModule trnInventoryModule = new Modules.TrnInventoryModule();
                    trnInventoryModule.UpdateSalesInventory(salesId);

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Sales not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ============
        // Table Groups
        // ============
        public List<Entities.MstTableGroupEntity> ListTableGroup()
        {
            var tableGroups = from d in db.MstTableGroups
                              where d.TableGroup != "Walk-in" && d.TableGroup != "Delivery"
                              select new Entities.MstTableGroupEntity
                              {
                                  Id = d.Id,
                                  TableGroup = d.TableGroup,
                                  EntryUserId = d.EntryUserId,
                                  EntryUserName = d.MstUser.UserName,
                                  EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                  UpdateUserId = d.UpdateUserId,
                                  UpdatedUserName = d.MstUser1.UserName,
                                  UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                                  IsLocked = d.IsLocked,
                              };

            return tableGroups.OrderBy(d => d.TableGroup).ToList();
        }

        // ======
        // Tables
        // ======
        public List<Entities.MstTableEntity> ListTable(Int32 tableGroupId)
        {
            var tables = from d in db.MstTables
                         where d.TableGroupId == tableGroupId
                         select new Entities.MstTableEntity
                         {
                             Id = d.Id,
                             TableCode = d.TableCode,
                         };

            return tables.OrderBy(d => d.TableCode).ToList();
        }

        // ===============
        // List Item Group 
        // ===============
        public List<Entities.MstItemGroupEntity> ListItemGroup()
        {
            var itemGroups = from d in db.MstItemGroups
                             select new Entities.MstItemGroupEntity
                             {
                                 Id = d.Id,
                                 ItemGroup = d.ItemGroup,
                                 ImagePath = d.ImagePath,
                                 KitchenReport = d.KitchenReport,
                                 EntryUserId = d.EntryUserId,
                                 EntryUserName = d.MstUser.UserName,
                                 EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                 UpdateUserId = d.UpdateUserId,
                                 UpdatedUserName = d.MstUser1.UserName,
                                 UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                                 IsLocked = d.IsLocked,
                             };

            return itemGroups.OrderBy(d => d.ItemGroup).ToList();
        }

        // ====================
        // List Item Group Item
        // ====================
        public List<Entities.MstItemGroupItemEntity> ListItemGroupItem(Int32 itemGroupId)
        {
            var itemGroupItems = from d in db.MstItemGroupItems
                                 where d.ItemGroupId == itemGroupId
                                 && d.Show == true
                                 select new Entities.MstItemGroupItemEntity
                                 {
                                     Id = d.Id,
                                     ItemId = d.ItemId,
                                     Barcode = d.MstItem.BarCode,
                                     Alias = d.MstItem.Alias
                                 };

            return itemGroupItems.OrderBy(d => d.Alias).ToList();
        }

        // ======================
        // Get Sales Return Items
        // ======================
        public List<Entities.TrnSalesLineEntity> ListReturnSalesItems(String ORNumber)
        {
            var collection = from d in db.TrnCollections
                             where d.CollectionNumber == ORNumber
                             && d.TerminalId == Modules.SysCurrentModule.GetCurrentSettings().TerminalId
                             && d.SalesId != null
                             && d.IsLocked == true
                             && d.IsCancelled == false
                             select d;

            if (collection.Any())
            {
                var salesLines = from d in db.TrnSalesLines
                                 where d.SalesId == collection.FirstOrDefault().SalesId
                                 group d by new
                                 {
                                     d.ItemId,
                                     d.MstItem.ItemDescription,
                                     d.MstItem.MstUnit.Unit,
                                     d.Price,
                                     d.DiscountAmount,
                                     d.NetPrice,
                                     d.TaxId,
                                     d.MstTax.Rate,
                                     d.TaxAmount

                                 } into g
                                 select new Entities.TrnSalesLineEntity
                                 {
                                     ItemId = g.Key.ItemId,
                                     ItemDescription = g.Key.ItemDescription,
                                     Unit = g.Key.Unit,
                                     Price = g.Key.Price,
                                     DiscountAmount = g.Key.DiscountAmount,
                                     NetPrice = g.Key.NetPrice,
                                     Quantity = g.Sum(s => s.Quantity),
                                     Amount = g.Key.NetPrice * g.Sum(s => s.Quantity),
                                     TaxId = g.Key.TaxId,
                                     TaxRate = g.Key.Rate,
                                     TaxAmount = g.Key.TaxAmount * -1
                                 };

                return salesLines.ToList();
            }
            else
            {
                return new List<Entities.TrnSalesLineEntity>();
            }
        }

        // =============================
        // Get Sales Return Sales Number
        // =============================
        public Entities.TrnCollectionEntity GetCurrentCollection(String ORNumber)
        {
            var collection = from d in db.TrnCollections
                             where d.CollectionNumber == ORNumber
                             && d.SalesId != null
                             select new Entities.TrnCollectionEntity
                             {
                                 Id = d.Id,
                                 SalesId = d.SalesId,
                                 SalesNumber = d.TrnSale.SalesNumber
                             };

            if (collection.Any())
            {
                return collection.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        // ============
        // Sales Return
        // ============
        public String[] ReturnSalesItems(Int32 currentSalesId, Int32 collectionId, List<Entities.TrnSalesLineEntity> objSalesLines)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var discount = from d in db.MstDiscounts where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().DefaultDiscountId) select d;
                if (discount.Any() == false)
                {
                    return new String[] { "Discount not found.", "0" };
                }

                var collection = from d in db.TrnCollections
                                 where d.Id == collectionId
                                 && d.IsLocked == true
                                 && d.IsCancelled == false
                                 select d;

                if (collection.Any() == false)
                {
                    return new String[] { "Collection not found.", "0" };
                }

                var sales = from d in db.TrnSales
                            where d.Id == collection.FirstOrDefault().SalesId
                            && d.IsLocked == true
                            && d.IsCancelled == false
                            select d;

                if (sales.Any() == false)
                {
                    return new String[] { "Sales not found.", "0" };
                }

                //var checkSalesIfReturn = from d in db.TrnSales
                //                         where d.IsLocked == true
                //                         && d.IsReturned == true
                //                         select d;
                //if (checkSalesIfReturn.Any())
                //{
                //    return new String[] { "Already returned.", "0" };
                //}

                Decimal totalAmount = 0;

                List<Data.TrnSalesLine> newSalesLines = new List<Data.TrnSalesLine>();
                if (objSalesLines.Any())
                {
                    foreach (var objSalesLine in objSalesLines)
                    {
                        var item = from d in db.MstItems
                                   where d.Id == objSalesLine.ItemId
                                   select d;

                        if (item.Any())
                        {
                            newSalesLines.Add(new Data.TrnSalesLine
                            {
                                SalesId = currentSalesId,
                                ItemId = item.FirstOrDefault().Id,
                                UnitId = item.FirstOrDefault().UnitId,
                                Price = objSalesLine.Price,
                                DiscountId = discount.FirstOrDefault().Id,
                                DiscountRate = discount.FirstOrDefault().DiscountRate,
                                DiscountAmount = objSalesLine.DiscountAmount,
                                NetPrice = objSalesLine.NetPrice,
                                Quantity = objSalesLine.Quantity * -1,
                                Amount = (objSalesLine.Price * objSalesLine.Quantity) * -1,
                                TaxId = objSalesLine.TaxId,
                                TaxRate = objSalesLine.TaxRate,
                                TaxAmount = objSalesLine.TaxAmount,
                                SalesAccountId = 159,
                                AssetAccountId = 255,
                                CostAccountId = 238,
                                TaxAccountId = 87,
                                SalesLineTimeStamp = DateTime.Now,
                                UserId = currentUserLogin.FirstOrDefault().Id,
                                Preparation = "NA",
                                IsPrepared = false,
                                Price1 = 0,
                                Price2 = 0,
                                Price2LessTax = 0,
                                PriceSplitPercentage = 0,
                            });

                            totalAmount += (objSalesLine.Price * objSalesLine.Quantity) * -1;
                        }
                    }

                    db.TrnSalesLines.InsertAllOnSubmit(newSalesLines);
                    db.SubmitChanges();
                }
                else
                {
                    return new String[] { "There are no items to return.", "0" };
                }

                var currentSales = from d in db.TrnSales
                                   where d.Id == currentSalesId
                                   select d;

                if (currentSales.Any())
                {
                    var updateSales = currentSales.FirstOrDefault();
                    updateSales.Amount = totalAmount;
                    updateSales.Remarks = "Return from customer.";
                    updateSales.IsReturned = true;
                    updateSales.ReturnApplication = "Return";
                    updateSales.UpdateUserId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId);
                    updateSales.UpdateDateTime = DateTime.Now;
                    db.SubmitChanges();
                }
                else
                {
                    return new String[] { "Current sales not found.", "0" };
                }

                String newObject = Modules.SysAuditTrailModule.GetObjectString(newSalesLines);

                Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                {
                    UserId = currentUserLogin.FirstOrDefault().Id,
                    AuditDate = DateTime.Now,
                    TableInformation = "TrnSalesLine",
                    RecordInformation = "",
                    FormInformation = newObject,
                    ActionInformation = "ReturnSalesItems"
                };
                Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                return new String[] { "", "1" };
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ===========
        // List Orders
        // ===========
        public List<Entities.TrnSalesEntity> ListOrders(DateTime dateTime, Int32 terminalId, String filter, String orderStatus)
        {
            if (orderStatus == "New")
            {
                var sales = from d in db.TrnSales
                            where d.SalesDate == dateTime
                            && d.TerminalId == terminalId
                            && d.IsLocked == true
                            && d.IsTendered == false
                            && d.IsCancelled == false
                            && d.IsDispatched == false
                            && (d.SalesNumber.Contains(filter)
                            || d.ManualInvoiceNumber.Contains(filter)
                            || d.MstCustomer.Customer.Contains(filter)
                            || d.DeliveryDriver.Contains(filter))
                            select new Entities.TrnSalesEntity
                            {
                                Id = d.Id,
                                PeriodId = d.PeriodId,
                                Period = d.MstPeriod.Period,
                                SalesDate = d.SalesDate.ToShortDateString(),
                                SalesNumber = d.SalesNumber,
                                ManualInvoiceNumber = d.ManualInvoiceNumber,
                                CollectionNumber = d.CollectionNumber,
                                Amount = d.Amount,
                                TableId = d.TableId,
                                Table = d.TableId != null ? d.MstTable.TableCode : "",
                                TableStatus = d.TableStatus,
                                CustomerId = d.CustomerId,
                                CustomerCode = d.MstCustomer.CustomerCode,
                                Customer = d.MstCustomer.Customer,
                                CustomerAddress = d.MstCustomer.Address,
                                AccountId = d.AccountId,
                                TermId = d.TermId,
                                Term = d.MstTerm.Term,
                                DiscountId = d.DiscountId,
                                SeniorCitizenId = d.SeniorCitizenId,
                                SeniorCitizenName = d.SeniorCitizenName,
                                SeniorCitizenAge = d.SeniorCitizenAge,
                                Remarks = d.Remarks,
                                SalesAgent = d.SalesAgent,
                                SalesAgentUserName = d.SalesAgent != null ? d.MstUser5.UserName : "",
                                TerminalId = d.TerminalId,
                                Terminal = d.MstTerminal.Terminal,
                                DeliveryId = d.DeliveryId,
                                DeliveryDriver = d.DeliveryDriver != null ? d.DeliveryDriver : "",
                                PreparedBy = d.PreparedBy,
                                PreparedByUserName = d.MstUser.FullName,
                                CheckedBy = d.CheckedBy,
                                CheckedByUserName = d.MstUser1.FullName,
                                ApprovedBy = d.ApprovedBy,
                                ApprovedByUserName = d.MstUser2.FullName,
                                IsLocked = d.IsLocked,
                                IsTendered = d.IsTendered,
                                IsCancelled = d.IsCancelled,
                                IsDispatched = d.IsDispatched,
                                IsReturned = d.IsReturned,
                                ReturnApplication = d.ReturnApplication,
                                PaidAmount = d.PaidAmount,
                                CreditAmount = d.CreditAmount,
                                DebitAmount = d.DebitAmount,
                                BalanceAmount = d.BalanceAmount,
                                EntryUserId = d.EntryUserId,
                                EntryUserName = d.MstUser3.FullName,
                                EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                UpdateUserId = d.UpdateUserId,
                                UpdatedUserName = d.MstUser4.FullName,
                                UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                                Pax = d.Pax,
                                NumberOfItems = d.TrnSalesLines.Any() ? d.TrnSalesLines.Count() : 0,
                                NumberOfItemsPrepared = d.TrnSalesLines.Any() ? d.TrnSalesLines.Where(i => i.IsPrepared == true).Count() : 0,
                                Status = d.TrnSalesLines.Any() ? d.TrnSalesLines.Count() - d.TrnSalesLines.Where(i => i.IsPrepared == true).Count() == 0 ? "Ready for Dispatch" : "Preparing" : ""
                            };

                return sales.OrderByDescending(d => d.Id).ToList();
            }
            else if (orderStatus == "Dispatched")
            {
                var sales = from d in db.TrnSales
                            where d.SalesDate == dateTime
                            && d.TerminalId == terminalId
                            && d.IsLocked == true
                            && d.IsTendered == false
                            && d.IsCancelled == false
                            && d.IsDispatched == true
                            && (d.SalesNumber.Contains(filter)
                            || d.ManualInvoiceNumber.Contains(filter)
                            || d.MstCustomer.Customer.Contains(filter)
                            || d.DeliveryDriver.Contains(filter))
                            select new Entities.TrnSalesEntity
                            {
                                Id = d.Id,
                                PeriodId = d.PeriodId,
                                Period = d.MstPeriod.Period,
                                SalesDate = d.SalesDate.ToShortDateString(),
                                SalesNumber = d.SalesNumber,
                                ManualInvoiceNumber = d.ManualInvoiceNumber,
                                CollectionNumber = d.CollectionNumber,
                                Amount = d.Amount,
                                TableId = d.TableId,
                                Table = d.TableId != null ? d.MstTable.TableCode : "",
                                TableStatus = d.TableStatus,
                                CustomerId = d.CustomerId,
                                CustomerCode = d.MstCustomer.CustomerCode,
                                Customer = d.MstCustomer.Customer,
                                CustomerAddress = d.MstCustomer.Address,
                                AccountId = d.AccountId,
                                TermId = d.TermId,
                                Term = d.MstTerm.Term,
                                DiscountId = d.DiscountId,
                                SeniorCitizenId = d.SeniorCitizenId,
                                SeniorCitizenName = d.SeniorCitizenName,
                                SeniorCitizenAge = d.SeniorCitizenAge,
                                Remarks = d.Remarks,
                                SalesAgent = d.SalesAgent,
                                SalesAgentUserName = d.SalesAgent != null ? d.MstUser5.UserName : "",
                                TerminalId = d.TerminalId,
                                Terminal = d.MstTerminal.Terminal,
                                DeliveryId = d.DeliveryId,
                                DeliveryDriver = d.DeliveryDriver != null ? d.DeliveryDriver : "",
                                PreparedBy = d.PreparedBy,
                                PreparedByUserName = d.MstUser.FullName,
                                CheckedBy = d.CheckedBy,
                                CheckedByUserName = d.MstUser1.FullName,
                                ApprovedBy = d.ApprovedBy,
                                ApprovedByUserName = d.MstUser2.FullName,
                                IsLocked = d.IsLocked,
                                IsTendered = d.IsTendered,
                                IsCancelled = d.IsCancelled,
                                IsDispatched = d.IsDispatched,
                                IsReturned = d.IsReturned,
                                ReturnApplication = d.ReturnApplication,
                                PaidAmount = d.PaidAmount,
                                CreditAmount = d.CreditAmount,
                                DebitAmount = d.DebitAmount,
                                BalanceAmount = d.BalanceAmount,
                                EntryUserId = d.EntryUserId,
                                EntryUserName = d.MstUser3.FullName,
                                EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                UpdateUserId = d.UpdateUserId,
                                UpdatedUserName = d.MstUser4.FullName,
                                UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                                Pax = d.Pax,
                                NumberOfItems = d.TrnSalesLines.Any() ? d.TrnSalesLines.Count() : 0,
                                NumberOfItemsPrepared = d.TrnSalesLines.Any() ? d.TrnSalesLines.Where(i => i.IsPrepared == true).Count() : 0,
                                Status = "To be delivered"
                            };

                return sales.OrderByDescending(d => d.Id).ToList();
            }
            else if (orderStatus == "Delivered")
            {
                var sales = from d in db.TrnSales
                            where d.SalesDate == dateTime
                            && d.TerminalId == terminalId
                            && d.IsLocked == true
                            && d.IsTendered == true
                            && d.IsCancelled == false
                            && d.IsDispatched == true
                            && (d.SalesNumber.Contains(filter)
                            || d.ManualInvoiceNumber.Contains(filter)
                            || d.MstCustomer.Customer.Contains(filter)
                            || d.DeliveryDriver.Contains(filter))
                            select new Entities.TrnSalesEntity
                            {
                                Id = d.Id,
                                PeriodId = d.PeriodId,
                                Period = d.MstPeriod.Period,
                                SalesDate = d.SalesDate.ToShortDateString(),
                                SalesNumber = d.SalesNumber,
                                ManualInvoiceNumber = d.ManualInvoiceNumber,
                                CollectionNumber = d.CollectionNumber,
                                Amount = d.Amount,
                                TableId = d.TableId,
                                Table = d.TableId != null ? d.MstTable.TableCode : "",
                                TableStatus = d.TableStatus,
                                CustomerId = d.CustomerId,
                                CustomerCode = d.MstCustomer.CustomerCode,
                                Customer = d.MstCustomer.Customer,
                                CustomerAddress = d.MstCustomer.Address,
                                AccountId = d.AccountId,
                                TermId = d.TermId,
                                Term = d.MstTerm.Term,
                                DiscountId = d.DiscountId,
                                SeniorCitizenId = d.SeniorCitizenId,
                                SeniorCitizenName = d.SeniorCitizenName,
                                SeniorCitizenAge = d.SeniorCitizenAge,
                                Remarks = d.Remarks,
                                SalesAgent = d.SalesAgent,
                                SalesAgentUserName = d.SalesAgent != null ? d.MstUser5.UserName : "",
                                TerminalId = d.TerminalId,
                                Terminal = d.MstTerminal.Terminal,
                                DeliveryId = d.DeliveryId,
                                DeliveryDriver = d.DeliveryDriver != null ? d.DeliveryDriver : "",
                                PreparedBy = d.PreparedBy,
                                PreparedByUserName = d.MstUser.FullName,
                                CheckedBy = d.CheckedBy,
                                CheckedByUserName = d.MstUser1.FullName,
                                ApprovedBy = d.ApprovedBy,
                                ApprovedByUserName = d.MstUser2.FullName,
                                IsLocked = d.IsLocked,
                                IsTendered = d.IsTendered,
                                IsCancelled = d.IsCancelled,
                                IsDispatched = d.IsDispatched,
                                IsReturned = d.IsReturned,
                                ReturnApplication = d.ReturnApplication,
                                PaidAmount = d.PaidAmount,
                                CreditAmount = d.CreditAmount,
                                DebitAmount = d.DebitAmount,
                                BalanceAmount = d.BalanceAmount,
                                EntryUserId = d.EntryUserId,
                                EntryUserName = d.MstUser3.FullName,
                                EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                UpdateUserId = d.UpdateUserId,
                                UpdatedUserName = d.MstUser4.FullName,
                                UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                                Pax = d.Pax,
                                NumberOfItems = d.TrnSalesLines.Any() ? d.TrnSalesLines.Count() : 0,
                                NumberOfItemsPrepared = d.TrnSalesLines.Any() ? d.TrnSalesLines.Where(i => i.IsPrepared == true).Count() : 0,
                                Status = "Paid and Delivered"
                            };

                return sales.OrderByDescending(d => d.Id).ToList();
            }
            else
            {
                return new List<Entities.TrnSalesEntity>();
            }
        }

        // ==============
        // Dispatch Sales
        // ==============
        public String[] DispatchSales(Int32 salesId)
        {
            try
            {
                var sales = from d in db.TrnSales
                            where d.Id == salesId
                            select d;

                if (sales.Any())
                {
                    if (sales.FirstOrDefault().IsDispatched == true)
                    {
                        return new String[] { "Already dispatched.", "0" };
                    }

                    if (sales.FirstOrDefault().TrnSalesLines.Any())
                    {
                        if (sales.FirstOrDefault().TrnSalesLines.Count() - sales.FirstOrDefault().TrnSalesLines.Where(d => d.IsPrepared == true).Count() != 0)
                        {
                            return new String[] { "Cannot dispatch if some items are not prepared.", "0" };
                        }
                    }

                    String manualInvoiceNumber = sales.FirstOrDefault().ManualInvoiceNumber;

                    var updateSales = sales.FirstOrDefault();
                    updateSales.IsDispatched = true;
                    updateSales.UpdateUserId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId);
                    updateSales.UpdateDateTime = DateTime.Now;
                    db.SubmitChanges();

                    if (Modules.SysCurrentModule.GetCurrentSettings().EnableEasyShopIntegration == true)
                    {
                        if (sales.FirstOrDefault().DeliveryId != null)
                        {
                            EasyShopReadyForDispatchRequest(Convert.ToInt32(sales.FirstOrDefault().DeliveryId));
                        }
                    }

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Sales not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { "", "1" };
            }
        }

        // ============================
        // Inform EasyShop for Dispatch
        // ============================
        public void EasyShopReadyForDispatchRequest(Int32 deliveryId)
        {
            try
            {
                // ============
                // Http Request
                // ============
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.fiestatogo.com.ph/easydelivery/deliveries/" + deliveryId + "/ready");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "PATCH";

                // ====
                // Data
                // ====
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {

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

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // ====================
        // Dropdown List Driver
        // ====================
        public List<Entities.SysDriver> DropdownListDriver()
        {
            List<Entities.SysDriver> drivers = new List<Entities.SysDriver>();

            // ============
            // Http Request
            // ============
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.fiestatogo.com.ph/easydelivery/drivers");
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
                Entities.SysDeliverDriver driver = (Entities.SysDeliverDriver)js.Deserialize(result, typeof(Entities.SysDeliverDriver));

                if (driver != null)
                {
                    if (driver.data.Any())
                    {
                        var listDrivers = from d in driver.data
                                          select new Entities.SysDriver
                                          {
                                              Id = d.attributes.id,
                                              FullName = d.attributes != null ? d.attributes.first_name + " " + d.attributes.last_name : ""
                                          };

                        drivers = listDrivers.ToList();
                    }
                }
            }

            return drivers;
        }

        // ===================
        // Assign Driver Sales
        // ===================
        public String[] AssignDriverSales(Int32 salesId, String driverName, String driverId)
        {
            try
            {
                var sales = from d in db.TrnSales
                            where d.Id == salesId
                            select d;

                if (sales.Any())
                {
                    if (sales.FirstOrDefault().IsLocked == true)
                    {
                        return new String[] { "Already locked.", "0" };
                    }

                    var updateSales = sales.FirstOrDefault();
                    updateSales.DeliveryDriver = driverName;
                    updateSales.IsLocked = true;
                    updateSales.UpdateUserId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId);
                    updateSales.UpdateDateTime = DateTime.Now;
                    db.SubmitChanges();

                    var cloudSettings = from d in db.IntCloudSettings select d;

                    var updatedSales = from d in db.TrnSales
                                       where d.Id == salesId
                                       select d;

                    if (updatedSales.Any())
                    {
                        var deliverOrder = new Entities.SysDeliveryOrder()
                        {
                            branch_code = cloudSettings.FirstOrDefault().BranchCode,
                            order_id = updatedSales.FirstOrDefault().ManualInvoiceNumber,
                            total = updatedSales.FirstOrDefault().Amount.ToString(),
                            driver_id = driverId
                        };

                        var deliveryData = new Entities.SysDelivery()
                        {
                            delivery = deliverOrder
                        };

                        if (Modules.SysCurrentModule.GetCurrentSettings().EnableEasyShopIntegration == true)
                        {
                            EasyShopDeliveryRequest(deliveryData, salesId);
                        }
                    }

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Sales not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ====================================
        // Inform EasyShop for Delivery Request
        // ====================================
        public void EasyShopDeliveryRequest(Entities.SysDelivery objDelivery, Int32 salesId)
        {
            try
            {
                var deliverOrder = new Entities.SysDeliveryOrder()
                {
                    branch_code = objDelivery.delivery.branch_code,
                    order_id = objDelivery.delivery.order_id,
                    total = objDelivery.delivery.total,
                    driver_id = objDelivery.delivery.driver_id
                };

                var deliveryData = new Entities.SysDelivery()
                {
                    delivery = deliverOrder
                };

                String json = new JavaScriptSerializer().Serialize(deliveryData);

                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.fiestatogo.com.ph/easydelivery/deliveries");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    Entities.SysDelivery delivery = new JavaScriptSerializer().Deserialize<Entities.SysDelivery>(json);
                    streamWriter.Write(new JavaScriptSerializer().Serialize(delivery));
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    if (result != null)
                    {
                        var definition = new
                        {
                            deliveries = new
                            {
                                data = new
                                {
                                    id = 0
                                }
                            }
                        };

                        String jsonResult = result;
                        var dataReturn = JsonConvert.DeserializeAnonymousType(jsonResult, definition);

                        Int32 deliverId = Convert.ToInt32(dataReturn.deliveries.data.id);

                        var sales = from d in db.TrnSales
                                    where d.Id == salesId
                                    select d;

                        if (sales.Any())
                        {
                            var updateDelivery = sales.FirstOrDefault();
                            updateDelivery.DeliveryId = deliverId;

                            db.SubmitChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // ================================
        // Inform EasyShop for Already Paid
        // ================================
        public void EasyShopAlreadyPaid(String documentReference)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://api.fiestatogo.com.ph/easyorder/orders/" + documentReference + "/paid");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "PATCH";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {

                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    if (result != null)
                    {

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        // ================
        // Tender All Sales
        // ================
        public String[] TenderAllSales(List<Int32> salesIds)
        {
            try
            {
                if (salesIds.Any())
                {
                    foreach (var salesId in salesIds)
                    {
                        var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                        if (currentUserLogin.Any() == false)
                        {
                            return new String[] { "Current login user not found.", "0" };
                        }

                        var currentSales = from d in db.TrnSales
                                           where d.Id == salesId
                                           && d.IsTendered == false
                                           select d;

                        if (!currentSales.Any())
                        {
                            return new String[] { "Sales not found.", "0" };
                        }

                        if (Modules.SysCurrentModule.GetCurrentSettings().AllowNegativeInventory == false)
                        {
                            Boolean isNegativeInventory = false;
                            String negativeInventoryItem = "";

                            if (currentSales.FirstOrDefault().TrnSalesLines.Where(d => d.MstItem.IsInventory == true).Any())
                            {
                                var groupedSalesLines = from d in currentSales.FirstOrDefault().TrnSalesLines.Where(d => d.MstItem.IsInventory == true)
                                                        group d by d.MstItem into g
                                                        select g;

                                foreach (var salesLine in groupedSalesLines.ToList())
                                {
                                    negativeInventoryItem = salesLine.Key.ItemDescription;

                                    if (salesLine.Key.OnhandQuantity <= 0)
                                    {
                                        isNegativeInventory = true;
                                        break;
                                    }
                                    else
                                    {
                                        if (salesLine.Key.OnhandQuantity < salesLine.Sum(d => d.Quantity))
                                        {
                                            isNegativeInventory = true;
                                            break;
                                        }
                                    }
                                }
                            }

                            if (isNegativeInventory == true)
                            {
                                return new String[] { "Negative inventory item found. " + negativeInventoryItem, "0" };
                            }

                            Boolean isNegativeInventoryComponent = false;
                            String negativeInventoryComponentItem = "";

                            if (currentSales.FirstOrDefault().TrnSalesLines.Where(d => d.MstItem.IsInventory == false).Any())
                            {
                                var groupedSalesLines = from d in currentSales.FirstOrDefault().TrnSalesLines.Where(d => d.MstItem.IsInventory == false)
                                                        group d by d.MstItem into g
                                                        select g;

                                foreach (var salesLine in groupedSalesLines.ToList())
                                {
                                    if (salesLine.Key.MstItemComponents.Any())
                                    {
                                        foreach (var component in salesLine.Key.MstItemComponents)
                                        {
                                            negativeInventoryComponentItem = component.MstItem1.ItemDescription;

                                            if (component.MstItem1.OnhandQuantity <= 0)
                                            {
                                                isNegativeInventoryComponent = true;
                                                break;
                                            }
                                            else
                                            {
                                                if (component.MstItem1.OnhandQuantity < (salesLine.Sum(d => d.Quantity) * component.Quantity))
                                                {
                                                    isNegativeInventoryComponent = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (isNegativeInventoryComponent == true)
                            {
                                return new String[] { "Negative inventory component item found. " + negativeInventoryComponentItem, "0" };
                            }
                        }

                        var collectedSales = from d in db.TrnCollections
                                             where d.SalesId == salesId
                                             && d.IsLocked == true
                                             select d;

                        if (collectedSales.Any())
                        {
                            return new String[] { "Sales already collected.", "0" };
                        }

                        String collectionNumber = "0000000001";
                        var lastCollection = from d in db.TrnCollections.OrderByDescending(d => d.Id)
                                             where d.TerminalId == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().TerminalId)
                                             select d;

                        if (lastCollection.Any())
                        {
                            Int32 newCollectionNumber = Convert.ToInt32(lastCollection.FirstOrDefault().CollectionNumber) + 1;
                            collectionNumber = FillLeadingZeroes(newCollectionNumber, 10);
                        }

                        DateTime currentDate = DateTime.Today;
                        if (Modules.SysCurrentModule.GetCurrentSettings().IsLoginDate == true)
                        {
                            currentDate = Convert.ToDateTime(Modules.SysCurrentModule.GetCurrentSettings().CurrentDate);
                        }

                        Data.TrnCollection newCollection = new Data.TrnCollection
                        {
                            PeriodId = currentSales.FirstOrDefault().PeriodId,
                            CollectionDate = currentDate,
                            CollectionNumber = collectionNumber,
                            TerminalId = currentSales.FirstOrDefault().TerminalId,
                            ManualORNumber = currentSales.FirstOrDefault().MstTerminal.Terminal + "-" + collectionNumber,
                            CustomerId = currentSales.FirstOrDefault().CustomerId,
                            Remarks = currentSales.FirstOrDefault().Remarks,
                            SalesId = currentSales.FirstOrDefault().Id,
                            SalesBalanceAmount = currentSales.FirstOrDefault().BalanceAmount,
                            Amount = 0,
                            TenderAmount = currentSales.FirstOrDefault().Amount,
                            ChangeAmount = 0,
                            PreparedBy = currentUserLogin.FirstOrDefault().Id,
                            CheckedBy = currentUserLogin.FirstOrDefault().Id,
                            ApprovedBy = currentUserLogin.FirstOrDefault().Id,
                            IsCancelled = false,
                            IsLocked = false,
                            EntryUserId = currentUserLogin.FirstOrDefault().Id,
                            EntryDateTime = DateTime.Now,
                            UpdateUserId = currentUserLogin.FirstOrDefault().Id,
                            UpdateDateTime = DateTime.Now
                        };

                        db.TrnCollections.InsertOnSubmit(newCollection);
                        db.SubmitChanges();

                        var payType = from d in db.MstPayTypes
                                      where d.PayTypeCode == "CASH"
                                      select d;

                        if (!payType.Any())
                        {
                            return new String[] { "Pay type cash not found.", "0" };
                        }

                        Data.TrnCollectionLine newCollectionLine = new Data.TrnCollectionLine()
                        {
                            CollectionId = newCollection.Id,
                            Amount = currentSales.FirstOrDefault().Amount,
                            PayTypeId = payType.FirstOrDefault().Id,
                            CheckNumber = "NA",
                            CheckDate = null,
                            CheckBank = "NA",
                            CreditCardVerificationCode = "NA",
                            CreditCardNumber = "NA",
                            CreditCardType = "NA",
                            CreditCardBank = "NA",
                            GiftCertificateNumber = "NA",
                            OtherInformation = "NA",
                            SalesReturnSalesId = null,
                            StockInId = null,
                            AccountId = Convert.ToInt32(payType.FirstOrDefault().AccountId),
                            CreditCardReferenceNumber = "NA",
                            CreditCardHolderName = "NA",
                            CreditCardExpiry = "NA",
                        };

                        db.TrnCollectionLines.InsertOnSubmit(newCollectionLine);
                        db.SubmitChanges();

                        Decimal salesAmount = currentSales.FirstOrDefault().Amount;
                        Decimal paidAmount = 0;

                        var collection = from d in db.TrnCollections
                                         where d.Id == newCollection.Id
                                         select d;

                        if (collection.Any())
                        {
                            Decimal totalCollectionLineAmount = 0;
                            var collectionLines = from d in db.TrnCollectionLines
                                                  where d.CollectionId == collection.FirstOrDefault().Id
                                                  select d;

                            if (collectionLines.Any())
                            {
                                totalCollectionLineAmount = collectionLines.Sum(d => d.Amount);
                            }

                            var lockCollection = collection.FirstOrDefault();
                            lockCollection.Amount = totalCollectionLineAmount - collection.FirstOrDefault().ChangeAmount;
                            lockCollection.IsLocked = true;
                            db.SubmitChanges();

                            paidAmount = totalCollectionLineAmount;
                        }

                        var lockSales = currentSales.FirstOrDefault();
                        lockSales.CollectionNumber = collection.FirstOrDefault().CollectionNumber;
                        lockSales.PaidAmount = paidAmount;
                        lockSales.BalanceAmount = salesAmount - paidAmount;
                        lockSales.IsLocked = true;
                        lockSales.IsTendered = true;
                        lockSales.UpdateUserId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId);
                        lockSales.UpdateDateTime = DateTime.Now;
                        db.SubmitChanges();

                        Modules.TrnInventoryModule trnInventoryModule = new Modules.TrnInventoryModule();
                        trnInventoryModule.UpdateSalesInventory(salesId);
                    }

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Empty Sales!", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // =============================
        // Get Sales Detail for Exchange
        // =============================
        public Entities.TrnSalesEntity GetExchangeSalesDetail(String orderReturnNumber)
        {
            var sale = from d in db.TrnSales
                       where d.SalesNumber == orderReturnNumber
                       && d.IsLocked == true
                       && d.IsReturned == true
                       && d.ReturnApplication == "Return"
                       select new Entities.TrnSalesEntity
                       {
                           Id = d.Id,
                           SalesNumber = d.SalesNumber,
                           Customer = d.MstCustomer.Customer,
                           Amount = d.Amount
                       };

            return sale.FirstOrDefault();
        }
    }
}

