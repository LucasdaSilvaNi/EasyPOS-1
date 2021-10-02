using EasyPOS.Data;
using OpenCvSharp.CPlusPlus;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.TrnPOS
{
    public partial class TrnPOSTenderForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;

        public TrnPOSBarcodeForm trnPOSBarcodeForm;
        public TrnPOSBarcodeDetailForm trnPOSBarcodeDetailForm;

        public TrnPOSTouchForm trnPOSTouchForm;
        public TrnPOSTouchDetailForm trnPOSTouchDetailForm;

        public Entities.TrnSalesEntity trnSalesEntity;
        public String collectionNumber = "";
        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public TrnPOSTenderForm(SysSoftwareForm softwareForm, TrnPOSBarcodeForm POSBarcodeForm, TrnPOSBarcodeDetailForm POSBarcodeDetailForm, TrnPOSTouchForm POSTouchForm, TrnPOSTouchDetailForm POSTouchDetailForm, Entities.TrnSalesEntity salesEntity)
        {
            InitializeComponent();

            Controllers.SysLanguageController sysLabel = new Controllers.SysLanguageController();
            if (sysLabel.ListLanguage("").Any())
            {
                sysLanguageEntities = sysLabel.ListLanguage("");
                var language = Modules.SysCurrentModule.GetCurrentSettings().Language;
                if (language != "English")
                {
                    buttonSales.Text = SetLabel(buttonSales.Text);
                    buttonTender.Text = SetLabel(buttonTender.Text);
                    buttonClose.Text = SetLabel(buttonClose.Text);
                    label1.Text = SetLabel(label1.Text);
                    label2.Text = SetLabel(label2.Text);
                    label3.Text = SetLabel(label3.Text);
                    label4.Text = SetLabel(label4.Text);
                    label5.Text = SetLabel(label5.Text);
                    label6.Text = SetLabel(label6.Text);
                    label7.Text = SetLabel(label7.Text);
                    label9.Text = SetLabel(label9.Text);
                    dataGridViewTenderPayType.Columns[2].HeaderText = SetLabel(dataGridViewTenderPayType.Columns[2].HeaderText);
                    dataGridViewTenderPayType.Columns[4].HeaderText = SetLabel(dataGridViewTenderPayType.Columns[4].HeaderText);
                }
            }

            sysSoftwareForm = softwareForm;

            trnPOSBarcodeForm = POSBarcodeForm;
            trnPOSBarcodeDetailForm = POSBarcodeDetailForm;

            trnPOSTouchForm = POSTouchForm;
            trnPOSTouchDetailForm = POSTouchDetailForm;

            trnSalesEntity = salesEntity;

            GetSalesDetail();
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

        public void GetSalesDetail()
        {
            textBoxTotalSalesAmount.Text = trnSalesEntity.Amount.ToString("#,##0.00");
            labelInvoiceNumber.Text = trnSalesEntity.SalesNumber;
            labelInvoiceDate.Text = trnSalesEntity.SalesDate;
            labelCustomerCode.Text = trnSalesEntity.CustomerCode;
            labelCustomer.Text = trnSalesEntity.Customer;
            labelRemarks.Text = trnSalesEntity.Remarks;

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
                List<Entities.DgvTrnSalesTenderPayTypeListEntity> payTypes = new List<Entities.DgvTrnSalesTenderPayTypeListEntity>();
                foreach (DataGridViewRow row in dataGridViewTenderPayType.Rows)
                {
                    payTypes.Add(new Entities.DgvTrnSalesTenderPayTypeListEntity()
                    {
                        Code = row.Cells[1].Value.ToString(),
                        PayType = row.Cells[2].Value.ToString(),
                        Amount = Convert.ToDecimal(row.Cells[4].Value),
                        OtherInformation = row.Cells[5].Value.ToString()
                    });
                }

                Decimal salesAmount = Convert.ToDecimal(textBoxTotalSalesAmount.Text);
                Decimal cashAmount = 0;
                Decimal nonCashAmount = 0;
                Decimal changeAmount = Convert.ToDecimal(textBoxChangeAmount.Text);

                var cashPayType = from d in payTypes where d.Code.Equals("CASH") == true select d;
                if (cashPayType.Any())
                {
                    cashAmount = cashPayType.FirstOrDefault().Amount;
                }

                var nonCashPayType = from d in payTypes where d.Code.Equals("CASH") == false select d;
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
                    CreateCollection(null);
                }
                else
                {
                    buttonTender.Enabled = false;
                    MessageBox.Show(invalidTenderMessage, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        public void CreateCollection(Image facepayCapturedImage)
        {
            List<Entities.TrnCollectionLineEntity> listCollectionLine = new List<Entities.TrnCollectionLineEntity>();
            if (dataGridViewTenderPayType.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewTenderPayType.Rows)
                {
                    Int32? salesReturnSalesId = null;
                    if (row.Cells[6].Value != null)
                    {
                        salesReturnSalesId = Convert.ToInt32(row.Cells[6].Value);
                    }

                    if (Convert.ToDecimal(row.Cells[4].Value) > 0)
                    {
                        String checkDate = null;
                        if (row.Cells[9].Value != null)
                        {
                            checkDate = row.Cells[9].Value.ToString();
                        }

                        listCollectionLine.Add(new Entities.TrnCollectionLineEntity()
                        {
                            Amount = Convert.ToDecimal(row.Cells[4].Value),
                            PayTypeId = Convert.ToInt32(row.Cells[0].Value),
                            CheckNumber = row.Cells[8].Value != null ? row.Cells[8].Value.ToString() : "NA",
                            CheckDate = checkDate,
                            CheckBank = row.Cells[10].Value != null ? row.Cells[10].Value.ToString() : "NA",
                            CreditCardVerificationCode = row.Cells[11].Value != null ? row.Cells[11].Value.ToString() : "NA",
                            CreditCardNumber = row.Cells[14].Value != null ? row.Cells[14].Value.ToString() : "NA",
                            CreditCardType = row.Cells[15].Value != null ? row.Cells[15].Value.ToString() : "NA",
                            CreditCardBank = row.Cells[16].Value != null ? row.Cells[16].Value.ToString() : "NA",
                            GiftCertificateNumber = row.Cells[18].Value != null ? row.Cells[18].Value.ToString() : "NA",
                            OtherInformation = row.Cells[5].Value != null ? row.Cells[5].Value.ToString() : "NA",
                            SalesReturnSalesId = salesReturnSalesId,
                            CreditCardReferenceNumber = row.Cells[12].Value != null ? row.Cells[12].Value.ToString() : "NA",
                            CreditCardHolderName = row.Cells[13].Value != null ? row.Cells[13].Value.ToString() : "NA",
                            CreditCardExpiry = row.Cells[17].Value != null ? row.Cells[17].Value.ToString() : "NA"
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
                    if (Modules.SysCurrentModule.GetCurrentSettings().IsTenderPrint == true)
                    {
                        DialogResult tenderPrinterReadyDialogResult = MessageBox.Show("Is printer ready?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (tenderPrinterReadyDialogResult == DialogResult.Yes)
                        {
                            if (Modules.SysCurrentModule.GetCurrentSettings().CollectionReport == "Official Receipt")
                            {
                                new TrnPOSOfficialReceiptReportForm(trnSalesEntity.Id, Convert.ToInt32(tenderSales[1]), false, "");
                            }
                            else if (Modules.SysCurrentModule.GetCurrentSettings().CollectionReport == "Delivery Receipt")
                            {
                                new TrnPOSDeliveryReceiptReportForm("", StockWithdrawalReport(trnSalesEntity.Id), true,false, true);
                            }
                            else
                            {

                            }
                        }
                    }

                    if (facepayCapturedImage != null)
                    {
                        SaveImageCaptured(facepayCapturedImage);
                    }
                    else
                    {
                        Close();
                    }

                    if (trnPOSBarcodeDetailForm != null)
                    {
                        trnPOSBarcodeDetailForm.Close();
                        sysSoftwareForm.RemoveTabPage();

                        if (trnPOSBarcodeForm != null)
                        {
                            trnPOSBarcodeDetailForm.trnSalesListForm.newSales();
                        }
                    }
                    else
                    {
                        if (trnPOSBarcodeForm != null)
                        {
                            trnPOSBarcodeForm.UpdateSalesListGridDataSource();
                        }
                    }

                    if (trnPOSTouchDetailForm != null)
                    {
                        trnPOSTouchDetailForm.Close();
                        sysSoftwareForm.RemoveTabPage();

                        if (trnPOSTouchForm != null)
                        {
                            trnPOSTouchForm.NewWalkInSales();
                        }
                    }
                    else
                    {
                        if (trnPOSTouchForm != null)
                        {
                            bool isPOSTouchActivityFormOpen = false;

                            FormCollection fc = Application.OpenForms;
                            foreach (Form frm in fc)
                            {
                                if (frm.Name == "TrnPOSTouchActivityForm")
                                {
                                    isPOSTouchActivityFormOpen = true;
                                }
                            }

                            if (isPOSTouchActivityFormOpen == true)
                            {
                                trnPOSTouchForm.ClosePOSTouchActivity();
                            }

                            trnPOSTouchForm.UpdateSalesListGridDataSource();
                        }
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

        // ===============================================
        // Stock Withdrawal Report (Copied) To be modified
        // ===============================================
        public List<Entities.RepSalesReportCollectionSummaryReportEntity> StockWithdrawalReport(Int32 salesId)
        {
            Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

            var stockWithdrawalReports = from d in db.TrnCollections
                                         where d.SalesId == salesId
                                         select new Entities.RepSalesReportCollectionSummaryReportEntity
                                         {
                                             Id = d.Id,
                                             SalesId = d.SalesId,
                                             CollectionNumber = d.CollectionNumber
                                         };

            return stockWithdrawalReports.ToList();
        }

        public void SaveImageCaptured(Image facepayCapturedImage)
        {
            try
            {
                Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
                Entities.TrnSalesEntity salesEntity = trnPOSSalesController.DetailSales(trnSalesEntity.Id);

                if (salesEntity != null && String.IsNullOrEmpty(salesEntity.CollectionNumber) == false)
                {
                    String imageName = salesEntity.CollectionNumber;
                    String facepayImagePath = Modules.SysCurrentModule.GetCurrentSettings().FacepayImagePath;

                    if (Directory.Exists(facepayImagePath) == false)
                    {
                        Directory.CreateDirectory(facepayImagePath);
                    }

                    facepayCapturedImage.Save(facepayImagePath + "\\" + imageName + ".jpeg", ImageFormat.Jpeg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                dataGridViewTenderPayType.Columns[2].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#7FBC00");
                dataGridViewTenderPayType.Columns[2].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#7FBC00");
                dataGridViewTenderPayType.Columns[2].DefaultCellStyle.ForeColor = Color.White;

                dataGridViewTenderPayType.Columns[3].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
                dataGridViewTenderPayType.Columns[3].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
                dataGridViewTenderPayType.Columns[3].DefaultCellStyle.ForeColor = Color.White;

                foreach (var objPayTypeList in payTypeList)
                {
                    dataGridViewTenderPayType.Rows.Add(
                        objPayTypeList.Id,
                        objPayTypeList.PayTypeCode,
                        objPayTypeList.PayType,
                        "#",
                        "0.00",
                        "",
                        null,
                        ""
                    );
                }
            }

            dataGridViewTenderPayType.Rows[0].Cells[4].Selected = true;
            ComputeAmount();
        }

        private void dataGridViewTenderPayType_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1 && dataGridViewTenderPayType.CurrentCell.ColumnIndex == dataGridViewTenderPayType.Columns["ColumnTenderListPayTypeAmount"].Index)
                {
                    Decimal amount = Convert.ToDecimal(dataGridViewTenderPayType.CurrentCell.Value);
                    dataGridViewTenderPayType.CurrentCell.Value = amount.ToString("#,##0.00");

                    if (dataGridViewTenderPayType.CurrentRow.Cells[1].Value.ToString() == "EXCHANGE")
                    {
                        if (dataGridViewTenderPayType.CurrentRow.Cells[4].Value.ToString() == "0.00")
                        {
                            dataGridViewTenderPayType.CurrentRow.Cells[6].Value = null;
                            dataGridViewTenderPayType.CurrentRow.Cells[7].Value = "";
                        }
                    }
                }

                Decimal totalTenderAmount = 0;

                if (dataGridViewTenderPayType.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dataGridViewTenderPayType.Rows)
                    {
                        totalTenderAmount += Convert.ToDecimal(row.Cells[4].Value);
                    }
                }

                textBoxTenderAmount.Text = totalTenderAmount.ToString("#,##0.00");

                Decimal changeAmount = totalTenderAmount - Convert.ToDecimal(textBoxTotalSalesAmount.Text);
                textBoxChangeAmount.Text = changeAmount.ToString("#,##0.00");

                buttonTender.Enabled = true;

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
                    totalTenderAmount += Convert.ToDecimal(row.Cells[4].Value);
                }
            }

            textBoxTenderAmount.Text = totalTenderAmount.ToString("#,##0.00");

            Decimal changeAmount = totalTenderAmount - Convert.ToDecimal(textBoxTotalSalesAmount.Text);
            textBoxChangeAmount.Text = changeAmount.ToString("#,##0.00");

            buttonTender.Enabled = true;
        }

        private void buttonSales_Click(object sender, EventArgs e)
        {
            TrnPOSTenderSalesForm trnSalesDetailTenderSalesForm = new TrnPOSTenderSalesForm(trnPOSBarcodeForm, trnPOSBarcodeDetailForm, trnPOSTouchForm, trnPOSTouchDetailForm, this, trnSalesEntity);
            trnSalesDetailTenderSalesForm.ShowDialog();
        }

        private void dataGridViewTenderPayType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && dataGridViewTenderPayType.CurrentCell.ColumnIndex == dataGridViewTenderPayType.Columns["ColumnTenderListPayTypePayType"].Index)
            {
                Decimal totalTenderAmount = 0;

                if (dataGridViewTenderPayType.Rows.Count > 0)
                {
                    foreach (DataGridViewRow row in dataGridViewTenderPayType.Rows)
                    {
                        totalTenderAmount += Convert.ToDecimal(row.Cells[4].Value);
                    }
                }

                Decimal payAmount = trnSalesEntity.Amount - totalTenderAmount;

                String payTypeCode = dataGridViewTenderPayType.Rows[dataGridViewTenderPayType.CurrentCell.RowIndex].Cells[dataGridViewTenderPayType.Columns["ColumnTenderListPayTypePayTypeCode"].Index].Value.ToString();
                if (payTypeCode == "EASYPAY")
                {
                    TrnPOSTenderEasypayInformationForm trnSalesDetailTenderEasypayInformationForm = new TrnPOSTenderEasypayInformationForm(this, dataGridViewTenderPayType, payAmount, trnSalesEntity.Id);
                    trnSalesDetailTenderEasypayInformationForm.ShowDialog();
                }
                else if (payTypeCode == "FACEPAY")
                {
                    TrnPOSTenderFacepayCameraForm trnSalesDetailTenderFacepayCameraForm = new TrnPOSTenderFacepayCameraForm(this, dataGridViewTenderPayType, payAmount);
                    trnSalesDetailTenderFacepayCameraForm.ShowDialog();
                }
                else if (payTypeCode == "CHARGE")
                {
                    dataGridViewTenderPayType.CurrentRow.Cells[4].Value = trnSalesEntity.Amount.ToString("#,##0.00");
                    ComputeAmount();
                }
                else if (payTypeCode == "EXCHANGE")
                {
                    TrnPOSTenderExchangeInformation trnPOSTenderExchangeInformation = new TrnPOSTenderExchangeInformation(this, dataGridViewTenderPayType);
                    trnPOSTenderExchangeInformation.ShowDialog();
                }
                else if (payTypeCode == "CREDITCARD")
                {
                    TrnPOSTenderCreditCardInformation trnPOSTenderCreditCardInformation = new TrnPOSTenderCreditCardInformation(this, dataGridViewTenderPayType, payAmount);
                    trnPOSTenderCreditCardInformation.ShowDialog();
                }
                else if (payTypeCode == "CHECK")
                {
                    TrnPOSTenderCheckInformation trnPOSTenderCheckInformation = new TrnPOSTenderCheckInformation(this, dataGridViewTenderPayType, payAmount);
                    trnPOSTenderCheckInformation.ShowDialog();
                }
                else if (payTypeCode == "GIFTCERTIFICATE")
                {
                    TrnPOSTenderGiftCertificateInformation trnPOSTenderGiftCertificateInformation = new TrnPOSTenderGiftCertificateInformation(this, dataGridViewTenderPayType, payAmount);
                    trnPOSTenderGiftCertificateInformation.ShowDialog();
                }
                else if (payTypeCode == "LOAD")
                {
                    TrnPOSTenderLoadInformation trnPOSTenderLoadInformation = new TrnPOSTenderLoadInformation(this, dataGridViewTenderPayType, payAmount);
                    trnPOSTenderLoadInformation.ShowDialog();
                }
                else
                {
                    TrnPOSTenderOtherInformationForm trnSalesDetailTenderMoreInfoForm = new TrnPOSTenderOtherInformationForm(this, dataGridViewTenderPayType);
                    trnSalesDetailTenderMoreInfoForm.ShowDialog();
                }
            }

            if (e.RowIndex > -1 && dataGridViewTenderPayType.CurrentCell.ColumnIndex == dataGridViewTenderPayType.Columns["ColumnTenderListNumpad"].Index)
            {
                SysKeyboard.SysNumpadForm sysKeyboardNumpadForm = new SysKeyboard.SysNumpadForm(this, dataGridViewTenderPayType);
                sysKeyboardNumpadForm.ShowDialog();
            }
        }

        private void TrnSalesDetailTenderForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Modules.SysSerialPortModule.CloseSerialPort();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F2:
                    {
                        if (buttonSales.Enabled == true)
                        {
                            buttonSales.PerformClick();
                            Close();
                        }

                        break;
                    }
                case Keys.F3:
                    {
                        if (buttonTender.Enabled == true)
                        {
                            buttonTender.PerformClick();
                            Close();
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