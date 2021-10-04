namespace EasyPOS.Forms.Software.TrnDisbursement
{
    partial class TrnDisbursementListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnDisbursementListForm));
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.dateTimePickerDisbursementListFilter = new System.Windows.Forms.DateTimePicker();
            this.buttonDisbursementListPageListLast = new System.Windows.Forms.Button();
            this.textBoxDisbursementListPageNumber = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.dataGridViewDisbursementList = new System.Windows.Forms.DataGridView();
            this.buttonDisbursementListPageListFirst = new System.Windows.Forms.Button();
            this.buttonDisbursementListPageListPrevious = new System.Windows.Forms.Button();
            this.buttonDisbursementListPageListNext = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBoxDisbursementListFilter = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonRefund = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ColumnDisbursementListButtonEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnDisbursementListButtonDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnDisbursementListId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDisbursementListDisbursementDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDisbursementListDisbursementNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDisbursementListDisbursementType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDisbursementListPayType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDisbursementListPayee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDisbursementListAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDisbursementListRemarks = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDisbursementListIsLocked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDisbursementList)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(50, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cash In/Out List";
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
            // dateTimePickerDisbursementListFilter
            // 
            this.dateTimePickerDisbursementListFilter.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerDisbursementListFilter.Location = new System.Drawing.Point(10, 5);
            this.dateTimePickerDisbursementListFilter.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePickerDisbursementListFilter.Name = "dateTimePickerDisbursementListFilter";
            this.dateTimePickerDisbursementListFilter.Size = new System.Drawing.Size(114, 26);
            this.dateTimePickerDisbursementListFilter.TabIndex = 22;
            this.dateTimePickerDisbursementListFilter.TabStop = false;
            this.dateTimePickerDisbursementListFilter.ValueChanged += new System.EventHandler(this.dateTimePickerDisbursementListFilter_ValueChanged);
            // 
            // buttonDisbursementListPageListLast
            // 
            this.buttonDisbursementListPageListLast.FlatAppearance.BorderSize = 0;
            this.buttonDisbursementListPageListLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDisbursementListPageListLast.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonDisbursementListPageListLast.Location = new System.Drawing.Point(278, 9);
            this.buttonDisbursementListPageListLast.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDisbursementListPageListLast.Name = "buttonDisbursementListPageListLast";
            this.buttonDisbursementListPageListLast.Size = new System.Drawing.Size(66, 26);
            this.buttonDisbursementListPageListLast.TabIndex = 16;
            this.buttonDisbursementListPageListLast.TabStop = false;
            this.buttonDisbursementListPageListLast.Text = "Last";
            this.buttonDisbursementListPageListLast.UseVisualStyleBackColor = false;
            this.buttonDisbursementListPageListLast.Click += new System.EventHandler(this.buttonDisbursementListPageListLast_Click);
            // 
            // textBoxDisbursementListPageNumber
            // 
            this.textBoxDisbursementListPageNumber.BackColor = System.Drawing.Color.White;
            this.textBoxDisbursementListPageNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxDisbursementListPageNumber.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxDisbursementListPageNumber.Location = new System.Drawing.Point(150, 13);
            this.textBoxDisbursementListPageNumber.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxDisbursementListPageNumber.Name = "textBoxDisbursementListPageNumber";
            this.textBoxDisbursementListPageNumber.ReadOnly = true;
            this.textBoxDisbursementListPageNumber.Size = new System.Drawing.Size(55, 16);
            this.textBoxDisbursementListPageNumber.TabIndex = 17;
            this.textBoxDisbursementListPageNumber.TabStop = false;
            this.textBoxDisbursementListPageNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            // dataGridViewDisbursementList
            // 
            this.dataGridViewDisbursementList.AllowUserToAddRows = false;
            this.dataGridViewDisbursementList.AllowUserToDeleteRows = false;
            this.dataGridViewDisbursementList.AllowUserToResizeRows = false;
            this.dataGridViewDisbursementList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewDisbursementList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewDisbursementList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDisbursementList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDisbursementListButtonEdit,
            this.ColumnDisbursementListButtonDelete,
            this.ColumnDisbursementListId,
            this.ColumnDisbursementListDisbursementDate,
            this.ColumnDisbursementListDisbursementNumber,
            this.ColumnDisbursementListDisbursementType,
            this.ColumnDisbursementListPayType,
            this.ColumnDisbursementListPayee,
            this.ColumnDisbursementListAmount,
            this.ColumnDisbursementListRemarks,
            this.ColumnDisbursementListIsLocked});
            this.dataGridViewDisbursementList.Location = new System.Drawing.Point(10, 34);
            this.dataGridViewDisbursementList.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewDisbursementList.MultiSelect = false;
            this.dataGridViewDisbursementList.Name = "dataGridViewDisbursementList";
            this.dataGridViewDisbursementList.ReadOnly = true;
            this.dataGridViewDisbursementList.RowTemplate.Height = 24;
            this.dataGridViewDisbursementList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDisbursementList.Size = new System.Drawing.Size(1077, 429);
            this.dataGridViewDisbursementList.TabIndex = 20;
            this.dataGridViewDisbursementList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDisbursementList_CellClick);
            // 
            // buttonDisbursementListPageListFirst
            // 
            this.buttonDisbursementListPageListFirst.Enabled = false;
            this.buttonDisbursementListPageListFirst.FlatAppearance.BorderSize = 0;
            this.buttonDisbursementListPageListFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDisbursementListPageListFirst.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonDisbursementListPageListFirst.Location = new System.Drawing.Point(10, 9);
            this.buttonDisbursementListPageListFirst.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDisbursementListPageListFirst.Name = "buttonDisbursementListPageListFirst";
            this.buttonDisbursementListPageListFirst.Size = new System.Drawing.Size(66, 26);
            this.buttonDisbursementListPageListFirst.TabIndex = 13;
            this.buttonDisbursementListPageListFirst.TabStop = false;
            this.buttonDisbursementListPageListFirst.Text = "First";
            this.buttonDisbursementListPageListFirst.UseVisualStyleBackColor = false;
            this.buttonDisbursementListPageListFirst.Click += new System.EventHandler(this.buttonDisbursementListPageListFirst_Click);
            // 
            // buttonDisbursementListPageListPrevious
            // 
            this.buttonDisbursementListPageListPrevious.Enabled = false;
            this.buttonDisbursementListPageListPrevious.FlatAppearance.BorderSize = 0;
            this.buttonDisbursementListPageListPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDisbursementListPageListPrevious.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonDisbursementListPageListPrevious.Location = new System.Drawing.Point(80, 9);
            this.buttonDisbursementListPageListPrevious.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDisbursementListPageListPrevious.Name = "buttonDisbursementListPageListPrevious";
            this.buttonDisbursementListPageListPrevious.Size = new System.Drawing.Size(66, 26);
            this.buttonDisbursementListPageListPrevious.TabIndex = 14;
            this.buttonDisbursementListPageListPrevious.TabStop = false;
            this.buttonDisbursementListPageListPrevious.Text = "Previous";
            this.buttonDisbursementListPageListPrevious.UseVisualStyleBackColor = false;
            this.buttonDisbursementListPageListPrevious.Click += new System.EventHandler(this.buttonDisbursementListPageListPrevious_Click);
            // 
            // buttonDisbursementListPageListNext
            // 
            this.buttonDisbursementListPageListNext.FlatAppearance.BorderSize = 0;
            this.buttonDisbursementListPageListNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDisbursementListPageListNext.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonDisbursementListPageListNext.Location = new System.Drawing.Point(210, 9);
            this.buttonDisbursementListPageListNext.Margin = new System.Windows.Forms.Padding(2);
            this.buttonDisbursementListPageListNext.Name = "buttonDisbursementListPageListNext";
            this.buttonDisbursementListPageListNext.Size = new System.Drawing.Size(66, 26);
            this.buttonDisbursementListPageListNext.TabIndex = 15;
            this.buttonDisbursementListPageListNext.TabStop = false;
            this.buttonDisbursementListPageListNext.Text = "Next";
            this.buttonDisbursementListPageListNext.UseVisualStyleBackColor = false;
            this.buttonDisbursementListPageListNext.Click += new System.EventHandler(this.buttonDisbursementListPageListNext_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.buttonDisbursementListPageListFirst);
            this.panel3.Controls.Add(this.buttonDisbursementListPageListPrevious);
            this.panel3.Controls.Add(this.buttonDisbursementListPageListNext);
            this.panel3.Controls.Add(this.buttonDisbursementListPageListLast);
            this.panel3.Controls.Add(this.textBoxDisbursementListPageNumber);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 468);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1096, 42);
            this.panel3.TabIndex = 21;
            // 
            // textBoxDisbursementListFilter
            // 
            this.textBoxDisbursementListFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDisbursementListFilter.Location = new System.Drawing.Point(127, 5);
            this.textBoxDisbursementListFilter.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxDisbursementListFilter.Name = "textBoxDisbursementListFilter";
            this.textBoxDisbursementListFilter.Size = new System.Drawing.Size(960, 26);
            this.textBoxDisbursementListFilter.TabIndex = 19;
            this.textBoxDisbursementListFilter.TabStop = false;
            this.textBoxDisbursementListFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxDisbursementListFilter_KeyDown);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dateTimePickerDisbursementListFilter);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.dataGridViewDisbursementList);
            this.panel2.Controls.Add(this.textBoxDisbursementListFilter);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 50);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1096, 510);
            this.panel2.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.buttonRefund);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Controls.Add(this.buttonAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1096, 50);
            this.panel1.TabIndex = 8;
            // 
            // buttonRefund
            // 
            this.buttonRefund.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefund.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonRefund.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(166)))), ((int)(((byte)(240)))));
            this.buttonRefund.FlatAppearance.BorderSize = 0;
            this.buttonRefund.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefund.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRefund.ForeColor = System.Drawing.Color.White;
            this.buttonRefund.Location = new System.Drawing.Point(865, 10);
            this.buttonRefund.Margin = new System.Windows.Forms.Padding(2);
            this.buttonRefund.Name = "buttonRefund";
            this.buttonRefund.Size = new System.Drawing.Size(72, 32);
            this.buttonRefund.TabIndex = 25;
            this.buttonRefund.TabStop = false;
            this.buttonRefund.Text = "Refund";
            this.buttonRefund.UseVisualStyleBackColor = false;
            this.buttonRefund.Click += new System.EventHandler(this.buttonRefund_Click);
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
            // ColumnDisbursementListButtonEdit
            // 
            this.ColumnDisbursementListButtonEdit.DataPropertyName = "ColumnDisbursementListButtonEdit";
            this.ColumnDisbursementListButtonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColumnDisbursementListButtonEdit.HeaderText = "";
            this.ColumnDisbursementListButtonEdit.Name = "ColumnDisbursementListButtonEdit";
            this.ColumnDisbursementListButtonEdit.ReadOnly = true;
            this.ColumnDisbursementListButtonEdit.Width = 70;
            // 
            // ColumnDisbursementListButtonDelete
            // 
            this.ColumnDisbursementListButtonDelete.DataPropertyName = "ColumnDisbursementListButtonDelete";
            this.ColumnDisbursementListButtonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColumnDisbursementListButtonDelete.HeaderText = "";
            this.ColumnDisbursementListButtonDelete.Name = "ColumnDisbursementListButtonDelete";
            this.ColumnDisbursementListButtonDelete.ReadOnly = true;
            this.ColumnDisbursementListButtonDelete.Width = 70;
            // 
            // ColumnDisbursementListId
            // 
            this.ColumnDisbursementListId.DataPropertyName = "ColumnDisbursementListId";
            this.ColumnDisbursementListId.HeaderText = "Id";
            this.ColumnDisbursementListId.Name = "ColumnDisbursementListId";
            this.ColumnDisbursementListId.ReadOnly = true;
            this.ColumnDisbursementListId.Visible = false;
            // 
            // ColumnDisbursementListDisbursementDate
            // 
            this.ColumnDisbursementListDisbursementDate.DataPropertyName = "ColumnDisbursementListDisbursementDate";
            this.ColumnDisbursementListDisbursementDate.HeaderText = "Cash In/Out Date";
            this.ColumnDisbursementListDisbursementDate.Name = "ColumnDisbursementListDisbursementDate";
            this.ColumnDisbursementListDisbursementDate.ReadOnly = true;
            this.ColumnDisbursementListDisbursementDate.Visible = false;
            this.ColumnDisbursementListDisbursementDate.Width = 95;
            // 
            // ColumnDisbursementListDisbursementNumber
            // 
            this.ColumnDisbursementListDisbursementNumber.DataPropertyName = "ColumnDisbursementListDisbursementNumber";
            this.ColumnDisbursementListDisbursementNumber.HeaderText = "Cash In/Out No.";
            this.ColumnDisbursementListDisbursementNumber.Name = "ColumnDisbursementListDisbursementNumber";
            this.ColumnDisbursementListDisbursementNumber.ReadOnly = true;
            this.ColumnDisbursementListDisbursementNumber.Width = 170;
            // 
            // ColumnDisbursementListDisbursementType
            // 
            this.ColumnDisbursementListDisbursementType.DataPropertyName = "ColumnDisbursementListDisbursementType";
            this.ColumnDisbursementListDisbursementType.HeaderText = "Type";
            this.ColumnDisbursementListDisbursementType.Name = "ColumnDisbursementListDisbursementType";
            this.ColumnDisbursementListDisbursementType.ReadOnly = true;
            // 
            // ColumnDisbursementListPayType
            // 
            this.ColumnDisbursementListPayType.DataPropertyName = "ColumnDisbursementListPayType";
            this.ColumnDisbursementListPayType.HeaderText = "Pay Type";
            this.ColumnDisbursementListPayType.Name = "ColumnDisbursementListPayType";
            this.ColumnDisbursementListPayType.ReadOnly = true;
            this.ColumnDisbursementListPayType.Width = 120;
            // 
            // ColumnDisbursementListPayee
            // 
            this.ColumnDisbursementListPayee.DataPropertyName = "ColumnDisbursementListPayee";
            this.ColumnDisbursementListPayee.HeaderText = "Payee";
            this.ColumnDisbursementListPayee.Name = "ColumnDisbursementListPayee";
            this.ColumnDisbursementListPayee.ReadOnly = true;
            this.ColumnDisbursementListPayee.Width = 200;
            // 
            // ColumnDisbursementListAmount
            // 
            this.ColumnDisbursementListAmount.DataPropertyName = "ColumnDisbursementListAmount";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ColumnDisbursementListAmount.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnDisbursementListAmount.HeaderText = "Amount";
            this.ColumnDisbursementListAmount.Name = "ColumnDisbursementListAmount";
            this.ColumnDisbursementListAmount.ReadOnly = true;
            this.ColumnDisbursementListAmount.Width = 120;
            // 
            // ColumnDisbursementListRemarks
            // 
            this.ColumnDisbursementListRemarks.DataPropertyName = "ColumnDisbursementListRemarks";
            this.ColumnDisbursementListRemarks.HeaderText = "Remarks";
            this.ColumnDisbursementListRemarks.Name = "ColumnDisbursementListRemarks";
            this.ColumnDisbursementListRemarks.ReadOnly = true;
            this.ColumnDisbursementListRemarks.Width = 300;
            // 
            // ColumnDisbursementListIsLocked
            // 
            this.ColumnDisbursementListIsLocked.DataPropertyName = "ColumnDisbursementListIsLocked";
            this.ColumnDisbursementListIsLocked.HeaderText = "L";
            this.ColumnDisbursementListIsLocked.Name = "ColumnDisbursementListIsLocked";
            this.ColumnDisbursementListIsLocked.ReadOnly = true;
            this.ColumnDisbursementListIsLocked.Width = 35;
            // 
            // TrnDisbursementListForm
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
            this.Name = "TrnDisbursementListForm";
            this.Text = "TrnDisbursementListForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDisbursementList)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.DateTimePicker dateTimePickerDisbursementListFilter;
        private System.Windows.Forms.Button buttonDisbursementListPageListLast;
        private System.Windows.Forms.TextBox textBoxDisbursementListPageNumber;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.DataGridView dataGridViewDisbursementList;
        private System.Windows.Forms.Button buttonDisbursementListPageListFirst;
        private System.Windows.Forms.Button buttonDisbursementListPageListPrevious;
        private System.Windows.Forms.Button buttonDisbursementListPageListNext;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox textBoxDisbursementListFilter;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonRefund;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnDisbursementListButtonEdit;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnDisbursementListButtonDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDisbursementListId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDisbursementListDisbursementDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDisbursementListDisbursementNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDisbursementListDisbursementType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDisbursementListPayType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDisbursementListPayee;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDisbursementListAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDisbursementListRemarks;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnDisbursementListIsLocked;
    }
}