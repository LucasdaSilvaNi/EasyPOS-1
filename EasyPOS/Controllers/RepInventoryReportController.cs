using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Controllers
{
    class RepInventoryReportController
    {
        // ============
        // Data Context
        // ============
        public Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

        // ======================
        // Dropdown List Item
        // ======================
        public List<Entities.MstItemEntity> DropdownListItem()
        {
            var items = from d in db.MstItems
                        where d.IsInventory == true
                        && d.IsLocked == true
                        select new Entities.MstItemEntity
                        {
                            Id = d.Id,
                            BarCode = d.BarCode,
                            ItemDescription = d.ItemDescription
                        };

            return items.OrderBy(d => d.ItemDescription).ToList();
        }
        // ======================
        // Dropdown List Item Category
        // ======================
        public List<Entities.MstItemEntity> DropdownListItemCategory()
        {
            var items = from d in db.MstItems
                        where d.IsInventory == true
                        && d.IsLocked == true
                        select new Entities.MstItemEntity
                        {
                            Category = d.Category
                        };

            return items.Distinct().OrderBy(d => d.Category).ToList();
        }
        // ================
        // Inventory Report
        // ================
        public List<Entities.RepInventoryReportEntity> InventoryReport(DateTime startDate, DateTime endDate, String category, String filter, Int32 itemId)
        {
            if (itemId == 0 &&category == "ALL" )
            {
                List<Entities.RepInventoryReportEntity> newRepInventoryReportEntity = new List<Entities.RepInventoryReportEntity>();
                var beginningInInventories = from d in db.TrnStockInLines
                                             where d.TrnStockIn.IsLocked == true
                                             && d.TrnStockIn.StockInDate < startDate.Date
                                             && d.MstItem.IsInventory == true
                                             && d.MstItem.IsLocked == true
                                             select new Entities.RepInventoryReportEntity
                                             {
                                                 Document = "Beg",
                                                 Id = "Beg-In-" + d.Id,
                                                 InventoryDate = d.TrnStockIn.StockInDate,
                                                 ItemCode = d.MstItem.ItemCode,
                                                 BarCode = d.MstItem.BarCode,
                                                 ItemDescription = d.MstItem.ItemDescription,
                                                 BeginningQuantity = d.Quantity,
                                                 InQuantity = 0,
                                                 OutQuantity = 0,
                                                 EndingQuantity = 0,
                                                 Unit = d.MstUnit.Unit,
                                                 Cost = d.MstItem.Cost,
                                                 Price = d.MstItem.Price,
                                                 Amount = 0
                                             };

                var beginningSoldInventories = from d in db.TrnSalesLines
                                               where d.TrnSale.IsLocked == true
                                               && d.TrnSale.IsCancelled == false
                                               && d.TrnSale.SalesDate < startDate.Date
                                               && d.MstItem.IsInventory == true
                                               && d.MstItem.IsLocked == true
                                               select new Entities.RepInventoryReportEntity
                                               {
                                                   Document = "Beg",
                                                   Id = "Beg-Sold-" + d.Id,
                                                   InventoryDate = d.TrnSale.SalesDate,
                                                   ItemCode = d.MstItem.ItemCode,
                                                   BarCode = d.MstItem.BarCode,
                                                   ItemDescription = d.MstItem.ItemDescription,
                                                   BeginningQuantity = d.Quantity * -1,
                                                   InQuantity = 0,
                                                   OutQuantity = 0,
                                                   EndingQuantity = 0,
                                                   Unit = d.MstUnit.Unit,
                                                   Cost = d.MstItem.Cost,
                                                   Price = d.MstItem.Price,
                                                   Amount = 0
                                               };

                List<Entities.RepInventoryReportEntity> beginningSoldComponentInventories = new List<Entities.RepInventoryReportEntity>();

                var beginningSoldComponents = from d in db.TrnSalesLines
                                              where d.TrnSale.IsLocked == true
                                              && d.TrnSale.IsCancelled == false
                                              && d.TrnSale.SalesDate < startDate.Date
                                              && d.MstItem.IsInventory == false
                                              && d.MstItem.MstItemComponents.Any() == true
                                              && d.MstItem.IsLocked == true
                                              select d;

                if (beginningSoldComponents.ToList().Any() == true)
                {
                    foreach (var beginningSoldComponent in beginningSoldComponents.ToList())
                    {
                        var itemComponents = beginningSoldComponent.MstItem.MstItemComponents;
                        if (itemComponents.Any() == true)
                        {
                            foreach (var itemComponent in itemComponents.ToList())
                            {
                                beginningSoldComponentInventories.Add(new Entities.RepInventoryReportEntity()
                                {
                                    Document = "Beg",
                                    Id = "Beg-Sold-Component" + itemComponent.Id,
                                    InventoryDate = beginningSoldComponent.TrnSale.SalesDate,
                                    ItemCode = itemComponent.MstItem1.ItemCode,
                                    BarCode = itemComponent.MstItem1.BarCode,
                                    ItemDescription = itemComponent.MstItem1.ItemDescription,
                                    BeginningQuantity = (itemComponent.Quantity * beginningSoldComponent.Quantity) * -1,
                                    InQuantity = 0,
                                    OutQuantity = 0,
                                    EndingQuantity = 0,
                                    Unit = itemComponent.MstItem1.MstUnit.Unit,
                                    Cost = itemComponent.MstItem1.Cost,
                                    Price = itemComponent.MstItem1.Price,
                                    Amount = 0
                                });
                            }
                        }
                    }
                }

                var beginningOutInventories = from d in db.TrnStockOutLines
                                              where d.TrnStockOut.IsLocked == true
                                              && d.TrnStockOut.StockOutDate < startDate.Date
                                              && d.MstItem.IsInventory == true
                                              && d.MstItem.IsLocked == true
                                              select new Entities.RepInventoryReportEntity
                                              {
                                                  Document = "Beg",
                                                  Id = "Beg-Out-" + d.Id,
                                                  InventoryDate = d.TrnStockOut.StockOutDate,
                                                  ItemCode = d.MstItem.ItemCode,
                                                  BarCode = d.MstItem.BarCode,
                                                  ItemDescription = d.MstItem.ItemDescription,
                                                  BeginningQuantity = d.Quantity * -1,
                                                  InQuantity = 0,
                                                  OutQuantity = 0,
                                                  EndingQuantity = 0,
                                                  Unit = d.MstUnit.Unit,
                                                  Cost = d.MstItem.Cost,
                                                  Price = d.MstItem.Price,
                                                  Amount = 0
                                              };

                var unionBeginningInventories = beginningInInventories.ToList().Union(beginningSoldInventories.ToList()).Union(beginningSoldComponentInventories.ToList()).Union(beginningOutInventories.ToList());

                var currentInInventories = from d in db.TrnStockInLines
                                           where d.TrnStockIn.IsLocked == true
                                           && d.TrnStockIn.StockInDate >= startDate.Date
                                           && d.TrnStockIn.StockInDate <= endDate.Date
                                           && d.MstItem.IsInventory == true
                                           && d.MstItem.IsLocked == true
                                           select new Entities.RepInventoryReportEntity
                                           {
                                               Document = "Cur",
                                               Id = "Cur-In-" + d.Id,
                                               InventoryDate = d.TrnStockIn.StockInDate,
                                               ItemCode = d.MstItem.ItemCode,
                                               BarCode = d.MstItem.BarCode,
                                               ItemDescription = d.MstItem.ItemDescription,
                                               BeginningQuantity = 0,
                                               InQuantity = d.Quantity,
                                               OutQuantity = 0,
                                               EndingQuantity = 0,
                                               Unit = d.MstUnit.Unit,
                                               Cost = d.MstItem.Cost,
                                               Price = d.MstItem.Price,
                                               Amount = 0
                                           };

                var currentSoldInventories = from d in db.TrnSalesLines
                                             where d.TrnSale.IsLocked == true
                                             && d.TrnSale.IsCancelled == false
                                             && d.TrnSale.SalesDate >= startDate.Date
                                             && d.TrnSale.SalesDate <= endDate.Date
                                             && d.MstItem.IsInventory == true
                                             && d.MstItem.IsLocked == true
                                             select new Entities.RepInventoryReportEntity
                                             {
                                                 Document = "Cur",
                                                 Id = "Cur-Sold-" + d.Id,
                                                 InventoryDate = d.TrnSale.SalesDate,
                                                 ItemCode = d.MstItem.ItemCode,
                                                 BarCode = d.MstItem.BarCode,
                                                 ItemDescription = d.MstItem.ItemDescription,
                                                 BeginningQuantity = 0,
                                                 InQuantity = 0,
                                                 OutQuantity = d.Quantity,
                                                 EndingQuantity = 0,
                                                 Unit = d.MstUnit.Unit,
                                                 Cost = d.MstItem.Cost,
                                                 Price = d.MstItem.Price,
                                                 Amount = 0
                                             };

                List<Entities.RepInventoryReportEntity> currentSoldComponentInventories = new List<Entities.RepInventoryReportEntity>();

                var currentSoldComponents = from d in db.TrnSalesLines
                                            where d.TrnSale.IsLocked == true
                                            && d.TrnSale.IsCancelled == false
                                            && d.TrnSale.SalesDate >= startDate.Date
                                            && d.TrnSale.SalesDate <= endDate.Date
                                            && d.MstItem.IsInventory == false
                                            && d.MstItem.MstItemComponents.Any() == true
                                            && d.MstItem.IsLocked == true
                                            select d;

                if (currentSoldComponents.ToList().Any() == true)
                {
                    foreach (var currentSoldComponent in currentSoldComponents.ToList())
                    {
                        var itemComponents = currentSoldComponent.MstItem.MstItemComponents;
                        if (itemComponents.Any() == true)
                        {
                            foreach (var itemComponent in itemComponents.ToList())
                            {
                                currentSoldComponentInventories.Add(new Entities.RepInventoryReportEntity()
                                {
                                    Document = "Cur",
                                    Id = "Cur-Sold-Component" + itemComponent.Id,
                                    InventoryDate = currentSoldComponent.TrnSale.SalesDate,
                                    ItemCode = itemComponent.MstItem1.ItemCode,
                                    BarCode = itemComponent.MstItem1.BarCode,
                                    ItemDescription = itemComponent.MstItem1.ItemDescription,
                                    BeginningQuantity = 0,
                                    InQuantity = 0,
                                    OutQuantity = itemComponent.Quantity * currentSoldComponent.Quantity,
                                    EndingQuantity = 0,
                                    Unit = itemComponent.MstItem1.MstUnit.Unit,
                                    Cost = itemComponent.MstItem1.Cost,
                                    Price = itemComponent.MstItem1.Price,
                                    Amount = 0
                                });
                            }
                        }
                    }
                }

                var currentOutInventories = from d in db.TrnStockOutLines
                                            where d.TrnStockOut.IsLocked == true
                                            && d.TrnStockOut.StockOutDate >= startDate.Date
                                            && d.TrnStockOut.StockOutDate <= endDate.Date
                                            && d.MstItem.IsInventory == true
                                            && d.MstItem.IsLocked == true
                                            select new Entities.RepInventoryReportEntity
                                            {
                                                Document = "Cur",
                                                Id = "Cur-Out-" + d.Id,
                                                InventoryDate = d.TrnStockOut.StockOutDate,
                                                ItemCode = d.MstItem.ItemCode,
                                                BarCode = d.MstItem.BarCode,
                                                ItemDescription = d.MstItem.ItemDescription,
                                                BeginningQuantity = 0,
                                                InQuantity = 0,
                                                OutQuantity = d.Quantity,
                                                EndingQuantity = 0,
                                                Unit = d.MstUnit.Unit,
                                                Cost = d.MstItem.Cost,
                                                Price = d.MstItem.Price,
                                                Amount = 0
                                            };

                var unionCurrentInventories = currentInInventories.ToList().Union(currentSoldInventories.ToList()).Union(currentSoldComponentInventories.ToList()).Union(currentOutInventories.ToList());

                var unionInventories = unionBeginningInventories.ToList().Union(unionCurrentInventories.ToList());
                if (unionInventories.Any())
                {
                    var inventories = from d in unionInventories
                                      group d by new
                                      {
                                          d.ItemCode,
                                          d.BarCode,
                                          d.ItemDescription,
                                          d.Unit,
                                          d.Cost,
                                          d.Price
                                      } into g
                                      select new Entities.RepInventoryReportEntity
                                      {
                                          ItemCode = g.Key.ItemCode,
                                          BarCode = g.Key.BarCode,
                                          ItemDescription = g.Key.ItemDescription,
                                          Unit = g.Key.Unit,
                                          BeginningQuantity = g.Sum(s => s.BeginningQuantity),
                                          InQuantity = g.Sum(s => s.InQuantity),
                                          OutQuantity = g.Sum(s => s.OutQuantity) * -1,
                                          EndingQuantity = g.Sum(s => (s.BeginningQuantity + s.InQuantity) - s.OutQuantity),
                                          CountQuantity = 0,
                                          Variance = g.Sum(s => (s.BeginningQuantity + s.InQuantity) - s.OutQuantity),
                                          Cost = g.Key.Cost,
                                          Price = g.Key.Price,
                                          Amount = g.Key.Cost * g.Sum(s => (s.BeginningQuantity + s.InQuantity) - s.OutQuantity),
                                      };

                    return inventories.Where(d => d.ItemDescription.ToUpper().Contains(filter.ToUpper()) == true || d.Unit.ToUpper().Contains(filter.ToUpper()) == true).OrderBy(d => d.ItemDescription).ToList();
                }
                else
                {
                    return new List<Entities.RepInventoryReportEntity>();
                }
            }
            else if (category != "ALL" && itemId != 0)
            {
                List<Entities.RepInventoryReportEntity> newRepInventoryReportEntity = new List<Entities.RepInventoryReportEntity>();
                var beginningInInventories = from d in db.TrnStockInLines
                                             where d.TrnStockIn.IsLocked == true
                                             && d.TrnStockIn.StockInDate < startDate.Date
                                             && d.MstItem.Category == category
                                             && d.MstItem.Id == itemId
                                             && d.MstItem.IsInventory == true
                                             && d.MstItem.IsLocked == true
                                             select new Entities.RepInventoryReportEntity
                                             {
                                                 Document = "Beg",
                                                 Id = "Beg-In-" + d.Id,
                                                 InventoryDate = d.TrnStockIn.StockInDate,
                                                 ItemCode = d.MstItem.ItemCode,
                                                 BarCode = d.MstItem.BarCode,
                                                 ItemDescription = d.MstItem.ItemDescription,
                                                 BeginningQuantity = d.Quantity,
                                                 InQuantity = 0,
                                                 OutQuantity = 0,
                                                 EndingQuantity = 0,
                                                 Unit = d.MstUnit.Unit,
                                                 Cost = d.MstItem.Cost,
                                                 Price = d.MstItem.Price,
                                                 Amount = 0
                                             };

                var beginningSoldInventories = from d in db.TrnSalesLines
                                               where d.TrnSale.IsLocked == true
                                               && d.TrnSale.IsCancelled == false
                                               && d.TrnSale.SalesDate < startDate.Date
                                               && d.MstItem.Category == category
                                               && d.MstItem.Id == itemId
                                               && d.MstItem.IsInventory == true
                                               && d.MstItem.IsLocked == true
                                               select new Entities.RepInventoryReportEntity
                                               {
                                                   Document = "Beg",
                                                   Id = "Beg-Sold-" + d.Id,
                                                   InventoryDate = d.TrnSale.SalesDate,
                                                   ItemCode = d.MstItem.ItemCode,
                                                   BarCode = d.MstItem.BarCode,
                                                   ItemDescription = d.MstItem.ItemDescription,
                                                   BeginningQuantity = d.Quantity * -1,
                                                   InQuantity = 0,
                                                   OutQuantity = 0,
                                                   EndingQuantity = 0,
                                                   Unit = d.MstUnit.Unit,
                                                   Cost = d.MstItem.Cost,
                                                   Price = d.MstItem.Price,
                                                   Amount = 0
                                               };

                List<Entities.RepInventoryReportEntity> beginningSoldComponentInventories = new List<Entities.RepInventoryReportEntity>();

                var beginningSoldComponents = from d in db.TrnSalesLines
                                              where d.TrnSale.IsLocked == true
                                              && d.TrnSale.IsCancelled == false
                                              && d.TrnSale.SalesDate < startDate.Date
                                              && d.MstItem.Category == category
                                              && d.MstItem.Id == itemId
                                              && d.MstItem.IsInventory == false
                                              && d.MstItem.MstItemComponents.Any() == true
                                              && d.MstItem.IsLocked == true
                                              select d;

                if (beginningSoldComponents.ToList().Any() == true)
                {
                    foreach (var beginningSoldComponent in beginningSoldComponents.ToList())
                    {
                        var itemComponents = beginningSoldComponent.MstItem.MstItemComponents;
                        if (itemComponents.Any() == true)
                        {
                            foreach (var itemComponent in itemComponents.ToList())
                            {
                                beginningSoldComponentInventories.Add(new Entities.RepInventoryReportEntity()
                                {
                                    Document = "Beg",
                                    Id = "Beg-Sold-Component" + itemComponent.Id,
                                    InventoryDate = beginningSoldComponent.TrnSale.SalesDate,
                                    ItemCode = itemComponent.MstItem1.ItemCode,
                                    BarCode = itemComponent.MstItem1.BarCode,
                                    ItemDescription = itemComponent.MstItem1.ItemDescription,
                                    BeginningQuantity = (itemComponent.Quantity * beginningSoldComponent.Quantity) * -1,
                                    InQuantity = 0,
                                    OutQuantity = 0,
                                    EndingQuantity = 0,
                                    Unit = itemComponent.MstItem1.MstUnit.Unit,
                                    Cost = itemComponent.MstItem1.Cost,
                                    Price = itemComponent.MstItem1.Price,
                                    Amount = 0
                                });
                            }
                        }
                    }
                }

                var beginningOutInventories = from d in db.TrnStockOutLines
                                              where d.TrnStockOut.IsLocked == true
                                              && d.TrnStockOut.StockOutDate < startDate.Date
                                              && d.MstItem.Category == category
                                              && d.MstItem.Id == itemId
                                              && d.MstItem.IsInventory == true
                                              && d.MstItem.IsLocked == true
                                              select new Entities.RepInventoryReportEntity
                                              {
                                                  Document = "Beg",
                                                  Id = "Beg-Out-" + d.Id,
                                                  InventoryDate = d.TrnStockOut.StockOutDate,
                                                  ItemCode = d.MstItem.ItemCode,
                                                  BarCode = d.MstItem.BarCode,
                                                  ItemDescription = d.MstItem.ItemDescription,
                                                  BeginningQuantity = d.Quantity * -1,
                                                  InQuantity = 0,
                                                  OutQuantity = 0,
                                                  EndingQuantity = 0,
                                                  Unit = d.MstUnit.Unit,
                                                  Cost = d.MstItem.Cost,
                                                  Price = d.MstItem.Price,
                                                  Amount = 0
                                              };

                var unionBeginningInventories = beginningInInventories.ToList().Union(beginningSoldInventories.ToList()).Union(beginningSoldComponentInventories.ToList()).Union(beginningOutInventories.ToList());

                var currentInInventories = from d in db.TrnStockInLines
                                           where d.TrnStockIn.IsLocked == true
                                           && d.TrnStockIn.StockInDate >= startDate.Date
                                           && d.TrnStockIn.StockInDate <= endDate.Date
                                           && d.MstItem.Category == category
                                           && d.MstItem.Id == itemId
                                           && d.MstItem.IsInventory == true
                                           && d.MstItem.IsLocked == true
                                           select new Entities.RepInventoryReportEntity
                                           {
                                               Document = "Cur",
                                               Id = "Cur-In-" + d.Id,
                                               InventoryDate = d.TrnStockIn.StockInDate,
                                               ItemCode = d.MstItem.ItemCode,
                                               BarCode = d.MstItem.BarCode,
                                               ItemDescription = d.MstItem.ItemDescription,
                                               BeginningQuantity = 0,
                                               InQuantity = d.Quantity,
                                               OutQuantity = 0,
                                               EndingQuantity = 0,
                                               Unit = d.MstUnit.Unit,
                                               Cost = d.MstItem.Cost,
                                               Price = d.MstItem.Price,
                                               Amount = 0
                                           };

                var currentSoldInventories = from d in db.TrnSalesLines
                                             where d.TrnSale.IsLocked == true
                                             && d.TrnSale.IsCancelled == false
                                             && d.TrnSale.SalesDate >= startDate.Date
                                             && d.TrnSale.SalesDate <= endDate.Date
                                             && d.MstItem.Category == category
                                             && d.MstItem.Id == itemId
                                             && d.MstItem.IsInventory == true
                                             && d.MstItem.IsLocked == true
                                             select new Entities.RepInventoryReportEntity
                                             {
                                                 Document = "Cur",
                                                 Id = "Cur-Sold-" + d.Id,
                                                 InventoryDate = d.TrnSale.SalesDate,
                                                 ItemCode = d.MstItem.ItemCode,
                                                 BarCode = d.MstItem.BarCode,
                                                 ItemDescription = d.MstItem.ItemDescription,
                                                 BeginningQuantity = 0,
                                                 InQuantity = 0,
                                                 OutQuantity = d.Quantity,
                                                 EndingQuantity = 0,
                                                 Unit = d.MstUnit.Unit,
                                                 Cost = d.MstItem.Cost,
                                                 Price = d.MstItem.Price,
                                                 Amount = 0
                                             };

                List<Entities.RepInventoryReportEntity> currentSoldComponentInventories = new List<Entities.RepInventoryReportEntity>();

                var currentSoldComponents = from d in db.TrnSalesLines
                                            where d.TrnSale.IsLocked == true
                                            && d.TrnSale.IsCancelled == false
                                            && d.TrnSale.SalesDate >= startDate.Date
                                            && d.TrnSale.SalesDate <= endDate.Date
                                            && d.MstItem.Category == category
                                            && d.MstItem.Id == itemId
                                            && d.MstItem.IsInventory == false
                                            && d.MstItem.MstItemComponents.Any() == true
                                            && d.MstItem.IsLocked == true
                                            select d;

                if (currentSoldComponents.ToList().Any() == true)
                {
                    foreach (var currentSoldComponent in currentSoldComponents.ToList())
                    {
                        var itemComponents = currentSoldComponent.MstItem.MstItemComponents;
                        if (itemComponents.Any() == true)
                        {
                            foreach (var itemComponent in itemComponents.ToList())
                            {
                                currentSoldComponentInventories.Add(new Entities.RepInventoryReportEntity()
                                {
                                    Document = "Cur",
                                    Id = "Cur-Sold-Component" + itemComponent.Id,
                                    InventoryDate = currentSoldComponent.TrnSale.SalesDate,
                                    ItemCode = itemComponent.MstItem1.ItemCode,
                                    BarCode = itemComponent.MstItem1.BarCode,
                                    ItemDescription = itemComponent.MstItem1.ItemDescription,
                                    BeginningQuantity = 0,
                                    InQuantity = 0,
                                    OutQuantity = itemComponent.Quantity * currentSoldComponent.Quantity,
                                    EndingQuantity = 0,
                                    Unit = itemComponent.MstItem1.MstUnit.Unit,
                                    Cost = itemComponent.MstItem1.Cost,
                                    Price = itemComponent.MstItem1.Price,
                                    Amount = 0
                                });
                            }
                        }
                    }
                }

                var currentOutInventories = from d in db.TrnStockOutLines
                                            where d.TrnStockOut.IsLocked == true
                                            && d.TrnStockOut.StockOutDate >= startDate.Date
                                            && d.TrnStockOut.StockOutDate <= endDate.Date
                                            && d.MstItem.Category == category
                                            && d.MstItem.Id == itemId
                                            && d.MstItem.IsInventory == true
                                            && d.MstItem.IsLocked == true
                                            select new Entities.RepInventoryReportEntity
                                            {
                                                Document = "Cur",
                                                Id = "Cur-Out-" + d.Id,
                                                InventoryDate = d.TrnStockOut.StockOutDate,
                                                ItemCode = d.MstItem.ItemCode,
                                                BarCode = d.MstItem.BarCode,
                                                ItemDescription = d.MstItem.ItemDescription,
                                                BeginningQuantity = 0,
                                                InQuantity = 0,
                                                OutQuantity = d.Quantity,
                                                EndingQuantity = 0,
                                                Unit = d.MstUnit.Unit,
                                                Cost = d.MstItem.Cost,
                                                Price = d.MstItem.Price,
                                                Amount = 0
                                            };

                var unionCurrentInventories = currentInInventories.ToList().Union(currentSoldInventories.ToList()).Union(currentSoldComponentInventories.ToList()).Union(currentOutInventories.ToList());

                var unionInventories = unionBeginningInventories.ToList().Union(unionCurrentInventories.ToList());
                if (unionInventories.Any())
                {
                    var inventories = from d in unionInventories
                                      group d by new
                                      {
                                          d.ItemCode,
                                          d.BarCode,
                                          d.ItemDescription,
                                          d.Unit,
                                          d.Cost,
                                          d.Price
                                      } into g
                                      select new Entities.RepInventoryReportEntity
                                      {
                                          ItemCode = g.Key.ItemCode,
                                          BarCode = g.Key.BarCode,
                                          ItemDescription = g.Key.ItemDescription,
                                          Unit = g.Key.Unit,
                                          BeginningQuantity = g.Sum(s => s.BeginningQuantity),
                                          InQuantity = g.Sum(s => s.InQuantity),
                                          OutQuantity = g.Sum(s => s.OutQuantity) * -1,
                                          EndingQuantity = g.Sum(s => (s.BeginningQuantity + s.InQuantity) - s.OutQuantity),
                                          CountQuantity = 0,
                                          Variance = g.Sum(s => (s.BeginningQuantity + s.InQuantity) - s.OutQuantity),
                                          Cost = g.Key.Cost,
                                          Price = g.Key.Price,
                                          Amount = g.Key.Cost * g.Sum(s => (s.BeginningQuantity + s.InQuantity) - s.OutQuantity),
                                      };

                    return inventories.Where(d => d.ItemDescription.ToUpper().Contains(filter.ToUpper()) == true || d.Unit.ToUpper().Contains(filter.ToUpper()) == true).OrderBy(d => d.ItemDescription).ToList();
                }
                else
                {
                    return new List<Entities.RepInventoryReportEntity>();
                }
            }
            else if (category == "ALL" && itemId != 0)
            {
                List<Entities.RepInventoryReportEntity> newRepInventoryReportEntity = new List<Entities.RepInventoryReportEntity>();
                var beginningInInventories = from d in db.TrnStockInLines
                                             where d.TrnStockIn.IsLocked == true
                                             && d.TrnStockIn.StockInDate < startDate.Date
                                             && d.MstItem.Id == itemId
                                             && d.MstItem.IsInventory == true
                                             && d.MstItem.IsLocked == true
                                             select new Entities.RepInventoryReportEntity
                                             {
                                                 Document = "Beg",
                                                 Id = "Beg-In-" + d.Id,
                                                 InventoryDate = d.TrnStockIn.StockInDate,
                                                 ItemCode = d.MstItem.ItemCode,
                                                 BarCode = d.MstItem.BarCode,
                                                 ItemDescription = d.MstItem.ItemDescription,
                                                 BeginningQuantity = d.Quantity,
                                                 InQuantity = 0,
                                                 OutQuantity = 0,
                                                 EndingQuantity = 0,
                                                 Unit = d.MstUnit.Unit,
                                                 Cost = d.MstItem.Cost,
                                                 Price = d.MstItem.Price,
                                                 Amount = 0
                                             };

                var beginningSoldInventories = from d in db.TrnSalesLines
                                               where d.TrnSale.IsLocked == true
                                               && d.TrnSale.IsCancelled == false
                                               && d.TrnSale.SalesDate < startDate.Date
                                              && d.MstItem.Id == itemId
                                               && d.MstItem.IsInventory == true
                                               && d.MstItem.IsLocked == true
                                               select new Entities.RepInventoryReportEntity
                                               {
                                                   Document = "Beg",
                                                   Id = "Beg-Sold-" + d.Id,
                                                   InventoryDate = d.TrnSale.SalesDate,
                                                   ItemCode = d.MstItem.ItemCode,
                                                   BarCode = d.MstItem.BarCode,
                                                   ItemDescription = d.MstItem.ItemDescription,
                                                   BeginningQuantity = d.Quantity * -1,
                                                   InQuantity = 0,
                                                   OutQuantity = 0,
                                                   EndingQuantity = 0,
                                                   Unit = d.MstUnit.Unit,
                                                   Cost = d.MstItem.Cost,
                                                   Price = d.MstItem.Price,
                                                   Amount = 0
                                               };

                List<Entities.RepInventoryReportEntity> beginningSoldComponentInventories = new List<Entities.RepInventoryReportEntity>();

                var beginningSoldComponents = from d in db.TrnSalesLines
                                              where d.TrnSale.IsLocked == true
                                              && d.TrnSale.IsCancelled == false
                                              && d.TrnSale.SalesDate < startDate.Date
                                              && d.MstItem.Id == itemId
                                              && d.MstItem.IsInventory == false
                                              && d.MstItem.MstItemComponents.Any() == true
                                              && d.MstItem.IsLocked == true
                                              select d;

                if (beginningSoldComponents.ToList().Any() == true)
                {
                    foreach (var beginningSoldComponent in beginningSoldComponents.ToList())
                    {
                        var itemComponents = beginningSoldComponent.MstItem.MstItemComponents;
                        if (itemComponents.Any() == true)
                        {
                            foreach (var itemComponent in itemComponents.ToList())
                            {
                                beginningSoldComponentInventories.Add(new Entities.RepInventoryReportEntity()
                                {
                                    Document = "Beg",
                                    Id = "Beg-Sold-Component" + itemComponent.Id,
                                    InventoryDate = beginningSoldComponent.TrnSale.SalesDate,
                                    ItemCode = itemComponent.MstItem1.ItemCode,
                                    BarCode = itemComponent.MstItem1.BarCode,
                                    ItemDescription = itemComponent.MstItem1.ItemDescription,
                                    BeginningQuantity = (itemComponent.Quantity * beginningSoldComponent.Quantity) * -1,
                                    InQuantity = 0,
                                    OutQuantity = 0,
                                    EndingQuantity = 0,
                                    Unit = itemComponent.MstItem1.MstUnit.Unit,
                                    Cost = itemComponent.MstItem1.Cost,
                                    Price = itemComponent.MstItem1.Price,
                                    Amount = 0
                                });
                            }
                        }
                    }
                }

                var beginningOutInventories = from d in db.TrnStockOutLines
                                              where d.TrnStockOut.IsLocked == true
                                              && d.TrnStockOut.StockOutDate < startDate.Date
                                              && d.MstItem.Id == itemId
                                              && d.MstItem.IsInventory == true
                                              && d.MstItem.IsLocked == true
                                              select new Entities.RepInventoryReportEntity
                                              {
                                                  Document = "Beg",
                                                  Id = "Beg-Out-" + d.Id,
                                                  InventoryDate = d.TrnStockOut.StockOutDate,
                                                  ItemCode = d.MstItem.ItemCode,
                                                  BarCode = d.MstItem.BarCode,
                                                  ItemDescription = d.MstItem.ItemDescription,
                                                  BeginningQuantity = d.Quantity * -1,
                                                  InQuantity = 0,
                                                  OutQuantity = 0,
                                                  EndingQuantity = 0,
                                                  Unit = d.MstUnit.Unit,
                                                  Cost = d.MstItem.Cost,
                                                  Price = d.MstItem.Price,
                                                  Amount = 0
                                              };

                var unionBeginningInventories = beginningInInventories.ToList().Union(beginningSoldInventories.ToList()).Union(beginningSoldComponentInventories.ToList()).Union(beginningOutInventories.ToList());

                var currentInInventories = from d in db.TrnStockInLines
                                           where d.TrnStockIn.IsLocked == true
                                           && d.TrnStockIn.StockInDate >= startDate.Date
                                           && d.TrnStockIn.StockInDate <= endDate.Date
                                           && d.MstItem.Id == itemId
                                           && d.MstItem.IsInventory == true
                                           && d.MstItem.IsLocked == true
                                           select new Entities.RepInventoryReportEntity
                                           {
                                               Document = "Cur",
                                               Id = "Cur-In-" + d.Id,
                                               InventoryDate = d.TrnStockIn.StockInDate,
                                               ItemCode = d.MstItem.ItemCode,
                                               BarCode = d.MstItem.BarCode,
                                               ItemDescription = d.MstItem.ItemDescription,
                                               BeginningQuantity = 0,
                                               InQuantity = d.Quantity,
                                               OutQuantity = 0,
                                               EndingQuantity = 0,
                                               Unit = d.MstUnit.Unit,
                                               Cost = d.MstItem.Cost,
                                               Price = d.MstItem.Price,
                                               Amount = 0
                                           };

                var currentSoldInventories = from d in db.TrnSalesLines
                                             where d.TrnSale.IsLocked == true
                                             && d.TrnSale.IsCancelled == false
                                             && d.TrnSale.SalesDate >= startDate.Date
                                             && d.TrnSale.SalesDate <= endDate.Date
                                             && d.MstItem.Id == itemId
                                             && d.MstItem.IsInventory == true
                                             && d.MstItem.IsLocked == true
                                             select new Entities.RepInventoryReportEntity
                                             {
                                                 Document = "Cur",
                                                 Id = "Cur-Sold-" + d.Id,
                                                 InventoryDate = d.TrnSale.SalesDate,
                                                 ItemCode = d.MstItem.ItemCode,
                                                 BarCode = d.MstItem.BarCode,
                                                 ItemDescription = d.MstItem.ItemDescription,
                                                 BeginningQuantity = 0,
                                                 InQuantity = 0,
                                                 OutQuantity = d.Quantity,
                                                 EndingQuantity = 0,
                                                 Unit = d.MstUnit.Unit,
                                                 Cost = d.MstItem.Cost,
                                                 Price = d.MstItem.Price,
                                                 Amount = 0
                                             };

                List<Entities.RepInventoryReportEntity> currentSoldComponentInventories = new List<Entities.RepInventoryReportEntity>();

                var currentSoldComponents = from d in db.TrnSalesLines
                                            where d.TrnSale.IsLocked == true
                                            && d.TrnSale.IsCancelled == false
                                            && d.TrnSale.SalesDate >= startDate.Date
                                            && d.TrnSale.SalesDate <= endDate.Date
                                            && d.MstItem.Id == itemId
                                            && d.MstItem.IsInventory == false
                                            && d.MstItem.MstItemComponents.Any() == true
                                            && d.MstItem.IsLocked == true
                                            select d;

                if (currentSoldComponents.ToList().Any() == true)
                {
                    foreach (var currentSoldComponent in currentSoldComponents.ToList())
                    {
                        var itemComponents = currentSoldComponent.MstItem.MstItemComponents;
                        if (itemComponents.Any() == true)
                        {
                            foreach (var itemComponent in itemComponents.ToList())
                            {
                                currentSoldComponentInventories.Add(new Entities.RepInventoryReportEntity()
                                {
                                    Document = "Cur",
                                    Id = "Cur-Sold-Component" + itemComponent.Id,
                                    InventoryDate = currentSoldComponent.TrnSale.SalesDate,
                                    ItemCode = itemComponent.MstItem1.ItemCode,
                                    BarCode = itemComponent.MstItem1.BarCode,
                                    ItemDescription = itemComponent.MstItem1.ItemDescription,
                                    BeginningQuantity = 0,
                                    InQuantity = 0,
                                    OutQuantity = itemComponent.Quantity * currentSoldComponent.Quantity,
                                    EndingQuantity = 0,
                                    Unit = itemComponent.MstItem1.MstUnit.Unit,
                                    Cost = itemComponent.MstItem1.Cost,
                                    Price = itemComponent.MstItem1.Price,
                                    Amount = 0
                                });
                            }
                        }
                    }
                }

                var currentOutInventories = from d in db.TrnStockOutLines
                                            where d.TrnStockOut.IsLocked == true
                                            && d.TrnStockOut.StockOutDate >= startDate.Date
                                            && d.TrnStockOut.StockOutDate <= endDate.Date
                                            && d.MstItem.Id == itemId
                                            && d.MstItem.IsInventory == true
                                            && d.MstItem.IsLocked == true
                                            select new Entities.RepInventoryReportEntity
                                            {
                                                Document = "Cur",
                                                Id = "Cur-Out-" + d.Id,
                                                InventoryDate = d.TrnStockOut.StockOutDate,
                                                ItemCode = d.MstItem.ItemCode,
                                                BarCode = d.MstItem.BarCode,
                                                ItemDescription = d.MstItem.ItemDescription,
                                                BeginningQuantity = 0,
                                                InQuantity = 0,
                                                OutQuantity = d.Quantity,
                                                EndingQuantity = 0,
                                                Unit = d.MstUnit.Unit,
                                                Cost = d.MstItem.Cost,
                                                Price = d.MstItem.Price,
                                                Amount = 0
                                            };

                var unionCurrentInventories = currentInInventories.ToList().Union(currentSoldInventories.ToList()).Union(currentSoldComponentInventories.ToList()).Union(currentOutInventories.ToList());

                var unionInventories = unionBeginningInventories.ToList().Union(unionCurrentInventories.ToList());
                if (unionInventories.Any())
                {
                    var inventories = from d in unionInventories
                                      group d by new
                                      {
                                          d.ItemCode,
                                          d.BarCode,
                                          d.ItemDescription,
                                          d.Unit,
                                          d.Cost,
                                          d.Price
                                      } into g
                                      select new Entities.RepInventoryReportEntity
                                      {
                                          ItemCode = g.Key.ItemCode,
                                          BarCode = g.Key.BarCode,
                                          ItemDescription = g.Key.ItemDescription,
                                          Unit = g.Key.Unit,
                                          BeginningQuantity = g.Sum(s => s.BeginningQuantity),
                                          InQuantity = g.Sum(s => s.InQuantity),
                                          OutQuantity = g.Sum(s => s.OutQuantity) * -1,
                                          EndingQuantity = g.Sum(s => (s.BeginningQuantity + s.InQuantity) - s.OutQuantity),
                                          CountQuantity = 0,
                                          Variance = g.Sum(s => (s.BeginningQuantity + s.InQuantity) - s.OutQuantity),
                                          Cost = g.Key.Cost,
                                          Price = g.Key.Price,
                                          Amount = g.Key.Cost * g.Sum(s => (s.BeginningQuantity + s.InQuantity) - s.OutQuantity),
                                      };

                    return inventories.Where(d => d.ItemDescription.ToUpper().Contains(filter.ToUpper()) == true || d.Unit.ToUpper().Contains(filter.ToUpper()) == true).OrderBy(d => d.ItemDescription).ToList();
                }
                else
                {
                    return new List<Entities.RepInventoryReportEntity>();
                }
            }
            else if (category != "ALL" && itemId == 0)
            {
                List<Entities.RepInventoryReportEntity> newRepInventoryReportEntity = new List<Entities.RepInventoryReportEntity>();
                var beginningInInventories = from d in db.TrnStockInLines
                                             where d.TrnStockIn.IsLocked == true
                                             && d.TrnStockIn.StockInDate < startDate.Date
                                             && d.MstItem.Category == category
                                             && d.MstItem.IsInventory == true
                                             && d.MstItem.IsLocked == true
                                             select new Entities.RepInventoryReportEntity
                                             {
                                                 Document = "Beg",
                                                 Id = "Beg-In-" + d.Id,
                                                 InventoryDate = d.TrnStockIn.StockInDate,
                                                 ItemCode = d.MstItem.ItemCode,
                                                 BarCode = d.MstItem.BarCode,
                                                 ItemDescription = d.MstItem.ItemDescription,
                                                 BeginningQuantity = d.Quantity,
                                                 InQuantity = 0,
                                                 OutQuantity = 0,
                                                 EndingQuantity = 0,
                                                 Unit = d.MstUnit.Unit,
                                                 Cost = d.MstItem.Cost,
                                                 Price = d.MstItem.Price,
                                                 Amount = 0
                                             };

                var beginningSoldInventories = from d in db.TrnSalesLines
                                               where d.TrnSale.IsLocked == true
                                               && d.TrnSale.IsCancelled == false
                                               && d.TrnSale.SalesDate < startDate.Date
                                               && d.MstItem.Category == category
                                               && d.MstItem.IsInventory == true
                                               && d.MstItem.IsLocked == true
                                               select new Entities.RepInventoryReportEntity
                                               {
                                                   Document = "Beg",
                                                   Id = "Beg-Sold-" + d.Id,
                                                   InventoryDate = d.TrnSale.SalesDate,
                                                   ItemCode = d.MstItem.ItemCode,
                                                   BarCode = d.MstItem.BarCode,
                                                   ItemDescription = d.MstItem.ItemDescription,
                                                   BeginningQuantity = d.Quantity * -1,
                                                   InQuantity = 0,
                                                   OutQuantity = 0,
                                                   EndingQuantity = 0,
                                                   Unit = d.MstUnit.Unit,
                                                   Cost = d.MstItem.Cost,
                                                   Price = d.MstItem.Price,
                                                   Amount = 0
                                               };

                List<Entities.RepInventoryReportEntity> beginningSoldComponentInventories = new List<Entities.RepInventoryReportEntity>();

                var beginningSoldComponents = from d in db.TrnSalesLines
                                              where d.TrnSale.IsLocked == true
                                              && d.TrnSale.IsCancelled == false
                                              && d.TrnSale.SalesDate < startDate.Date
                                              && d.MstItem.Category == category
                                              && d.MstItem.IsInventory == false
                                              && d.MstItem.MstItemComponents.Any() == true
                                              && d.MstItem.IsLocked == true
                                              select d;

                if (beginningSoldComponents.ToList().Any() == true)
                {
                    foreach (var beginningSoldComponent in beginningSoldComponents.ToList())
                    {
                        var itemComponents = beginningSoldComponent.MstItem.MstItemComponents;
                        if (itemComponents.Any() == true)
                        {
                            foreach (var itemComponent in itemComponents.ToList())
                            {
                                beginningSoldComponentInventories.Add(new Entities.RepInventoryReportEntity()
                                {
                                    Document = "Beg",
                                    Id = "Beg-Sold-Component" + itemComponent.Id,
                                    InventoryDate = beginningSoldComponent.TrnSale.SalesDate,
                                    ItemCode = itemComponent.MstItem1.ItemCode,
                                    BarCode = itemComponent.MstItem1.BarCode,
                                    ItemDescription = itemComponent.MstItem1.ItemDescription,
                                    BeginningQuantity = (itemComponent.Quantity * beginningSoldComponent.Quantity) * -1,
                                    InQuantity = 0,
                                    OutQuantity = 0,
                                    EndingQuantity = 0,
                                    Unit = itemComponent.MstItem1.MstUnit.Unit,
                                    Cost = itemComponent.MstItem1.Cost,
                                    Price = itemComponent.MstItem1.Price,
                                    Amount = 0
                                });
                            }
                        }
                    }
                }

                var beginningOutInventories = from d in db.TrnStockOutLines
                                              where d.TrnStockOut.IsLocked == true
                                              && d.TrnStockOut.StockOutDate < startDate.Date
                                              && d.MstItem.Category == category
                                              && d.MstItem.IsInventory == true
                                              && d.MstItem.IsLocked == true
                                              select new Entities.RepInventoryReportEntity
                                              {
                                                  Document = "Beg",
                                                  Id = "Beg-Out-" + d.Id,
                                                  InventoryDate = d.TrnStockOut.StockOutDate,
                                                  ItemCode = d.MstItem.ItemCode,
                                                  BarCode = d.MstItem.BarCode,
                                                  ItemDescription = d.MstItem.ItemDescription,
                                                  BeginningQuantity = d.Quantity * -1,
                                                  InQuantity = 0,
                                                  OutQuantity = 0,
                                                  EndingQuantity = 0,
                                                  Unit = d.MstUnit.Unit,
                                                  Cost = d.MstItem.Cost,
                                                  Price = d.MstItem.Price,
                                                  Amount = 0
                                              };

                var unionBeginningInventories = beginningInInventories.ToList().Union(beginningSoldInventories.ToList()).Union(beginningSoldComponentInventories.ToList()).Union(beginningOutInventories.ToList());

                var currentInInventories = from d in db.TrnStockInLines
                                           where d.TrnStockIn.IsLocked == true
                                           && d.TrnStockIn.StockInDate >= startDate.Date
                                           && d.TrnStockIn.StockInDate <= endDate.Date
                                           && d.MstItem.Category == category
                                           && d.MstItem.IsInventory == true
                                           && d.MstItem.IsLocked == true
                                           select new Entities.RepInventoryReportEntity
                                           {
                                               Document = "Cur",
                                               Id = "Cur-In-" + d.Id,
                                               InventoryDate = d.TrnStockIn.StockInDate,
                                               ItemCode = d.MstItem.ItemCode,
                                               BarCode = d.MstItem.BarCode,
                                               ItemDescription = d.MstItem.ItemDescription,
                                               BeginningQuantity = 0,
                                               InQuantity = d.Quantity,
                                               OutQuantity = 0,
                                               EndingQuantity = 0,
                                               Unit = d.MstUnit.Unit,
                                               Cost = d.MstItem.Cost,
                                               Price = d.MstItem.Price,
                                               Amount = 0
                                           };

                var currentSoldInventories = from d in db.TrnSalesLines
                                             where d.TrnSale.IsLocked == true
                                             && d.TrnSale.IsCancelled == false
                                             && d.TrnSale.SalesDate >= startDate.Date
                                             && d.TrnSale.SalesDate <= endDate.Date
                                             && d.MstItem.Category == category
                                             && d.MstItem.IsInventory == true
                                             && d.MstItem.IsLocked == true
                                             select new Entities.RepInventoryReportEntity
                                             {
                                                 Document = "Cur",
                                                 Id = "Cur-Sold-" + d.Id,
                                                 InventoryDate = d.TrnSale.SalesDate,
                                                 ItemCode = d.MstItem.ItemCode,
                                                 BarCode = d.MstItem.BarCode,
                                                 ItemDescription = d.MstItem.ItemDescription,
                                                 BeginningQuantity = 0,
                                                 InQuantity = 0,
                                                 OutQuantity = d.Quantity,
                                                 EndingQuantity = 0,
                                                 Unit = d.MstUnit.Unit,
                                                 Cost = d.MstItem.Cost,
                                                 Price = d.MstItem.Price,
                                                 Amount = 0
                                             };

                List<Entities.RepInventoryReportEntity> currentSoldComponentInventories = new List<Entities.RepInventoryReportEntity>();

                var currentSoldComponents = from d in db.TrnSalesLines
                                            where d.TrnSale.IsLocked == true
                                            && d.TrnSale.IsCancelled == false
                                            && d.TrnSale.SalesDate >= startDate.Date
                                            && d.TrnSale.SalesDate <= endDate.Date
                                            && d.MstItem.Category == category
                                            && d.MstItem.IsInventory == false
                                            && d.MstItem.MstItemComponents.Any() == true
                                            && d.MstItem.IsLocked == true
                                            select d;

                if (currentSoldComponents.ToList().Any() == true)
                {
                    foreach (var currentSoldComponent in currentSoldComponents.ToList())
                    {
                        var itemComponents = currentSoldComponent.MstItem.MstItemComponents;
                        if (itemComponents.Any() == true)
                        {
                            foreach (var itemComponent in itemComponents.ToList())
                            {
                                currentSoldComponentInventories.Add(new Entities.RepInventoryReportEntity()
                                {
                                    Document = "Cur",
                                    Id = "Cur-Sold-Component" + itemComponent.Id,
                                    InventoryDate = currentSoldComponent.TrnSale.SalesDate,
                                    ItemCode = itemComponent.MstItem1.ItemCode,
                                    BarCode = itemComponent.MstItem1.BarCode,
                                    ItemDescription = itemComponent.MstItem1.ItemDescription,
                                    BeginningQuantity = 0,
                                    InQuantity = 0,
                                    OutQuantity = itemComponent.Quantity * currentSoldComponent.Quantity,
                                    EndingQuantity = 0,
                                    Unit = itemComponent.MstItem1.MstUnit.Unit,
                                    Cost = itemComponent.MstItem1.Cost,
                                    Price = itemComponent.MstItem1.Price,
                                    Amount = 0
                                });
                            }
                        }
                    }
                }

                var currentOutInventories = from d in db.TrnStockOutLines
                                            where d.TrnStockOut.IsLocked == true
                                            && d.TrnStockOut.StockOutDate >= startDate.Date
                                            && d.TrnStockOut.StockOutDate <= endDate.Date
                                            && d.MstItem.Category == category
                                            && d.MstItem.IsInventory == true
                                            && d.MstItem.IsLocked == true
                                            select new Entities.RepInventoryReportEntity
                                            {
                                                Document = "Cur",
                                                Id = "Cur-Out-" + d.Id,
                                                InventoryDate = d.TrnStockOut.StockOutDate,
                                                ItemCode = d.MstItem.ItemCode,
                                                BarCode = d.MstItem.BarCode,
                                                ItemDescription = d.MstItem.ItemDescription,
                                                BeginningQuantity = 0,
                                                InQuantity = 0,
                                                OutQuantity = d.Quantity,
                                                EndingQuantity = 0,
                                                Unit = d.MstUnit.Unit,
                                                Cost = d.MstItem.Cost,
                                                Price = d.MstItem.Price,
                                                Amount = 0
                                            };

                var unionCurrentInventories = currentInInventories.ToList().Union(currentSoldInventories.ToList()).Union(currentSoldComponentInventories.ToList()).Union(currentOutInventories.ToList());

                var unionInventories = unionBeginningInventories.ToList().Union(unionCurrentInventories.ToList());
                if (unionInventories.Any())
                {
                    var inventories = from d in unionInventories
                                      group d by new
                                      {
                                          d.ItemCode,
                                          d.BarCode,
                                          d.ItemDescription,
                                          d.Unit,
                                          d.Cost,
                                          d.Price
                                      } into g
                                      select new Entities.RepInventoryReportEntity
                                      {
                                          ItemCode = g.Key.ItemCode,
                                          BarCode = g.Key.BarCode,
                                          ItemDescription = g.Key.ItemDescription,
                                          Unit = g.Key.Unit,
                                          BeginningQuantity = g.Sum(s => s.BeginningQuantity),
                                          InQuantity = g.Sum(s => s.InQuantity),
                                          OutQuantity = g.Sum(s => s.OutQuantity) * -1,
                                          EndingQuantity = g.Sum(s => (s.BeginningQuantity + s.InQuantity) - s.OutQuantity),
                                          CountQuantity = 0,
                                          Variance = g.Sum(s => (s.BeginningQuantity + s.InQuantity) - s.OutQuantity),
                                          Cost = g.Key.Cost,
                                          Price =g.Key.Price,
                                          Amount = g.Key.Cost * g.Sum(s => (s.BeginningQuantity + s.InQuantity) - s.OutQuantity),
                                      };

                    return inventories.Where(d => d.ItemDescription.ToUpper().Contains(filter.ToUpper()) == true || d.Unit.ToUpper().Contains(filter.ToUpper()) == true).OrderBy(d => d.ItemDescription).ToList();
                }
                else
                {
                    return new List<Entities.RepInventoryReportEntity>();
                }
            }
            else
            {
                return new List<Entities.RepInventoryReportEntity>();
            }
        }

        // ==========
        // Stock Card
        // ==========
        public List<Entities.RepInventoryReportStockCardEntity> StockCardReport(DateTime startDate, DateTime endDate, Int32 itemId, String filter/*, String Category*/)
        {
            if (itemId==0 /*&& Category == "ALL"*/)
            {
                List<Entities.RepInventoryReportStockCardEntity> newRepInventoryReportStockCardEntity = new List<Entities.RepInventoryReportStockCardEntity>();
                var beginningInInventories = from d in db.TrnStockInLines
                                             where d.TrnStockIn.IsLocked == true
                                             && d.TrnStockIn.StockInDate < startDate.Date
                                             && d.MstItem.IsInventory == true
                                             && d.MstItem.IsLocked == true
                                             select new Entities.RepInventoryReportStockCardEntity
                                             {
                                                 Document = "Beginning Balance",
                                                 InventoryDate = startDate.Date,
                                                 BeginningQuantity = d.Quantity,
                                                 InQuantity = 0,
                                                 OutQuantity = 0,
                                                 EndingQuantity = 0,
                                                 RunningQuantity = 0,
                                             };

                var beginningSoldInventories = from d in db.TrnSalesLines
                                               where d.TrnSale.IsLocked == true
                                               && d.TrnSale.IsCancelled == false
                                               && d.TrnSale.SalesDate < startDate.Date
                                               && d.MstItem.IsInventory == true
                                               && d.MstItem.IsLocked == true
                                               select new Entities.RepInventoryReportStockCardEntity
                                               {
                                                   Document = "Beginning Balance",
                                                   InventoryDate = startDate.Date,
                                                   BeginningQuantity = d.Quantity * -1,
                                                   InQuantity = 0,
                                                   OutQuantity = 0,
                                                   EndingQuantity = 0,
                                                   RunningQuantity = 0,
                                               };

                List<Entities.RepInventoryReportStockCardEntity> beginningSoldComponentInventories = new List<Entities.RepInventoryReportStockCardEntity>();

                var beginningSoldComponents = from d in db.TrnSalesLines
                                              where d.TrnSale.IsLocked == true
                                              && d.TrnSale.IsCancelled == false
                                              && d.TrnSale.SalesDate < startDate.Date
                                              && d.MstItem.IsInventory == false
                                              && d.MstItem.MstItemComponents.Any() == true
                                              && d.MstItem.IsLocked == true
                                              select d;

                if (beginningSoldComponents.ToList().Any() == true)
                {
                    foreach (var beginningSoldComponent in beginningSoldComponents.ToList())
                    {
                        var itemComponents = from d in beginningSoldComponent.MstItem.MstItemComponents.ToList()
                                             where d.ComponentItemId == itemId
                                             select d;

                        if (itemComponents.Any() == true)
                        {
                            foreach (var itemComponent in itemComponents.ToList())
                            {
                                beginningSoldComponentInventories.Add(new Entities.RepInventoryReportStockCardEntity()
                                {
                                    Document = "Beginning Balance",
                                    InventoryDate = startDate.Date,
                                    BeginningQuantity = (itemComponent.Quantity * beginningSoldComponent.Quantity) * -1,
                                    InQuantity = 0,
                                    OutQuantity = 0,
                                    EndingQuantity = 0,
                                    RunningQuantity = 0,
                                });
                            }
                        }
                    }
                }

                var beginningOutInventories = from d in db.TrnStockOutLines
                                              where d.TrnStockOut.IsLocked == true
                                              && d.TrnStockOut.StockOutDate < startDate.Date
                                              && d.MstItem.IsInventory == true
                                              && d.MstItem.IsLocked == true
                                              select new Entities.RepInventoryReportStockCardEntity
                                              {
                                                  Document = "Beginning Balance",
                                                  InventoryDate = startDate.Date,
                                                  BeginningQuantity = d.Quantity * -1,
                                                  InQuantity = 0,
                                                  OutQuantity = 0,
                                                  EndingQuantity = 0,
                                                  RunningQuantity = 0,
                                              };

                var unionBeginningInventories = beginningInInventories.ToList().Union(beginningSoldInventories.ToList()).Union(beginningSoldComponentInventories.ToList()).Union(beginningOutInventories.ToList());

                var currentInInventories = from d in db.TrnStockInLines
                                           where d.TrnStockIn.IsLocked == true
                                           && d.TrnStockIn.StockInDate >= startDate.Date
                                           && d.TrnStockIn.StockInDate <= endDate.Date
                                           && d.MstItem.IsInventory == true
                                           && d.MstItem.IsLocked == true
                                           select new Entities.RepInventoryReportStockCardEntity
                                           {
                                               Document = "IN-" + d.TrnStockIn.StockInNumber,
                                               InventoryDate = d.TrnStockIn.StockInDate,
                                               BeginningQuantity = 0,
                                               InQuantity = d.Quantity,
                                               OutQuantity = 0,
                                               EndingQuantity = d.Quantity,
                                               RunningQuantity = 0,
                                           };

                var currentSoldInventories = from d in db.TrnSalesLines
                                             where d.TrnSale.IsLocked == true
                                             && d.TrnSale.IsCancelled == false
                                             && d.TrnSale.SalesDate >= startDate.Date
                                             && d.TrnSale.SalesDate <= endDate.Date
                                             && d.MstItem.IsInventory == true
                                             && d.MstItem.IsLocked == true
                                             select new Entities.RepInventoryReportStockCardEntity
                                             {
                                                 Document = "SOLD-" + d.TrnSale.SalesNumber,
                                                 InventoryDate = d.TrnSale.SalesDate,
                                                 BeginningQuantity = 0,
                                                 InQuantity = 0,
                                                 OutQuantity = d.Quantity,
                                                 EndingQuantity = d.Quantity * -1,
                                                 RunningQuantity = 0,
                                             };

                List<Entities.RepInventoryReportStockCardEntity> currentSoldComponentInventories = new List<Entities.RepInventoryReportStockCardEntity>();

                var currentSoldComponents = from d in db.TrnSalesLines
                                            where d.TrnSale.IsLocked == true
                                            && d.TrnSale.IsCancelled == false
                                            && d.TrnSale.SalesDate >= startDate.Date
                                            && d.TrnSale.SalesDate <= endDate.Date
                                            && d.MstItem.IsInventory == false
                                            && d.MstItem.MstItemComponents.Any() == true
                                            && d.MstItem.IsLocked == true
                                            select d;

                if (currentSoldComponents.ToList().Any() == true)
                {
                    foreach (var currentSoldComponent in currentSoldComponents.ToList())
                    {
                        var itemComponents = from d in currentSoldComponent.MstItem.MstItemComponents.ToList()
                                             where d.ComponentItemId == itemId
                                             select d;

                        if (itemComponents.Any() == true)
                        {
                            foreach (var itemComponent in itemComponents.ToList())
                            {
                                currentSoldComponentInventories.Add(new Entities.RepInventoryReportStockCardEntity()
                                {
                                    Document = "SOLD-" + currentSoldComponent.TrnSale.SalesNumber,
                                    InventoryDate = currentSoldComponent.TrnSale.SalesDate,
                                    BeginningQuantity = 0,
                                    InQuantity = 0,
                                    OutQuantity = itemComponent.Quantity * currentSoldComponent.Quantity,
                                    EndingQuantity = (itemComponent.Quantity * currentSoldComponent.Quantity) * -1,
                                    RunningQuantity = 0
                                });
                            }
                        }
                    }
                }

                var currentOutInventories = from d in db.TrnStockOutLines
                                            where d.TrnStockOut.IsLocked == true
                                            && d.TrnStockOut.StockOutDate >= startDate.Date
                                            && d.TrnStockOut.StockOutDate <= endDate.Date
                                            && d.MstItem.IsInventory == true
                                            && d.MstItem.IsLocked == true
                                            select new Entities.RepInventoryReportStockCardEntity
                                            {
                                                Document = "OUT-" + d.TrnStockOut.StockOutNumber,
                                                InventoryDate = d.TrnStockOut.StockOutDate,
                                                BeginningQuantity = 0,
                                                InQuantity = 0,
                                                OutQuantity = d.Quantity,
                                                EndingQuantity = d.Quantity * -1,
                                                RunningQuantity = 0,
                                            };

                var unionCurrentInventories = currentInInventories.ToList().Union(currentSoldInventories.ToList()).Union(currentSoldComponentInventories.ToList()).Union(currentOutInventories.ToList());

                if (unionBeginningInventories.ToList().Any())
                {
                    var groupBeginningInventories = from d in unionBeginningInventories.ToList()
                                                    group d by new
                                                    {
                                                        d.Document,
                                                        d.InventoryDate
                                                    } into g
                                                    select new Entities.RepInventoryReportStockCardEntity
                                                    {
                                                        Document = g.Key.Document,
                                                        InventoryDate = g.Key.InventoryDate,
                                                        BeginningQuantity = g.Sum(s => s.BeginningQuantity),
                                                        InQuantity = g.Sum(s => s.InQuantity),
                                                        OutQuantity = g.Sum(s => s.OutQuantity),
                                                        EndingQuantity = g.Sum(s => s.BeginningQuantity),
                                                        RunningQuantity = g.Sum(s => s.RunningQuantity)
                                                    };
                    var unionInventories = groupBeginningInventories.ToList().Union(unionCurrentInventories.ToList());
                    if (unionInventories.Any())
                    {
                        var listUnionInventories = unionInventories.Where(d => d.Document.ToUpper().Contains(filter.ToUpper()) == true).OrderBy(d => d.InventoryDate).ToList();

                        List<Entities.RepInventoryReportStockCardEntity> runningBalanceInventories = new List<Entities.RepInventoryReportStockCardEntity>();
                        if (listUnionInventories.Any())
                        {
                            Int32 countLoop = 0;
                            Decimal runningQuantity = 0;

                            foreach (var unionInventory in listUnionInventories)
                            {
                                if (countLoop == 0)
                                {
                                    countLoop += 1;
                                    runningQuantity = unionInventory.EndingQuantity;
                                }
                                else
                                {
                                    runningQuantity += unionInventory.EndingQuantity;
                                }

                                runningBalanceInventories.Add(new Entities.RepInventoryReportStockCardEntity()
                                {
                                    Document = unionInventory.Document,
                                    InventoryDate = unionInventory.InventoryDate,
                                    BeginningQuantity = unionInventory.BeginningQuantity,
                                    InQuantity = unionInventory.InQuantity,
                                    OutQuantity = unionInventory.OutQuantity,
                                    EndingQuantity = unionInventory.EndingQuantity,
                                    RunningQuantity = runningQuantity
                                });
                            }
                        }

                        return runningBalanceInventories;
                    }
                    else
                    {
                        return new List<Entities.RepInventoryReportStockCardEntity>();
                    }
                }
                else
                {
                    var unionInventories = unionBeginningInventories.ToList().Union(unionCurrentInventories.ToList());
                    if (unionInventories.Any())
                    {
                        var listUnionInventories = unionInventories.Where(d => d.Document.ToUpper().Contains(filter.ToUpper()) == true).OrderBy(d => d.InventoryDate).ToList();

                        List<Entities.RepInventoryReportStockCardEntity> runningBalanceInventories = new List<Entities.RepInventoryReportStockCardEntity>();
                        if (listUnionInventories.Any())
                        {
                            Int32 countLoop = 0;
                            Decimal runningQuantity = 0;

                            foreach (var unionInventory in listUnionInventories)
                            {
                                if (countLoop == 0)
                                {
                                    countLoop += 1;
                                    runningQuantity = unionInventory.EndingQuantity;
                                }
                                else
                                {
                                    runningQuantity += unionInventory.EndingQuantity;
                                }

                                runningBalanceInventories.Add(new Entities.RepInventoryReportStockCardEntity()
                                {
                                    Document = unionInventory.Document,
                                    InventoryDate = unionInventory.InventoryDate,
                                    BeginningQuantity = unionInventory.BeginningQuantity,
                                    InQuantity = unionInventory.InQuantity,
                                    OutQuantity = unionInventory.OutQuantity,
                                    EndingQuantity = unionInventory.EndingQuantity,
                                    RunningQuantity = runningQuantity
                                });
                            }
                        }

                        return runningBalanceInventories;
                    }
                    else
                    {
                        return new List<Entities.RepInventoryReportStockCardEntity>();
                    }
                }
            }
            else if(itemId != 0/* && Category != "ALL"*/)
            {
                List<Entities.RepInventoryReportStockCardEntity> newRepInventoryReportStockCardEntity = new List<Entities.RepInventoryReportStockCardEntity>();
                var beginningInInventories = from d in db.TrnStockInLines
                                             where d.TrnStockIn.IsLocked == true
                                             && d.TrnStockIn.StockInDate < startDate.Date
                                             && d.ItemId == itemId
                                             //&& d.MstItem.Category == Category
                                             && d.MstItem.IsInventory == true
                                             && d.MstItem.IsLocked == true
                                             select new Entities.RepInventoryReportStockCardEntity
                                             {
                                                 Document = "Beginning Balance",
                                                 InventoryDate = startDate.Date,
                                                 BeginningQuantity = d.Quantity,
                                                 InQuantity = 0,
                                                 OutQuantity = 0,
                                                 EndingQuantity = 0,
                                                 RunningQuantity = 0,
                                             };

                var beginningSoldInventories = from d in db.TrnSalesLines
                                               where d.TrnSale.IsLocked == true
                                               && d.TrnSale.IsCancelled == false
                                               && d.TrnSale.SalesDate < startDate.Date
                                               && d.ItemId == itemId
                                               //&& d.MstItem.Category == Category
                                               && d.MstItem.IsInventory == true
                                               && d.MstItem.IsLocked == true
                                               select new Entities.RepInventoryReportStockCardEntity
                                               {
                                                   Document = "Beginning Balance",
                                                   InventoryDate = startDate.Date,
                                                   BeginningQuantity = d.Quantity * -1,
                                                   InQuantity = 0,
                                                   OutQuantity = 0,
                                                   EndingQuantity = 0,
                                                   RunningQuantity = 0,
                                               };

                List<Entities.RepInventoryReportStockCardEntity> beginningSoldComponentInventories = new List<Entities.RepInventoryReportStockCardEntity>();

                var beginningSoldComponents = from d in db.TrnSalesLines
                                              where d.TrnSale.IsLocked == true
                                              && d.TrnSale.IsCancelled == false
                                              && d.TrnSale.SalesDate < startDate.Date
                                              && d.MstItem.IsInventory == false
                                              && d.MstItem.MstItemComponents.Any() == true
                                              && d.MstItem.IsLocked == true
                                              select d;

                if (beginningSoldComponents.ToList().Any() == true)
                {
                    foreach (var beginningSoldComponent in beginningSoldComponents.ToList())
                    {
                        var itemComponents = from d in beginningSoldComponent.MstItem.MstItemComponents.ToList()
                                             where d.ComponentItemId == itemId
                                             select d;

                        if (itemComponents.Any() == true)
                        {
                            foreach (var itemComponent in itemComponents.ToList())
                            {
                                beginningSoldComponentInventories.Add(new Entities.RepInventoryReportStockCardEntity()
                                {
                                    Document = "Beginning Balance",
                                    InventoryDate = startDate.Date,
                                    BeginningQuantity = (itemComponent.Quantity * beginningSoldComponent.Quantity) * -1,
                                    InQuantity = 0,
                                    OutQuantity = 0,
                                    EndingQuantity = 0,
                                    RunningQuantity = 0,
                                });
                            }
                        }
                    }
                }

                var beginningOutInventories = from d in db.TrnStockOutLines
                                              where d.TrnStockOut.IsLocked == true
                                              && d.TrnStockOut.StockOutDate < startDate.Date
                                              && d.ItemId == itemId
                                              //&& d.MstItem.Category == Category
                                              && d.MstItem.IsInventory == true
                                              && d.MstItem.IsLocked == true
                                              select new Entities.RepInventoryReportStockCardEntity
                                              {
                                                  Document = "Beginning Balance",
                                                  InventoryDate = startDate.Date,
                                                  BeginningQuantity = d.Quantity * -1,
                                                  InQuantity = 0,
                                                  OutQuantity = 0,
                                                  EndingQuantity = 0,
                                                  RunningQuantity = 0,
                                              };

                var unionBeginningInventories = beginningInInventories.ToList().Union(beginningSoldInventories.ToList()).Union(beginningSoldComponentInventories.ToList()).Union(beginningOutInventories.ToList());

                var currentInInventories = from d in db.TrnStockInLines
                                           where d.TrnStockIn.IsLocked == true
                                           && d.TrnStockIn.StockInDate >= startDate.Date
                                           && d.TrnStockIn.StockInDate <= endDate.Date
                                           && d.ItemId == itemId
                                           //&& d.MstItem.Category == Category
                                           && d.MstItem.IsInventory == true
                                           && d.MstItem.IsLocked == true
                                           select new Entities.RepInventoryReportStockCardEntity
                                           {
                                               Document = "IN-" + d.TrnStockIn.StockInNumber,
                                               InventoryDate = d.TrnStockIn.StockInDate,
                                               BeginningQuantity = 0,
                                               InQuantity = d.Quantity,
                                               OutQuantity = 0,
                                               EndingQuantity = d.Quantity,
                                               RunningQuantity = 0,
                                           };

                var currentSoldInventories = from d in db.TrnSalesLines
                                             where d.TrnSale.IsLocked == true
                                             && d.TrnSale.IsCancelled == false
                                             && d.TrnSale.SalesDate >= startDate.Date
                                             && d.TrnSale.SalesDate <= endDate.Date
                                             && d.ItemId == itemId
                                             //&& d.MstItem.Category == Category
                                             && d.MstItem.IsInventory == true
                                             && d.MstItem.IsLocked == true
                                             select new Entities.RepInventoryReportStockCardEntity
                                             {
                                                 Document = "SOLD-" + d.TrnSale.SalesNumber,
                                                 InventoryDate = d.TrnSale.SalesDate,
                                                 BeginningQuantity = 0,
                                                 InQuantity = 0,
                                                 OutQuantity = d.Quantity,
                                                 EndingQuantity = d.Quantity * -1,
                                                 RunningQuantity = 0,
                                             };

                List<Entities.RepInventoryReportStockCardEntity> currentSoldComponentInventories = new List<Entities.RepInventoryReportStockCardEntity>();

                var currentSoldComponents = from d in db.TrnSalesLines
                                            where d.TrnSale.IsLocked == true
                                            && d.TrnSale.IsCancelled == false
                                            && d.TrnSale.SalesDate >= startDate.Date
                                            && d.TrnSale.SalesDate <= endDate.Date
                                            && d.MstItem.IsInventory == false
                                            && d.MstItem.MstItemComponents.Any() == true
                                            && d.MstItem.IsLocked == true
                                            select d;

                if (currentSoldComponents.ToList().Any() == true)
                {
                    foreach (var currentSoldComponent in currentSoldComponents.ToList())
                    {
                        var itemComponents = from d in currentSoldComponent.MstItem.MstItemComponents.ToList()
                                             where d.ComponentItemId == itemId
                                             select d;

                        if (itemComponents.Any() == true)
                        {
                            foreach (var itemComponent in itemComponents.ToList())
                            {
                                currentSoldComponentInventories.Add(new Entities.RepInventoryReportStockCardEntity()
                                {
                                    Document = "SOLD-" + currentSoldComponent.TrnSale.SalesNumber,
                                    InventoryDate = currentSoldComponent.TrnSale.SalesDate,
                                    BeginningQuantity = 0,
                                    InQuantity = 0,
                                    OutQuantity = itemComponent.Quantity * currentSoldComponent.Quantity,
                                    EndingQuantity = (itemComponent.Quantity * currentSoldComponent.Quantity) * -1,
                                    RunningQuantity = 0
                                });
                            }
                        }
                    }
                }

                var currentOutInventories = from d in db.TrnStockOutLines
                                            where d.TrnStockOut.IsLocked == true
                                            && d.TrnStockOut.StockOutDate >= startDate.Date
                                            && d.TrnStockOut.StockOutDate <= endDate.Date
                                            && d.ItemId == itemId
                                            //&& d.MstItem.Category == Category
                                            && d.MstItem.IsInventory == true
                                            && d.MstItem.IsLocked == true
                                            select new Entities.RepInventoryReportStockCardEntity
                                            {
                                                Document = "OUT-" + d.TrnStockOut.StockOutNumber,
                                                InventoryDate = d.TrnStockOut.StockOutDate,
                                                BeginningQuantity = 0,
                                                InQuantity = 0,
                                                OutQuantity = d.Quantity,
                                                EndingQuantity = d.Quantity * -1,
                                                RunningQuantity = 0,
                                            };

                var unionCurrentInventories = currentInInventories.ToList().Union(currentSoldInventories.ToList()).Union(currentSoldComponentInventories.ToList()).Union(currentOutInventories.ToList());

                if (unionBeginningInventories.ToList().Any())
                {
                    var groupBeginningInventories = from d in unionBeginningInventories.ToList()
                                                    group d by new
                                                    {
                                                        d.Document,
                                                        d.InventoryDate
                                                    } into g
                                                    select new Entities.RepInventoryReportStockCardEntity
                                                    {
                                                        Document = g.Key.Document,
                                                        InventoryDate = g.Key.InventoryDate,
                                                        BeginningQuantity = g.Sum(s => s.BeginningQuantity),
                                                        InQuantity = g.Sum(s => s.InQuantity),
                                                        OutQuantity = g.Sum(s => s.OutQuantity),
                                                        EndingQuantity = g.Sum(s => s.BeginningQuantity),
                                                        RunningQuantity = g.Sum(s => s.RunningQuantity)
                                                    };
                    var unionInventories = groupBeginningInventories.ToList().Union(unionCurrentInventories.ToList());
                    if (unionInventories.Any())
                    {
                        var listUnionInventories = unionInventories.Where(d => d.Document.ToUpper().Contains(filter.ToUpper()) == true).OrderBy(d => d.InventoryDate).ToList();

                        List<Entities.RepInventoryReportStockCardEntity> runningBalanceInventories = new List<Entities.RepInventoryReportStockCardEntity>();
                        if (listUnionInventories.Any())
                        {
                            Int32 countLoop = 0;
                            Decimal runningQuantity = 0;

                            foreach (var unionInventory in listUnionInventories)
                            {
                                if (countLoop == 0)
                                {
                                    countLoop += 1;
                                    runningQuantity = unionInventory.EndingQuantity;
                                }
                                else
                                {
                                    runningQuantity += unionInventory.EndingQuantity;
                                }

                                runningBalanceInventories.Add(new Entities.RepInventoryReportStockCardEntity()
                                {
                                    Document = unionInventory.Document,
                                    InventoryDate = unionInventory.InventoryDate,
                                    BeginningQuantity = unionInventory.BeginningQuantity,
                                    InQuantity = unionInventory.InQuantity,
                                    OutQuantity = unionInventory.OutQuantity,
                                    EndingQuantity = unionInventory.EndingQuantity,
                                    RunningQuantity = runningQuantity
                                });
                            }
                        }

                        return runningBalanceInventories;
                    }
                    else
                    {
                        return new List<Entities.RepInventoryReportStockCardEntity>();
                    }
                }
                else
                {
                    var unionInventories = unionBeginningInventories.ToList().Union(unionCurrentInventories.ToList());
                    if (unionInventories.Any())
                    {
                        var listUnionInventories = unionInventories.Where(d => d.Document.ToUpper().Contains(filter.ToUpper()) == true).OrderBy(d => d.InventoryDate).ToList();

                        List<Entities.RepInventoryReportStockCardEntity> runningBalanceInventories = new List<Entities.RepInventoryReportStockCardEntity>();
                        if (listUnionInventories.Any())
                        {
                            Int32 countLoop = 0;
                            Decimal runningQuantity = 0;

                            foreach (var unionInventory in listUnionInventories)
                            {
                                if (countLoop == 0)
                                {
                                    countLoop += 1;
                                    runningQuantity = unionInventory.EndingQuantity;
                                }
                                else
                                {
                                    runningQuantity += unionInventory.EndingQuantity;
                                }

                                runningBalanceInventories.Add(new Entities.RepInventoryReportStockCardEntity()
                                {
                                    Document = unionInventory.Document,
                                    InventoryDate = unionInventory.InventoryDate,
                                    BeginningQuantity = unionInventory.BeginningQuantity,
                                    InQuantity = unionInventory.InQuantity,
                                    OutQuantity = unionInventory.OutQuantity,
                                    EndingQuantity = unionInventory.EndingQuantity,
                                    RunningQuantity = runningQuantity
                                });
                            }
                        }

                        return runningBalanceInventories;
                    }
                    else
                    {
                        return new List<Entities.RepInventoryReportStockCardEntity>();
                    }
                }
            }
            //else if (itemId == 0 && Category != "ALL")
            //{
            //    List<Entities.RepInventoryReportStockCardEntity> newRepInventoryReportStockCardEntity = new List<Entities.RepInventoryReportStockCardEntity>();
            //    var beginningInInventories = from d in db.TrnStockInLines
            //                                 where d.TrnStockIn.IsLocked == true
            //                                 && d.TrnStockIn.StockInDate < startDate.Date
            //                                 && d.MstItem.Category == Category
            //                                 && d.MstItem.IsInventory == true
            //                                 && d.MstItem.IsLocked == true
            //                                 select new Entities.RepInventoryReportStockCardEntity
            //                                 {
            //                                     Document = "Beginning Balance",
            //                                     InventoryDate = startDate.Date,
            //                                     BeginningQuantity = d.Quantity,
            //                                     InQuantity = 0,
            //                                     OutQuantity = 0,
            //                                     EndingQuantity = 0,
            //                                     RunningQuantity = 0,
            //                                 };

            //    var beginningSoldInventories = from d in db.TrnSalesLines
            //                                   where d.TrnSale.IsLocked == true
            //                                   && d.TrnSale.IsCancelled == false
            //                                   && d.TrnSale.SalesDate < startDate.Date
            //                                   && d.MstItem.Category == Category
            //                                   && d.MstItem.IsInventory == true
            //                                   && d.MstItem.IsLocked == true
            //                                   select new Entities.RepInventoryReportStockCardEntity
            //                                   {
            //                                       Document = "Beginning Balance",
            //                                       InventoryDate = startDate.Date,
            //                                       BeginningQuantity = d.Quantity * -1,
            //                                       InQuantity = 0,
            //                                       OutQuantity = 0,
            //                                       EndingQuantity = 0,
            //                                       RunningQuantity = 0,
            //                                   };

            //    List<Entities.RepInventoryReportStockCardEntity> beginningSoldComponentInventories = new List<Entities.RepInventoryReportStockCardEntity>();

            //    var beginningSoldComponents = from d in db.TrnSalesLines
            //                                  where d.TrnSale.IsLocked == true
            //                                  && d.TrnSale.IsCancelled == false
            //                                  && d.TrnSale.SalesDate < startDate.Date
            //                                  && d.MstItem.IsInventory == false
            //                                  && d.MstItem.MstItemComponents.Any() == true
            //                                  && d.MstItem.IsLocked == true
            //                                  select d;

            //    if (beginningSoldComponents.ToList().Any() == true)
            //    {
            //        foreach (var beginningSoldComponent in beginningSoldComponents.ToList())
            //        {
            //            var itemComponents = from d in beginningSoldComponent.MstItem.MstItemComponents.ToList()
            //                                 where d.ComponentItemId == itemId
            //                                 select d;

            //            if (itemComponents.Any() == true)
            //            {
            //                foreach (var itemComponent in itemComponents.ToList())
            //                {
            //                    beginningSoldComponentInventories.Add(new Entities.RepInventoryReportStockCardEntity()
            //                    {
            //                        Document = "Beginning Balance",
            //                        InventoryDate = startDate.Date,
            //                        BeginningQuantity = (itemComponent.Quantity * beginningSoldComponent.Quantity) * -1,
            //                        InQuantity = 0,
            //                        OutQuantity = 0,
            //                        EndingQuantity = 0,
            //                        RunningQuantity = 0,
            //                    });
            //                }
            //            }
            //        }
            //    }

            //    var beginningOutInventories = from d in db.TrnStockOutLines
            //                                  where d.TrnStockOut.IsLocked == true
            //                                  && d.TrnStockOut.StockOutDate < startDate.Date
            //                                  && d.MstItem.Category == Category
            //                                  && d.MstItem.IsInventory == true
            //                                  && d.MstItem.IsLocked == true
            //                                  select new Entities.RepInventoryReportStockCardEntity
            //                                  {
            //                                      Document = "Beginning Balance",
            //                                      InventoryDate = startDate.Date,
            //                                      BeginningQuantity = d.Quantity * -1,
            //                                      InQuantity = 0,
            //                                      OutQuantity = 0,
            //                                      EndingQuantity = 0,
            //                                      RunningQuantity = 0,
            //                                  };

            //    var unionBeginningInventories = beginningInInventories.ToList().Union(beginningSoldInventories.ToList()).Union(beginningSoldComponentInventories.ToList()).Union(beginningOutInventories.ToList());

            //    var currentInInventories = from d in db.TrnStockInLines
            //                               where d.TrnStockIn.IsLocked == true
            //                               && d.TrnStockIn.StockInDate >= startDate.Date
            //                               && d.TrnStockIn.StockInDate <= endDate.Date
            //                               && d.MstItem.Category == Category
            //                               && d.MstItem.IsInventory == true
            //                               && d.MstItem.IsLocked == true
            //                               select new Entities.RepInventoryReportStockCardEntity
            //                               {
            //                                   Document = "IN-" + d.TrnStockIn.StockInNumber,
            //                                   InventoryDate = d.TrnStockIn.StockInDate,
            //                                   BeginningQuantity = 0,
            //                                   InQuantity = d.Quantity,
            //                                   OutQuantity = 0,
            //                                   EndingQuantity = d.Quantity,
            //                                   RunningQuantity = 0,
            //                               };

            //    var currentSoldInventories = from d in db.TrnSalesLines
            //                                 where d.TrnSale.IsLocked == true
            //                                 && d.TrnSale.IsCancelled == false
            //                                 && d.TrnSale.SalesDate >= startDate.Date
            //                                 && d.TrnSale.SalesDate <= endDate.Date
            //                                 && d.MstItem.Category == Category
            //                                 && d.MstItem.IsInventory == true
            //                                 && d.MstItem.IsLocked == true
            //                                 select new Entities.RepInventoryReportStockCardEntity
            //                                 {
            //                                     Document = "SOLD-" + d.TrnSale.SalesNumber,
            //                                     InventoryDate = d.TrnSale.SalesDate,
            //                                     BeginningQuantity = 0,
            //                                     InQuantity = 0,
            //                                     OutQuantity = d.Quantity,
            //                                     EndingQuantity = d.Quantity * -1,
            //                                     RunningQuantity = 0,
            //                                 };

            //    List<Entities.RepInventoryReportStockCardEntity> currentSoldComponentInventories = new List<Entities.RepInventoryReportStockCardEntity>();

            //    var currentSoldComponents = from d in db.TrnSalesLines
            //                                where d.TrnSale.IsLocked == true
            //                                && d.TrnSale.IsCancelled == false
            //                                && d.TrnSale.SalesDate >= startDate.Date
            //                                && d.TrnSale.SalesDate <= endDate.Date
            //                                && d.MstItem.IsInventory == false
            //                                && d.MstItem.MstItemComponents.Any() == true
            //                                && d.MstItem.IsLocked == true
            //                                select d;

            //    if (currentSoldComponents.ToList().Any() == true)
            //    {
            //        foreach (var currentSoldComponent in currentSoldComponents.ToList())
            //        {
            //            var itemComponents = from d in currentSoldComponent.MstItem.MstItemComponents.ToList()
            //                                 where d.ComponentItemId == itemId
            //                                 select d;

            //            if (itemComponents.Any() == true)
            //            {
            //                foreach (var itemComponent in itemComponents.ToList())
            //                {
            //                    currentSoldComponentInventories.Add(new Entities.RepInventoryReportStockCardEntity()
            //                    {
            //                        Document = "SOLD-" + currentSoldComponent.TrnSale.SalesNumber,
            //                        InventoryDate = currentSoldComponent.TrnSale.SalesDate,
            //                        BeginningQuantity = 0,
            //                        InQuantity = 0,
            //                        OutQuantity = itemComponent.Quantity * currentSoldComponent.Quantity,
            //                        EndingQuantity = (itemComponent.Quantity * currentSoldComponent.Quantity) * -1,
            //                        RunningQuantity = 0
            //                    });
            //                }
            //            }
            //        }
            //    }

            //    var currentOutInventories = from d in db.TrnStockOutLines
            //                                where d.TrnStockOut.IsLocked == true
            //                                && d.TrnStockOut.StockOutDate >= startDate.Date
            //                                && d.TrnStockOut.StockOutDate <= endDate.Date
            //                                && d.MstItem.Category == Category
            //                                && d.MstItem.IsInventory == true
            //                                && d.MstItem.IsLocked == true
            //                                select new Entities.RepInventoryReportStockCardEntity
            //                                {
            //                                    Document = "OUT-" + d.TrnStockOut.StockOutNumber,
            //                                    InventoryDate = d.TrnStockOut.StockOutDate,
            //                                    BeginningQuantity = 0,
            //                                    InQuantity = 0,
            //                                    OutQuantity = d.Quantity,
            //                                    EndingQuantity = d.Quantity * -1,
            //                                    RunningQuantity = 0,
            //                                };

            //    var unionCurrentInventories = currentInInventories.ToList().Union(currentSoldInventories.ToList()).Union(currentSoldComponentInventories.ToList()).Union(currentOutInventories.ToList());

            //    if (unionBeginningInventories.ToList().Any())
            //    {
            //        var groupBeginningInventories = from d in unionBeginningInventories.ToList()
            //                                        group d by new
            //                                        {
            //                                            d.Document,
            //                                            d.InventoryDate
            //                                        } into g
            //                                        select new Entities.RepInventoryReportStockCardEntity
            //                                        {
            //                                            Document = g.Key.Document,
            //                                            InventoryDate = g.Key.InventoryDate,
            //                                            BeginningQuantity = g.Sum(s => s.BeginningQuantity),
            //                                            InQuantity = g.Sum(s => s.InQuantity),
            //                                            OutQuantity = g.Sum(s => s.OutQuantity),
            //                                            EndingQuantity = g.Sum(s => s.BeginningQuantity),
            //                                            RunningQuantity = g.Sum(s => s.RunningQuantity)
            //                                        };
            //        var unionInventories = groupBeginningInventories.ToList().Union(unionCurrentInventories.ToList());
            //        if (unionInventories.Any())
            //        {
            //            var listUnionInventories = unionInventories.Where(d => d.Document.ToUpper().Contains(filter.ToUpper()) == true).OrderBy(d => d.InventoryDate).ToList();

            //            List<Entities.RepInventoryReportStockCardEntity> runningBalanceInventories = new List<Entities.RepInventoryReportStockCardEntity>();
            //            if (listUnionInventories.Any())
            //            {
            //                Int32 countLoop = 0;
            //                Decimal runningQuantity = 0;

            //                foreach (var unionInventory in listUnionInventories)
            //                {
            //                    if (countLoop == 0)
            //                    {
            //                        countLoop += 1;
            //                        runningQuantity = unionInventory.EndingQuantity;
            //                    }
            //                    else
            //                    {
            //                        runningQuantity += unionInventory.EndingQuantity;
            //                    }

            //                    runningBalanceInventories.Add(new Entities.RepInventoryReportStockCardEntity()
            //                    {
            //                        Document = unionInventory.Document,
            //                        InventoryDate = unionInventory.InventoryDate,
            //                        BeginningQuantity = unionInventory.BeginningQuantity,
            //                        InQuantity = unionInventory.InQuantity,
            //                        OutQuantity = unionInventory.OutQuantity,
            //                        EndingQuantity = unionInventory.EndingQuantity,
            //                        RunningQuantity = runningQuantity
            //                    });
            //                }
            //            }

            //            return runningBalanceInventories;
            //        }
            //        else
            //        {
            //            return new List<Entities.RepInventoryReportStockCardEntity>();
            //        }
            //    }
            //    else
            //    {
            //        var unionInventories = unionBeginningInventories.ToList().Union(unionCurrentInventories.ToList());
            //        if (unionInventories.Any())
            //        {
            //            var listUnionInventories = unionInventories.Where(d => d.Document.ToUpper().Contains(filter.ToUpper()) == true).OrderBy(d => d.InventoryDate).ToList();

            //            List<Entities.RepInventoryReportStockCardEntity> runningBalanceInventories = new List<Entities.RepInventoryReportStockCardEntity>();
            //            if (listUnionInventories.Any())
            //            {
            //                Int32 countLoop = 0;
            //                Decimal runningQuantity = 0;

            //                foreach (var unionInventory in listUnionInventories)
            //                {
            //                    if (countLoop == 0)
            //                    {
            //                        countLoop += 1;
            //                        runningQuantity = unionInventory.EndingQuantity;
            //                    }
            //                    else
            //                    {
            //                        runningQuantity += unionInventory.EndingQuantity;
            //                    }

            //                    runningBalanceInventories.Add(new Entities.RepInventoryReportStockCardEntity()
            //                    {
            //                        Document = unionInventory.Document,
            //                        InventoryDate = unionInventory.InventoryDate,
            //                        BeginningQuantity = unionInventory.BeginningQuantity,
            //                        InQuantity = unionInventory.InQuantity,
            //                        OutQuantity = unionInventory.OutQuantity,
            //                        EndingQuantity = unionInventory.EndingQuantity,
            //                        RunningQuantity = runningQuantity
            //                    });
            //                }
            //            }

            //            return runningBalanceInventories;
            //        }
            //        else
            //        {
            //            return new List<Entities.RepInventoryReportStockCardEntity>();
            //        }
            //    }
            //}
            //else if (itemId != 0 && Category =="ALL")
            //{
            //    List<Entities.RepInventoryReportStockCardEntity> newRepInventoryReportStockCardEntity = new List<Entities.RepInventoryReportStockCardEntity>();
            //    var beginningInInventories = from d in db.TrnStockInLines
            //                                 where d.TrnStockIn.IsLocked == true
            //                                 && d.TrnStockIn.StockInDate < startDate.Date
            //                                 && d.ItemId == itemId
            //                                 && d.MstItem.IsInventory == true
            //                                 && d.MstItem.IsLocked == true
            //                                 select new Entities.RepInventoryReportStockCardEntity
            //                                 {
            //                                     Document = "Beginning Balance",
            //                                     InventoryDate = startDate.Date,
            //                                     BeginningQuantity = d.Quantity,
            //                                     InQuantity = 0,
            //                                     OutQuantity = 0,
            //                                     EndingQuantity = 0,
            //                                     RunningQuantity = 0,
            //                                 };

            //    var beginningSoldInventories = from d in db.TrnSalesLines
            //                                   where d.TrnSale.IsLocked == true
            //                                   && d.TrnSale.IsCancelled == false
            //                                   && d.TrnSale.SalesDate < startDate.Date
            //                                   && d.ItemId == itemId
            //                                   && d.MstItem.IsInventory == true
            //                                   && d.MstItem.IsLocked == true
            //                                   select new Entities.RepInventoryReportStockCardEntity
            //                                   {
            //                                       Document = "Beginning Balance",
            //                                       InventoryDate = startDate.Date,
            //                                       BeginningQuantity = d.Quantity * -1,
            //                                       InQuantity = 0,
            //                                       OutQuantity = 0,
            //                                       EndingQuantity = 0,
            //                                       RunningQuantity = 0,
            //                                   };

            //    List<Entities.RepInventoryReportStockCardEntity> beginningSoldComponentInventories = new List<Entities.RepInventoryReportStockCardEntity>();

            //    var beginningSoldComponents = from d in db.TrnSalesLines
            //                                  where d.TrnSale.IsLocked == true
            //                                  && d.TrnSale.IsCancelled == false
            //                                  && d.TrnSale.SalesDate < startDate.Date
            //                                  && d.MstItem.IsInventory == false
            //                                  && d.MstItem.MstItemComponents.Any() == true
            //                                  && d.MstItem.IsLocked == true
            //                                  select d;

            //    if (beginningSoldComponents.ToList().Any() == true)
            //    {
            //        foreach (var beginningSoldComponent in beginningSoldComponents.ToList())
            //        {
            //            var itemComponents = from d in beginningSoldComponent.MstItem.MstItemComponents.ToList()
            //                                 where d.ComponentItemId == itemId
            //                                 select d;

            //            if (itemComponents.Any() == true)
            //            {
            //                foreach (var itemComponent in itemComponents.ToList())
            //                {
            //                    beginningSoldComponentInventories.Add(new Entities.RepInventoryReportStockCardEntity()
            //                    {
            //                        Document = "Beginning Balance",
            //                        InventoryDate = startDate.Date,
            //                        BeginningQuantity = (itemComponent.Quantity * beginningSoldComponent.Quantity) * -1,
            //                        InQuantity = 0,
            //                        OutQuantity = 0,
            //                        EndingQuantity = 0,
            //                        RunningQuantity = 0,
            //                    });
            //                }
            //            }
            //        }
            //    }

            //    var beginningOutInventories = from d in db.TrnStockOutLines
            //                                  where d.TrnStockOut.IsLocked == true
            //                                  && d.TrnStockOut.StockOutDate < startDate.Date
            //                                  && d.ItemId == itemId
            //                                  && d.MstItem.IsInventory == true
            //                                  && d.MstItem.IsLocked == true
            //                                  select new Entities.RepInventoryReportStockCardEntity
            //                                  {
            //                                      Document = "Beginning Balance",
            //                                      InventoryDate = startDate.Date,
            //                                      BeginningQuantity = d.Quantity * -1,
            //                                      InQuantity = 0,
            //                                      OutQuantity = 0,
            //                                      EndingQuantity = 0,
            //                                      RunningQuantity = 0,
            //                                  };

            //    var unionBeginningInventories = beginningInInventories.ToList().Union(beginningSoldInventories.ToList()).Union(beginningSoldComponentInventories.ToList()).Union(beginningOutInventories.ToList());

            //    var currentInInventories = from d in db.TrnStockInLines
            //                               where d.TrnStockIn.IsLocked == true
            //                               && d.TrnStockIn.StockInDate >= startDate.Date
            //                               && d.TrnStockIn.StockInDate <= endDate.Date
            //                               && d.ItemId == itemId
            //                               && d.MstItem.IsInventory == true
            //                               && d.MstItem.IsLocked == true
            //                               select new Entities.RepInventoryReportStockCardEntity
            //                               {
            //                                   Document = "IN-" + d.TrnStockIn.StockInNumber,
            //                                   InventoryDate = d.TrnStockIn.StockInDate,
            //                                   BeginningQuantity = 0,
            //                                   InQuantity = d.Quantity,
            //                                   OutQuantity = 0,
            //                                   EndingQuantity = d.Quantity,
            //                                   RunningQuantity = 0,
            //                               };

            //    var currentSoldInventories = from d in db.TrnSalesLines
            //                                 where d.TrnSale.IsLocked == true
            //                                 && d.TrnSale.IsCancelled == false
            //                                 && d.TrnSale.SalesDate >= startDate.Date
            //                                 && d.TrnSale.SalesDate <= endDate.Date
            //                                 && d.ItemId == itemId
            //                                 && d.MstItem.IsInventory == true
            //                                 && d.MstItem.IsLocked == true
            //                                 select new Entities.RepInventoryReportStockCardEntity
            //                                 {
            //                                     Document = "SOLD-" + d.TrnSale.SalesNumber,
            //                                     InventoryDate = d.TrnSale.SalesDate,
            //                                     BeginningQuantity = 0,
            //                                     InQuantity = 0,
            //                                     OutQuantity = d.Quantity,
            //                                     EndingQuantity = d.Quantity * -1,
            //                                     RunningQuantity = 0,
            //                                 };

            //    List<Entities.RepInventoryReportStockCardEntity> currentSoldComponentInventories = new List<Entities.RepInventoryReportStockCardEntity>();

            //    var currentSoldComponents = from d in db.TrnSalesLines
            //                                where d.TrnSale.IsLocked == true
            //                                && d.TrnSale.IsCancelled == false
            //                                && d.TrnSale.SalesDate >= startDate.Date
            //                                && d.TrnSale.SalesDate <= endDate.Date
            //                                && d.MstItem.IsInventory == false
            //                                && d.MstItem.MstItemComponents.Any() == true
            //                                && d.MstItem.IsLocked == true
            //                                select d;

            //    if (currentSoldComponents.ToList().Any() == true)
            //    {
            //        foreach (var currentSoldComponent in currentSoldComponents.ToList())
            //        {
            //            var itemComponents = from d in currentSoldComponent.MstItem.MstItemComponents.ToList()
            //                                 where d.ComponentItemId == itemId
            //                                 select d;

            //            if (itemComponents.Any() == true)
            //            {
            //                foreach (var itemComponent in itemComponents.ToList())
            //                {
            //                    currentSoldComponentInventories.Add(new Entities.RepInventoryReportStockCardEntity()
            //                    {
            //                        Document = "SOLD-" + currentSoldComponent.TrnSale.SalesNumber,
            //                        InventoryDate = currentSoldComponent.TrnSale.SalesDate,
            //                        BeginningQuantity = 0,
            //                        InQuantity = 0,
            //                        OutQuantity = itemComponent.Quantity * currentSoldComponent.Quantity,
            //                        EndingQuantity = (itemComponent.Quantity * currentSoldComponent.Quantity) * -1,
            //                        RunningQuantity = 0
            //                    });
            //                }
            //            }
            //        }
            //    }

            //    var currentOutInventories = from d in db.TrnStockOutLines
            //                                where d.TrnStockOut.IsLocked == true
            //                                && d.TrnStockOut.StockOutDate >= startDate.Date
            //                                && d.TrnStockOut.StockOutDate <= endDate.Date
            //                                && d.ItemId == itemId
            //                                && d.MstItem.IsInventory == true
            //                                && d.MstItem.IsLocked == true
            //                                select new Entities.RepInventoryReportStockCardEntity
            //                                {
            //                                    Document = "OUT-" + d.TrnStockOut.StockOutNumber,
            //                                    InventoryDate = d.TrnStockOut.StockOutDate,
            //                                    BeginningQuantity = 0,
            //                                    InQuantity = 0,
            //                                    OutQuantity = d.Quantity,
            //                                    EndingQuantity = d.Quantity * -1,
            //                                    RunningQuantity = 0,
            //                                };

            //    var unionCurrentInventories = currentInInventories.ToList().Union(currentSoldInventories.ToList()).Union(currentSoldComponentInventories.ToList()).Union(currentOutInventories.ToList());

            //    if (unionBeginningInventories.ToList().Any())
            //    {
            //        var groupBeginningInventories = from d in unionBeginningInventories.ToList()
            //                                        group d by new
            //                                        {
            //                                            d.Document,
            //                                            d.InventoryDate
            //                                        } into g
            //                                        select new Entities.RepInventoryReportStockCardEntity
            //                                        {
            //                                            Document = g.Key.Document,
            //                                            InventoryDate = g.Key.InventoryDate,
            //                                            BeginningQuantity = g.Sum(s => s.BeginningQuantity),
            //                                            InQuantity = g.Sum(s => s.InQuantity),
            //                                            OutQuantity = g.Sum(s => s.OutQuantity),
            //                                            EndingQuantity = g.Sum(s => s.BeginningQuantity),
            //                                            RunningQuantity = g.Sum(s => s.RunningQuantity)
            //                                        };
            //        var unionInventories = groupBeginningInventories.ToList().Union(unionCurrentInventories.ToList());
            //        if (unionInventories.Any())
            //        {
            //            var listUnionInventories = unionInventories.Where(d => d.Document.ToUpper().Contains(filter.ToUpper()) == true).OrderBy(d => d.InventoryDate).ToList();

            //            List<Entities.RepInventoryReportStockCardEntity> runningBalanceInventories = new List<Entities.RepInventoryReportStockCardEntity>();
            //            if (listUnionInventories.Any())
            //            {
            //                Int32 countLoop = 0;
            //                Decimal runningQuantity = 0;

            //                foreach (var unionInventory in listUnionInventories)
            //                {
            //                    if (countLoop == 0)
            //                    {
            //                        countLoop += 1;
            //                        runningQuantity = unionInventory.EndingQuantity;
            //                    }
            //                    else
            //                    {
            //                        runningQuantity += unionInventory.EndingQuantity;
            //                    }

            //                    runningBalanceInventories.Add(new Entities.RepInventoryReportStockCardEntity()
            //                    {
            //                        Document = unionInventory.Document,
            //                        InventoryDate = unionInventory.InventoryDate,
            //                        BeginningQuantity = unionInventory.BeginningQuantity,
            //                        InQuantity = unionInventory.InQuantity,
            //                        OutQuantity = unionInventory.OutQuantity,
            //                        EndingQuantity = unionInventory.EndingQuantity,
            //                        RunningQuantity = runningQuantity
            //                    });
            //                }
            //            }

            //            return runningBalanceInventories;
            //        }
            //        else
            //        {
            //            return new List<Entities.RepInventoryReportStockCardEntity>();
            //        }
            //    }
            //    else
            //    {
            //        var unionInventories = unionBeginningInventories.ToList().Union(unionCurrentInventories.ToList());
            //        if (unionInventories.Any())
            //        {
            //            var listUnionInventories = unionInventories.Where(d => d.Document.ToUpper().Contains(filter.ToUpper()) == true).OrderBy(d => d.InventoryDate).ToList();

            //            List<Entities.RepInventoryReportStockCardEntity> runningBalanceInventories = new List<Entities.RepInventoryReportStockCardEntity>();
            //            if (listUnionInventories.Any())
            //            {
            //                Int32 countLoop = 0;
            //                Decimal runningQuantity = 0;

            //                foreach (var unionInventory in listUnionInventories)
            //                {
            //                    if (countLoop == 0)
            //                    {
            //                        countLoop += 1;
            //                        runningQuantity = unionInventory.EndingQuantity;
            //                    }
            //                    else
            //                    {
            //                        runningQuantity += unionInventory.EndingQuantity;
            //                    }

            //                    runningBalanceInventories.Add(new Entities.RepInventoryReportStockCardEntity()
            //                    {
            //                        Document = unionInventory.Document,
            //                        InventoryDate = unionInventory.InventoryDate,
            //                        BeginningQuantity = unionInventory.BeginningQuantity,
            //                        InQuantity = unionInventory.InQuantity,
            //                        OutQuantity = unionInventory.OutQuantity,
            //                        EndingQuantity = unionInventory.EndingQuantity,
            //                        RunningQuantity = runningQuantity
            //                    });
            //                }
            //            }

            //            return runningBalanceInventories;
            //        }
            //        else
            //        {
            //            return new List<Entities.RepInventoryReportStockCardEntity>();
            //        }
            //    }
            //}
            else
            {
                return new List<Entities.RepInventoryReportStockCardEntity>();
            }
            
        }
           

        // ======================
        // Stock-In Detail Report
        // ======================
        public List<Entities.RepInventoryReportStockInDetailReportEntity> StockInDetailReport(string filter, DateTime startDate, DateTime endDate)
        {
            var stockInDetails = from d in db.TrnStockInLines
                                 where (d.TrnStockIn.StockInNumber.Contains(filter)
                                 ||d.TrnStockIn.ManualStockInNumber.Contains(filter)
                                 || d.MstUnit.Unit.Contains(filter))
                                       && d.TrnStockIn.IsLocked == true
                                       && d.TrnStockIn.StockInDate >= startDate.Date
                                       && d.TrnStockIn.StockInDate <= endDate.Date
                                 select new Entities.RepInventoryReportStockInDetailReportEntity
                                 {
                                     Id = d.Id,
                                     ItemCode = d.MstItem.ItemCode,
                                     BarCode = d.MstItem.BarCode,
                                     StockInDate = d.TrnStockIn.StockInDate.ToShortDateString(),
                                     StockInNumber = d.TrnStockIn.StockInNumber,
                                     ManualStockInNumber = d.TrnStockIn.ManualStockInNumber,
                                     Remarks = d.TrnStockIn.Remarks,
                                     IsReturn = d.TrnStockIn.IsReturn,
                                     Item = d.MstItem.ItemDescription,
                                     Unit = d.MstUnit.Unit,
                                     Quantity = d.Quantity,
                                     Cost = d.Cost,
                                     Amount = d.Amount,
                                     ExpiryDate = d.ExpiryDate.ToString(),
                                     LotNumber = d.LotNumber,
                                     SellingPrice = d.Price != null ? d.Price : 0
                                 };

            return stockInDetails.ToList();
        }

        // =======================
        // Stock-Out Detail Report
        // =======================
        public List<Entities.RepInventoryReportStockOutDetailEntity> StockOutDetailReport(DateTime startDate, DateTime endDate)
        {
            var stockOutDetails = from d in db.TrnStockOutLines
                                  where d.TrnStockOut.IsLocked == true
                                        && d.TrnStockOut.StockOutDate >= startDate.Date
                                        && d.TrnStockOut.StockOutDate <= endDate.Date
                                  select new Entities.RepInventoryReportStockOutDetailEntity
                                  {
                                      Id = d.Id,
                                      ItemCode = d.MstItem.ItemCode,
                                      BarCode = d.MstItem.BarCode,
                                      StockOutDate = d.TrnStockOut.StockOutDate.ToShortDateString(),
                                      StockOutNumber = d.TrnStockOut.StockOutNumber,
                                      ManualStockOutNumber = d.TrnStockOut.ManualStockOutNumber,
                                      Remarks = d.TrnStockOut.Remarks,
                                      Item = d.MstItem.ItemDescription,
                                      Unit = d.MstUnit.Unit,
                                      Quantity = d.Quantity,
                                      Cost = d.Cost,
                                      Amount = d.Amount,
                                  };
            return stockOutDetails.OrderByDescending(d => d.Id).ToList();
        }

        // =========================
        // Stock Count Detail Report
        // =========================
        public List<Entities.RepInventoryReportStockCountDetailReportEntity> StockCountDetailReport(DateTime startDate, DateTime endDate)
        {
            var stockCountDetails = from d in db.TrnStockCountLines
                                    where d.TrnStockCount.StockCountDate >= startDate.Date
                                          && d.TrnStockCount.StockCountDate <= endDate.Date
                                    select new Entities.RepInventoryReportStockCountDetailReportEntity
                                    {
                                        Id = d.Id,
                                        ItemCode = d.MstItem.ItemCode,
                                        BarCode = d.MstItem.BarCode,
                                        StockCountDate = d.TrnStockCount.StockCountDate.ToShortDateString(),
                                        StockCountNumber = d.TrnStockCount.StockCountNumber,
                                        Remarks = d.TrnStockCount.Remarks,
                                        Item = d.MstItem.ItemDescription,
                                        Unit = d.MstUnit.Unit,
                                        Quantity = d.Quantity,
                                        Cost = d.Cost,
                                        Amount = d.Amount
                                    };
            return stockCountDetails.OrderByDescending(d => d.Id).ToList();
        }
        // ====================
        // Inventory List Report
        // ====================
        public List<Entities.MstItemEntity> GetInventoryListReport()
        {
            var item = from d in db.MstItems
                       where d.IsInventory == true
                       && d.IsLocked == true
                       select new Entities.MstItemEntity
                       {
                           Id = d.Id,
                           ItemCode = d.ItemCode,
                           BarCode = d.BarCode,
                           ItemDescription = d.ItemDescription,
                           Unit = d.MstUnit.Unit,
                           Category = d.Category,
                           Cost = d.Cost,
                           Price = d.Price,
                           ReorderQuantity = d.ReorderQuantity,
                           OnhandQuantity = d.OnhandQuantity,
                           IsInventory = d.IsInventory,
                           IsLocked = d.IsLocked
                       };

            return item.ToList();
        }
        // ==================
        // Item Expiry Report
        // ==================
        public List<Entities.TrnStockInLineEntity> GetItemExpiryReport(DateTime startDate, DateTime endDate)
        {
            var item = from d in db.TrnStockInLines
                       where d.ExpiryDate >= startDate.Date
                       && d.ExpiryDate <= endDate.Date
                       select new Entities.TrnStockInLineEntity
                       {
                           ItemCode = d.MstItem.ItemCode,
                           BarCode = d.MstItem.BarCode,
                           ItemDescription = d.MstItem.ItemDescription,
                           Quantity = d.MstItem.OnhandQuantity,
                           Unit = d.MstUnit.Unit,
                           Cost = d.Cost,
                           Price = d.Price,
                           ExpiryDate = d.ExpiryDate.ToString(),
                           LotNumber = d.LotNumber
                          
                       };

            return item.ToList();
        }
    }
}
