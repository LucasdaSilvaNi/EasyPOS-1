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
    public partial class RepCollectionDetailReportForm : Form
    {
        public List<Entities.DgvRepSalesReportCollectionDetailReportListEntity> collectionDetailList;
        public BindingSource dataCollectionDetailListSource = new BindingSource();
        public PagedList<Entities.DgvRepSalesReportCollectionDetailReportListEntity> pageList;
        public Int32 pageNumber = 1;
        public Int32 pageSize = 50;

        public DateTime dateStart;
        public DateTime dateEnd;
        public Int32 idTerminal;

        public RepCollectionDetailReportForm(DateTime startDate, DateTime endDate, Int32 terminalId)
        {
            InitializeComponent();

            dateStart = startDate;
            dateEnd = endDate;
            idTerminal = terminalId;

            GetCollectionDetailListDataSource();
            GetDataGridViewCollectionDetailReportSource();
        }

        public List<Entities.DgvRepSalesReportCollectionDetailReportListEntity> GetCollectionDetailListData(DateTime startDate, DateTime endDate, Int32 terminalId)
        {
            String facepayImagePath = Modules.SysCurrentModule.GetCurrentSettings().FacepayImagePath;

            List<Entities.DgvRepSalesReportCollectionDetailReportListEntity> rowList = new List<Entities.DgvRepSalesReportCollectionDetailReportListEntity>();

            Controllers.RepSalesReportController repSalesReportController = new Controllers.RepSalesReportController();

            var collectionDetailList = repSalesReportController.CollectionDetailReport(startDate, endDate, terminalId);
            if (collectionDetailList.Any())
            {
                Decimal totalAmount = 0;
                var row = from d in collectionDetailList
                          select new Entities.DgvRepSalesReportCollectionDetailReportListEntity
                          {
                              ColumnTerminal = d.Terminal,
                              ColumnCollectionDate = d.CollectionDate,
                              ColumnCollectionNumber = d.CollectionNumber,
                              ColumnCustomerCode = d.CustomerCode,
                              ColumnCustomer = d.Customer,
                              ColumnSalesNumber = d.SalesNumber,
                              ColumnPayType = d.PayType,
                              ColumnAmount = d.IsCancelled == true ? "0" : d.PayTypeCode.Equals("CASH") ? (d.Amount - d.ChangeAmount).ToString("#,##0.00") : d.Amount.ToString("#,##0.00"),
                              ColumnCheckNumber = d.CheckNumber,
                              ColumnCheckDate = d.CheckDate,
                              ColumnCheckBank = d.CheckBank,
                              ColumnOtherInformation = d.OtherInformation,
                              ColumnPhoto = Directory.Exists(facepayImagePath) == true ? File.Exists(facepayImagePath + "\\" + d.CollectionNumber + ".jpeg") == true ? Image.FromFile(facepayImagePath + "\\" + d.CollectionNumber + ".jpeg") : null : null
                          };

                totalAmount = collectionDetailList.Sum(d => d.IsCancelled == true ? 0 : d.PayTypeCode.Equals("CASH") ? d.Amount - d.ChangeAmount : d.Amount);

                textBoxTotalAmount.Text = totalAmount.ToString("#,##0.00");

                rowList = row.ToList();
            }
            return rowList;
        }

        public void GetCollectionDetailListDataSource()
        {
            collectionDetailList = GetCollectionDetailListData(dateStart, dateEnd, idTerminal);
            if (collectionDetailList.Any())
            {

                pageList = new PagedList<Entities.DgvRepSalesReportCollectionDetailReportListEntity>(collectionDetailList, pageNumber, pageSize);

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
                dataCollectionDetailListSource.DataSource = pageList;
            }
            else
            {
                buttonPageListFirst.Enabled = false;
                buttonPageListPrevious.Enabled = false;
                buttonPageListNext.Enabled = false;
                buttonPageListLast.Enabled = false;

                dataCollectionDetailListSource.Clear();
                textBoxPageNumber.Text = "0 / 0";
            }
        }

        public void GetDataGridViewCollectionDetailReportSource()
        {
            dataGridViewCollectionDetailReport.DataSource = dataCollectionDetailListSource;
        }


        private void buttonCollectionListPageListFirst_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvRepSalesReportCollectionDetailReportListEntity>(collectionDetailList, 1, pageSize);
            dataCollectionDetailListSource.DataSource = pageList;

            buttonPageListFirst.Enabled = false;
            buttonPageListPrevious.Enabled = false;
            buttonPageListNext.Enabled = true;
            buttonPageListLast.Enabled = true;

            pageNumber = 1;
            textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
        }

        private void buttonCollectionListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (pageList.HasPreviousPage == true)
            {
                pageList = new PagedList<Entities.DgvRepSalesReportCollectionDetailReportListEntity>(collectionDetailList, --pageNumber, pageSize);
                dataCollectionDetailListSource.DataSource = pageList;
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

        private void buttonCollectionListPageListNext_Click(object sender, EventArgs e)
        {
            if (pageList.HasNextPage == true)
            {
                pageList = new PagedList<Entities.DgvRepSalesReportCollectionDetailReportListEntity>(collectionDetailList, ++pageNumber, pageSize);
                dataCollectionDetailListSource.DataSource = pageList;
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

        private void buttonCollectionListPageListLast_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvRepSalesReportCollectionDetailReportListEntity>(collectionDetailList, pageList.PageCount, pageSize);
            dataCollectionDetailListSource.DataSource = pageList;

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
                    String[] header = { "Terminal", "Collection Date", "Collection Number", "Customer Code", "Customer",
                        "Sales Number", "PayType", "Amount",  "CheckNumber", "CheckDate", "CheckBank", "OtherInformation"
                    };
                    csv.AppendLine(String.Join(",", header));


                    if (collectionDetailList.Any())
                    {
                        foreach (var collectionDetail in collectionDetailList)
                        {
                            String customerCode = "";
                            String checkNumber = "";
                            String checkDate = "";
                            String checkBank = "";
                            String otherInformation = "";



                            if (collectionDetail.ColumnCustomerCode != null)
                            {
                                customerCode = collectionDetail.ColumnCustomerCode.Replace(",", " ");
                            }

                            if (collectionDetail.ColumnCheckNumber != null)
                            {
                                checkNumber = collectionDetail.ColumnCheckNumber.Replace(",", " ");
                            }

                            if (collectionDetail.ColumnCheckDate != null)
                            {
                                otherInformation = collectionDetail.ColumnCheckDate.Replace(",", " ");
                            }

                            if (collectionDetail.ColumnCheckBank != null)
                            {
                                otherInformation = collectionDetail.ColumnCheckBank.Replace(",", " ");
                            }

                            if (collectionDetail.ColumnOtherInformation != null)
                            {
                                otherInformation = collectionDetail.ColumnOtherInformation.Replace(",", " ");
                            }


                            String[] data = {
                                collectionDetail.ColumnTerminal,
                                collectionDetail.ColumnCollectionDate,
                                collectionDetail.ColumnCollectionNumber,
                                customerCode,
                                collectionDetail.ColumnCustomer.Replace("," , " "),
                                collectionDetail.ColumnSalesNumber,
                                collectionDetail.ColumnPayType.Replace("," , " "),
                                collectionDetail.ColumnAmount.Replace("," , ""),
                                checkNumber,
                                checkDate,
                                checkBank,
                                otherInformation
                            };

                            csv.AppendLine(String.Join(",", data));
                        }
                    }

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\CollectionDetailReport_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("utf-8"));

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
            Close();
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            new RepCollectionDetailReportPDFForm(dateStart, dateEnd, idTerminal);
        }
    }
}
