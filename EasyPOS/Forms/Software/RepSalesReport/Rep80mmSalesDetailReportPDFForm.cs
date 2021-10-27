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
    public partial class Rep80mmSalesDetailReportPDFForm : Form
    {
        public DateTime dateStart;
        public DateTime dateEnd;
        public Int32 filterTerminalId;
        public Int32 filterCustomerId;
        public Int32 filterSalesAgentId;
        public Int32 filterSupplierId;
        public Int32 filterItemId;


        public Rep80mmSalesDetailReportPDFForm(DateTime startDate, DateTime endDate, Int32 terminalId, Int32 CustomerId, Int32 SalesAgentId)
        {
            InitializeComponent();

            dateStart = startDate;
            dateEnd = endDate;
            filterTerminalId = terminalId;
            filterCustomerId = CustomerId;
            filterSalesAgentId = SalesAgentId;

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

                var fileName = "80mmSalesDetailReport" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                var currentUser = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;

                //float h = tableHeader.TotalHeight + tableLines.TotalHeight;
                var pgSize = new iTextSharp.text.Rectangle(270, 13999);
                Document document = new Document(pgSize);
                document.SetMargins(5f, 5f, 5f, 5f);

                PdfWriter pdfWriter = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));

                document.Open();

                var systemCurrent = Modules.SysCurrentModule.GetCurrentSettings();

                String companyName = systemCurrent.CompanyName;
                String documentTitle = "Sales Detail Report";

                PdfPTable tableHeader = new PdfPTable(1);
                tableHeader.SetWidths(new float[] { 70f });
                tableHeader.TotalWidth = 250f;
                tableHeader.SplitLate = false;
                tableHeader.SplitRows = true;
                tableHeader.AddCell(new PdfPCell(new Phrase(documentTitle, fontHelvetica14Bold)) { Border = 0, Padding = 3f, PaddingBottom = 1f, PaddingLeft = 55f });
                tableHeader.AddCell(new PdfPCell(new Phrase("\nFrom : " + dateStart.ToShortDateString() + " To: " + dateEnd.ToShortDateString() + "\n", fontHelvetica10)) { Colspan = 1, Border = 0, Padding = 3f, PaddingBottom = 5f, PaddingLeft = 40f });
                document.Add(tableHeader);

                PdfPTable tableLines = new PdfPTable(2);
                tableLines.SetWidths(new float[] { 100f, 70f });
                tableLines.TotalWidth = 250f;
                tableLines.SplitLate = false;
                tableLines.SplitRows = true;
                tableLines.AddCell(new PdfPCell(new Phrase("Item", fontHelvetica10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Amount", fontHelvetica10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });

                Controllers.RepSalesReportController repSalesReport = new Controllers.RepSalesReportController();
                var salesList = repSalesReport.SalesDetailReport(dateStart, dateEnd, filterTerminalId, filterCustomerId, filterSalesAgentId, filterSupplierId, filterItemId);

                Decimal totalItemAmount = 0;

                var salesLineItem = from s in db.TrnSalesLines
                                    where s.TrnSale.SalesDate >= dateStart
                                     && s.TrnSale.SalesDate <= dateEnd
                                     && s.TrnSale.TerminalId == filterTerminalId
                                     && s.TrnSale.IsLocked == true
                                     && s.TrnSale.IsCancelled == false
                                    select s;

                if (salesLineItem.Any())
                {
                    var categories = from d in salesLineItem
                                     group d by d.MstItem.Category
                                     into g
                                     select g.Key;

                    if (categories.ToList().Any())
                    {
                        Decimal categorySubTotal = 0;
                        foreach (var category in categories)
                        {
                            var items = from d in salesLineItem
                                        where d.MstItem.Category == category
                                        select d;
                            // Label Category
                            String Category = category;
                            tableLines.AddCell(new PdfPCell(new Phrase(Category, fontHelvetica10Bold)) { Colspan = 3, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                            if (items.Any())
                            {
                                Decimal itemTotal = 0;
                                Decimal subItemTotal = 0;

                                foreach (var item in items)
                                {
                                    subItemTotal = item.Amount;
                                    itemTotal += subItemTotal;
                                    // List of items
                                    tableLines.AddCell(new PdfPCell(new Phrase("\n" + item.MstItem.BarCode + " - " + item.MstItem.ItemDescription + " " + item.Quantity.ToString("#,##0.00") + " " + item.MstUnit.Unit, fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                    tableLines.AddCell(new PdfPCell(new Phrase("\n" + subItemTotal.ToString("#,##0.00"), fontHelvetica10)) { Border = 0, PaddingLeft = 50f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                }
                                totalItemAmount += itemTotal;
                                categorySubTotal = (0 * itemTotal) + itemTotal;
                            }

                            
                            tableLines.AddCell(new PdfPCell(new Phrase(line)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = -5f, Colspan = 4 });
                            tableLines.AddCell(new PdfPCell(new Phrase("Sub Total", fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                            tableLines.AddCell(new PdfPCell(new Phrase(categorySubTotal.ToString("#,##0.00"), fontHelvetica10)) { Border = 0, PaddingLeft = 40f, PaddingTop = 3f, PaddingBottom = 0f });
                            tableLines.AddCell(new PdfPCell(new Phrase(line)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = -5f, Colspan = 4 });
                        }

                        tableLines.AddCell(new PdfPCell(new Phrase(line)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = -5f, Colspan = 4 });
                        tableLines.AddCell(new PdfPCell(new Phrase("Total: ", fontArial11Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, Colspan = 1, HorizontalAlignment = 2 });
                        tableLines.AddCell(new PdfPCell(new Phrase(totalItemAmount.ToString("#,##0.00"), fontArial11Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, HorizontalAlignment = 2 });
                    }
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


