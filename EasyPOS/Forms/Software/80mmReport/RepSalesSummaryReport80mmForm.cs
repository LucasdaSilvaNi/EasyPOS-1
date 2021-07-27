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
    public partial class RepSalesSummaryReport80mmForm : Form
    {
        public DateTime dateStart;
        public DateTime dateEnd;
        public Int32 filterTerminalId;
        private SysSoftwareForm softwareForm;

        public RepSalesSummaryReport80mmForm(DateTime startDate, DateTime endDate, Int32 terminalId, Boolean isPrintPreview, String printerName)
        {
            InitializeComponent();
            dateStart = startDate;
            dateEnd = endDate;
            filterTerminalId = terminalId;

            if (isPrintPreview == true)
            {
                printDocumentSalesSummaryReport.PrinterSettings.PrinterName = printerName;

                printDocumentSalesSummaryReport.DefaultPageSettings.PaperSize = new PaperSize("Official Receipt", 255, 38500);
                printDocumentSalesSummaryReport.Print();
            }
            else
            {
                if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Dot Matrix Printer")
                {
                    printDocumentSalesSummaryReport.DefaultPageSettings.PaperSize = new PaperSize("Official Receipt", 255, 38500);
                    printDocumentSalesSummaryReport.Print();
                }
                else if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Thermal Printer")
                {
                    printDocumentSalesSummaryReport.DefaultPageSettings.PaperSize = new PaperSize("Official Receipt", 270, 38500);
                    printDocumentSalesSummaryReport.Print();
                }
                else
                {
                    printDocumentSalesSummaryReport.DefaultPageSettings.PaperSize = new PaperSize("Official Receipt", 175, 38500);
                    printDocumentSalesSummaryReport.Print();
                }
            }
        }

        private void printDocumentSalesSummaryReport_PrintPage(object sender, PrintPageEventArgs e)
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
                String title = "Sales Summary Report";
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


                String salesDate = "\nSales Date";
                String salesNumber = "\nSales No.";
                String amount = "\nAmount";
                graphics.DrawString(salesDate, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(salesNumber, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                graphics.DrawString(amount, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(salesDate, fontArial7Regular).Height + 3.0F;

                // ========
                // 2nd Line
                // ========
                Point secondLineFirstPoint = new Point(0, Convert.ToInt32(y) + 3);
                Point secondLineSecondPoint = new Point(500, Convert.ToInt32(y) + 3);
                graphics.DrawLine(blackPen, secondLineFirstPoint, secondLineSecondPoint);


                Decimal totalNumberOfSales = 0;
                Decimal totalAmount = 0;

                var sales = from d in db.TrnSales
                            where d.SalesDate >= dateStart
                            && d.SalesDate <= dateEnd
                            && d.TerminalId == filterTerminalId
                            && d.IsLocked == true
                            && d.IsCancelled == false
                            select d;
                if (sales.Any())
                {

                    var SalesList = from s in sales
                                    group s by new
                                    {
                                        s.Id,
                                        s.MstTerminal,
                                        s.SalesDate,
                                        s.SalesNumber,
                                        s.MstCustomer,
                                        s.MstTerm,
                                        s.Remarks,
                                        s.MstUser,
                                        s.Amount,
                                        s.EntryDateTime,
                                    } into g
                                    select new
                                    {
                                        g.Key.MstTerminal,
                                        g.Key.MstTerminal.Terminal,
                                        g.Key.SalesDate,
                                        g.Key.SalesNumber,
                                        g.Key.MstCustomer,
                                        g.Key.MstCustomer.CustomerCode,
                                        g.Key.MstCustomer.Customer,
                                        g.Key.MstTerm,
                                        g.Key.MstTerm.Term,
                                        g.Key.Remarks,
                                        g.Key.MstUser,
                                        g.Key.MstUser.FullName,
                                        Amount = g.Sum(a => a.Amount),
                                        g.Key.EntryDateTime,

                                    };


                    foreach (var salesList in sales.ToList())
                    {
                        totalNumberOfSales += 1;
                        totalAmount += salesList.Amount;
                        String salesData = salesList.SalesDate + salesList.SalesNumber + salesList.Amount.ToString("#,##0.00");

                        RectangleF itemDataRectangle = new RectangleF
                        {
                            X = x,
                            Y = y,

                            Size = new Size(240, ((int)graphics.MeasureString(salesData, fontArial7Regular, 240, StringFormat.GenericDefault).Height))
                        };
                        graphics.DrawString("\n" + salesList.SalesDate.ToShortDateString() + "\n", fontArial7Regular, drawBrush, new RectangleF(x, y, 120, height), drawFormatLeft);
                        graphics.DrawString("\n" + salesList.SalesNumber + "\n", fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        graphics.DrawString("\n" + salesList.Amount.ToString("#,##0.00") + "\n", fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                        y += itemDataRectangle.Size.Height + 3.0F;
                    }
                }
                // ========
                // 3rd Line
                // ========
                Point thirdLineFirstPoint = new Point(0, Convert.ToInt32(y) + 12);
                Point thirdLineSecondPoint = new Point(500, Convert.ToInt32(y) + 12);
                graphics.DrawLine(blackPen, thirdLineFirstPoint, thirdLineSecondPoint);

                String totalSalesAmount = "\n" + totalAmount.ToString("#,##0.00");
                String totalNumberOfItemsQuantity = "\n" + totalNumberOfSales.ToString();
                graphics.DrawString(totalNumberOfItemsQuantity, fontArial7Regular, drawBrush, new RectangleF(x, y + 1, width, height), drawFormatLeft);
                graphics.DrawString(totalSalesAmount, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
            }
            else
            {
                // =================
                // 80mm Report Title
                // =================
                String title = "Sales Summary Report";
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


                String salesDate = "\nSales Date";
                String salesNumber = "\nSales No.";
                String amount = "\nAmount";
                graphics.DrawString(salesDate, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(salesNumber, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                graphics.DrawString(amount, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(salesDate, fontArial8Regular).Height + 3.0F;

                // ========
                // 2nd Line
                // ========
                Point secondLineFirstPoint = new Point(0, Convert.ToInt32(y) + 3);
                Point secondLineSecondPoint = new Point(500, Convert.ToInt32(y) + 3);
                graphics.DrawLine(blackPen, secondLineFirstPoint, secondLineSecondPoint);


                Decimal totalNumberOfSales = 0;
                Decimal totalAmount = 0;

                var sales = from d in db.TrnSales
                            where d.SalesDate >= dateStart
                            && d.SalesDate <= dateEnd
                            && d.TerminalId == filterTerminalId
                            && d.IsLocked == true
                            && d.IsCancelled == false
                            select d;
                if (sales.Any())
                {

                    var SalesList = from s in sales
                                    group s by new
                                    {
                                        s.Id,
                                        s.MstTerminal,
                                        s.SalesDate,
                                        s.SalesNumber,
                                        s.MstCustomer,
                                        s.MstTerm,
                                        s.Remarks,
                                        s.MstUser,
                                        s.Amount,
                                        s.EntryDateTime,
                                    } into g
                                    select new
                                    {
                                        g.Key.MstTerminal,
                                        g.Key.MstTerminal.Terminal,
                                        g.Key.SalesDate,
                                        g.Key.SalesNumber,
                                        g.Key.MstCustomer,
                                        g.Key.MstCustomer.CustomerCode,
                                        g.Key.MstCustomer.Customer,
                                        g.Key.MstTerm,
                                        g.Key.MstTerm.Term,
                                        g.Key.Remarks,
                                        g.Key.MstUser,
                                        g.Key.MstUser.FullName,
                                        Amount = g.Sum(a => a.Amount),
                                        g.Key.EntryDateTime,

                                    };


                    foreach (var salesList in sales.ToList())
                    {
                        totalNumberOfSales += 1;
                        totalAmount += salesList.Amount;
                        String salesData = salesList.SalesDate + salesList.SalesNumber + salesList.Amount.ToString("#,##0.00");

                        RectangleF itemDataRectangle = new RectangleF
                        {
                            X = x,
                            Y = y,

                            Size = new Size(240, ((int)graphics.MeasureString(salesData, fontArial8Regular, 240, StringFormat.GenericDefault).Height))
                        };
                        graphics.DrawString("\n" + salesList.SalesDate.ToShortDateString() + "\n", fontArial8Regular, drawBrush, new RectangleF(x, y, 120, height), drawFormatLeft);
                        graphics.DrawString("\n" + salesList.SalesNumber + "\n", fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                        graphics.DrawString("\n" + salesList.Amount.ToString("#,##0.00") + "\n", fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                        y += itemDataRectangle.Size.Height + 3.0F;
                    }
                }
                // ========
                // 3rd Line
                // ========
                Point thirdLineFirstPoint = new Point(0, Convert.ToInt32(y) + 12);
                Point thirdLineSecondPoint = new Point(500, Convert.ToInt32(y) + 12);
                graphics.DrawLine(blackPen, thirdLineFirstPoint, thirdLineSecondPoint);

                String totalSalesAmount = "\n" + totalAmount.ToString("#,##0.00");
                String totalNumberOfItemsQuantity = "\n" + totalNumberOfSales.ToString();
                graphics.DrawString(totalNumberOfItemsQuantity, fontArial8Regular, drawBrush, new RectangleF(x, y + 1, width, height), drawFormatLeft);
                graphics.DrawString(totalSalesAmount, fontArial8Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
            }


            if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Dot Matrix Printer")
            {
                String space = "\n\n\n\n\n\n\n\n\n\n.";
                graphics.DrawString(space, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            }
            else if (Modules.SysCurrentModule.GetCurrentSettings().PrinterType == "Thermal Printer")
            {
                String space = "\n\n\n.";
                graphics.DrawString(space, fontArial8Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            }
            else
            {
                String space = "\n.";
                graphics.DrawString(space, fontArial7Bold, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
            }
        }
    }
}
