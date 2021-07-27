using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software._80mm_Report
{
    public partial class RepInventoryReport80mmForm : Form
    {
        public DateTime startDate;
        public DateTime endDate;
        public String category;
        public Int32 itemId;
        public RepInventoryReport80mmForm(DateTime dateStart, DateTime dateEnd, String filterItemCategory, Int32 itemIds, Boolean isPrintPreview, String printerName)
        {
            InitializeComponent();
            startDate = dateStart;
            endDate = dateEnd;
            category = filterItemCategory;
            itemId = itemIds;

            if (isPrintPreview == true)
            {
                printDocument80mm.PrinterSettings.PrinterName = printerName;
                printDocument80mm.DefaultPageSettings.PaperSize = new PaperSize("Official Receipt", 255, 999999);
                printDocument80mm.Print();
            }
            else
            {
                if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Dot Matrix Printer")
                {
                    printDocument80mm.DefaultPageSettings.PaperSize = new PaperSize("Official Receipt", 255, 38500);
                    printDocument80mm.Print();

                }
                else if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Thermal Printer")
                {
                    printDocument80mm.DefaultPageSettings.PaperSize = new PaperSize("Official Receipt", 270, 38500);
                    printDocument80mm.Print();
                }
                else
                {
                    printDocument80mm.DefaultPageSettings.PaperSize = new PaperSize("Official Receipt", 175, 38500);
                    printDocument80mm.Print();
                }
            }

        }

        private void printDocument80mm_PrintPage(object sender, PrintPageEventArgs e)
        {
            // ============
            // Data Context
            // ============
            Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

            // =============
            // Font Settings
            // =============
            Font fontArial12Bold = new Font("Arial", 12, FontStyle.Bold);
            Font fontArial12Regular = new Font("Arial", 12, FontStyle.Regular);
            Font fontArial11Bold = new Font("Arial", 11, FontStyle.Bold);
            Font fontArial11Regular = new Font("Arial", 11, FontStyle.Regular);
            Font fontArial8Bold = new Font("Arial", 8, FontStyle.Bold);
            Font fontArial8Regular = new Font("Arial", 8, FontStyle.Regular);
            Font fontArial10Bold = new Font("Arial", 10, FontStyle.Bold);
            Font fontArial7Regular = new Font("Arial", 7, FontStyle.Regular);
            Font fontArial7Bold = new Font("Arial", 7, FontStyle.Bold);

            // ==================
            // Alignment Settings
            // ==================
            StringFormat drawFormatCenter = new StringFormat { Alignment = StringAlignment.Center };
            StringFormat drawFormatLeft = new StringFormat { Alignment = StringAlignment.Near };
            StringFormat drawFormatRight = new StringFormat { Alignment = StringAlignment.Far };

            float x, y;
            float width, height;
            if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Dot Matrix Printer")
            {
                x = 5; y = 5;
                width = 245.0F; height = 0F;
            }
            else if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Thermal Printer")
            {
                x = 5; y = 5;
                width = 260.0F; height = 0F;
            }
            else
            {
                x = 5; y = 5;
                width = 170.0F; height = 0F;
            }

            // ==============
            // Tools Settings
            // ==============
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            Pen blackPen = new Pen(Color.Black, 1);
            Pen whitePen = new Pen(Color.White, 1);

            // ========
            // Graphics
            // ========
            Graphics graphics = e.Graphics;

            // ==============
            // System Current
            // ==============
            var systemCurrent = Modules.SysCurrentModule.GetCurrentSettings();

            if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "58mm Printer")
            {
                // =================
                // 80mm Report Title
                // =================
                String title = "Inventory Report";
                graphics.DrawString(title, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(title, fontArial8Bold).Height;


                // =======================
                // Inventory Report Line
                // =======================
                if (itemId == 0 && category == "ALL")
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
                                              d.Cost
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
                                              Amount = g.Key.Cost * g.Sum(s => (s.BeginningQuantity + s.InQuantity) - s.OutQuantity),
                                          };

                        // ==================
                        // Date Range Header
                        // ==================
                        String RangeDateText = "FROM" + " " + startDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + " " + "TO" + " " + endDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                        graphics.DrawString(RangeDateText, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        y += graphics.MeasureString(RangeDateText, fontArial7Regular).Height;

                        // ========
                        // 1st Line
                        // ========
                        Point firstLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                        Point firstLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                        graphics.DrawLine(blackPen, firstLineFirstPoint, firstLineSecondPoint);

                        // ===============
                        // Stock-in Line
                        // ===============


                        String itemLabel = "\nITEM";
                        String unitLabel = "\nUNIT";
                        String balanceLabel = "\nBalance";
                        graphics.DrawString(itemLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                        graphics.DrawString(unitLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        graphics.DrawString(balanceLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                        y += graphics.MeasureString(itemLabel, fontArial7Regular).Height + 3.0F;

                        // ========
                        // 2nd Line
                        // ========
                        Point secondLineFirstPoint = new Point(0, Convert.ToInt32(y) + 3);
                        Point secondLineSecondPoint = new Point(500, Convert.ToInt32(y) + 3);
                        graphics.DrawLine(blackPen, secondLineFirstPoint, secondLineSecondPoint);

                        if (category != null)
                        {

                            RectangleF itemDataRectangle = new RectangleF
                            {
                                X = x,
                                Y = y,
                                Size = new Size(150, ((int)graphics.MeasureString(category, fontArial8Regular, 150, StringFormat.GenericDefault).Height))
                            };
                            graphics.DrawString(category, fontArial8Bold, drawBrush, new RectangleF(x, y + 5, width, height), drawFormatLeft);
                            y += itemDataRectangle.Size.Height * 2 + 3.0F;
                        }

                        if (inventories.Any())
                        {
                            foreach (var inventoryList in inventories.ToList())
                            {
                                float adjustStringName = 1;
                                if (inventoryList.ItemDescription.Length > 7)
                                {
                                    adjustStringName = 45;
                                }
                                String itemData = inventoryList.ItemDescription + inventoryList.Unit + inventoryList.EndingQuantity.ToString("#,##0.00");
                                RectangleF itemDataRectangle = new RectangleF
                                {
                                    X = x,
                                    Y = y,
                                    Size = new Size(240, ((int)graphics.MeasureString(itemData, fontArial7Regular, 240, StringFormat.GenericDefault).Height))
                                };
                                graphics.DrawString(inventoryList.ItemDescription + "\n", fontArial7Regular, drawBrush, new RectangleF(x, y, 60, height), drawFormatLeft);
                                graphics.DrawString(inventoryList.Unit + "\n", fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                                graphics.DrawString(inventoryList.EndingQuantity.ToString("#,##0.00") + "\n", fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                                y += itemDataRectangle.Size.Height + adjustStringName + 3.0F;
                            }
                        }
                        String space = "\n.";
                        graphics.DrawString(space, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    }
                    else
                    {
                        new List<Entities.RepInventoryReportEntity>();
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
                                              d.Cost
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
                                              Amount = g.Key.Cost * g.Sum(s => (s.BeginningQuantity + s.InQuantity) - s.OutQuantity),
                                          };

                        // ==================
                        // Date Range Header
                        // ==================
                        String RangeDateText = "FROM" + " " + startDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + " " + "TO" + " " + endDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                        graphics.DrawString(RangeDateText, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        y += graphics.MeasureString(RangeDateText, fontArial7Regular).Height;

                        // ========
                        // 1st Line
                        // ========
                        Point firstLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                        Point firstLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                        graphics.DrawLine(blackPen, firstLineFirstPoint, firstLineSecondPoint);

                        // ===============
                        // Stock-in Line
                        // ===============


                        String itemLabel = "\nITEM";
                        String unitLabel = "\nUNIT";
                        String balanceLabel = "\nBalance";
                        graphics.DrawString(itemLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                        graphics.DrawString(unitLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        graphics.DrawString(balanceLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                        y += graphics.MeasureString(itemLabel, fontArial7Regular).Height + 3.0F;

                        // ========
                        // 2nd Line
                        // ========
                        Point secondLineFirstPoint = new Point(0, Convert.ToInt32(y) + 3);
                        Point secondLineSecondPoint = new Point(500, Convert.ToInt32(y) + 3);
                        graphics.DrawLine(blackPen, secondLineFirstPoint, secondLineSecondPoint);

                        if (category != null)
                        {

                            RectangleF itemDataRectangle = new RectangleF
                            {
                                X = x,
                                Y = y,
                                Size = new Size(150, ((int)graphics.MeasureString(category, fontArial8Regular, 150, StringFormat.GenericDefault).Height))
                            };
                            graphics.DrawString(category, fontArial8Bold, drawBrush, new RectangleF(x, y + 5, width, height), drawFormatLeft);
                            y += itemDataRectangle.Size.Height * 2 + 3.0F;
                        }

                        if (inventories.Any())
                        {
                            foreach (var inventoryList in inventories.ToList())
                            {
                                float adjustStringName = 1;
                                if (inventoryList.ItemDescription.Length > 27)
                                {
                                    adjustStringName = 45;
                                }
                                String itemData = inventoryList.ItemDescription + inventoryList.Unit + inventoryList.EndingQuantity.ToString("#,##0.00");
                                RectangleF itemDataRectangle = new RectangleF
                                {
                                    X = x,
                                    Y = y,
                                    Size = new Size(240, ((int)graphics.MeasureString(itemData, fontArial7Regular, 240, StringFormat.GenericDefault).Height))
                                };
                                graphics.DrawString(inventoryList.ItemDescription + "\n", fontArial7Regular, drawBrush, new RectangleF(x, y, 60, height), drawFormatLeft);
                                graphics.DrawString(inventoryList.Unit + "\n", fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                                graphics.DrawString(inventoryList.EndingQuantity.ToString("#,##0.00") + "\n", fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                                y += itemDataRectangle.Size.Height + adjustStringName + 3.0F;
                            }
                        }
                        String space = "\n.";
                        graphics.DrawString(space, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    }
                    else
                    {
                        new List<Entities.RepInventoryReportEntity>();
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
                                              d.Cost
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
                                              Amount = g.Key.Cost * g.Sum(s => (s.BeginningQuantity + s.InQuantity) - s.OutQuantity),
                                          };

                        // ==================
                        // Date Range Header
                        // ==================
                        String RangeDateText = "FROM" + " " + startDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + " " + "TO" + " " + endDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                        graphics.DrawString(RangeDateText, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        y += graphics.MeasureString(RangeDateText, fontArial7Regular).Height;

                        // ========
                        // 1st Line
                        // ========
                        Point firstLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                        Point firstLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                        graphics.DrawLine(blackPen, firstLineFirstPoint, firstLineSecondPoint);

                        // ===============
                        // Stock-in Line
                        // ===============


                        String itemLabel = "\nITEM";
                        String unitLabel = "\nUNIT";
                        String balanceLabel = "\nBalance";
                        graphics.DrawString(itemLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                        graphics.DrawString(unitLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        graphics.DrawString(balanceLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                        y += graphics.MeasureString(itemLabel, fontArial7Regular).Height + 3.0F;

                        // ========
                        // 2nd Line
                        // ========
                        Point secondLineFirstPoint = new Point(0, Convert.ToInt32(y) + 3);
                        Point secondLineSecondPoint = new Point(500, Convert.ToInt32(y) + 3);
                        graphics.DrawLine(blackPen, secondLineFirstPoint, secondLineSecondPoint);

                        if (category != null)
                        {

                            RectangleF itemDataRectangle = new RectangleF
                            {
                                X = x,
                                Y = y,
                                Size = new Size(150, ((int)graphics.MeasureString(category, fontArial8Regular, 150, StringFormat.GenericDefault).Height))
                            };
                            graphics.DrawString(category, fontArial8Bold, drawBrush, new RectangleF(x, y + 5, width, height), drawFormatLeft);
                            y += itemDataRectangle.Size.Height * 2 + 3.0F;
                        }

                        if (inventories.Any())
                        {
                            foreach (var inventoryList in inventories.ToList())
                            {
                                float adjustStringName = 1;
                                if (inventoryList.ItemDescription.Length > 27)
                                {
                                    adjustStringName = 45;
                                }
                                String itemData = inventoryList.ItemDescription + inventoryList.Unit + inventoryList.EndingQuantity.ToString("#,##0.00");
                                RectangleF itemDataRectangle = new RectangleF
                                {
                                    X = x,
                                    Y = y,
                                    Size = new Size(240, ((int)graphics.MeasureString(itemData, fontArial7Regular, 240, StringFormat.GenericDefault).Height))
                                };
                                graphics.DrawString(inventoryList.ItemDescription + "\n", fontArial7Regular, drawBrush, new RectangleF(x, y, 60, height), drawFormatLeft);
                                graphics.DrawString(inventoryList.Unit + "\n", fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                                graphics.DrawString(inventoryList.EndingQuantity.ToString("#,##0.00") + "\n", fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                                y += itemDataRectangle.Size.Height + adjustStringName + 3.0F;
                            }
                        }
                        String space = "\n.";
                        graphics.DrawString(space, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    }
                    else
                    {
                        new List<Entities.RepInventoryReportEntity>();
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
                                              d.Cost
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
                                              Amount = g.Key.Cost * g.Sum(s => (s.BeginningQuantity + s.InQuantity) - s.OutQuantity),
                                          };
                        // ==================
                        // Date Range Header
                        // ==================
                        String RangeDateText = "FROM" + " " + startDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + " " + "TO" + " " + endDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                        graphics.DrawString(RangeDateText, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        y += graphics.MeasureString(RangeDateText, fontArial7Regular).Height;

                        // ========
                        // 1st Line
                        // ========
                        Point firstLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                        Point firstLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                        graphics.DrawLine(blackPen, firstLineFirstPoint, firstLineSecondPoint);

                        // ===============
                        // Stock-in Line
                        // ===============


                        String itemLabel = "\nITEM";
                        String unitLabel = "\nUNIT";
                        String balanceLabel = "\nBalance";
                        graphics.DrawString(itemLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                        graphics.DrawString(unitLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        graphics.DrawString(balanceLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                        y += graphics.MeasureString(itemLabel, fontArial7Regular).Height + 3.0F;

                        // ========
                        // 2nd Line
                        // ========
                        Point secondLineFirstPoint = new Point(0, Convert.ToInt32(y) + 3);
                        Point secondLineSecondPoint = new Point(500, Convert.ToInt32(y) + 3);
                        graphics.DrawLine(blackPen, secondLineFirstPoint, secondLineSecondPoint);

                        if (category != null)
                        {

                            RectangleF itemDataRectangle = new RectangleF
                            {
                                X = x,
                                Y = y,
                                Size = new Size(150, ((int)graphics.MeasureString(category, fontArial8Regular, 150, StringFormat.GenericDefault).Height))
                            };
                            graphics.DrawString(category, fontArial8Bold, drawBrush, new RectangleF(x, y + 5, width, height), drawFormatLeft);
                            y += itemDataRectangle.Size.Height * 2 + 3.0F;
                        }

                        if (inventories.Any())
                        {
                            foreach (var inventoryList in inventories.ToList())
                            {
                                float adjustStringName = 1;
                                if (inventoryList.ItemDescription.Length > 27)
                                {
                                    adjustStringName = 45;
                                }
                                String itemData = inventoryList.ItemDescription + inventoryList.Unit + inventoryList.EndingQuantity.ToString("#,##0.00");
                                RectangleF itemDataRectangle = new RectangleF
                                {
                                    X = x,
                                    Y = y,
                                    Size = new Size(240, ((int)graphics.MeasureString(itemData, fontArial7Regular, 240, StringFormat.GenericDefault).Height))
                                };
                                graphics.DrawString(inventoryList.ItemDescription + "\n", fontArial7Regular, drawBrush, new RectangleF(x, y, 60, height), drawFormatLeft);
                                graphics.DrawString(inventoryList.Unit + "\n", fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                                graphics.DrawString(inventoryList.EndingQuantity.ToString("#,##0.00") + "\n", fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                                y += itemDataRectangle.Size.Height + adjustStringName + 3.0F;
                            }
                        }

                        String space = "\n.";
                        graphics.DrawString(space, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    }
                    else
                    {
                        new List<Entities.RepInventoryReportEntity>();
                    }
                }
                else
                {
                    new List<Entities.RepInventoryReportEntity>();
                }
            }
            else
            {
                // =================
                // 80mm Report Title
                // =================
                String title = "Inventory Report";
                graphics.DrawString(title, fontArial11Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(title, fontArial11Bold).Height;


                // =======================
                // Inventory Report Line
                // =======================
                if (itemId == 0 && category == "ALL")
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
                                              d.Cost
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
                                              Amount = g.Key.Cost * g.Sum(s => (s.BeginningQuantity + s.InQuantity) - s.OutQuantity),
                                          };

                        // ==================
                        // Date Range Header
                        // ==================
                        String RangeDateText = "FROM" + " " + startDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + " " + "TO" + " " + endDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                        graphics.DrawString(RangeDateText, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        y += graphics.MeasureString(RangeDateText, fontArial8Regular).Height;

                        // ========
                        // 1st Line
                        // ========
                        Point firstLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                        Point firstLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                        graphics.DrawLine(blackPen, firstLineFirstPoint, firstLineSecondPoint);

                        // ===============
                        // Stock-in Line
                        // ===============


                        String itemLabel = "\nITEM";
                        String unitLabel = "\nUNIT";
                        String balanceLabel = "\nBalance";
                        graphics.DrawString(itemLabel, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                        graphics.DrawString(unitLabel, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        graphics.DrawString(balanceLabel, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                        y += graphics.MeasureString(itemLabel, fontArial8Regular).Height + 3.0F;

                        // ========
                        // 2nd Line
                        // ========
                        Point secondLineFirstPoint = new Point(0, Convert.ToInt32(y) + 3);
                        Point secondLineSecondPoint = new Point(500, Convert.ToInt32(y) + 3);
                        graphics.DrawLine(blackPen, secondLineFirstPoint, secondLineSecondPoint);

                        if (category != null)
                        {

                            RectangleF itemDataRectangle = new RectangleF
                            {
                                X = x,
                                Y = y,
                                Size = new Size(150, ((int)graphics.MeasureString(category, fontArial8Regular, 150, StringFormat.GenericDefault).Height))
                            };
                            graphics.DrawString(category, fontArial10Bold, drawBrush, new RectangleF(x, y + 5, width, height), drawFormatLeft);
                            y += itemDataRectangle.Size.Height * 2 + 3.0F;
                        }

                        if (inventories.Any())
                        {
                            foreach (var inventoryList in inventories.ToList())
                            {
                                float adjustStringName = 1;
                                if (inventoryList.ItemDescription.Length > 27)
                                {
                                    adjustStringName = 16;
                                }
                                String itemData = inventoryList.ItemDescription + inventoryList.Unit + inventoryList.EndingQuantity.ToString("#,##0.00");
                                RectangleF itemDataRectangle = new RectangleF
                                {
                                    X = x,
                                    Y = y,
                                    Size = new Size(240, ((int)graphics.MeasureString(itemData, fontArial8Regular, 240, StringFormat.GenericDefault).Height))
                                };
                                graphics.DrawString(inventoryList.ItemDescription + "\n", fontArial8Regular, drawBrush, new RectangleF(x, y, 120, height), drawFormatLeft);
                                graphics.DrawString(inventoryList.Unit + "\n", fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                                graphics.DrawString(inventoryList.EndingQuantity.ToString("#,##0.00") + "\n", fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                                y += itemDataRectangle.Size.Height + adjustStringName + 3.0F;
                            }
                        }
                        if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Dot Matrix Printer")
                        {
                            String space = "\n\n\n\n\n\n\n\n\n\n.";
                            graphics.DrawString(space, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        }
                        else
                        {
                            String space = "\n\n\n.";
                            graphics.DrawString(space, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        }
                    }
                    else
                    {
                        new List<Entities.RepInventoryReportEntity>();
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
                                              d.Cost
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
                                              Amount = g.Key.Cost * g.Sum(s => (s.BeginningQuantity + s.InQuantity) - s.OutQuantity),
                                          };

                        // ==================
                        // Date Range Header
                        // ==================
                        String RangeDateText = "FROM" + " " + startDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + " " + "TO" + " " + endDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                        graphics.DrawString(RangeDateText, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        y += graphics.MeasureString(RangeDateText, fontArial8Regular).Height;

                        // ========
                        // 1st Line
                        // ========
                        Point firstLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                        Point firstLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                        graphics.DrawLine(blackPen, firstLineFirstPoint, firstLineSecondPoint);

                        // ===============
                        // Stock-in Line
                        // ===============


                        String itemLabel = "\nITEM";
                        String unitLabel = "\nUNIT";
                        String balanceLabel = "\nBalance";
                        graphics.DrawString(itemLabel, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                        graphics.DrawString(unitLabel, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        graphics.DrawString(balanceLabel, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                        y += graphics.MeasureString(itemLabel, fontArial8Regular).Height + 3.0F;

                        // ========
                        // 2nd Line
                        // ========
                        Point secondLineFirstPoint = new Point(0, Convert.ToInt32(y) + 3);
                        Point secondLineSecondPoint = new Point(500, Convert.ToInt32(y) + 3);
                        graphics.DrawLine(blackPen, secondLineFirstPoint, secondLineSecondPoint);

                        if (category != null)
                        {

                            RectangleF itemDataRectangle = new RectangleF
                            {
                                X = x,
                                Y = y,
                                Size = new Size(150, ((int)graphics.MeasureString(category, fontArial8Regular, 150, StringFormat.GenericDefault).Height))
                            };
                            graphics.DrawString(category, fontArial10Bold, drawBrush, new RectangleF(x, y + 5, width, height), drawFormatLeft);
                            y += itemDataRectangle.Size.Height * 2 + 3.0F;
                        }

                        if (inventories.Any())
                        {
                            foreach (var inventoryList in inventories.ToList())
                            {
                                float adjustStringName = 1;
                                if (inventoryList.ItemDescription.Length > 27)
                                {
                                    adjustStringName = 16;
                                }
                                String itemData = inventoryList.ItemDescription + inventoryList.Unit + inventoryList.EndingQuantity.ToString("#,##0.00");
                                RectangleF itemDataRectangle = new RectangleF
                                {
                                    X = x,
                                    Y = y,
                                    Size = new Size(240, ((int)graphics.MeasureString(itemData, fontArial8Regular, 240, StringFormat.GenericDefault).Height))
                                };
                                graphics.DrawString(inventoryList.ItemDescription + "\n", fontArial8Regular, drawBrush, new RectangleF(x, y, 120, height), drawFormatLeft);
                                graphics.DrawString(inventoryList.Unit + "\n", fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                                graphics.DrawString(inventoryList.EndingQuantity.ToString("#,##0.00") + "\n", fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                                y += itemDataRectangle.Size.Height + adjustStringName + 3.0F;
                            }
                        }
                        if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Dot Matrix Printer")
                        {
                            String space = "\n\n\n\n\n\n\n\n\n\n.";
                            graphics.DrawString(space, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        }
                        else
                        {
                            String space = "\n\n\n.";
                            graphics.DrawString(space, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        }
                    }
                    else
                    {
                        new List<Entities.RepInventoryReportEntity>();
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
                                              d.Cost
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
                                              Amount = g.Key.Cost * g.Sum(s => (s.BeginningQuantity + s.InQuantity) - s.OutQuantity),
                                          };

                        // ==================
                        // Date Range Header
                        // ==================
                        String RangeDateText = "FROM" + " " + startDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + " " + "TO" + " " + endDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                        graphics.DrawString(RangeDateText, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        y += graphics.MeasureString(RangeDateText, fontArial8Regular).Height;

                        // ========
                        // 1st Line
                        // ========
                        Point firstLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                        Point firstLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                        graphics.DrawLine(blackPen, firstLineFirstPoint, firstLineSecondPoint);

                        // ===============
                        // Stock-in Line
                        // ===============


                        String itemLabel = "\nITEM";
                        String unitLabel = "\nUNIT";
                        String balanceLabel = "\nBalance";
                        graphics.DrawString(itemLabel, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                        graphics.DrawString(unitLabel, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        graphics.DrawString(balanceLabel, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                        y += graphics.MeasureString(itemLabel, fontArial8Regular).Height + 3.0F;

                        // ========
                        // 2nd Line
                        // ========
                        Point secondLineFirstPoint = new Point(0, Convert.ToInt32(y) + 3);
                        Point secondLineSecondPoint = new Point(500, Convert.ToInt32(y) + 3);
                        graphics.DrawLine(blackPen, secondLineFirstPoint, secondLineSecondPoint);

                        if (category != null)
                        {

                            RectangleF itemDataRectangle = new RectangleF
                            {
                                X = x,
                                Y = y,
                                Size = new Size(150, ((int)graphics.MeasureString(category, fontArial8Regular, 150, StringFormat.GenericDefault).Height))
                            };
                            graphics.DrawString(category, fontArial10Bold, drawBrush, new RectangleF(x, y + 5, width, height), drawFormatLeft);
                            y += itemDataRectangle.Size.Height * 2 + 3.0F;
                        }

                        if (inventories.Any())
                        {
                            foreach (var inventoryList in inventories.ToList())
                            {
                                float adjustStringName = 1;
                                if (inventoryList.ItemDescription.Length > 27)
                                {
                                    adjustStringName = 16;
                                }
                                String itemData = inventoryList.ItemDescription + inventoryList.Unit + inventoryList.EndingQuantity.ToString("#,##0.00");
                                RectangleF itemDataRectangle = new RectangleF
                                {
                                    X = x,
                                    Y = y,
                                    Size = new Size(240, ((int)graphics.MeasureString(itemData, fontArial8Regular, 240, StringFormat.GenericDefault).Height))
                                };
                                graphics.DrawString(inventoryList.ItemDescription + "\n", fontArial8Regular, drawBrush, new RectangleF(x, y, 120, height), drawFormatLeft);
                                graphics.DrawString(inventoryList.Unit + "\n", fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                                graphics.DrawString(inventoryList.EndingQuantity.ToString("#,##0.00") + "\n", fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                                y += itemDataRectangle.Size.Height + adjustStringName + 3.0F;
                            }
                        }
                        if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Dot Matrix Printer")
                        {
                            String space = "\n\n\n\n\n\n\n\n\n\n.";
                            graphics.DrawString(space, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        }
                        else
                        {
                            String space = "\n\n\n.";
                            graphics.DrawString(space, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        }
                    }
                    else
                    {
                        new List<Entities.RepInventoryReportEntity>();
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
                                              d.Cost
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
                                              Amount = g.Key.Cost * g.Sum(s => (s.BeginningQuantity + s.InQuantity) - s.OutQuantity),
                                          };
                        // ==================
                        // Date Range Header
                        // ==================
                        String RangeDateText = "FROM" + " " + startDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + " " + "TO" + " " + endDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                        graphics.DrawString(RangeDateText, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        y += graphics.MeasureString(RangeDateText, fontArial8Regular).Height;

                        // ========
                        // 1st Line
                        // ========
                        Point firstLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                        Point firstLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                        graphics.DrawLine(blackPen, firstLineFirstPoint, firstLineSecondPoint);

                        // ===============
                        // Stock-in Line
                        // ===============


                        String itemLabel = "\nITEM";
                        String unitLabel = "\nUNIT";
                        String balanceLabel = "\nBalance";
                        graphics.DrawString(itemLabel, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                        graphics.DrawString(unitLabel, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        graphics.DrawString(balanceLabel, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                        y += graphics.MeasureString(itemLabel, fontArial8Regular).Height + 3.0F;

                        // ========
                        // 2nd Line
                        // ========
                        Point secondLineFirstPoint = new Point(0, Convert.ToInt32(y) + 3);
                        Point secondLineSecondPoint = new Point(500, Convert.ToInt32(y) + 3);
                        graphics.DrawLine(blackPen, secondLineFirstPoint, secondLineSecondPoint);

                        if (category != null)
                        {

                            RectangleF itemDataRectangle = new RectangleF
                            {
                                X = x,
                                Y = y,
                                Size = new Size(150, ((int)graphics.MeasureString(category, fontArial8Regular, 150, StringFormat.GenericDefault).Height))
                            };
                            graphics.DrawString(category, fontArial10Bold, drawBrush, new RectangleF(x, y + 5, width, height), drawFormatLeft);
                            y += itemDataRectangle.Size.Height * 2 + 3.0F;
                        }

                        if (inventories.Any())
                        {
                            foreach (var inventoryList in inventories.ToList())
                            {
                                float adjustStringName = 1;
                                if (inventoryList.ItemDescription.Length > 27)
                                {
                                    adjustStringName = 16;
                                }
                                String itemData = inventoryList.ItemDescription + inventoryList.Unit + inventoryList.EndingQuantity.ToString("#,##0.00");
                                RectangleF itemDataRectangle = new RectangleF
                                {
                                    X = x,
                                    Y = y,
                                    Size = new Size(240, ((int)graphics.MeasureString(itemData, fontArial8Regular, 240, StringFormat.GenericDefault).Height))
                                };
                                graphics.DrawString(inventoryList.ItemDescription + "\n", fontArial8Regular, drawBrush, new RectangleF(x, y, 120, height), drawFormatLeft);
                                graphics.DrawString(inventoryList.Unit + "\n", fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                                graphics.DrawString(inventoryList.EndingQuantity.ToString("#,##0.00") + "\n", fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                                y += itemDataRectangle.Size.Height + adjustStringName + 3.0F;
                            }
                        }
                        if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Dot Matrix Printer")
                        {
                            String space = "\n\n\n\n\n\n\n\n\n\n.";
                            graphics.DrawString(space, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        }
                        else
                        {
                            String space = "\n\n\n.";
                            graphics.DrawString(space, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        }
                    }
                    else
                    {
                        new List<Entities.RepInventoryReportEntity>();
                    }
                }
                else
                {
                    new List<Entities.RepInventoryReportEntity>();
                }
            }

        }
    }
}

