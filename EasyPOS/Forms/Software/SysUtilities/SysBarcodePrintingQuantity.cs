using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zen.Barcode;

namespace EasyPOS.Forms.Software.SysUtilities
{
    public partial class SysBarcodePrintingQuantity : Form
    {
        public Int32 itemId;
        public Entities.MstItemEntity itemEntity;

        public Int32 quantity = 0;
        public Int32 columns = 0;

        public SysBarcodePrintingQuantity(Int32 itemIdFilter)
        {
            InitializeComponent();
            itemId = itemIdFilter;

            textBoxPrintQuantity.Text = "0";
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            printBarcode();
            Close();
        }

        public void printBarcode()
        {
            try
            {
                Controllers.MstItemController mstItemController = new Controllers.MstItemController();
                if (mstItemController.DetailItem(itemId) != null)
                {
                    itemEntity = mstItemController.DetailItem(itemId);

                    DialogResult printDialogBarcodePrintingDialogResult = printDialogBarcodePrinting.ShowDialog();
                    if (printDialogBarcodePrintingDialogResult == DialogResult.OK)
                    {
                        printDocumentBarcodePrinting.PrinterSettings.PrinterName = printDialogBarcodePrinting.PrinterSettings.PrinterName;
                        quantity = Convert.ToInt32(textBoxPrintQuantity.Text);

                        for (int i = 1; i <= quantity; i++)
                        {
                            if (i % 3 == 1)
                            {
                                columns = 1;
                            }
                            else if (i % 3 == 2)
                            {
                                columns = 2;
                            }
                            else if (i % 3 == 0)
                            {
                                columns = 3;
                                printDocumentBarcodePrinting.Print();
                            }
                            else
                            {
                                columns = 0;
                            }

                            if (i % 3 != 0)
                            {
                                if (quantity == i)
                                {
                                    if (quantity % 3 == 1)
                                    {
                                        columns = 1;
                                    }

                                    if (quantity % 3 == 2)
                                    {
                                        columns = 2;
                                    }

                                    printDocumentBarcodePrinting.Print();
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Item not found.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxPrintQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBoxPrintQuantity_Leave(object sender, EventArgs e)
        {
            try
            {
                textBoxPrintQuantity.Text = Convert.ToDecimal(textBoxPrintQuantity.Text).ToString("#,##0");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxPrintQuantity_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxPrintQuantity.Text))
            {
                textBoxPrintQuantity.Text = "0";
            }
        }

        private void printDocumentBarcodePrinting_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // =====
            // Sizes
            // =====
            float x = 5, y = 10;
            float width = 113.58F, height = 0F;

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

            // =============
            // Font Settings
            // =============
            Font fontArial6Bold = new Font("Arial", 6, FontStyle.Bold);
            Font fontArial6Regular = new Font("Arial", 6, FontStyle.Regular);
            Font fontArial7Bold = new Font("Arial", 7, FontStyle.Bold);
            Font fontArial7Regular = new Font("Arial", 7, FontStyle.Regular);
            Font fontArial8Bold = new Font("Arial", 8, FontStyle.Bold);
            Font fontArial8Regular = new Font("Arial", 8, FontStyle.Regular);
            Font fontCalibri6Bold = new Font("Calibri", 6, FontStyle.Bold);
            Font fontCalibri8Bold = new Font("Calibri", 8, FontStyle.Bold);
            Font fontCalibri10Bold = new Font("Calibri", 10, FontStyle.Bold);

            // ==================
            // Alignment Settings
            // ==================
            StringFormat drawFormatCenter = new StringFormat { Alignment = StringAlignment.Center };
            StringFormat drawFormatLeft = new StringFormat { Alignment = StringAlignment.Near };
            StringFormat drawFormatRight = new StringFormat { Alignment = StringAlignment.Far };

           
            String company = Modules.SysCurrentModule.GetCurrentSettings().CompanyName;
            String itemAlias = itemEntity.Alias;
            String barcodeNumber = itemEntity.BarCode;
            Code128BarcodeDraw barcode = BarcodeDrawFactory.Code128WithChecksum;
            Image image = barcode.Draw(itemEntity.BarCode, 35);
            String itemPrice = "P " + itemEntity.Price.ToString("#,##0.00");
            float AdjustStringItemDescription = 1;
            if (itemAlias.Length > 25)
            {
                AdjustStringItemDescription = 3;
            }

            switch (columns)
            {
                case 1:
                    graphics.DrawString(company, fontCalibri6Bold, drawBrush, new RectangleF(x, y:2, width, height), drawFormatCenter);
                    y += graphics.MeasureString(company, fontCalibri6Bold).Height;
                                        
                    graphics.DrawImage(image, new RectangleF(x, y:14, 107, 28));
                    y += image.Height + 7;

                    graphics.DrawString(barcodeNumber, fontCalibri6Bold, drawBrush, new RectangleF(x, y:41, width, height), drawFormatCenter);
                    y += graphics.MeasureString(barcodeNumber, fontCalibri6Bold).Height;

                    graphics.DrawString(itemAlias, fontArial6Bold, drawBrush, new RectangleF(x, y:52, width, height), drawFormatCenter);
                    y += graphics.MeasureString(itemAlias, fontArial6Bold).Height * AdjustStringItemDescription;

                    graphics.DrawString(itemPrice, fontCalibri10Bold, drawBrush, new RectangleF(x, y:78, width, height), drawFormatCenter);
                    y += graphics.MeasureString(itemPrice, fontCalibri10Bold).Height;

                    break;
                case 2:
                    graphics.DrawString(company, fontCalibri6Bold, drawBrush, new RectangleF(x, y:2, width, height), drawFormatCenter);
                    graphics.DrawString(company, fontCalibri6Bold, drawBrush, new RectangleF(x + 7 + width, y:2, width, height), drawFormatCenter);
                    y += graphics.MeasureString(company, fontCalibri6Bold).Height;
                                        
                    graphics.DrawImage(image, new RectangleF(x, y: 14, 107, 28));
                    graphics.DrawImage(image, new RectangleF(x + 15 + 107, y: 14, 107, 28));
                    y += image.Height + 7;

                    graphics.DrawString(barcodeNumber, fontCalibri6Bold, drawBrush, new RectangleF(x, y: 41, width, height), drawFormatCenter);
                    graphics.DrawString(barcodeNumber, fontCalibri6Bold, drawBrush, new RectangleF(x + 7 + width, y: 41, width, height), drawFormatCenter);
                    y += graphics.MeasureString(barcodeNumber, fontCalibri6Bold).Height;

                    graphics.DrawString(itemAlias, fontArial6Bold, drawBrush, new RectangleF(x, y: 52, width, height), drawFormatCenter);
                    graphics.DrawString(itemAlias, fontArial6Bold, drawBrush, new RectangleF(x + 7 + width, y: 52, width, height), drawFormatCenter);
                    y += graphics.MeasureString(itemAlias, fontArial6Bold).Height * AdjustStringItemDescription;

                    graphics.DrawString(itemPrice, fontCalibri10Bold, drawBrush, new RectangleF(x, y: 78, width, height), drawFormatCenter);
                    graphics.DrawString(itemPrice, fontCalibri10Bold, drawBrush, new RectangleF(x + 7 + width, y: 78, width, height), drawFormatCenter);
                    y += graphics.MeasureString(itemPrice, fontCalibri10Bold).Height;

                    break;
                case 3:
                    graphics.DrawString(company, fontCalibri6Bold, drawBrush, new RectangleF(x, y:2, width, height), drawFormatCenter);
                    graphics.DrawString(company, fontCalibri6Bold, drawBrush, new RectangleF(x + 7 + width, y:2, width, height), drawFormatCenter);
                    graphics.DrawString(company, fontCalibri6Bold, drawBrush, new RectangleF(x + 15 + width + width, y:2, width, height), drawFormatCenter);
                    y += graphics.MeasureString(company, fontCalibri6Bold).Height;
                                   
                    graphics.DrawImage(image, new RectangleF(x, y: 14, 107, 28));
                    graphics.DrawImage(image, new RectangleF(x + 15 + 107, y: 14, 107, 28));
                    graphics.DrawImage(image, new RectangleF(x + 30 + 214, y: 14, 107, 28));
                    y += image.Height + 7;

                    graphics.DrawString(barcodeNumber, fontCalibri6Bold, drawBrush, new RectangleF(x, y: 41, width, height), drawFormatCenter);
                    graphics.DrawString(barcodeNumber, fontCalibri6Bold, drawBrush, new RectangleF(x + 7 + width, y: 41, width, height), drawFormatCenter);
                    graphics.DrawString(barcodeNumber, fontCalibri6Bold, drawBrush, new RectangleF(x + 15 + width + width, y: 41, width, height), drawFormatCenter);
                    y += graphics.MeasureString(barcodeNumber, fontCalibri6Bold).Height;

                    graphics.DrawString(itemAlias, fontArial6Bold, drawBrush, new RectangleF(x, y: 52, width, height), drawFormatCenter);
                    graphics.DrawString(itemAlias, fontArial6Bold, drawBrush, new RectangleF(x + 7 + width, y: 52, width, height), drawFormatCenter);
                    graphics.DrawString(itemAlias, fontArial6Bold, drawBrush, new RectangleF(x + 15 + width + width, y: 52, width, height), drawFormatCenter);
                    y += graphics.MeasureString(itemAlias, fontArial6Bold).Height * AdjustStringItemDescription;

                    graphics.DrawString(itemPrice, fontCalibri10Bold, drawBrush, new RectangleF(x, y: 78, width, height), drawFormatCenter);
                    graphics.DrawString(itemPrice, fontCalibri10Bold, drawBrush, new RectangleF(x + 7 + width, y: 78, width, height), drawFormatCenter);
                    graphics.DrawString(itemPrice, fontCalibri10Bold, drawBrush, new RectangleF(x + 15 + width + width, y: 78, width, height), drawFormatCenter);
                    y += graphics.MeasureString(itemPrice, fontCalibri10Bold).Height;

                    break;
                default:
                    break;
            }
        }
    }
}
