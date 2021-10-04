namespace EasyPOS.Forms.Software.TrnDisbursement
{
    partial class TrnRefundForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnRefundForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBoxAmount = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxOrderReturnNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxCustomer = new System.Windows.Forms.TextBox();
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
            this.panel1.Controls.Add(this.buttonOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(531, 50);
            this.panel1.TabIndex = 8;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::EasyPOS.Properties.Resources.Disbursement;
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
            this.label1.Size = new System.Drawing.Size(80, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "Refund";
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
            this.buttonClose.Size = new System.Drawing.Size(70, 32);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonOK.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonOK.FlatAppearance.BorderSize = 0;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOK.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOK.ForeColor = System.Drawing.Color.White;
            this.buttonOK.Location = new System.Drawing.Point(376, 10);
            this.buttonOK.Margin = new System.Windows.Forms.Padding(2);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(70, 32);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = false;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBoxAmount);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.textBoxOrderReturnNumber);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.textBoxCustomer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(531, 113);
            this.panel2.TabIndex = 9;
            // 
            // textBoxAmount
            // 
            this.textBoxAmount.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxAmount.Location = new System.Drawing.Point(145, 74);
            this.textBoxAmount.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxAmount.Name = "textBoxAmount";
            this.textBoxAmount.ReadOnly = true;
            this.textBoxAmount.Size = new System.Drawing.Size(203, 29);
            this.textBoxAmount.TabIndex = 3;
            this.textBoxAmount.TabStop = false;
            this.textBoxAmount.Text = "0.00";
            this.textBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label9.Location = new System.Drawing.Point(63, 77);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 21);
            this.label9.TabIndex = 23;
            this.label9.Text = "Amount:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label2.Location = new System.Drawing.Point(11, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 21);
            this.label2.TabIndex = 13;
            this.label2.Text = "Order Return No.";
            // 
            // textBoxOrderReturnNumber
            // 
            this.textBoxOrderReturnNumber.AcceptsTab = true;
            this.textBoxOrderReturnNumber.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxOrderReturnNumber.HideSelection = false;
            this.textBoxOrderReturnNumber.Location = new System.Drawing.Point(145, 8);
            this.textBoxOrderReturnNumber.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxOrderReturnNumber.Name = "textBoxOrderReturnNumber";
            this.textBoxOrderReturnNumber.Size = new System.Drawing.Size(301, 29);
            this.textBoxOrderReturnNumber.TabIndex = 0;
            this.textBoxOrderReturnNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxOrderReturnNumber_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label3.Location = new System.Drawing.Point(51, 44);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 21);
            this.label3.TabIndex = 14;
            this.label3.Text = "Customer:";
            // 
            // textBoxCustomer
            // 
            this.textBoxCustomer.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxCustomer.Location = new System.Drawing.Point(145, 41);
            this.textBoxCustomer.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxCustomer.Name = "textBoxCustomer";
            this.textBoxCustomer.ReadOnly = true;
            this.textBoxCustomer.Size = new System.Drawing.Size(376, 29);
            this.textBoxCustomer.TabIndex = 1;
            this.textBoxCustomer.TabStop = false;
            // 
            // TrnRefundForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(531, 163);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "TrnRefundForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Refund";
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
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxOrderReturnNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxCustomer;
        private System.Windows.Forms.TextBox textBoxAmount;
        private System.Windows.Forms.Label label9;
    }
}