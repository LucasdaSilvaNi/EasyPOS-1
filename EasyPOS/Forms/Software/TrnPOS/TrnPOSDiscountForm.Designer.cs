namespace EasyPOS.Forms.Software.TrnPOS
{
    partial class TrnPOSDiscountForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnPOSDiscountForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxDiscountedPax = new System.Windows.Forms.TextBox();
            this.textBoxPax = new System.Windows.Forms.TextBox();
            this.textBoxTotalSalesAmount = new System.Windows.Forms.TextBox();
            this.comboBoxItem = new System.Windows.Forms.ComboBox();
            this.textBoxDiscountAmount = new System.Windows.Forms.TextBox();
            this.textBoxDiscountRate = new System.Windows.Forms.TextBox();
            this.textBoxSeniorCitizenAge = new System.Windows.Forms.TextBox();
            this.textBoxSeniorCitizenName = new System.Windows.Forms.TextBox();
            this.comboBoxDiscount = new System.Windows.Forms.ComboBox();
            this.textBoxSeniorCitizenID = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(570, 50);
            this.panel1.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::EasyPOS.Properties.Resources.Discounting;
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
            this.label1.Size = new System.Drawing.Size(96, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "Discount";
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
            this.buttonClose.Location = new System.Drawing.Point(451, 10);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(109, 32);
            this.buttonClose.TabIndex = 21;
            this.buttonClose.TabStop = false;
            this.buttonClose.Text = "Esc - Cancel";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
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
            this.buttonSave.Location = new System.Drawing.Point(335, 10);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(112, 32);
            this.buttonSave.TabIndex = 20;
            this.buttonSave.TabStop = false;
            this.buttonSave.Text = "F2 - OK";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.textBoxDiscountedPax);
            this.panel2.Controls.Add(this.textBoxPax);
            this.panel2.Controls.Add(this.textBoxTotalSalesAmount);
            this.panel2.Controls.Add(this.comboBoxItem);
            this.panel2.Controls.Add(this.textBoxDiscountAmount);
            this.panel2.Controls.Add(this.textBoxDiscountRate);
            this.panel2.Controls.Add(this.textBoxSeniorCitizenAge);
            this.panel2.Controls.Add(this.textBoxSeniorCitizenName);
            this.panel2.Controls.Add(this.comboBoxDiscount);
            this.panel2.Controls.Add(this.textBoxSeniorCitizenID);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(570, 335);
            this.panel2.TabIndex = 5;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label11.Location = new System.Drawing.Point(13, 304);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(118, 21);
            this.label11.TabIndex = 52;
            this.label11.Text = "Discounted Pax:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label10.Location = new System.Drawing.Point(13, 271);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 21);
            this.label10.TabIndex = 51;
            this.label10.Text = "Pax:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label5.Location = new System.Drawing.Point(13, 238);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 21);
            this.label5.TabIndex = 40;
            this.label5.Text = "SC/PWD Age:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label4.Location = new System.Drawing.Point(11, 205);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 21);
            this.label4.TabIndex = 38;
            this.label4.Text = "SC/PWD Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label2.Location = new System.Drawing.Point(11, 172);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 21);
            this.label2.TabIndex = 34;
            this.label2.Text = "SC/PWD ID:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label7.Location = new System.Drawing.Point(11, 139);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(134, 21);
            this.label7.TabIndex = 44;
            this.label7.Text = "Discount Amount:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label6.Location = new System.Drawing.Point(13, 106);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 21);
            this.label6.TabIndex = 42;
            this.label6.Text = "Rate (%):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label3.Location = new System.Drawing.Point(13, 73);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 21);
            this.label3.TabIndex = 37;
            this.label3.Text = "Discount:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label9.Location = new System.Drawing.Point(11, 40);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(105, 21);
            this.label9.TabIndex = 48;
            this.label9.Text = "Total Amount:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label8.Location = new System.Drawing.Point(11, 7);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 21);
            this.label8.TabIndex = 46;
            this.label8.Text = "Item:";
            // 
            // textBoxDiscountedPax
            // 
            this.textBoxDiscountedPax.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxDiscountedPax.Location = new System.Drawing.Point(160, 301);
            this.textBoxDiscountedPax.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxDiscountedPax.Name = "textBoxDiscountedPax";
            this.textBoxDiscountedPax.Size = new System.Drawing.Size(117, 29);
            this.textBoxDiscountedPax.TabIndex = 50;
            this.textBoxDiscountedPax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxDiscountedPax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxDiscountedPax_KeyPress);
            this.textBoxDiscountedPax.Leave += new System.EventHandler(this.textBoxDiscountedPax_Leave);
            // 
            // textBoxPax
            // 
            this.textBoxPax.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxPax.Location = new System.Drawing.Point(160, 268);
            this.textBoxPax.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPax.Name = "textBoxPax";
            this.textBoxPax.Size = new System.Drawing.Size(117, 29);
            this.textBoxPax.TabIndex = 49;
            this.textBoxPax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxPax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPax_KeyPress);
            this.textBoxPax.Leave += new System.EventHandler(this.textBoxPax_Leave);
            // 
            // textBoxTotalSalesAmount
            // 
            this.textBoxTotalSalesAmount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxTotalSalesAmount.Location = new System.Drawing.Point(160, 37);
            this.textBoxTotalSalesAmount.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxTotalSalesAmount.Name = "textBoxTotalSalesAmount";
            this.textBoxTotalSalesAmount.ReadOnly = true;
            this.textBoxTotalSalesAmount.Size = new System.Drawing.Size(224, 29);
            this.textBoxTotalSalesAmount.TabIndex = 47;
            this.textBoxTotalSalesAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // comboBoxItem
            // 
            this.comboBoxItem.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBoxItem.FormattingEnabled = true;
            this.comboBoxItem.Location = new System.Drawing.Point(160, 4);
            this.comboBoxItem.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxItem.Name = "comboBoxItem";
            this.comboBoxItem.Size = new System.Drawing.Size(344, 29);
            this.comboBoxItem.TabIndex = 45;
            this.comboBoxItem.SelectedIndexChanged += new System.EventHandler(this.comboBoxItem_SelectedIndexChanged);
            this.comboBoxItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.comboBoxItem_KeyDown);
            this.comboBoxItem.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxItem_KeyPress);
            this.comboBoxItem.Leave += new System.EventHandler(this.comboBoxItem_Leave);
            // 
            // textBoxDiscountAmount
            // 
            this.textBoxDiscountAmount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxDiscountAmount.Location = new System.Drawing.Point(160, 136);
            this.textBoxDiscountAmount.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxDiscountAmount.Name = "textBoxDiscountAmount";
            this.textBoxDiscountAmount.Size = new System.Drawing.Size(224, 29);
            this.textBoxDiscountAmount.TabIndex = 43;
            this.textBoxDiscountAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxDiscountAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxDiscountAmount_KeyDown);
            this.textBoxDiscountAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxDiscountAmount_KeyPress);
            this.textBoxDiscountAmount.Leave += new System.EventHandler(this.textBoxDiscountAmount_Leave);
            // 
            // textBoxDiscountRate
            // 
            this.textBoxDiscountRate.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxDiscountRate.Location = new System.Drawing.Point(160, 103);
            this.textBoxDiscountRate.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxDiscountRate.Name = "textBoxDiscountRate";
            this.textBoxDiscountRate.Size = new System.Drawing.Size(224, 29);
            this.textBoxDiscountRate.TabIndex = 41;
            this.textBoxDiscountRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxDiscountRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxDiscountRate_KeyDown);
            this.textBoxDiscountRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxDiscountRate_KeyPress);
            this.textBoxDiscountRate.Leave += new System.EventHandler(this.textBoxDiscountRate_Leave);
            // 
            // textBoxSeniorCitizenAge
            // 
            this.textBoxSeniorCitizenAge.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxSeniorCitizenAge.Location = new System.Drawing.Point(160, 235);
            this.textBoxSeniorCitizenAge.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSeniorCitizenAge.Name = "textBoxSeniorCitizenAge";
            this.textBoxSeniorCitizenAge.Size = new System.Drawing.Size(117, 29);
            this.textBoxSeniorCitizenAge.TabIndex = 3;
            this.textBoxSeniorCitizenAge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxSeniorCitizenAge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSeniorCitizenAge_KeyPress);
            this.textBoxSeniorCitizenAge.Leave += new System.EventHandler(this.textBoxSeniorCitizenAge_Leave);
            // 
            // textBoxSeniorCitizenName
            // 
            this.textBoxSeniorCitizenName.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxSeniorCitizenName.Location = new System.Drawing.Point(160, 202);
            this.textBoxSeniorCitizenName.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSeniorCitizenName.Name = "textBoxSeniorCitizenName";
            this.textBoxSeniorCitizenName.Size = new System.Drawing.Size(344, 29);
            this.textBoxSeniorCitizenName.TabIndex = 2;
            // 
            // comboBoxDiscount
            // 
            this.comboBoxDiscount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.comboBoxDiscount.FormattingEnabled = true;
            this.comboBoxDiscount.Location = new System.Drawing.Point(160, 70);
            this.comboBoxDiscount.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxDiscount.Name = "comboBoxDiscount";
            this.comboBoxDiscount.Size = new System.Drawing.Size(344, 29);
            this.comboBoxDiscount.TabIndex = 0;
            this.comboBoxDiscount.SelectedIndexChanged += new System.EventHandler(this.comboBoxDiscount_SelectedIndexChanged);
            // 
            // textBoxSeniorCitizenID
            // 
            this.textBoxSeniorCitizenID.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxSeniorCitizenID.Location = new System.Drawing.Point(160, 169);
            this.textBoxSeniorCitizenID.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSeniorCitizenID.Name = "textBoxSeniorCitizenID";
            this.textBoxSeniorCitizenID.Size = new System.Drawing.Size(344, 29);
            this.textBoxSeniorCitizenID.TabIndex = 1;
            // 
            // TrnPOSDiscountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(570, 385);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.Name = "TrnPOSDiscountForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Discount";
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
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBoxSeniorCitizenID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxDiscount;
        private System.Windows.Forms.TextBox textBoxSeniorCitizenAge;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxSeniorCitizenName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxDiscountRate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxDiscountAmount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxItem;
        private System.Windows.Forms.TextBox textBoxTotalSalesAmount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxDiscountedPax;
        private System.Windows.Forms.TextBox textBoxPax;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
    }
}