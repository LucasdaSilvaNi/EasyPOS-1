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

namespace EasyPOS.Forms.Software.MstItem
{
    public partial class MstItemListForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;
        public static Int32 pageSize = 15;
        public static List<Entities.DgvMstItemListEntity> itemListData = new List<Entities.DgvMstItemListEntity>();
        public static Int32 pageNumber = 1;
        public PagedList<Entities.DgvMstItemListEntity> itemListPageList = new PagedList<Entities.DgvMstItemListEntity>(itemListData, pageNumber, pageSize);
        public BindingSource itemListDataSource = new BindingSource();
        public List<String> inventoryOption;
        public List<String> lockOption;
        public List<String> supplierOption;
        public Entities.MstItemEntity mstItemEntity;
        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public MstItemListForm(SysSoftwareForm softwareForm)
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
                    label1.Text = SetLabel(label1.Text);
                    dataGridViewItemList.Columns[3].HeaderText = SetLabel(dataGridViewItemList.Columns[3].HeaderText);
                    dataGridViewItemList.Columns[4].HeaderText = SetLabel(dataGridViewItemList.Columns[4].HeaderText);
                    dataGridViewItemList.Columns[5].HeaderText = SetLabel(dataGridViewItemList.Columns[5].HeaderText);
                    dataGridViewItemList.Columns[6].HeaderText = SetLabel(dataGridViewItemList.Columns[6].HeaderText);
                    dataGridViewItemList.Columns[7].HeaderText = SetLabel(dataGridViewItemList.Columns[7].HeaderText);
                    dataGridViewItemList.Columns[8].HeaderText = SetLabel(dataGridViewItemList.Columns[8].HeaderText);
                    dataGridViewItemList.Columns[9].HeaderText = SetLabel(dataGridViewItemList.Columns[9].HeaderText);
                    dataGridViewItemList.Columns[11].HeaderText = SetLabel(dataGridViewItemList.Columns[11].HeaderText);
                    dataGridViewItemList.Columns[12].HeaderText = SetLabel(dataGridViewItemList.Columns[12].HeaderText);
                    dataGridViewItemList.Columns[13].HeaderText = SetLabel(dataGridViewItemList.Columns[13].HeaderText);
                    buttonItemListPageListFirst.Text = SetLabel(buttonItemListPageListFirst.Text);
                    buttonItemListPageListLast.Text = SetLabel(buttonItemListPageListLast.Text);
                    buttonItemListPageListNext.Text = SetLabel(buttonItemListPageListNext.Text);
                    buttonItemListPageListPrevious.Text = SetLabel(buttonItemListPageListPrevious.Text);
                }
            }

            sysSoftwareForm = softwareForm;

            sysUserRights = new Modules.SysUserRightsModule("MstItem");
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
                    dataGridViewItemList.Columns[0].Visible = false;
                }

                if (sysUserRights.GetUserRights().CanDelete == false)
                {
                    dataGridViewItemList.Columns[1].Visible = false;
                }
                inventoryOption = new List<String>
                {
                    "All",
                    "Inventory",
                    "Non-Inventory"
                };
                comboBoxIsInventory.DataSource = inventoryOption;

                lockOption = new List<String>
                {
                    "All",
                    "Locked",
                    "Unlocked"
                };
                comboBoxIsLocked.DataSource = lockOption;
                if (Modules.SysCurrentModule.GetCurrentSettings().HideItemListBarcode == true)
                {
                    ColumnItemListBarcode.Visible = false;
                }
                else
                {
                    ColumnItemListBarcode.Visible = true;
                }
                if (Modules.SysCurrentModule.GetCurrentSettings().HideItemListItemCode == true)
                {
                    ColumnItemListCode.Visible = false;
                }
                else
                {
                    ColumnItemListCode.Visible = true;
                }

                CreateItemListDataGridView();

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

        public void UpdateItemListDataSource()
        {
            String selectedIsInventory = Convert.ToString(comboBoxIsInventory.SelectedValue);
            String selectedIsLocked = Convert.ToString(comboBoxIsLocked.SelectedValue);

            SetItemListDataSourceAsync(selectedIsInventory, selectedIsLocked);
        }

        public async void SetItemListDataSourceAsync(String isInventory, String isLocked)
        {
            List<Entities.DgvMstItemListEntity> getItemListData = await GetItemListDataTask(isInventory, isLocked);
            if (getItemListData.Any())
            {
                itemListData = getItemListData;
                itemListPageList = new PagedList<Entities.DgvMstItemListEntity>(itemListData, pageNumber, pageSize);

                if (itemListPageList.PageCount == 1)
                {
                    buttonItemListPageListFirst.Enabled = false;
                    buttonItemListPageListPrevious.Enabled = false;
                    buttonItemListPageListNext.Enabled = false;
                    buttonItemListPageListLast.Enabled = false;
                }
                else if (pageNumber == 1)
                {
                    buttonItemListPageListFirst.Enabled = false;
                    buttonItemListPageListPrevious.Enabled = false;
                    buttonItemListPageListNext.Enabled = true;
                    buttonItemListPageListLast.Enabled = true;
                }
                else if (pageNumber == itemListPageList.PageCount)
                {
                    buttonItemListPageListFirst.Enabled = true;
                    buttonItemListPageListPrevious.Enabled = true;
                    buttonItemListPageListNext.Enabled = false;
                    buttonItemListPageListLast.Enabled = false;
                }
                else
                {
                    buttonItemListPageListFirst.Enabled = true;
                    buttonItemListPageListPrevious.Enabled = true;
                    buttonItemListPageListNext.Enabled = true;
                    buttonItemListPageListLast.Enabled = true;
                }
                moveToFirstPage();
                textBoxItemListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
                itemListDataSource.DataSource = itemListPageList;
            }
            else
            {
                buttonItemListPageListFirst.Enabled = false;
                buttonItemListPageListPrevious.Enabled = false;
                buttonItemListPageListNext.Enabled = false;
                buttonItemListPageListLast.Enabled = false;

                pageNumber = 1;

                itemListData = new List<Entities.DgvMstItemListEntity>();
                itemListDataSource.Clear();
                textBoxItemListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvMstItemListEntity>> GetItemListDataTask(String selectedIsInventory, String selectedIsLocked)
        {
            String filter = textBoxItemListFilter.Text;
            Controllers.MstItemController mstItemController = new Controllers.MstItemController();
            List<Entities.MstItemEntity> listItem = mstItemController.ListItem(filter, selectedIsInventory, selectedIsLocked);
            if (listItem.Any())
            {
                var items = from d in listItem
                            select new Entities.DgvMstItemListEntity
                            {
                                ColumnItemListButtonEdit = "Edit",
                                ColumnItemListButtonDelete = "Delete",
                                ColumnItemListId = d.Id,
                                ColumnItemListCode = d.ItemCode,
                                ColumnItemListDescription = d.ItemDescription,
                                ColumnItemListBarcode = d.BarCode,
                                ColumnItemListUnit = d.Unit,
                                ColumnItemListCategory = d.Category,
                                ColumnItemListCost = d.Cost.ToString("#,##0.00"),
                                ColumnItemListPrice = d.Price.ToString("#,##0.00"),
                                ColumnItemListOnHandQuantity = d.OnhandQuantity.ToString("#,##0"),
                                ColumnItemListIsInventory = d.IsInventory,
                                ColumnItemListIsLocked = d.IsLocked,
                                ColumnSupplier = d.Supplier

                            };

                return Task.FromResult(items.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvMstItemListEntity>());
            }
        }

        public void CreateItemListDataGridView()
        {
            UpdateItemListDataSource();

            dataGridViewItemList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewItemList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewItemList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewItemList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewItemList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewItemList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewItemList.DataSource = itemListDataSource;
        }

        public void GetItemListCurrentSelectedCell(Int32 rowIndex)
        {
            Controllers.MstItemController newItem = new Controllers.MstItemController();
            var detail = newItem.DetailItem(rowIndex);

            if (detail != null)
            {
                sysSoftwareForm.displayTimeStamp(detail.EntryUserUserName, detail.EntryDateTime + " " + detail.EntryTime, detail.UpdatedUserUserName, detail.UpdateDateTime + " " + detail.UpdateTime);

            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
            sysSoftwareForm.RemoveTabPage();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Controllers.MstItemController mstItemController = new Controllers.MstItemController();
            String[] addItem = mstItemController.AddItem();
            if (addItem[1].Equals("0") == false)
            {
                sysSoftwareForm.AddTabPageItemDetail(this, mstItemController.DetailItem(Convert.ToInt32(addItem[1])));
                UpdateItemListDataSource();
            }
            else
            {
                MessageBox.Show(addItem[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetItemListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewItemList.CurrentCell.ColumnIndex == dataGridViewItemList.Columns["ColumnItemListButtonEdit"].Index)
            {
                Controllers.MstItemController mstItemController = new Controllers.MstItemController();
                sysSoftwareForm.AddTabPageItemDetail(this, mstItemController.DetailItem(Convert.ToInt32(dataGridViewItemList.Rows[e.RowIndex].Cells[2].Value)));
            }

            if (e.RowIndex > -1 && dataGridViewItemList.CurrentCell.ColumnIndex == dataGridViewItemList.Columns["ColumnItemListButtonDelete"].Index)
            {
                Boolean isLocked = Convert.ToBoolean(dataGridViewItemList.Rows[e.RowIndex].Cells[12].Value);

                if (isLocked == true)
                {
                    MessageBox.Show("Already locked.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult deleteDialogResult = MessageBox.Show("Delete Item?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (deleteDialogResult == DialogResult.Yes)
                    {
                        Controllers.MstItemController mstItemController = new Controllers.MstItemController();

                        String[] deleteItem = mstItemController.DeleteItem(Convert.ToInt32(dataGridViewItemList.Rows[e.RowIndex].Cells[2].Value));
                        if (deleteItem[1].Equals("0") == false)
                        {
                            Int32 currentPageNumber = pageNumber;

                            pageNumber = 1;
                            UpdateItemListDataSource();

                            if (itemListPageList != null)
                            {
                                if (itemListData.Count() % pageSize == 1)
                                {
                                    pageNumber = currentPageNumber;
                                }
                                else if (itemListData.Count() < 1)
                                {
                                    pageNumber = 1;
                                }
                                else
                                {
                                    pageNumber = currentPageNumber;
                                }

                                itemListDataSource.DataSource = itemListPageList;
                            }
                        }
                        else
                        {
                            MessageBox.Show(deleteItem[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void textBoxItemListFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateItemListDataSource();
            }
        }
        public void moveToFirstPage()
        {
            itemListPageList = new PagedList<Entities.DgvMstItemListEntity>(itemListData, 1, pageSize);
            itemListDataSource.DataSource = itemListPageList;

            buttonItemListPageListFirst.Enabled = false;
            buttonItemListPageListPrevious.Enabled = false;
            buttonItemListPageListNext.Enabled = true;
            buttonItemListPageListLast.Enabled = true;

            pageNumber = 1;
            textBoxItemListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }
        private void buttonItemListPageListFirst_Click(object sender, EventArgs e)
        {
            moveToFirstPage();
        }

        private void buttonItemListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (itemListPageList.HasPreviousPage == true)
            {
                itemListPageList = new PagedList<Entities.DgvMstItemListEntity>(itemListData, --pageNumber, pageSize);
                itemListDataSource.DataSource = itemListPageList;
            }

            buttonItemListPageListNext.Enabled = true;
            buttonItemListPageListLast.Enabled = true;

            if (pageNumber == 1)
            {
                buttonItemListPageListFirst.Enabled = false;
                buttonItemListPageListPrevious.Enabled = false;
            }

            textBoxItemListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonItemListPageListNext_Click(object sender, EventArgs e)
        {
            if (itemListPageList.HasNextPage == true)
            {
                itemListPageList = new PagedList<Entities.DgvMstItemListEntity>(itemListData, ++pageNumber, pageSize);
                itemListDataSource.DataSource = itemListPageList;
            }

            buttonItemListPageListFirst.Enabled = true;
            buttonItemListPageListPrevious.Enabled = true;

            if (pageNumber == itemListPageList.PageCount)
            {
                buttonItemListPageListNext.Enabled = false;
                buttonItemListPageListLast.Enabled = false;
            }

            textBoxItemListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonItemListPageListLast_Click(object sender, EventArgs e)
        {
            itemListPageList = new PagedList<Entities.DgvMstItemListEntity>(itemListData, itemListPageList.PageCount, pageSize);
            itemListDataSource.DataSource = itemListPageList;

            buttonItemListPageListFirst.Enabled = true;
            buttonItemListPageListPrevious.Enabled = true;
            buttonItemListPageListNext.Enabled = false;
            buttonItemListPageListLast.Enabled = false;

            pageNumber = itemListPageList.PageCount;
            textBoxItemListPageNumber.Text = pageNumber + " / " + itemListPageList.PageCount;
        }

        private void comboBoxIsInventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateItemListDataSource();
        }

        private void comboBoxIsLocked_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateItemListDataSource();
        }

        private void dataGridViewItemList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
