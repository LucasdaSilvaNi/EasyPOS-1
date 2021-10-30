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
    public partial class RepCustomerListReportForm : Form
    {
        public List<Entities.DgvMstCustomerListEntity> customerList;
        public BindingSource dataCustomerListSource = new BindingSource();
        public PagedList<Entities.DgvMstCustomerListEntity> pageList;
        public Int32 pageNumber = 1;
        public Int32 pageSize = 50;

        public RepCustomerListReportForm()
        {
            InitializeComponent();
            GetCustomerListDataSource();
            GetCustomerListDataGridSource();
        }
        public List<Entities.DgvMstCustomerListEntity> GetCustomerListReport()
        {
            List<Entities.DgvMstCustomerListEntity> rowList = new List<Entities.DgvMstCustomerListEntity>();

            Controllers.RepSalesReportController repSalesDetailReportController = new Controllers.RepSalesReportController();

            var customerListReport = repSalesDetailReportController.GetCustomerListReport();
            if (customerListReport.Any())
            {
                var row = from d in customerListReport
                          select new Entities.DgvMstCustomerListEntity
                          {
                              ColumnCustomerListCustomerCode = d.CustomerCode,
                              ColumnCustomerListCustomer = d.Customer,
                              ColumnCustomerListContactNumber = d.ContactNumber,
                              ColumnCustomerListAddress = d.Address
                          };

                rowList = row.ToList();

            }
            return rowList;
        }
        public void GetCustomerListDataSource()
        {
            customerList = GetCustomerListReport();
            if (customerList.Any())
            {

                pageList = new PagedList<Entities.DgvMstCustomerListEntity>(customerList, pageNumber, pageSize);

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
            dataGridViewCustomerListReport.DataSource = dataCustomerListSource;

        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonPageListFirst_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvMstCustomerListEntity>(customerList, 1, pageSize);
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
                pageList = new PagedList<Entities.DgvMstCustomerListEntity>(customerList, --pageNumber, pageSize);
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
                pageList = new PagedList<Entities.DgvMstCustomerListEntity>(customerList, ++pageNumber, pageSize);
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
            pageList = new PagedList<Entities.DgvMstCustomerListEntity>(customerList, pageList.PageCount, pageSize);
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
                    String[] header = { "Customer Code", "Customer", "Contact Number", "Address"};
                    csv.AppendLine(String.Join(",", header));

                    if (customerList.Any())
                    {
                        foreach (var customer in customerList)
                        {
                            String customerCode = "";
                            if (customer.ColumnCustomerListCustomerCode != null)
                            {
                                customerCode = customer.ColumnCustomerListCustomerCode.Replace(",", "");
                            }

                            String[] data = {
                                customerCode,
                                customer.ColumnCustomerListCustomer.Replace("," , ""),
                                customer.ColumnCustomerListContactNumber.Replace("," , ""),
                                customer.ColumnCustomerListAddress.Replace(",", String.Empty).Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty),
                            };
                            csv.AppendLine(String.Join(",", data));
                        }
                    }

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\CustomerListReport_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("utf-8"));

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
