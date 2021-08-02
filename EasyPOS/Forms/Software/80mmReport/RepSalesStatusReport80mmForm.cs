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
    public partial class RepSalesStatusReport80mmForm : Form
    {
        public DateTime dateStart;
        public DateTime dateEnd;
        public Int32 filterTerminalId;
        public RepSalesStatusReport80mmForm(DateTime startDate, DateTime endDate, Int32 terminalId, Boolean isPrintPreview, String printerName)
        {
            InitializeComponent();
            dateStart = startDate;
            dateEnd = endDate;
            filterTerminalId = terminalId;

            if (isPrintPreview == true)
            {
                printDocument80mm.PrinterSettings.PrinterName = printerName;

                printDocument80mm.DefaultPageSettings.PaperSize = new PaperSize("Official Receipt", 255, 38500);
                printDocument80mm.Print();
            }
            else
            {
                if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Dot Matrix Printer")
                {
                    printDocument80mm.DefaultPageSettings.PaperSize = new PaperSize("Official Receipt", 255, 38500);
                    printDocument80mm.Print();

                }
                else if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Thermal Printer")
                {
                    printDocument80mm.DefaultPageSettings.PaperSize = new PaperSize("Official Receipt", 270, 38500);
                    printDocument80mm.Print();
                }
                else
                {
                    printDocument80mm.DefaultPageSettings.PaperSize = new PaperSize("Official Receipt", 175, 38500);
                    printDocument80mm.Print();
                }
            }
        }

        private void printDocument80mm_PrintPage(object sender, PrintPageEventArgs e)
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
            Font fontArial10Bold = new Font("Arial", 10, FontStyle.Bold);
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
                String title = "Sales Status Report";
                graphics.DrawString(title, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(title, fontArial8Bold).Height;

                // ==================
                // Date Range Header
                // ==================
                String RangeDateText = "FROM" + " " + dateStart.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + " " + "TO" + " " + dateEnd.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                graphics.DrawString(RangeDateText, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(RangeDateText, fontArial7Regular).Height;

                // ========
                // 1st Line
                // ========
                Point firstLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point firstLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, firstLineFirstPoint, firstLineSecondPoint);

                // ===============
                // Stock-in Line
                // ===============


                String salesItem = "\nPay Type";
                String amount = "\nAmount";
                graphics.DrawString(salesItem, fontArial7Bold, drawBrush, new RectangleF(x + 20, y, width, height), drawFormatLeft);
                graphics.DrawString(amount, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(salesItem, fontArial8Regular).Height + 3.0F;

                // ========
                // 2nd Line
                // ========
                Point secondLineFirstPoint = new Point(0, Convert.ToInt32(y) + 3);
                Point secondLineSecondPoint = new Point(500, Convert.ToInt32(y) + 3);
                graphics.DrawLine(blackPen, secondLineFirstPoint, secondLineSecondPoint);

                Decimal totalAmount = 0;

                String collectedLabel = "Collected";
                graphics.DrawString(collectedLabel, fontArial7Bold, drawBrush, new RectangleF(x + 20, y + 5, width, height), drawFormatLeft);
                var collectionLines = from s in db.TrnCollectionLines
                                      where s.TrnCollection.CollectionDate >= dateStart
                                       && s.TrnCollection.CollectionDate <= dateEnd
                                       && s.TrnCollection.TerminalId == filterTerminalId
                                       && s.TrnCollection.IsLocked == true
                                      group s by new
                                      {
                                          s.MstPayType.PayType
                                      } into g
                                      select new Entities.TrnCollectionLineEntity
                                      {
                                          PayType = g.Key.PayType,
                                          Amount = g.Sum(a => a.Amount)
                                      };
                if (collectionLines.Any())
                {
                    foreach (var collection in collectionLines)
                    {
                        String PayType = collection.PayType;
                        String PayTypeAmount = collection.Amount.ToString("#,##0.00");
                        totalAmount += collection.Amount;
                        RectangleF itemDataRectangle = new RectangleF
                        {
                            X = x + 20,
                            Y = y + 20,
                            Size = new Size(150, ((int)graphics.MeasureString(PayType + PayTypeAmount, fontArial7Regular, 150, StringFormat.GenericDefault).Height))
                        };
                        graphics.DrawString(PayType, fontArial7Regular, Brushes.Black, itemDataRectangle, drawFormatLeft);
                        y += itemDataRectangle.Size.Height + 3.0F;

                        graphics.DrawString(PayTypeAmount, fontArial7Regular, drawBrush, new RectangleF(x, y + 2, 170.0F, height), drawFormatRight);
                    }
                    // ========
                    // 3rd Line
                    // ========
                    Point thirdLineFirstPoint = new Point(0, Convert.ToInt32(y) + 15);
                    Point thirdLineSecondPoint = new Point(500, Convert.ToInt32(y) + 15);
                    graphics.DrawLine(blackPen, thirdLineFirstPoint, thirdLineSecondPoint);

                    String totalSalesAmount = "\n" + totalAmount.ToString("#,##0.00");
                    graphics.DrawString(totalSalesAmount, fontArial7Bold, drawBrush, new RectangleF(x, y + 3, width, height), drawFormatRight);
                }
            }
            else
            {
                // =================
                // 80mm Report Title
                // =================
                String title = "Sales Status Report";
                graphics.DrawString(title, fontArial11Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(title, fontArial11Bold).Height;

                // ==================
                // Date Range Header
                // ==================
                String RangeDateText = "FROM" + " " + dateStart.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + " " + "TO" + " " + dateEnd.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                graphics.DrawString(RangeDateText, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(RangeDateText, fontArial8Regular).Height;

                // ========
                // 1st Line
                // ========
                Point firstLineFirstPoint = new Point(0, Convert.ToInt32(y) + 5);
                Point firstLineSecondPoint = new Point(500, Convert.ToInt32(y) + 5);
                graphics.DrawLine(blackPen, firstLineFirstPoint, firstLineSecondPoint);

                // ===============
                // Stock-in Line
                // ===============


                String salesItem = "\nPay Type";
                String amount = "\nAmount";
                graphics.DrawString(salesItem, fontArial8Bold, drawBrush, new RectangleF(x + 20, y, width, height), drawFormatLeft);
                graphics.DrawString(amount, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(salesItem, fontArial8Regular).Height + 3.0F;

                // ========
                // 2nd Line
                // ========
                Point secondLineFirstPoint = new Point(0, Convert.ToInt32(y) + 3);
                Point secondLineSecondPoint = new Point(500, Convert.ToInt32(y) + 3);
                graphics.DrawLine(blackPen, secondLineFirstPoint, secondLineSecondPoint);

                Decimal totalAmount = 0;

                String collectedLabel = "Collected";
                graphics.DrawString(collectedLabel, fontArial8Bold, drawBrush, new RectangleF(x + 20, y + 5, width, height), drawFormatLeft);
                var collectionLines = from s in db.TrnCollectionLines
                                      where s.TrnCollection.CollectionDate >= dateStart
                                       && s.TrnCollection.CollectionDate <= dateEnd
                                       && s.TrnCollection.TerminalId == filterTerminalId
                                       && s.TrnCollection.IsLocked == true
                                      group s by new
                                      {
                                          s.MstPayType.PayType
                                      } into g
                                      select new Entities.TrnCollectionLineEntity
                                      {
                                          PayType = g.Key.PayType,
                                          Amount = g.Sum(a => a.Amount)
                                      };
                if (collectionLines.Any())
                {
                    foreach (var collection in collectionLines)
                    {
                        String PayType = collection.PayType;
                        String PayTypeAmount = collection.Amount.ToString("#,##0.00");
                        totalAmount += collection.Amount;
                        RectangleF itemDataRectangle = new RectangleF
                        {
                            X = x + 20,
                            Y = y + 20,
                            Size = new Size(150, ((int)graphics.MeasureString(PayType + PayTypeAmount, fontArial8Regular, 150, StringFormat.GenericDefault).Height))
                        };
                        graphics.DrawString(PayType, fontArial8Regular, Brushes.Black, itemDataRectangle, drawFormatLeft);
                        y += itemDataRectangle.Size.Height + 3.0F;
                        if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Dot Matrix Printer")
                        {
                            graphics.DrawString(PayTypeAmount, fontArial8Regular, drawBrush, new RectangleF(x, y + 2, 245.0F, height), drawFormatRight);
                        }
                        else
                        {
                            graphics.DrawString(PayTypeAmount, fontArial8Regular, drawBrush, new RectangleF(x, y + 2, 255.0F, height), drawFormatRight);
                        }
                    }
                    // ========
                    // 3rd Line
                    // ========
                    Point thirdLineFirstPoint = new Point(0, Convert.ToInt32(y) + 15);
                    Point thirdLineSecondPoint = new Point(500, Convert.ToInt32(y) + 15);
                    graphics.DrawLine(blackPen, thirdLineFirstPoint, thirdLineSecondPoint);

                    String totalSalesAmount = "\n" + totalAmount.ToString("#,##0.00");
                    graphics.DrawString(totalSalesAmount, fontArial8Bold, drawBrush, new RectangleF(x, y + 3, width, height), drawFormatRight);
                }
            }

            if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Dot Matrix Printer")
            {
                String space = "\n\n\n\n\n\n\n\n\n\n.";
                graphics.DrawString(space, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            }
            else
            {
                String space = "\n\n\n.";
                graphics.DrawString(space, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            }
        }

    }
}
