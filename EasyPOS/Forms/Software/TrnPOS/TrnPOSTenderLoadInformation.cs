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
    public partial class TrnPOSTenderLoadInformation : Form
    {
        public TrnPOSTenderForm trnPOSTenderForm;
        public DataGridView mstDataGridViewTenderPayType;

        public TrnPOSTenderLoadInformation(TrnPOSTenderForm POSTenderForm, DataGridView dataGridViewTenderPayType, Decimal totalSalesAmount)
        {
            InitializeComponent();

            trnPOSTenderForm = POSTenderForm;
            mstDataGridViewTenderPayType = dataGridViewTenderPayType;

            textBoxAmount.Text = totalSalesAmount.ToString("#,##0.00");
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {


            String customerCode = textBoxCardNumber.Text;
            Decimal amount = Convert.ToDecimal(textBoxAmount.Text);

            Controllers.MstCustomerController mstCustomerController = new Controllers.MstCustomerController();
            if (mstCustomerController.DetailCustomerPerCustomerCode(customerCode) != null)
            {
                Int32 customerId = mstCustomerController.DetailCustomerPerCustomerCode(customerCode).Id;
                String customer = mstCustomerController.DetailCustomerPerCustomerCode(customerCode).Customer;
                Int32 termId = mstCustomerController.DetailCustomerPerCustomerCode(customerCode).TermId;
                String address = mstCustomerController.DetailCustomerPerCustomerCode(customerCode).Address;

                Entities.TrnSalesEntity newSalesEntity = new Entities.TrnSalesEntity()
                {
                    CustomerId = customerId,
                    TermId = termId,
                    Remarks = "Sales Number: " + trnPOSTenderForm.trnSalesEntity.SalesNumber + ", Sales Date: " + trnPOSTenderForm.trnSalesEntity.SalesDate,
                    SalesAgent = Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId
                };

                Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
                String[] updateSales = trnPOSSalesController.TenderUpdateSales(trnPOSTenderForm.trnSalesEntity.Id, newSalesEntity);
                if (updateSales[1].Equals("0") == false)
                {
                    trnPOSTenderForm.trnSalesEntity.CustomerId = customerId;
                    trnPOSTenderForm.trnSalesEntity.CustomerCode = customerCode;
                    trnPOSTenderForm.trnSalesEntity.Customer = customer;
                    trnPOSTenderForm.trnSalesEntity.CustomerAddress = address;
                    trnPOSTenderForm.trnSalesEntity.Remarks = "Sales Number: " + trnPOSTenderForm.trnSalesEntity.SalesNumber + ", Sales Date: " + trnPOSTenderForm.trnSalesEntity.SalesDate;
                    trnPOSTenderForm.GetSalesDetail();

                    if (trnPOSTenderForm.trnPOSBarcodeDetailForm != null)
                    {
                        trnPOSTenderForm.trnPOSBarcodeDetailForm.trnSalesEntity.CustomerCode = customerCode;
                        trnPOSTenderForm.trnPOSBarcodeDetailForm.trnSalesEntity.Customer = customer;
                        trnPOSTenderForm.trnPOSBarcodeDetailForm.trnSalesEntity.Remarks = newSalesEntity.Remarks;
                        trnPOSTenderForm.trnPOSBarcodeDetailForm.GetSalesDetail();
                    }
                    else
                    {
                        if (trnPOSTenderForm.trnPOSBarcodeForm != null)
                        {
                            trnPOSTenderForm.trnPOSBarcodeForm.UpdateSalesListGridDataSource();
                        }
                    }

                    if (trnPOSTenderForm.trnPOSTouchDetailForm != null)
                    {
                        trnPOSTenderForm.trnPOSTouchDetailForm.trnSalesEntity.CustomerCode = customerCode;
                        trnPOSTenderForm.trnPOSTouchDetailForm.trnSalesEntity.Customer = customer;
                        trnPOSTenderForm.trnPOSTouchDetailForm.trnSalesEntity.Remarks = newSalesEntity.Remarks;
                        trnPOSTenderForm.trnPOSTouchDetailForm.GetSalesDetail();
                    }
                    else
                    {
                        if (trnPOSTenderForm.trnPOSTouchForm != null)
                        {
                            trnPOSTenderForm.trnPOSTouchForm.UpdateSalesListGridDataSource();
                        }
                    }

                    Decimal loadAmount = mstCustomerController.DetailCustomerPerCustomerCode(customerCode).LoadAmount;
                    if (loadAmount >= amount)
                    {
                        Entities.MstCustomerLoadEntity newCustomerLoad = new Entities.MstCustomerLoadEntity()
                        {
                            Id = 0,
                            CustomerId = mstCustomerController.DetailCustomerPerCustomerCode(customerCode).Id,
                            CardNumber = customerCode,
                            LoadDate = DateTime.Today.ToShortDateString(),
                            Type = "Sales",
                            Amount = amount * -1,
                            Remarks = "Sales Number: " + trnPOSTenderForm.trnSalesEntity.SalesNumber + ", Sales Date: " + trnPOSTenderForm.trnSalesEntity.SalesDate
                        };

                        Controllers.MstCustomerLoadController mstCustomerLoadController = new Controllers.MstCustomerLoadController();
                        String[] addCustomerLoad = mstCustomerLoadController.AddCustomerLoad(newCustomerLoad);

                        if (addCustomerLoad[1].Equals("0") == true)
                        {
                            MessageBox.Show(addCustomerLoad[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            LoadPay();
                            Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Insufficient Balance! Please Reload.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(updateSales[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Invalid card number!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadPay()
        {
            try
            {
                Decimal currentAmount = Convert.ToDecimal(textBoxAmount.Text);
                if (currentAmount >= 0)
                {
                    if (mstDataGridViewTenderPayType.Rows.Contains(mstDataGridViewTenderPayType.CurrentRow))
                    {
                        Int32 id = Convert.ToInt32(mstDataGridViewTenderPayType.CurrentRow.Cells[0].Value);
                        String payTypeCode = mstDataGridViewTenderPayType.CurrentRow.Cells[1].Value.ToString();
                        String payType = mstDataGridViewTenderPayType.CurrentRow.Cells[2].Value.ToString();
                        Decimal amount = Convert.ToDecimal(textBoxAmount.Text);
                        String otherInformation = "Reward Payment " + DateTime.Now.ToLongDateString();
                        String LoadNumber = textBoxCardNumber.Text;

                        mstDataGridViewTenderPayType.CurrentRow.Cells[0].Value = id;
                        mstDataGridViewTenderPayType.CurrentRow.Cells[1].Value = payTypeCode;
                        mstDataGridViewTenderPayType.CurrentRow.Cells[2].Value = payType;
                        mstDataGridViewTenderPayType.CurrentRow.Cells[4].Value = amount.ToString("#,##0.00");
                        mstDataGridViewTenderPayType.CurrentRow.Cells[5].Value = otherInformation;
                        mstDataGridViewTenderPayType.CurrentRow.Cells[6].Value = null;
                        mstDataGridViewTenderPayType.CurrentRow.Cells[7].Value = "";
                        mstDataGridViewTenderPayType.CurrentRow.Cells[8].Value = "NA";
                        mstDataGridViewTenderPayType.CurrentRow.Cells[9].Value = null;
                        mstDataGridViewTenderPayType.CurrentRow.Cells[10].Value = "NA";
                        mstDataGridViewTenderPayType.CurrentRow.Cells[11].Value = "NA";
                        mstDataGridViewTenderPayType.CurrentRow.Cells[12].Value = "NA";
                        mstDataGridViewTenderPayType.CurrentRow.Cells[13].Value = "NA";
                        mstDataGridViewTenderPayType.CurrentRow.Cells[14].Value = "NA";
                        mstDataGridViewTenderPayType.CurrentRow.Cells[15].Value = "NA";
                        mstDataGridViewTenderPayType.CurrentRow.Cells[16].Value = "NA";
                        mstDataGridViewTenderPayType.CurrentRow.Cells[17].Value = "NA";
                        mstDataGridViewTenderPayType.CurrentRow.Cells[18].Value = LoadNumber;
                    }

                    mstDataGridViewTenderPayType.Refresh();
                    Close();

                    mstDataGridViewTenderPayType.Focus();
                    mstDataGridViewTenderPayType.CurrentRow.Cells[2].Selected = true;

                    trnPOSTenderForm.ComputeAmount();
                    trnPOSTenderForm.CreateCollection(null);
                }
                else
                {
                    MessageBox.Show("Cannot pay if amount is zero.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxAmount_Leave(object sender, EventArgs e)
        {
            textBoxAmount.Text = Convert.ToDecimal(textBoxAmount.Text).ToString("#,##0.00");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    {
                        if (buttonOK.Enabled == true)
                        {
                            buttonOK.PerformClick();
                        }

                        break;
                    }
                case Keys.Escape:
                    {
                        Close();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
