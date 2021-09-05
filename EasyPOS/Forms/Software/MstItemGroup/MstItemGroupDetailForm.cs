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

namespace EasyPOS.Forms.Software.MstItemGroup
{
    public partial class MstItemGroupDetailForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public MstItemGroupListForm mstItemGroupListForm;
        public Entities.MstItemGroupEntity mstItemGroupEntity;

        public static List<Entities.DgvMstItemGroupItemListEntity> itemGroupItemData = new List<Entities.DgvMstItemGroupItemListEntity>();
        public static Int32 itemGroupItemPageNumber = 1;
        public static Int32 itemGroupItemPageSize = 50;
        public PagedList<Entities.DgvMstItemGroupItemListEntity> itemGroupItemPageList = new PagedList<Entities.DgvMstItemGroupItemListEntity>(itemGroupItemData, itemGroupItemPageNumber, itemGroupItemPageSize);
        public BindingSource itemGroupItemDataSource = new BindingSource();

        public List<String> kitchens;

        public MstItemGroupDetailForm(SysSoftwareForm softwareForm, MstItemGroupListForm itemListForm, Entities.MstItemGroupEntity itemEntity)
        {
            InitializeComponent();
            sysSoftwareForm = softwareForm;

            sysUserRights = new Modules.SysUserRightsModule("MstItemGroupDetail");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                mstItemGroupListForm = itemListForm;
                mstItemGroupEntity = itemEntity;

                if (sysUserRights.GetUserRights().CanAdd == false)
                {
                    buttonSearchItem.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanEdit == false)
                {
                    dataGridViewItemGroupItemList.Columns[0].Visible = false;
                }

                if (sysUserRights.GetUserRights().CanDelete == false)
                {
                    dataGridViewItemGroupItemList.Columns[1].Visible = false;
                }

                kitchens = new List<String>
                {
                    "Kitchen 1",
                    "Kitchen 2",
                    "Kitchen 3",
                    "Kitchen 4",
                    "Kitchen 5",
                    "Kitchen 6",
                    "Kitchen 7",
                    "Kitchen 8",
                    "Kitchen 9",
                    "Kitchen 10",
                };

                comboBoxItemGroupKitchens.DataSource = kitchens;

                GetItemGroupDetail();
                textBoxItemGroup.Focus();
            }

            var id = mstItemGroupEntity.Id;

            Controllers.MstItemGroupController mstItemGroup = new Controllers.MstItemGroupController();
            var detail = mstItemGroup.DetailItemGroup(id);

