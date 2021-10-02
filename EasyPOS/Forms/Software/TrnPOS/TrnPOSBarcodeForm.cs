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

namespace EasyPOS.Forms.Software.TrnPOS
{
    public partial class TrnPOSBarcodeForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public List<Entities.DgvTrnSalesListEntity> salesList;
        public BindingSource dataSalesListSource = new BindingSource();
        public PagedList<Entities.DgvTrnSalesListEntity> pageList;
        public Int32 pageNumber = 1;
        public Int32 pageSize = 50;
        public Boolean isAutoRefresh = false;

        public SerialPort serialPort = null;
        public String cancelRemarks = "";
        public Boolean continueCancel = false;

        public List<String> lockOption;
        public Boolean isTerminalSelected = false;
        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public TrnPOSBarcodeForm(SysSoftwareForm softwareForm)
        {
            InitializeComponent();

            Controllers.SysLanguageController sysLabel = new Controllers.SysLanguageController();
            if (sysLabel.ListLanguage("").Any())
            {
                sysLanguageEntities = sysLabel.ListLanguage("");
                var language = Modules.SysCurrentModule.GetCurrentSettings().Language;
                if (language != "English")
                {
                    buttonTender.Text = SetLabel(buttonTender.Text);
                    label1.Text = SetLabel(label1.Text);
                    buttonReprint.Text = SetLabel(buttonReprint.Text);
                    buttonCancel.Text = SetLabel(buttonCancel.Text);
                    buttonSales.Text = SetLabel(buttonSales.Text);
                    buttonClose.Text = SetLabel(buttonClose.Text);
                    dataGridViewSalesList.Columns[5].HeaderText = SetLabel(dataGridViewSalesList.Columns[5].HeaderText);
                    dataGridViewSalesList.Columns[6].HeaderText = SetLabel(dataGridViewSalesList.Columns[6].HeaderText);
                    dataGridViewSalesList.Columns[7].HeaderText = SetLabel(dataGridViewSalesList.Columns[7].HeaderText);
                    dataGridViewSalesList.Columns[8].HeaderText = SetLabel(dataGridViewSalesList.Columns[8].HeaderText);
                    dataGridViewSalesList.Columns[9].HeaderText = SetLabel(dataGridViewSalesList.Columns[9].HeaderText);
                    dataGridViewSalesList.Columns[10].HeaderText = SetLabel(dataGridViewSalesList.Columns[10].HeaderText);
                    dataGridViewSalesList.Columns[11].HeaderText = SetLabel(dataGridViewSalesList.Columns[11].HeaderText);
                    dataGridViewSalesList.Columns[12].HeaderText = SetLabel(dataGridViewSalesList.Columns[12].HeaderText);
                    dataGridViewSalesList.Columns[13].HeaderText = SetLabel(dataGridViewSalesList.Columns[13].HeaderText);
                    dataGridViewSalesList.Columns[14].HeaderText = SetLabel(dataGridViewSalesList.Columns[14].HeaderText);
                    label2.Text = SetLabel(label2.Text);
                    label3.Text = SetLabel(label3.Text);
                    label4.Text = SetLabel(label4.Text);
                    label5.Text = SetLabel(label5.Text);
                    label6.Text = SetLabel(label6.Text);
                    label7.Text = SetLabel(label7.Text);
                    label8.Text = SetLabel(label8.Text);
                    dataGridViewSalesLineItemDisplay.Columns[0].HeaderText = SetLabel(dataGridViewSalesLineItemDisplay.Columns[0].HeaderText);
                    dataGridViewSalesLineItemDisplay.Columns[1].HeaderText = SetLabel(dataGridViewSalesLineItemDisplay.Columns[1].HeaderText);
                    dataGridViewSalesLineItemDisplay.Columns[2].HeaderText = SetLabel(dataGridViewSalesLineItemDisplay.Columns[2].HeaderText);
                    buttonSalesListPageListFirst.Text = SetLabel(buttonSalesListPageListFirst.Text);
                    buttonSalesListPageListPrevious.Text = SetLabel(buttonSalesListPageListPrevious.Text);
                    buttonSalesListPageListNext.Text = SetLabel(buttonSalesListPageListNext.Text);
                    buttonSalesListPageListLast.Text = SetLabel(buttonSalesListPageListLast.Text);
                    buttonAutoRefresh.Text = SetLabel(buttonAutoRefresh.Text);
                    labelLastChange.Text = SetLabel(labelLastChange.Text);
                    ColumnEdit.Text = SetLabel(ColumnEdit.Text);
                    ColumnDelete.Text = SetLabel(ColumnDelete.Text);
                }
            }
            sysSoftwareForm = softwareForm;
            if (Modules.SysCurrentModule.GetCurrentSettings().HideSalesAmount == true)
            {
                ColumnSalesLlineAmount.Visible = false;
                ColumnAmount.Visible = false;
            }
            else
            {
                ColumnSalesLlineAmount.Visible = true;
                ColumnAmount.Visible = true;
            }
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
                if (sysUserRights.GetUserRights().CanAdd == false)
                {
                    buttonSales.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanTender == false)
                {
                    buttonTender.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanPrint == false)
                {
                    buttonReprint.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanCancel == false)
                {
                    buttonCancel.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanEdit == false)
                {
                    dataGridViewSalesList.Columns[0].Visible = false;
                }

                if (sysUserRights.GetUserRights().CanDelete == false)
                {
                    dataGridViewSalesList.Columns[1].Visible = false;
                }

                lockOption = new List<String>
                {
                    "All",
                    "Locked",
                    "Unlocked",
                    "Tendered",
                    "Cancelled"
                };
                comboBoxLockOption.DataSource = lockOption;

                GetTerminalList();
                timerRefreshSalesListGrid.Start();
            }

            Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
            textBoxLastChange.Text = trnPOSSalesController.GetLastChange(Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().TerminalId)).ToString("#,##0.00");
        }

        public string SetLabel(string label)
        {
            if (sysLanguageEntities.Any())
            {
                foreach (var displayedLabel in sysLanguageEntities)
                {
                    if (displayedLabel.Label == label)
                    {
                        return displayedLabel.DisplayedLabel;
                    }
                }
            }
            return label;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            sysSoftwareForm.RemoveTabPage();
        }

        private void buttonSales_Click(object sender, EventArgs e)
        {
            if (Modules.SysCurrentModule.GetCurrentSettings().PromptLoginSales == true)
            {
                Account.SysLogin.SysLoginForm login = new Account.SysLogin.SysLoginForm(this, null, null, null, false);
                login.ShowDialog();
            }
            else
            {
                newSales();
            }
        }

        public void buttonAutoSales()
        {
            if (Modules.SysCurrentModule.GetCurrentSettings().PromptLoginSales == true)
            {
                Account.SysLogin.SysLoginForm login = new Account.SysLogin.SysLoginForm(this, null, null, null, false);
                login.ShowDialog();
            }
            else
            {
                newSales();
            }
        }

        public void newSales()
        {
            Int32 customerId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().WalkinCustomerId);

            Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
            String[] addSales = trnPOSSalesController.AddSales("", customerId);
            if (addSales[1].Equals("0") == false)
            {
                sysSoftwareForm.AddTabPagePOSSalesDetail(this, trnPOSSalesController.DetailSales(Convert.ToInt32(addSales[1])));
                UpdateSalesListGridDataSource();
            }
            else
            {
                MessageBox.Show(addSales[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            if (isTerminalSelected == true)
            {
                UpdateSalesListGridDataSource();
            }
            else
            {
                isTerminalSelected = true;
            }
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
            String selectedIsLocked = Convert.ToString(comboBoxLockOption.SelectedValue);

            GetSalesListDataAsync(salesDate, terminalId, filter, selectedIsLocked);

            Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
            textBoxLastChange.Text = trnPOSSalesController.GetLastChange(Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().TerminalId)).ToString("#,##0.00");

        }

        public async void GetSalesListDataAsync(DateTime salesDate, Int32 terminalId, String filter, String isLocked)
        {
            salesList = await GetSalesListDataTask(salesDate, terminalId, filter, isLocked);
            if (salesList.Any())
            {
                dataGridViewSalesList.BeginInvoke((MethodInvoker)delegate ()
                {
                    pageList = new PagedList<Entities.DgvTrnSalesListEntity>(salesList, pageNumber, pageSize);

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

        public async Task<List<Entities.DgvTrnSalesListEntity>> GetSalesListDataTask(DateTime salesDate, Int32 terminalId, String filter, String selectedIsLocked)
        {
            return await Task.Factory.StartNew(() =>
            {
                List<Entities.DgvTrnSalesListEntity> rowList = new List<Entities.DgvTrnSalesListEntity>();

                Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
                var salesList = trnPOSSalesController.ListSales(salesDate, terminalId, filter, selectedIsLocked);

                if (salesList.Any())
                {
                    Boolean isHiddenValueSalesAmount = Modules.SysCurrentModule.GetCurrentSettings().HideSalesAmount;
                    var row = from d in salesList
                              select new Entities.DgvTrnSalesListEntity
                              {
                                  ColumnDelete = "Delete",
                                  ColumnEdit = "Edit",
                                  ColumnId = d.Id,
                                  ColumnTerminal = d.Terminal,
                                  ColumnSalesDate = d.SalesDate,
                                  ColumnSalesNumber = d.SalesNumber,
                                  ColumnRececiptInvoiceNumber = d.CollectionNumber,
                                  ColumnCustomerCode = d.CustomerCode,
                                  ColumnCustomer = d.Customer,
                                  ColumnSalesAgent = d.SalesAgentUserName,
                                  //ColumnAmount = isHiddenValueSalesAmount == true ? hideSalesAmmount(d.Amount.ToString("#,##0.00")) : d.Amount.ToString("#,##0.00"),
                                  ColumnAmount = d.Amount.ToString("#,##0.00"),
                                  ColumnIsLocked = d.IsLocked,
                                  ColumnIsTendered = d.IsTendered,
                                  ColumnIsCancelled = d.IsCancelled,
                                  ColumnRemarks = d.Remarks,
                                  ColumnSpace = "",
                                  ColumnTable = d.Table,
                                  ColumnManualSalesNumber = d.ManualInvoiceNumber,
                                  ColumnDelivery = d.DeliveryDriver
                              };

                    rowList = row.ToList();
                }

                return rowList;

            });
        }

        //public string hideSalesAmmount(String value)
        //{

        //    Decimal stringLength = value.Length;
        //    String hiddenValue = "";

        //    for (var i = 0; i < stringLength; i++)
        //    {
        //        hiddenValue += "*";
        //    }
        //    return hiddenValue;
        //}

        public void CreateSalesListDataGrid()
        {
            dataGridViewSalesList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewSalesList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewSalesList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewSalesList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewSalesList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewSalesList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewSalesList.DataSource = dataSalesListSource;
        }

        private void dataGridViewSalesList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                CurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewSalesList.CurrentCell.ColumnIndex == dataGridViewSalesList.Columns["ColumnEdit"].Index)
            {
                Boolean isTendered = Convert.ToBoolean(dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnIsTendered"].Index].Value);

                if (isTendered == true)
                {
                    MessageBox.Show("Already tendered.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
                    if (trnPOSSalesController.IsSalesTendered(Convert.ToInt32(dataGridViewSalesList.Rows[e.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnId"].Index].Value)) == true)
                    {
                        MessageBox.Show("Already tendered.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        sysSoftwareForm.AddTabPagePOSSalesDetail(this, trnPOSSalesController.DetailSales(Convert.ToInt32(dataGridViewSalesList.Rows[e.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnId"].Index].Value)));
                    }
                }
            }

            if (e.RowIndex > -1 && dataGridViewSalesList.CurrentCell.ColumnIndex == dataGridViewSalesList.Columns["ColumnDelete"].Index)
            {
                Boolean isLocked = Convert.ToBoolean(dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnIsLocked"].Index].Value);
                Boolean isTendered = Convert.ToBoolean(dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnIsTendered"].Index].Value);

                if (isLocked == true)
                {
                    MessageBox.Show("Already locked.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (isTendered == true)
                {
                    MessageBox.Show("Already tendered.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
                    if (trnPOSSalesController.IsSalesTendered(Convert.ToInt32(dataGridViewSalesList.Rows[e.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnId"].Index].Value)) == true)
                    {
                        MessageBox.Show("Already tendered.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        DialogResult deleteDialogResult = MessageBox.Show("Delete Sales?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (deleteDialogResult == DialogResult.Yes)
                        {
                            String[] deleteSales = trnPOSSalesController.DeleteSales(Convert.ToInt32(dataGridViewSalesList.Rows[e.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnId"].Index].Value));
                            if (deleteSales[1].Equals("0") == false)
                            {
                                pageNumber = 1;
                                UpdateSalesListGridDataSource();
                            }
                            else
                            {
                                MessageBox.Show(deleteSales[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        public void CurrentSelectedCell(Int32 rowIndex)
        {
            dataGridViewSalesLineItemDisplay.Rows.Clear();
            dataGridViewSalesLineItemDisplay.Refresh();

            Controllers.TrnSalesController newSales = new Controllers.TrnSalesController();
            var detail = newSales.DetailSales(rowIndex);

            if (detail != null)
            {
                sysSoftwareForm.displayTimeStamp(detail.EntryUserUserName, detail.EntryDateTime + " " + detail.EntryTime, detail.UpdatedUserUserName, detail.UpdateDateTime + " " + detail.UpdateTime);
            }

            if (rowIndex == -1)
            {
                labelTerminal.Text = "";
                labelTransactionDate.Text = "";
                labelInvoiceNumber.Text = "";
                labelReceiptInvoiceNumber.Text = "";
                labelCustomerCode.Text = "";
                labelCustomer.Text = "";
                labelPreparedBy.Text = "";
            }
            else
            {
                labelTerminal.Text = dataGridViewSalesList.Rows[rowIndex].Cells[dataGridViewSalesList.Columns["ColumnTerminal"].Index].Value.ToString();
                labelTransactionDate.Text = dataGridViewSalesList.Rows[rowIndex].Cells[dataGridViewSalesList.Columns["ColumnSalesDate"].Index].Value.ToString();
                labelInvoiceNumber.Text = dataGridViewSalesList.Rows[rowIndex].Cells[dataGridViewSalesList.Columns["ColumnSalesNumber"].Index].Value.ToString();

                String receiptInvoiceNumber = "";
                if (dataGridViewSalesList.Rows[rowIndex].Cells[dataGridViewSalesList.Columns["ColumnRececiptInvoiceNumber"].Index].Value != null)
                {
                    receiptInvoiceNumber = dataGridViewSalesList.Rows[rowIndex].Cells[dataGridViewSalesList.Columns["ColumnRececiptInvoiceNumber"].Index].Value.ToString();
                }

                labelReceiptInvoiceNumber.Text = receiptInvoiceNumber;
                labelCustomerCode.Text = dataGridViewSalesList.Rows[rowIndex].Cells[dataGridViewSalesList.Columns["ColumnCustomerCode"].Index].Value.ToString();
                labelCustomer.Text = dataGridViewSalesList.Rows[rowIndex].Cells[dataGridViewSalesList.Columns["ColumnCustomer"].Index].Value.ToString();
                labelPreparedBy.Text = dataGridViewSalesList.Rows[rowIndex].Cells[dataGridViewSalesList.Columns["ColumnSalesAgent"].Index].Value.ToString();

                Controllers.TrnSalesLineController trnPOSSalesLineController = new Controllers.TrnSalesLineController();

                if (trnPOSSalesLineController.ListSalesLine(Convert.ToInt32(dataGridViewSalesList.Rows[rowIndex].Cells[dataGridViewSalesList.Columns["ColumnId"].Index].Value)).Any())
                {
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
                            //Boolean isHiddenValueSalesAmount = Modules.SysCurrentModule.GetCurrentSettings().HideSalesAmount;

                            dataGridViewSalesLineItemDisplay.Rows.Add(
                                salesLineItem.Quantity.ToString("#,##0.00"),
                                salesLineItem.ItemDescription + "   " + salesLineItem.Unit + Environment.NewLine + " @P" + salesLineItem.Price.ToString("#,##0.00") + " Less: " + salesLineItem.DiscountAmount.ToString("#,##0.00") + " - " + salesLineItem.Tax,
                                //isHiddenValueSalesAmount == true ? hideSalesAmmount(salesLineItem.Amount.ToString("#,##0.00")) : salesLineItem.Amount.ToString("#,##0.00"));
                                salesLineItem.Amount.ToString("#,##0.00"));
                        };
                    }
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (Modules.SysCurrentModule.GetCurrentSettings().AllowCancelPreviousSales == true)
            {
                if (dataGridViewSalesList.Rows.Count != -1)
                {
                    if (dataGridViewSalesList.CurrentCell.RowIndex != -1)
                    {
                        Boolean isCanclled = Convert.ToBoolean(dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnIsCancelled"].Index].Value);
                        Boolean isTendered = Convert.ToBoolean(dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnIsTendered"].Index].Value);

                        if (isCanclled == true)
                        {
                            MessageBox.Show("Already Cancelled.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (isTendered == true)
                            {
                                TrnPOSCancelRemarksForm trnSalesListCancelRemarksForm = new TrnPOSCancelRemarksForm(sysSoftwareForm, this, null, Convert.ToInt32(dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[2].Value));
                                trnSalesListCancelRemarksForm.ShowDialog();

                                if (continueCancel)
                                {
                                    DialogResult cancelDialogResult = MessageBox.Show("Cancel Transaction? ", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (cancelDialogResult == DialogResult.Yes)
                                    {
                                        Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();

                                        String[] cancelSales = trnPOSSalesController.CancelSales(Convert.ToInt32(dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnId"].Index].Value), cancelRemarks);
                                        if (cancelSales[1].Equals("0") == false)
                                        {
                                            String collectionNumber = dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnRececiptInvoiceNumber"].Index].Value.ToString();
                                            MessageBox.Show(collectionNumber + " is successfully cancelled.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            UpdateSalesListGridDataSource();
                                        }
                                        else
                                        {
                                            MessageBox.Show(cancelSales[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select sales.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Sales empty.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (dataGridViewSalesList.Rows.Count > 0)
                {
                    if (dataGridViewSalesList.CurrentCell.RowIndex != -1)
                    {
                        Boolean isCanclled = Convert.ToBoolean(dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnIsCancelled"].Index].Value);
                        Boolean isTendered = Convert.ToBoolean(dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnIsTendered"].Index].Value);

                        if (isCanclled == true)
                        {
                            MessageBox.Show("Already Cancelled.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (isTendered == true)
                            {
                                TrnPOSCancelRemarksForm trnSalesListCancelRemarksForm = new TrnPOSCancelRemarksForm(sysSoftwareForm, this, null, Convert.ToInt32(dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[2].Value));
                                trnSalesListCancelRemarksForm.ShowDialog();

                                if (continueCancel)
                                {
                                    DialogResult cancelDialogResult = MessageBox.Show("Cancel Transaction? ", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (cancelDialogResult == DialogResult.Yes)
                                    {
                                        Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();

                                        if (trnPOSSalesController.CanCancelCollection(Convert.ToInt32(dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnId"].Index].Value)))
                                        {
                                            String[] cancelSales = trnPOSSalesController.CancelSales(Convert.ToInt32(dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnId"].Index].Value), cancelRemarks);
                                            if (cancelSales[1].Equals("0") == false)
                                            {
                                                String collectionNumber = dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnRececiptInvoiceNumber"].Index].Value.ToString();
                                                MessageBox.Show(collectionNumber + " is successfully cancelled.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                                UpdateSalesListGridDataSource();
                                            }
                                            else
                                            {
                                                MessageBox.Show(cancelSales[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Not allowed.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select sales.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Sales empty.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        public void SetCancelRemarks(String remarks)
        {
            cancelRemarks = remarks;
        }

        public void SetContinueCancel(Boolean cancel)
        {
            continueCancel = cancel;
        }

        private void buttonTender_Click(object sender, EventArgs e)
        {
            if (dataGridViewSalesList.Rows.Count > 0)
            {
                if (Convert.ToDecimal(dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnAmount"].Index].Value) > 0)
                {
                    if (dataGridViewSalesList.CurrentCell.RowIndex != -1)
                    {
                        Boolean isTendered = Convert.ToBoolean(dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnIsTendered"].Index].Value);

                        if (isTendered == true)
                        {
                            MessageBox.Show("Already tendered.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
                            if (trnPOSSalesController.IsSalesTendered(Convert.ToInt32(dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnId"].Index].Value)) == true)
                            {
                                MessageBox.Show("Already tendered.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                Modules.SysSerialPortModule.OpenSerialPort();

                                Entities.TrnSalesEntity newSalesEntity = new Entities.TrnSalesEntity
                                {
                                    Id = Convert.ToInt32(dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnId"].Index].Value),
                                    Amount = Convert.ToDecimal(dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnAmount"].Index].Value),
                                    SalesNumber = dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnSalesNumber"].Index].Value.ToString(),
                                    SalesDate = dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnSalesDate"].Index].Value.ToString(),
                                    CustomerCode = dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnCustomerCode"].Index].Value.ToString(),
                                    Customer = dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnCustomer"].Index].Value.ToString(),
                                    Remarks = dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnRemarks"].Index].Value.ToString()
                                };

                                String line1 = Modules.SysCurrentModule.GetCurrentSettings().CustomerDisplayFirstLineMessage;
                                String line2 = "P " + newSalesEntity.Amount.ToString("#,##0.00");

                                if (newSalesEntity.Amount > 0)
                                {
                                    line1 = "TOTAL:";
                                }

                                Modules.SysSerialPortModule.WriteSeralPortMessage(line1, line2);

                                TrnPOSTenderForm trnSalesDetailTenderForm = new TrnPOSTenderForm(sysSoftwareForm, this, null, null, null, newSalesEntity);
                                trnSalesDetailTenderForm.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select sales.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Cannot tender zero amount.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Sales empty.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonReprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewSalesList.Rows.Count > 1)
                {
                    if (dataGridViewSalesList.CurrentCell.RowIndex != -1)
                    {
                        Boolean isLocked = Convert.ToBoolean(dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnIsLocked"].Index].Value);
                        Boolean isTendered = Convert.ToBoolean(dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnIsTendered"].Index].Value);

                        if (isTendered != true)
                        {
                            MessageBox.Show("Not tendered.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (isLocked != true)
                        {
                            MessageBox.Show("Not locked.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            Int32 salesId = Convert.ToInt32(dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnId"].Index].Value);

                            Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
                            Int32 collectionId = trnPOSSalesController.GetCollectionId(Convert.ToInt32(dataGridViewSalesList.Rows[dataGridViewSalesList.CurrentCell.RowIndex].Cells[dataGridViewSalesList.Columns["ColumnId"].Index].Value));
                            if (collectionId != 0)
                            {
                                TrnPOSReprintForm trnSalesListReprintForm = new TrnPOSReprintForm(sysSoftwareForm, salesId, collectionId);
                                trnSalesListReprintForm.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("No collection.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select sales.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Sales empty.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSalesListPageListFirst_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvTrnSalesListEntity>(salesList, 1, pageSize);
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
                pageList = new PagedList<Entities.DgvTrnSalesListEntity>(salesList, --pageNumber, pageSize);
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
                pageList = new PagedList<Entities.DgvTrnSalesListEntity>(salesList, ++pageNumber, pageSize);
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
            pageList = new PagedList<Entities.DgvTrnSalesListEntity>(salesList, pageList.PageCount, pageSize);
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F2:
                    {
                        if (buttonTender.Enabled == true)
                        {
                            buttonTender.PerformClick();
                            Focus();
                        }

                        break;
                    }
                case Keys.F3:
                    {
                        if (buttonReprint.Enabled == true)
                        {
                            buttonReprint.PerformClick();
                            Focus();
                        }

                        break;
                    }
                case Keys.F4:
                    {
                        if (buttonCancel.Enabled == true)
                        {
                            buttonCancel.PerformClick();
                            Focus();
                        }

                        break;
                    }
                case Keys.F5:
                    {
                        if (buttonSales.Enabled == true)
                        {
                            buttonSales.PerformClick();
                            Focus();
                        }

                        break;
                    }
                case Keys.Escape:
                    {
                        if (buttonClose.Enabled == true)
                        {
                            buttonClose.PerformClick();
                            Focus();
                        }

                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dataGridViewSalesList_SelectionChanged(object sender, EventArgs e)
        {
            //CurrentSelectedCell(dataGridViewSalesList.CurrentCell.RowIndex);
        }

        private void comboBoxLockOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSalesListGridDataSource();
        }
    }
}
