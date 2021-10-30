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

namespace EasyPOS.Forms.Software.RepInventoryReport
{
    public partial class RepStockInDetailReportForm : Form
    {
        public List<Entities.DgvRepInventoryStockInDetailReportListEntity> stockInDetailReportList;
        public BindingSource dataStockInDetailReportListSource = new BindingSource();
        public PagedList<Entities.DgvRepInventoryStockInDetailReportListEntity> pageList;
        public Int32 pageNumber = 1;
        public Int32 pageSize = 50;

        public DateTime startDate;
        public DateTime endDate;

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();


        public RepStockInDetailReportForm(DateTime dateStart, DateTime dateEnd)
        {
            InitializeComponent();

            label1.Text = SetLabel(label1.Text);
            buttonClose.Text = SetLabel(buttonClose.Text);
            buttonPrint.Text = SetLabel(buttonPrint.Text);

            startDate = dateStart;
            endDate = dateEnd;

            GetStockInDetailDataSource();
            GetDataGridViewStockInDetailSource();
        }

        public string SetLabel(string label)
        {
            Controllers.SysLanguageController sysLabel = new Controllers.SysLanguageController();
            var language = Modules.SysCurrentModule.GetCurrentSettings().Language;
            sysLanguageEntities = sysLabel.ListLanguage("");
            if (sysLanguageEntities.Any())
            {

                if (sysLabel.ListLanguage("").Any())
                {

                    foreach (var displayedLabel in sysLanguageEntities)
                    {
                        if (language != "English")
                        {
                            if (displayedLabel.Label == label)
                            {
                                return displayedLabel.DisplayedLabel;
                            }

                        }
                        else
                        {
                            if (displayedLabel.Label == label)
                            {
                                return displayedLabel.Label;
                            }
                        }
                    }
                }
            }
            return label;
        }

        public List<Entities.DgvRepInventoryStockInDetailReportListEntity> GetStockInDetailReportListData(DateTime startDate, DateTime endDate)
        {
            String filter = textBoxFilter.Text;
            List<Entities.DgvRepInventoryStockInDetailReportListEntity> rowList = new List<Entities.DgvRepInventoryStockInDetailReportListEntity>();

            Controllers.RepInventoryReportController repInvetoryReportController = new Controllers.RepInventoryReportController();

            var stockInDetailReportList = repInvetoryReportController.StockInDetailReport(filter,startDate, endDate);
            if (stockInDetailReportList.OrderByDescending(d => d.Id).Any())
            {
                Decimal totalAmount = 0;
                var row = from d in stockInDetailReportList
                          select new Entities.DgvRepInventoryStockInDetailReportListEntity
                          {
                              ColumnItemListCode = d.ItemCode,
                              ColumnItemListBarcode = d.BarCode,
                              ColumnStockInDate = d.StockInDate,
                              ColumnStockInNumber = d.StockInNumber,
                              ColumnManualStockInNumber = d.ManualStockInNumber,
                              ColumnRemarks = d.Remarks,
                              ColumnIsReturn = d.IsReturn,
                              ColumnItem = d.Item,
                              ColumnUnit = d.Unit,
                              ColumnQuantity = d.Quantity.ToString("#,##0.00"),
                              ColumnCost = d.Cost.ToString("#,##0.00"),
                              ColumnAmount = d.Amount.ToString("#,##0.00"),
                              ColumnExpiryDate = d.ExpiryDate,
                              ColumnLotNumber = d.LotNumber,
                              ColumnSellingPrice = d.SellingPrice != null ? Convert.ToDecimal(d.SellingPrice).ToString("#,##0.00") : "0.00"
                          };

                totalAmount = stockInDetailReportList.Sum(d => d.Amount);

                textBoxTotalAmount.Text = totalAmount.ToString("#,##0.00");

                rowList = row.ToList();
            }
            return rowList;
        }

        public void GetStockInDetailDataSource()
        {
            stockInDetailReportList = GetStockInDetailReportListData(startDate, endDate);
            if (stockInDetailReportList.Any())
            {
                pageList = new PagedList<Entities.DgvRepInventoryStockInDetailReportListEntity>(stockInDetailReportList, pageNumber, pageSize);

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
                dataStockInDetailReportListSource.DataSource = pageList;
            }
            else
            {
                buttonPageListFirst.Enabled = false;
                buttonPageListPrevious.Enabled = false;
                buttonPageListNext.Enabled = false;
                buttonPageListLast.Enabled = false;

                dataStockInDetailReportListSource.Clear();
                textBoxPageNumber.Text = "0 / 0";
            }
        }

