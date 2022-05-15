using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PagedList;

namespace EasyPOS.Forms.Software.SysDispatchStation
{
    public partial class SysDispatchStationForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public List<Entities.DgvSysDispatchStationSalesListEntity> salesList;
        public BindingSource dataSalesListSource = new BindingSource();
        public PagedList<Entities.DgvSysDispatchStationSalesListEntity> pageList;
        public Int32 pageNumber = 1;
        public Int32 pageSize = 50;
        public Boolean isAutoRefresh = false;

        public SerialPort serialPort = null;
        public String cancelRemarks = "";
        public Boolean continueCancel = false;

        public String orderStatus = "New";

        public SysDispatchStationForm(SysSoftwareForm softwareForm)
        {
            InitializeComponent();
            sysSoftwareForm = softwareForm;

            String currentDate = DateTime.Today.ToShortDateString() + "\t\t";
            if (Modules.SysCurrentModule.GetCurrentSettings().IsLoginDate == true)
            {
                currentDate = Modules.SysCurrentModule.GetCurrentSettings().CurrentDate + "\t\t";
            }

            dateTimePickerSalesDate.Value = Convert.ToDateTime(currentDate);

            sysUserRights = new Modules.SysUserRightsModule("TrnSales");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                GetTerminalList();
                timerRefreshSalesListGrid.Start();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            sysSoftwareForm.RemoveTabPage();
        }

        private void dateTimePickerSalesDate_ValueChanged(object sender, EventArgs e)
        {
            pageNumber = 1;
            UpdateSalesListGridDataSource();
        }

