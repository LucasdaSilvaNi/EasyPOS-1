namespace EasyPOS.Forms.Software.TrnStockCount
{
    partial class TrnStockCountDetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnStockCountDetailForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonPost = new System.Windows.Forms.Button();
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
            this.dataGridViewStockCountLineList = new System.Windows.Forms.DataGridView();
            this.ColumnStockCountLineListButtonEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnStockCountLineListButtonDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnStockCountLineListId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockCountLineListStockCountId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockCountLineListItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockCountLineListItemBarcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockCountLineListItemDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockCountLineListUnitId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockCountLineListUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockCountLineListQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockCountLineListCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockCountLineListAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxBarcode = new System.Windows.Forms.TextBox();
            this.buttonBarcode = new System.Windows.Forms.Button();
            this.buttonSearchItem = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.buttonStockCountLineListPageListFirst = new System.Windows.Forms.Button();
            this.buttonStockCountLineListPageListPrevious = new System.Windows.Forms.Button();
            this.buttonStockCountLineListPageListNext = new System.Windows.Forms.Button();
            this.buttonStockCountLineListPageListLast = new System.Windows.Forms.Button();
            this.textBoxStockCountLineListPageNumber = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBoxRemarks = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxApprovedBy = new System.Windows.Forms.ComboBox();
            this.comboBoxCheckedBy = new System.Windows.Forms.ComboBox();
            this.comboBoxPreparedBy = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePickerStockCountDate = new System.Windows.Forms.DateTimePicker();
            this.textBoxStockCountNumber = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialogGenerateCSV = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStockCountLineList)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.buttonPost);
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
            // buttonPost
            // 
            this.buttonPost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonPost.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonPost.FlatAppearance.BorderSize = 0;
            this.buttonPost.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPost.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPost.ForeColor = System.Drawing.Color.White;
            this.buttonPost.Location = new System.Drawing.Point(715, 10);
            this.buttonPost.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPost.Name = "buttonPost";
            this.buttonPost.Size = new System.Drawing.Size(70, 32);
            this.buttonPost.TabIndex = 24;
            this.buttonPost.TabStop = false;
            this.buttonPost.Text = "Post";
            this.buttonPost.UseVisualStyleBackColor = false;
            this.buttonPost.Click += new System.EventHandler(this.buttonPost_Click);
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
            this.pictureBox1.Image = global::EasyPOS.Properties.Resources.Stock_Count;
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
            this.label1.Size = new System.Drawing.Size(190, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "Stock-Count Detail";
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
            this.panel5.Controls.Add(this.dataGridViewStockCountLineList);
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
            // dataGridViewStockCountLineList
            // 
            this.dataGridViewStockCountLineList.AllowUserToAddRows = false;
            this.dataGridViewStockCountLineList.AllowUserToDeleteRows = false;
            this.dataGridViewStockCountLineList.AllowUserToResizeRows = false;
            this.dataGridViewStockCountLineList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewStockCountLineList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewStockCountLineList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewStockCountLineList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStockCountLineList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnStockCountLineListButtonEdit,
            this.ColumnStockCountLineListButtonDelete,
            this.ColumnStockCountLineListId,
            this.ColumnStockCountLineListStockCountId,
            this.ColumnStockCountLineListItemId,
            this.ColumnStockCountLineListItemBarcode,
            this.ColumnStockCountLineListItemDescription,
            this.ColumnStockCountLineListUnitId,
            this.ColumnStockCountLineListUnit,
            this.ColumnStockCountLineListQuantity,
            this.ColumnStockCountLineListCost,
            this.ColumnStockCountLineListAmount});
            this.dataGridViewStockCountLineList.Location = new System.Drawing.Point(10, 42);
            this.dataGridViewStockCountLineList.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewStockCountLineList.MultiSelect = false;
            this.dataGridViewStockCountLineList.Name = "dataGridViewStockCountLineList";
            this.dataGridViewStockCountLineList.ReadOnly = true;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.dataGridViewStockCountLineList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewStockCountLineList.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.dataGridViewStockCountLineList.RowTemplate.Height = 24;
            this.dataGridViewStockCountLineList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewStockCountLineList.Size = new System.Drawing.Size(1077, 227);
            this.dataGridViewStockCountLineList.TabIndex = 2;
            this.dataGridViewStockCountLineList.TabStop = false;
            this.dataGridViewStockCountLineList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewStockCountLineList_CellClick);
            this.dataGridViewStockCountLineList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewStockCountLineList_CellContentClick);
            // 
            // ColumnStockCountLineListButtonEdit
            // 
            this.ColumnStockCountLineListButtonEdit.DataPropertyName = "ColumnStockCountLineListButtonEdit";
            this.ColumnStockCountLineListButtonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColumnStockCountLineListButtonEdit.HeaderText = "";
            this.ColumnStockCountLineListButtonEdit.Name = "ColumnStockCountLineListButtonEdit";
            this.ColumnStockCountLineListButtonEdit.ReadOnly = true;
            this.ColumnStockCountLineListButtonEdit.Width = 70;
            // 
            // ColumnStockCountLineListButtonDelete
            // 
            this.ColumnStockCountLineListButtonDelete.DataPropertyName = "ColumnStockCountLineListButtonDelete";
            this.ColumnStockCountLineListButtonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColumnStockCountLineListButtonDelete.HeaderText = "";
            this.ColumnStockCountLineListButtonDelete.Name = "ColumnStockCountLineListButtonDelete";
            this.ColumnStockCountLineListButtonDelete.ReadOnly = true;
            this.ColumnStockCountLineListButtonDelete.Width = 70;
            // 
            // ColumnStockCountLineListId
            // 
            this.ColumnStockCountLineListId.DataPropertyName = "ColumnStockCountLineListId";
            this.ColumnStockCountLineListId.HeaderText = "Id";
            this.ColumnStockCountLineListId.Name = "ColumnStockCountLineListId";
            this.ColumnStockCountLineListId.ReadOnly = true;
            this.ColumnStockCountLineListId.Visible = false;
            // 
            // ColumnStockCountLineListStockCountId
            // 
            this.ColumnStockCountLineListStockCountId.DataPropertyName = "ColumnStockCountLineListStockCountId";
            this.ColumnStockCountLineListStockCountId.HeaderText = "Stock Count Id";
            this.ColumnStockCountLineListStockCountId.Name = "ColumnStockCountLineListStockCountId";
            this.ColumnStockCountLineListStockCountId.ReadOnly = true;
            this.ColumnStockCountLineListStockCountId.Visible = false;
            // 
            // ColumnStockCountLineListItemId
            // 
            this.ColumnStockCountLineListItemId.DataPropertyName = "ColumnStockCountLineListItemId";
            this.ColumnStockCountLineListItemId.HeaderText = "Item Id";
            this.ColumnStockCountLineListItemId.Name = "ColumnStockCountLineListItemId";
            this.ColumnStockCountLineListItemId.ReadOnly = true;
            this.ColumnStockCountLineListItemId.Visible = false;
            // 
            // ColumnStockCountLineListItemBarcode
            // 
            this.ColumnStockCountLineListItemBarcode.DataPropertyName = "ColumnStockCountLineListItemBarcode";
            this.ColumnStockCountLineListItemBarcode.HeaderText = "Barcode";
            this.ColumnStockCountLineListItemBarcode.Name = "ColumnStockCountLineListItemBarcode";
            this.ColumnStockCountLineListItemBarcode.ReadOnly = true;
            this.ColumnStockCountLineListItemBarcode.Visible = false;
            // 
            // ColumnStockCountLineListItemDescription
            // 
            this.ColumnStockCountLineListItemDescription.DataPropertyName = "ColumnStockCountLineListItemDescription";
            this.ColumnStockCountLineListItemDescription.HeaderText = "Item Description";
            this.ColumnStockCountLineListItemDescription.Name = "ColumnStockCountLineListItemDescription";
            this.ColumnStockCountLineListItemDescription.ReadOnly = true;
            this.ColumnStockCountLineListItemDescription.Width = 200;
            // 
            // ColumnStockCountLineListUnitId
            // 
            this.ColumnStockCountLineListUnitId.DataPropertyName = "ColumnStockCountLineListUnitId";
            this.ColumnStockCountLineListUnitId.HeaderText = "Unit Id";
            this.ColumnStockCountLineListUnitId.Name = "ColumnStockCountLineListUnitId";
            this.ColumnStockCountLineListUnitId.ReadOnly = true;
            this.ColumnStockCountLineListUnitId.Visible = false;
            // 
            // ColumnStockCountLineListUnit
            // 
            this.ColumnStockCountLineListUnit.DataPropertyName = "ColumnStockCountLineListUnit";
            this.ColumnStockCountLineListUnit.HeaderText = "Unit";
            this.ColumnStockCountLineListUnit.Name = "ColumnStockCountLineListUnit";
            this.ColumnStockCountLineListUnit.ReadOnly = true;
            // 
            // ColumnStockCountLineListQuantity
            // 
            this.ColumnStockCountLineListQuantity.DataPropertyName = "ColumnStockCountLineListQuantity";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnStockCountLineListQuantity.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnStockCountLineListQuantity.HeaderText = "Quantity";
            this.ColumnStockCountLineListQuantity.Name = "ColumnStockCountLineListQuantity";
            this.ColumnStockCountLineListQuantity.ReadOnly = true;
            // 
            // ColumnStockCountLineListCost
            // 
            this.ColumnStockCountLineListCost.DataPropertyName = "ColumnStockCountLineListCost";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnStockCountLineListCost.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnStockCountLineListCost.HeaderText = "Cost";
            this.ColumnStockCountLineListCost.Name = "ColumnStockCountLineListCost";
            this.ColumnStockCountLineListCost.ReadOnly = true;
            // 
            // ColumnStockCountLineListAmount
            // 
            this.ColumnStockCountLineListAmount.DataPropertyName = "ColumnStockCountLineListAmount";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnStockCountLineListAmount.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColumnStockCountLineListAmount.HeaderText = "Amount";
            this.ColumnStockCountLineListAmount.Name = "ColumnStockCountLineListAmount";
            this.ColumnStockCountLineListAmount.ReadOnly = true;
            // 
            // textBoxBarcode
            // 
            this.textBoxBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBarcode.Font = new System.Drawing.Font("Segoe UI Semibold", 13.3F, System.Drawing.FontStyle.Bold);
            this.textBoxBarcode.Location = new System.Drawing.Point(157, 6);
            this.textBoxBarcode.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxBarcode.Name = "textBoxBarcode";
            this.textBoxBarcode.Size = new System.Drawing.Size(632, 31);
            this.textBoxBarcode.TabIndex = 10;
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
            this.buttonBarcode.Size = new System.Drawing.Size(143, 32);
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
            this.panel4.Controls.Add(this.buttonStockCountLineListPageListFirst);
            this.panel4.Controls.Add(this.buttonStockCountLineListPageListPrevious);
            this.panel4.Controls.Add(this.buttonStockCountLineListPageListNext);
            this.panel4.Controls.Add(this.buttonStockCountLineListPageListLast);
            this.panel4.Controls.Add(this.textBoxStockCountLineListPageNumber);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 468);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1096, 42);
            this.panel4.TabIndex = 25;
            // 
            // buttonStockCountLineListPageListFirst
            // 
            this.buttonStockCountLineListPageListFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStockCountLineListPageListFirst.Enabled = false;
            this.buttonStockCountLineListPageListFirst.FlatAppearance.BorderSize = 0;
            this.buttonStockCountLineListPageListFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStockCountLineListPageListFirst.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonStockCountLineListPageListFirst.Location = new System.Drawing.Point(10, 9);
            this.buttonStockCountLineListPageListFirst.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStockCountLineListPageListFirst.Name = "buttonStockCountLineListPageListFirst";
            this.buttonStockCountLineListPageListFirst.Size = new System.Drawing.Size(66, 26);
            this.buttonStockCountLineListPageListFirst.TabIndex = 13;
            this.buttonStockCountLineListPageListFirst.TabStop = false;
            this.buttonStockCountLineListPageListFirst.Text = "First";
            this.buttonStockCountLineListPageListFirst.UseVisualStyleBackColor = false;
            this.buttonStockCountLineListPageListFirst.Click += new System.EventHandler(this.buttonStockCountLineListPageListFirst_Click);
            // 
            // buttonStockCountLineListPageListPrevious
            // 
            this.buttonStockCountLineListPageListPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStockCountLineListPageListPrevious.Enabled = false;
            this.buttonStockCountLineListPageListPrevious.FlatAppearance.BorderSize = 0;
            this.buttonStockCountLineListPageListPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStockCountLineListPageListPrevious.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonStockCountLineListPageListPrevious.Location = new System.Drawing.Point(80, 9);
            this.buttonStockCountLineListPageListPrevious.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStockCountLineListPageListPrevious.Name = "buttonStockCountLineListPageListPrevious";
            this.buttonStockCountLineListPageListPrevious.Size = new System.Drawing.Size(66, 26);
            this.buttonStockCountLineListPageListPrevious.TabIndex = 14;
            this.buttonStockCountLineListPageListPrevious.TabStop = false;
            this.buttonStockCountLineListPageListPrevious.Text = "Previous";
            this.buttonStockCountLineListPageListPrevious.UseVisualStyleBackColor = false;
            this.buttonStockCountLineListPageListPrevious.Click += new System.EventHandler(this.buttonStockCountLineListPageListPrevious_Click);
            // 
            // buttonStockCountLineListPageListNext
            // 
            this.buttonStockCountLineListPageListNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStockCountLineListPageListNext.FlatAppearance.BorderSize = 0;
            this.buttonStockCountLineListPageListNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStockCountLineListPageListNext.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonStockCountLineListPageListNext.Location = new System.Drawing.Point(210, 9);
            this.buttonStockCountLineListPageListNext.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStockCountLineListPageListNext.Name = "buttonStockCountLineListPageListNext";
            this.buttonStockCountLineListPageListNext.Size = new System.Drawing.Size(66, 26);
            this.buttonStockCountLineListPageListNext.TabIndex = 15;
            this.buttonStockCountLineListPageListNext.TabStop = false;
            this.buttonStockCountLineListPageListNext.Text = "Next";
            this.buttonStockCountLineListPageListNext.UseVisualStyleBackColor = false;
            this.buttonStockCountLineListPageListNext.Click += new System.EventHandler(this.buttonStockCountLineListPageListNext_Click);
            // 
            // buttonStockCountLineListPageListLast
            // 
            this.buttonStockCountLineListPageListLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStockCountLineListPageListLast.FlatAppearance.BorderSize = 0;
            this.buttonStockCountLineListPageListLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStockCountLineListPageListLast.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonStockCountLineListPageListLast.Location = new System.Drawing.Point(278, 9);
            this.buttonStockCountLineListPageListLast.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStockCountLineListPageListLast.Name = "buttonStockCountLineListPageListLast";
            this.buttonStockCountLineListPageListLast.Size = new System.Drawing.Size(66, 26);
            this.buttonStockCountLineListPageListLast.TabIndex = 16;
            this.buttonStockCountLineListPageListLast.TabStop = false;
            this.buttonStockCountLineListPageListLast.Text = "Last";
            this.buttonStockCountLineListPageListLast.UseVisualStyleBackColor = false;
            this.buttonStockCountLineListPageListLast.Click += new System.EventHandler(this.buttonStockCountLineListPageListLast_Click);
            // 
            // textBoxStockCountLineListPageNumber
            // 
            this.textBoxStockCountLineListPageNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxStockCountLineListPageNumber.BackColor = System.Drawing.Color.White;
            this.textBoxStockCountLineListPageNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxStockCountLineListPageNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxStockCountLineListPageNumber.Location = new System.Drawing.Point(150, 13);
            this.textBoxStockCountLineListPageNumber.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxStockCountLineListPageNumber.Name = "textBoxStockCountLineListPageNumber";
            this.textBoxStockCountLineListPageNumber.ReadOnly = true;
            this.textBoxStockCountLineListPageNumber.Size = new System.Drawing.Size(55, 16);
            this.textBoxStockCountLineListPageNumber.TabIndex = 17;
            this.textBoxStockCountLineListPageNumber.TabStop = false;
            this.textBoxStockCountLineListPageNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel3.Controls.Add(this.textBoxRemarks);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label12);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.comboBoxApprovedBy);
            this.panel3.Controls.Add(this.comboBoxCheckedBy);
            this.panel3.Controls.Add(this.comboBoxPreparedBy);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.dateTimePickerStockCountDate);
            this.panel3.Controls.Add(this.textBoxStockCountNumber);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(8);
            this.panel3.Size = new System.Drawing.Size(1096, 195);
            this.panel3.TabIndex = 0;
            // 
            // textBoxRemarks
            // 
            this.textBoxRemarks.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.textBoxRemarks.Location = new System.Drawing.Point(157, 66);
            this.textBoxRemarks.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxRemarks.Multiline = true;
            this.textBoxRemarks.Name = "textBoxRemarks";
            this.textBoxRemarks.Size = new System.Drawing.Size(239, 114);
            this.textBoxRemarks.TabIndex = 2;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label13.Location = new System.Drawing.Point(414, 69);
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
            this.label12.Location = new System.Drawing.Point(414, 41);
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
            this.label11.Location = new System.Drawing.Point(414, 11);
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
            this.comboBoxApprovedBy.Location = new System.Drawing.Point(518, 68);
            this.comboBoxApprovedBy.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxApprovedBy.Name = "comboBoxApprovedBy";
            this.comboBoxApprovedBy.Size = new System.Drawing.Size(231, 27);
            this.comboBoxApprovedBy.TabIndex = 5;
            // 
            // comboBoxCheckedBy
            // 
            this.comboBoxCheckedBy.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.comboBoxCheckedBy.FormattingEnabled = true;
            this.comboBoxCheckedBy.Location = new System.Drawing.Point(518, 38);
            this.comboBoxCheckedBy.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxCheckedBy.Name = "comboBoxCheckedBy";
            this.comboBoxCheckedBy.Size = new System.Drawing.Size(231, 27);
            this.comboBoxCheckedBy.TabIndex = 4;
            // 
            // comboBoxPreparedBy
            // 
            this.comboBoxPreparedBy.Enabled = false;
            this.comboBoxPreparedBy.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.comboBoxPreparedBy.FormattingEnabled = true;
            this.comboBoxPreparedBy.Location = new System.Drawing.Point(518, 9);
            this.comboBoxPreparedBy.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxPreparedBy.Name = "comboBoxPreparedBy";
            this.comboBoxPreparedBy.Size = new System.Drawing.Size(231, 27);
            this.comboBoxPreparedBy.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label7.Location = new System.Drawing.Point(12, 66);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 19);
            this.label7.TabIndex = 11;
            this.label7.Text = "Remarks:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label4.Location = new System.Drawing.Point(11, 37);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "Stock-Count Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label2.Location = new System.Drawing.Point(11, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "Stock-Count Number:";
            // 
            // dateTimePickerStockCountDate
            // 
            this.dateTimePickerStockCountDate.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.dateTimePickerStockCountDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerStockCountDate.Location = new System.Drawing.Point(157, 37);
            this.dateTimePickerStockCountDate.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePickerStockCountDate.Name = "dateTimePickerStockCountDate";
            this.dateTimePickerStockCountDate.Size = new System.Drawing.Size(158, 26);
            this.dateTimePickerStockCountDate.TabIndex = 1;
            // 
            // textBoxStockCountNumber
            // 
            this.textBoxStockCountNumber.Enabled = false;
            this.textBoxStockCountNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.textBoxStockCountNumber.Location = new System.Drawing.Point(157, 9);
            this.textBoxStockCountNumber.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxStockCountNumber.Name = "textBoxStockCountNumber";
            this.textBoxStockCountNumber.Size = new System.Drawing.Size(158, 26);
            this.textBoxStockCountNumber.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // TrnStockCountDetailForm
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
            this.Name = "TrnStockCountDetailForm";
            this.Text = "TrnStockOutDetail";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStockCountLineList)).EndInit();
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
        private System.Windows.Forms.Button buttonStockCountLineListPageListFirst;
        private System.Windows.Forms.Button buttonStockCountLineListPageListPrevious;
        private System.Windows.Forms.Button buttonStockCountLineListPageListNext;
        private System.Windows.Forms.Button buttonStockCountLineListPageListLast;
        private System.Windows.Forms.TextBox textBoxStockCountLineListPageNumber;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBoxRemarks;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBoxApprovedBy;
        private System.Windows.Forms.ComboBox comboBoxCheckedBy;
        private System.Windows.Forms.ComboBox comboBoxPreparedBy;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePickerStockCountDate;
        private System.Windows.Forms.TextBox textBoxStockCountNumber;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox textBoxBarcode;
        private System.Windows.Forms.Button buttonBarcode;
        private System.Windows.Forms.Button buttonSearchItem;
        private System.Windows.Forms.DataGridView dataGridViewStockCountLineList;
        private System.Windows.Forms.Button buttonPost;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogGenerateCSV;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnStockCountLineListButtonEdit;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnStockCountLineListButtonDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockCountLineListId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockCountLineListStockCountId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockCountLineListItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockCountLineListItemBarcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockCountLineListItemDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockCountLineListUnitId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockCountLineListUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockCountLineListQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockCountLineListCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockCountLineListAmount;
    }
}