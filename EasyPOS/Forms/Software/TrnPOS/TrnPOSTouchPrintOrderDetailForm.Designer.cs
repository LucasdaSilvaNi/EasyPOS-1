
namespace EasyPOS.Forms.Software.TrnPOS
{
    partial class TrnPOSTouchPrintOrderDetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnPOSTouchPrintOrderDetailForm));
            this.dataGridViewPrintOrderSalesLineList = new System.Windows.Forms.DataGridView();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.printDialogSelectPrinter = new System.Windows.Forms.PrintDialog();
            this.buttonClose = new System.Windows.Forms.Button();
            this.ColumnSalesLineListPrintOrderId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSalesLineListPrintOrderSalesId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSalesLineListPrintOrderItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSalesLineListPrintOrderPrinted = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnSalesLineListItemPrintOrderDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSalesLineListPrintOrderPreparation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPrintOrderSalesLineList)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewPrintOrderSalesLineList
            // 
            this.dataGridViewPrintOrderSalesLineList.AllowUserToAddRows = false;
            this.dataGridViewPrintOrderSalesLineList.AllowUserToDeleteRows = false;
            this.dataGridViewPrintOrderSalesLineList.AllowUserToResizeRows = false;
            this.dataGridViewPrintOrderSalesLineList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPrintOrderSalesLineList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewPrintOrderSalesLineList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPrintOrderSalesLineList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSalesLineListPrintOrderId,
            this.ColumnSalesLineListPrintOrderSalesId,
            this.ColumnSalesLineListPrintOrderItemId,
            this.ColumnSalesLineListPrintOrderPrinted,
            this.ColumnSalesLineListItemPrintOrderDescription,
            this.ColumnSalesLineListPrintOrderPreparation});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewPrintOrderSalesLineList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewPrintOrderSalesLineList.Location = new System.Drawing.Point(0, 37);
            this.dataGridViewPrintOrderSalesLineList.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewPrintOrderSalesLineList.Name = "dataGridViewPrintOrderSalesLineList";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPrintOrderSalesLineList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewPrintOrderSalesLineList.RowHeadersVisible = false;
            this.dataGridViewPrintOrderSalesLineList.RowTemplate.Height = 24;
            this.dataGridViewPrintOrderSalesLineList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPrintOrderSalesLineList.Size = new System.Drawing.Size(793, 385);
            this.dataGridViewPrintOrderSalesLineList.TabIndex = 28;
            this.dataGridViewPrintOrderSalesLineList.TabStop = false;
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
            this.buttonPrint.Location = new System.Drawing.Point(650, 2);
            this.buttonPrint.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(70, 32);
            this.buttonPrint.TabIndex = 29;
            this.buttonPrint.TabStop = false;
            this.buttonPrint.Text = "Print";
            this.buttonPrint.UseVisualStyleBackColor = false;
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // printDialogSelectPrinter
            // 
            this.printDialogSelectPrinter.UseEXDialog = true;
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
            this.buttonClose.Location = new System.Drawing.Point(723, 2);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(2);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(70, 32);
            this.buttonClose.TabIndex = 30;
            this.buttonClose.TabStop = false;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // ColumnSalesLineListPrintOrderId
            // 
            this.ColumnSalesLineListPrintOrderId.DataPropertyName = "ColumnSalesLineListPrintOrderId";
            this.ColumnSalesLineListPrintOrderId.HeaderText = "Id";
            this.ColumnSalesLineListPrintOrderId.Name = "ColumnSalesLineListPrintOrderId";
            this.ColumnSalesLineListPrintOrderId.ReadOnly = true;
            this.ColumnSalesLineListPrintOrderId.Visible = false;
            // 
            // ColumnSalesLineListPrintOrderSalesId
            // 
            this.ColumnSalesLineListPrintOrderSalesId.DataPropertyName = "ColumnSalesLineListPrintOrderSalesId";
            this.ColumnSalesLineListPrintOrderSalesId.HeaderText = "SalesId";
            this.ColumnSalesLineListPrintOrderSalesId.Name = "ColumnSalesLineListPrintOrderSalesId";
            this.ColumnSalesLineListPrintOrderSalesId.ReadOnly = true;
            this.ColumnSalesLineListPrintOrderSalesId.Visible = false;
            // 
            // ColumnSalesLineListPrintOrderItemId
            // 
            this.ColumnSalesLineListPrintOrderItemId.DataPropertyName = "ColumnSalesLineListPrintOrderItemId";
            this.ColumnSalesLineListPrintOrderItemId.HeaderText = "ItemId";
            this.ColumnSalesLineListPrintOrderItemId.Name = "ColumnSalesLineListPrintOrderItemId";
            this.ColumnSalesLineListPrintOrderItemId.ReadOnly = true;
            this.ColumnSalesLineListPrintOrderItemId.Visible = false;
            // 
            // ColumnSalesLineListPrintOrderPrinted
            // 
            this.ColumnSalesLineListPrintOrderPrinted.DataPropertyName = "ColumnSalesLineListPrintOrderPrinted";
            this.ColumnSalesLineListPrintOrderPrinted.FalseValue = "false";
            this.ColumnSalesLineListPrintOrderPrinted.HeaderText = "Printed";
            this.ColumnSalesLineListPrintOrderPrinted.Name = "ColumnSalesLineListPrintOrderPrinted";
            this.ColumnSalesLineListPrintOrderPrinted.TrueValue = "";
            // 
            // ColumnSalesLineListItemPrintOrderDescription
            // 
            this.ColumnSalesLineListItemPrintOrderDescription.DataPropertyName = "ColumnSalesLineListItemPrintOrderDescription";
            this.ColumnSalesLineListItemPrintOrderDescription.HeaderText = "Item Description";
            this.ColumnSalesLineListItemPrintOrderDescription.Name = "ColumnSalesLineListItemPrintOrderDescription";
            this.ColumnSalesLineListItemPrintOrderDescription.ReadOnly = true;
            this.ColumnSalesLineListItemPrintOrderDescription.Width = 350;
            // 
            // ColumnSalesLineListPrintOrderPreparation
            // 
            this.ColumnSalesLineListPrintOrderPreparation.DataPropertyName = "ColumnSalesLineListPrintOrderPreparation";
            this.ColumnSalesLineListPrintOrderPreparation.HeaderText = "Preparation";
            this.ColumnSalesLineListPrintOrderPreparation.Name = "ColumnSalesLineListPrintOrderPreparation";
            this.ColumnSalesLineListPrintOrderPreparation.Width = 350;
            // 
            // TrnPOSTouchPrintOrderDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 418);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonPrint);
            this.Controls.Add(this.dataGridViewPrintOrderSalesLineList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TrnPOSTouchPrintOrderDetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "POS Touch Print Order Detail Form";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPrintOrderSalesLineList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewPrintOrderSalesLineList;
        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.PrintDialog printDialogSelectPrinter;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSalesLineListPrintOrderId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSalesLineListPrintOrderSalesId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSalesLineListPrintOrderItemId;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnSalesLineListPrintOrderPrinted;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSalesLineListItemPrintOrderDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSalesLineListPrintOrderPreparation;
    }
}