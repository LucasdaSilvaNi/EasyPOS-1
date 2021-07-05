using EasyPOS.Entities;
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

namespace EasyPOS.Forms.Software.TrnCollection
{
    public partial class TrnCollectionListForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public static List<DgvTrnCollectionListEntity> collectionListData = new List<DgvTrnCollectionListEntity>();
        public static Int32 pageNumber = 1;
        public static Int32 pageSize = 50;
        public PagedList<DgvTrnCollectionListEntity> itemListPageList = new PagedList<DgvTrnCollectionListEntity>(collectionListData, pageNumber, pageSize);
        public BindingSource itemListDataSource = new BindingSource();
        public TrnCollectionListForm(SysSoftwareForm softwareForm)
        {
            InitializeComponent();
            sysSoftwareForm = softwareForm;

            String currentDate = DateTime.Today.ToShortDateString() + "\t\t";
            if (Modules.SysCurrentModule.GetCurrentSettings().IsLoginDate == true)
            {
                currentDate = Modules.SysCurrentModule.GetCurrentSettings().CurrentDate + "\t\t";
            }

            dateTimePickerCollectionListFilter.Value = Convert.ToDateTime(currentDate);

            sysUserRights = new Modules.SysUserRightsModule("TrnCollection");
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
                    dataGridViewCollectionList.Columns[0].Visible = false;
                }

                if (sysUserRights.GetUserRights().CanDelete == false)
                {
                    dataGridViewCollectionList.Columns[1].Visible = false;
                }

