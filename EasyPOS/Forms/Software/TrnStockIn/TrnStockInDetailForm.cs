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

namespace EasyPOS.Forms.Software.TrnStockIn
{
    public partial class TrnStockInDetailForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;
        public TrnStockInListForm trnStockInListForm;
        public Entities.TrnStockInEntity trnStockInEntity;

        public static List<Entities.DgvTrnStockInLineListEntity> stockInLineData = new List<Entities.DgvTrnStockInLineListEntity>();
        public static Int32 stockInLinePageNumber = 1;
        public static Int32 stockInLinePageSize = 50;
        public PagedList<Entities.DgvTrnStockInLineListEntity> stockInLinePageList = new PagedList<Entities.DgvTrnStockInLineListEntity>(stockInLineData, stockInLinePageNumber, stockInLinePageSize);
        public BindingSource stockInLineDataSource = new BindingSource();

        public TrnStockInDetailForm(SysSoftwareForm softwareForm, TrnStockInListForm stockInListForm, Entities.TrnStockInEntity stockInEntity)
        {
            InitializeComponent();
            sysSoftwareForm = softwareForm;

            sysUserRights = new Modules.SysUserRightsModule("TrnStockInDetail");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                trnStockInListForm = stockInListForm;
                trnStockInEntity = stockInEntity;

                GetSupplierList();
            }

            var id = trnStockInEntity.Id;

            Controllers.TrnStockInController trnStockIn = new Controllers.TrnStockInController();
            var detail = trnStockIn.DetailStockIn(id);

            if (detail != null)
            {
                sysSoftwareForm.displayTimeStamp(detail.EntryUserUserName, detail.EntryDateTime + " " + detail.EntryTime, detail.UpdateUserUserName, detail.UpdateDateTime + " " + detail.UpdateTime);
            }

