namespace EasyPOS.Forms.Software.MstItemGroup
{
    partial class MstItemGroupDetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MstItemGroupDetailForm));
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.buttonSearchItem = new System.Windows.Forms.Button();
            this.dataGridViewItemGroupItemList = new System.Windows.Forms.DataGridView();
            this.ColumnItemGroupItemListButtonEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnItemGroupItemListButtonDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnItemGroupItemListId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnItemGroupItemListItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnItemGroupItemListItemDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnItemGroupItemListItemGroupId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnItemGroupItemListShow = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.buttonItemGroupItemListPageListFirst = new System.Windows.Forms.Button();
            this.buttonItemGroupItemListPageListPrevious = new System.Windows.Forms.Button();
            this.buttonItemGroupItemListPageListNext = new System.Windows.Forms.Button();
            this.buttonItemGroupItemListPageListLast = new System.Windows.Forms.Button();
            this.textBoxItemGroupItemListPageNumber = new System.Windows.Forms.TextBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.comboBoxItemGroupKitchens = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxItemGroupImagePath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxItemGroup = new System.Windows.Forms.TextBox();
            this.buttonLock = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonUnlock = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItemGroupItemList)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1096, 510);
            this.panel2.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1096, 510);
            this.panel3.TabIndex = 9;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Control;
            this.panel5.Controls.Add(this.buttonSearchItem);
            this.panel5.Controls.Add(this.dataGridViewItemGroupItemList);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 106);
            this.panel5.Margin = new System.Windows.Forms.Padding(2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1096, 362);
            this.panel5.TabIndex = 29;
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
            this.buttonSearchItem.TabIndex = 9;
            this.buttonSearchItem.TabStop = false;
            this.buttonSearchItem.Text = "Search Item";
            this.buttonSearchItem.UseVisualStyleBackColor = false;
            this.buttonSearchItem.Click += new System.EventHandler(this.buttonSearchItem_Click);
            // 
            // dataGridViewItemGroupItemList
            // 
            this.dataGridViewItemGroupItemList.AllowUserToAddRows = false;
            this.dataGridViewItemGroupItemList.AllowUserToDeleteRows = false;
            this.dataGridViewItemGroupItemList.AllowUserToResizeRows = false;
            this.dataGridViewItemGroupItemList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewItemGroupItemList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewItemGroupItemList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewItemGroupItemList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewItemGroupItemList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnItemGroupItemListButtonEdit,
            this.ColumnItemGroupItemListButtonDelete,
            this.ColumnItemGroupItemListId,
            this.ColumnItemGroupItemListItemId,
            this.ColumnItemGroupItemListItemDescription,
            this.ColumnItemGroupItemListItemGroupId,
            this.ColumnItemGroupItemListShow});
            this.dataGridViewItemGroupItemList.Location = new System.Drawing.Point(10, 42);
            this.dataGridViewItemGroupItemList.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewItemGroupItemList.MultiSelect = false;
            this.dataGridViewItemGroupItemList.Name = "dataGridViewItemGroupItemList";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.dataGridViewItemGroupItemList.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewItemGroupItemList.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.dataGridViewItemGroupItemList.RowTemplate.Height = 24;
            this.dataGridViewItemGroupItemList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewItemGroupItemList.Size = new System.Drawing.Size(1077, 316);
            this.dataGridViewItemGroupItemList.TabIndex = 1;
            this.dataGridViewItemGroupItemList.TabStop = false;
            this.dataGridViewItemGroupItemList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewItemGroupItemList_CellClick);
            // 
            // ColumnItemGroupItemListButtonEdit
            // 
            this.ColumnItemGroupItemListButtonEdit.DataPropertyName = "ColumnItemGroupItemListButtonEdit";
            this.ColumnItemGroupItemListButtonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColumnItemGroupItemListButtonEdit.HeaderText = "";
            this.ColumnItemGroupItemListButtonEdit.Name = "ColumnItemGroupItemListButtonEdit";
            this.ColumnItemGroupItemListButtonEdit.ReadOnly = true;
            this.ColumnItemGroupItemListButtonEdit.Width = 70;
            // 
            // ColumnItemGroupItemListButtonDelete
            // 
            this.ColumnItemGroupItemListButtonDelete.DataPropertyName = "ColumnItemGroupItemListButtonDelete";
            this.ColumnItemGroupItemListButtonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColumnItemGroupItemListButtonDelete.HeaderText = "";
            this.ColumnItemGroupItemListButtonDelete.Name = "ColumnItemGroupItemListButtonDelete";
            this.ColumnItemGroupItemListButtonDelete.ReadOnly = true;
            this.ColumnItemGroupItemListButtonDelete.Width = 70;
            // 
            // ColumnItemGroupItemListId
            // 
            this.ColumnItemGroupItemListId.DataPropertyName = "ColumnItemGroupItemListId";
            this.ColumnItemGroupItemListId.HeaderText = "Id";
            this.ColumnItemGroupItemListId.Name = "ColumnItemGroupItemListId";
            this.ColumnItemGroupItemListId.ReadOnly = true;
            this.ColumnItemGroupItemListId.Visible = false;
            // 
            // ColumnItemGroupItemListItemId
            // 
            this.ColumnItemGroupItemListItemId.DataPropertyName = "ColumnItemGroupItemListItemId";
            this.ColumnItemGroupItemListItemId.HeaderText = "Item Id";
            this.ColumnItemGroupItemListItemId.Name = "ColumnItemGroupItemListItemId";
            this.ColumnItemGroupItemListItemId.ReadOnly = true;
            this.ColumnItemGroupItemListItemId.Visible = false;
            // 
            // ColumnItemGroupItemListItemDescription
            // 
            this.ColumnItemGroupItemListItemDescription.DataPropertyName = "ColumnItemGroupItemListItemDescription";
            this.ColumnItemGroupItemListItemDescription.HeaderText = "Item Description";
            this.ColumnItemGroupItemListItemDescription.Name = "ColumnItemGroupItemListItemDescription";
            this.ColumnItemGroupItemListItemDescription.ReadOnly = true;
            this.ColumnItemGroupItemListItemDescription.Width = 300;
            // 
            // ColumnItemGroupItemListItemGroupId
            // 
            this.ColumnItemGroupItemListItemGroupId.DataPropertyName = "ColumnItemGroupItemListItemGroupId";
            this.ColumnItemGroupItemListItemGroupId.HeaderText = "Item Group Id";
            this.ColumnItemGroupItemListItemGroupId.Name = "ColumnItemGroupItemListItemGroupId";
            this.ColumnItemGroupItemListItemGroupId.ReadOnly = true;
            this.ColumnItemGroupItemListItemGroupId.Visible = false;
            // 
            // ColumnItemGroupItemListShow
            // 
            this.ColumnItemGroupItemListShow.DataPropertyName = "ColumnItemGroupItemListShow";
            this.ColumnItemGroupItemListShow.HeaderText = "Show";
            this.ColumnItemGroupItemListShow.Name = "ColumnItemGroupItemListShow";
            this.ColumnItemGroupItemListShow.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnItemGroupItemListShow.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.buttonItemGroupItemListPageListFirst);
            this.panel4.Controls.Add(this.buttonItemGroupItemListPageListPrevious);
            this.panel4.Controls.Add(this.buttonItemGroupItemListPageListNext);
            this.panel4.Controls.Add(this.buttonItemGroupItemListPageListLast);
            this.panel4.Controls.Add(this.textBoxItemGroupItemListPageNumber);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 468);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1096, 42);
            this.panel4.TabIndex = 25;
            // 
            // buttonItemGroupItemListPageListFirst
            // 
            this.buttonItemGroupItemListPageListFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonItemGroupItemListPageListFirst.Enabled = false;
            this.buttonItemGroupItemListPageListFirst.FlatAppearance.BorderSize = 0;
            this.buttonItemGroupItemListPageListFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonItemGroupItemListPageListFirst.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonItemGroupItemListPageListFirst.Location = new System.Drawing.Point(10, 9);
            this.buttonItemGroupItemListPageListFirst.Margin = new System.Windows.Forms.Padding(2);
            this.buttonItemGroupItemListPageListFirst.Name = "buttonItemGroupItemListPageListFirst";
            this.buttonItemGroupItemListPageListFirst.Size = new System.Drawing.Size(66, 26);
            this.buttonItemGroupItemListPageListFirst.TabIndex = 23;
            this.buttonItemGroupItemListPageListFirst.TabStop = false;
            this.buttonItemGroupItemListPageListFirst.Text = "First";
            this.buttonItemGroupItemListPageListFirst.UseVisualStyleBackColor = false;
            this.buttonItemGroupItemListPageListFirst.Click += new System.EventHandler(this.buttonItemGroupItemListPageListFirst_Click);
            // 
            // buttonItemGroupItemListPageListPrevious
            // 
            this.buttonItemGroupItemListPageListPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonItemGroupItemListPageListPrevious.Enabled = false;
            this.buttonItemGroupItemListPageListPrevious.FlatAppearance.BorderSize = 0;
            this.buttonItemGroupItemListPageListPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonItemGroupItemListPageListPrevious.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonItemGroupItemListPageListPrevious.Location = new System.Drawing.Point(80, 9);
            this.buttonItemGroupItemListPageListPrevious.Margin = new System.Windows.Forms.Padding(2);
            this.buttonItemGroupItemListPageListPrevious.Name = "buttonItemGroupItemListPageListPrevious";
            this.buttonItemGroupItemListPageListPrevious.Size = new System.Drawing.Size(66, 26);
            this.buttonItemGroupItemListPageListPrevious.TabIndex = 24;
            this.buttonItemGroupItemListPageListPrevious.TabStop = false;
            this.buttonItemGroupItemListPageListPrevious.Text = "Previous";
            this.buttonItemGroupItemListPageListPrevious.UseVisualStyleBackColor = false;
            this.buttonItemGroupItemListPageListPrevious.Click += new System.EventHandler(this.buttonItemGroupItemListPageListPrevious_Click);
            // 
            // buttonItemGroupItemListPageListNext
            // 
            this.buttonItemGroupItemListPageListNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonItemGroupItemListPageListNext.FlatAppearance.BorderSize = 0;
            this.buttonItemGroupItemListPageListNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonItemGroupItemListPageListNext.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonItemGroupItemListPageListNext.Location = new System.Drawing.Point(210, 9);
            this.buttonItemGroupItemListPageListNext.Margin = new System.Windows.Forms.Padding(2);
            this.buttonItemGroupItemListPageListNext.Name = "buttonItemGroupItemListPageListNext";
            this.buttonItemGroupItemListPageListNext.Size = new System.Drawing.Size(66, 26);
            this.buttonItemGroupItemListPageListNext.TabIndex = 26;
            this.buttonItemGroupItemListPageListNext.TabStop = false;
            this.buttonItemGroupItemListPageListNext.Text = "Next";
            this.buttonItemGroupItemListPageListNext.UseVisualStyleBackColor = false;
            this.buttonItemGroupItemListPageListNext.Click += new System.EventHandler(this.buttonItemGroupItemListPageListNext_Click);
            // 
            // buttonItemGroupItemListPageListLast
            // 
            this.buttonItemGroupItemListPageListLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonItemGroupItemListPageListLast.FlatAppearance.BorderSize = 0;
            this.buttonItemGroupItemListPageListLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonItemGroupItemListPageListLast.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonItemGroupItemListPageListLast.Location = new System.Drawing.Point(278, 9);
            this.buttonItemGroupItemListPageListLast.Margin = new System.Windows.Forms.Padding(2);
            this.buttonItemGroupItemListPageListLast.Name = "buttonItemGroupItemListPageListLast";
            this.buttonItemGroupItemListPageListLast.Size = new System.Drawing.Size(66, 26);
            this.buttonItemGroupItemListPageListLast.TabIndex = 26;
            this.buttonItemGroupItemListPageListLast.TabStop = false;
            this.buttonItemGroupItemListPageListLast.Text = "Last";
            this.buttonItemGroupItemListPageListLast.UseVisualStyleBackColor = false;
            this.buttonItemGroupItemListPageListLast.Click += new System.EventHandler(this.buttonItemGroupItemListPageListLast_Click);
            // 
            // textBoxItemGroupItemListPageNumber
            // 
            this.textBoxItemGroupItemListPageNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxItemGroupItemListPageNumber.BackColor = System.Drawing.Color.White;
            this.textBoxItemGroupItemListPageNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxItemGroupItemListPageNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxItemGroupItemListPageNumber.Location = new System.Drawing.Point(150, 13);
            this.textBoxItemGroupItemListPageNumber.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxItemGroupItemListPageNumber.Name = "textBoxItemGroupItemListPageNumber";
            this.textBoxItemGroupItemListPageNumber.ReadOnly = true;
            this.textBoxItemGroupItemListPageNumber.Size = new System.Drawing.Size(55, 16);
            this.textBoxItemGroupItemListPageNumber.TabIndex = 25;
            this.textBoxItemGroupItemListPageNumber.TabStop = false;
            this.textBoxItemGroupItemListPageNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel7.Controls.Add(this.comboBoxItemGroupKitchens);
            this.panel7.Controls.Add(this.label3);
            this.panel7.Controls.Add(this.textBoxItemGroupImagePath);
            this.panel7.Controls.Add(this.label4);
            this.panel7.Controls.Add(this.label2);
            this.panel7.Controls.Add(this.textBoxItemGroup);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Margin = new System.Windows.Forms.Padding(2);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(8);
            this.panel7.Size = new System.Drawing.Size(1096, 106);
            this.panel7.TabIndex = 0;
            // 
            // comboBoxItemGroupKitchens
            // 
            this.comboBoxItemGroupKitchens.FormattingEnabled = true;
            this.comboBoxItemGroupKitchens.Location = new System.Drawing.Point(118, 66);
            this.comboBoxItemGroupKitchens.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxItemGroupKitchens.Name = "comboBoxItemGroupKitchens";
            this.comboBoxItemGroupKitchens.Size = new System.Drawing.Size(222, 27);
            this.comboBoxItemGroupKitchens.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label3.Location = new System.Drawing.Point(11, 69);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 19);
            this.label3.TabIndex = 9;
            this.label3.Text = "Kitchen Report:";
            // 
            // textBoxItemGroupImagePath
            // 
            this.textBoxItemGroupImagePath.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.textBoxItemGroupImagePath.Location = new System.Drawing.Point(118, 38);
            this.textBoxItemGroupImagePath.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxItemGroupImagePath.Name = "textBoxItemGroupImagePath";
            this.textBoxItemGroupImagePath.Size = new System.Drawing.Size(427, 26);
            this.textBoxItemGroupImagePath.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label4.Location = new System.Drawing.Point(32, 40);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 19);
            this.label4.TabIndex = 8;
            this.label4.Text = "Image Path:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.label2.Location = new System.Drawing.Point(31, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "Item Group:";
            // 
            // textBoxItemGroup
            // 
            this.textBoxItemGroup.AcceptsTab = true;
            this.textBoxItemGroup.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.textBoxItemGroup.HideSelection = false;
            this.textBoxItemGroup.Location = new System.Drawing.Point(118, 9);
            this.textBoxItemGroup.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxItemGroup.Name = "textBoxItemGroup";
            this.textBoxItemGroup.Size = new System.Drawing.Size(222, 26);
            this.textBoxItemGroup.TabIndex = 0;
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
            this.buttonLock.Location = new System.Drawing.Point(866, 10);
            this.buttonLock.Margin = new System.Windows.Forms.Padding(2);
            this.buttonLock.Name = "buttonLock";
            this.buttonLock.Size = new System.Drawing.Size(70, 32);
            this.buttonLock.TabIndex = 20;
            this.buttonLock.TabStop = false;
            this.buttonLock.Text = "Lock";
            this.buttonLock.UseVisualStyleBackColor = false;
            this.buttonLock.Click += new System.EventHandler(this.buttonLock_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(50, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "Item Group Detail";
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
            this.buttonClose.TabIndex = 22;
            this.buttonClose.TabStop = false;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
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
            this.buttonUnlock.Location = new System.Drawing.Point(941, 10);
            this.buttonUnlock.Margin = new System.Windows.Forms.Padding(2);
            this.buttonUnlock.Name = "buttonUnlock";
            this.buttonUnlock.Size = new System.Drawing.Size(70, 32);
            this.buttonUnlock.TabIndex = 21;
            this.buttonUnlock.TabStop = false;
            this.buttonUnlock.Text = "Unlock";
            this.buttonUnlock.UseVisualStyleBackColor = false;
            this.buttonUnlock.Click += new System.EventHandler(this.buttonUnlock_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.buttonLock);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.buttonUnlock);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1096, 50);
            this.panel1.TabIndex = 7;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(10, 10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // MstItemGroupDetailForm
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
            this.Name = "MstItemGroupDetailForm";
            this.Text = "MstItemDetailForm";
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItemGroupItemList)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonLock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonUnlock;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button buttonItemGroupItemListPageListFirst;
        private System.Windows.Forms.Button buttonItemGroupItemListPageListPrevious;
        private System.Windows.Forms.Button buttonItemGroupItemListPageListNext;
        private System.Windows.Forms.Button buttonItemGroupItemListPageListLast;
        private System.Windows.Forms.TextBox textBoxItemGroupItemListPageNumber;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxItemGroup;
        private System.Windows.Forms.TextBox textBoxItemGroupImagePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DataGridView dataGridViewItemGroupItemList;
        private System.Windows.Forms.Button buttonSearchItem;
        private System.Windows.Forms.ComboBox comboBoxItemGroupKitchens;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnItemGroupItemListButtonEdit;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnItemGroupItemListButtonDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItemGroupItemListId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItemGroupItemListItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItemGroupItemListItemDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItemGroupItemListItemGroupId;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnItemGroupItemListShow;
    }
}