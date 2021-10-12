using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.SysSystemTables
{
    public partial class SysSystemTablesForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public static Int32 pageSize = 50;

        public static List<Entities.DgvMstSystemTablesAccountListEntity> accountListData = new List<Entities.DgvMstSystemTablesAccountListEntity>();
        public PagedList<Entities.DgvMstSystemTablesAccountListEntity> accountListPageList = new PagedList<Entities.DgvMstSystemTablesAccountListEntity>(accountListData, accountListPageNumber, pageSize);
        public BindingSource accountListDataSource = new BindingSource();
        public static Int32 accountListPageNumber = 1;

        public static List<Entities.DgvMstSystemTablesPayTypeListEntity> payTypeListData = new List<Entities.DgvMstSystemTablesPayTypeListEntity>();
        public PagedList<Entities.DgvMstSystemTablesPayTypeListEntity> payTypeListPageList = new PagedList<Entities.DgvMstSystemTablesPayTypeListEntity>(payTypeListData, accountListPageNumber, pageSize);
        public BindingSource payTypeListDataSource = new BindingSource();
        public static Int32 payTypeListPageNumber = 1;

        public static List<Entities.DgvMstSystemTablesTaxListEntity> taxListData = new List<Entities.DgvMstSystemTablesTaxListEntity>();
        public PagedList<Entities.DgvMstSystemTablesTaxListEntity> taxListPageList = new PagedList<Entities.DgvMstSystemTablesTaxListEntity>(taxListData, accountListPageNumber, pageSize);
        public BindingSource taxListDataSource = new BindingSource();
        public static Int32 taxListPageNumber = 1;

        public static List<Entities.DgvMstSystemTablesUnitListEntity> unitListData = new List<Entities.DgvMstSystemTablesUnitListEntity>();
        public PagedList<Entities.DgvMstSystemTablesUnitListEntity> unitListPageList = new PagedList<Entities.DgvMstSystemTablesUnitListEntity>(unitListData, unitListPageNumber, pageSize);
        public BindingSource unitListDataSource = new BindingSource();
        public static Int32 unitListPageNumber = 1;

        public static List<Entities.DgvMstSystemTablesPeriodListEntity> periodListData = new List<Entities.DgvMstSystemTablesPeriodListEntity>();
        public PagedList<Entities.DgvMstSystemTablesPeriodListEntity> periodListPageList = new PagedList<Entities.DgvMstSystemTablesPeriodListEntity>(periodListData, accountListPageNumber, pageSize);
        public BindingSource periodListDataSource = new BindingSource();
        public static Int32 periodListPageNumber = 1;

        public static List<Entities.DgvMstSystemTablesTerminalListEntity> terminalListData = new List<Entities.DgvMstSystemTablesTerminalListEntity>();
        public PagedList<Entities.DgvMstSystemTablesTerminalListEntity> terminalListPageList = new PagedList<Entities.DgvMstSystemTablesTerminalListEntity>(terminalListData, accountListPageNumber, pageSize);
        public BindingSource terminalListDataSource = new BindingSource();
        public static Int32 terminalListPageNumber = 1;

        public static List<Entities.DgvMstSystemTablesSupplierListEntity> supplierListData = new List<Entities.DgvMstSystemTablesSupplierListEntity>();
        public PagedList<Entities.DgvMstSystemTablesSupplierListEntity> supplierListPageList = new PagedList<Entities.DgvMstSystemTablesSupplierListEntity>(supplierListData, accountListPageNumber, pageSize);
        public BindingSource supplierListDataSource = new BindingSource();
        public static Int32 supplierListPageNumber = 1;

        public static List<Entities.DgvMstSystemTablesLanguageListEntity> languageListData = new List<Entities.DgvMstSystemTablesLanguageListEntity>();
        public PagedList<Entities.DgvMstSystemTablesLanguageListEntity> languageListPageList = new PagedList<Entities.DgvMstSystemTablesLanguageListEntity>(languageListData, languageListPageNumber, pageSize);
        public BindingSource languageListDataSource = new BindingSource();
        public static Int32 languageListPageNumber = 1;

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();


        public SysSystemTablesForm(SysSoftwareForm softwareForm)
        {
            InitializeComponent();

            Controllers.SysLanguageController sysLabel = new Controllers.SysLanguageController();
            if (sysLabel.ListLanguage("").Any())
            {
                sysLanguageEntities = sysLabel.ListLanguage("");
                var language = Modules.SysCurrentModule.GetCurrentSettings().Language;
                if (language != "English")
                {
                    buttonClose.Text = SetLabel(buttonClose.Text);
                    buttonAdd.Text = SetLabel(buttonAdd.Text);
                    label1.Text = SetLabel(label1.Text);
                    tabPage1.Text = SetLabel(tabPage1.Text);
                    tabPage2.Text = SetLabel(tabPage2.Text);
                    tabPage3.Text = SetLabel(tabPage3.Text);
                    tabPage4.Text = SetLabel(tabPage4.Text);
                    tabPage5.Text = SetLabel(tabPage5.Text);
                    tabPage6.Text = SetLabel(tabPage6.Text);
                    tabPage7.Text = SetLabel(tabPage7.Text);
                    tabPage8.Text = SetLabel(tabPage8.Text);
                    dataGridViewAccountList.Columns[3].HeaderText = SetLabel(dataGridViewAccountList.Columns[3].HeaderText);
                    dataGridViewAccountList.Columns[4].HeaderText = SetLabel(dataGridViewAccountList.Columns[4].HeaderText);
                    dataGridViewAccountList.Columns[5].HeaderText = SetLabel(dataGridViewAccountList.Columns[5].HeaderText);
                    buttonAccountListPageListFirst.Text = SetLabel(buttonAccountListPageListFirst.Text);
                    buttonAccountListPageListLast.Text = SetLabel(buttonAccountListPageListLast.Text);
                    buttonAccountListPageListNext.Text = SetLabel(buttonAccountListPageListNext.Text);
                    buttonAccountListPageListPrevious.Text = SetLabel(buttonAccountListPageListPrevious.Text);
                    dataGridViewPayTypeList.Columns[3].HeaderText = SetLabel(dataGridViewPayTypeList.Columns[3].HeaderText);
                    dataGridViewPayTypeList.Columns[4].HeaderText = SetLabel(dataGridViewPayTypeList.Columns[4].HeaderText);
                    dataGridViewPayTypeList.Columns[6].HeaderText = SetLabel(dataGridViewPayTypeList.Columns[6].HeaderText);
                    buttonPayTypeListPageListFirst.Text = SetLabel(buttonPayTypeListPageListFirst.Text);
                    buttonPayTypeListPageListLast.Text = SetLabel(buttonPayTypeListPageListLast.Text);
                    buttonPayTypeListPageListNext.Text = SetLabel(buttonPayTypeListPageListNext.Text);
                    buttonPayTypeListPageListPrevious.Text = SetLabel(buttonPayTypeListPageListPrevious.Text);
                    dataGridViewTaxList.Columns[3].HeaderText = SetLabel(dataGridViewTaxList.Columns[3].HeaderText);
                    dataGridViewTaxList.Columns[4].HeaderText = SetLabel(dataGridViewTaxList.Columns[4].HeaderText);
                    dataGridViewTaxList.Columns[5].HeaderText = SetLabel(dataGridViewTaxList.Columns[5].HeaderText);
                    dataGridViewTaxList.Columns[7].HeaderText = SetLabel(dataGridViewTaxList.Columns[7].HeaderText);
                    buttonTaxListPageListFirst.Text = SetLabel(buttonTaxListPageListFirst.Text);
                    buttonTaxListPageListLast.Text = SetLabel(buttonTaxListPageListLast.Text);
                    buttonTaxListPageListNext.Text = SetLabel(buttonTaxListPageListNext.Text);
                    buttonTaxListPageListPrevious.Text = SetLabel(buttonTaxListPageListPrevious.Text);
                    dataGridViewUnitList.Columns[3].HeaderText = SetLabel(dataGridViewUnitList.Columns[3].HeaderText);
                    buttonUnitListPageListFirst.Text = SetLabel(buttonUnitListPageListFirst.Text);
                    buttonUnitListPageListLast.Text = SetLabel(buttonUnitListPageListLast.Text);
                    buttonUnitListPageListNext.Text = SetLabel(buttonUnitListPageListNext.Text);
                    buttonUnitListPageListPrevious.Text = SetLabel(buttonUnitListPageListPrevious.Text);
                    dataGridViewPeriodList.Columns[3].HeaderText = SetLabel(dataGridViewPeriodList.Columns[3].HeaderText);
                    buttonPeriodListPageListFirst.Text = SetLabel(buttonPeriodListPageListFirst.Text);
                    buttonPeriodListPageListLast.Text = SetLabel(buttonPeriodListPageListLast.Text);
                    buttonPeriodListPageListNext.Text = SetLabel(buttonPeriodListPageListNext.Text);
                    buttonPeriodListPageListPrevious.Text = SetLabel(buttonPeriodListPageListPrevious.Text);
                    dataGridViewTerminalList.Columns[3].HeaderText = SetLabel(dataGridViewTerminalList.Columns[3].HeaderText);
                    buttonTerminalListPageListFirst.Text = SetLabel(buttonTerminalListPageListFirst.Text);
                    buttonTerminalListPageListLast.Text = SetLabel(buttonTerminalListPageListLast.Text);
                    buttonTerminalListPageListNext.Text = SetLabel(buttonTerminalListPageListNext.Text);
                    buttonTerminalListPageListPrevious.Text = SetLabel(buttonTerminalListPageListPrevious.Text);
                    dataGridViewSupplierList.Columns[3].HeaderText = SetLabel(dataGridViewSupplierList.Columns[3].HeaderText);
                    dataGridViewSupplierList.Columns[4].HeaderText = SetLabel(dataGridViewSupplierList.Columns[4].HeaderText);
                    dataGridViewSupplierList.Columns[5].HeaderText = SetLabel(dataGridViewSupplierList.Columns[5].HeaderText);
                    dataGridViewSupplierList.Columns[6].HeaderText = SetLabel(dataGridViewSupplierList.Columns[6].HeaderText);
                    dataGridViewSupplierList.Columns[9].HeaderText = SetLabel(dataGridViewSupplierList.Columns[9].HeaderText);
                    dataGridViewSupplierList.Columns[11].HeaderText = SetLabel(dataGridViewSupplierList.Columns[11].HeaderText);
                    buttonSupplierListPageListFirst.Text = SetLabel(buttonSupplierListPageListFirst.Text);
                    buttonSupplierListPageListLast.Text = SetLabel(buttonSupplierListPageListLast.Text);
                    buttonSupplierListPageListNext.Text = SetLabel(buttonSupplierListPageListNext.Text);
                    buttonSupplierListPageListPrevious.Text = SetLabel(buttonSupplierListPageListPrevious.Text);
                    dataGridViewLanguageList.Columns[3].HeaderText = SetLabel(dataGridViewLanguageList.Columns[3].HeaderText);
                    dataGridViewLanguageList.Columns[4].HeaderText = SetLabel(dataGridViewLanguageList.Columns[4].HeaderText);
                    buttonLanguageListPageListFirst.Text = SetLabel(buttonLanguageListPageListFirst.Text);
                    buttonLanguageListPageListLast.Text = SetLabel(buttonLanguageListPageListLast.Text);
                    buttonLanguageListPageListNext.Text = SetLabel(buttonLanguageListPageListNext.Text);
                    buttonLanguageListPageListPrevious.Text = SetLabel(buttonLanguageListPageListPrevious.Text);
                }
            }

            sysSoftwareForm = softwareForm;

            sysUserRights = new Modules.SysUserRightsModule("SysTables");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (sysUserRights.GetUserRights().CanAdd == false)
                {
                    buttonAdd.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanEdit == false)
                {
                    dataGridViewAccountList.Columns[0].Visible = false;
                    dataGridViewPayTypeList.Columns[0].Visible = false;
                    dataGridViewTaxList.Columns[0].Visible = false;
                    dataGridViewUnitList.Columns[0].Visible = false;
                    dataGridViewPeriodList.Columns[0].Visible = false;
                    dataGridViewTerminalList.Columns[0].Visible = false;
                    dataGridViewSupplierList.Columns[0].Visible = false;
                    dataGridViewLanguageList.Columns[0].Visible = false;
                }

                if (sysUserRights.GetUserRights().CanDelete == false)
                {
                    dataGridViewAccountList.Columns[1].Visible = false;
                    dataGridViewPayTypeList.Columns[1].Visible = false;
                    dataGridViewTaxList.Columns[1].Visible = false;
                    dataGridViewUnitList.Columns[1].Visible = false;
                    dataGridViewPeriodList.Columns[1].Visible = false;
                    dataGridViewTerminalList.Columns[1].Visible = false;
                    dataGridViewSupplierList.Columns[1].Visible = false;
                    dataGridViewLanguageList.Columns[1].Visible = false;
                }

                CreateAccountListDataGridView();
                CreatePayTypeListDataGridView();
                CreateTaxListDataGridView();
                CreateUnitListDataGridView();
                CreatePeriodListDataGridView();
                CreateTerminalListDataGridView();
                CreateSupplierListDataGridView();
                CreateLanguageListDataGridView();
            }
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

        public string SetLabelEditDelete(string label)
        {
            if (sysLanguageEntities.Any())
            {
                Controllers.SysLanguageController sysLabel = new Controllers.SysLanguageController();
                var language = Modules.SysCurrentModule.GetCurrentSettings().Language;
                sysLanguageEntities = sysLabel.ListLanguage("");
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


        // =======
        // Account
        // =======
        public void UpdateAccountListDataSource()
        {
            SetAccountListDataSourceAsync();
        }

        public async void SetAccountListDataSourceAsync()
        {
            List<Entities.DgvMstSystemTablesAccountListEntity> getAccountListData = await GetAccountListDataTask();
            if (getAccountListData.Any())
            {
                accountListData = getAccountListData;
                accountListPageList = new PagedList<Entities.DgvMstSystemTablesAccountListEntity>(accountListData, accountListPageNumber, pageSize);

                if (accountListPageList.PageCount == 1)
                {
                    buttonAccountListPageListFirst.Enabled = false;
                    buttonAccountListPageListPrevious.Enabled = false;
                    buttonAccountListPageListNext.Enabled = false;
                    buttonAccountListPageListLast.Enabled = false;
                }
                else if (accountListPageNumber == 1)
                {
                    buttonAccountListPageListFirst.Enabled = false;
                    buttonAccountListPageListPrevious.Enabled = false;
                    buttonAccountListPageListNext.Enabled = true;
                    buttonAccountListPageListLast.Enabled = true;
                }
                else if (accountListPageNumber == accountListPageList.PageCount)
                {
                    buttonAccountListPageListFirst.Enabled = true;
                    buttonAccountListPageListPrevious.Enabled = true;
                    buttonAccountListPageListNext.Enabled = false;
                    buttonAccountListPageListLast.Enabled = false;
                }
                else
                {
                    buttonAccountListPageListFirst.Enabled = true;
                    buttonAccountListPageListPrevious.Enabled = true;
                    buttonAccountListPageListNext.Enabled = true;
                    buttonAccountListPageListLast.Enabled = true;
                }

                textBoxAccountListPageNumber.Text = accountListPageNumber + " / " + accountListPageList.PageCount;
                accountListDataSource.DataSource = accountListPageList;
            }
            else
            {
                buttonAccountListPageListFirst.Enabled = false;
                buttonAccountListPageListPrevious.Enabled = false;
                buttonAccountListPageListNext.Enabled = false;
                buttonAccountListPageListLast.Enabled = false;

                accountListPageNumber = 1;

                accountListData = new List<Entities.DgvMstSystemTablesAccountListEntity>();
                accountListDataSource.Clear();
                textBoxAccountListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvMstSystemTablesAccountListEntity>> GetAccountListDataTask()
        {
            string gridEdit = "Edit";
            string gridDelete = "Delete";
            var language = Modules.SysCurrentModule.GetCurrentSettings().Language;
            if (language != "English")
            {
                gridEdit = "编辑";
                gridDelete = "删除";
            }
            else
            {
                gridEdit = "Edit";
                gridDelete = "Delete";
            }

            String filter = textBoxAccountListFilter.Text;
            Controllers.MstAccountController mstAccountController = new Controllers.MstAccountController();

            List<Entities.MstAccountEntity> listAccount = mstAccountController.ListAccount(filter);
            if (listAccount.Any())
            {
                var accounts = from d in listAccount
                               select new Entities.DgvMstSystemTablesAccountListEntity
                               {
                                   ColumnAccountListButtonEdit = gridEdit,
                                   ColumnAccountListButtonDelete = gridDelete,
                                   ColumnAccountListId = d.Id,
                                   ColumnAccountListCode = d.Code,
                                   ColumnAccountListAccount = d.Account,
                                   ColumnAccountListAccountType = d.AccountType
                               };

                return Task.FromResult(accounts.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvMstSystemTablesAccountListEntity>());
            }
        }

        public void CreateAccountListDataGridView()
        {
            UpdateAccountListDataSource();

            dataGridViewAccountList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewAccountList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewAccountList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewAccountList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewAccountList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewAccountList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewAccountList.DataSource = accountListDataSource;
        }

        public void GetAccountListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void buttonAccountListPageListFirst_Click(object sender, EventArgs e)
        {
            accountListPageList = new PagedList<Entities.DgvMstSystemTablesAccountListEntity>(accountListData, 1, pageSize);
            accountListDataSource.DataSource = accountListPageList;

            buttonAccountListPageListFirst.Enabled = false;
            buttonAccountListPageListPrevious.Enabled = false;
            buttonAccountListPageListNext.Enabled = true;
            buttonAccountListPageListLast.Enabled = true;

            accountListPageNumber = 1;
            textBoxAccountListPageNumber.Text = accountListPageNumber + " / " + accountListPageList.PageCount;
        }

        private void buttonAccountListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (accountListPageList.HasPreviousPage == true)
            {
                accountListPageList = new PagedList<Entities.DgvMstSystemTablesAccountListEntity>(accountListData, --accountListPageNumber, pageSize);
                accountListDataSource.DataSource = accountListPageList;
            }

            buttonAccountListPageListNext.Enabled = true;
            buttonAccountListPageListLast.Enabled = true;

            if (accountListPageNumber == 1)
            {
                buttonAccountListPageListFirst.Enabled = false;
                buttonAccountListPageListPrevious.Enabled = false;
            }

            textBoxAccountListPageNumber.Text = accountListPageNumber + " / " + accountListPageList.PageCount;
        }

        private void buttonAccountListPageListNext_Click(object sender, EventArgs e)
        {
            if (accountListPageList.HasNextPage == true)
            {
                accountListPageList = new PagedList<Entities.DgvMstSystemTablesAccountListEntity>(accountListData, ++accountListPageNumber, pageSize);
                accountListDataSource.DataSource = accountListPageList;
            }

            buttonAccountListPageListFirst.Enabled = true;
            buttonAccountListPageListPrevious.Enabled = true;

            if (accountListPageNumber == accountListPageList.PageCount)
            {
                buttonAccountListPageListNext.Enabled = false;
                buttonAccountListPageListLast.Enabled = false;
            }

            textBoxAccountListPageNumber.Text = accountListPageNumber + " / " + accountListPageList.PageCount;
        }

        private void buttonAccountListPageListLast_Click(object sender, EventArgs e)
        {
            accountListPageList = new PagedList<Entities.DgvMstSystemTablesAccountListEntity>(accountListData, accountListPageList.PageCount, pageSize);
            accountListDataSource.DataSource = accountListPageList;

            buttonAccountListPageListFirst.Enabled = true;
            buttonAccountListPageListPrevious.Enabled = true;
            buttonAccountListPageListNext.Enabled = false;
            buttonAccountListPageListLast.Enabled = false;

            accountListPageNumber = accountListPageList.PageCount;
            textBoxAccountListPageNumber.Text = accountListPageNumber + " / " + accountListPageList.PageCount;
        }

        private void dataGridViewAccountList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetAccountListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewAccountList.CurrentCell.ColumnIndex == dataGridViewAccountList.Columns["ColumnAccountListButtonEdit"].Index)
            {
                Entities.MstAccountEntity account = new Entities.MstAccountEntity
                {
                    Id = Convert.ToInt32(dataGridViewAccountList.Rows[e.RowIndex].Cells[2].Value),
                    Code = dataGridViewAccountList.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    Account = dataGridViewAccountList.Rows[e.RowIndex].Cells[4].Value.ToString(),
                    AccountType = dataGridViewAccountList.Rows[e.RowIndex].Cells[5].Value.ToString()
                };

                SysAccountDetailForm sysSystemTablesAccountDetailForm = new SysAccountDetailForm(this, account);
                sysSystemTablesAccountDetailForm.ShowDialog();
            }

            if (e.RowIndex > -1 && dataGridViewAccountList.CurrentCell.ColumnIndex == dataGridViewAccountList.Columns["ColumnAccountListButtonDelete"].Index)
            {
                DialogResult deleteDialogResult = MessageBox.Show("Delete Account?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (deleteDialogResult == DialogResult.Yes)
                {
                    Controllers.MstAccountController mstAccountController = new Controllers.MstAccountController();

                    String[] deleteAccount = mstAccountController.DeleteAccount(Convert.ToInt32(dataGridViewAccountList.Rows[e.RowIndex].Cells[2].Value));
                    if (deleteAccount[1].Equals("0") == false)
                    {
                        Int32 currentPageNumber = accountListPageNumber;

                        accountListPageNumber = 1;
                        UpdateAccountListDataSource();
                    }
                    else
                    {
                        MessageBox.Show(deleteAccount[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void textBoxAccountListFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateAccountListDataSource();
            }
        }

        // ========
        // Pay Type
        // ========
        public void UpdatePayTypeListDataSource()
        {
            SetPayTypeListDataSourceAsync();
        }

        public async void SetPayTypeListDataSourceAsync()
        {
            List<Entities.DgvMstSystemTablesPayTypeListEntity> getPayTypeListData = await GetPayTypeListDataTask();
            if (getPayTypeListData.Any())
            {
                payTypeListData = getPayTypeListData;
                payTypeListPageList = new PagedList<Entities.DgvMstSystemTablesPayTypeListEntity>(payTypeListData, payTypeListPageNumber, pageSize);

                if (payTypeListPageList.PageCount == 1)
                {
                    buttonPayTypeListPageListFirst.Enabled = false;
                    buttonPayTypeListPageListPrevious.Enabled = false;
                    buttonPayTypeListPageListNext.Enabled = false;
                    buttonPayTypeListPageListLast.Enabled = false;
                }
                else if (payTypeListPageNumber == 1)
                {
                    buttonPayTypeListPageListFirst.Enabled = false;
                    buttonPayTypeListPageListPrevious.Enabled = false;
                    buttonPayTypeListPageListNext.Enabled = true;
                    buttonPayTypeListPageListLast.Enabled = true;
                }
                else if (payTypeListPageNumber == payTypeListPageList.PageCount)
                {
                    buttonPayTypeListPageListFirst.Enabled = true;
                    buttonPayTypeListPageListPrevious.Enabled = true;
                    buttonPayTypeListPageListNext.Enabled = false;
                    buttonPayTypeListPageListLast.Enabled = false;
                }
                else
                {
                    buttonPayTypeListPageListFirst.Enabled = true;
                    buttonPayTypeListPageListPrevious.Enabled = true;
                    buttonPayTypeListPageListNext.Enabled = true;
                    buttonPayTypeListPageListLast.Enabled = true;
                }

                textBoxPayTypeListPageNumber.Text = payTypeListPageNumber + " / " + payTypeListPageList.PageCount;
                payTypeListDataSource.DataSource = payTypeListPageList;
            }
            else
            {
                buttonPayTypeListPageListFirst.Enabled = false;
                buttonPayTypeListPageListPrevious.Enabled = false;
                buttonPayTypeListPageListNext.Enabled = false;
                buttonPayTypeListPageListLast.Enabled = false;

                payTypeListPageNumber = 1;

                payTypeListData = new List<Entities.DgvMstSystemTablesPayTypeListEntity>();
                payTypeListDataSource.Clear();
                textBoxPayTypeListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvMstSystemTablesPayTypeListEntity>> GetPayTypeListDataTask()
        {
            string gridEdit = "Edit";
            string gridDelete = "Delete";
            var language = Modules.SysCurrentModule.GetCurrentSettings().Language;
            if (language != "English")
            {
                gridEdit = "编辑";
                gridDelete = "删除";
            }
            else
            {
                gridEdit = "Edit";
                gridDelete = "Delete";
            }

            String filter = textBoxPayTypeListFilter.Text;
            Controllers.MstPayTypeController mstPayTypeController = new Controllers.MstPayTypeController();

            List<Entities.MstPayTypeEntity> listPayType = mstPayTypeController.ListPayType(filter);
            if (listPayType.Any())
            {
                var payTypes = from d in listPayType
                               select new Entities.DgvMstSystemTablesPayTypeListEntity
                               {
                                   ColumnPayTypeListButtonEdit = gridEdit,
                                   ColumnPayTypeListButtonDelete = gridDelete,
                                   ColumnPayTypeListId = d.Id,
                                   ColumnPayTypeListPayTypeCode = d.PayTypeCode,
                                   ColumnPayTypeListPayType = d.PayType,
                                   ColumnAccountId = Convert.ToInt32(d.AccountId),
                                   ColumnPayTypeListAccount = d.Account
                               };

                return Task.FromResult(payTypes.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvMstSystemTablesPayTypeListEntity>());
            }
        }

        public void CreatePayTypeListDataGridView()
        {
            UpdatePayTypeListDataSource();

            dataGridViewPayTypeList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewPayTypeList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewPayTypeList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewPayTypeList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewPayTypeList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewPayTypeList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewPayTypeList.DataSource = payTypeListDataSource;
        }

        private void textBoxPayTypeListFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdatePayTypeListDataSource();
            }
        }

        private void dataGridViewPayTypeList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetPayTypeListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewPayTypeList.CurrentCell.ColumnIndex == dataGridViewPayTypeList.Columns["ColumnPayTypeListButtonEdit"].Index)
            {
                Entities.MstPayTypeEntity selectePaytype = new Entities.MstPayTypeEntity()
                {
                    Id = Convert.ToInt32(dataGridViewPayTypeList.Rows[e.RowIndex].Cells[2].Value),
                    PayTypeCode = dataGridViewPayTypeList.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    PayType = dataGridViewPayTypeList.Rows[e.RowIndex].Cells[4].Value.ToString(),
                    AccountId = Convert.ToInt32(dataGridViewPayTypeList.Rows[e.RowIndex].Cells[5].Value)
                };
                SysPayTypeDetailForm sysSystemTablesPayTypeDetailForm = new SysPayTypeDetailForm(this, selectePaytype);
                sysSystemTablesPayTypeDetailForm.ShowDialog();
            }

            if (e.RowIndex > -1 && dataGridViewPayTypeList.CurrentCell.ColumnIndex == dataGridViewPayTypeList.Columns["ColumnPayTypeListButtonDelete"].Index)
            {

                DialogResult deleteDialogResult = MessageBox.Show("Delete PayType?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (deleteDialogResult == DialogResult.Yes)
                {
                    Controllers.MstPayTypeController mstPayTypeController = new Controllers.MstPayTypeController();

                    String[] deletePayType = mstPayTypeController.DeletePayType(Convert.ToInt32(dataGridViewPayTypeList.Rows[e.RowIndex].Cells[2].Value));
                    if (deletePayType[1].Equals("0") == false)
                    {
                        Int32 currentPageNumber = payTypeListPageNumber;

                        payTypeListPageNumber = 1;
                        UpdatePayTypeListDataSource();
                    }
                    else
                    {
                        MessageBox.Show(deletePayType[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void GetPayTypeListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void buttonPayTypeListPageListFirst_Click(object sender, EventArgs e)
        {
            payTypeListPageList = new PagedList<Entities.DgvMstSystemTablesPayTypeListEntity>(payTypeListData, 1, pageSize);
            payTypeListDataSource.DataSource = payTypeListPageList;

            buttonPayTypeListPageListFirst.Enabled = false;
            buttonPayTypeListPageListPrevious.Enabled = false;
            buttonPayTypeListPageListNext.Enabled = true;
            buttonPayTypeListPageListLast.Enabled = true;

            payTypeListPageNumber = 1;
            textBoxPayTypeListPageNumber.Text = payTypeListPageNumber + " / " + payTypeListPageList.PageCount;
        }

        private void buttonPayTypeListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (payTypeListPageList.HasPreviousPage == true)
            {
                payTypeListPageList = new PagedList<Entities.DgvMstSystemTablesPayTypeListEntity>(payTypeListData, --payTypeListPageNumber, pageSize);
                payTypeListDataSource.DataSource = payTypeListPageList;
            }

            buttonPayTypeListPageListNext.Enabled = true;
            buttonPayTypeListPageListLast.Enabled = true;

            if (payTypeListPageNumber == 1)
            {
                buttonPayTypeListPageListFirst.Enabled = false;
                buttonPayTypeListPageListPrevious.Enabled = false;
            }

            textBoxPayTypeListPageNumber.Text = payTypeListPageNumber + " / " + payTypeListPageList.PageCount;
        }

        private void buttonPayTypeListPageListNext_Click(object sender, EventArgs e)
        {
            if (payTypeListPageList.HasNextPage == true)
            {
                payTypeListPageList = new PagedList<Entities.DgvMstSystemTablesPayTypeListEntity>(payTypeListData, ++payTypeListPageNumber, pageSize);
                payTypeListDataSource.DataSource = payTypeListPageList;
            }

            buttonPayTypeListPageListFirst.Enabled = true;
            buttonPayTypeListPageListPrevious.Enabled = true;

            if (payTypeListPageNumber == payTypeListPageList.PageCount)
            {
                buttonPayTypeListPageListNext.Enabled = false;
                buttonPayTypeListPageListLast.Enabled = false;
            }

            textBoxPayTypeListPageNumber.Text = payTypeListPageNumber + " / " + payTypeListPageList.PageCount;
        }

        private void buttonPayTypeListPageListLast_Click(object sender, EventArgs e)
        {
            payTypeListPageList = new PagedList<Entities.DgvMstSystemTablesPayTypeListEntity>(payTypeListData, payTypeListPageList.PageCount, pageSize);
            payTypeListDataSource.DataSource = payTypeListPageList;

            buttonPayTypeListPageListFirst.Enabled = true;
            buttonPayTypeListPageListPrevious.Enabled = true;
            buttonPayTypeListPageListNext.Enabled = false;
            buttonPayTypeListPageListLast.Enabled = false;

            payTypeListPageNumber = payTypeListPageList.PageCount;
            textBoxPayTypeListPageNumber.Text = payTypeListPageNumber + " / " + payTypeListPageList.PageCount;
        }

        // ===
        // Tax
        // ===
        public void UpdateTaxListDataSource()
        {
            SetTaxListDataSourceAsync();
        }

        public async void SetTaxListDataSourceAsync()
        {
            List<Entities.DgvMstSystemTablesTaxListEntity> getTaxListData = await GetTaxListDataTask();
            if (getTaxListData.Any())
            {
                taxListData = getTaxListData;
                taxListPageList = new PagedList<Entities.DgvMstSystemTablesTaxListEntity>(taxListData, taxListPageNumber, pageSize);

                if (taxListPageList.PageCount == 1)
                {
                    buttonTaxListPageListFirst.Enabled = false;
                    buttonTaxListPageListPrevious.Enabled = false;
                    buttonTaxListPageListNext.Enabled = false;
                    buttonTaxListPageListLast.Enabled = false;
                }
                else if (taxListPageNumber == 1)
                {
                    buttonTaxListPageListFirst.Enabled = false;
                    buttonTaxListPageListPrevious.Enabled = false;
                    buttonTaxListPageListNext.Enabled = true;
                    buttonTaxListPageListLast.Enabled = true;
                }
                else if (taxListPageNumber == taxListPageList.PageCount)
                {
                    buttonTaxListPageListFirst.Enabled = true;
                    buttonTaxListPageListPrevious.Enabled = true;
                    buttonTaxListPageListNext.Enabled = false;
                    buttonTaxListPageListLast.Enabled = false;
                }
                else
                {
                    buttonTaxListPageListFirst.Enabled = true;
                    buttonTaxListPageListPrevious.Enabled = true;
                    buttonTaxListPageListNext.Enabled = true;
                    buttonTaxListPageListLast.Enabled = true;
                }

                textBoxTaxListPageNumber.Text = taxListPageNumber + " / " + taxListPageList.PageCount;
                taxListDataSource.DataSource = taxListPageList;
            }
            else
            {
                buttonTaxListPageListFirst.Enabled = false;
                buttonTaxListPageListPrevious.Enabled = false;
                buttonTaxListPageListNext.Enabled = false;
                buttonTaxListPageListLast.Enabled = false;

                taxListPageNumber = 1;

                taxListData = new List<Entities.DgvMstSystemTablesTaxListEntity>();
                taxListDataSource.Clear();
                textBoxTaxListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvMstSystemTablesTaxListEntity>> GetTaxListDataTask()
        {
            String filter = textBoxTaxListFilter.Text;
            Controllers.MstTaxController mstTaxController = new Controllers.MstTaxController();

            List<Entities.MstTaxEntity> listTax = mstTaxController.ListTax(filter);
            if (listTax.Any())
            {
                var taxs = from d in listTax
                           select new Entities.DgvMstSystemTablesTaxListEntity
                           {
                               ColumnTaxListButtonEdit = SetLabelEditDelete("Edit"),
                               ColumnTaxListButtonDelete = SetLabelEditDelete("Delete"),
                               ColumnTaxListId = d.Id,
                               ColumnTaxListCode = d.Code,
                               ColumnTaxListTax = d.Tax,
                               ColumnTaxListRate = d.Rate.ToString("#,##0.00"),
                               ColumnTaxListAccountId = d.AccountId,
                               ColumnTaxListAccount = d.Account
                           };

                return Task.FromResult(taxs.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvMstSystemTablesTaxListEntity>());
            }
        }

        public void CreateTaxListDataGridView()
        {
            UpdateTaxListDataSource();

            dataGridViewTaxList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewTaxList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewTaxList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewTaxList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewTaxList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewTaxList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewTaxList.DataSource = taxListDataSource;
        }

        private void textBoxTaxListFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateTaxListDataSource();
            }
        }

        private void dataGridViewTaxList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetTaxListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewTaxList.CurrentCell.ColumnIndex == dataGridViewTaxList.Columns["ColumnTaxListButtonEdit"].Index)
            {
                Entities.MstTaxEntity selectedTax = new Entities.MstTaxEntity()
                {
                    Id = Convert.ToInt32(dataGridViewTaxList.Rows[e.RowIndex].Cells[2].Value),
                    Code = dataGridViewTaxList.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    Tax = dataGridViewTaxList.Rows[e.RowIndex].Cells[4].Value.ToString(),
                    Rate = Convert.ToDecimal(dataGridViewTaxList.Rows[e.RowIndex].Cells[5].Value),
                    AccountId = Convert.ToInt32(dataGridViewTaxList.Rows[e.RowIndex].Cells[6].Value)
                };

                SysTaxDetailForm sysSystemTablesTaxDetailForm = new SysTaxDetailForm(this, selectedTax);
                sysSystemTablesTaxDetailForm.ShowDialog();
            }

            if (e.RowIndex > -1 && dataGridViewTaxList.CurrentCell.ColumnIndex == dataGridViewTaxList.Columns["ColumnTaxListButtonDelete"].Index)
            {
                DialogResult deleteDialogResult = MessageBox.Show("Delete Tax?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (deleteDialogResult == DialogResult.Yes)
                {
                    Controllers.MstTaxController mstTaxController = new Controllers.MstTaxController();

                    String[] deleteTax = mstTaxController.DeleteTax(Convert.ToInt32(dataGridViewTaxList.Rows[e.RowIndex].Cells[2].Value));
                    if (deleteTax[1].Equals("0") == false)
                    {
                        Int32 currentPageNumber = taxListPageNumber;

                        taxListPageNumber = 1;
                        UpdateTaxListDataSource();
                    }
                    else
                    {
                        MessageBox.Show(deleteTax[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void GetTaxListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void buttonTaxListPageListFirst_Click(object sender, EventArgs e)
        {
            taxListPageList = new PagedList<Entities.DgvMstSystemTablesTaxListEntity>(taxListData, 1, pageSize);
            taxListDataSource.DataSource = taxListPageList;

            buttonTaxListPageListFirst.Enabled = false;
            buttonTaxListPageListPrevious.Enabled = false;
            buttonTaxListPageListNext.Enabled = true;
            buttonTaxListPageListLast.Enabled = true;

            taxListPageNumber = 1;
            textBoxTaxListPageNumber.Text = taxListPageNumber + " / " + taxListPageList.PageCount;
        }

        private void buttonTaxListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (taxListPageList.HasPreviousPage == true)
            {
                taxListPageList = new PagedList<Entities.DgvMstSystemTablesTaxListEntity>(taxListData, --taxListPageNumber, pageSize);
                taxListDataSource.DataSource = taxListPageList;
            }

            buttonTaxListPageListNext.Enabled = true;
            buttonTaxListPageListLast.Enabled = true;

            if (taxListPageNumber == 1)
            {
                buttonTaxListPageListFirst.Enabled = false;
                buttonTaxListPageListPrevious.Enabled = false;
            }

            textBoxTaxListPageNumber.Text = taxListPageNumber + " / " + taxListPageList.PageCount;
        }

        private void buttonTaxListPageListNext_Click(object sender, EventArgs e)
        {
            if (taxListPageList.HasNextPage == true)
            {
                taxListPageList = new PagedList<Entities.DgvMstSystemTablesTaxListEntity>(taxListData, ++taxListPageNumber, pageSize);
                taxListDataSource.DataSource = taxListPageList;
            }

            buttonTaxListPageListFirst.Enabled = true;
            buttonTaxListPageListPrevious.Enabled = true;

            if (taxListPageNumber == taxListPageList.PageCount)
            {
                buttonTaxListPageListNext.Enabled = false;
                buttonTaxListPageListLast.Enabled = false;
            }

            textBoxTaxListPageNumber.Text = taxListPageNumber + " / " + taxListPageList.PageCount;
        }

        private void buttonTaxListPageListLast_Click(object sender, EventArgs e)
        {
            taxListPageList = new PagedList<Entities.DgvMstSystemTablesTaxListEntity>(taxListData, taxListPageList.PageCount, pageSize);
            taxListDataSource.DataSource = taxListPageList;

            buttonTaxListPageListFirst.Enabled = true;
            buttonTaxListPageListPrevious.Enabled = true;
            buttonTaxListPageListNext.Enabled = false;
            buttonTaxListPageListLast.Enabled = false;

            taxListPageNumber = taxListPageList.PageCount;
            textBoxTaxListPageNumber.Text = taxListPageNumber + " / " + taxListPageList.PageCount;
        }

        // ====
        // Unit
        // ====
        public void UpdateUnitListDataSource()
        {
            SetUnitListDataSourceAsync();
        }

        public async void SetUnitListDataSourceAsync()
        {
            List<Entities.DgvMstSystemTablesUnitListEntity> getUnitListData = await GetUnitListDataTask();
            if (getUnitListData.Any())
            {
                unitListData = getUnitListData;
                unitListPageList = new PagedList<Entities.DgvMstSystemTablesUnitListEntity>(unitListData, unitListPageNumber, pageSize);

                if (unitListPageList.PageCount == 1)
                {
                    buttonUnitListPageListFirst.Enabled = false;
                    buttonUnitListPageListPrevious.Enabled = false;
                    buttonUnitListPageListNext.Enabled = false;
                    buttonUnitListPageListLast.Enabled = false;
                }
                else if (unitListPageNumber == 1)
                {
                    buttonUnitListPageListFirst.Enabled = false;
                    buttonUnitListPageListPrevious.Enabled = false;
                    buttonUnitListPageListNext.Enabled = true;
                    buttonUnitListPageListLast.Enabled = true;
                }
                else if (unitListPageNumber == unitListPageList.PageCount)
                {
                    buttonUnitListPageListFirst.Enabled = true;
                    buttonUnitListPageListPrevious.Enabled = true;
                    buttonUnitListPageListNext.Enabled = false;
                    buttonUnitListPageListLast.Enabled = false;
                }
                else
                {
                    buttonUnitListPageListFirst.Enabled = true;
                    buttonUnitListPageListPrevious.Enabled = true;
                    buttonUnitListPageListNext.Enabled = true;
                    buttonUnitListPageListLast.Enabled = true;
                }

                textBoxUnitListPageNumber.Text = unitListPageNumber + " / " + unitListPageList.PageCount;
                unitListDataSource.DataSource = unitListPageList;
            }
            else
            {
                buttonUnitListPageListFirst.Enabled = false;
                buttonUnitListPageListPrevious.Enabled = false;
                buttonUnitListPageListNext.Enabled = false;
                buttonUnitListPageListLast.Enabled = false;

                unitListPageNumber = 1;

                unitListData = new List<Entities.DgvMstSystemTablesUnitListEntity>();
                unitListDataSource.Clear();
                textBoxUnitListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvMstSystemTablesUnitListEntity>> GetUnitListDataTask()
        {
            string gridEdit = "Edit";
            string gridDelete = "Delete";
            var language = Modules.SysCurrentModule.GetCurrentSettings().Language;
            if (language != "English")
            {
                gridEdit = "编辑";
                gridDelete = "删除";
            }
            else
            {
                gridEdit = "Edit";
                gridDelete = "Delete";
            }

            String filter = textBoxUnitListFilter.Text;
            Controllers.MstUnitController mstUnitController = new Controllers.MstUnitController();

            List<Entities.MstUnitEntity> listUnit = mstUnitController.ListUnit(filter);
            if (listUnit.Any())
            {
                var units = from d in listUnit
                            select new Entities.DgvMstSystemTablesUnitListEntity
                            {
                                ColumnUnitListButtonEdit = gridEdit,
                                ColumnUnitListButtonDelete = gridDelete,
                                ColumnUnitListId = d.Id,
                                ColumnUnitListUnit = d.Unit
                            };

                return Task.FromResult(units.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvMstSystemTablesUnitListEntity>());
            }
        }

        public void CreateUnitListDataGridView()
        {
            UpdateUnitListDataSource();

            dataGridViewUnitList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewUnitList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewUnitList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewUnitList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewUnitList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewUnitList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewUnitList.DataSource = unitListDataSource;
        }

        private void textBoxUnitListFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateUnitListDataSource();
            }
        }

        private void dataGridViewUnitList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetUnitListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewUnitList.CurrentCell.ColumnIndex == dataGridViewUnitList.Columns["ColumnUnitListButtonEdit"].Index)
            {
                Entities.MstUnitEntity selectedUnit = new Entities.MstUnitEntity()
                {
                    Id = Convert.ToInt32(dataGridViewUnitList.Rows[e.RowIndex].Cells[2].Value),
                    Unit = dataGridViewUnitList.Rows[e.RowIndex].Cells[3].Value.ToString()
                };
                SysUnitDetailForm sysSystemTablesUnitDetailForm = new SysUnitDetailForm(this, selectedUnit);
                sysSystemTablesUnitDetailForm.ShowDialog();
            }

            if (e.RowIndex > -1 && dataGridViewUnitList.CurrentCell.ColumnIndex == dataGridViewUnitList.Columns["ColumnUnitListButtonDelete"].Index)
            {
                DialogResult deleteDialogResult = MessageBox.Show("Delete Unit?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (deleteDialogResult == DialogResult.Yes)
                {
                    Controllers.MstUnitController mstUnitController = new Controllers.MstUnitController();

                    String[] deleteUnit = mstUnitController.DeleteUnit(Convert.ToInt32(dataGridViewUnitList.Rows[e.RowIndex].Cells[2].Value));
                    if (deleteUnit[1].Equals("0") == false)
                    {
                        Int32 currentPageNumber = unitListPageNumber;

                        unitListPageNumber = 1;
                        UpdateUnitListDataSource();
                    }
                    else
                    {
                        MessageBox.Show(deleteUnit[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void GetUnitListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void buttonUnitListPageListFirst_Click(object sender, EventArgs e)
        {
            unitListPageList = new PagedList<Entities.DgvMstSystemTablesUnitListEntity>(unitListData, 1, pageSize);
            unitListDataSource.DataSource = unitListPageList;

            buttonUnitListPageListFirst.Enabled = false;
            buttonUnitListPageListPrevious.Enabled = false;
            buttonUnitListPageListNext.Enabled = true;
            buttonUnitListPageListLast.Enabled = true;

            unitListPageNumber = 1;
            textBoxUnitListPageNumber.Text = unitListPageNumber + " / " + unitListPageList.PageCount;
        }

        private void buttonUnitListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (unitListPageList.HasPreviousPage == true)
            {
                unitListPageList = new PagedList<Entities.DgvMstSystemTablesUnitListEntity>(unitListData, --unitListPageNumber, pageSize);
                unitListDataSource.DataSource = unitListPageList;
            }

            buttonUnitListPageListNext.Enabled = true;
            buttonUnitListPageListLast.Enabled = true;

            if (unitListPageNumber == 1)
            {
                buttonUnitListPageListFirst.Enabled = false;
                buttonUnitListPageListPrevious.Enabled = false;
            }

            textBoxUnitListPageNumber.Text = unitListPageNumber + " / " + unitListPageList.PageCount;
        }

        private void buttonUnitListPageListNext_Click(object sender, EventArgs e)
        {
            if (unitListPageList.HasNextPage == true)
            {
                unitListPageList = new PagedList<Entities.DgvMstSystemTablesUnitListEntity>(unitListData, ++unitListPageNumber, pageSize);
                unitListDataSource.DataSource = unitListPageList;
            }

            buttonUnitListPageListFirst.Enabled = true;
            buttonUnitListPageListPrevious.Enabled = true;

            if (unitListPageNumber == unitListPageList.PageCount)
            {
                buttonUnitListPageListNext.Enabled = false;
                buttonUnitListPageListLast.Enabled = false;
            }

            textBoxUnitListPageNumber.Text = unitListPageNumber + " / " + unitListPageList.PageCount;
        }

        private void buttonUnitListPageListLast_Click(object sender, EventArgs e)
        {
            unitListPageList = new PagedList<Entities.DgvMstSystemTablesUnitListEntity>(unitListData, unitListPageList.PageCount, pageSize);
            unitListDataSource.DataSource = unitListPageList;

            buttonUnitListPageListFirst.Enabled = true;
            buttonUnitListPageListPrevious.Enabled = true;
            buttonUnitListPageListNext.Enabled = false;
            buttonUnitListPageListLast.Enabled = false;

            unitListPageNumber = unitListPageList.PageCount;
            textBoxUnitListPageNumber.Text = unitListPageNumber + " / " + unitListPageList.PageCount;
        }

        // ======
        // Period
        // ======
        public void UpdatePeriodListDataSource()
        {
            SetPeriodListDataSourceAsync();
        }

        public async void SetPeriodListDataSourceAsync()
        {
            List<Entities.DgvMstSystemTablesPeriodListEntity> getPeriodListData = await GetPeriodListDataTask();
            if (getPeriodListData.Any())
            {
                periodListData = getPeriodListData;
                periodListPageList = new PagedList<Entities.DgvMstSystemTablesPeriodListEntity>(periodListData, periodListPageNumber, pageSize);

                if (periodListPageList.PageCount == 1)
                {
                    buttonPeriodListPageListFirst.Enabled = false;
                    buttonPeriodListPageListPrevious.Enabled = false;
                    buttonPeriodListPageListNext.Enabled = false;
                    buttonPeriodListPageListLast.Enabled = false;
                }
                else if (periodListPageNumber == 1)
                {
                    buttonPeriodListPageListFirst.Enabled = false;
                    buttonPeriodListPageListPrevious.Enabled = false;
                    buttonPeriodListPageListNext.Enabled = true;
                    buttonPeriodListPageListLast.Enabled = true;
                }
                else if (periodListPageNumber == periodListPageList.PageCount)
                {
                    buttonPeriodListPageListFirst.Enabled = true;
                    buttonPeriodListPageListPrevious.Enabled = true;
                    buttonPeriodListPageListNext.Enabled = false;
                    buttonPeriodListPageListLast.Enabled = false;
                }
                else
                {
                    buttonPeriodListPageListFirst.Enabled = true;
                    buttonPeriodListPageListPrevious.Enabled = true;
                    buttonPeriodListPageListNext.Enabled = true;
                    buttonPeriodListPageListLast.Enabled = true;
                }

                textBoxPeriodListPageNumber.Text = periodListPageNumber + " / " + periodListPageList.PageCount;
                periodListDataSource.DataSource = periodListPageList;
            }
            else
            {
                buttonPeriodListPageListFirst.Enabled = false;
                buttonPeriodListPageListPrevious.Enabled = false;
                buttonPeriodListPageListNext.Enabled = false;
                buttonPeriodListPageListLast.Enabled = false;

                periodListPageNumber = 1;

                periodListData = new List<Entities.DgvMstSystemTablesPeriodListEntity>();
                periodListDataSource.Clear();
                textBoxPeriodListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvMstSystemTablesPeriodListEntity>> GetPeriodListDataTask()
        {
            String filter = textBoxPeriodListFilter.Text;
            Controllers.MstPeriodController mstPeriodController = new Controllers.MstPeriodController();

            List<Entities.MstPeriodEntity> listPeriod = mstPeriodController.ListPeriod(filter);
            if (listPeriod.Any())
            {
                var periods = from d in listPeriod
                              select new Entities.DgvMstSystemTablesPeriodListEntity
                              {
                                  ColumnPeriodListButtonEdit = SetLabelEditDelete("Edit"),
                                  ColumnPeriodListButtonDelete = SetLabelEditDelete("Delete"),
                                  ColumnPeriodListId = d.Id,
                                  ColumnPeriodListPeriod = d.Period
                              };

                return Task.FromResult(periods.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvMstSystemTablesPeriodListEntity>());
            }
        }

        public void CreatePeriodListDataGridView()
        {
            UpdatePeriodListDataSource();

            dataGridViewPeriodList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewPeriodList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewPeriodList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewPeriodList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewPeriodList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewPeriodList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewPeriodList.DataSource = periodListDataSource;
        }

        private void textBoxPeriodListFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdatePeriodListDataSource();
            }
        }

        private void dataGridViewPeriodList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetPeriodListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewPeriodList.CurrentCell.ColumnIndex == dataGridViewPeriodList.Columns["ColumnPeriodListButtonEdit"].Index)
            {
                Entities.MstPeriodEntity selectedPeriod = new Entities.MstPeriodEntity()
                {
                    Id = Convert.ToInt32(dataGridViewPeriodList.Rows[e.RowIndex].Cells[2].Value),
                    Period = dataGridViewPeriodList.Rows[e.RowIndex].Cells[3].Value.ToString()
                };
                SysPeriodDetailForm sysSystemTablesPeriodDetailForm = new SysPeriodDetailForm(this, selectedPeriod);
                sysSystemTablesPeriodDetailForm.ShowDialog();
            }

            if (e.RowIndex > -1 && dataGridViewPeriodList.CurrentCell.ColumnIndex == dataGridViewPeriodList.Columns["ColumnPeriodListButtonDelete"].Index)
            {
                DialogResult deleteDialogResult = MessageBox.Show("Delete Period?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (deleteDialogResult == DialogResult.Yes)
                {
                    Controllers.MstPeriodController mstPeriodController = new Controllers.MstPeriodController();

                    String[] deletePeriod = mstPeriodController.DeletePeriod(Convert.ToInt32(dataGridViewPeriodList.Rows[e.RowIndex].Cells[2].Value));
                    if (deletePeriod[1].Equals("0") == false)
                    {
                        Int32 currentPageNumber = periodListPageNumber;

                        periodListPageNumber = 1;
                        UpdatePeriodListDataSource();
                    }
                    else
                    {
                        MessageBox.Show(deletePeriod[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void GetPeriodListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void buttonPeriodListPageListFirst_Click(object sender, EventArgs e)
        {
            periodListPageList = new PagedList<Entities.DgvMstSystemTablesPeriodListEntity>(periodListData, 1, pageSize);
            periodListDataSource.DataSource = periodListPageList;

            buttonPeriodListPageListFirst.Enabled = false;
            buttonPeriodListPageListPrevious.Enabled = false;
            buttonPeriodListPageListNext.Enabled = true;
            buttonPeriodListPageListLast.Enabled = true;

            periodListPageNumber = 1;
            textBoxPeriodListPageNumber.Text = periodListPageNumber + " / " + periodListPageList.PageCount;
        }

        private void buttonPeriodListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (periodListPageList.HasPreviousPage == true)
            {
                periodListPageList = new PagedList<Entities.DgvMstSystemTablesPeriodListEntity>(periodListData, --periodListPageNumber, pageSize);
                periodListDataSource.DataSource = periodListPageList;
            }

            buttonPeriodListPageListNext.Enabled = true;
            buttonPeriodListPageListLast.Enabled = true;

            if (periodListPageNumber == 1)
            {
                buttonPeriodListPageListFirst.Enabled = false;
                buttonPeriodListPageListPrevious.Enabled = false;
            }

            textBoxPeriodListPageNumber.Text = periodListPageNumber + " / " + periodListPageList.PageCount;
        }

        private void buttonPeriodListPageListNext_Click(object sender, EventArgs e)
        {
            if (periodListPageList.HasNextPage == true)
            {
                periodListPageList = new PagedList<Entities.DgvMstSystemTablesPeriodListEntity>(periodListData, ++periodListPageNumber, pageSize);
                periodListDataSource.DataSource = periodListPageList;
            }

            buttonPeriodListPageListFirst.Enabled = true;
            buttonPeriodListPageListPrevious.Enabled = true;

            if (periodListPageNumber == periodListPageList.PageCount)
            {
                buttonPeriodListPageListNext.Enabled = false;
                buttonPeriodListPageListLast.Enabled = false;
            }

            textBoxPeriodListPageNumber.Text = periodListPageNumber + " / " + periodListPageList.PageCount;
        }

        private void buttonPeriodListPageListLast_Click(object sender, EventArgs e)
        {
            periodListPageList = new PagedList<Entities.DgvMstSystemTablesPeriodListEntity>(periodListData, periodListPageList.PageCount, pageSize);
            periodListDataSource.DataSource = periodListPageList;

            buttonPeriodListPageListFirst.Enabled = true;
            buttonPeriodListPageListPrevious.Enabled = true;
            buttonPeriodListPageListNext.Enabled = false;
            buttonPeriodListPageListLast.Enabled = false;

            periodListPageNumber = periodListPageList.PageCount;
            textBoxPeriodListPageNumber.Text = periodListPageNumber + " / " + periodListPageList.PageCount;
        }

        // ========
        // Terminal
        // ========
        public void UpdateTerminalListDataSource()
        {
            SetTerminalListDataSourceAsync();
        }

        public async void SetTerminalListDataSourceAsync()
        {
            List<Entities.DgvMstSystemTablesTerminalListEntity> getTerminalListData = await GetTerminalListDataTask();
            if (getTerminalListData.Any())
            {
                terminalListData = getTerminalListData;
                terminalListPageList = new PagedList<Entities.DgvMstSystemTablesTerminalListEntity>(terminalListData, terminalListPageNumber, pageSize);

                if (terminalListPageList.PageCount == 1)
                {
                    buttonTerminalListPageListFirst.Enabled = false;
                    buttonTerminalListPageListPrevious.Enabled = false;
                    buttonTerminalListPageListNext.Enabled = false;
                    buttonTerminalListPageListLast.Enabled = false;
                }
                else if (terminalListPageNumber == 1)
                {
                    buttonTerminalListPageListFirst.Enabled = false;
                    buttonTerminalListPageListPrevious.Enabled = false;
                    buttonTerminalListPageListNext.Enabled = true;
                    buttonTerminalListPageListLast.Enabled = true;
                }
                else if (terminalListPageNumber == terminalListPageList.PageCount)
                {
                    buttonTerminalListPageListFirst.Enabled = true;
                    buttonTerminalListPageListPrevious.Enabled = true;
                    buttonTerminalListPageListNext.Enabled = false;
                    buttonTerminalListPageListLast.Enabled = false;
                }
                else
                {
                    buttonTerminalListPageListFirst.Enabled = true;
                    buttonTerminalListPageListPrevious.Enabled = true;
                    buttonTerminalListPageListNext.Enabled = true;
                    buttonTerminalListPageListLast.Enabled = true;
                }

                textBoxTerminalListPageNumber.Text = terminalListPageNumber + " / " + terminalListPageList.PageCount;
                terminalListDataSource.DataSource = terminalListPageList;
            }
            else
            {
                buttonTerminalListPageListFirst.Enabled = false;
                buttonTerminalListPageListPrevious.Enabled = false;
                buttonTerminalListPageListNext.Enabled = false;
                buttonTerminalListPageListLast.Enabled = false;

                terminalListPageNumber = 1;

                terminalListData = new List<Entities.DgvMstSystemTablesTerminalListEntity>();
                terminalListDataSource.Clear();
                textBoxTerminalListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvMstSystemTablesTerminalListEntity>> GetTerminalListDataTask()
        {
            String filter = textBoxTerminalListFilter.Text;
            Controllers.MstTerminalController mstTerminalController = new Controllers.MstTerminalController();

            List<Entities.MstTerminalEntity> listTerminal = mstTerminalController.ListTerminal(filter);
            if (listTerminal.Any())
            {
                var terminals = from d in listTerminal
                                select new Entities.DgvMstSystemTablesTerminalListEntity
                                {
                                    ColumnTerminalListButtonEdit = SetLabelEditDelete("Edit"),
                                    ColumnTerminalListButtonDelete = SetLabelEditDelete("Delete"),
                                    ColumnTerminalListId = d.Id,
                                    ColumnTerminalListTerminal = d.Terminal
                                };

                return Task.FromResult(terminals.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvMstSystemTablesTerminalListEntity>());
            }
        }

        public void CreateTerminalListDataGridView()
        {
            UpdateTerminalListDataSource();

            dataGridViewTerminalList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewTerminalList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewTerminalList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewTerminalList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewTerminalList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewTerminalList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewTerminalList.DataSource = terminalListDataSource;
        }

        private void textBoxTerminalListFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateTerminalListDataSource();
            }
        }

        private void dataGridViewTerminalList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetTerminalListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewTerminalList.CurrentCell.ColumnIndex == dataGridViewTerminalList.Columns["ColumnTerminalListButtonEdit"].Index)
            {
                Entities.MstTerminalEntity selectedTerminal = new Entities.MstTerminalEntity()
                {
                    Id = Convert.ToInt32(dataGridViewTerminalList.Rows[e.RowIndex].Cells[2].Value),
                    Terminal = dataGridViewTerminalList.Rows[e.RowIndex].Cells[3].Value.ToString()
                };
                SysTerminalDetailForm sysSystemTablesTerminalDetailForm = new SysTerminalDetailForm(this, selectedTerminal);
                sysSystemTablesTerminalDetailForm.ShowDialog();
            }

            if (e.RowIndex > -1 && dataGridViewTerminalList.CurrentCell.ColumnIndex == dataGridViewTerminalList.Columns["ColumnTerminalListButtonDelete"].Index)
            {
                DialogResult deleteDialogResult = MessageBox.Show("Delete Terminal?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (deleteDialogResult == DialogResult.Yes)
                {
                    Controllers.MstTerminalController mstTerminalController = new Controllers.MstTerminalController();

                    String[] deleteTerminal = mstTerminalController.DeleteTerminal(Convert.ToInt32(dataGridViewTerminalList.Rows[e.RowIndex].Cells[2].Value));
                    if (deleteTerminal[1].Equals("0") == false)
                    {
                        Int32 currentPageNumber = terminalListPageNumber;

                        terminalListPageNumber = 1;
                        UpdateTerminalListDataSource();
                    }
                    else
                    {
                        MessageBox.Show(deleteTerminal[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void GetTerminalListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void buttonTerminalListPageListFirst_Click(object sender, EventArgs e)
        {
            terminalListPageList = new PagedList<Entities.DgvMstSystemTablesTerminalListEntity>(terminalListData, 1, pageSize);
            terminalListDataSource.DataSource = terminalListPageList;

            buttonTerminalListPageListFirst.Enabled = false;
            buttonTerminalListPageListPrevious.Enabled = false;
            buttonTerminalListPageListNext.Enabled = true;
            buttonTerminalListPageListLast.Enabled = true;

            terminalListPageNumber = 1;
            textBoxTerminalListPageNumber.Text = terminalListPageNumber + " / " + terminalListPageList.PageCount;
        }

        private void buttonTerminalListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (terminalListPageList.HasPreviousPage == true)
            {
                terminalListPageList = new PagedList<Entities.DgvMstSystemTablesTerminalListEntity>(terminalListData, --terminalListPageNumber, pageSize);
                terminalListDataSource.DataSource = terminalListPageList;
            }

            buttonTerminalListPageListNext.Enabled = true;
            buttonTerminalListPageListLast.Enabled = true;

            if (terminalListPageNumber == 1)
            {
                buttonTerminalListPageListFirst.Enabled = false;
                buttonTerminalListPageListPrevious.Enabled = false;
            }

            textBoxTerminalListPageNumber.Text = terminalListPageNumber + " / " + terminalListPageList.PageCount;
        }

        private void buttonTerminalListPageListNext_Click(object sender, EventArgs e)
        {
            if (terminalListPageList.HasNextPage == true)
            {
                terminalListPageList = new PagedList<Entities.DgvMstSystemTablesTerminalListEntity>(terminalListData, ++terminalListPageNumber, pageSize);
                terminalListDataSource.DataSource = terminalListPageList;
            }

            buttonTerminalListPageListFirst.Enabled = true;
            buttonTerminalListPageListPrevious.Enabled = true;

            if (terminalListPageNumber == terminalListPageList.PageCount)
            {
                buttonTerminalListPageListNext.Enabled = false;
                buttonTerminalListPageListLast.Enabled = false;
            }

            textBoxTerminalListPageNumber.Text = terminalListPageNumber + " / " + terminalListPageList.PageCount;
        }

        private void buttonTerminalListPageListLast_Click(object sender, EventArgs e)
        {
            terminalListPageList = new PagedList<Entities.DgvMstSystemTablesTerminalListEntity>(terminalListData, terminalListPageList.PageCount, pageSize);
            terminalListDataSource.DataSource = terminalListPageList;

            buttonTerminalListPageListFirst.Enabled = true;
            buttonTerminalListPageListPrevious.Enabled = true;
            buttonTerminalListPageListNext.Enabled = false;
            buttonTerminalListPageListLast.Enabled = false;

            terminalListPageNumber = terminalListPageList.PageCount;
            textBoxTerminalListPageNumber.Text = terminalListPageNumber + " / " + terminalListPageList.PageCount;
        }

        // ========
        // Supplier
        // ========
        public void UpdateSupplierListDataSource()
        {
            SetSupplierListDataSourceAsync();
        }

        public async void SetSupplierListDataSourceAsync()
        {
            List<Entities.DgvMstSystemTablesSupplierListEntity> getSupplierListData = await GetSupplierListDataTask();
            if (getSupplierListData.Any())
            {
                supplierListData = getSupplierListData;
                supplierListPageList = new PagedList<Entities.DgvMstSystemTablesSupplierListEntity>(supplierListData, supplierListPageNumber, pageSize);

                if (supplierListPageList.PageCount == 1)
                {
                    buttonSupplierListPageListFirst.Enabled = false;
                    buttonSupplierListPageListPrevious.Enabled = false;
                    buttonSupplierListPageListNext.Enabled = false;
                    buttonSupplierListPageListLast.Enabled = false;
                }
                else if (supplierListPageNumber == 1)
                {
                    buttonSupplierListPageListFirst.Enabled = false;
                    buttonSupplierListPageListPrevious.Enabled = false;
                    buttonSupplierListPageListNext.Enabled = true;
                    buttonSupplierListPageListLast.Enabled = true;
                }
                else if (supplierListPageNumber == supplierListPageList.PageCount)
                {
                    buttonSupplierListPageListFirst.Enabled = true;
                    buttonSupplierListPageListPrevious.Enabled = true;
                    buttonSupplierListPageListNext.Enabled = false;
                    buttonSupplierListPageListLast.Enabled = false;
                }
                else
                {
                    buttonSupplierListPageListFirst.Enabled = true;
                    buttonSupplierListPageListPrevious.Enabled = true;
                    buttonSupplierListPageListNext.Enabled = true;
                    buttonSupplierListPageListLast.Enabled = true;
                }

                textBoxSupplierListPageNumber.Text = supplierListPageNumber + " / " + supplierListPageList.PageCount;
                supplierListDataSource.DataSource = supplierListPageList;
            }
            else
            {
                buttonSupplierListPageListFirst.Enabled = false;
                buttonSupplierListPageListPrevious.Enabled = false;
                buttonSupplierListPageListNext.Enabled = false;
                buttonSupplierListPageListLast.Enabled = false;

                supplierListPageNumber = 1;

                supplierListData = new List<Entities.DgvMstSystemTablesSupplierListEntity>();
                supplierListDataSource.Clear();
                textBoxSupplierListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvMstSystemTablesSupplierListEntity>> GetSupplierListDataTask()
        {
            string gridEdit = "Edit";
            string gridDelete = "Delete";
            var language = Modules.SysCurrentModule.GetCurrentSettings().Language;
            if (language != "English")
            {
                gridEdit = "编辑";
                gridDelete = "删除";
            }
            else
            {
                gridEdit = "Edit";
                gridDelete = "Delete";
            }

            String filter = textBoxSupplierListFilter.Text;
            Controllers.MstSupplierController mstSupplierController = new Controllers.MstSupplierController();

            List<Entities.MstSupplierEntity> listSupplier = mstSupplierController.ListSupplier(filter);
            if (listSupplier.Any())
            {
                var suppliers = from d in listSupplier
                                select new Entities.DgvMstSystemTablesSupplierListEntity
                                {
                                    ColumnSupplierListButtonEdit = gridEdit,
                                    ColumnSupplierListButtonDelete = gridDelete,
                                    ColumnSupplierListId = d.Id,
                                    ColumnSupplierListSupplier = d.Supplier,
                                    ColumnSupplierListAddress = d.Address,
                                    ColumnSupplierListTelephoneNumber = d.TelephoneNumber,
                                    ColumnSupplierListCellphoneNumber = d.CellphoneNumber,
                                    ColumnSupplierListFaxNumber = d.FaxNumber,
                                    ColumnSupplierListTermId = d.TermId,
                                    ColumnSupplierListTIN = d.TIN,
                                    ColumnSupplierListAccountId = d.AccountId,
                                    ColumnSupplierListIsLocked = d.IsLocked
                                };

                return Task.FromResult(suppliers.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvMstSystemTablesSupplierListEntity>());
            }
        }

        public void CreateSupplierListDataGridView()
        {
            UpdateSupplierListDataSource();

            dataGridViewSupplierList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewSupplierList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewSupplierList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewSupplierList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewSupplierList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewSupplierList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewSupplierList.DataSource = supplierListDataSource;
        }

        private void textBoxSupplierListFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateSupplierListDataSource();
            }
        }

        private void dataGridViewSupplierList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetSupplierListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewSupplierList.CurrentCell.ColumnIndex == dataGridViewSupplierList.Columns["ColumnSupplierListButtonEdit"].Index)
            {
                Entities.MstSupplierEntity selectedSupplier = new Entities.MstSupplierEntity()
                {
                    Id = Convert.ToInt32(dataGridViewSupplierList.Rows[e.RowIndex].Cells[2].Value),
                    Supplier = dataGridViewSupplierList.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    Address = dataGridViewSupplierList.Rows[e.RowIndex].Cells[4].Value.ToString(),
                    TelephoneNumber = dataGridViewSupplierList.Rows[e.RowIndex].Cells[5].Value.ToString(),
                    CellphoneNumber = dataGridViewSupplierList.Rows[e.RowIndex].Cells[6].Value.ToString(),
                    FaxNumber = dataGridViewSupplierList.Rows[e.RowIndex].Cells[7].Value.ToString(),
                    TermId = Convert.ToInt32(dataGridViewSupplierList.Rows[e.RowIndex].Cells[8].Value),
                    TIN = dataGridViewSupplierList.Rows[e.RowIndex].Cells[9].Value.ToString(),
                    AccountId = Convert.ToInt32(dataGridViewSupplierList.Rows[e.RowIndex].Cells[10].Value),
                    IsLocked = Convert.ToBoolean(dataGridViewSupplierList.Rows[e.RowIndex].Cells[11].Value)
                };

                SysSupplierDetailForm systemTablesSupplierDetailForm = new SysSupplierDetailForm(this, selectedSupplier);
                systemTablesSupplierDetailForm.ShowDialog();
            }

            if (e.RowIndex > -1 && dataGridViewSupplierList.CurrentCell.ColumnIndex == dataGridViewSupplierList.Columns["ColumnSupplierListButtonDelete"].Index)
            {
                DialogResult deleteDialogResult = MessageBox.Show("Delete Supplier?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (deleteDialogResult == DialogResult.Yes)
                {
                    Controllers.MstSupplierController mstSupplierController = new Controllers.MstSupplierController();

                    String[] deleteSupplier = mstSupplierController.DeleteSupplier(Convert.ToInt32(dataGridViewSupplierList.Rows[e.RowIndex].Cells[2].Value));
                    if (deleteSupplier[1].Equals("0") == false)
                    {
                        Int32 currentPageNumber = supplierListPageNumber;

                        supplierListPageNumber = 1;
                        UpdateSupplierListDataSource();
                    }
                    else
                    {
                        MessageBox.Show(deleteSupplier[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void GetSupplierListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void buttonSupplierListPageListFirst_Click(object sender, EventArgs e)
        {
            supplierListPageList = new PagedList<Entities.DgvMstSystemTablesSupplierListEntity>(supplierListData, 1, pageSize);
            supplierListDataSource.DataSource = supplierListPageList;

            buttonSupplierListPageListFirst.Enabled = false;
            buttonSupplierListPageListPrevious.Enabled = false;
            buttonSupplierListPageListNext.Enabled = true;
            buttonSupplierListPageListLast.Enabled = true;

            supplierListPageNumber = 1;
            textBoxSupplierListPageNumber.Text = supplierListPageNumber + " / " + supplierListPageList.PageCount;
        }

        private void buttonSupplierListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (supplierListPageList.HasPreviousPage == true)
            {
                supplierListPageList = new PagedList<Entities.DgvMstSystemTablesSupplierListEntity>(supplierListData, --supplierListPageNumber, pageSize);
                supplierListDataSource.DataSource = supplierListPageList;
            }

            buttonSupplierListPageListNext.Enabled = true;
            buttonSupplierListPageListLast.Enabled = true;

            if (supplierListPageNumber == 1)
            {
                buttonSupplierListPageListFirst.Enabled = false;
                buttonSupplierListPageListPrevious.Enabled = false;
            }

            textBoxSupplierListPageNumber.Text = supplierListPageNumber + " / " + supplierListPageList.PageCount;
        }

        private void buttonSupplierListPageListNext_Click(object sender, EventArgs e)
        {
            if (supplierListPageList.HasNextPage == true)
            {
                supplierListPageList = new PagedList<Entities.DgvMstSystemTablesSupplierListEntity>(supplierListData, ++supplierListPageNumber, pageSize);
                supplierListDataSource.DataSource = supplierListPageList;
            }

            buttonSupplierListPageListFirst.Enabled = true;
            buttonSupplierListPageListPrevious.Enabled = true;

            if (supplierListPageNumber == supplierListPageList.PageCount)
            {
                buttonSupplierListPageListNext.Enabled = false;
                buttonSupplierListPageListLast.Enabled = false;
            }

            textBoxSupplierListPageNumber.Text = supplierListPageNumber + " / " + supplierListPageList.PageCount;
        }

        private void buttonSupplierListPageListLast_Click(object sender, EventArgs e)
        {
            supplierListPageList = new PagedList<Entities.DgvMstSystemTablesSupplierListEntity>(supplierListData, supplierListPageList.PageCount, pageSize);
            supplierListDataSource.DataSource = supplierListPageList;

            buttonSupplierListPageListFirst.Enabled = true;
            buttonSupplierListPageListPrevious.Enabled = true;
            buttonSupplierListPageListNext.Enabled = false;
            buttonSupplierListPageListLast.Enabled = false;

            supplierListPageNumber = supplierListPageList.PageCount;
            textBoxSupplierListPageNumber.Text = supplierListPageNumber + " / " + supplierListPageList.PageCount;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            sysSoftwareForm.RemoveTabPage();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            String selectedTab = tabControlSystemTable.SelectedTab.Text.ToString();
            switch (selectedTab)
            {
                case "Account":
                case "帐户":
                    SysAccountDetailForm sysSystemTablesAccountDetailForm = new SysAccountDetailForm(this, null);
                    sysSystemTablesAccountDetailForm.ShowDialog();
                    break;
                case "Pay Type":
                case "薪酬类型":
                    SysPayTypeDetailForm sysSystemTablesPayTypeDetailForm = new SysPayTypeDetailForm(this, null);
                    sysSystemTablesPayTypeDetailForm.ShowDialog();
                    break;
                case "Tax":
                case "税":
                    SysTaxDetailForm sysSystemTablesTaxDetailForm = new SysTaxDetailForm(this, null);
                    sysSystemTablesTaxDetailForm.ShowDialog();
                    break;
                case "Unit":
                case "单元":
                    SysUnitDetailForm sysSystemTablesUnitDetailForm = new SysUnitDetailForm(this, null);
                    sysSystemTablesUnitDetailForm.ShowDialog();
                    break;
                case "Period":
                case "时期":
                    SysPeriodDetailForm sysSystemTablesPeriodDetailForm = new SysPeriodDetailForm(this, null);
                    sysSystemTablesPeriodDetailForm.ShowDialog();
                    break;
                case "Terminal":
                case "终端":
                    SysTerminalDetailForm sysSystemTablesTerminalDetailForm = new SysTerminalDetailForm(this, null);
                    sysSystemTablesTerminalDetailForm.ShowDialog();
                    break;
                case "Supplier":
                case "供应商":
                    Controllers.MstSupplierController mstSupplierController = new Controllers.MstSupplierController();
                    String[] newSupplier = mstSupplierController.AddSupplier();
                    if (newSupplier[1].Equals("0") == false)
                    {
                        var newSupplierDetail = mstSupplierController.DetailSupplier(Convert.ToInt32(newSupplier[0]));

                        SysSupplierDetailForm systemTablesSupplierDetailForm = new SysSupplierDetailForm(this, newSupplierDetail);
                        systemTablesSupplierDetailForm.ShowDialog();

                        UpdateSupplierListDataSource();
                    }
                    else
                    {
                        MessageBox.Show(newSupplier[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "Label":
                case "标签":
                    SysLanguageDetailForm sysLanguageDetailForm = new SysLanguageDetailForm(this, null);
                    sysLanguageDetailForm.ShowDialog();
                    break;
            }
        }
        // =====
        // Label
        // =====
        public void UpdateLanguageListDataSource()
        {
            SetLanguageListDataSourceAsync();
        }

        public async void SetLanguageListDataSourceAsync()
        {
            List<Entities.DgvMstSystemTablesLanguageListEntity> getLanguagetListData = await GetLanguageListDataTask();
            if (getLanguagetListData.Any())
            {
                languageListData = getLanguagetListData;
                languageListPageList = new PagedList<Entities.DgvMstSystemTablesLanguageListEntity>(languageListData, languageListPageNumber, pageSize);

                if (languageListPageList.PageCount == 1)
                {
                    buttonLanguageListPageListFirst.Enabled = false;
                    buttonLanguageListPageListPrevious.Enabled = false;
                    buttonLanguageListPageListNext.Enabled = false;
                    buttonLanguageListPageListLast.Enabled = false;
                }
                else if (languageListPageNumber == 1)
                {
                    buttonLanguageListPageListFirst.Enabled = false;
                    buttonLanguageListPageListPrevious.Enabled = false;
                    buttonLanguageListPageListNext.Enabled = true;
                    buttonLanguageListPageListLast.Enabled = true;
                }
                else if (languageListPageNumber == languageListPageList.PageCount)
                {
                    buttonLanguageListPageListFirst.Enabled = true;
                    buttonLanguageListPageListPrevious.Enabled = true;
                    buttonLanguageListPageListNext.Enabled = false;
                    buttonLanguageListPageListLast.Enabled = false;
                }
                else
                {
                    buttonLanguageListPageListFirst.Enabled = true;
                    buttonLanguageListPageListPrevious.Enabled = true;
                    buttonLanguageListPageListNext.Enabled = true;
                    buttonLanguageListPageListLast.Enabled = true;
                }

                textBoxLanguageListPageNumber.Text = languageListPageNumber + " / " + languageListPageList.PageCount;
                languageListDataSource.DataSource = languageListPageList;
            }
            else
            {
                buttonLanguageListPageListFirst.Enabled = false;
                buttonLanguageListPageListPrevious.Enabled = false;
                buttonLanguageListPageListNext.Enabled = false;
                buttonLanguageListPageListLast.Enabled = false;

                languageListPageNumber = 1;

                languageListData = new List<Entities.DgvMstSystemTablesLanguageListEntity>();
                languageListDataSource.Clear();
                textBoxLanguageListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvMstSystemTablesLanguageListEntity>> GetLanguageListDataTask()
        {
            string gridEdit = "Edit";
            string gridDelete = "Delete";
            var languageLabel = Modules.SysCurrentModule.GetCurrentSettings().Language;
            if (languageLabel != "English")
            {
                gridEdit = "编辑";
                gridDelete = "删除";
            }
            else
            {
                gridEdit = "Edit";
                gridDelete = "Delete";
            }

            String filter = textBoxLanguageListFilter.Text;
            Controllers.SysLanguageController sysLanguageController = new Controllers.SysLanguageController();

            List<Entities.SysLanguageEntity> listLanguage = sysLanguageController.ListLanguage(filter);
            if (listLanguage.Any())
            {
                var language = from d in listLanguage
                               select new Entities.DgvMstSystemTablesLanguageListEntity
                               {
                                   ColumnLanguageListButtonEdit = gridEdit,
                                   ColumnLanguageListButtonDelete = gridDelete,
                                   ColumnLanguageListId = d.Id,
                                   ColumnLanguageListLabel = d.Label,
                                   ColumnLanguageListDisplayLabel = d.DisplayedLabel,

                               };

                return Task.FromResult(language.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvMstSystemTablesLanguageListEntity>());
            }
        }

        public void CreateLanguageListDataGridView()
        {
            UpdateLanguageListDataSource();

            dataGridViewLanguageList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewLanguageList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewLanguageList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewLanguageList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewLanguageList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewLanguageList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewLanguageList.DataSource = languageListDataSource;
        }

        private void textBoxLanguageListFilter_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateLanguageListDataSource();
            }
        }

        private void dataGridViewLanguageList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetLanguageListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewLanguageList.CurrentCell.ColumnIndex == dataGridViewLanguageList.Columns["ColumnLanguageListButtonEdit"].Index)
            {
                Entities.SysLanguageEntity selectedLanguage = new Entities.SysLanguageEntity()
                {
                    Id = Convert.ToInt32(dataGridViewLanguageList.Rows[e.RowIndex].Cells[2].Value),
                    Label = dataGridViewLanguageList.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    DisplayedLabel = dataGridViewLanguageList.Rows[e.RowIndex].Cells[4].Value.ToString()
                };
                SysLanguageDetailForm sysSystemTablesLanguageDetailForm = new SysLanguageDetailForm(this, selectedLanguage);
                sysSystemTablesLanguageDetailForm.ShowDialog();
            }

            if (e.RowIndex > -1 && dataGridViewLanguageList.CurrentCell.ColumnIndex == dataGridViewLanguageList.Columns["ColumnLanguageListButtonDelete"].Index)
            {
                DialogResult deleteDialogResult = MessageBox.Show("Delete Label?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (deleteDialogResult == DialogResult.Yes)
                {
                    Controllers.SysLanguageController sysLanguageController = new Controllers.SysLanguageController();

                    String[] deleteLanguage = sysLanguageController.DeleteLanguage(Convert.ToInt32(dataGridViewLanguageList.Rows[e.RowIndex].Cells[2].Value));
                    if (deleteLanguage[1].Equals("0") == false)
                    {
                        Int32 currentPageNumber = languageListPageNumber;

                        languageListPageNumber = 1;
                        UpdateLanguageListDataSource();
                    }
                    else
                    {
                        MessageBox.Show(deleteLanguage[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        public void GetLanguageListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void buttonLanguageListPageListFirst_Click(object sender, EventArgs e)
        {
            languageListPageList = new PagedList<Entities.DgvMstSystemTablesLanguageListEntity>(languageListData, 1, pageSize);
            languageListDataSource.DataSource = languageListPageList;

            buttonLanguageListPageListFirst.Enabled = false;
            buttonLanguageListPageListPrevious.Enabled = false;
            buttonLanguageListPageListNext.Enabled = true;
            buttonLanguageListPageListLast.Enabled = true;

            languageListPageNumber = 1;
            textBoxLanguageListPageNumber.Text = languageListPageNumber + " / " + languageListPageList.PageCount;
        }

        private void buttonLanguageListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (languageListPageList.HasPreviousPage == true)
            {
                languageListPageList = new PagedList<Entities.DgvMstSystemTablesLanguageListEntity>(languageListData, --languageListPageNumber, pageSize);
                languageListDataSource.DataSource = languageListPageList;
            }

            buttonLanguageListPageListNext.Enabled = true;
            buttonLanguageListPageListLast.Enabled = true;

            if (languageListPageNumber == 1)
            {
                buttonLanguageListPageListFirst.Enabled = false;
                buttonLanguageListPageListPrevious.Enabled = false;
            }

            textBoxLanguageListPageNumber.Text = languageListPageNumber + " / " + languageListPageList.PageCount;
        }

        private void buttonLanguageListPageListNext_Click(object sender, EventArgs e)
        {
            if (languageListPageList.HasNextPage == true)
            {
                languageListPageList = new PagedList<Entities.DgvMstSystemTablesLanguageListEntity>(languageListData, ++languageListPageNumber, pageSize);
                languageListDataSource.DataSource = languageListPageList;
            }

            buttonLanguageListPageListFirst.Enabled = true;
            buttonLanguageListPageListPrevious.Enabled = true;

            if (languageListPageNumber == languageListPageList.PageCount)
            {
                buttonLanguageListPageListNext.Enabled = false;
                buttonLanguageListPageListLast.Enabled = false;
            }

            textBoxLanguageListPageNumber.Text = languageListPageNumber + " / " + languageListPageList.PageCount;
        }

        private void buttonLanguageListPageListLast_Click(object sender, EventArgs e)
        {
            languageListPageList = new PagedList<Entities.DgvMstSystemTablesLanguageListEntity>(languageListData, languageListPageList.PageCount, pageSize);
            languageListDataSource.DataSource = languageListPageList;

            buttonLanguageListPageListFirst.Enabled = true;
            buttonLanguageListPageListPrevious.Enabled = true;
            buttonLanguageListPageListNext.Enabled = false;
            buttonLanguageListPageListLast.Enabled = false;

            languageListPageNumber = languageListPageList.PageCount;
            textBoxLanguageListPageNumber.Text = languageListPageNumber + " / " + languageListPageList.PageCount;
        }
    }

}
