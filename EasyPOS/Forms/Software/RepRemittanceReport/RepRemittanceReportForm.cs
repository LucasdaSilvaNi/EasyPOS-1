using System;
using System.Collections;
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

namespace EasyPOS.Forms.Software.RepRemittanceReport
{
    public partial class RepRemittanceReportForm : Form
    {
        private Modules.SysUserRightsModule sysUserRights;
        public RepRemittanceForm repRemittanceReportForm;

        public Int32 filterTerminalId;
        public DateTime filterStartDate;
        public DateTime filterEndDate;
        public Int32 filterUserId;
        public Int32 filterDisbursementId;

        public Entities.RepRemitanceReportEntity remitanceReportEntity;

        public RepRemittanceReportForm(RepRemittanceForm remittanceReportForm, DateTime startDate, DateTime endDate, Int32 terminalId, Int32 userId, Int32 disbursementId)
        {
            InitializeComponent();

            sysUserRights = new Modules.SysUserRightsModule("RepDisbursementRemittance");
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

            repRemittanceReportForm = remittanceReportForm;

            filterStartDate = startDate;
            filterEndDate = endDate;
            filterTerminalId = terminalId;
            filterUserId = userId;
            filterDisbursementId = disbursementId;

            if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Dot Matrix Printer")
            {
                printDocumentRemittanceReport.DefaultPageSettings.PaperSize = new PaperSize("Remittance Report", 255, 1000);
                RemittanceReportDataSource();
            }
            else if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Thermal Printer")
            {
                printDocumentRemittanceReport.DefaultPageSettings.PaperSize = new PaperSize("Remittance Report", 280, 1000);
                RemittanceReportDataSource();
            }
            else if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "HP A799")
            {
                printDocumentRemittanceReport.DefaultPageSettings.PaperSize = new PaperSize("Remittance Report", 280, 1000);
                RemittanceReportDataSource();
            }
            else
            {
                printDocumentRemittanceReport.DefaultPageSettings.PaperSize = new PaperSize("Remittance Report", 175, 1000);
                RemittanceReportDataSource();
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            DialogResult printerDialogResult = printDialogRemittanceReport.ShowDialog();
            if (printerDialogResult == DialogResult.OK)
            {
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
            printDocumentRemittanceReport.Print();
        }

        public void RemittanceReportDataSource()
        {
            Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

            Entities.RepRemitanceReportEntity repRemitanceReportEntity = new Entities.RepRemitanceReportEntity()
            {
                Terminal = "",
                PreparedBy = "",
                RemittanceDate = DateTime.Today.ToShortDateString(),
                CollectionLines = new List<Entities.TrnCollectionLineEntity>(),
                Disbursements = new List<Entities.TrnDisbursementEntity>(),
                DisbursementNumber = "0000000000",
                DisbursementType = "NA",
                PayType = "NA",
                TotalCollection = 0,
                Amount1000 = 0,
                Amount500 = 0,
                Amount200 = 0,
                Amount100 = 0,
                Amount50 = 0,
                Amount20 = 0,
                Amount10 = 0,
                Amount5 = 0,
                Amount1 = 0,
                Amount025 = 0,
                Amount010 = 0,
                Amount005 = 0,
                Amount001 = 0,
                RemittedAmount = 0,
                CashCollectedAmount = 0,
                CashInAmount = 0,
                CashOutAmount = 0,
                OverShortAmount = 0,
            };

            var terminal = from d in db.MstTerminals where d.Id == filterTerminalId select d;
            if (terminal.Any())
            {
                repRemitanceReportEntity.Terminal = terminal.FirstOrDefault().Terminal;
            }

            var preparedBy = from d in db.MstUsers where d.Id == filterUserId select d;
            if (preparedBy.Any())
            {
                repRemitanceReportEntity.PreparedBy = preparedBy.FirstOrDefault().FullName;
            }

            var collectionLines = from d in db.TrnCollectionLines
                                  where d.TrnCollection.TerminalId == filterTerminalId
                                  && d.TrnCollection.CollectionDate >= filterStartDate
                                  && d.TrnCollection.CollectionDate <= filterEndDate
                                  && d.TrnCollection.PreparedBy == filterUserId
                                  && d.TrnCollection.IsLocked == true
                                  group d by new
                                  {
                                      d.MstPayType.PayTypeCode,
                                      d.MstPayType.PayType,
                                  } into g
                                  select new
                                  {
                                      g.Key.PayTypeCode,
                                      g.Key.PayType,
                                      Amount = g.Sum(s => s.TrnCollection.IsCancelled == true ? 0 : s.MstPayType.PayTypeCode.Equals("CASH") ? s.Amount - s.TrnCollection.ChangeAmount : s.Amount)
                                  };

            if (collectionLines.Any())
            {
                foreach (var collectionLine in collectionLines)
                {
                    repRemitanceReportEntity.CollectionLines.Add(new Entities.TrnCollectionLineEntity()
                    {
                        PayType = collectionLine.PayType,
                        Amount = collectionLine.Amount
                    });
                }

                repRemitanceReportEntity.TotalCollection = collectionLines.Sum(d => d.Amount);
                repRemitanceReportEntity.CashCollectedAmount = collectionLines.Where(d => d.PayTypeCode == "CASH").Any() ? collectionLines.Where(d => d.PayTypeCode == "CASH").Sum(d => d.Amount) : 0;
            }

            var cashIns = from d in db.TrnDisbursements
                          where d.Id != filterDisbursementId
                          && d.TerminalId == filterTerminalId
                          && d.DisbursementDate >= filterStartDate
                          && d.DisbursementDate <= filterEndDate
                          && d.PreparedBy == filterUserId
                          && d.DisbursementType == "DEBIT"
                          && d.MstPayType.PayTypeCode == "CASH"
                          group d by new
                          {
                              d.MstPayType.PayType,
                          } into g
                          select new
                          {
                              g.Key.PayType,
                              Amount = g.Sum(s => s.Amount)
                          };

            if (cashIns.Any())
            {
                foreach (var cashIn in cashIns)
                {
                    repRemitanceReportEntity.Disbursements.Add(new Entities.TrnDisbursementEntity()
                    {
                        PayType = cashIn.PayType,
                        Amount = cashIn.Amount
                    });
                }

                repRemitanceReportEntity.CashInAmount = cashIns.Sum(d => d.Amount);
            }

            var cashOuts = from d in db.TrnDisbursements
                           where d.Id != filterDisbursementId
                           && d.TerminalId == filterTerminalId
                           && d.DisbursementDate >= filterStartDate
                           && d.DisbursementDate <= filterEndDate
                           && d.PreparedBy == filterUserId
                           && d.DisbursementType == "CREDIT"
                           && d.MstPayType.PayTypeCode == "CASH"
                           group d by new
                           {
                               d.MstPayType.PayType,
                           } into g
                           select new
                           {
                               g.Key.PayType,
                               Amount = g.Sum(s => s.Amount)
                           };

            if (cashOuts.Any())
            {
                foreach (var cashOut in cashOuts)
                {
                    repRemitanceReportEntity.Disbursements.Add(new Entities.TrnDisbursementEntity()
                    {
                        PayType = cashOut.PayType,
                        Amount = cashOut.Amount
                    });
                }

                repRemitanceReportEntity.CashOutAmount = cashOuts.Sum(d => d.Amount);
            }

            var remittance = from d in db.TrnDisbursements
                             where d.Id == filterDisbursementId
                             && d.TerminalId == filterTerminalId
                             && d.DisbursementDate >= filterStartDate
                             && d.DisbursementDate <= filterEndDate
                             && d.PreparedBy == filterUserId
                             && d.DisbursementType == "CREDIT"
                             && d.MstPayType.PayTypeCode == "CASH"
                             && d.IsLocked == true
                             select d;

            if (remittance.Any())
            {
                repRemitanceReportEntity.DisbursementNumber = remittance.FirstOrDefault().DisbursementNumber;
                repRemitanceReportEntity.RemittanceDate = remittance.FirstOrDefault().DisbursementDate.ToShortDateString();
                repRemitanceReportEntity.DisbursementType = remittance.FirstOrDefault().DisbursementType;
                repRemitanceReportEntity.PayType = remittance.FirstOrDefault().MstPayType.PayType;
                repRemitanceReportEntity.Amount1000 = remittance.FirstOrDefault().Amount1000 != null ? remittance.FirstOrDefault().Amount1000 : 0;
                repRemitanceReportEntity.Amount500 = remittance.FirstOrDefault().Amount500 != null ? remittance.FirstOrDefault().Amount500 : 0;
                repRemitanceReportEntity.Amount200 = remittance.FirstOrDefault().Amount200 != null ? remittance.FirstOrDefault().Amount200 : 0;
                repRemitanceReportEntity.Amount100 = remittance.FirstOrDefault().Amount100 != null ? remittance.FirstOrDefault().Amount100 : 0;
                repRemitanceReportEntity.Amount50 = remittance.FirstOrDefault().Amount50 != null ? remittance.FirstOrDefault().Amount50 : 0;
                repRemitanceReportEntity.Amount20 = remittance.FirstOrDefault().Amount20 != null ? remittance.FirstOrDefault().Amount20 : 0;
                repRemitanceReportEntity.Amount10 = remittance.FirstOrDefault().Amount10 != null ? remittance.FirstOrDefault().Amount10 : 0;
                repRemitanceReportEntity.Amount5 = remittance.FirstOrDefault().Amount5 != null ? remittance.FirstOrDefault().Amount5 : 0;
                repRemitanceReportEntity.Amount1 = remittance.FirstOrDefault().Amount1 != null ? remittance.FirstOrDefault().Amount1 : 0;
                repRemitanceReportEntity.Amount025 = remittance.FirstOrDefault().Amount025 != null ? remittance.FirstOrDefault().Amount025 : 0;
                repRemitanceReportEntity.Amount010 = remittance.FirstOrDefault().Amount010 != null ? remittance.FirstOrDefault().Amount010 : 0;
                repRemitanceReportEntity.Amount005 = remittance.FirstOrDefault().Amount005 != null ? remittance.FirstOrDefault().Amount005 : 0;
                repRemitanceReportEntity.Amount001 = remittance.FirstOrDefault().Amount001 != null ? remittance.FirstOrDefault().Amount001 : 0;

                Decimal totalRemittedAmount = 0;

                totalRemittedAmount = (1000 * (decimal)repRemitanceReportEntity.Amount1000)
                                    + (500 * (decimal)repRemitanceReportEntity.Amount500)
                                    + (200 * (decimal)repRemitanceReportEntity.Amount200)
                                    + (100 * (decimal)repRemitanceReportEntity.Amount100)
                                    + (50 * (decimal)repRemitanceReportEntity.Amount50)
                                    + (20 * (decimal)repRemitanceReportEntity.Amount20)
                                    + (10 * (decimal)repRemitanceReportEntity.Amount10)
                                    + (5 * (decimal)repRemitanceReportEntity.Amount5)
                                    + (1 * (decimal)repRemitanceReportEntity.Amount1)
                                    + (0.25m * (decimal)repRemitanceReportEntity.Amount025)
                                    + (0.10m * (decimal)repRemitanceReportEntity.Amount010)
                                    + (0.05m * (decimal)repRemitanceReportEntity.Amount005)
                                    + (0.01m * (decimal)repRemitanceReportEntity.Amount001);

                repRemitanceReportEntity.RemittedAmount = totalRemittedAmount;
            }

            repRemitanceReportEntity.OverShortAmount = repRemitanceReportEntity.RemittedAmount - (repRemitanceReportEntity.CashCollectedAmount + (repRemitanceReportEntity.CashInAmount - repRemitanceReportEntity.CashOutAmount));

            remitanceReportEntity = repRemitanceReportEntity;
        }

        private void printDocumentRemittanceReport_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            var dataSource = remitanceReportEntity;

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
            else if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "HP A799")
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

