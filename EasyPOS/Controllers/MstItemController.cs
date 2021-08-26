using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Controllers
{
    class MstItemController
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
         
        // ==========
        // List Items
        // ==========
        public List<Entities.MstItemEntity> ListItem(String filter, String selectedIsInventory, String selectedIsLocked)
        {
            if (selectedIsInventory == "All" && selectedIsLocked != "All")
            {
                Boolean isLocked = true;
                if (selectedIsLocked == "Unlocked")
                {
                    isLocked = false;
                }

                var items = from d in db.MstItems
                            where (d.ItemCode.Contains(filter)
                            || d.ItemDescription.Contains(filter)
                            || d.BarCode.Contains(filter)
                            || d.Category.Contains(filter)
                            || d.Alias.Contains(filter)
                            || d.MstUnit.Unit.Contains(filter)
                            || d.MstSupplier.Supplier.Contains(filter))
                            && d.IsLocked == isLocked
                            select new Entities.MstItemEntity
                            {
                                Id = d.Id,
                                ItemCode = d.ItemCode,
                                BarCode = d.BarCode,
                                ItemDescription = d.ItemDescription,
                                Alias = d.Alias,
                                GenericName = d.GenericName,
                                Category = d.Category,
                                SalesAccountId = d.SalesAccountId,
                                AssetAccountId = d.AssetAccountId,
                                CostAccountId = d.CostAccountId,
                                InTaxId = d.InTaxId,
                                OutTaxId = d.OutTaxId,
                                UnitId = d.UnitId,
                                Unit = d.MstUnit.Unit,
                                DefaultSupplierId = d.DefaultSupplierId,
                                Supplier = d.MstSupplier.Supplier,
                                Cost = d.Cost,
                                MarkUp = d.MarkUp,
                                Price = d.Price,
                                ImagePath = d.ImagePath,
                                ReorderQuantity = d.ReorderQuantity,
                                OnhandQuantity = d.OnhandQuantity,
                                IsInventory = d.IsInventory,
                                ExpiryDate = d.ExpiryDate != null ? Convert.ToDateTime(d.ExpiryDate).ToShortDateString() : "",
                                LotNumber = d.LotNumber,
                                Remarks = d.Remarks,
                                EntryUserId = d.EntryUserId,
                                EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                UpdateUserId = d.UpdateUserId,
                                UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                                IsLocked = d.IsLocked,
                                DefaultKitchenReport = d.DefaultKitchenReport,
                                IsPackage = d.IsPackage
                            };

                //return items.OrderByDescending(d => d.Id).ToList();
                return items.OrderBy(d => d.ItemDescription).ToList();

            }
            else if (selectedIsLocked == "All" && selectedIsInventory != "All")
            {
                Boolean isInventory = true;
                if (selectedIsInventory == "Non-Inventory")
                {
                    isInventory = false;
                }

                var items = from d in db.MstItems
                            where (d.ItemCode.Contains(filter)
                            || d.ItemDescription.Contains(filter)
                            || d.BarCode.Contains(filter)
                            || d.Category.Contains(filter)
                            || d.Alias.Contains(filter)
                            || d.MstUnit.Unit.Contains(filter)
                            || d.MstSupplier.Supplier.Contains(filter))
                            && d.IsInventory == isInventory
                            select new Entities.MstItemEntity
                            {
                                Id = d.Id,
                                ItemCode = d.ItemCode,
                                BarCode = d.BarCode,
                                ItemDescription = d.ItemDescription,
                                Alias = d.Alias,
                                GenericName = d.GenericName,
                                Category = d.Category,
                                SalesAccountId = d.SalesAccountId,
                                AssetAccountId = d.AssetAccountId,
                                CostAccountId = d.CostAccountId,
                                InTaxId = d.InTaxId,
                                OutTaxId = d.OutTaxId,
                                UnitId = d.UnitId,
                                Unit = d.MstUnit.Unit,
                                DefaultSupplierId = d.DefaultSupplierId,
                                Supplier = d.MstSupplier.Supplier,
                                Cost = d.Cost,
                                MarkUp = d.MarkUp,
                                Price = d.Price,
                                ImagePath = d.ImagePath,
                                ReorderQuantity = d.ReorderQuantity,
                                OnhandQuantity = d.OnhandQuantity,
                                IsInventory = d.IsInventory,
                                ExpiryDate = d.ExpiryDate != null ? Convert.ToDateTime(d.ExpiryDate).ToShortDateString() : "",
                                LotNumber = d.LotNumber,
                                Remarks = d.Remarks,
                                EntryUserId = d.EntryUserId,
                                EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                UpdateUserId = d.UpdateUserId,
                                UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                                IsLocked = d.IsLocked,
                                DefaultKitchenReport = d.DefaultKitchenReport,
                                IsPackage = d.IsPackage
                            };

                //return items.OrderByDescending(d => d.Id).ToList();
                return items.OrderBy(d => d.ItemDescription).ToList();
            }
            else if (selectedIsLocked == "All" && selectedIsInventory == "All")
            {
                var items = from d in db.MstItems
                            where d.ItemCode.Contains(filter)
                            || d.ItemDescription.Contains(filter)
                            || d.BarCode.Contains(filter)
                            || d.Category.Contains(filter)
                            || d.Alias.Contains(filter)
                            || d.MstUnit.Unit.Contains(filter)
                            || d.MstSupplier.Supplier.Contains(filter)
                            select new Entities.MstItemEntity
                            {
                                Id = d.Id,
                                ItemCode = d.ItemCode,
                                BarCode = d.BarCode,
                                ItemDescription = d.ItemDescription,
                                Alias = d.Alias,
                                GenericName = d.GenericName,
                                Category = d.Category,
                                SalesAccountId = d.SalesAccountId,
                                AssetAccountId = d.AssetAccountId,
                                CostAccountId = d.CostAccountId,
                                InTaxId = d.InTaxId,
                                OutTaxId = d.OutTaxId,
                                UnitId = d.UnitId,
                                Unit = d.MstUnit.Unit,
                                DefaultSupplierId = d.DefaultSupplierId,
                                Supplier = d.MstSupplier.Supplier,
                                Cost = d.Cost,
                                MarkUp = d.MarkUp,
                                Price = d.Price,
                                ImagePath = d.ImagePath,
                                ReorderQuantity = d.ReorderQuantity,
                                OnhandQuantity = d.OnhandQuantity,
                                IsInventory = d.IsInventory,
                                ExpiryDate = d.ExpiryDate != null ? Convert.ToDateTime(d.ExpiryDate).ToShortDateString() : "",
                                LotNumber = d.LotNumber,
                                Remarks = d.Remarks,
                                EntryUserId = d.EntryUserId,
                                EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                UpdateUserId = d.UpdateUserId,
                                UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                                IsLocked = d.IsLocked,
                                DefaultKitchenReport = d.DefaultKitchenReport,
                                IsPackage = d.IsPackage
                            };

                //return items.OrderByDescending(d => d.Id).ToList();
                return items.OrderBy(d => d.ItemDescription).ToList();
            }
            else if (selectedIsLocked != "All" && selectedIsInventory != "All")
            {
                Boolean isInventory = true;
                if (selectedIsInventory == "Non-Inventory")
                {
                    isInventory = false;
                }
                Boolean isLocked = true;
                if (selectedIsLocked == "Unlocked")
                {
                    isLocked = false;
                }

                var items = from d in db.MstItems
                            where (d.ItemCode.Contains(filter)
                            || d.ItemDescription.Contains(filter)
                            || d.BarCode.Contains(filter)
                            || d.Category.Contains(filter)
                            || d.Alias.Contains(filter)
                            || d.MstUnit.Unit.Contains(filter)
                            || d.MstSupplier.Supplier.Contains(filter))
                            && d.IsInventory == isInventory
                            && d.IsLocked == isLocked
                            select new Entities.MstItemEntity
                            {
                                Id = d.Id,
                                ItemCode = d.ItemCode,
                                BarCode = d.BarCode,
                                ItemDescription = d.ItemDescription,
                                Alias = d.Alias,
                                GenericName = d.GenericName,
                                Category = d.Category,
                                SalesAccountId = d.SalesAccountId,
                                AssetAccountId = d.AssetAccountId,
                                CostAccountId = d.CostAccountId,
                                InTaxId = d.InTaxId,
                                OutTaxId = d.OutTaxId,
                                UnitId = d.UnitId,
                                Unit = d.MstUnit.Unit,
                                DefaultSupplierId = d.DefaultSupplierId,
                                Supplier = d.MstSupplier.Supplier,
                                Cost = d.Cost,
                                MarkUp = d.MarkUp,
                                Price = d.Price,
                                ImagePath = d.ImagePath,
                                ReorderQuantity = d.ReorderQuantity,
                                OnhandQuantity = d.OnhandQuantity,
                                IsInventory = d.IsInventory,
                                ExpiryDate = d.ExpiryDate != null ? Convert.ToDateTime(d.ExpiryDate).ToShortDateString() : "",
                                LotNumber = d.LotNumber,
                                Remarks = d.Remarks,
                                EntryUserId = d.EntryUserId,
                                EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                UpdateUserId = d.UpdateUserId,
                                UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                                IsLocked = d.IsLocked,
                                DefaultKitchenReport = d.DefaultKitchenReport,
                                IsPackage = d.IsPackage
                            };

                //return items.OrderByDescending(d => d.Id).ToList();
                return items.OrderBy(d => d.ItemDescription).ToList();
            }
            else
            {
                return new List<Entities.MstItemEntity>();
            }
        }

        // ===========
        // Detail Item
        // ===========
        public Entities.MstItemEntity DetailItem(Int32 id)
        {
            var item = from d in db.MstItems
                       where d.Id == id
                       select new Entities.MstItemEntity
                       {
                           Id = d.Id,
                           ItemCode = d.ItemCode,
                           BarCode = d.BarCode,
                           ItemDescription = d.ItemDescription,
                           Alias = d.Alias,
                           GenericName = d.GenericName,
                           Category = d.Category,
                           SalesAccountId = d.SalesAccountId,
                           AssetAccountId = d.AssetAccountId,
                           CostAccountId = d.CostAccountId,
                           InTaxId = d.InTaxId,
                           OutTaxId = d.OutTaxId,
                           UnitId = d.UnitId,
                           Unit = d.MstUnit.Unit,
                           DefaultSupplierId = d.DefaultSupplierId,
                           Cost = d.Cost,
                           MarkUp = d.MarkUp,
                           Price = d.Price,
                           ImagePath = d.ImagePath,
                           ReorderQuantity = d.ReorderQuantity,
                           OnhandQuantity = d.OnhandQuantity,
                           IsInventory = d.IsInventory,
                           ExpiryDate = d.ExpiryDate != null ? Convert.ToDateTime(d.ExpiryDate).ToShortDateString() : "",
                           LotNumber = d.LotNumber,
                           Remarks = d.Remarks,
                           EntryUserId = d.EntryUserId,
                           EntryUserUserName = d.MstUser.FullName,
                           EntryDateTime = d.EntryDateTime.ToShortDateString(),
                           EntryTime = d.EntryDateTime.ToShortTimeString(),
                           UpdateUserId = d.UpdateUserId,
                           UpdatedUserUserName = d.MstUser1.FullName,
                           UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                           UpdateTime = d.UpdateDateTime.ToShortTimeString(),
                           IsLocked = d.IsLocked,
                           DefaultKitchenReport = d.DefaultKitchenReport,
                           IsPackage = d.IsPackage,
                           cValue = d.cValue,
                           ChildItemId = d.ChildItemId
                       };

            return item.FirstOrDefault();
        }
        // ====================
        // List Utilities Items
        // ====================
        public List<Entities.MstItemEntity> UtilitiesListItem(String filter)
        {
            var items = from d in db.MstItems
                        where d.ItemCode.Contains(filter)
                        || d.ItemDescription.Contains(filter)
                        || d.BarCode.Contains(filter)
                        || d.Category.Contains(filter)
                        || d.Alias.Contains(filter)
                        || d.MstUnit.Unit.Contains(filter)
                        select new Entities.MstItemEntity
                        {
                            Id = d.Id,
                            ItemCode = d.ItemCode,
                            BarCode = d.BarCode,
                            ItemDescription = d.ItemDescription,
                            Alias = d.Alias,
                            GenericName = d.GenericName,
                            Category = d.Category,
                            SalesAccountId = d.SalesAccountId,
                            AssetAccountId = d.AssetAccountId,
                            CostAccountId = d.CostAccountId,
                            InTaxId = d.InTaxId,
                            OutTaxId = d.OutTaxId,
                            UnitId = d.UnitId,
                            Unit = d.MstUnit.Unit,
                            DefaultSupplierId = d.DefaultSupplierId,
                            Supplier = d.MstSupplier.Supplier,
                            Cost = d.Cost,
                            MarkUp = d.MarkUp,
                            Price = d.Price,
                            ImagePath = d.ImagePath,
                            ReorderQuantity = d.ReorderQuantity,
                            OnhandQuantity = d.OnhandQuantity,
                            IsInventory = d.IsInventory,
                            ExpiryDate = d.ExpiryDate != null ? Convert.ToDateTime(d.ExpiryDate).ToShortDateString() : "",
                            LotNumber = d.LotNumber,
                            Remarks = d.Remarks,
                            EntryUserId = d.EntryUserId,
                            EntryDateTime = d.EntryDateTime.ToShortDateString(),
                            UpdateUserId = d.UpdateUserId,
                            UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                            IsLocked = d.IsLocked,
                            DefaultKitchenReport = d.DefaultKitchenReport,
                            IsPackage = d.IsPackage,
                            cValue = d.cValue,
                            ChildItemId = d.ChildItemId
                        };

            return items.OrderByDescending(d => d.Id).ToList();
        }

        // ====================
        // Dropdown List - Unit
        // ====================
        public List<Entities.MstUnitEntity> DropdownListItemUnit()
        {
            var units = from d in db.MstUnits
                        select new Entities.MstUnitEntity
                        {
                            Id = d.Id,
                            Unit = d.Unit
                        };

            return units.ToList();
        }

        // ========================
        // Dropdown List - Supplier
        // ========================
        public List<Entities.MstSupplierEntity> DropdownListItemSupplier()
        {
            var suppliers = from d in db.MstSuppliers
                            select new Entities.MstSupplierEntity
                            {
                                Id = d.Id,
                                Supplier = d.Supplier
                            };

            //return suppliers.ToList();
            return suppliers.OrderBy(d => d.Supplier).ToList();
        }

        // ===================
        // Dropdown List - Tax
        // ===================
        public List<Entities.MstTaxEntity> DropdownListItemTax()
        {
            var taxes = from d in db.MstTaxes
                        select new Entities.MstTaxEntity
                        {
                            Id = d.Id,
                            Tax = d.Tax
                        };

            return taxes.ToList();
        }
        // ===================
        // Dropdown List - Category
        // ===================
        public List<Entities.MstItemEntity> DropdownListItemCategory()
        {
            var categories = from d in db.MstItems
                             group d by new
                           {
                               d.Category
                           }
                           into g
                           select new Entities.MstItemEntity
                           {
                               Category = g.Key.Category
                           };

            return categories.ToList();
        }
        // ========================
        // Dropdown List - Item List
        // ========================
        public List<Entities.MstItemEntity> DropdownListItemList()
        {
            var itemList = from d in db.MstItems
                            select new Entities.MstItemEntity
                            {
                                Id = d.Id,
                                ItemDescription = d.ItemDescription
                            };

    
            return itemList.OrderBy(d => d.ItemDescription).ToList();
        }
        // ==============================
        // Dropdown List - Get Child Item
        // ==============================
        public List<Entities.MstItemEntity> DropdownListChildItemList(Int32 currentItemId)
        {
            var itemList = from d in db.MstItems
                           where d.Id != currentItemId
                           select new Entities.MstItemEntity
                           {
                               Id = d.Id,
                               ItemDescription = d.ItemDescription
                           };


            return itemList.OrderBy(d => d.ItemDescription).ToList();
        }

        // ========
        // Add Item
        // ========
        public String[] AddItem()
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                String itemCode = "0000000001";
                var lastItemCode = from d in db.MstItems.OrderByDescending(d => d.Id) select d;
                if (lastItemCode.Any())
                {
                    Int32 newItemCode = Convert.ToInt32(lastItemCode.FirstOrDefault().ItemCode) + 1;
                    itemCode = FillLeadingZeroes(newItemCode, 10);
                }

                var salesAccount = from d in db.MstAccounts where d.Account == "Sales" select d;
                if (salesAccount.Any() == false)
                {
                    return new String[] { "Sales account not found.", "0" };
                }

                var assetsAccount = from d in db.MstAccounts where d.Account == "Inventory" select d;
                if (assetsAccount.Any() == false)
                {
                    return new String[] { "Assets account not found.", "0" };
                }

                var costAccount = from d in db.MstAccounts where d.Account == "Cost of Sales" select d;
                if (costAccount.Any() == false)
                {
                    return new String[] { "Cost account not found.", "0" };
                }

                var tax = from d in db.MstTaxes where d.Code == "VAT" select d;
                if (tax.Any() == false)
                {
                    return new String[] { "Tax not found.", "0" };
                }

                var unit = from d in db.MstUnits select d;
                if (unit.Any() == false)
                {
                    return new String[] { "Unit not found.", "0" };
                }

                var supplier = from d in db.MstSuppliers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().ReturnSupplierId) select d;
                if (supplier.Any() == false)
                {
                    return new String[] { "Supplier not found.", "0" };
                }

                Data.MstItem newItem = new Data.MstItem()
                {
                    ItemCode = itemCode,
                    BarCode = "NA",
                    ItemDescription = "NA",
                    Alias = "NA",
                    GenericName = "NA",
                    Category = "NA",
                    SalesAccountId = salesAccount.FirstOrDefault().Id,
                    AssetAccountId = assetsAccount.FirstOrDefault().Id,
                    CostAccountId = costAccount.FirstOrDefault().Id,
                    InTaxId = tax.FirstOrDefault().Id,
                    OutTaxId = tax.FirstOrDefault().Id,
                    UnitId = unit.FirstOrDefault().Id,
                    DefaultSupplierId = supplier.FirstOrDefault().Id,
                    Cost = 0,
                    MarkUp = 0,
                    Price = 0,
                    ImagePath = "NA",
                    ReorderQuantity = 0,
                    OnhandQuantity = 0,
                    IsInventory = true,
                    ExpiryDate = null,
                    LotNumber = "NA",
                    Remarks = "NA",
                    EntryUserId = currentUserLogin.FirstOrDefault().Id,
                    EntryDateTime = DateTime.Now,
                    UpdateUserId = currentUserLogin.FirstOrDefault().Id,
                    UpdateDateTime = DateTime.Now,
                    IsLocked = false,
                    DefaultKitchenReport = "",
                    IsPackage = false,
                    cValue = 0,
                    ChildItemId = null
                };

                db.MstItems.InsertOnSubmit(newItem);
                db.SubmitChanges();

                String newObject = Modules.SysAuditTrailModule.GetObjectString(newItem);

                Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                {
                    UserId = currentUserLogin.FirstOrDefault().Id,
                    AuditDate = DateTime.Now,
                    TableInformation = "MstItem",
                    RecordInformation = "",
                    FormInformation = newObject,
                    ActionInformation = "AddItem"
                };
                Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                return new String[] { "", newItem.Id.ToString() };
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // =========
        // Lock Item
        // =========
        public String[] LockItem(Int32 id, Entities.MstItemEntity objItem)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var item = from d in db.MstItems
                           where d.Id == id
                           select d;

                if (item.Any())
                {
                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(item.FirstOrDefault());

                    var lockItem = item.FirstOrDefault();
                    lockItem.ItemCode = objItem.ItemCode;
                    lockItem.BarCode = objItem.BarCode;
                    lockItem.ItemDescription = objItem.ItemDescription;
                    lockItem.Alias = objItem.Alias;
                    lockItem.GenericName = objItem.GenericName;
                    lockItem.Category = objItem.Category;
                    lockItem.OutTaxId = objItem.OutTaxId;
                    lockItem.UnitId = objItem.UnitId;
                    lockItem.DefaultSupplierId = objItem.DefaultSupplierId;
                    lockItem.Cost = objItem.Cost;
                    lockItem.MarkUp = objItem.MarkUp;
                    lockItem.Price = objItem.Price;
                    lockItem.ReorderQuantity = objItem.ReorderQuantity;
                    lockItem.OnhandQuantity = objItem.OnhandQuantity;
                    lockItem.IsInventory = objItem.IsInventory;
                    lockItem.IsPackage = objItem.IsPackage;
                    lockItem.ExpiryDate = Convert.ToDateTime(objItem.ExpiryDate);
                    lockItem.LotNumber = objItem.LotNumber;
                    lockItem.Remarks = objItem.Remarks;
                    lockItem.UpdateUserId = currentUserLogin.FirstOrDefault().Id;
                    lockItem.UpdateDateTime = DateTime.Now;
                    lockItem.IsLocked = true;
                    lockItem.cValue = objItem.cValue;
                    lockItem.ChildItemId = objItem.ChildItemId;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(item.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "MstItem",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "LockItem"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "" };
                }
                else
                {
                    return new String[] { "Item not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ===========
        // Unlock Item
        // ===========
        public String[] UnlockItem(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var item = from d in db.MstItems
                           where d.Id == id
                           select d;

                if (item.Any())
                {
                    if (item.FirstOrDefault().ItemCode.Equals("0000000001"))
                    {
                        return new String[] { "Unlock not allowed.", "0" };
                    }

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(item.FirstOrDefault());

                    var unLockItem = item.FirstOrDefault();
                    unLockItem.UpdateUserId = currentUserLogin.FirstOrDefault().Id;
                    unLockItem.UpdateDateTime = DateTime.Today;
                    unLockItem.IsLocked = false;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(item.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "MstItem",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "UnlockItem"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "" };
                }
                else
                {
                    return new String[] { "Item not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ===========
        // Delete Item
        // ===========
        public String[] DeleteItem(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var item = from d in db.MstItems
                           where d.Id == id
                           select d;

                if (item.Any())
                {
                    if (item.FirstOrDefault().ItemCode.Equals("0000000001"))
                    {
                        return new String[] { "Delete not allowed.", "0" };
                    }

                    if (item.FirstOrDefault().IsLocked == false)
                    {
                        var deleteItem = item.FirstOrDefault();
                        db.MstItems.DeleteOnSubmit(deleteItem);

                        String oldObject = Modules.SysAuditTrailModule.GetObjectString(item.FirstOrDefault());

                        Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                        {
                            UserId = currentUserLogin.FirstOrDefault().Id,
                            AuditDate = DateTime.Now,
                            TableInformation = "MstItem",
                            RecordInformation = oldObject,
                            FormInformation = "",
                            ActionInformation = "DeleteItem"
                        };
                        Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                        db.SubmitChanges();

                        return new String[] { "", "" };
                    }
                    else
                    {
                        return new String[] { "Item is Locked", "0" };
                    }
                }
                else
                {
                    return new String[] { "Item is Locked", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ===========
        // Import Item
        // ===========
        public String[] ImportItem(List<Entities.MstItemEntity> objitemList)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                if (objitemList.Any())
                {
                    var salesAccount = from d in db.MstAccounts where d.Account == "Sales" select d;
                    if (salesAccount.Any() == false)
                    {
                        return new String[] { "Sales account not found.", "0" };
                    }

                    var assetsAccount = from d in db.MstAccounts where d.Account == "Inventory" select d;
                    if (assetsAccount.Any() == false)
                    {
                        return new String[] { "Assets account not found.", "0" };
                    }

                    var costAccount = from d in db.MstAccounts where d.Account == "Cost of Sales" select d;
                    if (costAccount.Any() == false)
                    {
                        return new String[] { "Cost account not found.", "0" };
                    }

                    var tax = from d in db.MstTaxes where d.Code == "VAT" select d;
                    if (tax.Any() == false)
                    {
                        return new String[] { "Tax not found.", "0" };
                    }

                    var supplier = from d in db.MstSuppliers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().ReturnSupplierId) select d;
                    if (supplier.Any() == false)
                    {
                        return new String[] { "Supplier not found.", "0" };
                    }

                    foreach (var obj in objitemList)
                    {
                        String itemCode = "0000000001";
                        var lastItemCode = from d in db.MstItems.OrderByDescending(d => d.Id) select d;
                        if (lastItemCode.Any())
                        {
                            Int32 newItemCode = Convert.ToInt32(lastItemCode.FirstOrDefault().ItemCode) + 1;
                            itemCode = FillLeadingZeroes(newItemCode, 10);
                        }

                        var unit = from d in db.MstUnits
                                   where d.Unit == obj.Unit
                                   select d;

                        if (unit.Any() == false)
                        {
                            return new String[] { "Unit not found.", "0" };
                        }

                        Data.MstItem newItem = new Data.MstItem()
                        {
                            ItemCode = itemCode,
                            BarCode = obj.BarCode,
                            ItemDescription = obj.ItemDescription,
                            Alias = "NA",
                            GenericName = "NA",
                            Category = "NA",
                            SalesAccountId = salesAccount.FirstOrDefault().Id,
                            AssetAccountId = assetsAccount.FirstOrDefault().Id,
                            CostAccountId = costAccount.FirstOrDefault().Id,
                            InTaxId = tax.FirstOrDefault().Id,
                            OutTaxId = tax.FirstOrDefault().Id,
                            UnitId = unit.FirstOrDefault().Id,
                            DefaultSupplierId = supplier.FirstOrDefault().Id,
                            Cost = Convert.ToDecimal(obj.Cost),
                            MarkUp = 0,
                            Price = Convert.ToDecimal(obj.Price),
                            ImagePath = "NA",
                            ReorderQuantity = 0,
                            OnhandQuantity = 0,
                            IsInventory = true,
                            ExpiryDate = null,
                            LotNumber = "NA",
                            Remarks = "NA",
                            EntryUserId = currentUserLogin.FirstOrDefault().Id,
                            EntryDateTime = DateTime.Today,
                            UpdateUserId = currentUserLogin.FirstOrDefault().Id,
                            UpdateDateTime = DateTime.Today,
                            IsLocked = true,
                            DefaultKitchenReport = "",
                            IsPackage = false,
                            ChildItemId = null,
                            cValue = 0
                        };

                        db.MstItems.InsertOnSubmit(newItem);
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
        // ==================
        // Update Item Price
        // ==================
        public String[] UpdateItemPrice(List<Entities.MstItemEntity> objitemList)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                if (objitemList.Any())
                {

                    foreach (var obj in objitemList)
                    {
                        var item = from d in db.MstItems
                                   where d.BarCode == obj.BarCode
                                   select d;
                        if (item.Any() == false)
                        {
                            return new String[] { "Item not found.", "0" };
                        }
                        var updateItem = item.FirstOrDefault();
                        updateItem.Price = obj.Price;
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
