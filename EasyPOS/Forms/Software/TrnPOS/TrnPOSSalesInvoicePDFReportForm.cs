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

namespace EasyPOS.Forms.Software.TrnPOS
{
    public partial class TrnPOSSalesInvoicePDFReportForm : Form
    {
        public Int32 trnSalesId = 0;
        public TrnPOSSalesInvoicePDFReportForm(Int32 salesId)
        {
            InitializeComponent();
            trnSalesId = salesId;
            PrintReport();
        }
        public void PrintReport()
        {
            try
            {
                Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

                iTextSharp.text.Font fontHelvetica9 = FontFactory.GetFont(BaseFont.HELVETICA, 9);
                iTextSharp.text.Font fontHelvetica9Bold = FontFactory.GetFont(BaseFont.HELVETICA, 9, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font fontHelvetica10 = FontFactory.GetFont(BaseFont.HELVETICA, 10);
                iTextSharp.text.Font fontHelvetica10Italic = FontFactory.GetFont(BaseFont.HELVETICA, 10, iTextSharp.text.Font.ITALIC);
                iTextSharp.text.Font fontHelvetica10Bold = FontFactory.GetFont(BaseFont.HELVETICA, 10, iTextSharp.text.Font.BOLD);
                iTextSharp.text.Font fontHelvetica14Bold = FontFactory.GetFont(BaseFont.HELVETICA, 14, iTextSharp.text.Font.BOLD);

                Paragraph line = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.5F, 100.0F, BaseColor.DARK_GRAY, Element.ALIGN_MIDDLE, 10F)));

                var fileName = "SalesInvoice" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
                var currentUser = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;

                var systemCurrent = Modules.SysCurrentModule.GetCurrentSettings();

                Document document = new Document(PageSize.LETTER);
                document.SetMargins(30f, 30f, 50f, 30f);