                CreateCollectionListDataGridView();
            }

        }
        public void UpdateCollectionListDataSource()
        {
            SetCollectionListDataSourceAsync();
        }
        public async void SetCollectionListDataSourceAsync()
        {
            List<DgvTrnCollectionListEntity> getCollectionListData = await GetCollectionListDataTask();
            if (getCollectionListData.Any())
            {
                collectionListData = getCollectionListData;
                itemListPageList = new PagedList<DgvTrnCollectionListEntity>(collectionListData, pageNumber, pageSize);

                if (itemListPageList.PageCount == 1)
                {
                    buttonCollectionListPageListFirst.Enabled = false;
                    buttonCollectionListPageListPrevious.Enabled = false;
                    buttonCollectionListPageListNext.Enabled = false;
                    buttonCollectionListPageListLast.Enabled = false;
                }
                else if (pageNumber == 1)
                {
                    buttonCollectionListPageListFirst.Enabled = false;
                    buttonCollectionListPageListPrevious.Enabled = false;
                    buttonCollectionListPageListNext.Enabled = true;
                    buttonCollectionListPageListLast.Enabled = true;
                }
                else if (pageNumber == itemListPageList.PageCount)
                {
                    buttonCollectionListPageListFirst.Enabled = true;
                    buttonCollectionListPageListPrevious.Enabled = true;
                    buttonCollectionListPageListNext.Enabled = false;
                    buttonCollectionListPageListLast.Enabled = false;
                }
                else
                {
                    buttonCollectionListPageListFirst.Enabled = true;
                    buttonCollectionListPageListPrevious.Enabled = true;
                    buttonCollectionListPageListNext.Enabled = true;
                    buttonCollectionListPageListLast.Enabled = true;
                }

                textBoxCollectionListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
                itemListDataSource.DataSource = itemListPageList;
            }
            else
            {
                buttonCollectionListPageListFirst.Enabled = false;
                buttonCollectionListPageListPrevious.Enabled = false;
                buttonCollectionListPageListNext.Enabled = false;
                buttonCollectionListPageListLast.Enabled = false;

                pageNumber = 1;

                collectionListData = new List<DgvTrnCollectionListEntity>();
                itemListDataSource.Clear();
                textBoxCollectionListPageNumber.Text = "1 / 1";
            }
        }
        public Task<List<DgvTrnCollectionListEntity>> GetCollectionListDataTask()
        {
            DateTime dateFilter = dateTimePickerCollectionListFilter.Value.Date;
            String filter = textBoxCollectionListFilter.Text;
            Controllers.TrnCollectionController trnCollectionController = new Controllers.TrnCollectionController();

            List<TrnCollectionEntity> listCollection = trnCollectionController.ListCollection(dateFilter, filter);
            if (listCollection.Any())
            {
                var items = from d in listCollection
                            select new DgvTrnCollectionListEntity
                            {
                                ColumnCollectionListButtonEdit = "Edit",
                                ColumnCollectionListButtonDelete = "Delete",
                                ColumnCollectionListId = d.Id,
                                ColumnCollectionListPeriodId = d.PeriodId,
                                ColumnCollectionListCollectionDate = d.CollectionDate,
                                ColumnCollectionListCollectionNumber = d.CollectionNumber,
                                ColumnCollectionListTerminalId = d.TerminalId,
                                ColumnCollectionListTerminal = d.Terminal,
                                ColumnCollectionListCancelledCollectionNumber = d.CancelledCollectionNumber,
                                ColumnCollectionListManualORNumber = d.ManualORNumber,
                                ColumnCollectionListCustomerId = d.CustomerId,
                                ColumnCollectionListCustomer = d.Customer,
                                ColumnCollectionListRemarks = d.Remarks,
                                ColumnCollectionListSalesId = d.SalesId,
                                ColumnCollectionListSalesBalanceAmount = d.SalesBalanceAmount,
                                ColumnCollectionListAmount = d.Amount.ToString("#,##0.00"),
                                ColumnCollectionListTenderAmount = d.TenderAmount.ToString("#,##0.00"),
                                ColumnCollectionListChangeAmount = d.ChangeAmount.ToString("#,##0.00"),
                                ColumnCollectionListPreparedBy = d.PreparedBy.ToString(),
                                ColumnCollectionListCheckedBy = d.CheckedBy.ToString(),
                                ColumnCollectionListApprovedBy = d.ApprovedBy.ToString(),
                                ColumnCollectionListIsCancelled = d.IsCancelled,
                                ColumnCollectionListIsLocked = d.IsLocked,
                                ColumnCollectionListEntryUserId = d.EntryUserId,
                                ColumnCollectionListEntryDateTime = d.EntryDateTime,
                                ColumnCollectionListUpdateUserId = d.UpdateUserId,
                                ColumnCollectionListUpdateDateTime = d.UpdateDateTime,
                            };

                return Task.FromResult(items.ToList());
            }
            else
            {
                return Task.FromResult(new List<DgvTrnCollectionListEntity>());
            }
        }
        public void CreateCollectionListDataGridView()
        {
            UpdateCollectionListDataSource();

            dataGridViewCollectionList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewCollectionList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewCollectionList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewCollectionList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewCollectionList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewCollectionList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewCollectionList.DataSource = itemListDataSource;
        }
        public void GetCollectionListCurrentSelectedCell(Int32 rowIndex)
        {

        }
        private void dataGridViewCollectionList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetCollectionListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewCollectionList.CurrentCell.ColumnIndex == dataGridViewCollectionList.Columns["ColumnCollectionListButtonEdit"].Index)
            {
                Controllers.TrnCollectionController trnCollectionController = new Controllers.TrnCollectionController();
                sysSoftwareForm.AddTabPageCollectionDetail(this, trnCollectionController.DetailCollection(Convert.ToInt32(dataGridViewCollectionList.Rows[e.RowIndex].Cells[2].Value)));
            }

            if (e.RowIndex > -1 && dataGridViewCollectionList.CurrentCell.ColumnIndex == dataGridViewCollectionList.Columns["ColumnCollectionListButtonDelete"].Index)
            {
                Boolean isLocked = Convert.ToBoolean(dataGridViewCollectionList.Rows[e.RowIndex].Cells[22].Value);

                if (isLocked == true)
                {
                    MessageBox.Show("Already locked.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult deleteDialogResult = MessageBox.Show("Delete Collection?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (deleteDialogResult == DialogResult.Yes)
                    {
                        Controllers.TrnCollectionController trnCollectionController = new Controllers.TrnCollectionController();

                        String[] deleteCollection = trnCollectionController.DeleteCollection(Convert.ToInt32(dataGridViewCollectionList.Rows[e.RowIndex].Cells[2].Value));
                        if (deleteCollection[1].Equals("0") == false)
                        {
                            pageNumber = 1;
                            UpdateCollectionListDataSource();
                        }
                        else
                        {
                            MessageBox.Show(deleteCollection[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void dateTimePickerCollectionListFilter_ValueChanged(object sender, EventArgs e)
        {
            pageNumber = 1;
            UpdateCollectionListDataSource();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Controllers.TrnCollectionController trnCollectionController = new Controllers.TrnCollectionController();
            String[] addCollection = trnCollectionController.AddCollection();
            if (addCollection[1].Equals("0") == false)
            {
                sysSoftwareForm.AddTabPageCollectionDetail(this, trnCollectionController.DetailCollection(Convert.ToInt32(addCollection[1])));
                UpdateCollectionListDataSource();
            }
            else
            {
                MessageBox.Show(addCollection[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
            sysSoftwareForm.RemoveTabPage();
        }

        private void buttonCollectionListPageListFirst_Click(object sender, EventArgs e)
        {
            itemListPageList = new PagedList<Entities.DgvTrnCollectionListEntity>(collectionListData, 1, pageSize);
            itemListDataSource.DataSource = itemListPageList;

            buttonCollectionListPageListFirst.Enabled = false;
            buttonCollectionListPageListPrevious.Enabled = false;
            buttonCollectionListPageListNext.Enabled = true;
            buttonCollectionListPageListLast.Enabled = true;

            pageNumber = 1;
            textBoxCollectionListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;

        }

        private void buttonCollectionListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (itemListPageList.HasPreviousPage == true)
            {
                itemListPageList = new PagedList<Entities.DgvTrnCollectionListEntity>(collectionListData, --pageNumber, pageSize);
                itemListDataSource.DataSource = itemListPageList;
            }

            buttonCollectionListPageListNext.Enabled = true;
            buttonCollectionListPageListLast.Enabled = true;

            if (pageNumber == 1)
            {
                buttonCollectionListPageListFirst.Enabled = false;
                buttonCollectionListPageListPrevious.Enabled = false;
            }

            textBoxCollectionListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonCollectionListPageListNext_Click(object sender, EventArgs e)
        {
            if (itemListPageList.HasNextPage == true)
            {
                itemListPageList = new PagedList<Entities.DgvTrnCollectionListEntity>(collectionListData, ++pageNumber, pageSize);
                itemListDataSource.DataSource = itemListPageList;
            }

            buttonCollectionListPageListFirst.Enabled = true;
            buttonCollectionListPageListPrevious.Enabled = true;

            if (pageNumber == itemListPageList.PageCount)
            {
                buttonCollectionListPageListNext.Enabled = false;
                buttonCollectionListPageListLast.Enabled = false;
            }

            textBoxCollectionListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonCollectionListPageListLast_Click(object sender, EventArgs e)
        {
            itemListPageList = new PagedList<Entities.DgvTrnCollectionListEntity>(collectionListData, itemListPageList.PageCount, pageSize);
            itemListDataSource.DataSource = itemListPageList;

            buttonCollectionListPageListFirst.Enabled = true;
            buttonCollectionListPageListPrevious.Enabled = true;
            buttonCollectionListPageListNext.Enabled = false;
            buttonCollectionListPageListLast.Enabled = false;

            pageNumber = itemListPageList.PageCount;
            textBoxCollectionListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }
    }
}
