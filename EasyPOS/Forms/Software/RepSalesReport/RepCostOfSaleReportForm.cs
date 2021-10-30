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
    public partial class RepCostOfSaleReportForm : Form
    {
        public List<Entities.DgvRepSalesCostOfSalesReportEntity> salesDetailList;
        public BindingSource dataSalesDatailListSource = new BindingSource();
        public PagedList<Entities.DgvRepSalesCostOfSalesReportEntity> pageList;
        public Int32 pageNumber = 1;
        public Int32 pageSize = 50;

        public DateTime dateStart;
        public DateTime dateEnd;
        public Int32 filterTerminalId;
        public Int32 filterCustomerId;
        public Int32 filterSalesAgentId;
        public Int32 filterSupplierId;
        public Int32 filterItemId;
        public RepCostOfSaleReportForm(DateTime startDate, DateTime endDate, Int32 terminalId, Int32 CustomerId, Int32 SalesAgentId, Int32 supplierId, Int32 itemId)
        {
            InitializeComponent();
            dateStart = startDate;
            dateEnd = endDate;
            filterTerminalId = terminalId;
            filterCustomerId = CustomerId;
            filterSalesAgentId = SalesAgentId;
            filterSupplierId = supplierId;
            filterItemId = itemId;

            GetSalesDetailListDataSource();
            GetSalesDetailListDataGridSource();
        }

        public List<Entities.DgvRepSalesCostOfSalesReportEntity> GetSalesDetailListData(DateTime startDate, DateTime endDate, Int32 terminalId, Int32 CustomerId, Int32 SalesAgentId, Int32 supplierId, Int32 itemId)
        {
            List<Entities.DgvRepSalesCostOfSalesReportEntity> rowList = new List<Entities.DgvRepSalesCostOfSalesReportEntity>();

            Controllers.RepSalesReportController repSalesDetailReportController = new Controllers.RepSalesReportController();

            var salesDetailList = repSalesDetailReportController.SalesDetailReport(startDate, endDate, terminalId, CustomerId, SalesAgentId, supplierId, itemId);
            if (salesDetailList.OrderByDescending(d => d.Id).Any())
            {
                Decimal totalCost = 0;

                var row = from d in salesDetailList
                          select new Entities.DgvRepSalesCostOfSalesReportEntity
                          {
                              ColumnTerminal = d.Terminal,
                              ColumnDate = d.Date,
                              ColumnSalesNumber = d.SalesNumber,
                              ColumnBarCode = d.BarCode,
                              ColumnItemDescription = d.ItemDescription,
                              ColumnQuantity = d.Quantity.ToString("#,##0.00"),
                              ColumnCost = d.Cost.ToString("#,##0.00"),
                              ColumnCostAmount = d.CostAmount.ToString("#,##0.00"),
                          };

                totalCost = salesDetailList.Sum(d => d.Cost);

                textBoxTotalCost.Text = totalCost.ToString("#,##0.00");

                rowList = row.ToList();

            }
            return rowList;
        }
        public void GetSalesDetailListDataSource()
        {
            salesDetailList = GetSalesDetailListData(dateStart, dateEnd, filterTerminalId, filterCustomerId, filterSalesAgentId, filterSupplierId, filterItemId);
            if (salesDetailList.Any())
            {

                pageList = new PagedList<Entities.DgvRepSalesCostOfSalesReportEntity>(salesDetailList, pageNumber, pageSize);

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
            dataGridViewCostOfSalesReport.DataSource = dataSalesDatailListSource;

        }
        private void buttonSalesListPageListFirst_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvRepSalesCostOfSalesReportEntity>(salesDetailList, 1, pageSize);
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
                pageList = new PagedList<Entities.DgvRepSalesCostOfSalesReportEntity>(salesDetailList, --pageNumber, pageSize);
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
                pageList = new PagedList<Entities.DgvRepSalesCostOfSalesReportEntity>(salesDetailList, ++pageNumber, pageSize);
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
            pageList = new PagedList<Entities.DgvRepSalesCostOfSalesReportEntity>(salesDetailList, pageList.PageCount, pageSize);
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
                    String[] header = { "Terminal", "Date", "Sales Number", "Barcode", "Item Description", "Quantity", "Cost", "CostAmount"};
                    csv.AppendLine(String.Join(",", header));

                    if (salesDetailList.Any())
                    {
                        foreach (var salesDetail in salesDetailList)
                        {
                           
                            String[] data = {
                                salesDetail.ColumnTerminal,
                                salesDetail.ColumnDate,
                               "="+"\""+salesDetail.ColumnSalesNumber+"\"",
                               "="+"\""+salesDetail.ColumnBarCode.Replace("," , "")+"\"",
                                salesDetail.ColumnItemDescription.Replace("," , ""),
                                salesDetail.ColumnQuantity.Replace("," , ""),
                                salesDetail.ColumnCost.Replace("," , ""),
                                salesDetail.ColumnCostAmount.Replace("," , ""),
                            };
                            csv.AppendLine(String.Join(",", data));
                        }
                    }

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\CostOfSalesReport_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("utf-8"));

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
