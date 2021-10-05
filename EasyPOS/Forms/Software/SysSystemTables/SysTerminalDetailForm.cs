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
    public partial class SysTerminalDetailForm : Form
    {
        SysSystemTablesForm sysSystemTablesForm;
        private Modules.SysUserRightsModule sysUserRights;

        Entities.MstTerminalEntity mstTerminalEntity;

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public SysTerminalDetailForm(SysSystemTablesForm systemTablesForm, Entities.MstTerminalEntity terminalEntity)
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

            sysSystemTablesForm = systemTablesForm;
            mstTerminalEntity = terminalEntity;

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

                LoadTerminal();
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
        public void LoadTerminal()
        {
            if (mstTerminalEntity != null)
            {
                textBoxTerminal.Text = mstTerminalEntity.Terminal;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (mstTerminalEntity == null)
            {
                Entities.MstTerminalEntity newTerminal = new Entities.MstTerminalEntity()
                {
                    Terminal = textBoxTerminal.Text
                };

                Controllers.MstTerminalController mstTerminalController = new Controllers.MstTerminalController();
                String[] addTerminal = mstTerminalController.AddTerminal(newTerminal);
                if (addTerminal[1].Equals("0") == true)
                {
                    MessageBox.Show(addTerminal[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    sysSystemTablesForm.UpdateTerminalListDataSource();
                    Close();
                }
            }
            else
            {
                mstTerminalEntity.Terminal = textBoxTerminal.Text;
                Controllers.MstTerminalController mstTerminalController = new Controllers.MstTerminalController();
                String[] updateTerminal = mstTerminalController.UpdateTerminal(mstTerminalEntity);
                if (updateTerminal[1].Equals("0") == true)
                {
                    MessageBox.Show(updateTerminal[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    sysSystemTablesForm.UpdateTerminalListDataSource();
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