        private void GetDataGridViewStockInDetailSource()
        {
            dataGridViewStockInDetailReport.DataSource = dataStockInDetailReportListSource;
        }

        private void buttonPageListFirst_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvRepInventoryStockInDetailReportListEntity>(stockInDetailReportList, 1, pageSize);
            dataStockInDetailReportListSource.DataSource = pageList;

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
                pageList = new PagedList<Entities.DgvRepInventoryStockInDetailReportListEntity>(stockInDetailReportList, --pageNumber, pageSize);
                dataStockInDetailReportListSource.DataSource = pageList;
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
                pageList = new PagedList<Entities.DgvRepInventoryStockInDetailReportListEntity>(stockInDetailReportList, ++pageNumber, pageSize);
                dataStockInDetailReportListSource.DataSource = pageList;
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
            pageList = new PagedList<Entities.DgvRepInventoryStockInDetailReportListEntity>(stockInDetailReportList, pageList.PageCount, pageSize);
            dataStockInDetailReportListSource.DataSource = pageList;

            buttonPageListFirst.Enabled = true;
            buttonPageListPrevious.Enabled = true;
            buttonPageListNext.Enabled = false;
            buttonPageListLast.Enabled = false;

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
                    String[] header = { "Item Code", "Barcode", "StockInDate", "StockInNumber", "ManualStockInNumber", "Remarks", "IsReturn", "Item", "Unit", "Quantity", "Cost", "Amount", "ExpiryDate", "LotNumber", "SellingPrice" };
                    csv.AppendLine(String.Join(",", header));

                    if (stockInDetailReportList.Any())
                    {
                        foreach (var stockInDetail in stockInDetailReportList)
                        {
                            String manualStockInNumber = "";
                            if (stockInDetail.ColumnManualStockInNumber != null)
                            {
                                manualStockInNumber = stockInDetail.ColumnManualStockInNumber;
                            }

                            String expiryDate = "";
                            if (stockInDetail.ColumnExpiryDate != null)
                            {
                                expiryDate = stockInDetail.ColumnExpiryDate;
                            }

                            String lotNumber = "";
                            if (stockInDetail.ColumnLotNumber != null)
                            {
                                lotNumber = stockInDetail.ColumnLotNumber;
                            }

                            String sellingPrice = "";
                            if (stockInDetail.ColumnSellingPrice != null)
                            {
                                sellingPrice = stockInDetail.ColumnSellingPrice;
                            }

                            String amount = "";
                            if (stockInDetail.ColumnAmount != null)
                            {
                                amount = stockInDetail.ColumnAmount;
                            }

                            String cost = "";
                            if (stockInDetail.ColumnCost != null)
                            {
                                cost = stockInDetail.ColumnCost;
                            }

                            String[] data = {
                                "="+"\""+stockInDetail.ColumnItemListCode+"\"",
                                "="+"\""+stockInDetail.ColumnItemListBarcode+"\"",
                                stockInDetail.ColumnStockInDate,
                                stockInDetail.ColumnStockInNumber,
                                manualStockInNumber.Replace("," , ""),
                                stockInDetail.ColumnRemarks.Replace(",", String.Empty).Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty),
                                stockInDetail.ColumnIsReturn.ToString(),
                                stockInDetail.ColumnItem.Replace("," , " "),
                                stockInDetail.ColumnUnit.Replace("," , " "),
                                stockInDetail.ColumnQuantity.Replace("," , ""),
                                cost.Replace("," , ""),
                                amount.Replace("," , ""),
                                expiryDate.Replace("," , ""),
                                lotNumber.Replace("," , ""),
                                sellingPrice.Replace("," , "")
                            };

                            csv.AppendLine(String.Join(",", data));
                        }
                    }

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\StockInDetailReport_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("utf-8"));

                    MessageBox.Show("Generate CSV Successful!", "Generate CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                GetStockInDetailDataSource();
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            new RepStockInDetailReportPDFForm(startDate, endDate, textBoxFilter.Text);
        }
    }
}
