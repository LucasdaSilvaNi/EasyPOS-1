namespace EasyPOS.Forms.Software.TrnStockOut
{
    partial class TrnStockOutDetailForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnStockOutDetailForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonLock = new System.Windows.Forms.Button();
            this.buttonUnlock = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            this.dataGridViewStockOutLineList = new System.Windows.Forms.DataGridView();
            this.ColumnStockOutLineListButtonEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnStockOutLineListButtonDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnStockOutLineListId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockOutLineListStockOutId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockOutLineListItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockOutLineListItemBarcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockOutLineListItemDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockOutLineListUnitId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockOutLineListUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockOutLineListQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockOutLineListCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockOutLineListAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockOutLineListPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockOutLineListAssetAccountId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockOutLineListAssetAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxBarcode = new System.Windows.Forms.TextBox();
            this.buttonBarcode = new System.Windows.Forms.Button();
            this.buttonSearchItem = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.buttonStockOutLineListPageListFirst = new System.Windows.Forms.Button();
            this.buttonStockOutLineListPageListPrevious = new System.Windows.Forms.Button();
            this.buttonStockOutLineListPageListNext = new System.Windows.Forms.Button();
            this.buttonStockOutLineListPageListLast = new System.Windows.Forms.Button();
            this.textBoxStockOutLineListPageNumber = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxManualStockOutNumber = new System.Windows.Forms.TextBox();
            this.textBoxRemarks = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxApprovedBy = new System.Windows.Forms.ComboBox();
            this.comboBoxCheckedBy = new System.Windows.Forms.ComboBox();
            this.comboBoxPreparedBy = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxAccount = new System.Windows.Forms.ComboBox();
            this.dateTimePickerStockOutDate = new System.Windows.Forms.DateTimePicker();
            this.textBoxStockOutNumber = new System.Windows.Forms.TextBox();
            this.folderBrowserDialogGenerateCSV = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStockOutLineList)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.buttonLock);
            this.panel1.Controls.Add(this.buttonUnlock);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.buttonPrint);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1096, 50);
            this.panel1.TabIndex = 8;
            // 
            // buttonLock
            // 
            this.buttonLock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonLock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonLock.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonLock.FlatAppearance.BorderSize = 0;
            this.buttonLock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLock.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLock.ForeColor = System.Drawing.Color.White;
            this.buttonLock.Location = new System.Drawing.Point(790, 10);
            this.buttonLock.Margin = new System.Windows.Forms.Padding(2);
            this.buttonLock.Name = "buttonLock";
            this.buttonLock.Size = new System.Drawing.Size(70, 32);
            this.buttonLock.TabIndex = 20;
            this.buttonLock.TabStop = false;
            this.buttonLock.Text = "Lock";
            this.buttonLock.UseVisualStyleBackColor = false;
            this.buttonLock.Click += new System.EventHandler(this.buttonLock_Click);
            // 
            // buttonUnlock
            // 
            this.buttonUnlock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUnlock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonUnlock.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonUnlock.FlatAppearance.BorderSize = 0;
            this.buttonUnlock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUnlock.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUnlock.ForeColor = System.Drawing.Color.White;
            this.buttonUnlock.Location = new System.Drawing.Point(866, 10);
            this.buttonUnlock.Margin = new System.Windows.Forms.Padding(2);
            this.buttonUnlock.Name = "buttonUnlock";
            this.buttonUnlock.Size = new System.Drawing.Size(70, 32);
            this.buttonUnlock.TabIndex = 21;
            this.buttonUnlock.TabStop = false;
            this.buttonUnlock.Text = "Unlock";
            this.buttonUnlock.UseVisualStyleBackColor = false;
            this.buttonUnlock.Click += new System.EventHandler(this.buttonUnlock_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::EasyPOS.Properties.Resources.Stock_Out;
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
            this.label1.Size = new System.Drawing.Size(169, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "Stock-Out Detail";
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
            this.buttonClose.Location = new System.Drawing.Point(1016, 10);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(70, 32);
            this.buttonClose.TabIndex = 23;
            this.buttonClose.TabStop = false;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
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
            this.buttonPrint.Location = new System.Drawing.Point(941, 10);
            this.buttonPrint.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(70, 32);
            this.buttonPrint.TabIndex = 22;
            this.buttonPrint.TabStop = false;
            this.buttonPrint.Text = "Print";
            this.buttonPrint.UseVisualStyleBackColor = false;
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1096, 510);
            this.panel2.TabIndex = 9;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Control;
            this.panel5.Controls.Add(this.buttonExport);
            this.panel5.Controls.Add(this.buttonImport);
            this.panel5.Controls.Add(this.dataGridViewStockOutLineList);
            this.panel5.Controls.Add(this.textBoxBarcode);
            this.panel5.Controls.Add(this.buttonBarcode);
            this.panel5.Controls.Add(this.buttonSearchItem);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 195);
            this.panel5.Margin = new System.Windows.Forms.Padding(2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1096, 273);
            this.panel5.TabIndex = 28;
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(188)))), ((int)(((byte)(0)))));
            this.buttonExport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonExport.FlatAppearance.BorderSize = 0;
            this.buttonExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExport.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExport.ForeColor = System.Drawing.Color.White;
            this.buttonExport.Location = new System.Drawing.Point(867, 5);
            this.buttonExport.Margin = new System.Windows.Forms.Padding(2);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(70, 32);
            this.buttonExport.TabIndex = 26;
            this.buttonExport.TabStop = false;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = false;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonImport
            // 
            this.buttonImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonImport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(188)))), ((int)(((byte)(0)))));
            this.buttonImport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonImport.FlatAppearance.BorderSize = 0;
            this.buttonImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonImport.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonImport.ForeColor = System.Drawing.Color.White;
            this.buttonImport.Location = new System.Drawing.Point(793, 5);
            this.buttonImport.Margin = new System.Windows.Forms.Padding(2);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(70, 32);
            this.buttonImport.TabIndex = 25;
            this.buttonImport.TabStop = false;
            this.buttonImport.Text = "Import";
            this.buttonImport.UseVisualStyleBackColor = false;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // dataGridViewStockOutLineList
            // 
            this.dataGridViewStockOutLineList.AllowUserToAddRows = false;
            this.dataGridViewStockOutLineList.AllowUserToDeleteRows = false;
            this.dataGridViewStockOutLineList.AllowUserToResizeRows = false;
            this.dataGridViewStockOutLineList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewStockOutLineList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewStockOutLineList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewStockOutLineList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStockOutLineList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnStockOutLineListButtonEdit,
            this.ColumnStockOutLineListButtonDelete,
            this.ColumnStockOutLineListId,
            this.ColumnStockOutLineListStockOutId,
            this.ColumnStockOutLineListItemId,
            this.ColumnStockOutLineListItemBarcode,
            this.ColumnStockOutLineListItemDescription,
            this.ColumnStockOutLineListUnitId,
            this.ColumnStockOutLineListUnit,
            this.ColumnStockOutLineListQuantity,
            this.ColumnStockOutLineListCost,
            this.ColumnStockOutLineListAmount,
            this.ColumnStockOutLineListPrice,
            this.ColumnStockOutLineListAssetAccountId,
            this.ColumnStockOutLineListAssetAccount});
            this.dataGridViewStockOutLineList.Location = new System.Drawing.Point(10, 42);
            this.dataGridViewStockOutLineList.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewStockOutLineList.MultiSelect = false;
            this.dataGridViewStockOutLineList.Name = "dataGridViewStockOutLineList";
            this.dataGridViewStockOutLineList.ReadOnly = true;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.dataGridViewStockOutLineList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewStockOutLineList.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.dataGridViewStockOutLineList.RowTemplate.Height = 24;
            this.dataGridViewStockOutLineList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewStockOutLineList.Size = new System.Drawing.Size(1077, 227);
            this.dataGridViewStockOutLineList.TabIndex = 2;
            this.dataGridViewStockOutLineList.TabStop = false;
            this.dataGridViewStockOutLineList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewStockOutLineList_CellClick);
            // 
            // ColumnStockOutLineListButtonEdit
            // 
            this.ColumnStockOutLineListButtonEdit.DataPropertyName = "ColumnStockOutLineListButtonEdit";
            this.ColumnStockOutLineListButtonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColumnStockOutLineListButtonEdit.HeaderText = "";
            this.ColumnStockOutLineListButtonEdit.Name = "ColumnStockOutLineListButtonEdit";
            this.ColumnStockOutLineListButtonEdit.ReadOnly = true;
            this.ColumnStockOutLineListButtonEdit.Width = 70;
            // 
            // ColumnStockOutLineListButtonDelete
            // 
            this.ColumnStockOutLineListButtonDelete.DataPropertyName = "ColumnStockOutLineListButtonDelete";
            this.ColumnStockOutLineListButtonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColumnStockOutLineListButtonDelete.HeaderText = "";
            this.ColumnStockOutLineListButtonDelete.Name = "ColumnStockOutLineListButtonDelete";
            this.ColumnStockOutLineListButtonDelete.ReadOnly = true;
            this.ColumnStockOutLineListButtonDelete.Width = 70;
            // 
            // ColumnStockOutLineListId
            // 
            this.ColumnStockOutLineListId.DataPropertyName = "ColumnStockOutLineListId";
            this.ColumnStockOutLineListId.HeaderText = "Id";
            this.ColumnStockOutLineListId.Name = "ColumnStockOutLineListId";
            this.ColumnStockOutLineListId.ReadOnly = true;
            this.ColumnStockOutLineListId.Visible = false;
            // 
            // ColumnStockOutLineListStockOutId
            // 
            this.ColumnStockOutLineListStockOutId.DataPropertyName = "ColumnStockOutLineListStockOutId";
            this.ColumnStockOutLineListStockOutId.HeaderText = "Stock-Out Id";
            this.ColumnStockOutLineListStockOutId.Name = "ColumnStockOutLineListStockOutId";
            this.ColumnStockOutLineListStockOutId.ReadOnly = true;
            this.ColumnStockOutLineListStockOutId.Visible = false;
            // 
            // ColumnStockOutLineListItemId
            // 
            this.ColumnStockOutLineListItemId.DataPropertyName = "ColumnStockOutLineListItemId";
            this.ColumnStockOutLineListItemId.HeaderText = "Item Id";
            this.ColumnStockOutLineListItemId.Name = "ColumnStockOutLineListItemId";
            this.ColumnStockOutLineListItemId.ReadOnly = true;
            this.ColumnStockOutLineListItemId.Visible = false;
            // 
            // ColumnStockOutLineListItemBarcode
            // 
            this.ColumnStockOutLineListItemBarcode.DataPropertyName = "ColumnStockOutLineListItemBarcode";
            this.ColumnStockOutLineListItemBarcode.HeaderText = "Barcode";
            this.ColumnStockOutLineListItemBarcode.Name = "ColumnStockOutLineListItemBarcode";
            this.ColumnStockOutLineListItemBarcode.ReadOnly = true;
            this.ColumnStockOutLineListItemBarcode.Visible = false;
            // 
            // ColumnStockOutLineListItemDescription
            // 
            this.ColumnStockOutLineListItemDescription.DataPropertyName = "ColumnStockOutLineListItemDescription";
            this.ColumnStockOutLineListItemDescription.HeaderText = "Item Description";
            this.ColumnStockOutLineListItemDescription.Name = "ColumnStockOutLineListItemDescription";
            this.ColumnStockOutLineListItemDescription.ReadOnly = true;
            this.ColumnStockOutLineListItemDescription.Width = 200;
            // 
            // ColumnStockOutLineListUnitId
            // 
            this.ColumnStockOutLineListUnitId.DataPropertyName = "ColumnStockOutLineListUnitId";
            this.ColumnStockOutLineListUnitId.HeaderText = "Unit Id";
            this.ColumnStockOutLineListUnitId.Name = "ColumnStockOutLineListUnitId";
            this.ColumnStockOutLineListUnitId.ReadOnly = true;
            this.ColumnStockOutLineListUnitId.Visible = false;
            // 
            // ColumnStockOutLineListUnit
            // 
            this.ColumnStockOutLineListUnit.DataPropertyName = "ColumnStockOutLineListUnit";
            this.ColumnStockOutLineListUnit.HeaderText = "Unit";
            this.ColumnStockOutLineListUnit.Name = "ColumnStockOutLineListUnit";
            this.ColumnStockOutLineListUnit.ReadOnly = true;
            // 
            // ColumnStockOutLineListQuantity
            // 
            this.ColumnStockOutLineListQuantity.DataPropertyName = "ColumnStockOutLineListQuantity";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnStockOutLineListQuantity.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnStockOutLineListQuantity.HeaderText = "Quantity";
            this.ColumnStockOutLineListQuantity.Name = "ColumnStockOutLineListQuantity";
            this.ColumnStockOutLineListQuantity.ReadOnly = true;
            // 
            // ColumnStockOutLineListCost
            // 
            this.ColumnStockOutLineListCost.DataPropertyName = "ColumnStockOutLineListCost";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnStockOutLineListCost.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnStockOutLineListCost.HeaderText = "Cost";
            this.ColumnStockOutLineListCost.Name = "ColumnStockOutLineListCost";
            this.ColumnStockOutLineListCost.ReadOnly = true;
            // 
            // ColumnStockOutLineListAmount
            // 
            this.ColumnStockOutLineListAmount.DataPropertyName = "ColumnStockOutLineListAmount";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnStockOutLineListAmount.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColumnStockOutLineListAmount.HeaderText = "Amount";
            this.ColumnStockOutLineListAmount.Name = "ColumnStockOutLineListAmount";
            this.ColumnStockOutLineListAmount.ReadOnly = true;
            // 
            // ColumnStockOutLineListPrice
            // 
            this.ColumnStockOutLineListPrice.DataPropertyName = "ColumnStockOutLineListPrice";
            this.ColumnStockOutLineListPrice.HeaderText = "Price";
            this.ColumnStockOutLineListPrice.Name = "ColumnStockOutLineListPrice";
            this.ColumnStockOutLineListPrice.ReadOnly = true;
            // 
            // ColumnStockOutLineListAssetAccountId
            // 
            this.ColumnStockOutLineListAssetAccountId.DataPropertyName = "ColumnStockOutLineListAssetAccountId";
            this.ColumnStockOutLineListAssetAccountId.HeaderText = "Asset Account Id";
            this.ColumnStockOutLineListAssetAccountId.Name = "ColumnStockOutLineListAssetAccountId";
            this.ColumnStockOutLineListAssetAccountId.ReadOnly = true;
            this.ColumnStockOutLineListAssetAccountId.Visible = false;
            // 
            // ColumnStockOutLineListAssetAccount
            // 
            this.ColumnStockOutLineListAssetAccount.DataPropertyName = "ColumnStockOutLineListAssetAccount";
            this.ColumnStockOutLineListAssetAccount.HeaderText = "Asset Account";
            this.ColumnStockOutLineListAssetAccount.Name = "ColumnStockOutLineListAssetAccount";
            this.ColumnStockOutLineListAssetAccount.ReadOnly = true;
            this.ColumnStockOutLineListAssetAccount.Visible = false;
            // 
            // textBoxBarcode
            // 
            this.textBoxBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBarcode.Font = new System.Drawing.Font("Segoe UI Semibold", 13.3F, System.Drawing.FontStyle.Bold);
            this.textBoxBarcode.Location = new System.Drawing.Point(195, 6);
            this.textBoxBarcode.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxBarcode.Name = "textBoxBarcode";
            this.textBoxBarcode.Size = new System.Drawing.Size(587, 31);
            this.textBoxBarcode.TabIndex = 8;
            this.textBoxBarcode.TabStop = false;
            this.textBoxBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxBarcode_KeyDown);
            // 
            // buttonBarcode
            // 
            this.buttonBarcode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(188)))), ((int)(((byte)(0)))));
            this.buttonBarcode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(34)))), ((int)(((byte)(116)))));
            this.buttonBarcode.FlatAppearance.BorderSize = 0;
            this.buttonBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBarcode.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.buttonBarcode.ForeColor = System.Drawing.Color.White;
            this.buttonBarcode.Location = new System.Drawing.Point(10, 5);
            this.buttonBarcode.Margin = new System.Windows.Forms.Padding(2);
            this.buttonBarcode.Name = "buttonBarcode";
            this.buttonBarcode.Size = new System.Drawing.Size(181, 32);
            this.buttonBarcode.TabIndex = 9;
            this.buttonBarcode.TabStop = false;
            this.buttonBarcode.Text = "Barcode";
            this.buttonBarcode.UseVisualStyleBackColor = false;
            this.buttonBarcode.Click += new System.EventHandler(this.buttonBarcode_Click);
            // 
            // buttonSearchItem
            // 
            this.buttonSearchItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSearchItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(188)))), ((int)(((byte)(0)))));
            this.buttonSearchItem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(34)))), ((int)(((byte)(116)))));
            this.buttonSearchItem.FlatAppearance.BorderSize = 0;
            this.buttonSearchItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearchItem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.buttonSearchItem.ForeColor = System.Drawing.Color.White;
            this.buttonSearchItem.Location = new System.Drawing.Point(941, 5);
            this.buttonSearchItem.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSearchItem.Name = "buttonSearchItem";
            this.buttonSearchItem.Size = new System.Drawing.Size(146, 32);
            this.buttonSearchItem.TabIndex = 8;
            this.buttonSearchItem.TabStop = false;
            this.buttonSearchItem.Text = "Search Item";
            this.buttonSearchItem.UseVisualStyleBackColor = false;
            this.buttonSearchItem.Click += new System.EventHandler(this.buttonSearchItem_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.buttonStockOutLineListPageListFirst);
            this.panel4.Controls.Add(this.buttonStockOutLineListPageListPrevious);
            this.panel4.Controls.Add(this.buttonStockOutLineListPageListNext);
            this.panel4.Controls.Add(this.buttonStockOutLineListPageListLast);
            this.panel4.Controls.Add(this.textBoxStockOutLineListPageNumber);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 468);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1096, 42);
            this.panel4.TabIndex = 25;
            // 
            // buttonStockOutLineListPageListFirst
            // 
            this.buttonStockOutLineListPageListFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStockOutLineListPageListFirst.Enabled = false;
            this.buttonStockOutLineListPageListFirst.FlatAppearance.BorderSize = 0;
            this.buttonStockOutLineListPageListFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStockOutLineListPageListFirst.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonStockOutLineListPageListFirst.Location = new System.Drawing.Point(10, 9);
            this.buttonStockOutLineListPageListFirst.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStockOutLineListPageListFirst.Name = "buttonStockOutLineListPageListFirst";
            this.buttonStockOutLineListPageListFirst.Size = new System.Drawing.Size(66, 26);
            this.buttonStockOutLineListPageListFirst.TabIndex = 13;
            this.buttonStockOutLineListPageListFirst.TabStop = false;
            this.buttonStockOutLineListPageListFirst.Text = "First";
            this.buttonStockOutLineListPageListFirst.UseVisualStyleBackColor = false;
            this.buttonStockOutLineListPageListFirst.Click += new System.EventHandler(this.buttonStockOutLineListPageListFirst_Click);
            // 
            // buttonStockOutLineListPageListPrevious
            // 
            this.buttonStockOutLineListPageListPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStockOutLineListPageListPrevious.Enabled = false;
            this.buttonStockOutLineListPageListPrevious.FlatAppearance.BorderSize = 0;
            this.buttonStockOutLineListPageListPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStockOutLineListPageListPrevious.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonStockOutLineListPageListPrevious.Location = new System.Drawing.Point(80, 9);
            this.buttonStockOutLineListPageListPrevious.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStockOutLineListPageListPrevious.Name = "buttonStockOutLineListPageListPrevious";
            this.buttonStockOutLineListPageListPrevious.Size = new System.Drawing.Size(66, 26);
            this.buttonStockOutLineListPageListPrevious.TabIndex = 14;
            this.buttonStockOutLineListPageListPrevious.TabStop = false;
            this.buttonStockOutLineListPageListPrevious.Text = "Previous";
            this.buttonStockOutLineListPageListPrevious.UseVisualStyleBackColor = false;
            this.buttonStockOutLineListPageListPrevious.Click += new System.EventHandler(this.buttonStockOutLineListPageListPrevious_Click);
            // 
            // buttonStockOutLineListPageListNext
            // 
            this.buttonStockOutLineListPageListNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStockOutLineListPageListNext.FlatAppearance.BorderSize = 0;
            this.buttonStockOutLineListPageListNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStockOutLineListPageListNext.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonStockOutLineListPageListNext.Location = new System.Drawing.Point(210, 9);
            this.buttonStockOutLineListPageListNext.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStockOutLineListPageListNext.Name = "buttonStockOutLineListPageListNext";
            this.buttonStockOutLineListPageListNext.Size = new System.Drawing.Size(66, 26);
            this.buttonStockOutLineListPageListNext.TabIndex = 15;
            this.buttonStockOutLineListPageListNext.TabStop = false;
            this.buttonStockOutLineListPageListNext.Text = "Next";
            this.buttonStockOutLineListPageListNext.UseVisualStyleBackColor = false;
            this.buttonStockOutLineListPageListNext.Click += new System.EventHandler(this.buttonStockOutLineListPageListNext_Click);
            // 
            // buttonStockOutLineListPageListLast
            // 
            this.buttonStockOutLineListPageListLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStockOutLineListPageListLast.FlatAppearance.BorderSize = 0;
            this.buttonStockOutLineListPageListLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStockOutLineListPageListLast.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonStockOutLineListPageListLast.Location = new System.Drawing.Point(278, 9);
            this.buttonStockOutLineListPageListLast.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStockOutLineListPageListLast.Name = "buttonStockOutLineListPageListLast";
            this.buttonStockOutLineListPageListLast.Size = new System.Drawing.Size(66, 26);
            this.buttonStockOutLineListPageListLast.TabIndex = 16;
            this.buttonStockOutLineListPageListLast.TabStop = false;
            this.buttonStockOutLineListPageListLast.Text = "Last";
            this.buttonStockOutLineListPageListLast.UseVisualStyleBackColor = false;
            this.buttonStockOutLineListPageListLast.Click += new System.EventHandler(this.buttonStockOutLineListPageListLast_Click);
            // 
            // textBoxStockOutLineListPageNumber
            // 
            this.textBoxStockOutLineListPageNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxStockOutLineListPageNumber.BackColor = System.Drawing.Color.White;
            this.textBoxStockOutLineListPageNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxStockOutLineListPageNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxStockOutLineListPageNumber.Location = new System.Drawing.Point(150, 13);
            this.textBoxStockOutLineListPageNumber.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxStockOutLineListPageNumber.Name = "textBoxStockOutLineListPageNumber";
            this.textBoxStockOutLineListPageNumber.ReadOnly = true;
            this.textBoxStockOutLineListPageNumber.Size = new System.Drawing.Size(55, 16);
            this.textBoxStockOutLineListPageNumber.TabIndex = 17;
            this.textBoxStockOutLineListPageNumber.TabStop = false;
            this.textBoxStockOutLineListPageNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.textBoxManualStockOutNumber);
            this.panel3.Controls.Add(this.textBoxRemarks);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.comboBoxApprovedBy);
            this.panel3.Controls.Add(this.comboBoxCheckedBy);
            this.panel3.Controls.Add(this.comboBoxPreparedBy);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.comboBoxAccount);
            this.panel3.Controls.Add(this.dateTimePickerStockOutDate);
            this.panel3.Controls.Add(this.textBoxStockOutNumber);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(8);
            this.panel3.Size = new System.Drawing.Size(1096, 195);
            this.panel3.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label3.Location = new System.Drawing.Point(6, 100);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 19);
            this.label3.TabIndex = 25;
            this.label3.Text = "Manual Stock-Out Number:";
            // 
            // textBoxManualStockOutNumber
            // 
            this.textBoxManualStockOutNumber.Enabled = false;
            this.textBoxManualStockOutNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.textBoxManualStockOutNumber.Location = new System.Drawing.Point(195, 97);
            this.textBoxManualStockOutNumber.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxManualStockOutNumber.Name = "textBoxManualStockOutNumber";
            this.textBoxManualStockOutNumber.Size = new System.Drawing.Size(239, 26);
            this.textBoxManualStockOutNumber.TabIndex = 3;
            // 
            // textBoxRemarks
            // 
            this.textBoxRemarks.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.textBoxRemarks.Location = new System.Drawing.Point(195, 127);
            this.textBoxRemarks.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxRemarks.Multiline = true;
            this.textBoxRemarks.Name = "textBoxRemarks";
            this.textBoxRemarks.Size = new System.Drawing.Size(239, 54);
            this.textBoxRemarks.TabIndex = 4;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label13.Location = new System.Drawing.Point(456, 74);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(91, 19);
            this.label13.TabIndex = 23;
            this.label13.Text = "Approved by:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label12.Location = new System.Drawing.Point(456, 43);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 19);
            this.label12.TabIndex = 22;
            this.label12.Text = "Checked by:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label11.Location = new System.Drawing.Point(456, 11);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 19);
            this.label11.TabIndex = 21;
            this.label11.Text = "Prepared by:";
            // 
            // comboBoxApprovedBy
            // 
            this.comboBoxApprovedBy.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.comboBoxApprovedBy.FormattingEnabled = true;
            this.comboBoxApprovedBy.Location = new System.Drawing.Point(559, 68);
            this.comboBoxApprovedBy.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxApprovedBy.Name = "comboBoxApprovedBy";
            this.comboBoxApprovedBy.Size = new System.Drawing.Size(231, 27);
            this.comboBoxApprovedBy.TabIndex = 7;
            // 
            // comboBoxCheckedBy
            // 
            this.comboBoxCheckedBy.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.comboBoxCheckedBy.FormattingEnabled = true;
            this.comboBoxCheckedBy.Location = new System.Drawing.Point(559, 38);
            this.comboBoxCheckedBy.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxCheckedBy.Name = "comboBoxCheckedBy";
            this.comboBoxCheckedBy.Size = new System.Drawing.Size(231, 27);
            this.comboBoxCheckedBy.TabIndex = 6;
            // 
            // comboBoxPreparedBy
            // 
            this.comboBoxPreparedBy.Enabled = false;
            this.comboBoxPreparedBy.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.comboBoxPreparedBy.FormattingEnabled = true;
            this.comboBoxPreparedBy.Location = new System.Drawing.Point(559, 9);
            this.comboBoxPreparedBy.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxPreparedBy.Name = "comboBoxPreparedBy";
            this.comboBoxPreparedBy.Size = new System.Drawing.Size(231, 27);
            this.comboBoxPreparedBy.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label7.Location = new System.Drawing.Point(17, 130);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 19);
            this.label7.TabIndex = 11;
            this.label7.Text = "Remarks:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label5.Location = new System.Drawing.Point(17, 70);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 19);
            this.label5.TabIndex = 9;
            this.label5.Text = "Account:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label4.Location = new System.Drawing.Point(17, 41);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "Stock-Out Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label2.Location = new System.Drawing.Point(17, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "Stock-Out Number:";
            // 
            // comboBoxAccount
            // 
            this.comboBoxAccount.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.comboBoxAccount.FormattingEnabled = true;
            this.comboBoxAccount.Location = new System.Drawing.Point(195, 66);
            this.comboBoxAccount.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxAccount.Name = "comboBoxAccount";
            this.comboBoxAccount.Size = new System.Drawing.Size(239, 27);
            this.comboBoxAccount.TabIndex = 2;
            // 
            // dateTimePickerStockOutDate
            // 
            this.dateTimePickerStockOutDate.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.dateTimePickerStockOutDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerStockOutDate.Location = new System.Drawing.Point(195, 37);
            this.dateTimePickerStockOutDate.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePickerStockOutDate.Name = "dateTimePickerStockOutDate";
            this.dateTimePickerStockOutDate.Size = new System.Drawing.Size(158, 26);
            this.dateTimePickerStockOutDate.TabIndex = 1;
            // 
            // textBoxStockOutNumber
            // 
            this.textBoxStockOutNumber.Enabled = false;
            this.textBoxStockOutNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.textBoxStockOutNumber.Location = new System.Drawing.Point(195, 9);
            this.textBoxStockOutNumber.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxStockOutNumber.Name = "textBoxStockOutNumber";
            this.textBoxStockOutNumber.Size = new System.Drawing.Size(158, 26);
            this.textBoxStockOutNumber.TabIndex = 0;
            // 
            // TrnStockOutDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1096, 560);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "TrnStockOutDetailForm";
            this.Text = "TrnStockOutDetail";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStockOutLineList)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonLock;
        private System.Windows.Forms.Button buttonUnlock;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button buttonStockOutLineListPageListFirst;
        private System.Windows.Forms.Button buttonStockOutLineListPageListPrevious;
        private System.Windows.Forms.Button buttonStockOutLineListPageListNext;
        private System.Windows.Forms.Button buttonStockOutLineListPageListLast;
        private System.Windows.Forms.TextBox textBoxStockOutLineListPageNumber;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBoxRemarks;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBoxApprovedBy;
        private System.Windows.Forms.ComboBox comboBoxCheckedBy;
        private System.Windows.Forms.ComboBox comboBoxPreparedBy;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxAccount;
        private System.Windows.Forms.DateTimePicker dateTimePickerStockOutDate;
        private System.Windows.Forms.TextBox textBoxStockOutNumber;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox textBoxBarcode;
        private System.Windows.Forms.Button buttonBarcode;
        private System.Windows.Forms.Button buttonSearchItem;
        private System.Windows.Forms.DataGridView dataGridViewStockOutLineList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxManualStockOutNumber;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogGenerateCSV;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnStockOutLineListButtonEdit;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnStockOutLineListButtonDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockOutLineListId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockOutLineListStockOutId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockOutLineListItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockOutLineListItemBarcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockOutLineListItemDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockOutLineListUnitId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockOutLineListUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockOutLineListQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockOutLineListCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockOutLineListAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockOutLineListPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockOutLineListAssetAccountId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockOutLineListAssetAccount;
    }
}