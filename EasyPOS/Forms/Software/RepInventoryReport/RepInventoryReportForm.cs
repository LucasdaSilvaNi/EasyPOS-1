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
    public partial class RepInventoryReportForm : Form
    {
        public List<Entities.DgvRepInventoryInventoryReportListEntity> inventoryReportList;
        public BindingSource dataInventoryReportListSource = new BindingSource();
        public PagedList<Entities.DgvRepInventoryInventoryReportListEntity> pageList;
        public Int32 pageNumber = 1;
        public Int32 pageSize = 50;

        public DateTime startDate;
        public DateTime endDate;
        public String category;
        public Int32 itemId;

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public RepInventoryReportForm(DateTime dateStart, DateTime dateEnd, String filterItemCategory, Int32 itemIds)
        {
            InitializeComponent();

            buttonClose.Text = SetLabel(buttonClose.Text);
            buttonPrint.Text = SetLabel(buttonPrint.Text);
            label1.Text = SetLabel(label1.Text);

            startDate = dateStart;
            endDate = dateEnd;
            category = filterItemCategory;
            itemId = itemIds;

            GetInventoryReportDataSource("");
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

        public List<Entities.DgvRepInventoryInventoryReportListEntity> GetInventoryReportListData(DateTime startDate, DateTime endDate, String category, String filter, Int32 itemId)
        {
            List<Entities.DgvRepInventoryInventoryReportListEntity> rowList = new List<Entities.DgvRepInventoryInventoryReportListEntity>();

            Controllers.RepInventoryReportController repInvetoryReportController = new Controllers.RepInventoryReportController();

            var inventoryReportList = repInvetoryReportController.InventoryReport(startDate, endDate, category, filter, itemId);
            if (inventoryReportList.Any())
            {
                Decimal totalAmount = 0;
                var row = from d in inventoryReportList
                          select new Entities.DgvRepInventoryInventoryReportListEntity
                          {
                              ColumnItemCode = d.ItemCode,
                              ColumnBarCode = d.BarCode,
                              ColumnItemDescription = d.ItemDescription,
                              ColumnUnit = d.Unit,
                              ColumnBegQuantity = d.BeginningQuantity.ToString("#,##0.00"),
                              ColumnInQuantity = d.InQuantity.ToString("#,##0.00"),
                              ColumnOutQuantity = d.OutQuantity.ToString("#,##0.00"),
                              ColumnEndingQuantity = d.EndingQuantity.ToString("#,##0.00"),
                              ColumnStockCount = d.CountQuantity.ToString("#,##0.00"),
                              ColumnVariance = d.Variance.ToString("#,##0.00"),
                              ColumnCost = d.Cost.ToString("#,##0.00"),
                              ColumnItemPrice = d.Price.ToString("#,##0.00"),
                              ColumnAmount = d.Amount.ToString("#,##0.00")
                          };

                totalAmount = inventoryReportList.Sum(d => d.Amount);

                textBoxTotalAmount.Text = totalAmount.ToString("#,##0.00");

                rowList = row.ToList();

            }
            return rowList;
        }

        public void GetInventoryReportDataSource(String filter)
        {
            inventoryReportList = GetInventoryReportListData(startDate, endDate, category, filter, itemId);
            if (inventoryReportList.Any())
            {

                pageList = new PagedList<Entities.DgvRepInventoryInventoryReportListEntity>(inventoryReportList, pageNumber, pageSize);

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
                dataInventoryReportListSource.DataSource = pageList;
            }
            else
            {
                buttonPageListFirst.Enabled = false;
                buttonPageListPrevious.Enabled = false;
                buttonPageListNext.Enabled = false;
                buttonPageListLast.Enabled = false;

                dataInventoryReportListSource.Clear();
                textBoxPageNumber.Text = "0 / 0";
            }
        }

        public void GetDataGridViewCollectionDetailReportSource()
        {
            dataGridViewInventoryReport.DataSource = dataInventoryReportListSource;
        }

        private void buttonPageListFirst_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvRepInventoryInventoryReportListEntity>(inventoryReportList, 1, pageSize);
            dataInventoryReportListSource.DataSource = pageList;

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
                pageList = new PagedList<Entities.DgvRepInventoryInventoryReportListEntity>(inventoryReportList, --pageNumber, pageSize);
                dataInventoryReportListSource.DataSource = pageList;
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
                pageList = new PagedList<Entities.DgvRepInventoryInventoryReportListEntity>(inventoryReportList, ++pageNumber, pageSize);
                dataInventoryReportListSource.DataSource = pageList;
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
            pageList = new PagedList<Entities.DgvRepInventoryInventoryReportListEntity>(inventoryReportList, pageList.PageCount, pageSize);
            dataInventoryReportListSource.DataSource = pageList;

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
                GetInventoryReportDataSource(textBoxFilter.Text);
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
                        "Item Code",
                        "Barcode",
                        "Item Description",
                        "Unit",
                        "Beginning Quantity",
                        "In Quantity",
                        "Out Quantity",
                        "Ending Quantity",
                        "Count Quantity",
                        "Variance Quantity",
                        "Cost",
                        "Price",
                        "Amount"
                    };

                    csv.AppendLine(String.Join(",", header));

                    if (inventoryReportList.Any())
                    {
                        foreach (var inventoryReport in inventoryReportList)
                        {
                            String[] data = {
                              "=" + "\"" + inventoryReport.ColumnItemCode +"\"",
                              "=" + "\"" + inventoryReport.ColumnBarCode +"\"",
                              inventoryReport.ColumnItemDescription.Replace(",", ""),
                              inventoryReport.ColumnUnit.Replace(",", ""),
                              inventoryReport.ColumnBegQuantity.Replace(",", ""),
                              inventoryReport.ColumnInQuantity.Replace(",", ""),
                              inventoryReport.ColumnOutQuantity.Replace(",", ""),
                              inventoryReport.ColumnEndingQuantity.Replace(",", ""),
                              inventoryReport.ColumnStockCount.Replace(",", ""),
                              inventoryReport.ColumnVariance.Replace(",", ""),
                              inventoryReport.ColumnCost.Replace(",", ""),
                              inventoryReport.ColumnItemPrice.Replace(",", ""),
                              inventoryReport.ColumnAmount.Replace(",", "")
                            };

                            csv.AppendLine(String.Join(",", data));
                        }
                    }

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\InventoryReport_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("iso-8859-1"));

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
            new RepInventoryReportPDFForm(startDate, endDate, category, textBoxFilter.Text, itemId);
        }
    }
}
