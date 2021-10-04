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

namespace EasyPOS.Forms.Software.TrnDisbursement
{
    public partial class TrnDisbursementListForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public static List<Entities.DgvTrnDisbursementListEntity> itemListData = new List<Entities.DgvTrnDisbursementListEntity>();
        public static Int32 pageNumber = 1;
        public static Int32 pageSize = 50;
        public PagedList<Entities.DgvTrnDisbursementListEntity> itemListPageList = new PagedList<Entities.DgvTrnDisbursementListEntity>(itemListData, pageNumber, pageSize);
        public BindingSource itemListDataSource = new BindingSource();

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();


        public TrnDisbursementListForm(SysSoftwareForm softwareForm)
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
                    buttonRefund.Text = SetLabel(buttonRefund.Text);
                    label1.Text = SetLabel(label1.Text);
                    dataGridViewDisbursementList.Columns[4].HeaderText = SetLabel(dataGridViewDisbursementList.Columns[4].HeaderText);
                    dataGridViewDisbursementList.Columns[5].HeaderText = SetLabel(dataGridViewDisbursementList.Columns[5].HeaderText);
                    dataGridViewDisbursementList.Columns[6].HeaderText = SetLabel(dataGridViewDisbursementList.Columns[6].HeaderText);
                    dataGridViewDisbursementList.Columns[7].HeaderText = SetLabel(dataGridViewDisbursementList.Columns[7].HeaderText);
                    dataGridViewDisbursementList.Columns[8].HeaderText = SetLabel(dataGridViewDisbursementList.Columns[8].HeaderText);
                    dataGridViewDisbursementList.Columns[9].HeaderText = SetLabel(dataGridViewDisbursementList.Columns[9].HeaderText);
                    dataGridViewDisbursementList.Columns[10].HeaderText = SetLabel(dataGridViewDisbursementList.Columns[10].HeaderText);
                    buttonDisbursementListPageListFirst.Text = SetLabel(buttonDisbursementListPageListFirst.Text);
                    buttonDisbursementListPageListLast.Text = SetLabel(buttonDisbursementListPageListLast.Text);
                    buttonDisbursementListPageListNext.Text = SetLabel(buttonDisbursementListPageListNext.Text);
                    buttonDisbursementListPageListPrevious.Text = SetLabel(buttonDisbursementListPageListPrevious.Text);
                }
            }

            sysSoftwareForm = softwareForm;

            String currentDate = DateTime.Today.ToShortDateString() + "\t\t";
            if (Modules.SysCurrentModule.GetCurrentSettings().IsLoginDate == true)
            {
                currentDate = Modules.SysCurrentModule.GetCurrentSettings().CurrentDate + "\t\t";
            }

            dateTimePickerDisbursementListFilter.Value = Convert.ToDateTime(currentDate);

            sysUserRights = new Modules.SysUserRightsModule("TrnDisbursement");
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
                    dataGridViewDisbursementList.Columns[0].Visible = false;

                }

                if (sysUserRights.GetUserRights().CanDelete == false)
                {
                    dataGridViewDisbursementList.Columns[1].Visible = false;

                }
                CreateDisbursementListDataGridView();
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

        public void UpdateDisbursementListDataSource()
        {
            SetDisbursementListDataSourceAsync();
        }

        public async void SetDisbursementListDataSourceAsync()
        {
            List<Entities.DgvTrnDisbursementListEntity> getDisbursementListData = await GetDisbursementListDataTask();
            if (getDisbursementListData.Any())
            {
                itemListData = getDisbursementListData;
                itemListPageList = new PagedList<Entities.DgvTrnDisbursementListEntity>(itemListData, pageNumber, pageSize);

                if (itemListPageList.PageCount == 1)
                {
                    buttonDisbursementListPageListFirst.Enabled = false;
                    buttonDisbursementListPageListPrevious.Enabled = false;
                    buttonDisbursementListPageListNext.Enabled = false;
                    buttonDisbursementListPageListLast.Enabled = false;
                }
                else if (pageNumber == 1)
                {
                    buttonDisbursementListPageListFirst.Enabled = false;
                    buttonDisbursementListPageListPrevious.Enabled = false;
                    buttonDisbursementListPageListNext.Enabled = true;
                    buttonDisbursementListPageListLast.Enabled = true;
                }
                else if (pageNumber == itemListPageList.PageCount)
                {
                    buttonDisbursementListPageListFirst.Enabled = true;
                    buttonDisbursementListPageListPrevious.Enabled = true;
                    buttonDisbursementListPageListNext.Enabled = false;
                    buttonDisbursementListPageListLast.Enabled = false;
                }
                else
                {
                    buttonDisbursementListPageListFirst.Enabled = true;
                    buttonDisbursementListPageListPrevious.Enabled = true;
                    buttonDisbursementListPageListNext.Enabled = true;
                    buttonDisbursementListPageListLast.Enabled = true;
                }

                textBoxDisbursementListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
                itemListDataSource.DataSource = itemListPageList;
            }
            else
            {
                buttonDisbursementListPageListFirst.Enabled = false;
                buttonDisbursementListPageListPrevious.Enabled = false;
                buttonDisbursementListPageListNext.Enabled = false;
                buttonDisbursementListPageListLast.Enabled = false;

                pageNumber = 1;

                itemListData = new List<Entities.DgvTrnDisbursementListEntity>();
                itemListDataSource.Clear();
                textBoxDisbursementListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvTrnDisbursementListEntity>> GetDisbursementListDataTask()
        {
            DateTime dateFilter = dateTimePickerDisbursementListFilter.Value.Date;
            String filter = textBoxDisbursementListFilter.Text;
            Controllers.TrnDisbursementController trnDisbursementController = new Controllers.TrnDisbursementController();

            List<Entities.TrnDisbursementEntity> listDisbursement = trnDisbursementController.ListDisbursement(dateFilter, filter);
            if (listDisbursement.Any())
            {
                var items = from d in listDisbursement
                            select new Entities.DgvTrnDisbursementListEntity
                            {
                                ColumnDisbursementListButtonEdit = "Edit",
                                ColumnDisbursementListButtonDelete = "Delete",
                                ColumnDisbursementListId = d.Id,
                                ColumnDisbursementListDisbursementDate = d.DisbursementDate,
                                ColumnDisbursementListDisbursementNumber = d.DisbursementNumber,
                                ColumnDisbursementListDisbursementType = d.DisbursementType,
                                ColumnDisbursementListPayType = d.PayType,
                                ColumnDisbursementListPayee = d.Payee,
                                ColumnDisbursementListAmount = d.Amount.ToString("#,##0.00"),
                                ColumnDisbursementListRemarks = d.Remarks,
                                ColumnDisbursementListIsLocked = d.IsLocked
                            };

                return Task.FromResult(items.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvTrnDisbursementListEntity>());
            }
        }

        public void CreateDisbursementListDataGridView()
        {
            UpdateDisbursementListDataSource();

            dataGridViewDisbursementList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewDisbursementList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewDisbursementList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewDisbursementList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewDisbursementList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewDisbursementList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewDisbursementList.DataSource = itemListDataSource;
        }

        public void GetDisbursementListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Controllers.TrnDisbursementController trnDisbursementController = new Controllers.TrnDisbursementController();
            String[] addDisbursement = trnDisbursementController.AddDisbursement();
            if (addDisbursement[1].Equals("0") == false)
            {
                sysSoftwareForm.AddTabPageDisbursementDetail(this, trnDisbursementController.DetailDisbursement(Convert.ToInt32(addDisbursement[1])));
                UpdateDisbursementListDataSource();
            }
            else
            {
                MessageBox.Show(addDisbursement[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
            sysSoftwareForm.RemoveTabPage();
        }

        private void dateTimePickerDisbursementListFilter_ValueChanged(object sender, EventArgs e)
        {
            UpdateDisbursementListDataSource();
        }

        private void textBoxDisbursementListFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateDisbursementListDataSource();
            }
        }

        private void dataGridViewDisbursementList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetDisbursementListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewDisbursementList.CurrentCell.ColumnIndex == dataGridViewDisbursementList.Columns["ColumnDisbursementListButtonEdit"].Index)
            {
                Controllers.TrnDisbursementController trnDisbursementController = new Controllers.TrnDisbursementController();
                sysSoftwareForm.AddTabPageDisbursementDetail(this, trnDisbursementController.DetailDisbursement(Convert.ToInt32(dataGridViewDisbursementList.Rows[e.RowIndex].Cells[2].Value)));
            }

            if (e.RowIndex > -1 && dataGridViewDisbursementList.CurrentCell.ColumnIndex == dataGridViewDisbursementList.Columns["ColumnDisbursementListButtonDelete"].Index)
            {
                Boolean isLocked = Convert.ToBoolean(dataGridViewDisbursementList.Rows[e.RowIndex].Cells[dataGridViewDisbursementList.Columns["ColumnDisbursementListIsLocked"].Index].Value);

                if (isLocked == true)
                {
                    MessageBox.Show("Already locked.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult deleteDialogResult = MessageBox.Show("Delete Disbursement?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (deleteDialogResult == DialogResult.Yes)
                    {
                        Controllers.TrnDisbursementController trnDisbursementController = new Controllers.TrnDisbursementController();

                        String[] deleteDisbursement = trnDisbursementController.DeleteDisbursement(Convert.ToInt32(dataGridViewDisbursementList.Rows[e.RowIndex].Cells[2].Value));
                        if (deleteDisbursement[1].Equals("0") == false)
                        {
                            pageNumber = 1;
                            UpdateDisbursementListDataSource();
                        }
                        else
                        {
                            MessageBox.Show(deleteDisbursement[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void buttonDisbursementListPageListFirst_Click(object sender, EventArgs e)
        {
            itemListPageList = new PagedList<Entities.DgvTrnDisbursementListEntity>(itemListData, 1, pageSize);
            itemListDataSource.DataSource = itemListPageList;

            buttonDisbursementListPageListFirst.Enabled = false;
            buttonDisbursementListPageListPrevious.Enabled = false;
            buttonDisbursementListPageListNext.Enabled = true;
            buttonDisbursementListPageListLast.Enabled = true;

            pageNumber = 1;
            textBoxDisbursementListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonDisbursementListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (itemListPageList.HasPreviousPage == true)
            {
                itemListPageList = new PagedList<Entities.DgvTrnDisbursementListEntity>(itemListData, --pageNumber, pageSize);
                itemListDataSource.DataSource = itemListPageList;
            }

            buttonDisbursementListPageListNext.Enabled = true;
            buttonDisbursementListPageListLast.Enabled = true;

            if (pageNumber == 1)
            {
                buttonDisbursementListPageListFirst.Enabled = false;
                buttonDisbursementListPageListPrevious.Enabled = false;
            }

            textBoxDisbursementListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonDisbursementListPageListNext_Click(object sender, EventArgs e)
        {
            if (itemListPageList.HasNextPage == true)
            {
                itemListPageList = new PagedList<Entities.DgvTrnDisbursementListEntity>(itemListData, ++pageNumber, pageSize);
                itemListDataSource.DataSource = itemListPageList;
            }

            buttonDisbursementListPageListFirst.Enabled = true;
            buttonDisbursementListPageListPrevious.Enabled = true;

            if (pageNumber == itemListPageList.PageCount)
            {
                buttonDisbursementListPageListNext.Enabled = false;
                buttonDisbursementListPageListLast.Enabled = false;
            }

            textBoxDisbursementListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonDisbursementListPageListLast_Click(object sender, EventArgs e)
        {
            itemListPageList = new PagedList<Entities.DgvTrnDisbursementListEntity>(itemListData, itemListPageList.PageCount, pageSize);
            itemListDataSource.DataSource = itemListPageList;

            buttonDisbursementListPageListFirst.Enabled = true;
            buttonDisbursementListPageListPrevious.Enabled = true;
            buttonDisbursementListPageListNext.Enabled = false;
            buttonDisbursementListPageListLast.Enabled = false;

            pageNumber = itemListPageList.PageCount;
            textBoxDisbursementListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonRefund_Click(object sender, EventArgs e)
        {
            TrnRefundForm trnRefundForm = new TrnRefundForm(this);
            trnRefundForm.ShowDialog();
        }
    }
}
