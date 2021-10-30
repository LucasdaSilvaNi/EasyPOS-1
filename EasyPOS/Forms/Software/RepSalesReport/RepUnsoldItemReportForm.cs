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
    public partial class RepUnsoldItemReportForm : Form
    {
        public List<Entities.DgvRepUnsoldItemEntity> salesDetailList;
        public BindingSource dataUnsoldItemSource = new BindingSource();
        public PagedList<Entities.DgvRepUnsoldItemEntity> pageList;
        public Int32 pageNumber = 1;
        public Int32 pageSize = 50;

        public DateTime dateStart;
        public DateTime dateEnd;
        public RepUnsoldItemReportForm(DateTime startDate, DateTime endDate)
        {
            dateStart = startDate;
            dateEnd = endDate;
            InitializeComponent();
            GetUnsoldItemDataSource();
            GetUnsoldItemListDataGridSource();
        }
        public List<Entities.DgvRepUnsoldItemEntity> GetUnsoldItemListData(DateTime startDate, DateTime endDate)
        {
            List<Entities.DgvRepUnsoldItemEntity> rowList = new List<Entities.DgvRepUnsoldItemEntity>();

            Controllers.RepSalesReportController repUnsoldItemsReportController = new Controllers.RepSalesReportController();

            var salesDetailList = repUnsoldItemsReportController.UnsoldItemsReport(startDate, endDate);
            if (salesDetailList.Any())
            {
                List<Entities.DgvRepUnsoldItemEntity> newUnsoldItemsReportList = new List<Entities.DgvRepUnsoldItemEntity>();
                foreach (var unsoldItemsReport in salesDetailList)
                {
                    newUnsoldItemsReportList.Add(new Entities.DgvRepUnsoldItemEntity()
                    {
                        ColumnBarCode = unsoldItemsReport.BarCode,
                        ColumnItemDescription = unsoldItemsReport.ItemDescription,
                        ColumnItemCategory = unsoldItemsReport.ItemCategory,
                        ColumnUnit = unsoldItemsReport.Unit,
                        ColumnPrice = unsoldItemsReport.Price.ToString("#,##0.00"),
                        ColumnCost = unsoldItemsReport.Cost.ToString("#,##0.00"),
                    });

                }

                rowList = newUnsoldItemsReportList.ToList();
            }

            return rowList;
        }
        public void GetUnsoldItemDataSource()
        {
            salesDetailList = GetUnsoldItemListData(dateStart, dateEnd);
            if (salesDetailList.Any())
            {

                pageList = new PagedList<Entities.DgvRepUnsoldItemEntity>(salesDetailList, pageNumber, pageSize);

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
                dataUnsoldItemSource.DataSource = pageList;
            }
            else
            {
                buttonPageListFirst.Enabled = false;
                buttonPageListPrevious.Enabled = false;
                buttonPageListNext.Enabled = false;
                buttonPageListLast.Enabled = false;

                dataUnsoldItemSource.Clear();
                textBoxPageNumber.Text = "0 / 0";
            }
        }
        public void GetUnsoldItemListDataGridSource()
        {
            dataGridViewUnsoldItems.DataSource = dataUnsoldItemSource;

        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonPageListFirst_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvRepUnsoldItemEntity>(salesDetailList, 1, pageSize);
            dataUnsoldItemSource.DataSource = pageList;

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
                pageList = new PagedList<Entities.DgvRepUnsoldItemEntity>(salesDetailList, --pageNumber, pageSize);
                dataUnsoldItemSource.DataSource = pageList;
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
                pageList = new PagedList<Entities.DgvRepUnsoldItemEntity>(salesDetailList, ++pageNumber, pageSize);
                dataUnsoldItemSource.DataSource = pageList;
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
            pageList = new PagedList<Entities.DgvRepUnsoldItemEntity>(salesDetailList, pageList.PageCount, pageSize);
            dataUnsoldItemSource.DataSource = pageList;

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
                    DateTime startDate = dateStart;
                    DateTime endDate = dateEnd;

                    StringBuilder csv = new StringBuilder();
                    String[] header = { "BarCode", "Item Description", "Category", "Unit", "Cost", "Price" };
                    csv.AppendLine(String.Join(",", header));

                    if (salesDetailList.Any())
                    {
                        foreach (var salesDetail in salesDetailList)
                        {
                            String[] data = {
                                             salesDetail.ColumnBarCode,
                                             salesDetail.ColumnItemDescription.Replace("," , " "),
                                             salesDetail.ColumnItemCategory.Replace("," , " "),
                                             salesDetail.ColumnUnit,
                                             salesDetail.ColumnCost.Replace("," , ""),
                                             salesDetail.ColumnPrice.Replace(",", " "),
                            };

                            csv.AppendLine(String.Join(",", data));
                        }
                    }

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\TopSellingItemsReport_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("utf-8"));

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
