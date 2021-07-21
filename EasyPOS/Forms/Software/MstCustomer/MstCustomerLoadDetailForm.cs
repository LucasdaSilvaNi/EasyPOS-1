using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.MstCustomer
{
    public partial class MstCustomerLoadDetailForm : Form
    {
        MstCustomerDetailForm mstCustomerDetailForm;
        Entities.MstCustomerLoadEntity mstCustomerLoadEntity;

        public MstCustomerLoadDetailForm(MstCustomerDetailForm customerDetailForm, Entities.MstCustomerLoadEntity customerLoadEntity)
        {
            InitializeComponent();

            mstCustomerDetailForm = customerDetailForm;
            mstCustomerLoadEntity = customerLoadEntity;

            LoadCustomerLoad();
        }

        public void LoadCustomerLoad()
        {
            if (mstCustomerLoadEntity != null)
            {
                textBoxAmount.Text = mstCustomerLoadEntity.Amount.ToString("#,##0.00");
            }
            else
            {
                textBoxAmount.Text = "0.00";
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (mstCustomerLoadEntity.Id == 0)
            {
                Entities.MstCustomerLoadEntity newCustomerLoad = new Entities.MstCustomerLoadEntity()
                {
                    Id = mstCustomerLoadEntity.Id,
                    CustomerId = mstCustomerLoadEntity.CustomerId,
                    CardNumber = mstCustomerDetailForm.mstCustomerEntity.CustomerCode,
                    LoadDate = DateTime.Today.ToShortDateString(),
                    Type = "Load",
                    Amount = Convert.ToDecimal(textBoxAmount.Text),
                    Remarks = "Beginning Balance"
                };

                Controllers.MstCustomerLoadController mstCustomerLoadController = new Controllers.MstCustomerLoadController();
                String[] addCustomerLoad = mstCustomerLoadController.AddCustomerLoad(newCustomerLoad);

                if (addCustomerLoad[1].Equals("0") == true)
                {
                    MessageBox.Show(addCustomerLoad[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    mstCustomerDetailForm.UpdateCustomerLoadListDataSource();
                    Close();
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
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
            textBoxAmount.Text = Convert.ToDecimal(textBoxAmount.Text).ToString("#,##0.00");
        }
    }
}
