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
    public partial class Rep80mmCollectionDetailReportPDFForm : Form
    {
        public DateTime dateStart;
        public DateTime dateEnd;
        public Int32 filterTerminalId;
        

        public Rep80mmCollectionDetailReportPDFForm(DateTime startDate, DateTime endDate, Int32 terminalId)
        {
            InitializeComponent();

            dateStart = startDate;
            dateEnd = endDate;
            filterTerminalId = terminalId;
           
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

                var fileName = "80mmCollectionDetailReport" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                var currentUser = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;

                //float h = tableHeader.TotalHeight + tableLines.TotalHeight;
                var pgSize = new iTextSharp.text.Rectangle(270, 13999);
                Document document = new Document(pgSize);
                document.SetMargins(5f, 5f, 5f, 5f);

                PdfWriter pdfWriter = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));

                document.Open();

                var systemCurrent = Modules.SysCurrentModule.GetCurrentSettings();

                String companyName = systemCurrent.CompanyName;
                String documentTitle = "Collection Detail Report";

                PdfPTable tableHeader = new PdfPTable(1);
                tableHeader.SetWidths(new float[] { 70f });
                tableHeader.TotalWidth = 250f;
                tableHeader.SplitLate = false;
                tableHeader.SplitRows = true;
                tableHeader.AddCell(new PdfPCell(new Phrase(documentTitle, fontHelvetica14Bold)) { Colspan = 1, Border = 0, Padding = 3f, PaddingBottom = 1f, PaddingLeft = 40f });
                tableHeader.AddCell(new PdfPCell(new Phrase("\n  From : " + dateStart.ToShortDateString() + " To: " + dateEnd.ToShortDateString() + "\n", fontHelvetica10)) { Colspan = 1, Border = 0, Padding = 3f, PaddingBottom = 5f, PaddingLeft = 40f });
                document.Add(tableHeader);

                PdfPTable tableLines = new PdfPTable(3);
                tableLines.SetWidths(new float[] { 70f, 70f, 70f });
                tableLines.TotalWidth = 250f;
                tableLines.SplitLate = false;
                tableLines.SplitRows = true;
                tableLines.AddCell(new PdfPCell(new Phrase("Date", fontHelvetica10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("OR No.", fontHelvetica10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Amount", fontHelvetica10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });

                Controllers.RepSalesReportController repSalesSummaryReportController = new Controllers.RepSalesReportController();
                var salesList = repSalesSummaryReportController.CollectionDetailReport(dateStart, dateEnd, filterTerminalId);

                Decimal totalAmount = 0;
                Decimal totalCollectionCount = 0;
                Decimal subCollectionCount = 0;

                if (salesList.Any())
                {
                    Decimal CollectionCount = 0;
                    foreach (var sales in salesList)
                    {
                        CollectionCount += 1;
                        tableLines.AddCell(new PdfPCell(new Phrase(sales.CollectionDate, fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                        tableLines.AddCell(new PdfPCell(new Phrase(sales.CollectionNumber, fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                        tableLines.AddCell(new PdfPCell(new Phrase(sales.Amount.ToString("#,##0.00"), fontHelvetica10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });

                        totalAmount += Convert.ToDecimal(sales.Amount);
                    }
                    subCollectionCount = (0 * CollectionCount) + CollectionCount;
                    totalCollectionCount += CollectionCount;
                }

                tableLines.AddCell(new PdfPCell(new Phrase(line)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = -5f, Colspan = 4 });
                tableLines.AddCell(new PdfPCell(new Phrase(totalCollectionCount.ToString(), fontArial11Bold)) { Colspan = 0, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, HorizontalAlignment = 2 });
                tableLines.AddCell(new PdfPCell(new Phrase("Total: ", fontArial11Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, Colspan = 1, HorizontalAlignment = 2 });
                tableLines.AddCell(new PdfPCell(new Phrase(totalAmount.ToString("#,##0.00"), fontArial11Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, HorizontalAlignment = 2 });
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


