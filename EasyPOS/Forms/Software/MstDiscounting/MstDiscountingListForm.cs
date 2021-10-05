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

namespace EasyPOS.Forms.Software.MstDiscounting
{
    public partial class MstDiscountingListForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public static List<Entities.DgvMstDiscountingListEntity> discountListData = new List<Entities.DgvMstDiscountingListEntity>();
        public static Int32 pageNumber = 1;
        public static Int32 pageSize = 50;
        public PagedList<Entities.DgvMstDiscountingListEntity> discountListPageList = new PagedList<Entities.DgvMstDiscountingListEntity>(discountListData, pageNumber, pageSize);
        public BindingSource discountListDataSource = new BindingSource();

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();


        public MstDiscountingListForm(SysSoftwareForm softwareForm)
        {
            InitializeComponent();

            buttonAdd.Text = SetLabel(buttonAdd.Text);
            buttonClose.Text = SetLabel(buttonClose.Text);
            label1.Text = SetLabel(label1.Text);
            dataGridViewDiscountList.Columns[3].HeaderText = SetLabel(dataGridViewDiscountList.Columns[3].HeaderText);
            dataGridViewDiscountList.Columns[4].HeaderText = SetLabel(dataGridViewDiscountList.Columns[4].HeaderText);
            dataGridViewDiscountList.Columns[5].HeaderText = SetLabel(dataGridViewDiscountList.Columns[5].HeaderText);
            buttonDiscountListPageListFirst.Text = SetLabel(buttonDiscountListPageListFirst.Text);
            buttonDiscountListPageListLast.Text = SetLabel(buttonDiscountListPageListLast.Text);
            buttonDiscountListPageListNext.Text = SetLabel(buttonDiscountListPageListNext.Text);
            buttonDiscountListPageListPrevious.Text = SetLabel(buttonDiscountListPageListPrevious.Text);

            sysSoftwareForm = softwareForm;

            sysUserRights = new Modules.SysUserRightsModule("MstDiscount");
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
                    dataGridViewDiscountList.Columns[0].Visible = false;
                }

                if (sysUserRights.GetUserRights().CanDelete == false)
                {
                    dataGridViewDiscountList.Columns[1].Visible = false;
                }

                CreateDiscountListDataGridView();
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

        public void UpdateDiscountListDataSource()
        {
            SetDiscountListDataSourceAsync();
        }

