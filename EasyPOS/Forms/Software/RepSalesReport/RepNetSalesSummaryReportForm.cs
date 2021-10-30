using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.RepSalesReport
{
    public partial class RepNetSalesSummaryReportForm : Form
    {
        public List<Entities.DgvRepNetSalesSummaryReportDailyEntity> netSalesSummaryList;
        public BindingSource dataNetSalesSummaryListSource = new BindingSource();
        public PagedList<Entities.DgvRepNetSalesSummaryReportDailyEntity> pageList;

        public Int32 pageNumber = 1;
        public Int32 pageSize = 50;

        public DateTime dateStart;
        public DateTime dateEnd;

        public RepNetSalesSummaryReportForm(DateTime startDate, DateTime endDate)
        {
            dateStart = startDate;
            dateEnd = endDate;
            InitializeComponent();
            GetNetSalesSummaryDataSource();
            GetNetSalesSummaryListDataGridSource();
        }

        public List<Entities.DgvRepNetSalesSummaryReportDailyEntity> NetSalesSummaryList(DateTime startDate, DateTime endDate)
        {
            List<Entities.DgvRepNetSalesSummaryReportDailyEntity> rowList = new List<Entities.DgvRepNetSalesSummaryReportDailyEntity>();
            Controllers.RepSalesReportController repSalesDetailReportController = new Controllers.RepSalesReportController();
            var netSalesSummaryList = repSalesDetailReportController.GetNetSalesSummaryReportDaily(startDate, endDate);
            if (netSalesSummaryList.Any())
            {
                var row = from d in netSalesSummaryList
                          select new Entities.DgvRepNetSalesSummaryReportDailyEntity
                          {
                              ColumnNetSalesSummaryDate = d.Date.ToShortDateString(),
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

                pageList = new PagedList<Entities.DgvRepNetSalesSummaryReportDailyEntity>(netSalesSummaryList, pageNumber, pageSize);

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
            dataGridViewNetSalesSummaryReport.DataSource = dataNetSalesSummaryListSource;

        }

       
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonPageListFirst_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvRepNetSalesSummaryReportDailyEntity>(netSalesSummaryList, 1, pageSize);
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
                pageList = new PagedList<Entities.DgvRepNetSalesSummaryReportDailyEntity>(netSalesSummaryList, --pageNumber, pageSize);
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
                pageList = new PagedList<Entities.DgvRepNetSalesSummaryReportDailyEntity>(netSalesSummaryList, ++pageNumber, pageSize);
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
            {
                pageList = new PagedList<Entities.DgvRepNetSalesSummaryReportDailyEntity>(netSalesSummaryList, pageList.PageCount, pageSize);
                dataNetSalesSummaryListSource.DataSource = pageList;

                buttonPageListFirst.Enabled = true;
                buttonPageListPrevious.Enabled = true;
                buttonPageListNext.Enabled = false;
                buttonPageListLast.Enabled = false;

                pageNumber = pageList.PageCount;
                textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
            }

        }

        private void buttonGenerateCSV_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = folderBrowserDialogGenerateCSV.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    StringBuilder csv = new StringBuilder();
                    String[] header = { "Date", "Customer Count", "Quantity", "Cost Amount", "Sales Amount", "Margin Amount", "%" };
                    csv.AppendLine(String.Join(",", header));

                    if (netSalesSummaryList.Any())
                    {
                        foreach (var sales in netSalesSummaryList)
                        {
                            String[] data = {
                                sales.ColumnNetSalesSummaryDate.Replace(",", String.Empty).Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty),
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
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\NetSalesSummaryReport_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("utf-8"));

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
