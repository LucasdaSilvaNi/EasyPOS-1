namespace EasyPOS.Forms.Software.TrnPOS
{
    partial class TrnPOSTouchActivityForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnPOSTouchActivityForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.imageListPOSTouchOthers = new System.Windows.Forms.ImageList(this.components);
            this.buttonReprint = new System.Windows.Forms.Button();
            this.imageListPOSTouchBIllOut = new System.Windows.Forms.ImageList(this.components);
            this.buttonDeliver = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelInvoiceNumber = new System.Windows.Forms.Label();
            this.buttonTender = new System.Windows.Forms.Button();
            this.buttonBillOut = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.imageListPOSTouchCancel = new System.Windows.Forms.ImageList(this.components);
            this.buttonSplitMergeBill = new System.Windows.Forms.Button();
            this.buttonPrintPartialBill = new System.Windows.Forms.Button();
            this.buttonEditOrder = new System.Windows.Forms.Button();
            this.imageListPOSTouchEditOrder = new System.Windows.Forms.ImageList(this.components);
            this.printDialogReprintOR = new System.Windows.Forms.PrintDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(803, 50);
            this.panel1.TabIndex = 5;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::EasyPOS.Properties.Resources.POS_Touch;
            this.pictureBox1.Location = new System.Drawing.Point(10, 10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(50, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "POS Touch Activity";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(79)))), ((int)(((byte)(28)))));
            this.buttonClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(79)))), ((int)(((byte)(28)))));
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.ForeColor = System.Drawing.Color.White;
            this.buttonClose.Location = new System.Drawing.Point(683, 10);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(109, 32);
            this.buttonClose.TabIndex = 21;
            this.buttonClose.TabStop = false;
            this.buttonClose.Text = "Esc - Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.buttonCancel);
            this.panel2.Controls.Add(this.buttonReprint);
            this.panel2.Controls.Add(this.buttonDeliver);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.buttonTender);
            this.panel2.Controls.Add(this.buttonBillOut);
            this.panel2.Controls.Add(this.buttonDelete);
            this.panel2.Controls.Add(this.buttonSplitMergeBill);
            this.panel2.Controls.Add(this.buttonPrintPartialBill);
            this.panel2.Controls.Add(this.buttonEditOrder);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(803, 378);
            this.panel2.TabIndex = 6;
            // 
            // buttonCancel
            // 
            this.buttonCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonCancel.FlatAppearance.BorderSize = 0;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.buttonCancel.ForeColor = System.Drawing.Color.White;
            this.buttonCancel.ImageIndex = 0;
            this.buttonCancel.ImageList = this.imageListPOSTouchOthers;
            this.buttonCancel.Location = new System.Drawing.Point(665, 155);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Padding = new System.Windows.Forms.Padding(8);
            this.buttonCancel.Size = new System.Drawing.Size(127, 105);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "\r\nF7 - Cancel";
            this.buttonCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonCancel.UseVisualStyleBackColor = false;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // imageListPOSTouchOthers
            // 
            this.imageListPOSTouchOthers.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPOSTouchOthers.ImageStream")));
            this.imageListPOSTouchOthers.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListPOSTouchOthers.Images.SetKeyName(0, "cancel.png");
            this.imageListPOSTouchOthers.Images.SetKeyName(1, "print.png");
            this.imageListPOSTouchOthers.Images.SetKeyName(2, "splitMergeBill.png");
            this.imageListPOSTouchOthers.Images.SetKeyName(3, "tender.png");
            this.imageListPOSTouchOthers.Images.SetKeyName(4, "DeliveryMan.png");
            // 
            // buttonReprint
            // 
            this.buttonReprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonReprint.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonReprint.FlatAppearance.BorderSize = 0;
            this.buttonReprint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReprint.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.buttonReprint.ForeColor = System.Drawing.Color.White;
            this.buttonReprint.ImageIndex = 0;
            this.buttonReprint.ImageList = this.imageListPOSTouchBIllOut;
            this.buttonReprint.Location = new System.Drawing.Point(272, 155);
            this.buttonReprint.Margin = new System.Windows.Forms.Padding(2);
            this.buttonReprint.Name = "buttonReprint";
            this.buttonReprint.Padding = new System.Windows.Forms.Padding(8);
            this.buttonReprint.Size = new System.Drawing.Size(258, 105);
            this.buttonReprint.TabIndex = 10;
            this.buttonReprint.Text = " \r\nF5 -Reprint";
            this.buttonReprint.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonReprint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonReprint.UseVisualStyleBackColor = false;
            this.buttonReprint.Click += new System.EventHandler(this.buttonReprint_Click);
            // 
            // imageListPOSTouchBIllOut
            // 
            this.imageListPOSTouchBIllOut.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPOSTouchBIllOut.ImageStream")));
            this.imageListPOSTouchBIllOut.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListPOSTouchBIllOut.Images.SetKeyName(0, "print.png");
            // 
            // buttonDeliver
            // 
            this.buttonDeliver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonDeliver.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonDeliver.FlatAppearance.BorderSize = 0;
            this.buttonDeliver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeliver.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.buttonDeliver.ForeColor = System.Drawing.Color.White;
            this.buttonDeliver.ImageIndex = 4;
            this.buttonDeliver.ImageList = this.imageListPOSTouchOthers;
            this.buttonDeliver.Location = new System.Drawing.Point(10, 264);
            this.buttonDeliver.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDeliver.Name = "buttonDeliver";
            this.buttonDeliver.Padding = new System.Windows.Forms.Padding(8);
            this.buttonDeliver.Size = new System.Drawing.Size(258, 105);
            this.buttonDeliver.TabIndex = 9;
            this.buttonDeliver.Text = "\r\nF8 - Deliver";
            this.buttonDeliver.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonDeliver.UseVisualStyleBackColor = false;
            this.buttonDeliver.Click += new System.EventHandler(this.buttonDeliver_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(188)))), ((int)(((byte)(0)))));
            this.panel3.Controls.Add(this.labelInvoiceNumber);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(803, 42);
            this.panel3.TabIndex = 8;
            // 
            // labelInvoiceNumber
            // 
            this.labelInvoiceNumber.AutoSize = true;
            this.labelInvoiceNumber.Font = new System.Drawing.Font("Segoe UI", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInvoiceNumber.ForeColor = System.Drawing.Color.White;
            this.labelInvoiceNumber.Location = new System.Drawing.Point(14, 6);
            this.labelInvoiceNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelInvoiceNumber.Name = "labelInvoiceNumber";
            this.labelInvoiceNumber.Size = new System.Drawing.Size(0, 25);
            this.labelInvoiceNumber.TabIndex = 6;
            // 
            // buttonTender
            // 
            this.buttonTender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonTender.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonTender.FlatAppearance.BorderSize = 0;
            this.buttonTender.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTender.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.buttonTender.ForeColor = System.Drawing.Color.White;
            this.buttonTender.ImageIndex = 3;
            this.buttonTender.ImageList = this.imageListPOSTouchOthers;
            this.buttonTender.Location = new System.Drawing.Point(534, 46);
            this.buttonTender.Margin = new System.Windows.Forms.Padding(2);
            this.buttonTender.Name = "buttonTender";
            this.buttonTender.Padding = new System.Windows.Forms.Padding(8);
            this.buttonTender.Size = new System.Drawing.Size(258, 105);
            this.buttonTender.TabIndex = 7;
            this.buttonTender.Text = "\r\nF4 - Tender";
            this.buttonTender.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonTender.UseVisualStyleBackColor = false;
            this.buttonTender.Click += new System.EventHandler(this.buttonTender_Click);
            // 
            // buttonBillOut
            // 
            this.buttonBillOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonBillOut.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonBillOut.FlatAppearance.BorderSize = 0;
            this.buttonBillOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBillOut.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.buttonBillOut.ForeColor = System.Drawing.Color.White;
            this.buttonBillOut.ImageIndex = 0;
            this.buttonBillOut.ImageList = this.imageListPOSTouchBIllOut;
            this.buttonBillOut.Location = new System.Drawing.Point(272, 46);
            this.buttonBillOut.Margin = new System.Windows.Forms.Padding(2);
            this.buttonBillOut.Name = "buttonBillOut";
            this.buttonBillOut.Padding = new System.Windows.Forms.Padding(8);
            this.buttonBillOut.Size = new System.Drawing.Size(258, 105);
            this.buttonBillOut.TabIndex = 6;
            this.buttonBillOut.Text = " \r\nF3 -Bill Out";
            this.buttonBillOut.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonBillOut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonBillOut.UseVisualStyleBackColor = false;
            this.buttonBillOut.Click += new System.EventHandler(this.buttonBillOut_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonDelete.FlatAppearance.BorderSize = 0;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.buttonDelete.ForeColor = System.Drawing.Color.White;
            this.buttonDelete.ImageIndex = 3;
            this.buttonDelete.ImageList = this.imageListPOSTouchCancel;
            this.buttonDelete.Location = new System.Drawing.Point(534, 155);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Padding = new System.Windows.Forms.Padding(8);
            this.buttonDelete.Size = new System.Drawing.Size(127, 105);
            this.buttonDelete.TabIndex = 4;
            this.buttonDelete.Text = "\r\nF6 - Delete";
            this.buttonDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // imageListPOSTouchCancel
            // 
            this.imageListPOSTouchCancel.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPOSTouchCancel.ImageStream")));
            this.imageListPOSTouchCancel.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListPOSTouchCancel.Images.SetKeyName(0, "cancel.png");
            this.imageListPOSTouchCancel.Images.SetKeyName(1, "delete.png");
            this.imageListPOSTouchCancel.Images.SetKeyName(2, "splitMergeBill.png");
            this.imageListPOSTouchCancel.Images.SetKeyName(3, "delete.png");
            // 
            // buttonSplitMergeBill
            // 
            this.buttonSplitMergeBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonSplitMergeBill.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonSplitMergeBill.FlatAppearance.BorderSize = 0;
            this.buttonSplitMergeBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSplitMergeBill.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.buttonSplitMergeBill.ForeColor = System.Drawing.Color.White;
            this.buttonSplitMergeBill.ImageIndex = 2;
            this.buttonSplitMergeBill.ImageList = this.imageListPOSTouchOthers;
            this.buttonSplitMergeBill.Location = new System.Drawing.Point(534, 264);
            this.buttonSplitMergeBill.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSplitMergeBill.Name = "buttonSplitMergeBill";
            this.buttonSplitMergeBill.Padding = new System.Windows.Forms.Padding(8);
            this.buttonSplitMergeBill.Size = new System.Drawing.Size(258, 105);
            this.buttonSplitMergeBill.TabIndex = 3;
            this.buttonSplitMergeBill.Text = "\r\nF10 - Split/Merge Bill";
            this.buttonSplitMergeBill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonSplitMergeBill.UseVisualStyleBackColor = false;
            this.buttonSplitMergeBill.Click += new System.EventHandler(this.buttonSplitMergeBill_Click);
            // 
            // buttonPrintPartialBill
            // 
            this.buttonPrintPartialBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonPrintPartialBill.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonPrintPartialBill.FlatAppearance.BorderSize = 0;
            this.buttonPrintPartialBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPrintPartialBill.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.buttonPrintPartialBill.ForeColor = System.Drawing.Color.White;
            this.buttonPrintPartialBill.ImageIndex = 1;
            this.buttonPrintPartialBill.ImageList = this.imageListPOSTouchOthers;
            this.buttonPrintPartialBill.Location = new System.Drawing.Point(272, 264);
            this.buttonPrintPartialBill.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPrintPartialBill.Name = "buttonPrintPartialBill";
            this.buttonPrintPartialBill.Padding = new System.Windows.Forms.Padding(8);
            this.buttonPrintPartialBill.Size = new System.Drawing.Size(257, 105);
            this.buttonPrintPartialBill.TabIndex = 2;
            this.buttonPrintPartialBill.Text = "\r\nF9 - Print Partial Bill";
            this.buttonPrintPartialBill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonPrintPartialBill.UseVisualStyleBackColor = false;
            // 
            // buttonEditOrder
            // 
            this.buttonEditOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonEditOrder.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonEditOrder.FlatAppearance.BorderSize = 0;
            this.buttonEditOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEditOrder.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.buttonEditOrder.ForeColor = System.Drawing.Color.White;
            this.buttonEditOrder.ImageIndex = 1;
            this.buttonEditOrder.ImageList = this.imageListPOSTouchEditOrder;
            this.buttonEditOrder.Location = new System.Drawing.Point(11, 46);
            this.buttonEditOrder.Margin = new System.Windows.Forms.Padding(2);
            this.buttonEditOrder.Name = "buttonEditOrder";
            this.buttonEditOrder.Padding = new System.Windows.Forms.Padding(8);
            this.buttonEditOrder.Size = new System.Drawing.Size(257, 214);
            this.buttonEditOrder.TabIndex = 1;
            this.buttonEditOrder.Text = "\r\nF2 - Edit Order";
            this.buttonEditOrder.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonEditOrder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonEditOrder.UseVisualStyleBackColor = false;
            this.buttonEditOrder.Click += new System.EventHandler(this.buttonEditOrder_Click);
            // 
            // imageListPOSTouchEditOrder
            // 
            this.imageListPOSTouchEditOrder.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListPOSTouchEditOrder.ImageStream")));
            this.imageListPOSTouchEditOrder.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListPOSTouchEditOrder.Images.SetKeyName(0, "cancel.png");
            this.imageListPOSTouchEditOrder.Images.SetKeyName(1, "editOrder.png");
            this.imageListPOSTouchEditOrder.Images.SetKeyName(2, "print.png");
            this.imageListPOSTouchEditOrder.Images.SetKeyName(3, "printBills.png");
            this.imageListPOSTouchEditOrder.Images.SetKeyName(4, "splitMergeBill.png");
            this.imageListPOSTouchEditOrder.Images.SetKeyName(5, "tender.png");
            // 
            // printDialogReprintOR
            // 
            this.printDialogReprintOR.UseEXDialog = true;
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // TrnPOSTouchActivityForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(803, 428);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "TrnPOSTouchActivityForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POS Touch Activity";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonSplitMergeBill;
        private System.Windows.Forms.Button buttonPrintPartialBill;
        private System.Windows.Forms.Button buttonEditOrder;
        private System.Windows.Forms.Button buttonTender;
        private System.Windows.Forms.Button buttonBillOut;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelInvoiceNumber;
        private System.Windows.Forms.ImageList imageListPOSTouchEditOrder;
        private System.Windows.Forms.ImageList imageListPOSTouchBIllOut;
        private System.Windows.Forms.ImageList imageListPOSTouchOthers;
        private System.Windows.Forms.Button buttonDeliver;
        private System.Windows.Forms.Button buttonReprint;
        private System.Windows.Forms.PrintDialog printDialogReprintOR;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ImageList imageListPOSTouchCancel;
        private System.Windows.Forms.ImageList imageList1;
    }
}