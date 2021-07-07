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

namespace EasyPOS.Forms.Software.TrnPurchaseOrder
{
    public partial class TrnPurchaseOrderDetailForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;
        public TrnPurchaseOrderListForm trnPurchaseOrderListForm;
        public Entities.TrnPurchaseOrderEntity trnPurchaseOrderEntity;

        public static List<Entities.DgvTrnPurchaseOrderLineListEntity> purchaseOrderLineData = new List<Entities.DgvTrnPurchaseOrderLineListEntity>();
        public static Int32 PurchaseOrderLinePageNumber = 1;
        public static Int32 PurchaseOrderLinePageSize = 50;
        public PagedList<Entities.DgvTrnPurchaseOrderLineListEntity> purchaseOrderLinePageList = new PagedList<Entities.DgvTrnPurchaseOrderLineListEntity>(purchaseOrderLineData, PurchaseOrderLinePageNumber, PurchaseOrderLinePageSize);
        public BindingSource purchaseOrderLineDataSource = new BindingSource();
        public TrnPurchaseOrderDetailForm(SysSoftwareForm softwareForm, TrnPurchaseOrderListForm purchaseOrderListForm, Entities.TrnPurchaseOrderEntity purchaseOrderEntity)
        {
            InitializeComponent();
            sysSoftwareForm = softwareForm;

            sysUserRights = new Modules.SysUserRightsModule("TrnPurchaseOrderDetail");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                trnPurchaseOrderListForm = purchaseOrderListForm;
                trnPurchaseOrderEntity = purchaseOrderEntity;

                GetStatus();
            }

        }
        public void GetStatus()
        {
            List<String> statuses = new List<String>();
            statuses.Add("NEW");
            statuses.Add("RECEIVED");
            comboBoxStatus.DataSource = statuses;

            GetSupplierList();
        }
        public void GetSupplierList()
        {
            Controllers.TrnPurchaseOrderController trnPurchaseOrderController = new Controllers.TrnPurchaseOrderController();
            if (trnPurchaseOrderController.DropdownListPurchaseOrderSupplier().Any())
            {
                comboBoxSupplier.DataSource = trnPurchaseOrderController.DropdownListPurchaseOrderSupplier();
                comboBoxSupplier.ValueMember = "Id";
                comboBoxSupplier.DisplayMember = "Supplier";

                GetUserList();
            }
        }
        public void GetUserList()
        {
            Controllers.TrnPurchaseOrderController trnPurchaseOrderController = new Controllers.TrnPurchaseOrderController();
            if (trnPurchaseOrderController.DropdownListPurchaseOrderUser().Any())
            {
                comboBoxPreparedBy.DataSource = trnPurchaseOrderController.DropdownListPurchaseOrderUser();
                comboBoxPreparedBy.ValueMember = "Id";
                comboBoxPreparedBy.DisplayMember = "FullName";

                comboBoxCheckedBy.DataSource = trnPurchaseOrderController.DropdownListPurchaseOrderUser();
                comboBoxCheckedBy.ValueMember = "Id";
                comboBoxCheckedBy.DisplayMember = "FullName";

                comboBoxApprovedBy.DataSource = trnPurchaseOrderController.DropdownListPurchaseOrderUser();
                comboBoxApprovedBy.ValueMember = "Id";
                comboBoxApprovedBy.DisplayMember = "FullName";

                GetPurchaseOrderDetail();
            }
        }
        public void GetPurchaseOrderDetail()
        {
            UpdateComponents(trnPurchaseOrderEntity.IsLocked);

            textBoxPurchaseOrderNumber.Text = trnPurchaseOrderEntity.PurchaseOrderNumber;
            dateTimePickerPurchaseOrderDate.Value = Convert.ToDateTime(trnPurchaseOrderEntity.PurchaseOrderDate);
            comboBoxSupplier.SelectedValue = trnPurchaseOrderEntity.SupplierId;
            textBoxRemarks.Text = trnPurchaseOrderEntity.Remarks;
            comboBoxPreparedBy.SelectedValue = trnPurchaseOrderEntity.PreparedBy;
            comboBoxCheckedBy.SelectedValue = trnPurchaseOrderEntity.CheckedBy;
            comboBoxApprovedBy.SelectedValue = trnPurchaseOrderEntity.ApprovedBy;
            comboBoxStatus.Text = trnPurchaseOrderEntity.Status;

            CreatePurchaseOrderLineListDataGridView();
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

            if (sysUserRights.GetUserRights().CanPrint == false)
            {
                buttonPrint.Enabled = false;
            }
            else
            {
                buttonPrint.Enabled = isLocked;
            }

            if (sysUserRights.GetUserRights().CanAdd == false)
            {
                textBoxBarcode.Enabled = false;
                buttonBarcode.Enabled = false;
                buttonSearchItem.Enabled = false;
            }
            else
            {
                buttonBarcode.Enabled = !isLocked;
                buttonSearchItem.Enabled = !isLocked;
                textBoxBarcode.Enabled = !isLocked;
            }

            buttonStockIn.Enabled = isLocked;

            dateTimePickerPurchaseOrderDate.Enabled = !isLocked;
            comboBoxSupplier.Enabled = !isLocked;
            textBoxRemarks.Enabled = !isLocked;
            comboBoxPreparedBy.Enabled = !isLocked;
            comboBoxCheckedBy.Enabled = !isLocked;
            comboBoxApprovedBy.Enabled = !isLocked;
            comboBoxStatus.Enabled = !isLocked;

            dataGridViewPurchaseOrderLineList.Columns[0].Visible = !isLocked;
            dataGridViewPurchaseOrderLineList.Columns[1].Visible = !isLocked;
            dataGridViewPurchaseOrderLineList.Columns[11].Visible = isLocked;
            dataGridViewPurchaseOrderLineList.Columns[11].ReadOnly = !isLocked;
            dateTimePickerPurchaseOrderDate.Focus();
        }

        private void buttonLock_Click(object sender, EventArgs e)
        {
            Controllers.TrnPurchaseOrderController trnPurchaseOrderController = new Controllers.TrnPurchaseOrderController();

            Entities.TrnPurchaseOrderEntity newPurchaseOrderEntity = new Entities.TrnPurchaseOrderEntity()
            {
                PurchaseOrderDate = dateTimePickerPurchaseOrderDate.Value.Date.ToShortDateString(),
                SupplierId = Convert.ToInt32(comboBoxSupplier.SelectedValue),
                Remarks = textBoxRemarks.Text,
                PreparedBy = Convert.ToInt32(comboBoxPreparedBy.SelectedValue),
                CheckedBy = Convert.ToInt32(comboBoxCheckedBy.SelectedValue),
                ApprovedBy = Convert.ToInt32(comboBoxApprovedBy.SelectedValue),
                Status = comboBoxStatus.Text
            };

            String[] lockPurchaseOrder = trnPurchaseOrderController.LockPurcherOrder(trnPurchaseOrderEntity.Id, newPurchaseOrderEntity);
            if (lockPurchaseOrder[1].Equals("0") == false)
            {
                UpdateComponents(true);
                trnPurchaseOrderListForm.UpdatePurchaseOrderListDataSource();
            }
            else
            {
                MessageBox.Show(lockPurchaseOrder[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUnlock_Click(object sender, EventArgs e)
        {
            Controllers.TrnPurchaseOrderController trnPurchaseOrderController = new Controllers.TrnPurchaseOrderController();

            String[] unlockPurchaseOrder = trnPurchaseOrderController.UnlockPurchaseOrder(trnPurchaseOrderEntity.Id);
            if (unlockPurchaseOrder[1].Equals("0") == false)
            {
                UpdateComponents(false);
                trnPurchaseOrderListForm.UpdatePurchaseOrderListDataSource();
            }
            else
            {
                MessageBox.Show(unlockPurchaseOrder[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            new TrnPurchaseOrderDetailReportForm(trnPurchaseOrderEntity.Id);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            sysSoftwareForm.RemoveTabPage();
        }
        public void UpdatePurchaseOrderLineListDataSource()
        {
            SetPurchaseOrderLineListDataSourceAsync();
        }
        public async void SetPurchaseOrderLineListDataSourceAsync()
        {
            List<Entities.DgvTrnPurchaseOrderLineListEntity> getPurchaseOrderLineListData = await GetPurchaseOrderLineListDataTask();
            if (getPurchaseOrderLineListData.Any())
            {
                purchaseOrderLineData = getPurchaseOrderLineListData;
                purchaseOrderLinePageList = new PagedList<Entities.DgvTrnPurchaseOrderLineListEntity>(purchaseOrderLineData, PurchaseOrderLinePageNumber, PurchaseOrderLinePageSize);

                if (purchaseOrderLinePageList.PageCount == 1)
                {
                    buttonPurchaseOrderLineListPageListFirst.Enabled = false;
                    buttonPurchaseOrderLineListPageListPrevious.Enabled = false;
                    buttonPurchaseOrderLineListPageListNext.Enabled = false;
                    buttonPurchaseOrderLineListPageListLast.Enabled = false;
                }
                else if (PurchaseOrderLinePageNumber == 1)
                {
                    buttonPurchaseOrderLineListPageListFirst.Enabled = false;
                    buttonPurchaseOrderLineListPageListPrevious.Enabled = false;
                    buttonPurchaseOrderLineListPageListNext.Enabled = true;
                    buttonPurchaseOrderLineListPageListLast.Enabled = true;
                }
                else if (PurchaseOrderLinePageNumber == purchaseOrderLinePageList.PageCount)
                {
                    buttonPurchaseOrderLineListPageListFirst.Enabled = true;
                    buttonPurchaseOrderLineListPageListPrevious.Enabled = true;
                    buttonPurchaseOrderLineListPageListNext.Enabled = false;
                    buttonPurchaseOrderLineListPageListLast.Enabled = false;
                }
                else
                {
                    buttonPurchaseOrderLineListPageListFirst.Enabled = true;
                    buttonPurchaseOrderLineListPageListPrevious.Enabled = true;
                    buttonPurchaseOrderLineListPageListNext.Enabled = true;
                    buttonPurchaseOrderLineListPageListLast.Enabled = true;
                }

                textBoxPurchaseOrderLineListPageNumber.Text = PurchaseOrderLinePageNumber + " / " + purchaseOrderLinePageList.PageCount;
                purchaseOrderLineDataSource.DataSource = purchaseOrderLinePageList;
            }
            else
            {
                buttonPurchaseOrderLineListPageListFirst.Enabled = false;
                buttonPurchaseOrderLineListPageListPrevious.Enabled = false;
                buttonPurchaseOrderLineListPageListNext.Enabled = false;
                buttonPurchaseOrderLineListPageListLast.Enabled = false;

                PurchaseOrderLinePageNumber = 1;

                purchaseOrderLineData = new List<Entities.DgvTrnPurchaseOrderLineListEntity>();
                purchaseOrderLineDataSource.Clear();
                textBoxPurchaseOrderLineListPageNumber.Text = "1 / 1";
            }
        }
        public Task<List<Entities.DgvTrnPurchaseOrderLineListEntity>> GetPurchaseOrderLineListDataTask()
        {
            Controllers.TrnPurchaseOrderLineController trnPurchaseOrderLineController = new Controllers.TrnPurchaseOrderLineController();

            List<Entities.TrnPurchaseOrderLineEntity> listPurchaseOrderLine = trnPurchaseOrderLineController.ListPurchaseOrderLine(trnPurchaseOrderEntity.Id);
            if (listPurchaseOrderLine.Any())
            {
                var items = from d in listPurchaseOrderLine
                            select new Entities.DgvTrnPurchaseOrderLineListEntity
                            {
                                ColumnPurchaseOrderLineListButtonEdit = "Edit",
                                ColumnPurchaseOrderLineListButtonDelete = "Delete",
                                ColumnPurchaseOrderLineListId = d.Id,
                                ColumnPurchaseOrderLineListPurchaseOrderId = d.PurchaseOrderId,
                                ColumnPurchaseOrderLineListItemId = d.ItemId,
                                ColumnPurchaseOrderLineListItemDescription = d.ItemDescription,
                                ColumnPurchaseOrderLineListUnitId = d.UnitId,
                                ColumnPurchaseOrderLineListUnit = d.Unit,
                                ColumnPurchaseOrderLineListQuantity = Convert.ToDecimal(d.Quantity.ToString("#,##0.00")),
                                ColumnPurchaseOrderLineListCost = Convert.ToDecimal(d.Cost.ToString("#,##0.00")),
                                ColumnPurchaseOrderLineListAmount = Convert.ToDecimal(d.Amount.ToString("#,##0.00")),
                                ColumnPurchaseOrderLineListReceivedQuantity = Convert.ToDecimal(d.ReceivedQuantity.ToString("#,##0.00")),
                            };

                return Task.FromResult(items.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvTrnPurchaseOrderLineListEntity>());
            }
        }
        public void CreatePurchaseOrderLineListDataGridView()
        {
            UpdatePurchaseOrderLineListDataSource();

            dataGridViewPurchaseOrderLineList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewPurchaseOrderLineList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewPurchaseOrderLineList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewPurchaseOrderLineList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewPurchaseOrderLineList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewPurchaseOrderLineList.Columns[1].DefaultCellStyle.ForeColor = Color.White;



            dataGridViewPurchaseOrderLineList.DataSource = purchaseOrderLineDataSource;
        }
        public void GetPurchaseOrderLineListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void dataGridViewPurchaseOrderLineList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetPurchaseOrderLineListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewPurchaseOrderLineList.CurrentCell.ColumnIndex == dataGridViewPurchaseOrderLineList.Columns["ColumnPurchaseOrderLineListButtonEdit"].Index)
            {
                var id = Convert.ToInt32(dataGridViewPurchaseOrderLineList.Rows[e.RowIndex].Cells[dataGridViewPurchaseOrderLineList.Columns["ColumnPurchaseOrderLineListId"].Index].Value);
                var purchaseOrderId = Convert.ToInt32(dataGridViewPurchaseOrderLineList.Rows[e.RowIndex].Cells[dataGridViewPurchaseOrderLineList.Columns["ColumnPurchaseOrderLineListPurchaseOrderId"].Index].Value);
                var itemId = Convert.ToInt32(dataGridViewPurchaseOrderLineList.Rows[e.RowIndex].Cells[dataGridViewPurchaseOrderLineList.Columns["ColumnPurchaseOrderLineListItemId"].Index].Value);
                var itemDescription = dataGridViewPurchaseOrderLineList.Rows[e.RowIndex].Cells[dataGridViewPurchaseOrderLineList.Columns["ColumnPurchaseOrderLineListItemDescription"].Index].Value.ToString();
                var unitId = Convert.ToInt32(dataGridViewPurchaseOrderLineList.Rows[e.RowIndex].Cells[dataGridViewPurchaseOrderLineList.Columns["ColumnPurchaseOrderLineListUnitId"].Index].Value);
                var unit = dataGridViewPurchaseOrderLineList.Rows[e.RowIndex].Cells[dataGridViewPurchaseOrderLineList.Columns["ColumnPurchaseOrderLineListUnit"].Index].Value.ToString();
                var quantity = Convert.ToDecimal(dataGridViewPurchaseOrderLineList.Rows[e.RowIndex].Cells[dataGridViewPurchaseOrderLineList.Columns["ColumnPurchaseOrderLineListQuantity"].Index].Value);
                var cost = Convert.ToDecimal(dataGridViewPurchaseOrderLineList.Rows[e.RowIndex].Cells[dataGridViewPurchaseOrderLineList.Columns["ColumnPurchaseOrderLineListCost"].Index].Value);
                var amount = Convert.ToDecimal(dataGridViewPurchaseOrderLineList.Rows[e.RowIndex].Cells[dataGridViewPurchaseOrderLineList.Columns["ColumnPurchaseOrderLineListAmount"].Index].Value);
                var receivedQuantity = Convert.ToDecimal(dataGridViewPurchaseOrderLineList.Rows[e.RowIndex].Cells[dataGridViewPurchaseOrderLineList.Columns["ColumnPurchaseOrderLineListReceivedQuantity"].Index].Value);

                Entities.TrnPurchaseOrderLineEntity trnPurchaseOrderLineEntity = new Entities.TrnPurchaseOrderLineEntity()
                {
                    Id = id,
                    PurchaseOrderId = purchaseOrderId,
                    ItemId = itemId,
                    ItemDescription = itemDescription,
                    UnitId = unitId,
                    Unit = unit,
                    Quantity = quantity,
                    Cost = cost,
                    Amount = amount,
                    ReceivedQuantity = receivedQuantity
                };

                TrnPurchaseOrderLineItemDetailForm trnPurchaseOrderDetailPurchaseOrderLineItemDetailForm = new TrnPurchaseOrderLineItemDetailForm(this, trnPurchaseOrderLineEntity);
                trnPurchaseOrderDetailPurchaseOrderLineItemDetailForm.ShowDialog();
            }

            if (e.RowIndex > -1 && dataGridViewPurchaseOrderLineList.CurrentCell.ColumnIndex == dataGridViewPurchaseOrderLineList.Columns["ColumnPurchaseOrderLineListButtonDelete"].Index)
            {
                DialogResult deleteDialogResult = MessageBox.Show("Delete Purchase Order?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (deleteDialogResult == DialogResult.Yes)
                {
                    var id = Convert.ToInt32(dataGridViewPurchaseOrderLineList.Rows[e.RowIndex].Cells[dataGridViewPurchaseOrderLineList.Columns["ColumnPurchaseOrderLineListId"].Index].Value);

                    Controllers.TrnPurchaseOrderLineController trnPurchaseOrderLineController = new Controllers.TrnPurchaseOrderLineController();
                    String[] deletePurchaseOrderLine = trnPurchaseOrderLineController.DeletePurchaseOrderLine(id);
                    if (deletePurchaseOrderLine[1].Equals("0") == false)
                    {
                        PurchaseOrderLinePageNumber = 1;
                        UpdatePurchaseOrderLineListDataSource();
                    }
                    else
                    {
                        MessageBox.Show(deletePurchaseOrderLine[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonPurchaseOrderLineListPageListFirst_Click(object sender, EventArgs e)
        {
            purchaseOrderLinePageList = new PagedList<Entities.DgvTrnPurchaseOrderLineListEntity>(purchaseOrderLineData, 1, PurchaseOrderLinePageSize);
            purchaseOrderLineDataSource.DataSource = purchaseOrderLinePageList;

            buttonPurchaseOrderLineListPageListFirst.Enabled = false;
            buttonPurchaseOrderLineListPageListPrevious.Enabled = false;
            buttonPurchaseOrderLineListPageListNext.Enabled = true;
            buttonPurchaseOrderLineListPageListLast.Enabled = true;

            PurchaseOrderLinePageNumber = 1;
            textBoxPurchaseOrderLineListPageNumber.Text = PurchaseOrderLinePageNumber + " / " + purchaseOrderLinePageList.PageCount;
        }

        private void buttonPurchaseOrderLineListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (purchaseOrderLinePageList.HasPreviousPage == true)
            {
                purchaseOrderLinePageList = new PagedList<Entities.DgvTrnPurchaseOrderLineListEntity>(purchaseOrderLineData, --PurchaseOrderLinePageNumber, PurchaseOrderLinePageSize);
                purchaseOrderLineDataSource.DataSource = purchaseOrderLinePageList;
            }

            buttonPurchaseOrderLineListPageListNext.Enabled = true;
            buttonPurchaseOrderLineListPageListLast.Enabled = true;

            if (PurchaseOrderLinePageNumber == 1)
            {
                buttonPurchaseOrderLineListPageListFirst.Enabled = false;
                buttonPurchaseOrderLineListPageListPrevious.Enabled = false;
            }

            textBoxPurchaseOrderLineListPageNumber.Text = PurchaseOrderLinePageNumber + " / " + purchaseOrderLinePageList.PageCount;
        }

        private void buttonPurchaseOrderLineListPageListNext_Click(object sender, EventArgs e)
        {
            if (purchaseOrderLinePageList.HasNextPage == true)
            {
                purchaseOrderLinePageList = new PagedList<Entities.DgvTrnPurchaseOrderLineListEntity>(purchaseOrderLineData, ++PurchaseOrderLinePageNumber, PurchaseOrderLinePageSize);
                purchaseOrderLineDataSource.DataSource = purchaseOrderLinePageList;
            }

            buttonPurchaseOrderLineListPageListFirst.Enabled = true;
            buttonPurchaseOrderLineListPageListPrevious.Enabled = true;

            if (PurchaseOrderLinePageNumber == purchaseOrderLinePageList.PageCount)
            {
                buttonPurchaseOrderLineListPageListNext.Enabled = false;
                buttonPurchaseOrderLineListPageListLast.Enabled = false;
            }

            textBoxPurchaseOrderLineListPageNumber.Text = PurchaseOrderLinePageNumber + " / " + purchaseOrderLinePageList.PageCount;
        }

        private void buttonPurchaseOrderLineListPageListLast_Click(object sender, EventArgs e)
        {

            purchaseOrderLinePageList = new PagedList<Entities.DgvTrnPurchaseOrderLineListEntity>(purchaseOrderLineData, purchaseOrderLinePageList.PageCount, PurchaseOrderLinePageSize);
            purchaseOrderLineDataSource.DataSource = purchaseOrderLinePageList;

            buttonPurchaseOrderLineListPageListFirst.Enabled = true;
            buttonPurchaseOrderLineListPageListPrevious.Enabled = true;
            buttonPurchaseOrderLineListPageListNext.Enabled = false;
            buttonPurchaseOrderLineListPageListLast.Enabled = false;

            PurchaseOrderLinePageNumber = purchaseOrderLinePageList.PageCount;
            textBoxPurchaseOrderLineListPageNumber.Text = PurchaseOrderLinePageNumber + " / " + purchaseOrderLinePageList.PageCount;
        }

        private void buttonSearchItem_Click(object sender, EventArgs e)
        {
            TrnPurchaseOrderSearchItemForm trnPurchaseOrderDetailSearchItemForm = new TrnPurchaseOrderSearchItemForm(this, trnPurchaseOrderEntity);
            trnPurchaseOrderDetailSearchItemForm.ShowDialog();
        }
        private void buttonBarcode_Click(object sender, EventArgs e)
        {
            textBoxBarcode.Focus();
            textBoxBarcode.SelectAll();
        }

        private void textBoxBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Controllers.TrnPurchaseOrderLineController trnPOSPurchaseOrderLineController = new Controllers.TrnPurchaseOrderLineController();

                if (Modules.SysCurrentModule.GetCurrentSettings().IsBarcodeQuantityAlwaysOne == true)
                {
                    trnPOSPurchaseOrderLineController.BarcodePurchaseOrderLine(trnPurchaseOrderEntity.Id, textBoxBarcode.Text);
                    UpdatePurchaseOrderLineListDataSource();
                }
                else
                {
                    Entities.MstItemEntity detailItem = trnPOSPurchaseOrderLineController.DetailSearchItem(textBoxBarcode.Text);
                    if (detailItem != null)
                    {
                        var purchaseOrderId = trnPurchaseOrderEntity.Id;
                        var itemId = detailItem.Id;
                        var itemDescription = detailItem.ItemDescription;
                        var unitId = detailItem.UnitId;
                        var unit = detailItem.Unit;

                        Entities.TrnPurchaseOrderLineEntity trnPurchaseOrderLineEntity = new Entities.TrnPurchaseOrderLineEntity()
                        {
                            Id = 0,
                            PurchaseOrderId = purchaseOrderId,
                            ItemId = itemId,
                            ItemDescription = itemDescription,
                            UnitId = unitId,
                            Unit = unit,
                            Quantity = 1,
                            Cost = 0,
                            Amount = 0,
                        };

                        TrnPurchaseOrderLineItemDetailForm trnStockInDetailStockInLineItemDetailForm = new TrnPurchaseOrderLineItemDetailForm(this, trnPurchaseOrderLineEntity);
                        trnStockInDetailStockInLineItemDetailForm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Item not found.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                textBoxBarcode.SelectAll();
            }
        }

        private void buttonStockIn_Click(object sender, EventArgs e)
        {
            DialogResult cancelDialogResult = MessageBox.Show("Add Stock-In?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cancelDialogResult == DialogResult.Yes)
            {
                Controllers.TrnPurchaseOrderController trnPurchaseOrderController = new Controllers.TrnPurchaseOrderController();
                String[] postPurchaseOrder = trnPurchaseOrderController.PostStockIn(trnPurchaseOrderEntity.Id);

                if (postPurchaseOrder[1].Equals("0") == false)
                {
                    UpdateComponents(true);

                    var trnStockInListForm = new TrnStockIn.TrnStockInListForm(sysSoftwareForm)
                    {
                        TopLevel = false,
                        Visible = true,
                        Dock = DockStyle.Fill
                    };

                    Controllers.TrnStockInController trnStockInController = new Controllers.TrnStockInController();
                    sysSoftwareForm.AddTabPageStockInDetail(trnStockInListForm, trnStockInController.DetailStockIn(Convert.ToInt32(postPurchaseOrder[1])));
                }
                else
                {
                    MessageBox.Show(postPurchaseOrder[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridViewPurchaseOrderLineList_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("HELLO");
        }

        private void dataGridViewPurchaseOrderLineList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && dataGridViewPurchaseOrderLineList.CurrentCell.ColumnIndex == dataGridViewPurchaseOrderLineList.Columns["ColumnPurchaseOrderLineListReceivedQuantity"].Index)
            {
                var id = Convert.ToInt32(dataGridViewPurchaseOrderLineList.Rows[e.RowIndex].Cells[dataGridViewPurchaseOrderLineList.Columns["ColumnPurchaseOrderLineListId"].Index].Value);
                var receivedQuantity = Convert.ToDecimal(dataGridViewPurchaseOrderLineList.Rows[e.RowIndex].Cells[dataGridViewPurchaseOrderLineList.Columns["ColumnPurchaseOrderLineListReceivedQuantity"].Index].Value);

                Controllers.TrnPurchaseOrderLineController trnPurchaseOrderLineController = new Controllers.TrnPurchaseOrderLineController();
                trnPurchaseOrderLineController.UpdatePurchaseOrderLineReceivedQuantity(id, receivedQuantity);
            }
        }

        private void dataGridViewPurchaseOrderLineList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(ColumnPurchaseOrderLineListReceivedQuantityKeyPress);

            if (dataGridViewPurchaseOrderLineList.CurrentCell.ColumnIndex == dataGridViewPurchaseOrderLineList.Columns["ColumnPurchaseOrderLineListReceivedQuantity"].Index)
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(ColumnPurchaseOrderLineListReceivedQuantityKeyPress);
                }
            }
        }

        private void ColumnPurchaseOrderLineListReceivedQuantityKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '-') && ((sender as TextBox).Text.IndexOf('-') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
