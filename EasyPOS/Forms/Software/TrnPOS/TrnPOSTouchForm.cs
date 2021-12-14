using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.TrnPOS
{
    public partial class TrnPOSTouchForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public List<Entities.DgvTrnSalesListEntity> salesList;

        public BindingSource dataOpenSalesListSource = new BindingSource();
        public BindingSource dataBilledOutSalesListSource = new BindingSource();
        public BindingSource dataCollectedSalesListSource = new BindingSource();

        public TrnPOSTouchActivityForm trnPOSTouchActivityForm;

        private List<Entities.MstTableGroupEntity> listTableGroups = new List<Entities.MstTableGroupEntity>();
        private ToolTip tableGroupToolTip = new ToolTip();
        private const int tableGroupNoOfButtons = 6;
        private int tableGroupPages;
        private int tableGroupPage = 1;
        private Int32 selectedTableGroupId;

        private List<Entities.MstTableEntity> listTables = new List<Entities.MstTableEntity>();
        private ToolTip tableToolTip = new ToolTip();
        private const int tableNoOfButtons = 30;
        private int tablePages;
        private int tablePage = 1;
        private int selectedTableGroup = 1;
        Button[] tableButtons;

        public Boolean isTerminalSelected = false;

        public TrnPOSTouchForm(SysSoftwareForm softwareForm)
        {
            InitializeComponent();

            String currentDate = DateTime.Today.ToShortDateString() + "\t\t";
            if (Modules.SysCurrentModule.GetCurrentSettings().IsLoginDate == true)
            {
                currentDate = Modules.SysCurrentModule.GetCurrentSettings().CurrentDate + "\t\t";
            }

            dateTimePickerSalesDate.Value = Convert.ToDateTime(currentDate);

            sysUserRights = new Modules.SysUserRightsModule("TrnRestaurant");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (sysUserRights.GetUserRights().CanAdd == false)
                {
                    buttonWalkIn.Enabled = false;
                }

                GetTerminalList();

                sysSoftwareForm = softwareForm;
                if (Modules.SysCurrentModule.GetCurrentSettings().HideSalesAmount == true)
                {
                    textBoxTotalAmount.Visible = false;
                    tabPageCollectedColumnAmount.Visible = false;
                }
                else
                {
                    textBoxTotalAmount.Visible = true;
                    tabPageCollectedColumnAmount.Visible = true;
                }
            }

            Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
            textBoxLastChange.Text = trnPOSSalesController.GetLastChange(Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().TerminalId)).ToString("#,##0.00");

            sysSoftwareForm = softwareForm;

            Controllers.TrnSalesController trnSalesController = new Controllers.TrnSalesController();
            listTableGroups = trnSalesController.ListTableGroup();
            tableGroupPages = listTableGroups.Count();

            tableButtons = new Button[] {
                    buttonTable1,
                    buttonTable2,
                    buttonTable3,
                    buttonTable4,
                    buttonTable5,
                    buttonTable6,
                    buttonTable7,
                    buttonTable8,
                    buttonTable9,
                    buttonTable10,
                    buttonTable11,
                    buttonTable12,
                    buttonTable13,
                    buttonTable14,
                    buttonTable15,
                    buttonTable16,
                    buttonTable17,
                    buttonTable18,
                    buttonTable19,
                    buttonTable20,
                    buttonTable21,
                    buttonTable22,
                    buttonTable23,
                    buttonTable24,
                    buttonTable25,
                    buttonTable26,
                    buttonTable27,
                    buttonTable28,
                    buttonTable29,
                    buttonTable30
                };

            for (int i = 0; i < tableNoOfButtons; i++)
            {
                tableButtons[i].Click += new EventHandler(buttonTable_Click);
            }

            FillTableGroup();
        }

        private void FillTableGroup()
        {
            try
            {
                Button[] tableGroupButtons = new Button[] {
                    buttonTableGroup1,
                    buttonTableGroup2,
                    buttonTableGroup3,
                    buttonTableGroup4,
                    buttonTableGroup5,
                    buttonTableGroup6
                };

                for (int i = 0; i < tableGroupNoOfButtons; i++)
                {
                    tableGroupToolTip.SetToolTip(tableGroupButtons[i], "");
                    tableGroupButtons[i].Text = "";
                }

                var listTableGroupPage = listTableGroups.Skip((tableGroupPage - 1) * tableGroupNoOfButtons).Take(tableGroupNoOfButtons).ToList();
                if (listTableGroupPage.Any())
                {
                    for (int i = 0; i < listTableGroupPage.Count(); i++)
                    {
                        tableGroupToolTip.SetToolTip(tableGroupButtons[i], listTableGroupPage[i].Id.ToString());
                        tableGroupButtons[i].Text = listTableGroupPage[i].TableGroup;
                    }
                }

                selectedTableGroupId = listTableGroupPage[0].Id;
                FillTable(selectedTableGroupId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillTable(Int32 tableGroupId)
        {
            try
            {
                Controllers.TrnSalesController trnSalesController = new Controllers.TrnSalesController();

                listTables = trnSalesController.ListTable(tableGroupId, dateTimePickerSalesDate.Value.Date);
                tablePages = listTables.Count();

                for (int i = 0; i < tableNoOfButtons; i++)
                {
                    tableToolTip.SetToolTip(tableButtons[i], "");
                    tableButtons[i].Text = "";
                }

                var listTablePage = listTables.Skip((tablePage - 1) * tableNoOfButtons).Take(tableNoOfButtons).ToList();
                if (listTablePage.Any())
                {
                    for (int i = 0; i < listTablePage.Count(); i++)
                    {
                        tableToolTip.SetToolTip(tableButtons[i], listTablePage[i].TableCode.ToString());

                        if (listTablePage[i].HasSales == true)
                        {
                            tableButtons[i].BackColor = Color.Brown;
                            tableButtons[i].ForeColor = Color.White;
                        }
                        else
                        {
                            tableButtons[i].BackColor = SystemColors.Control;
                            tableButtons[i].ForeColor = Color.Black;
                        }

                        tableButtons[i].Text = listTablePage[i].TableCode;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonTable_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            String tableCode = tableToolTip.GetToolTip(b);

            Int32 customerId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().WalkinCustomerId);
            NewSales(tableCode, customerId);
        }

        public void NewSales(String tableCode, Int32 customerId)
        {
            try
            {
                Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
                Int32 salesId = trnPOSSalesController.getTableSalesId(tableCode, dateTimePickerSalesDate.Value.Date);

                if (salesId != 0)
                {
                    Entities.TrnSalesEntity salesEntity = trnPOSSalesController.DetailSales(salesId);

                    trnPOSTouchActivityForm = new TrnPOSTouchActivityForm(sysSoftwareForm, this, salesEntity);
                    trnPOSTouchActivityForm.ShowDialog();
                }
                else
                {
                    String[] addSales = trnPOSSalesController.AddSales(tableCode, customerId);

                    if (addSales[1].Equals("0") == false)
                    {
                        sysSoftwareForm.AddTabPagePOSTouchSalesDetail(this, trnPOSSalesController.DetailSales(Convert.ToInt32(addSales[1])));
                        UpdateSalesListGridDataSource();
                    }
                    else
                    {
                        MessageBox.Show(addSales[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            sysSoftwareForm.RemoveTabPage();
        }

        private void buttonWalkIn_Click(object sender, EventArgs e)
        {
            if (Modules.SysCurrentModule.GetCurrentSettings().PromptLoginSales == true)
            {
                Account.SysLogin.SysLoginForm login = new Account.SysLogin.SysLoginForm(null, null, this, null, false);
                login.ShowDialog();
            }
            else
            {
                NewWalkInSales();
            }

        }

        public void NewWalkInSales()
        {
            Int32 customerId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().WalkinCustomerId);

            Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
            String[] addSales = trnPOSSalesController.AddSales("Walk-in", customerId);
            if (addSales[1].Equals("0") == false)
            {
                sysSoftwareForm.AddTabPagePOSTouchSalesDetail(this, trnPOSSalesController.DetailSales(Convert.ToInt32(addSales[1])));
                UpdateSalesListGridDataSource();
            }
            else
            {
                MessageBox.Show(addSales[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControlSales_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                UpdateSalesListGridDataSource();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dateTimePickerSalesDate_ValueChanged(object sender, EventArgs e)
        {
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
            String filter = textBoxFilter.Text;

            GetSalesListDataAsync(salesDate, terminalId, filter);

            Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
            textBoxLastChange.Text = trnPOSSalesController.GetLastChange(Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().TerminalId)).ToString("#,##0.00");

            switch (selectedTableGroup)
            {
                case 1:
                    if (tableGroupToolTip.GetToolTip(buttonTableGroup1) != "")
                    {
                        Int32 tableGroupId = Convert.ToInt32(tableGroupToolTip.GetToolTip(buttonTableGroup1));
                        selectedTableGroupId = tableGroupId;
                        FillTable(tableGroupId);
                    }
                    break;
                case 2:
                    if (tableGroupToolTip.GetToolTip(buttonTableGroup2) != "")
                    {
                        Int32 tableGroupId = Convert.ToInt32(tableGroupToolTip.GetToolTip(buttonTableGroup2));
                        selectedTableGroupId = tableGroupId;
                        FillTable(tableGroupId);
                    }
                    break;
                case 3:
                    if (tableGroupToolTip.GetToolTip(buttonTableGroup3) != "")
                    {
                        Int32 tableGroupId = Convert.ToInt32(tableGroupToolTip.GetToolTip(buttonTableGroup3));
                        selectedTableGroupId = tableGroupId;
                        FillTable(tableGroupId);
                    }
                    break;
                case 4:
                    if (tableGroupToolTip.GetToolTip(buttonTableGroup4) != "")
                    {
                        Int32 tableGroupId = Convert.ToInt32(tableGroupToolTip.GetToolTip(buttonTableGroup4));
                        selectedTableGroupId = tableGroupId;
                        FillTable(tableGroupId);
                    }
                    break;
                case 5:
                    if (tableGroupToolTip.GetToolTip(buttonTableGroup5) != "")
                    {
                        Int32 tableGroupId = Convert.ToInt32(tableGroupToolTip.GetToolTip(buttonTableGroup5));
                        selectedTableGroupId = tableGroupId;
                        FillTable(tableGroupId);
                    }
                    break;
                case 6:
                    if (tableGroupToolTip.GetToolTip(buttonTableGroup6) != "")
                    {
                        Int32 tableGroupId = Convert.ToInt32(tableGroupToolTip.GetToolTip(buttonTableGroup6));
                        selectedTableGroupId = tableGroupId;
                        FillTable(tableGroupId);
                    }
                    break;
                default:
                    break;
            }
        }

        public async void GetSalesListDataAsync(DateTime salesDate, Int32 terminalId, String filter)
        {
            var salesList = await GetSalesListDataTask(salesDate, terminalId, filter);
            if (salesList.Any())
            {
                if (tabControlSales.SelectedTab == tabPageOpen)
                {
                    buttonTenderAll.Enabled = true;

                    var openSales = from d in salesList where d.ColumnIsLocked == false && d.ColumnIsCancelled == false select d;
                    if (openSales.Any())
                    {
                        dataOpenSalesListSource.DataSource = openSales;
                        textBoxTotalAmount.Text = openSales.Sum(d => Convert.ToDecimal(d.ColumnAmount)).ToString("#,##0.00");
                    }
                    else
                    {
                        dataOpenSalesListSource.Clear();
                        textBoxTotalAmount.Text = (0).ToString("#,##0.00");
                    }
                }

                if (tabControlSales.SelectedTab == tabPageBilledOut)
                {
                    buttonTenderAll.Enabled = true;

                    var billedOutSales = from d in salesList where d.ColumnIsLocked == true && d.ColumnIsTendered == false && d.ColumnIsCancelled == false select d;
                    if (billedOutSales.Any())
                    {
                        dataBilledOutSalesListSource.DataSource = billedOutSales;
                        textBoxTotalAmount.Text = billedOutSales.Sum(d => Convert.ToDecimal(d.ColumnAmount)).ToString("#,##0.00");
                    }
                    else
                    {
                        dataBilledOutSalesListSource.Clear();
                        textBoxTotalAmount.Text = (0).ToString("#,##0.00");
                    }
                }

                if (tabControlSales.SelectedTab == tabPageCollected)
                {
                    buttonTenderAll.Enabled = false;

                    var collectedSales = from d in salesList where d.ColumnIsTendered == true && d.ColumnIsCancelled == false select d;
                    if (collectedSales.Any())
                    {
                        dataCollectedSalesListSource.DataSource = collectedSales;
                        textBoxTotalAmount.Text = collectedSales.Sum(d => Convert.ToDecimal(d.ColumnAmount)).ToString("#,##0.00");
                    }
                    else
                    {
                        dataCollectedSalesListSource.Clear();
                        textBoxTotalAmount.Text = (0).ToString("#,##0.00");
                    }
                }
            }
            else
            {
                dataOpenSalesListSource.Clear();
                dataBilledOutSalesListSource.Clear();
                dataCollectedSalesListSource.Clear();
            }
        }

        public async Task<List<Entities.DgvTrnSalesListEntity>> GetSalesListDataTask(DateTime salesDate, Int32 terminalId, String filter)
        {
            return await Task.Factory.StartNew(() =>
            {
                List<Entities.DgvTrnSalesListEntity> rowList = new List<Entities.DgvTrnSalesListEntity>();

                Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
                var salesList = trnPOSSalesController.POSTTouchListSales(salesDate, terminalId, filter);
                if (salesList.Any())
                {
                    var row = from d in salesList
                              select new Entities.DgvTrnSalesListEntity
                              {
                                  ColumnEdit = "Edit",
                                  ColumnDelete = "Delete",
                                  ColumnId = d.Id,
                                  ColumnTerminal = d.Terminal,
                                  ColumnSalesNumber = d.SalesNumber,
                                  ColumnManualSalesNumber = d.ManualInvoiceNumber,
                                  ColumnRececiptInvoiceNumber = d.CollectionNumber,
                                  ColumnCustomerCode = d.CustomerCode,
                                  ColumnCustomer = d.Customer,
                                  ColumnSalesAgent = d.SalesAgentUserName,
                                  ColumnTable = d.Table,
                                  ColumnIsLocked = d.IsLocked,
                                  ColumnIsTendered = d.IsTendered,
                                  ColumnIsCancelled = d.IsCancelled,
                                  ColumnSalesDate = d.EntryDateTime.ToString(),
                                  ColumnSpace = "",
                                  ColumnRemarks = d.Remarks,
                                  ColumnAmount = d.Amount.ToString("#,##0.00"),
                                  ColumnDelivery = d.DeliveryDriver
                              };

                    rowList = row.ToList();
                }

                return rowList;
            });
        }

        public void ArrangeColumns()
        {
            dataGridViewOpenSalesList.AutoGenerateColumns = false;
            dataGridViewOpenSalesList.Columns["TabPageOpenColumnEdit"].DisplayIndex = 0;
            dataGridViewOpenSalesList.Columns["TabPageOpenColumnDelete"].DisplayIndex = 1;
            dataGridViewOpenSalesList.Columns["TabPageOpenColumnId"].DisplayIndex = 2;
            dataGridViewOpenSalesList.Columns["TabPageOpenColumnTerminal"].DisplayIndex = 3;
            dataGridViewOpenSalesList.Columns["TabPageOpenColumnSalesDate"].DisplayIndex = 4;
            dataGridViewOpenSalesList.Columns["TabPageOpenColumnSalesNumber"].DisplayIndex = 6;
            dataGridViewOpenSalesList.Columns["TabPageOpenColumnManualSalesNumber"].DisplayIndex = 7;
            dataGridViewOpenSalesList.Columns["TabPageOpenColumnRececiptInvoiceNumber"].DisplayIndex = 8;
            dataGridViewOpenSalesList.Columns["TabPageOpenColumnCustomerCode"].DisplayIndex = 9;
            dataGridViewOpenSalesList.Columns["TabPageOpenColumnCustomer"].DisplayIndex = 10;
            dataGridViewOpenSalesList.Columns["TabPageOpenColumnSalesAgent"].DisplayIndex = 12;
            dataGridViewOpenSalesList.Columns["TabPageOpenColumnTable"].DisplayIndex = 5;
            dataGridViewOpenSalesList.Columns["TabPageOpenColumnIsLocked"].DisplayIndex = 13;
            dataGridViewOpenSalesList.Columns["TabPageOpenColumnIsTendered"].DisplayIndex = 14;
            dataGridViewOpenSalesList.Columns["TabPageOpenColumnIsCancelled"].DisplayIndex = 15;
            dataGridViewOpenSalesList.Columns["TabPageOpenColumnRemarks"].DisplayIndex = 16;
            dataGridViewOpenSalesList.Columns["TabPageOpenColumnDelivery"].DisplayIndex = 17;
            dataGridViewOpenSalesList.Columns["TabPageOpenColumnAmount"].DisplayIndex = 11;
            dataGridViewOpenSalesList.Columns["TabPageOpenColumnSpace"].DisplayIndex = 18;

            dataGridViewBilledOutSalesList.AutoGenerateColumns = false;
            dataGridViewBilledOutSalesList.Columns["tabPageBilledOutColumnEdit"].DisplayIndex = 0;
            dataGridViewBilledOutSalesList.Columns["tabPageBilledOutColumnDelete"].DisplayIndex = 1;
            dataGridViewBilledOutSalesList.Columns["tabPageBilledOutColumnId"].DisplayIndex = 2;
            dataGridViewBilledOutSalesList.Columns["tabPageBilledOutColumnTerminal"].DisplayIndex = 3;
            dataGridViewBilledOutSalesList.Columns["tabPageBilledOutColumnSalesDate"].DisplayIndex = 4;
            dataGridViewBilledOutSalesList.Columns["tabPageBilledOutColumnSalesNumber"].DisplayIndex = 6;
            dataGridViewBilledOutSalesList.Columns["tabPageBilledOutColumnManualSalesNumber"].DisplayIndex = 7;
            dataGridViewBilledOutSalesList.Columns["tabPageBilledOutColumnInvoiceNumber"].DisplayIndex = 8;
            dataGridViewBilledOutSalesList.Columns["tabPageBilledOutColumnCustomerCode"].DisplayIndex = 9;
            dataGridViewBilledOutSalesList.Columns["tabPageBilledOutColumnCustomer"].DisplayIndex = 10;
            dataGridViewBilledOutSalesList.Columns["tabPageBilledOutColumnSalesAgent"].DisplayIndex = 12;
            dataGridViewBilledOutSalesList.Columns["tabPageBilledOutColumnTable"].DisplayIndex = 5;
            dataGridViewBilledOutSalesList.Columns["tabPageBilledOutColumnIsLocked"].DisplayIndex = 13;
            dataGridViewBilledOutSalesList.Columns["tabPageBilledOutColumnIsTendered"].DisplayIndex = 14;
            dataGridViewBilledOutSalesList.Columns["tabPageBilledOutColumnIsCancelled"].DisplayIndex = 15;
            dataGridViewBilledOutSalesList.Columns["tabPageBilledOutColumnRemarks"].DisplayIndex = 16;
            dataGridViewBilledOutSalesList.Columns["tabPageBilledOutColumnDelivery"].DisplayIndex = 17;
            dataGridViewBilledOutSalesList.Columns["tabPageBilledOutColumnAmount"].DisplayIndex = 11;
            dataGridViewBilledOutSalesList.Columns["tabPageBilledOutColumnSpace"].DisplayIndex = 18;

            dataGridViewCollectedSalesList.AutoGenerateColumns = false;
            dataGridViewCollectedSalesList.Columns["tabPageCollectedColumnEdit"].DisplayIndex = 0;
            dataGridViewCollectedSalesList.Columns["tabPageCollectedColumnDelete"].DisplayIndex = 1;
            dataGridViewCollectedSalesList.Columns["tabPageCollectedColumnId"].DisplayIndex = 2;
            dataGridViewCollectedSalesList.Columns["tabPageCollectedColumnTerminal"].DisplayIndex = 3;
            dataGridViewCollectedSalesList.Columns["tabPageCollectedColumnSalesDate"].DisplayIndex = 18;
            dataGridViewCollectedSalesList.Columns["tabPageCollectedColumnSalesNumber"].DisplayIndex = 5;
            dataGridViewCollectedSalesList.Columns["tabPageCollectedColumnManualSalesNumber"].DisplayIndex = 7;
            dataGridViewCollectedSalesList.Columns["tabPageCollectedColumnCollectionNumber"].DisplayIndex = 8;
            dataGridViewCollectedSalesList.Columns["tabPageCollectedColumnCustomerCode"].DisplayIndex = 9;
            dataGridViewCollectedSalesList.Columns["tabPageCollectedColumnCustomer"].DisplayIndex = 10;
            dataGridViewCollectedSalesList.Columns["tabPageCollectedColumnSalesAgent"].DisplayIndex = 12;
            dataGridViewCollectedSalesList.Columns["tabPageCollectedColumnTable"].DisplayIndex = 4;
            dataGridViewCollectedSalesList.Columns["tabPageCollectedColumnIsLocked"].DisplayIndex = 13;
            dataGridViewCollectedSalesList.Columns["tabPageCollectedColumnIsTendered"].DisplayIndex = 14;
            dataGridViewCollectedSalesList.Columns["tabPageCollectedColumnIsCancelled"].DisplayIndex = 15;
            dataGridViewCollectedSalesList.Columns["tabPageCollectedColumnRemarks"].DisplayIndex = 16;
            dataGridViewCollectedSalesList.Columns["tabPageCollectedColumnDelivery"].DisplayIndex = 17;
            dataGridViewCollectedSalesList.Columns["tabPageCollectedColumnAmount"].DisplayIndex = 11;
            dataGridViewCollectedSalesList.Columns["tabPageCollectedColumnSpace"].DisplayIndex = 18;
        }

        public void CreateSalesListDataGrid()
        {
            ArrangeColumns();

            dataGridViewOpenSalesList.DataSource = dataOpenSalesListSource;
            dataGridViewBilledOutSalesList.DataSource = dataBilledOutSalesListSource;
            dataGridViewCollectedSalesList.DataSource = dataCollectedSalesListSource;
        }

        private void dataGridViewOpenSalesList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && dataGridViewOpenSalesList.CurrentCell.ColumnIndex == dataGridViewOpenSalesList.Columns["TabPageOpenColumnSalesNumber"].Index)
            {
                Controllers.TrnSalesController trnSalesController = new Controllers.TrnSalesController();
                Entities.TrnSalesEntity salesEntity = trnSalesController.DetailSales(Convert.ToInt32(dataGridViewOpenSalesList.Rows[dataGridViewOpenSalesList.CurrentCell.RowIndex].Cells[dataGridViewOpenSalesList.Columns["TabPageOpenColumnId"].Index].Value));
                trnPOSTouchActivityForm = new TrnPOSTouchActivityForm(sysSoftwareForm, this, salesEntity);
                trnPOSTouchActivityForm.ShowDialog();
            }
        }

        private void dataGridViewBilledOutSalesList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && dataGridViewBilledOutSalesList.CurrentCell.ColumnIndex == dataGridViewBilledOutSalesList.Columns["tabPageBilledOutColumnSalesNumber"].Index)
            {
                Controllers.TrnSalesController trnSalesController = new Controllers.TrnSalesController();
                Entities.TrnSalesEntity salesEntity = trnSalesController.DetailSales(Convert.ToInt32(dataGridViewBilledOutSalesList.Rows[dataGridViewBilledOutSalesList.CurrentCell.RowIndex].Cells[dataGridViewBilledOutSalesList.Columns["tabPageBilledOutColumnId"].Index].Value));

                trnPOSTouchActivityForm = new TrnPOSTouchActivityForm(sysSoftwareForm, this, salesEntity);
                trnPOSTouchActivityForm.ShowDialog();
            }
        }

        private void dataGridViewCollectedSalesList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && dataGridViewCollectedSalesList.CurrentCell.ColumnIndex == dataGridViewCollectedSalesList.Columns["tabPageCollectedColumnSalesNumber"].Index)
            {
                Controllers.TrnSalesController trnSalesController = new Controllers.TrnSalesController();
                Entities.TrnSalesEntity salesEntity = trnSalesController.DetailSales(Convert.ToInt32(dataGridViewCollectedSalesList.Rows[dataGridViewCollectedSalesList.CurrentCell.RowIndex].Cells[dataGridViewCollectedSalesList.Columns["tabPageCollectedColumnId"].Index].Value));

                trnPOSTouchActivityForm = new TrnPOSTouchActivityForm(sysSoftwareForm, this, salesEntity);
                trnPOSTouchActivityForm.ShowDialog();
            }
        }

        public void ClosePOSTouchActivity()
        {
            trnPOSTouchActivityForm.Close();
        }

        private void buttonDelivery_Click(object sender, EventArgs e)
        {
            TrnPOSDeliveryCustomerInformation trnPOSDeliveryCustomerInformation = new TrnPOSDeliveryCustomerInformation(this);
            trnPOSDeliveryCustomerInformation.ShowDialog();
        }

        private void buttonTableGroupPagePrevious_Click(object sender, EventArgs e)
        {
            tableGroupPage--;
            if (tableGroupPage == 0)
            {
                tableGroupPage = 1;
            }

            tablePage = 1;

            FillTableGroup();
        }

        private void buttonTableGroupPageNext_Click(object sender, EventArgs e)
        {
            tableGroupPage++;

            Int32 modulosPage = tableGroupPages % tableGroupNoOfButtons;
            Int32 maximumNoOfPages = (tableGroupPages - modulosPage) / tableGroupNoOfButtons;

            if (modulosPage > 0)
            {
                maximumNoOfPages += 1;
            }

            if (tableGroupPage > maximumNoOfPages)
            {
                tableGroupPage = maximumNoOfPages;
            }

            tablePage = 1;

            FillTableGroup();
        }

        private void buttonTableGroup1_Click(object sender, EventArgs e)
        {
            tablePage = 1;
            selectedTableGroup = 1;

            if (tableGroupToolTip.GetToolTip(buttonTableGroup1) != "")
            {
                Int32 tableGroupId = Convert.ToInt32(tableGroupToolTip.GetToolTip(buttonTableGroup1));
                selectedTableGroupId = tableGroupId;
                FillTable(tableGroupId);
            }
        }

        private void buttonTableGroup2_Click(object sender, EventArgs e)
        {
            tablePage = 1;
            selectedTableGroup = 2;

            if (tableGroupToolTip.GetToolTip(buttonTableGroup2) != "")
            {
                Int32 tableGroupId = Convert.ToInt32(tableGroupToolTip.GetToolTip(buttonTableGroup2));
                selectedTableGroupId = tableGroupId;
                FillTable(tableGroupId);
            }
        }

        private void buttonTableGroup3_Click(object sender, EventArgs e)
        {
            tablePage = 1;
            selectedTableGroup = 3;

            if (tableGroupToolTip.GetToolTip(buttonTableGroup3) != "")
            {
                Int32 tableGroupId = Convert.ToInt32(tableGroupToolTip.GetToolTip(buttonTableGroup3));
                selectedTableGroupId = tableGroupId;
                FillTable(tableGroupId);
            }
        }

        private void buttonTableGroup4_Click(object sender, EventArgs e)
        {
            tablePage = 1;
            selectedTableGroup = 4;

            if (tableGroupToolTip.GetToolTip(buttonTableGroup4) != "")
            {
                Int32 tableGroupId = Convert.ToInt32(tableGroupToolTip.GetToolTip(buttonTableGroup4));
                selectedTableGroupId = tableGroupId;
                FillTable(tableGroupId);
            }
        }

        private void buttonTableGroup5_Click(object sender, EventArgs e)
        {
            tablePage = 1;
            selectedTableGroup = 5;

            if (tableGroupToolTip.GetToolTip(buttonTableGroup5) != "")
            {
                Int32 tableGroupId = Convert.ToInt32(tableGroupToolTip.GetToolTip(buttonTableGroup5));
                selectedTableGroupId = tableGroupId;
                FillTable(tableGroupId);
            }
        }

        private void buttonTableGroup6_Click(object sender, EventArgs e)
        {
            tablePage = 1;
            selectedTableGroup = 6;

            if (tableGroupToolTip.GetToolTip(buttonTableGroup6) != "")
            {
                Int32 tableGroupId = Convert.ToInt32(tableGroupToolTip.GetToolTip(buttonTableGroup5));
                selectedTableGroupId = tableGroupId;
                FillTable(tableGroupId);
            }
        }

        private void buttonTablePrevious_Click(object sender, EventArgs e)
        {
            tablePage--;
            if (tablePage == 0)
            {
                tablePage = 1;
            }

            FillTable(selectedTableGroupId);
        }

        private void buttonTableNext_Click(object sender, EventArgs e)
        {
            tablePage++;

            Int32 modulosPage = tablePages % tableNoOfButtons;
            Int32 maximumNoOfPages = (tablePages - modulosPage) / tableNoOfButtons;

            if (modulosPage > 0)
            {
                maximumNoOfPages += 1;
            }

            if (tablePage > maximumNoOfPages)
            {
                tablePage = maximumNoOfPages;
            }

            FillTable(selectedTableGroupId);
        }

        private void textBoxFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateSalesListGridDataSource();
            }
        }

        private void buttonTenderAll_Click(object sender, EventArgs e)
        {
            DialogResult deleteDialogResult = MessageBox.Show("Are you sure you want to tender all sales?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (deleteDialogResult == DialogResult.Yes)
            {
                Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();

                if (tabControlSales.SelectedTab == tabPageOpen)
                {
                    List<Int32> salesIds = new List<Int32>();
                    foreach (DataGridViewRow row in dataGridViewOpenSalesList.Rows)
                    {
                        var salesId = Convert.ToInt32(row.Cells[dataGridViewOpenSalesList.Columns["TabPageOpenColumnId"].Index].Value);
                        salesIds.Add(salesId);
                    }

                    String[] tenderAllSales = trnPOSSalesController.TenderAllSales(salesIds);
                    if (tenderAllSales[1].Equals("0") == false)
                    {
                        MessageBox.Show("Tender successful.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UpdateSalesListGridDataSource();
                    }
                    else
                    {
                        MessageBox.Show(tenderAllSales[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (tabControlSales.SelectedTab == tabPageBilledOut)
                {
                    List<Int32> salesIds = new List<Int32>();
                    foreach (DataGridViewRow row in dataGridViewBilledOutSalesList.Rows)
                    {
                        var salesId = Convert.ToInt32(row.Cells[dataGridViewBilledOutSalesList.Columns["tabPageBilledOutColumnId"].Index].Value);
                        salesIds.Add(salesId);
                    }

                    String[] tenderAllSales = trnPOSSalesController.TenderAllSales(salesIds);
                    if (tenderAllSales[1].Equals("0") == false)
                    {
                        MessageBox.Show("Tender successful.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        UpdateSalesListGridDataSource();
                    }
                    else
                    {
                        MessageBox.Show(tenderAllSales[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonHideItems_Click(object sender, EventArgs e)
        {
            if (panelWalkIn.Visible == true)
            {
                panelWalkIn.Visible = false;
            }
            else
            {
                panelWalkIn.Visible = true;
            }
        }
    }
}
