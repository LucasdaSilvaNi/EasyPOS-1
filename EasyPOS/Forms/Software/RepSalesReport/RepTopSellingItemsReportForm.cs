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
    public partial class RepTopSellingItemsReportForm : Form
    {
        public List<Entities.DgvRepSalesReportTopSellingItemsReportListEntity> salesDetailList;
        public BindingSource dataSalesDatailListSource = new BindingSource();
        public PagedList<Entities.DgvRepSalesReportTopSellingItemsReportListEntity> pageList;
        public Int32 pageNumber = 1;
        public Int32 pageSize = 50;

        public DateTime dateStart;
        public DateTime dateEnd;

        public RepTopSellingItemsReportForm(DateTime startDate, DateTime endDate)
        {
            InitializeComponent();

            dateStart = startDate;
            dateEnd = endDate;

            GetSalesDetailListDataSource();
            GetSalesDetailListDataGridSource();
        }

        public List<Entities.DgvRepSalesReportTopSellingItemsReportListEntity> GetSalesDetailListData(DateTime startDate, DateTime endDate)
        {
            List<Entities.DgvRepSalesReportTopSellingItemsReportListEntity> rowList = new List<Entities.DgvRepSalesReportTopSellingItemsReportListEntity>();

            Controllers.RepSalesReportController repTopSellingItemsReportController = new Controllers.RepSalesReportController();

            var salesDetailList = repTopSellingItemsReportController.TopSellingItemsReport(startDate, endDate);
            if (salesDetailList.Any())
            {
                Decimal totalAmount = 0;
                Int32 number = 0;

                List<Entities.DgvRepSalesReportTopSellingItemsReportListEntity> newTopSellingItemsReportList = new List<Entities.DgvRepSalesReportTopSellingItemsReportListEntity>();
                foreach (var topSellingItemsReport in salesDetailList)
                {
                    number += 1;

                    newTopSellingItemsReportList.Add(new Entities.DgvRepSalesReportTopSellingItemsReportListEntity()
                    {
                        ColumnNumber = number.ToString(),
                        ColumnItemCode = topSellingItemsReport.ItemCode,
                        ColumnItemDescription = topSellingItemsReport.ItemDescription,
                        ColumnItemCategory = topSellingItemsReport.ItemCategory,
                        ColumnUnit = topSellingItemsReport.Unit,
                        ColumnPrice = topSellingItemsReport.Price.ToString("#,##0.00"),
                        ColumnQuantity = topSellingItemsReport.Quantity.ToString("#,##0.00"),
                        ColumnAmount = topSellingItemsReport.Amount.ToString("#,##0.00")
                    });

                    totalAmount += topSellingItemsReport.Amount;
                }

                textBoxTotalAmount.Text = totalAmount.ToString("#,##0.00");
                rowList = newTopSellingItemsReportList.ToList();
            }

            return rowList;
        }

        public void GetSalesDetailListDataSource()
        {
            salesDetailList = GetSalesDetailListData(dateStart, dateEnd);
            if (salesDetailList.Any())
            {
                pageList = new PagedList<Entities.DgvRepSalesReportTopSellingItemsReportListEntity>(salesDetailList, pageNumber, pageSize);

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
                dataSalesDatailListSource.DataSource = pageList;
            }
            else
            {
                buttonPageListFirst.Enabled = false;
                buttonPageListPrevious.Enabled = false;
                buttonPageListNext.Enabled = false;
                buttonPageListLast.Enabled = false;

                dataSalesDatailListSource.Clear();
                textBoxPageNumber.Text = "0 / 0";
            }
        }

        public void GetSalesDetailListDataGridSource()
        {
            dataGridViewTopSellingItemsReport.DataSource = dataSalesDatailListSource;
        }

        private void buttonSalesListPageListFirst_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvRepSalesReportTopSellingItemsReportListEntity>(salesDetailList, 1, pageSize);
            dataSalesDatailListSource.DataSource = pageList;

            buttonPageListFirst.Enabled = false;
            buttonPageListPrevious.Enabled = false;
            buttonPageListNext.Enabled = true;
            buttonPageListLast.Enabled = true;

            pageNumber = 1;
            textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
        }

        private void buttonSalesListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (pageList.HasPreviousPage == true)
            {
                pageList = new PagedList<Entities.DgvRepSalesReportTopSellingItemsReportListEntity>(salesDetailList, --pageNumber, pageSize);
                dataSalesDatailListSource.DataSource = pageList;
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

        private void buttonSalesListPageListNext_Click(object sender, EventArgs e)
        {
            if (pageList.HasNextPage == true)
            {
                pageList = new PagedList<Entities.DgvRepSalesReportTopSellingItemsReportListEntity>(salesDetailList, ++pageNumber, pageSize);
                dataSalesDatailListSource.DataSource = pageList;
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

        private void buttonSalesListPageListLast_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvRepSalesReportTopSellingItemsReportListEntity>(salesDetailList, pageList.PageCount, pageSize);
            dataSalesDatailListSource.DataSource = pageList;

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
                    String[] header = { "No.", "Item Code", "Item Description", "Item Category", "Unit", "Quantity", "Price", "Amount" };
                    csv.AppendLine(String.Join(",", header));

                    if (salesDetailList.Any())
                    {
                        foreach (var salesDetail in salesDetailList)
                        {
                            String[] data = {salesDetail.ColumnNumber,
                                             salesDetail.ColumnItemCode,
                                             salesDetail.ColumnItemDescription.Replace("," , " "),
                                             salesDetail.ColumnItemCategory.Replace("," , " "),
                                             salesDetail.ColumnUnit,
                                             salesDetail.ColumnQuantity.Replace("," , ""),
                                             salesDetail.ColumnPrice.Replace(",", " "),
                                             salesDetail.ColumnAmount.Replace("," , ""),
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

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
