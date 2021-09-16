namespace EasyPOS.Forms.Software.TrnPOS
{
    partial class TrnPOSReturn
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnPOSReturn));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonReturn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxReturnSalesNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel15 = new System.Windows.Forms.Panel();
            this.buttonReturnPageListFirst = new System.Windows.Forms.Button();
            this.buttonReturnPageListPrevious = new System.Windows.Forms.Button();
            this.buttonReturnPageListNext = new System.Windows.Forms.Button();
            this.buttonReturnPageListLast = new System.Windows.Forms.Button();
            this.textBoxReturnPageNumber = new System.Windows.Forms.TextBox();
            this.textBoxReturnORNumber = new System.Windows.Forms.TextBox();
            this.dataGridViewReturnItems = new System.Windows.Forms.DataGridView();
            this.ColumnReturnItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnReturnItemDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnReturnUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnReturnPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnReturnQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnReturnDiscountAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnReturnNetPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnReturnAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnReturnPickItem = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnReturnUnpickItem = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnReturnReturnQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnReturnTaxId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnReturnTaxRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnReturnTaxAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReturnItems)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.buttonReturn);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(912, 50);
            this.panel1.TabIndex = 6;
            // 
            // buttonReturn
            // 
            this.buttonReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReturn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonReturn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonReturn.FlatAppearance.BorderSize = 0;
            this.buttonReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReturn.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReturn.ForeColor = System.Drawing.Color.White;
            this.buttonReturn.Location = new System.Drawing.Point(682, 10);
            this.buttonReturn.Margin = new System.Windows.Forms.Padding(2);
            this.buttonReturn.Name = "buttonReturn";
            this.buttonReturn.Size = new System.Drawing.Size(108, 32);
            this.buttonReturn.TabIndex = 7;
            this.buttonReturn.TabStop = false;
            this.buttonReturn.Text = "F2 - Return";
            this.buttonReturn.UseVisualStyleBackColor = false;
            this.buttonReturn.Click += new System.EventHandler(this.buttonReturn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::EasyPOS.Properties.Resources.POS;
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
            this.label1.Size = new System.Drawing.Size(76, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "Return";
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
            this.buttonClose.Location = new System.Drawing.Point(794, 10);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(109, 32);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.TabStop = false;
            this.buttonClose.Text = "Esc - Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.textBoxReturnSalesNumber);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.panel15);
            this.panel2.Controls.Add(this.textBoxReturnORNumber);
            this.panel2.Controls.Add(this.dataGridViewReturnItems);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(912, 393);
            this.panel2.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label3.Location = new System.Drawing.Point(11, 40);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 21);
            this.label3.TabIndex = 28;
            this.label3.Text = "Sales Number:";
            // 
            // textBoxReturnSalesNumber
            // 
            this.textBoxReturnSalesNumber.AcceptsTab = true;
            this.textBoxReturnSalesNumber.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxReturnSalesNumber.HideSelection = false;
            this.textBoxReturnSalesNumber.Location = new System.Drawing.Point(126, 37);
            this.textBoxReturnSalesNumber.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxReturnSalesNumber.Name = "textBoxReturnSalesNumber";
            this.textBoxReturnSalesNumber.ReadOnly = true;
            this.textBoxReturnSalesNumber.Size = new System.Drawing.Size(314, 29);
            this.textBoxReturnSalesNumber.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label2.Location = new System.Drawing.Point(25, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 21);
            this.label2.TabIndex = 19;
            this.label2.Text = "OR Number:";
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.White;
            this.panel15.Controls.Add(this.buttonReturnPageListFirst);
            this.panel15.Controls.Add(this.buttonReturnPageListPrevious);
            this.panel15.Controls.Add(this.buttonReturnPageListNext);
            this.panel15.Controls.Add(this.buttonReturnPageListLast);
            this.panel15.Controls.Add(this.textBoxReturnPageNumber);
            this.panel15.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel15.Location = new System.Drawing.Point(0, 351);
            this.panel15.Margin = new System.Windows.Forms.Padding(2);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(912, 42);
            this.panel15.TabIndex = 26;
            // 
            // buttonReturnPageListFirst
            // 
            this.buttonReturnPageListFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonReturnPageListFirst.Enabled = false;
            this.buttonReturnPageListFirst.FlatAppearance.BorderSize = 0;
            this.buttonReturnPageListFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReturnPageListFirst.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonReturnPageListFirst.Location = new System.Drawing.Point(10, 7);
            this.buttonReturnPageListFirst.Margin = new System.Windows.Forms.Padding(2);
            this.buttonReturnPageListFirst.Name = "buttonReturnPageListFirst";
            this.buttonReturnPageListFirst.Size = new System.Drawing.Size(66, 26);
            this.buttonReturnPageListFirst.TabIndex = 13;
            this.buttonReturnPageListFirst.Text = "First";
            this.buttonReturnPageListFirst.UseVisualStyleBackColor = false;
            this.buttonReturnPageListFirst.Click += new System.EventHandler(this.buttonReturnPageListFirst_Click);
            // 
            // buttonReturnPageListPrevious
            // 
            this.buttonReturnPageListPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonReturnPageListPrevious.Enabled = false;
            this.buttonReturnPageListPrevious.FlatAppearance.BorderSize = 0;
            this.buttonReturnPageListPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReturnPageListPrevious.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonReturnPageListPrevious.Location = new System.Drawing.Point(80, 7);
            this.buttonReturnPageListPrevious.Margin = new System.Windows.Forms.Padding(2);
            this.buttonReturnPageListPrevious.Name = "buttonReturnPageListPrevious";
            this.buttonReturnPageListPrevious.Size = new System.Drawing.Size(66, 26);
            this.buttonReturnPageListPrevious.TabIndex = 14;
            this.buttonReturnPageListPrevious.Text = "Previous";
            this.buttonReturnPageListPrevious.UseVisualStyleBackColor = false;
            this.buttonReturnPageListPrevious.Click += new System.EventHandler(this.buttonReturnPageListPrevious_Click);
            // 
            // buttonReturnPageListNext
            // 
            this.buttonReturnPageListNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonReturnPageListNext.FlatAppearance.BorderSize = 0;
            this.buttonReturnPageListNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReturnPageListNext.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonReturnPageListNext.Location = new System.Drawing.Point(210, 7);
            this.buttonReturnPageListNext.Margin = new System.Windows.Forms.Padding(2);
            this.buttonReturnPageListNext.Name = "buttonReturnPageListNext";
            this.buttonReturnPageListNext.Size = new System.Drawing.Size(66, 26);
            this.buttonReturnPageListNext.TabIndex = 15;
            this.buttonReturnPageListNext.Text = "Next";
            this.buttonReturnPageListNext.UseVisualStyleBackColor = false;
            this.buttonReturnPageListNext.Click += new System.EventHandler(this.buttonReturnPageListNext_Click);
            // 
            // buttonReturnPageListLast
            // 
            this.buttonReturnPageListLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonReturnPageListLast.FlatAppearance.BorderSize = 0;
            this.buttonReturnPageListLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReturnPageListLast.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonReturnPageListLast.Location = new System.Drawing.Point(278, 7);
            this.buttonReturnPageListLast.Margin = new System.Windows.Forms.Padding(2);
            this.buttonReturnPageListLast.Name = "buttonReturnPageListLast";
            this.buttonReturnPageListLast.Size = new System.Drawing.Size(66, 26);
            this.buttonReturnPageListLast.TabIndex = 16;
            this.buttonReturnPageListLast.Text = "Last";
            this.buttonReturnPageListLast.UseVisualStyleBackColor = false;
            this.buttonReturnPageListLast.Click += new System.EventHandler(this.buttonReturnPageListLast_Click);
            // 
            // textBoxReturnPageNumber
            // 
            this.textBoxReturnPageNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxReturnPageNumber.BackColor = System.Drawing.Color.White;
            this.textBoxReturnPageNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxReturnPageNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxReturnPageNumber.Location = new System.Drawing.Point(150, 11);
            this.textBoxReturnPageNumber.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxReturnPageNumber.Name = "textBoxReturnPageNumber";
            this.textBoxReturnPageNumber.ReadOnly = true;
            this.textBoxReturnPageNumber.Size = new System.Drawing.Size(55, 16);
            this.textBoxReturnPageNumber.TabIndex = 17;
            this.textBoxReturnPageNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxReturnORNumber
            // 
            this.textBoxReturnORNumber.AcceptsTab = true;
            this.textBoxReturnORNumber.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.textBoxReturnORNumber.HideSelection = false;
            this.textBoxReturnORNumber.Location = new System.Drawing.Point(126, 4);
            this.textBoxReturnORNumber.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxReturnORNumber.Name = "textBoxReturnORNumber";
            this.textBoxReturnORNumber.Size = new System.Drawing.Size(314, 29);
            this.textBoxReturnORNumber.TabIndex = 18;
            this.textBoxReturnORNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxReturnORNumber_KeyDown);
            // 
            // dataGridViewReturnItems
            // 
            this.dataGridViewReturnItems.AllowUserToAddRows = false;
            this.dataGridViewReturnItems.AllowUserToDeleteRows = false;
            this.dataGridViewReturnItems.AllowUserToResizeRows = false;
            this.dataGridViewReturnItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewReturnItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewReturnItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReturnItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnReturnItemId,
            this.ColumnReturnItemDescription,
            this.ColumnReturnUnit,
            this.ColumnReturnPrice,
            this.ColumnReturnQuantity,
            this.ColumnReturnDiscountAmount,
            this.ColumnReturnNetPrice,
            this.ColumnReturnAmount,
            this.ColumnReturnPickItem,
            this.ColumnReturnUnpickItem,
            this.ColumnReturnReturnQuantity,
            this.ColumnReturnTaxId,
            this.ColumnReturnTaxRate,
            this.ColumnReturnTaxAmount});
            this.dataGridViewReturnItems.Location = new System.Drawing.Point(10, 70);
            this.dataGridViewReturnItems.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewReturnItems.MultiSelect = false;
            this.dataGridViewReturnItems.Name = "dataGridViewReturnItems";
            this.dataGridViewReturnItems.RowTemplate.Height = 24;
            this.dataGridViewReturnItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewReturnItems.Size = new System.Drawing.Size(893, 276);
            this.dataGridViewReturnItems.TabIndex = 25;
            this.dataGridViewReturnItems.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewReturn_CellClick);
            // 
            // ColumnReturnItemId
            // 
            this.ColumnReturnItemId.DataPropertyName = "ColumnReturnItemId";
            this.ColumnReturnItemId.HeaderText = "Item Id";
            this.ColumnReturnItemId.Name = "ColumnReturnItemId";
            this.ColumnReturnItemId.ReadOnly = true;
            this.ColumnReturnItemId.Visible = false;
            // 
            // ColumnReturnItemDescription
            // 
            this.ColumnReturnItemDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnReturnItemDescription.DataPropertyName = "ColumnReturnItemDescription";
            this.ColumnReturnItemDescription.HeaderText = "Item Description";
            this.ColumnReturnItemDescription.MinimumWidth = 180;
            this.ColumnReturnItemDescription.Name = "ColumnReturnItemDescription";
            this.ColumnReturnItemDescription.ReadOnly = true;
            // 
            // ColumnReturnUnit
            // 
            this.ColumnReturnUnit.DataPropertyName = "ColumnReturnUnit";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnReturnUnit.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnReturnUnit.HeaderText = "Unit";
            this.ColumnReturnUnit.Name = "ColumnReturnUnit";
            this.ColumnReturnUnit.ReadOnly = true;
            this.ColumnReturnUnit.Width = 70;
            // 
            // ColumnReturnPrice
            // 
            this.ColumnReturnPrice.DataPropertyName = "ColumnReturnPrice";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnReturnPrice.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnReturnPrice.HeaderText = "Price";
            this.ColumnReturnPrice.Name = "ColumnReturnPrice";
            this.ColumnReturnPrice.ReadOnly = true;
            // 
            // ColumnReturnQuantity
            // 
            this.ColumnReturnQuantity.DataPropertyName = "ColumnReturnQuantity";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnReturnQuantity.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnReturnQuantity.HeaderText = "Quantity";
            this.ColumnReturnQuantity.Name = "ColumnReturnQuantity";
            this.ColumnReturnQuantity.ReadOnly = true;
            // 
            // ColumnReturnDiscountAmount
            // 
            this.ColumnReturnDiscountAmount.DataPropertyName = "ColumnReturnDiscountAmount";
            this.ColumnReturnDiscountAmount.HeaderText = "Discount";
            this.ColumnReturnDiscountAmount.Name = "ColumnReturnDiscountAmount";
            // 
            // ColumnReturnNetPrice
            // 
            this.ColumnReturnNetPrice.DataPropertyName = "ColumnReturnNetPrice";
            this.ColumnReturnNetPrice.HeaderText = "Net Price";
            this.ColumnReturnNetPrice.Name = "ColumnReturnNetPrice";
            // 
            // ColumnReturnAmount
            // 
            this.ColumnReturnAmount.DataPropertyName = "ColumnReturnAmount";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnReturnAmount.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColumnReturnAmount.HeaderText = "Amount";
            this.ColumnReturnAmount.Name = "ColumnReturnAmount";
            this.ColumnReturnAmount.ReadOnly = true;
            // 
            // ColumnReturnPickItem
            // 
            this.ColumnReturnPickItem.DataPropertyName = "ColumnReturnPickItem";
            this.ColumnReturnPickItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColumnReturnPickItem.HeaderText = "";
            this.ColumnReturnPickItem.Name = "ColumnReturnPickItem";
            this.ColumnReturnPickItem.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnReturnPickItem.Width = 70;
            // 
            // ColumnReturnUnpickItem
            // 
            this.ColumnReturnUnpickItem.DataPropertyName = "ColumnReturnUnpickItem";
            this.ColumnReturnUnpickItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColumnReturnUnpickItem.HeaderText = "";
            this.ColumnReturnUnpickItem.Name = "ColumnReturnUnpickItem";
            this.ColumnReturnUnpickItem.Width = 70;
            // 
            // ColumnReturnReturnQuantity
            // 
            this.ColumnReturnReturnQuantity.DataPropertyName = "ColumnReturnReturnQuantity";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnReturnReturnQuantity.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColumnReturnReturnQuantity.HeaderText = "Return Quantity";
            this.ColumnReturnReturnQuantity.Name = "ColumnReturnReturnQuantity";
            this.ColumnReturnReturnQuantity.ReadOnly = true;
            this.ColumnReturnReturnQuantity.Width = 150;
            // 
            // ColumnReturnTaxId
            // 
            this.ColumnReturnTaxId.DataPropertyName = "ColumnReturnTaxId";
            this.ColumnReturnTaxId.HeaderText = "Tax Id";
            this.ColumnReturnTaxId.Name = "ColumnReturnTaxId";
            this.ColumnReturnTaxId.Visible = false;
            // 
            // ColumnReturnTaxRate
            // 
            this.ColumnReturnTaxRate.DataPropertyName = "ColumnReturnTaxRate";
            this.ColumnReturnTaxRate.HeaderText = "Tax Rate";
            this.ColumnReturnTaxRate.Name = "ColumnReturnTaxRate";
            this.ColumnReturnTaxRate.Visible = false;
            // 
            // ColumnReturnTaxAmount
            // 
            this.ColumnReturnTaxAmount.DataPropertyName = "ColumnReturnTaxAmount";
            this.ColumnReturnTaxAmount.HeaderText = "Tax Amount";
            this.ColumnReturnTaxAmount.Name = "ColumnReturnTaxAmount";
            this.ColumnReturnTaxAmount.Visible = false;
            // 
            // TrnPOSReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(912, 443);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "TrnPOSReturn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Return";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel15.ResumeLayout(false);
            this.panel15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReturnItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridViewReturnItems;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Button buttonReturnPageListFirst;
        private System.Windows.Forms.Button buttonReturnPageListPrevious;
        private System.Windows.Forms.Button buttonReturnPageListNext;
        private System.Windows.Forms.Button buttonReturnPageListLast;
        private System.Windows.Forms.TextBox textBoxReturnPageNumber;
        private System.Windows.Forms.Button buttonReturn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxReturnSalesNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxReturnORNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReturnItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReturnItemDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReturnUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReturnPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReturnQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReturnDiscountAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReturnNetPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReturnAmount;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnReturnPickItem;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnReturnUnpickItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReturnReturnQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReturnTaxId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReturnTaxRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnReturnTaxAmount;
    }
}