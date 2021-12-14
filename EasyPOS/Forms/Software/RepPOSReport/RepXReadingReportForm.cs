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

namespace EasyPOS.Forms.Software.RepPOSReport
{
    public partial class RepXReadingReportForm : Form
    {
        private Modules.SysUserRightsModule sysUserRights;

        public Forms.Software.RepPOSReport.RepPOSReportForm repPOSReportForm;
        public Int32 filterTerminalId;
        public String filterTerminal = "";
        public DateTime filterDate;
        public Int32 filterSalesAgentId;
        public String filterSalesAgent;
        public Entities.RepPOSReportXReadingReportEntity xReadingReportEntity;

        public RepXReadingReportForm(Forms.Software.RepPOSReport.RepPOSReportForm POSReportForm, Int32 terminalId, DateTime date, Int32 salesAgentId)
        {
            InitializeComponent();

            repPOSReportForm = POSReportForm;
            filterTerminalId = terminalId;
            filterDate = date;
            filterSalesAgentId = salesAgentId;

            if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Dot Matrix Printer")
            {
                printDocumentXReadingReport.DefaultPageSettings.PaperSize = new PaperSize("X Reading Report", 255, 1000);
                XReadingDataSource();
            }
            else if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Thermal Printer")
            {
                printDocumentXReadingReport.DefaultPageSettings.PaperSize = new PaperSize("X Reading Report", 280, 1000);
                XReadingDataSource();
            }
            else
            {
                printDocumentXReadingReport.DefaultPageSettings.PaperSize = new PaperSize("X Reading Report", 175, 1000);
                XReadingDataSource();
            }
            sysUserRights = new Modules.SysUserRightsModule("RepPOS (X Reading)");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (sysUserRights.GetUserRights().CanPrint == false)
                {
                    buttonPrint.Enabled = false;
                }

            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            DialogResult printerDialogResult = printDialogXReadingReport.ShowDialog();
            if (printerDialogResult == DialogResult.OK)
            {
                Int32 currentUser = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId);
                Entities.SysReadingPrintCount newPrintCout = new Entities.SysReadingPrintCount()
                {
                    PrintDate = Convert.ToDateTime(DateTime.Today.ToShortDateString()),
                    PrintCount = 1,
                    PrintType = "X",
                    UserId = currentUser
                };
                Controllers.SysReadingPrintCountController xreadingCount = new Controllers.SysReadingPrintCountController();
                xreadingCount.InsertPrintCount(newPrintCout);

                PrintReport();
                Close();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void PrintReport()
        {
            printDocumentXReadingReport.Print();
        }

        public void XReadingDataSource()
        {
            Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

            Entities.RepPOSReportXReadingReportEntity repXReadingReportEntity = new Entities.RepPOSReportXReadingReportEntity()
            {
                SalesAgent = "",
                Date = "",
                TotalGrossSales = 0,
                TotalRegularDiscount = 0,
                TotalSeniorDiscount = 0,
                TotalPWDDiscount = 0,
                TotalSalesReturn = 0,
                TotalNetSales = 0,
                CollectionLines = new List<Entities.TrnCollectionLineEntity>(),
                TotalRefund = 0,
                TotalCollection = 0,
                TotalVATSales = 0,
                TotalVATAmount = 0,
                TotalNonVAT = 0,
                TotalVATExclusive = 0,
                TotalVATExempt = 0,
                TotalVATZeroRated = 0,
                CounterIdStart = "0000000000",
                CounterIdEnd = "0000000000",
                TotalCancelledTrnsactionCount = 0,
                TotalCancelledAmount = 0,
                TotalNumberOfTransactions = 0,
                TotalNumberOfSKU = 0,
                TotalQuantity = 0,
                TotalPreviousReading = 0,
                RunningTotal = 0
            };

            repXReadingReportEntity.Date = filterDate.ToShortDateString();

            var user = from d in db.MstUsers
                       where d.Id == filterSalesAgentId
                       select d;
            filterSalesAgent = user.FirstOrDefault().UserName;

            var terminal = from d in db.MstTerminals
                           where d.Id == filterTerminalId
                           select d;

            if (terminal.Any())
            {
                filterTerminal = terminal.FirstOrDefault().Terminal;
            }

            Decimal currentDeclareRate = 0;

            var currentSysDeclareRate = from d in db.SysDeclareRates
                                        where d.Date == filterDate
                                        select d;

            if (currentSysDeclareRate.Any())
            {
                if (currentSysDeclareRate.FirstOrDefault().DeclareRate != null)
                {
                    currentDeclareRate = Convert.ToDecimal(currentSysDeclareRate.FirstOrDefault().DeclareRate);
                }
            }
            else
            {
                currentDeclareRate = Modules.SysCurrentModule.GetCurrentSettings().DeclareRate;
            }

            var currentCollectionSalesLineQuery = from d in db.TrnSalesLines
                                                  where d.TrnSale.TrnCollections.Any() == true
                                                  && d.TrnSale.TrnCollections.Where(
                                                      c => c.TerminalId == filterTerminalId &&
                                                           c.CollectionDate == filterDate &&
                                                           c.IsLocked == true &&
                                                           c.IsCancelled == false &&
                                                           c.SalesId != null &&
                                                           c.PreparedBy == filterSalesAgentId).Any() == true
                                                  && d.TrnSale.IsLocked == true
                                                  && d.TrnSale.IsCancelled == false
                                                  && d.TrnSale.IsReturned == false
                                                  select d;

            if (currentCollectionSalesLineQuery.Any())
            {
                Decimal totalGrossSales = 0;
                Decimal totalRegularDiscount = 0;
                Decimal totalSeniorCitizenDiscount = 0;
                Decimal totalPWDDiscount = 0;
                Decimal totalSalesReturn = 0;
                Decimal totalVATSales = 0;
                Decimal totalVATAmount = 0;
                Decimal totalNonVATSales = 0;
                Decimal totalVATExemptSales = 0;
                Decimal totalVATZeroRatedSales = 0;
                Decimal totalNoOfSKUs = 0;
                Decimal totalQUantity = 0;

                var salesLinesQuery = from d in currentCollectionSalesLineQuery
                                      where d.Quantity > 0 && d.MstItem.ItemCode != "0000000001"
                                      select d;

                if (salesLinesQuery.Any())
                {
                    totalNoOfSKUs += salesLinesQuery.Count();
                    totalQUantity += salesLinesQuery.Sum(d => d.Quantity);

                    var salesLines = salesLinesQuery.ToArray();

                    Decimal VATAmountValue = salesLines.Sum(d =>
                            d.MstTax.Code == "EXEMPTVAT" ? ((d.Price * d.Quantity) / (1 + (d.MstItem.MstTax1.Rate / 100)) * (d.MstItem.MstTax1.Rate / 100)) : d.TaxAmount
                    );
                    totalGrossSales = salesLines.Sum(d => d.Price * d.Quantity) - VATAmountValue;

                    totalRegularDiscount = salesLines.Sum(d =>
                        d.MstDiscount.Discount != "Senior Citizen Discount" && d.MstDiscount.Discount != "PWD" ? d.DiscountAmount * d.Quantity : 0
                    );

                    totalSeniorCitizenDiscount = salesLines.Sum(d =>
                        d.MstDiscount.Discount == "Senior Citizen Discount" ? d.DiscountAmount * d.Quantity : 0
                    );

                    totalPWDDiscount = salesLines.Sum(d =>
                        d.MstDiscount.Discount == "PWD" ? d.DiscountAmount * d.Quantity : 0
                    );

                    totalVATSales = salesLines.Sum(d =>
                        d.MstTax.Code == "VAT" ? d.Amount - (d.Amount / (1 + (d.MstTax.Rate / 100)) * (d.MstTax.Rate / 100)) : 0
                    );

                    totalVATAmount = salesLines.Sum(d =>
                        d.MstTax.Code == "EXEMPTVAT" ? 0 : d.TaxAmount
                    //d.MstTax.Code == "EXEMPTVAT" ? ((d.Price * d.Quantity) / (1 + (d.MstItem.MstTax1.Rate / 100)) * (d.MstItem.MstTax1.Rate / 100)) : d.TaxAmount
                    );

                    totalNonVATSales = salesLines.Sum(d =>
                        d.MstTax.Code == "NONVAT" ? d.Amount : 0
                    );

                    totalVATExemptSales = salesLines.Sum(d =>
                    d.MstTax.Code == "EXEMPTVAT" ? ((d.Price * d.Quantity) - ((d.Price * d.Quantity) / (1 + (d.MstItem.MstTax1.Rate / 100)) * (d.MstItem.MstTax1.Rate / 100))) : 0
                    //d.MstTax.Code == "EXEMPTVAT" ? ((d.Price * d.Quantity) - ((d.Price * d.Quantity) / (1 + (d.MstItem.MstTax1.Rate / 100)) * (d.MstItem.MstTax1.Rate / 100))) - d.DiscountAmount * d.Quantity : 0
                    //d.MstTax.Code == "EXEMPTVAT" ? ((d.Price * d.Quantity) - ((d.Price * d.Quantity) / (1 + (d.MstItem.MstTax1.Rate / 100)) * (d.MstItem.MstTax1.Rate / 100))) - totalSeniorCitizenDiscount - totalPWDDiscount : 0
                    );

                    totalVATZeroRatedSales = salesLines.Sum(d =>
                        d.MstTax.Code == "ZEROVAT" ? d.Amount : 0
                    );
                }

                Decimal VATSalesReturn = 0;
                Decimal VATAmountSalesReturn = 0;
                Decimal VATExemptSalesReturn = 0;
                Decimal VATAmountExemptSalesReturn = 0;

                var salesReturnLinesQuery = from d in db.TrnSalesLines
                                            where d.Quantity < 0
                                            && d.TrnSale.SalesDate == filterDate
                                            && d.TrnSale.IsLocked == true
                                            && d.TrnSale.IsCancelled == false
                                            && d.TrnSale.IsReturned == true
                                            && d.TrnSale.PreparedBy == filterSalesAgentId
                                            select d;

                if (salesReturnLinesQuery.Any())
                {
                    var salesReturnLines = salesReturnLinesQuery.ToArray();

                    VATSalesReturn = salesReturnLines.Sum(d =>
                        d.MstTax.Code == "VAT" ? d.Amount : 0
                    );

                    VATAmountSalesReturn = salesReturnLines.Sum(d =>
                        d.MstTax.Code == "VAT" ? d.TaxAmount : 0
                    ) * -1;

                    VATExemptSalesReturn = salesReturnLines.Sum(d =>
                        d.MstTax.Code == "EXEMPTVAT" ? d.Amount : 0
                    );

                    VATAmountExemptSalesReturn = salesReturnLines.Sum(d =>
                        d.MstTax.Code == "EXEMPTVAT" ? ((d.Price * (d.Quantity * -1)) / (1 + (d.MstItem.MstTax1.Rate / 100)) * (d.MstItem.MstTax1.Rate / 100)) : d.TaxAmount
                    ) * -1;

                    totalSalesReturn = (VATSalesReturn + VATExemptSalesReturn);
                }

                repXReadingReportEntity.TotalGrossSales = totalGrossSales;
                repXReadingReportEntity.TotalRegularDiscount = totalRegularDiscount;
                repXReadingReportEntity.TotalSeniorDiscount = totalSeniorCitizenDiscount;
                repXReadingReportEntity.TotalPWDDiscount = totalPWDDiscount;
                repXReadingReportEntity.TotalSalesReturn = totalSalesReturn;

                String _totalGrossSales = (totalGrossSales * currentDeclareRate).ToString("#,##0.00");
                String _totalRegularDiscount = (totalRegularDiscount * currentDeclareRate).ToString("#,##0.00");
                String _totalSeniorCitizenDiscount = (totalSeniorCitizenDiscount * currentDeclareRate).ToString("#,##0.00");
                String _totalPWDDiscount = (totalPWDDiscount * currentDeclareRate).ToString("#,##0.00");
                String _totalSalesReturn = (totalSalesReturn * currentDeclareRate).ToString("#,##0.00");

                repXReadingReportEntity.TotalNetSales = Convert.ToDecimal(_totalGrossSales) -
                                                        Convert.ToDecimal(_totalRegularDiscount) -
                                                        Convert.ToDecimal(_totalSeniorCitizenDiscount) -
                                                        Convert.ToDecimal(_totalPWDDiscount) -
                                                        (Convert.ToDecimal(_totalSalesReturn) * -1);

                repXReadingReportEntity.TotalVATSales = totalVATSales - (VATSalesReturn * -1);
                repXReadingReportEntity.TotalVATAmount = totalVATAmount - VATAmountSalesReturn - VATAmountExemptSalesReturn;
                repXReadingReportEntity.TotalNonVAT = totalNonVATSales;
                repXReadingReportEntity.TotalVATExempt = (totalVATExemptSales - Convert.ToDecimal(_totalSeniorCitizenDiscount) - Convert.ToDecimal(_totalPWDDiscount)) - (VATExemptSalesReturn * -1);
                //repXReadingReportEntity.TotalVATExempt = totalVATExemptSales - (VATExemptSalesReturn * -1);
                repXReadingReportEntity.TotalVATZeroRated = totalVATZeroRatedSales;
                repXReadingReportEntity.TotalNumberOfSKU = totalNoOfSKUs;
                repXReadingReportEntity.TotalQuantity = totalQUantity;
            }

            var disbursmenet = from d in db.TrnDisbursements
                               where d.TerminalId == filterTerminalId
                               && d.DisbursementDate == filterDate
                               && d.IsLocked == true
                               && d.IsRefund == true
                               && d.RefundSalesId != null
                               && d.PreparedBy == filterSalesAgentId
                               select d;

            if (disbursmenet.Any())
            {
                repXReadingReportEntity.TotalRefund = disbursmenet.Sum(d => d.Amount);
            }

            var currentCollectionLinesQuery = from d in db.TrnCollectionLines
                                              where d.TrnCollection.TerminalId == filterTerminalId
                                              && d.TrnCollection.CollectionDate == filterDate
                                              && d.TrnCollection.IsLocked == true
                                              && d.TrnCollection.PreparedBy == filterSalesAgentId
                                              group d by new
                                              {
                                                  d.MstPayType.PayTypeCode,
                                                  d.MstPayType.PayType,
                                              } into g
                                              select new
                                              {
                                                  g.Key.PayTypeCode,
                                                  g.Key.PayType,
                                                  Amount = g.Sum(s => s.TrnCollection.IsCancelled == false ? s.MstPayType.PayTypeCode == "CASH" ? s.Amount - s.TrnCollection.ChangeAmount : s.Amount : 0)
                                                  //Amount = g.Sum(s => s.TrnCollection.IsCancelled == false ? s.MstPayType.PayTypeCode == "CASH" ? s.Amount : 0 : 0)
                                              };

            Decimal totalCollectionAmount = 0;

            if (currentCollectionLinesQuery.ToList().Any())
            {
                var currentCollectionLines = currentCollectionLinesQuery.ToArray();

                for (Int32 i = 0; i < currentCollectionLines.Count(); i++)
                {
                    var collectionLine = currentCollectionLines[i];

                    repXReadingReportEntity.CollectionLines.Add(new Entities.TrnCollectionLineEntity()
                    {
                        PayType = collectionLine.PayType,
                        Amount = collectionLine.Amount
                    });

                    totalCollectionAmount += collectionLine.Amount;
                }
            }

            repXReadingReportEntity.TotalCollection = totalCollectionAmount - repXReadingReportEntity.TotalRefund;

            var counterCollections = from d in db.TrnCollections
                                     where d.TerminalId == filterTerminalId
                                     && d.CollectionDate == filterDate
                                     && d.IsLocked == true
                                     select d;

            if (counterCollections.Any())
            {
                repXReadingReportEntity.CounterIdStart = counterCollections.OrderBy(d => d.Id).FirstOrDefault().CollectionNumber;
                repXReadingReportEntity.CounterIdEnd = counterCollections.OrderByDescending(d => d.Id).FirstOrDefault().CollectionNumber;
                repXReadingReportEntity.TotalNumberOfTransactions = counterCollections.Count();
            }

            var currentCancelledCollections = from d in db.TrnCollections
                                              where d.TerminalId == filterTerminalId
                                              && d.CollectionDate == filterDate
                                              && d.IsLocked == true
                                              && d.IsCancelled == true
                                              select d;

            if (currentCancelledCollections.Any())
            {
                repXReadingReportEntity.TotalCancelledTrnsactionCount = currentCancelledCollections.Count();
                repXReadingReportEntity.TotalCancelledAmount = currentCancelledCollections.Sum(d => d.Amount);
            }

            Int32 printCount = 0;
            var printCountZReading = from d in db.SysReadingPrintCounts
                                     where d.PrintType == "X"
                                     && d.PrintDate == Convert.ToDateTime(DateTime.Today.ToShortDateString())
                                     && d.UserId == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId)
                                     select d;

            if (printCountZReading.Any())
            {
                printCount = printCountZReading.Sum(d => d.PrintCount);
                repXReadingReportEntity.XPrintCount = printCount + 1;
            }
            else
            {
                repXReadingReportEntity.XPrintCount = printCount + 1;
            }

            xReadingReportEntity = repXReadingReportEntity;
        }

