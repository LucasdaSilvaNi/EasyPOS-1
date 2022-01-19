using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.RepSalesReport
{
    public partial class RepAccountsReceivableSummaryReportForm : Form
    {
        public List<Entities.DgvRepSalesReportAccountsReceivableSummaryReportListEntity> salesList;
        public BindingSource dataSalesListSource = new BindingSource();
        public PagedList<Entities.DgvRepSalesReportAccountsReceivableSummaryReportListEntity> pageList;
        public Int32 pageNumber = 1;
        public Int32 pageSize = 50;

        public DateTime dateAsOf;
        public Int32 customerId;

        public RepAccountsReceivableSummaryReportForm(DateTime _dateAsOf, Int32 _customerId)
        {
            InitializeComponent();

            dateAsOf = _dateAsOf;
            customerId = _customerId;

            GetSalesSummaryListDataSource();
            CreateSalesSummaryListDataGrid();
        }

        public List<Entities.DgvRepSalesReportAccountsReceivableSummaryReportListEntity> GetSalesSummaryListData(DateTime dateAsOf, Int32 customerId)
        {
            List<Entities.DgvRepSalesReportAccountsReceivableSummaryReportListEntity> rowList = new List<Entities.DgvRepSalesReportAccountsReceivableSummaryReportListEntity>();

            Controllers.RepSalesReportController repAccountsReceivableSummaryReportController = new Controllers.RepSalesReportController();

            var salesList = repAccountsReceivableSummaryReportController.AccountsReceivableSummaryReport(dateAsOf, customerId);
            if (salesList.Any())
            {
                Decimal totalBalance = 0;

                var row = from d in salesList
                          select new Entities.DgvRepSalesReportAccountsReceivableSummaryReportListEntity
                          {
                              ColumnCustomer = d.ColumnCustomer,
                              ColumnTerm = d.ColumnTerm,
                              ColumnCreditLimit = d.ColumnCreditLimit,
                              ColumnSalesNumber = d.ColumnSalesNumber,
                              ColumnSalesDate = d.ColumnSalesDate,
                              ColumnSalesAmount = d.ColumnSalesAmount,
                              ColumnPaymentAmount = d.ColumnPaymentAmount,
                              ColumnBalanceAmount = d.ColumnBalanceAmount,
                              ColumnDueDate = d.ColumnDueDate,
                              ColumnCurrent = d.ColumnCurrent,
                              Column30Days = d.Column30Days,
                              Column60Days = d.Column60Days,
                              Column90Days = d.Column90Days,
                              Column120Days = d.Column120Days,
                          };

                rowList = row.ToList();

                totalBalance = row.Sum(d => Convert.ToDecimal(d.ColumnBalanceAmount));
                textBoxTotalAmount.Text = totalBalance.ToString("#,##0.00");
            }
            else
            {
                textBoxTotalAmount.Text = "0.00";
            }

            return rowList;
        }

        public void GetSalesSummaryListDataSource()
        {
            salesList = GetSalesSummaryListData(dateAsOf, customerId);
            if (salesList.Any())
            {

                pageList = new PagedList<Entities.DgvRepSalesReportAccountsReceivableSummaryReportListEntity>(salesList, pageNumber, pageSize);

                if (pageList.PageCount == 1)
                {
                    buttonSalesListPageListFirst.Enabled = false;
                    buttonSalesListPageListPrevious.Enabled = false;
                    buttonSalesListPageListNext.Enabled = false;
                    buttonSalesListPageListLast.Enabled = false;
                }
                else if (pageNumber == 1)
                {
                    buttonSalesListPageListFirst.Enabled = false;
                    buttonSalesListPageListPrevious.Enabled = false;
                    buttonSalesListPageListNext.Enabled = true;
                    buttonSalesListPageListLast.Enabled = true;
                }
                else if (pageNumber == pageList.PageCount)
                {
                    buttonSalesListPageListFirst.Enabled = true;
                    buttonSalesListPageListPrevious.Enabled = true;
                    buttonSalesListPageListNext.Enabled = false;
                    buttonSalesListPageListLast.Enabled = false;
                }
                else
                {
                    buttonSalesListPageListFirst.Enabled = true;
                    buttonSalesListPageListPrevious.Enabled = true;
                    buttonSalesListPageListNext.Enabled = true;
                    buttonSalesListPageListLast.Enabled = true;
                }

                textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
                dataSalesListSource.DataSource = pageList;
            }
            else
            {
                buttonSalesListPageListFirst.Enabled = false;
                buttonSalesListPageListPrevious.Enabled = false;
                buttonSalesListPageListNext.Enabled = false;
                buttonSalesListPageListLast.Enabled = false;

                dataSalesListSource.Clear();
                textBoxPageNumber.Text = "0 / 0";
            }
        }

        public void CreateSalesSummaryListDataGrid()
        {
            dataGridViewAccountsReceivableSummaryReport.DataSource = dataSalesListSource;
        }

        private void buttonSalesListPageListFirst_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvRepSalesReportAccountsReceivableSummaryReportListEntity>(salesList, 1, pageSize);
            dataSalesListSource.DataSource = pageList;

            buttonSalesListPageListFirst.Enabled = false;
            buttonSalesListPageListPrevious.Enabled = false;
            buttonSalesListPageListNext.Enabled = true;
            buttonSalesListPageListLast.Enabled = true;

            pageNumber = 1;
            textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
        }

        private void buttonSalesListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (pageList.HasPreviousPage == true)
            {
                pageList = new PagedList<Entities.DgvRepSalesReportAccountsReceivableSummaryReportListEntity>(salesList, --pageNumber, pageSize);
                dataSalesListSource.DataSource = pageList;
            }

            buttonSalesListPageListNext.Enabled = true;
            buttonSalesListPageListLast.Enabled = true;

            if (pageNumber == 1)
            {
                buttonSalesListPageListFirst.Enabled = false;
                buttonSalesListPageListPrevious.Enabled = false;
            }

            textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
        }

        private void buttonSalesListPageListNext_Click(object sender, EventArgs e)
        {
            if (pageList.HasNextPage == true)
            {
                pageList = new PagedList<Entities.DgvRepSalesReportAccountsReceivableSummaryReportListEntity>(salesList, ++pageNumber, pageSize);
                dataSalesListSource.DataSource = pageList;
            }

            buttonSalesListPageListFirst.Enabled = true;
            buttonSalesListPageListPrevious.Enabled = true;

            if (pageNumber == pageList.PageCount)
            {
                buttonSalesListPageListNext.Enabled = false;
                buttonSalesListPageListLast.Enabled = false;
            }

            textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
        }

        private void buttonSalesListPageListLast_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvRepSalesReportAccountsReceivableSummaryReportListEntity>(salesList, pageList.PageCount, pageSize);
            dataSalesListSource.DataSource = pageList;

            buttonSalesListPageListFirst.Enabled = true;
            buttonSalesListPageListPrevious.Enabled = true;
            buttonSalesListPageListNext.Enabled = false;
            buttonSalesListPageListLast.Enabled = false;

            pageNumber = pageList.PageCount;
            textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
        }

        private void buttonClose_OnClick(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonGenerateCSV_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = folderBrowserDialogGenerateCSV.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    StringBuilder csv = new StringBuilder();
                    String[] header = { "Customer", "Term", "Credit Limit", "Sales No.", "Sales Date", "Sales Amount", "Payment", "Balance", "Due Date", "Current", "30 Days", "60 Days", "90 Days", "120 Days" };
                    csv.AppendLine(String.Join(",", header));

                    if (salesList.Any())
                    {
                        foreach (var sales in salesList)
                        {
                            String[] data = {
                                sales.ColumnCustomer.Replace("," , ""),
                                sales.ColumnTerm,
                                sales.ColumnCreditLimit.Replace("," , ""),
                                sales.ColumnSalesNumber,
                                sales.ColumnSalesDate,
                                sales.ColumnSalesAmount.Replace("," , ""),
                                sales.ColumnPaymentAmount.Replace("," , ""),
                                sales.ColumnBalanceAmount.Replace("," , ""),
                                sales.ColumnDueDate,
                                sales.ColumnCurrent.Replace("," , ""),
                                sales.Column30Days.Replace("," , ""),
                                sales.Column60Days.Replace("," , ""),
                                sales.Column90Days.Replace("," , ""),
                                sales.Column120Days.Replace("," , "")
                            };

                            csv.AppendLine(String.Join(",", data));
                        }
                    }

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\AccountsReceivableSummaryReport_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("utf-8"));

                    MessageBox.Show("Generate CSV Successful!", "Generate CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            new RepAccountsReceivableReportPDFForm(dateAsOf, customerId);
        }
    }
}
