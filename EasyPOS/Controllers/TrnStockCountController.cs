using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Controllers
{
    class TrnStockCountController
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

        // ================
        // List Stock-Count 
        // ================
        public List<Entities.TrnStockCountEntity> ListStockCount(DateTime dateFilter, String filter)
        {
            var stockCounts = from d in db.TrnStockCounts
                              where d.StockCountDate == dateFilter
                              && d.StockCountNumber.Contains(filter)
                              select new Entities.TrnStockCountEntity
                              {
                                  Id = d.Id,
                                  PeriodId = d.PeriodId,
                                  StockCountDate = d.StockCountDate.ToShortDateString(),
                                  StockCountNumber = d.StockCountNumber,
                                  Remarks = d.Remarks,
                                  PreparedBy = d.PreparedBy,
                                  CheckedBy = d.CheckedBy,
                                  ApprovedBy = d.ApprovedBy,
                                  IsLocked = d.IsLocked,
                                  EntryUserId = d.EntryUserId,
                                  EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                  UpdateUserId = d.UpdateUserId,
                                  UpdateDateTime = d.UpdateDateTime.ToShortDateString()
                              };

            return stockCounts.OrderByDescending(d => d.Id).ToList();
        }

        // ==================
        // Detail Stock-Count
        // ==================
        public Entities.TrnStockCountEntity DetailStockCount(Int32 id)
        {
            var stockCount = from d in db.TrnStockCounts
                             where d.Id == id
                             select new Entities.TrnStockCountEntity
                             {
                                 Id = d.Id,
                                 PeriodId = d.PeriodId,
                                 StockCountDate = d.StockCountDate.ToShortDateString(),
                                 StockCountNumber = d.StockCountNumber,
                                 Remarks = d.Remarks,
                                 PreparedBy = d.PreparedBy,
                                 CheckedBy = d.CheckedBy,
                                 ApprovedBy = d.ApprovedBy,
                                 IsLocked = d.IsLocked,
                                 EntryUserId = d.EntryUserId,
                                 EntryUserUserName = d.MstUser3.FullName,
                                 EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                 EntryTime = d.EntryDateTime.ToShortTimeString(),
                                 UpdateUserId = d.UpdateUserId,
                                 UpdateUserUserName = d.MstUser4.FullName,
                                 UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                                 UpdateTime = d.UpdateDateTime.ToShortTimeString()
                             };

            return stockCount.FirstOrDefault();
        }

        // ====================
        // Dropdown List - User
        // ====================
        public List<Entities.MstUserEntity> DropdownListStockCountUser()
        {
            var users = from d in db.MstUsers
                        select new Entities.MstUserEntity
                        {
                            Id = d.Id,
                            FullName = d.FullName
                        };

            return users.ToList();
        }

        // ===============
        // Add Stock-Count
        // ===============
        public String[] AddStockCount()
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

                DateTime currentDate = DateTime.Today;
                if (Modules.SysCurrentModule.GetCurrentSettings().IsLoginDate == true)
                {
                    currentDate = Convert.ToDateTime(Modules.SysCurrentModule.GetCurrentSettings().CurrentDate);
                }

                String stockCountNumber = "0000000001";
                var lastStockCount = from d in db.TrnStockCounts.OrderByDescending(d => d.Id) select d;
                if (lastStockCount.Any())
                {
                    Int32 newStockCountNumber = Convert.ToInt32(lastStockCount.FirstOrDefault().StockCountNumber) + 1;
                    stockCountNumber = FillLeadingZeroes(newStockCountNumber, 10);
                }

                Data.TrnStockCount newStockCount = new Data.TrnStockCount()
                {
                    PeriodId = period.FirstOrDefault().Id,
                    StockCountDate = currentDate,
                    StockCountNumber = stockCountNumber,
                    Remarks = "",
                    PreparedBy = currentUserLogin.FirstOrDefault().Id,
                    CheckedBy = currentUserLogin.FirstOrDefault().Id,
                    ApprovedBy = currentUserLogin.FirstOrDefault().Id,
                    IsLocked = false,
                    EntryUserId = currentUserLogin.FirstOrDefault().Id,
                    EntryDateTime = DateTime.Now,
                    UpdateUserId = currentUserLogin.FirstOrDefault().Id,
                    UpdateDateTime = DateTime.Now
                };

                db.TrnStockCounts.InsertOnSubmit(newStockCount);
                db.SubmitChanges();

                String newObject = Modules.SysAuditTrailModule.GetObjectString(newStockCount);

                Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                {
                    UserId = currentUserLogin.FirstOrDefault().Id,
                    AuditDate = DateTime.Now,
                    TableInformation = "TrnStockCount",
                    RecordInformation = "",
                    FormInformation = newObject,
                    ActionInformation = "AddStockCount"
                };
                Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                return new String[] { "", newStockCount.Id.ToString() };
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ================
        // Lock Stock-Count
        // ================
        public String[] LockStockCount(Int32 id, Entities.TrnStockCountEntity objStockCount)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var checkedByUser = from d in db.MstUsers
                                    where d.Id == objStockCount.CheckedBy
                                    && d.IsLocked == true
                                    select d;

                if (checkedByUser.Any() == false)
                {
                    return new String[] { "Checked by user not found.", "0" };
                }

                var approvedByUser = from d in db.MstUsers
                                     where d.Id == objStockCount.ApprovedBy
                                     && d.IsLocked == true
                                     select d;

                if (approvedByUser.Any() == false)
                {
                    return new String[] { "Approved by user not found.", "0" };
                }

                var stockCount = from d in db.TrnStockCounts
                                 where d.Id == id
                                 select d;

                if (stockCount.Any())
                {
                    if (stockCount.FirstOrDefault().IsLocked)
                    {
                        return new String[] { "Already locked.", "0" };
                    }

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(stockCount.FirstOrDefault());

                    var lockStockCount = stockCount.FirstOrDefault();
                    lockStockCount.StockCountDate = Convert.ToDateTime(objStockCount.StockCountDate);
                    lockStockCount.Remarks = objStockCount.Remarks;
                    lockStockCount.CheckedBy = objStockCount.CheckedBy;
                    lockStockCount.ApprovedBy = objStockCount.ApprovedBy;
                    lockStockCount.IsLocked = true;
                    lockStockCount.UpdateUserId = currentUserLogin.FirstOrDefault().Id;
                    lockStockCount.UpdateDateTime = DateTime.Now;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(stockCount.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "TrnStockCount",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "LockStockCount"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Stock-Count not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ==================
        // Unlock Stock-Count
        // ==================
        public String[] UnlockStockCount(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var stockCount = from d in db.TrnStockCounts
                                 where d.Id == id
                                 select d;

                if (stockCount.Any())
                {
                    if (stockCount.FirstOrDefault().IsLocked == false)
                    {
                        return new String[] { "Already unlocked.", "0" };
                    }

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(stockCount.FirstOrDefault());

                    var unlockStockCount = stockCount.FirstOrDefault();
                    unlockStockCount.IsLocked = false;
                    unlockStockCount.UpdateUserId = currentUserLogin.FirstOrDefault().Id;
                    unlockStockCount.UpdateDateTime = DateTime.Now;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(stockCount.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "TrnStockCount",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "UnlockStockCount"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Stock-Count not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ==================
        // Delete Stock-Count
        // ==================
        public String[] DeleteStockCount(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var stockCount = from d in db.TrnStockCounts
                                 where d.Id == id
                                 select d;

                if (stockCount.Any())
                {
                    if (stockCount.FirstOrDefault().IsLocked)
                    {
                        return new String[] { "Stock-Count is locked", "0" };
                    }

                    var deleteStockCount = stockCount.FirstOrDefault();
                    db.TrnStockCounts.DeleteOnSubmit(deleteStockCount);

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(stockCount.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "TrnStockCount",
                        RecordInformation = oldObject,
                        FormInformation = "",
                        ActionInformation = "DeleteStockCount"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    db.SubmitChanges();

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Stock-Count not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ================
        // Post Stock-Count
        // ================
        public String[] PostStockCount(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var account = from d in db.MstAccounts select d;
                if (account.Any() == false)
                {
                    return new String[] { "Account not found.", "0" };
                }

                var stockCount = from d in db.TrnStockCounts
                                 where d.Id == id
                                 select d;

                if (stockCount.Any())
                {
                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(stockCount.FirstOrDefault());

                    String stockOutNumber = "0000000001";
                    var lastStockOut = from d in db.TrnStockOuts.OrderByDescending(d => d.Id) select d;
                    if (lastStockOut.Any())
                    {
                        Int32 newStockOutNumber = Convert.ToInt32(lastStockOut.FirstOrDefault().StockOutNumber) + 1;
                        stockOutNumber = FillLeadingZeroes(newStockOutNumber, 10);
                    }

                    Data.TrnStockOut newStockOut = new Data.TrnStockOut()
                    {
                        PeriodId = stockCount.FirstOrDefault().PeriodId,
                        StockOutDate = stockCount.FirstOrDefault().StockCountDate,
                        StockOutNumber = stockOutNumber,
                        AccountId = account.FirstOrDefault().Id,
                        Remarks = stockCount.FirstOrDefault().Remarks,
                        PreparedBy = stockCount.FirstOrDefault().PreparedBy,
                        CheckedBy = stockCount.FirstOrDefault().CheckedBy,
                        ApprovedBy = stockCount.FirstOrDefault().ApprovedBy,
                        IsLocked = true,
                        EntryUserId = currentUserLogin.FirstOrDefault().Id,
                        EntryDateTime = DateTime.Now,
                        UpdateUserId = currentUserLogin.FirstOrDefault().Id,
                        UpdateDateTime = DateTime.Now
                    };

                    db.TrnStockOuts.InsertOnSubmit(newStockOut);
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(newStockOut);

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "TrnStockCount",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "PostStockCount"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    if (stockCount.FirstOrDefault().TrnStockCountLines.Any() == true)
                    {
                        List<Data.TrnStockOutLine> newStockOutLines = new List<Data.TrnStockOutLine>();
                        foreach (var stockCountLines in stockCount.FirstOrDefault().TrnStockCountLines)
                        {
                            newStockOutLines.Add(new Data.TrnStockOutLine
                            {
                                StockOutId = newStockOut.Id,
                                ItemId = stockCountLines.ItemId,
                                UnitId = stockCountLines.UnitId,
                                Quantity = stockCountLines.MstItem.OnhandQuantity - stockCountLines.Quantity,
                                Cost = stockCountLines.Cost,
                                Amount = stockCountLines.Amount,
                                AssetAccountId = stockCountLines.MstItem.AssetAccountId
                            });
                        }

                        db.TrnStockOutLines.InsertAllOnSubmit(newStockOutLines);
                        db.SubmitChanges();
                    }

                    Modules.TrnInventoryModule trnInventoryModule = new Modules.TrnInventoryModule();
                    trnInventoryModule.UpdateStockOutInventory(newStockOut.Id);

                    return new String[] { "", newStockOut.Id.ToString() };
                }
                else
                {
                    return new String[] { "Stock-Count not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }
    }
}
