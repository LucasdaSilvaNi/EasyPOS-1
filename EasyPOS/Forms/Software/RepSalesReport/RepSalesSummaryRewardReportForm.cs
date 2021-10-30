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
    public partial class RepSalesSummaryRewardReportForm : Form
    {
        public List<Entities.DgvSalesSummaryRewardReportEntity> customerList;
        public BindingSource dataCustomerListSource = new BindingSource();
        public PagedList<Entities.DgvSalesSummaryRewardReportEntity> pageList;
        public Int32 pageNumber = 1;
        public Int32 pageSize = 50;
        public Int32 filterCustomer;
        public RepSalesSummaryRewardReportForm(Int32 filter)
        {
            InitializeComponent();
            filterCustomer = filter;

            GetCustomerListDataSource();
            GetCustomerListDataGridSource();
        }
        public List<Entities.DgvSalesSummaryRewardReportEntity> GetSalesSummaryRewardListData(Int32 filter)
        {
            List<Entities.DgvSalesSummaryRewardReportEntity> rowList = new List<Entities.DgvSalesSummaryRewardReportEntity>();
            Controllers.RepSalesReportController repSalesReportController = new Controllers.RepSalesReportController();

            var customerListReport = repSalesReportController.GetSalesSummaryRewardListData(filter);
            if (customerListReport.Any())
            {
                var row = from d in customerListReport
                          select new Entities.DgvSalesSummaryRewardReportEntity
                          {
                              ColumnCustomer = d.Customer,
                              ColumnRewardNo = d.RewardNumber,
                              ColumnAvailableReward = d.AvailableReward.ToString("#,##0.00"),
                              ColumnTotalClaimReward = d.TotalClaimRewards != null ? Convert.ToDecimal(d.TotalClaimRewards).ToString("#,##0.00") : "0.00"
                          };

                rowList = row.ToList();

            }
            return rowList;

        }
        public void GetCustomerListDataSource()
        {
            customerList = GetSalesSummaryRewardListData(filterCustomer);
            if (customerList.Any())
            {

                pageList = new PagedList<Entities.DgvSalesSummaryRewardReportEntity>(customerList, pageNumber, pageSize);

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
                dataCustomerListSource.DataSource = pageList;
            }
            else
            {
                buttonPageListFirst.Enabled = false;
                buttonPageListPrevious.Enabled = false;
                buttonPageListNext.Enabled = false;
                buttonPageListLast.Enabled = false;

                dataCustomerListSource.Clear();
                textBoxPageNumber.Text = "0 / 0";
            }
        }
        public void GetCustomerListDataGridSource()
        {
            dataGridViewSalesSummaryRewardReport.DataSource = dataCustomerListSource;

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonPageListFirst_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvSalesSummaryRewardReportEntity>(customerList, 1, pageSize);
            dataCustomerListSource.DataSource = pageList;

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
                pageList = new PagedList<Entities.DgvSalesSummaryRewardReportEntity>(customerList, --pageNumber, pageSize);
                dataCustomerListSource.DataSource = pageList;
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
                pageList = new PagedList<Entities.DgvSalesSummaryRewardReportEntity>(customerList, ++pageNumber, pageSize);
                dataCustomerListSource.DataSource = pageList;
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
            pageList = new PagedList<Entities.DgvSalesSummaryRewardReportEntity>(customerList, pageList.PageCount, pageSize);
            dataCustomerListSource.DataSource = pageList;

            buttonPageListFirst.Enabled = true;
            buttonPageListPrevious.Enabled = true;
            buttonPageListNext.Enabled = false;
            buttonPageListLast.Enabled = false;

            pageNumber = pageList.PageCount;
            textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
        }

        private void buttonGenerateCSV_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = folderBrowserDialogGenerateCSV.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    StringBuilder csv = new StringBuilder();
                    String[] header = { "Customer", "Reward Number", "Available Reward", "Total Claim Reward"};
                    csv.AppendLine(String.Join(",", header));

                    if (customerList.Any())
                    {
                        foreach (var customer in customerList)
                        {

                            String[] data = {
                                customer.ColumnCustomer.Replace("," , ""),
                                customer.ColumnRewardNo.Replace(",", String.Empty).Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty),
                                customer.ColumnAvailableReward.Replace(",", String.Empty).Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty),
                                customer.ColumnTotalClaimReward.Replace(",", String.Empty).Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty)
                            };
                            csv.AppendLine(String.Join(",", data));
                        }
                    }

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\SalesSummaryRewardReport_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("utf-8"));

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
