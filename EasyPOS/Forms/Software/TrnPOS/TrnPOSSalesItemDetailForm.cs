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
    public partial class TrnPOSSalesItemDetailForm : Form
    {
        public TrnPOSBarcodeDetailForm trnSalesDetailForm;
        public TrnPOSTouchDetailForm trnPOSTouchDetailForm;
        public Entities.TrnSalesLineEntity trnSalesLineEntity;
        public Entities.TrnSalesEntity trnSalesEntity;
        public TrnPOSSearchItemForm trnPOSSearchItemForm;
        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public TrnPOSSalesItemDetailForm(TrnPOSBarcodeDetailForm salesDetailForm, TrnPOSTouchDetailForm POSTouchDetailForm, Entities.TrnSalesLineEntity salesLineEntity, TrnPOSSearchItemForm posSearchItemForm)
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
                    buttonSave.Text = SetLabel(buttonSave.Text);
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
                    label11.Text = SetLabel(label11.Text);
                    label12.Text = SetLabel(label12.Text);
                    label13.Text = SetLabel(label13.Text);
                    
                }
            }

            trnSalesDetailForm = salesDetailForm;
            trnPOSTouchDetailForm = POSTouchDetailForm;
            trnPOSSearchItemForm = posSearchItemForm;


            trnSalesLineEntity = salesLineEntity;

            if (Modules.SysCurrentModule.GetCurrentSettings().EnableEditPrice == false)
            {
                textBoxSalesLinePrice.ReadOnly = true;
            }
            else
            {
                textBoxSalesLinePrice.ReadOnly = false;
            }

            GetDiscountList();

            textBoxSalesLineQuantity.Focus();
            textBoxSalesLineQuantity.SelectAll();
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

        public void GetDiscountList()
        {
            Controllers.TrnSalesLineController trnPOSSalesLineController = new Controllers.TrnSalesLineController();
            if (trnPOSSalesLineController.DropdownListDiscount().Any())
            {
                Int32? discountId = null;

                if (trnSalesDetailForm != null)
                {
                    discountId = trnSalesDetailForm.trnSalesEntity.DiscountId;
                }

                if (trnPOSTouchDetailForm != null)
                {
                    discountId = trnPOSTouchDetailForm.trnSalesEntity.DiscountId;
                }

                if (discountId != null)
                {
                    if (trnPOSSalesLineController.IsVATExempt(Convert.ToInt32(discountId)) == true)
                    {
                        var discounts = from d in trnPOSSalesLineController.DropdownListDiscount()
                                        select d;

                        comboBoxSalesLineDiscount.DataSource = discounts.ToList();
                        comboBoxSalesLineDiscount.ValueMember = "Id";
                        comboBoxSalesLineDiscount.DisplayMember = "Discount";

                    }
                    else
                    {
                        var discounts = from d in trnPOSSalesLineController.DropdownListDiscount()
                                        where d.Id != 7
                                        && d.Id != 16
                                        select d;

                        comboBoxSalesLineDiscount.DataSource = discounts.ToList();
                        comboBoxSalesLineDiscount.ValueMember = "Id";
                        comboBoxSalesLineDiscount.DisplayMember = "Discount";
                    }
                }
                else
                {
                    var discounts = from d in trnPOSSalesLineController.DropdownListDiscount()
                                    where d.Id != 7
                                    && d.Id != 16
                                    select d;

                    comboBoxSalesLineDiscount.DataSource = discounts.ToList();
                    comboBoxSalesLineDiscount.ValueMember = "Id";
                    comboBoxSalesLineDiscount.DisplayMember = "Discount";
                }

                GetSalesLineItemDetail();
            }
        }

        private void GetSalesLineItemDetail()
        {
            textBoxItemDescription.Text = trnSalesLineEntity.ItemDescription;
            textBoxSalesLineQuantity.Text = trnSalesLineEntity.Quantity.ToString("#,##0.00");
            textBoxSalesLineUnit.Text = trnSalesLineEntity.Unit;
            textBoxSalesLinePrice.Text = trnSalesLineEntity.Price.ToString("#,##0.00");
            comboBoxSalesLineDiscount.SelectedValue = trnSalesLineEntity.DiscountId;
            textBoxSalesLineDiscountRate.Text = trnSalesLineEntity.DiscountRate.ToString("#,##0.00");
            textBoxSalesLineDiscountAmount.Text = trnSalesLineEntity.DiscountAmount.ToString("#,##0.00");
            textBoxSalesLineNetPrice.Text = trnSalesLineEntity.NetPrice.ToString("#,##0.00");
            textBoxSalesLineAmount.Text = trnSalesLineEntity.Amount.ToString("#,##0.00");
            textBoxSalesLineVAT.Text = trnSalesLineEntity.Tax;
            textBoxSalesLineVATRate.Text = trnSalesLineEntity.TaxRate.ToString("#,##0.00");
            textBoxSalesLineVATAmount.Text = trnSalesLineEntity.TaxAmount.ToString("#,##0.00");
            textBoxSalesLineRemarks.Text = trnSalesLineEntity.Preparation;

            Int32? discountId = null;

            if (trnSalesDetailForm != null)
            {
                discountId = trnSalesDetailForm.trnSalesEntity.DiscountId;
            }

            if (trnPOSTouchDetailForm != null)
            {
                discountId = trnPOSTouchDetailForm.trnSalesEntity.DiscountId;
            }

            if (discountId != null)
            {
                Controllers.TrnSalesLineController trnPOSSalesLineController = new Controllers.TrnSalesLineController();
                if (trnPOSSalesLineController.IsVATExempt(Convert.ToInt32(discountId)) == true)
                {
                    comboBoxSalesLineDiscount.Enabled = false;
                }
                else
                {
                    comboBoxSalesLineDiscount.Enabled = true;
                }
            }
        }

        public void SaveSalesLine()
        {
            Entities.TrnSalesLineEntity newSalesLineEntity = new Entities.TrnSalesLineEntity()
            {
                Id = trnSalesLineEntity.Id,
                SalesId = trnSalesLineEntity.SalesId,
                ItemId = trnSalesLineEntity.ItemId,
                IsPrinted = Convert.ToBoolean(trnSalesLineEntity.IsPrinted),
                ItemDescription = trnSalesLineEntity.ItemDescription,
                UnitId = trnSalesLineEntity.UnitId,
                Unit = trnSalesLineEntity.Unit,
                Price = Convert.ToDecimal(textBoxSalesLinePrice.Text),
                DiscountId = Convert.ToInt32(comboBoxSalesLineDiscount.SelectedValue),
                Discount = trnSalesLineEntity.Discount,
                DiscountRate = Convert.ToDecimal(textBoxSalesLineDiscountRate.Text),
                DiscountAmount = Convert.ToDecimal(textBoxSalesLineDiscountAmount.Text),
                NetPrice = Convert.ToDecimal(textBoxSalesLineNetPrice.Text),
                Quantity = Convert.ToDecimal(textBoxSalesLineQuantity.Text),
                Amount = Convert.ToDecimal(textBoxSalesLineAmount.Text),
                TaxId = trnSalesLineEntity.TaxId,
                Tax = trnSalesLineEntity.Tax,
                TaxRate = trnSalesLineEntity.TaxRate,
                TaxAmount = Convert.ToDecimal(textBoxSalesLineVATAmount.Text),
                SalesAccountId = trnSalesLineEntity.SalesAccountId,
                AssetAccountId = trnSalesLineEntity.AssetAccountId,
                CostAccountId = trnSalesLineEntity.CostAccountId,
                TaxAccountId = trnSalesLineEntity.TaxAccountId,
                SalesLineTimeStamp = trnSalesLineEntity.SalesLineTimeStamp,
                UserId = trnSalesLineEntity.UserId,
                Preparation = textBoxSalesLineRemarks.Text,
                Price1 = 0,
                Price2 = 0,
                Price2LessTax = 0,
                PriceSplitPercentage = 0,
            };

            Controllers.TrnSalesLineController trnPOSSalesLineController = new Controllers.TrnSalesLineController();
            if (newSalesLineEntity.Id == 0)
            {
                String[] addSales = trnPOSSalesLineController.AddSalesLine(newSalesLineEntity);
                if (addSales[1].Equals("0") == false)
                {
                    if (trnSalesDetailForm != null)
                    {
                        trnSalesDetailForm.GetSalesLineList();
                    }

                    if (trnPOSTouchDetailForm != null)
                    {
                        trnPOSTouchDetailForm.GetSalesLineList();
                        trnPOSTouchDetailForm.UpdatePOSTouchSalesListDataSource();
                    }
                    if(trnPOSSearchItemForm != null)
                    {
                        trnPOSSearchItemForm.resetCursor();
                    }
                    Close();
                }
                else
                {
                    MessageBox.Show(addSales[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                String[] addSales = trnPOSSalesLineController.UpdateSalesLine(trnSalesLineEntity.Id, newSalesLineEntity);
                if (addSales[1].Equals("0") == false)
                {
                    if (trnSalesDetailForm != null)
                    {
                        trnSalesDetailForm.GetSalesLineList();
                    }

                    if (trnPOSTouchDetailForm != null)
                    {
                        trnPOSTouchDetailForm.GetSalesLineList();
                    }

                    Close();
                }
                else
                {
                    MessageBox.Show(addSales[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //private bool CheckFormOpened(string name)
        //{
        //    FormCollection fc = Application.OpenForms;

        //    foreach (Form frm in fc)
        //    {
        //        if (frm.Name == name)
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}
        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveSalesLine();
            //if (CheckFormOpened("TrnPOSSearchItemForm") == true)
            //{
            //    TrnPOSSearchItemForm trnSalesDetailSearchItemForm = new TrnPOSSearchItemForm(null, null, trnSalesEntity);
            //    trnSalesDetailSearchItemForm.Close();
            //}

            //TrnPOSSearchItemForm trnSalesDetailSearchItemForms = new TrnPOSSearchItemForm(null, null, trnSalesEntity);
            //trnSalesDetailSearchItemForms.ShowDialog();
        }

        private void comboBoxSalesLineDiscount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSalesLineDiscount.SelectedItem == null)
            {
                return;
            }

            var selectedItemDiscount = (Entities.MstDiscountEntity)comboBoxSalesLineDiscount.SelectedItem;
            if (selectedItemDiscount != null)
            {
                if (selectedItemDiscount.Id == 3)
                {
                    textBoxSalesLineDiscountRate.ReadOnly = false;
                    textBoxSalesLineDiscountAmount.ReadOnly = false;

                    textBoxSalesLineDiscountRate.Text = trnSalesLineEntity.DiscountRate.ToString();
                    textBoxSalesLineDiscountAmount.Text = trnSalesLineEntity.DiscountAmount.ToString();
                }
                else
                {
                    textBoxSalesLineDiscountRate.ReadOnly = true;
                    textBoxSalesLineDiscountAmount.ReadOnly = true;
                    if(selectedItemDiscount.DiscountRate == 0)
                    {
                        textBoxSalesLineDiscountAmount.Text = selectedItemDiscount.DiscountAmount.ToString("#,##0.00");
                    }
                    if (selectedItemDiscount.DiscountAmount == 0)
                    {
                        textBoxSalesLineDiscountRate.Text = selectedItemDiscount.DiscountRate.ToString("#,##0.00");
                    }
                }
                if (selectedItemDiscount.DiscountRate == 0)
                {
                    textBoxSalesLineDiscountAmount.Text = selectedItemDiscount.DiscountAmount.ToString("#,##0.00");
                    ComputeDiscountRate();
                }
                
                ComputeAmount();

            }
        }

        public void ComputeAmount()
        {
            try
            {
                if (String.IsNullOrEmpty(textBoxSalesLinePrice.Text) == false)
                {
                    Decimal quantity = Convert.ToDecimal(textBoxSalesLineQuantity.Text);
                    Decimal price = Convert.ToDecimal(textBoxSalesLinePrice.Text);
                    Decimal discountRate = Convert.ToDecimal(textBoxSalesLineDiscountRate.Text);
                    Decimal taxRate = trnSalesLineEntity.TaxRate;

                    Decimal discountAmount = Convert.ToDecimal(textBoxSalesLineDiscountAmount.Text);
                    if (discountRate > 0)
                    {
                        discountAmount = price * (discountRate / 100);
                    }

                    Decimal netPrice = price - discountAmount;
                    Decimal amount = netPrice * quantity;

                    Decimal taxAmount = 0;
                    if (taxRate > 0)
                    {
                        taxAmount = amount / (1 + (taxRate / 100)) * (taxRate / 100);
                    }

                    textBoxSalesLineDiscountAmount.Text = discountAmount.ToString("#,##0.00");
                    textBoxSalesLineNetPrice.Text = netPrice.ToString("#,##0.00");
                    textBoxSalesLineAmount.Text = amount.ToString("#,##0.00");
                    textBoxSalesLineVATAmount.Text = taxAmount.ToString("#,##0.00");
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxSalesLineQuantity_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxSalesLineQuantity.Text))
            {
                textBoxSalesLineQuantity.Text = "0.00";
            }

            ComputeAmount();
        }

        private void textBoxSalesLineQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBoxSalesLineQuantity_Leave(object sender, EventArgs e)
        {
            textBoxSalesLineQuantity.Text = Convert.ToDecimal(textBoxSalesLineQuantity.Text).ToString("#,##0.00");
        }

        private void textBoxSalesLineQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    SaveSalesLine();
            //}
        }

        private void textBoxSalesLinePrice_Click(object sender, EventArgs e)
        {
            TrnPOSItemPriceForm trnSalesDetailSalesItemDetailItemPriceForm = new TrnPOSItemPriceForm(this, trnSalesLineEntity);
            trnSalesDetailSalesItemDetailItemPriceForm.ShowDialog();
        }

        public void UpdatePrice(Decimal price)
        {
            textBoxSalesLinePrice.Text = price.ToString("#,##0.00");
            ComputeAmount();
        }

        private void textBoxSalesLineDiscountRate_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxSalesLineDiscountRate.Text))
            {
                textBoxSalesLineDiscountRate.Text = "0.00";
            }
            else
            {
                ComputeAmount();
                textBoxSalesLineDiscountRate.Text = Convert.ToDecimal(textBoxSalesLineDiscountRate.Text).ToString();
            }
        }

        private void textBoxSalesLineDiscountAmount_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxSalesLineDiscountAmount.Text))
            {
                textBoxSalesLineDiscountAmount.Text = "0.00";
            }
            else
            {
                ComputeDiscountRate();
                textBoxSalesLineDiscountAmount.Text = Convert.ToDecimal(textBoxSalesLineDiscountAmount.Text).ToString();
            }
        }
        public void ComputeDiscountRate()
        {
            Decimal discountAmount = Convert.ToDecimal(textBoxSalesLineDiscountAmount.Text);
            if (discountAmount > 0)
            {
                Decimal quantity = Convert.ToDecimal(textBoxSalesLineQuantity.Text);
                Decimal price = Convert.ToDecimal(textBoxSalesLinePrice.Text);
                Decimal amount = quantity * price;

                Decimal discountRate = (discountAmount / amount) * 100;
                textBoxSalesLineDiscountRate.Text = discountRate.ToString("#,##0.00");
                ComputeAmount();
            }
            else
            {
                textBoxSalesLineDiscountRate.Text = "0";
                ComputeAmount();
            }
        }

        private void textBoxSalesLineDiscountRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void textBoxSalesLineDiscountAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    {
                        if (buttonSave.Enabled == true)
                        {
                            Controllers.MstItemPriceController priceCotroller = new Controllers.MstItemPriceController();
                            if (Modules.SysCurrentModule.GetCurrentSettings().IsTriggeredQuantity == true)
                            {
                                if (priceCotroller.TriggeredItemPrice(trnSalesLineEntity.ItemId, Convert.ToDecimal(textBoxSalesLineQuantity.Text)).Any())
                                {
                                    textBoxSalesLinePrice.Text = priceCotroller.TriggeredItemPrice(trnSalesLineEntity.ItemId, Convert.ToDecimal(textBoxSalesLineQuantity.Text)).FirstOrDefault().Price.ToString("#,##0.00");
                                }
                            }
                            ComputeAmount();
                            buttonSave.PerformClick();
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