        public void GetTerminalList()
        {
            Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
            if (trnPOSSalesController.DropdownListTerminal().Any())
            {
                comboBoxTerminal.DataSource = trnPOSSalesController.DropdownListTerminal();
                comboBoxTerminal.ValueMember = "Id";
                comboBoxTerminal.DisplayMember = "Terminal";

                comboBoxTerminal.SelectedValue = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().TerminalId);

                UpdateSalesListGridDataSource();
                CreateSalesListDataGrid();
            }
        }

        private void comboBoxTerminal_SelectedIndexChanged(object sender, EventArgs e)
        {
            pageNumber = 1;
            UpdateSalesListGridDataSource();
        }

        private void textBoxSalesListFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateSalesListGridDataSource();
            }
        }

        public void UpdateSalesListGridDataSource()
        {
            DateTime salesDate = dateTimePickerSalesDate.Value.Date;
            Int32 terminalId = Convert.ToInt32(comboBoxTerminal.SelectedValue);
            String filter = textBoxSalesListFilter.Text;

            GetSalesListDataAsync(salesDate, terminalId, filter, orderStatus);
        }

        public async void GetSalesListDataAsync(DateTime salesDate, Int32 terminalId, String filter, String orderStatus)
        {
            salesList = await GetSalesListDataTask(salesDate, terminalId, filter, orderStatus);
            if (salesList.Any())
            {
                dataGridViewSalesList.BeginInvoke((MethodInvoker)delegate ()
                {
                    pageList = new PagedList<Entities.DgvSysDispatchStationSalesListEntity>(salesList, pageNumber, pageSize);

                    if (pageList.PageCount == 1)
                    {
                        buttonSalesListPageListFirst.Enabled = false;
                        buttonSalesListPageListPrevious.Enabled = false;
                        buttonSalesListPageListNext.Enabled = false;
                        buttonSalesListPageListLast.Enabled = false;
                    }
                    else if (pageNumber == 1)
                    {
                        buttonSalesListPageListFirst.Enabled = false;
                        buttonSalesListPageListPrevious.Enabled = false;
                        buttonSalesListPageListNext.Enabled = true;
                        buttonSalesListPageListLast.Enabled = true;
                    }
                    else if (pageNumber == pageList.PageCount)
                    {
                        buttonSalesListPageListFirst.Enabled = true;
                        buttonSalesListPageListPrevious.Enabled = true;
                        buttonSalesListPageListNext.Enabled = false;
                        buttonSalesListPageListLast.Enabled = false;
                    }
                    else
                    {
                        buttonSalesListPageListFirst.Enabled = true;
                        buttonSalesListPageListPrevious.Enabled = true;
                        buttonSalesListPageListNext.Enabled = true;
                        buttonSalesListPageListLast.Enabled = true;
                    }

                    textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
                    dataSalesListSource.DataSource = pageList;

                    CurrentSelectedCell(dataGridViewSalesList.CurrentCell.RowIndex);
                });
            }
            else
            {
                buttonSalesListPageListFirst.Enabled = false;
                buttonSalesListPageListPrevious.Enabled = false;
                buttonSalesListPageListNext.Enabled = false;
                buttonSalesListPageListLast.Enabled = false;

                dataSalesListSource.Clear();
                textBoxPageNumber.Text = "0 / 0";

                CurrentSelectedCell(-1);
            }
        }

        public async Task<List<Entities.DgvSysDispatchStationSalesListEntity>> GetSalesListDataTask(DateTime salesDate, Int32 terminalId, String filter, String orderStatus)
        {
            return await Task.Factory.StartNew(() =>
            {
                List<Entities.DgvSysDispatchStationSalesListEntity> rowList = new List<Entities.DgvSysDispatchStationSalesListEntity>();

                Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
                var salesList = trnPOSSalesController.ListOrders(salesDate, terminalId, filter, orderStatus);
                if (salesList.Any())
                {
                    var row = from d in salesList
                              select new Entities.DgvSysDispatchStationSalesListEntity
                              {
                                  ColumnButtonDispatch = "Dispatch",
                                  ColumnId = d.Id,
                                  ColumnTerminal = d.Terminal,
                                  ColumnSalesDate = d.SalesDate,
                                  ColumnSalesNumber = d.SalesNumber,
                                  ColumnManualSalesNumber = d.ManualInvoiceNumber,
                                  ColumnCustomer = d.Customer,
                                  ColumnCustomerAddress = d.Remarks,
                                  ColumnDelivery = d.DeliveryDriver,
                                  ColumnNumberOfItems = d.NumberOfItems.ToString("#,##0"),
                                  ColumnIsLocked = d.IsLocked,
                                  ColumnIsTendered = d.IsTendered,
                                  ColumnIsCancelled = d.IsCancelled,
                                  ColumnIsDispatched = d.IsDispatched,
                                  ColumnPrepared = d.NumberOfItemsPrepared.ToString("#,##0"),
                                  ColumnStatus = d.Status,
                                  ColumnSpace = ""
                              };

                    rowList = row.ToList();
                }

                return rowList;
            });
        }

        public void CreateSalesListDataGrid()
        {
            dataGridViewSalesList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewSalesList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewSalesList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewSalesList.DataSource = dataSalesListSource;
        }

        private void dataGridViewSalesList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                CurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewSalesList.CurrentCell.ColumnIndex == dataGridViewSalesList.Columns["ColumnButtonDispatch"].Index)
            {
                DialogResult dispatchDialogResult = MessageBox.Show("Dispatch Order?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dispatchDialogResult == DialogResult.Yes)
                {
                    Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
                    String[] dispatchSales = trnPOSSalesController.DispatchSales(Convert.ToInt32(dataGridViewSalesList.Rows[e.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnId"].Index].Value));
                    if (dispatchSales[1].Equals("0") == false)
                    {
                        pageNumber = 1;

                        UpdateSalesListGridDataSource();
                    }
                    else
                    {
                        MessageBox.Show(dispatchSales[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void CurrentSelectedCell(Int32 rowIndex)
        {
            dataGridViewSalesLineItemDisplay.Rows.Clear();
            dataGridViewSalesLineItemDisplay.Refresh();

            if (rowIndex == -1)
            {
                labelManualSalesNumber.Text = "0000000000";
                labelCustomerName.Text = "";
                labelDeliveryMan.Text = "";
                textBoxCustomerAddress.Text = "";
                textBoxTimeOrdered.Text = "HH:MM:SS";
            }
            else
            {
                labelManualSalesNumber.Text = dataGridViewSalesList.Rows[rowIndex].Cells[dataGridViewSalesList.Columns["ColumnManualSalesNumber"].Index].Value.ToString();
                labelCustomerName.Text = dataGridViewSalesList.Rows[rowIndex].Cells[dataGridViewSalesList.Columns["ColumnCustomer"].Index].Value.ToString();
                labelDeliveryMan.Text = dataGridViewSalesList.Rows[rowIndex].Cells[dataGridViewSalesList.Columns["ColumnDelivery"].Index].Value.ToString();
                textBoxCustomerAddress.Text = dataGridViewSalesList.Rows[rowIndex].Cells[dataGridViewSalesList.Columns["ColumnCustomerAddress"].Index].Value.ToString();

                Controllers.TrnSalesLineController trnPOSSalesLineController = new Controllers.TrnSalesLineController();
                if (trnPOSSalesLineController.ListSalesLine(Convert.ToInt32(dataGridViewSalesList.Rows[rowIndex].Cells[dataGridViewSalesList.Columns["ColumnId"].Index].Value)).Any())
                {
                    textBoxTimeOrdered.Text = trnPOSSalesLineController.ListSalesLine(Convert.ToInt32(dataGridViewSalesList.Rows[rowIndex].Cells[dataGridViewSalesList.Columns["ColumnId"].Index].Value)).OrderByDescending(d => d.Id).FirstOrDefault().SalesLineTimeStamp;

                    var groupedSalesLineItems = from d in trnPOSSalesLineController.ListSalesLine(Convert.ToInt32(dataGridViewSalesList.Rows[rowIndex].Cells[dataGridViewSalesList.Columns["ColumnId"].Index].Value))
                                                group d by new
                                                {
                                                    d.ItemDescription,
                                                    d.Unit,
                                                    d.Price,
                                                    d.Tax
                                                } into g
                                                select new
                                                {
                                                    g.Key.ItemDescription,
                                                    g.Key.Unit,
                                                    g.Key.Price,
                                                    g.Key.Tax,
                                                    Quantity = g.Sum(s => s.Quantity),
                                                    DiscountAmount = g.Sum(s => s.DiscountAmount),
                                                    Amount = g.Sum(s => s.Amount)
                                                };

                    var salesLineItemList = groupedSalesLineItems.ToList();
                    if (salesLineItemList.Any())
                    {
                        foreach (var salesLineItem in salesLineItemList)
                        {
                            dataGridViewSalesLineItemDisplay.Rows.Add(
                                salesLineItem.Quantity.ToString("#,##0.00"),
                                salesLineItem.ItemDescription + "   " + salesLineItem.Unit + Environment.NewLine + " @P" + salesLineItem.Price.ToString("#,##0.00") + " Less: " + salesLineItem.DiscountAmount.ToString("#,##0.00") + " - " + salesLineItem.Tax,
                                salesLineItem.Amount.ToString("#,##0.00")
                            );
                        }
                    }
                }
            }
        }

        private void buttonSalesListPageListFirst_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvSysDispatchStationSalesListEntity>(salesList, 1, pageSize);
            dataSalesListSource.DataSource = pageList;

            buttonSalesListPageListFirst.Enabled = false;
            buttonSalesListPageListPrevious.Enabled = false;
            buttonSalesListPageListNext.Enabled = true;
            buttonSalesListPageListLast.Enabled = true;

            pageNumber = 1;
            textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
        }

        private void buttonSalesListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (pageList.HasPreviousPage == true)
            {
                pageList = new PagedList<Entities.DgvSysDispatchStationSalesListEntity>(salesList, --pageNumber, pageSize);
                dataSalesListSource.DataSource = pageList;
            }

            buttonSalesListPageListNext.Enabled = true;
            buttonSalesListPageListLast.Enabled = true;

            if (pageNumber == 1)
            {
                buttonSalesListPageListFirst.Enabled = false;
                buttonSalesListPageListPrevious.Enabled = false;
            }

            textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
        }

        private void buttonSalesListPageListNext_Click(object sender, EventArgs e)
        {
            if (pageList.HasNextPage == true)
            {
                pageList = new PagedList<Entities.DgvSysDispatchStationSalesListEntity>(salesList, ++pageNumber, pageSize);
                dataSalesListSource.DataSource = pageList;
            }

            buttonSalesListPageListFirst.Enabled = true;
            buttonSalesListPageListPrevious.Enabled = true;

            if (pageNumber == pageList.PageCount)
            {
                buttonSalesListPageListNext.Enabled = false;
                buttonSalesListPageListLast.Enabled = false;
            }

            textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
        }

        private void buttonSalesListPageListLast_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvSysDispatchStationSalesListEntity>(salesList, pageList.PageCount, pageSize);
            dataSalesListSource.DataSource = pageList;

            buttonSalesListPageListFirst.Enabled = true;
            buttonSalesListPageListPrevious.Enabled = true;
            buttonSalesListPageListNext.Enabled = false;
            buttonSalesListPageListLast.Enabled = false;

            pageNumber = pageList.PageCount;
            textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
        }

        private void timerRefreshSalesListGrid_Tick(object sender, EventArgs e)
        {
            if (isAutoRefresh == true)
            {
                UpdateSalesListGridDataSource();
            }
        }

        private void buttonAutoRefresh_Click(object sender, EventArgs e)
        {
            if (isAutoRefresh == true)
            {
                isAutoRefresh = false;

                buttonAutoRefresh.Text = "Start";
                buttonAutoRefresh.BackColor = ColorTranslator.FromHtml("#7FBC00");
                buttonAutoRefresh.ForeColor = Color.White;
            }
            else
            {
                isAutoRefresh = true;

                buttonAutoRefresh.Text = "Stop";
                buttonAutoRefresh.BackColor = ColorTranslator.FromHtml("#F34F1C");
                buttonAutoRefresh.ForeColor = Color.White;
            }
        }

        private void buttonNewOrders_Click(object sender, EventArgs e)
        {
            orderStatus = "New";
            UpdateSalesListGridDataSource();

            buttonNewOrders.BackColor = ColorTranslator.FromHtml("#01A6F0");
            buttonDispatchedOrders.BackColor = ColorTranslator.FromHtml("#7fbc00");
            buttonDeliveredOrders.BackColor = ColorTranslator.FromHtml("#7fbc00");

            dataGridViewSalesList.Columns[dataGridViewSalesList.Columns["ColumnButtonDispatch"].Index].Visible = true;
        }

        private void buttonDispatchedOrders_Click(object sender, EventArgs e)
        {
            orderStatus = "Dispatched";
            UpdateSalesListGridDataSource();

            buttonNewOrders.BackColor = ColorTranslator.FromHtml("#7fbc00");
            buttonDispatchedOrders.BackColor = ColorTranslator.FromHtml("#01A6F0");
            buttonDeliveredOrders.BackColor = ColorTranslator.FromHtml("#7fbc00");

            dataGridViewSalesList.Columns[dataGridViewSalesList.Columns["ColumnButtonDispatch"].Index].Visible = false;
        }

        private void buttonDeliveredOrders_Click(object sender, EventArgs e)
        {
            orderStatus = "Delivered";
            UpdateSalesListGridDataSource();

            buttonNewOrders.BackColor = ColorTranslator.FromHtml("#7fbc00");
            buttonDispatchedOrders.BackColor = ColorTranslator.FromHtml("#7fbc00");
            buttonDeliveredOrders.BackColor = ColorTranslator.FromHtml("#01A6F0");

            dataGridViewSalesList.Columns[dataGridViewSalesList.Columns["ColumnButtonDispatch"].Index].Visible = false;
        }
    }
}
