using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.TrnDisbursement
{
    public partial class TrnDisbursementDetailForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public TrnDisbursementListForm trnDisbursementListForm;
        public Entities.TrnDisbursementEntity trnDisbursementEntity;

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();


        public TrnDisbursementDetailForm(SysSoftwareForm softwareForm, TrnDisbursementListForm disbursementListForm, Entities.TrnDisbursementEntity disbursementEntity)
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
                    buttonPrint.Text = SetLabel(buttonPrint.Text);
                    label1.Text = SetLabel(label1.Text);
                    label2.Text = SetLabel(label2.Text);
                    label3.Text = SetLabel(label3.Text);
                    label4.Text = SetLabel(label4.Text);
                    label5.Text = SetLabel(label5.Text);
                    label6.Text = SetLabel(label6.Text);
                    label7.Text = SetLabel(label7.Text);
                    label8.Text = SetLabel(label8.Text);
                    label9.Text = SetLabel(label9.Text);
                    label10.Text = SetLabel(label10.Text);
                    label15.Text = SetLabel(label15.Text);
                    label16.Text = SetLabel(label16.Text);
                    label17.Text = SetLabel(label17.Text);
                    label11.Text = SetLabel(label11.Text);
                    label12.Text = SetLabel(label12.Text);
                    label14.Text = SetLabel(label14.Text);
                    label13.Text = SetLabel(label13.Text);
                    label18.Text = SetLabel(label18.Text);
                    label19.Text = SetLabel(label19.Text);
                    checkBoxRefund.Text = SetLabel(checkBoxRefund.Text);
                }
            }

            sysSoftwareForm = softwareForm;
            trnDisbursementListForm = disbursementListForm;
            trnDisbursementEntity = disbursementEntity;

            sysUserRights = new Modules.SysUserRightsModule("TrnDisbursementDetail");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                GetTerminals();
                comboBoxTerminal.Focus();
            }

            var id = trnDisbursementEntity.Id;

            Controllers.TrnDisbursementController newDIsbursmeent = new Controllers.TrnDisbursementController();
            var detail = newDIsbursmeent.DetailDisbursement(id);

            if (detail != null)
            {
                sysSoftwareForm.displayTimeStamp(detail.EntryUserUsername, detail.EntryDateTime + " " + detail.EntryTime, detail.UpdateUserUsername, detail.UpdateDateTime + " " + detail.UpdateTime);
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

        public void GetTerminals()
        {
            Controllers.TrnDisbursementController trnDisbursementController = new Controllers.TrnDisbursementController();
            if (trnDisbursementController.DropdownListDisbursementTerminal().Any())
            {
                comboBoxTerminal.DataSource = trnDisbursementController.DropdownListDisbursementTerminal();
                comboBoxTerminal.ValueMember = "Id";
                comboBoxTerminal.DisplayMember = "Terminal";
            }

            GetRemittanceTypes();
        }

        public void GetRemittanceTypes()
        {
            Controllers.TrnDisbursementController trnDisbursementController = new Controllers.TrnDisbursementController();
            if (trnDisbursementController.DropdownListDisbursementRemittanceType().Any())
            {
                comboBoxRemittanceType.DataSource = trnDisbursementController.DropdownListDisbursementRemittanceType();
            }

            GetPayTypes();
        }

        public void GetPayTypes()
        {
            Controllers.TrnDisbursementController trnDisbursementController = new Controllers.TrnDisbursementController();
            if (trnDisbursementController.DropdownListDisbursementPayType().Any())
            {
                comboBoxPayType.DataSource = trnDisbursementController.DropdownListDisbursementPayType();
                comboBoxPayType.ValueMember = "Id";
                comboBoxPayType.DisplayMember = "PayType";
            }

            GetAccounts();
        }

        public void GetAccounts()
        {
            Controllers.TrnDisbursementController trnDisbursementController = new Controllers.TrnDisbursementController();
            if (trnDisbursementController.DropdownListDisbursementAccount().Any())
            {
                comboBoxAccount.DataSource = trnDisbursementController.DropdownListDisbursementAccount();
                comboBoxAccount.ValueMember = "Id";
                comboBoxAccount.DisplayMember = "Account";
            }

            GetUsers();
        }

        public void GetUsers()
        {
            Controllers.TrnDisbursementController trnDisbursementController = new Controllers.TrnDisbursementController();
            if (trnDisbursementController.DropdownListDisbursementUser().Any())
            {
                comboBoxPreparedBy.DataSource = trnDisbursementController.DropdownListDisbursementUser();
                comboBoxPreparedBy.ValueMember = "Id";
                comboBoxPreparedBy.DisplayMember = "FullName";

                comboBoxCheckedBy.DataSource = trnDisbursementController.DropdownListDisbursementUser();
                comboBoxCheckedBy.ValueMember = "Id";
                comboBoxCheckedBy.DisplayMember = "FullName";

                comboBoxApprovedBy.DataSource = trnDisbursementController.DropdownListDisbursementUser();
                comboBoxApprovedBy.ValueMember = "Id";
                comboBoxApprovedBy.DisplayMember = "FullName";
            }

            GetRemittanceDetail();
        }

        public void GetRemittanceDetail()
        {
            textBoxRemittanceNumber.Text = trnDisbursementEntity.DisbursementNumber;
            comboBoxTerminal.SelectedValue = trnDisbursementEntity.TerminalId;
            dateTimePickerRemittanceDate.Value = Convert.ToDateTime(trnDisbursementEntity.DisbursementDate);
            comboBoxRemittanceType.Text = trnDisbursementEntity.DisbursementType;
            comboBoxPayType.SelectedValue = trnDisbursementEntity.PayTypeId;
            comboBoxAccount.SelectedValue = trnDisbursementEntity.AccountId;
            textBoxPayee.Text = trnDisbursementEntity.Payee;
            textBoxAmount.Text = trnDisbursementEntity.Amount.ToString("#,##0.00");
            textBoxRemarks.Text = trnDisbursementEntity.Remarks;
            checkBoxRefund.Checked = trnDisbursementEntity.IsRefund;
            textBoxSalesReturnNumber.Text = trnDisbursementEntity.RefundSalesNumber;
            comboBoxPreparedBy.SelectedValue = trnDisbursementEntity.PreparedBy;
            comboBoxCheckedBy.SelectedValue = trnDisbursementEntity.CheckedBy;
            comboBoxApprovedBy.SelectedValue = trnDisbursementEntity.ApprovedBy;
            textBoxAmountDenominationXP1000.Text = trnDisbursementEntity.Amount1000 != null ? Convert.ToDecimal(trnDisbursementEntity.Amount1000).ToString("#,##0") : "0";
            textBoxAmountDenominationXP500.Text = trnDisbursementEntity.Amount500 != null ? Convert.ToDecimal(trnDisbursementEntity.Amount500).ToString("#,##0") : "0";
            textBoxAmountDenominationXP200.Text = trnDisbursementEntity.Amount200 != null ? Convert.ToDecimal(trnDisbursementEntity.Amount200).ToString("#,##0") : "0";
            textBoxAmountDenominationXP100.Text = trnDisbursementEntity.Amount100 != null ? Convert.ToDecimal(trnDisbursementEntity.Amount100).ToString("#,##0") : "0";
            textBoxAmountDenominationXP50.Text = trnDisbursementEntity.Amount50 != null ? Convert.ToDecimal(trnDisbursementEntity.Amount50).ToString("#,##0") : "0";
            textBoxAmountDenominationXP20.Text = trnDisbursementEntity.Amount20 != null ? Convert.ToDecimal(trnDisbursementEntity.Amount20).ToString("#,##0") : "0";
            textBoxAmountDenominationXP10.Text = trnDisbursementEntity.Amount10 != null ? Convert.ToDecimal(trnDisbursementEntity.Amount10).ToString("#,##0") : "0";
            textBoxAmountDenominationXP5.Text = trnDisbursementEntity.Amount5 != null ? Convert.ToDecimal(trnDisbursementEntity.Amount5).ToString("#,##0") : "0";
            textBoxAmountDenominationXP1.Text = trnDisbursementEntity.Amount1 != null ? Convert.ToDecimal(trnDisbursementEntity.Amount1).ToString("#,##0") : "0";
            textBoxAmountDenominationXC25.Text = trnDisbursementEntity.Amount025 != null ? Convert.ToDecimal(trnDisbursementEntity.Amount025).ToString("#,##0") : "0";
            textBoxAmountDenominationXC10.Text = trnDisbursementEntity.Amount010 != null ? Convert.ToDecimal(trnDisbursementEntity.Amount010).ToString("#,##0") : "0";
            textBoxAmountDenominationXC5.Text = trnDisbursementEntity.Amount005 != null ? Convert.ToDecimal(trnDisbursementEntity.Amount005).ToString("#,##0") : "0";
            textBoxAmountDenominationXC1.Text = trnDisbursementEntity.Amount001 != null ? Convert.ToDecimal(trnDisbursementEntity.Amount001).ToString("#,##0") : "0";

            EnableDisableControls(trnDisbursementEntity.IsLocked);
        }

        public void EnableDisableControls(Boolean isLocked)
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

            if (sysUserRights.GetUserRights().CanPrint == false)
            {
                buttonPrint.Enabled = false;
            }
            else
            {
                buttonPrint.Enabled = isLocked;
            }

            //comboBoxTerminal.Enabled = !isLocked;
            dateTimePickerRemittanceDate.Enabled = !isLocked;
            comboBoxRemittanceType.Enabled = !isLocked;
            comboBoxPayType.Enabled = !isLocked;
            comboBoxAccount.Enabled = !isLocked;
            textBoxPayee.Enabled = !isLocked;
            textBoxAmount.Enabled = !isLocked;
            textBoxRemarks.Enabled = !isLocked;
            checkBoxRefund.Enabled = !isLocked;
            textBoxSalesReturnNumber.Enabled = !isLocked;
            textBoxAmountDenominationXP1000.Enabled = !isLocked;
            textBoxAmountDenominationXP500.Enabled = !isLocked;
            textBoxAmountDenominationXP200.Enabled = !isLocked;
            textBoxAmountDenominationXP100.Enabled = !isLocked;
            textBoxAmountDenominationXP50.Enabled = !isLocked;
            textBoxAmountDenominationXP20.Enabled = !isLocked;
            textBoxAmountDenominationXP10.Enabled = !isLocked;
            textBoxAmountDenominationXP5.Enabled = !isLocked;
            textBoxAmountDenominationXP1.Enabled = !isLocked;
            textBoxAmountDenominationXC25.Enabled = !isLocked;
            textBoxAmountDenominationXC10.Enabled = !isLocked;
            textBoxAmountDenominationXC5.Enabled = !isLocked;
            textBoxAmountDenominationXC1.Enabled = !isLocked;
            comboBoxCheckedBy.Enabled = !isLocked;
            comboBoxApprovedBy.Enabled = !isLocked;
            comboBoxTerminal.Focus();
        }

        private void buttonLock_Click(object sender, EventArgs e)
        {
            Controllers.TrnDisbursementController trnDisbursementController = new Controllers.TrnDisbursementController();

            Entities.TrnDisbursementEntity newDisbursementEntity = new Entities.TrnDisbursementEntity()
            {
                PeriodId = 1,
                DisbursementDate = dateTimePickerRemittanceDate.Value.Date.ToShortDateString(),
                DisbursementType = comboBoxRemittanceType.Text,
                AccountId = Convert.ToInt32(comboBoxAccount.SelectedValue),
                Amount = Convert.ToDecimal(textBoxAmount.Text),
                PayTypeId = Convert.ToInt32(comboBoxPayType.SelectedValue),
                TerminalId = Convert.ToInt32(comboBoxTerminal.SelectedValue),
                Remarks = textBoxRemarks.Text,
                IsRefund = checkBoxRefund.Checked,
                RefundSalesId = 0,
                RefundSalesNumber = textBoxSalesReturnNumber.Text,
                StockInId = null,
                CheckedBy = Convert.ToInt32(comboBoxCheckedBy.SelectedValue),
                ApprovedBy = Convert.ToInt32(comboBoxApprovedBy.SelectedValue),
                Amount1000 = Convert.ToDecimal(textBoxAmountDenominationXP1000.Text),
                Amount500 = Convert.ToDecimal(textBoxAmountDenominationXP500.Text),
                Amount200 = Convert.ToDecimal(textBoxAmountDenominationXP200.Text),
                Amount100 = Convert.ToDecimal(textBoxAmountDenominationXP100.Text),
                Amount50 = Convert.ToDecimal(textBoxAmountDenominationXP50.Text),
                Amount20 = Convert.ToDecimal(textBoxAmountDenominationXP20.Text),
                Amount10 = Convert.ToDecimal(textBoxAmountDenominationXP10.Text),
                Amount5 = Convert.ToDecimal(textBoxAmountDenominationXP5.Text),
                Amount1 = Convert.ToDecimal(textBoxAmountDenominationXP1.Text),
                Amount025 = Convert.ToDecimal(textBoxAmountDenominationXC25.Text),
                Amount010 = Convert.ToDecimal(textBoxAmountDenominationXC10.Text),
                Amount005 = Convert.ToDecimal(textBoxAmountDenominationXC5.Text),
                Amount001 = Convert.ToDecimal(textBoxAmountDenominationXC1.Text),
                Payee = textBoxPayee.Text
            };

            String[] lockDisbursement = trnDisbursementController.LockDisbursement(trnDisbursementEntity.Id, newDisbursementEntity);
            if (lockDisbursement[1].Equals("0") == false)
            {
                EnableDisableControls(true);
                trnDisbursementListForm.UpdateDisbursementListDataSource();
            }
            else
            {
                MessageBox.Show(lockDisbursement[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUnlock_Click(object sender, EventArgs e)
        {
            Controllers.TrnDisbursementController trnDisbursementController = new Controllers.TrnDisbursementController();

            String[] unlockDisbursement = trnDisbursementController.UnlockDisbursement(trnDisbursementEntity.Id);
            if (unlockDisbursement[1].Equals("0") == false)
            {
                EnableDisableControls(false);
                trnDisbursementListForm.UpdateDisbursementListDataSource();
            }
            else
            {
                MessageBox.Show(unlockDisbursement[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            new TrnPrintDisbursementForm(trnDisbursementEntity.Id);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (textBoxAmount.Enabled == false)
            {
                sysSoftwareForm.RemoveTabPage();
            }
            else
            {
                DialogResult closeDialogResult = MessageBox.Show("Save Changes?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (closeDialogResult == DialogResult.Yes)
                {
                    Controllers.TrnDisbursementController trnDisbursementController = new Controllers.TrnDisbursementController();

                    Entities.TrnDisbursementEntity newDisbursementEntity = new Entities.TrnDisbursementEntity()
                    {
                        PeriodId = 1,
                        DisbursementDate = dateTimePickerRemittanceDate.Value.Date.ToShortDateString(),
                        DisbursementType = comboBoxRemittanceType.Text,
                        AccountId = Convert.ToInt32(comboBoxAccount.SelectedValue),
                        Amount = Convert.ToDecimal(textBoxAmount.Text),
                        PayTypeId = Convert.ToInt32(comboBoxPayType.SelectedValue),
                        TerminalId = Convert.ToInt32(comboBoxTerminal.SelectedValue),
                        Remarks = textBoxRemarks.Text,
                        IsRefund = checkBoxRefund.Checked,
                        RefundSalesId = 0,
                        RefundSalesNumber = textBoxSalesReturnNumber.Text,
                        StockInId = null,
                        CheckedBy = Convert.ToInt32(comboBoxCheckedBy.SelectedValue),
                        ApprovedBy = Convert.ToInt32(comboBoxApprovedBy.SelectedValue),
                        Amount1000 = Convert.ToDecimal(textBoxAmountDenominationXP1000.Text),
                        Amount500 = Convert.ToDecimal(textBoxAmountDenominationXP500.Text),
                        Amount200 = Convert.ToDecimal(textBoxAmountDenominationXP200.Text),
                        Amount100 = Convert.ToDecimal(textBoxAmountDenominationXP100.Text),
                        Amount50 = Convert.ToDecimal(textBoxAmountDenominationXP50.Text),
                        Amount20 = Convert.ToDecimal(textBoxAmountDenominationXP20.Text),
                        Amount10 = Convert.ToDecimal(textBoxAmountDenominationXP10.Text),
                        Amount5 = Convert.ToDecimal(textBoxAmountDenominationXP5.Text),
                        Amount1 = Convert.ToDecimal(textBoxAmountDenominationXP1.Text),
                        Amount025 = Convert.ToDecimal(textBoxAmountDenominationXC25.Text),
                        Amount010 = Convert.ToDecimal(textBoxAmountDenominationXC10.Text),
                        Amount005 = Convert.ToDecimal(textBoxAmountDenominationXC5.Text),
                        Amount001 = Convert.ToDecimal(textBoxAmountDenominationXC1.Text),
                        Payee = textBoxPayee.Text
                    };

                    String[] lockDisbursement = trnDisbursementController.SaveDisbursement(trnDisbursementEntity.Id, newDisbursementEntity);
                    if (lockDisbursement[1].Equals("0") == false)
                    {
                        sysSoftwareForm.RemoveTabPage();
                        trnDisbursementListForm.UpdateDisbursementListDataSource();
                    }
                    else
                    {
                        MessageBox.Show(lockDisbursement[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    sysSoftwareForm.RemoveTabPage();
                }
            }
        }

        private void checkBoxReturn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '-') && ((sender as TextBox).Text.IndexOf('-') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBoxAmount_Leave(object sender, EventArgs e)
        {
            //Decimal P1000 = Convert.ToDecimal(textBoxAmountDenominationXP1000.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXP1000.Text) * 1000 : 0;
            //Decimal P500 = Convert.ToDecimal(textBoxAmountDenominationXP500.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXP500.Text) * 500 : 0;
            //Decimal P200 = Convert.ToDecimal(textBoxAmountDenominationXP200.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXP200.Text) * 200 : 0;
            //Decimal P100 = Convert.ToDecimal(textBoxAmountDenominationXP100.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXP100.Text) * 100 : 0;
            //Decimal P50 = Convert.ToDecimal(textBoxAmountDenominationXP50.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXP50.Text) * 50 : 0;
            //Decimal P20 = Convert.ToDecimal(textBoxAmountDenominationXP20.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXP20.Text) * 20 : 0;
            //Decimal P10 = Convert.ToDecimal(textBoxAmountDenominationXP10.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXP10.Text) * 10 : 0;
            //Decimal P5 = Convert.ToDecimal(textBoxAmountDenominationXP5.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXP5.Text) * 5 : 0;
            //Decimal P1 = Convert.ToDecimal(textBoxAmountDenominationXP1.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXP1.Text) * 1 : 0;
            //Decimal C25 = Convert.ToDecimal(textBoxAmountDenominationXC25.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXC25.Text) * Convert.ToDecimal(1m / 4m) : 0;
            //Decimal C10 = Convert.ToDecimal(textBoxAmountDenominationXC10.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXC10.Text) * Convert.ToDecimal(1m / 10m) : 0;
            //Decimal C5 = Convert.ToDecimal(textBoxAmountDenominationXC5.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXC5.Text) * Convert.ToDecimal(1m / 20m) : 0;
            //Decimal C1 = Convert.ToDecimal(textBoxAmountDenominationXC1.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXC1.Text) * Convert.ToDecimal(1m / 100m) : 0;
            //Decimal totalAmount = P1000 + P500 + P200 + P100 + P50 + P20 + P10 + P5 + P1 + C25 + C10 + C5 + C1;

            //if (totalAmount != Convert.ToDecimal(textBoxAmount.Text))
            //{
            //    textBoxAmountDenominationXP1000.Text = "0";
            //    textBoxAmountDenominationXP500.Text = "0";
            //    textBoxAmountDenominationXP200.Text = "0";
            //    textBoxAmountDenominationXP100.Text = "0";
            //    textBoxAmountDenominationXP50.Text = "0";
            //    textBoxAmountDenominationXP20.Text = "0";
            //    textBoxAmountDenominationXP10.Text = "0";
            //    textBoxAmountDenominationXP5.Text = "0";
            //    textBoxAmountDenominationXP1.Text = "0";
            //    textBoxAmountDenominationXC25.Text = "0";
            //    textBoxAmountDenominationXC10.Text = "0";
            //    textBoxAmountDenominationXC5.Text = "0";
            //    textBoxAmountDenominationXC1.Text = "0";

            //    textBoxAmount.Text = Convert.ToDecimal(textBoxAmount.Text).ToString("#,##0.00");
            //}
        }

        private void textBoxAmountDenomination_Leave(object sender, EventArgs e)
        {
            Decimal P1000 = Convert.ToDecimal(textBoxAmountDenominationXP1000.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXP1000.Text) * 1000 : 0;
            Decimal P500 = Convert.ToDecimal(textBoxAmountDenominationXP500.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXP500.Text) * 500 : 0;
            Decimal P200 = Convert.ToDecimal(textBoxAmountDenominationXP200.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXP200.Text) * 200 : 0;
            Decimal P100 = Convert.ToDecimal(textBoxAmountDenominationXP100.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXP100.Text) * 100 : 0;
            Decimal P50 = Convert.ToDecimal(textBoxAmountDenominationXP50.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXP50.Text) * 50 : 0;
            Decimal P20 = Convert.ToDecimal(textBoxAmountDenominationXP20.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXP20.Text) * 20 : 0;
            Decimal P10 = Convert.ToDecimal(textBoxAmountDenominationXP10.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXP10.Text) * 10 : 0;
            Decimal P5 = Convert.ToDecimal(textBoxAmountDenominationXP5.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXP5.Text) * 5 : 0;
            Decimal P1 = Convert.ToDecimal(textBoxAmountDenominationXP1.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXP1.Text) * 1 : 0;
            Decimal C25 = Convert.ToDecimal(textBoxAmountDenominationXC25.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXC25.Text) * Convert.ToDecimal(1m / 4m) : 0;
            Decimal C10 = Convert.ToDecimal(textBoxAmountDenominationXC10.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXC10.Text) * Convert.ToDecimal(1m / 10m) : 0;
            Decimal C5 = Convert.ToDecimal(textBoxAmountDenominationXC5.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXC5.Text) * Convert.ToDecimal(1m / 20m) : 0;
            Decimal C1 = Convert.ToDecimal(textBoxAmountDenominationXC1.Text) > 0 ? Convert.ToDecimal(textBoxAmountDenominationXC1.Text) * Convert.ToDecimal(1m / 100m) : 0;
            Decimal totalAmount = P1000 + P500 + P200 + P100 + P50 + P20 + P10 + P5 + P1 + C25 + C10 + C5 + C1;

            textBoxAmount.Text = totalAmount.ToString("#,##0.00");
        }
    }
}
