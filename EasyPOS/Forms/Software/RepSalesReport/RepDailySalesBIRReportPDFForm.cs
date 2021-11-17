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
    public partial class RepDailySalesBIRReportPDFForm : Form
    {
        public DateTime dateStart;
        public DateTime dateEnd;
        public Int32 filterTerminalId;

        public RepDailySalesBIRReportPDFForm(DateTime startDate, DateTime endDate, Int32 terminalId)
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

                iTextSharp.text.Font fontTimesNewRoman7 = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 7);
                iTextSharp.text.Font fontTimesNewRoman10 = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10);
                iTextSharp.text.Font fontTimesNewRoman10Italic = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, iTextSharp.text.Font.ITALIC);
                iTextSharp.text.Font fontTimesNewRoman10Bold = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font fontTimesNewRoman14Bold = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 14, iTextSharp.text.Font.BOLD);

                Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.5F, 100.0F, BaseColor.DARK_GRAY, Element.ALIGN_MIDDLE, 10F)));

                var fileName = "DailySalesBIRReport" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                var currentUser = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;

                var systemCurrent = Modules.SysCurrentModule.GetCurrentSettings();

                Document document = new Document(PageSize.LETTER.Rotate());
                document.SetMargins(30f, 30f, 100f, 30f);

                PdfWriter pdfWriter = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
                pdfWriter.PageEvent = new ConfigureHeaderFooter(dateStart, dateEnd, filterTerminalId);

                document.Open();

                String terminalNumber = "";

                var terminal = from d in db.MstTerminals
                               where d.Id == filterTerminalId
                               select d;

                if (terminal.Any())
                {
                    terminalNumber = terminal.FirstOrDefault().Terminal;
                }

                Controllers.RepSalesReportController repSalesSummaryReportController = new Controllers.RepSalesReportController();
                var collectionList = repSalesSummaryReportController.CollectionSummaryReport(dateStart, dateEnd, filterTerminalId);

                if (collectionList.Any())
                {
                    PdfPTable tableLines = new PdfPTable(12);
                    tableLines.SetWidths(new float[] { 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f });
                    tableLines.WidthPercentage = 100;

                    var groupCollection = from d in collectionList.OrderBy(d => d.CollectionDate)
                                          group d by d.CollectionDate
                                          into g
                                          select g;

                    if (groupCollection.ToList().Any())
                    {
                        Decimal totalOverAllGrossSales = 0;
                        Decimal totalOverAllRegularDiscount = 0;
                        Decimal totalOverAllSeniorCitizenDiscount = 0;
                        Decimal totalOverAllPWDDiscount = 0;
                        Decimal totalOverAllVATSales = 0;
                        Decimal totalOverAllVATAmount = 0;
                        Decimal totalOverAllNonVATSales = 0;
                        Decimal totalOverAllVATExemptSales = 0;
                        Decimal totalOverAllVATZeroRatedSales = 0;

                        foreach (var data in groupCollection.ToList())
                        {
                            var currentCollection = from d in collectionList
                                                    where d.CollectionDate == data.Key
                                                    select d;

                            if (currentCollection.Any())
                            {
                                var startCollectionNumber = data.OrderBy(d => d.Id).FirstOrDefault().CollectionNumber;
                                var endCollectionNumber = data.FirstOrDefault().CollectionNumber;

                                var amount = currentCollection.Sum(d => d.Amount);

                                Decimal totalGrossSales = 0;
                                Decimal totalRegularDiscount = 0;
                                Decimal totalSeniorCitizenDiscount = 0;
                                Decimal totalPWDDiscount = 0;
                                Decimal totalVATSales = 0;
                                Decimal totalVATAmount = 0;
                                Decimal totalNonVATSales = 0;
                                Decimal totalVATExemptSales = 0;
                                Decimal totalVATZeroRatedSales = 0;

                                foreach (var dataCurrentCollection in currentCollection.ToList())
                                {
                                    var currenCollectionData = from d in db.TrnCollections
                                                               where d.Id == dataCurrentCollection.Id
                                                               select d;

                                    if (currenCollectionData.Any())
                                    {
                                        if (currenCollectionData.FirstOrDefault().SalesId != null)
                                        {
                                            var salesLines = from d in db.TrnSalesLines
                                                             where d.SalesId == currenCollectionData.FirstOrDefault().SalesId
                                                             select d;

                                            if (salesLines.Any())
                                            {
                                                Decimal VATAmountValue = salesLines.Sum(d =>
                                                    d.MstTax.Code == "EXEMPTVAT" ? ((d.Price * d.Quantity) / (1 + (d.MstItem.MstTax1.Rate / 100)) * (d.MstItem.MstTax1.Rate / 100)) : d.TaxAmount
                                                );
                                                totalGrossSales += salesLines.Sum(d => d.Price * d.Quantity) - VATAmountValue;

                                                totalRegularDiscount += salesLines.Sum(d =>
                                                    d.MstDiscount.Discount != "Senior Citizen Discount" && d.MstDiscount.Discount != "PWD" ? d.DiscountAmount * d.Quantity : 0
                                                );

                                                totalSeniorCitizenDiscount += salesLines.Sum(d =>
                                                    d.MstDiscount.Discount == "Senior Citizen Discount" ? d.DiscountAmount * d.Quantity : 0
                                                );

                                                totalPWDDiscount += salesLines.Sum(d =>
                                                    d.MstDiscount.Discount == "PWD" ? d.DiscountAmount * d.Quantity : 0
                                                );

                                                totalVATSales += salesLines.Sum(d =>
                                                    d.MstTax.Code == "VAT" ? d.Amount - (d.Amount / (1 + (d.MstTax.Rate / 100)) * (d.MstTax.Rate / 100)) : 0
                                                );

                                                totalVATAmount += salesLines.Sum(d =>
                                                    d.MstTax.Code == "EXEMPTVAT" ? 0 : d.TaxAmount
                                                );

                                                totalNonVATSales += salesLines.Sum(d =>
                                                    d.MstTax.Code == "NONVAT" ? d.Amount : 0
                                                );

                                                totalVATExemptSales += salesLines.Sum(d =>
                                                    d.MstTax.Code == "EXEMPTVAT" ? ((d.Price * d.Quantity) - ((d.Price * d.Quantity) / (1 + (d.MstItem.MstTax1.Rate / 100)) * (d.MstItem.MstTax1.Rate / 100))) : 0
                                                );

                                                totalVATZeroRatedSales += salesLines.Sum(d =>
                                                    d.MstTax.Code == "ZEROVAT" ? d.Amount : 0
                                                );
                                            }
                                        }
                                    }
                                }

                                tableLines.AddCell(new PdfPCell(new Phrase(data.Key, fontTimesNewRoman10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                tableLines.AddCell(new PdfPCell(new Phrase(startCollectionNumber, fontTimesNewRoman10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                tableLines.AddCell(new PdfPCell(new Phrase(endCollectionNumber, fontTimesNewRoman10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                tableLines.AddCell(new PdfPCell(new Phrase(totalGrossSales.ToString("#,##0.00"), fontTimesNewRoman10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                tableLines.AddCell(new PdfPCell(new Phrase(totalRegularDiscount.ToString("#,##0.00"), fontTimesNewRoman10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                tableLines.AddCell(new PdfPCell(new Phrase(totalSeniorCitizenDiscount.ToString("#,##0.00"), fontTimesNewRoman10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                tableLines.AddCell(new PdfPCell(new Phrase(totalPWDDiscount.ToString("#,##0.00"), fontTimesNewRoman10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                tableLines.AddCell(new PdfPCell(new Phrase(totalVATSales.ToString("#,##0.00"), fontTimesNewRoman10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                tableLines.AddCell(new PdfPCell(new Phrase(totalVATAmount.ToString("#,##0.00"), fontTimesNewRoman10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                tableLines.AddCell(new PdfPCell(new Phrase(totalNonVATSales.ToString("#,##0.00"), fontTimesNewRoman10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                tableLines.AddCell(new PdfPCell(new Phrase((totalVATExemptSales - totalSeniorCitizenDiscount - totalPWDDiscount).ToString("#,##0.00"), fontTimesNewRoman10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                tableLines.AddCell(new PdfPCell(new Phrase(totalVATZeroRatedSales.ToString("#,##0.00"), fontTimesNewRoman10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });

                                totalOverAllGrossSales += totalGrossSales;
                                totalOverAllRegularDiscount += totalRegularDiscount;
                                totalOverAllSeniorCitizenDiscount += totalSeniorCitizenDiscount;
                                totalOverAllPWDDiscount += totalPWDDiscount;
                                totalOverAllVATSales += totalVATSales;
                                totalOverAllVATAmount += totalVATAmount;
                                totalOverAllNonVATSales += totalNonVATSales;
                                totalOverAllVATExemptSales += totalVATExemptSales - totalSeniorCitizenDiscount - totalPWDDiscount;
                                totalOverAllVATZeroRatedSales += totalVATZeroRatedSales;
                            }
                        }

                        tableLines.AddCell(new PdfPCell(new Phrase(line)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = -5f, Colspan = 12 });
                        tableLines.AddCell(new PdfPCell(new Phrase("Total: ", fontTimesNewRoman10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, Colspan = 3, HorizontalAlignment = 2 });
                        tableLines.AddCell(new PdfPCell(new Phrase(totalOverAllGrossSales.ToString("#,##0.00"), fontTimesNewRoman10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, HorizontalAlignment = 2 });
                        tableLines.AddCell(new PdfPCell(new Phrase(totalOverAllRegularDiscount.ToString("#,##0.00"), fontTimesNewRoman10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, HorizontalAlignment = 2 });
                        tableLines.AddCell(new PdfPCell(new Phrase(totalOverAllSeniorCitizenDiscount.ToString("#,##0.00"), fontTimesNewRoman10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, HorizontalAlignment = 2 });
                        tableLines.AddCell(new PdfPCell(new Phrase(totalOverAllPWDDiscount.ToString("#,##0.00"), fontTimesNewRoman10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, HorizontalAlignment = 2 });
                        tableLines.AddCell(new PdfPCell(new Phrase(totalOverAllVATSales.ToString("#,##0.00"), fontTimesNewRoman10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, HorizontalAlignment = 2 });
                        tableLines.AddCell(new PdfPCell(new Phrase(totalOverAllVATAmount.ToString("#,##0.00"), fontTimesNewRoman10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, HorizontalAlignment = 2 });
                        tableLines.AddCell(new PdfPCell(new Phrase(totalOverAllNonVATSales.ToString("#,##0.00"), fontTimesNewRoman10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, HorizontalAlignment = 2 });
                        tableLines.AddCell(new PdfPCell(new Phrase(totalOverAllVATExemptSales.ToString("#,##0.00"), fontTimesNewRoman10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, HorizontalAlignment = 2 });
                        tableLines.AddCell(new PdfPCell(new Phrase(totalOverAllVATZeroRatedSales.ToString("#,##0.00"), fontTimesNewRoman10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, HorizontalAlignment = 2 });
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy ERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        class ConfigureHeaderFooter : PdfPageEventHelper
        {
            public DateTime dateStart;
            public DateTime dateEnd;
            public Int32 filterTerminalId;

            public Data.easyposdbDataContext db;

            public ConfigureHeaderFooter(DateTime startDate, DateTime endDate, Int32 terminalId)
            {
                dateStart = startDate;
                dateEnd = endDate;
                filterTerminalId = terminalId;
                db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                iTextSharp.text.Font fontTimesNewRoman7 = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 7);
                iTextSharp.text.Font fontTimesNewRoman10 = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10);
                iTextSharp.text.Font fontTimesNewRoman10Bold = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font fontTimesNewRoman14Bold = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 14, iTextSharp.text.Font.BOLD);

                Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0F, 100.0F, BaseColor.BLACK, Element.ALIGN_MIDDLE, 7F)));

                var systemCurrent = Modules.SysCurrentModule.GetCurrentSettings();

                String companyName = systemCurrent.CompanyName;
                String documentTitle = "Daily Sales Report";

                PdfPTable tableHeader = new PdfPTable(4);
                tableHeader.SetWidths(new float[] { 20f, 30f, 20f, 50f });
                tableHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tableHeader.AddCell(new PdfPCell(new Phrase(companyName, fontTimesNewRoman14Bold)) { Colspan = 2, Border = 0, Padding = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase(documentTitle, fontTimesNewRoman14Bold)) { HorizontalAlignment = 2, Colspan = 2, Border = 0, Padding = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase("Terminal: " + filterTerminalId, fontTimesNewRoman10)) { HorizontalAlignment = 2, Colspan = 2, Border = 0, Padding = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase("From : " + dateStart.ToShortDateString() + " To: " + dateEnd.ToShortDateString() + "\n", fontTimesNewRoman10)) { Colspan = 4, Border = 0, Padding = 3f, PaddingBottom = -5f });

                String columnName = "";
                if (Modules.SysCurrentModule.GetCurrentSettings().ORPrintTitle == "O F F I C I A L  R E C E I P T")
                {
                    columnName = "OR";
                }
                else
                {
                    columnName = "Sales Invoice";
                }

                PdfPTable tableLines = new PdfPTable(12);
                tableLines.SetWidths(new float[] { 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f, 50f });
                tableLines.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tableLines.AddCell(new PdfPCell(new Phrase(columnName + " Date", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("From", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("To", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Gross Sales", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Regular Discount", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Senior Discount", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("PWD Discount", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("VAT Sales", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("VAT Amount", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Non VAT", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("VAT Exempt", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("VAT Zero Rated", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableHeader.AddCell(new PdfPCell(tableLines) { Border = 0, Colspan = 4, PaddingBottom = -5f, PaddingLeft = 0f, PaddingRight = 0f });
                tableHeader.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetTop(document.TopMargin) + 67f, writer.DirectContent);
            }
        }
    }
}