            if (detail != null)
            {
                sysSoftwareForm.displayTimeStamp(detail.EntryUserUserName, detail.EntryDateTime + " " + detail.EntryTime, detail.UpdatedUserUserName, detail.UpdateDateTime + " " + detail.UpdateTime);
            }
        }

        public void GetItemGroupDetail()
        {
            textBoxItemGroup.Text = mstItemGroupEntity.ItemGroup;
            textBoxItemGroupImagePath.Text = mstItemGroupEntity.ImagePath;
            comboBoxItemGroupKitchens.Text = mstItemGroupEntity.KitchenReport;
            UpdateComponents(mstItemGroupEntity.IsLocked);

            CreateItemGroupItemListDataGridView();
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

            if (sysUserRights.GetUserRights().CanAdd == false)
            {
                buttonSearchItem.Enabled = false;
            }
            else
            {
                buttonSearchItem.Enabled = !isLocked;
            }

            if (sysUserRights.GetUserRights().CanEdit == false)
            {
                dataGridViewItemGroupItemList.Columns[0].Visible = false;
                dataGridViewItemGroupItemList.Columns[6].ReadOnly = true;
            }
            else
            {
                dataGridViewItemGroupItemList.Columns[0].Visible = !isLocked;
                dataGridViewItemGroupItemList.Columns[6].ReadOnly = isLocked;
            }

            if (sysUserRights.GetUserRights().CanDelete == false)
            {
                dataGridViewItemGroupItemList.Columns[1].Visible = false;
            }
            else
            {
                dataGridViewItemGroupItemList.Columns[1].Visible = !isLocked;
            }

            textBoxItemGroup.Enabled = !isLocked;
            textBoxItemGroupImagePath.Enabled = !isLocked;
            comboBoxItemGroupKitchens.Enabled = !isLocked;
            textBoxItemGroup.Focus();
        }

        private void buttonLock_Click(object sender, EventArgs e)
        {
            if (kitchens.Contains(comboBoxItemGroupKitchens.Text) == false)
            {
                MessageBox.Show("Invalid kitchen report!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Controllers.MstItemGroupController mstItemGroupController = new Controllers.MstItemGroupController();

                Entities.MstItemGroupEntity newItemGroupEntity = new Entities.MstItemGroupEntity()
                {
                    ItemGroup = textBoxItemGroup.Text,
                    ImagePath = textBoxItemGroupImagePath.Text,
                    KitchenReport = comboBoxItemGroupKitchens.Text
                };

                List<Entities.MstItemGroupItemEntity> obItemGroupItem = new List<Entities.MstItemGroupItemEntity>();

                foreach (DataGridViewRow row in dataGridViewItemGroupItemList.Rows)
                {
                    obItemGroupItem.Add(new Entities.MstItemGroupItemEntity()
                    {
                        Id = Convert.ToInt32(row.Cells[2].Value),
                        Show = Convert.ToBoolean(row.Cells[6].Value)
                    });
                }

                String[] lockItemGroup = mstItemGroupController.LockItemGroup(mstItemGroupEntity.Id, newItemGroupEntity, obItemGroupItem);
                if (lockItemGroup[1].Equals("0") == false)
                {
                    UpdateComponents(true);
                    mstItemGroupListForm.UpdateItemGroupListDataSource();
                }
                else
                {
                    UpdateComponents(false);
                    MessageBox.Show(lockItemGroup[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonUnlock_Click(object sender, EventArgs e)
        {
            Controllers.MstItemGroupController mstItemGroupController = new Controllers.MstItemGroupController();

            String[] unlockItemGroup = mstItemGroupController.UnlockItemGroup(mstItemGroupEntity.Id);
            if (unlockItemGroup[1].Equals("0") == false)
            {
                UpdateComponents(false);
                mstItemGroupListForm.UpdateItemGroupListDataSource();
            }
            else
            {
                UpdateComponents(true);
                MessageBox.Show(unlockItemGroup[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            sysSoftwareForm.RemoveTabPage();
        }


        public void UpdateItemGroupItemListDataSource()
        {
            SetItemGroupItemListDataSourceAsync();
        }

        public async void SetItemGroupItemListDataSourceAsync()
        {
            List<Entities.DgvMstItemGroupItemListEntity> getItemGroupItemListData = await GetItemGroupItemListDataTask();
            if (getItemGroupItemListData.Any())
            {
                itemGroupItemData = getItemGroupItemListData;
                itemGroupItemPageList = new PagedList<Entities.DgvMstItemGroupItemListEntity>(itemGroupItemData, itemGroupItemPageNumber, itemGroupItemPageSize);

                if (itemGroupItemPageList.PageCount == 1)
                {
                    buttonItemGroupItemListPageListFirst.Enabled = false;
                    buttonItemGroupItemListPageListPrevious.Enabled = false;
                    buttonItemGroupItemListPageListNext.Enabled = false;
                    buttonItemGroupItemListPageListLast.Enabled = false;
                }
                else if (itemGroupItemPageNumber == 1)
                {
                    buttonItemGroupItemListPageListFirst.Enabled = false;
                    buttonItemGroupItemListPageListPrevious.Enabled = false;
                    buttonItemGroupItemListPageListNext.Enabled = true;
                    buttonItemGroupItemListPageListLast.Enabled = true;
                }
                else if (itemGroupItemPageNumber == itemGroupItemPageList.PageCount)
                {
                    buttonItemGroupItemListPageListFirst.Enabled = true;
                    buttonItemGroupItemListPageListPrevious.Enabled = true;
                    buttonItemGroupItemListPageListNext.Enabled = false;
                    buttonItemGroupItemListPageListLast.Enabled = false;
                }
                else
                {
                    buttonItemGroupItemListPageListFirst.Enabled = true;
                    buttonItemGroupItemListPageListPrevious.Enabled = true;
                    buttonItemGroupItemListPageListNext.Enabled = true;
                    buttonItemGroupItemListPageListLast.Enabled = true;
                }

                textBoxItemGroupItemListPageNumber.Text = itemGroupItemPageNumber + " / " + itemGroupItemPageList.PageCount;
                itemGroupItemDataSource.DataSource = itemGroupItemPageList;
            }
            else
            {
                buttonItemGroupItemListPageListFirst.Enabled = false;
                buttonItemGroupItemListPageListPrevious.Enabled = false;
                buttonItemGroupItemListPageListNext.Enabled = false;
                buttonItemGroupItemListPageListLast.Enabled = false;

                itemGroupItemPageNumber = 1;

                itemGroupItemData = new List<Entities.DgvMstItemGroupItemListEntity>();
                itemGroupItemDataSource.Clear();
                textBoxItemGroupItemListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvMstItemGroupItemListEntity>> GetItemGroupItemListDataTask()
        {
            Controllers.MstItemGroupItemController trnItemGroupItemController = new Controllers.MstItemGroupItemController();

            List<Entities.MstItemGroupItemEntity> listItemGroupItem = trnItemGroupItemController.ListItemGroupItem(mstItemGroupEntity.Id);
            if (listItemGroupItem.Any())
            {
                var items = from d in listItemGroupItem
                            select new Entities.DgvMstItemGroupItemListEntity
                            {
                                ColumnItemGroupItemListButtonEdit = "Edit",
                                ColumnItemGroupItemListButtonDelete = "Delete",
                                ColumnItemGroupItemListId = d.Id,
                                ColumnItemGroupItemListItemId = d.ItemId,
                                ColumnItemGroupItemListItemDescription = d.ItemDescription,
                                ColumnItemGroupItemListItemGroupId = d.ItemGroupId,
                                ColumnItemGroupItemListShow = d.Show != null ? Convert.ToBoolean(d.Show) : false
                            };

                return Task.FromResult(items.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvMstItemGroupItemListEntity>());
            }
        }

        public void CreateItemGroupItemListDataGridView()
        {
            UpdateItemGroupItemListDataSource();

            dataGridViewItemGroupItemList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewItemGroupItemList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewItemGroupItemList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewItemGroupItemList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewItemGroupItemList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewItemGroupItemList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewItemGroupItemList.DataSource = itemGroupItemDataSource;
        }

        public void GetItemGroupItemListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void dataGridViewItemGroupItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetItemGroupItemListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewItemGroupItemList.CurrentCell.ColumnIndex == dataGridViewItemGroupItemList.Columns["ColumnItemGroupItemListButtonEdit"].Index)
            {
                var id = Convert.ToInt32(dataGridViewItemGroupItemList.Rows[e.RowIndex].Cells[dataGridViewItemGroupItemList.Columns["ColumnItemGroupItemListId"].Index].Value);
                var itemId = Convert.ToInt32(dataGridViewItemGroupItemList.Rows[e.RowIndex].Cells[dataGridViewItemGroupItemList.Columns["ColumnItemGroupItemListItemId"].Index].Value);
                var itemDescription = dataGridViewItemGroupItemList.Rows[e.RowIndex].Cells[dataGridViewItemGroupItemList.Columns["ColumnItemGroupItemListItemDescription"].Index].Value.ToString();
                var itemGroupId = Convert.ToInt32(dataGridViewItemGroupItemList.Rows[e.RowIndex].Cells[dataGridViewItemGroupItemList.Columns["ColumnItemGroupItemListItemGroupId"].Index].Value);

                Entities.MstItemGroupItemEntity mstItemGroupItemEntity = new Entities.MstItemGroupItemEntity()
                {
                    Id = id,
                    ItemId = itemId,
                    Barcode = "",
                    ItemDescription = itemDescription,
                    Alias = "",
                    ItemGroupId = itemGroupId
                };

                MstItemGroupSearchItemForm mstItemGroupSearchItemForm = new MstItemGroupSearchItemForm(this, mstItemGroupItemEntity);
                mstItemGroupSearchItemForm.ShowDialog();
            }

            if (e.RowIndex > -1 && dataGridViewItemGroupItemList.CurrentCell.ColumnIndex == dataGridViewItemGroupItemList.Columns["ColumnItemGroupItemListButtonDelete"].Index)
            {
                DialogResult deleteDialogResult = MessageBox.Show("Delete Item?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (deleteDialogResult == DialogResult.Yes)
                {
                    var id = Convert.ToInt32(dataGridViewItemGroupItemList.Rows[e.RowIndex].Cells[dataGridViewItemGroupItemList.Columns["ColumnItemGroupItemListId"].Index].Value);

                    Controllers.MstItemGroupItemController trnItemGroupItemController = new Controllers.MstItemGroupItemController();
                    String[] deleteItemGroupItem = trnItemGroupItemController.DeleteItemGroupItem(id);
                    if (deleteItemGroupItem[1].Equals("0") == false)
                    {
                        itemGroupItemPageNumber = 1;
                        UpdateItemGroupItemListDataSource();
                    }
                    else
                    {
                        MessageBox.Show(deleteItemGroupItem[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonItemGroupItemListPageListFirst_Click(object sender, EventArgs e)
        {
            itemGroupItemPageList = new PagedList<Entities.DgvMstItemGroupItemListEntity>(itemGroupItemData, 1, itemGroupItemPageSize);
            itemGroupItemDataSource.DataSource = itemGroupItemPageList;

            buttonItemGroupItemListPageListFirst.Enabled = false;
            buttonItemGroupItemListPageListPrevious.Enabled = false;
            buttonItemGroupItemListPageListNext.Enabled = true;
            buttonItemGroupItemListPageListLast.Enabled = true;

            itemGroupItemPageNumber = 1;
            textBoxItemGroupItemListPageNumber.Text = itemGroupItemPageNumber + " / " + itemGroupItemPageList.PageCount;
        }

        private void buttonItemGroupItemListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (itemGroupItemPageList.HasPreviousPage == true)
            {
                itemGroupItemPageList = new PagedList<Entities.DgvMstItemGroupItemListEntity>(itemGroupItemData, --itemGroupItemPageNumber, itemGroupItemPageSize);
                itemGroupItemDataSource.DataSource = itemGroupItemPageList;
            }

            buttonItemGroupItemListPageListNext.Enabled = true;
            buttonItemGroupItemListPageListLast.Enabled = true;

            if (itemGroupItemPageNumber == 1)
            {
                buttonItemGroupItemListPageListFirst.Enabled = false;
                buttonItemGroupItemListPageListPrevious.Enabled = false;
            }

            textBoxItemGroupItemListPageNumber.Text = itemGroupItemPageNumber + " / " + itemGroupItemPageList.PageCount;
        }

        private void buttonItemGroupItemListPageListNext_Click(object sender, EventArgs e)
        {
            if (itemGroupItemPageList.HasNextPage == true)
            {
                itemGroupItemPageList = new PagedList<Entities.DgvMstItemGroupItemListEntity>(itemGroupItemData, ++itemGroupItemPageNumber, itemGroupItemPageSize);
                itemGroupItemDataSource.DataSource = itemGroupItemPageList;
            }

            buttonItemGroupItemListPageListFirst.Enabled = true;
            buttonItemGroupItemListPageListPrevious.Enabled = true;

            if (itemGroupItemPageNumber == itemGroupItemPageList.PageCount)
            {
                buttonItemGroupItemListPageListNext.Enabled = false;
                buttonItemGroupItemListPageListLast.Enabled = false;
            }

            textBoxItemGroupItemListPageNumber.Text = itemGroupItemPageNumber + " / " + itemGroupItemPageList.PageCount;
        }

        private void buttonItemGroupItemListPageListLast_Click(object sender, EventArgs e)
        {
            itemGroupItemPageList = new PagedList<Entities.DgvMstItemGroupItemListEntity>(itemGroupItemData, itemGroupItemPageList.PageCount, itemGroupItemPageSize);
            itemGroupItemDataSource.DataSource = itemGroupItemPageList;

            buttonItemGroupItemListPageListFirst.Enabled = true;
            buttonItemGroupItemListPageListPrevious.Enabled = true;
            buttonItemGroupItemListPageListNext.Enabled = false;
            buttonItemGroupItemListPageListLast.Enabled = false;

            itemGroupItemPageNumber = itemGroupItemPageList.PageCount;
            textBoxItemGroupItemListPageNumber.Text = itemGroupItemPageNumber + " / " + itemGroupItemPageList.PageCount;
        }

        private void buttonSearchItem_Click(object sender, EventArgs e)
        {
            Entities.MstItemGroupItemEntity mstItemGroupItemEntity = new Entities.MstItemGroupItemEntity()
            {
                Id = 0,
                ItemId = 0,
                Barcode = "",
                ItemDescription = "",
                Alias = "",
                ItemGroupId = mstItemGroupEntity.Id
            };

            MstItemGroupSearchItemForm mstItemGroupSearchItemForm = new MstItemGroupSearchItemForm(this, mstItemGroupItemEntity);
            mstItemGroupSearchItemForm.ShowDialog();
        }
    }
}
