using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Controllers
{
    class MstDiscountController
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

        // =============
        // List Discount 
        // =============
        public List<Entities.MstDiscountEntity> ListDiscount(String filter)
        {
            var discounts = from d in db.MstDiscounts
                            where d.Discount.Contains(filter)
                            select new Entities.MstDiscountEntity
                            {
                                Id = d.Id,
                                Discount = d.Discount,
                                DiscountRate = d.DiscountRate,
                                IsVatExempt = d.IsVatExempt,
                                IsDateScheduled = d.IsDateScheduled,
                                DateStart = d.DateStart != null ? Convert.ToDateTime(d.DateStart).ToShortDateString() : "",
                                DateEnd = d.DateEnd != null ? Convert.ToDateTime(d.DateEnd).ToShortDateString() : "",
                                IsTimeScheduled = d.IsTimeScheduled,
                                TimeStart = d.TimeStart != null ? Convert.ToDateTime(d.TimeStart).ToShortTimeString() : "",
                                TimeEnd = d.TimeEnd != null ? Convert.ToDateTime(d.TimeEnd).ToShortTimeString() : "",
                                IsDayScheduled = d.IsDayScheduled,
                                DayMon = d.DayMon,
                                DayTue = d.DayTue,
                                DayWed = d.DayWed,
                                DayThu = d.DayThu,
                                DayFri = d.DayFri,
                                DaySat = d.DaySat,
                                DaySun = d.DaySun,
                                IsLocked = d.IsLocked,
                                EntryUserId = d.EntryUserId,
                                EntryUserName = d.MstUser.UserName,
                                EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                UpdateUserId = d.UpdateUserId,
                                UpdatedUserName = d.MstUser1.UserName,
                                UpdateDateTime = d.UpdateDateTime.ToShortDateString()
                            };

            return discounts.OrderByDescending(d => d.Id).ToList();
        }

        // ===============
        // Detail Discount
        // ===============
        public Entities.MstDiscountEntity DetailDiscount(Int32 id)
        {
            var discount = from d in db.MstDiscounts
                           where d.Id == id
                           select new Entities.MstDiscountEntity
                           {
                               Id = d.Id,
                               Discount = d.Discount,
                               DiscountRate = d.DiscountRate,
                               IsVatExempt = d.IsVatExempt,
                               IsDateScheduled = d.IsDateScheduled,
                               DateStart = d.DateStart != null ? Convert.ToDateTime(d.DateStart).ToShortDateString() : "",
                               DateEnd = d.DateEnd != null ? Convert.ToDateTime(d.DateEnd).ToShortDateString() : "",
                               IsTimeScheduled = d.IsTimeScheduled,
                               TimeStart = d.TimeStart != null ? Convert.ToDateTime(d.TimeStart).ToShortTimeString() : "",
                               TimeEnd = d.TimeEnd != null ? Convert.ToDateTime(d.TimeEnd).ToShortTimeString() : "",
                               IsDayScheduled = d.IsDayScheduled,
                               DayMon = d.DayMon,
                               DayTue = d.DayTue,
                               DayWed = d.DayWed,
                               DayThu = d.DayThu,
                               DayFri = d.DayFri,
                               DaySat = d.DaySat,
                               DaySun = d.DaySun,
                               IsLocked = d.IsLocked,
                               EntryUserId = d.EntryUserId,
                               EntryUserName = d.MstUser.UserName,
                               EntryUserUserName = d.MstUser.FullName,
                               EntryDateTime = d.EntryDateTime.ToShortDateString(),
                               EntryTime = d.EntryDateTime.ToShortTimeString(),
                               UpdateUserId = d.UpdateUserId,
                               UpdatedUserName = d.MstUser1.UserName,
                               UpdatedUserUserName = d.MstUser1.FullName,
                               UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                               UpdateTime = d.UpdateDateTime.ToShortTimeString()
                           };

            return discount.FirstOrDefault();
        }

        // ============ 
        // Add Discount
        // ============
        public String[] AddDiscount()
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                Data.MstDiscount newDiscount = new Data.MstDiscount()
                {
                    Discount = "",
                    DiscountRate = 0,
                    IsVatExempt = false,
                    IsDateScheduled = false,
                    DateStart = null,
                    DateEnd = null,
                    IsTimeScheduled = false,
                    TimeStart = null,
                    TimeEnd = null,
                    IsDayScheduled = false,
                    DayMon = false,
                    DayTue = false,
                    DayWed = false,
                    DayThu = false,
                    DayFri = false,
                    DaySat = false,
                    DaySun = false,
                    IsLocked = false,
                    EntryUserId = currentUserLogin.FirstOrDefault().Id,
                    EntryDateTime = DateTime.Now,
                    UpdateUserId = currentUserLogin.FirstOrDefault().Id,
                    UpdateDateTime = DateTime.Now
                };

                db.MstDiscounts.InsertOnSubmit(newDiscount);
                db.SubmitChanges();

                String newObject = Modules.SysAuditTrailModule.GetObjectString(newDiscount);

                Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                {
                    UserId = currentUserLogin.FirstOrDefault().Id,
                    AuditDate = DateTime.Now,
                    TableInformation = "MstDiscount",
                    RecordInformation = "",
                    FormInformation = newObject,
                    ActionInformation = "AddDiscount"
                };
                Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                return new String[] { "", newDiscount.Id.ToString() };
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // =============
        // Lock Discount
        // =============
        public String[] LockDiscount(Int32 id, Entities.MstDiscountEntity objDiscount)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var discount = from d in db.MstDiscounts
                               where d.Id == id
                               select d;

                if (discount.Any())
                {
                    if (discount.FirstOrDefault().IsLocked)
                    {
                        return new String[] { "Already locked.", "0" };
                    }

                    DateTime? dateStart = null;
                    if (String.IsNullOrEmpty(objDiscount.DateStart) == false)
                    {
                        dateStart = Convert.ToDateTime(objDiscount.DateStart);
                    }

                    DateTime? dateEnd = null;
                    if (String.IsNullOrEmpty(objDiscount.DateEnd) == false)
                    {
                        dateEnd = Convert.ToDateTime(objDiscount.DateEnd);
                    }

                    DateTime? timeStart = null;
                    if (String.IsNullOrEmpty(objDiscount.TimeStart) == false)
                    {
                        timeStart = Convert.ToDateTime(objDiscount.TimeStart);
                    }

                    DateTime? timeEnd = null;
                    if (String.IsNullOrEmpty(objDiscount.TimeEnd) == false)
                    {
                        timeEnd = Convert.ToDateTime(objDiscount.TimeEnd);
                    }

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(discount.FirstOrDefault());

                    var lockDiscount = discount.FirstOrDefault();
                    lockDiscount.Discount = objDiscount.Discount;
                    lockDiscount.DiscountRate = objDiscount.DiscountRate;
                    lockDiscount.IsVatExempt = objDiscount.IsVatExempt;
                    lockDiscount.IsDateScheduled = objDiscount.IsDateScheduled;
                    lockDiscount.DateStart = dateStart;
                    lockDiscount.DateEnd = dateEnd;
                    lockDiscount.IsTimeScheduled = objDiscount.IsTimeScheduled;
                    lockDiscount.TimeStart = timeStart;
                    lockDiscount.TimeEnd = timeEnd;
                    lockDiscount.IsDayScheduled = objDiscount.IsDayScheduled;
                    lockDiscount.DayMon = objDiscount.DayMon;
                    lockDiscount.DayTue = objDiscount.DayTue;
                    lockDiscount.DayWed = objDiscount.DayWed;
                    lockDiscount.DayThu = objDiscount.DayThu;
                    lockDiscount.DayFri = objDiscount.DayFri;
                    lockDiscount.DaySat = objDiscount.DaySat;
                    lockDiscount.DaySun = objDiscount.DaySun;
                    lockDiscount.IsLocked = true;
                    lockDiscount.UpdateUserId = currentUserLogin.FirstOrDefault().Id;
                    lockDiscount.UpdateDateTime = DateTime.Now;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(discount.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "MstDiscount",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "LockDiscount"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Discount not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ===============
        // Unlock Discount
        // ===============
        public String[] UnlockDiscount(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var discount = from d in db.MstDiscounts
                               where d.Id == id
                               select d;

                if (discount.Any())
                {
                    if (discount.FirstOrDefault().IsLocked == false)
                    {
                        return new String[] { "Already unlocked.", "0" };
                    }

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(discount.FirstOrDefault());

                    var unlockDiscount = discount.FirstOrDefault();
                    unlockDiscount.IsLocked = false;
                    unlockDiscount.UpdateUserId = currentUserLogin.FirstOrDefault().Id;
                    unlockDiscount.UpdateDateTime = DateTime.Now;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(discount.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "MstDiscount",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "UnlockDiscount"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Discount not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ===============
        // Delete Discount
        // ===============
        public String[] DeleteDiscount(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var discount = from d in db.MstDiscounts
                               where d.Id == id
                               select d;

                if (discount.Any())
                {
                    if (discount.FirstOrDefault().IsLocked)
                    {
                        return new String[] { "Discount is locked", "0" };
                    }

                    var deleteDiscount = discount.FirstOrDefault();
                    db.MstDiscounts.DeleteOnSubmit(deleteDiscount);

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(discount.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "MstDiscount",
                        RecordInformation = oldObject,
                        FormInformation = "",
                        ActionInformation = "DeleteDiscount"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    db.SubmitChanges();

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Discount not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        } 
        // =============
        // Save Discount
        // =============
        public String[] SaveDiscount(Int32 id, Entities.MstDiscountEntity objDiscount)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var discount = from d in db.MstDiscounts
                               where d.Id == id
                               select d;

                if (discount.Any())
                {
                    if (discount.FirstOrDefault().IsLocked)
                    {
                        return new String[] { "Already locked.", "0" };
                    }

                    DateTime? dateStart = null;
                    if (String.IsNullOrEmpty(objDiscount.DateStart) == false)
                    {
                        dateStart = Convert.ToDateTime(objDiscount.DateStart);
                    }

                    DateTime? dateEnd = null;
                    if (String.IsNullOrEmpty(objDiscount.DateEnd) == false)
                    {
                        dateEnd = Convert.ToDateTime(objDiscount.DateEnd);
                    }

                    DateTime? timeStart = null;
                    if (String.IsNullOrEmpty(objDiscount.TimeStart) == false)
                    {
                        timeStart = Convert.ToDateTime(objDiscount.TimeStart);
                    }

                    DateTime? timeEnd = null;
                    if (String.IsNullOrEmpty(objDiscount.TimeEnd) == false)
                    {
                        timeEnd = Convert.ToDateTime(objDiscount.TimeEnd);
                    }

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(discount.FirstOrDefault());

                    var saveDiscount = discount.FirstOrDefault();
                    saveDiscount.Discount = objDiscount.Discount;
                    saveDiscount.DiscountRate = objDiscount.DiscountRate;
                    saveDiscount.IsVatExempt = objDiscount.IsVatExempt;
                    saveDiscount.IsDateScheduled = objDiscount.IsDateScheduled;
                    saveDiscount.DateStart = dateStart;
                    saveDiscount.DateEnd = dateEnd;
                    saveDiscount.IsTimeScheduled = objDiscount.IsTimeScheduled;
                    saveDiscount.TimeStart = timeStart;
                    saveDiscount.TimeEnd = timeEnd;
                    saveDiscount.IsDayScheduled = objDiscount.IsDayScheduled;
                    saveDiscount.DayMon = objDiscount.DayMon;
                    saveDiscount.DayTue = objDiscount.DayTue;
                    saveDiscount.DayWed = objDiscount.DayWed;
                    saveDiscount.DayThu = objDiscount.DayThu;
                    saveDiscount.DayFri = objDiscount.DayFri;
                    saveDiscount.DaySat = objDiscount.DaySat;
                    saveDiscount.DaySun = objDiscount.DaySun;
                    saveDiscount.UpdateUserId = currentUserLogin.FirstOrDefault().Id;
                    saveDiscount.UpdateDateTime = DateTime.Now;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(discount.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "MstDiscount",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "SaveDiscount"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Discount not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }
    }
}
