using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.TrnStockCount
{
    public partial class TrnStockCountDetailForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;
        public TrnStockCountListForm trnStockCountListForm;
        public Entities.TrnStockCountEntity trnStockCountEntity;

        public static List<Entities.DgvTrnStockCountLineListEntity> stockOutLineData = new List<Entities.DgvTrnStockCountLineListEntity>();
        public static Int32 stockOutLinePageNumber = 1;
        public static Int32 stockOutLinePageSize = 50;
        public PagedList<Entities.DgvTrnStockCountLineListEntity> stockOutLinePageList = new PagedList<Entities.DgvTrnStockCountLineListEntity>(stockOutLineData, stockOutLinePageNumber, stockOutLinePageSize);
        public BindingSource stockOutLineDataSource = new BindingSource();

        public TrnStockCountDetailForm(SysSoftwareForm softwareForm, TrnStockCountListForm stockOutListForm, Entities.TrnStockCountEntity stockOutEntity)
        {
            InitializeComponent();

            sysSoftwareForm = softwareForm;

            sysUserRights = new Modules.SysUserRightsModule("TrnStockCountDetail");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                trnStockCountListForm = stockOutListForm;
                trnStockCountEntity = stockOutEntity;

                GetUserList();
            }
            var id = trnStockCountEntity.Id;

            Controllers.TrnStockCountController trnStockCount = new Controllers.TrnStockCountController();
            var detail = trnStockCount.DetailStockCount(id);

            if (detail != null)
            {
                sysSoftwareForm.displayTimeStamp(detail.EntryUserUserName, detail.EntryDateTime + " " + detail.EntryTime, detail.UpdateUserUserName, detail.UpdateDateTime + " " + detail.UpdateTime);
            }
        }

        public void GetUserList()
        {
            Controllers.TrnStockCountController trnStockCountController = new Controllers.TrnStockCountController();
            if (trnStockCountController.DropdownListStockCountUser().Any())
            {
                comboBoxPreparedBy.DataSource = trnStockCountController.DropdownListStockCountUser();
                comboBoxPreparedBy.ValueMember = "Id";
                comboBoxPreparedBy.DisplayMember = "FullName";

                comboBoxCheckedBy.DataSource = trnStockCountController.DropdownListStockCountUser();
                comboBoxCheckedBy.ValueMember = "Id";
                comboBoxCheckedBy.DisplayMember = "FullName";

                comboBoxApprovedBy.DataSource = trnStockCountController.DropdownListStockCountUser();
                comboBoxApprovedBy.ValueMember = "Id";
                comboBoxApprovedBy.DisplayMember = "FullName";

                GetStockCountDetail();
            }
        }

        public void GetStockCountDetail()
        {
            UpdateComponents(trnStockCountEntity.IsLocked);

            textBoxStockCountNumber.Text = trnStockCountEntity.StockCountNumber;
            dateTimePickerStockCountDate.Value = Convert.ToDateTime(trnStockCountEntity.StockCountDate);
            textBoxRemarks.Text = trnStockCountEntity.Remarks;
            comboBoxPreparedBy.SelectedValue = trnStockCountEntity.PreparedBy;
            comboBoxCheckedBy.SelectedValue = trnStockCountEntity.CheckedBy;
            comboBoxApprovedBy.SelectedValue = trnStockCountEntity.ApprovedBy;

            CreateStockCountLineListDataGridView();
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
                buttonPost.Enabled = isLocked;
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

            dateTimePickerStockCountDate.Enabled = !isLocked;
            textBoxRemarks.Enabled = !isLocked;
            comboBoxCheckedBy.Enabled = !isLocked;
            comboBoxApprovedBy.Enabled = !isLocked;

            dataGridViewStockCountLineList.Columns[dataGridViewStockCountLineList.Columns["ColumnStockCountLineListButtonEdit"].Index].Visible = !isLocked;
            dataGridViewStockCountLineList.Columns[dataGridViewStockCountLineList.Columns["ColumnStockCountLineListButtonDelete"].Index].Visible = !isLocked;
        }

        private void buttonLock_Click(object sender, EventArgs e)
        {
            Controllers.TrnStockCountController trnStockCountController = new Controllers.TrnStockCountController();

            Entities.TrnStockCountEntity newStockCountEntity = new Entities.TrnStockCountEntity()
            {
                StockCountDate = dateTimePickerStockCountDate.Value.Date.ToShortDateString(),
                Remarks = textBoxRemarks.Text,
                CheckedBy = Convert.ToInt32(comboBoxCheckedBy.SelectedValue),
                ApprovedBy = Convert.ToInt32(comboBoxApprovedBy.SelectedValue)
            };

            String[] lockStockCount = trnStockCountController.LockStockCount(trnStockCountEntity.Id, newStockCountEntity);
            if (lockStockCount[1].Equals("0") == false)
            {
                UpdateComponents(true);
                trnStockCountListForm.UpdateStockCountListDataSource();
            }
            else
            {
                MessageBox.Show(lockStockCount[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUnlock_Click(object sender, EventArgs e)
        {
            Controllers.TrnStockCountController trnStockCountController = new Controllers.TrnStockCountController();

            String[] unlockStockCount = trnStockCountController.UnlockStockCount(trnStockCountEntity.Id);
            if (unlockStockCount[1].Equals("0") == false)
            {
                UpdateComponents(false);
                trnStockCountListForm.UpdateStockCountListDataSource();
            }
            else
            {
                MessageBox.Show(unlockStockCount[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            new TrnStockCountDetailReportForm(trnStockCountEntity.Id);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            sysSoftwareForm.RemoveTabPage();
        }

        public void UpdateStockCountLineListDataSource()
        {
            SetStockCountLineListDataSourceAsync();
        }

        public async void SetStockCountLineListDataSourceAsync()
        {
            List<Entities.DgvTrnStockCountLineListEntity> getStockCountLineListData = await GetStockCountLineListDataTask();
            if (getStockCountLineListData.Any())
            {
                stockOutLineData = getStockCountLineListData;
                stockOutLinePageList = new PagedList<Entities.DgvTrnStockCountLineListEntity>(stockOutLineData, stockOutLinePageNumber, stockOutLinePageSize);

                if (stockOutLinePageList.PageCount == 1)
                {
                    buttonStockCountLineListPageListFirst.Enabled = false;
                    buttonStockCountLineListPageListPrevious.Enabled = false;
                    buttonStockCountLineListPageListNext.Enabled = false;
                    buttonStockCountLineListPageListLast.Enabled = false;
                }
                else if (stockOutLinePageNumber == 1)
                {
                    buttonStockCountLineListPageListFirst.Enabled = false;
                    buttonStockCountLineListPageListPrevious.Enabled = false;
                    buttonStockCountLineListPageListNext.Enabled = true;
                    buttonStockCountLineListPageListLast.Enabled = true;
                }
                else if (stockOutLinePageNumber == stockOutLinePageList.PageCount)
                {
                    buttonStockCountLineListPageListFirst.Enabled = true;
                    buttonStockCountLineListPageListPrevious.Enabled = true;
                    buttonStockCountLineListPageListNext.Enabled = false;
                    buttonStockCountLineListPageListLast.Enabled = false;
                }
                else
                {
                    buttonStockCountLineListPageListFirst.Enabled = true;
                    buttonStockCountLineListPageListPrevious.Enabled = true;
                    buttonStockCountLineListPageListNext.Enabled = true;
                    buttonStockCountLineListPageListLast.Enabled = true;
                }

                textBoxStockCountLineListPageNumber.Text = stockOutLinePageNumber + " / " + stockOutLinePageList.PageCount;
                stockOutLineDataSource.DataSource = stockOutLinePageList;
            }
            else
            {
                buttonStockCountLineListPageListFirst.Enabled = false;
                buttonStockCountLineListPageListPrevious.Enabled = false;
                buttonStockCountLineListPageListNext.Enabled = false;
                buttonStockCountLineListPageListLast.Enabled = false;

                stockOutLinePageNumber = 1;

                stockOutLineData = new List<Entities.DgvTrnStockCountLineListEntity>();
                stockOutLineDataSource.Clear();
                textBoxStockCountLineListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvTrnStockCountLineListEntity>> GetStockCountLineListDataTask()
        {
            Controllers.TrnStockCountLineController trnStockCountLineController = new Controllers.TrnStockCountLineController();

            List<Entities.TrnStockCountLineEntity> listStockCountLine = trnStockCountLineController.ListStockCountLine(trnStockCountEntity.Id);
            if (listStockCountLine.Any())
            {
                var items = from d in listStockCountLine
                            select new Entities.DgvTrnStockCountLineListEntity
                            {
                                ColumnStockCountLineListButtonEdit = "Edit",
                                ColumnStockCountLineListButtonDelete = "Delete",
                                ColumnStockCountLineListId = d.Id,
                                ColumnStockCountLineListStockCountId = d.StockCountId,
                                ColumnStockCountLineListItemId = d.ItemId,
                                ColumnStockCountLineListItemBarcode = d.ItemBarcode,
                                ColumnStockCountLineListItemDescription = d.ItemDescription,
                                ColumnStockCountLineListUnitId = d.UnitId,
                                ColumnStockCountLineListUnit = d.Unit,
                                ColumnStockCountLineListQuantity = d.Quantity.ToString("#,##0.00"),
                                ColumnStockCountLineListCost = d.Cost.ToString("#,##0.00"),
                                ColumnStockCountLineListAmount = d.Amount.ToString("#,##0.00"),
                            };

                return Task.FromResult(items.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvTrnStockCountLineListEntity>());
            }
        }

        public void CreateStockCountLineListDataGridView()
        {
            UpdateStockCountLineListDataSource();

            dataGridViewStockCountLineList.Columns[dataGridViewStockCountLineList.Columns["ColumnStockCountLineListButtonEdit"].Index].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewStockCountLineList.Columns[dataGridViewStockCountLineList.Columns["ColumnStockCountLineListButtonEdit"].Index].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewStockCountLineList.Columns[dataGridViewStockCountLineList.Columns["ColumnStockCountLineListButtonEdit"].Index].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewStockCountLineList.Columns[dataGridViewStockCountLineList.Columns["ColumnStockCountLineListButtonDelete"].Index].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewStockCountLineList.Columns[dataGridViewStockCountLineList.Columns["ColumnStockCountLineListButtonDelete"].Index].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewStockCountLineList.Columns[dataGridViewStockCountLineList.Columns["ColumnStockCountLineListButtonDelete"].Index].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewStockCountLineList.DataSource = stockOutLineDataSource;
        }

        public void GetStockCountLineListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void dataGridViewStockCountLineList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetStockCountLineListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewStockCountLineList.CurrentCell.ColumnIndex == dataGridViewStockCountLineList.Columns["ColumnStockCountLineListButtonEdit"].Index)
            {
                var id = Convert.ToInt32(dataGridViewStockCountLineList.Rows[e.RowIndex].Cells[dataGridViewStockCountLineList.Columns["ColumnStockCountLineListId"].Index].Value);
                var stockOutId = Convert.ToInt32(dataGridViewStockCountLineList.Rows[e.RowIndex].Cells[dataGridViewStockCountLineList.Columns["ColumnStockCountLineListStockCountId"].Index].Value);
                var itemId = Convert.ToInt32(dataGridViewStockCountLineList.Rows[e.RowIndex].Cells[dataGridViewStockCountLineList.Columns["ColumnStockCountLineListItemId"].Index].Value);
                var itemDescription = dataGridViewStockCountLineList.Rows[e.RowIndex].Cells[dataGridViewStockCountLineList.Columns["ColumnStockCountLineListItemDescription"].Index].Value.ToString();
                var unitId = Convert.ToInt32(dataGridViewStockCountLineList.Rows[e.RowIndex].Cells[dataGridViewStockCountLineList.Columns["ColumnStockCountLineListUnitId"].Index].Value);
                var unit = dataGridViewStockCountLineList.Rows[e.RowIndex].Cells[dataGridViewStockCountLineList.Columns["ColumnStockCountLineListUnit"].Index].Value.ToString();
                var quantity = Convert.ToDecimal(dataGridViewStockCountLineList.Rows[e.RowIndex].Cells[dataGridViewStockCountLineList.Columns["ColumnStockCountLineListQuantity"].Index].Value);
                var cost = Convert.ToDecimal(dataGridViewStockCountLineList.Rows[e.RowIndex].Cells[dataGridViewStockCountLineList.Columns["ColumnStockCountLineListCost"].Index].Value);
                var amount = Convert.ToDecimal(dataGridViewStockCountLineList.Rows[e.RowIndex].Cells[dataGridViewStockCountLineList.Columns["ColumnStockCountLineListAmount"].Index].Value);

                Entities.TrnStockCountLineEntity trnStockCountLineEntity = new Entities.TrnStockCountLineEntity()
                {
                    Id = id,
                    StockCountId = stockOutId,
                    ItemId = itemId,
                    ItemDescription = itemDescription,
                    UnitId = unitId,
                    Unit = unit,
                    Quantity = quantity,
                    Cost = cost,
                    Amount = amount
                };

                TrnStockCountLineItemDetailForm trnStockCountLineItemDetailForm = new TrnStockCountLineItemDetailForm(this, trnStockCountLineEntity);
                trnStockCountLineItemDetailForm.ShowDialog();
            }

            if (e.RowIndex > -1 && dataGridViewStockCountLineList.CurrentCell.ColumnIndex == dataGridViewStockCountLineList.Columns["ColumnStockCountLineListButtonDelete"].Index)
            {
                DialogResult deleteDialogResult = MessageBox.Show("Delete Stock-In?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (deleteDialogResult == DialogResult.Yes)
                {
                    var id = Convert.ToInt32(dataGridViewStockCountLineList.Rows[e.RowIndex].Cells[dataGridViewStockCountLineList.Columns["ColumnStockCountLineListId"].Index].Value);

                    Controllers.TrnStockCountLineController trnStockCountLineController = new Controllers.TrnStockCountLineController();
                    String[] deleteStockCountLine = trnStockCountLineController.DeleteStockCountLine(id);
                    if (deleteStockCountLine[1].Equals("0") == false)
                    {
                        stockOutLinePageNumber = 1;
                        UpdateStockCountLineListDataSource();
                    }
                    else
                    {
                        MessageBox.Show(deleteStockCountLine[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonStockCountLineListPageListFirst_Click(object sender, EventArgs e)
        {
            stockOutLinePageList = new PagedList<Entities.DgvTrnStockCountLineListEntity>(stockOutLineData, 1, stockOutLinePageSize);
            stockOutLineDataSource.DataSource = stockOutLinePageList;

            buttonStockCountLineListPageListFirst.Enabled = false;
            buttonStockCountLineListPageListPrevious.Enabled = false;
            buttonStockCountLineListPageListNext.Enabled = true;
            buttonStockCountLineListPageListLast.Enabled = true;

            stockOutLinePageNumber = 1;
            textBoxStockCountLineListPageNumber.Text = stockOutLinePageNumber + " / " + stockOutLinePageList.PageCount;
        }

        private void buttonStockCountLineListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (stockOutLinePageList.HasPreviousPage == true)
            {
                stockOutLinePageList = new PagedList<Entities.DgvTrnStockCountLineListEntity>(stockOutLineData, --stockOutLinePageNumber, stockOutLinePageSize);
                stockOutLineDataSource.DataSource = stockOutLinePageList;
            }

            buttonStockCountLineListPageListNext.Enabled = true;
            buttonStockCountLineListPageListLast.Enabled = true;

            if (stockOutLinePageNumber == 1)
            {
                buttonStockCountLineListPageListFirst.Enabled = false;
                buttonStockCountLineListPageListPrevious.Enabled = false;
            }

            textBoxStockCountLineListPageNumber.Text = stockOutLinePageNumber + " / " + stockOutLinePageList.PageCount;
        }

        private void buttonStockCountLineListPageListNext_Click(object sender, EventArgs e)
        {
            if (stockOutLinePageList.HasNextPage == true)
            {
                stockOutLinePageList = new PagedList<Entities.DgvTrnStockCountLineListEntity>(stockOutLineData, ++stockOutLinePageNumber, stockOutLinePageSize);
                stockOutLineDataSource.DataSource = stockOutLinePageList;
            }

            buttonStockCountLineListPageListFirst.Enabled = true;
            buttonStockCountLineListPageListPrevious.Enabled = true;

            if (stockOutLinePageNumber == stockOutLinePageList.PageCount)
            {
                buttonStockCountLineListPageListNext.Enabled = false;
                buttonStockCountLineListPageListLast.Enabled = false;
            }

            textBoxStockCountLineListPageNumber.Text = stockOutLinePageNumber + " / " + stockOutLinePageList.PageCount;
        }

        private void buttonStockCountLineListPageListLast_Click(object sender, EventArgs e)
        {
            stockOutLinePageList = new PagedList<Entities.DgvTrnStockCountLineListEntity>(stockOutLineData, stockOutLinePageList.PageCount, stockOutLinePageSize);
            stockOutLineDataSource.DataSource = stockOutLinePageList;

            buttonStockCountLineListPageListFirst.Enabled = true;
            buttonStockCountLineListPageListPrevious.Enabled = true;
            buttonStockCountLineListPageListNext.Enabled = false;
            buttonStockCountLineListPageListLast.Enabled = false;

            stockOutLinePageNumber = stockOutLinePageList.PageCount;
            textBoxStockCountLineListPageNumber.Text = stockOutLinePageNumber + " / " + stockOutLinePageList.PageCount;
        }

        private void buttonSearchItem_Click(object sender, EventArgs e)
        {
            TrnStockCountSearchItemForm trnStockCountDetailSearchItemForm = new TrnStockCountSearchItemForm(this, trnStockCountEntity);
            trnStockCountDetailSearchItemForm.ShowDialog();
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
                Controllers.TrnStockCountLineController trnPOSStockCountLineController = new Controllers.TrnStockCountLineController();

                if (Modules.SysCurrentModule.GetCurrentSettings().IsBarcodeQuantityAlwaysOne == true)
                {
                    trnPOSStockCountLineController.BarcodeStockCountLine(trnStockCountEntity.Id, textBoxBarcode.Text);
                    UpdateStockCountLineListDataSource();
                }
                else
                {
                    Entities.MstItemEntity detailItem = trnPOSStockCountLineController.DetailSearchItem(textBoxBarcode.Text);
                    if (detailItem != null)
                    {
                        var stockOutId = trnStockCountEntity.Id;
                        var itemId = detailItem.Id;
                        var itemDescription = detailItem.ItemDescription;
                        var unitId = detailItem.UnitId;
                        var unit = detailItem.Unit;
                        var price = detailItem.Price;

                        Entities.TrnStockCountLineEntity trnStockCountLineEntity = new Entities.TrnStockCountLineEntity()
                        {
                            Id = 0,
                            StockCountId = stockOutId,
                            ItemId = itemId,
                            ItemDescription = itemDescription,
                            UnitId = unitId,
                            Unit = unit,
                            Quantity = 1,
                            Cost = 0,
                            Amount = 0
                        };

                        TrnStockCountLineItemDetailForm trnStockCountLineItemDetailForm = new TrnStockCountLineItemDetailForm(this, trnStockCountLineEntity);
                        trnStockCountLineItemDetailForm.ShowDialog();
                    }
                    else
                {
                        MessageBox.Show("Item not found.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                textBoxBarcode.SelectAll();
            }
        }

        private void buttonPost_Click(object sender, EventArgs e)
        {
            DialogResult cancelDialogResult = MessageBox.Show("Post Inventory?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cancelDialogResult == DialogResult.Yes)
            {
                Controllers.TrnStockCountController trnStockCountController = new Controllers.TrnStockCountController();
                String[] postStockCount = trnStockCountController.PostStockCount(trnStockCountEntity.Id);

                if (postStockCount[1].Equals("0") == false)
                {
                    UpdateComponents(true);

                    var trnStockOutListForm = new TrnStockOut.TrnStockOutListForm(sysSoftwareForm)
                    {
                        TopLevel = false,
                        Visible = true,
                        Dock = DockStyle.Fill
                    };

                    Controllers.TrnStockOutController trnStockOutController = new Controllers.TrnStockOutController();
                    sysSoftwareForm.AddTabPageStockOutDetail(trnStockOutListForm, trnStockOutController.DetailStockOut(Convert.ToInt32(postStockCount[1])));
                }
                else
                {
                    MessageBox.Show(postStockCount[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = folderBrowserDialogGenerateCSV.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    StringBuilder csv = new StringBuilder();
                    String[] header = {
                        "Barcode",
                        "Item Description",
                        "Unit",
                        "Quantity",
                        "Cost",
                        "Amount",
                    };

                    csv.AppendLine(String.Join(",", header));

                    if (stockOutLineData.Any())
                    {
                        foreach (var stockOutLine in stockOutLineData)
                        {
                            String[] data = {
                              stockOutLine.ColumnStockCountLineListItemBarcode,
                              stockOutLine.ColumnStockCountLineListItemDescription.Replace(",", ""),
                              stockOutLine.ColumnStockCountLineListUnit,
                              stockOutLine.ColumnStockCountLineListQuantity.Replace(",", ""),
                              stockOutLine.ColumnStockCountLineListCost.Replace(",", ""),
                              stockOutLine.ColumnStockCountLineListAmount .Replace(",", "")
                            };

                            csv.AppendLine(String.Join(",", data));
                        }
                    }

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\StockCountLine_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("iso-8859-1"));

                    MessageBox.Show("Generate CSV Successful!", "Generate CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewStockCountLineList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            TrnStockCountLineDetailImportForm stockCountDetailImportForm = new TrnStockCountLineDetailImportForm(this);
            stockCountDetailImportForm.ShowDialog();
        }
    }
}
