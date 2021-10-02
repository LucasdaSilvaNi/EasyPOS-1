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
    public partial class MstUserDetailForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public MstUserListForm mstUserListForm;
        public Entities.MstUserEntity mstUserEntity;

        public static List<Entities.DgvMstUserFormListEntity> userFormData = new List<Entities.DgvMstUserFormListEntity>();
        public static Int32 userFormPageNumber = 1;
        public static Int32 userFormPageSize = 50;
        public PagedList<Entities.DgvMstUserFormListEntity> userFormPageList = new PagedList<Entities.DgvMstUserFormListEntity>(userFormData, userFormPageNumber, userFormPageSize);
        public BindingSource userFormDataSource = new BindingSource();

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public MstUserDetailForm(SysSoftwareForm softwareForm, MstUserListForm userListForm, Entities.MstUserEntity userEntity)
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
                    buttonLock.Text = SetLabel(buttonLock.Text);
                    buttonUnlock.Text = SetLabel(buttonUnlock.Text);
                    buttonAddUserForm.Text = SetLabel(buttonAddUserForm.Text);
                    buttonCopyUseRights.Text = SetLabel(buttonCopyUseRights.Text);
                    label1.Text = SetLabel(label1.Text);
                    label2.Text = SetLabel(label2.Text);
                    label3.Text = SetLabel(label3.Text);
                    label4.Text = SetLabel(label4.Text);
                    label5.Text = SetLabel(label5.Text);
                    dataGridViewUserFormList.Columns[4].HeaderText = SetLabel(dataGridViewUserFormList.Columns[4].HeaderText);
                    dataGridViewUserFormList.Columns[6].HeaderText = SetLabel(dataGridViewUserFormList.Columns[6].HeaderText);
                    dataGridViewUserFormList.Columns[7].HeaderText = SetLabel(dataGridViewUserFormList.Columns[7].HeaderText);
                    dataGridViewUserFormList.Columns[8].HeaderText = SetLabel(dataGridViewUserFormList.Columns[8].HeaderText);
                    dataGridViewUserFormList.Columns[9].HeaderText = SetLabel(dataGridViewUserFormList.Columns[9].HeaderText);
                    dataGridViewUserFormList.Columns[10].HeaderText = SetLabel(dataGridViewUserFormList.Columns[10].HeaderText);
                    dataGridViewUserFormList.Columns[11].HeaderText = SetLabel(dataGridViewUserFormList.Columns[11].HeaderText);
                    dataGridViewUserFormList.Columns[12].HeaderText = SetLabel(dataGridViewUserFormList.Columns[12].HeaderText);
                    dataGridViewUserFormList.Columns[13].HeaderText = SetLabel(dataGridViewUserFormList.Columns[13].HeaderText);
                    dataGridViewUserFormList.Columns[14].HeaderText = SetLabel(dataGridViewUserFormList.Columns[14].HeaderText);
                    dataGridViewUserFormList.Columns[15].HeaderText = SetLabel(dataGridViewUserFormList.Columns[15].HeaderText);
                    dataGridViewUserFormList.Columns[16].HeaderText = SetLabel(dataGridViewUserFormList.Columns[16].HeaderText);
                    dataGridViewUserFormList.Columns[17].HeaderText = SetLabel(dataGridViewUserFormList.Columns[17].HeaderText);
                    dataGridViewUserFormList.Columns[18].HeaderText = SetLabel(dataGridViewUserFormList.Columns[18].HeaderText);
                    buttonUserFormListPageListFirst.Text = SetLabel(buttonUserFormListPageListFirst.Text);
                    buttonUserFormListPageListLast.Text = SetLabel(buttonUserFormListPageListLast.Text);
                    buttonUserFormListPageListNext.Text = SetLabel(buttonUserFormListPageListNext.Text);
                    buttonUserFormListPageListPrevious.Text = SetLabel(buttonUserFormListPageListPrevious.Text);
                }
            }

            sysSoftwareForm = softwareForm;
            mstUserListForm = userListForm;
            mstUserEntity = userEntity;

            sysUserRights = new Modules.SysUserRightsModule("MstUserDetail");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (sysUserRights.GetUserRights().CanAdd == false)
                {
                    buttonAddUserForm.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanEdit == false)
                {
                    dataGridViewUserFormList.Columns[0].Visible = false;
                }
                if (sysUserRights.GetUserRights().CanDelete)
                {
                    dataGridViewUserFormList.Columns[1].Visible = false;
                }

                GetUserDetail();
                textBoxFullName.Focus();
            }
            var id = mstUserEntity.Id;

            Controllers.MstUserController mstUser = new Controllers.MstUserController();
            var detail = mstUser.DetailUser(id);

            if (detail != null)
            {
                sysSoftwareForm.displayTimeStamp(detail.EntryUserName, detail.EntryDateTime + " " + detail.EntryTime, detail.UpdatedUserName, detail.UpdateDateTime + " " + detail.UpdateTime);
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
        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (textBoxUserName.Enabled == false)
            {
                sysSoftwareForm.RemoveTabPage();
            }
            else
            {
                DialogResult closeDialogResult = MessageBox.Show("Save Changes?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (closeDialogResult == DialogResult.Yes)
                {
                    Controllers.MstUserController mstUserController = new Controllers.MstUserController();

                    Entities.MstUserEntity newUserEntity = new Entities.MstUserEntity()
                    {
                        UserName = textBoxUserName.Text,
                        Password = textBoxPassword.Text,
                        FullName = textBoxFullName.Text,
                        UserCardNumber = textBoxUserCardNumber.Text,
                    };

                    String[] lockUser = mstUserController.SaveUser(mstUserEntity.Id, newUserEntity);
                    if (lockUser[1].Equals("0") == false)
                    {
                        sysSoftwareForm.RemoveTabPage();
                        mstUserListForm.UpdateUserListDataSource();
                    }
                    else
                    {
                        MessageBox.Show(lockUser[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    sysSoftwareForm.RemoveTabPage();
                }
            }
            
        }

        public void GetUserDetail()
        {
            UpdateComponents(mstUserEntity.IsLocked);

            textBoxFullName.Text = mstUserEntity.FullName;
            textBoxUserName.Text = mstUserEntity.UserName;
            textBoxPassword.Text = mstUserEntity.Password;
            textBoxUserCardNumber.Text = mstUserEntity.UserCardNumber;

            CreateUserFormListDataGridView();
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

            textBoxFullName.Enabled = !isLocked;
            textBoxUserName.Enabled = !isLocked;
            textBoxPassword.Enabled = !isLocked;

            buttonAddUserForm.Enabled = !isLocked;
            buttonCopyUseRights.Enabled = !isLocked;

            dataGridViewUserFormList.Columns[0].Visible = !isLocked;
            dataGridViewUserFormList.Columns[1].Visible = !isLocked;
            textBoxFullName.Focus();
        }

        private void buttonLock_Click(object sender, EventArgs e)
        {
            Controllers.MstUserController mstUserController = new Controllers.MstUserController();

            Entities.MstUserEntity newUserEntity = new Entities.MstUserEntity()
            {
                UserName = textBoxUserName.Text,
                Password = textBoxPassword.Text,
                FullName = textBoxFullName.Text,
                UserCardNumber = textBoxUserCardNumber.Text,
            };

            String[] lockUser = mstUserController.LockUser(mstUserEntity.Id, newUserEntity);
            if (lockUser[1].Equals("0") == false)
            {
                UpdateComponents(true);
                mstUserListForm.UpdateUserListDataSource();
            }
            else
            {
                UpdateComponents(false);
                MessageBox.Show(lockUser[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUnlock_Click(object sender, EventArgs e)
        {
            Controllers.MstUserController mstUserController = new Controllers.MstUserController();

            String[] unlockUser = mstUserController.UnlockUser(mstUserEntity.Id);
            if (unlockUser[1].Equals("0") == false)
            {
                UpdateComponents(false);
                mstUserListForm.UpdateUserListDataSource();
            }
            else
            {
                UpdateComponents(true);
                MessageBox.Show(unlockUser[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateUserFormListDataSource()
        {
            SetUserFormListDataSourceAsync();
        }

        public async void SetUserFormListDataSourceAsync()
        {
            List<Entities.DgvMstUserFormListEntity> getUserFormListData = await GetUserFormListDataTask();
            if (getUserFormListData.Any())
            {
                userFormData = getUserFormListData;
                userFormPageList = new PagedList<Entities.DgvMstUserFormListEntity>(userFormData, userFormPageNumber, userFormPageSize);

                if (userFormPageList.PageCount == 1)
                {
                    buttonUserFormListPageListFirst.Enabled = false;
                    buttonUserFormListPageListPrevious.Enabled = false;
                    buttonUserFormListPageListNext.Enabled = false;
                    buttonUserFormListPageListLast.Enabled = false;
                }
                else if (userFormPageNumber == 1)
                {
                    buttonUserFormListPageListFirst.Enabled = false;
                    buttonUserFormListPageListPrevious.Enabled = false;
                    buttonUserFormListPageListNext.Enabled = true;
                    buttonUserFormListPageListLast.Enabled = true;
                }
                else if (userFormPageNumber == userFormPageList.PageCount)
                {
                    buttonUserFormListPageListFirst.Enabled = true;
                    buttonUserFormListPageListPrevious.Enabled = true;
                    buttonUserFormListPageListNext.Enabled = false;
                    buttonUserFormListPageListLast.Enabled = false;
                }
                else
                {
                    buttonUserFormListPageListFirst.Enabled = true;
                    buttonUserFormListPageListPrevious.Enabled = true;
                    buttonUserFormListPageListNext.Enabled = true;
                    buttonUserFormListPageListLast.Enabled = true;
                }

                textBoxUserFormListPageNumber.Text = userFormPageNumber + " / " + userFormPageList.PageCount;
                userFormDataSource.DataSource = userFormPageList;
            }
            else
            {
                buttonUserFormListPageListFirst.Enabled = false;
                buttonUserFormListPageListPrevious.Enabled = false;
                buttonUserFormListPageListNext.Enabled = false;
                buttonUserFormListPageListLast.Enabled = false;

                userFormPageNumber = 1;

                userFormData = new List<Entities.DgvMstUserFormListEntity>();
                userFormDataSource.Clear();
                textBoxUserFormListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvMstUserFormListEntity>> GetUserFormListDataTask()
        {
            Controllers.MstUserFormController trnUserFormController = new Controllers.MstUserFormController();

            List<Entities.MstUserFormEntity> listUserForm = trnUserFormController.ListUserForm(mstUserEntity.Id);
            if (listUserForm.Any())
            {
                var items = from d in listUserForm
                            select new Entities.DgvMstUserFormListEntity
                            {
                                ColumnUserFormListButtonEdit = "Edit",
                                ColumnUserFormListButtonDelete = "Delete",
                                ColumnUserFormListId = d.Id,
                                ColumnUserFormListFormId = d.FormId,
                                ColumnUserFormListForm = d.Form,
                                ColumnUserFormListUserId = d.UserId,
                                ColumnUserFormListCanDelete = d.CanDelete,
                                ColumnUserFormListCanAdd = d.CanAdd,
                                ColumnUserFormListCanLock = d.CanLock,
                                ColumnUserFormListCanUnlock = d.CanUnlock,
                                ColumnUserFormListCanPrint = d.CanPrint,
                                ColumnUserFormListCanPreview = d.CanPreview,
                                ColumnUserFormListCanEdit = d.CanEdit,
                                ColumnUserFormListCanTender = d.CanTender,
                                ColumnUserFormListCanDiscount = d.CanDiscount,
                                ColumnUserFormListCanView = d.CanView,
                                ColumnUserFormListCanSplit = d.CanSplit,
                                ColumnUserFormListCanCancel = d.CanCancel,
                                ColumnUserFormListCanReturn = d.CanReturn
                            };

                return Task.FromResult(items.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvMstUserFormListEntity>());
            }
        }

        public void CreateUserFormListDataGridView()
        {
            UpdateUserFormListDataSource();

            dataGridViewUserFormList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewUserFormList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewUserFormList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewUserFormList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewUserFormList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewUserFormList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewUserFormList.DataSource = userFormDataSource;
        }

        public void GetUserFormListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void dataGridViewUserFormList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetUserFormListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewUserFormList.CurrentCell.ColumnIndex == dataGridViewUserFormList.Columns["ColumnUserFormListButtonEdit"].Index)
            {
                var id = Convert.ToInt32(dataGridViewUserFormList.Rows[e.RowIndex].Cells[dataGridViewUserFormList.Columns["ColumnUserFormListId"].Index].Value);
                var formId = Convert.ToInt32(dataGridViewUserFormList.Rows[e.RowIndex].Cells[dataGridViewUserFormList.Columns["ColumnUserFormListFormId"].Index].Value);
                var formDescription = dataGridViewUserFormList.Rows[e.RowIndex].Cells[dataGridViewUserFormList.Columns["ColumnUserFormListForm"].Index].Value.ToString();
                var userId = Convert.ToInt32(dataGridViewUserFormList.Rows[e.RowIndex].Cells[dataGridViewUserFormList.Columns["ColumnUserFormListUserId"].Index].Value);
                var canDelete = Convert.ToBoolean(dataGridViewUserFormList.Rows[e.RowIndex].Cells[dataGridViewUserFormList.Columns["ColumnUserFormListCanDelete"].Index].Value);
                var canAdd = Convert.ToBoolean(dataGridViewUserFormList.Rows[e.RowIndex].Cells[dataGridViewUserFormList.Columns["ColumnUserFormListCanAdd"].Index].Value);
                var canLock = Convert.ToBoolean(dataGridViewUserFormList.Rows[e.RowIndex].Cells[dataGridViewUserFormList.Columns["ColumnUserFormListCanLock"].Index].Value);
                var canUnlock = Convert.ToBoolean(dataGridViewUserFormList.Rows[e.RowIndex].Cells[dataGridViewUserFormList.Columns["ColumnUserFormListCanUnlock"].Index].Value);
                var canPrint = Convert.ToBoolean(dataGridViewUserFormList.Rows[e.RowIndex].Cells[dataGridViewUserFormList.Columns["ColumnUserFormListCanPrint"].Index].Value);
                var canPreview = Convert.ToBoolean(dataGridViewUserFormList.Rows[e.RowIndex].Cells[dataGridViewUserFormList.Columns["ColumnUserFormListCanPreview"].Index].Value);
                var canEdit = Convert.ToBoolean(dataGridViewUserFormList.Rows[e.RowIndex].Cells[dataGridViewUserFormList.Columns["ColumnUserFormListCanEdit"].Index].Value);
                var canTender = Convert.ToBoolean(dataGridViewUserFormList.Rows[e.RowIndex].Cells[dataGridViewUserFormList.Columns["ColumnUserFormListCanTender"].Index].Value);
                var canDiscount = Convert.ToBoolean(dataGridViewUserFormList.Rows[e.RowIndex].Cells[dataGridViewUserFormList.Columns["ColumnUserFormListCanDiscount"].Index].Value);
                var canView = Convert.ToBoolean(dataGridViewUserFormList.Rows[e.RowIndex].Cells[dataGridViewUserFormList.Columns["ColumnUserFormListCanView"].Index].Value);
                var canSplit = Convert.ToBoolean(dataGridViewUserFormList.Rows[e.RowIndex].Cells[dataGridViewUserFormList.Columns["ColumnUserFormListCanSplit"].Index].Value);
                var canCancel = Convert.ToBoolean(dataGridViewUserFormList.Rows[e.RowIndex].Cells[dataGridViewUserFormList.Columns["ColumnUserFormListCanCancel"].Index].Value);
                var canReturn = Convert.ToBoolean(dataGridViewUserFormList.Rows[e.RowIndex].Cells[dataGridViewUserFormList.Columns["ColumnUserFormListCanReturn"].Index].Value);

                Entities.MstUserFormEntity mstUserFormEntity = new Entities.MstUserFormEntity()
                {
                    Id = id,
                    FormId = formId,
                    Form = formDescription,
                    UserId = mstUserEntity.Id,
                    CanDelete = canDelete,
                    CanAdd = canAdd,
                    CanLock = canLock,
                    CanUnlock = canUnlock,
                    CanPrint = canPrint,
                    CanPreview = canPreview,
                    CanEdit = canEdit,
                    CanTender = canTender,
                    CanDiscount = canDiscount,
                    CanView = canView,
                    CanSplit = canSplit,
                    CanCancel = canCancel,
                    CanReturn = canReturn
                };

                MstUserFormDetailForm mstUserDetailUserFormDetailForm = new MstUserFormDetailForm(this, mstUserFormEntity);
                mstUserDetailUserFormDetailForm.ShowDialog();
            }

            if (e.RowIndex > -1 && dataGridViewUserFormList.CurrentCell.ColumnIndex == dataGridViewUserFormList.Columns["ColumnUserFormListButtonDelete"].Index)
            {
                DialogResult deleteDialogResult = MessageBox.Show("Delete Form?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (deleteDialogResult == DialogResult.Yes)
                {
                    var id = Convert.ToInt32(dataGridViewUserFormList.Rows[e.RowIndex].Cells[dataGridViewUserFormList.Columns["ColumnUserFormListId"].Index].Value);

                    Controllers.MstUserFormController trnUserFormController = new Controllers.MstUserFormController();
                    String[] deleteUserForm = trnUserFormController.DeleteUserForm(id);
                    if (deleteUserForm[1].Equals("0") == false)
                    {
                        userFormPageNumber = 1;
                        UpdateUserFormListDataSource();
                    }
                    else
                    {
                        MessageBox.Show(deleteUserForm[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonUserFormListPageListFirst_Click(object sender, EventArgs e)
        {
            userFormPageList = new PagedList<Entities.DgvMstUserFormListEntity>(userFormData, 1, userFormPageSize);
            userFormDataSource.DataSource = userFormPageList;

            buttonUserFormListPageListFirst.Enabled = false;
            buttonUserFormListPageListPrevious.Enabled = false;
            buttonUserFormListPageListNext.Enabled = true;
            buttonUserFormListPageListLast.Enabled = true;

            userFormPageNumber = 1;
            textBoxUserFormListPageNumber.Text = userFormPageNumber + " / " + userFormPageList.PageCount;
        }

        private void buttonUserFormListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (userFormPageList.HasPreviousPage == true)
            {
                userFormPageList = new PagedList<Entities.DgvMstUserFormListEntity>(userFormData, --userFormPageNumber, userFormPageSize);
                userFormDataSource.DataSource = userFormPageList;
            }

            buttonUserFormListPageListNext.Enabled = true;
            buttonUserFormListPageListLast.Enabled = true;

            if (userFormPageNumber == 1)
            {
                buttonUserFormListPageListFirst.Enabled = false;
                buttonUserFormListPageListPrevious.Enabled = false;
            }

            textBoxUserFormListPageNumber.Text = userFormPageNumber + " / " + userFormPageList.PageCount;
        }

        private void buttonUserFormListPageListNext_Click(object sender, EventArgs e)
        {
            if (userFormPageList.HasNextPage == true)
            {
                userFormPageList = new PagedList<Entities.DgvMstUserFormListEntity>(userFormData, ++userFormPageNumber, userFormPageSize);
                userFormDataSource.DataSource = userFormPageList;
            }

            buttonUserFormListPageListFirst.Enabled = true;
            buttonUserFormListPageListPrevious.Enabled = true;

            if (userFormPageNumber == userFormPageList.PageCount)
            {
                buttonUserFormListPageListNext.Enabled = false;
                buttonUserFormListPageListLast.Enabled = false;
            }

            textBoxUserFormListPageNumber.Text = userFormPageNumber + " / " + userFormPageList.PageCount;
        }

        private void buttonUserFormListPageListLast_Click(object sender, EventArgs e)
        {
            userFormPageList = new PagedList<Entities.DgvMstUserFormListEntity>(userFormData, userFormPageList.PageCount, userFormPageSize);
            userFormDataSource.DataSource = userFormPageList;

            buttonUserFormListPageListFirst.Enabled = true;
            buttonUserFormListPageListPrevious.Enabled = true;
            buttonUserFormListPageListNext.Enabled = false;
            buttonUserFormListPageListLast.Enabled = false;

            userFormPageNumber = userFormPageList.PageCount;
            textBoxUserFormListPageNumber.Text = userFormPageNumber + " / " + userFormPageList.PageCount;
        }

        private void buttonAddUserForm_Click(object sender, EventArgs e)
        {
            Entities.MstUserFormEntity mstUserFormEntity = new Entities.MstUserFormEntity()
            {
                Id = 0,
                FormId = 0,
                Form = "",
                UserId = mstUserEntity.Id,
                CanDelete = false,
                CanAdd = false,
                CanLock = false,
                CanUnlock = false,
                CanPrint = false,
                CanPreview = false,
                CanEdit = false,
                CanTender = false,
                CanDiscount = false,
                CanView = false,
                CanSplit = false,
                CanCancel = false,
                CanReturn = false
            };

            MstUserFormDetailForm mstUserDetailUserFormDetailForm = new MstUserFormDetailForm(this, mstUserFormEntity);
            mstUserDetailUserFormDetailForm.ShowDialog();
        }

        private void buttonCopyUseRights_Click(object sender, EventArgs e)
        {
            MstCopyUserRightsForm mstUserCopyUserRightsForm = new MstCopyUserRightsForm(this, mstUserEntity.Id);
            mstUserCopyUserRightsForm.ShowDialog();
        }
    }
}