            // ============
            // Company Name
            // ============
            String companyName = systemCurrent.CompanyName;
            graphics.DrawString(companyName, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += graphics.MeasureString(companyName, fontArial8Regular).Height;

            // ===============
            // Company Address
            // ===============
            String companyAddress = systemCurrent.Address;

            float adjuctHeight = 1;
            if (companyAddress.Length > 43)
            {
                adjuctHeight = 2;
            }

            graphics.DrawString(companyAddress, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += (graphics.MeasureString(companyAddress, fontArial8Regular).Height * adjuctHeight);

            // =======================
            // Remittance Report Title
            // =======================
            String remittanceReportTitle = "Remittance Report";
            graphics.DrawString(remittanceReportTitle, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += graphics.MeasureString(remittanceReportTitle, fontArial8Bold).Height;

            // ====
            // Date 
            // ====
            String collectionDateText = "From " + filterStartDate.ToShortDateString() + " To " + filterEndDate.ToShortDateString();
            graphics.DrawString(collectionDateText, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += graphics.MeasureString(collectionDateText, fontArial8Regular).Height;

            // ========
            // Terminal 
            // ========
            String terminal = "Terminal: " + dataSource.Terminal;
            graphics.DrawString(terminal, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += graphics.MeasureString(terminal, fontArial8Regular).Height;

            // ===========
            // Prepared By 
            // ===========
            String preparedBy = "Prepared By: " + dataSource.PreparedBy;
            graphics.DrawString(preparedBy, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            y += graphics.MeasureString(preparedBy, fontArial8Regular).Height;

            //// ========
            //// 1st Line
            //// ========
            //Point firstLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
            //Point firstLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
            //graphics.DrawLine(blackPen, firstLineFirstPoint, firstLineSecondPoint);

            // ========
            // 2nd Line
            // ========
            Point secondLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
            Point secondLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
            graphics.DrawLine(blackPen, secondLineFirstPoint, secondLineSecondPoint);
            if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "58mm Printer")
            {
                // ================
                // Collection Title
                // ================
                String collectionTitle = "\nCollection";
                graphics.DrawString(collectionTitle, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(collectionTitle, fontArial7Bold).Height;

                // ========
                // 3rd Line
                // ========
                Point thirdLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point thirdLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, thirdLineFirstPoint, thirdLineSecondPoint);

                // ==============================
                // Collection Pay Type and Amount
                // ==============================
                String collectionPayTypeLabel = "\nPay Type";
                String collectionAmountLabel = "\nReceived Amount";
                graphics.DrawString(collectionPayTypeLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(collectionAmountLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(collectionAmountLabel, fontArial7Bold).Height;

                if (dataSource.CollectionLines.Any())
                {
                    foreach (var collectionLine in dataSource.CollectionLines)
                    {
                        // ================
                        // Collection Lines
                        // ================
                        String collectionLineLabel = collectionLine.PayType;
                        String collectionLineData = collectionLine.Amount.ToString("#,##0.00");
                        graphics.DrawString(collectionLineLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                        graphics.DrawString(collectionLineData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                        y += graphics.MeasureString(collectionLineData, fontArial7Regular).Height;
                    }
                }
                else
                {
                    // ================
                    // Collection Lines
                    // ================
                    String collectionLineLabel = "Cash";
                    String collectionLineData = "0.00";
                    graphics.DrawString(collectionLineLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(collectionLineData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(collectionLineData, fontArial7Regular).Height;
                }

                // ================
                // Collection Lines
                // ================
                String totalCollectionLineLabel = "Total Collection";
                String totalCollectionLineData = dataSource.TotalCollection.ToString("#,##0.00");
                graphics.DrawString(totalCollectionLineLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalCollectionLineData, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalCollectionLineData, fontArial7Bold).Height;

                // =========
                // 4rth Line
                // =========
                Point forthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point forthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, forthLineFirstPoint, forthLineSecondPoint);

                // ==================
                // Disbursement Title
                // ==================
                String disbursementTitle = "\nCash In/Out";
                graphics.DrawString(disbursementTitle, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(disbursementTitle, fontArial7Bold).Height;

                // ========
                // 5th Line
                // ========
                Point fifthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point fifthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, fifthLineFirstPoint, fifthLineSecondPoint);

                // ================================
                // Disbursement Pay Type and Amount
                // ================================
                String disbursementPayTypeLabel = "\nPay Type";
                String disbursementAmountLabel = "\nAmount";
                graphics.DrawString(disbursementPayTypeLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(disbursementAmountLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(disbursementAmountLabel, fontArial7Bold).Height;

                //if (dataSource.Disbursements.Any())
                //{
                //    foreach (var disbursement in dataSource.Disbursements)
                //    {
                //        // ============
                //        // Disbursement
                //        // ============
                //        String disbursementLabel = disbursement.PayType;
                //        String disbursementData = disbursement.Amount.ToString("#,##0.00");
                //        graphics.DrawString(disbursementLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                //        graphics.DrawString(disbursementData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                //        y += graphics.MeasureString(disbursementData, fontArial8Regular).Height;
                //    }
                //}
                //else
                //{
                //    // ============
                //    // Disbursement
                //    // ============
                //    String disbursementLabel = "Cash";
                //    String disbursementData = "0.00";
                //    graphics.DrawString(disbursementLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                //    graphics.DrawString(disbursementData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                //    y += graphics.MeasureString(disbursementData, fontArial8Regular).Height;
                //}

                // =======
                // Cash In
                // =======
                String disbursementTotalCashInLabel = "Cash In";
                String disbursementTotalCashInData = dataSource.CashInAmount.ToString("#,##0.00");
                graphics.DrawString(disbursementTotalCashInLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(disbursementTotalCashInData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(disbursementTotalCashInData, fontArial7Bold).Height;

                // ========
                // Cash Out
                // ========
                String disbursementTotalCashOutLabel = "Cash Out";
                String disbursementTotalCashOutData = dataSource.CashOutAmount.ToString("#,##0.00");
                graphics.DrawString(disbursementTotalCashOutLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(disbursementTotalCashOutData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(disbursementTotalCashOutData, fontArial7Bold).Height;

                // ========
                // 6th Line
                // ========
                Point sixthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point sixthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, sixthLineFirstPoint, sixthLineSecondPoint);

                // ===================
                // Disbursement Number
                // ===================
                String disbursementNumberLabel = "\nCash In/Out Number";
                String disbursementNumberData = "\n" + dataSource.DisbursementNumber;
                graphics.DrawString(disbursementNumberLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(disbursementNumberData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(disbursementNumberData, fontArial7Regular).Height;

                // =================
                // Disbursement Date
                // =================
                String disbursementDateLabel = "Cash In/Out Date";
                String disbursementDateData = dataSource.RemittanceDate;
                graphics.DrawString(disbursementDateLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(disbursementDateData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(disbursementDateData, fontArial7Regular).Height;

                // =================
                // Disbursement Type
                // =================
                String disbursementTypeLabel = "Cash In/Out Type";
                String disbursementTypeData = dataSource.DisbursementType;
                graphics.DrawString(disbursementTypeLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(disbursementTypeData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(disbursementTypeData, fontArial7Regular).Height;

                // ========
                // Pay Type
                // ========
                String payTypeLabel = "Pay Type";
                String payTypeData = dataSource.PayType;
                graphics.DrawString(payTypeLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(payTypeData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(payTypeData, fontArial7Regular).Height;

                // ========
                // 7th Line
                // ========
                Point seventhLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point seventhLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, seventhLineFirstPoint, seventhLineSecondPoint);

                // =========================
                // Amount Denomination Title
                // =========================
                String amountDenominationTitle = "\nAmount Denomination";
                graphics.DrawString(amountDenominationTitle, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(amountDenominationTitle, fontArial7Bold).Height;

                // ========
                // 8th Line
                // ========
                Point eightLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point eightLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, eightLineFirstPoint, eightLineSecondPoint);

                // ==================
                // Decimal Conversion
                // ==================
                Decimal amount1000 = Convert.ToDecimal(dataSource.Amount1000);
                Decimal amount500 = Convert.ToDecimal(dataSource.Amount500);
                Decimal amount200 = Convert.ToDecimal(dataSource.Amount200);
                Decimal amount100 = Convert.ToDecimal(dataSource.Amount100);
                Decimal amount50 = Convert.ToDecimal(dataSource.Amount50);
                Decimal amount20 = Convert.ToDecimal(dataSource.Amount20);
                Decimal amount10 = Convert.ToDecimal(dataSource.Amount10);
                Decimal amount5 = Convert.ToDecimal(dataSource.Amount5);
                Decimal amount1 = Convert.ToDecimal(dataSource.Amount1);
                Decimal amount025 = Convert.ToDecimal(dataSource.Amount025);
                Decimal amount010 = Convert.ToDecimal(dataSource.Amount010);
                Decimal amount005 = Convert.ToDecimal(dataSource.Amount005);
                Decimal amount001 = Convert.ToDecimal(dataSource.Amount001);
                Decimal remittedAmount = Convert.ToDecimal(dataSource.RemittedAmount);
                Decimal cashCollectedAmount = Convert.ToDecimal(dataSource.CashCollectedAmount);
                Decimal cashInAmount = Convert.ToDecimal(dataSource.CashInAmount);
                Decimal cashOutAmount = Convert.ToDecimal(dataSource.CashOutAmount);
                Decimal overShortAmount = Convert.ToDecimal(dataSource.OverShortAmount);

                // ==========
                // Amount1000
                // ==========
                String amount1000Label = "\nAmount 1,000.00 x " + amount1000.ToString("#,##0");
                String amount1000Data = "\n" + Convert.ToDecimal(amount1000 * 1000).ToString("#,##0.00");
                graphics.DrawString(amount1000Label, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount1000Data, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount1000Data, fontArial7Regular).Height;

                // ==========
                // Amount500
                // ==========
                String amount500Label = "Amount 500.00 x " + amount500.ToString("#,##0");
                String amount500Data = Convert.ToDecimal(amount500 * 500).ToString("#,##0.00");
                graphics.DrawString(amount500Label, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount500Data, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount500Data, fontArial7Regular).Height;

                // =========
                // Amount200
                // =========
                String amount200Label = "Amount 200.00 x " + amount200.ToString("#,##0");
                String amount200Data = Convert.ToDecimal(amount200 * 200).ToString("#,##0.00");
                graphics.DrawString(amount200Label, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount200Data, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount200Data, fontArial7Regular).Height;

                // =========
                // Amount100
                // =========
                String amount100Label = "Amount 100.00 x " + amount100.ToString("#,##0");
                String amount100Data = Convert.ToDecimal(amount100 * 100).ToString("#,##0.00");
                graphics.DrawString(amount100Label, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount100Data, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount100Data, fontArial7Regular).Height;

                // ========
                // Amount50
                // ========
                String amount50Label = "Amount 50.00 x " + amount50.ToString("#,##0");
                String amount50Data = Convert.ToDecimal(amount50 * 50).ToString("#,##0.00");
                graphics.DrawString(amount50Label, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount50Data, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount50Data, fontArial7Regular).Height;

                // ========
                // Amount20
                // ========
                String amount20Label = "Amount 20.00 x " + amount20.ToString("#,##0");
                String amount20Data = Convert.ToDecimal(amount20 * 20).ToString("#,##0.00");
                graphics.DrawString(amount20Label, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount20Data, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount20Data, fontArial7Regular).Height;

                // ========
                // Amount10
                // ========
                String amount10Label = "Amount 10.00 x " + amount10.ToString("#,##0");
                String amount10Data = Convert.ToDecimal(amount10 * 10).ToString("#,##0.00");
                graphics.DrawString(amount10Label, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount10Data, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount10Data, fontArial7Regular).Height;

                // ========
                // Amount 5
                // ========
                String amount5Label = "Amount 5.00 x " + amount5.ToString("#,##0");
                String amount5Data = Convert.ToDecimal(amount5 * 5).ToString("#,##0.00");
                graphics.DrawString(amount5Label, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount5Data, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount5Data, fontArial7Regular).Height;

                // ========
                // Amount 1
                // ========
                String amount1Label = "Amount 1.00 x " + amount1.ToString("#,##0");
                String amount1Data = Convert.ToDecimal(amount1 * 1).ToString("#,##0");
                graphics.DrawString(amount1Label, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount1Data, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount1Data, fontArial7Regular).Height;

                // ==========
                // Amount 025
                // ==========
                String amount025Label = "Amount 0.25 x " + amount025.ToString("#,##0");
                String amount025Data = Convert.ToDecimal(amount1 * 0.25m).ToString("#,##0.00");
                graphics.DrawString(amount025Label, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount025Data, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount025Data, fontArial7Regular).Height;

                // ==========
                // Amount 010
                // ==========
                String amount010Label = "Amount 0.10 x " + amount010.ToString("#,##0");
                String amount010Data = amount010.ToString("#,##0.00");
                graphics.DrawString(amount010Label, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount010Data, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount010Data, fontArial7Regular).Height;

                // ==========
                // Amount 005
                // ==========
                String amount005Label = "Amount 0.05 x " + amount005.ToString("#,##0");
                String amount005Data = Convert.ToDecimal(amount1 * 0.05m).ToString("#,##0.00");
                graphics.DrawString(amount005Label, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount005Data, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount005Data, fontArial7Regular).Height;

                // ==========
                // Amount 001
                // ==========
                String amount001Label = "Amount 0.01 x " + amount001.ToString("#,##0");
                String amount001Data = Convert.ToDecimal(amount1 * 0.01m).ToString("#,##0.00");
                graphics.DrawString(amount001Label, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount001Data, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount001Data, fontArial7Regular).Height;

                // ========
                // 9th Line
                // ========
                Point ninethLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point ninethLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, ninethLineFirstPoint, ninethLineSecondPoint);

                // ===============
                // Remitted Amount
                // ===============
                String remittedAmountLabel = "\nRemitted Cash";
                String remittedAmountData = "\n" + remittedAmount.ToString("#,##0.00");
                graphics.DrawString(remittedAmountLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(remittedAmountData, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(remittedAmountData, fontArial7Bold).Height;

                // ================
                // Collected Amount
                // ================
                String cashCollectedAmountLabel = "Cash Collected";
                String cashCollectedAmountData = cashCollectedAmount.ToString("#,##0.00");
                graphics.DrawString(cashCollectedAmountLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(cashCollectedAmountData, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(cashCollectedAmountData, fontArial7Bold).Height;

                // ================
                // Cash In - Amount
                // ================
                String cashInAmountLabel = "Cash In";
                String cashInAmountData = cashInAmount.ToString("#,##0.00");
                graphics.DrawString(cashInAmountLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(cashInAmountData, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(cashInAmountData, fontArial7Bold).Height;

                // =================
                // Cash Out - Amount
                // =================
                String cashInOutAmountLabel = "Cash Out";
                String cashInOutAmountData = cashOutAmount.ToString("#,##0.00");
                graphics.DrawString(cashInOutAmountLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(cashInOutAmountData, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(cashInOutAmountData, fontArial7Bold).Height;

                // ====================
                // Over or Short Amount
                // ====================
                String overShortAmountLabel = "";
                if (overShortAmount > 0)
                {
                    overShortAmountLabel = "Over";
                }
                else if (overShortAmount < 0)
                {
                    overShortAmountLabel = "Short";
                }
                else
                {
                    overShortAmountLabel = "";
                }

                if (String.IsNullOrEmpty(overShortAmountLabel) == false)
                {
                    String overShortAmountData = overShortAmount.ToString("#,##0.00");
                    graphics.DrawString(overShortAmountLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(overShortAmountData, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(overShortAmountData, fontArial7Bold).Height;
                }

                // =========
                // 13th Line
                // =========
                Point thirheenthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point thirtheenthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, thirheenthLineFirstPoint, thirtheenthLineSecondPoint);
            }
            else
            {
                // ================
                // Collection Title
                // ================
                String collectionTitle = "\nCollection";
                graphics.DrawString(collectionTitle, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(collectionTitle, fontArial8Bold).Height;

                // ========
                // 3rd Line
                // ========
                Point thirdLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point thirdLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, thirdLineFirstPoint, thirdLineSecondPoint);

                // ==============================
                // Collection Pay Type and Amount
                // ==============================
                String collectionPayTypeLabel = "\nPay Type";
                String collectionAmountLabel = "\nReceived Amount";
                graphics.DrawString(collectionPayTypeLabel, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(collectionAmountLabel, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(collectionAmountLabel, fontArial8Bold).Height;

                if (dataSource.CollectionLines.Any())
                {
                    foreach (var collectionLine in dataSource.CollectionLines)
                    {
                        // ================
                        // Collection Lines
                        // ================
                        String collectionLineLabel = collectionLine.PayType;
                        String collectionLineData = collectionLine.Amount.ToString("#,##0.00");
                        graphics.DrawString(collectionLineLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                        graphics.DrawString(collectionLineData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                        y += graphics.MeasureString(collectionLineData, fontArial8Regular).Height;
                    }
                }
                else
                {
                    // ================
                    // Collection Lines
                    // ================
                    String collectionLineLabel = "Cash";
                    String collectionLineData = "0.00";
                    graphics.DrawString(collectionLineLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(collectionLineData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(collectionLineData, fontArial8Regular).Height;
                }

                // ================
                // Collection Lines
                // ================
                String totalCollectionLineLabel = "Total Collection";
                String totalCollectionLineData = dataSource.TotalCollection.ToString("#,##0.00");
                graphics.DrawString(totalCollectionLineLabel, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalCollectionLineData, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalCollectionLineData, fontArial8Bold).Height;

                // =========
                // 4rth Line
                // =========
                Point forthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point forthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, forthLineFirstPoint, forthLineSecondPoint);

                // ==================
                // Disbursement Title
                // ==================
                String disbursementTitle = "\nCash In/Out";
                graphics.DrawString(disbursementTitle, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(disbursementTitle, fontArial8Bold).Height;

                // ========
                // 5th Line
                // ========
                Point fifthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point fifthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, fifthLineFirstPoint, fifthLineSecondPoint);

                // ================================
                // Disbursement Pay Type and Amount
                // ================================
                String disbursementPayTypeLabel = "\nPay Type";
                String disbursementAmountLabel = "\nAmount";
                graphics.DrawString(disbursementPayTypeLabel, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(disbursementAmountLabel, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(disbursementAmountLabel, fontArial8Bold).Height;

                //if (dataSource.Disbursements.Any())
                //{
                //    foreach (var disbursement in dataSource.Disbursements)
                //    {
                //        // ============
                //        // Disbursement
                //        // ============
                //        String disbursementLabel = disbursement.PayType;
                //        String disbursementData = disbursement.Amount.ToString("#,##0.00");
                //        graphics.DrawString(disbursementLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                //        graphics.DrawString(disbursementData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                //        y += graphics.MeasureString(disbursementData, fontArial8Regular).Height;
                //    }
                //}
                //else
                //{
                //    // ============
                //    // Disbursement
                //    // ============
                //    String disbursementLabel = "Cash";
                //    String disbursementData = "0.00";
                //    graphics.DrawString(disbursementLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                //    graphics.DrawString(disbursementData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                //    y += graphics.MeasureString(disbursementData, fontArial8Regular).Height;
                //}

                // =======
                // Cash In
                // =======
                String disbursementTotalCashInLabel = "Cash In";
                String disbursementTotalCashInData = dataSource.CashInAmount.ToString("#,##0.00");
                graphics.DrawString(disbursementTotalCashInLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(disbursementTotalCashInData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(disbursementTotalCashInData, fontArial8Bold).Height;

                // ========
                // Cash Out
                // ========
                String disbursementTotalCashOutLabel = "Cash Out";
                String disbursementTotalCashOutData = dataSource.CashOutAmount.ToString("#,##0.00");
                graphics.DrawString(disbursementTotalCashOutLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(disbursementTotalCashOutData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(disbursementTotalCashOutData, fontArial8Bold).Height;

                // ========
                // 6th Line
                // ========
                Point sixthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point sixthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, sixthLineFirstPoint, sixthLineSecondPoint);

                // ===================
                // Disbursement Number
                // ===================
                String disbursementNumberLabel = "\nCash In/Out Number";
                String disbursementNumberData = "\n" + dataSource.DisbursementNumber;
                graphics.DrawString(disbursementNumberLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(disbursementNumberData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(disbursementNumberData, fontArial8Regular).Height;

                // =================
                // Disbursement Date
                // =================
                String disbursementDateLabel = "Cash In/Out Date";
                String disbursementDateData = dataSource.RemittanceDate;
                graphics.DrawString(disbursementDateLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(disbursementDateData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(disbursementDateData, fontArial8Regular).Height;

                // =================
                // Disbursement Type
                // =================
                String disbursementTypeLabel = "Cash In/Out Type";
                String disbursementTypeData = dataSource.DisbursementType;
                graphics.DrawString(disbursementTypeLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(disbursementTypeData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(disbursementTypeData, fontArial8Regular).Height;

                // ========
                // Pay Type
                // ========
                String payTypeLabel = "Pay Type";
                String payTypeData = dataSource.PayType;
                graphics.DrawString(payTypeLabel, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(payTypeData, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(payTypeData, fontArial8Regular).Height;

                // ========
                // 7th Line
                // ========
                Point seventhLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point seventhLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, seventhLineFirstPoint, seventhLineSecondPoint);

                // =========================
                // Amount Denomination Title
                // =========================
                String amountDenominationTitle = "\nAmount Denomination";
                graphics.DrawString(amountDenominationTitle, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(amountDenominationTitle, fontArial8Bold).Height;

                // ========
                // 8th Line
                // ========
                Point eightLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point eightLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, eightLineFirstPoint, eightLineSecondPoint);

                // ==================
                // Decimal Conversion
                // ==================
                Decimal amount1000 = Convert.ToDecimal(dataSource.Amount1000);
                Decimal amount500 = Convert.ToDecimal(dataSource.Amount500);
                Decimal amount200 = Convert.ToDecimal(dataSource.Amount200);
                Decimal amount100 = Convert.ToDecimal(dataSource.Amount100);
                Decimal amount50 = Convert.ToDecimal(dataSource.Amount50);
                Decimal amount20 = Convert.ToDecimal(dataSource.Amount20);
                Decimal amount10 = Convert.ToDecimal(dataSource.Amount10);
                Decimal amount5 = Convert.ToDecimal(dataSource.Amount5);
                Decimal amount1 = Convert.ToDecimal(dataSource.Amount1);
                Decimal amount025 = Convert.ToDecimal(dataSource.Amount025);
                Decimal amount010 = Convert.ToDecimal(dataSource.Amount010);
                Decimal amount005 = Convert.ToDecimal(dataSource.Amount005);
                Decimal amount001 = Convert.ToDecimal(dataSource.Amount001);
                Decimal remittedAmount = Convert.ToDecimal(dataSource.RemittedAmount);
                Decimal cashCollectedAmount = Convert.ToDecimal(dataSource.CashCollectedAmount);
                Decimal cashInAmount = Convert.ToDecimal(dataSource.CashInAmount);
                Decimal cashOutAmount = Convert.ToDecimal(dataSource.CashOutAmount);
                Decimal overShortAmount = Convert.ToDecimal(dataSource.OverShortAmount);

                // ==========
                // Amount1000
                // ==========
                String amount1000Label = "\nAmount 1,000.00 x " + amount1000.ToString("#,##0");
                String amount1000Data = "\n" + Convert.ToDecimal(amount1000 * 1000).ToString("#,##0.00");
                graphics.DrawString(amount1000Label, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount1000Data, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount1000Data, fontArial8Regular).Height;

                // ==========
                // Amount500
                // ==========
                String amount500Label = "Amount 500.00 x " + amount500.ToString("#,##0");
                String amount500Data = Convert.ToDecimal(amount500 * 500).ToString("#,##0.00");
                graphics.DrawString(amount500Label, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount500Data, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount500Data, fontArial8Regular).Height;

                // =========
                // Amount200
                // =========
                String amount200Label = "Amount 200.00 x " + amount200.ToString("#,##0");
                String amount200Data = Convert.ToDecimal(amount200 * 200).ToString("#,##0.00");
                graphics.DrawString(amount200Label, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount200Data, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount200Data, fontArial8Regular).Height;

                // =========
                // Amount100
                // =========
                String amount100Label = "Amount 100.00 x " + amount100.ToString("#,##0");
                String amount100Data = Convert.ToDecimal(amount100 * 100).ToString("#,##0.00");
                graphics.DrawString(amount100Label, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount100Data, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount100Data, fontArial8Regular).Height;

                // ========
                // Amount50
                // ========
                String amount50Label = "Amount 50.00 x " + amount50.ToString("#,##0");
                String amount50Data = Convert.ToDecimal(amount50 * 50).ToString("#,##0.00");
                graphics.DrawString(amount50Label, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount50Data, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount50Data, fontArial8Regular).Height;

                // ========
                // Amount20
                // ========
                String amount20Label = "Amount 20.00 x " + amount20.ToString("#,##0");
                String amount20Data = Convert.ToDecimal(amount20 * 20).ToString("#,##0.00");
                graphics.DrawString(amount20Label, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount20Data, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount20Data, fontArial8Regular).Height;

                // ========
                // Amount10
                // ========
                String amount10Label = "Amount 10.00 x " + amount10.ToString("#,##0");
                String amount10Data = Convert.ToDecimal(amount10 * 10).ToString("#,##0.00");
                graphics.DrawString(amount10Label, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount10Data, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount10Data, fontArial8Regular).Height;

                // ========
                // Amount 5
                // ========
                String amount5Label = "Amount 5.00 x " + amount5.ToString("#,##0");
                String amount5Data = Convert.ToDecimal(amount5 * 5).ToString("#,##0.00");
                graphics.DrawString(amount5Label, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount5Data, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount5Data, fontArial8Regular).Height;

                // ========
                // Amount 1
                // ========
                String amount1Label = "Amount 1.00 x " + amount1.ToString("#,##0");
                String amount1Data = Convert.ToDecimal(amount1 * 1).ToString("#,##0");
                graphics.DrawString(amount1Label, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount1Data, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount1Data, fontArial8Regular).Height;

                // ==========
                // Amount 025
                // ==========
                String amount025Label = "Amount 0.25 x " + amount025.ToString("#,##0");
                String amount025Data = Convert.ToDecimal(amount1 * 0.25m).ToString("#,##0.00");
                graphics.DrawString(amount025Label, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount025Data, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount025Data, fontArial8Regular).Height;

                // ==========
                // Amount 010
                // ==========
                String amount010Label = "Amount 0.10 x " + amount010.ToString("#,##0");
                String amount010Data = amount010.ToString("#,##0.00");
                graphics.DrawString(amount010Label, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount010Data, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount010Data, fontArial8Regular).Height;

                // ==========
                // Amount 005
                // ==========
                String amount005Label = "Amount 0.05 x " + amount005.ToString("#,##0");
                String amount005Data = Convert.ToDecimal(amount1 * 0.05m).ToString("#,##0.00");
                graphics.DrawString(amount005Label, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount005Data, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount005Data, fontArial8Regular).Height;

                // ==========
                // Amount 001
                // ==========
                String amount001Label = "Amount 0.01 x " + amount001.ToString("#,##0");
                String amount001Data = Convert.ToDecimal(amount1 * 0.01m).ToString("#,##0.00");
                graphics.DrawString(amount001Label, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(amount001Data, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(amount001Data, fontArial8Regular).Height;

                // ========
                // 9th Line
                // ========
                Point ninethLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point ninethLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, ninethLineFirstPoint, ninethLineSecondPoint);

                // ===============
                // Remitted Amount
                // ===============
                String remittedAmountLabel = "\nRemitted Cash";
                String remittedAmountData = "\n" + remittedAmount.ToString("#,##0.00");
                graphics.DrawString(remittedAmountLabel, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(remittedAmountData, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(remittedAmountData, fontArial8Bold).Height;

                // ================
                // Collected Amount
                // ================
                String cashCollectedAmountLabel = "Cash Collected";
                String cashCollectedAmountData = cashCollectedAmount.ToString("#,##0.00");
                graphics.DrawString(cashCollectedAmountLabel, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(cashCollectedAmountData, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(cashCollectedAmountData, fontArial8Bold).Height;

                // ================
                // Cash In - Amount
                // ================
                String cashInAmountLabel = "Cash In";
                String cashInAmountData = cashInAmount.ToString("#,##0.00");
                graphics.DrawString(cashInAmountLabel, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(cashInAmountData, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(cashInAmountData, fontArial8Bold).Height;

                // =================
                // Cash Out - Amount
                // =================
                String cashInOutAmountLabel = "Cash Out";
                String cashInOutAmountData = cashOutAmount.ToString("#,##0.00");
                graphics.DrawString(cashInOutAmountLabel, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(cashInOutAmountData, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(cashInOutAmountData, fontArial8Bold).Height;

                // ====================
                // Over or Short Amount
                // ====================
                String overShortAmountLabel = "";
                if (overShortAmount > 0)
                {
                    overShortAmountLabel = "Over";
                }
                else if (overShortAmount < 0)
                {
                    overShortAmountLabel = "Short";
                }
                else
                {
                    overShortAmountLabel = "";
                }

                if (String.IsNullOrEmpty(overShortAmountLabel) == false)
                {
                    String overShortAmountData = overShortAmount.ToString("#,##0.00");
                    graphics.DrawString(overShortAmountLabel, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(overShortAmountData, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(overShortAmountData, fontArial8Bold).Height;
                }

                // =========
                // 13th Line
                // =========
                Point thirheenthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point thirtheenthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, thirheenthLineFirstPoint, thirtheenthLineSecondPoint);
            }

        }
    }
}