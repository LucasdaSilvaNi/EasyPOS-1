using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Controllers
{
    class TrnPurchaseOrderLineController
    {
        // ============
        // Data Context
        // ============
        public Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

        // ========================
        // List Purchase Order Line
        // ========================
        public List<Entities.TrnPurchaseOrderLineEntity> ListPurchaseOrderLine(Int32 purchaseOrderId)
        {
            var purchaseOrderLines = from d in db.TrnPurchaseOrderLines
                                     where d.PurchaseOrderId == purchaseOrderId
                                     select new Entities.TrnPurchaseOrderLineEntity
                                     {
                                         Id = d.Id,
                                         PurchaseOrderId = d.PurchaseOrderId,
                                         ItemId = d.ItemId,
                                         ItemDescription = d.MstItem.ItemDescription,
                                         UnitId = d.UnitId,
                                         Unit = d.MstUnit.Unit,
                                         Quantity = d.Quantity,
                                         Cost = d.MstItem.Cost,
                                         Amount = d.Amount,
                                         ReceivedQuantity = d.ReceivedQuantity,
                                     };

            return purchaseOrderLines.OrderByDescending(d => d.Id).ToList();
        }

        // ================
        // List Search Item
        // ================
        public List<Entities.MstItemEntity> ListSearchItem(String filter)
        {
            var items = from d in db.MstItems
                        where d.IsInventory == true
                        && (d.BarCode.Contains(filter)
                        || d.ItemDescription.Contains(filter)
                        || d.GenericName.Contains(filter))
                        select new Entities.MstItemEntity
                        {
                            Id = d.Id,
                            BarCode = d.BarCode,
                            ItemDescription = d.ItemDescription,
                            GenericName = d.GenericName,
                            OutTaxId = d.OutTaxId,
                            OutTax = d.MstTax1.Tax,
                            OutTaxRate = d.MstTax1.Rate,
                            UnitId = d.UnitId,
                            Unit = d.MstUnit.Unit,
                            Price = d.Price,
                            Cost = d.Cost,
                            OnhandQuantity = d.OnhandQuantity
                        };

            return items.OrderBy(d => d.ItemDescription).ToList();


        }
        // ===========
        // Detail Item
        // ===========
        public Entities.MstItemEntity DetailSearchItem(String barcode)
        {
            var item = from d in db.MstItems
                       where d.BarCode.Equals(barcode)
                       && d.IsInventory == true
                       select new Entities.MstItemEntity
                       {
                           Id = d.Id,
                           BarCode = d.BarCode,
                           ItemDescription = d.ItemDescription,
                           GenericName = d.GenericName,
                           OutTaxId = d.OutTaxId,
                           OutTax = d.MstTax1.Tax,
                           OutTaxRate = d.MstTax1.Rate,
                           UnitId = d.UnitId,
                           Unit = d.MstUnit.Unit,
                           Price = d.Price,
                           OnhandQuantity = d.OnhandQuantity
                       };

            return item.FirstOrDefault();
        }

        // =======================
        // Add Purchase Order Line
        // =======================
        public String[] AddPurchaseOrderLine(Entities.TrnPurchaseOrderLineEntity objPurchaseOrderLine)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var purchaseOrder = from d in db.TrnPurchaseOrders
                                    where d.Id == objPurchaseOrderLine.PurchaseOrderId
                                    select d;

                if (purchaseOrder.Any() == false)
                {
                    return new String[] { "Purchase Order transaction not found.", "0" };
                }

                var item = from d in db.MstItems
                           where d.Id == objPurchaseOrderLine.ItemId
                           && d.IsInventory == true
                           && d.IsLocked == true
                           select d;

                if (item.Any() == false)
                {
                    return new String[] { "Item not found.", "0" };
                }

                var account = from d in db.MstAccounts
                              where d.Account.Equals("Inventory")
                              && d.IsLocked == true
                              select d;

                if (account.Any() == false)
                {
                    return new String[] { "Account not found.", "0" };
                }

                Data.TrnPurchaseOrderLine newPurchaseOrderLine = new Data.TrnPurchaseOrderLine
                {
                    PurchaseOrderId = objPurchaseOrderLine.PurchaseOrderId,
                    ItemId = objPurchaseOrderLine.ItemId,
                    UnitId = item.FirstOrDefault().UnitId,
                    Quantity = objPurchaseOrderLine.Quantity,
                    Cost = objPurchaseOrderLine.Cost,
                    Amount = objPurchaseOrderLine.Amount,
                    ReceivedQuantity = objPurchaseOrderLine.ReceivedQuantity
                };

                db.TrnPurchaseOrderLines.InsertOnSubmit(newPurchaseOrderLine);
                db.SubmitChanges();

                String newObject = Modules.SysAuditTrailModule.GetObjectString(newPurchaseOrderLine);

                Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                {
                    UserId = currentUserLogin.FirstOrDefault().Id,
                    AuditDate = DateTime.Now,
                    TableInformation = "TrnPurchaseOrderLine",
                    RecordInformation = "",
                    FormInformation = newObject,
                    ActionInformation = "AddPurchaseOrderLine"
                };
                Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                return new String[] { "", "1" };
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ==========================
        // Update Purchase Order Line
        // ==========================
        public String[] UpdatePurchaseOrderLine(Int32 id, Entities.TrnPurchaseOrderLineEntity objPurchaseOrderLine)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var purchaseOrderLine = from d in db.TrnPurchaseOrderLines
                                        where d.Id == id
                                        select d;

                if (purchaseOrderLine.Any())
                {
                    var purchaseOrderLines = from d in db.TrnPurchaseOrderLines
                                             where d.Id == objPurchaseOrderLine.PurchaseOrderId
                                             select d;

                    if (purchaseOrderLine.Any() == false)
                    {
                        return new String[] { "Purchase Order transaction not found.", "0" };
                    }

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(purchaseOrderLine.FirstOrDefault());

                    var updatePurchaseOrderLine = purchaseOrderLine.FirstOrDefault();
                    updatePurchaseOrderLine.Quantity = objPurchaseOrderLine.Quantity;
                    updatePurchaseOrderLine.Cost = objPurchaseOrderLine.Cost;
                    updatePurchaseOrderLine.Amount = objPurchaseOrderLine.Amount;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(purchaseOrderLine.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "TrnPurchaseOrderLine",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "UpdatePurchaseOrderLine"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Purchase Order line not found.  " + id, "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ==========================
        // Delete Purchase Order Line
        // ==========================
        public String[] DeletePurchaseOrderLine(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var purchaseOrderLine = from d in db.TrnPurchaseOrderLines
                                        where d.Id == id
                                        select d;

                if (purchaseOrderLine.Any())
                {
                    var deletePurchaseOrderLine = purchaseOrderLine.FirstOrDefault();
                    db.TrnPurchaseOrderLines.DeleteOnSubmit(deletePurchaseOrderLine);

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(purchaseOrderLine.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "TrnPurchaseOrderLine",
                        RecordInformation = oldObject,
                        FormInformation = "",
                        ActionInformation = "DeletePurchaseOrderLine"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    db.SubmitChanges();

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Purchase Order line not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // =====================
        // Barcode Stock-In Line
        // =====================
        public String[] BarcodePurchaseOrderLine(Int32 purchaseorderId, String barcode)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var purchaseOrder = from d in db.TrnPurchaseOrders
                                    where d.Id == purchaseorderId
                                    select d;

                if (purchaseOrder.Any() == false)
                {
                    return new String[] { "Stock-In transaction not found.", "0" };
                }

                var item = from d in db.MstItems
                           where d.BarCode.Equals(barcode)
                           && d.IsInventory == true
                           && d.IsLocked == true
                           select d;

                if (item.Any() == false)
                {
                    return new String[] { "Item not found.", "0" };
                }

                var account = from d in db.MstAccounts
                              where d.Account.Equals("Inventory")
                              && d.IsLocked == true
                              select d;

                if (account.Any() == false)
                {
                    return new String[] { "Account not found.", "0" };
                }

                Data.TrnPurchaseOrderLine newPurchaseOrderLine = new Data.TrnPurchaseOrderLine
                {
                    PurchaseOrderId = purchaseorderId,
                    ItemId = item.FirstOrDefault().Id,
                    UnitId = item.FirstOrDefault().UnitId,
                    Quantity = 1,
                    Cost = 0,
                    Amount = 0,
                    ReceivedQuantity = 0
                };

                db.TrnPurchaseOrderLines.InsertOnSubmit(newPurchaseOrderLine);
                db.SubmitChanges();

                String newObject = Modules.SysAuditTrailModule.GetObjectString(newPurchaseOrderLine);

                Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                {
                    UserId = currentUserLogin.FirstOrDefault().Id,
                    AuditDate = DateTime.Now,
                    TableInformation = "TrnPurchaseOrderLine",
                    RecordInformation = "",
                    FormInformation = newObject,
                    ActionInformation = "AddPurchaseOrderLine"
                };
                Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                return new String[] { "", "1" };
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ========================
        // Update Received Quantity
        // ========================
        public String[] UpdatePurchaseOrderLineReceivedQuantity(Int32 id, Decimal receivedQuantity)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var purchaseOrderLine = from d in db.TrnPurchaseOrderLines
                                        where d.Id == id
                                        select d;

                if (purchaseOrderLine.Any())
                {
                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(purchaseOrderLine.FirstOrDefault());

                    var updatePurchaseOrderLine = purchaseOrderLine.FirstOrDefault();
                    updatePurchaseOrderLine.ReceivedQuantity = receivedQuantity;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(purchaseOrderLine.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "TrnPurchaseOrderLine",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "UpdatePurchaseOrderLine"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Purchase Order line not found.  " + id, "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }
    }
}
