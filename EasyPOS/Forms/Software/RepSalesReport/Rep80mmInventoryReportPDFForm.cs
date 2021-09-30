using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.RepSalesReport
{
    public partial class Rep80mmInventoryReportPDFForm : Form
    {
        public DateTime startDate;
        public DateTime endDate;
        public String category;
        public Int32 itemId;

        public Rep80mmInventoryReportPDFForm(DateTime dateStart, DateTime dateEnd, String filterItemCategory, Int32 itemIds)
        {
            InitializeComponent();

            startDate = dateStart;
            endDate = dateEnd;
            category = filterItemCategory;
            itemId = itemIds;

            PrintReport();
        }

        public void PrintReport()
        {
            try
            {
                Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

                iTextSharp.text.Font fontHelvetica10 = FontFactory.GetFont(BaseFont.HELVETICA, 10);
                iTextSharp.text.Font fontHelvetica10Italic = FontFactory.GetFont(BaseFont.HELVETICA, 10, iTextSharp.text.Font.ITALIC);
                iTextSharp.text.Font fontHelvetica10Bold = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, 10, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font fontArial11Bold = FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font fontHelvetica14Bold = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, 12, iTextSharp.text.Font.BOLD);

                Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.5F, 100.0F, BaseColor.DARK_GRAY, Element.ALIGN_MIDDLE, 10F)));

                var fileName = "80mmInventoryReport" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                var currentUser = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;

                //float h = tableHeader.TotalHeight + tableLines.TotalHeight;
                var pgSize = new iTextSharp.text.Rectangle(270, 13999);
                Document document = new Document(pgSize);
                document.SetMargins(1f, 5f, 5f, 5f);

                PdfWriter pdfWriter = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));

                document.Open();

                var systemCurrent = Modules.SysCurrentModule.GetCurrentSettings();

                String companyName = systemCurrent.CompanyName;
                String documentTitle = "Inventory Report";

                PdfPTable tableHeader = new PdfPTable(1);
                tableHeader.SetWidths(new float[] { 70f });
                tableHeader.TotalWidth = 250f;
                tableHeader.SplitLate = false;
                tableHeader.SplitRows = true;
                tableHeader.AddCell(new PdfPCell(new Phrase(documentTitle, fontHelvetica14Bold)) { Border = 0, Padding = 3f, PaddingBottom = 1f, PaddingLeft = 55f });
                tableHeader.AddCell(new PdfPCell(new Phrase("\nFrom : " + startDate.ToShortDateString() + " To: " + endDate.ToShortDateString() + "\n", fontHelvetica10)) { Colspan = 1, Border = 0, Padding = 3f, PaddingBottom = 5f, PaddingLeft = 40f });
                document.Add(tableHeader);

                PdfPTable tableLines = new PdfPTable(3);
                tableLines.SetWidths(new float[] { 100f, 70f, 70f });
                tableLines.TotalWidth = 250f;
                tableLines.SplitLate = false;
                tableLines.SplitRows = true;
                tableLines.AddCell(new PdfPCell(new Phrase("Item", fontHelvetica10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Unit", fontHelvetica10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Balance", fontHelvetica10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });

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
                        //tableHeader.AddCell(new PdfPCell(new Phrase(documentTitle, fontHelvetica14Bold)) { Border = 0, Padding = 3f, PaddingBottom = 1f, PaddingLeft = 55f });
                        //tableHeader.AddCell(new PdfPCell(new Phrase("\nFrom : " + startDate.ToShortDateString() + " To: " + endDate.ToShortDateString() + "\n", fontHelvetica10)) { Colspan = 1, Border = 0, Padding = 3f, PaddingBottom = 5f, PaddingLeft = 40f });

                        // ========
                        // 1st Line
                        // ========
                        //tableLines.AddCell(new PdfPCell(new Phrase(line)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = -5f, Colspan = 4 });

                        // ===============
                        // Stock-in Line
                        // ===============
                        //tableLines.AddCell(new PdfPCell(new Phrase("Item", fontHelvetica10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                        //tableLines.AddCell(new PdfPCell(new Phrase("Unit", fontHelvetica10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                        //tableLines.AddCell(new PdfPCell(new Phrase("Balance", fontHelvetica10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });

                        // ========
                        // 2nd Line
                        // ========
                        //tableLines.AddCell(new PdfPCell(new Phrase(line)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = -5f, Colspan = 4 });

                        if (category != null)
                        {

                            tableLines.AddCell(new PdfPCell(new Phrase(category, fontHelvetica10Bold)) { Colspan = 5, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });

                        }

                        if (inventories.Any())
                        {
                            foreach (var inventoryList in inventories.ToList())
                            {
                                tableLines.AddCell(new PdfPCell(new Phrase(inventoryList.ItemDescription, fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                tableLines.AddCell(new PdfPCell(new Phrase(inventoryList.Unit, fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                tableLines.AddCell(new PdfPCell(new Phrase(inventoryList.EndingQuantity.ToString("#,##0.00"), fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                            }
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

                        //// ==================
                        //// Date Range Header
                        //// ==================
                        //tableHeader.AddCell(new PdfPCell(new Phrase("\nFrom : " + startDate.ToShortDateString() + " To: " + endDate.ToShortDateString() + "\n", fontHelvetica10)) { Colspan = 1, Border = 0, Padding = 3f, PaddingBottom = 5f, PaddingLeft = 40f });

                        //// ========
                        //// 1st Line
                        //// ========
                        //tableLines.AddCell(new PdfPCell(new Phrase(line)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = -5f, Colspan = 4 });


                        //// ===============
                        //// Stock-in Line
                        //// ===============
                        //tableLines.AddCell(new PdfPCell(new Phrase("Item", fontHelvetica10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                        //tableLines.AddCell(new PdfPCell(new Phrase("Unit", fontHelvetica10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                        //tableLines.AddCell(new PdfPCell(new Phrase("Balance", fontHelvetica10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });

                        //// ========
                        //// 2nd Line
                        //// ========
                        //tableLines.AddCell(new PdfPCell(new Phrase(line)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = -5f, Colspan = 4 });


                        if (category != null)
                        {
                            tableLines.AddCell(new PdfPCell(new Phrase(category, fontHelvetica10Bold)) { Colspan = 5, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                        }

                        if (inventories.Any())
                        {
                            foreach (var inventoryList in inventories.ToList())
                            {
                                tableLines.AddCell(new PdfPCell(new Phrase(inventoryList.ItemDescription, fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                tableLines.AddCell(new PdfPCell(new Phrase(inventoryList.Unit, fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                tableLines.AddCell(new PdfPCell(new Phrase(inventoryList.EndingQuantity.ToString("#,##0.00"), fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                            }
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

                        //// ==================
                        //// Date Range Header
                        //// ==================
                        //tableHeader.AddCell(new PdfPCell(new Phrase("\nFrom : " + startDate.ToShortDateString() + " To: " + endDate.ToShortDateString() + "\n", fontHelvetica10)) { Colspan = 1, Border = 0, Padding = 3f, PaddingBottom = 5f, PaddingLeft = 40f });

                        //// ========
                        //// 1st Line
                        //// ========
                        //tableLines.AddCell(new PdfPCell(new Phrase(line)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = -5f, Colspan = 4 });


                        //// ===============
                        //// Stock-in Line
                        //// ===============
                        //tableLines.AddCell(new PdfPCell(new Phrase("Item", fontHelvetica10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                        //tableLines.AddCell(new PdfPCell(new Phrase("Unit", fontHelvetica10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                        //tableLines.AddCell(new PdfPCell(new Phrase("Balance", fontHelvetica10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });

                        //// ========
                        //// 2nd Line
                        //// ========
                        //tableLines.AddCell(new PdfPCell(new Phrase(line)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = -5f, Colspan = 4 });


                        if (category != null)
                        {
                            tableLines.AddCell(new PdfPCell(new Phrase(category, fontHelvetica10Bold)) { Colspan = 5, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                        }

                        if (inventories.Any())
                        {
                            foreach (var inventoryList in inventories.ToList())
                            {
                                tableLines.AddCell(new PdfPCell(new Phrase(inventoryList.ItemDescription, fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                tableLines.AddCell(new PdfPCell(new Phrase(inventoryList.Unit, fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                tableLines.AddCell(new PdfPCell(new Phrase(inventoryList.EndingQuantity.ToString("#,##0.00"), fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                            }
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
                        //// ==================
                        //// Date Range Header
                        //// ==================
                        //tableHeader.AddCell(new PdfPCell(new Phrase("\nFrom : " + startDate.ToShortDateString() + " To: " + endDate.ToShortDateString() + "\n", fontHelvetica10)) { Colspan = 1, Border = 0, Padding = 3f, PaddingBottom = 5f, PaddingLeft = 40f });

                        //// ========
                        //// 1st Line
                        //// ========
                        //tableLines.AddCell(new PdfPCell(new Phrase(line)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = -5f, Colspan = 4 });


                        //// ======
                        //// Header
                        //// ======
                        //tableLines.AddCell(new PdfPCell(new Phrase("Item", fontHelvetica10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                        //tableLines.AddCell(new PdfPCell(new Phrase("Unit", fontHelvetica10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                        //tableLines.AddCell(new PdfPCell(new Phrase("Balance", fontHelvetica10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });

                        //// ========
                        //// 2nd Line
                        //// ========
                        //tableLines.AddCell(new PdfPCell(new Phrase(line)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = -5f, Colspan = 4 });


                        if (category != null)
                        {
                            tableLines.AddCell(new PdfPCell(new Phrase(category, fontHelvetica10Bold)) { Colspan = 5, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                        }

                        if (inventories.Any())
                        {
                            foreach (var inventoryList in inventories.ToList())
                            {
                                tableLines.AddCell(new PdfPCell(new Phrase(inventoryList.ItemDescription, fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                tableLines.AddCell(new PdfPCell(new Phrase(inventoryList.Unit, fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                tableLines.AddCell(new PdfPCell(new Phrase(inventoryList.EndingQuantity.ToString("#,##0.00"), fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                            }
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



                document.Add(tableLines);

                document.Close();

                //ProcessStartInfo info = new ProcessStartInfo(fileName)
                //{
                //    Verb = "Print",
                //    CreateNoWindow = true,
                //    WindowStyle = ProcessWindowStyle.Hidden
                //};

                //Process printDwg = new Process
                //{
                //    StartInfo = info
                //};

                //printDwg.Start();
                //printDwg.Close();

                Process.Start(fileName);
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy ERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


