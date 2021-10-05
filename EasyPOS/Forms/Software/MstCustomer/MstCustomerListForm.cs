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

namespace EasyPOS.Forms.Software.MstCustomer
{
    public partial class MstCustomerListForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public static List<Entities.DgvMstCustomerListEntity> itemListData = new List<Entities.DgvMstCustomerListEntity>();
        public static Int32 pageNumber = 1;
        public static Int32 pageSize = 50;
        public PagedList<Entities.DgvMstCustomerListEntity> itemListPageList = new PagedList<Entities.DgvMstCustomerListEntity>(itemListData, pageNumber, pageSize);
        public BindingSource itemListDataSource = new BindingSource();

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public MstCustomerListForm(SysSoftwareForm softwareForm)
        {
            InitializeComponent();

            buttonAdd.Text = SetLabel(buttonAdd.Text);
            buttonClose.Text = SetLabel(buttonClose.Text);
            label1.Text = SetLabel(label1.Text);
            dataGridViewCustomerList.Columns[3].HeaderText = SetLabel(dataGridViewCustomerList.Columns[3].HeaderText);
            dataGridViewCustomerList.Columns[4].HeaderText = SetLabel(dataGridViewCustomerList.Columns[4].HeaderText);
            dataGridViewCustomerList.Columns[5].HeaderText = SetLabel(dataGridViewCustomerList.Columns[5].HeaderText);
            dataGridViewCustomerList.Columns[6].HeaderText = SetLabel(dataGridViewCustomerList.Columns[6].HeaderText);
            dataGridViewCustomerList.Columns[7].HeaderText = SetLabel(dataGridViewCustomerList.Columns[7].HeaderText);
            buttonCustomerListPageListFirst.Text = SetLabel(buttonCustomerListPageListFirst.Text);
            buttonCustomerListPageListLast.Text = SetLabel(buttonCustomerListPageListFirst.Text);
            buttonCustomerListPageListNext.Text = SetLabel(buttonCustomerListPageListFirst.Text);
            buttonCustomerListPageListPrevious.Text = SetLabel(buttonCustomerListPageListFirst.Text);

            sysSoftwareForm = softwareForm;

            sysUserRights = new Modules.SysUserRightsModule("MstCustomer");
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
                    dataGridViewCustomerList.Columns[0].Visible = false;
                }

                if (sysUserRights.GetUserRights().CanDelete == false)
                {
                    dataGridViewCustomerList.Columns[1].Visible = false;
                }

