using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PagedList;

namespace EasyPOS.Forms.Software.TrnStockIn
{
    public partial class TrnStockInListForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public static List<Entities.DgvTrnStockInListEntity> itemListData = new List<Entities.DgvTrnStockInListEntity>();
        public static Int32 pageNumber = 1;
        public static Int32 pageSize = 50;
        public PagedList<Entities.DgvTrnStockInListEntity> itemListPageList = new PagedList<Entities.DgvTrnStockInListEntity>(itemListData, pageNumber, pageSize);
        public BindingSource itemListDataSource = new BindingSource();

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public TrnStockInListForm(SysSoftwareForm softwareForm)
        {
            InitializeComponent();

            label1.Text = SetLabel(label1.Text);
            buttonAdd.Text = SetLabel(buttonAdd.Text);
            buttonClose.Text = SetLabel(buttonClose.Text);
            buttonStockInListPageListFirst.Text = SetLabel(buttonStockInListPageListFirst.Text);
            buttonStockInListPageListPrevious.Text = SetLabel(buttonStockInListPageListPrevious.Text);
            buttonStockInListPageListNext.Text = SetLabel(buttonStockInListPageListNext.Text);
            buttonStockInListPageListLast.Text = SetLabel(buttonStockInListPageListLast.Text);
            dataGridViewStockInList.Columns[4].HeaderText = SetLabel(dataGridViewStockInList.Columns[4].HeaderText);
            dataGridViewStockInList.Columns[5].HeaderText = SetLabel(dataGridViewStockInList.Columns[5].HeaderText);
            dataGridViewStockInList.Columns[6].HeaderText = SetLabel(dataGridViewStockInList.Columns[6].HeaderText);
            dataGridViewStockInList.Columns[7].HeaderText = SetLabel(dataGridViewStockInList.Columns[7].HeaderText);
            dataGridViewStockInList.Columns[8].HeaderText = SetLabel(dataGridViewStockInList.Columns[8].HeaderText);

            sysSoftwareForm = softwareForm;

            String currentDate = DateTime.Today.ToShortDateString() + "\t\t";
            if (Modules.SysCurrentModule.GetCurrentSettings().IsLoginDate == true)
            {
                currentDate = Modules.SysCurrentModule.GetCurrentSettings().CurrentDate + "\t\t";
            }

            dateTimePickerStockInListFilter.Value = Convert.ToDateTime(currentDate);

            sysUserRights = new Modules.SysUserRightsModule("TrnStockIn");
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
                    dataGridViewStockInList.Columns[0].Visible = false;
                }

                if (sysUserRights.GetUserRights().CanDelete == false)
                {
                    dataGridViewStockInList.Columns[1].Visible = false;
                }

                CreateStockInListDataGridView();
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

        public void UpdateStockInListDataSource()
        {
            SetStockInListDataSourceAsync();
        }

