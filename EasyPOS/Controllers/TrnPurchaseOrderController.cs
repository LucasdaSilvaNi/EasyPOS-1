using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Controllers
{
    class TrnPurchaseOrderController
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
        // ===================
        // List Purchase Order 
        // ===================
        public List<Entities.TrnPurchaseOrderEntity> ListPurchaseOrder(DateTime dateFilter, String filter)
        {
            var purchaseOrders = from d in db.TrnPurchaseOrders
                                 where d.PurchaseOrderDate == dateFilter
                                 && d.PurchaseOrderNumber.Contains(filter)
                                 select new Entities.TrnPurchaseOrderEntity
                                 {
                                     Id = d.Id,
                                     PeriodId = d.PeriodId,
                                     PurchaseOrderDate = d.PurchaseOrderDate.ToShortDateString(),
                                     PurchaseOrderNumber = d.PurchaseOrderNumber,
                                     Amount = d.Amount,
                                     SupplierId = d.SupplierId,
                                     Supplier = d.MstSupplier.Supplier,
                                     Remarks = d.Remarks,
                                     PreparedBy = d.PreparedBy,
                                     CheckedBy = d.CheckedBy,
                                     ApprovedBy = d.ApprovedBy,
                                     IsLocked = d.IsLocked,
                                     EntryUser = d.MstUser.FullName,
                                     EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                     UpdateUser = d.MstUser1.FullName,
                                     UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                                     RequestedBy = d.RequestedBy
                                 };
            return purchaseOrders.OrderByDescending(d => d.Id).ToList();
        }

        // =====================
        // Detail Purchase Order
        // =====================
        public Entities.TrnPurchaseOrderEntity DetailPurchaseOrder(Int32 id)
        {
            var purchaseOrder = from d in db.TrnPurchaseOrders
                                where d.Id == id
                                select new Entities.TrnPurchaseOrderEntity
                                {
                                    Id = d.Id,
                                    PeriodId = d.PeriodId,
                                    PurchaseOrderDate = d.PurchaseOrderDate.ToShortDateString(),
                                    PurchaseOrderNumber = d.PurchaseOrderNumber,
                                    Amount = d.Amount,
                                    SupplierId = d.SupplierId,
                                    Supplier = d.MstSupplier.Supplier,
                                    Remarks = d.Remarks,
                                    PreparedBy = d.PreparedBy,
                                    CheckedBy = d.CheckedBy,
                                    ApprovedBy = d.ApprovedBy,
                                    IsLocked = d.IsLocked,
                                    EntryUser = d.MstUser.FullName,
                                    EntryDateTime = d.EntryDateTime.ToShortDateString(),
                                    UpdateUser = d.MstUser1.FullName,
                                    UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                                    RequestedBy = d.RequestedBy,
                                    Status = d.Status
                                };
            return purchaseOrder.FirstOrDefault();
        }
        // ========================
        // Dropdown List - Supplier
        // ========================
        public List<Entities.MstSupplierEntity> DropdownListPurchaseOrderSupplier()
        {
            var suppliers = from d in db.MstSuppliers
                            select new Entities.MstSupplierEntity
                            {
                                Id = d.Id,
                                Supplier = d.Supplier
                            };

            return suppliers.ToList();
        }
       

        // ====================
        // Dropdown List - User
        // ====================
        public List<Entities.MstUserEntity> DropdownListPurchaseOrderUser()
        {
            var users = from d in db.MstUsers
                        select new Entities.MstUserEntity
                        {
                            Id = d.Id,
                            FullName = d.FullName
                        };

            return users.ToList();
        }

        // ==================
        // Add Purchase Order
        // ==================
        public String[] AddPurchaseOrder()
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

                var supplier = from d in db.MstSuppliers select d;
                if (supplier.Any() == false)
                {
                    return new String[] { "Supplier not found.", "0" };
                }

                DateTime currentDate = DateTime.Today;
                if (Modules.SysCurrentModule.GetCurrentSettings().IsLoginDate == true)
                {
                    currentDate = Convert.ToDateTime(Modules.SysCurrentModule.GetCurrentSettings().CurrentDate);
                }

                String PurchaseOrderNumber = "0000000001";
                var lastSPurchaseOrder = from d in db.TrnPurchaseOrders.OrderByDescending(d => d.Id) select d;
                if (lastSPurchaseOrder.Any())
                {
                    Int32 newpurchaseOrder = Convert.ToInt32(lastSPurchaseOrder.FirstOrDefault().PurchaseOrderNumber) + 1;
                    PurchaseOrderNumber = FillLeadingZeroes(newpurchaseOrder, 10);
                }

                Data.TrnPurchaseOrder newPurchaseOrder = new Data.TrnPurchaseOrder()
                {
                    PeriodId = period.FirstOrDefault().Id,
                    PurchaseOrderDate = currentDate,
                    PurchaseOrderNumber = PurchaseOrderNumber,
                    Amount = 0,
                    SupplierId = supplier.FirstOrDefault().Id,
                    Remarks = "",
                    PreparedBy = currentUserLogin.FirstOrDefault().Id,
                    CheckedBy = currentUserLogin.FirstOrDefault().Id,
                    ApprovedBy = currentUserLogin.FirstOrDefault().Id,
                    IsLocked = false,
                    EntryUserId = currentUserLogin.FirstOrDefault().Id,
                    EntryDateTime = DateTime.Now,
                    UpdateUserId = currentUserLogin.FirstOrDefault().Id,
                    UpdateDateTime = DateTime.Now,
                    RequestedBy = currentUserLogin.FirstOrDefault().Id
                };
                db.TrnPurchaseOrders.InsertOnSubmit(newPurchaseOrder);
                db.SubmitChanges();

                String newObject = Modules.SysAuditTrailModule.GetObjectString(newPurchaseOrder);

                Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                {
                    UserId = currentUserLogin.FirstOrDefault().Id,
                    AuditDate = DateTime.Now,
                    TableInformation = "TrnPurchaseOrder",
                    RecordInformation = "",
                    FormInformation = newObject,
                    ActionInformation = "AddPurchaseOrder"
                };
                Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                return new String[] { "", newPurchaseOrder.Id.ToString() };
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }
        // ===================
        // Lock Purchase Order
        // ===================
        public String[] LockPurcherOrder(Int32 id, Entities.TrnPurchaseOrderEntity objPurchaseOrder)
        {
            try
            {

                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var supplier = from d in db.MstSuppliers
                               where d.Id == objPurchaseOrder.SupplierId
                               && d.IsLocked == true
                               select d;

                if (supplier.Any() == false)
                {
                    return new String[] { "Supplier not found.", "0" };
                }

                var checkedByUser = from d in db.MstUsers
                                    where d.Id == objPurchaseOrder.CheckedBy
                                    && d.IsLocked == true
                                    select d;

                if (checkedByUser.Any() == false)
                {
                    return new String[] { "Checked by user not found.", "0" };
                }

                var approvedByUser = from d in db.MstUsers
                                     where d.Id == objPurchaseOrder.ApprovedBy
                                     && d.IsLocked == true
                                     select d;

                if (approvedByUser.Any() == false)
                {
                    return new String[] { "Approved by user not found.", "0" };
                }

                var purchaseOrder = from d in db.TrnPurchaseOrders
                                    where d.Id == id
                                    select d;

                if (purchaseOrder.Any())
                {
                    if (purchaseOrder.FirstOrDefault().IsLocked)
                    {
                        return new String[] { "Already locked.", "0" };
                    }

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(purchaseOrder.FirstOrDefault());

                    var lockPurchaseOrder = purchaseOrder.FirstOrDefault();
                    lockPurchaseOrder.PurchaseOrderDate = Convert.ToDateTime(objPurchaseOrder.PurchaseOrderDate);
                    lockPurchaseOrder.SupplierId = objPurchaseOrder.SupplierId;
                    lockPurchaseOrder.Remarks = objPurchaseOrder.Remarks;
                    lockPurchaseOrder.CheckedBy = objPurchaseOrder.CheckedBy;
                    lockPurchaseOrder.ApprovedBy = objPurchaseOrder.ApprovedBy;
                    lockPurchaseOrder.IsLocked = true;
                    lockPurchaseOrder.UpdateUserId = currentUserLogin.FirstOrDefault().Id;
                    lockPurchaseOrder.UpdateDateTime = DateTime.Now;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(purchaseOrder.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "TrnPurchaseOrder",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "LockPurchaseOrder"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Purchase Order not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ===============
        // Unlock Purchase Order
        // ===============
        public String[] UnlockPurchaseOrder(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var purchaseOrder = from d in db.TrnPurchaseOrders
                                    where d.Id == id
                                    select d;

                if (purchaseOrder.Any())
                {
                    if (purchaseOrder.FirstOrDefault().IsLocked == false)
                    {
                        return new String[] { "Already unlocked.", "0" };
                    }

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(purchaseOrder.FirstOrDefault());

                    var unlockPurchaseOrder = purchaseOrder.FirstOrDefault();
                    unlockPurchaseOrder.IsLocked = false;
                    unlockPurchaseOrder.UpdateUserId = currentUserLogin.FirstOrDefault().Id;
                    unlockPurchaseOrder.UpdateDateTime = DateTime.Now;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(purchaseOrder.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "TrnPurchaseOrder",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "UnlockPurchaseOrder"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Purchase Order not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }
        // =====================
        // Delete Purchase Order
        // =====================
        public String[] DeletePurchaseOrder(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var purchaseOrder = from d in db.TrnPurchaseOrders
                                    where d.Id == id
                                    select d;

                if (purchaseOrder.Any())
                {
                    if (purchaseOrder.FirstOrDefault().IsLocked)
                    {
                        return new String[] { "Purchase Order is locked", "0" };
                    }

                    var deletePurchaseOrder = purchaseOrder.FirstOrDefault();
                    db.TrnPurchaseOrders.DeleteOnSubmit(deletePurchaseOrder);

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(purchaseOrder.FirstOrDefault());

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
                    return new String[] { "Purchase Order not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }
        // ================
        // Post Stock-In
        // ================
        public String[] PostStockIn(Int32 id)
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

                var PurchaseOrder = from d in db.TrnPurchaseOrders
                                    where d.Id == id
                                    select d;

                if (PurchaseOrder.Any())
                {
                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(PurchaseOrder.FirstOrDefault());

                    String stockInNumber = "0000000001";
                    var lastStockIn = from d in db.TrnStockIns.OrderByDescending(d => d.Id) select d;
                    if (lastStockIn.Any())
                    {
                        Int32 newStockInNumber = Convert.ToInt32(lastStockIn.FirstOrDefault().StockInNumber) + 1;
                        stockInNumber = FillLeadingZeroes(newStockInNumber, 10);
                    }

                    Data.TrnStockIn newStockIn = new Data.TrnStockIn()
                    {
                        PeriodId = PurchaseOrder.FirstOrDefault().PeriodId,
                        StockInDate = PurchaseOrder.FirstOrDefault().PurchaseOrderDate,
                        StockInNumber = stockInNumber,
                        SupplierId = PurchaseOrder.FirstOrDefault().SupplierId,
                        Remarks = PurchaseOrder.FirstOrDefault().PurchaseOrderNumber + PurchaseOrder.FirstOrDefault().Remarks,
                        PreparedBy = PurchaseOrder.FirstOrDefault().PreparedBy,
                        CheckedBy = PurchaseOrder.FirstOrDefault().CheckedBy,
                        ApprovedBy = PurchaseOrder.FirstOrDefault().ApprovedBy,
                        IsLocked = true,
                        EntryUserId = currentUserLogin.FirstOrDefault().Id,
                        EntryDateTime = DateTime.Now,
                        UpdateUserId = currentUserLogin.FirstOrDefault().Id,
                        UpdateDateTime = DateTime.Now
                    };

                    db.TrnStockIns.InsertOnSubmit(newStockIn);
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(newStockIn);

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

                    if (PurchaseOrder.FirstOrDefault().TrnPurchaseOrderLines.Any() == true)
                    {
                        List<Data.TrnStockInLine> newStockInLines = new List<Data.TrnStockInLine>();
                        foreach (var purchaseOrderLine in PurchaseOrder.FirstOrDefault().TrnPurchaseOrderLines)
                        {
                            newStockInLines.Add(new Data.TrnStockInLine
                            {
                                StockInId = newStockIn.Id,
                                ItemId = purchaseOrderLine.ItemId,
                                UnitId = purchaseOrderLine.UnitId,
                                Quantity = purchaseOrderLine.ReceivedQuantity,
                                Cost = purchaseOrderLine.Cost,
                                Amount = purchaseOrderLine.ReceivedQuantity * purchaseOrderLine.Cost,
                                AssetAccountId = purchaseOrderLine.MstItem.AssetAccountId
                            });
                        }

                        db.TrnStockInLines.InsertAllOnSubmit(newStockInLines);
                        db.SubmitChanges();
                    }

                    Modules.TrnInventoryModule trnInventoryModule = new Modules.TrnInventoryModule();
                    trnInventoryModule.UpdateStockInInventory(newStockIn.Id);

                    var updatePO = PurchaseOrder.FirstOrDefault();
                    updatePO.Status = "RECEIVED";
                    db.SubmitChanges();

                    return new String[] { "", newStockIn.Id.ToString() };
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

