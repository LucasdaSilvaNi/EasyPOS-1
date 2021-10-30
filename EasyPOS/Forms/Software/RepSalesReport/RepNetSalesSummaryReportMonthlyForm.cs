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
    public partial class RepNetSalesSummaryReportMonthlyForm : Form
    {
        public List<Entities.DgvRepNetSalesSummaryReportMonthlyEntity> netSalesSummaryList;
        public BindingSource dataNetSalesSummaryListSource = new BindingSource();
        public PagedList<Entities.DgvRepNetSalesSummaryReportMonthlyEntity> pageList;

        public Int32 pageNumber = 1;
        public Int32 pageSize = 50;

        public DateTime dateStart;
        public DateTime dateEnd;
        public RepNetSalesSummaryReportMonthlyForm(DateTime startDate, DateTime endDate)
        {
            dateStart = startDate;
            dateEnd = endDate;
            InitializeComponent();
            GetNetSalesSummaryDataSource();
            GetNetSalesSummaryListDataGridSource();
        }

        public String getMonth(Int32 number)
        {
            String returnMonth = "";

            switch(number)
            {
                case 1:
                    returnMonth = "January";
                    break;
                case 2:
                    returnMonth = "Febraury";
                    break;
                case 3:
                    returnMonth = "March";
                    break;
                case 4:
                    returnMonth = "April";
                    break;
                case 5:
                    returnMonth = "May";
                    break;
                case 6:
                    returnMonth = "June";
                    break;
                case 7:
                    returnMonth = "July";
                    break;
                case 8:
                    returnMonth = "August";
                    break;
                case 9:
                    returnMonth = "September";
                    break;
                case 10:
                    returnMonth = "October";
                    break;
                case 11:
                    returnMonth = "November";
                    break;
                case 12:
                    returnMonth = "December";
                    break;
                default:
                    break;
            }

            return returnMonth;
        }

        public List<Entities.DgvRepNetSalesSummaryReportMonthlyEntity> NetSalesSummaryList(DateTime startDate, DateTime endDate)
        {
            List<Entities.DgvRepNetSalesSummaryReportMonthlyEntity> rowList = new List<Entities.DgvRepNetSalesSummaryReportMonthlyEntity>();
            Controllers.RepSalesReportController repSalesDetailReportController = new Controllers.RepSalesReportController();
            var netSalesSummaryList = repSalesDetailReportController.GetNetSalesSummaryReportMonthly(startDate, endDate);
            if (netSalesSummaryList.Any())
            {
                var row = from d in netSalesSummaryList
                          select new Entities.DgvRepNetSalesSummaryReportMonthlyEntity
                          {
                              ColumnNetSalesSummaryMonth = getMonth(d.Month),
                              ColumnNetSalesSummaryYear = d.Year.ToString(),
                              ColumnNetSalesSummaryCustomerCount = d.CustomerCount.ToString("#,##"),
                              ColumnNetSalesSummaryQuantity = d.Quantity.ToString("#,##.00"),
                              ColumnNetSalesSummaryCostAmount = d.CostAmount.ToString("#,##.00"),
                              ColumnNetSalesSummarySalesAmount = d.SalesAmount.ToString("#,##.00"),
                              ColumnNetSalesSummaryMarginAmount = d.MarginAmount.ToString("#,##.00"),
                              ColumnNetSalesSummaryPercentage = d.Percentage.ToString("#,##.00")
                          };

                rowList = row.ToList();
            }
            return rowList;
        }
        public void GetNetSalesSummaryDataSource()
        {
            netSalesSummaryList = NetSalesSummaryList(dateStart, dateEnd);
            if (netSalesSummaryList.Any())
            {

                pageList = new PagedList<Entities.DgvRepNetSalesSummaryReportMonthlyEntity>(netSalesSummaryList, pageNumber, pageSize);

                if (pageList.PageCount == 1)
                {
                    buttonPageListFirst.Enabled = false;
                    buttonPageListPrevious.Enabled = false;
                    buttonPageListNext.Enabled = false;
                    buttonPageListLast.Enabled = false;
                }
                else if (pageNumber == 1)
                {
                    buttonPageListFirst.Enabled = false;
                    buttonPageListPrevious.Enabled = false;
                    buttonPageListNext.Enabled = true;
                    buttonPageListLast.Enabled = true;
                }
                else if (pageNumber == pageList.PageCount)
                {
                    buttonPageListFirst.Enabled = true;
                    buttonPageListPrevious.Enabled = true;
                    buttonPageListNext.Enabled = false;
                    buttonPageListLast.Enabled = false;
                }
                else
                {
                    buttonPageListFirst.Enabled = true;
                    buttonPageListPrevious.Enabled = true;
                    buttonPageListNext.Enabled = true;
                    buttonPageListLast.Enabled = true;
                }

                textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
                dataNetSalesSummaryListSource.DataSource = pageList;
            }
            else
            {
                buttonPageListFirst.Enabled = false;
                buttonPageListPrevious.Enabled = false;
                buttonPageListNext.Enabled = false;
                buttonPageListLast.Enabled = false;

                dataNetSalesSummaryListSource.Clear();
                textBoxPageNumber.Text = "0 / 0";
            }
        }
        public void GetNetSalesSummaryListDataGridSource()
        {
            dataGridViewNetSalesSummaryMonthlyReport.DataSource = dataNetSalesSummaryListSource;

        }
        private void buttonPageListFirst_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvRepNetSalesSummaryReportMonthlyEntity>(netSalesSummaryList, 1, pageSize);
            dataNetSalesSummaryListSource.DataSource = pageList;

            buttonPageListFirst.Enabled = false;
            buttonPageListPrevious.Enabled = false;
            buttonPageListNext.Enabled = true;
            buttonPageListLast.Enabled = true;

            pageNumber = 1;
            textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
        }
        private void buttonPageListPrevious_Click(object sender, EventArgs e)
        {
            if (pageList.HasPreviousPage == true)
            {
                pageList = new PagedList<Entities.DgvRepNetSalesSummaryReportMonthlyEntity>(netSalesSummaryList, --pageNumber, pageSize);
                dataNetSalesSummaryListSource.DataSource = pageList;
            }

            buttonPageListNext.Enabled = true;
            buttonPageListLast.Enabled = true;

            if (pageNumber == 1)
            {
                buttonPageListFirst.Enabled = false;
                buttonPageListPrevious.Enabled = false;
            }

            textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
        }
        private void buttonPageListNext_Click(object sender, EventArgs e)
        {
            if (pageList.HasNextPage == true)
            {
                pageList = new PagedList<Entities.DgvRepNetSalesSummaryReportMonthlyEntity>(netSalesSummaryList, ++pageNumber, pageSize);
                dataNetSalesSummaryListSource.DataSource = pageList;
            }

            buttonPageListFirst.Enabled = true;
            buttonPageListPrevious.Enabled = true;

            if (pageNumber == pageList.PageCount)
            {
                buttonPageListNext.Enabled = false;
                buttonPageListLast.Enabled = false;
            }

            textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
        }

        private void buttonPageListLast_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvRepNetSalesSummaryReportMonthlyEntity>(netSalesSummaryList, pageList.PageCount, pageSize);
            dataNetSalesSummaryListSource.DataSource = pageList;

            buttonPageListFirst.Enabled = true;
            buttonPageListPrevious.Enabled = true;
            buttonPageListNext.Enabled = false;
            buttonPageListLast.Enabled = false;

            pageNumber = pageList.PageCount;
            textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
        }

        private void buttonClose_Click(object sender, EventArgs e)
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
                    String[] header = { "Month", "Year", "Customer Count", "Quantity", "Cost Amount", "Sales Amount", "Margin Amount", "%" };
                    csv.AppendLine(String.Join(",", header));

                    if (netSalesSummaryList.Any())
                    {
                        foreach (var sales in netSalesSummaryList)
                        {
                            String[] data = {
                                sales.ColumnNetSalesSummaryMonth.Replace(",", String.Empty).Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty),
                                sales.ColumnNetSalesSummaryYear.Replace(",", String.Empty).Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty),
                                sales.ColumnNetSalesSummaryCustomerCount.Replace(",", String.Empty).Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty),
                                sales.ColumnNetSalesSummaryQuantity.Replace(",", String.Empty).Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty),
                                sales.ColumnNetSalesSummaryCostAmount.Replace(",", String.Empty).Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty),
                                sales.ColumnNetSalesSummarySalesAmount.Replace(",", String.Empty).Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty),
                                sales.ColumnNetSalesSummaryMarginAmount.Replace(",", String.Empty).Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty),
                                sales.ColumnNetSalesSummaryPercentage.Replace(",", String.Empty).Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty),

                            };

                            csv.AppendLine(String.Join(",", data));
                        }
                    }

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\NetSalesSummaryMonthlyReport_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("utf-8"));

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
