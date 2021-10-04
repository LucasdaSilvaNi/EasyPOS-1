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
    public partial class MstCopyUserRightsForm : Form
    {
        public MstUserDetailForm mstUserDetailForm;
        public Int32 userId;

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();


        public MstCopyUserRightsForm(MstUserDetailForm userDetailForm, Int32 currentUserId)
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
                    buttonSave.Text = SetLabel(buttonSave.Text);
                    label1.Text = SetLabel(label1.Text);
                    label2.Text = SetLabel(label2.Text);

                }
            }

            mstUserDetailForm = userDetailForm;
            userId = currentUserId;
            //sysUserRights = new Modules.SysUserRightsModule("SysTables");
            //if (sysUserRights.GetUserRights() == null)
            //{
            //    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //{
            //    if (sysUserRights.GetUserRights().CanAdd == false)
            //    {
            //        buttonSave.Enabled = false;
            //    }

            //    LoadUnit();
            //}
            LoadUser();
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

        public void LoadUser()
        {
            Controllers.MstUserFormController mstUserFormController = new Controllers.MstUserFormController();
            var users = mstUserFormController.ListUser();
            if (users.Any())
            {
                comboBoxUser.DataSource = users;
                comboBoxUser.ValueMember = "Id";
                comboBoxUser.DisplayMember = "UserName";
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Controllers.MstUserFormController mstUserFormController = new Controllers.MstUserFormController();
            var copyUserFormFromUserId = Convert.ToInt32(comboBoxUser.SelectedValue);
            if (userId != 0) {
                

                String[] copyUserForm = mstUserFormController.CopyUserRight(userId, copyUserFormFromUserId);
                if (copyUserForm[1].Equals("0") == false)
                {
                    mstUserDetailForm.UpdateUserFormListDataSource();
                    Close();
                }
                else
                {
                    MessageBox.Show(copyUserForm[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
