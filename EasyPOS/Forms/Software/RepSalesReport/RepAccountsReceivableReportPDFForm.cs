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
    public partial class RepAccountsReceivableReportPDFForm : Form
    {
        public DateTime dateAsOf = DateTime.Today;

        public RepAccountsReceivableReportPDFForm(DateTime _dateAsOf)
        {
            InitializeComponent();

            dateAsOf = _dateAsOf;
            PrintReport();
        }

        public Decimal ComputeAge(Int32 Age, Int32 Elapsed, Decimal Amount)
        {
            Decimal returnValue = 0;

            if (Age == 0)
            {
                if (Elapsed < 30)
                {
                    returnValue = Amount;
                }
            }
            else if (Age == 1)
            {
                if (Elapsed >= 30 && Elapsed < 60)
                {
                    returnValue = Amount;
                }
            }
            else if (Age == 2)
            {
                if (Elapsed >= 60 && Elapsed < 90)
                {
                    returnValue = Amount;
                }
            }
            else if (Age == 3)
            {
                if (Elapsed >= 90 && Elapsed < 120)
                {
                    returnValue = Amount;
                }
            }
            else if (Age == 4)
            {
                if (Elapsed >= 120)
                {
                    returnValue = Amount;
                }
            }
            else
            {
                returnValue = 0;
            }

            return returnValue;
        }

        public void PrintReport()
        {
            try
            {
                Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

                iTextSharp.text.Font fontTimesNewRoman10 = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9);
                iTextSharp.text.Font fontTimesNewRoman10Italic = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9, iTextSharp.text.Font.ITALIC);
                iTextSharp.text.Font fontTimesNewRoman10Bold = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font fontTimesNewRoman14Bold = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 11, iTextSharp.text.Font.BOLD);

                Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.5F, 100.0F, BaseColor.DARK_GRAY, Element.ALIGN_MIDDLE, 10F)));

                var fileName = "AccountsReceivableReport" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                var currentUser = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;

                var systemCurrent = Modules.SysCurrentModule.GetCurrentSettings();

                Document document = new Document(PageSize.LETTER.Rotate());
                document.SetMargins(30f, 30f, 100f, 30f);

                PdfWriter pdfWriter = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
                pdfWriter.PageEvent = new ConfigureHeaderFooter(dateAsOf);

                document.Open();

                List<Entities.DgvRepSalesReportAccountsReceivableSummaryReportListEntity> newSales = new List<Entities.DgvRepSalesReportAccountsReceivableSummaryReportListEntity>();

                var sales = from d in db.TrnSales
                            where d.SalesDate <= Convert.ToDateTime(dateAsOf)
                            && d.BalanceAmount > 0
                            && d.IsLocked == true
                            && d.IsTendered == false
                            && d.IsCancelled == false
                            select d;

                if (sales.Any())
                {
                    foreach (var sale in sales)
                    {
                        var ColumnSalesAmount = sale.Amount.ToString("#,##0.00");
                        var ColumnPaymentAmount = sale.PaidAmount.ToString("#,##0.00");
                        var ColumnBalanceAmount = sale.BalanceAmount.ToString("#,##0.00");
                        var ColumnDueDate = sale.SalesDate.AddDays(Convert.ToInt32(sale.MstTerm.NumberOfDays)).ToShortDateString();
                        var ColumnCurrent = ComputeAge(0, Convert.ToDateTime(dateAsOf).Subtract(sale.SalesDate.AddDays(Convert.ToInt32(sale.MstTerm.NumberOfDays))).Days, sale.BalanceAmount).ToString("#,##0.00");
                        var Column30Days = ComputeAge(1, Convert.ToDateTime(dateAsOf).Subtract(sale.SalesDate.AddDays(Convert.ToInt32(sale.MstTerm.NumberOfDays))).Days, sale.BalanceAmount).ToString("#,##0.00");
                        var Column60Days = ComputeAge(2, Convert.ToDateTime(dateAsOf).Subtract(sale.SalesDate.AddDays(Convert.ToInt32(sale.MstTerm.NumberOfDays))).Days, sale.BalanceAmount).ToString("#,##0.00");
                        var Column90Days = ComputeAge(3, Convert.ToDateTime(dateAsOf).Subtract(sale.SalesDate.AddDays(Convert.ToInt32(sale.MstTerm.NumberOfDays))).Days, sale.BalanceAmount).ToString("#,##0.00");
                        var Column120Days = ComputeAge(4, Convert.ToDateTime(dateAsOf).Subtract(sale.SalesDate.AddDays(Convert.ToInt32(sale.MstTerm.NumberOfDays))).Days, sale.BalanceAmount).ToString("#,##0.00");

                        newSales.Add(new Entities.DgvRepSalesReportAccountsReceivableSummaryReportListEntity
                        {
                            ColumnCustomer = sale.MstCustomer.Customer,
                            ColumnTerm = sale.MstTerm.Term,
                            ColumnCreditLimit = sale.MstCustomer.CreditLimit.ToString("#,##0.00"),
                            ColumnSalesNumber = sale.SalesNumber,
                            ColumnSalesDate = sale.SalesDate.ToShortDateString(),
                            ColumnSalesAmount = ColumnSalesAmount,
                            ColumnPaymentAmount = ColumnPaymentAmount,
                            ColumnBalanceAmount = ColumnBalanceAmount,
                            ColumnDueDate = ColumnDueDate,
                            ColumnCurrent = ColumnCurrent,
                            Column30Days = Column30Days,
                            Column60Days = Column60Days,
                            Column90Days = Column90Days,
                            Column120Days = Column120Days
                        });
                    }
                }

                var accountReceivables = from d in newSales select d;
                if (accountReceivables.Any())
                {
                    PdfPTable tableLines = new PdfPTable(14);
                    tableLines.SetWidths(new float[] { 100f, 50f, 60f, 70f, 55f, 60f, 60f, 60f, 60f, 60f, 60f, 60f, 60f, 60f });
                    tableLines.WidthPercentage = 100;

                    Decimal sales_amount = 0;
                    Decimal payment_amount = 0;
                    Decimal balance_amount = 0;
                    Decimal current_amount = 0;
                    Decimal _30Days_amount = 0;
                    Decimal _60Days_amount = 0;
                    Decimal _90Days_amount = 0;
                    Decimal _120Days_amount = 0;

                    foreach (var accountReceivable in accountReceivables)
                    {
                        tableLines.AddCell(new PdfPCell(new Phrase(accountReceivable.ColumnCustomer, fontTimesNewRoman10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                        tableLines.AddCell(new PdfPCell(new Phrase(accountReceivable.ColumnTerm, fontTimesNewRoman10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                        tableLines.AddCell(new PdfPCell(new Phrase(accountReceivable.ColumnCreditLimit, fontTimesNewRoman10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                        tableLines.AddCell(new PdfPCell(new Phrase(accountReceivable.ColumnSalesNumber, fontTimesNewRoman10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                        tableLines.AddCell(new PdfPCell(new Phrase(accountReceivable.ColumnSalesDate, fontTimesNewRoman10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                        tableLines.AddCell(new PdfPCell(new Phrase(accountReceivable.ColumnSalesAmount, fontTimesNewRoman10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                        tableLines.AddCell(new PdfPCell(new Phrase(accountReceivable.ColumnPaymentAmount, fontTimesNewRoman10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                        tableLines.AddCell(new PdfPCell(new Phrase(accountReceivable.ColumnBalanceAmount, fontTimesNewRoman10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                        tableLines.AddCell(new PdfPCell(new Phrase(accountReceivable.ColumnDueDate, fontTimesNewRoman10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                        tableLines.AddCell(new PdfPCell(new Phrase(accountReceivable.ColumnCurrent, fontTimesNewRoman10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                        tableLines.AddCell(new PdfPCell(new Phrase(accountReceivable.Column30Days, fontTimesNewRoman10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                        tableLines.AddCell(new PdfPCell(new Phrase(accountReceivable.Column60Days, fontTimesNewRoman10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                        tableLines.AddCell(new PdfPCell(new Phrase(accountReceivable.Column90Days, fontTimesNewRoman10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                        tableLines.AddCell(new PdfPCell(new Phrase(accountReceivable.Column120Days, fontTimesNewRoman10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });

                        sales_amount += Convert.ToDecimal(accountReceivable.ColumnSalesAmount);
                        payment_amount += Convert.ToDecimal(accountReceivable.ColumnPaymentAmount);
                        balance_amount += Convert.ToDecimal(accountReceivable.ColumnBalanceAmount);
                        current_amount += Convert.ToDecimal(accountReceivable.ColumnCurrent);
                        _30Days_amount += Convert.ToDecimal(accountReceivable.Column30Days);
                        _60Days_amount += Convert.ToDecimal(accountReceivable.Column60Days);
                        _90Days_amount += Convert.ToDecimal(accountReceivable.Column90Days);
                        _120Days_amount += Convert.ToDecimal(accountReceivable.Column120Days);
                    }

                    tableLines.AddCell(new PdfPCell(new Phrase(line)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = -5f, Colspan = 14 });
                    tableLines.AddCell(new PdfPCell(new Phrase("Total: ", fontTimesNewRoman10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, Colspan = 5, HorizontalAlignment = 2 });
                    tableLines.AddCell(new PdfPCell(new Phrase(sales_amount.ToString("#,##0.00"), fontTimesNewRoman10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, HorizontalAlignment = 2 });
                    tableLines.AddCell(new PdfPCell(new Phrase(payment_amount.ToString("#,##0.00"), fontTimesNewRoman10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, HorizontalAlignment = 2 });
                    tableLines.AddCell(new PdfPCell(new Phrase(balance_amount.ToString("#,##0.00"), fontTimesNewRoman10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, HorizontalAlignment = 2 });
                    tableLines.AddCell(new PdfPCell(new Phrase("", fontTimesNewRoman10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, HorizontalAlignment = 2 });
                    tableLines.AddCell(new PdfPCell(new Phrase(current_amount.ToString("#,##0.00"), fontTimesNewRoman10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, HorizontalAlignment = 2 });
                    tableLines.AddCell(new PdfPCell(new Phrase(_30Days_amount.ToString("#,##0.00"), fontTimesNewRoman10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, HorizontalAlignment = 2 });
                    tableLines.AddCell(new PdfPCell(new Phrase(_60Days_amount.ToString("#,##0.00"), fontTimesNewRoman10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, HorizontalAlignment = 2 });
                    tableLines.AddCell(new PdfPCell(new Phrase(_90Days_amount.ToString("#,##0.00"), fontTimesNewRoman10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, HorizontalAlignment = 2 });
                    tableLines.AddCell(new PdfPCell(new Phrase(_120Days_amount.ToString("#,##0.00"), fontTimesNewRoman10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 20f, HorizontalAlignment = 2 });

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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy ERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        class ConfigureHeaderFooter : PdfPageEventHelper
        {
            public DateTime dateAsOf = DateTime.Today;
            public Data.easyposdbDataContext db;

            public ConfigureHeaderFooter(DateTime _dateAsOf)
            {
                dateAsOf = _dateAsOf;
                db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                iTextSharp.text.Font fontTimesNewRoman10 = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10);
                iTextSharp.text.Font fontTimesNewRoman10Bold = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font fontTimesNewRoman14Bold = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 14, iTextSharp.text.Font.BOLD);

                Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0F, 100.0F, BaseColor.BLACK, Element.ALIGN_MIDDLE, 7F)));

                var systemCurrent = Modules.SysCurrentModule.GetCurrentSettings();

                String companyName = systemCurrent.CompanyName;
                String documentTitle = "Accounts Receivable";

                PdfPTable tableHeader = new PdfPTable(4);
                tableHeader.SetWidths(new float[] { 20f, 30f, 20f, 50f });
                tableHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tableHeader.AddCell(new PdfPCell(new Phrase(companyName, fontTimesNewRoman14Bold)) { Colspan = 2, Border = 0, Padding = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase(documentTitle, fontTimesNewRoman14Bold)) { HorizontalAlignment = 2, Colspan = 2, Border = 0, Padding = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase("Date as of: " + dateAsOf.ToShortDateString() + "\n", fontTimesNewRoman10)) { Colspan = 4, Border = 0, Padding = 3f, PaddingBottom = -5f });

                PdfPTable tableLines = new PdfPTable(14);
                tableLines.SetWidths(new float[] { 100f, 50f, 60f, 70f, 50f, 60f, 60f, 60f, 50f, 60f, 60f, 60f, 60f, 60f });
                tableLines.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tableLines.AddCell(new PdfPCell(new Phrase(" \n", fontTimesNewRoman10Bold)) { Border = 0, Colspan = 14, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Customer", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Term", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Credit Limit", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Sales No.", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Date", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Amount", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Payment", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Balance", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Due Date", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Current", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("30 Days", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("60 Days", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("90 Days", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("120 Days", fontTimesNewRoman10Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });

                tableHeader.AddCell(new PdfPCell(tableLines) { Border = 0, Colspan = 6, PaddingBottom = -5f, PaddingLeft = 0f, PaddingRight = 0f });
                tableHeader.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetTop(document.TopMargin) + 67f, writer.DirectContent);
            }
        }
    }
}