        private void printDocumentXReadingReport_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

            var dataSource = xReadingReportEntity;

            Decimal currentDeclareRate = 0;

            var currentSysDeclareRate = from d in db.SysDeclareRates
                                        where d.Date == filterDate
                                        select d;

            if (currentSysDeclareRate.Any())
            {
                if (currentSysDeclareRate.FirstOrDefault().DeclareRate != null)
                {
                    currentDeclareRate = Convert.ToDecimal(currentSysDeclareRate.FirstOrDefault().DeclareRate);
                }
            }
            else
            {
                currentDeclareRate = Modules.SysCurrentModule.GetCurrentSettings().DeclareRate;
            }

            Decimal totalGrossSales = dataSource.TotalGrossSales * currentDeclareRate;
            Decimal totalRegularDiscount = dataSource.TotalRegularDiscount * currentDeclareRate;
            Decimal totalSeniorDiscount = dataSource.TotalSeniorDiscount * currentDeclareRate;
            Decimal totalPWDDiscount = dataSource.TotalPWDDiscount * currentDeclareRate;
            Decimal totalSalesReturn = dataSource.TotalSalesReturn * currentDeclareRate;
            Decimal totalNetSales = dataSource.TotalNetSales;
            Int32 totalPrintCount = dataSource.XPrintCount;

