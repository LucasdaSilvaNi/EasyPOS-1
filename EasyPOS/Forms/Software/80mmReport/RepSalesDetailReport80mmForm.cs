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
    public partial class RepSalesDetailReport80mmForm : Form
    {
        public DateTime dateStart;
        public DateTime dateEnd;
        public Int32 filterTerminalId;
        public RepSalesDetailReport80mmForm(DateTime startDate, DateTime endDate, Int32 terminalId)
        {
            InitializeComponent();
            dateStart = startDate;
            dateEnd = endDate;
            filterTerminalId = terminalId;

                if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Dot Matrix Printer")
                {
                    printDocumentSalesDetailReport.DefaultPageSettings.PaperSize = new PaperSize("Official Receipt", 255, 38500);
                }
                else if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Thermal Printer")
                {
                    printDocumentSalesDetailReport.DefaultPageSettings.PaperSize = new PaperSize("Official Receipt", 270, 38500);
                }
                else
                {
                    printDocumentSalesDetailReport.DefaultPageSettings.PaperSize = new PaperSize("Official Receipt", 175, 38500);
                }
        }

        private void printDocumentSalesDetailReport_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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
            Font fontArial9Bold = new Font("Arial", 9, FontStyle.Bold);
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
                String title = "Sales Detail Report";
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


                String salesItem = "\nItem";
                String amount = "\nAmount";
                graphics.DrawString(salesItem, fontArial7Bold, drawBrush, new RectangleF(x + 20, y, width, height), drawFormatLeft);
                graphics.DrawString(amount, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(salesItem, fontArial7Regular).Height + 3.0F;

                // ========
                // 2nd Line
                // ========
                Point secondLineFirstPoint = new Point(0, Convert.ToInt32(y) + 3);
                Point secondLineSecondPoint = new Point(500, Convert.ToInt32(y) + 3);
                graphics.DrawLine(blackPen, secondLineFirstPoint, secondLineSecondPoint);
                Decimal totalItemAmount = 0;

                var salesLineItem = from s in db.TrnSalesLines
                                    where s.TrnSale.SalesDate >= dateStart
                                     && s.TrnSale.SalesDate <= dateEnd
                                     && s.TrnSale.TerminalId == filterTerminalId
                                     && s.TrnSale.IsLocked == true
                                     && s.TrnSale.IsCancelled == false
                                    select s;
                if (salesLineItem.Any())
                {
                    var categories = from d in salesLineItem
                                     group d by d.MstItem.Category
                                     into g
                                     select g.Key;

                    if (categories.ToList().Any())
                    {
                        Decimal categorySubTotal = 0;
                        foreach (var category in categories)
                        {
                            var items = from d in salesLineItem
                                        where d.MstItem.Category == category
                                        select d;
                            // Label Category
                            String Category = category;
                            graphics.DrawString(Category + "\n", fontArial8Bold, Brushes.Black, new RectangleF(x, y + 15, 240, 150), drawFormatLeft);
                            if (items.Any())
                            {
                                Decimal itemTotal = 0;
                                Decimal subItemTotal = 0;

                                foreach (var item in items)
                                {
                                    subItemTotal = item.Amount;
                                    String salesData = "\n" + item.MstItem.BarCode + " - " + item.MstItem.ItemDescription;
                                    String salesQuantityUnit = "\n" + "                         " + item.Quantity.ToString("#,##0.00") + item.MstUnit.Unit;

                                    RectangleF itemDataRectangle = new RectangleF
                                    {
                                        X = x,
                                        Y = y,
                                        Size = new Size(240, ((int)graphics.MeasureString(Category + salesData + salesQuantityUnit, fontArial7Regular, 150, StringFormat.GenericDefault).Height))
                                    };
                                    itemTotal += subItemTotal;
                                    // List of items
                                    graphics.DrawString("\n" + salesData + salesQuantityUnit, fontArial7Regular, Brushes.Black, new RectangleF(x, y + 5, 120, height), drawFormatLeft);
                                    graphics.DrawString("\n" + subItemTotal.ToString("#,##0.00"), fontArial7Regular, Brushes.Black, new RectangleF(x, y + 20, width, height), drawFormatRight);
                                    y += itemDataRectangle.Size.Height;

                                }
                                totalItemAmount += itemTotal;
                                categorySubTotal = (0 * itemTotal) + itemTotal;
                            }

                            // ========
                            // 3rd Line
                            // ========
                            Point thirdLineFirstPoint = new Point(0, Convert.ToInt32(y) + 30);
                            Point thirdLineSecondPoint = new Point(500, Convert.ToInt32(y) + 30);
                            graphics.DrawLine(blackPen, thirdLineFirstPoint, thirdLineSecondPoint);

                            String amountLabel = "\nSub Total";
                            graphics.DrawString(amountLabel, fontArial8Bold, drawBrush, new RectangleF(x, y + 18, width, height), drawFormatLeft);
                            graphics.DrawString(categorySubTotal.ToString("#,##0.00"), fontArial8Bold, drawBrush, new RectangleF(x, y + 31, width, height), drawFormatRight);
                            y += 30;
                        }
                        // ========
                        // 4th Line
                        // ========
                        Point fourthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 17);
                        Point fourthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 17);
                        graphics.DrawLine(blackPen, fourthLineFirstPoint, fourthLineSecondPoint);

                        String totalSalesAmounts = "\n" + totalItemAmount.ToString("#,##0.00");
                        String amountLabesl = "\n Total";
                        graphics.DrawString(amountLabesl, fontArial8Bold, drawBrush, new RectangleF(x, y + 6, width, height), drawFormatLeft);
                        graphics.DrawString(totalSalesAmounts, fontArial8Bold, drawBrush, new RectangleF(x, y + 5, width, height), drawFormatRight);
                    }
                }
            }
            else
            {
                // =================
                // 80mm Report Title
                // =================
                String title = "Sales Detail Report";
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


                String salesItem = "\nItem";
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
                Decimal totalItemAmount = 0;

                var salesLineItem = from s in db.TrnSalesLines
                                    where s.TrnSale.SalesDate >= dateStart
                                     && s.TrnSale.SalesDate <= dateEnd
                                     && s.TrnSale.TerminalId == filterTerminalId
                                     && s.TrnSale.IsLocked == true
                                     && s.TrnSale.IsCancelled == false
                                    select s;
                if (salesLineItem.Any())
                {
                    var categories = from d in salesLineItem
                                     group d by d.MstItem.Category
                                     into g
                                     select g.Key;

                    if (categories.ToList().Any())
                    {
                        Decimal categorySubTotal = 0;
                        foreach (var category in categories)
                        {
                            var items = from d in salesLineItem
                                        where d.MstItem.Category == category
                                        select d;
                            // Label Category
                            String Category = category;
                            graphics.DrawString(Category + "\n", fontArial10Bold, Brushes.Black, new RectangleF(x, y + 15, 240, 150), drawFormatLeft);
                            if (items.Any())
                            {
                                Decimal itemTotal = 0;
                                Decimal subItemTotal = 0;

                                foreach (var item in items)
                                {
                                    subItemTotal = item.Amount;
                                    String salesData = "\n" + item.MstItem.BarCode + " - " + item.MstItem.ItemDescription;
                                    String salesQuantityUnit = "\n" + "                         " + item.Quantity.ToString("#,##0.00") + item.MstUnit.Unit;

                                    RectangleF itemDataRectangle = new RectangleF
                                    {
                                        X = x,
                                        Y = y,
                                        Size = new Size(240, ((int)graphics.MeasureString(Category + salesData + salesQuantityUnit, fontArial8Regular, 150, StringFormat.GenericDefault).Height))
                                    };
                                    itemTotal += subItemTotal;
                                    // List of items
                                    graphics.DrawString("\n" + salesData + salesQuantityUnit, fontArial8Regular, Brushes.Black, new RectangleF(x, y + 5, 180, height), drawFormatLeft);
                                    graphics.DrawString("\n" + subItemTotal.ToString("#,##0.00"), fontArial8Regular, Brushes.Black, new RectangleF(x, y + 20, width, height), drawFormatRight);
                                    y += itemDataRectangle.Size.Height;

                                }
                                totalItemAmount += itemTotal;
                                categorySubTotal = (0 * itemTotal) + itemTotal;
                            }

                            // ========
                            // 3rd Line
                            // ========
                            Point thirdLineFirstPoint = new Point(0, Convert.ToInt32(y) + 30);
                            Point thirdLineSecondPoint = new Point(500, Convert.ToInt32(y) + 30);
                            graphics.DrawLine(blackPen, thirdLineFirstPoint, thirdLineSecondPoint);

                            String amountLabel = "\nSub Total";
                            graphics.DrawString(amountLabel, fontArial9Bold, drawBrush, new RectangleF(x, y + 18, width, height), drawFormatLeft);
                            graphics.DrawString(categorySubTotal.ToString("#,##0.00"), fontArial9Bold, drawBrush, new RectangleF(x, y + 31, width, height), drawFormatRight);
                            y += 30;
                        }
                        // ========
                        // 4th Line
                        // ========
                        Point fourthLineFirstPoint = new Point(0, Convert.ToInt32(y) + 17);
                        Point fourthLineSecondPoint = new Point(500, Convert.ToInt32(y) + 17);
                        graphics.DrawLine(blackPen, fourthLineFirstPoint, fourthLineSecondPoint);

                        String totalSalesAmounts = "\n" + totalItemAmount.ToString("#,##0.00");
                        String amountLabesl = "\n Total";
                        graphics.DrawString(amountLabesl, fontArial9Bold, drawBrush, new RectangleF(x, y + 6, width, height), drawFormatLeft);
                        graphics.DrawString(totalSalesAmounts, fontArial9Bold, drawBrush, new RectangleF(x, y + 5, width, height), drawFormatRight);
                    }
                }

                //if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Dot Matrix Printer")
                //{
                //    String space = "\n\n\n\n\n\n\n\n\n\n.";
                //    graphics.DrawString(space, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                //}
                //else if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Thermal Printer")
                //{
                //    String space = "\n\n\n.";
                //    graphics.DrawString(space, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                //}
                //else
                //{
                //    String space = "\n.";
                //    graphics.DrawString(space, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                //}
            }

        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            DialogResult printerDialogResult = printDialogSalesDetailReport.ShowDialog();
            if (printerDialogResult == DialogResult.OK)
            {
                PrintReport();
                Close();
            }
        }

        public void PrintReport()
        {
            printDocumentSalesDetailReport.Print();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}