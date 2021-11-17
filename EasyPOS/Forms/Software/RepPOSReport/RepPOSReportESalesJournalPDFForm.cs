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

namespace EasyPOS.Forms.Software.RepPOSReport
{
    public partial class RepPOSReportESalesJournalPDFForm : Form
    {
        public Int32 terminalId = 0;
        public DateTime startDate = DateTime.Today;
        public DateTime endDate = DateTime.Today;

        public RepPOSReportESalesJournalPDFForm(Int32 _terminalId, DateTime _startDate, DateTime _endDate)
        {
            InitializeComponent();

            terminalId = _terminalId;
            startDate = _startDate;
            endDate = _endDate;

            PrintReport();
        }

        public void PrintReport()
        {
            try
            {
                Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

                iTextSharp.text.Font fontTimesNewRoman10 = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9);
                iTextSharp.text.Font fontTimesNewRoman7 = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 7);
                iTextSharp.text.Font fontTimesNewRoman10Italic = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9, iTextSharp.text.Font.ITALIC);
                iTextSharp.text.Font fontTimesNewRoman10Bold = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font fontTimesNewRoman14Bold = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 11, iTextSharp.text.Font.BOLD);

                Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.5F, 100.0F, BaseColor.DARK_GRAY, Element.ALIGN_MIDDLE, 10F)));

                var fileName = "E Sales Journal Report" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                var currentUser = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;

                var systemCurrent = Modules.SysCurrentModule.GetCurrentSettings();

                Document document = new Document(PageSize.LETTER);
                document.SetMargins(30f, 30f, 30f, 30f);

                PdfWriter pdfWriter = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));

                document.Open();

                String companyName = systemCurrent.CompanyName;
                String documentTitle = "E-Sales Journal";
                String terminalNumber = "";

                var terminal = from d in db.MstTerminals
                               where d.Id == terminalId
                               select d;

                if (terminal.Any())
                {
                    terminalNumber = terminal.FirstOrDefault().Terminal;
                }

                PdfPTable tableHeader = new PdfPTable(1);
                tableHeader.SetWidths(new float[] { 100f });
                tableHeader.WidthPercentage = 100;
                tableHeader.AddCell(new PdfPCell(new Phrase(documentTitle, fontTimesNewRoman14Bold)) { Border = 0, Padding = 3f, PaddingBottom = 0f });
                tableHeader.AddCell(new PdfPCell(new Phrase("Terminal: " + terminalNumber, fontTimesNewRoman10)) { Border = 0, Padding = 3f, PaddingBottom = 0f });
                tableHeader.AddCell(new PdfPCell(new Phrase("From " + startDate.ToShortDateString() + " To " + endDate.ToShortDateString(), fontTimesNewRoman10)) { Border = 0, Padding = 3f, PaddingBottom = 6f });
                document.Add(tableHeader);

                PdfPTable tableLines = new PdfPTable(5);
                tableLines.SetWidths(new float[] { 10f, 20f, 20f, 20f, 30f });
                tableLines.WidthPercentage = 100;
                tableLines.AddCell(new PdfPCell(new Phrase("Date", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Counter Start ID", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Counter End ID", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Amount", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase(" ", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f, Border = 0 });

                var counterCollections = from d in db.TrnCollections
                                         where d.TerminalId == terminalId
                                         && d.CollectionDate >= startDate
                                         && d.CollectionDate <= endDate
                                         && d.IsLocked == true
                                         select d;

                if (counterCollections.Any())
                {
                    var counterCollectionsGroupedByDates = from d in counterCollections
                                                           group d by d.CollectionDate into g
                                                           select g;

                    foreach (var counterCollectionsGroupedByDate in counterCollectionsGroupedByDates.OrderBy(d => d.Key))
                    {
                        var currentCounter = counterCollections.Where(d => d.CollectionDate == counterCollectionsGroupedByDate.Key);
                        if (currentCounter.Any())
                        {
                            var amount = currentCounter.Sum(d => d.Amount);
                            var startCounterID = currentCounter.OrderBy(d => d.Id).FirstOrDefault().CollectionNumber;
                            var endCounterID = currentCounter.OrderByDescending(d => d.Id).FirstOrDefault().CollectionNumber;

                            tableLines.AddCell(new PdfPCell(new Phrase(counterCollectionsGroupedByDate.Key.ToShortDateString(), fontTimesNewRoman10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                            tableLines.AddCell(new PdfPCell(new Phrase(startCounterID, fontTimesNewRoman10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                            tableLines.AddCell(new PdfPCell(new Phrase(endCounterID, fontTimesNewRoman10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                            tableLines.AddCell(new PdfPCell(new Phrase(amount.ToString("#,##0.00"), fontTimesNewRoman10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                            tableLines.AddCell(new PdfPCell(new Phrase("", fontTimesNewRoman10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                        }
                    }
                }

                document.Add(tableLines);
                 PdfPTable tableFooter = new PdfPTable(1);
                tableFooter.SetWidths(new float[] { 1000f });
                tableFooter.WidthPercentage = 100;
                tableFooter.AddCell(new PdfPCell(new Phrase(Modules.SysCurrentModule.GetCurrentSettings().ZReadingFooter, fontTimesNewRoman7)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 50f, PaddingBottom = 20f, HorizontalAlignment = 1 });
                document.Add(tableFooter);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy ERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


