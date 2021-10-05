using EasyPOS.Forms.Software.RepSalesReport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.RepRemittanceReport
{
    public partial class RepRemittanceForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public RepRemittanceForm(SysSoftwareForm softwareForm)
        {
            InitializeComponent();

            label1.Text = SetLabel(label1.Text);
            label2.Text = SetLabel(label2.Text);
            labelTerminal.Text = SetLabel(labelTerminal.Text);
            labelStartDate.Text = SetLabel(labelStartDate.Text);
            labelEndDate.Text = SetLabel(labelEndDate.Text);
            labelUser.Text = SetLabel(labelUser.Text);
            labelRemittanceNumber.Text = SetLabel(labelRemittanceNumber.Text);
            buttonView.Text = SetLabel(buttonView.Text);
            buttonClose.Text = SetLabel(buttonClose.Text);


            //Listbox Change Language
            listBoxRemittanceReport.BeginUpdate();

            ListBox.ObjectCollection items = listBoxRemittanceReport.Items;
            int count = items.Count;

            for (int i = 0; i < count; i++)
            {
                // — Update The Item
                items[i] = SetLabel(items[i].ToString());
            }

            listBoxRemittanceReport.EndUpdate();
            //End Change Language

            sysSoftwareForm = softwareForm;

            GetTerminalList();
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

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void GetTerminalList()
        {
            Controllers.RepRemittanceReportController repRemittanceReportController = new Controllers.RepRemittanceReportController();
            if (repRemittanceReportController.DropdownListTerminal().Any())
            {
                comboBoxTerminal.DataSource = repRemittanceReportController.DropdownListTerminal();
                comboBoxTerminal.ValueMember = "Id";
                comboBoxTerminal.DisplayMember = "Terminal";

                GetUserList();
            }
        }

        public void GetUserList()
        {
            Controllers.RepRemittanceReportController repRemittanceReportController = new Controllers.RepRemittanceReportController();
            if (repRemittanceReportController.DropdownListUser().Any())
            {
                comboBoxUser.DataSource = repRemittanceReportController.DropdownListUser();
                comboBoxUser.ValueMember = "Id";
                comboBoxUser.DisplayMember = "FullName";
            }

            GetRemittanceNumberList();
        }

        public void GetRemittanceNumberList()
        {
            Int32 terminalId = Convert.ToInt32(comboBoxTerminal.SelectedValue);
            Int32 userId = Convert.ToInt32(comboBoxUser.SelectedValue);
            DateTime startDate = dateTimePickerStartDateFilter.Value.Date;
            DateTime endDate = dateTimePickerEndDateFilter.Value.Date;

            Controllers.RepRemittanceReportController repRemittanceReportController = new Controllers.RepRemittanceReportController();
            if (repRemittanceReportController.DropdownListRemittanceNumber(terminalId, userId, startDate, endDate).Any())
            {
                comboBoxRemittanceNumber.DataSource = repRemittanceReportController.DropdownListRemittanceNumber(terminalId, userId, startDate, endDate);
                comboBoxRemittanceNumber.ValueMember = "Id";
                comboBoxRemittanceNumber.DisplayMember = "DisbursementNumber";
            }
            else
            {
                comboBoxRemittanceNumber.DataSource = null;
                comboBoxRemittanceNumber.ValueMember = "Id";
                comboBoxRemittanceNumber.DisplayMember = "DisbursementNumber";
            }
        }

        private void listBoxRemittanceReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxRemittanceReport.SelectedItem != null)
            {
                String selectedItem = listBoxRemittanceReport.SelectedItem.ToString();
                switch (selectedItem)
                {
                    case "Remittance Report":
                    case "汇款报告":
                        labelTerminal.Visible = true;
                        comboBoxTerminal.Visible = true;
                        dateTimePickerStartDateFilter.Visible = true;
                        labelStartDate.Visible = true;
                        dateTimePickerEndDateFilter.Visible = true;
                        labelEndDate.Visible = true;
                        comboBoxUser.Visible = true;
                        labelUser.Visible = true;
                        comboBoxRemittanceNumber.Visible = true;
                        labelRemittanceNumber.Visible = true;
                        comboBoxTerminal.Focus();
                        break;
                    case "Cash In/Out Summary Report":
                    case "提现/提现汇总报告":
                        labelTerminal.Visible = true;
                        comboBoxTerminal.Visible = true;
                        dateTimePickerStartDateFilter.Visible = true;
                        labelStartDate.Visible = true;
                        dateTimePickerEndDateFilter.Visible = true;
                        labelEndDate.Visible = true;
                        comboBoxUser.Visible = false;
                        labelUser.Visible = false;
                        comboBoxRemittanceNumber.Visible = false;
                        labelRemittanceNumber.Visible = false;
                        comboBoxTerminal.Focus();
                        break;
                    default:
                        labelTerminal.Visible = false;
                        comboBoxTerminal.Visible = false;
                        dateTimePickerStartDateFilter.Visible = false;
                        labelStartDate.Visible = false;
                        dateTimePickerEndDateFilter.Visible = false;
                        labelEndDate.Visible = false;
                        comboBoxUser.Visible = false;
                        labelUser.Visible = false;
                        comboBoxRemittanceNumber.Visible = false;
                        labelRemittanceNumber.Visible = false;
                        break;
                }
            }
            else
            {
                MessageBox.Show("Please select a report.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonView_Click(object sender, EventArgs e)
        {
            if (listBoxRemittanceReport.SelectedItem != null)
            {
                String selectedItem = listBoxRemittanceReport.SelectedItem.ToString();
                switch (selectedItem)
                {
                    case "Remittance Report":
                    case "汇款报告":
                        sysUserRights = new Modules.SysUserRightsModule("RepDisbursementRemittance");
                        if (sysUserRights.GetUserRights() == null)
                        {
                            MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (sysUserRights.GetUserRights().CanView == true)
                            {
                                if (comboBoxRemittanceNumber.SelectedValue == null)
                                {
                                    MessageBox.Show("Please provide disbursement number.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    RepRemittanceReportForm repRemittanceReportRemittanceReport = new RepRemittanceReportForm(this, dateTimePickerStartDateFilter.Value.Date, dateTimePickerEndDateFilter.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue), Convert.ToInt32(comboBoxUser.SelectedValue), Convert.ToInt32(comboBoxRemittanceNumber.SelectedValue));
                                    repRemittanceReportRemittanceReport.ShowDialog();
                                }
                            }
                            else
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    case "Cash In/Out Summary Report":
                    case "提现/提现汇总报告":
                        sysUserRights = new Modules.SysUserRightsModule("RepDisbursementRemittance");
                        if (sysUserRights.GetUserRights() == null)
                        {
                            MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (sysUserRights.GetUserRights().CanView == true)
                            {
                                RepCashInOutSummaryReportForm repCashInOutSummaryReportForm = new RepCashInOutSummaryReportForm(dateTimePickerStartDateFilter.Value.Date, dateTimePickerEndDateFilter.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue));
                                repCashInOutSummaryReportForm.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    default:
                        MessageBox.Show("Please select a report.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                MessageBox.Show("Please select a report.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_OnClick(object sender, EventArgs e)
        {
            sysSoftwareForm.RemoveTabPage();
        }

        private void comboBoxTerminal_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetRemittanceNumberList();
        }

        private void comboBoxUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetRemittanceNumberList();
        }

        private void dateTimePickerStartDateFilter_ValueChanged(object sender, EventArgs e)
        {
            GetRemittanceNumberList();
        }

        private void dateTimePickerEndDateFilter_ValueChanged(object sender, EventArgs e)
        {
            GetRemittanceNumberList();
        }
    }
}
