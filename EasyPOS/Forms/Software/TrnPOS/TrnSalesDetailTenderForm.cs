﻿using System;
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
    public partial class TrnSalesDetailTenderForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        public TrnSalesListForm trnSalesListForm;
        public TrnSalesDetailForm trnSalesDetailForm;
        public Entities.TrnSalesEntity trnSalesEntity;

        public TrnSalesDetailTenderForm(SysSoftwareForm softwareForm, TrnSalesListForm salesListForm, TrnSalesDetailForm salesDetailForm, Entities.TrnSalesEntity salesEntity)
        {
            InitializeComponent();

            sysSoftwareForm = softwareForm;

            trnSalesListForm = salesListForm;
            trnSalesDetailForm = salesDetailForm;

            trnSalesEntity = salesEntity;

            GetSalesDetail();
        }

        public void GetSalesDetail()
        {
            textBoxTotalSalesAmount.Text = trnSalesEntity.Amount.ToString("#,##0.00");
            labelInvoiceNumber.Text = trnSalesEntity.SalesNumber;
            labelInvoiceDate.Text = trnSalesEntity.SalesDate;
            labelCustomer.Text = trnSalesEntity.Customer;

            GetPayTypeList();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonTender_Click(object sender, EventArgs e)
        {
            TenderSales();
        }

        public void TenderSales()
        {
            if (Convert.ToDecimal(textBoxChangeAmount.Text) < 0)
            {
                buttonTender.Enabled = false;
                //MessageBox.Show("Change amount must be non-negative value.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                List<Entities.DgvSalesDetailTenderPayTypeEntity> payTypes = new List<Entities.DgvSalesDetailTenderPayTypeEntity>();
                foreach (DataGridViewRow row in dataGridViewTenderPayType.Rows)
                {
                    payTypes.Add(new Entities.DgvSalesDetailTenderPayTypeEntity()
                    {
                        PayType = row.Cells[1].Value.ToString(),
                        Amount = Convert.ToDecimal(row.Cells[2].Value),
                        OtherInformation = row.Cells[3].Value.ToString()
                    });
                }

                Decimal salesAmount = Convert.ToDecimal(textBoxTotalSalesAmount.Text);
                Decimal cashAmount = 0;
                Decimal nonCashAmount = 0;
                Decimal changeAmount = Convert.ToDecimal(textBoxChangeAmount.Text);

                var cashPayType = from d in payTypes where d.PayType.Equals("Cash") == true select d;
                if (cashPayType.Any())
                {
                    cashAmount = cashPayType.FirstOrDefault().Amount;
                }

                var nonCashPayType = from d in payTypes where d.PayType.Equals("Cash") == false select d;
                if (nonCashPayType.Any())
                {
                    nonCashAmount = nonCashPayType.Sum(d => d.Amount);
                }

                Boolean isValidTender = false;
                String invalidTenderMessage = "";

                if (cashAmount > 0)
                {
                    if (cashAmount >= changeAmount)
                    {
                        isValidTender = true;
                    }
                    else
                    {
                        invalidTenderMessage = "Cash amount must be greater than the change amount.";
                    }
                }
                else
                {
                    if (cashAmount == 0)
                    {
                        if (nonCashAmount == salesAmount)
                        {
                            isValidTender = true;
                        }
                        else
                        {
                            invalidTenderMessage = "Non-cash amount must be equal to the sales amount.";
                        }
                    }
                    else
                    {
                        invalidTenderMessage = "Cash amount must not below zero.";
                    }
                }

                if (isValidTender == true)
                {
                    buttonTender.Enabled = true;
                    CreateCollection();
                }
                else
                {
                    buttonTender.Enabled = false;
                    //MessageBox.Show(invalidTenderMessage, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void CreateCollection()
        {
            List<Entities.TrnCollectionLineEntity> listCollectionLine = new List<Entities.TrnCollectionLineEntity>();
            if (dataGridViewTenderPayType.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewTenderPayType.Rows)
                {
                    if (Convert.ToDecimal(row.Cells[2].Value) > 0)
                    {
                        listCollectionLine.Add(new Entities.TrnCollectionLineEntity()
                        {
                            Amount = Convert.ToDecimal(row.Cells[2].Value),
                            PayTypeId = Convert.ToInt32(row.Cells[0].Value),
                            CheckNumber = "NA",
                            CheckDate = null,
                            CheckBank = "NA",
                            CreditCardVerificationCode = "NA",
                            CreditCardNumber = "NA",
                            CreditCardType = "NA",
                            CreditCardBank = "NA",
                            GiftCertificateNumber = "NA",
                            OtherInformation = row.Cells[3].Value.ToString(),
                            CreditCardReferenceNumber = "NA",
                            CreditCardHolderName = "NA",
                            CreditCardExpiry = "NA"
                        });
                    }
                }
            }

            if (listCollectionLine.Any())
            {
                Entities.TrnCollectionEntity newCollection = new Entities.TrnCollectionEntity()
                {
                    TenderAmount = Convert.ToDecimal(textBoxTenderAmount.Text),
                    ChangeAmount = Convert.ToDecimal(textBoxChangeAmount.Text),
                    CollectionLines = listCollectionLine
                };

                Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
                String[] tenderSales = trnPOSSalesController.TenderSales(trnSalesEntity.Id, newCollection);
                if (tenderSales[1].Equals("0") == false)
                {
                    if (Modules.SysCurrentModule.GetCurrentSettings().IsTenderPrint == "true")
                    {
                        DialogResult tenderPrinterReadyDialogResult = MessageBox.Show("Is printer ready?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (tenderPrinterReadyDialogResult == DialogResult.Yes)
                        {
                            if (Modules.SysCurrentModule.GetCurrentSettings().CollectionReport == "Official Receipt")
                            {
                                new Reports.RepOfficialReceiptReportForm(trnSalesEntity.Id, Convert.ToInt32(tenderSales[1]), false);
                            }
                            else if (Modules.SysCurrentModule.GetCurrentSettings().CollectionReport == "Delivery Receipt")
                            {
                                new Reports.RepDeliveryReceiptReportForm(trnSalesEntity.Id, Convert.ToInt32(tenderSales[1]), false);
                            }
                        }
                    }

                    Close();

                    if (trnSalesDetailForm != null)
                    {
                        trnSalesDetailForm.Close();
                        sysSoftwareForm.RemoveTabPage();

                        trnSalesDetailForm.trnSalesListForm.newSales();
                    }
                    else
                    {
                        trnSalesListForm.UpdateSalesListGridDataSource();
                    }
                }
                else
                {
                    MessageBox.Show(tenderSales[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Cannot tender zero amount.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GetPayTypeList()
        {
            dataGridViewTenderPayType.Rows.Clear();
            dataGridViewTenderPayType.Refresh();

            Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();

            var payTypeList = trnPOSSalesController.TenderListPayType();
            if (payTypeList.Any())
            {
                dataGridViewTenderPayType.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#7FBC00");
                dataGridViewTenderPayType.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#7FBC00");
                dataGridViewTenderPayType.Columns[1].DefaultCellStyle.ForeColor = Color.White;

                foreach (var objPayTypeList in payTypeList)
                {
                    dataGridViewTenderPayType.Rows.Add(
                        objPayTypeList.Id,
                        objPayTypeList.PayType,
                        "0.00",
                        ""
                    );
                }
            }

            dataGridViewTenderPayType.Rows[0].Cells[2].Selected = true;
            ComputeAmount();
        }

        private void dataGridViewTenderPayType_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && dataGridViewTenderPayType.CurrentCell.ColumnIndex == dataGridViewTenderPayType.Columns["ColumnTenderListPayTypeOtherInformation"].Index)
                {
                    dataGridViewTenderPayType.CurrentCell.Value = Convert.ToDecimal(dataGridViewTenderPayType.CurrentCell.Value).ToString("#,##0.00");
                }

                ComputeAmount();
                TenderSales();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridViewTenderPayType.CurrentCell.Value = "0.00";
            }
        }

        public void ComputeAmount()
        {
            Decimal totalTenderAmount = 0;

            if (dataGridViewTenderPayType.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewTenderPayType.Rows)
                {
                    totalTenderAmount += Convert.ToDecimal(row.Cells[2].Value);
                }
            }

            textBoxTenderAmount.Text = totalTenderAmount.ToString("#,##0.00");

            Decimal changeAmount = totalTenderAmount - Convert.ToDecimal(textBoxTotalSalesAmount.Text);
            textBoxChangeAmount.Text = changeAmount.ToString("#,##0.00");
        }

        private void buttonSales_Click(object sender, EventArgs e)
        {
            TrnSalesDetailTenderSalesForm trnSalesDetailTenderSalesForm = new TrnSalesDetailTenderSalesForm(trnSalesDetailForm, this, trnSalesEntity);
            trnSalesDetailTenderSalesForm.ShowDialog();
        }

        private void dataGridViewTenderPayType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && dataGridViewTenderPayType.CurrentCell.ColumnIndex == dataGridViewTenderPayType.Columns["ColumnTenderListPayTypePayType"].Index)
            {
                TrnSalesDetailTenderMoreInformationForm trnSalesDetailTenderMoreInfoForm = new TrnSalesDetailTenderMoreInformationForm(this, dataGridViewTenderPayType);
                trnSalesDetailTenderMoreInfoForm.ShowDialog();
            }
        }

        private void TrnSalesDetailTenderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Modules.SysSerialPortModule.CloseSerialPort();
        }
    }
}