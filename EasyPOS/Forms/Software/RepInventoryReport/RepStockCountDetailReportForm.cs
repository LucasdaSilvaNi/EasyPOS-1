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
    public partial class RepStockCountDetailReportForm : Form
    {
        public List<Entities.DgvRepInventoryStockCountDetailReportListEntity> stockCountDetailReportList;
        public BindingSource dataStockCountDetailReportListSource = new BindingSource();
        public PagedList<Entities.DgvRepInventoryStockCountDetailReportListEntity> pageList;
        public Int32 pageNumber = 1;
        public Int32 pageSize = 50;

        public DateTime startDate;
        public DateTime endDate;

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public RepStockCountDetailReportForm(DateTime dateStart, DateTime dateEnd)
        {
            InitializeComponent();

            label1.Text = SetLabel(label1.Text);
            buttonClose.Text = SetLabel(buttonClose.Text);
            buttonPrint.Text = SetLabel(buttonPrint.Text);

            startDate = dateStart;
            endDate = dateEnd;

            GetStockCountDetailDataSource();
            GetDataGridViewStockCountDetailSource();
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

        public List<Entities.DgvRepInventoryStockCountDetailReportListEntity> GetStockCountDetailReportListData(DateTime startDate, DateTime endDate)
        {
            List<Entities.DgvRepInventoryStockCountDetailReportListEntity> rowList = new List<Entities.DgvRepInventoryStockCountDetailReportListEntity>();

            Controllers.RepInventoryReportController repInvetoryReportController = new Controllers.RepInventoryReportController();

            var stockCountDetailReportList = repInvetoryReportController.StockCountDetailReport(startDate, endDate);
            if (stockCountDetailReportList.OrderByDescending(d => d.Id).Any())
            {
                Decimal totalAmount = 0;
                var row = from d in stockCountDetailReportList
                          select new Entities.DgvRepInventoryStockCountDetailReportListEntity
                          {
                              ColumnItemListCode = d.ItemCode,
                              ColumnItemListBarcode = d.BarCode,
                              ColumnStockCountDate = d.StockCountDate,
                              ColumnStockCountNumber = d.StockCountNumber,
                              ColumnRemarks = d.Remarks,
                              ColumnItem = d.Item,
                              ColumnUnit = d.Unit,
                              ColumnQuantity = d.Quantity.ToString("#,##0.00"),
                              ColumnCost = d.Cost.ToString("#,##0.00"),
                              ColumnAmount = d.Amount.ToString("#,##0.00")
                          };
                totalAmount = stockCountDetailReportList.Sum(d => d.Amount);

                textBoxTotalAmount.Text = totalAmount.ToString("#,##0.00");

                rowList = row.ToList();
            }
            return rowList;
        }

        public void GetStockCountDetailDataSource()
        {
            stockCountDetailReportList = GetStockCountDetailReportListData(startDate, endDate);
            if (stockCountDetailReportList.Any())
            {
                pageList = new PagedList<Entities.DgvRepInventoryStockCountDetailReportListEntity>(stockCountDetailReportList, pageNumber, pageSize);

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
                dataStockCountDetailReportListSource.DataSource = pageList;
            }
            else
            {
                buttonPageListFirst.Enabled = false;
                buttonPageListPrevious.Enabled = false;
                buttonPageListNext.Enabled = false;
                buttonPageListLast.Enabled = false;

                dataStockCountDetailReportListSource.Clear();
                textBoxPageNumber.Text = "0 / 0";
            }
        }

        private void GetDataGridViewStockCountDetailSource()
        {
            dataGridViewStockCountDetailReport.DataSource = dataStockCountDetailReportListSource;
        }

        private void buttonPageListFirst_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvRepInventoryStockCountDetailReportListEntity>(stockCountDetailReportList, 1, pageSize);
            dataStockCountDetailReportListSource.DataSource = pageList;

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
                pageList = new PagedList<Entities.DgvRepInventoryStockCountDetailReportListEntity>(stockCountDetailReportList, --pageNumber, pageSize);
                dataStockCountDetailReportListSource.DataSource = pageList;
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
                pageList = new PagedList<Entities.DgvRepInventoryStockCountDetailReportListEntity>(stockCountDetailReportList, ++pageNumber, pageSize);
                dataStockCountDetailReportListSource.DataSource = pageList;
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
            pageList = new PagedList<Entities.DgvRepInventoryStockCountDetailReportListEntity>(stockCountDetailReportList, pageList.PageCount, pageSize);
            dataStockCountDetailReportListSource.DataSource = pageList;

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
                    String[] header = { "Item Code", "Barcode", "StockCountDate", "StockCountNumber", "Remarks", "Item", "Unit", "Quantity", "Cost", "Amount" };
                    csv.AppendLine(String.Join(",", header));

                    if (stockCountDetailReportList.Any())
                    {
                        foreach (var stockCountDetail in stockCountDetailReportList)
                        {
                            String[] data = {
                              "="+"\""+stockCountDetail.ColumnItemListCode+"\"",
                              "="+"\""+stockCountDetail.ColumnItemListBarcode+"\"",
                              stockCountDetail.ColumnStockCountDate,
                              stockCountDetail.ColumnStockCountNumber,
                              stockCountDetail.ColumnRemarks.Replace(",", String.Empty).Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty),
                              stockCountDetail.ColumnItem.Replace(",", ""),
                              stockCountDetail.ColumnUnit.Replace(",", ""),
                              stockCountDetail.ColumnQuantity.Replace(",", ""),
                              stockCountDetail.ColumnCost.Replace(",", ""),
                              stockCountDetail.ColumnAmount.Replace(",", "")
                            };

                            csv.AppendLine(String.Join(",", data));
                        }
                    }

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\StockCountDetailReport_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("iso-8859-1"));

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
            new RepStockCountDetailReportPDFForm(startDate, endDate);
        }
    }
}
