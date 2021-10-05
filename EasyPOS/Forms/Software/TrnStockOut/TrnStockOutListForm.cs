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

namespace EasyPOS.Forms.Software.TrnStockOut
{
    public partial class TrnStockOutListForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public static List<Entities.DgvStockOutListStockOutEntity> itemListData = new List<Entities.DgvStockOutListStockOutEntity>();
        public static Int32 pageNumber = 1;
        public static Int32 pageSize = 50;
        public PagedList<Entities.DgvStockOutListStockOutEntity> itemListPageList = new PagedList<Entities.DgvStockOutListStockOutEntity>(itemListData, pageNumber, pageSize);
        public BindingSource itemListDataSource = new BindingSource();

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public TrnStockOutListForm(SysSoftwareForm softwareForm)
        {
            InitializeComponent();

            label1.Text = SetLabel(label1.Text);
            buttonAdd.Text = SetLabel(buttonAdd.Text);
            buttonClose.Text = SetLabel(buttonClose.Text);
            buttonStockOutListPageListFirst.Text = SetLabel(buttonStockOutListPageListFirst.Text);
            buttonStockOutListPageListPrevious.Text = SetLabel(buttonStockOutListPageListPrevious.Text);
            buttonStockOutListPageListNext.Text = SetLabel(buttonStockOutListPageListNext.Text);
            buttonStockOutListPageListLast.Text = SetLabel(buttonStockOutListPageListLast.Text);
            dataGridViewStockOutList.Columns[4].HeaderText = SetLabel(dataGridViewStockOutList.Columns[4].HeaderText);
            dataGridViewStockOutList.Columns[5].HeaderText = SetLabel(dataGridViewStockOutList.Columns[5].HeaderText);
            dataGridViewStockOutList.Columns[6].HeaderText = SetLabel(dataGridViewStockOutList.Columns[6].HeaderText);
            dataGridViewStockOutList.Columns[7].HeaderText = SetLabel(dataGridViewStockOutList.Columns[7].HeaderText);

            sysSoftwareForm = softwareForm;

            String currentDate = DateTime.Today.ToShortDateString() + "\t\t";
            if (Modules.SysCurrentModule.GetCurrentSettings().IsLoginDate == true)
            {
                currentDate = Modules.SysCurrentModule.GetCurrentSettings().CurrentDate + "\t\t";
            }

            dateTimePickerStockOutListFilter.Value = Convert.ToDateTime(currentDate);

            sysUserRights = new Modules.SysUserRightsModule("TrnStockOut");
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
                    dataGridViewStockOutList.Columns[0].Visible = false;
                }

                if (sysUserRights.GetUserRights().CanDelete == false)
                {
                    dataGridViewStockOutList.Columns[1].Visible = false;
                }

                CreateStockOutListDataGridView();
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

        public void UpdateStockOutListDataSource()
        {
            SetStockOutListDataSourceAsync();
        }

