using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.TrnStockIn
{
    public partial class TrnStockInLineItemDetailForm : Form
    {
        public TrnStockInDetailForm trnStockInDetailForm;
        public Entities.TrnStockInLineEntity trnStockInLineEntity;

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public TrnStockInLineItemDetailForm(TrnStockInDetailForm stockInDetailForm, Entities.TrnStockInLineEntity stockInLineEntity)
        {
            InitializeComponent();

            label1.Text = SetLabel(label1.Text);
            label2.Text = SetLabel(label2.Text);
            label3.Text = SetLabel(label3.Text);
            label4.Text = SetLabel(label4.Text);
            label6.Text = SetLabel(label6.Text);
            label7.Text = SetLabel(label7.Text);
            label8.Text = SetLabel(label8.Text);
            label9.Text = SetLabel(label9.Text);
            buttonSave.Text = SetLabel(buttonSave.Text);
            buttonClose.Text = SetLabel(buttonClose.Text);

            trnStockInDetailForm = stockInDetailForm;
            trnStockInLineEntity = stockInLineEntity;

            GetStockInLineItemDetail();

            textBoxStockInLineQuantity.Focus();
            textBoxStockInLineQuantity.SelectAll();
            if (Modules.SysCurrentModule.GetCurrentSettings().HideStockInPriceAndCost == true)
            {
                textBoxStockInLineCost.Enabled = false;
                textBoxStockInLinePrice.Enabled = false;
            }
            else
            {
                textBoxStockInLineCost.Enabled = true;
                textBoxStockInLinePrice.Enabled = true;
            }
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

        public void GetStockInLineItemDetail()
        {
            textBoxStockInLineItemDescription.Text = trnStockInLineEntity.ItemDescription;
            textBoxStockInLineQuantity.Text = trnStockInLineEntity.Quantity.ToString("#,##0.00");
            textBoxStockInLineUnit.Text = trnStockInLineEntity.Unit;
            textBoxStockInLineCost.Text = trnStockInLineEntity.Cost.ToString("#,##0.00");
            textBoxStockInLineAmount.Text = trnStockInLineEntity.Amount.ToString("#,##0.00");

            //if (String.IsNullOrEmpty(trnStockInLineEntity.ExpiryDate) == true)
            //{
            //    checkBoxIncludeExpiry.Checked = false;
            //    dateTimePickerStockInLineExpiryDate.Enabled = false;

            //    dateTimePickerStockInLineExpiryDate.Value = DateTime.Today;
            //}
            //else
            //{
            //    checkBoxIncludeExpiry.Checked = true;
            //    dateTimePickerStockInLineExpiryDate.Enabled = true;

            //    dateTimePickerStockInLineExpiryDate.Value = Convert.ToDateTime(trnStockInLineEntity.ExpiryDate);
            //}

            textBoxStockInLineLotNumber.Text = trnStockInLineEntity.LotNumber;
            textBoxStockInLinePrice.Text = trnStockInLineEntity.Price != null ? Convert.ToDecimal(trnStockInLineEntity.Price).ToString("#,##0.00") : "0.00";
        }

        public void ComputeAmount()
        {
            try
            {
                if (String.IsNullOrEmpty(textBoxStockInLineQuantity.Text) == false && String.IsNullOrEmpty(textBoxStockInLineCost.Text) == false)
                {
                    Decimal quantity = Convert.ToDecimal(textBoxStockInLineQuantity.Text);
                    Decimal cost = Convert.ToDecimal(textBoxStockInLineCost.Text);
                    Decimal amount = cost * quantity;

                    textBoxStockInLineAmount.Text = amount.ToString("#,##0.00");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveStockInLine();
        }

        public void SaveStockInLine()
        {
            var id = trnStockInLineEntity.Id;
            var stockInId = trnStockInLineEntity.StockInId;
            var itemId = trnStockInLineEntity.ItemId;
            var itemDescription = trnStockInLineEntity.ItemDescription;
            var unitId = trnStockInLineEntity.UnitId;
            var unit = trnStockInLineEntity.Unit;
            var quantity = Convert.ToDecimal(textBoxStockInLineQuantity.Text);
            var cost = Convert.ToDecimal(textBoxStockInLineCost.Text);
            var amount = Convert.ToDecimal(textBoxStockInLineAmount.Text);
            //var expiryDate = checkBoxIncludeExpiry.Checked == true ? dateTimePickerStockInLineExpiryDate.Value.ToShortDateString() : String.Empty;
            var expiryDate = textBoxExpiryDate.Text;
            var lotNumber = textBoxStockInLineLotNumber.Text;
            var assetAccountId = trnStockInLineEntity.AssetAccountId;
            var assetAccount = trnStockInLineEntity.AssetAccount;
            var price = Convert.ToDecimal(textBoxStockInLinePrice.Text);

            Entities.TrnStockInLineEntity newStockInLineEntity = new Entities.TrnStockInLineEntity()
            {
                Id = id,
                StockInId = stockInId,
                ItemId = itemId,
                ItemDescription = itemDescription,
                UnitId = unitId,
                Unit = unit,
                Quantity = quantity,
                Cost = cost,
                Amount = amount,
                ExpiryDate = expiryDate,
                LotNumber = lotNumber,
                AssetAccountId = assetAccountId,
                AssetAccount = assetAccount,
                Price = price
            };

            Controllers.TrnStockInLineController trnPOSStockInLineController = new Controllers.TrnStockInLineController();
            if (newStockInLineEntity.Id == 0)
            {
                String[] addStockInLine = trnPOSStockInLineController.AddStockInLine(newStockInLineEntity);
                if (addStockInLine[1].Equals("0") == false)
                {
                    trnStockInDetailForm.UpdateStockInLineListDataSource();
                    Close();
                }
                else
                {
                    MessageBox.Show(addStockInLine[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                String[] updateStockInLine = trnPOSStockInLineController.UpdateStockInLine(trnStockInLineEntity.Id, newStockInLineEntity);
                if (updateStockInLine[1].Equals("0") == false)
                {
                    trnStockInDetailForm.UpdateStockInLineListDataSource();
                    Close();
                }
                else
                {
                    MessageBox.Show(updateStockInLine[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxStockInLineQuantity_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBoxStockInLineCost_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBoxStockInLinePrice_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBoxStockInLineQuantity_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxStockInLineQuantity.Text))
            {
                textBoxStockInLineQuantity.Text = "0.00";
            }

            ComputeAmount();
        }

        private void textBoxStockInLineCost_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxStockInLineCost.Text))
            {
                textBoxStockInLineCost.Text = "0.00";
            }

            ComputeAmount();
        }

        private void textBoxStockInLinePrice_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxStockInLinePrice.Text))
            {
                textBoxStockInLinePrice.Text = "0.00";
            }
        }

        private void textBoxStockInLineQuantity_Leave(object sender, EventArgs e)
        {
            textBoxStockInLineQuantity.Text = Convert.ToDecimal(textBoxStockInLineQuantity.Text).ToString("#,##0.00");
        }

        private void textBoxStockInLineCost_Leave(object sender, EventArgs e)
        {
            textBoxStockInLineCost.Text = Convert.ToDecimal(textBoxStockInLineCost.Text).ToString("#,##0.00");
        }

        private void textBoxStockInLinePrice_Leave(object sender, EventArgs e)
        {
            textBoxStockInLinePrice.Text = Convert.ToDecimal(textBoxStockInLinePrice.Text).ToString("#,##0.00");
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    {
                        if (buttonSave.Enabled == true)
                        {
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


        //private void checkBoxIncludeExpiry_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (checkBoxIncludeExpiry.Checked == true)
        //    {
        //        dateTimePickerStockInLineExpiryDate.Enabled = true;
        //    }
        //    else
        //    {
        //        dateTimePickerStockInLineExpiryDate.Enabled = false;
        //    }
        //}
    }
}
