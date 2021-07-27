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

namespace EasyPOS.Forms.Software.TrnPOS
{
    public partial class TrnPOSSalesOrderReportFormLabelPrinter : Form
    {
        public Int32 trnSalesId = 0;

        public Int32 ItemId = 0;
        public String ItemDescription = "";
        public String Unit = "";
        public Decimal Price = 0;
        public Decimal NetPrice = 0;
        public Decimal DiscountRate = 0;
        public String TaxCode = "";
        public String Tax = "";
        public Decimal Amount = 0;
        public Decimal Quantity = 0;
        public Decimal DiscountAmount = 0;
        public Decimal TaxAmount = 0;
        public String Remarks = "";

        public TrnPOSSalesOrderReportFormLabelPrinter(Int32 salesId, String printerName)
        {
            InitializeComponent();
            trnSalesId = salesId;

            printDocumentLabelReport.PrinterSettings.PrinterName = printerName;
            printDocumentLabelReport.DefaultPageSettings.PaperSize = new PaperSize("Official Receipt", 170, 135);

            Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

            var sales = from d in db.TrnSales
                        where d.Id == trnSalesId
                        select d;
            
            if (sales.Any())
            {
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
                                                   TaxAmount = g.Sum(a => a.TaxAmount),
                                                   g.Key.Preparation,
                                               };

                    if (salesLineGroupbyItem.Any())
                    {
                        foreach (var salesLine in salesLineGroupbyItem.ToList())
                        {
                            ItemId = salesLine.ItemId;
                            ItemDescription = salesLine.ItemDescription;
                            Unit = salesLine.Unit;
                            Price = salesLine.Price;
                            NetPrice = salesLine.NetPrice;
                            TaxCode = salesLine.MstTax.Code;
                            Tax = salesLine.Tax;
                            Amount = salesLine.Amount;
                            Quantity = salesLine.Quantity;
                            DiscountAmount = salesLine.DiscountAmount;
                            TaxAmount = salesLine.TaxAmount;
                            Remarks = salesLine.Preparation;

                            printDocumentLabelReport.Print();

                        }
                    }
                }
            }
        }

        private void printDocumentLabelReport_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
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
                width = 107.0F; height = 0F;
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

            var sales = from d in db.TrnSales
                        where d.Id == trnSalesId
                        select d;

            if (sales.Any())
            {
                String collectionNumberText = sales.FirstOrDefault().SalesNumber;
                graphics.DrawString(collectionNumberText, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(collectionNumberText, fontArial7Regular).Height;

                String collectionDateText = sales.FirstOrDefault().SalesDate.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture);
                graphics.DrawString(collectionDateText, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(collectionDateText, fontArial7Regular).Height;

                String collectionTimeText = sales.FirstOrDefault().UpdateDateTime.ToString("H:mm:ss", CultureInfo.InvariantCulture);
                graphics.DrawString(collectionTimeText, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatCenter);
                y += graphics.MeasureString(collectionTimeText, fontArial7Regular).Height;

                // ==========
                // Sales Line
                // ==========
                Decimal totalAmount = 0;
                String itemData = ItemDescription + "\n" + Quantity.ToString("#,##0.00") + " " + Unit + " @ " + Price.ToString("#,##0.00") + " - " + TaxCode[0];

                RectangleF itemDataRectangle = new RectangleF
                {
                    X = x,
                    Y = y,
                    Size = new Size(170, ((int)graphics.MeasureString(itemData, fontArial7Regular, 170, StringFormat.GenericDefault).Height))
                };
                graphics.DrawString(itemData, fontArial7Regular, Brushes.Black, itemDataRectangle, drawFormatLeft);

                // ============
                // Total Amount
                // ============
                String totalSalesLabel = "\n\n\nTotal Amount:";
                String totalSalesAmount = "\n\n\n" + totalAmount.ToString("#,##0.00");
                graphics.DrawString(totalSalesLabel, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                graphics.DrawString(totalSalesAmount, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatRight);
                y += graphics.MeasureString(totalSalesAmount, fontArial7Regular).Height;

                // =======
                // Remarks
                // =======
                String remarks = "Remarks: " + Remarks;
                RectangleF remarksDataRectangle = new RectangleF
                {
                    X = x,
                    Y = y,
                    Size = new Size(170, ((int)graphics.MeasureString(remarks, fontArial7Regular, 170, StringFormat.GenericDefault).Height))
                };
                graphics.DrawString(remarks, fontArial7Regular, Brushes.Black, remarksDataRectangle, drawFormatLeft);
                y += remarksDataRectangle.Size.Height + 3.0F;

                // ========
                // Customer
                // ========
                String customerName = sales.FirstOrDefault().MstCustomer.Customer;

                String customerData = customerName;
                graphics.DrawString(customerData, fontArial7Regular, drawBrush, new RectangleF(x, y, width, height), drawFormatLeft);
                y += graphics.MeasureString(customerData, fontArial7Regular).Height;
            }
        }
    }
}
