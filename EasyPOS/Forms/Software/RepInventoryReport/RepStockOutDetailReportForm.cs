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
    public partial class RepStockOutDetailReportForm : Form
    {
        public List<Entities.DgvRepInventoryStockOutDetailReportListEntity> stockOutDetailReportList;
        public BindingSource dataStockOutDetailReportListSource = new BindingSource();
        public PagedList<Entities.DgvRepInventoryStockOutDetailReportListEntity> pageList;
        public Int32 pageNumber = 1;
        public Int32 pageSize = 50;

        public DateTime startDate;
        public DateTime endDate;

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public RepStockOutDetailReportForm(DateTime dateStart, DateTime dateEnd)
        {
            InitializeComponent();

            label1.Text = SetLabel(label1.Text);
            buttonClose.Text = SetLabel(buttonClose.Text);
            buttonPrint.Text = SetLabel(buttonPrint.Text);

            startDate = dateStart;
            endDate = dateEnd;

            GetStockOutDetailDataSource();
            GetDataGridViewStockOutDetailSource();
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

        public List<Entities.DgvRepInventoryStockOutDetailReportListEntity> GetStockOutDetailReportListData(DateTime startDate, DateTime endDate)
        {
            List<Entities.DgvRepInventoryStockOutDetailReportListEntity> rowList = new List<Entities.DgvRepInventoryStockOutDetailReportListEntity>();

            Controllers.RepInventoryReportController repInvetoryReportController = new Controllers.RepInventoryReportController();

            var stockOutDetailReportList = repInvetoryReportController.StockOutDetailReport(startDate, endDate);
            if (stockOutDetailReportList.OrderByDescending(d => d.Id).Any())
            {
                Decimal totalAmount = 0;
                var row = from d in stockOutDetailReportList
                          select new Entities.DgvRepInventoryStockOutDetailReportListEntity
                          {
                              ColumnItemListCode = d.ItemCode,
                              ColumnItemListBarcode = d.BarCode,
                              ColumnStockOutDate = d.StockOutDate,
                              ColumnStockOutNumber = d.StockOutNumber,
                              ColumnManualStockOutNumber = d.ManualStockOutNumber,
                              ColumnRemarks = d.Remarks,
                              ColumnItem = d.Item,
                              ColumnUnit = d.Item,
                              ColumnQuantity = d.Quantity.ToString("#,##0.00"),
                              ColumnCost = d.Cost.ToString("#,##0.00"),
                              ColumnAmount = d.Amount.ToString("#,##0.00")
                          };

                totalAmount = stockOutDetailReportList.Sum(d => d.Amount);

                textBoxTotalAmount.Text = totalAmount.ToString("#,##0.00");

                rowList = row.ToList();
            }
            return rowList;
        }

        public void GetStockOutDetailDataSource()
        {
            stockOutDetailReportList = GetStockOutDetailReportListData(startDate, endDate);
            if (stockOutDetailReportList.Any())
            {
                pageList = new PagedList<Entities.DgvRepInventoryStockOutDetailReportListEntity>(stockOutDetailReportList, pageNumber, pageSize);

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
                dataStockOutDetailReportListSource.DataSource = pageList;
            }
            else
            {
                buttonPageListFirst.Enabled = false;
                buttonPageListPrevious.Enabled = false;
                buttonPageListNext.Enabled = false;
                buttonPageListLast.Enabled = false;

                dataStockOutDetailReportListSource.Clear();
                textBoxPageNumber.Text = "0 / 0";
            }
        }

        private void GetDataGridViewStockOutDetailSource()
        {
            dataGridViewStockOutDetailReport.DataSource = dataStockOutDetailReportListSource;
        }

        private void buttonPageListFirst_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvRepInventoryStockOutDetailReportListEntity>(stockOutDetailReportList, 1, pageSize);
            dataStockOutDetailReportListSource.DataSource = pageList;

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
                pageList = new PagedList<Entities.DgvRepInventoryStockOutDetailReportListEntity>(stockOutDetailReportList, --pageNumber, pageSize);
                dataStockOutDetailReportListSource.DataSource = pageList;
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
                pageList = new PagedList<Entities.DgvRepInventoryStockOutDetailReportListEntity>(stockOutDetailReportList, ++pageNumber, pageSize);
                dataStockOutDetailReportListSource.DataSource = pageList;
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
            pageList = new PagedList<Entities.DgvRepInventoryStockOutDetailReportListEntity>(stockOutDetailReportList, pageList.PageCount, pageSize);
            dataStockOutDetailReportListSource.DataSource = pageList;

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
                    String[] header = { "Item Code", "Barcode", "StockOutDate", "StockOutNumber", "ManualStockOutNumber", "Remarks", "Item", "Unit", "Quantity", "Cost", "Amount" };
                    csv.AppendLine(String.Join(",", header));

                    if (stockOutDetailReportList.Any())
                    {
                        foreach (var stockOutDetail in stockOutDetailReportList)
                        {
                            String[] data = {
                              "="+"\""+stockOutDetail.ColumnItemListCode+"\"",
                              "="+"\""+stockOutDetail.ColumnItemListBarcode+"\"",
                              stockOutDetail.ColumnStockOutDate,
                              stockOutDetail.ColumnStockOutNumber,
                              stockOutDetail.ColumnManualStockOutNumber,
                              stockOutDetail.ColumnRemarks.Replace(",", String.Empty).Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty),
                              stockOutDetail.ColumnItem.Replace("," , " "),
                              stockOutDetail.ColumnUnit.Replace("," , " "),
                              stockOutDetail.ColumnQuantity.Replace("," , ""),
                              stockOutDetail.ColumnCost.Replace("," , " "),
                              stockOutDetail.ColumnAmount.Replace("," , "")
                            };

                            csv.AppendLine(String.Join(",", data));
                        }
                    }

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\StockOutDetailReport_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("iso-8859-1"));

                    MessageBox.Show("Generate CSV Successful!", "Generate CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            new RepStockOutDetailReportPDFForm(startDate, endDate);
        }
    }
}
