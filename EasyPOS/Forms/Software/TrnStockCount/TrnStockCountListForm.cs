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

namespace EasyPOS.Forms.Software.TrnStockCount
{
    public partial class TrnStockCountListForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public static List<Entities.DgvTrnStockCountListEntity> itemListData = new List<Entities.DgvTrnStockCountListEntity>();
        public static Int32 pageNumber = 1;
        public static Int32 pageSize = 50;
        public PagedList<Entities.DgvTrnStockCountListEntity> itemListPageList = new PagedList<Entities.DgvTrnStockCountListEntity>(itemListData, pageNumber, pageSize);
        public BindingSource itemListDataSource = new BindingSource();

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public TrnStockCountListForm(SysSoftwareForm softwareForm)
        {
            InitializeComponent();

            label1.Text = SetLabel(label1.Text);
            buttonAdd.Text = SetLabel(buttonAdd.Text);
            buttonClose.Text = SetLabel(buttonClose.Text);
            buttonStockCountListPageListFirst.Text = SetLabel(buttonStockCountListPageListFirst.Text);
            buttonStockCountListPageListPrevious.Text = SetLabel(buttonStockCountListPageListPrevious.Text);
            buttonStockCountListPageListNext.Text = SetLabel(buttonStockCountListPageListNext.Text);
            buttonStockCountListPageListLast.Text = SetLabel(buttonStockCountListPageListLast.Text);
            dataGridViewStockCountList.Columns[4].HeaderText = SetLabel(dataGridViewStockCountList.Columns[4].HeaderText);
            dataGridViewStockCountList.Columns[5].HeaderText = SetLabel(dataGridViewStockCountList.Columns[5].HeaderText);
            dataGridViewStockCountList.Columns[6].HeaderText = SetLabel(dataGridViewStockCountList.Columns[6].HeaderText);

            sysSoftwareForm = softwareForm;

            String currentDate = DateTime.Today.ToShortDateString() + "\t\t";
            if (Modules.SysCurrentModule.GetCurrentSettings().IsLoginDate == true)
            {
                currentDate = Modules.SysCurrentModule.GetCurrentSettings().CurrentDate + "\t\t";
            }

            dateTimePickerStockCountListFilter.Value = Convert.ToDateTime(currentDate);

            sysUserRights = new Modules.SysUserRightsModule("TrnStockCount");
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
                    dataGridViewStockCountList.Columns[0].Visible = false;
                }

                if (sysUserRights.GetUserRights().CanDelete == false)
                {
                    dataGridViewStockCountList.Columns[1].Visible = false;
                }

                CreateStockCountListDataGridView();
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

        public void UpdateStockCountListDataSource()
        {
            SetStockCountListDataSourceAsync();
        }

