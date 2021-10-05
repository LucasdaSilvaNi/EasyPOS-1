using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.TrnStockCount
{
    public partial class TrnStockCountLineItemDetailForm : Form
    {
        public TrnStockCountDetailForm trnStockCountDetailForm;
        public Entities.TrnStockCountLineEntity trnStockCountLineEntity;

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public TrnStockCountLineItemDetailForm(TrnStockCountDetailForm stockCountDetailForm, Entities.TrnStockCountLineEntity stockCountLineEntity)
        {
            InitializeComponent();

            label1.Text = SetLabel(label1.Text);
            label2.Text = SetLabel(label2.Text);
            label3.Text = SetLabel(label3.Text);
            label4.Text = SetLabel(label4.Text);
            label9.Text = SetLabel(label9.Text);
            buttonSave.Text = SetLabel(buttonSave.Text);
            buttonClose.Text = SetLabel(buttonClose.Text);

            trnStockCountDetailForm = stockCountDetailForm;
            trnStockCountLineEntity = stockCountLineEntity;

            GetStockCountLineItemDetail();

            textBoxStockCountLineQuantity.Focus();
            textBoxStockCountLineQuantity.SelectAll();
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
        public void GetStockCountLineItemDetail()
        {
            textBoxStockCountLineItemDescription.Text = trnStockCountLineEntity.ItemDescription;
            textBoxStockCountLineQuantity.Text = trnStockCountLineEntity.Quantity.ToString("#,##0.00");
            textBoxStockCountLineUnit.Text = trnStockCountLineEntity.Unit;
            textBoxStockCountLineCost.Text = trnStockCountLineEntity.Cost.ToString("#,##0.00");
            textBoxStockCountLineAmount.Text = trnStockCountLineEntity.Amount.ToString("#,##0.00");
        }

        public void ComputeAmount()
        {
            try
            {
                if (String.IsNullOrEmpty(textBoxStockCountLineQuantity.Text) == false && String.IsNullOrEmpty(textBoxStockCountLineCost.Text) == false)
                {
                    Decimal quantity = Convert.ToDecimal(textBoxStockCountLineQuantity.Text);
                    Decimal cost = Convert.ToDecimal(textBoxStockCountLineCost.Text);
                    Decimal amount = cost * quantity;

                    textBoxStockCountLineAmount.Text = amount.ToString("#,##0.00");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveStockCountLine();
        }

        public void SaveStockCountLine()
        {
            var id = trnStockCountLineEntity.Id;
            var stockCountId = trnStockCountLineEntity.StockCountId;
            var itemId = trnStockCountLineEntity.ItemId;
            var itemDescription = trnStockCountLineEntity.ItemDescription;
            var unitId = trnStockCountLineEntity.UnitId;
            var unit = trnStockCountLineEntity.Unit;
            var quantity = Convert.ToDecimal(textBoxStockCountLineQuantity.Text);
            var cost = Convert.ToDecimal(textBoxStockCountLineCost.Text);
            var amount = Convert.ToDecimal(textBoxStockCountLineAmount.Text);

            Entities.TrnStockCountLineEntity newStockCountLineEntity = new Entities.TrnStockCountLineEntity()
            {
                Id = id,
                StockCountId = stockCountId,
                ItemId = itemId,
                ItemDescription = itemDescription,
                UnitId = unitId,
                Unit = unit,
                Quantity = quantity,
                Cost = cost,
                Amount = amount
            };

            Controllers.TrnStockCountLineController trnPOSStockCountLineController = new Controllers.TrnStockCountLineController();
            if (newStockCountLineEntity.Id == 0)
            {
                String[] addStockCountLine = trnPOSStockCountLineController.AddStockCountLine(newStockCountLineEntity);
                if (addStockCountLine[1].Equals("0") == false)
                {
                    trnStockCountDetailForm.UpdateStockCountLineListDataSource();
                    Close();
                }
                else
                {
                    MessageBox.Show(addStockCountLine[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                String[] updateStockCountLine = trnPOSStockCountLineController.UpdateStockCountLine(trnStockCountLineEntity.Id, newStockCountLineEntity);
                if (updateStockCountLine[1].Equals("0") == false)
                {
                    trnStockCountDetailForm.UpdateStockCountLineListDataSource();
                    Close();
                }
                else
                {
                    MessageBox.Show(updateStockCountLine[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxStockCountLineQuantity_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBoxStockCountLineCost_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBoxStockCountLineQuantity_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxStockCountLineQuantity.Text))
            {
                textBoxStockCountLineQuantity.Text = "0.00";
            }

            ComputeAmount();
        }

        private void textBoxStockCountLineCost_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBoxStockCountLineCost.Text))
            {
                textBoxStockCountLineCost.Text = "0.00";
            }

            ComputeAmount();
        }

        private void textBoxStockCountLineQuantity_Leave(object sender, EventArgs e)
        {
            textBoxStockCountLineQuantity.Text = Convert.ToDecimal(textBoxStockCountLineQuantity.Text).ToString("#,##0.00");
        }

        private void textBoxStockCountLineCost_Leave(object sender, EventArgs e)
        {
            textBoxStockCountLineCost.Text = Convert.ToDecimal(textBoxStockCountLineCost.Text).ToString("#,##0.00");
        }
    }
}
