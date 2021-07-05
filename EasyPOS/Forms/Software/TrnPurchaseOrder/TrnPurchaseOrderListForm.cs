using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyPOS.Entities;
using PagedList;

namespace EasyPOS.Forms.Software.TrnPurchaseOrder
{
    public partial class TrnPurchaseOrderListForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public static List<DgvTrnPurchaseOrderListEntity> itemListData = new List<DgvTrnPurchaseOrderListEntity>();
        public static Int32 pageNumber = 1;
        public static Int32 pageSize = 50;
        public PagedList<DgvTrnPurchaseOrderListEntity> itemListPageList = new PagedList<DgvTrnPurchaseOrderListEntity>(itemListData, pageNumber, pageSize);
        public BindingSource itemListDataSource = new BindingSource();
        public TrnPurchaseOrderListForm(SysSoftwareForm softwareForm)
        {
            sysSoftwareForm = softwareForm;
            InitializeComponent();

            String currentDate = DateTime.Today.ToShortDateString() + "\t\t";
            if (Modules.SysCurrentModule.GetCurrentSettings().IsLoginDate == true)
            {
                currentDate = Modules.SysCurrentModule.GetCurrentSettings().CurrentDate + "\t\t";
            }

            dateTimePickerPurchaseOrderListFilter.Value = Convert.ToDateTime(currentDate);

            sysUserRights = new Modules.SysUserRightsModule("TrnPurchaseOrder");
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
                    dataGridViewPurchaseOrderList.Columns[0].Visible = false;
                }

                if (sysUserRights.GetUserRights().CanDelete == false)
                {
                    dataGridViewPurchaseOrderList.Columns[1].Visible = false;
                }

