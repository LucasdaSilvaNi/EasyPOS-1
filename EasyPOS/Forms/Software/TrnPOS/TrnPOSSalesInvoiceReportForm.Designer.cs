namespace EasyPOS.Forms.Software.TrnPOS
{
    partial class TrnPOSSalesInvoiceReportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrnPOSSalesInvoiceReportForm));
            this.printDocumentSalesInvoiceReport = new System.Drawing.Printing.PrintDocument();
            this.SuspendLayout();
            // 
            // printDocumentSalesInvoiceReport
            // 
            this.printDocumentSalesInvoiceReport.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocumentSalesInvoiceReport_PrintPage);
            // 
            // TrnPOSSalesInvoiceReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(649, 749);
            this.ControlBox = false;
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximumSize = new System.Drawing.Size(726, 10000);
            this.MinimumSize = new System.Drawing.Size(0, 726);
            this.Name = "TrnPOSSalesInvoiceReportForm";
            this.Text = "Sales Invoice Report";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Drawing.Printing.PrintDocument printDocumentSalesInvoiceReport;
    }
}