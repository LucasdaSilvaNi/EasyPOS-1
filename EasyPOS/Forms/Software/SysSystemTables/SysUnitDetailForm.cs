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
    public partial class SysUnitDetailForm : Form
    {
        SysSystemTablesForm sysSystemTablesForm;
        private Modules.SysUserRightsModule sysUserRights;

        Entities.MstUnitEntity mstUnitEntity;

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public SysUnitDetailForm(SysSystemTablesForm systemTablesForm, Entities.MstUnitEntity unitEntity)
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
            mstUnitEntity = unitEntity;

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

                LoadUnit();
                textBoxUnit.Focus();
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

        public void LoadUnit()
        {
            if (mstUnitEntity != null)
            {
                textBoxUnit.Text = mstUnitEntity.Unit;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (mstUnitEntity == null)
            {
                Entities.MstUnitEntity newUnit = new Entities.MstUnitEntity()
                {
                    Unit = textBoxUnit.Text
                };

                Controllers.MstUnitController mstUnitController = new Controllers.MstUnitController();
                String[] addUnit = mstUnitController.AddUnit(newUnit);
                if (addUnit[1].Equals("0") == true)
                {
                    MessageBox.Show(addUnit[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    sysSystemTablesForm.UpdateUnitListDataSource();
                    Close();
                }
            }
            else
            {
                mstUnitEntity.Unit = textBoxUnit.Text;
                Controllers.MstUnitController mstUnitController = new Controllers.MstUnitController();
                String[] updateUnit = mstUnitController.UpdateUnit(mstUnitEntity);
                if (updateUnit[1].Equals("0") == true)
                {
                    MessageBox.Show(updateUnit[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    sysSystemTablesForm.UpdateUnitListDataSource();
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
