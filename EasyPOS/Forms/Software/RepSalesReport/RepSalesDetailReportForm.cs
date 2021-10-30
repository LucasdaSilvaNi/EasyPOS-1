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
    public partial class RepSalesDetailReportForm : Form
    {
        public List<Entities.DgvRepSalesReportSalesDetailReportListEntity> salesDetailList;
        public BindingSource dataSalesDatailListSource = new BindingSource();
        public PagedList<Entities.DgvRepSalesReportSalesDetailReportListEntity> pageList;
        public Int32 pageNumber = 1;
        public Int32 pageSize = 50;

        public DateTime dateStart;
        public DateTime dateEnd;
        public Int32 filterTerminalId;
        public Int32 filterCustomerId;
        public Int32 filterSalesAgentId;
        public Int32 filterSupplierId;
        public Int32 filterItemId;

        public RepSalesDetailReportForm(DateTime startDate, DateTime endDate, Int32 terminalId, Int32 CustomerId, Int32 SalesAgentId, Int32 supplierId, Int32 itemId)
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

        public List<Entities.DgvRepSalesReportSalesDetailReportListEntity> GetSalesDetailListData(DateTime startDate, DateTime endDate, Int32 terminalId, Int32 CustomerId, Int32 SalesAgentId, Int32 supplierId, Int32 itemId)
        {
            List<Entities.DgvRepSalesReportSalesDetailReportListEntity> rowList = new List<Entities.DgvRepSalesReportSalesDetailReportListEntity>();

            Controllers.RepSalesReportController repSalesDetailReportController = new Controllers.RepSalesReportController();

            var salesDetailList = repSalesDetailReportController.SalesDetailReport(startDate, endDate, terminalId, CustomerId, SalesAgentId, supplierId, itemId);
            if (salesDetailList.OrderByDescending(d => d.Id).Any())
            {
                Decimal totalAmount = 0;

                var row = from d in salesDetailList
                          select new Entities.DgvRepSalesReportSalesDetailReportListEntity
                          {
                              ColumnTerminal = d.Terminal,
                              ColumnDate = d.Date,
                              ColumnSalesNumber = d.SalesNumber,
                              ColumnCustomerCode = d.CustomerCode,
                              ColumnCustomer = d.Customer,
                              ColumnItemCode = d.ItemCode,
                              ColumnBarCode = d.BarCode,
                              ColumnItemDescription = d.ItemDescription,
                              ColumnSupplier = d.Supplier,
                              ColumnItemCategory = d.ItemCategory,
                              ColumnUnit = d.Unit,
                              ColumnCost = d.Cost.ToString("#,##0.00"),
                              ColumnPrice = d.Price.ToString("#,##0.00"),
                              ColumnCostAmount = d.CostAmount.ToString("#,##0.00"),
                              ColumnDiscountAmount = d.DiscountAmount.ToString("#,##0.00"),
                              ColumnNetPrice = d.NetPrice.ToString("#,##0.00"),
                              ColumnQuantity = d.Quantity.ToString("#,##0.00"),
                              ColumnAmount = d.Amount.ToString("#,##0.00"),
                              ColumnTax = d.Tax,
                              ColumnTaxRate = d.TaxRate.ToString("#,##0.00"),
                              ColumnTaxAmount = d.TaxAmount.ToString("#,##0.00")
                          };

                totalAmount = salesDetailList.Sum(d => d.Amount);

                textBoxTotalAmount.Text = totalAmount.ToString("#,##0.00");

                rowList = row.ToList();

            }
            return rowList;
        }

        public void GetSalesDetailListDataSource()
        {
            salesDetailList = GetSalesDetailListData(dateStart, dateEnd, filterTerminalId, filterCustomerId, filterSalesAgentId, filterSupplierId, filterItemId);
            if (salesDetailList.Any())
            {

                pageList = new PagedList<Entities.DgvRepSalesReportSalesDetailReportListEntity>(salesDetailList, pageNumber, pageSize);

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
            dataGridViewSalesDetailReport.DataSource = dataSalesDatailListSource;

        }

        private void buttonSalesListPageListFirst_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvRepSalesReportSalesDetailReportListEntity>(salesDetailList, 1, pageSize);
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
                pageList = new PagedList<Entities.DgvRepSalesReportSalesDetailReportListEntity>(salesDetailList, --pageNumber, pageSize);
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
                pageList = new PagedList<Entities.DgvRepSalesReportSalesDetailReportListEntity>(salesDetailList, ++pageNumber, pageSize);
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
            pageList = new PagedList<Entities.DgvRepSalesReportSalesDetailReportListEntity>(salesDetailList, pageList.PageCount, pageSize);
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
                    String[] header = { "Terminal", "Date", "Sales Number", "Customer Code", "Customer", "Item Code", "Barcode", "Item Description", "Item Category", "Supplier", "Unit", "Cost", "Price", "CostAmount", "Discount", "Net Price", "Quantity", "Amount", "Tax", "Tax Rate", "Tax Amount" };
                    csv.AppendLine(String.Join(",", header));

                    if (salesDetailList.Any())
                    {
                        foreach (var salesDetail in salesDetailList)
                        {
                            String customerCode = "";
                            if (salesDetail.ColumnCustomerCode != null)
                            {
                                customerCode = salesDetail.ColumnCustomerCode.Replace(",", "");
                            }

                            String[] data = {
                                salesDetail.ColumnTerminal,
                                salesDetail.ColumnDate,
                                salesDetail.ColumnSalesNumber,
                                customerCode,
                                salesDetail.ColumnCustomer.Replace("," , ""),
                                salesDetail.ColumnItemCode.Replace("," , ""),
                                salesDetail.ColumnBarCode.Replace("," , ""),
                                salesDetail.ColumnItemDescription.Replace("," , ""),
                                salesDetail.ColumnItemCategory.Replace("," , ""),
                                salesDetail.ColumnSupplier.Replace("," , ""),
                                salesDetail.ColumnUnit.Replace("," , ""),
                                salesDetail.ColumnCost.Replace("," , ""),
                                salesDetail.ColumnPrice.Replace("," , ""),
                                salesDetail.ColumnCostAmount.Replace("," , ""),
                                salesDetail.ColumnDiscountAmount.Replace("," , ""),
                                salesDetail.ColumnNetPrice.Replace("," , ""),
                                salesDetail.ColumnQuantity.Replace("," , ""),
                                salesDetail.ColumnAmount.Replace("," , ""),
                                salesDetail.ColumnTax.Replace("," , ""),
                                salesDetail.ColumnTaxRate.Replace("," , ""),
                                salesDetail.ColumnTaxAmount.Replace("," , "")
                            };
                            csv.AppendLine(String.Join(",", data));
                        }
                    }

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\SalesDetailReport_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("utf-8"));

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

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            new RepSalesDetailReportPDFForm(dateStart, dateEnd, filterTerminalId);
        }
    }
}
