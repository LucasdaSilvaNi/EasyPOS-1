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

namespace EasyPOS.Forms.Software.MstUser
{
    public partial class MstUserListForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public static List<Entities.DgvMstUserListEntity> userListData = new List<Entities.DgvMstUserListEntity>();
        public static Int32 pageNumber = 1;
        public static Int32 pageSize = 50;
        public PagedList<Entities.DgvMstUserListEntity> userListPageList = new PagedList<Entities.DgvMstUserListEntity>(userListData, pageNumber, pageSize);
        public BindingSource userListDataSource = new BindingSource();
        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public MstUserListForm(SysSoftwareForm softwareForm)
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
                    dataGridViewUserList.Columns[3].HeaderText = SetLabel(dataGridViewUserList.Columns[3].HeaderText);
                    dataGridViewUserList.Columns[4].HeaderText = SetLabel(dataGridViewUserList.Columns[4].HeaderText);
                    dataGridViewUserList.Columns[5].HeaderText = SetLabel(dataGridViewUserList.Columns[5].HeaderText);
                    buttonUserListPageListFirst.Text = SetLabel(buttonUserListPageListFirst.Text);
                    buttonUserListPageListLast.Text = SetLabel(buttonUserListPageListLast.Text);
                    buttonUserListPageListNext.Text = SetLabel(buttonUserListPageListNext.Text);
                    buttonUserListPageListPrevious.Text = SetLabel(buttonUserListPageListPrevious.Text);
                }
            }

            sysSoftwareForm = softwareForm;

            sysUserRights = new Modules.SysUserRightsModule("MstUser");
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
                    dataGridViewUserList.Columns[0].Visible = false;
                }

                if (sysUserRights.GetUserRights().CanDelete == false)
                {
                    dataGridViewUserList.Columns[1].Visible = false;
                }

                CreateUserListDataGridView();
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

        public void UpdateUserListDataSource()
        {
            SetUserListDataSourceAsync();
        }

        public async void SetUserListDataSourceAsync()
        {
            List<Entities.DgvMstUserListEntity> getUserListData = await GetUserListDataTask();
            if (getUserListData.Any())
            {
                userListData = getUserListData;
                userListPageList = new PagedList<Entities.DgvMstUserListEntity>(userListData, pageNumber, pageSize);

                if (userListPageList.PageCount == 1)
                {
                    buttonUserListPageListFirst.Enabled = false;
                    buttonUserListPageListPrevious.Enabled = false;
                    buttonUserListPageListNext.Enabled = false;
                    buttonUserListPageListLast.Enabled = false;
                }
                else if (pageNumber == 1)
                {
                    buttonUserListPageListFirst.Enabled = false;
                    buttonUserListPageListPrevious.Enabled = false;
                    buttonUserListPageListNext.Enabled = true;
                    buttonUserListPageListLast.Enabled = true;
                }
                else if (pageNumber == userListPageList.PageCount)
                {
                    buttonUserListPageListFirst.Enabled = true;
                    buttonUserListPageListPrevious.Enabled = true;
                    buttonUserListPageListNext.Enabled = false;
                    buttonUserListPageListLast.Enabled = false;
                }
                else
                {
                    buttonUserListPageListFirst.Enabled = true;
                    buttonUserListPageListPrevious.Enabled = true;
                    buttonUserListPageListNext.Enabled = true;
                    buttonUserListPageListLast.Enabled = true;
                }

                textBoxUserListPageNumber.Text = pageNumber + " / " + userListPageList.PageCount;
                userListDataSource.DataSource = userListPageList;
            }
            else
            {
                buttonUserListPageListFirst.Enabled = false;
                buttonUserListPageListPrevious.Enabled = false;
                buttonUserListPageListNext.Enabled = false;
                buttonUserListPageListLast.Enabled = false;

                pageNumber = 1;

                userListDataSource.Clear();
                textBoxUserListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvMstUserListEntity>> GetUserListDataTask()
        {
            String filter = textBoxUserListFilter.Text;
            Controllers.MstUserController mstUserController = new Controllers.MstUserController();

            List<Entities.MstUserEntity> listUser = mstUserController.ListUser(filter);
            if (listUser.Any())
            {
                var users = from d in listUser
                            select new Entities.DgvMstUserListEntity
                            {
                                ColumnUserListButtonEdit = "Edit",
                                ColumnUserListButtonDelete = "Delete",
                                ColumnUserListId = d.Id,
                                ColumnUserListUserName = d.UserName,
                                ColumnUserListFullName = d.FullName,
                                ColumnUserListIsLocked = d.IsLocked
                            };

                return Task.FromResult(users.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvMstUserListEntity>());
            }
        }

        public void CreateUserListDataGridView()
        {
            UpdateUserListDataSource();

            dataGridViewUserList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewUserList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewUserList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewUserList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewUserList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewUserList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewUserList.DataSource = userListDataSource;
        }

        public void GetUserListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void textBoxUserListFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateUserListDataSource();
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Controllers.MstUserController mstUserController = new Controllers.MstUserController();
            String[] addUser = mstUserController.AddUser();
            if (addUser[1].Equals("0") == false)
            {
                sysSoftwareForm.AddTabPageUserDetail(this, mstUserController.DetailUser(Convert.ToInt32(addUser[1])));
                UpdateUserListDataSource();
            }
            else
            {
                MessageBox.Show(addUser[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            sysSoftwareForm.RemoveTabPage();
        }

        private void dataGridViewUserList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetUserListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewUserList.CurrentCell.ColumnIndex == dataGridViewUserList.Columns["ColumnUserListButtonEdit"].Index)
            {
                Controllers.MstUserController mstUserController = new Controllers.MstUserController();
                sysSoftwareForm.AddTabPageUserDetail(this, mstUserController.DetailUser(Convert.ToInt32(dataGridViewUserList.Rows[e.RowIndex].Cells[2].Value)));
            }

            if (e.RowIndex > -1 && dataGridViewUserList.CurrentCell.ColumnIndex == dataGridViewUserList.Columns["ColumnUserListButtonDelete"].Index)
            {
                Boolean isLocked = Convert.ToBoolean(dataGridViewUserList.Rows[e.RowIndex].Cells[5].Value);

                if (isLocked == true)
                {
                    MessageBox.Show("Already locked.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult deleteDialogResult = MessageBox.Show("Delete User?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (deleteDialogResult == DialogResult.Yes)
                    {
                        Controllers.MstUserController mstUserController = new Controllers.MstUserController();

                        String[] deleteUser = mstUserController.DeleteUser(Convert.ToInt32(dataGridViewUserList.Rows[e.RowIndex].Cells[2].Value));
                        if (deleteUser[1].Equals("0") == false)
                        {
                            Int32 currentPageNumber = pageNumber;

                            pageNumber = 1;
                            UpdateUserListDataSource();

                            if (userListPageList != null)
                            {
                                if (userListData.Count() % pageSize == 1)
                                {
                                    pageNumber = currentPageNumber - 1;
                                }
                                else
                                {
                                    pageNumber = currentPageNumber;
                                }

                                userListDataSource.DataSource = userListPageList;
                            }
                        }
                        else
                        {
                            MessageBox.Show(deleteUser[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void buttonUserListPageListFirst_Click(object sender, EventArgs e)
        {
            userListPageList = new PagedList<Entities.DgvMstUserListEntity>(userListData, 1, pageSize);
            userListDataSource.DataSource = userListPageList;

            buttonUserListPageListFirst.Enabled = false;
            buttonUserListPageListPrevious.Enabled = false;
            buttonUserListPageListNext.Enabled = true;
            buttonUserListPageListLast.Enabled = true;

            pageNumber = 1;
            textBoxUserListPageNumber.Text = pageNumber + " / " + userListPageList.PageCount;
        }

        private void buttonUserListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (userListPageList.HasPreviousPage == true)
            {
                userListPageList = new PagedList<Entities.DgvMstUserListEntity>(userListData, --pageNumber, pageSize);
                userListDataSource.DataSource = userListPageList;
            }

            buttonUserListPageListNext.Enabled = true;
            buttonUserListPageListLast.Enabled = true;

            if (pageNumber == 1)
            {
                buttonUserListPageListFirst.Enabled = false;
                buttonUserListPageListPrevious.Enabled = false;
            }

            textBoxUserListPageNumber.Text = pageNumber + " / " + userListPageList.PageCount;
        }

        private void buttonUserListPageListNext_Click(object sender, EventArgs e)
        {
            if (userListPageList.HasNextPage == true)
            {
                userListPageList = new PagedList<Entities.DgvMstUserListEntity>(userListData, ++pageNumber, pageSize);
                userListDataSource.DataSource = userListPageList;
            }

            buttonUserListPageListFirst.Enabled = true;
            buttonUserListPageListPrevious.Enabled = true;

            if (pageNumber == userListPageList.PageCount)
            {
                buttonUserListPageListNext.Enabled = false;
                buttonUserListPageListLast.Enabled = false;
            }

            textBoxUserListPageNumber.Text = pageNumber + " / " + userListPageList.PageCount;
        }

        private void buttonUserListPageListLast_Click(object sender, EventArgs e)
        {
            userListPageList = new PagedList<Entities.DgvMstUserListEntity>(userListData, userListPageList.PageCount, pageSize);
            userListDataSource.DataSource = userListPageList;

            buttonUserListPageListFirst.Enabled = true;
            buttonUserListPageListPrevious.Enabled = true;
            buttonUserListPageListNext.Enabled = false;
            buttonUserListPageListLast.Enabled = false;

            pageNumber = userListPageList.PageCount;
            textBoxUserListPageNumber.Text = pageNumber + " / " + userListPageList.PageCount;
        }
    }
}