                CreatePurchaseOrderListDataGridView();
            }
        }

        public void UpdatePurchaseOrderListDataSource()
        {
            SetPurchaseOrderListDataSourceAsync();
        }
        public async void SetPurchaseOrderListDataSourceAsync()
        {
            List<DgvTrnPurchaseOrderListEntity> getPurchaseOrderListData = await GetPurchaseOrderListDataTask();
            if (getPurchaseOrderListData.Any())
            {
                itemListData = getPurchaseOrderListData;
                itemListPageList = new PagedList<DgvTrnPurchaseOrderListEntity>(itemListData, pageNumber, pageSize);

                if (itemListPageList.PageCount == 1)
                {
                    buttonPurchaseOrderListPageListFirst.Enabled = false;
                    buttonPurchaseOrderListPageListPrevious.Enabled = false;
                    buttonPurchaseOrderListPageListNext.Enabled = false;
                    buttonPurchaseOrderListPageListLast.Enabled = false;
                }
                else if (pageNumber == 1)
                {
                    buttonPurchaseOrderListPageListFirst.Enabled = false;
                    buttonPurchaseOrderListPageListPrevious.Enabled = false;
                    buttonPurchaseOrderListPageListNext.Enabled = true;
                    buttonPurchaseOrderListPageListLast.Enabled = true;
                }
                else if (pageNumber == itemListPageList.PageCount)
                {
                    buttonPurchaseOrderListPageListFirst.Enabled = true;
                    buttonPurchaseOrderListPageListPrevious.Enabled = true;
                    buttonPurchaseOrderListPageListNext.Enabled = false;
                    buttonPurchaseOrderListPageListLast.Enabled = false;
                }
                else
                {
                    buttonPurchaseOrderListPageListFirst.Enabled = true;
                    buttonPurchaseOrderListPageListPrevious.Enabled = true;
                    buttonPurchaseOrderListPageListNext.Enabled = true;
                    buttonPurchaseOrderListPageListLast.Enabled = true;
                }

                textBoxPurchaseOrderListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
                itemListDataSource.DataSource = itemListPageList;
            }
            else
            {
                buttonPurchaseOrderListPageListFirst.Enabled = false;
                buttonPurchaseOrderListPageListPrevious.Enabled = false;
                buttonPurchaseOrderListPageListNext.Enabled = false;
                buttonPurchaseOrderListPageListLast.Enabled = false;

                pageNumber = 1;

                itemListData = new List<DgvTrnPurchaseOrderListEntity>();
                itemListDataSource.Clear();
                textBoxPurchaseOrderListPageNumber.Text = "1 / 1";
            }
        }
        public Task<List<DgvTrnPurchaseOrderListEntity>> GetPurchaseOrderListDataTask()
        {
            DateTime dateFilter = dateTimePickerPurchaseOrderListFilter.Value.Date;
            String filter = textBoxPurchaseOrderListFilter.Text;
            Controllers.TrnPurchaseOrderController trnPurchaseOrderController = new Controllers.TrnPurchaseOrderController();

            List<TrnPurchaseOrderEntity> listPurchaseOrder = trnPurchaseOrderController.ListPurchaseOrder(dateFilter, filter);
            if (listPurchaseOrder.Any())
            {
                var items = from d in listPurchaseOrder
                            select new DgvTrnPurchaseOrderListEntity
                            {
                                ColumnPurchaseOrderListButtonEdit = "Edit",
                                ColumnPurchaseOrderListButtonDelete = "Delete",
                                ColumnPurchaseOrderListId = d.Id,
                                ColumnPurchaseOrderListPeriodId = d.PeriodId,
                                ColumnPurchaseOrderListPurchaseOrderDate = d.PurchaseOrderDate,
                                ColumnPurchaseOrderListPurchaseOrderNumber = d.PurchaseOrderNumber,
                                ColumnPurchaseOrderListAmount = d.Amount.ToString("#,##0.00"),
                                ColumnPurchaseOrderListSupplier = d.Supplier,
                                ColumnPurchaseOrderListRemarks = d.Remarks,
                                ColumnPurchaseOrderListPreparedBy = d.PreparedBy.ToString(),
                                ColumnPurchaseOrderListCheckedBy = d.CheckedBy.ToString(),
                                ColumnPurchaseOrderListApprovedBy = d.ApprovedBy.ToString(),
                                ColumnPurchaseOrderListIsLocked = d.IsLocked,
                                ColumnPurchaseOrderListEntryUserId = d.EntryUserId,
                                ColumnPurchaseOrderListEntryDateTime = d.EntryDateTime,
                                ColumnPurchaseOrderListUpdateUserId = d.UpdateUserId,
                                ColumnPurchaseOrderListUpdateDateTime = d.UpdateDateTime,
                                ColumnPurchaseOrderListRequestedBy = d.RequestedBy.ToString()
                            };

                return Task.FromResult(items.ToList());
            }
            else
            {
                return Task.FromResult(new List<DgvTrnPurchaseOrderListEntity>());
            }
        }
        public void CreatePurchaseOrderListDataGridView()
        {
            UpdatePurchaseOrderListDataSource();

            dataGridViewPurchaseOrderList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewPurchaseOrderList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewPurchaseOrderList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewPurchaseOrderList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewPurchaseOrderList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewPurchaseOrderList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewPurchaseOrderList.DataSource = itemListDataSource;
        }
        public void GetPurchaseOrderListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Controllers.TrnPurchaseOrderController trnPurchaseOrderController = new Controllers.TrnPurchaseOrderController();
            String[] addPurchaseOrder = trnPurchaseOrderController.AddPurchaseOrder();
            if (addPurchaseOrder[1].Equals("0") == false)
            {
                sysSoftwareForm.AddTabPagePurchaseOrderDetail(this, trnPurchaseOrderController.DetailPurchaseOrder(Convert.ToInt32(addPurchaseOrder[1])));
                UpdatePurchaseOrderListDataSource();
            }
            else
            {
                MessageBox.Show(addPurchaseOrder[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
            sysSoftwareForm.RemoveTabPage();
        }

        private void dataGridViewPurchaseOrderList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetPurchaseOrderListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewPurchaseOrderList.CurrentCell.ColumnIndex == dataGridViewPurchaseOrderList.Columns["ColumnPurchaseOrderListButtonEdit"].Index)
            {
                Controllers.TrnPurchaseOrderController trnPurchaseOrderController = new Controllers.TrnPurchaseOrderController();
                sysSoftwareForm.AddTabPagePurchaseOrderDetail(this, trnPurchaseOrderController.DetailPurchaseOrder(Convert.ToInt32(dataGridViewPurchaseOrderList.Rows[e.RowIndex].Cells[2].Value)));
            }

            if (e.RowIndex > -1 && dataGridViewPurchaseOrderList.CurrentCell.ColumnIndex == dataGridViewPurchaseOrderList.Columns["ColumnPurchaseOrderListButtonDelete"].Index)
            {
                Boolean isLocked = Convert.ToBoolean(dataGridViewPurchaseOrderList.Rows[e.RowIndex].Cells[12].Value);

                if (isLocked == true)
                {
                    MessageBox.Show("Already locked.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult deleteDialogResult = MessageBox.Show("Delete PurchaseOrder?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (deleteDialogResult == DialogResult.Yes)
                    {
                        Controllers.TrnPurchaseOrderController trnPurchaseOrderController = new Controllers.TrnPurchaseOrderController();

                        String[] deletePurchaseOrder = trnPurchaseOrderController.DeletePurchaseOrder(Convert.ToInt32(dataGridViewPurchaseOrderList.Rows[e.RowIndex].Cells[2].Value));
                        if (deletePurchaseOrder[1].Equals("0") == false)
                        {
                            pageNumber = 1;
                            UpdatePurchaseOrderListDataSource();
                        }
                        else
                        {
                            MessageBox.Show(deletePurchaseOrder[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void dateTimePickerPurchaseOrderListFilter_ValueChanged(object sender, EventArgs e)
        {
            pageNumber = 1;
            UpdatePurchaseOrderListDataSource();
        }

        private void buttonPurchaseOrderListPageListFirst_Click(object sender, EventArgs e)
        {
            itemListPageList = new PagedList<Entities.DgvTrnPurchaseOrderListEntity>(itemListData, 1, pageSize);
            itemListDataSource.DataSource = itemListPageList;

            buttonPurchaseOrderListPageListFirst.Enabled = false;
            buttonPurchaseOrderListPageListPrevious.Enabled = false;
            buttonPurchaseOrderListPageListNext.Enabled = true;
            buttonPurchaseOrderListPageListLast.Enabled = true;

            pageNumber = 1;
            textBoxPurchaseOrderListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonPurchaseOrderListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (itemListPageList.HasPreviousPage == true)
            {
                itemListPageList = new PagedList<Entities.DgvTrnPurchaseOrderListEntity>(itemListData, --pageNumber, pageSize);
                itemListDataSource.DataSource = itemListPageList;
            }

            buttonPurchaseOrderListPageListNext.Enabled = true;
            buttonPurchaseOrderListPageListLast.Enabled = true;

            if (pageNumber == 1)
            {
                buttonPurchaseOrderListPageListFirst.Enabled = false;
                buttonPurchaseOrderListPageListPrevious.Enabled = false;
            }

            textBoxPurchaseOrderListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonPurchaseOrderListPageListNext_Click(object sender, EventArgs e)
        {
            if (itemListPageList.HasNextPage == true)
            {
                itemListPageList = new PagedList<Entities.DgvTrnPurchaseOrderListEntity>(itemListData, ++pageNumber, pageSize);
                itemListDataSource.DataSource = itemListPageList;
            }

            buttonPurchaseOrderListPageListFirst.Enabled = true;
            buttonPurchaseOrderListPageListPrevious.Enabled = true;

            if (pageNumber == itemListPageList.PageCount)
            {
                buttonPurchaseOrderListPageListNext.Enabled = false;
                buttonPurchaseOrderListPageListLast.Enabled = false;
            }

            textBoxPurchaseOrderListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonPurchaseOrderListPageListLast_Click(object sender, EventArgs e)
        {
            itemListPageList = new PagedList<Entities.DgvTrnPurchaseOrderListEntity>(itemListData, itemListPageList.PageCount, pageSize);
            itemListDataSource.DataSource = itemListPageList;

            buttonPurchaseOrderListPageListFirst.Enabled = true;
            buttonPurchaseOrderListPageListPrevious.Enabled = true;
            buttonPurchaseOrderListPageListNext.Enabled = false;
            buttonPurchaseOrderListPageListLast.Enabled = false;

            pageNumber = itemListPageList.PageCount;
            textBoxPurchaseOrderListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }
    }
}
