namespace EasyPOS.Forms.Software.TrnStockIn
{
    partial class TrnStockInDetailForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnStockInDetailForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonLock = new System.Windows.Forms.Button();
            this.buttonUnlock = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxManualStockInNumber = new System.Windows.Forms.TextBox();
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
            this.dateTimePickerStockInDate = new System.Windows.Forms.DateTimePicker();
            this.textBoxStockInNumber = new System.Windows.Forms.TextBox();
            this.comboBoxSupplier = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.buttonStockInLineListPageListFirst = new System.Windows.Forms.Button();
            this.buttonStockInLineListPageListPrevious = new System.Windows.Forms.Button();
            this.buttonStockInLineListPageListNext = new System.Windows.Forms.Button();
            this.buttonStockInLineListPageListLast = new System.Windows.Forms.Button();
            this.textBoxStockInLineListPageNumber = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.buttonExportAllItems = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            this.dataGridViewStockInLineList = new System.Windows.Forms.DataGridView();
            this.ColumnStockInLineListButtonEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnStockInLineListButtonDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnStockInLineListId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockInLineListStockInId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockInLineListItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockInLineListItemBarcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockInLineListItemDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockInLineListUnitId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockInLineListUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockInLineListQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockInLineListCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockInLineListAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockInLineListExpiryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockInLineListLotNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockInLineListAssetAccountId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockInLineListAssetAccount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnStockInLineListPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxBarcode = new System.Windows.Forms.TextBox();
            this.buttonBarcode = new System.Windows.Forms.Button();
            this.buttonSearchItem = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.openFileDialogImportCSV = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialogGenerateCSV = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStockInLineList)).BeginInit();
            this.panel2.SuspendLayout();
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
            this.panel1.TabIndex = 7;
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
            this.pictureBox1.Image = global::EasyPOS.Properties.Resources.Stock_In;
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
            this.label1.Size = new System.Drawing.Size(152, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "Stock-In Detail";
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
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.textBoxManualStockInNumber);
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
            this.panel3.Controls.Add(this.dateTimePickerStockInDate);
            this.panel3.Controls.Add(this.textBoxStockInNumber);
            this.panel3.Controls.Add(this.comboBoxSupplier);
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
            this.label3.Location = new System.Drawing.Point(11, 97);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 19);
            this.label3.TabIndex = 25;
            this.label3.Text = "Manual Stock-In Number:";
            // 
            // textBoxManualStockInNumber
            // 
            this.textBoxManualStockInNumber.Enabled = false;
            this.textBoxManualStockInNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.textBoxManualStockInNumber.Location = new System.Drawing.Point(186, 97);
            this.textBoxManualStockInNumber.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxManualStockInNumber.Name = "textBoxManualStockInNumber";
            this.textBoxManualStockInNumber.Size = new System.Drawing.Size(239, 26);
            this.textBoxManualStockInNumber.TabIndex = 3;
            // 
            // textBoxRemarks
            // 
            this.textBoxRemarks.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.textBoxRemarks.Location = new System.Drawing.Point(186, 127);
            this.textBoxRemarks.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxRemarks.Multiline = true;
            this.textBoxRemarks.Name = "textBoxRemarks";
            this.textBoxRemarks.Size = new System.Drawing.Size(239, 55);
            this.textBoxRemarks.TabIndex = 4;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label13.Location = new System.Drawing.Point(454, 70);
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
            this.label12.Location = new System.Drawing.Point(454, 43);
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
            this.label11.Location = new System.Drawing.Point(454, 16);
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
            this.comboBoxApprovedBy.Location = new System.Drawing.Point(570, 67);
            this.comboBoxApprovedBy.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxApprovedBy.Name = "comboBoxApprovedBy";
            this.comboBoxApprovedBy.Size = new System.Drawing.Size(231, 27);
            this.comboBoxApprovedBy.TabIndex = 10;
            // 
            // comboBoxCheckedBy
            // 
            this.comboBoxCheckedBy.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.comboBoxCheckedBy.FormattingEnabled = true;
            this.comboBoxCheckedBy.Location = new System.Drawing.Point(570, 37);
            this.comboBoxCheckedBy.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxCheckedBy.Name = "comboBoxCheckedBy";
            this.comboBoxCheckedBy.Size = new System.Drawing.Size(231, 27);
            this.comboBoxCheckedBy.TabIndex = 9;
            // 
            // comboBoxPreparedBy
            // 
            this.comboBoxPreparedBy.Enabled = false;
            this.comboBoxPreparedBy.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.comboBoxPreparedBy.FormattingEnabled = true;
            this.comboBoxPreparedBy.Location = new System.Drawing.Point(570, 8);
            this.comboBoxPreparedBy.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxPreparedBy.Name = "comboBoxPreparedBy";
            this.comboBoxPreparedBy.Size = new System.Drawing.Size(231, 27);
            this.comboBoxPreparedBy.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label7.Location = new System.Drawing.Point(12, 130);
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
            this.label5.Location = new System.Drawing.Point(10, 69);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 19);
            this.label5.TabIndex = 9;
            this.label5.Text = "Supplier:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label4.Location = new System.Drawing.Point(11, 40);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "Stock-In Date:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label2.Location = new System.Drawing.Point(10, 16);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "Stock-In Number:";
            // 
            // dateTimePickerStockInDate
            // 
            this.dateTimePickerStockInDate.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.dateTimePickerStockInDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerStockInDate.Location = new System.Drawing.Point(186, 37);
            this.dateTimePickerStockInDate.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePickerStockInDate.Name = "dateTimePickerStockInDate";
            this.dateTimePickerStockInDate.Size = new System.Drawing.Size(158, 26);
            this.dateTimePickerStockInDate.TabIndex = 1;
            // 
            // textBoxStockInNumber
            // 
            this.textBoxStockInNumber.Enabled = false;
            this.textBoxStockInNumber.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.textBoxStockInNumber.Location = new System.Drawing.Point(186, 9);
            this.textBoxStockInNumber.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxStockInNumber.Name = "textBoxStockInNumber";
            this.textBoxStockInNumber.Size = new System.Drawing.Size(158, 26);
            this.textBoxStockInNumber.TabIndex = 0;
            this.textBoxStockInNumber.TabStop = false;
            // 
            // comboBoxSupplier
            // 
            this.comboBoxSupplier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.comboBoxSupplier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxSupplier.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.comboBoxSupplier.FormattingEnabled = true;
            this.comboBoxSupplier.Location = new System.Drawing.Point(186, 66);
            this.comboBoxSupplier.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxSupplier.Name = "comboBoxSupplier";
            this.comboBoxSupplier.Size = new System.Drawing.Size(239, 27);
            this.comboBoxSupplier.TabIndex = 2;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.buttonStockInLineListPageListFirst);
            this.panel4.Controls.Add(this.buttonStockInLineListPageListPrevious);
            this.panel4.Controls.Add(this.buttonStockInLineListPageListNext);
            this.panel4.Controls.Add(this.buttonStockInLineListPageListLast);
            this.panel4.Controls.Add(this.textBoxStockInLineListPageNumber);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 468);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1096, 42);
            this.panel4.TabIndex = 25;
            // 
            // buttonStockInLineListPageListFirst
            // 
            this.buttonStockInLineListPageListFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStockInLineListPageListFirst.Enabled = false;
            this.buttonStockInLineListPageListFirst.FlatAppearance.BorderSize = 0;
            this.buttonStockInLineListPageListFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStockInLineListPageListFirst.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonStockInLineListPageListFirst.Location = new System.Drawing.Point(10, 9);
            this.buttonStockInLineListPageListFirst.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStockInLineListPageListFirst.Name = "buttonStockInLineListPageListFirst";
            this.buttonStockInLineListPageListFirst.Size = new System.Drawing.Size(66, 26);
            this.buttonStockInLineListPageListFirst.TabIndex = 13;
            this.buttonStockInLineListPageListFirst.TabStop = false;
            this.buttonStockInLineListPageListFirst.Text = "First";
            this.buttonStockInLineListPageListFirst.UseVisualStyleBackColor = false;
            this.buttonStockInLineListPageListFirst.Click += new System.EventHandler(this.buttonStockInLineListPageListFirst_Click);
            // 
            // buttonStockInLineListPageListPrevious
            // 
            this.buttonStockInLineListPageListPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStockInLineListPageListPrevious.Enabled = false;
            this.buttonStockInLineListPageListPrevious.FlatAppearance.BorderSize = 0;
            this.buttonStockInLineListPageListPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStockInLineListPageListPrevious.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonStockInLineListPageListPrevious.Location = new System.Drawing.Point(80, 9);
            this.buttonStockInLineListPageListPrevious.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStockInLineListPageListPrevious.Name = "buttonStockInLineListPageListPrevious";
            this.buttonStockInLineListPageListPrevious.Size = new System.Drawing.Size(66, 26);
            this.buttonStockInLineListPageListPrevious.TabIndex = 14;
            this.buttonStockInLineListPageListPrevious.TabStop = false;
            this.buttonStockInLineListPageListPrevious.Text = "Previous";
            this.buttonStockInLineListPageListPrevious.UseVisualStyleBackColor = false;
            this.buttonStockInLineListPageListPrevious.Click += new System.EventHandler(this.buttonStockInLineListPageListPrevious_Click);
            // 
            // buttonStockInLineListPageListNext
            // 
            this.buttonStockInLineListPageListNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStockInLineListPageListNext.FlatAppearance.BorderSize = 0;
            this.buttonStockInLineListPageListNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStockInLineListPageListNext.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonStockInLineListPageListNext.Location = new System.Drawing.Point(210, 9);
            this.buttonStockInLineListPageListNext.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStockInLineListPageListNext.Name = "buttonStockInLineListPageListNext";
            this.buttonStockInLineListPageListNext.Size = new System.Drawing.Size(66, 26);
            this.buttonStockInLineListPageListNext.TabIndex = 15;
            this.buttonStockInLineListPageListNext.TabStop = false;
            this.buttonStockInLineListPageListNext.Text = "Next";
            this.buttonStockInLineListPageListNext.UseVisualStyleBackColor = false;
            this.buttonStockInLineListPageListNext.Click += new System.EventHandler(this.buttonStockInLineListPageListNext_Click);
            // 
            // buttonStockInLineListPageListLast
            // 
            this.buttonStockInLineListPageListLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStockInLineListPageListLast.FlatAppearance.BorderSize = 0;
            this.buttonStockInLineListPageListLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStockInLineListPageListLast.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonStockInLineListPageListLast.Location = new System.Drawing.Point(278, 9);
            this.buttonStockInLineListPageListLast.Margin = new System.Windows.Forms.Padding(2);
            this.buttonStockInLineListPageListLast.Name = "buttonStockInLineListPageListLast";
            this.buttonStockInLineListPageListLast.Size = new System.Drawing.Size(66, 26);
            this.buttonStockInLineListPageListLast.TabIndex = 16;
            this.buttonStockInLineListPageListLast.TabStop = false;
            this.buttonStockInLineListPageListLast.Text = "Last";
            this.buttonStockInLineListPageListLast.UseVisualStyleBackColor = false;
            this.buttonStockInLineListPageListLast.Click += new System.EventHandler(this.buttonStockInLineListPageListLast_Click);
            // 
            // textBoxStockInLineListPageNumber
            // 
            this.textBoxStockInLineListPageNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxStockInLineListPageNumber.BackColor = System.Drawing.Color.White;
            this.textBoxStockInLineListPageNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxStockInLineListPageNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxStockInLineListPageNumber.Location = new System.Drawing.Point(150, 13);
            this.textBoxStockInLineListPageNumber.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxStockInLineListPageNumber.Name = "textBoxStockInLineListPageNumber";
            this.textBoxStockInLineListPageNumber.ReadOnly = true;
            this.textBoxStockInLineListPageNumber.Size = new System.Drawing.Size(55, 16);
            this.textBoxStockInLineListPageNumber.TabIndex = 17;
            this.textBoxStockInLineListPageNumber.TabStop = false;
            this.textBoxStockInLineListPageNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Control;
            this.panel5.Controls.Add(this.buttonExportAllItems);
            this.panel5.Controls.Add(this.buttonExport);
            this.panel5.Controls.Add(this.buttonImport);
            this.panel5.Controls.Add(this.dataGridViewStockInLineList);
            this.panel5.Controls.Add(this.textBoxBarcode);
            this.panel5.Controls.Add(this.buttonBarcode);
            this.panel5.Controls.Add(this.buttonSearchItem);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 195);
            this.panel5.Margin = new System.Windows.Forms.Padding(2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1096, 273);
            this.panel5.TabIndex = 26;
            // 
            // buttonExportAllItems
            // 
            this.buttonExportAllItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExportAllItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(188)))), ((int)(((byte)(0)))));
            this.buttonExportAllItems.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonExportAllItems.FlatAppearance.BorderSize = 0;
            this.buttonExportAllItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExportAllItems.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExportAllItems.ForeColor = System.Drawing.Color.White;
            this.buttonExportAllItems.Location = new System.Drawing.Point(810, 5);
            this.buttonExportAllItems.Margin = new System.Windows.Forms.Padding(2);
            this.buttonExportAllItems.Name = "buttonExportAllItems";
            this.buttonExportAllItems.Size = new System.Drawing.Size(126, 32);
            this.buttonExportAllItems.TabIndex = 26;
            this.buttonExportAllItems.TabStop = false;
            this.buttonExportAllItems.Text = "Export All Items";
            this.buttonExportAllItems.UseVisualStyleBackColor = false;
            this.buttonExportAllItems.Click += new System.EventHandler(this.buttonExportAllItems_Click);
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
            this.buttonExport.Location = new System.Drawing.Point(737, 5);
            this.buttonExport.Margin = new System.Windows.Forms.Padding(2);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(69, 32);
            this.buttonExport.TabIndex = 25;
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
            this.buttonImport.Location = new System.Drawing.Point(663, 5);
            this.buttonImport.Margin = new System.Windows.Forms.Padding(2);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(69, 32);
            this.buttonImport.TabIndex = 24;
            this.buttonImport.TabStop = false;
            this.buttonImport.Text = "Import";
            this.buttonImport.UseVisualStyleBackColor = false;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // dataGridViewStockInLineList
            // 
            this.dataGridViewStockInLineList.AllowUserToAddRows = false;
            this.dataGridViewStockInLineList.AllowUserToDeleteRows = false;
            this.dataGridViewStockInLineList.AllowUserToResizeRows = false;
            this.dataGridViewStockInLineList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewStockInLineList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewStockInLineList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewStockInLineList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStockInLineList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnStockInLineListButtonEdit,
            this.ColumnStockInLineListButtonDelete,
            this.ColumnStockInLineListId,
            this.ColumnStockInLineListStockInId,
            this.ColumnStockInLineListItemId,
            this.ColumnStockInLineListItemBarcode,
            this.ColumnStockInLineListItemDescription,
            this.ColumnStockInLineListUnitId,
            this.ColumnStockInLineListUnit,
            this.ColumnStockInLineListQuantity,
            this.ColumnStockInLineListCost,
            this.ColumnStockInLineListAmount,
            this.ColumnStockInLineListExpiryDate,
            this.ColumnStockInLineListLotNumber,
            this.ColumnStockInLineListAssetAccountId,
            this.ColumnStockInLineListAssetAccount,
            this.ColumnStockInLineListPrice});
            this.dataGridViewStockInLineList.Location = new System.Drawing.Point(10, 42);
            this.dataGridViewStockInLineList.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewStockInLineList.MultiSelect = false;
            this.dataGridViewStockInLineList.Name = "dataGridViewStockInLineList";
            this.dataGridViewStockInLineList.ReadOnly = true;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.dataGridViewStockInLineList.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewStockInLineList.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.dataGridViewStockInLineList.RowTemplate.Height = 24;
            this.dataGridViewStockInLineList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewStockInLineList.Size = new System.Drawing.Size(1077, 227);
            this.dataGridViewStockInLineList.TabIndex = 1;
            this.dataGridViewStockInLineList.TabStop = false;
            this.dataGridViewStockInLineList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewStockInLineList_CellClick);
            // 
            // ColumnStockInLineListButtonEdit
            // 
            this.ColumnStockInLineListButtonEdit.DataPropertyName = "ColumnStockInLineListButtonEdit";
            this.ColumnStockInLineListButtonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColumnStockInLineListButtonEdit.HeaderText = "";
            this.ColumnStockInLineListButtonEdit.Name = "ColumnStockInLineListButtonEdit";
            this.ColumnStockInLineListButtonEdit.ReadOnly = true;
            this.ColumnStockInLineListButtonEdit.Width = 70;
            // 
            // ColumnStockInLineListButtonDelete
            // 
            this.ColumnStockInLineListButtonDelete.DataPropertyName = "ColumnStockInLineListButtonDelete";
            this.ColumnStockInLineListButtonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColumnStockInLineListButtonDelete.HeaderText = "";
            this.ColumnStockInLineListButtonDelete.Name = "ColumnStockInLineListButtonDelete";
            this.ColumnStockInLineListButtonDelete.ReadOnly = true;
            this.ColumnStockInLineListButtonDelete.Width = 70;
            // 
            // ColumnStockInLineListId
            // 
            this.ColumnStockInLineListId.DataPropertyName = "ColumnStockInLineListId";
            this.ColumnStockInLineListId.HeaderText = "Id";
            this.ColumnStockInLineListId.Name = "ColumnStockInLineListId";
            this.ColumnStockInLineListId.ReadOnly = true;
            this.ColumnStockInLineListId.Visible = false;
            // 
            // ColumnStockInLineListStockInId
            // 
            this.ColumnStockInLineListStockInId.DataPropertyName = "ColumnStockInLineListStockInId";
            this.ColumnStockInLineListStockInId.HeaderText = "Stock-In Id";
            this.ColumnStockInLineListStockInId.Name = "ColumnStockInLineListStockInId";
            this.ColumnStockInLineListStockInId.ReadOnly = true;
            this.ColumnStockInLineListStockInId.Visible = false;
            // 
            // ColumnStockInLineListItemId
            // 
            this.ColumnStockInLineListItemId.DataPropertyName = "ColumnStockInLineListItemId";
            this.ColumnStockInLineListItemId.HeaderText = "Item Id";
            this.ColumnStockInLineListItemId.Name = "ColumnStockInLineListItemId";
            this.ColumnStockInLineListItemId.ReadOnly = true;
            this.ColumnStockInLineListItemId.Visible = false;
            // 
            // ColumnStockInLineListItemBarcode
            // 
            this.ColumnStockInLineListItemBarcode.DataPropertyName = "ColumnStockInLineListItemBarcode";
            this.ColumnStockInLineListItemBarcode.HeaderText = "Item Barcode";
            this.ColumnStockInLineListItemBarcode.Name = "ColumnStockInLineListItemBarcode";
            this.ColumnStockInLineListItemBarcode.ReadOnly = true;
            this.ColumnStockInLineListItemBarcode.Visible = false;
            // 
            // ColumnStockInLineListItemDescription
            // 
            this.ColumnStockInLineListItemDescription.DataPropertyName = "ColumnStockInLineListItemDescription";
            this.ColumnStockInLineListItemDescription.HeaderText = "Item Description";
            this.ColumnStockInLineListItemDescription.Name = "ColumnStockInLineListItemDescription";
            this.ColumnStockInLineListItemDescription.ReadOnly = true;
            this.ColumnStockInLineListItemDescription.Width = 200;
            // 
            // ColumnStockInLineListUnitId
            // 
            this.ColumnStockInLineListUnitId.DataPropertyName = "ColumnStockInLineListUnitId";
            this.ColumnStockInLineListUnitId.HeaderText = "Unit Id";
            this.ColumnStockInLineListUnitId.Name = "ColumnStockInLineListUnitId";
            this.ColumnStockInLineListUnitId.ReadOnly = true;
            this.ColumnStockInLineListUnitId.Visible = false;
            // 
            // ColumnStockInLineListUnit
            // 
            this.ColumnStockInLineListUnit.DataPropertyName = "ColumnStockInLineListUnit";
            this.ColumnStockInLineListUnit.HeaderText = "Unit";
            this.ColumnStockInLineListUnit.Name = "ColumnStockInLineListUnit";
            this.ColumnStockInLineListUnit.ReadOnly = true;
            // 
            // ColumnStockInLineListQuantity
            // 
            this.ColumnStockInLineListQuantity.DataPropertyName = "ColumnStockInLineListQuantity";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnStockInLineListQuantity.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnStockInLineListQuantity.HeaderText = "Quantity";
            this.ColumnStockInLineListQuantity.Name = "ColumnStockInLineListQuantity";
            this.ColumnStockInLineListQuantity.ReadOnly = true;
            // 
            // ColumnStockInLineListCost
            // 
            this.ColumnStockInLineListCost.DataPropertyName = "ColumnStockInLineListCost";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnStockInLineListCost.DefaultCellStyle = dataGridViewCellStyle3;
            this.ColumnStockInLineListCost.HeaderText = "Cost";
            this.ColumnStockInLineListCost.Name = "ColumnStockInLineListCost";
            this.ColumnStockInLineListCost.ReadOnly = true;
            // 
            // ColumnStockInLineListAmount
            // 
            this.ColumnStockInLineListAmount.DataPropertyName = "ColumnStockInLineListAmount";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnStockInLineListAmount.DefaultCellStyle = dataGridViewCellStyle4;
            this.ColumnStockInLineListAmount.HeaderText = "Amount";
            this.ColumnStockInLineListAmount.Name = "ColumnStockInLineListAmount";
            this.ColumnStockInLineListAmount.ReadOnly = true;
            // 
            // ColumnStockInLineListExpiryDate
            // 
            this.ColumnStockInLineListExpiryDate.DataPropertyName = "ColumnStockInLineListExpiryDate";
            this.ColumnStockInLineListExpiryDate.HeaderText = "Expiry";
            this.ColumnStockInLineListExpiryDate.Name = "ColumnStockInLineListExpiryDate";
            this.ColumnStockInLineListExpiryDate.ReadOnly = true;
            // 
            // ColumnStockInLineListLotNumber
            // 
            this.ColumnStockInLineListLotNumber.DataPropertyName = "ColumnStockInLineListLotNumber";
            this.ColumnStockInLineListLotNumber.HeaderText = "Lot No.";
            this.ColumnStockInLineListLotNumber.Name = "ColumnStockInLineListLotNumber";
            this.ColumnStockInLineListLotNumber.ReadOnly = true;
            this.ColumnStockInLineListLotNumber.Width = 120;
            // 
            // ColumnStockInLineListAssetAccountId
            // 
            this.ColumnStockInLineListAssetAccountId.DataPropertyName = "ColumnStockInLineListAssetAccountId";
            this.ColumnStockInLineListAssetAccountId.HeaderText = "Asset Account Id";
            this.ColumnStockInLineListAssetAccountId.Name = "ColumnStockInLineListAssetAccountId";
            this.ColumnStockInLineListAssetAccountId.ReadOnly = true;
            this.ColumnStockInLineListAssetAccountId.Visible = false;
            // 
            // ColumnStockInLineListAssetAccount
            // 
            this.ColumnStockInLineListAssetAccount.DataPropertyName = "ColumnStockInLineListAssetAccount";
            this.ColumnStockInLineListAssetAccount.HeaderText = "Asset Account";
            this.ColumnStockInLineListAssetAccount.Name = "ColumnStockInLineListAssetAccount";
            this.ColumnStockInLineListAssetAccount.ReadOnly = true;
            this.ColumnStockInLineListAssetAccount.Visible = false;
            // 
            // ColumnStockInLineListPrice
            // 
            this.ColumnStockInLineListPrice.DataPropertyName = "ColumnStockInLineListPrice";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnStockInLineListPrice.DefaultCellStyle = dataGridViewCellStyle5;
            this.ColumnStockInLineListPrice.HeaderText = "Price";
            this.ColumnStockInLineListPrice.Name = "ColumnStockInLineListPrice";
            this.ColumnStockInLineListPrice.ReadOnly = true;
            // 
            // textBoxBarcode
            // 
            this.textBoxBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBarcode.Font = new System.Drawing.Font("Segoe UI Semibold", 13.3F, System.Drawing.FontStyle.Bold);
            this.textBoxBarcode.Location = new System.Drawing.Point(181, 6);
            this.textBoxBarcode.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxBarcode.Name = "textBoxBarcode";
            this.textBoxBarcode.Size = new System.Drawing.Size(474, 31);
            this.textBoxBarcode.TabIndex = 11;
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
            this.buttonBarcode.Size = new System.Drawing.Size(167, 32);
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
            this.panel2.TabIndex = 8;
            // 
            // openFileDialogImportCSV
            // 
            this.openFileDialogImportCSV.FileName = "openFileDialogImport";
            // 
            // TrnStockInDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1096, 560);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TrnStockInDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TrnStockInDetail";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStockInLineList)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonLock;
        private System.Windows.Forms.Button buttonUnlock;
        private System.Windows.Forms.Button buttonPrint;
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
        private System.Windows.Forms.ComboBox comboBoxSupplier;
        private System.Windows.Forms.DateTimePicker dateTimePickerStockInDate;
        private System.Windows.Forms.TextBox textBoxStockInNumber;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button buttonStockInLineListPageListFirst;
        private System.Windows.Forms.Button buttonStockInLineListPageListPrevious;
        private System.Windows.Forms.Button buttonStockInLineListPageListNext;
        private System.Windows.Forms.Button buttonStockInLineListPageListLast;
        private System.Windows.Forms.TextBox textBoxStockInLineListPageNumber;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox textBoxBarcode;
        private System.Windows.Forms.Button buttonBarcode;
        private System.Windows.Forms.Button buttonSearchItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxManualStockInNumber;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.OpenFileDialog openFileDialogImportCSV;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogGenerateCSV;
        private System.Windows.Forms.DataGridView dataGridViewStockInLineList;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnStockInLineListButtonEdit;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnStockInLineListButtonDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockInLineListId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockInLineListStockInId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockInLineListItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockInLineListItemBarcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockInLineListItemDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockInLineListUnitId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockInLineListUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockInLineListQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockInLineListCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockInLineListAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockInLineListExpiryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockInLineListLotNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockInLineListAssetAccountId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockInLineListAssetAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnStockInLineListPrice;
        private System.Windows.Forms.Button buttonExportAllItems;
    }
}