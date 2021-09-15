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
    public partial class RepItemExpiryReportForm : Form
    {
        public List<Entities.DgvItemExpiryEntity> itemList;
        public BindingSource dataItemListSource = new BindingSource();
        public PagedList<Entities.DgvItemExpiryEntity> pageList;
        public Int32 pageNumber = 1;
        public Int32 pageSize = 50;

        public DateTime dateStart;
        public DateTime dateEnd;
        public RepItemExpiryReportForm(DateTime startDate, DateTime endDate)
        {
            dateStart = startDate;
            dateEnd = endDate;
            InitializeComponent();
            GetInventoryListDataSource();
            GetItemExpiryDataGridSource();
        }
        public List<Entities.DgvItemExpiryEntity> GetInventoryListReport(DateTime startDate, DateTime endDate)
        {
            List<Entities.DgvItemExpiryEntity> rowList = new List<Entities.DgvItemExpiryEntity>();

            Controllers.RepInventoryReportController repInvetoryReportController = new Controllers.RepInventoryReportController();

            var inventoryListReport = repInvetoryReportController.GetItemExpiryReport(startDate, endDate);
            if (inventoryListReport.Any())
            {
                var row = from d in inventoryListReport
                          select new Entities.DgvItemExpiryEntity
                          {
                              ColumnItemListCode = d.ItemCode,
                              ColumnItemListBarcode = d.BarCode,
                              ColumnItem = d.ItemDescription,
                              ColumnOnHandQuantity = Convert.ToDecimal(d.Quantity).ToString("#,##0.00"),
                              ColumnUnit = d.Unit,
                              ColumnCost = Convert.ToDecimal(d.Cost).ToString("#,##0.00"),
                              ColumnPrice = Convert.ToDecimal(d.Price).ToString("#,##0.00"),
                              ColumnExpiryDate = d.ExpiryDate,
                          };

                rowList = row.ToList();

            }
            return rowList;
        }
        public void GetInventoryListDataSource()
        {
            itemList = GetInventoryListReport(dateStart, dateEnd);
            if (itemList.Any())
            {

                pageList = new PagedList<Entities.DgvItemExpiryEntity>(itemList, pageNumber, pageSize);

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
                dataItemListSource.DataSource = pageList;
            }
            else
            {
                buttonPageListFirst.Enabled = false;
                buttonPageListPrevious.Enabled = false;
                buttonPageListNext.Enabled = false;
                buttonPageListLast.Enabled = false;

                dataItemListSource.Clear();
                textBoxPageNumber.Text = "0 / 0";
            }
        }
        public void GetItemExpiryDataGridSource()
        {
            dataGridViewItemExpiryReport.DataSource = dataItemListSource;
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonPageListFirst_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvItemExpiryEntity>(itemList, 1, pageSize);
            dataItemListSource.DataSource = pageList;

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
                pageList = new PagedList<Entities.DgvItemExpiryEntity>(itemList, --pageNumber, pageSize);
                dataItemListSource.DataSource = pageList;
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
                pageList = new PagedList<Entities.DgvItemExpiryEntity>(itemList, ++pageNumber, pageSize);
                dataItemListSource.DataSource = pageList;
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
            pageList = new PagedList<Entities.DgvItemExpiryEntity>(itemList, pageList.PageCount, pageSize);
            dataItemListSource.DataSource = pageList;

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
                    String[] header = { "Item Code", "Barcode", "Item Description","Unit", "Cost", "Price", "ExpiryDate" };
                    csv.AppendLine(String.Join(",", header));

                    if (itemList.Any())
                    {
                        foreach (var item in itemList)
                        {
                            String expiryDate = "";
                            if (item.ColumnExpiryDate != null)
                            {
                                expiryDate = item.ColumnExpiryDate.ToString().Replace(",", "");
                            }
                            String[] data = {
                               "="+"\""+ item.ColumnItemListCode +"\"",
                               "="+"\""+ item.ColumnItemListBarcode +"\"",
                                item.ColumnItem.Replace("," , ""),
                                item.ColumnUnit.Replace("," , ""),
                                item.ColumnCost.Replace("," , ""),
                                item.ColumnPrice.Replace("," , ""),
                                expiryDate,
                            };
                            csv.AppendLine(String.Join(",", data));
                        }
                    }

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\ItemExpiryReport_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("iso-8859-1"));

                    MessageBox.Show("Generate CSV Successful!", "Generate CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dataGridViewItemExpiryReport_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (Int32 i = 0; i < itemList.Count(); i++)
            {
                if (Convert.ToDateTime(itemList[i].ColumnExpiryDate) < DateTime.Today)
                {
                    dataGridViewItemExpiryReport.Rows[i].Cells[7].Style.BackColor = Color.Red;
                    dataGridViewItemExpiryReport.Rows[i].Cells[7].Style.ForeColor = Color.White;
                }
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            new RepItemExpiryReportPDFForm(dateStart, dateEnd);
        }
    }
}
