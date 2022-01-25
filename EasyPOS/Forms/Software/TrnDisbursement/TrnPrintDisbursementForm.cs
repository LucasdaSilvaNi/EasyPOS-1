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

namespace EasyPOS.Forms.Software.TrnDisbursement
{
    public partial class TrnPrintDisbursementForm : Form
    {
        public Int32 trnDisbursementId = 0;

        public TrnPrintDisbursementForm(Int32 disbursementId)
        {
            InitializeComponent();

            trnDisbursementId = disbursementId;
            if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Dot Matrix Printer")
            {
                printDocumentOfficialReceipt.DefaultPageSettings.PaperSize = new PaperSize("Disbursement Report", 255, 1000);
                printDocumentOfficialReceipt.Print();
            }
            else if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Thermal Printer")
            {
                printDocumentOfficialReceipt.DefaultPageSettings.PaperSize = new PaperSize("Disbursement Report", 280, 1000);
                printDocumentOfficialReceipt.Print();
            }
            else if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "HP A799")
            {
                printDocumentOfficialReceipt.DefaultPageSettings.PaperSize = new PaperSize("Disbursement Report", 280, 1000);
                printDocumentOfficialReceipt.Print();
            }
            else
            {
                printDocumentOfficialReceipt.DefaultPageSettings.PaperSize = new PaperSize("Disbursement Report", 175, 1000);
                printDocumentOfficialReceipt.Print();
            }
        }

        private void printDocumentOfficialReceipt_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // ============
            // Data Context
            // ============
            Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

            // =============
            // Font Settings
            // =============
            Font fontArial7Bold = new Font("Arial", 7, FontStyle.Bold);
            Font fontArial7Regular = new Font("Arial", 7, FontStyle.Regular);
            Font fontArial8Bold = new Font("Arial", 8, FontStyle.Bold);
            Font fontArial8Regular = new Font("Arial", 8, FontStyle.Regular);
            Font fontArial9Bold = new Font("Arial", 9, FontStyle.Bold);
            Font fontArial9Regular = new Font("Arial", 9, FontStyle.Regular);
            Font fontArial10Bold = new Font("Arial", 10, FontStyle.Bold);
            Font fontArial10Regular = new Font("Arial", 10, FontStyle.Regular);
            Font fontArial11Bold = new Font("Arial", 11, FontStyle.Bold);
            Font fontArial11Regular = new Font("Arial", 11, FontStyle.Regular);
            Font fontArial12Bold = new Font("Arial", 12, FontStyle.Bold);
            Font fontArial12Regular = new Font("Arial", 12, FontStyle.Regular);

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