                PdfWriter pdfWriter = PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));

                document.Open();

               
                var salesHeader = from d in db.TrnSales
                            where d.Id == trnSalesId
                            select d;

                String companyName = systemCurrent.CompanyName;
                String address = systemCurrent.Address;
                String documentTitle = "Sales Invoice";
                String invoiceDate = salesHeader.FirstOrDefault().SalesDate.ToShortDateString();
                String customer = salesHeader.FirstOrDefault().MstCustomer.Customer;
                String salesNumber = salesHeader.FirstOrDefault().SalesNumber;
                String remarks = salesHeader.FirstOrDefault().Remarks;
                String terms = salesHeader.FirstOrDefault().MstTerm.Term;
                String contactNumber = salesHeader.FirstOrDefault().MstCustomer.ContactNumber;
                String customerAdd = salesHeader.FirstOrDefault().MstCustomer.Address;

                PdfPTable tableHeader = new PdfPTable(4);
                tableHeader.SetWidths(new float[] { 20f, 30f, 20f, 50f });
                tableHeader.WidthPercentage = 100;
                tableHeader.AddCell(new PdfPCell(new Phrase(companyName, fontHelvetica14Bold)) { Colspan = 2, Border = 0, Padding = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase(documentTitle, fontHelvetica14Bold)) { HorizontalAlignment = 2, Colspan = 2, Border = 0, Padding = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase(address + "\n", fontHelvetica10)) { Colspan = 4, Border = PdfPCell.BOTTOM_BORDER, Padding = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase("Invoice Date: ", fontHelvetica10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase(invoiceDate, fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase("Customer: ", fontHelvetica10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase(customer, fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase("Address: ", fontHelvetica10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase(customerAdd, fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase("Contact No.: ", fontHelvetica10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase(contactNumber, fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase("Sales No.: ", fontHelvetica10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase(salesNumber, fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase("Term: ", fontHelvetica10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase(terms, fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase("Remarks: ", fontHelvetica10Bold)) { Colspan = 1, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase(remarks, fontHelvetica10)) { Colspan = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                tableHeader.AddCell(new PdfPCell(new Phrase("\n", fontHelvetica10)) { Colspan = 7, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 3f });
                document.Add(tableHeader);

                PdfPTable tableLines = new PdfPTable(7);
                tableLines.SetWidths(new float[] { 30f, 30f, 100f, 50f, 50f, 50f, 50f });
                tableLines.WidthPercentage = 100;
                tableLines.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                tableLines.AddCell(new PdfPCell(new Phrase("Qty", fontHelvetica9Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Unit", fontHelvetica9Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Item", fontHelvetica9Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Price", fontHelvetica9Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Discount", fontHelvetica9Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Net Price", fontHelvetica9Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
                tableLines.AddCell(new PdfPCell(new Phrase("Amount", fontHelvetica9Bold)) { HorizontalAlignment = 1, PaddingTop = 2f, PaddingBottom = 5f });
              
                var sales = from d in db.TrnSales
                            where d.Id == trnSalesId
                            select d;

                if (sales.Any())
                {
                    Decimal totalItemAmount = 0;
                    Decimal totalAmount = 0;
                    Decimal totalNumberOfItems = 0;
                    Decimal itemSubTotal = 0;

                    var salesLines = from d in db.TrnSalesLines where d.SalesId == trnSalesId select d;
                    if (salesLines.Any())
                    {
                        var salesLineGroupbyItem = from s in salesLines
                                                   group s by new
                                                   {
                                                       s.SalesId,
                                                       s.ItemId,
                                                       s.MstItem,
                                                       s.UnitId,
                                                       s.MstUnit,
                                                       s.NetPrice,
                                                       s.Price,
                                                       s.TaxId,
                                                       s.MstTax,
                                                       s.DiscountId,
                                                       s.DiscountRate,
                                                       s.SalesAccountId,
                                                       s.AssetAccountId,
                                                       s.CostAccountId,
                                                       s.TaxAccountId,
                                                       s.SalesLineTimeStamp,
                                                       s.UserId,
                                                       s.Preparation,
                                                       s.Price1,
                                                       s.Price2,
                                                       s.Price2LessTax,
                                                       s.PriceSplitPercentage
                                                   } into g
                                                   select new
                                                   {
                                                       g.Key.ItemId,
                                                       g.Key.MstItem,
                                                       g.Key.MstItem.ItemDescription,
                                                       g.Key.MstUnit.Unit,
                                                       g.Key.Price,
                                                       g.Key.NetPrice,
                                                       g.Key.DiscountId,
                                                       g.Key.DiscountRate,
                                                       g.Key.TaxId,
                                                       g.Key.MstTax,
                                                       g.Key.MstTax.Tax,
                                                       Amount = g.Sum(a => a.Amount),
                                                       Quantity = g.Sum(a => a.Quantity),
                                                       DiscountAmount = g.Sum(a => a.DiscountAmount * a.Quantity),
                                                       TaxAmount = g.Sum(a => a.TaxAmount)
                                                   };

                        if (salesLineGroupbyItem.Any())
                        {
                           

                            foreach (var salesLine in salesLineGroupbyItem.ToList())
                            {
                                totalNumberOfItems += salesLine.Quantity;

                                totalAmount += salesLine.Amount - salesLine.DiscountAmount;
                                itemSubTotal = salesLine.Price * salesLine.Quantity;
                                totalItemAmount = itemSubTotal - salesLine.DiscountAmount;

                                //Item Data
                                tableLines.AddCell(new PdfPCell(new Phrase(salesLine.Quantity.ToString("#,##0.00"), fontHelvetica10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                    tableLines.AddCell(new PdfPCell(new Phrase(salesLine.Unit, fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                    tableLines.AddCell(new PdfPCell(new Phrase(salesLine.ItemDescription, fontHelvetica10)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                    tableLines.AddCell(new PdfPCell(new Phrase(salesLine.Price.ToString("#,##0.00"), fontHelvetica10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                    tableLines.AddCell(new PdfPCell(new Phrase(salesLine.DiscountAmount.ToString("#,##0.00"), fontHelvetica10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                    tableLines.AddCell(new PdfPCell(new Phrase(salesLine.NetPrice.ToString("#,##0.00"), fontHelvetica10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                                    tableLines.AddCell(new PdfPCell(new Phrase((totalItemAmount).ToString("#,##0.00"), fontHelvetica10)) { HorizontalAlignment = 2, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 0f });
                               
                            }
                            
                            tableLines.AddCell(new PdfPCell(new Phrase(line)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 5f, Colspan = 7 });
                            tableLines.AddCell(new PdfPCell(new Phrase(totalNumberOfItems.ToString("#,##0.00"), fontHelvetica10Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 5f, HorizontalAlignment = 2 });
                            tableLines.AddCell(new PdfPCell(new Phrase("Total: ", fontHelvetica14Bold)) { Colspan = 5, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 5f, HorizontalAlignment = 2 });
                            tableLines.AddCell(new PdfPCell(new Phrase(totalAmount.ToString("#,##0.00"), fontHelvetica14Bold)) { Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 5f, HorizontalAlignment = 2 });
                            document.Add(tableLines);

                            PdfPTable tableUsers = new PdfPTable(2);
                            tableUsers.SetWidths(new float[] { 100f, 100f });
                            tableUsers.WidthPercentage = 100;
                            tableLines.AddCell(new PdfPCell(new Phrase(" ", fontHelvetica14Bold)) { Colspan = 8, Border = 0, PaddingLeft = 3f, PaddingRight = 3f, PaddingTop = 3f, PaddingBottom = 50f });
                            tableUsers.AddCell(new PdfPCell(new Phrase("Prepared by: ", fontHelvetica10Bold)) { PaddingTop = 5f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                            tableUsers.AddCell(new PdfPCell(new Phrase("Received by: ", fontHelvetica10Bold)) { PaddingTop = 5f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                            tableUsers.AddCell(new PdfPCell(new Phrase(" ", fontHelvetica10Bold)) { PaddingTop = 5f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                            tableUsers.AddCell(new PdfPCell(new Phrase(" ")) { PaddingBottom = 5f });
                            tableUsers.AddCell(new PdfPCell(new Phrase(salesLines.FirstOrDefault().MstUser.FullName, fontHelvetica10Bold)) { HorizontalAlignment = 1, PaddingTop = 5f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                            tableUsers.AddCell(new PdfPCell(new Phrase(" ", fontHelvetica10Bold)) { HorizontalAlignment = 1, PaddingTop = 5f, PaddingBottom = 9f, PaddingLeft = 5f, PaddingRight = 5f });
                            document.Add(tableUsers);
                            document.Close();

                        }

                    }
                    
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
       
    }
}