        public async void SetDiscountListDataSourceAsync()
        {
            List<Entities.DgvMstDiscountingListEntity> getDiscountListData = await GetDiscountListDataTask();
            if (getDiscountListData.Any())
            {
                discountListData = getDiscountListData;
                discountListPageList = new PagedList<Entities.DgvMstDiscountingListEntity>(discountListData, pageNumber, pageSize);

                if (discountListPageList.PageCount == 1)
                {
                    buttonDiscountListPageListFirst.Enabled = false;
                    buttonDiscountListPageListPrevious.Enabled = false;
                    buttonDiscountListPageListNext.Enabled = false;
                    buttonDiscountListPageListLast.Enabled = false;
                }
                else if (pageNumber == 1)
                {
                    buttonDiscountListPageListFirst.Enabled = false;
                    buttonDiscountListPageListPrevious.Enabled = false;
                    buttonDiscountListPageListNext.Enabled = true;
                    buttonDiscountListPageListLast.Enabled = true;
                }
                else if (pageNumber == discountListPageList.PageCount)
                {
                    buttonDiscountListPageListFirst.Enabled = true;
                    buttonDiscountListPageListPrevious.Enabled = true;
                    buttonDiscountListPageListNext.Enabled = false;
                    buttonDiscountListPageListLast.Enabled = false;
                }
                else
                {
                    buttonDiscountListPageListFirst.Enabled = true;
                    buttonDiscountListPageListPrevious.Enabled = true;
                    buttonDiscountListPageListNext.Enabled = true;
                    buttonDiscountListPageListLast.Enabled = true;
                }

                textBoxDiscountListPageNumber.Text = pageNumber + " / " + discountListPageList.PageCount;
                discountListDataSource.DataSource = discountListPageList;
            }
            else
            {
                buttonDiscountListPageListFirst.Enabled = false;
                buttonDiscountListPageListPrevious.Enabled = false;
                buttonDiscountListPageListNext.Enabled = false;
                buttonDiscountListPageListLast.Enabled = false;

                pageNumber = 1;

                discountListData = new List<Entities.DgvMstDiscountingListEntity>();
                discountListDataSource.Clear();
                textBoxDiscountListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvMstDiscountingListEntity>> GetDiscountListDataTask()
        {
            String filter = textBoxDiscountListFilter.Text;
            Controllers.MstDiscountController mstDiscountController = new Controllers.MstDiscountController();

            List<Entities.MstDiscountEntity> listDiscount = mstDiscountController.ListDiscount(filter);
            if (listDiscount.Any())
            {
                var discounts = from d in listDiscount
                                select new Entities.DgvMstDiscountingListEntity
                                {
                                    ColumnDiscountListButtonEdit = SetLabel("Edit"),
                                    ColumnDiscountListButtonDelete = SetLabel("Delete"),
                                    ColumnDiscountListId = d.Id,
                                    ColumnDiscountListDiscount = d.Discount,
                                    ColumnDiscountListDiscountRate = d.DiscountRate.ToString("#,##0.00"),
                                    ColumnDiscountListIsLocked = d.IsLocked
                                };

                return Task.FromResult(discounts.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvMstDiscountingListEntity>());
            }
        }

        public void CreateDiscountListDataGridView()
        {
            UpdateDiscountListDataSource();

            dataGridViewDiscountList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewDiscountList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewDiscountList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewDiscountList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewDiscountList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewDiscountList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewDiscountList.DataSource = discountListDataSource;
        }

        public void GetDiscountListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            sysSoftwareForm.RemoveTabPage();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Controllers.MstDiscountController mstDiscountController = new Controllers.MstDiscountController();
            String[] addDiscount = mstDiscountController.AddDiscount();
            if (addDiscount[1].Equals("0") == false)
            {
                sysSoftwareForm.AddTabPageDiscountingDetail(this, mstDiscountController.DetailDiscount(Convert.ToInt32(addDiscount[1])));
                UpdateDiscountListDataSource();
            }
            else
            {
                MessageBox.Show(addDiscount[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewDiscountList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetDiscountListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewDiscountList.CurrentCell.ColumnIndex == dataGridViewDiscountList.Columns["ColumnDiscountListButtonEdit"].Index)
            {
                Controllers.MstDiscountController mstDiscountController = new Controllers.MstDiscountController();

                var discountDetail = new Modules.SysUserRightsModule("MstDiscountDetail");
                if (discountDetail.GetUserRights() == null)
                {
                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    sysSoftwareForm.AddTabPageDiscountingDetail(this, mstDiscountController.DetailDiscount(Convert.ToInt32(dataGridViewDiscountList.Rows[e.RowIndex].Cells[2].Value)));
                }
            }

            if (e.RowIndex > -1 && dataGridViewDiscountList.CurrentCell.ColumnIndex == dataGridViewDiscountList.Columns["ColumnDiscountListButtonDelete"].Index)
            {
                Boolean isLocked = Convert.ToBoolean(dataGridViewDiscountList.Rows[e.RowIndex].Cells[dataGridViewDiscountList.Columns["ColumnDiscountListIsLocked"].Index].Value);

                if (isLocked == true)
                {
                    MessageBox.Show("Already locked.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult deleteDialogResult = MessageBox.Show("Delete Discount?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (deleteDialogResult == DialogResult.Yes)
                    {
                        Controllers.MstDiscountController mstDiscountController = new Controllers.MstDiscountController();

                        String[] deleteDiscount = mstDiscountController.DeleteDiscount(Convert.ToInt32(dataGridViewDiscountList.Rows[e.RowIndex].Cells[2].Value));
                        if (deleteDiscount[1].Equals("0") == false)
                        {
                            Int32 currentPageNumber = pageNumber;

                            pageNumber = 1;
                            UpdateDiscountListDataSource();

                            if (discountListPageList != null)
                            {
                                if (discountListData.Count() % pageSize == 1)
                                {
                                    pageNumber = currentPageNumber - 1;
                                }
                                else if (discountListData.Count() < 1)
                                {
                                    pageNumber = 1;
                                }
                                else
                                {
                                    pageNumber = currentPageNumber;
                                }

                                discountListDataSource.DataSource = discountListPageList;
                            }
                        }
                        else
                        {
                            MessageBox.Show(deleteDiscount[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void textBoxDiscountListFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateDiscountListDataSource();
            }
        }

        private void buttonDiscountListPageListFirst_Click(object sender, EventArgs e)
        {
            discountListPageList = new PagedList<Entities.DgvMstDiscountingListEntity>(discountListData, 1, pageSize);
            discountListDataSource.DataSource = discountListPageList;

            buttonDiscountListPageListFirst.Enabled = false;
            buttonDiscountListPageListPrevious.Enabled = false;
            buttonDiscountListPageListNext.Enabled = true;
            buttonDiscountListPageListLast.Enabled = true;

            pageNumber = 1;
            textBoxDiscountListPageNumber.Text = pageNumber + " / " + discountListPageList.PageCount;
        }

        private void buttonDiscountListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (discountListPageList.HasPreviousPage == true)
            {
                discountListPageList = new PagedList<Entities.DgvMstDiscountingListEntity>(discountListData, --pageNumber, pageSize);
                discountListDataSource.DataSource = discountListPageList;
            }

            buttonDiscountListPageListNext.Enabled = true;
            buttonDiscountListPageListLast.Enabled = true;

            if (pageNumber == 1)
            {
                buttonDiscountListPageListFirst.Enabled = false;
                buttonDiscountListPageListPrevious.Enabled = false;
            }

            textBoxDiscountListPageNumber.Text = pageNumber + " / " + discountListPageList.PageCount;
        }

        private void buttonDiscountListPageListNext_Click(object sender, EventArgs e)
        {
            if (discountListPageList.HasNextPage == true)
            {
                discountListPageList = new PagedList<Entities.DgvMstDiscountingListEntity>(discountListData, ++pageNumber, pageSize);
                discountListDataSource.DataSource = discountListPageList;
            }

            buttonDiscountListPageListFirst.Enabled = true;
            buttonDiscountListPageListPrevious.Enabled = true;

            if (pageNumber == discountListPageList.PageCount)
            {
                buttonDiscountListPageListNext.Enabled = false;
                buttonDiscountListPageListLast.Enabled = false;
            }

            textBoxDiscountListPageNumber.Text = pageNumber + " / " + discountListPageList.PageCount;
        }

        private void buttonDiscountListPageListLast_Click(object sender, EventArgs e)
        {
            discountListPageList = new PagedList<Entities.DgvMstDiscountingListEntity>(discountListData, discountListPageList.PageCount, pageSize);
            discountListDataSource.DataSource = discountListPageList;

            buttonDiscountListPageListFirst.Enabled = true;
            buttonDiscountListPageListPrevious.Enabled = true;
            buttonDiscountListPageListNext.Enabled = false;
            buttonDiscountListPageListLast.Enabled = false;

            pageNumber = discountListPageList.PageCount;
            textBoxDiscountListPageNumber.Text = pageNumber + " / " + discountListPageList.PageCount;
        }
    }
}
