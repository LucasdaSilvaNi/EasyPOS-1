using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.TrnPOS
{
    public partial class TrnPOSLockSalesForm : Form
    {
        public TrnPOSBarcodeDetailForm trnPOSBarcodeDetailForm;
        public TrnPOSTouchDetailForm trnPOSTouchDetailForm;

        public Entities.TrnSalesEntity trnSalesEntity;

        public String customerCode = "";
        public String customerName = "";
        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public TrnPOSLockSalesForm(TrnPOSBarcodeDetailForm POSBarcodeDetailForm, TrnPOSTouchDetailForm POSTouchDetailForm, Entities.TrnSalesEntity salesEntity)
        {
            InitializeComponent();

            Controllers.SysLanguageController sysLabel = new Controllers.SysLanguageController();
            if (sysLabel.ListLanguage("").Any())
            {
                sysLanguageEntities = sysLabel.ListLanguage("");
                var language = Modules.SysCurrentModule.GetCurrentSettings().Language;
                if (language != "English")
                {
                    label1.Text = SetLabel(label1.Text);
                    label2.Text = SetLabel(label2.Text);
                    label3.Text = SetLabel(label3.Text);
                    label4.Text = SetLabel(label4.Text);
                    label5.Text = SetLabel(label5.Text);
                    label6.Text = SetLabel(label6.Text);
                    label7.Text = SetLabel(label7.Text);
                    buttonSave.Text = SetLabel(buttonSave.Text);
                    buttonClose.Text = SetLabel(buttonClose.Text);
                }
            }

            trnPOSBarcodeDetailForm = POSBarcodeDetailForm;
            trnPOSTouchDetailForm = POSTouchDetailForm;
            trnSalesEntity = salesEntity;

            GetTermList();
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
            Close();
        }

        public void GetTermList()
        {
            Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
            if (trnPOSSalesController.TenderSalesDropdownListTerm().Any())
            {
                comboBoxTenderSalesTerms.DataSource = trnPOSSalesController.TenderSalesDropdownListTerm();
                comboBoxTenderSalesTerms.ValueMember = "Id";
                comboBoxTenderSalesTerms.DisplayMember = "Term";

                GetCustomerList();
            }


        }

        public void GetCustomerList()
        {
            if (Modules.SysCurrentModule.GetCurrentSettings().DisableSalesCustomerSelection == true)
            {
                comboBoxTenderSalesCustomer.Enabled = false;
            }
            else
            {
                comboBoxTenderSalesCustomer.Enabled = true;
            }

            Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
            if (trnPOSSalesController.TenderSalesDropdownListCustomer().Any())
            {
                comboBoxTenderSalesCustomer.DataSource = trnPOSSalesController.TenderSalesDropdownListCustomer();
                comboBoxTenderSalesCustomer.ValueMember = "Id";
                comboBoxTenderSalesCustomer.DisplayMember = "Customer";

                GetUserList();
            }

        }

        public void GetUserList()
        {
            Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
            if (trnPOSSalesController.TenderSalesDropdownListUser().Any())
            {
                comboBoxTenderSalesUsers.DataSource = trnPOSSalesController.TenderSalesDropdownListUser();
                comboBoxTenderSalesUsers.ValueMember = "Id";
                comboBoxTenderSalesUsers.DisplayMember = "FullName";

                var currentUserId = Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId;
                comboBoxTenderSalesUsers.SelectedValue = Convert.ToInt32(currentUserId);
            }

            textBoxCustomerCode.Text = trnSalesEntity.CustomerCode;
            comboBoxTenderSalesCustomer.SelectedValue = trnSalesEntity.CustomerId;
            comboBoxTenderSalesTerms.SelectedValue = trnSalesEntity.TermId;
            textBoxTenderSalesRemarks.Text = trnSalesEntity.Remarks;
            textBoxTenderedAmount.Text = trnSalesEntity.CollectedAmount.ToString("#,##0.00");
        }

        private void comboBoxTenderSalesCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTenderSalesCustomer.SelectedItem == null)
            {
                return;
            }

            var selectedItemCustomer = (Entities.MstCustomerEntity)comboBoxTenderSalesCustomer.SelectedItem;
            if (selectedItemCustomer != null)
            {
                customerCode = selectedItemCustomer.CustomerCode;
                customerName = selectedItemCustomer.Customer;
                textBoxTenderSalesRewardAvailable.Text = selectedItemCustomer.AvailableReward.ToString("#,##0.00");
                comboBoxTenderSalesTerms.SelectedValue = selectedItemCustomer.TermId;
                textBoxCustomerCode2.Text = selectedItemCustomer.CustomerCode;

            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if ((Convert.ToDecimal(textBoxTenderedAmount.Text) > 0 && Convert.ToDecimal(textBoxTenderedAmount.Text) >= trnSalesEntity.Amount) && textBoxTenderedAmount.Text != "")
            {
                Entities.TrnSalesEntity newSalesEntity = new Entities.TrnSalesEntity()
                {
                    CustomerId = Convert.ToInt32(comboBoxTenderSalesCustomer.SelectedValue),
                    TermId = Convert.ToInt32(comboBoxTenderSalesTerms.SelectedValue),
                    Remarks = textBoxTenderSalesRemarks.Text,
                    SalesAgent = Convert.ToInt32(comboBoxTenderSalesUsers.SelectedValue),
                    Amount = trnSalesEntity.Amount,
                    CollectedAmount = Convert.ToDecimal(textBoxTenderedAmount.Text),
                    OrderChangeAmount = Convert.ToDecimal(textBoxTenderedAmount.Text) - trnSalesEntity.Amount,
                };

                Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
                String[] updateSales = trnPOSSalesController.LockSales(trnSalesEntity.Id, newSalesEntity);
                if (updateSales[1].Equals("0") == false)
                {
                    if (trnPOSBarcodeDetailForm != null)
                    {
                        trnPOSBarcodeDetailForm.trnSalesEntity.CustomerCode = customerCode;
                        trnPOSBarcodeDetailForm.trnSalesEntity.Customer = customerName;
                        trnPOSBarcodeDetailForm.trnSalesEntity.Remarks = newSalesEntity.Remarks;
                        trnSalesEntity.CollectedAmount = newSalesEntity.CollectedAmount;
                        trnSalesEntity.OrderChangeAmount = newSalesEntity.OrderChangeAmount;

                        trnPOSBarcodeDetailForm.GetSalesDetail();
                        trnPOSBarcodeDetailForm.LockComponents(true);

                        trnPOSBarcodeDetailForm.trnSalesEntity.CustomerId = newSalesEntity.CustomerId;
                        trnPOSBarcodeDetailForm.trnSalesEntity.TermId = newSalesEntity.TermId;
                        trnPOSBarcodeDetailForm.trnSalesEntity.Remarks = newSalesEntity.Remarks;
                        trnPOSBarcodeDetailForm.trnSalesEntity.SalesAgent = newSalesEntity.SalesAgent;
                        trnSalesEntity.CollectedAmount = newSalesEntity.CollectedAmount;
                        trnSalesEntity.OrderChangeAmount = newSalesEntity.OrderChangeAmount;
                    }

                    if (trnPOSTouchDetailForm != null)
                    {
                        trnPOSTouchDetailForm.trnSalesEntity.CustomerCode = customerCode;
                        trnPOSTouchDetailForm.trnSalesEntity.Customer = customerName;
                        trnPOSTouchDetailForm.trnSalesEntity.Remarks = newSalesEntity.Remarks;

                        trnPOSTouchDetailForm.GetSalesDetail();
                        trnPOSTouchDetailForm.LockComponents(true);

                        trnPOSTouchDetailForm.trnSalesEntity.CustomerId = newSalesEntity.CustomerId;
                        trnPOSTouchDetailForm.trnSalesEntity.TermId = newSalesEntity.TermId;
                        trnPOSTouchDetailForm.trnSalesEntity.Remarks = newSalesEntity.Remarks;
                        trnPOSTouchDetailForm.trnSalesEntity.SalesAgent = newSalesEntity.SalesAgent;
                    }

                    Close();
                }
                else
                {
                    MessageBox.Show(updateSales[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid Tendered Amount!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxTenderedAmount.Focus();
                textBoxTenderedAmount.SelectAll();
            }
        }

        private void textBoxCustomerCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                List<Entities.MstCustomerEntity> customers = (List<Entities.MstCustomerEntity>)comboBoxTenderSalesCustomer.DataSource;
                var customer = from d in customers
                               where d.CustomerCode == textBoxCustomerCode.Text
                               select d;

                if (customer.Any())
                {
                    comboBoxTenderSalesCustomer.SelectedValue = customer.FirstOrDefault().Id;
                }
                else
                {
                    MessageBox.Show("Invalid customer code.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F2:
                    {
                        if (buttonSave.Enabled == true)
                        {
                            buttonSave.PerformClick();
                            Focus();
                        }

                        break;
                    }
                case Keys.Escape:
                    {
                        if (buttonClose.Enabled == true)
                        {
                            buttonClose.PerformClick();
                            Focus();
                        }

                        break;
                    }
                case Keys.Enter:
                    {
                        if (buttonSave.Enabled == true)
                        {
                            buttonSave.PerformClick();
                            Focus();
                        }

                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void comboBoxTenderSalesCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                String inputString = comboBoxTenderSalesCustomer.Text;
                comboBoxTenderSalesCustomer.SelectedIndex = comboBoxTenderSalesCustomer.FindString(inputString);
            }
        }
    }
}