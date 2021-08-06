using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Modules
{
    class TrnInventoryModule
    {
        // ============
        // Data Context
        // ============
        public Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

        // ======================
        // Update Sales Inventory
        // ======================
        public void UpdateSalesInventory(Int32 salesId)
        {
            try
            {
                var sales = from d in db.TrnSales
                            where d.Id == salesId
                            select d;

                if (sales.Any())
                {
                    var salesLines = sales.FirstOrDefault().TrnSalesLines;
                    if (salesLines.Any())
                    {
                        var salesLineItems = from d in salesLines
                                             group d by new
                                             {
                                                 d.MstItem
                                             } into g
                                             select new
                                             {
                                                 g.Key.MstItem,
                                             };

                        if (salesLineItems.Any())
                        {
                            foreach (var salesLineItem in salesLineItems)
                            {
                                if (salesLineItem.MstItem.IsInventory == true)
                                {
                                    UpdateItemInventory(salesLineItem.MstItem.Id);
                                }
                                else
                                {
                                    if (salesLineItem.MstItem.MstItemComponents.Any())
                                    {
                                        foreach (var component in salesLineItem.MstItem.MstItemComponents)
                                        {
                                            UpdateItemInventory(component.ComponentItemId);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // =========================
        // Update Stock-In Inventory
        // =========================
        public void UpdateStockInInventory(Int32 stockInId)
        {
            try
            {
                var stockIn = from d in db.TrnStockIns
                              where d.Id == stockInId
                              select d;

                if (stockIn.Any())
                {
                    var stockInLines = stockIn.FirstOrDefault().TrnStockInLines.Where(d => d.MstItem.IsInventory == true);
                    if (stockInLines.Any())
                    {
                        var stockInLineItems = from d in stockInLines
                                               group d by new
                                               {
                                                   d.ItemId
                                               } into g
                                               select new
                                               {
                                                   g.Key.ItemId
                                               };

                        if (stockInLineItems.Any())
                        {
                            foreach (var stockInLineItem in stockInLineItems)
                            {
                                UpdateItemInventory(stockInLineItem.ItemId);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // ==========================
        // Update Stock-Out Inventory
        // ==========================
        public void UpdateStockOutInventory(Int32 stockOutId)
        {
            try
            {
                var stockOut = from d in db.TrnStockOuts
                               where d.Id == stockOutId
                               select d;

                if (stockOut.Any())
                {
                    var stockOutLines = stockOut.FirstOrDefault().TrnStockOutLines.Where(d => d.MstItem.IsInventory == true);
                    if (stockOutLines.Any())
                    {
                        var stockOutLineItems = from d in stockOutLines
                                                group d by new
                                                {
                                                    d.ItemId
                                                } into g
                                                select new
                                                {
                                                    g.Key.ItemId
                                                };

                        if (stockOutLineItems.Any())
                        {
                            foreach (var stockOutLineItem in stockOutLineItems)
                            {
                                UpdateItemInventory(stockOutLineItem.ItemId);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // =====================
        // Update Item Inventory
        // =====================
        public void UpdateItemInventory(Int32 itemId)
        {
            var item = from d in db.MstItems where d.Id == itemId select d;
            if (item.Any())
            {
                // Get total IN quantity
                Decimal totalStockInLineQuantity = 0;
                var allStockInLineItems = from d in db.TrnStockInLines
                                          where d.ItemId == itemId
                                          && d.TrnStockIn.IsLocked == true
                                          select d;

                if (allStockInLineItems.Any())
                {
                    totalStockInLineQuantity = allStockInLineItems.Sum(d => d.Quantity);
                }

                // Get total SOLD quantity
                Decimal totalSalesLineQuantity = 0;
                var allSalesLineItems = from d in db.TrnSalesLines
                                        where d.ItemId == itemId
                                        && d.TrnSale.IsLocked == true
                                        && d.TrnSale.IsCancelled == false
                                        && d.TrnSale.IsReturned == false
                                        select d;

                if (allSalesLineItems.Any())
                {
                    totalSalesLineQuantity = allSalesLineItems.Sum(d => d.Quantity);
                }

                // Get total RETURNED quantity
                Decimal totalReturnedQuantity = 0;
                var allReturnedItems = from d in db.TrnSalesLines
                                        where d.ItemId == itemId
                                        && d.TrnSale.IsLocked == true
                                        && d.TrnSale.IsCancelled == false
                                        && d.TrnSale.IsReturned == true
                                        select d;

                if (allReturnedItems.Any())
                {
                    totalReturnedQuantity = allReturnedItems.Sum(d => d.Quantity < 0 ? d.Quantity * -1 : d.Quantity);
                }

                // Get total OUT quantity
                Decimal totalStockOutLineQuantity = 0;
                var allStockOutLineItems = from d in db.TrnStockOutLines
                                           where d.ItemId == itemId
                                           && d.TrnStockOut.IsLocked == true
                                           select d;

                if (allStockOutLineItems.Any())
                {
                    totalStockOutLineQuantity = allStockOutLineItems.Sum(d => d.Quantity);
                }

                // Get total SOLD Components Quantity
                Decimal totalSalesLineComponentQuantity = 0;
                var components = from d in db.MstItemComponents
                                 where d.ComponentItemId == itemId
                                 select d;

                if (components.Any())
                {
                    foreach (var component in components)
                    {
                        var allSalesLineComponentItems = from d in db.TrnSalesLines
                                                         where d.ItemId == component.ItemId
                                                         && d.TrnSale.IsLocked == true
                                                         && d.TrnSale.IsCancelled == false
                                                         select d;

                        if (allSalesLineComponentItems.Any())
                        {
                            totalSalesLineComponentQuantity += allSalesLineComponentItems.Sum(d => d.Quantity) * component.Quantity;
                        }
                    }
                }

                var updateItem = item.FirstOrDefault();
                updateItem.OnhandQuantity = (totalStockInLineQuantity + totalReturnedQuantity) - (totalSalesLineQuantity + totalStockOutLineQuantity + totalSalesLineComponentQuantity);
                db.SubmitChanges();
            }
        }
    }
}
