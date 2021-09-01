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

namespace EasyPOS.Forms.Software.TrnCollection
{
    public partial class TrnCollectionDetailReportForm : Form
    {
        Int32 _collectionId = 0;
        public TrnCollectionDetailReportForm(Int32 collectionId)
        {
            _collectionId = collectionId;
            InitializeComponent();
            PrintCollectionDetailReport();
        }

        public void PrintCollectionDetailReport()
        {
            try
            {
                Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

                iTextSharp.text.Font fontTimesNewRoman9 = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9);
                iTextSharp.text.Font fontTimesNewRoman9Bold = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font fontTimesNewRoman10 = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10);
                iTextSharp.text.Font fontTimesNewRoman10Italic = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, iTextSharp.text.Font.ITALIC);
                iTextSharp.text.Font fontTimesNewRoman11 = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 11);
                iTextSharp.text.Font fontTimesNewRoman11Bold = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 11, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font fontTimesNewRoman14Bold = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 14, iTextSharp.text.Font.BOLD);

                Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.5F, 100.0F, BaseColor.DARK_GRAY, Element.ALIGN_MIDDLE, 10F)));

                var fileName = "CollectionDetailReport" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                var currentUser = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;

                var systemCurrent = Modules.SysCurrentModule.GetCurrentSettings();

                Document document = new Document(PageSize.LETTER.Rotate());
                document.SetMargins(30f, 30f, 127f, 30f);

                PdfWriter pdfWriter = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
                pdfWriter.PageEvent = new CollectionDetailReportHeaderFooter(_collectionId);

                document.Open();

                var collectionLines = from d in db.TrnCollectionLines
                                      where d.CollectionId == _collectionId
                                      select d;
                if (collectionLines.Any())
                {
                    PdfPTable tableItem = new PdfPTable(13);
                    tableItem.SetWidths(new float[] { 40f, 50, 50f, 40f, 30f, 30f, 50f, 50f, 50f, 30f, 30f, 30f, 30f });
                    tableItem.WidthPercentage = 100;

                    foreach (var CollectionLines in collectionLines)
                    {
                        tableItem.AddCell(new PdfPCell(new Phrase(CollectionLines.MstPayType.PayType, fontTimesNewRoman10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 50f, PaddingBottom = -25f });
                        tableItem.AddCell(new PdfPCell(new Phrase(CollectionLines.Amount.ToString("#,##0.00"), fontTimesNewRoman10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 8f, PaddingTop = 50f, PaddingBottom = -25f });
                        tableItem.AddCell(new PdfPCell(new Phrase(CollectionLines.CheckNumber, fontTimesNewRoman10)) { Border = 0, PaddingLeft = 8f, PaddingRight = 3f, PaddingTop = 50f, PaddingBottom = -25f });
                        tableItem.AddCell(new PdfPCell(new Phrase(CollectionLines.CheckDate.ToString(), fontTimesNewRoman10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 50f, PaddingBottom = -25f });
                        tableItem.AddCell(new PdfPCell(new Phrase(CollectionLines.CheckBank, fontTimesNewRoman10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 50f, PaddingBottom = -25f });
                        tableItem.AddCell(new PdfPCell(new Phrase(CollectionLines.GiftCertificateNumber, fontTimesNewRoman10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 50f, PaddingBottom = -25f });
                        tableItem.AddCell(new PdfPCell(new Phrase(CollectionLines.OtherInformation, fontTimesNewRoman10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 8f, PaddingTop = 50f, PaddingBottom = -25f });
                        tableItem.AddCell(new PdfPCell(new Phrase(CollectionLines.CreditCardNumber, fontTimesNewRoman10)) { Border = 0, PaddingLeft = 8f, PaddingRight = 3f, PaddingTop = 50f, PaddingBottom = -25f });
                        tableItem.AddCell(new PdfPCell(new Phrase(CollectionLines.CreditCardHolderName, fontTimesNewRoman10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 50f, PaddingBottom = -25f });
                        tableItem.AddCell(new PdfPCell(new Phrase(CollectionLines.CreditCardReferenceNumber, fontTimesNewRoman10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 50f, PaddingBottom = -25f });
                        tableItem.AddCell(new PdfPCell(new Phrase(CollectionLines.CreditCardType, fontTimesNewRoman10)) { HorizontalAlignment = 2,  Border = 0, PaddingLeft = 3f, PaddingRight = 8f, PaddingTop = 50f, PaddingBottom = -25f });
                        tableItem.AddCell(new PdfPCell(new Phrase(CollectionLines.CreditCardExpiry, fontTimesNewRoman10)) { Border = 0, PaddingLeft = 8f, PaddingRight = 3f, PaddingTop = 50f, PaddingBottom = -25f });
                        tableItem.AddCell(new PdfPCell(new Phrase(CollectionLines.CreditCardVerificationCode, fontTimesNewRoman9)) { HorizontalAlignment = 2 , Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 50f, PaddingBottom = 10f });

                    }
                    tableItem.AddCell(new PdfPCell(new Phrase(line)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 30f, PaddingBottom = 5f, Colspan = 13 });

                    document.Add(tableItem);

                    PdfPTable tableUsers = new PdfPTable(3);
                    tableUsers.SetWidths(new float[] { 100f, 100f, 100f });
                    tableUsers.WidthPercentage = 100;
                    tableUsers.AddCell(new PdfPCell(new Phrase("Prepared by", fontTimesNewRoman11Bold)) { PaddingTop = 5f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                    tableUsers.AddCell(new PdfPCell(new Phrase("Checked by", fontTimesNewRoman11Bold)) { PaddingTop = 5f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                    tableUsers.AddCell(new PdfPCell(new Phrase("Approved by", fontTimesNewRoman11Bold)) { PaddingTop = 5f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                    tableUsers.AddCell(new PdfPCell(new Phrase(" ")) { PaddingBottom = 30f });
                    tableUsers.AddCell(new PdfPCell(new Phrase(" ")) { PaddingBottom = 30f });
                    tableUsers.AddCell(new PdfPCell(new Phrase(" ")) { PaddingBottom = 30f });
                    tableUsers.AddCell(new PdfPCell(new Phrase(collectionLines.FirstOrDefault().TrnCollection.MstUser.FullName, fontTimesNewRoman11)) { HorizontalAlignment = 1, PaddingTop = 5f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                    tableUsers.AddCell(new PdfPCell(new Phrase(collectionLines.FirstOrDefault().TrnCollection.MstUser1.FullName, fontTimesNewRoman11)) { HorizontalAlignment = 1, PaddingTop = 5f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                    tableUsers.AddCell(new PdfPCell(new Phrase(collectionLines.FirstOrDefault().TrnCollection.MstUser2.FullName, fontTimesNewRoman11)) { HorizontalAlignment = 1, PaddingTop = 5f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                    document.Add(tableUsers);

                    document.Close();
                    Process.Start(fileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy ERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        class CollectionDetailReportHeaderFooter : PdfPageEventHelper
        {
            public Int32 _collectionId = 0;
            public Data.easyposdbDataContext db;

            public CollectionDetailReportHeaderFooter(Int32 collectionId)
            {
                _collectionId = collectionId;
                db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                iTextSharp.text.Font fontTimesNewRoman9 = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9);
                iTextSharp.text.Font fontTimesNewRoman9Bold = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 9, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font fontTimesNewRoman10 = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10);
                iTextSharp.text.Font fontTimesNewRoman10Bold = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font fontTimesNewRoman10Italic = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 10, iTextSharp.text.Font.ITALIC);
                iTextSharp.text.Font fontTimesNewRoman11 = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 11);
                iTextSharp.text.Font fontTimesNewRoman11Bold = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 11, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font fontTimesNewRoman14Bold = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 14, iTextSharp.text.Font.BOLD);

                Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0F, 100.0F, BaseColor.BLACK, Element.ALIGN_MIDDLE, 7F)));

                var systemCurrent = Modules.SysCurrentModule.GetCurrentSettings();

                var collection = from d in db.TrnCollections
                                 where d.Id == _collectionId
                                 select d;

                String companyName = systemCurrent.CompanyName;
                String address = systemCurrent.Address;
                String documentTitle = "Collection Report";

                String collectionNumber = collection.FirstOrDefault().CollectionNumber;
                String collectionDate = collection.FirstOrDefault().CollectionDate.ToLongDateString();
                String manualORNumber = collection.FirstOrDefault().ManualORNumber;
                String terminal = collection.FirstOrDefault().MstTerminal.Terminal;
                String customer = collection.FirstOrDefault().MstCustomer.Customer;
                String salesNumber = collection.FirstOrDefault().TrnSale.SalesNumber;
                String salesBalance = collection.FirstOrDefault().SalesBalanceAmount.ToString();
                String remarks = collection.FirstOrDefault().Remarks;


                PdfPTable tableHeader = new PdfPTable(4);
                tableHeader.SetWidths(new float[] { 20f, 30f, 20f, 50f });
                tableHeader.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

                tableHeader.AddCell(new PdfPCell(new Phrase(companyName + "\n", fontTimesNewRoman14Bold)) { Colspan = 2, Border = 0, Padding = 3f, PaddingBottom = 0f });
                tableHeader.AddCell(new PdfPCell(new Phrase(documentTitle + "\n", fontTimesNewRoman14Bold)) { HorizontalAlignment = 2, Colspan = 2, Border = 0, Padding = 3f, PaddingBottom = 0f });
                tableHeader.AddCell(new PdfPCell(new Phrase(address + "\n", fontTimesNewRoman11)) { Colspan = 4, Border = PdfPCell.BOTTOM_BORDER, Padding = 3f, PaddingBottom = 3f });

                tableHeader.AddCell(new PdfPCell(new Phrase("Collection No.: ", fontTimesNewRoman11Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase(collectionNumber, fontTimesNewRoman11)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });

                tableHeader.AddCell(new PdfPCell(new Phrase("Collection Date: ", fontTimesNewRoman11Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase(collectionDate, fontTimesNewRoman11)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase("Manual OR Number: ", fontTimesNewRoman11Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase(manualORNumber, fontTimesNewRoman11)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase("Terminal: ", fontTimesNewRoman11Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase(terminal, fontTimesNewRoman11)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase("Customer: ", fontTimesNewRoman11Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase(customer, fontTimesNewRoman11)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase("Sales No.: ", fontTimesNewRoman11Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase(salesNumber, fontTimesNewRoman11)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase("Sales Balance: ", fontTimesNewRoman11Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase(salesBalance, fontTimesNewRoman11)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });

                tableHeader.AddCell(new PdfPCell(new Phrase("Remarks: ", fontTimesNewRoman11Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase(remarks, fontTimesNewRoman11)) { Colspan = 3, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });

                PdfPTable tableItem = new PdfPTable(13);
                tableItem.SetWidths(new float[] { 40f, 50, 50f, 40f, 30f, 30f, 50f, 50f, 50f, 30f, 30f, 30f, 30f });
                tableItem.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tableItem.AddCell(new PdfPCell(new Phrase(" ", fontTimesNewRoman9Bold)) { Border = 0, Colspan = 13, PaddingTop = -8f });
                tableItem.AddCell(new PdfPCell(new Phrase("Pay Type", fontTimesNewRoman9Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableItem.AddCell(new PdfPCell(new Phrase("Amount", fontTimesNewRoman9Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableItem.AddCell(new PdfPCell(new Phrase("Check Number", fontTimesNewRoman9Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableItem.AddCell(new PdfPCell(new Phrase("Check Date", fontTimesNewRoman9Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableItem.AddCell(new PdfPCell(new Phrase("Check Bank", fontTimesNewRoman9Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableItem.AddCell(new PdfPCell(new Phrase("GC Number", fontTimesNewRoman9Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableItem.AddCell(new PdfPCell(new Phrase("Other Info", fontTimesNewRoman9Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableItem.AddCell(new PdfPCell(new Phrase("Credit Card Number", fontTimesNewRoman9Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableItem.AddCell(new PdfPCell(new Phrase("Credit Card Holder", fontTimesNewRoman9Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableItem.AddCell(new PdfPCell(new Phrase("Credit Card Reference Number", fontTimesNewRoman9Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableItem.AddCell(new PdfPCell(new Phrase("Credit Card Type", fontTimesNewRoman9Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableItem.AddCell(new PdfPCell(new Phrase("Credit Card Expiry", fontTimesNewRoman9Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableItem.AddCell(new PdfPCell(new Phrase("Credit Card Verification Code", fontTimesNewRoman9Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });

                tableHeader.AddCell(new PdfPCell(tableItem) { Border = 0, Colspan = 4, PaddingBottom = -5f, PaddingLeft = 0f, PaddingRight = 0f });
                tableHeader.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetTop(document.TopMargin) + 97f, writer.DirectContent);
            }
        }
    }
}
