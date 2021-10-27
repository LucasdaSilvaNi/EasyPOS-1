namespace EasyPOS.Forms.Software.RepSalesReport
{
    partial class RepSalesDetailReportForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RepSalesDetailReportForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dataGridViewSalesDetailReport = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonGenerateCSV = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBoxTotalAmount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonPageListFirst = new System.Windows.Forms.Button();
            this.buttonPageListNext = new System.Windows.Forms.Button();
            this.buttonPageListLast = new System.Windows.Forms.Button();
            this.buttonPageListPrevious = new System.Windows.Forms.Button();
            this.textBoxPageNumber = new System.Windows.Forms.TextBox();
            this.folderBrowserDialogGenerateCSV = new System.Windows.Forms.FolderBrowserDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ColumnTerminal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSalesNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCustomerCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCustomer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBarCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnItemDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnItemCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSupplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCostAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDiscountAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNetPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTaxRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTaxAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSalesDetailReport)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::EasyPOS.Properties.Resources.Reports;
            this.pictureBox1.Location = new System.Drawing.Point(14, 8);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(45, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // dataGridViewSalesDetailReport
            // 
            this.dataGridViewSalesDetailReport.AllowUserToAddRows = false;
            this.dataGridViewSalesDetailReport.AllowUserToDeleteRows = false;
            this.dataGridViewSalesDetailReport.AllowUserToResizeRows = false;
            this.dataGridViewSalesDetailReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewSalesDetailReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewSalesDetailReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSalesDetailReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnTerminal,
            this.ColumnDate,
            this.ColumnSalesNumber,
            this.ColumnCustomerCode,
            this.ColumnCustomer,
            this.ColumnItemCode,
            this.ColumnBarCode,
            this.ColumnItemDescription,
            this.ColumnItemCategory,
            this.ColumnSupplier,
            this.ColumnQuantity,
            this.ColumnUnit,
            this.ColumnCost,
            this.ColumnPrice,
            this.ColumnCostAmount,
            this.ColumnDiscountAmount,
            this.ColumnNetPrice,
            this.ColumnAmount,
            this.ColumnTax,
            this.ColumnTaxRate,
            this.ColumnTaxAmount});
            this.dataGridViewSalesDetailReport.Location = new System.Drawing.Point(12, 68);
            this.dataGridViewSalesDetailReport.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewSalesDetailReport.MultiSelect = false;
            this.dataGridViewSalesDetailReport.Name = "dataGridViewSalesDetailReport";
            this.dataGridViewSalesDetailReport.ReadOnly = true;
            this.dataGridViewSalesDetailReport.RowHeadersWidth = 51;
            this.dataGridViewSalesDetailReport.RowTemplate.Height = 24;
            this.dataGridViewSalesDetailReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSalesDetailReport.ShowEditingIcon = false;
            this.dataGridViewSalesDetailReport.Size = new System.Drawing.Size(1344, 528);
            this.dataGridViewSalesDetailReport.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.buttonPrint);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.buttonGenerateCSV);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1370, 62);
            this.panel1.TabIndex = 10;
            // 
            // buttonPrint
            // 
            this.buttonPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonPrint.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonPrint.FlatAppearance.BorderSize = 0;
            this.buttonPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPrint.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPrint.ForeColor = System.Drawing.Color.White;
            this.buttonPrint.Location = new System.Drawing.Point(1082, 11);
            this.buttonPrint.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(88, 40);
            this.buttonPrint.TabIndex = 27;
            this.buttonPrint.TabStop = false;
            this.buttonPrint.Text = "Print";
            this.buttonPrint.UseVisualStyleBackColor = false;
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
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
            this.buttonClose.Location = new System.Drawing.Point(1268, 11);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(88, 40);
            this.buttonClose.TabIndex = 6;
            this.buttonClose.TabStop = false;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonGenerateCSV
            // 
            this.buttonGenerateCSV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenerateCSV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonGenerateCSV.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonGenerateCSV.FlatAppearance.BorderSize = 0;
            this.buttonGenerateCSV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGenerateCSV.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGenerateCSV.ForeColor = System.Drawing.Color.White;
            this.buttonGenerateCSV.Location = new System.Drawing.Point(1175, 11);
            this.buttonGenerateCSV.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.buttonGenerateCSV.Name = "buttonGenerateCSV";
            this.buttonGenerateCSV.Size = new System.Drawing.Size(88, 40);
            this.buttonGenerateCSV.TabIndex = 5;
            this.buttonGenerateCSV.TabStop = false;
            this.buttonGenerateCSV.Text = "CSV";
            this.buttonGenerateCSV.UseVisualStyleBackColor = false;
            this.buttonGenerateCSV.Click += new System.EventHandler(this.buttonGenerateCSV_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(71, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 35);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sales Detail Report";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.textBoxTotalAmount);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.buttonPageListFirst);
            this.panel4.Controls.Add(this.buttonPageListNext);
            this.panel4.Controls.Add(this.buttonPageListLast);
            this.panel4.Controls.Add(this.buttonPageListPrevious);
            this.panel4.Controls.Add(this.textBoxPageNumber);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 600);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1370, 52);
            this.panel4.TabIndex = 20;
            // 
            // textBoxTotalAmount
            // 
            this.textBoxTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxTotalAmount.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTotalAmount.Location = new System.Drawing.Point(1000, 15);
            this.textBoxTotalAmount.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxTotalAmount.Name = "textBoxTotalAmount";
            this.textBoxTotalAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBoxTotalAmount.Size = new System.Drawing.Size(308, 23);
            this.textBoxTotalAmount.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(860, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 23);
            this.label2.TabIndex = 13;
            this.label2.Text = "Total Amount:";
            // 
            // buttonPageListFirst
            // 
            this.buttonPageListFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPageListFirst.Enabled = false;
            this.buttonPageListFirst.FlatAppearance.BorderSize = 0;
            this.buttonPageListFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPageListFirst.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.buttonPageListFirst.Location = new System.Drawing.Point(12, 10);
            this.buttonPageListFirst.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPageListFirst.Name = "buttonPageListFirst";
            this.buttonPageListFirst.Size = new System.Drawing.Size(82, 32);
            this.buttonPageListFirst.TabIndex = 8;
            this.buttonPageListFirst.TabStop = false;
            this.buttonPageListFirst.Text = "First";
            this.buttonPageListFirst.UseVisualStyleBackColor = false;
            this.buttonPageListFirst.Click += new System.EventHandler(this.buttonSalesListPageListFirst_Click);
            // 
            // buttonPageListNext
            // 
            this.buttonPageListNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPageListNext.FlatAppearance.BorderSize = 0;
            this.buttonPageListNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPageListNext.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.buttonPageListNext.Location = new System.Drawing.Point(338, 10);
            this.buttonPageListNext.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPageListNext.Name = "buttonPageListNext";
            this.buttonPageListNext.Size = new System.Drawing.Size(82, 32);
            this.buttonPageListNext.TabIndex = 10;
            this.buttonPageListNext.TabStop = false;
            this.buttonPageListNext.Text = "Next";
            this.buttonPageListNext.UseVisualStyleBackColor = false;
            this.buttonPageListNext.Click += new System.EventHandler(this.buttonSalesListPageListNext_Click);
            // 
            // buttonPageListLast
            // 
            this.buttonPageListLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPageListLast.FlatAppearance.BorderSize = 0;
            this.buttonPageListLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPageListLast.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.buttonPageListLast.Location = new System.Drawing.Point(422, 10);
            this.buttonPageListLast.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPageListLast.Name = "buttonPageListLast";
            this.buttonPageListLast.Size = new System.Drawing.Size(82, 32);
            this.buttonPageListLast.TabIndex = 11;
            this.buttonPageListLast.TabStop = false;
            this.buttonPageListLast.Text = "Last";
            this.buttonPageListLast.UseVisualStyleBackColor = false;
            this.buttonPageListLast.Click += new System.EventHandler(this.buttonSalesListPageListLast_Click);
            // 
            // buttonPageListPrevious
            // 
            this.buttonPageListPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPageListPrevious.Enabled = false;
            this.buttonPageListPrevious.FlatAppearance.BorderSize = 0;
            this.buttonPageListPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPageListPrevious.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.buttonPageListPrevious.Location = new System.Drawing.Point(100, 10);
            this.buttonPageListPrevious.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPageListPrevious.Name = "buttonPageListPrevious";
            this.buttonPageListPrevious.Size = new System.Drawing.Size(88, 32);
            this.buttonPageListPrevious.TabIndex = 9;
            this.buttonPageListPrevious.TabStop = false;
            this.buttonPageListPrevious.Text = "Previous";
            this.buttonPageListPrevious.UseVisualStyleBackColor = false;
            this.buttonPageListPrevious.Click += new System.EventHandler(this.buttonSalesListPageListPrevious_Click);
            // 
            // textBoxPageNumber
            // 
            this.textBoxPageNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxPageNumber.BackColor = System.Drawing.Color.White;
            this.textBoxPageNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPageNumber.Location = new System.Drawing.Point(231, 15);
            this.textBoxPageNumber.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxPageNumber.Name = "textBoxPageNumber";
            this.textBoxPageNumber.ReadOnly = true;
            this.textBoxPageNumber.Size = new System.Drawing.Size(69, 23);
            this.textBoxPageNumber.TabIndex = 12;
            this.textBoxPageNumber.TabStop = false;
            this.textBoxPageNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.dataGridViewSalesDetailReport);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1370, 652);
            this.panel2.TabIndex = 21;
            // 
            // ColumnTerminal
            // 
            this.ColumnTerminal.DataPropertyName = "ColumnTerminal";
            this.ColumnTerminal.HeaderText = "Terminal";
            this.ColumnTerminal.MinimumWidth = 6;
            this.ColumnTerminal.Name = "ColumnTerminal";
            this.ColumnTerminal.ReadOnly = true;
            this.ColumnTerminal.Width = 125;
            // 
            // ColumnDate
            // 
            this.ColumnDate.DataPropertyName = "ColumnDate";
            this.ColumnDate.HeaderText = "Sales Date";
            this.ColumnDate.MinimumWidth = 6;
            this.ColumnDate.Name = "ColumnDate";
            this.ColumnDate.ReadOnly = true;
            this.ColumnDate.Width = 120;
            // 
            // ColumnSalesNumber
            // 
            this.ColumnSalesNumber.DataPropertyName = "ColumnSalesNumber";
            this.ColumnSalesNumber.HeaderText = "Sales No.";
            this.ColumnSalesNumber.MinimumWidth = 6;
            this.ColumnSalesNumber.Name = "ColumnSalesNumber";
            this.ColumnSalesNumber.ReadOnly = true;
            this.ColumnSalesNumber.Width = 120;
            // 
            // ColumnCustomerCode
            // 
            this.ColumnCustomerCode.DataPropertyName = "ColumnCustomerCode";
            this.ColumnCustomerCode.HeaderText = "Customer Code";
            this.ColumnCustomerCode.MinimumWidth = 6;
            this.ColumnCustomerCode.Name = "ColumnCustomerCode";
            this.ColumnCustomerCode.ReadOnly = true;
            this.ColumnCustomerCode.Width = 150;
            // 
            // ColumnCustomer
            // 
            this.ColumnCustomer.DataPropertyName = "ColumnCustomer";
            this.ColumnCustomer.HeaderText = "Customer";
            this.ColumnCustomer.MinimumWidth = 6;
            this.ColumnCustomer.Name = "ColumnCustomer";
            this.ColumnCustomer.ReadOnly = true;
            this.ColumnCustomer.Width = 200;
            // 
            // ColumnItemCode
            // 
            this.ColumnItemCode.DataPropertyName = "ColumnItemCode";
            this.ColumnItemCode.HeaderText = "Item Code";
            this.ColumnItemCode.MinimumWidth = 6;
            this.ColumnItemCode.Name = "ColumnItemCode";
            this.ColumnItemCode.ReadOnly = true;
            this.ColumnItemCode.Width = 150;
            // 
            // ColumnBarCode
            // 
            this.ColumnBarCode.DataPropertyName = "ColumnBarCode";
            this.ColumnBarCode.HeaderText = "Barcode";
            this.ColumnBarCode.MinimumWidth = 6;
            this.ColumnBarCode.Name = "ColumnBarCode";
            this.ColumnBarCode.ReadOnly = true;
            this.ColumnBarCode.Width = 125;
            // 
            // ColumnItemDescription
            // 
            this.ColumnItemDescription.DataPropertyName = "ColumnItemDescription";
            this.ColumnItemDescription.HeaderText = "Item Description";
            this.ColumnItemDescription.MinimumWidth = 6;
            this.ColumnItemDescription.Name = "ColumnItemDescription";
            this.ColumnItemDescription.ReadOnly = true;
            this.ColumnItemDescription.Width = 200;
            // 
            // ColumnItemCategory
            // 
            this.ColumnItemCategory.DataPropertyName = "ColumnItemCategory";
            this.ColumnItemCategory.HeaderText = "Category";
            this.ColumnItemCategory.MinimumWidth = 6;
            this.ColumnItemCategory.Name = "ColumnItemCategory";
            this.ColumnItemCategory.ReadOnly = true;
            this.ColumnItemCategory.Width = 120;
            // 
            // ColumnSupplier
            // 
            this.ColumnSupplier.DataPropertyName = "ColumnSupplier";
            this.ColumnSupplier.HeaderText = "Supplier";
            this.ColumnSupplier.MinimumWidth = 6;
            this.ColumnSupplier.Name = "ColumnSupplier";
            this.ColumnSupplier.ReadOnly = true;
            this.ColumnSupplier.Width = 125;
            // 
            // ColumnQuantity
            // 
            this.ColumnQuantity.DataPropertyName = "ColumnQuantity";
            this.ColumnQuantity.HeaderText = "Quantity";
            this.ColumnQuantity.MinimumWidth = 6;
            this.ColumnQuantity.Name = "ColumnQuantity";
            this.ColumnQuantity.ReadOnly = true;
            this.ColumnQuantity.Width = 120;
            // 
            // ColumnUnit
            // 
            this.ColumnUnit.DataPropertyName = "ColumnUnit";
            this.ColumnUnit.HeaderText = "Unit";
            this.ColumnUnit.MinimumWidth = 6;
            this.ColumnUnit.Name = "ColumnUnit";
            this.ColumnUnit.ReadOnly = true;
            this.ColumnUnit.Width = 125;
            // 
            // ColumnCost
            // 
            this.ColumnCost.DataPropertyName = "ColumnCost";
            this.ColumnCost.HeaderText = "Cost";
            this.ColumnCost.MinimumWidth = 6;
            this.ColumnCost.Name = "ColumnCost";
            this.ColumnCost.ReadOnly = true;
            this.ColumnCost.Width = 125;
            // 
            // ColumnPrice
            // 
            this.ColumnPrice.DataPropertyName = "ColumnPrice";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnPrice.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnPrice.HeaderText = "Price";
            this.ColumnPrice.MinimumWidth = 6;
            this.ColumnPrice.Name = "ColumnPrice";
            this.ColumnPrice.ReadOnly = true;
            this.ColumnPrice.Width = 120;
            // 
            // ColumnCostAmount
            // 
            this.ColumnCostAmount.DataPropertyName = "ColumnCostAmount";
            this.ColumnCostAmount.HeaderText = "Cost Amount";
            this.ColumnCostAmount.MinimumWidth = 6;
            this.ColumnCostAmount.Name = "ColumnCostAmount";
            this.ColumnCostAmount.ReadOnly = true;
            this.ColumnCostAmount.Width = 125;
            // 
            // ColumnDiscountAmount
            // 
            this.ColumnDiscountAmount.DataPropertyName = "ColumnDiscountAmount";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnDiscountAmount.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnDiscountAmount.HeaderText = "Discount";
            this.ColumnDiscountAmount.MinimumWidth = 6;
            this.ColumnDiscountAmount.Name = "ColumnDiscountAmount";
            this.ColumnDiscountAmount.ReadOnly = true;
            this.ColumnDiscountAmount.Width = 120;
            // 
            // ColumnNetPrice
            // 
            this.ColumnNetPrice.DataPropertyName = "ColumnNetPrice";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnNetPrice.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnNetPrice.HeaderText = "Net Price";
            this.ColumnNetPrice.MinimumWidth = 6;
            this.ColumnNetPrice.Name = "ColumnNetPrice";
            this.ColumnNetPrice.ReadOnly = true;
            this.ColumnNetPrice.Width = 120;
            // 
            // ColumnAmount
            // 
            this.ColumnAmount.DataPropertyName = "ColumnAmount";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnAmount.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColumnAmount.HeaderText = "Amount";
            this.ColumnAmount.MinimumWidth = 6;
            this.ColumnAmount.Name = "ColumnAmount";
            this.ColumnAmount.ReadOnly = true;
            this.ColumnAmount.Width = 120;
            // 
            // ColumnTax
            // 
            this.ColumnTax.DataPropertyName = "ColumnTax";
            this.ColumnTax.HeaderText = "Tax";
            this.ColumnTax.MinimumWidth = 6;
            this.ColumnTax.Name = "ColumnTax";
            this.ColumnTax.ReadOnly = true;
            this.ColumnTax.Width = 120;
            // 
            // ColumnTaxRate
            // 
            this.ColumnTaxRate.DataPropertyName = "ColumnTaxRate";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnTaxRate.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColumnTaxRate.HeaderText = "Tax Rate";
            this.ColumnTaxRate.MinimumWidth = 6;
            this.ColumnTaxRate.Name = "ColumnTaxRate";
            this.ColumnTaxRate.ReadOnly = true;
            this.ColumnTaxRate.Width = 120;
            // 
            // ColumnTaxAmount
            // 
            this.ColumnTaxAmount.DataPropertyName = "ColumnTaxAmount";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnTaxAmount.DefaultCellStyle = dataGridViewCellStyle6;
            this.ColumnTaxAmount.HeaderText = "Tax Amount";
            this.ColumnTaxAmount.MinimumWidth = 6;
            this.ColumnTaxAmount.Name = "ColumnTaxAmount";
            this.ColumnTaxAmount.ReadOnly = true;
            this.ColumnTaxAmount.Width = 120;
            // 
            // RepSalesDetailReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1370, 652);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "RepSalesDetailReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sales Detail Report";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSalesDetailReport)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dataGridViewSalesDetailReport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonGenerateCSV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBoxTotalAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonPageListFirst;
        private System.Windows.Forms.Button buttonPageListNext;
        private System.Windows.Forms.Button buttonPageListLast;
        private System.Windows.Forms.Button buttonPageListPrevious;
        private System.Windows.Forms.TextBox textBoxPageNumber;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogGenerateCSV;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTerminal;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSalesNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCustomerCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCustomer;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBarCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItemDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItemCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSupplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCostAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDiscountAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNetPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTax;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTaxRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTaxAmount;
    }
}