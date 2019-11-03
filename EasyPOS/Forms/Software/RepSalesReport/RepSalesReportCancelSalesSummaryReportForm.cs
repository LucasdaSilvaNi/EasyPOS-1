﻿using PagedList;
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
    public partial class RepSalesReportCancelSalesSummaryReportForm : Form
    {
        public List<Entities.DgvSalesReportCancelSalesReportEntity> cancelledSalesList;
        public BindingSource dataCancelSalesListSource = new BindingSource();
        public PagedList<Entities.DgvSalesReportCancelSalesReportEntity> pageList;
        public Int32 pageNumber = 1;
        public Int32 pageSize = 50;

        public DateTime dateStart;
        public DateTime dateEnd;

        public RepSalesReportCancelSalesSummaryReportForm(DateTime startDate, DateTime endDate)
        {
            InitializeComponent();
            dateStart = startDate;
            dateEnd = endDate;

            GetCancelSalesListDataSource();
            GetCancelSalesSummaryReportSource();
        }

        public List<Entities.DgvSalesReportCancelSalesReportEntity> GetCancelSalesSummaryListData(DateTime startDate, DateTime endDate)
        {
            List<Entities.DgvSalesReportCancelSalesReportEntity> rowList = new List<Entities.DgvSalesReportCancelSalesReportEntity>();

            Controllers.RepSalesReportController repSalesReportController = new Controllers.RepSalesReportController();

            var cancelSalesList = repSalesReportController.CancelSalesSummaryList(startDate, endDate);
            if (cancelSalesList.Any())
            {
                Decimal totalAmount = 0;
                var row = from d in cancelSalesList
                          select new Entities.DgvSalesReportCancelSalesReportEntity
                          {
                              ColumnId = d.Id,
                              ColumnPeriodId = d.Id,
                              ColumnPeriod = d.Period,
                              ColumnTerminal = d.Terminal,
                              ColumnSalesDate = d.SalesDate,
                              ColumnSalesNumber = d.SalesNumber,
                              ColumnManualInvoiceNumber = d.ManualInvoiceNumber,
                              ColumnAmount = d.Amount.ToString("#,##0.00"),
                              ColumnTableId = d.TableId,
                              ColumnCustomerId = d.CustomerId,
                              ColumnCustomer = d.Customer,
                              ColumnAccountId = d.AccountId,
                              ColumnTermId = d.TermId,
                              ColumnTerm = d.Term,
                              ColumnDiscountId = d.DiscountId,
                              ColumnRemarks = d.Remarks,
                              ColumnTerminalId = d.TerminalId,
                              ColumnPreparedBy = d.PreparedBy,
                              ColumnPreparedByUserName = d.PreparedByUserName,
                              ColumnPax = d.Pax,
                              ColumnTable = d.Table,
                          };

                totalAmount = cancelSalesList.Sum(d => d.Amount);

                textBoxTotalAmount.Text = totalAmount.ToString("#,##0.00");

                rowList = row.ToList();

            }
            return rowList;
        }

        public void GetCancelSalesListDataSource()
        {
            cancelledSalesList = GetCancelSalesSummaryListData(dateStart, dateEnd);
            if (cancelledSalesList.Any())
            {

                pageList = new PagedList<Entities.DgvSalesReportCancelSalesReportEntity>(cancelledSalesList, pageNumber, pageSize);

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
                dataCancelSalesListSource.DataSource = pageList;
            }
            else
            {
                buttonPageListFirst.Enabled = false;
                buttonPageListPrevious.Enabled = false;
                buttonPageListNext.Enabled = false;
                buttonPageListLast.Enabled = false;

                dataCancelSalesListSource.Clear();
                textBoxPageNumber.Text = "0 / 0";
            }
        }

        public void GetCancelSalesSummaryReportSource() {
            dataGridCancelSalesSummaryReport.DataSource = dataCancelSalesListSource;
        }
      

        private void buttonSalesListPageListFirst_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvSalesReportCancelSalesReportEntity>(cancelledSalesList, 1, pageSize);
            dataCancelSalesListSource.DataSource = pageList;

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
                pageList = new PagedList<Entities.DgvSalesReportCancelSalesReportEntity>(cancelledSalesList, --pageNumber, pageSize);
                dataCancelSalesListSource.DataSource = pageList;
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
                pageList = new PagedList<Entities.DgvSalesReportCancelSalesReportEntity>(cancelledSalesList, ++pageNumber, pageSize);
                dataCancelSalesListSource.DataSource = pageList;
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
            pageList = new PagedList<Entities.DgvSalesReportCancelSalesReportEntity>(cancelledSalesList, pageList.PageCount, pageSize);
            dataCancelSalesListSource.DataSource = pageList;

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
                    String[] header = { "Terminal", "Date", "Cancelled Sales Number", "Manual Invoice No.", "Customer", "Term", "Remarks", "Prepared By", "Amount", "Pax", "Table" };
                    csv.AppendLine(String.Join(",", header));

                    if (cancelledSalesList.Any())
                    {
                        foreach (var sales in cancelledSalesList)
                        {
                            String[] data = {sales.ColumnTerminal,
                                sales.ColumnSalesDate,
                                sales.ColumnSalesNumber,
                                sales.ColumnManualInvoiceNumber,
                                sales.ColumnCustomer.Replace("," , " "),
                                sales.ColumnTerm,
                                sales.ColumnRemarks,
                                sales.ColumnPreparedByUserName,
                                sales.ColumnAmount,
                                sales.ColumnPax.ToString(),
                                sales.ColumnTable
                            };

                            csv.AppendLine(String.Join(",", data));
                        }
                    }

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\CancelledSalesSummaryReport_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("iso-8859-1"));

                    MessageBox.Show("Generate CSV Successful!", "Generate CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}