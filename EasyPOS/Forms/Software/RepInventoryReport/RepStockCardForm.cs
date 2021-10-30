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
    public partial class RepStockCardForm : Form
    {
        public List<Entities.DgvRepInventoryStockCardListEntity> stockCardReportList;
        public BindingSource dataStockCardReportListSource = new BindingSource();
        public PagedList<Entities.DgvRepInventoryStockCardListEntity> pageList;
        public Int32 pageNumber = 1;
        public Int32 pageSize = 50;

        public DateTime startDate;
        public DateTime endDate;
        public Int32 itemId;
        //public String category;

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();


        public RepStockCardForm(DateTime dateStart, DateTime dateEnd, Int32 filterItemId/*, String categoryFilter*/)
        {
            InitializeComponent();

            label1.Text = SetLabel(label1.Text);
            buttonClose.Text = SetLabel(buttonClose.Text);
            buttonPrint.Text = SetLabel(buttonPrint.Text);

            startDate = dateStart;
            endDate = dateEnd;
            itemId = filterItemId;
            //category = categoryFilter;

            GetStockCardReportDataSource("");
            GetDataGridViewCollectionDetailReportSource();
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

        public List<Entities.DgvRepInventoryStockCardListEntity> GetStockCardReportListData(DateTime startDate, DateTime endDate, Int32 itemId, String filter/*, String category*/)
        {
            List<Entities.DgvRepInventoryStockCardListEntity> rowList = new List<Entities.DgvRepInventoryStockCardListEntity>();

            Controllers.RepInventoryReportController repInvetoryReportController = new Controllers.RepInventoryReportController();

            var stockCardReportList = repInvetoryReportController.StockCardReport(startDate, endDate, itemId, filter/*, category*/);
            if (stockCardReportList.Any())
            {
                var row = from d in stockCardReportList
                          select new Entities.DgvRepInventoryStockCardListEntity
                          {
                              ColumnInventoryDate = d.InventoryDate,
                              ColumnDocument = d.Document,
                              ColumnBegQuantity = d.BeginningQuantity.ToString("#,##0.00"),
                              ColumnInQuantity = d.InQuantity.ToString("#,##0.00"),
                              ColumnOutQuantity = d.OutQuantity.ToString("#,##0.00"),
                              ColumnEndingQuantity = d.EndingQuantity.ToString("#,##0.00"),
                              ColumnRunningQuantity = d.RunningQuantity.ToString("#,##0.00"),
                          };

                rowList = row.ToList();
            }

            return rowList;
        }

        public void GetStockCardReportDataSource(String filter)
        {
            stockCardReportList = GetStockCardReportListData(startDate, endDate, itemId, filter/*, category*/);
            if (stockCardReportList.Any())
            {
                pageList = new PagedList<Entities.DgvRepInventoryStockCardListEntity>(stockCardReportList, pageNumber, pageSize);

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
                dataStockCardReportListSource.DataSource = pageList;
            }
            else
            {
                buttonPageListFirst.Enabled = false;
                buttonPageListPrevious.Enabled = false;
                buttonPageListNext.Enabled = false;
                buttonPageListLast.Enabled = false;

                dataStockCardReportListSource.Clear();
                textBoxPageNumber.Text = "0 / 0";
            }
        }

        public void GetDataGridViewCollectionDetailReportSource()
        {
            dataGridViewInventoryReport.DataSource = dataStockCardReportListSource;
        }

        private void buttonPageListFirst_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvRepInventoryStockCardListEntity>(stockCardReportList, 1, pageSize);
            dataStockCardReportListSource.DataSource = pageList;

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
                pageList = new PagedList<Entities.DgvRepInventoryStockCardListEntity>(stockCardReportList, --pageNumber, pageSize);
                dataStockCardReportListSource.DataSource = pageList;
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
                pageList = new PagedList<Entities.DgvRepInventoryStockCardListEntity>(stockCardReportList, ++pageNumber, pageSize);
                dataStockCardReportListSource.DataSource = pageList;
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
            pageList = new PagedList<Entities.DgvRepInventoryStockCardListEntity>(stockCardReportList, pageList.PageCount, pageSize);
            dataStockCardReportListSource.DataSource = pageList;

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

        private void textBoxFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetStockCardReportDataSource(textBoxFilter.Text);
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
                    String[] header = {
                        "Document",
                        "Date",
                        "Beginning Quantity",
                        "In Quantity",
                        "Out Quantity",
                        "Ending Quantity",

                    };

                    csv.AppendLine(String.Join(",", header));

                    if (stockCardReportList.Any())
                    {
                        Decimal totalEndingQty = 0;

                        foreach (var stockCardReport in stockCardReportList)
                        {
                            String[] data = {
                              stockCardReport.ColumnDocument.Replace(",", ""),
                              stockCardReport.ColumnInventoryDate.ToShortDateString().Replace(",", ""),
                              stockCardReport.ColumnBegQuantity.Replace(",", ""),
                              stockCardReport.ColumnInQuantity.Replace(",", ""),
                              stockCardReport.ColumnOutQuantity.Replace(",", ""),
                              stockCardReport.ColumnEndingQuantity.Replace(",", ""),
                            };
                            csv.AppendLine(String.Join(",", data));

                            totalEndingQty += Convert.ToDecimal(stockCardReport.ColumnEndingQuantity);
                        }
                        String[] data1 = {
                              "",
                              "",
                              "",
                              "",
                              "Total Ending Quantity:",
                              totalEndingQty.ToString("#,##0.00").Replace(",", "")
                            };
                        csv.AppendLine(String.Join(",", data1));
                    }

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\StockCard_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("utf-8"));

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
            new RepStockCardReportPDFForm(startDate, endDate, textBoxFilter.Text, itemId);
        }
    }
}