        public async void SetStockOutListDataSourceAsync()
        {
            List<Entities.DgvStockOutListStockOutEntity> getStockOutListData = await GetStockOutListDataTask();
            if (getStockOutListData.Any())
            {
                itemListData = getStockOutListData;
                itemListPageList = new PagedList<Entities.DgvStockOutListStockOutEntity>(itemListData, pageNumber, pageSize);

                if (itemListPageList.PageCount == 1)
                {
                    buttonStockOutListPageListFirst.Enabled = false;
                    buttonStockOutListPageListPrevious.Enabled = false;
                    buttonStockOutListPageListNext.Enabled = false;
                    buttonStockOutListPageListLast.Enabled = false;
                }
                else if (pageNumber == 1)
                {
                    buttonStockOutListPageListFirst.Enabled = false;
                    buttonStockOutListPageListPrevious.Enabled = false;
                    buttonStockOutListPageListNext.Enabled = true;
                    buttonStockOutListPageListLast.Enabled = true;
                }
                else if (pageNumber == itemListPageList.PageCount)
                {
                    buttonStockOutListPageListFirst.Enabled = true;
                    buttonStockOutListPageListPrevious.Enabled = true;
                    buttonStockOutListPageListNext.Enabled = false;
                    buttonStockOutListPageListLast.Enabled = false;
                }
                else
                {
                    buttonStockOutListPageListFirst.Enabled = true;
                    buttonStockOutListPageListPrevious.Enabled = true;
                    buttonStockOutListPageListNext.Enabled = true;
                    buttonStockOutListPageListLast.Enabled = true;
                }

                textBoxStockOutListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
                itemListDataSource.DataSource = itemListPageList;
            }
            else
            {
                buttonStockOutListPageListFirst.Enabled = false;
                buttonStockOutListPageListPrevious.Enabled = false;
                buttonStockOutListPageListNext.Enabled = false;
                buttonStockOutListPageListLast.Enabled = false;

                pageNumber = 1;

                itemListData = new List<Entities.DgvStockOutListStockOutEntity>();
                itemListDataSource.Clear();
                textBoxStockOutListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvStockOutListStockOutEntity>> GetStockOutListDataTask()
        {
            DateTime dateFilter = dateTimePickerStockOutListFilter.Value.Date;
            String filter = textBoxStockOutListFilter.Text;
            Controllers.TrnStockOutController trnStockOutController = new Controllers.TrnStockOutController();

            List<Entities.TrnStockOutEntity> listStockOut = trnStockOutController.ListStockOut(dateFilter, filter);
            if (listStockOut.Any())
            {
                var items = from d in listStockOut
                            select new Entities.DgvStockOutListStockOutEntity
                            {
                                ColumnStockOutListButtonEdit = SetLabel("Edit"),
                                ColumnStockOutListButtonDelete = SetLabel("Delete"),
                                ColumnStockOutListId = d.Id,
                                ColumnStockOutListStockOutDate = d.StockOutDate,
                                ColumnStockOutListStockOutNumber = d.StockOutNumber,
                                ColumnStockOutListManualStockOutNumber = d.ManualStockOutNumber,
                                ColumnStockOutListRemarks = d.Remarks,
                                ColumnStockOutListIsLocked = d.IsLocked
                            };

                return Task.FromResult(items.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvStockOutListStockOutEntity>());
            }
        }

        public void CreateStockOutListDataGridView()
        {
            UpdateStockOutListDataSource();

            dataGridViewStockOutList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewStockOutList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewStockOutList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewStockOutList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewStockOutList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewStockOutList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewStockOutList.DataSource = itemListDataSource;
        }

        public void GetStockOutListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Controllers.TrnStockOutController trnStockOutController = new Controllers.TrnStockOutController();
            String[] addStockOut = trnStockOutController.AddStockOut();
            if (addStockOut[1].Equals("0") == false)
            {
                sysSoftwareForm.AddTabPageStockOutDetail(this, trnStockOutController.DetailStockOut(Convert.ToInt32(addStockOut[1])));
                UpdateStockOutListDataSource();
            }
            else
            {
                MessageBox.Show(addStockOut[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
            sysSoftwareForm.RemoveTabPage();
        }

        private void dateTimePickerStockOutListFilter_ValueChanged(object sender, EventArgs e)
        {
            UpdateStockOutListDataSource();
        }

        private void textBoxStockOutListFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateStockOutListDataSource();
            }
        }

        private void dataGridViewStockOutList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetStockOutListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewStockOutList.CurrentCell.ColumnIndex == dataGridViewStockOutList.Columns["ColumnStockOutListButtonEdit"].Index)
            {
                Controllers.TrnStockOutController trnStockOutController = new Controllers.TrnStockOutController();
                sysSoftwareForm.AddTabPageStockOutDetail(this, trnStockOutController.DetailStockOut(Convert.ToInt32(dataGridViewStockOutList.Rows[e.RowIndex].Cells[2].Value)));
            }

            if (e.RowIndex > -1 && dataGridViewStockOutList.CurrentCell.ColumnIndex == dataGridViewStockOutList.Columns["ColumnStockOutListButtonDelete"].Index)
            {
                Boolean isLocked = Convert.ToBoolean(dataGridViewStockOutList.Rows[e.RowIndex].Cells[7].Value);

                if (isLocked == true)
                {
                    MessageBox.Show("Already locked.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult deleteDialogResult = MessageBox.Show("Delete Stock-Out?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (deleteDialogResult == DialogResult.Yes)
                    {
                        Controllers.TrnStockOutController trnStockOutController = new Controllers.TrnStockOutController();

                        String[] deleteStockOut = trnStockOutController.DeleteStockOut(Convert.ToInt32(dataGridViewStockOutList.Rows[e.RowIndex].Cells[2].Value));
                        if (deleteStockOut[1].Equals("0") == false)
                        {
                            pageNumber = 1;
                            UpdateStockOutListDataSource();
                        }
                        else
                        {
                            MessageBox.Show(deleteStockOut[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void buttonStockOutListPageListFirst_Click(object sender, EventArgs e)
        {
            itemListPageList = new PagedList<Entities.DgvStockOutListStockOutEntity>(itemListData, 1, pageSize);
            itemListDataSource.DataSource = itemListPageList;

            buttonStockOutListPageListFirst.Enabled = false;
            buttonStockOutListPageListPrevious.Enabled = false;
            buttonStockOutListPageListNext.Enabled = true;
            buttonStockOutListPageListLast.Enabled = true;

            pageNumber = 1;
            textBoxStockOutListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonStockOutListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (itemListPageList.HasPreviousPage == true)
            {
                itemListPageList = new PagedList<Entities.DgvStockOutListStockOutEntity>(itemListData, --pageNumber, pageSize);
                itemListDataSource.DataSource = itemListPageList;
            }

            buttonStockOutListPageListNext.Enabled = true;
            buttonStockOutListPageListLast.Enabled = true;

            if (pageNumber == 1)
            {
                buttonStockOutListPageListFirst.Enabled = false;
                buttonStockOutListPageListPrevious.Enabled = false;
            }

            textBoxStockOutListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonStockOutListPageListNext_Click(object sender, EventArgs e)
        {
            if (itemListPageList.HasNextPage == true)
            {
                itemListPageList = new PagedList<Entities.DgvStockOutListStockOutEntity>(itemListData, ++pageNumber, pageSize);
                itemListDataSource.DataSource = itemListPageList;
            }

            buttonStockOutListPageListFirst.Enabled = true;
            buttonStockOutListPageListPrevious.Enabled = true;

            if (pageNumber == itemListPageList.PageCount)
            {
                buttonStockOutListPageListNext.Enabled = false;
                buttonStockOutListPageListLast.Enabled = false;
            }

            textBoxStockOutListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonStockOutListPageListLast_Click(object sender, EventArgs e)
        {
            itemListPageList = new PagedList<Entities.DgvStockOutListStockOutEntity>(itemListData, itemListPageList.PageCount, pageSize);
            itemListDataSource.DataSource = itemListPageList;

            buttonStockOutListPageListFirst.Enabled = true;
            buttonStockOutListPageListPrevious.Enabled = true;
            buttonStockOutListPageListNext.Enabled = false;
            buttonStockOutListPageListLast.Enabled = false;

            pageNumber = itemListPageList.PageCount;
            textBoxStockOutListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }
    }
}
