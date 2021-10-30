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
    public partial class RepCollectionSummaryReport : Form
    {
        public List<Entities.DgvRepSalesReportCollectionSummaryReportEntityList> collectionList;
        public BindingSource dataCollectionListSource = new BindingSource();
        public PagedList<Entities.DgvRepSalesReportCollectionSummaryReportEntityList> pageList;
        public Int32 pageNumber = 1;
        public Int32 pageSize = 50;

        public DateTime dateStart;
        public DateTime dateEnd;
        public Int32 idTerminal;

        public RepCollectionSummaryReport(DateTime startDate, DateTime endDate, Int32 terminalId)
        {
            InitializeComponent();
            dateStart = startDate;
            dateEnd = endDate;
            idTerminal = terminalId;

            GetCollectionSummaryListDataSource();
            GetDgvCollectionSource();
        }

        public List<Entities.DgvRepSalesReportCollectionSummaryReportEntityList> GetCollectionListData(DateTime startDate, DateTime endDate, Int32 terminalId)
        {
            List<Entities.DgvRepSalesReportCollectionSummaryReportEntityList> rowList = new List<Entities.DgvRepSalesReportCollectionSummaryReportEntityList>();

            Controllers.RepSalesReportController repCollectionReportController = new Controllers.RepSalesReportController();

            var collectionList = repCollectionReportController.CollectionSummaryReport(startDate, endDate, terminalId);
            if (collectionList.Any())
            {
                Decimal totalAmount = 0;
                var row = from d in collectionList
                          select new Entities.DgvRepSalesReportCollectionSummaryReportEntityList
                          {
                              ColumnTerminal = d.Terminal,
                              ColumnCollectionDate = d.CollectionDate,
                              ColumnCollectionNumber = d.CollectionNumber,
                              ColumnCustomerCode = d.CustomerCode,
                              ColumnCustomer = d.Customer,
                              ColumnSalesNumber = d.SalesNumber,
                              ColumnRemarks = d.Remarks,
                              ColumnPreparedBy = d.PreparedByUserName,
                              ColumnIsCancelled = d.IsCancelled,
                              ColumnAmount = d.IsCancelled == true ? "0" : d.Amount.ToString("#,##0.00"),
                              ColumnEntryDateTime = d.EntryDateTime
                          };

                totalAmount = collectionList.Where(d => d.IsCancelled == false).Sum(d => d.Amount - d.ChangeAmount);

                textBoxTotalAmount.Text = totalAmount.ToString("#,##0.00");

                rowList = row.ToList();
            }
            return rowList;
        }

        public void GetCollectionSummaryListDataSource()
        {
            collectionList = GetCollectionListData(dateStart, dateEnd, idTerminal);
            if (collectionList.Any())
            {

                pageList = new PagedList<Entities.DgvRepSalesReportCollectionSummaryReportEntityList>(collectionList, pageNumber, pageSize);

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
                dataCollectionListSource.DataSource = pageList;
            }
            else
            {
                buttonPageListFirst.Enabled = false;
                buttonPageListPrevious.Enabled = false;
                buttonPageListNext.Enabled = false;
                buttonPageListLast.Enabled = false;

                dataCollectionListSource.Clear();
                textBoxPageNumber.Text = "0 / 0";
            }
        }

        public void GetDgvCollectionSource()
        {
            dataGridViewCollectionReport.DataSource = dataCollectionListSource;
        }

        private void buttoncollectionListPageListFirst_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvRepSalesReportCollectionSummaryReportEntityList>(collectionList, 1, pageSize);
            dataCollectionListSource.DataSource = pageList;

            buttonPageListFirst.Enabled = false;
            buttonPageListPrevious.Enabled = false;
            buttonPageListNext.Enabled = true;
            buttonPageListLast.Enabled = true;

            pageNumber = 1;
            textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
        }

        private void buttoncollectionListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (pageList.HasPreviousPage == true)
            {
                pageList = new PagedList<Entities.DgvRepSalesReportCollectionSummaryReportEntityList>(collectionList, --pageNumber, pageSize);
                dataCollectionListSource.DataSource = pageList;
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

        private void buttoncollectionListPageListNext_Click(object sender, EventArgs e)
        {
            if (pageList.HasNextPage == true)
            {
                pageList = new PagedList<Entities.DgvRepSalesReportCollectionSummaryReportEntityList>(collectionList, ++pageNumber, pageSize);
                dataCollectionListSource.DataSource = pageList;
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

        private void buttoncollectionListPageListLast_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvRepSalesReportCollectionSummaryReportEntityList>(collectionList, pageList.PageCount, pageSize);
            dataCollectionListSource.DataSource = pageList;

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
                    DateTime startDate = dateStart;
                    DateTime endDate = dateEnd;

                    StringBuilder csv = new StringBuilder();
                    String[] header = { "Terminal", "Collection Date", "Collection Number", "Customer Code", "Customer", "Sales Number", "Remarks", "PreparedBy", "Cancelled", "Amount" };
                    csv.AppendLine(String.Join(",", header));

                    if (collectionList.Any())
                    {
                        foreach (var collection in collectionList)
                        {
                            String customerCode = "";
                            if (collection.ColumnCustomerCode != null)
                            {
                                customerCode = collection.ColumnCustomerCode.Replace(",", "");
                            }

                            String[] data = {collection.ColumnTerminal,
                                        collection.ColumnCollectionDate,
                                        collection.ColumnCollectionNumber,
                                        customerCode,
                                        collection.ColumnCustomer.Replace("," , ""),
                                        collection.ColumnSalesNumber,
                                        collection.ColumnRemarks.Replace(",", String.Empty).Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty),
                                        collection.ColumnPreparedBy.Replace(",", ""),
                                        collection.ColumnIsCancelled.ToString(),
                                        collection.ColumnAmount.Replace("," , ""),
                                        collection.ColumnEntryDateTime,
                            };

                            csv.AppendLine(String.Join(",", data));
                        }
                    }

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\CollectionSummaryReport_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("utf-8"));

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
            new RepCollectionSummaryReportPDFForm(dateStart, dateEnd, idTerminal);
        }
    }
}
