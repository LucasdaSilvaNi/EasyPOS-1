using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Controllers
{
    class MstItemGroupController
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

        // ===============
        // List Item Group 
        // ===============
        public List<Entities.MstItemGroupEntity> ListItemGroup(String filter)
        {
            var itemGroups = from d in db.MstItemGroups
                             where d.ItemGroup.Contains(filter)
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

            return itemGroups.OrderByDescending(d => d.Id).ToList();
        }

        // =================
        // Detail Item Group
        // =================
        public Entities.MstItemGroupEntity DetailItemGroup(Int32 id)
        {
            var itemGroup = from d in db.MstItemGroups
                            where d.Id == id
                            select new Entities.MstItemGroupEntity
                            {
                                Id = d.Id,
                                ItemGroup = d.ItemGroup,
                                ImagePath = d.ImagePath,
                                KitchenReport = d.KitchenReport,
                                EntryUserId = d.EntryUserId,
                                EntryUserName = d.MstUser.UserName,
                                EntryUserUserName = d.MstUser.FullName,
                                EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                EntryTime = d.EntryDateTime.ToShortTimeString(),
                                UpdateUserId = d.UpdateUserId,
                                UpdatedUserName = d.MstUser1.UserName,
                                UpdatedUserUserName = d.MstUser1.FullName,
                                UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                                UpdateTime = d.UpdateDateTime.ToShortTimeString(),
                                IsLocked = d.IsLocked
                            };

            return itemGroup.FirstOrDefault();
        }

        // ==============
        // Add Item Group
        // ==============
        public String[] AddItemGroup()
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                Data.MstItemGroup newItemGroup = new Data.MstItemGroup()
                {
                    ItemGroup = "",
                    ImagePath = "",
                    KitchenReport = "Kitchen 1",
                    EntryUserId = currentUserLogin.FirstOrDefault().Id,
                    EntryDateTime = DateTime.Now,
                    UpdateUserId = currentUserLogin.FirstOrDefault().Id,
                    UpdateDateTime = DateTime.Now,
                    IsLocked = false
                };

                db.MstItemGroups.InsertOnSubmit(newItemGroup);
                db.SubmitChanges();

                String newObject = Modules.SysAuditTrailModule.GetObjectString(newItemGroup);

                Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                {
                    UserId = currentUserLogin.FirstOrDefault().Id,
                    AuditDate = DateTime.Now,
                    TableInformation = "MstItemGroup",
                    RecordInformation = "",
                    FormInformation = newObject,
                    ActionInformation = "AddItemGroup"
                };
                Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                return new String[] { "", newItemGroup.Id.ToString() };
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ===============
        // Lock Item Group
        // ===============
        public String[] LockItemGroup(Int32 id, Entities.MstItemGroupEntity objItemGroup, List<Entities.MstItemGroupItemEntity> objItemGroupItems)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var itemGroup = from d in db.MstItemGroups
                                where d.Id == id
                                select d;

                if (itemGroup.Any())
                {
                    if (itemGroup.FirstOrDefault().IsLocked)
                    {
                        return new String[] { "Already locked.", "0" };
                    }

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(itemGroup.FirstOrDefault());

                    var lockItemGroup = itemGroup.FirstOrDefault();
                    lockItemGroup.ItemGroup = objItemGroup.ItemGroup;
                    lockItemGroup.ImagePath = objItemGroup.ImagePath;
                    lockItemGroup.KitchenReport = objItemGroup.KitchenReport;
                    lockItemGroup.UpdateUserId = currentUserLogin.FirstOrDefault().Id;
                    lockItemGroup.UpdateDateTime = DateTime.Now;
                    lockItemGroup.IsLocked = true;
                    db.SubmitChanges();

                    var itemGroupItems = from d in db.MstItemGroupItems
                                         where d.ItemGroupId == id
                                         select d;

                    if (itemGroupItems.Any())
                    {
                        foreach (var itemGroupItem in itemGroupItems)
                        {
                            var item = from d in db.MstItems
                                       where d.Id == itemGroupItem.ItemId
                                       select d;

                            if (item.Any())
                            {
                                var updateItemKitchenReport = item.FirstOrDefault();
                                updateItemKitchenReport.DefaultKitchenReport = objItemGroup.KitchenReport;

                                db.SubmitChanges();
                            }
                        }
                    }

                    if (objItemGroupItems.Any())
                    {
                        foreach (var objItemGroupItem in objItemGroupItems)
                        {
                            var itemGroupItem = from d in db.MstItemGroupItems
                                                where d.Id == objItemGroupItem.Id
                                                select d;

                            if (itemGroupItem.Any())
                            {
                                var updateShow = itemGroupItem.FirstOrDefault();
                                updateShow.Show = objItemGroupItem.Show;
                            }
                        }

                        db.SubmitChanges();
                    }

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(itemGroup.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "MstItemGroup",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "LockItemGroup"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "ItemGroup not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // =================
        // Unlock Item Group
        // =================
        public String[] UnlockItemGroup(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var itemGroup = from d in db.MstItemGroups
                                where d.Id == id
                                select d;

                if (itemGroup.Any())
                {
                    if (itemGroup.FirstOrDefault().IsLocked == false)
                    {
                        return new String[] { "Already unlocked.", "0" };
                    }

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(itemGroup.FirstOrDefault());

                    var unlockItemGroup = itemGroup.FirstOrDefault();
                    unlockItemGroup.IsLocked = false;
                    unlockItemGroup.UpdateUserId = currentUserLogin.FirstOrDefault().Id;
                    unlockItemGroup.UpdateDateTime = DateTime.Now;
                    db.SubmitChanges();

                    var itemGroupItems = from d in db.MstItemGroupItems
                                         where d.ItemGroupId == id
                                         select d;

                    if (itemGroupItems.Any())
                    {
                        foreach (var itemGroupItem in itemGroupItems)
                        {
                            var item = from d in db.MstItems
                                       where d.Id == itemGroupItem.ItemId
                                       select d;

                            if (item.Any())
                            {
                                var updateItemKitchenReport = item.FirstOrDefault();
                                updateItemKitchenReport.DefaultKitchenReport = "";

                                db.SubmitChanges();
                            }
                        }
                    }

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(itemGroup.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "MstItemGroup",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "UnlockItemGroup"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "ItemGroup not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // =================
        // Delete Item Group
        // =================
        public String[] DeleteItemGroup(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var itemGroup = from d in db.MstItemGroups
                                where d.Id == id
                                select d;

                if (itemGroup.Any())
                {
                    if (itemGroup.FirstOrDefault().IsLocked)
                    {
                        return new String[] { "ItemGroup is locked", "0" };
                    }

                    var deleteItemGroup = itemGroup.FirstOrDefault();
                    db.MstItemGroups.DeleteOnSubmit(deleteItemGroup);

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(itemGroup.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "MstItemGroup",
                        RecordInformation = oldObject,
                        FormInformation = "",
                        ActionInformation = "DeleteItemGroup"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    db.SubmitChanges();

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "ItemGroup not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }
    }
}
