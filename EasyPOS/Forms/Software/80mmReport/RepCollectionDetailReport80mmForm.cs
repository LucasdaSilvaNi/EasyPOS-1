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

namespace EasyPOS.Forms.Software._80mmReport
{
    public partial class RepCollectionDetailReport80mmForm : Form
    {
        public DateTime dateStart;
        public DateTime dateEnd;
        public Int32 filterTerminalId;
        public RepCollectionDetailReport80mmForm(DateTime startDate, DateTime endDate, Int32 terminalId)
        {
            InitializeComponent();
            dateStart = startDate;
            dateEnd = endDate;
            filterTerminalId = terminalId;

            if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Dot Matrix Printer")
            {
                printDocument80mm.DefaultPageSettings.PaperSize = new PaperSize("Official Receipt", 255, 38500);
            }
            else if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Thermal Printer")
            {
                printDocument80mm.DefaultPageSettings.PaperSize = new PaperSize("Official Receipt", 270, 38500);
            }
            else
            {
                printDocument80mm.DefaultPageSettings.PaperSize = new PaperSize("Official Receipt", 175, 38500);
            }
        }

        private void printDocument80mm_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // ============
            // Data Context
            // ============
            Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

            // =============
            // Font Settings
            // =============
            Font fontArial12Bold = new Font("Arial", 12, FontStyle.Bold);
            Font fontArial12Regular = new Font("Arial", 12, FontStyle.Regular);
            Font fontArial11Bold = new Font("Arial", 11, FontStyle.Bold);
            Font fontArial11Regular = new Font("Arial", 11, FontStyle.Regular);
            Font fontArial8Bold = new Font("Arial", 8, FontStyle.Bold);
            Font fontArial8Regular = new Font("Arial", 8, FontStyle.Regular);
            Font fontArial9Bold = new Font("Arial", 9, FontStyle.Bold);
            Font fontArial10Bold = new Font("Arial", 10, FontStyle.Bold);
            Font fontArial7Regular = new Font("Arial", 7, FontStyle.Regular);
            Font fontArial7Bold = new Font("Arial", 7, FontStyle.Bold);

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
            if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "58mm Printer")
            {
                // =================
                // 80mm Report Title
                // =================
                String title = "Collection Detail Report";
                graphics.DrawString(title, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(title, fontArial8Bold).Height;

                // ==================
                // Date Range Header
                // ==================
                String RangeDateText = "FROM" + " " + dateStart.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + " " + "TO" + " " + dateEnd.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                graphics.DrawString(RangeDateText, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(RangeDateText, fontArial7Regular).Height;

                // ==================
                // Terminal Header
                // ==================
                String terminalHeader = "Terminal" + " " + filterTerminalId.ToString();
                graphics.DrawString(terminalHeader, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(terminalHeader, fontArial7Regular).Height;

                // ========
                // 1st Line
                // ========
                Point firstLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point firstLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, firstLineFirstPoint, firstLineSecondPoint);

                // ===============
                // Collection Line
                // ===============


                String date = "\nDate";
                String ORNo = "\nOR No.";
                String Amount = "\nAmount";
                graphics.DrawString(date, fontArial7Bold, drawBrush, new RectangleF(x + 20, y, width, height), drawFormatLeft);
                graphics.DrawString(ORNo, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                graphics.DrawString(Amount, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(date, fontArial7Regular).Height + 3.0F;

                // ========
                // 2nd Line
                // ========
                Point secondLineFirstPoint = new Point(0, Convert.ToInt32(y) + 3);
                Point secondLineSecondPoint = new Point(500, Convert.ToInt32(y) + 3);
                graphics.DrawLine(blackPen, secondLineFirstPoint, secondLineSecondPoint);

                Decimal CollectionLineTotalAmount = 0;
                Decimal totalCollectionCount = 0;
                Decimal subCollectionCount = 0;


                var collectionLines = from s in db.TrnCollectionLines
                                      where s.TrnCollection.CollectionDate >= dateStart
                                       && s.TrnCollection.CollectionDate <= dateEnd
                                       && s.TrnCollection.TerminalId == filterTerminalId
                                       && s.TrnCollection.IsLocked == true
                                      select s;
                if (collectionLines.Any())
                {
                    var payTypes = from d in collectionLines
                                   group d by d.MstPayType.PayType
                                   into g
                                   select g.Key;
                    if (payTypes.Any())
                    {
                        foreach (var payType in payTypes)
                        {
                            var collection = from d in collectionLines
                                             where d.MstPayType.PayType == payType
                                             select d;
                            String paytype = payType;
                            graphics.DrawString(paytype + "\n", fontArial8Bold, Brushes.Black, new RectangleF(x, y + 15, 240, 240), drawFormatLeft);

                            if (collection.Any())
                            {
                                Decimal CollectionTotal = 0;
                                Decimal subCollectionTotal = 0;
                                Decimal CollectionCount = 0;

                                foreach (var collections in collection)
                                {
                                    CollectionCount += 1;
                                    subCollectionTotal = (collections.TrnCollection.IsCancelled == true ? 0 : collections.TrnCollection.Amount);
                                    String collectionData = "\n" + collections.TrnCollection.CollectionDate + collections.TrnCollection.ManualORNumber + collections.TrnCollection.Amount.ToString("#,##0.00");

                                    RectangleF itemDataRectangle = new RectangleF
                                    {
                                        X = x,
                                        Y = y,
                                        Size = new Size(240, ((int)graphics.MeasureString(collectionData, fontArial8Regular, 300, StringFormat.GenericDefault).Height))
                                    };
                                    graphics.DrawString(collections.TrnCollection.CollectionDate.ToShortDateString() + "\n", fontArial8Regular, drawBrush, new RectangleF(x, y + 30, width, height), drawFormatLeft);
                                    graphics.DrawString(collections.TrnCollection.ManualORNumber + "\n", fontArial8Regular, drawBrush, new RectangleF(x, y + 30, width, height), drawFormatCenter);
                                    graphics.DrawString(collections.TrnCollection.IsCancelled == true ? "0" : collections.MstPayType.PayTypeCode.Equals("CASH") ? collections.TrnCollection.Amount.ToString("#,##0.00") : "\n", fontArial8Regular, drawBrush, new RectangleF(x, y + 30, width, height), drawFormatRight);
                                    y += itemDataRectangle.Size.Height;
                                    CollectionTotal += subCollectionTotal;


                                }
                                CollectionLineTotalAmount += CollectionTotal;
                                subCollectionTotal = (0 * CollectionTotal) + CollectionTotal;
                                subCollectionCount = (0 * CollectionCount) + CollectionCount;
                                totalCollectionCount += CollectionCount;

                            }
                        }
                        // ========
                        // 4th Line
                        // ========
                        Point fourthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 17);
                        Point fourthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 17);
                        graphics.DrawLine(blackPen, fourthLineFirstPoint, fourthLineSecondPoint);

                        String totalSalesAmounts = "\n" + CollectionLineTotalAmount.ToString("#,##0.00");
                        graphics.DrawString(totalCollectionCount.ToString(), fontArial8Bold, drawBrush, new RectangleF(x, y + 17, width, height), drawFormatLeft);
                        graphics.DrawString(totalSalesAmounts, fontArial8Bold, drawBrush, new RectangleF(x, y + 5, width, height), drawFormatRight);
                        // ========
                        // 5th Line
                        // ========
                        Point fifthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 32);
                        Point fifthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 32);
                        graphics.DrawLine(blackPen, fifthLineFirstPoint, fifthLineSecondPoint);
                        // ========
                        // 6th Line
                        // ========
                        Point sixthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 37);
                        Point sixthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 37);
                        graphics.DrawLine(blackPen, sixthLineFirstPoint, sixthLineSecondPoint);

                    }
                }
            }
            else
            {
                // =================
                // 80mm Report Title
                // =================
                String title = "Collection Detail Report";
                graphics.DrawString(title, fontArial11Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(title, fontArial11Bold).Height;

                // ==================
                // Date Range Header
                // ==================
                String RangeDateText = "FROM" + " " + dateStart.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + " " + "TO" + " " + dateEnd.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                graphics.DrawString(RangeDateText, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(RangeDateText, fontArial8Regular).Height;

                // ==================
                // Terminal Header
                // ==================
                String terminalHeader = "Terminal " + filterTerminalId.ToString();
                graphics.DrawString(terminalHeader, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(terminalHeader, fontArial8Regular).Height;

                // ========
                // 1st Line
                // ========
                Point firstLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point firstLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, firstLineFirstPoint, firstLineSecondPoint);


                // ===============
                // Collection Line
                // ===============


                String date = "\nDate";
                String ORNo = "\nOR No.";
                String Amount = "\nAmount";
                graphics.DrawString(date, fontArial8Bold, drawBrush, new RectangleF(x + 20, y, width, height), drawFormatLeft);
                graphics.DrawString(ORNo, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                graphics.DrawString(Amount, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(date, fontArial8Regular).Height + 3.0F;

                // ========
                // 2nd Line
                // ========
                Point secondLineFirstPoint = new Point(0, Convert.ToInt32(y) + 3);
                Point secondLineSecondPoint = new Point(500, Convert.ToInt32(y) + 3);
                graphics.DrawLine(blackPen, secondLineFirstPoint, secondLineSecondPoint);

                Decimal CollectionLineTotalAmount = 0;
                Decimal totalCollectionCount = 0;
                Decimal subCollectionCount = 0;


                var collectionLines = from s in db.TrnCollectionLines
                                      where s.TrnCollection.CollectionDate >= dateStart
                                       && s.TrnCollection.CollectionDate <= dateEnd
                                       && s.TrnCollection.TerminalId == filterTerminalId
                                       && s.TrnCollection.IsLocked == true
                                      select s;
                if (collectionLines.Any())
                {
                    var payTypes = from d in collectionLines
                                   group d by d.MstPayType.PayType
                                   into g
                                   select g.Key;
                    if (payTypes.Any())
                    {
                        foreach (var payType in payTypes)
                        {
                            var collection = from d in collectionLines
                                             where d.MstPayType.PayType == payType
                                             select d;
                            String paytype = payType;
                            graphics.DrawString(paytype + "\n", fontArial8Bold, Brushes.Black, new RectangleF(x, y + 15, 240, 240), drawFormatLeft);

                            if (collection.Any())
                            {
                                Decimal CollectionTotal = 0;
                                Decimal subCollectionTotal = 0;
                                Decimal CollectionCount = 0;

                                foreach (var collections in collection)
                                {
                                    CollectionCount += 1;
                                    subCollectionTotal = (collections.TrnCollection.IsCancelled == true ? 0 : collections.TrnCollection.Amount);
                                    String collectionData = "\n" + collections.TrnCollection.CollectionDate + collections.TrnCollection.ManualORNumber + collections.TrnCollection.Amount.ToString("#,##0.00");

                                    RectangleF itemDataRectangle = new RectangleF
                                    {
                                        X = x,
                                        Y = y,
                                        Size = new Size(240, ((int)graphics.MeasureString(collectionData, fontArial8Regular, 300, StringFormat.GenericDefault).Height))
                                    };
                                    graphics.DrawString(collections.TrnCollection.CollectionDate.ToShortDateString() + "\n", fontArial8Regular, drawBrush, new RectangleF(x, y + 30, width, height), drawFormatLeft);
                                    graphics.DrawString(collections.TrnCollection.ManualORNumber + "\n", fontArial8Regular, drawBrush, new RectangleF(x, y + 30, width, height), drawFormatCenter);
                                    graphics.DrawString(collections.TrnCollection.IsCancelled == true ? "0" : collections.MstPayType.PayTypeCode.Equals("CASH") ? collections.TrnCollection.Amount.ToString("#,##0.00") : "\n", fontArial8Regular, drawBrush, new RectangleF(x, y + 30, width, height), drawFormatRight);
                                    y += itemDataRectangle.Size.Height;
                                    CollectionTotal += subCollectionTotal;


                                }
                                CollectionLineTotalAmount += CollectionTotal;
                                subCollectionTotal = (0 * CollectionTotal) + CollectionTotal;
                                subCollectionCount = (0 * CollectionCount) + CollectionCount;
                                totalCollectionCount += CollectionCount;

                            }
                        }
                        // ========
                        // 4th Line
                        // ========
                        Point fourthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 17);
                        Point fourthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 17);
                        graphics.DrawLine(blackPen, fourthLineFirstPoint, fourthLineSecondPoint);

                        String totalSalesAmounts = "\n" + CollectionLineTotalAmount.ToString("#,##0.00");
                        graphics.DrawString(totalCollectionCount.ToString(), fontArial9Bold, drawBrush, new RectangleF(x, y + 17, width, height), drawFormatLeft);
                        graphics.DrawString(totalSalesAmounts, fontArial9Bold, drawBrush, new RectangleF(x, y + 5, width, height), drawFormatRight);
                        // ========
                        // 5th Line
                        // ========
                        Point fifthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 32);
                        Point fifthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 32);
                        graphics.DrawLine(blackPen, fifthLineFirstPoint, fifthLineSecondPoint);
                        // ========
                        // 6th Line
                        // ========
                        Point sixthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 37);
                        Point sixthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 37);
                        graphics.DrawLine(blackPen, sixthLineFirstPoint, sixthLineSecondPoint);

                    }
                }


                //if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Dot Matrix Printer")
                //{
                //    String space = "\n\n\n\n\n\n\n\n\n\n.";
                //    graphics.DrawString(space, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                //}
                //else
                //{
                //    String space = "\n\n\n.";
                //    graphics.DrawString(space, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                //}
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            DialogResult printerDialogResult = printDialogCollectionDetailReport.ShowDialog();
            if (printerDialogResult == DialogResult.OK)
            {
                PrintReport();
                Close();
            }
        }

        public void PrintReport()
        {
            printDocument80mm.Print();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
