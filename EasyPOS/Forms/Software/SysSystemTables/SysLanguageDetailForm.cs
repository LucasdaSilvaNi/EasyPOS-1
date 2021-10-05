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

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public SysLanguageDetailForm(SysSystemTablesForm systemTablesForm, Entities.SysLanguageEntity languageEntity)
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
                    label3.Text = SetLabel(label3.Text);

                }
            }

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