                CreateCustomerListDataGridView();
            }
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

        public void UpdateCustomerListDataSource()
        {
            SetCustomerListDataSourceAsync();
        }

        public async void SetCustomerListDataSourceAsync()
        {
            List<Entities.DgvMstCustomerListEntity> getCustomerListData = await GetCustomerListDataTask();
            if (getCustomerListData.Any())
            {
                itemListData = getCustomerListData;
                itemListPageList = new PagedList<Entities.DgvMstCustomerListEntity>(itemListData, pageNumber, pageSize);

                if (itemListPageList.PageCount == 1)
                {
                    buttonCustomerListPageListFirst.Enabled = false;
                    buttonCustomerListPageListPrevious.Enabled = false;
                    buttonCustomerListPageListNext.Enabled = false;
                    buttonCustomerListPageListLast.Enabled = false;
                }
                else if (pageNumber == 1)
                {
                    buttonCustomerListPageListFirst.Enabled = false;
                    buttonCustomerListPageListPrevious.Enabled = false;
                    buttonCustomerListPageListNext.Enabled = true;
                    buttonCustomerListPageListLast.Enabled = true;
                }
                else if (pageNumber == itemListPageList.PageCount)
                {
                    buttonCustomerListPageListFirst.Enabled = true;
                    buttonCustomerListPageListPrevious.Enabled = true;
                    buttonCustomerListPageListNext.Enabled = false;
                    buttonCustomerListPageListLast.Enabled = false;
                }
                else
                {
                    buttonCustomerListPageListFirst.Enabled = true;
                    buttonCustomerListPageListPrevious.Enabled = true;
                    buttonCustomerListPageListNext.Enabled = true;
                    buttonCustomerListPageListLast.Enabled = true;
                }

                textBoxCustomerListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
                itemListDataSource.DataSource = itemListPageList;
            }
            else
            {
                buttonCustomerListPageListFirst.Enabled = false;
                buttonCustomerListPageListPrevious.Enabled = false;
                buttonCustomerListPageListNext.Enabled = false;
                buttonCustomerListPageListLast.Enabled = false;

                pageNumber = 1;

                itemListData = new List<Entities.DgvMstCustomerListEntity>();
                itemListDataSource.Clear();
                textBoxCustomerListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvMstCustomerListEntity>> GetCustomerListDataTask()
        {
            String filter = textBoxCustomerListFilter.Text;
            Controllers.MstCustomerController mstCustomerController = new Controllers.MstCustomerController();

            List<Entities.MstCustomerEntity> listCustomer = mstCustomerController.ListCustomer(filter);
            if (listCustomer.Any())
            {
                var items = from d in listCustomer
                            select new Entities.DgvMstCustomerListEntity
                            {
                                ColumnCustomerListButtonEdit = SetLabel("Edit"),
                                ColumnCustomerListButtonDelete = SetLabel("Delete"),
                                ColumnCustomerListId = d.Id,
                                ColumnCustomerListCustomerCode = d.CustomerCode,
                                ColumnCustomerListCustomer = d.Customer,
                                ColumnCustomerListContactNumber = d.ContactNumber,
                                ColumnCustomerListAddress = d.Address,
                                ColumnCustomerListIsLocked = d.IsLocked
                            };

                return Task.FromResult(items.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvMstCustomerListEntity>());
            }
        }

        public void CreateCustomerListDataGridView()
        {
            UpdateCustomerListDataSource();

            dataGridViewCustomerList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewCustomerList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewCustomerList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewCustomerList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewCustomerList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewCustomerList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewCustomerList.DataSource = itemListDataSource;
        }

        public void GetCustomerListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
           
                Controllers.MstCustomerController mstCustomerController = new Controllers.MstCustomerController();
                String[] addCustomer = mstCustomerController.AddCustomer();
                if (addCustomer[1].Equals("0") == false)
                {
                    sysSoftwareForm.AddTabPageCustomerDetail(this, mstCustomerController.DetailCustomer(Convert.ToInt32(addCustomer[1])));
                    UpdateCustomerListDataSource();
                }
                else
                {
                    MessageBox.Show(addCustomer[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
            sysSoftwareForm.RemoveTabPage();
        }

        private void buttonCustomerListPageListFirst_Click(object sender, EventArgs e)
        {
            itemListPageList = new PagedList<Entities.DgvMstCustomerListEntity>(itemListData, 1, pageSize);
            itemListDataSource.DataSource = itemListPageList;

            buttonCustomerListPageListFirst.Enabled = false;
            buttonCustomerListPageListPrevious.Enabled = false;
            buttonCustomerListPageListNext.Enabled = true;
            buttonCustomerListPageListLast.Enabled = true;

            pageNumber = 1;
            textBoxCustomerListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonCustomerListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (itemListPageList.HasPreviousPage == true)
            {
                itemListPageList = new PagedList<Entities.DgvMstCustomerListEntity>(itemListData, --pageNumber, pageSize);
                itemListDataSource.DataSource = itemListPageList;
            }

            buttonCustomerListPageListNext.Enabled = true;
            buttonCustomerListPageListLast.Enabled = true;

            if (pageNumber == 1)
            {
                buttonCustomerListPageListFirst.Enabled = false;
                buttonCustomerListPageListPrevious.Enabled = false;
            }

            textBoxCustomerListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonCustomerListPageListNext_Click(object sender, EventArgs e)
        {
            if (itemListPageList.HasNextPage == true)
            {
                itemListPageList = new PagedList<Entities.DgvMstCustomerListEntity>(itemListData, ++pageNumber, pageSize);
                itemListDataSource.DataSource = itemListPageList;
            }

            buttonCustomerListPageListFirst.Enabled = true;
            buttonCustomerListPageListPrevious.Enabled = true;

            if (pageNumber == itemListPageList.PageCount)
            {
                buttonCustomerListPageListNext.Enabled = false;
                buttonCustomerListPageListLast.Enabled = false;
            }

            textBoxCustomerListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonCustomerListPageListLast_Click(object sender, EventArgs e)
        {
            itemListPageList = new PagedList<Entities.DgvMstCustomerListEntity>(itemListData, itemListPageList.PageCount, pageSize);
            itemListDataSource.DataSource = itemListPageList;

            buttonCustomerListPageListFirst.Enabled = true;
            buttonCustomerListPageListPrevious.Enabled = true;
            buttonCustomerListPageListNext.Enabled = false;
            buttonCustomerListPageListLast.Enabled = false;

            pageNumber = itemListPageList.PageCount;
            textBoxCustomerListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void dataGridViewCustomerList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetCustomerListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewCustomerList.CurrentCell.ColumnIndex == dataGridViewCustomerList.Columns["ColumnCustomerListButtonEdit"].Index)
            {
                Controllers.MstCustomerController mstCustomerController = new Controllers.MstCustomerController();
                sysSoftwareForm.AddTabPageCustomerDetail(this, mstCustomerController.DetailCustomer(Convert.ToInt32(dataGridViewCustomerList.Rows[e.RowIndex].Cells[2].Value)));
            }

            if (e.RowIndex > -1 && dataGridViewCustomerList.CurrentCell.ColumnIndex == dataGridViewCustomerList.Columns["ColumnCustomerListButtonDelete"].Index)
            {
                Boolean isLocked = Convert.ToBoolean(dataGridViewCustomerList.Rows[e.RowIndex].Cells[7].Value);

                if (isLocked == true)
                {
                    MessageBox.Show("Already locked.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult deleteDialogResult = MessageBox.Show("Delete Customer?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (deleteDialogResult == DialogResult.Yes)
                    {
                        Controllers.MstCustomerController mstCustomerController = new Controllers.MstCustomerController();

                        String[] deleteCustomer = mstCustomerController.DeleteCustomer(Convert.ToInt32(dataGridViewCustomerList.Rows[e.RowIndex].Cells[2].Value));
                        if (deleteCustomer[1].Equals("0") == false)
                        {
                            pageNumber = 1;
                            UpdateCustomerListDataSource();
                        }
                        else
                        {
                            MessageBox.Show(deleteCustomer[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void textBoxCustomerListFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateCustomerListDataSource();
            }
        }
    }
}
