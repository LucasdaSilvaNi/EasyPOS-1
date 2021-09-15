using EasyPOS.Forms.Software.MstItem;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.MstCustomer
{
    public partial class MstCustomerDetailForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public MstCustomerListForm mstCustomerListForm;
        public Entities.MstCustomerEntity mstCustomerEntity;

        public static Int32 pageSize = 50;
        public static Int32 pageNumber = 1;

        public static List<Entities.DgvMstCustomerLoadListEntity> customerLoadListData = new List<Entities.DgvMstCustomerLoadListEntity>();
        public PagedList<Entities.DgvMstCustomerLoadListEntity> customerLoadListPageList = new PagedList<Entities.DgvMstCustomerLoadListEntity>(customerLoadListData, pageNumber, pageSize);
        public BindingSource customerLoadListDataSource = new BindingSource();

        public MstCustomerDetailForm(SysSoftwareForm softwareForm, MstCustomerListForm itemListForm, Entities.MstCustomerEntity itemEntity)
        {
            InitializeComponent();
            sysSoftwareForm = softwareForm;

            sysUserRights = new Modules.SysUserRightsModule("MstCustomerDetail");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                mstCustomerListForm = itemListForm;
                mstCustomerEntity = itemEntity;

                textBoxCustomerCode.Focus();
                GetTermList();
            }
            var id = mstCustomerEntity.Id;

            Controllers.MstCustomerController customerController = new Controllers.MstCustomerController();
            var detail = customerController.DetailCustomer(id);

            if (detail != null)
            {
                sysSoftwareForm.displayTimeStamp(detail.EntryUserUserName, detail.EntryDateTime + " " + detail.EntryTime, detail.UpdatedUserUserName, detail.UpdateDateTime + " " + detail.UpdateTime);
            }
        }

        public void GetTermList()
        {
            Controllers.MstCustomerController mstCustomerController = new Controllers.MstCustomerController();
            if (mstCustomerController.DropdownListCustomerTerm().Any())
            {
                comboBoxTerm.DataSource = mstCustomerController.DropdownListCustomerTerm();
                comboBoxTerm.ValueMember = "Id";
                comboBoxTerm.DisplayMember = "Term";

                GetCustomerDetail();
            }
        }

        public void GetCustomerDetail()
        {
            UpdateComponents(mstCustomerEntity.IsLocked);

            textBoxCustomerCode.Text = mstCustomerEntity.CustomerCode;
            textBoxCustomer.Text = mstCustomerEntity.Customer;
            textBoxAddress.Text = mstCustomerEntity.Address;
            textBoxContactPerson.Text = mstCustomerEntity.ContactPerson;
            textBoxContactNumber.Text = mstCustomerEntity.ContactNumber;
            textBoxCreditLimit.Text = mstCustomerEntity.CreditLimit.ToString("#,##0.00");
            comboBoxTerm.SelectedValue = mstCustomerEntity.TermId;
            textBoxTIN.Text = mstCustomerEntity.TIN;
            checkBoxWithReward.Checked = mstCustomerEntity.WithReward;
            textBoxRewardNumber.Text = mstCustomerEntity.RewardNumber;
            textBoxRewardConversion.Text = mstCustomerEntity.RewardConversion.ToString("#,##0.00");
            textBoxAvailableReward.Text = mstCustomerEntity.AvailableReward.ToString("#,##0.00");
            textBoxDefaultPrice.Text = mstCustomerEntity.DefaultPriceDescription;
            textBoxBusinessStyle.Text = mstCustomerEntity.BusinessStyle;
            textBoxLoadBalance.Text = mstCustomerEntity.LoadAmount.ToString("#,##0.00");

            CreateCustomerLoadListDataGridView();
        }

        public void UpdateComponents(Boolean isLocked)
        {
            if (sysUserRights.GetUserRights().CanLock == false)
            {
                buttonLock.Enabled = false;
            }
            else
            {
                buttonLock.Enabled = !isLocked;
            }

            if (sysUserRights.GetUserRights().CanUnlock == false)
            {
                buttonUnlock.Enabled = false;
            }
            else
            {
                buttonUnlock.Enabled = isLocked;
            }

            textBoxCustomerCode.Enabled = !isLocked;
            textBoxCustomer.Enabled = !isLocked;
            textBoxAddress.Enabled = !isLocked;
            textBoxContactPerson.Enabled = !isLocked;
            textBoxContactNumber.Enabled = !isLocked;
            textBoxCreditLimit.Enabled = !isLocked;
            comboBoxTerm.Enabled = !isLocked;
            textBoxTIN.Enabled = !isLocked;
            checkBoxWithReward.Enabled = !isLocked;
            textBoxRewardNumber.Enabled = !isLocked;
            textBoxRewardConversion.Enabled = !isLocked;
            textBoxDefaultPrice.Enabled = !isLocked;
            buttonAddLoad.Enabled = !isLocked;
            textBoxCustomerCode.Focus();
            textBoxBusinessStyle.Enabled = !isLocked;
        }

        private void buttonLock_Click(object sender, EventArgs e)
        {
            Controllers.MstCustomerController mstCustomerController = new Controllers.MstCustomerController();

            Entities.MstCustomerEntity newCustomerEntity = new Entities.MstCustomerEntity()
            {
                CustomerCode = textBoxCustomerCode.Text,
                Customer = textBoxCustomer.Text,
                Address = textBoxAddress.Text,
                ContactPerson = textBoxContactPerson.Text,
                ContactNumber = textBoxContactNumber.Text,
                CreditLimit = Convert.ToDecimal(textBoxCreditLimit.Text),
                TermId = Convert.ToInt32(comboBoxTerm.SelectedValue),
                TIN = textBoxTIN.Text,
                WithReward = checkBoxWithReward.Checked,
                RewardNumber = textBoxRewardNumber.Text,
                RewardConversion = Convert.ToDecimal(textBoxRewardConversion.Text),
                AvailableReward = Convert.ToDecimal(textBoxAvailableReward.Text),
                DefaultPriceDescription = textBoxDefaultPrice.Text,
                BusinessStyle = textBoxBusinessStyle.Text
            };

            String[] lockCustomer = mstCustomerController.LockCustomer(mstCustomerEntity.Id, newCustomerEntity);
            if (lockCustomer[1].Equals("0") == false)
            {
                UpdateComponents(true);
                mstCustomerListForm.UpdateCustomerListDataSource();
            }
            else
            {
                UpdateComponents(false);
                MessageBox.Show(lockCustomer[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUnlock_Click(object sender, EventArgs e)
        {
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Controllers.MstCustomerController mstCustomerController = new Controllers.MstCustomerController();

                String[] unlockCustomer = mstCustomerController.UnlockCustomer(mstCustomerEntity.Id);
                if (unlockCustomer[1].Equals("0") == false)
                {
                    UpdateComponents(false);
                    mstCustomerListForm.UpdateCustomerListDataSource();
                }
                else
                {
                    UpdateComponents(true);
                    MessageBox.Show(unlockCustomer[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (textBoxCustomer.Enabled == false)
            {
                sysSoftwareForm.RemoveTabPage();
            }
            else
            {
                DialogResult closeDialogResult = MessageBox.Show("Close and Lock?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (closeDialogResult == DialogResult.Yes)
                {
                    Controllers.MstCustomerController mstCustomerController = new Controllers.MstCustomerController();

                    Entities.MstCustomerEntity newCustomerEntity = new Entities.MstCustomerEntity()
                    {
                        CustomerCode = textBoxCustomerCode.Text,
                        Customer = textBoxCustomer.Text,
                        Address = textBoxAddress.Text,
                        ContactPerson = textBoxContactPerson.Text,
                        ContactNumber = textBoxContactNumber.Text,
                        CreditLimit = Convert.ToDecimal(textBoxCreditLimit.Text),
                        TermId = Convert.ToInt32(comboBoxTerm.SelectedValue),
                        TIN = textBoxTIN.Text,
                        WithReward = checkBoxWithReward.Checked,
                        RewardNumber = textBoxRewardNumber.Text,
                        RewardConversion = Convert.ToDecimal(textBoxRewardConversion.Text),
                        AvailableReward = Convert.ToDecimal(textBoxAvailableReward.Text),
                        DefaultPriceDescription = textBoxDefaultPrice.Text,
                        BusinessStyle = textBoxBusinessStyle.Text
                    };
                    sysSoftwareForm.RemoveTabPage();

                    String[] lockCustomer = mstCustomerController.LockCustomer(mstCustomerEntity.Id, newCustomerEntity);
                    if (lockCustomer[1].Equals("0") == false)
                    {
                        UpdateComponents(true);
                        mstCustomerListForm.UpdateCustomerListDataSource();
                    }
                    else
                    {
                        UpdateComponents(false);
                        MessageBox.Show(lockCustomer[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    sysSoftwareForm.RemoveTabPage();
                }
            }
        }

        private void textBoxCreditLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBoxRewardConversion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBoxAvailableReward_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }


        public void UpdateCustomerLoadListDataSource()
        {
            SetCustomerLoadListDataSourceAsync();
        }

        public async void SetCustomerLoadListDataSourceAsync()
        {
            List<Entities.DgvMstCustomerLoadListEntity> getCustomerLoadListData = await GetCustomerLoadListDataTask();
            if (getCustomerLoadListData.Any())
            {
                customerLoadListData = getCustomerLoadListData;
                customerLoadListPageList = new PagedList<Entities.DgvMstCustomerLoadListEntity>(customerLoadListData, pageNumber, pageSize);

                if (customerLoadListPageList.PageCount == 1)
                {
                    buttonCustomerLoadListPageListFirst.Enabled = false;
                    buttonCustomerLoadListPageListPrevious.Enabled = false;
                    buttonCustomerLoadListPageListNext.Enabled = false;
                    buttonCustomerLoadListPageListLast.Enabled = false;
                }
                else if (pageNumber == 1)
                {
                    buttonCustomerLoadListPageListFirst.Enabled = false;
                    buttonCustomerLoadListPageListPrevious.Enabled = false;
                    buttonCustomerLoadListPageListNext.Enabled = true;
                    buttonCustomerLoadListPageListLast.Enabled = true;
                }
                else if (pageNumber == customerLoadListPageList.PageCount)
                {
                    buttonCustomerLoadListPageListFirst.Enabled = true;
                    buttonCustomerLoadListPageListPrevious.Enabled = true;
                    buttonCustomerLoadListPageListNext.Enabled = false;
                    buttonCustomerLoadListPageListLast.Enabled = false;
                }
                else
                {
                    buttonCustomerLoadListPageListFirst.Enabled = true;
                    buttonCustomerLoadListPageListPrevious.Enabled = true;
                    buttonCustomerLoadListPageListNext.Enabled = true;
                    buttonCustomerLoadListPageListLast.Enabled = true;
                }

                textBoxCustomerLoadListPageNumber.Text = pageNumber + " / " + customerLoadListPageList.PageCount;
                customerLoadListDataSource.DataSource = customerLoadListPageList;
            }
            else
            {
                buttonCustomerLoadListPageListFirst.Enabled = false;
                buttonCustomerLoadListPageListPrevious.Enabled = false;
                buttonCustomerLoadListPageListNext.Enabled = false;
                buttonCustomerLoadListPageListLast.Enabled = false;

                pageNumber = 1;

                customerLoadListData = new List<Entities.DgvMstCustomerLoadListEntity>();
                customerLoadListDataSource.Clear();
                textBoxCustomerLoadListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvMstCustomerLoadListEntity>> GetCustomerLoadListDataTask()
        {
            Controllers.MstCustomerLoadController mstCustomerLoadController = new Controllers.MstCustomerLoadController();
            List<Entities.MstCustomerLoadEntity> listCustomerLoad = mstCustomerLoadController.ListCustomerLoad(mstCustomerEntity.Id);
            if (listCustomerLoad.Any())
            {
                var customerLoads = from d in listCustomerLoad
                                    select new Entities.DgvMstCustomerLoadListEntity
                                    {
                                        ColumnCustomerLoadId = d.Id,
                                        ColumnCustomerLoadCustomerId = d.CustomerId.ToString(),
                                        ColumnCustomerLoadCardNumber = d.CardNumber,
                                        ColumnCustomerLoadLoadDate = d.LoadDate,
                                        ColumnCustomerLoadType = d.Type,
                                        ColumnCustomerLoadAmount = d.Amount.ToString("#,##0.00"),
                                        ColumnCustomerLoadRemarks = d.Remarks
                                    };

                textBoxLoadBalance.Text = listCustomerLoad.Sum(d => d.Amount).ToString("#,##0.00");
                return Task.FromResult(customerLoads.ToList());
            }
            else
            {
                textBoxLoadBalance.Text = "0.00";
                return Task.FromResult(new List<Entities.DgvMstCustomerLoadListEntity>());
            }
        }

        public void CreateCustomerLoadListDataGridView()
        {
            UpdateCustomerLoadListDataSource();

            dataGridViewCustomerLoadList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewCustomerLoadList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewCustomerLoadList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewCustomerLoadList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewCustomerLoadList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewCustomerLoadList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewCustomerLoadList.DataSource = customerLoadListDataSource;
        }

        private void textBoxCustomerLoadListFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateCustomerLoadListDataSource();
            }
        }

        private void dataGridViewCustomerLoadList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetCustomerLoadListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewCustomerLoadList.CurrentCell.ColumnIndex == dataGridViewCustomerLoadList.Columns["ColumnCustomerLoadListButtonEdit"].Index)
            {
                Entities.MstCustomerLoadEntity selectedCustomerLoad = new Entities.MstCustomerLoadEntity()
                {
                    Id = Convert.ToInt32(dataGridViewCustomerLoadList.Rows[e.RowIndex].Cells[dataGridViewCustomerLoadList.Columns["ColumnCustomerLoadListId"].Index].Value),
                    CustomerId = mstCustomerEntity.Id,
                    CardNumber = dataGridViewCustomerLoadList.Rows[e.RowIndex].Cells[dataGridViewCustomerLoadList.Columns["ColumnCustomerLoadListPriceDescription"].Index].Value.ToString(),
                    LoadDate = dataGridViewCustomerLoadList.Rows[e.RowIndex].Cells[dataGridViewCustomerLoadList.Columns["ColumnCustomerLoadListPrice"].Index].Value.ToString(),
                    Type = dataGridViewCustomerLoadList.Rows[e.RowIndex].Cells[dataGridViewCustomerLoadList.Columns["ColumnCustomerLoadListTriggerQuantity"].Index].Value.ToString(),
                    Amount = Convert.ToDecimal(dataGridViewCustomerLoadList.Rows[e.RowIndex].Cells[dataGridViewCustomerLoadList.Columns["ColumnCustomerLoadListTriggerQuantity"].Index].Value),
                    Remarks = dataGridViewCustomerLoadList.Rows[e.RowIndex].Cells[dataGridViewCustomerLoadList.Columns["ColumnCustomerLoadListTriggerQuantity"].Index].Value.ToString(),
                };

                MstCustomerLoadDetailForm customerLoadDetailForm = new MstCustomerLoadDetailForm(this, selectedCustomerLoad);
                customerLoadDetailForm.ShowDialog();
            }

            if (e.RowIndex > -1 && dataGridViewCustomerLoadList.CurrentCell.ColumnIndex == dataGridViewCustomerLoadList.Columns["ColumnCustomerLoadListButtonDelete"].Index)
            {
                DialogResult deleteDialogResult = MessageBox.Show("Delete CustomerLoad?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (deleteDialogResult == DialogResult.Yes)
                {
                    Controllers.MstCustomerLoadController mstCustomerLoadController = new Controllers.MstCustomerLoadController();

                    String[] deleteCustomerLoad = mstCustomerLoadController.DeleteCustomerLoad(Convert.ToInt32(dataGridViewCustomerLoadList.Rows[e.RowIndex].Cells[2].Value));
                    if (deleteCustomerLoad[1].Equals("0") == false)
                    {
                        Int32 currentPageNumber = pageNumber;

                        pageNumber = 1;
                        UpdateCustomerLoadListDataSource();
                    }
                    else
                    {
                        MessageBox.Show(deleteCustomerLoad[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void GetCustomerLoadListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void buttonCustomerLoadListPageListFirst_Click(object sender, EventArgs e)
        {
            customerLoadListPageList = new PagedList<Entities.DgvMstCustomerLoadListEntity>(customerLoadListData, 1, pageSize);
            customerLoadListDataSource.DataSource = customerLoadListPageList;

            buttonCustomerLoadListPageListFirst.Enabled = false;
            buttonCustomerLoadListPageListPrevious.Enabled = false;
            buttonCustomerLoadListPageListNext.Enabled = true;
            buttonCustomerLoadListPageListLast.Enabled = true;

            pageNumber = 1;
            textBoxCustomerLoadListPageNumber.Text = pageNumber + " / " + customerLoadListPageList.PageCount;
        }

        private void buttonCustomerLoadListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (customerLoadListPageList.HasPreviousPage == true)
            {
                customerLoadListPageList = new PagedList<Entities.DgvMstCustomerLoadListEntity>(customerLoadListData, --pageNumber, pageSize);
                customerLoadListDataSource.DataSource = customerLoadListPageList;
            }

            buttonCustomerLoadListPageListNext.Enabled = true;
            buttonCustomerLoadListPageListLast.Enabled = true;

            if (pageNumber == 1)
            {
                buttonCustomerLoadListPageListFirst.Enabled = false;
                buttonCustomerLoadListPageListPrevious.Enabled = false;
            }

            textBoxCustomerLoadListPageNumber.Text = pageNumber + " / " + customerLoadListPageList.PageCount;
        }

        private void buttonCustomerLoadListPageListNext_Click(object sender, EventArgs e)
        {
            if (customerLoadListPageList.HasNextPage == true)
            {
                customerLoadListPageList = new PagedList<Entities.DgvMstCustomerLoadListEntity>(customerLoadListData, ++pageNumber, pageSize);
                customerLoadListDataSource.DataSource = customerLoadListPageList;
            }

            buttonCustomerLoadListPageListFirst.Enabled = true;
            buttonCustomerLoadListPageListPrevious.Enabled = true;

            if (pageNumber == customerLoadListPageList.PageCount)
            {
                buttonCustomerLoadListPageListNext.Enabled = false;
                buttonCustomerLoadListPageListLast.Enabled = false;
            }

            textBoxCustomerLoadListPageNumber.Text = pageNumber + " / " + customerLoadListPageList.PageCount;
        }

        private void buttonCustomerLoadListPageListLast_Click(object sender, EventArgs e)
        {
            customerLoadListPageList = new PagedList<Entities.DgvMstCustomerLoadListEntity>(customerLoadListData, customerLoadListPageList.PageCount, pageSize);
            customerLoadListDataSource.DataSource = customerLoadListPageList;

            buttonCustomerLoadListPageListFirst.Enabled = true;
            buttonCustomerLoadListPageListPrevious.Enabled = true;
            buttonCustomerLoadListPageListNext.Enabled = false;
            buttonCustomerLoadListPageListLast.Enabled = false;

            pageNumber = customerLoadListPageList.PageCount;
            textBoxCustomerLoadListPageNumber.Text = pageNumber + " / " + customerLoadListPageList.PageCount;
        }

        private void buttonAddCustomerLoad_Click(object sender, EventArgs e)
        {
            Entities.MstCustomerLoadEntity newCustomerLoad = new Entities.MstCustomerLoadEntity()
            {
                Id = 0,
                CustomerId = mstCustomerEntity.Id,
                CardNumber = "",
                LoadDate = DateTime.Today.ToShortDateString(),
                Type = "Load",
                Amount = 0,
                Remarks = ""
            };

            MstCustomerLoadDetailForm sysSystemTablesCustomerLoadDetailForm = new MstCustomerLoadDetailForm(this, newCustomerLoad);
            sysSystemTablesCustomerLoadDetailForm.ShowDialog();
        }

    }
}
