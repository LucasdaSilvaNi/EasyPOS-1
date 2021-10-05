using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyPOS.Entities;

namespace EasyPOS.Forms.Software.SysSystemTables
{
    public partial class SysTaxDetailForm : Form
    {
        SysSystemTablesForm sysSystemTablesForm;
        private Modules.SysUserRightsModule sysUserRights;

        MstTaxEntity mstTaxEntity;

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public SysTaxDetailForm(SysSystemTablesForm systemTablesForm, MstTaxEntity taxEntity)
        {
            InitializeComponent();

            buttonSave.Text = SetLabel(buttonSave.Text);
            buttonClose.Text = SetLabel(buttonClose.Text);
            label1.Text = SetLabel(label1.Text);
            label2.Text = SetLabel(label2.Text);
            label3.Text = SetLabel(label3.Text);
            label4.Text = SetLabel(label4.Text);
            label5.Text = SetLabel(label5.Text);

            sysSystemTablesForm = systemTablesForm;
            mstTaxEntity = taxEntity;

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

                GetAccountList();
                textBoxCode.Focus();
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

        public void LoadTax()
        {
            if (mstTaxEntity != null)
            {
                textBoxCode.Text = mstTaxEntity.Code;
                textBoxTax.Text = mstTaxEntity.Tax;
                textBoxRate.Text = mstTaxEntity.Rate.ToString("#,##0.00");
                comboBoxAccount.SelectedValue = mstTaxEntity.AccountId;
            }
            else
            {
                textBoxRate.Text = "0.00";
            }
        }

        public void GetAccountList()
        {
            Controllers.MstTaxController mstTaxController = new Controllers.MstTaxController();
            var accounts = mstTaxController.DropDownListAccount();
            if (accounts.Any())
            {
                comboBoxAccount.DataSource = accounts;
                comboBoxAccount.ValueMember = "Id";
                comboBoxAccount.DisplayMember = "Account";

                LoadTax();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (mstTaxEntity == null)
            {
                MstTaxEntity newTax = new MstTaxEntity()
                {
                    Code = textBoxCode.Text,
                    Tax = textBoxTax.Text,
                    Rate = Convert.ToDecimal(textBoxRate.Text),
                    AccountId = Convert.ToInt32(comboBoxAccount.SelectedValue)
                };

                Controllers.MstTaxController mstTaxController = new Controllers.MstTaxController();
                String[] addTax = mstTaxController.AddTax(newTax);
                if (addTax[1].Equals("0") == true)
                {
                    MessageBox.Show(addTax[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    sysSystemTablesForm.UpdateTaxListDataSource();
                    Close();
                }
            }
            else
            {
                mstTaxEntity.Code = textBoxCode.Text;
                mstTaxEntity.Tax = textBoxTax.Text;
                mstTaxEntity.Rate = Convert.ToDecimal(textBoxRate.Text);
                mstTaxEntity.AccountId = Convert.ToInt32(comboBoxAccount.SelectedValue);

                Controllers.MstTaxController mstTaxController = new Controllers.MstTaxController();
                String[] updateTax = mstTaxController.UpdateTax(mstTaxEntity);
                if (updateTax[1].Equals("0") == true)
                {
                    MessageBox.Show(updateTax[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    sysSystemTablesForm.UpdateTaxListDataSource();
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