        public async void SetStockInListDataSourceAsync()
        {
            List<Entities.DgvTrnStockInListEntity> getStockInListData = await GetStockInListDataTask();
            if (getStockInListData.Any())
            {
                itemListData = getStockInListData;
                itemListPageList = new PagedList<Entities.DgvTrnStockInListEntity>(itemListData, pageNumber, pageSize);

                if (itemListPageList.PageCount == 1)
                {
                    buttonStockInListPageListFirst.Enabled = false;
                    buttonStockInListPageListPrevious.Enabled = false;
                    buttonStockInListPageListNext.Enabled = false;
                    buttonStockInListPageListLast.Enabled = false;
                }
                else if (pageNumber == 1)
                {
                    buttonStockInListPageListFirst.Enabled = false;
                    buttonStockInListPageListPrevious.Enabled = false;
                    buttonStockInListPageListNext.Enabled = true;
                    buttonStockInListPageListLast.Enabled = true;
                }
                else if (pageNumber == itemListPageList.PageCount)
                {
                    buttonStockInListPageListFirst.Enabled = true;
                    buttonStockInListPageListPrevious.Enabled = true;
                    buttonStockInListPageListNext.Enabled = false;
                    buttonStockInListPageListLast.Enabled = false;
                }
                else
                {
                    buttonStockInListPageListFirst.Enabled = true;
                    buttonStockInListPageListPrevious.Enabled = true;
                    buttonStockInListPageListNext.Enabled = true;
                    buttonStockInListPageListLast.Enabled = true;
                }

                textBoxStockInListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
                itemListDataSource.DataSource = itemListPageList;
            }
            else
            {
                buttonStockInListPageListFirst.Enabled = false;
                buttonStockInListPageListPrevious.Enabled = false;
                buttonStockInListPageListNext.Enabled = false;
                buttonStockInListPageListLast.Enabled = false;

                pageNumber = 1;

                itemListData = new List<Entities.DgvTrnStockInListEntity>();
                itemListDataSource.Clear();
                textBoxStockInListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvTrnStockInListEntity>> GetStockInListDataTask()
        {
            DateTime dateFilter = dateTimePickerStockInListFilter.Value.Date;
            String filter = textBoxStockInListFilter.Text;
            Controllers.TrnStockInController trnStockInController = new Controllers.TrnStockInController();

            List<Entities.TrnStockInEntity> listStockIn = trnStockInController.ListStockIn(dateFilter, filter);
            if (listStockIn.Any())
            {
                var items = from d in listStockIn
                            select new Entities.DgvTrnStockInListEntity
                            {
                                ColumnStockInListButtonEdit = SetLabel("Edit"),
                                ColumnStockInListButtonDelete = SetLabel("Delete"),
                                ColumnStockInListId = d.Id,
                                ColumnStockInListStockInDate = d.StockInDate,
                                ColumnStockInListStockInNumber = d.StockInNumber,
                                ColumnStockInListManualStockInNumber = d.ManualStockInNumber,
                                ColumnStockInListSupplier = d.Supplier,
                                ColumnStockInListRemarks = d.Remarks,
                                ColumnStockInListIsLocked = d.IsLocked
                            };

                return Task.FromResult(items.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvTrnStockInListEntity>());
            }
        }

        public void CreateStockInListDataGridView()
        {
            UpdateStockInListDataSource();

            dataGridViewStockInList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewStockInList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewStockInList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewStockInList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewStockInList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewStockInList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewStockInList.DataSource = itemListDataSource;
        }

        public void GetStockInListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Controllers.TrnStockInController trnStockInController = new Controllers.TrnStockInController();
            String[] addStockIn = trnStockInController.AddStockIn();
            if (addStockIn[1].Equals("0") == false)
            {
                sysSoftwareForm.AddTabPageStockInDetail(this, trnStockInController.DetailStockIn(Convert.ToInt32(addStockIn[1])));
                UpdateStockInListDataSource();
            }
            else
            {
                MessageBox.Show(addStockIn[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
            sysSoftwareForm.RemoveTabPage();
        }

        private void dataGridViewStockInList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetStockInListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewStockInList.CurrentCell.ColumnIndex == dataGridViewStockInList.Columns["ColumnStockInListButtonEdit"].Index)
            {
                Controllers.TrnStockInController trnStockInController = new Controllers.TrnStockInController();
                sysSoftwareForm.AddTabPageStockInDetail(this, trnStockInController.DetailStockIn(Convert.ToInt32(dataGridViewStockInList.Rows[e.RowIndex].Cells[2].Value)));
            }

            if (e.RowIndex > -1 && dataGridViewStockInList.CurrentCell.ColumnIndex == dataGridViewStockInList.Columns["ColumnStockInListButtonDelete"].Index)
            {
                Boolean isLocked = Convert.ToBoolean(dataGridViewStockInList.Rows[e.RowIndex].Cells[8].Value);

                if (isLocked == true)
                {
                    MessageBox.Show("Already locked.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult deleteDialogResult = MessageBox.Show("Delete Stock-In?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (deleteDialogResult == DialogResult.Yes)
                    {
                        Controllers.TrnStockInController trnStockInController = new Controllers.TrnStockInController();

                        String[] deleteStockIn = trnStockInController.DeleteStockIn(Convert.ToInt32(dataGridViewStockInList.Rows[e.RowIndex].Cells[2].Value));
                        if (deleteStockIn[1].Equals("0") == false)
                        {
                            pageNumber = 1;
                            UpdateStockInListDataSource();
                        }
                        else
                        {
                            MessageBox.Show(deleteStockIn[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void dateTimePickerStockInListFilter_ValueChanged(object sender, EventArgs e)
        {
            UpdateStockInListDataSource();
        }

        private void textBoxStockInListFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateStockInListDataSource();
            }
        }

        private void buttonStockInListPageListFirst_Click(object sender, EventArgs e)
        {
            itemListPageList = new PagedList<Entities.DgvTrnStockInListEntity>(itemListData, 1, pageSize);
            itemListDataSource.DataSource = itemListPageList;

            buttonStockInListPageListFirst.Enabled = false;
            buttonStockInListPageListPrevious.Enabled = false;
            buttonStockInListPageListNext.Enabled = true;
            buttonStockInListPageListLast.Enabled = true;

            pageNumber = 1;
            textBoxStockInListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonStockInListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (itemListPageList.HasPreviousPage == true)
            {
                itemListPageList = new PagedList<Entities.DgvTrnStockInListEntity>(itemListData, --pageNumber, pageSize);
                itemListDataSource.DataSource = itemListPageList;
            }

            buttonStockInListPageListNext.Enabled = true;
            buttonStockInListPageListLast.Enabled = true;

            if (pageNumber == 1)
            {
                buttonStockInListPageListFirst.Enabled = false;
                buttonStockInListPageListPrevious.Enabled = false;
            }

            textBoxStockInListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonStockInListPageListNext_Click(object sender, EventArgs e)
        {
            if (itemListPageList.HasNextPage == true)
            {
                itemListPageList = new PagedList<Entities.DgvTrnStockInListEntity>(itemListData, ++pageNumber, pageSize);
                itemListDataSource.DataSource = itemListPageList;
            }

            buttonStockInListPageListFirst.Enabled = true;
            buttonStockInListPageListPrevious.Enabled = true;

            if (pageNumber == itemListPageList.PageCount)
            {
                buttonStockInListPageListNext.Enabled = false;
                buttonStockInListPageListLast.Enabled = false;
            }

            textBoxStockInListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonStockInListPageListLast_Click(object sender, EventArgs e)
        {
            itemListPageList = new PagedList<Entities.DgvTrnStockInListEntity>(itemListData, itemListPageList.PageCount, pageSize);
            itemListDataSource.DataSource = itemListPageList;

            buttonStockInListPageListFirst.Enabled = true;
            buttonStockInListPageListPrevious.Enabled = true;
            buttonStockInListPageListNext.Enabled = false;
            buttonStockInListPageListLast.Enabled = false;

            pageNumber = itemListPageList.PageCount;
            textBoxStockInListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }
    }
}
