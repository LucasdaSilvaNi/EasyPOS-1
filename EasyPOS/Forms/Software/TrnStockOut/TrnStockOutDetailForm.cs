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

namespace EasyPOS.Forms.Software.TrnStockOut
{
    public partial class TrnStockOutDetailForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public TrnStockOutListForm trnStockOutListForm;
        public Entities.TrnStockOutEntity trnStockOutEntity;

        public static List<Entities.DgvTrnStockOutLineListEntity> stockOutLineData = new List<Entities.DgvTrnStockOutLineListEntity>();
        public static Int32 stockOutLinePageNumber = 1;
        public static Int32 stockOutLinePageSize = 50;
        public PagedList<Entities.DgvTrnStockOutLineListEntity> stockOutLinePageList = new PagedList<Entities.DgvTrnStockOutLineListEntity>(stockOutLineData, stockOutLinePageNumber, stockOutLinePageSize);
        public BindingSource stockOutLineDataSource = new BindingSource();

        public TrnStockOutDetailForm(SysSoftwareForm softwareForm, TrnStockOutListForm stockOutListForm, Entities.TrnStockOutEntity stockOutEntity)
        {
            InitializeComponent();
            sysSoftwareForm = softwareForm;

            sysUserRights = new Modules.SysUserRightsModule("TrnStockOutDetail");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                trnStockOutListForm = stockOutListForm;
                trnStockOutEntity = stockOutEntity;

                GetAccountList();
            }
            var id = trnStockOutEntity.Id;

            Controllers.TrnStockOutController trnStockOut = new Controllers.TrnStockOutController();
            var detail = trnStockOut.DetailStockOut(id);

