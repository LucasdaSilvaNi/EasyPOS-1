using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.SysSystemTables
{
    public partial class SysLanguageDetailForm : Form
    {
        SysSystemTablesForm sysSystemTablesForm;
        private Modules.SysUserRightsModule sysUserRights;

        Entities.SysLanguageEntity sysLanguagetEntity;
        public SysLanguageDetailForm(SysSystemTablesForm systemTablesForm, Entities.SysLanguageEntity languageEntity)
        {
            InitializeComponent();
            sysSystemTablesForm = systemTablesForm;
            sysLanguagetEntity = languageEntity;

            sysUserRights = new Modules.SysUserRightsModule("SysTables");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (sysUserRights.GetUserRights().CanAdd == false)
                {
                    buttonSave.Enabled = false;
                }

                LoadLanguage();
                textBoxLabel.Focus();
            }
        }
        public void LoadLanguage()
        {
            if (sysLanguagetEntity != null)
            {
                textBoxLabel.Text = sysLanguagetEntity.Label;
                textBoxDisplayLabel.Text = sysLanguagetEntity.DisplayedLabel;
            }
        }
        
        private void buttonSave_Click_1(object sender, EventArgs e)
        {
            if (sysLanguagetEntity == null)
            {
                Entities.SysLanguageEntity newLanguage = new Entities.SysLanguageEntity()
                {
                    Label = textBoxLabel.Text,
                    DisplayedLabel = textBoxDisplayLabel.Text
                };

                Controllers.SysLanguageController sysLanguageController = new Controllers.SysLanguageController();
                String[] addLanguage = sysLanguageController.AddLanguage(newLanguage);
                if (addLanguage[1].Equals("0") == true)
                {
                    MessageBox.Show(addLanguage[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    sysSystemTablesForm.UpdateLanguageListDataSource();
                    Close();
                }
            }
            else
            {
                sysLanguagetEntity.Label = textBoxLabel.Text;
                sysLanguagetEntity.DisplayedLabel = textBoxDisplayLabel.Text;
                Controllers.SysLanguageController sysLanguageController = new Controllers.SysLanguageController();
                String[] updateLanguage = sysLanguageController.UpdateLanguage(sysLanguagetEntity);
                if (updateLanguage[1].Equals("0") == true)
                {
                    MessageBox.Show(updateLanguage[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    sysSystemTablesForm.UpdateLanguageListDataSource();
                    Close();
                }

            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