            // =====
            // Title
            // =====
            if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "58mm Printer")
            {
                String disbursementTitle = "Disbursement";
                graphics.DrawString(disbursementTitle, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(disbursementTitle, fontArial8Bold).Height;

                // ====
                // Line
                // ====
                Point lineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point lineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, lineFirstPoint, lineSecondPoint);

                // =================
                // Collection Header
                // =================
                var disbursement = from d in db.TrnDisbursements where d.Id == trnDisbursementId select d;
                if (disbursement.Any())
                {
                    String disbursementNumber = "\n" + disbursement.FirstOrDefault().DisbursementNumber;
                    graphics.DrawString(disbursementNumber, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(disbursementNumber, fontArial7Regular).Height;

                    String disbursementDate = disbursement.FirstOrDefault().DisbursementDate.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture);
                    graphics.DrawString(disbursementDate, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(disbursementDate, fontArial7Regular).Height;

                    String disbursementType = disbursement.FirstOrDefault().DisbursementType;
                    graphics.DrawString("Type: " + disbursementType, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(disbursementType, fontArial7Regular).Height;

                    String terminal = disbursement.FirstOrDefault().MstTerminal.Terminal;
                    graphics.DrawString("Terminal: " + terminal, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(terminal, fontArial7Regular).Height;

                    String payType = disbursement.FirstOrDefault().MstPayType.PayType;
                    graphics.DrawString("Pay Type: " + payType, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(payType, fontArial7Regular).Height;

                    // ========
                    // 1st Line
                    // ========
                    Point firstLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                    Point firstLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                    graphics.DrawLine(blackPen, firstLineFirstPoint, firstLineSecondPoint);

                    String amountDenomination = "\nAmount Denomination";
                    graphics.DrawString(amountDenomination, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(amountDenomination, fontArial8Bold).Height + 5.0F;

                    String data1000 = disbursement.FirstOrDefault().Amount1000 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount1000).ToString("#,##0.00") : "0.00";
                    String label1000 = "x P1,000";
                    graphics.DrawString(data1000, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label1000, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label1000, fontArial7Regular).Height + 5.0F;

                    String data500 = disbursement.FirstOrDefault().Amount500 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount500).ToString("#,##0.00") : "0.00";
                    String label500 = "x P500";
                    graphics.DrawString(data500, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label500, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label1000, fontArial7Regular).Height + 5.0F;

                    String data200 = disbursement.FirstOrDefault().Amount200 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount200).ToString("#,##0.00") : "0.00";
                    String label200 = "x P200";
                    graphics.DrawString(data200, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label200, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label200, fontArial7Regular).Height + 5.0F;

                    String data100 = disbursement.FirstOrDefault().Amount100 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount100).ToString("#,##0.00") : "0.00";
                    String label100 = "x P100";
                    graphics.DrawString(data100, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label100, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label100, fontArial7Regular).Height + 5.0F;

                    String data50 = disbursement.FirstOrDefault().Amount50 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount50).ToString("#,##0.00") : "0.00";
                    String label50 = "x P50";
                    graphics.DrawString(data50, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label50, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label50, fontArial7Regular).Height + 5.0F;

                    String data20 = disbursement.FirstOrDefault().Amount20 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount20).ToString("#,##0.00") : "0.00";
                    String label20 = "x P20";
                    graphics.DrawString(data20, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label20, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label20, fontArial7Regular).Height + 5.0F;

                    String data10 = disbursement.FirstOrDefault().Amount10 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount10).ToString("#,##0.00") : "0.00";
                    String label10 = "x P10";
                    graphics.DrawString(data10, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label10, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label10, fontArial7Regular).Height + 5.0F;

                    String data5 = disbursement.FirstOrDefault().Amount5 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount5).ToString("#,##0.00") : "0.00";
                    String label5 = "x P5";
                    graphics.DrawString(data5, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label5, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label5, fontArial7Regular).Height + 5.0F;

                    String data1 = disbursement.FirstOrDefault().Amount1 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount1).ToString("#,##0.00") : "0.00";
                    String label1 = "x P1";
                    graphics.DrawString(data1, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label1, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label1, fontArial7Regular).Height + 5.0F;

                    String data025 = disbursement.FirstOrDefault().Amount025 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount025).ToString("#,##0.00") : "0.00";
                    String label025 = "x .25c";
                    graphics.DrawString(data025, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label025, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label025, fontArial7Regular).Height + 5.0F;

                    String data010 = disbursement.FirstOrDefault().Amount010 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount010).ToString("#,##0.00") : "0.00";
                    String label010 = "x .10c";
                    graphics.DrawString(data010, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label010, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label010, fontArial7Regular).Height + 5.0F;

                    String data005 = disbursement.FirstOrDefault().Amount005 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount005).ToString("#,##0.00") : "0.00";
                    String label005 = "x .05c";
                    graphics.DrawString(data005, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label005, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label005, fontArial7Regular).Height + 5.0F;

                    String data001 = disbursement.FirstOrDefault().Amount001 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount001).ToString("#,##0.00") : "0.00";
                    String label001 = "x .01c";
                    graphics.DrawString(data001, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label001, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label001, fontArial7Regular).Height + 5.0F;

                    // ========
                    // 2nd Line
                    // ========
                    Point secondLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                    Point secondLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                    graphics.DrawLine(blackPen, secondLineFirstPoint, secondLineSecondPoint);

                    // ==============================
                    // Total Sales and Total Discount
                    // ==============================
                    String amountLabel = "\nAmount";
                    String amountValue = "\n" + disbursement.FirstOrDefault().Amount.ToString("#,##0.00");
                    graphics.DrawString(amountLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(amountValue, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(amountLabel, fontArial7Bold).Height;

                    // ========
                    // 3rd Line
                    // ========
                    Point thirdLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                    Point thirdLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                    graphics.DrawLine(blackPen, thirdLineFirstPoint, thirdLineSecondPoint);

                    String remarksLabel = "\nRemarks";
                    graphics.DrawString(remarksLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(remarksLabel, fontArial7Regular).Height;

                    String remarksValue = "\n" + disbursement.FirstOrDefault().Remarks;
                    RectangleF remarksValuRectangle = new RectangleF
                    {
                        X = x,
                        Y = y,
                        Size = new Size(245, ((int)graphics.MeasureString(remarksValue, fontArial7Regular, 245, StringFormat.GenericDefault).Height))
                    };
                    graphics.DrawString(remarksValue, fontArial7Regular, Brushes.Black, remarksValuRectangle, drawFormatLeft);
                    y += remarksValuRectangle.Size.Height + 5.0F;

                    // ========
                    // 4th Line
                    // ========
                    Point forthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                    Point forthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                    graphics.DrawLine(blackPen, forthLineFirstPoint, forthLineSecondPoint);

                    String preparedByValue = "\n\n\n\n" + disbursement.FirstOrDefault().MstUser.UserName;
                    graphics.DrawString(preparedByValue, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(preparedByValue, fontArial7Regular).Height;

                    // ========
                    // 5th Line
                    // ========
                    Point fifthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                    Point fifthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                    graphics.DrawLine(blackPen, fifthLineFirstPoint, fifthLineSecondPoint);

                    String preparedByLabel = "\nPrepared by";
                    graphics.DrawString(preparedByLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(preparedByLabel, fontArial7Bold).Height;

                    // ========
                    // 6th Line
                    // ========
                    Point sixthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                    Point sixthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                    graphics.DrawLine(blackPen, sixthLineFirstPoint, sixthLineSecondPoint);

                    String receivedBySignatureSpace = ".\n\n\n\n";
                    graphics.DrawString(receivedBySignatureSpace, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(receivedBySignatureSpace, fontArial7Regular).Height;

                    // ========
                    // 7th Line
                    // ========
                    Point seventhLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                    Point seventhLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                    graphics.DrawLine(blackPen, seventhLineFirstPoint, seventhLineSecondPoint);

                    String receivedByLabel = "\nReceived by";
                    graphics.DrawString(receivedByLabel, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(receivedByLabel, fontArial7Bold).Height;

                    // ========
                    // 8th Line
                    // ========
                    Point eightLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                    Point eightLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                    graphics.DrawLine(blackPen, eightLineFirstPoint, eightLineSecondPoint);
                }
            }
            else
            {
                String disbursementTitle = "Disbursement";
                graphics.DrawString(disbursementTitle, fontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(disbursementTitle, fontArial10Bold).Height;

                // ====
                // Line
                // ====
                Point lineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point lineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, lineFirstPoint, lineSecondPoint);

                // =================
                // Collection Header
                // =================
                var disbursement = from d in db.TrnDisbursements where d.Id == trnDisbursementId select d;
                if (disbursement.Any())
                {
                    String disbursementNumber = "\n" + disbursement.FirstOrDefault().DisbursementNumber;
                    graphics.DrawString(disbursementNumber, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(disbursementNumber, fontArial9Regular).Height;

                    String disbursementDate = disbursement.FirstOrDefault().DisbursementDate.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture);
                    graphics.DrawString(disbursementDate, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(disbursementDate, fontArial9Regular).Height;

                    String disbursementType = disbursement.FirstOrDefault().DisbursementType;
                    graphics.DrawString("Type: " + disbursementType, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(disbursementType, fontArial9Regular).Height;

                    String terminal = disbursement.FirstOrDefault().MstTerminal.Terminal;
                    graphics.DrawString("Terminal: " + terminal, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(terminal, fontArial9Regular).Height;

                    String payType = disbursement.FirstOrDefault().MstPayType.PayType;
                    graphics.DrawString("Pay Type: " + payType, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(payType, fontArial9Regular).Height;

                    // ========
                    // 1st Line
                    // ========
                    Point firstLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                    Point firstLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                    graphics.DrawLine(blackPen, firstLineFirstPoint, firstLineSecondPoint);

                    String amountDenomination = "\nAmount Denomination";
                    graphics.DrawString(amountDenomination, fontArial10Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(amountDenomination, fontArial10Bold).Height + 5.0F;

                    String data1000 = disbursement.FirstOrDefault().Amount1000 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount1000).ToString("#,##0.00") : "0.00";
                    String label1000 = "x P1,000";
                    graphics.DrawString(data1000, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label1000, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label1000, fontArial9Regular).Height + 5.0F;

                    String data500 = disbursement.FirstOrDefault().Amount500 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount500).ToString("#,##0.00") : "0.00";
                    String label500 = "x P500";
                    graphics.DrawString(data500, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label500, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label1000, fontArial9Regular).Height + 5.0F;

                    String data200 = disbursement.FirstOrDefault().Amount200 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount200).ToString("#,##0.00") : "0.00";
                    String label200 = "x P200";
                    graphics.DrawString(data200, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label200, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label200, fontArial9Regular).Height + 5.0F;

                    String data100 = disbursement.FirstOrDefault().Amount100 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount100).ToString("#,##0.00") : "0.00";
                    String label100 = "x P100";
                    graphics.DrawString(data100, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label100, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label100, fontArial9Regular).Height + 5.0F;

                    String data50 = disbursement.FirstOrDefault().Amount50 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount50).ToString("#,##0.00") : "0.00";
                    String label50 = "x P50";
                    graphics.DrawString(data50, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label50, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label50, fontArial9Regular).Height + 5.0F;

                    String data20 = disbursement.FirstOrDefault().Amount20 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount20).ToString("#,##0.00") : "0.00";
                    String label20 = "x P20";
                    graphics.DrawString(data20, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label20, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label20, fontArial9Regular).Height + 5.0F;

                    String data10 = disbursement.FirstOrDefault().Amount10 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount10).ToString("#,##0.00") : "0.00";
                    String label10 = "x P10";
                    graphics.DrawString(data10, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label10, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label10, fontArial9Regular).Height + 5.0F;

                    String data5 = disbursement.FirstOrDefault().Amount5 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount5).ToString("#,##0.00") : "0.00";
                    String label5 = "x P5";
                    graphics.DrawString(data5, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label5, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label5, fontArial9Regular).Height + 5.0F;

                    String data1 = disbursement.FirstOrDefault().Amount1 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount1).ToString("#,##0.00") : "0.00";
                    String label1 = "x P1";
                    graphics.DrawString(data1, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label1, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label1, fontArial9Regular).Height + 5.0F;

                    String data025 = disbursement.FirstOrDefault().Amount025 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount025).ToString("#,##0.00") : "0.00";
                    String label025 = "x .25c";
                    graphics.DrawString(data025, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label025, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label025, fontArial9Regular).Height + 5.0F;

                    String data010 = disbursement.FirstOrDefault().Amount010 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount010).ToString("#,##0.00") : "0.00";
                    String label010 = "x .10c";
                    graphics.DrawString(data010, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label010, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label010, fontArial9Regular).Height + 5.0F;

                    String data005 = disbursement.FirstOrDefault().Amount005 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount005).ToString("#,##0.00") : "0.00";
                    String label005 = "x .05c";
                    graphics.DrawString(data005, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label005, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label005, fontArial9Regular).Height + 5.0F;

                    String data001 = disbursement.FirstOrDefault().Amount001 != null ? Convert.ToDecimal(disbursement.FirstOrDefault().Amount001).ToString("#,##0.00") : "0.00";
                    String label001 = "x .01c";
                    graphics.DrawString(data001, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(label001, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(label001, fontArial9Regular).Height + 5.0F;

                    // ========
                    // 2nd Line
                    // ========
                    Point secondLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                    Point secondLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                    graphics.DrawLine(blackPen, secondLineFirstPoint, secondLineSecondPoint);

                    // ==============================
                    // Total Sales and Total Discount
                    // ==============================
                    String amountLabel = "\nAmount";
                    String amountValue = "\n" + disbursement.FirstOrDefault().Amount.ToString("#,##0.00");
                    graphics.DrawString(amountLabel, fontArial9Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                    graphics.DrawString(amountValue, fontArial9Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                    y += graphics.MeasureString(amountLabel, fontArial9Bold).Height;

                    // ========
                    // 3rd Line
                    // ========
                    Point thirdLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                    Point thirdLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                    graphics.DrawLine(blackPen, thirdLineFirstPoint, thirdLineSecondPoint);

                    String remarksLabel = "\nRemarks";
                    graphics.DrawString(remarksLabel, fontArial9Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(remarksLabel, fontArial9Regular).Height;

                    String remarksValue = "\n" + disbursement.FirstOrDefault().Remarks;
                    RectangleF remarksValuRectangle = new RectangleF
                    {
                        X = x,
                        Y = y,
                        Size = new Size(245, ((int)graphics.MeasureString(remarksValue, fontArial9Regular, 245, StringFormat.GenericDefault).Height))
                    };
                    graphics.DrawString(remarksValue, fontArial9Regular, Brushes.Black, remarksValuRectangle, drawFormatLeft);
                    y += remarksValuRectangle.Size.Height + 5.0F;

                    // ========
                    // 4th Line
                    // ========
                    Point forthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                    Point forthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                    graphics.DrawLine(blackPen, forthLineFirstPoint, forthLineSecondPoint);

                    String preparedByValue = "\n\n\n\n" + disbursement.FirstOrDefault().MstUser.UserName;
                    graphics.DrawString(preparedByValue, fontArial9Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(preparedByValue, fontArial9Regular).Height;

                    // ========
                    // 5th Line
                    // ========
                    Point fifthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                    Point fifthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                    graphics.DrawLine(blackPen, fifthLineFirstPoint, fifthLineSecondPoint);

                    String preparedByLabel = "\nPrepared by";
                    graphics.DrawString(preparedByLabel, fontArial9Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(preparedByLabel, fontArial9Bold).Height;

                    // ========
                    // 6th Line
                    // ========
                    Point sixthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                    Point sixthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                    graphics.DrawLine(blackPen, sixthLineFirstPoint, sixthLineSecondPoint);

                    String receivedBySignatureSpace = ".\n\n\n\n";
                    graphics.DrawString(receivedBySignatureSpace, fontArial9Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(receivedBySignatureSpace, fontArial9Regular).Height;

                    // ========
                    // 7th Line
                    // ========
                    Point seventhLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                    Point seventhLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                    graphics.DrawLine(blackPen, seventhLineFirstPoint, seventhLineSecondPoint);

                    String receivedByLabel = "\nReceived by";
                    graphics.DrawString(receivedByLabel, fontArial9Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                    y += graphics.MeasureString(receivedByLabel, fontArial9Bold).Height;

                    // ========
                    // 8th Line
                    // ========
                    Point eightLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                    Point eightLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                    graphics.DrawLine(blackPen, eightLineFirstPoint, eightLineSecondPoint);
                }
            }
            
            if(Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "58mm Printer")
            {
                String space = ".\n";
                graphics.DrawString(space, fontArial9Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            }
            else
            {
                String space = ".\n\n\n\n";
                graphics.DrawString(space, fontArial9Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            }
            
        }
    }
}