            // =============
            // Font Settings
            // =============
            Font fontArial12Bold = new Font("Arial", 12, FontStyle.Bold);
            Font fontArial12Regular = new Font("Arial", 12, FontStyle.Regular);
            Font fontArial11Bold = new Font("Arial", 11, FontStyle.Bold);
            Font fontArial11Regular = new Font("Arial", 11, FontStyle.Regular);
            Font fontArial8Bold = new Font("Arial", 8, FontStyle.Bold);
            Font fontArial8Regular = new Font("Arial", 8, FontStyle.Regular);
            Font fontArial7Bold = new Font("Arial", 7, FontStyle.Bold);
            Font fontArial7Regular = new Font("Arial", 7, FontStyle.Regular);
            Font fontArial6Bold = new Font("Arial", 6, FontStyle.Bold);
            Font fontArial6Regular = new Font("Arial", 6, FontStyle.Regular);

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
                width = 270.0F; height = 0F;
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

            // ============
            // Company Name
            // ============
            String companyName = systemCurrent.CompanyName;
            float adjustStringName = 1;
            if (companyName.Length > 43)
            {
                adjustStringName = 3;
            }
            graphics.DrawString(companyName, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += graphics.MeasureString(companyName, fontArial8Regular).Height * adjustStringName;

            // ===============
            // Company Address
            // ===============
            String companyAddress = systemCurrent.Address;
            RectangleF companyAddressRectangle = new RectangleF
            {
                X = x,
                Y = y,
                Size = new Size(245, ((int)graphics.MeasureString(companyAddress, fontArial8Regular, 245, StringFormat.GenericDefault).Height))
            };
            graphics.DrawString(companyAddress, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += companyAddressRectangle.Size.Height + 12F;

            // ==========
            // TIN Number
            // ==========
            String TINNumber = systemCurrent.TIN;
            RectangleF TINNumbersRectangle = new RectangleF
            {
                X = x,
                Y = y,
                Size = new Size(245, ((int)graphics.MeasureString(TINNumber, fontArial8Regular, 245, StringFormat.GenericDefault).Height))
            };
            graphics.DrawString("TIN: " + TINNumber, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += TINNumbersRectangle.Size.Height + 1.0F;

            // =============
            // Serial Number
            // =============
            String serialNo = systemCurrent.SerialNo;
            RectangleF SNNumbersRectangle = new RectangleF
            {
                X = x,
                Y = y,
                Size = new Size(245, ((int)graphics.MeasureString(serialNo, fontArial8Regular, 245, StringFormat.GenericDefault).Height))
            };
            graphics.DrawString("SN: " + serialNo, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += SNNumbersRectangle.Size.Height + 1.0F;

            // ==============
            // Machine Number
            // ==============
            String machineNo = systemCurrent.MachineNo;
            graphics.DrawString("MIN: " + machineNo, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += graphics.MeasureString(companyAddress, fontArial8Regular).Height;

            // ===============
            // Terminal Number
            // ===============
            String terminal = filterTerminal;
            graphics.DrawString("Terminal: " + terminal, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += graphics.MeasureString(companyAddress, fontArial8Regular).Height;

            // ======================
            // X Reading Report Title
            // ======================
            String zReadingReportTitle = "X Reading Report";
            graphics.DrawString(zReadingReportTitle, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += graphics.MeasureString(zReadingReportTitle, fontArial8Regular).Height;

            // ====
            // Date 
            // ====
            String collectionDateText = filterDate.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture);
            graphics.DrawString(collectionDateText, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += graphics.MeasureString(collectionDateText, fontArial8Regular).Height;

            // ====
            // Cashier 
            // ====
            graphics.DrawString(filterSalesAgent, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += graphics.MeasureString(filterSalesAgent, fontArial8Regular).Height;

            // ====
            // X Print Count 
            // ====
            String printCountLabel = "Print Count : ";
            string printCount = totalPrintCount.ToString();
            graphics.DrawString(printCountLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
            graphics.DrawString(printCount, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
            y += graphics.MeasureString(printCount, fontArial8Regular).Height;

            // ========
            // 1st Line
            // ========
            Point firstLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
            Point firstLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
            graphics.DrawLine(blackPen, firstLineFirstPoint, firstLineSecondPoint);

            if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "58mm Printer")
            {
                // ===========
                // Gross Sales
                // ===========
                String totalGrossSalesLabel = "\nGross Sales";
                String totalGrossSalesData = "\n" + totalGrossSales.ToString("#,##0.00");
                graphics.DrawString(totalGrossSalesLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalGrossSalesData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalGrossSalesData, fontArial7Regular).Height;

                // ================
                // Regular Discount
                // ================
                String totalRegularDiscountLabel = "Regular Discount";
                String totalRegularDiscountData = totalRegularDiscount.ToString("#,##0.00");
                graphics.DrawString(totalRegularDiscountLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalRegularDiscountData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalRegularDiscountData, fontArial7Regular).Height;

                // ===============
                // Senior Discount
                // ===============
                String totalSeniorDiscountLabel = "Senior Discount";
                String totalSeniorDiscountData = totalSeniorDiscount.ToString("#,##0.00");
                graphics.DrawString(totalSeniorDiscountLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalSeniorDiscountData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalSeniorDiscountData, fontArial7Regular).Height;

                // ============
                // PWD Discount
                // ============
                String totalPWDDiscountLabel = "PWD Discount";
                String totalPWDDiscountData = totalPWDDiscount.ToString("#,##0.00");
                graphics.DrawString(totalPWDDiscountLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalPWDDiscountData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalPWDDiscountData, fontArial7Regular).Height;

                // ============
                // Sales Return
                // ============
                String totalSalesReturnLabel = "Sales Return";
                String totalSalesReturnData = "(" + totalSalesReturn.ToString("#,##0.00") + ")";
                graphics.DrawString(totalSalesReturnLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalSalesReturnData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalSalesReturnData, fontArial7Regular).Height;

                // =========
                // Net Sales
                // =========
                String totalNetSalesLabel = "Net Sales \n\n";
                String totalNetSalesData = totalNetSales.ToString("#,##0.00") + "\n\n";
                graphics.DrawString(totalNetSalesLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalNetSalesData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalNetSalesData, fontArial7Regular).Height;

                // ========
                // 2nd Line
                // ========
                Point secondLineFirstPoint = new Point(0, Convert.ToInt32(y) - 7);
                Point secondLineSecondPoint = new Point(500, Convert.ToInt32(y) - 7);
                graphics.DrawLine(blackPen, secondLineFirstPoint, secondLineSecondPoint);

                if (dataSource.CollectionLines.Any())
                {
                    var previousSalesReturnLines = dataSource.CollectionLines.ToArray();

                    for (Int32 i = 0; i < previousSalesReturnLines.Count(); i++)
                    {
                        var collectionLine = previousSalesReturnLines[i];

                        // ================
                        // Collection Lines
                        // ================
                        String collectionLineLabel = collectionLine.PayType;
                        String collectionLineData = (collectionLine.Amount * currentDeclareRate).ToString("#,##0.00");
                        graphics.DrawString(collectionLineLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                        graphics.DrawString(collectionLineData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                        y += graphics.MeasureString(collectionLineData, fontArial7Regular).Height;
                    }

                    Decimal totalRefund = dataSource.TotalRefund * currentDeclareRate;

                    String totalRefundLabel = "Refund";
                    String totalRefundData = "(" + totalRefund.ToString("#,##0.00") + ")";
                    graphics.DrawString(totalRefundLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(totalRefundData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(totalRefundData, fontArial7Regular).Height;

                    // ========
                    // 3rd Line
                    // ========
                    Point thirdLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                    Point thirdLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                    graphics.DrawLine(blackPen, thirdLineFirstPoint, thirdLineSecondPoint);
                }

                // ========
                // 3rd Line
                // ========
                Point thirdLineFirstPoint2 = new Point(0, Convert.ToInt32(y) + 5);
                Point thirdLineSecondPoint2 = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, thirdLineFirstPoint2, thirdLineSecondPoint2);

                Decimal totalCollection = dataSource.TotalCollection * currentDeclareRate;

                String totalCollectionLabel = "\nTotal Collection";
                String totalCollectionData = "\n" + totalCollection.ToString("#,##0.00");
                graphics.DrawString(totalCollectionLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalCollectionData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalCollectionData, fontArial7Regular).Height;

                // ========
                // 4th Line
                // ========
                Point forthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point forthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, forthLineFirstPoint, forthLineSecondPoint);

                Decimal totalVATSales = dataSource.TotalVATSales * currentDeclareRate;
                Decimal totalVATAmount = dataSource.TotalVATAmount * currentDeclareRate;
                Decimal totalNonVAT = dataSource.TotalNonVAT * currentDeclareRate;
                Decimal totalVATExclusive = dataSource.TotalVATExclusive * currentDeclareRate;
                Decimal totalVATExempt = dataSource.TotalVATExempt * currentDeclareRate;
                Decimal totalVATZeroRated = dataSource.TotalVATZeroRated * currentDeclareRate;

                String vatSalesLabel = "\nVAT Sales";
                String totalVatSalesData = "\n" + totalVATSales.ToString("#,##0.00");
                graphics.DrawString(vatSalesLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalVatSalesData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalVatSalesData, fontArial7Regular).Height;

                String totalVATAmountLabel = "VAT Amount";
                String totalVATAmountData = totalVATAmount.ToString("#,##0.00");
                graphics.DrawString(totalVATAmountLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalVATAmountData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalVATAmountData, fontArial7Regular).Height;

                String totalNonVATLabel = "Non-VAT";
                String totalNonVATAmount = totalNonVAT.ToString("#,##0.00");
                graphics.DrawString(totalNonVATLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalNonVATAmount, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalNonVATAmount, fontArial7Regular).Height;

                String totalVATExemptLabel = "VAT Exempt";
                String totalVATExemptData = totalVATExempt.ToString("#,##0.00");
                graphics.DrawString(totalVATExemptLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalVATExemptData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalVATExemptData, fontArial7Regular).Height;

                String totalVATZeroRatedLabel = "VAT Zero Rated";
                String totalVatZeroRatedData = totalVATZeroRated.ToString("#,##0.00");
                graphics.DrawString(totalVATZeroRatedLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalVatZeroRatedData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalVatZeroRatedData, fontArial7Regular).Height;

                // ========
                // 9th Line
                // ========
                Point ninethLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point ninethLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, ninethLineFirstPoint, ninethLineSecondPoint);

                Decimal totalVAT = Convert.ToDecimal(totalVATSales.ToString("#,##0.00")) +
                    Convert.ToDecimal(totalVATAmount.ToString("#,##0.00")) +
                    Convert.ToDecimal(totalNonVAT.ToString("#,##0.00")) +
                    Convert.ToDecimal(totalVATExempt.ToString("#,##0.00")) +
                    Convert.ToDecimal(totalVATZeroRated.ToString("#,##0.00"));
                String totalVATTotalLabel = "\nTotal";
                String totalVatTotalData = "\n" + totalVAT.ToString("#,##0.00");
                graphics.DrawString(totalVATTotalLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalVatTotalData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalVatTotalData, fontArial8Regular).Height;

                // ========
                // 5th Line
                // ========
                Point fifthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point fifthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, fifthLineFirstPoint, fifthLineSecondPoint);

                String counterIdStart = dataSource.CounterIdStart;
                String counterIdEnd = dataSource.CounterIdEnd;

                String startCounterIdLabel = "\nCounter ID Start";
                String startCounterIdData = "\n" + counterIdStart;
                graphics.DrawString(startCounterIdLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(startCounterIdData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(startCounterIdData, fontArial7Regular).Height;

                String endCounterIdLabel = "Counter ID End";
                String endCounterIdData = counterIdEnd;
                graphics.DrawString(endCounterIdLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(endCounterIdData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(endCounterIdData, fontArial7Regular).Height;

                // ========
                // 6th Line
                // ========
                Point sixthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point sixthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, sixthLineFirstPoint, sixthLineSecondPoint);

                Decimal totalCancelledTrnsactionCount = dataSource.TotalCancelledTrnsactionCount;
                Decimal totalCancelledAmount = dataSource.TotalCancelledAmount * currentDeclareRate;

                String totalCancelledTrnsactionCountLabel = "\nCancelled Tx.";
                String totalCancelledTrnsactionCountData = "\n" + totalCancelledTrnsactionCount.ToString("#,##0");
                graphics.DrawString(totalCancelledTrnsactionCountLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalCancelledTrnsactionCountData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalCancelledTrnsactionCountData, fontArial7Regular).Height;

                String totalCancelledAmountLabel = "Cancelled Amount";
                String totalCancelledAmountData = totalCancelledAmount.ToString("#,##0.00");
                graphics.DrawString(totalCancelledAmountLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalCancelledAmountData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalCancelledAmountData, fontArial7Regular).Height;

                // ========
                // 7th Line
                // ========
                Point seventhLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point seventhLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, seventhLineFirstPoint, seventhLineSecondPoint);

                Decimal totalNumberOfTransactions = dataSource.TotalNumberOfTransactions;
                Decimal totalNumberOfSKU = dataSource.TotalNumberOfSKU;
                Decimal totalQuantity = dataSource.TotalQuantity;

                String totalNumberOfTransactionsLabel = "\nNo. of Transactions";
                String totalNumberOfTransactionsData = "\n" + totalNumberOfTransactions.ToString("#,##0");
                graphics.DrawString(totalNumberOfTransactionsLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalNumberOfTransactionsData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalNumberOfTransactionsData, fontArial7Regular).Height;

                String totalNumberOfSKULabel = "No. of SKU";
                String totalNumberOfSKUData = totalNumberOfSKU.ToString("#,##0");
                graphics.DrawString(totalNumberOfSKULabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalNumberOfSKUData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalNumberOfSKUData, fontArial7Regular).Height;

                String totalQuantityLabel = "Total Quantity";
                String totalQuantityData = totalQuantity.ToString("#,##0.00");
                graphics.DrawString(totalQuantityLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalQuantityData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalQuantityData, fontArial7Regular).Height;

                // ========
                // 8th Line
                // ========
                Point eightLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point eightLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, eightLineFirstPoint, eightLineSecondPoint);

                String zReadingFooter = systemCurrent.ZReadingFooter;

                if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Dot Matrix Printer")
                {
                    String zReadingEndLabel = "\n" + zReadingFooter;
                    graphics.DrawString(zReadingEndLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(zReadingEndLabel, fontArial7Regular).Height;
                }
                else
                {
                    String zReadingEndLabel = "\n" + zReadingFooter;
                    graphics.DrawString(zReadingEndLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(zReadingEndLabel, fontArial7Regular).Height;
                }
            }
            else
            {
                // ===========
                // Gross Sales
                // ===========
                String totalGrossSalesLabel = "\nGross Sales (Net of VAT)";
                String totalGrossSalesData = "\n" + totalGrossSales.ToString("#,##0.00");
                graphics.DrawString(totalGrossSalesLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalGrossSalesData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalGrossSalesData, fontArial8Regular).Height;

                // ================
                // Regular Discount
                // ================
                String totalRegularDiscountLabel = "Regular Discount";
                String totalRegularDiscountData = totalRegularDiscount.ToString("#,##0.00");
                graphics.DrawString(totalRegularDiscountLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalRegularDiscountData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalRegularDiscountData, fontArial8Regular).Height;

                // ===============
                // Senior Discount
                // ===============
                String totalSeniorDiscountLabel = "Senior Discount";
                String totalSeniorDiscountData = totalSeniorDiscount.ToString("#,##0.00");
                graphics.DrawString(totalSeniorDiscountLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalSeniorDiscountData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalSeniorDiscountData, fontArial8Regular).Height;

                // ============
                // PWD Discount
                // ============
                String totalPWDDiscountLabel = "PWD Discount";
                String totalPWDDiscountData = totalPWDDiscount.ToString("#,##0.00");
                graphics.DrawString(totalPWDDiscountLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalPWDDiscountData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalPWDDiscountData, fontArial8Regular).Height;

                // ============
                // Sales Return
                // ============
                String totalSalesReturnLabel = "Sales Return";
                String totalSalesReturnData = "(" + totalSalesReturn.ToString("#,##0.00") + ")";
                graphics.DrawString(totalSalesReturnLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalSalesReturnData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalSalesReturnData, fontArial8Regular).Height;

                // =========
                // Net Sales
                // =========
                String totalNetSalesLabel = "Net Sales \n\n";
                String totalNetSalesData = totalNetSales.ToString("#,##0.00") + "\n\n";
                graphics.DrawString(totalNetSalesLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalNetSalesData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalNetSalesData, fontArial8Regular).Height;

                // ========
                // 2nd Line
                // ========
                Point secondLineFirstPoint = new Point(0, Convert.ToInt32(y) - 7);
                Point secondLineSecondPoint = new Point(500, Convert.ToInt32(y) - 7);
                graphics.DrawLine(blackPen, secondLineFirstPoint, secondLineSecondPoint);

                if (dataSource.CollectionLines.Any())
                {
                    var previousSalesReturnLines = dataSource.CollectionLines.ToArray();

                    for (Int32 i = 0; i < previousSalesReturnLines.Count(); i++)
                    {
                        var collectionLine = previousSalesReturnLines[i];

                        // ================
                        // Collection Lines
                        // ================
                        String collectionLineLabel = collectionLine.PayType;
                        String collectionLineData = (collectionLine.Amount * currentDeclareRate).ToString("#,##0.00");
                        graphics.DrawString(collectionLineLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                        graphics.DrawString(collectionLineData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                        y += graphics.MeasureString(collectionLineData, fontArial8Regular).Height;
                    }

                    Decimal totalRefund = dataSource.TotalRefund * currentDeclareRate;

                    String totalRefundLabel = "Refund";
                    String totalRefundData = "(" + totalRefund.ToString("#,##0.00") + ")";
                    graphics.DrawString(totalRefundLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(totalRefundData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(totalRefundData, fontArial8Regular).Height;

                    // ========
                    // 3rd Line
                    // ========
                    Point thirdLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                    Point thirdLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                    graphics.DrawLine(blackPen, thirdLineFirstPoint, thirdLineSecondPoint);
                }

                // ========
                // 3rd Line
                // ========
                Point thirdLineFirstPoint2 = new Point(0, Convert.ToInt32(y) + 5);
                Point thirdLineSecondPoint2 = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, thirdLineFirstPoint2, thirdLineSecondPoint2);

                Decimal totalCollection = dataSource.TotalCollection * currentDeclareRate;

                String totalCollectionLabel = "\nTotal Collection";
                String totalCollectionData = "\n" + totalCollection.ToString("#,##0.00");
                graphics.DrawString(totalCollectionLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalCollectionData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalCollectionData, fontArial8Regular).Height;

                // ========
                // 4th Line
                // ========
                Point forthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point forthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, forthLineFirstPoint, forthLineSecondPoint);

                Decimal totalVATSales = dataSource.TotalVATSales * currentDeclareRate;
                Decimal totalVATAmount = dataSource.TotalVATAmount * currentDeclareRate;
                Decimal totalNonVAT = dataSource.TotalNonVAT * currentDeclareRate;
                Decimal totalVATExclusive = dataSource.TotalVATExclusive * currentDeclareRate;
                Decimal totalVATExempt = dataSource.TotalVATExempt * currentDeclareRate;
                Decimal totalVATZeroRated = dataSource.TotalVATZeroRated * currentDeclareRate;

                String vatSalesLabel = "\nVAT Sales";
                String totalVatSalesData = "\n" + totalVATSales.ToString("#,##0.00");
                graphics.DrawString(vatSalesLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalVatSalesData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalVatSalesData, fontArial8Regular).Height;

                String totalVATAmountLabel = "VAT Amount";
                String totalVATAmountData = totalVATAmount.ToString("#,##0.00");
                graphics.DrawString(totalVATAmountLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalVATAmountData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalVATAmountData, fontArial8Regular).Height;

                String totalNonVATLabel = "Non-VAT";
                String totalNonVATAmount = totalNonVAT.ToString("#,##0.00");
                graphics.DrawString(totalNonVATLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalNonVATAmount, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalNonVATAmount, fontArial8Regular).Height;

                String totalVATExemptLabel = "VAT Exempt";
                String totalVATExemptData = totalVATExempt.ToString("#,##0.00");
                graphics.DrawString(totalVATExemptLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalVATExemptData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalVATExemptData, fontArial8Regular).Height;

                String totalVATZeroRatedLabel = "VAT Zero Rated";
                String totalVatZeroRatedData = totalVATZeroRated.ToString("#,##0.00");
                graphics.DrawString(totalVATZeroRatedLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalVatZeroRatedData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalVatZeroRatedData, fontArial8Regular).Height;

                // ========
                // 9th Line
                // ========
                Point ninethLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point ninethLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, ninethLineFirstPoint, ninethLineSecondPoint);

                Decimal totalVAT = Convert.ToDecimal(totalVATSales.ToString("#,##0.00")) +
                    Convert.ToDecimal(totalVATAmount.ToString("#,##0.00")) +
                    Convert.ToDecimal(totalNonVAT.ToString("#,##0.00")) +
                    Convert.ToDecimal(totalVATExempt.ToString("#,##0.00")) +
                    Convert.ToDecimal(totalVATZeroRated.ToString("#,##0.00"));
                String totalVATTotalLabel = "\nTotal";
                String totalVatTotalData = "\n" + totalVAT.ToString("#,##0.00");
                graphics.DrawString(totalVATTotalLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalVatTotalData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalVatTotalData, fontArial8Regular).Height;

                // ========
                // 5th Line
                // ========
                Point fifthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point fifthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, fifthLineFirstPoint, fifthLineSecondPoint);

                String counterIdStart = dataSource.CounterIdStart;
                String counterIdEnd = dataSource.CounterIdEnd;

                String startCounterIdLabel = "\nCounter ID Start";
                String startCounterIdData = "\n" + counterIdStart;
                graphics.DrawString(startCounterIdLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(startCounterIdData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(startCounterIdData, fontArial8Regular).Height;

                String endCounterIdLabel = "Counter ID End";
                String endCounterIdData = counterIdEnd;
                graphics.DrawString(endCounterIdLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(endCounterIdData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(endCounterIdData, fontArial8Regular).Height;

                // ========
                // 6th Line
                // ========
                Point sixthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point sixthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, sixthLineFirstPoint, sixthLineSecondPoint);

                Decimal totalCancelledTrnsactionCount = dataSource.TotalCancelledTrnsactionCount;
                Decimal totalCancelledAmount = dataSource.TotalCancelledAmount * currentDeclareRate;

                String totalCancelledTrnsactionCountLabel = "\nCancelled Tx.";
                String totalCancelledTrnsactionCountData = "\n" + totalCancelledTrnsactionCount.ToString("#,##0");
                graphics.DrawString(totalCancelledTrnsactionCountLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalCancelledTrnsactionCountData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalCancelledTrnsactionCountData, fontArial8Regular).Height;

                String totalCancelledAmountLabel = "Cancelled Amount";
                String totalCancelledAmountData = totalCancelledAmount.ToString("#,##0.00");
                graphics.DrawString(totalCancelledAmountLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalCancelledAmountData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalCancelledAmountData, fontArial8Regular).Height;

                // ========
                // 7th Line
                // ========
                Point seventhLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point seventhLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, seventhLineFirstPoint, seventhLineSecondPoint);

                Decimal totalNumberOfTransactions = dataSource.TotalNumberOfTransactions;
                Decimal totalNumberOfSKU = dataSource.TotalNumberOfSKU;
                Decimal totalQuantity = dataSource.TotalQuantity;

                String totalNumberOfTransactionsLabel = "\nNo. of Transactions";
                String totalNumberOfTransactionsData = "\n" + totalNumberOfTransactions.ToString("#,##0");
                graphics.DrawString(totalNumberOfTransactionsLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalNumberOfTransactionsData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalNumberOfTransactionsData, fontArial8Regular).Height;

                String totalNumberOfSKULabel = "No. of SKU";
                String totalNumberOfSKUData = totalNumberOfSKU.ToString("#,##0");
                graphics.DrawString(totalNumberOfSKULabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalNumberOfSKUData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalNumberOfSKUData, fontArial8Regular).Height;

                String totalQuantityLabel = "Total Quantity";
                String totalQuantityData = totalQuantity.ToString("#,##0.00");
                graphics.DrawString(totalQuantityLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalQuantityData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalQuantityData, fontArial8Regular).Height;

                // ========
                // 8th Line
                // ========
                Point eightLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point eightLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, eightLineFirstPoint, eightLineSecondPoint);

                String zReadingFooter = systemCurrent.ZReadingFooter;

                if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Dot Matrix Printer")
                {
                    String zReadingEndLabel = "\n" + zReadingFooter;
                    graphics.DrawString(zReadingEndLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(zReadingEndLabel, fontArial8Regular).Height;
                }
                else
                {
                    String zReadingEndLabel = "\n" + zReadingFooter;
                    graphics.DrawString(zReadingEndLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(zReadingEndLabel, fontArial8Regular).Height;
                }
            }
        }
    }
}
