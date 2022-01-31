namespace EasyPOS.Forms.Software.TrnPOS
{
    partial class TrnPOSSalesItemDetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnPOSSalesItemDetailForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSave = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.textBoxItemDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSalesLineQuantity = new System.Windows.Forms.TextBox();
            this.textBoxSalesLineUnit = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSalesLinePrice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxSalesLineDiscount = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxSalesLineDiscountRate = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxSalesLineDiscountAmount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxSalesLineNetPrice = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxSalesLineAmount = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxSalesLineVAT = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxSalesLineVATRate = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxSalesLineVATAmount = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxSalesLineRemarks = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxBodegaItemQty = new System.Windows.Forms.TextBox();
            this.checkBoxDelivery = new System.Windows.Forms.CheckBox();
            this.comboBoxDeliveryStatus = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(860, 62);
            this.panel1.TabIndex = 5;
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonSave.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonSave.FlatAppearance.BorderSize = 0;
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location = new System.Drawing.Point(569, 12);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(140, 40);
            this.buttonSave.TabIndex = 20;
            this.buttonSave.TabStop = false;
            this.buttonSave.Text = "Ent - Save";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::EasyPOS.Properties.Resources.POS;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(62, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 35);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sales Item";
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
            this.buttonClose.Location = new System.Drawing.Point(714, 12);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(136, 40);
            this.buttonClose.TabIndex = 20;
            this.buttonClose.TabStop = false;
            this.buttonClose.Text = "Esc - Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // textBoxItemDescription
            // 
            this.textBoxItemDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxItemDescription.BackColor = System.Drawing.Color.White;
            this.textBoxItemDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxItemDescription.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.textBoxItemDescription.Location = new System.Drawing.Point(12, 6);
            this.textBoxItemDescription.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxItemDescription.Name = "textBoxItemDescription";
            this.textBoxItemDescription.ReadOnly = true;
            this.textBoxItemDescription.Size = new System.Drawing.Size(836, 40);
            this.textBoxItemDescription.TabIndex = 13;
            this.textBoxItemDescription.TabStop = false;
            this.textBoxItemDescription.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label2.Location = new System.Drawing.Point(14, 56);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 28);
            this.label2.TabIndex = 7;
            this.label2.Text = "Quantity:";
            // 
            // textBoxSalesLineQuantity
            // 
            this.textBoxSalesLineQuantity.AcceptsTab = true;
            this.textBoxSalesLineQuantity.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxSalesLineQuantity.HideSelection = false;
            this.textBoxSalesLineQuantity.Location = new System.Drawing.Point(182, 52);
            this.textBoxSalesLineQuantity.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSalesLineQuantity.Name = "textBoxSalesLineQuantity";
            this.textBoxSalesLineQuantity.Size = new System.Drawing.Size(269, 34);
            this.textBoxSalesLineQuantity.TabIndex = 0;
            this.textBoxSalesLineQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxSalesLineQuantity.TextChanged += new System.EventHandler(this.textBoxSalesLineQuantity_TextChanged);
            this.textBoxSalesLineQuantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSalesLineQuantity_KeyDown);
            this.textBoxSalesLineQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSalesLineQuantity_KeyPress);
            this.textBoxSalesLineQuantity.Leave += new System.EventHandler(this.textBoxSalesLineQuantity_Leave);
            // 
            // textBoxSalesLineUnit
            // 
            this.textBoxSalesLineUnit.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxSalesLineUnit.Location = new System.Drawing.Point(182, 92);
            this.textBoxSalesLineUnit.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSalesLineUnit.Name = "textBoxSalesLineUnit";
            this.textBoxSalesLineUnit.ReadOnly = true;
            this.textBoxSalesLineUnit.Size = new System.Drawing.Size(269, 34);
            this.textBoxSalesLineUnit.TabIndex = 2;
            this.textBoxSalesLineUnit.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label3.Location = new System.Drawing.Point(14, 96);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 28);
            this.label3.TabIndex = 9;
            this.label3.Text = "Unit:";
            // 
            // textBoxSalesLinePrice
            // 
            this.textBoxSalesLinePrice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBoxSalesLinePrice.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Underline);
            this.textBoxSalesLinePrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(238)))));
            this.textBoxSalesLinePrice.Location = new System.Drawing.Point(182, 132);
            this.textBoxSalesLinePrice.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSalesLinePrice.Name = "textBoxSalesLinePrice";
            this.textBoxSalesLinePrice.ReadOnly = true;
            this.textBoxSalesLinePrice.Size = new System.Drawing.Size(269, 34);
            this.textBoxSalesLinePrice.TabIndex = 3;
            this.textBoxSalesLinePrice.TabStop = false;
            this.textBoxSalesLinePrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxSalesLinePrice.Click += new System.EventHandler(this.textBoxSalesLinePrice_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label4.Location = new System.Drawing.Point(14, 136);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 28);
            this.label4.TabIndex = 11;
            this.label4.Text = "Price:";
            // 
            // comboBoxSalesLineDiscount
            // 
            this.comboBoxSalesLineDiscount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBoxSalesLineDiscount.FormattingEnabled = true;
            this.comboBoxSalesLineDiscount.Location = new System.Drawing.Point(182, 172);
            this.comboBoxSalesLineDiscount.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxSalesLineDiscount.Name = "comboBoxSalesLineDiscount";
            this.comboBoxSalesLineDiscount.Size = new System.Drawing.Size(269, 36);
            this.comboBoxSalesLineDiscount.TabIndex = 4;
            this.comboBoxSalesLineDiscount.SelectedIndexChanged += new System.EventHandler(this.comboBoxSalesLineDiscount_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label5.Location = new System.Drawing.Point(12, 176);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 28);
            this.label5.TabIndex = 14;
            this.label5.Text = "Discount:";
            // 
            // textBoxSalesLineDiscountRate
            // 
            this.textBoxSalesLineDiscountRate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxSalesLineDiscountRate.Location = new System.Drawing.Point(182, 214);
            this.textBoxSalesLineDiscountRate.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSalesLineDiscountRate.Name = "textBoxSalesLineDiscountRate";
            this.textBoxSalesLineDiscountRate.ReadOnly = true;
            this.textBoxSalesLineDiscountRate.Size = new System.Drawing.Size(269, 34);
            this.textBoxSalesLineDiscountRate.TabIndex = 5;
            this.textBoxSalesLineDiscountRate.TabStop = false;
            this.textBoxSalesLineDiscountRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxSalesLineDiscountRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSalesLineDiscountRate_KeyPress);
            this.textBoxSalesLineDiscountRate.Leave += new System.EventHandler(this.textBoxSalesLineDiscountRate_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label6.Location = new System.Drawing.Point(12, 218);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 28);
            this.label6.TabIndex = 15;
            this.label6.Text = "Discount Rate:";
            // 
            // textBoxSalesLineDiscountAmount
            // 
            this.textBoxSalesLineDiscountAmount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxSalesLineDiscountAmount.Location = new System.Drawing.Point(182, 254);
            this.textBoxSalesLineDiscountAmount.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSalesLineDiscountAmount.Name = "textBoxSalesLineDiscountAmount";
            this.textBoxSalesLineDiscountAmount.ReadOnly = true;
            this.textBoxSalesLineDiscountAmount.Size = new System.Drawing.Size(269, 34);
            this.textBoxSalesLineDiscountAmount.TabIndex = 6;
            this.textBoxSalesLineDiscountAmount.TabStop = false;
            this.textBoxSalesLineDiscountAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxSalesLineDiscountAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSalesLineDiscountAmount_KeyPress);
            this.textBoxSalesLineDiscountAmount.Leave += new System.EventHandler(this.textBoxSalesLineDiscountAmount_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label7.Location = new System.Drawing.Point(8, 258);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(169, 28);
            this.label7.TabIndex = 17;
            this.label7.Text = "Discount Amount:";
            // 
            // textBoxSalesLineNetPrice
            // 
            this.textBoxSalesLineNetPrice.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxSalesLineNetPrice.Location = new System.Drawing.Point(182, 294);
            this.textBoxSalesLineNetPrice.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSalesLineNetPrice.Name = "textBoxSalesLineNetPrice";
            this.textBoxSalesLineNetPrice.ReadOnly = true;
            this.textBoxSalesLineNetPrice.Size = new System.Drawing.Size(269, 34);
            this.textBoxSalesLineNetPrice.TabIndex = 7;
            this.textBoxSalesLineNetPrice.TabStop = false;
            this.textBoxSalesLineNetPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label8.Location = new System.Drawing.Point(14, 298);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 28);
            this.label8.TabIndex = 19;
            this.label8.Text = "Net Price:";
            // 
            // textBoxSalesLineAmount
            // 
            this.textBoxSalesLineAmount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxSalesLineAmount.Location = new System.Drawing.Point(182, 334);
            this.textBoxSalesLineAmount.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSalesLineAmount.Name = "textBoxSalesLineAmount";
            this.textBoxSalesLineAmount.ReadOnly = true;
            this.textBoxSalesLineAmount.Size = new System.Drawing.Size(269, 34);
            this.textBoxSalesLineAmount.TabIndex = 8;
            this.textBoxSalesLineAmount.TabStop = false;
            this.textBoxSalesLineAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label9.Location = new System.Drawing.Point(14, 338);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 28);
            this.label9.TabIndex = 21;
            this.label9.Text = "Amount:";
            // 
            // textBoxSalesLineVAT
            // 
            this.textBoxSalesLineVAT.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxSalesLineVAT.Location = new System.Drawing.Point(182, 374);
            this.textBoxSalesLineVAT.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSalesLineVAT.Name = "textBoxSalesLineVAT";
            this.textBoxSalesLineVAT.ReadOnly = true;
            this.textBoxSalesLineVAT.Size = new System.Drawing.Size(269, 34);
            this.textBoxSalesLineVAT.TabIndex = 9;
            this.textBoxSalesLineVAT.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label10.Location = new System.Drawing.Point(14, 378);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 28);
            this.label10.TabIndex = 23;
            this.label10.Text = "VAT:";
            // 
            // textBoxSalesLineVATRate
            // 
            this.textBoxSalesLineVATRate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxSalesLineVATRate.Location = new System.Drawing.Point(182, 414);
            this.textBoxSalesLineVATRate.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSalesLineVATRate.Name = "textBoxSalesLineVATRate";
            this.textBoxSalesLineVATRate.ReadOnly = true;
            this.textBoxSalesLineVATRate.Size = new System.Drawing.Size(269, 34);
            this.textBoxSalesLineVATRate.TabIndex = 10;
            this.textBoxSalesLineVATRate.TabStop = false;
            this.textBoxSalesLineVATRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label11.Location = new System.Drawing.Point(12, 418);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 28);
            this.label11.TabIndex = 25;
            this.label11.Text = "VAT Rate:";
            // 
            // textBoxSalesLineVATAmount
            // 
            this.textBoxSalesLineVATAmount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxSalesLineVATAmount.Location = new System.Drawing.Point(182, 454);
            this.textBoxSalesLineVATAmount.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSalesLineVATAmount.Name = "textBoxSalesLineVATAmount";
            this.textBoxSalesLineVATAmount.ReadOnly = true;
            this.textBoxSalesLineVATAmount.Size = new System.Drawing.Size(269, 34);
            this.textBoxSalesLineVATAmount.TabIndex = 11;
            this.textBoxSalesLineVATAmount.TabStop = false;
            this.textBoxSalesLineVATAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label12.Location = new System.Drawing.Point(12, 458);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(125, 28);
            this.label12.TabIndex = 27;
            this.label12.Text = "VAT Amount:";
            // 
            // textBoxSalesLineRemarks
            // 
            this.textBoxSalesLineRemarks.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxSalesLineRemarks.Location = new System.Drawing.Point(458, 214);
            this.textBoxSalesLineRemarks.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSalesLineRemarks.Multiline = true;
            this.textBoxSalesLineRemarks.Name = "textBoxSalesLineRemarks";
            this.textBoxSalesLineRemarks.Size = new System.Drawing.Size(392, 274);
            this.textBoxSalesLineRemarks.TabIndex = 12;
            this.textBoxSalesLineRemarks.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label13.Location = new System.Drawing.Point(455, 180);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(209, 28);
            this.label13.TabIndex = 29;
            this.label13.Text = "Remarks / Preparation:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.comboBoxDeliveryStatus);
            this.panel2.Controls.Add(this.checkBoxDelivery);
            this.panel2.Controls.Add(this.textBoxBodegaItemQty);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.textBoxItemDescription);
            this.panel2.Controls.Add(this.textBoxSalesLineRemarks);
            this.panel2.Controls.Add(this.textBoxSalesLineQuantity);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.textBoxSalesLineVATAmount);
            this.panel2.Controls.Add(this.textBoxSalesLineUnit);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.textBoxSalesLineVATRate);
            this.panel2.Controls.Add(this.textBoxSalesLinePrice);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.comboBoxSalesLineDiscount);
            this.panel2.Controls.Add(this.textBoxSalesLineVAT);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.textBoxSalesLineAmount);
            this.panel2.Controls.Add(this.textBoxSalesLineDiscountRate);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.textBoxSalesLineNetPrice);
            this.panel2.Controls.Add(this.textBoxSalesLineDiscountAmount);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 62);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(860, 500);
            this.panel2.TabIndex = 31;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label14.Location = new System.Drawing.Point(468, 55);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(117, 28);
            this.label14.TabIndex = 30;
            this.label14.Text = "For Bodega:";
            // 
            // textBoxBodegaItemQty
            // 
            this.textBoxBodegaItemQty.AcceptsTab = true;
            this.textBoxBodegaItemQty.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxBodegaItemQty.HideSelection = false;
            this.textBoxBodegaItemQty.Location = new System.Drawing.Point(589, 53);
            this.textBoxBodegaItemQty.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxBodegaItemQty.Name = "textBoxBodegaItemQty";
            this.textBoxBodegaItemQty.Size = new System.Drawing.Size(261, 34);
            this.textBoxBodegaItemQty.TabIndex = 2;
            this.textBoxBodegaItemQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // checkBoxDelivery
            // 
            this.checkBoxDelivery.AutoSize = true;
            this.checkBoxDelivery.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.checkBoxDelivery.Location = new System.Drawing.Point(502, 96);
            this.checkBoxDelivery.Name = "checkBoxDelivery";
            this.checkBoxDelivery.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxDelivery.Size = new System.Drawing.Size(105, 32);
            this.checkBoxDelivery.TabIndex = 33;
            this.checkBoxDelivery.Text = "Delivery";
            this.checkBoxDelivery.UseVisualStyleBackColor = true;
            this.checkBoxDelivery.CheckedChanged += new System.EventHandler(this.checkBoxDelivery_CheckedChanged);
            // 
            // comboBoxDeliveryStatus
            // 
            this.comboBoxDeliveryStatus.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBoxDeliveryStatus.FormattingEnabled = true;
            this.comboBoxDeliveryStatus.Location = new System.Drawing.Point(589, 132);
            this.comboBoxDeliveryStatus.Name = "comboBoxDeliveryStatus";
            this.comboBoxDeliveryStatus.Size = new System.Drawing.Size(259, 36);
            this.comboBoxDeliveryStatus.TabIndex = 34;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label15.Location = new System.Drawing.Point(515, 136);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(69, 28);
            this.label15.TabIndex = 35;
            this.label15.Text = "Status:";
            // 
            // TrnPOSSalesItemDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(860, 562);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.MaximizeBox = false;
            this.Name = "TrnPOSSalesItemDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Item";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TextBox textBoxItemDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSalesLineQuantity;
        private System.Windows.Forms.TextBox textBoxSalesLineUnit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSalesLinePrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxSalesLineDiscount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxSalesLineDiscountRate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxSalesLineDiscountAmount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxSalesLineNetPrice;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxSalesLineAmount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxSalesLineVAT;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxSalesLineVATRate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxSalesLineVATAmount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxSalesLineRemarks;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox comboBoxDeliveryStatus;
        private System.Windows.Forms.CheckBox checkBoxDelivery;
        private System.Windows.Forms.TextBox textBoxBodegaItemQty;
        private System.Windows.Forms.Label label14;
    }
}