            //if (Modules.SysCurrentModule.GetCurrentSettings().HideStockInPriceAndCost == true)
            //{
            //    ColumnStockInLineListCost.Visible = false;
            //    ColumnStockInLineListPrice.Visible = false;
            //}
            //else
            //{
            //    ColumnStockInLineListCost.Visible = true;
            //    ColumnStockInLineListPrice.Visible = true;
            //}

        }

        public void GetSupplierList()
        {
            Controllers.TrnStockInController trnStockInController = new Controllers.TrnStockInController();
            if (trnStockInController.DropdownListStockInSupplier().Any())
            {
                comboBoxSupplier.DataSource = trnStockInController.DropdownListStockInSupplier();
                comboBoxSupplier.ValueMember = "Id";
                comboBoxSupplier.DisplayMember = "Supplier";

                GetUserList();
            }
        }

        public void GetUserList()
        {
            Controllers.TrnStockInController trnStockInController = new Controllers.TrnStockInController();
            if (trnStockInController.DropdownListStockInUser().Any())
            {
                comboBoxPreparedBy.DataSource = trnStockInController.DropdownListStockInUser();
                comboBoxPreparedBy.ValueMember = "Id";
                comboBoxPreparedBy.DisplayMember = "FullName";

                comboBoxCheckedBy.DataSource = trnStockInController.DropdownListStockInUser();
                comboBoxCheckedBy.ValueMember = "Id";
                comboBoxCheckedBy.DisplayMember = "FullName";

                comboBoxApprovedBy.DataSource = trnStockInController.DropdownListStockInUser();
                comboBoxApprovedBy.ValueMember = "Id";
                comboBoxApprovedBy.DisplayMember = "FullName";

                GetStockInDetail();
            }
        }

        public void GetStockInDetail()
        {
            UpdateComponents(trnStockInEntity.IsLocked);

            textBoxManualStockInNumber.Text = trnStockInEntity.ManualStockInNumber;
            textBoxStockInNumber.Text = trnStockInEntity.StockInNumber;
            dateTimePickerStockInDate.Value = Convert.ToDateTime(trnStockInEntity.StockInDate);
            comboBoxSupplier.SelectedValue = trnStockInEntity.SupplierId;
            textBoxRemarks.Text = trnStockInEntity.Remarks;
            comboBoxPreparedBy.SelectedValue = trnStockInEntity.PreparedBy;
            comboBoxCheckedBy.SelectedValue = trnStockInEntity.CheckedBy;
            comboBoxApprovedBy.SelectedValue = trnStockInEntity.ApprovedBy;

            CreateStockInLineListDataGridView();
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

            textBoxManualStockInNumber.Enabled = !isLocked;
            dateTimePickerStockInDate.Enabled = !isLocked;
            comboBoxSupplier.Enabled = !isLocked;
            textBoxRemarks.Enabled = !isLocked;
            comboBoxCheckedBy.Enabled = !isLocked;
            comboBoxApprovedBy.Enabled = !isLocked;

            dataGridViewStockInLineList.Columns[0].Visible = !isLocked;
            dataGridViewStockInLineList.Columns[1].Visible = !isLocked;
            dateTimePickerStockInDate.Focus();
        }

        private void buttonLock_Click(object sender, EventArgs e)
        {
            Controllers.TrnStockInController trnStockInController = new Controllers.TrnStockInController();

            Entities.TrnStockInEntity newStockInEntity = new Entities.TrnStockInEntity()
            {
                ManualStockInNumber = textBoxManualStockInNumber.Text,
                StockInDate = dateTimePickerStockInDate.Value.Date.ToShortDateString(),
                SupplierId = Convert.ToInt32(comboBoxSupplier.SelectedValue),
                Remarks = textBoxRemarks.Text,
                CheckedBy = Convert.ToInt32(comboBoxCheckedBy.SelectedValue),
                ApprovedBy = Convert.ToInt32(comboBoxApprovedBy.SelectedValue)
            };

            String[] lockStockIn = trnStockInController.LockStockIn(trnStockInEntity.Id, newStockInEntity);
            if (lockStockIn[1].Equals("0") == false)
            {
                UpdateComponents(true);
                trnStockInListForm.UpdateStockInListDataSource();
            }
            else
            {
                MessageBox.Show(lockStockIn[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUnlock_Click(object sender, EventArgs e)
        {
            Controllers.TrnStockInController trnStockInController = new Controllers.TrnStockInController();

            String[] unlockStockIn = trnStockInController.UnlockStockIn(trnStockInEntity.Id);
            if (unlockStockIn[1].Equals("0") == false)
            {
                UpdateComponents(false);
                trnStockInListForm.UpdateStockInListDataSource();
            }
            else
            {
                MessageBox.Show(unlockStockIn[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            new TrnStockInDetailReportForm(trnStockInEntity.Id);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (textBoxRemarks.Enabled == false)
            {
                sysSoftwareForm.RemoveTabPage();
            }
            else
            {
                DialogResult closeDialogResult = MessageBox.Show("Save Changes?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (closeDialogResult == DialogResult.Yes)
                {
                    Controllers.TrnStockInController trnStockInController = new Controllers.TrnStockInController();

                    Entities.TrnStockInEntity newStockInEntity = new Entities.TrnStockInEntity()
                    {
                        ManualStockInNumber = textBoxManualStockInNumber.Text,
                        StockInDate = dateTimePickerStockInDate.Value.Date.ToShortDateString(),
                        SupplierId = Convert.ToInt32(comboBoxSupplier.SelectedValue),
                        Remarks = textBoxRemarks.Text,
                        CheckedBy = Convert.ToInt32(comboBoxCheckedBy.SelectedValue),
                        ApprovedBy = Convert.ToInt32(comboBoxApprovedBy.SelectedValue)
                    };

                    String[] lockStockIn = trnStockInController.SaveStockIn(trnStockInEntity.Id, newStockInEntity);
                    if (lockStockIn[1].Equals("0") == false)
                    {
                        sysSoftwareForm.RemoveTabPage();
                        trnStockInListForm.UpdateStockInListDataSource();
                    }
                    else
                    {
                        MessageBox.Show(lockStockIn[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    sysSoftwareForm.RemoveTabPage();
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
                        "Price"
                    };

                    csv.AppendLine(String.Join(",", header));

                    if (stockInLineData.Any())
                    {
                        foreach (var stockInLine in stockInLineData)
                        {
                            //string barcode = stockInLine.ColumnStockInLineListItemBarcode;
                            String[] data = {
                              //barcode = "(\""+barcode+"\")".Replace(",",""),
                               "="+"\""+stockInLine.ColumnStockInLineListItemBarcode+"\"",
                              stockInLine.ColumnStockInLineListItemDescription.Replace(",", ""),
                              stockInLine.ColumnStockInLineListUnit,
                              stockInLine.ColumnStockInLineListQuantity.Replace(",", ""),
                              stockInLine.ColumnStockInLineListCost.Replace(",", ""),
                              stockInLine.ColumnStockInLineListAmount.Replace(",", ""),
                              stockInLine.ColumnStockInLineListPrice.ToString().Replace(",", ""),

                            };

                            csv.AppendLine(String.Join(",", data));
                        }
                    }

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\StockInLine_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("iso-8859-1"));

                    MessageBox.Show("Generate CSV Successful!", "Generate CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void UpdateStockInLineListDataSource()
        {
            SetStockInLineListDataSourceAsync();
        }

        public async void SetStockInLineListDataSourceAsync()
        {
            List<Entities.DgvTrnStockInLineListEntity> getStockInLineListData = await GetStockInLineListDataTask();
            if (getStockInLineListData.Any())
            {
                stockInLineData = getStockInLineListData;
                stockInLinePageList = new PagedList<Entities.DgvTrnStockInLineListEntity>(stockInLineData, stockInLinePageNumber, stockInLinePageSize);

                if (stockInLinePageList.PageCount == 1)
                {
                    buttonStockInLineListPageListFirst.Enabled = false;
                    buttonStockInLineListPageListPrevious.Enabled = false;
                    buttonStockInLineListPageListNext.Enabled = false;
                    buttonStockInLineListPageListLast.Enabled = false;
                }
                else if (stockInLinePageNumber == 1)
                {
                    buttonStockInLineListPageListFirst.Enabled = false;
                    buttonStockInLineListPageListPrevious.Enabled = false;
                    buttonStockInLineListPageListNext.Enabled = true;
                    buttonStockInLineListPageListLast.Enabled = true;
                }
                else if (stockInLinePageNumber == stockInLinePageList.PageCount)
                {
                    buttonStockInLineListPageListFirst.Enabled = true;
                    buttonStockInLineListPageListPrevious.Enabled = true;
                    buttonStockInLineListPageListNext.Enabled = false;
                    buttonStockInLineListPageListLast.Enabled = false;
                }
                else
                {
                    buttonStockInLineListPageListFirst.Enabled = true;
                    buttonStockInLineListPageListPrevious.Enabled = true;
                    buttonStockInLineListPageListNext.Enabled = true;
                    buttonStockInLineListPageListLast.Enabled = true;
                }

                textBoxStockInLineListPageNumber.Text = stockInLinePageNumber + " / " + stockInLinePageList.PageCount;
                stockInLineDataSource.DataSource = stockInLinePageList;
            }
            else
            {
                buttonStockInLineListPageListFirst.Enabled = false;
                buttonStockInLineListPageListPrevious.Enabled = false;
                buttonStockInLineListPageListNext.Enabled = false;
                buttonStockInLineListPageListLast.Enabled = false;

                stockInLinePageNumber = 1;

                stockInLineData = new List<Entities.DgvTrnStockInLineListEntity>();
                stockInLineDataSource.Clear();
                textBoxStockInLineListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvTrnStockInLineListEntity>> GetStockInLineListDataTask()
        {
            Controllers.TrnStockInLineController trnStockInLineController = new Controllers.TrnStockInLineController();

            List<Entities.TrnStockInLineEntity> listStockInLine = trnStockInLineController.ListStockInLine(trnStockInEntity.Id);
            if (listStockInLine.Any())
            {
                var items = from d in listStockInLine
                            select new Entities.DgvTrnStockInLineListEntity
                            {
                                ColumnStockInLineListButtonEdit = "Edit",
                                ColumnStockInLineListButtonDelete = "Delete",
                                ColumnStockInLineListId = d.Id,
                                ColumnStockInLineListStockInId = d.StockInId,
                                ColumnStockInLineListItemId = d.ItemId,
                                ColumnStockInLineListItemBarcode = d.ItemBarcode,
                                ColumnStockInLineListItemDescription = d.ItemDescription,
                                ColumnStockInLineListUnitId = d.UnitId,
                                ColumnStockInLineListUnit = d.Unit,
                                ColumnStockInLineListQuantity = d.Quantity.ToString("#,##0.00"),
                                ColumnStockInLineListCost = d.Cost.ToString("#,##0.00"),
                                ColumnStockInLineListAmount = d.Amount.ToString("#,##0.00"),
                                ColumnStockInLineListExpiryDate = d.ExpiryDate,
                                ColumnStockInLineListLotNumber = d.LotNumber,
                                ColumnStockInLineListAssetAccountId = d.AssetAccountId,
                                ColumnStockInLineListAssetAccount = d.AssetAccount,
                                ColumnStockInLineListPrice = d.Price != null ? Convert.ToDecimal(d.Price).ToString("#,##0.00") : Convert.ToDecimal("0").ToString("#,##0.00"),
                            };

                return Task.FromResult(items.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvTrnStockInLineListEntity>());
            }
        }

        public void CreateStockInLineListDataGridView()
        {
            UpdateStockInLineListDataSource();

            dataGridViewStockInLineList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewStockInLineList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewStockInLineList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewStockInLineList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewStockInLineList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewStockInLineList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewStockInLineList.DataSource = stockInLineDataSource;
        }

        public void GetStockInLineListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void dataGridViewStockInLineList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetStockInLineListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewStockInLineList.CurrentCell.ColumnIndex == dataGridViewStockInLineList.Columns["ColumnStockInLineListButtonEdit"].Index)
            {
                var id = Convert.ToInt32(dataGridViewStockInLineList.Rows[e.RowIndex].Cells[dataGridViewStockInLineList.Columns["ColumnStockInLineListId"].Index].Value);
                var stockInId = Convert.ToInt32(dataGridViewStockInLineList.Rows[e.RowIndex].Cells[dataGridViewStockInLineList.Columns["ColumnStockInLineListStockInId"].Index].Value);
                var itemId = Convert.ToInt32(dataGridViewStockInLineList.Rows[e.RowIndex].Cells[dataGridViewStockInLineList.Columns["ColumnStockInLineListItemId"].Index].Value);
                var itemDescription = dataGridViewStockInLineList.Rows[e.RowIndex].Cells[dataGridViewStockInLineList.Columns["ColumnStockInLineListItemDescription"].Index].Value.ToString();
                var unitId = Convert.ToInt32(dataGridViewStockInLineList.Rows[e.RowIndex].Cells[dataGridViewStockInLineList.Columns["ColumnStockInLineListUnitId"].Index].Value);
                var unit = dataGridViewStockInLineList.Rows[e.RowIndex].Cells[dataGridViewStockInLineList.Columns["ColumnStockInLineListUnit"].Index].Value.ToString();
                var quantity = Convert.ToDecimal(dataGridViewStockInLineList.Rows[e.RowIndex].Cells[dataGridViewStockInLineList.Columns["ColumnStockInLineListQuantity"].Index].Value);
                var cost = Convert.ToDecimal(dataGridViewStockInLineList.Rows[e.RowIndex].Cells[dataGridViewStockInLineList.Columns["ColumnStockInLineListCost"].Index].Value);
                var amount = Convert.ToDecimal(dataGridViewStockInLineList.Rows[e.RowIndex].Cells[dataGridViewStockInLineList.Columns["ColumnStockInLineListAmount"].Index].Value);
                var expiryDate = dataGridViewStockInLineList.Rows[e.RowIndex].Cells[dataGridViewStockInLineList.Columns["ColumnStockInLineListExpiryDate"].Index].Value.ToString();
                var lotNumber = dataGridViewStockInLineList.Rows[e.RowIndex].Cells[dataGridViewStockInLineList.Columns["ColumnStockInLineListLotNumber"].Index].Value.ToString();
                var assetAccountId = Convert.ToInt32(dataGridViewStockInLineList.Rows[e.RowIndex].Cells[dataGridViewStockInLineList.Columns["ColumnStockInLineListAssetAccountId"].Index].Value);
                var assetAccount = dataGridViewStockInLineList.Rows[e.RowIndex].Cells[dataGridViewStockInLineList.Columns["ColumnStockInLineListAssetAccount"].Index].Value.ToString();
                var price = Convert.ToDecimal(dataGridViewStockInLineList.Rows[e.RowIndex].Cells[dataGridViewStockInLineList.Columns["ColumnStockInLineListPrice"].Index].Value);

                Entities.TrnStockInLineEntity trnStockInLineEntity = new Entities.TrnStockInLineEntity()
                {
                    Id = id,
                    StockInId = stockInId,
                    ItemId = itemId,
                    ItemDescription = itemDescription,
                    UnitId = unitId,
                    Unit = unit,
                    Quantity = quantity,
                    Cost = cost,
                    Amount = amount,
                    ExpiryDate = expiryDate,
                    LotNumber = lotNumber,
                    AssetAccountId = assetAccountId,
                    AssetAccount = assetAccount,
                    Price = price
                };

                TrnStockInLineItemDetailForm trnStockInDetailStockInLineItemDetailForm = new TrnStockInLineItemDetailForm(this, trnStockInLineEntity);
                trnStockInDetailStockInLineItemDetailForm.ShowDialog();
            }

            if (e.RowIndex > -1 && dataGridViewStockInLineList.CurrentCell.ColumnIndex == dataGridViewStockInLineList.Columns["ColumnStockInLineListButtonDelete"].Index)
            {
                DialogResult deleteDialogResult = MessageBox.Show("Delete Stock-In?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (deleteDialogResult == DialogResult.Yes)
                {
                    var id = Convert.ToInt32(dataGridViewStockInLineList.Rows[e.RowIndex].Cells[dataGridViewStockInLineList.Columns["ColumnStockInLineListId"].Index].Value);

                    Controllers.TrnStockInLineController trnStockInLineController = new Controllers.TrnStockInLineController();
                    String[] deleteStockInLine = trnStockInLineController.DeleteStockInLine(id);
                    if (deleteStockInLine[1].Equals("0") == false)
                    {
                        stockInLinePageNumber = 1;
                        UpdateStockInLineListDataSource();
                    }
                    else
                    {
                        MessageBox.Show(deleteStockInLine[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonStockInLineListPageListFirst_Click(object sender, EventArgs e)
        {
            stockInLinePageList = new PagedList<Entities.DgvTrnStockInLineListEntity>(stockInLineData, 1, stockInLinePageSize);
            stockInLineDataSource.DataSource = stockInLinePageList;

            buttonStockInLineListPageListFirst.Enabled = false;
            buttonStockInLineListPageListPrevious.Enabled = false;
            buttonStockInLineListPageListNext.Enabled = true;
            buttonStockInLineListPageListLast.Enabled = true;

            stockInLinePageNumber = 1;
            textBoxStockInLineListPageNumber.Text = stockInLinePageNumber + " / " + stockInLinePageList.PageCount;
        }

        private void buttonStockInLineListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (stockInLinePageList.HasPreviousPage == true)
            {
                stockInLinePageList = new PagedList<Entities.DgvTrnStockInLineListEntity>(stockInLineData, --stockInLinePageNumber, stockInLinePageSize);
                stockInLineDataSource.DataSource = stockInLinePageList;
            }

            buttonStockInLineListPageListNext.Enabled = true;
            buttonStockInLineListPageListLast.Enabled = true;

            if (stockInLinePageNumber == 1)
            {
                buttonStockInLineListPageListFirst.Enabled = false;
                buttonStockInLineListPageListPrevious.Enabled = false;
            }

            textBoxStockInLineListPageNumber.Text = stockInLinePageNumber + " / " + stockInLinePageList.PageCount;
        }

        private void buttonStockInLineListPageListNext_Click(object sender, EventArgs e)
        {
            if (stockInLinePageList.HasNextPage == true)
            {
                stockInLinePageList = new PagedList<Entities.DgvTrnStockInLineListEntity>(stockInLineData, ++stockInLinePageNumber, stockInLinePageSize);
                stockInLineDataSource.DataSource = stockInLinePageList;
            }

            buttonStockInLineListPageListFirst.Enabled = true;
            buttonStockInLineListPageListPrevious.Enabled = true;

            if (stockInLinePageNumber == stockInLinePageList.PageCount)
            {
                buttonStockInLineListPageListNext.Enabled = false;
                buttonStockInLineListPageListLast.Enabled = false;
            }

            textBoxStockInLineListPageNumber.Text = stockInLinePageNumber + " / " + stockInLinePageList.PageCount;
        }

        private void buttonStockInLineListPageListLast_Click(object sender, EventArgs e)
        {
            stockInLinePageList = new PagedList<Entities.DgvTrnStockInLineListEntity>(stockInLineData, stockInLinePageList.PageCount, stockInLinePageSize);
            stockInLineDataSource.DataSource = stockInLinePageList;

            buttonStockInLineListPageListFirst.Enabled = true;
            buttonStockInLineListPageListPrevious.Enabled = true;
            buttonStockInLineListPageListNext.Enabled = false;
            buttonStockInLineListPageListLast.Enabled = false;

            stockInLinePageNumber = stockInLinePageList.PageCount;
            textBoxStockInLineListPageNumber.Text = stockInLinePageNumber + " / " + stockInLinePageList.PageCount;
        }

        private void buttonSearchItem_Click(object sender, EventArgs e)
        {
            TrnStockInSearchItemForm trnStockInDetailSearchItemForm = new TrnStockInSearchItemForm(this, trnStockInEntity);
            trnStockInDetailSearchItemForm.ShowDialog();
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
                Controllers.TrnStockInLineController trnPOSStockInLineController = new Controllers.TrnStockInLineController();

                if (Modules.SysCurrentModule.GetCurrentSettings().IsBarcodeQuantityAlwaysOne == true)
                {
                    trnPOSStockInLineController.BarcodeStockInLine(trnStockInEntity.Id, textBoxBarcode.Text);
                    UpdateStockInLineListDataSource();
                }
                else
                {
                    Entities.MstItemEntity detailItem = trnPOSStockInLineController.DetailSearchItem(textBoxBarcode.Text);
                    if (detailItem != null)
                    {
                        var stockInId = trnStockInEntity.Id;
                        var itemId = detailItem.Id;
                        var itemDescription = detailItem.ItemDescription;
                        var unitId = detailItem.UnitId;
                        var unit = detailItem.Unit;
                        var price = detailItem.Price;

                        Entities.TrnStockInLineEntity trnStockInLineEntity = new Entities.TrnStockInLineEntity()
                        {
                            Id = 0,
                            StockInId = stockInId,
                            ItemId = itemId,
                            ItemDescription = itemDescription,
                            UnitId = unitId,
                            Unit = unit,
                            Quantity = 1,
                            Cost = 0,
                            Amount = 0,
                            ExpiryDate = "",
                            LotNumber = "",
                            AssetAccountId = 0,
                            AssetAccount = "",
                            Price = price
                        };

                        TrnStockInLineItemDetailForm trnStockInDetailStockInLineItemDetailForm = new TrnStockInLineItemDetailForm(this, trnStockInLineEntity);
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

        private void buttonImport_Click(object sender, EventArgs e)
        {
            TrnStockInLineDetailImportForm stockInDetailImportForm = new TrnStockInLineDetailImportForm(this);
            stockInDetailImportForm.ShowDialog();
        }

        private void buttonExportAllItems_Click(object sender, EventArgs e)
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
                        "Price"
                    };

                    csv.AppendLine(String.Join(",", header));

                    Controllers.RepInventoryReportController repInvetoryReportController = new Controllers.RepInventoryReportController();
                    var inventoryListReport = repInvetoryReportController.GetInventoryListReport();

                    if (inventoryListReport.Any())
                    {
                        foreach (var stockInLine in inventoryListReport)
                        {
                            //string barcode = stockInLine.ColumnStockInLineListItemBarcode;
                            String[] data = {
                              //barcode = "(\""+barcode+"\")".Replace(",",""),
                               "="+"\""+stockInLine.BarCode+"\"",
                              stockInLine.ItemDescription.Replace(",", ""),
                              stockInLine.Unit,
                              "0",
                              Convert.ToString(stockInLine.Cost),
                              "0",
                              Convert.ToString(stockInLine.Price)
                            };

                            csv.AppendLine(String.Join(",", data));
                        }
                    }

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\StockInAllItems_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("iso-8859-1"));

                    MessageBox.Show("Generate CSV Successful!", "Generate CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
