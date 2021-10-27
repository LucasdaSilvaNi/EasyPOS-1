using EasyPOS.Forms.Software.TrnPOS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Account.SysLogin
{
    public partial class SysLoginForm : Form
    {
        public TrnPOSBarcodeForm _trnPOSBarcodeForm;
        public TrnPOSBarcodeDetailForm _trnPOSBarcodeDetailForm;
        public TrnPOSTouchForm _trnPOSTouchForm;
        public TrnPOSTouchDetailForm _trnPOSTouchDetailForm;
        public Boolean _isOverride = false;
        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public SysLoginForm(TrnPOSBarcodeForm trnPOSBarcodeForm, TrnPOSBarcodeDetailForm trnPOSBarcodeDetailForm, TrnPOSTouchForm trnPOSTouchForm, TrnPOSTouchDetailForm trnPOSTouchDetailForm, Boolean isOverride)
        {
            InitializeComponent();

            Controllers.SysLanguageController sysLabel = new Controllers.SysLanguageController();
            if (sysLabel.ListLanguage("").Any())
            {
                sysLanguageEntities = sysLabel.ListLanguage("");
                var language = Modules.SysCurrentModule.GetCurrentSettings().Language;
                if (language != "English")
                {
                    buttonLogin.Text = SetLabel(buttonLogin.Text);
                    buttonClose.Text = SetLabel(buttonClose.Text);
                    radioButtonLoginDate.Text = SetLabel(radioButtonLoginDate.Text);
                    radioButtonSystemDate.Text = SetLabel(radioButtonSystemDate.Text);
                    label1.Text = SetLabel(label1.Text);
                    label6.Text = SetLabel(label6.Text);
                    label7.Text = SetLabel(label7.Text);
                    label8.Text = SetLabel(label8.Text);
                }
            }

            _isOverride = isOverride;

            if(Modules.SysCurrentModule.GetCurrentSettings().DateLogin == false)
            {
                radioButtonLoginDate.Enabled = false;
            }
            else
            {
                radioButtonLoginDate.Enabled = true;
            }

            if (Modules.SysCurrentModule.GetCurrentSettings().SwipeLogin == false)
            {
                textBoxUserCardNumber.Visible = false;
            }
            else
            {
                textBoxUserCardNumber.Visible = true;
            }
            dateTimePickerLoginDate.Enabled = false;
            textBoxUsername.Focus();
            labelVersion.Text = "EasyPOS Version: " + Modules.SysCurrentModule.GetCurrentSettings().CurrentVersion;
            labelSupport.Text = "Support: Easyfis Corporation " + Modules.SysCurrentModule.GetCurrentSettings().CurrentSupport;

            _trnPOSBarcodeForm = trnPOSBarcodeForm;
            _trnPOSBarcodeDetailForm = trnPOSBarcodeDetailForm;
            _trnPOSTouchForm = trnPOSTouchForm;
            _trnPOSTouchDetailForm = trnPOSTouchDetailForm;
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
            if (_trnPOSTouchForm != null || _trnPOSBarcodeForm != null)
            {
                if (Modules.SysCurrentModule.GetCurrentSettings().PromptLoginSales == false)
                {
                    Environment.Exit(0);
                }
                else
                {
                    Hide();
                }
            }
            else if (_trnPOSBarcodeDetailForm != null || _trnPOSTouchDetailForm != null)
            {
                Hide();
            }
            else
            {
                Environment.Exit(0);
            }
        }
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Login();
        }
        private bool CheckFormOpened(string name)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Name == name)
                {
                    return true;
                }
            }

            return false;
        }

        public void Login()
        {
            Controllers.SysLoginController sysLoginController = new Controllers.SysLoginController();
            String[] login = sysLoginController.Login(textBoxUserCardNumber.Text, textBoxUsername.Text, textBoxPassword.Text, dateTimePickerLoginDate.Value.ToShortDateString(), radioButtonLoginDate.Checked, _isOverride);
            if (login[1].Equals("0") == false)
            {
                if (_isOverride == true)
                {
                    if (_trnPOSTouchDetailForm != null)
                    {
                        _trnPOSTouchDetailForm.OverrideSales(Convert.ToInt32(login[1]));
                    }

                    if (_trnPOSBarcodeDetailForm != null)
                    {
                        _trnPOSBarcodeDetailForm.OverrideSales(Convert.ToInt32(login[1]));
                    }

                    Hide();
                }
                else
                {
                    if (Modules.SysCurrentModule.GetCurrentSettings().PromptLoginSales == false)
                    {
                        Software.SysSoftwareForm sysSoftwareForm = new Software.SysSoftwareForm();
                        sysSoftwareForm.Show();

                        Hide();
                    }
                    else
                    {
                        Hide();

                        if (CheckFormOpened("SysSoftwareForm") == false)
                        {
                            Software.SysSoftwareForm sysSoftwareForm = new Software.SysSoftwareForm();
                            sysSoftwareForm.Show();
                        }
                        else
                        {
                            if (_trnPOSTouchForm != null)
                            {
                                _trnPOSTouchForm.NewWalkInSales();
                            }

                            if (_trnPOSBarcodeForm != null)
                            {
                                _trnPOSBarcodeForm.newSales();
                            }

                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(login[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void textBoxUserCardNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }
        private void textBoxUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }

        private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }

        private void SysLoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isOverride == false)
            {
                if (_trnPOSTouchForm != null || _trnPOSTouchDetailForm != null || _trnPOSBarcodeForm != null || _trnPOSBarcodeDetailForm != null)
                {
                    if (Modules.SysCurrentModule.GetCurrentSettings().PromptLoginSales == false)
                    {
                        Environment.Exit(0);
                    }
                }
                else
                {
                    Environment.Exit(0);
                }
            }

        }

        private void radioButtonSystemDate_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerLoginDate.Enabled = false;
        }

        private void radioButtonLoginDate_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerLoginDate.Enabled = true;
        }
    }
}
