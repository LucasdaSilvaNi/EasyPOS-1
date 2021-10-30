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
    public partial class RepCashInOutSummaryReportForm : Form
    {
        public List<Entities.DgvRepRemittanceReportCashInOutSummaryReportListEntity> disbursementList;
        public BindingSource dataSalesListSource = new BindingSource();
        public PagedList<Entities.DgvRepRemittanceReportCashInOutSummaryReportListEntity> pageList;
        public Int32 pageNumber = 1;
        public Int32 pageSize = 50;

        public DateTime dateStart;
        public DateTime dateEnd;
        public Int32 filterTerminalId;

        public RepCashInOutSummaryReportForm(DateTime startDate, DateTime endDate, Int32 terminalId)
        {
            InitializeComponent();

            dateStart = startDate;
            dateEnd = endDate;
            filterTerminalId = terminalId;

            GetSalesSummaryListDataSource();
            CreateSalesSummaryListDataGrid();
        }

        public List<Entities.DgvRepRemittanceReportCashInOutSummaryReportListEntity> GetSalesSummaryListData(DateTime startDate, DateTime endDate, Int32 terminalId)
        {
            List<Entities.DgvRepRemittanceReportCashInOutSummaryReportListEntity> rowList = new List<Entities.DgvRepRemittanceReportCashInOutSummaryReportListEntity>();

            Controllers.RepRemittanceReportController repRemittanceReportController = new Controllers.RepRemittanceReportController();

            var disbursementList = repRemittanceReportController.DisbursementSummaryReport(startDate, endDate, terminalId);
            if (disbursementList.Any())
            {
                Decimal totalAmount = 0;
                var row = from d in disbursementList
                          select new Entities.DgvRepRemittanceReportCashInOutSummaryReportListEntity
                          {
                              ColumnId = d.Id.ToString(),
                              ColumnDisbursementDate = d.DisbursementDate,
                              ColumnDisbursementNumber = d.DisbursementNumber,
                              ColumnDisbursementType = d.DisbursementType,
                              ColumnRemarks = d.Remarks,
                              ColumnPayType = d.PayType,
                              ColumnUser = d.User,
                              ColumnAmount = d.Amount.ToString("#,##0.00")
                          };

                totalAmount = disbursementList.Sum(d => d.Amount);
                textBoxTotalAmount.Text = totalAmount.ToString("#,##0.00");

                rowList = row.ToList();

            }
            return rowList;
        }

        public void GetSalesSummaryListDataSource()
        {
            disbursementList = GetSalesSummaryListData(dateStart, dateEnd, filterTerminalId);
            if (disbursementList.Any())
            {

                pageList = new PagedList<Entities.DgvRepRemittanceReportCashInOutSummaryReportListEntity>(disbursementList, pageNumber, pageSize);

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
            dataGridViewSalesSummaryReport.DataSource = dataSalesListSource;
        }

        private void buttonSalesListPageListFirst_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvRepRemittanceReportCashInOutSummaryReportListEntity>(disbursementList, 1, pageSize);
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
                pageList = new PagedList<Entities.DgvRepRemittanceReportCashInOutSummaryReportListEntity>(disbursementList, --pageNumber, pageSize);
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
                pageList = new PagedList<Entities.DgvRepRemittanceReportCashInOutSummaryReportListEntity>(disbursementList, ++pageNumber, pageSize);
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
            pageList = new PagedList<Entities.DgvRepRemittanceReportCashInOutSummaryReportListEntity>(disbursementList, pageList.PageCount, pageSize);
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
                    String[] header = { "Cash In/Out Date", "Cash In/Out Number", "Cash In/Out Type", "Pay Type", "User", "Amount" };
                    csv.AppendLine(String.Join(",", header));

                    if (disbursementList.Any())
                    {
                        foreach (var disbursements in disbursementList)
                        {
                            String[] data = {
                                disbursements.ColumnDisbursementDate.Replace(",", ""),
                                disbursements.ColumnDisbursementNumber.Replace(",", ""),
                                disbursements.ColumnDisbursementType.Replace(",", ""),
                                disbursements.ColumnPayType.Replace(",", ""),
                                disbursements.ColumnUser.Replace(",", ""),
                                disbursements.ColumnAmount.Replace("," , ""),
                            };

                            csv.AppendLine(String.Join(",", data));
                        }
                    }

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\CashInOutSummaryReport_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("utf-8"));

                    MessageBox.Show("Generate CSV Successful!", "Generate CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