        public async void SetStockCountListDataSourceAsync()
        {
            List<Entities.DgvTrnStockCountListEntity> getStockCountListData = await GetStockCountListDataTask();
            if (getStockCountListData.Any())
            {
                itemListData = getStockCountListData;
                itemListPageList = new PagedList<Entities.DgvTrnStockCountListEntity>(itemListData, pageNumber, pageSize);

                if (itemListPageList.PageCount == 1)
                {
                    buttonStockCountListPageListFirst.Enabled = false;
                    buttonStockCountListPageListPrevious.Enabled = false;
                    buttonStockCountListPageListNext.Enabled = false;
                    buttonStockCountListPageListLast.Enabled = false;
                }
                else if (pageNumber == 1)
                {
                    buttonStockCountListPageListFirst.Enabled = false;
                    buttonStockCountListPageListPrevious.Enabled = false;
                    buttonStockCountListPageListNext.Enabled = true;
                    buttonStockCountListPageListLast.Enabled = true;
                }
                else if (pageNumber == itemListPageList.PageCount)
                {
                    buttonStockCountListPageListFirst.Enabled = true;
                    buttonStockCountListPageListPrevious.Enabled = true;
                    buttonStockCountListPageListNext.Enabled = false;
                    buttonStockCountListPageListLast.Enabled = false;
                }
                else
                {
                    buttonStockCountListPageListFirst.Enabled = true;
                    buttonStockCountListPageListPrevious.Enabled = true;
                    buttonStockCountListPageListNext.Enabled = true;
                    buttonStockCountListPageListLast.Enabled = true;
                }

                textBoxStockCountListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
                itemListDataSource.DataSource = itemListPageList;
            }
            else
            {
                buttonStockCountListPageListFirst.Enabled = false;
                buttonStockCountListPageListPrevious.Enabled = false;
                buttonStockCountListPageListNext.Enabled = false;
                buttonStockCountListPageListLast.Enabled = false;

                pageNumber = 1;

                itemListData = new List<Entities.DgvTrnStockCountListEntity>();
                itemListDataSource.Clear();
                textBoxStockCountListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvTrnStockCountListEntity>> GetStockCountListDataTask()
        {
            DateTime dateFilter = dateTimePickerStockCountListFilter.Value.Date;
            String filter = textBoxStockCountListFilter.Text;
            Controllers.TrnStockCountController trnStockCountController = new Controllers.TrnStockCountController();

            List<Entities.TrnStockCountEntity> listStockCount = trnStockCountController.ListStockCount(dateFilter, filter);
            if (listStockCount.Any())
            {
                var items = from d in listStockCount
                            select new Entities.DgvTrnStockCountListEntity
                            {
                                ColumnStockCountListButtonEdit = SetLabel("Edit"),
                                ColumnStockCountListButtonDelete = SetLabel("Delete"),
                                ColumnStockCountListId = d.Id,
                                ColumnStockCountListStockCountDate = d.StockCountDate,
                                ColumnStockCountListStockCountNumber = d.StockCountNumber,
                                ColumnStockCountListRemarks = d.Remarks,
                                ColumnStockCountListIsLocked = d.IsLocked
                            };

                return Task.FromResult(items.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvTrnStockCountListEntity>());
            }
        }

        public void CreateStockCountListDataGridView()
        {
            UpdateStockCountListDataSource();

            dataGridViewStockCountList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewStockCountList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewStockCountList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewStockCountList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewStockCountList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewStockCountList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewStockCountList.DataSource = itemListDataSource;
        }

        public void GetStockCountListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Controllers.TrnStockCountController trnStockCountController = new Controllers.TrnStockCountController();
            String[] addStockCount = trnStockCountController.AddStockCount();
            if (addStockCount[1].Equals("0") == false)
            {
                sysSoftwareForm.AddTabPageStockCountDetail(this, trnStockCountController.DetailStockCount(Convert.ToInt32(addStockCount[1])));
                UpdateStockCountListDataSource();
            }
            else
            {
                MessageBox.Show(addStockCount[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
            sysSoftwareForm.RemoveTabPage();
        }

        private void dateTimePickerStockCountListFilter_ValueChanged(object sender, EventArgs e)
        {
            UpdateStockCountListDataSource();
        }

        private void textBoxStockCountListFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateStockCountListDataSource();
            }
        }

        private void dataGridViewStockCountList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetStockCountListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewStockCountList.CurrentCell.ColumnIndex == dataGridViewStockCountList.Columns["ColumnStockCountListButtonEdit"].Index)
            {
                Controllers.TrnStockCountController trnStockCountController = new Controllers.TrnStockCountController();
                sysSoftwareForm.AddTabPageStockCountDetail(this, trnStockCountController.DetailStockCount(Convert.ToInt32(dataGridViewStockCountList.Rows[e.RowIndex].Cells[2].Value)));
            }

            if (e.RowIndex > -1 && dataGridViewStockCountList.CurrentCell.ColumnIndex == dataGridViewStockCountList.Columns["ColumnStockCountListButtonDelete"].Index)
            {
                Boolean isLocked = Convert.ToBoolean(dataGridViewStockCountList.Rows[e.RowIndex].Cells[6].Value);

                if (isLocked == true)
                {
                    MessageBox.Show("Already locked.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult deleteDialogResult = MessageBox.Show("Delete Stock-Out?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (deleteDialogResult == DialogResult.Yes)
                    {
                        Controllers.TrnStockCountController trnStockCountController = new Controllers.TrnStockCountController();

                        String[] deleteStockCount = trnStockCountController.DeleteStockCount(Convert.ToInt32(dataGridViewStockCountList.Rows[e.RowIndex].Cells[2].Value));
                        if (deleteStockCount[1].Equals("0") == false)
                        {
                            pageNumber = 1;
                            UpdateStockCountListDataSource();
                        }
                        else
                        {
                            MessageBox.Show(deleteStockCount[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void buttonStockCountListPageListFirst_Click(object sender, EventArgs e)
        {
            itemListPageList = new PagedList<Entities.DgvTrnStockCountListEntity>(itemListData, 1, pageSize);
            itemListDataSource.DataSource = itemListPageList;

            buttonStockCountListPageListFirst.Enabled = false;
            buttonStockCountListPageListPrevious.Enabled = false;
            buttonStockCountListPageListNext.Enabled = true;
            buttonStockCountListPageListLast.Enabled = true;

            pageNumber = 1;
            textBoxStockCountListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonStockCountListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (itemListPageList.HasPreviousPage == true)
            {
                itemListPageList = new PagedList<Entities.DgvTrnStockCountListEntity>(itemListData, --pageNumber, pageSize);
                itemListDataSource.DataSource = itemListPageList;
            }

            buttonStockCountListPageListNext.Enabled = true;
            buttonStockCountListPageListLast.Enabled = true;

            if (pageNumber == 1)
            {
                buttonStockCountListPageListFirst.Enabled = false;
                buttonStockCountListPageListPrevious.Enabled = false;
            }

            textBoxStockCountListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonStockCountListPageListNext_Click(object sender, EventArgs e)
        {
            if (itemListPageList.HasNextPage == true)
            {
                itemListPageList = new PagedList<Entities.DgvTrnStockCountListEntity>(itemListData, ++pageNumber, pageSize);
                itemListDataSource.DataSource = itemListPageList;
            }

            buttonStockCountListPageListFirst.Enabled = true;
            buttonStockCountListPageListPrevious.Enabled = true;

            if (pageNumber == itemListPageList.PageCount)
            {
                buttonStockCountListPageListNext.Enabled = false;
                buttonStockCountListPageListLast.Enabled = false;
            }

            textBoxStockCountListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonStockCountListPageListLast_Click(object sender, EventArgs e)
        {
            itemListPageList = new PagedList<Entities.DgvTrnStockCountListEntity>(itemListData, itemListPageList.PageCount, pageSize);
            itemListDataSource.DataSource = itemListPageList;

            buttonStockCountListPageListFirst.Enabled = true;
            buttonStockCountListPageListPrevious.Enabled = true;
            buttonStockCountListPageListNext.Enabled = false;
            buttonStockCountListPageListLast.Enabled = false;

            pageNumber = itemListPageList.PageCount;
            textBoxStockCountListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }
    }
}