            if (detail != null)
            {
                sysSoftwareForm.displayTimeStamp(detail.EntryUserName, detail.EntryDateTime + " " + detail.EntryTime, detail.UpdateUserName, detail.UpdateDateTime + " " + detail.UpdateTime);
            }

        }

        public void GetAccountList()
        {
            Controllers.TrnStockOutController trnStockOutController = new Controllers.TrnStockOutController();
            if (trnStockOutController.DropdownListStockOutAccount().Any())
            {
                comboBoxAccount.DataSource = trnStockOutController.DropdownListStockOutAccount();
                comboBoxAccount.ValueMember = "Id";
                comboBoxAccount.DisplayMember = "Account";

                GetUserList();
            }
        }

        public void GetUserList()
        {
            Controllers.TrnStockOutController trnStockOutController = new Controllers.TrnStockOutController();
            if (trnStockOutController.DropdownListStockOutUser().Any())
            {
                comboBoxPreparedBy.DataSource = trnStockOutController.DropdownListStockOutUser();
                comboBoxPreparedBy.ValueMember = "Id";
                comboBoxPreparedBy.DisplayMember = "FullName";

                comboBoxCheckedBy.DataSource = trnStockOutController.DropdownListStockOutUser();
                comboBoxCheckedBy.ValueMember = "Id";
                comboBoxCheckedBy.DisplayMember = "FullName";

                comboBoxApprovedBy.DataSource = trnStockOutController.DropdownListStockOutUser();
                comboBoxApprovedBy.ValueMember = "Id";
                comboBoxApprovedBy.DisplayMember = "FullName";

                GetStockOutDetail();
            }
        }

        public void GetStockOutDetail()
        {
            UpdateComponents(trnStockOutEntity.IsLocked);

            textBoxManualStockOutNumber.Text = trnStockOutEntity.ManualStockOutNumber;
            textBoxStockOutNumber.Text = trnStockOutEntity.StockOutNumber;
            dateTimePickerStockOutDate.Value = Convert.ToDateTime(trnStockOutEntity.StockOutDate);
            comboBoxAccount.SelectedValue = trnStockOutEntity.AccountId;
            textBoxRemarks.Text = trnStockOutEntity.Remarks;
            comboBoxPreparedBy.SelectedValue = trnStockOutEntity.PreparedBy;
            comboBoxCheckedBy.SelectedValue = trnStockOutEntity.CheckedBy;
            comboBoxApprovedBy.SelectedValue = trnStockOutEntity.ApprovedBy;

            CreateStockOutLineListDataGridView();
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

            textBoxManualStockOutNumber.Enabled = !isLocked;
            dateTimePickerStockOutDate.Enabled = !isLocked;
            comboBoxAccount.Enabled = !isLocked;
            textBoxRemarks.Enabled = !isLocked;
            comboBoxCheckedBy.Enabled = !isLocked;
            comboBoxApprovedBy.Enabled = !isLocked;

            dataGridViewStockOutLineList.Columns[0].Visible = !isLocked;
            dataGridViewStockOutLineList.Columns[1].Visible = !isLocked;
            dateTimePickerStockOutDate.Focus();
        }

        private void buttonLock_Click(object sender, EventArgs e)
        {
            Controllers.TrnStockOutController trnStockOutController = new Controllers.TrnStockOutController();

            Entities.TrnStockOutEntity newStockOutEntity = new Entities.TrnStockOutEntity()
            {
                ManualStockOutNumber = textBoxManualStockOutNumber.Text,
                StockOutDate = dateTimePickerStockOutDate.Value.Date.ToShortDateString(),
                AccountId = Convert.ToInt32(comboBoxAccount.SelectedValue),
                Remarks = textBoxRemarks.Text,
                CheckedBy = Convert.ToInt32(comboBoxCheckedBy.SelectedValue),
                ApprovedBy = Convert.ToInt32(comboBoxApprovedBy.SelectedValue)
            };

            String[] lockStockOut = trnStockOutController.LockStockOut(trnStockOutEntity.Id, newStockOutEntity);
            if (lockStockOut[1].Equals("0") == false)
            {
                UpdateComponents(true);
                trnStockOutListForm.UpdateStockOutListDataSource();
            }
            else
            {
                MessageBox.Show(lockStockOut[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUnlock_Click(object sender, EventArgs e)
        {
            Controllers.TrnStockOutController trnStockOutController = new Controllers.TrnStockOutController();

            String[] unlockStockOut = trnStockOutController.UnlockStockOut(trnStockOutEntity.Id);
            if (unlockStockOut[1].Equals("0") == false)
            {
                UpdateComponents(false);
                trnStockOutListForm.UpdateStockOutListDataSource();
            }
            else
            {
                MessageBox.Show(unlockStockOut[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            new TrnStockOutDetailReportForm(trnStockOutEntity.Id);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            sysSoftwareForm.RemoveTabPage();
        }

        public void UpdateStockOutLineListDataSource()
        {
            SetStockOutLineListDataSourceAsync();
        }

        public async void SetStockOutLineListDataSourceAsync()
        {
            List<Entities.DgvTrnStockOutLineListEntity> getStockOutLineListData = await GetStockOutLineListDataTask();
            if (getStockOutLineListData.Any())
            {
                stockOutLineData = getStockOutLineListData;
                stockOutLinePageList = new PagedList<Entities.DgvTrnStockOutLineListEntity>(stockOutLineData, stockOutLinePageNumber, stockOutLinePageSize);

                if (stockOutLinePageList.PageCount == 1)
                {
                    buttonStockOutLineListPageListFirst.Enabled = false;
                    buttonStockOutLineListPageListPrevious.Enabled = false;
                    buttonStockOutLineListPageListNext.Enabled = false;
                    buttonStockOutLineListPageListLast.Enabled = false;
                }
                else if (stockOutLinePageNumber == 1)
                {
                    buttonStockOutLineListPageListFirst.Enabled = false;
                    buttonStockOutLineListPageListPrevious.Enabled = false;
                    buttonStockOutLineListPageListNext.Enabled = true;
                    buttonStockOutLineListPageListLast.Enabled = true;
                }
                else if (stockOutLinePageNumber == stockOutLinePageList.PageCount)
                {
                    buttonStockOutLineListPageListFirst.Enabled = true;
                    buttonStockOutLineListPageListPrevious.Enabled = true;
                    buttonStockOutLineListPageListNext.Enabled = false;
                    buttonStockOutLineListPageListLast.Enabled = false;
                }
                else
                {
                    buttonStockOutLineListPageListFirst.Enabled = true;
                    buttonStockOutLineListPageListPrevious.Enabled = true;
                    buttonStockOutLineListPageListNext.Enabled = true;
                    buttonStockOutLineListPageListLast.Enabled = true;
                }

                textBoxStockOutLineListPageNumber.Text = stockOutLinePageNumber + " / " + stockOutLinePageList.PageCount;
                stockOutLineDataSource.DataSource = stockOutLinePageList;
            }
            else
            {
                buttonStockOutLineListPageListFirst.Enabled = false;
                buttonStockOutLineListPageListPrevious.Enabled = false;
                buttonStockOutLineListPageListNext.Enabled = false;
                buttonStockOutLineListPageListLast.Enabled = false;

                stockOutLinePageNumber = 1;

                stockOutLineData = new List<Entities.DgvTrnStockOutLineListEntity>();
                stockOutLineDataSource.Clear();
                textBoxStockOutLineListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvTrnStockOutLineListEntity>> GetStockOutLineListDataTask()
        {
            Controllers.TrnStockOutLineController trnStockOutLineController = new Controllers.TrnStockOutLineController();

            List<Entities.TrnStockOutLineEntity> listStockOutLine = trnStockOutLineController.ListStockOutLine(trnStockOutEntity.Id);
            if (listStockOutLine.Any())
            {
                var items = from d in listStockOutLine
                            select new Entities.DgvTrnStockOutLineListEntity
                            {
                                ColumnStockOutLineListButtonEdit = "Edit",
                                ColumnStockOutLineListButtonDelete = "Delete",
                                ColumnStockOutLineListId = d.Id,
                                ColumnStockOutLineListStockOutId = d.StockOutId,
                                ColumnStockOutLineListItemBarcode = d.ItemBarcode,
                                ColumnStockOutLineListItemId = d.ItemId,
                                ColumnStockOutLineListItemDescription = d.ItemDescription,
                                ColumnStockOutLineListUnitId = d.UnitId,
                                ColumnStockOutLineListUnit = d.Unit,
                                ColumnStockOutLineListQuantity = d.Quantity.ToString("#,##0.00"),
                                ColumnStockOutLineListCost = d.Cost.ToString("#,##0.00"),
                                ColumnStockOutLineListAmount = d.Amount.ToString("#,##0.00"),
                                ColumnStockOutLineListPrice = d.Price.ToString("#,##0.00"),
                                ColumnStockOutLineListAssetAccountId = d.AssetAccountId,
                                ColumnStockOutLineListAssetAccount = d.AssetAccount
                            };

                return Task.FromResult(items.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvTrnStockOutLineListEntity>());
            }
        }

        public void CreateStockOutLineListDataGridView()
        {
            UpdateStockOutLineListDataSource();

            dataGridViewStockOutLineList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewStockOutLineList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewStockOutLineList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewStockOutLineList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewStockOutLineList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewStockOutLineList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewStockOutLineList.DataSource = stockOutLineDataSource;
        }

        public void GetStockOutLineListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void dataGridViewStockOutLineList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetStockOutLineListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewStockOutLineList.CurrentCell.ColumnIndex == dataGridViewStockOutLineList.Columns["ColumnStockOutLineListButtonEdit"].Index)
            {
                var id = Convert.ToInt32(dataGridViewStockOutLineList.Rows[e.RowIndex].Cells[dataGridViewStockOutLineList.Columns["ColumnStockOutLineListId"].Index].Value);
                var stockOutId = Convert.ToInt32(dataGridViewStockOutLineList.Rows[e.RowIndex].Cells[dataGridViewStockOutLineList.Columns["ColumnStockOutLineListStockOutId"].Index].Value);
                var itemId = Convert.ToInt32(dataGridViewStockOutLineList.Rows[e.RowIndex].Cells[dataGridViewStockOutLineList.Columns["ColumnStockOutLineListItemId"].Index].Value);
                var itemDescription = dataGridViewStockOutLineList.Rows[e.RowIndex].Cells[dataGridViewStockOutLineList.Columns["ColumnStockOutLineListItemDescription"].Index].Value.ToString();
                var unitId = Convert.ToInt32(dataGridViewStockOutLineList.Rows[e.RowIndex].Cells[dataGridViewStockOutLineList.Columns["ColumnStockOutLineListUnitId"].Index].Value);
                var unit = dataGridViewStockOutLineList.Rows[e.RowIndex].Cells[dataGridViewStockOutLineList.Columns["ColumnStockOutLineListUnit"].Index].Value.ToString();
                var quantity = Convert.ToDecimal(dataGridViewStockOutLineList.Rows[e.RowIndex].Cells[dataGridViewStockOutLineList.Columns["ColumnStockOutLineListQuantity"].Index].Value);
                var cost = Convert.ToDecimal(dataGridViewStockOutLineList.Rows[e.RowIndex].Cells[dataGridViewStockOutLineList.Columns["ColumnStockOutLineListCost"].Index].Value);
                var amount = Convert.ToDecimal(dataGridViewStockOutLineList.Rows[e.RowIndex].Cells[dataGridViewStockOutLineList.Columns["ColumnStockOutLineListAmount"].Index].Value);
                var assetAccountId = Convert.ToInt32(dataGridViewStockOutLineList.Rows[e.RowIndex].Cells[dataGridViewStockOutLineList.Columns["ColumnStockOutLineListAssetAccountId"].Index].Value);
                var assetAccount = dataGridViewStockOutLineList.Rows[e.RowIndex].Cells[dataGridViewStockOutLineList.Columns["ColumnStockOutLineListAssetAccount"].Index].Value.ToString();

                Entities.TrnStockOutLineEntity trnStockOutLineEntity = new Entities.TrnStockOutLineEntity()
                {
                    Id = id,
                    StockOutId = stockOutId,
                    ItemId = itemId,
                    ItemDescription = itemDescription,
                    UnitId = unitId,
                    Unit = unit,
                    Quantity = quantity,
                    Cost = cost,
                    Amount = amount,
                    AssetAccountId = assetAccountId,
                    AssetAccount = assetAccount
                };

                TrnStockOutLineItemDetailForm trnStockOutDetailStockOutLineItemDetailForm = new TrnStockOutLineItemDetailForm(this, trnStockOutLineEntity);
                trnStockOutDetailStockOutLineItemDetailForm.ShowDialog();
            }

            if (e.RowIndex > -1 && dataGridViewStockOutLineList.CurrentCell.ColumnIndex == dataGridViewStockOutLineList.Columns["ColumnStockOutLineListButtonDelete"].Index)
            {
                DialogResult deleteDialogResult = MessageBox.Show("Delete Stock-In?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (deleteDialogResult == DialogResult.Yes)
                {
                    var id = Convert.ToInt32(dataGridViewStockOutLineList.Rows[e.RowIndex].Cells[dataGridViewStockOutLineList.Columns["ColumnStockOutLineListId"].Index].Value);

                    Controllers.TrnStockOutLineController trnStockOutLineController = new Controllers.TrnStockOutLineController();
                    String[] deleteStockOutLine = trnStockOutLineController.DeleteStockOutLine(id);
                    if (deleteStockOutLine[1].Equals("0") == false)
                    {
                        stockOutLinePageNumber = 1;
                        UpdateStockOutLineListDataSource();
                    }
                    else
                    {
                        MessageBox.Show(deleteStockOutLine[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonStockOutLineListPageListFirst_Click(object sender, EventArgs e)
        {
            stockOutLinePageList = new PagedList<Entities.DgvTrnStockOutLineListEntity>(stockOutLineData, 1, stockOutLinePageSize);
            stockOutLineDataSource.DataSource = stockOutLinePageList;

            buttonStockOutLineListPageListFirst.Enabled = false;
            buttonStockOutLineListPageListPrevious.Enabled = false;
            buttonStockOutLineListPageListNext.Enabled = true;
            buttonStockOutLineListPageListLast.Enabled = true;

            stockOutLinePageNumber = 1;
            textBoxStockOutLineListPageNumber.Text = stockOutLinePageNumber + " / " + stockOutLinePageList.PageCount;
        }

        private void buttonStockOutLineListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (stockOutLinePageList.HasPreviousPage == true)
            {
                stockOutLinePageList = new PagedList<Entities.DgvTrnStockOutLineListEntity>(stockOutLineData, --stockOutLinePageNumber, stockOutLinePageSize);
                stockOutLineDataSource.DataSource = stockOutLinePageList;
            }

            buttonStockOutLineListPageListNext.Enabled = true;
            buttonStockOutLineListPageListLast.Enabled = true;

            if (stockOutLinePageNumber == 1)
            {
                buttonStockOutLineListPageListFirst.Enabled = false;
                buttonStockOutLineListPageListPrevious.Enabled = false;
            }

            textBoxStockOutLineListPageNumber.Text = stockOutLinePageNumber + " / " + stockOutLinePageList.PageCount;
        }

        private void buttonStockOutLineListPageListNext_Click(object sender, EventArgs e)
        {
            if (stockOutLinePageList.HasNextPage == true)
            {
                stockOutLinePageList = new PagedList<Entities.DgvTrnStockOutLineListEntity>(stockOutLineData, ++stockOutLinePageNumber, stockOutLinePageSize);
                stockOutLineDataSource.DataSource = stockOutLinePageList;
            }

            buttonStockOutLineListPageListFirst.Enabled = true;
            buttonStockOutLineListPageListPrevious.Enabled = true;

            if (stockOutLinePageNumber == stockOutLinePageList.PageCount)
            {
                buttonStockOutLineListPageListNext.Enabled = false;
                buttonStockOutLineListPageListLast.Enabled = false;
            }

            textBoxStockOutLineListPageNumber.Text = stockOutLinePageNumber + " / " + stockOutLinePageList.PageCount;
        }

        private void buttonStockOutLineListPageListLast_Click(object sender, EventArgs e)
        {
            stockOutLinePageList = new PagedList<Entities.DgvTrnStockOutLineListEntity>(stockOutLineData, stockOutLinePageList.PageCount, stockOutLinePageSize);
            stockOutLineDataSource.DataSource = stockOutLinePageList;

            buttonStockOutLineListPageListFirst.Enabled = true;
            buttonStockOutLineListPageListPrevious.Enabled = true;
            buttonStockOutLineListPageListNext.Enabled = false;
            buttonStockOutLineListPageListLast.Enabled = false;

            stockOutLinePageNumber = stockOutLinePageList.PageCount;
            textBoxStockOutLineListPageNumber.Text = stockOutLinePageNumber + " / " + stockOutLinePageList.PageCount;
        }

        private void buttonSearchItem_Click(object sender, EventArgs e)
        {
            TrnStockOutSearchItemForm trnStockOutDetailSearchItemForm = new TrnStockOutSearchItemForm(this, trnStockOutEntity);
            trnStockOutDetailSearchItemForm.ShowDialog();
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
                Controllers.TrnStockOutLineController trnPOSStockOutLineController = new Controllers.TrnStockOutLineController();

                if (Modules.SysCurrentModule.GetCurrentSettings().IsBarcodeQuantityAlwaysOne == true)
                {
                    trnPOSStockOutLineController.BarcodeStockOutLine(trnStockOutEntity.Id, textBoxBarcode.Text);
                    UpdateStockOutLineListDataSource();
                }
                else
                {
                    Entities.MstItemEntity detailItem = trnPOSStockOutLineController.DetailSearchItem(textBoxBarcode.Text);
                    if (detailItem != null)
                    {
                        var stockOutId = trnStockOutEntity.Id;
                        var itemId = detailItem.Id;
                        var itemDescription = detailItem.ItemDescription;
                        var unitId = detailItem.UnitId;
                        var unit = detailItem.Unit;

                        Entities.TrnStockOutLineEntity trnStockOutLineEntity = new Entities.TrnStockOutLineEntity()
                        {
                            Id = 0,
                            StockOutId = stockOutId,
                            ItemId = itemId,
                            ItemDescription = itemDescription,
                            UnitId = unitId,
                            Unit = unit,
                            Quantity = 1,
                            Cost = 0,
                            Amount = 0,
                            AssetAccountId = 0,
                            AssetAccount = ""
                        };

                        TrnStockOutLineItemDetailForm trnStockOutDetailStockOutLineItemDetailForm = new TrnStockOutLineItemDetailForm(this, trnStockOutLineEntity);
                        trnStockOutDetailStockOutLineItemDetailForm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Item not found.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                textBoxBarcode.SelectAll();
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
                        "Price",
                    };

                    csv.AppendLine(String.Join(",", header));

                    if (stockOutLineData.Any())
                    {
                        foreach (var stockOutLine in stockOutLineData)
                        {
                            String[] data = {
                              "="+"\""+stockOutLine.ColumnStockOutLineListItemBarcode+"\"",
                              stockOutLine.ColumnStockOutLineListItemDescription.Replace(",", ""),
                              stockOutLine.ColumnStockOutLineListUnit,
                              stockOutLine.ColumnStockOutLineListQuantity.Replace(",", ""),
                              stockOutLine.ColumnStockOutLineListCost.Replace(",", ""),
                              stockOutLine.ColumnStockOutLineListAmount.Replace(",", ""),
                              stockOutLine.ColumnStockOutLineListPrice.Replace(",", ""),
                            };

                            csv.AppendLine(String.Join(",", data));
                        }
                    }

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\StockOutLine_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("iso-8859-1"));

                    MessageBox.Show("Generate CSV Successful!", "Generate CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            TrnStockOutLineDetailImportForm stockOutDetailImportForm = new TrnStockOutLineDetailImportForm(this);
            stockOutDetailImportForm.ShowDialog();
        }
    }
}
