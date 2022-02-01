using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.TrnPOS
{
    public partial class TrnPOSBarcodeDetailForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public TrnPOSBarcodeForm trnSalesListForm;
        public Entities.TrnSalesEntity trnSalesEntity;
        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public TrnPOSBarcodeDetailForm(SysSoftwareForm softwareForm, TrnPOSBarcodeForm salesListForm, Entities.TrnSalesEntity salesEntity)
        {
            InitializeComponent();

            Controllers.SysLanguageController sysLabel = new Controllers.SysLanguageController();
            if (sysLabel.ListLanguage("").Any())
            {
                sysLanguageEntities = sysLabel.ListLanguage("");
                var language = Modules.SysCurrentModule.GetCurrentSettings().Language;
                if (language != "English")
                {
                    buttonTender.Text = SetLabel(buttonTender.Text);
                    buttonPrint.Text = SetLabel(buttonPrint.Text);
                    buttonLock.Text = SetLabel(buttonLock.Text);
                    buttonUnlock.Text = SetLabel(buttonUnlock.Text);
                    buttonReturn.Text = SetLabel(buttonReturn.Text);
                    buttonDiscount.Text = SetLabel(buttonDiscount.Text);
                    buttonDiscount.Text = SetLabel(buttonDiscount.Text);
                    buttonTender.Text = SetLabel(buttonTender.Text);
                    buttonOverRide.Text = SetLabel(buttonOverRide.Text);
                    buttonClose.Text = SetLabel(buttonClose.Text);
                    buttonBarcode.Text = SetLabel(buttonBarcode.Text);
                    buttonSearchItem.Text = SetLabel(buttonSearchItem.Text);
                    buttonDownload.Text = SetLabel(buttonDownload.Text);
                    dataGridViewSalesLineList.Columns[5].HeaderText = SetLabel(dataGridViewSalesLineList.Columns[5].HeaderText);
                    dataGridViewSalesLineList.Columns[6].HeaderText = SetLabel(dataGridViewSalesLineList.Columns[6].HeaderText);
                    dataGridViewSalesLineList.Columns[7].HeaderText = SetLabel(dataGridViewSalesLineList.Columns[7].HeaderText);
                    dataGridViewSalesLineList.Columns[8].HeaderText = SetLabel(dataGridViewSalesLineList.Columns[8].HeaderText);
                    dataGridViewSalesLineList.Columns[9].HeaderText = SetLabel(dataGridViewSalesLineList.Columns[9].HeaderText);
                    dataGridViewSalesLineList.Columns[15].HeaderText = SetLabel(dataGridViewSalesLineList.Columns[15].HeaderText);
                    dataGridViewSalesLineList.Columns[11].HeaderText = SetLabel(dataGridViewSalesLineList.Columns[11].HeaderText);
                    dataGridViewSalesLineList.Columns[12].HeaderText = SetLabel(dataGridViewSalesLineList.Columns[12].HeaderText);
                    dataGridViewSalesLineList.Columns[13].HeaderText = SetLabel(dataGridViewSalesLineList.Columns[13].HeaderText);
                    dataGridViewSalesLineList.Columns[14].HeaderText = SetLabel(dataGridViewSalesLineList.Columns[14].HeaderText);
                    dataGridViewSalesLineList.Columns[17].HeaderText = SetLabel(dataGridViewSalesLineList.Columns[17].HeaderText);
                    dataGridViewSalesLineList.Columns[18].HeaderText = SetLabel(dataGridViewSalesLineList.Columns[18].HeaderText);
                    dataGridViewSalesLineList.Columns[19].HeaderText = SetLabel(dataGridViewSalesLineList.Columns[19].HeaderText);
                    label5.Text = SetLabel(label5.Text);
                    label6.Text = SetLabel(label6.Text);
                    label7.Text = SetLabel(label7.Text);
                    label3.Text = SetLabel(label3.Text);
                    label4.Text = SetLabel(label4.Text);
                    label2.Text = SetLabel(label2.Text);
                    label1.Text = SetLabel(label1.Text);
                }
            }

            sysSoftwareForm = softwareForm;
            trnSalesListForm = salesListForm;
            trnSalesEntity = salesEntity;

            Modules.SysSerialPortModule.OpenSerialPort();

            sysUserRights = new Modules.SysUserRightsModule("TrnSalesDetail");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (sysUserRights.GetUserRights().CanLock == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonLock.Enabled = false;
                    }
                    else
                    {
                        buttonLock.Enabled = true;
                    }
                }
                else
                {
                    buttonLock.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanUnlock == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonUnlock.Enabled = true;
                    }
                    else
                    {
                        buttonUnlock.Enabled = false;
                    }
                }
                else
                {
                    buttonUnlock.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanReturn == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonReturn.Enabled = false;
                    }
                    else
                    {
                        buttonReturn.Enabled = true;
                    }
                }
                else
                {
                    buttonReturn.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanDiscount == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonDiscount.Enabled = false;
                    }
                    else
                    {
                        buttonDiscount.Enabled = true;
                    }
                }
                else
                {
                    buttonDiscount.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanTender == true)
                {
                    buttonTender.Enabled = true;
                }
                else
                {
                    buttonTender.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanPrint == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonPrint.Enabled = true;
                    }
                    else
                    {
                        buttonPrint.Enabled = false;
                    }
                }
                else
                {
                    buttonPrint.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanAdd == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonBarcode.Enabled = false;
                        textBoxBarcode.Enabled = false;
                        buttonSearchItem.Enabled = false;
                        buttonDownload.Enabled = false;
                    }
                    else
                    {
                        buttonBarcode.Enabled = true;
                        textBoxBarcode.Enabled = true;
                        buttonSearchItem.Enabled = true;
                        buttonDownload.Enabled = true;
                    }
                }
                else
                {
                    buttonBarcode.Enabled = false;
                    textBoxBarcode.Enabled = false;
                    buttonSearchItem.Enabled = false;
                    buttonDownload.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanEdit == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        dataGridViewSalesLineList.Columns[0].Visible = false;
                    }
                    else
                    {
                        dataGridViewSalesLineList.Columns[0].Visible = true;
                    }
                }
                else
                {
                    dataGridViewSalesLineList.Columns[0].Visible = true;
                }

                if (sysUserRights.GetUserRights().CanDelete == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        dataGridViewSalesLineList.Columns[1].Visible = false;
                    }
                    else
                    {
                        dataGridViewSalesLineList.Columns[1].Visible = true;
                    }
                }
                else
                {
                    dataGridViewSalesLineList.Columns[1].Visible = false;
                }

                GetSalesDetail();
                GetSalesLineList();
            }
            var id = trnSalesEntity.Id;

            Controllers.TrnSalesController newSales = new Controllers.TrnSalesController();
            var detail = newSales.DetailSales(id);

            if (detail != null)
            {
                sysSoftwareForm.displayTimeStamp(detail.EntryUserUserName, detail.EntryDateTime + " " + detail.EntryTime, detail.UpdatedUserUserName, detail.UpdateDateTime + " " + detail.UpdateTime);
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
        private void buttonOverRide_Click(object sender, EventArgs e)
        {
            Account.SysLogin.SysLoginForm login = new Account.SysLogin.SysLoginForm(null, this, null, null, true);
            login.ShowDialog();
        }
        public void OverrideSales(Int32 overrideUserId)
        {
            sysUserRights.OverrideUserRights(overrideUserId, "TrnSalesDetail");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (sysUserRights.GetUserRights().CanLock == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonLock.Enabled = false;
                    }
                    else
                    {
                        buttonLock.Enabled = true;
                    }
                }
                else
                {
                    buttonLock.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanUnlock == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonUnlock.Enabled = true;
                    }
                    else
                    {
                        buttonUnlock.Enabled = false;
                    }
                }
                else
                {
                    buttonUnlock.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanReturn == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonReturn.Enabled = false;
                    }
                    else
                    {
                        buttonReturn.Enabled = true;
                    }
                }
                else
                {
                    buttonReturn.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanDiscount == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonDiscount.Enabled = false;
                    }
                    else
                    {
                        buttonDiscount.Enabled = true;
                    }
                }
                else
                {
                    buttonDiscount.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanTender == true)
                {
                    buttonTender.Enabled = true;
                }
                else
                {
                    buttonTender.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanPrint == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonPrint.Enabled = true;
                    }
                    else
                    {
                        buttonPrint.Enabled = false;
                    }
                }
                else
                {
                    buttonPrint.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanAdd == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonBarcode.Enabled = false;
                        textBoxBarcode.Enabled = false;
                        buttonSearchItem.Enabled = false;
                        buttonDownload.Enabled = false;
                    }
                    else
                    {
                        buttonBarcode.Enabled = true;
                        textBoxBarcode.Enabled = true;
                        buttonSearchItem.Enabled = true;
                        buttonDownload.Enabled = true;
                    }
                }
                else
                {
                    buttonBarcode.Enabled = false;
                    textBoxBarcode.Enabled = false;
                    buttonSearchItem.Enabled = false;
                    buttonDownload.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanEdit == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        dataGridViewSalesLineList.Columns[0].Visible = false;
                    }
                    else
                    {
                        dataGridViewSalesLineList.Columns[0].Visible = true;
                    }
                }
                else
                {
                    dataGridViewSalesLineList.Columns[0].Visible = true;
                }

                if (sysUserRights.GetUserRights().CanDelete == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        dataGridViewSalesLineList.Columns[1].Visible = false;
                    }
                    else
                    {
                        dataGridViewSalesLineList.Columns[1].Visible = true;
                    }
                }
                else
                {
                    dataGridViewSalesLineList.Columns[1].Visible = false;
                }

                GetSalesDetail();
                GetSalesLineList();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();

            trnSalesListForm.UpdateSalesListGridDataSource();
            sysSoftwareForm.RemoveTabPage();
        }

        public void GetSalesDetail()
        {
            textBoxTotalSalesAmount.Text = trnSalesEntity.Amount.ToString("#,##0.00");
            labelInvoiceNumber.Text = trnSalesEntity.SalesNumber;
            labelInvoiceDate.Text = trnSalesEntity.SalesDate;
            labelCustomerCode.Text = trnSalesEntity.CustomerCode;
            labelCustomer.Text = trnSalesEntity.Customer;
            labelRemarks.Text = trnSalesEntity.Remarks;

        }

        private void buttonTender_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(textBoxTotalSalesAmount.Text) > 0)
            {
                Entities.TrnSalesEntity newSalesEntity = new Entities.TrnSalesEntity
                {
                    Id = trnSalesEntity.Id,
                    Amount = Convert.ToDecimal(textBoxTotalSalesAmount.Text),
                    SalesNumber = trnSalesEntity.SalesNumber,
                    SalesDate = trnSalesEntity.SalesDate,
                    CustomerId = trnSalesEntity.CustomerId,
                    CustomerCode = trnSalesEntity.CustomerCode,
                    Customer = trnSalesEntity.Customer,
                    Remarks = trnSalesEntity.Remarks
                };
                if (Modules.SysCurrentModule.GetCurrentSettings().RestrictCashin == true)
                {
                    Int32 currentUser = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId);
                    Controllers.TrnDisbursementController trnDisbursementController = new Controllers.TrnDisbursementController();
                    var disbursementList = trnDisbursementController.ScanCashIn(currentUser, DateTime.Today, "DEBIT");

                    if (disbursementList != null)
                    {
                        TrnPOSTenderForm trnSalesDetailTenderForm = new TrnPOSTenderForm(sysSoftwareForm, trnSalesListForm, this, null, null, newSalesEntity);
                        trnSalesDetailTenderForm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Please make a cash-in transaction first!", "EasyPOS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    TrnPOSTenderForm trnSalesDetailTenderForm = new TrnPOSTenderForm(sysSoftwareForm, trnSalesListForm, this, null, null, newSalesEntity);
                    trnSalesDetailTenderForm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Cannot tender zero amount.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSearchItem_Click(object sender, EventArgs e)
        {
            TrnPOSSearchItemForm trnSalesDetailSearchItemForm = new TrnPOSSearchItemForm(this, null, trnSalesEntity);
            trnSalesDetailSearchItemForm.ShowDialog();
        }

        public void GetSalesLineList()
        {
            Decimal totalSalesAmount = 0;

            dataGridViewSalesLineList.Rows.Clear();
            dataGridViewSalesLineList.Refresh();

            Controllers.TrnSalesLineController trnPOSSalesLineController = new Controllers.TrnSalesLineController();

            var salesLineList = trnPOSSalesLineController.ListSalesLine(trnSalesEntity.Id);
            if (salesLineList.Any())
            {
                dataGridViewSalesLineList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
                dataGridViewSalesLineList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
                dataGridViewSalesLineList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

                dataGridViewSalesLineList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
                dataGridViewSalesLineList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
                dataGridViewSalesLineList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

                foreach (var objSalesLineList in salesLineList)
                {
                    totalSalesAmount += objSalesLineList.Amount;

                    dataGridViewSalesLineList.Rows.Add(
                        "Edit",
                        "Delete",
                        objSalesLineList.Id,
                        objSalesLineList.SalesId,
                        objSalesLineList.ItemId,
                        objSalesLineList.ItemDescription,
                        objSalesLineList.Quantity.ToString("#,##0.00"),
                        objSalesLineList.UnitId,
                        objSalesLineList.Unit,
                        objSalesLineList.Price.ToString("#,##0.00"),
                        objSalesLineList.DiscountId,
                        objSalesLineList.Discount,
                        objSalesLineList.DiscountRate.ToString("#,##0.00"),
                        objSalesLineList.DiscountAmount.ToString("#,##0.00"),
                        objSalesLineList.NetPrice.ToString("#,##0.00"),
                        objSalesLineList.Amount.ToString("#,##0.00"),
                        objSalesLineList.TaxId,
                        objSalesLineList.Tax,
                        objSalesLineList.TaxRate.ToString("#,##0.00"),
                        objSalesLineList.TaxAmount.ToString("#,##0.00"),
                        objSalesLineList.SalesAccountId,
                        objSalesLineList.AssetAccountId,
                        objSalesLineList.CostAccountId,
                        objSalesLineList.TaxAccountId,
                        objSalesLineList.SalesLineTimeStamp,
                        objSalesLineList.UserId,
                        objSalesLineList.Preparation,
                        objSalesLineList.Price1.ToString("#,##0.00"),
                        objSalesLineList.Price2.ToString("#,##0.00"),
                        objSalesLineList.Price2LessTax.ToString("#,##0.00"),
                        objSalesLineList.PriceSplitPercentage.ToString("#,##0.00"),
                        objSalesLineList.BodegaItemQty.ToString("#,##0.00"),
                        objSalesLineList.IsDelivery,
                        objSalesLineList.DeliveryStatus
                    );
                }
            }



            textBoxTotalSalesAmount.Text = totalSalesAmount.ToString("#,##0.00");
            trnSalesEntity.Amount = totalSalesAmount;

            String line1 = Modules.SysCurrentModule.GetCurrentSettings().CustomerDisplayFirstLineMessage;
            String line2 = "P " + textBoxTotalSalesAmount.Text;

            if (totalSalesAmount > 0)
            {
                line1 = "TOTAL:";
            }

            Modules.SysSerialPortModule.WriteSeralPortMessage(line1, line2);
            Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
            textBoxChange.Text = trnPOSSalesController.GetLastChange(Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().TerminalId)).ToString("#,##0.00");
        }

        private void dataGridViewSalesLineList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && dataGridViewSalesLineList.CurrentCell.ColumnIndex == dataGridViewSalesLineList.Columns["ColumnSalesLineEdit"].Index)
            {
                Int32 Id = Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[2].Value);
                Int32 SalesId = Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[3].Value);
                Int32 ItemId = Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[4].Value);
                String ItemDescription = dataGridViewSalesLineList.Rows[e.RowIndex].Cells[5].Value.ToString();
                Decimal Quantity = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[6].Value);
                Int32 UnitId = Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[7].Value);
                String Unit = dataGridViewSalesLineList.Rows[e.RowIndex].Cells[8].Value.ToString();
                Decimal Price = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[9].Value);
                Int32 DiscountId = Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[10].Value);
                String Discount = dataGridViewSalesLineList.Rows[e.RowIndex].Cells[11].Value.ToString(); ;
                Decimal DiscountRate = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[12].Value);
                Decimal DiscountAmount = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[13].Value);
                Decimal NetPrice = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[14].Value);
                Decimal Amount = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[15].Value);
                Int32 TaxId = Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[16].Value);
                String Tax = dataGridViewSalesLineList.Rows[e.RowIndex].Cells[17].Value.ToString();
                Decimal TaxRate = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[18].Value);
                Decimal TaxAmount = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[19].Value);
                Int32 SalesAccountId = Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[20].Value);
                Int32 AssetAccountId = Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[21].Value);
                Int32 CostAccountId = Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[22].Value);
                Int32 TaxAccountId = Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[23].Value);
                String SalesLineTimeStamp = dataGridViewSalesLineList.Rows[e.RowIndex].Cells[24].Value.ToString();
                Int32 UserId = Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[25].Value);
                String Preparation = dataGridViewSalesLineList.Rows[e.RowIndex].Cells[26].Value.ToString();
                Decimal Price1 = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[27].Value);
                Decimal Price2 = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[28].Value);
                Decimal Price2LessTax = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[29].Value);
                Decimal PriceSplitPercentage = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[30].Value);
                Decimal BodegaItemQty = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[31].Value);
                Boolean? IsDelivery = Convert.ToBoolean(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[32].Value);
                String DeliveryStatus = dataGridViewSalesLineList.Rows[e.RowIndex].Cells[33].Value.ToString();

                Entities.TrnSalesLineEntity trnSalesLineEntity = new Entities.TrnSalesLineEntity()
                {
                    Id = Id,
                    SalesId = SalesId,
                    ItemId = ItemId,
                    ItemDescription = ItemDescription,
                    UnitId = UnitId,
                    Unit = Unit,
                    Price = Price,
                    DiscountId = DiscountId,
                    Discount = Discount,
                    DiscountRate = DiscountRate,
                    DiscountAmount = DiscountAmount,
                    NetPrice = NetPrice,
                    Quantity = Quantity,
                    Amount = Amount,
                    TaxId = TaxId,
                    Tax = Tax,
                    TaxRate = TaxRate,
                    TaxAmount = TaxAmount,
                    SalesAccountId = SalesAccountId,
                    AssetAccountId = AssetAccountId,
                    CostAccountId = CostAccountId,
                    TaxAccountId = TaxAccountId,
                    SalesLineTimeStamp = SalesLineTimeStamp,
                    UserId = UserId,
                    Preparation = Preparation,
                    Price1 = Price1,
                    Price2 = Price2,
                    Price2LessTax = Price2LessTax,
                    PriceSplitPercentage = PriceSplitPercentage,
                    BodegaItemQty = BodegaItemQty,
                    IsDelivery = IsDelivery,
                    DeliveryStatus = DeliveryStatus
                };

                TrnPOSSalesItemDetailForm trnSalesDetailSalesItemDetailForm = new TrnPOSSalesItemDetailForm(this, null, trnSalesLineEntity, null);
                trnSalesDetailSalesItemDetailForm.ShowDialog();
            }

            if (dataGridViewSalesLineList.Rows.Count > 0)
            {
                if (e.RowIndex > -1 && dataGridViewSalesLineList.CurrentCell.ColumnIndex == dataGridViewSalesLineList.Columns["ColumnSalesLineDelete"].Index)
                {
                    DialogResult deleteDialogResult = MessageBox.Show("Delete Item?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (deleteDialogResult == DialogResult.Yes)
                    {
                        Controllers.TrnSalesLineController trnPOSSalesLineController = new Controllers.TrnSalesLineController();

                        String[] deleteSalesLine = trnPOSSalesLineController.DeleteSalesLine(Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[2].Value));
                        if (deleteSalesLine[1].Equals("0") == false)
                        {
                            GetSalesLineList();
                        }
                        else
                        {
                            MessageBox.Show(deleteSalesLine[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

        }

        private void textBoxBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Controllers.TrnSalesLineController trnPOSSalesLineController = new Controllers.TrnSalesLineController();

                if (Modules.SysCurrentModule.GetCurrentSettings().IsBarcodeQuantityAlwaysOne == true)
                {
                    trnPOSSalesLineController.BarcodeSalesLine(trnSalesEntity.Id, textBoxBarcode.Text);
                    GetSalesLineList();
                }
                else
                {
                    Entities.MstItemEntity detailItem = trnPOSSalesLineController.DetailItem(textBoxBarcode.Text);
                    if (detailItem != null)
                    {
                        Int32 ItemId = detailItem.Id;
                        Int32 SalesId = trnSalesEntity.Id;
                        String ItemDescription = detailItem.ItemDescription;
                        Int32 TaxId = detailItem.OutTaxId;
                        String Tax = detailItem.OutTax;
                        Decimal TaxRate = detailItem.OutTaxRate;
                        Int32 UnitId = detailItem.UnitId;
                        String Unit = detailItem.Unit;
                        Decimal Price = detailItem.Price;
                        Int32 DiscountId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().DefaultDiscountId);
                        Int32 UserId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId);

                        Entities.TrnSalesLineEntity trnSalesLineEntity = new Entities.TrnSalesLineEntity()
                        {
                            Id = 0,
                            SalesId = SalesId,
                            ItemId = ItemId,
                            ItemDescription = ItemDescription,
                            UnitId = UnitId,
                            Unit = Unit,
                            Price = Price,
                            DiscountId = DiscountId,
                            Discount = "",
                            DiscountRate = 0,
                            DiscountAmount = 0,
                            NetPrice = Price,
                            Quantity = 1,
                            Amount = Price,
                            TaxId = TaxId,
                            Tax = Tax,
                            TaxRate = TaxRate,
                            TaxAmount = 0,
                            SalesAccountId = 159,
                            AssetAccountId = 255,
                            CostAccountId = 238,
                            TaxAccountId = 87,
                            SalesLineTimeStamp = DateTime.Now.Date.ToShortDateString(),
                            UserId = UserId,
                            Preparation = "",
                            Price1 = 0,
                            Price2 = 0,
                            Price2LessTax = 0,
                            PriceSplitPercentage = 0
                        };

                        TrnPOSSalesItemDetailForm trnSalesDetailSalesItemDetailForm = new TrnPOSSalesItemDetailForm(this, null, trnSalesLineEntity, null);
                        trnSalesDetailSalesItemDetailForm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Item not found.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                textBoxBarcode.SelectAll();
            }
        }

        private void buttonBarcode_Click(object sender, EventArgs e)
        {
            textBoxBarcode.Focus();
            textBoxBarcode.SelectAll();
        }

        private void TrnSalesDetailForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Modules.SysSerialPortModule.CloseSerialPort();
        }

        private void buttonDiscount_Click(object sender, EventArgs e)
        {
            Decimal salesAmount = 0;
            List<Entities.TrnSalesLineEntity> listSalesLine = new List<Entities.TrnSalesLineEntity>();
            if (Convert.ToDecimal(textBoxTotalSalesAmount.Text) > 0)
            {
                foreach (DataGridViewRow row in dataGridViewSalesLineList.Rows)
                {
                    Decimal price = Convert.ToDecimal(row.Cells[dataGridViewSalesLineList.Columns["ColumnSalesLinePrice"].Index].Value);
                    Decimal quantity = Convert.ToDecimal(row.Cells[dataGridViewSalesLineList.Columns["ColumnSalesLineQuantity"].Index].Value);

                    salesAmount += price * quantity;

                    Int32 ItemId = Convert.ToInt32(row.Cells[4].Value);
                    String ItemDescription = row.Cells[5].Value.ToString();
                    Decimal Price = Convert.ToDecimal(row.Cells[9].Value);
                    Decimal Quantity = Convert.ToDecimal(row.Cells[6].Value);


                    listSalesLine.Add(new Entities.TrnSalesLineEntity()
                    {
                        Id = 0,
                        SalesId = 0,
                        ItemId = ItemId,
                        ItemDescription = ItemDescription,
                        UnitId = 0,
                        Unit = "",
                        Price = Price,
                        DiscountId = 0,
                        Discount = "",
                        DiscountRate = 0,
                        DiscountAmount = 0,
                        NetPrice = 0,
                        Quantity = Quantity,
                        Amount = 0,
                        TaxId = 0,
                        Tax = "",
                        TaxRate = 0,
                        TaxAmount = 0,
                        SalesAccountId = 159,
                        AssetAccountId = 255,
                        CostAccountId = 238,
                        TaxAccountId = 87,
                        SalesLineTimeStamp = DateTime.Now.Date.ToShortDateString(),
                        UserId = 0,
                        Preparation = "",
                        Price1 = 0,
                        Price2 = 0,
                        Price2LessTax = 0,
                        PriceSplitPercentage = 0
                    });
                }

                if (listSalesLine.Any())
                {
                    var groupedSalesLine = from d in listSalesLine.ToList()
                                           group d by new
                                           {
                                               d.ItemId,
                                               d.ItemDescription
                                           } into g
                                           select new Entities.TrnSalesLineEntity()
                                           {
                                               Id = 0,
                                               SalesId = 0,
                                               ItemId = g.Key.ItemId,
                                               ItemDescription = g.Key.ItemDescription,
                                               UnitId = 0,
                                               Unit = "",
                                               Price = 0,
                                               DiscountId = 0,
                                               Discount = "",
                                               DiscountRate = 0,
                                               DiscountAmount = 0,
                                               NetPrice = 0,
                                               Quantity = 1,
                                               Amount = g.Sum(i => i.Price * i.Quantity),
                                               TaxId = 0,
                                               Tax = "",
                                               TaxRate = 0,
                                               TaxAmount = 0,
                                               SalesAccountId = 159,
                                               AssetAccountId = 255,
                                               CostAccountId = 238,
                                               TaxAccountId = 87,
                                               SalesLineTimeStamp = DateTime.Now.Date.ToShortDateString(),
                                               UserId = 0,
                                               Preparation = "",
                                               Price1 = 0,
                                               Price2 = 0,
                                               Price2LessTax = 0,
                                               PriceSplitPercentage = 0
                                           };

                    TrnPOSDiscountForm trnSalesDetailDiscountForm = new TrnPOSDiscountForm(this, null, salesAmount, groupedSalesLine.ToList());
                    trnSalesDetailDiscountForm.ShowDialog();
                }
                else
                {
                    TrnPOSDiscountForm trnSalesDetailDiscountForm = new TrnPOSDiscountForm(this, null, salesAmount, new List<Entities.TrnSalesLineEntity>());
                    trnSalesDetailDiscountForm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Cannot discount with empty sales.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            TrnPOSDownloadItemsForm trnPOSDownloadItemsForm = new TrnPOSDownloadItemsForm(sysSoftwareForm, this, null, trnSalesEntity.Id);
            trnPOSDownloadItemsForm.ShowDialog();
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            TrnPOSReturn trnPOSReturn = new TrnPOSReturn(this, null);
            trnPOSReturn.ShowDialog();
        }

        public void LockComponents(Boolean isLocked)
        {
            buttonSearchItem.Enabled = !isLocked;
            buttonDownload.Enabled = !isLocked;
            buttonPrint.Enabled = isLocked;
            buttonLock.Enabled = !isLocked;
            buttonUnlock.Enabled = isLocked;
            buttonReturn.Enabled = !isLocked;
            buttonDiscount.Enabled = !isLocked;
            buttonBarcode.Enabled = !isLocked;
            textBoxBarcode.Enabled = !isLocked;

            dataGridViewSalesLineList.Columns[0].Visible = !isLocked;
            dataGridViewSalesLineList.Columns[1].Visible = !isLocked;

            trnSalesListForm.UpdateSalesListGridDataSource();
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
        private void buttonLock_Click(object sender, EventArgs e)
        {
            if (Modules.SysCurrentModule.GetCurrentSettings().LockAutoSales == false)
            {
                TrnPOSLockSalesForm trnPOSLockSalesForm = new TrnPOSLockSalesForm(this, null, trnSalesEntity);
                trnPOSLockSalesForm.ShowDialog();

                if (Modules.SysCurrentModule.GetCurrentSettings().DisableLockTender == true)
                {
                    buttonTender.Enabled = false;
                }
            }
            else
            {
                TrnPOSLockSalesForm trnPOSLockSalesForm = new TrnPOSLockSalesForm(this, null, trnSalesEntity);
                trnPOSLockSalesForm.ShowDialog();

                if (CheckFormOpened("SysSoftwareForm") == false)
                {
                    SysSoftwareForm sysSoftwareForm = new Software.SysSoftwareForm();
                    sysSoftwareForm.Show();
                }
                else
                {
                    trnSalesListForm.buttonAutoSales();
                }

                if (Modules.SysCurrentModule.GetCurrentSettings().DisableLockTender == true)
                {
                    buttonTender.Enabled = false;
                }
            }

        }

        private void buttonUnlock_Click(object sender, EventArgs e)
        {
            Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
            String[] lockSales = trnPOSSalesController.UnlockSales(trnSalesEntity.Id);
            if (lockSales[1].Equals("0") == false)
            {
                LockComponents(false);
            }
            else
            {
                MessageBox.Show(lockSales[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            // ============
            // Data Context
            // ============
            Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

            var salesLines = from d in db.TrnSalesLines where d.SalesId == trnSalesEntity.Id && d.BodegaItemQty > 0 select d;

            if (Modules.SysCurrentModule.GetCurrentSettings().ChoosePrinter == true)
            {
                DialogResult printDialogResult = printDialogSalesOrder.ShowDialog();
                if (printDialogResult == DialogResult.OK)
                {
                    if (trnSalesEntity.IsReturned == true)
                    {
                        new TrnPOSReturnReportForm(trnSalesEntity.Id);
                    }
                    else
                    {
                        //Debug.WriteLine(Modules.SysCurrentModule.GetCurrentSettings().SalesOrderPrinterType);

                        if (Modules.SysCurrentModule.GetCurrentSettings().SalesOrderPrinterType == "Label Printer")
                        {
                            new TrnPOSSalesOrderReportFormLabelPrinter(trnSalesEntity.Id, printDialogSalesOrder.PrinterSettings.PrinterName);
                        }

                        else
                        {
                            if (Modules.SysCurrentModule.GetCurrentSettings().BodegaTransaction == true)
                            {
                                if (salesLines.Any())
                                {
                                    new TrnPOSSalesOrderBodegaForm(trnSalesEntity.Id, printDialogSalesOrder.PrinterSettings.PrinterName);
                                }
                            }
                            new TrnPOSSalesOrderReportForm(trnSalesEntity.Id, printDialogSalesOrder.PrinterSettings.PrinterName);
                        }
                    }
                }
            }
            else
            {
                if (trnSalesEntity.IsReturned == true)
                {
                    new TrnPOSReturnReportForm(trnSalesEntity.Id);
                }
                else if (Modules.SysCurrentModule.GetCurrentSettings().SalesOrderPrinterType == "Letter Printer")
                {
                    new TrnPOSSalesInvoicePDFReportForm(trnSalesEntity.Id);
                }
                else
                {
                    if (Modules.SysCurrentModule.GetCurrentSettings().BodegaTransaction == true)
                    {
                        if (salesLines.Any())
                        {
                            new TrnPOSSalesOrderBodegaForm(trnSalesEntity.Id, "");
                        }
                    }
                    new TrnPOSSalesOrderReportForm(trnSalesEntity.Id, "");
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F2:
                    {
                        if (buttonPrint.Enabled == true)
                        {
                            buttonPrint.PerformClick();
                            Focus();
                        }

                        break;
                    }
                case Keys.F3:
                    {
                        if (buttonLock.Enabled == true)
                        {
                            buttonLock.PerformClick();
                            Focus();
                        }

                        break;
                    }
                case Keys.F4:
                    {
                        if (buttonUnlock.Enabled == true)
                        {
                            buttonUnlock.PerformClick();
                            Focus();
                        }

                        break;
                    }
                case Keys.F5:
                    {
                        if (buttonReturn.Enabled == true)
                        {
                            buttonReturn.PerformClick();
                            Focus();
                        }

                        break;
                    }
                case Keys.F6:
                    {
                        if (buttonDiscount.Enabled == true)
                        {
                            buttonDiscount.PerformClick();
                            Focus();
                        }

                        break;
                    }
                case Keys.F7:
                    {
                        if (buttonTender.Enabled == true)
                        {
                            buttonTender.PerformClick();
                            Focus();
                        }

                        break;
                    }
                case Keys.F8:
                    {
                        if (buttonBarcode.Enabled == true)
                        {
                            buttonBarcode.PerformClick();
                            Focus();
                        }

                        break;
                    }
                case Keys.F9:
                    {
                        if (buttonSearchItem.Enabled == true)
                        {
                            buttonSearchItem.PerformClick();
                            Focus();
                        }

                        break;
                    }
                case Keys.F10:
                    {
                        if (buttonDownload.Enabled == true)
                        {
                            buttonDownload.PerformClick();
                            Focus();
                        }

                        break;
                    }
                case Keys.F11:
                    {
                        if (buttonOverRide.Enabled == true)
                        {
                            buttonOverRide.PerformClick();
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
                default:
                    {
                        break;
                    }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dataGridViewSalesLineList_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGridViewSalesLineList.SelectedRows.Count == 0)
            {
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Int32 Id = Convert.ToInt32(dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[2].Value);
                    Int32 SalesId = Convert.ToInt32(dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[3].Value);
                    Int32 ItemId = Convert.ToInt32(dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[4].Value);
                    String ItemDescription = dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[5].Value.ToString();
                    Decimal Quantity = Convert.ToDecimal(dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[6].Value);
                    Int32 UnitId = Convert.ToInt32(dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[7].Value);
                    String Unit = dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[8].Value.ToString();
                    Decimal Price = Convert.ToDecimal(dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[9].Value);
                    Int32 DiscountId = Convert.ToInt32(dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[10].Value);
                    String Discount = dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[11].Value.ToString(); ;
                    Decimal DiscountRate = Convert.ToDecimal(dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[12].Value);
                    Decimal DiscountAmount = Convert.ToDecimal(dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[13].Value);
                    Decimal NetPrice = Convert.ToDecimal(dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[14].Value);
                    Decimal Amount = Convert.ToDecimal(dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[15].Value);
                    Int32 TaxId = Convert.ToInt32(dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[16].Value);
                    String Tax = dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[17].Value.ToString();
                    Decimal TaxRate = Convert.ToDecimal(dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[18].Value);
                    Decimal TaxAmount = Convert.ToDecimal(dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[19].Value);
                    Int32 SalesAccountId = Convert.ToInt32(dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[20].Value);
                    Int32 AssetAccountId = Convert.ToInt32(dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[21].Value);
                    Int32 CostAccountId = Convert.ToInt32(dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[22].Value);
                    Int32 TaxAccountId = Convert.ToInt32(dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[23].Value);
                    String SalesLineTimeStamp = dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[24].Value.ToString();
                    Int32 UserId = Convert.ToInt32(dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[25].Value);
                    String Preparation = dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[26].Value.ToString();
                    Decimal Price1 = Convert.ToDecimal(dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[27].Value);
                    Decimal Price2 = Convert.ToDecimal(dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[28].Value);
                    Decimal Price2LessTax = Convert.ToDecimal(dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[29].Value);
                    Decimal PriceSplitPercentage = Convert.ToDecimal(dataGridViewSalesLineList.Rows[dataGridViewSalesLineList.CurrentCell.RowIndex].Cells[30].Value);

                    Entities.TrnSalesLineEntity trnSalesLineEntity = new Entities.TrnSalesLineEntity()
                    {
                        Id = Id,
                        SalesId = SalesId,
                        ItemId = ItemId,
                        ItemDescription = ItemDescription,
                        UnitId = UnitId,
                        Unit = Unit,
                        Price = Price,
                        DiscountId = DiscountId,
                        Discount = Discount,
                        DiscountRate = DiscountRate,
                        DiscountAmount = DiscountAmount,
                        NetPrice = NetPrice,
                        Quantity = Quantity,
                        Amount = Amount,
                        TaxId = TaxId,
                        Tax = Tax,
                        TaxRate = TaxRate,
                        TaxAmount = TaxAmount,
                        SalesAccountId = SalesAccountId,
                        AssetAccountId = AssetAccountId,
                        CostAccountId = CostAccountId,
                        TaxAccountId = TaxAccountId,
                        SalesLineTimeStamp = SalesLineTimeStamp,
                        UserId = UserId,
                        Preparation = Preparation,
                        Price1 = Price1,
                        Price2 = Price2,
                        Price2LessTax = Price2LessTax,
                        PriceSplitPercentage = PriceSplitPercentage,
                    };

                    TrnPOSSalesItemDetailForm trnSalesDetailSalesItemDetailForm = new TrnPOSSalesItemDetailForm(this, null, trnSalesLineEntity, null);
                    trnSalesDetailSalesItemDetailForm.ShowDialog();
                }
            }
        }
    }
}
