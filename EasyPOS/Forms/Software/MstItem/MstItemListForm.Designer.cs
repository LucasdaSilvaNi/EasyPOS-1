namespace EasyPOS.Forms.Software.MstItem
{
    partial class MstItemListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MstItemListForm));
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.comboBoxIsLocked = new System.Windows.Forms.ComboBox();
            this.comboBoxIsInventory = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.buttonItemListPageListFirst = new System.Windows.Forms.Button();
            this.buttonItemListPageListPrevious = new System.Windows.Forms.Button();
            this.buttonItemListPageListNext = new System.Windows.Forms.Button();
            this.buttonItemListPageListLast = new System.Windows.Forms.Button();
            this.textBoxItemListPageNumber = new System.Windows.Forms.TextBox();
            this.dataGridViewItemList = new System.Windows.Forms.DataGridView();
            this.textBoxItemListFilter = new System.Windows.Forms.TextBox();
            this.ColumnItemListButtonEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnItemListButtonDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnItemListId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnItemListCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnItemListDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnItemListBarcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnItemListUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnItemListCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSupplier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnItemListPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnItemListCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnItemListOnHandQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnItemListIsInventory = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnItemListIsLocked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItemList)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonAdd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonAdd.FlatAppearance.BorderSize = 0;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdd.ForeColor = System.Drawing.Color.White;
            this.buttonAdd.Location = new System.Drawing.Point(941, 10);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(70, 32);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.TabStop = false;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
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
            this.buttonClose.TabIndex = 1;
            this.buttonClose.TabStop = false;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.buttonAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1096, 50);
            this.panel1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::EasyPOS.Properties.Resources.Item;
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
            this.label1.Size = new System.Drawing.Size(94, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "Item List";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.comboBoxIsLocked);
            this.panel2.Controls.Add(this.comboBoxIsInventory);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.dataGridViewItemList);
            this.panel2.Controls.Add(this.textBoxItemListFilter);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1096, 510);
            this.panel2.TabIndex = 19;
            // 
            // comboBoxIsLocked
            // 
            this.comboBoxIsLocked.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxIsLocked.FormattingEnabled = true;
            this.comboBoxIsLocked.Location = new System.Drawing.Point(965, 4);
            this.comboBoxIsLocked.Name = "comboBoxIsLocked";
            this.comboBoxIsLocked.Size = new System.Drawing.Size(121, 27);
            this.comboBoxIsLocked.TabIndex = 19;
            this.comboBoxIsLocked.SelectedIndexChanged += new System.EventHandler(this.comboBoxIsLocked_SelectedIndexChanged);
            // 
            // comboBoxIsInventory
            // 
            this.comboBoxIsInventory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxIsInventory.FormattingEnabled = true;
            this.comboBoxIsInventory.Location = new System.Drawing.Point(807, 4);
            this.comboBoxIsInventory.Name = "comboBoxIsInventory";
            this.comboBoxIsInventory.Size = new System.Drawing.Size(153, 27);
            this.comboBoxIsInventory.TabIndex = 18;
            this.comboBoxIsInventory.SelectedIndexChanged += new System.EventHandler(this.comboBoxIsInventory_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.buttonItemListPageListFirst);
            this.panel3.Controls.Add(this.buttonItemListPageListPrevious);
            this.panel3.Controls.Add(this.buttonItemListPageListNext);
            this.panel3.Controls.Add(this.buttonItemListPageListLast);
            this.panel3.Controls.Add(this.textBoxItemListPageNumber);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 468);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1096, 42);
            this.panel3.TabIndex = 18;
            // 
            // buttonItemListPageListFirst
            // 
            this.buttonItemListPageListFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonItemListPageListFirst.Enabled = false;
            this.buttonItemListPageListFirst.FlatAppearance.BorderSize = 0;
            this.buttonItemListPageListFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonItemListPageListFirst.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonItemListPageListFirst.Location = new System.Drawing.Point(10, 9);
            this.buttonItemListPageListFirst.Margin = new System.Windows.Forms.Padding(2);
            this.buttonItemListPageListFirst.Name = "buttonItemListPageListFirst";
            this.buttonItemListPageListFirst.Size = new System.Drawing.Size(66, 26);
            this.buttonItemListPageListFirst.TabIndex = 13;
            this.buttonItemListPageListFirst.TabStop = false;
            this.buttonItemListPageListFirst.Text = "First";
            this.buttonItemListPageListFirst.UseVisualStyleBackColor = false;
            this.buttonItemListPageListFirst.Click += new System.EventHandler(this.buttonItemListPageListFirst_Click);
            // 
            // buttonItemListPageListPrevious
            // 
            this.buttonItemListPageListPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonItemListPageListPrevious.Enabled = false;
            this.buttonItemListPageListPrevious.FlatAppearance.BorderSize = 0;
            this.buttonItemListPageListPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonItemListPageListPrevious.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonItemListPageListPrevious.Location = new System.Drawing.Point(80, 9);
            this.buttonItemListPageListPrevious.Margin = new System.Windows.Forms.Padding(2);
            this.buttonItemListPageListPrevious.Name = "buttonItemListPageListPrevious";
            this.buttonItemListPageListPrevious.Size = new System.Drawing.Size(66, 26);
            this.buttonItemListPageListPrevious.TabIndex = 14;
            this.buttonItemListPageListPrevious.TabStop = false;
            this.buttonItemListPageListPrevious.Text = "Previous";
            this.buttonItemListPageListPrevious.UseVisualStyleBackColor = false;
            this.buttonItemListPageListPrevious.Click += new System.EventHandler(this.buttonItemListPageListPrevious_Click);
            // 
            // buttonItemListPageListNext
            // 
            this.buttonItemListPageListNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonItemListPageListNext.FlatAppearance.BorderSize = 0;
            this.buttonItemListPageListNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonItemListPageListNext.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonItemListPageListNext.Location = new System.Drawing.Point(210, 9);
            this.buttonItemListPageListNext.Margin = new System.Windows.Forms.Padding(2);
            this.buttonItemListPageListNext.Name = "buttonItemListPageListNext";
            this.buttonItemListPageListNext.Size = new System.Drawing.Size(66, 26);
            this.buttonItemListPageListNext.TabIndex = 15;
            this.buttonItemListPageListNext.TabStop = false;
            this.buttonItemListPageListNext.Text = "Next";
            this.buttonItemListPageListNext.UseVisualStyleBackColor = false;
            this.buttonItemListPageListNext.Click += new System.EventHandler(this.buttonItemListPageListNext_Click);
            // 
            // buttonItemListPageListLast
            // 
            this.buttonItemListPageListLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonItemListPageListLast.FlatAppearance.BorderSize = 0;
            this.buttonItemListPageListLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonItemListPageListLast.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonItemListPageListLast.Location = new System.Drawing.Point(278, 9);
            this.buttonItemListPageListLast.Margin = new System.Windows.Forms.Padding(2);
            this.buttonItemListPageListLast.Name = "buttonItemListPageListLast";
            this.buttonItemListPageListLast.Size = new System.Drawing.Size(66, 26);
            this.buttonItemListPageListLast.TabIndex = 16;
            this.buttonItemListPageListLast.TabStop = false;
            this.buttonItemListPageListLast.Text = "Last";
            this.buttonItemListPageListLast.UseVisualStyleBackColor = false;
            this.buttonItemListPageListLast.Click += new System.EventHandler(this.buttonItemListPageListLast_Click);
            // 
            // textBoxItemListPageNumber
            // 
            this.textBoxItemListPageNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxItemListPageNumber.BackColor = System.Drawing.Color.White;
            this.textBoxItemListPageNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxItemListPageNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxItemListPageNumber.Location = new System.Drawing.Point(150, 13);
            this.textBoxItemListPageNumber.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxItemListPageNumber.Name = "textBoxItemListPageNumber";
            this.textBoxItemListPageNumber.ReadOnly = true;
            this.textBoxItemListPageNumber.Size = new System.Drawing.Size(55, 16);
            this.textBoxItemListPageNumber.TabIndex = 17;
            this.textBoxItemListPageNumber.TabStop = false;
            this.textBoxItemListPageNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dataGridViewItemList
            // 
            this.dataGridViewItemList.AllowUserToAddRows = false;
            this.dataGridViewItemList.AllowUserToDeleteRows = false;
            this.dataGridViewItemList.AllowUserToResizeRows = false;
            this.dataGridViewItemList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewItemList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewItemList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnItemListButtonEdit,
            this.ColumnItemListButtonDelete,
            this.ColumnItemListId,
            this.ColumnItemListCode,
            this.ColumnItemListDescription,
            this.ColumnItemListBarcode,
            this.ColumnItemListUnit,
            this.ColumnItemListCategory,
            this.ColumnSupplier,
            this.ColumnItemListPrice,
            this.ColumnItemListCost,
            this.ColumnItemListOnHandQuantity,
            this.ColumnItemListIsInventory,
            this.ColumnItemListIsLocked});
            this.dataGridViewItemList.Location = new System.Drawing.Point(10, 34);
            this.dataGridViewItemList.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewItemList.MultiSelect = false;
            this.dataGridViewItemList.Name = "dataGridViewItemList";
            this.dataGridViewItemList.ReadOnly = true;
            this.dataGridViewItemList.RowTemplate.Height = 24;
            this.dataGridViewItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewItemList.Size = new System.Drawing.Size(1077, 429);
            this.dataGridViewItemList.TabIndex = 9;
            this.dataGridViewItemList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewItemList_CellClick);
            this.dataGridViewItemList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewItemList_CellContentClick);
            // 
            // textBoxItemListFilter
            // 
            this.textBoxItemListFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxItemListFilter.Location = new System.Drawing.Point(10, 4);
            this.textBoxItemListFilter.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxItemListFilter.Name = "textBoxItemListFilter";
            this.textBoxItemListFilter.Size = new System.Drawing.Size(791, 26);
            this.textBoxItemListFilter.TabIndex = 8;
            this.textBoxItemListFilter.TabStop = false;
            this.textBoxItemListFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxItemListFilter_KeyDown);
            // 
            // ColumnItemListButtonEdit
            // 
            this.ColumnItemListButtonEdit.DataPropertyName = "ColumnItemListButtonEdit";
            this.ColumnItemListButtonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColumnItemListButtonEdit.HeaderText = "";
            this.ColumnItemListButtonEdit.MinimumWidth = 70;
            this.ColumnItemListButtonEdit.Name = "ColumnItemListButtonEdit";
            this.ColumnItemListButtonEdit.ReadOnly = true;
            this.ColumnItemListButtonEdit.Width = 70;
            // 
            // ColumnItemListButtonDelete
            // 
            this.ColumnItemListButtonDelete.DataPropertyName = "ColumnItemListButtonDelete";
            this.ColumnItemListButtonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColumnItemListButtonDelete.HeaderText = "";
            this.ColumnItemListButtonDelete.MinimumWidth = 70;
            this.ColumnItemListButtonDelete.Name = "ColumnItemListButtonDelete";
            this.ColumnItemListButtonDelete.ReadOnly = true;
            this.ColumnItemListButtonDelete.Width = 70;
            // 
            // ColumnItemListId
            // 
            this.ColumnItemListId.DataPropertyName = "ColumnItemListId";
            this.ColumnItemListId.HeaderText = "Id";
            this.ColumnItemListId.MinimumWidth = 50;
            this.ColumnItemListId.Name = "ColumnItemListId";
            this.ColumnItemListId.ReadOnly = true;
            this.ColumnItemListId.Visible = false;
            this.ColumnItemListId.Width = 50;
            // 
            // ColumnItemListCode
            // 
            this.ColumnItemListCode.DataPropertyName = "ColumnItemListCode";
            this.ColumnItemListCode.HeaderText = "Item Code";
            this.ColumnItemListCode.MinimumWidth = 100;
            this.ColumnItemListCode.Name = "ColumnItemListCode";
            this.ColumnItemListCode.ReadOnly = true;
            // 
            // ColumnItemListDescription
            // 
            this.ColumnItemListDescription.DataPropertyName = "ColumnItemListDescription";
            this.ColumnItemListDescription.HeaderText = "Item Description";
            this.ColumnItemListDescription.MinimumWidth = 200;
            this.ColumnItemListDescription.Name = "ColumnItemListDescription";
            this.ColumnItemListDescription.ReadOnly = true;
            this.ColumnItemListDescription.Width = 200;
            // 
            // ColumnItemListBarcode
            // 
            this.ColumnItemListBarcode.DataPropertyName = "ColumnItemListBarcode";
            this.ColumnItemListBarcode.HeaderText = "Barcode";
            this.ColumnItemListBarcode.MinimumWidth = 100;
            this.ColumnItemListBarcode.Name = "ColumnItemListBarcode";
            this.ColumnItemListBarcode.ReadOnly = true;
            // 
            // ColumnItemListUnit
            // 
            this.ColumnItemListUnit.DataPropertyName = "ColumnItemListUnit";
            this.ColumnItemListUnit.HeaderText = "Unit";
            this.ColumnItemListUnit.MinimumWidth = 45;
            this.ColumnItemListUnit.Name = "ColumnItemListUnit";
            this.ColumnItemListUnit.ReadOnly = true;
            this.ColumnItemListUnit.Width = 45;
            // 
            // ColumnItemListCategory
            // 
            this.ColumnItemListCategory.DataPropertyName = "ColumnItemListCategory";
            this.ColumnItemListCategory.HeaderText = "Category";
            this.ColumnItemListCategory.MinimumWidth = 100;
            this.ColumnItemListCategory.Name = "ColumnItemListCategory";
            this.ColumnItemListCategory.ReadOnly = true;
            // 
            // ColumnSupplier
            // 
            this.ColumnSupplier.DataPropertyName = "ColumnSupplier";
            this.ColumnSupplier.HeaderText = "Default Supplier";
            this.ColumnSupplier.MinimumWidth = 200;
            this.ColumnSupplier.Name = "ColumnSupplier";
            this.ColumnSupplier.ReadOnly = true;
            this.ColumnSupplier.Width = 200;
            // 
            // ColumnItemListPrice
            // 
            this.ColumnItemListPrice.DataPropertyName = "ColumnItemListPrice";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnItemListPrice.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnItemListPrice.HeaderText = "Price";
            this.ColumnItemListPrice.MinimumWidth = 75;
            this.ColumnItemListPrice.Name = "ColumnItemListPrice";
            this.ColumnItemListPrice.ReadOnly = true;
            this.ColumnItemListPrice.Width = 75;
            // 
            // ColumnItemListCost
            // 
            this.ColumnItemListCost.DataPropertyName = "ColumnItemListCost";
            this.ColumnItemListCost.HeaderText = "Cost";
            this.ColumnItemListCost.MinimumWidth = 100;
            this.ColumnItemListCost.Name = "ColumnItemListCost";
            this.ColumnItemListCost.ReadOnly = true;
            this.ColumnItemListCost.Visible = false;
            // 
            // ColumnItemListOnHandQuantity
            // 
            this.ColumnItemListOnHandQuantity.DataPropertyName = "ColumnItemListOnHandQuantity";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnItemListOnHandQuantity.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnItemListOnHandQuantity.HeaderText = "Qty.";
            this.ColumnItemListOnHandQuantity.MinimumWidth = 45;
            this.ColumnItemListOnHandQuantity.Name = "ColumnItemListOnHandQuantity";
            this.ColumnItemListOnHandQuantity.ReadOnly = true;
            this.ColumnItemListOnHandQuantity.Width = 45;
            // 
            // ColumnItemListIsInventory
            // 
            this.ColumnItemListIsInventory.DataPropertyName = "ColumnItemListIsInventory";
            this.ColumnItemListIsInventory.HeaderText = "I";
            this.ColumnItemListIsInventory.MinimumWidth = 25;
            this.ColumnItemListIsInventory.Name = "ColumnItemListIsInventory";
            this.ColumnItemListIsInventory.ReadOnly = true;
            this.ColumnItemListIsInventory.Width = 25;
            // 
            // ColumnItemListIsLocked
            // 
            this.ColumnItemListIsLocked.DataPropertyName = "ColumnItemListIsLocked";
            this.ColumnItemListIsLocked.HeaderText = "L";
            this.ColumnItemListIsLocked.MinimumWidth = 25;
            this.ColumnItemListIsLocked.Name = "ColumnItemListIsLocked";
            this.ColumnItemListIsLocked.ReadOnly = true;
            this.ColumnItemListIsLocked.Width = 25;
            // 
            // MstItemListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1096, 560);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "MstItemListForm";
            this.Text = "Item List";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItemList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonItemListPageListFirst;
        private System.Windows.Forms.Button buttonItemListPageListPrevious;
        private System.Windows.Forms.Button buttonItemListPageListNext;
        private System.Windows.Forms.Button buttonItemListPageListLast;
        private System.Windows.Forms.TextBox textBoxItemListPageNumber;
        private System.Windows.Forms.DataGridView dataGridViewItemList;
        private System.Windows.Forms.TextBox textBoxItemListFilter;
        private System.Windows.Forms.ComboBox comboBoxIsLocked;
        private System.Windows.Forms.ComboBox comboBoxIsInventory;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnItemListButtonEdit;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnItemListButtonDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItemListId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItemListCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItemListDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItemListBarcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItemListUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItemListCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSupplier;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItemListPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItemListCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItemListOnHandQuantity;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnItemListIsInventory;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnItemListIsLocked;
    }
}