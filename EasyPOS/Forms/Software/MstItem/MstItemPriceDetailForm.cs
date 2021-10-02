using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.MstItem
{
    public partial class MstItemPriceDetailForm : Form
    {
        MstItemDetailForm mstItemDetailForm;
        Entities.MstItemPriceEntity mstItemPriceEntity;
        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();


        public MstItemPriceDetailForm(MstItemDetailForm itemDetailForm, Entities.MstItemPriceEntity itemPriceEntity)
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

                }
            }

            mstItemDetailForm = itemDetailForm;
            mstItemPriceEntity = itemPriceEntity;

            LoadItemPrice();
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

        public void LoadItemPrice()
        {
            if (mstItemPriceEntity != null)
            {
                textBoxPriceDescription.Text = mstItemPriceEntity.PriceDescription;
                textBoxPrice.Text = mstItemPriceEntity.Price.ToString("#,##0.00");
                textBoxTriggerQuantity.Text = mstItemPriceEntity.TriggerQuantity.ToString("#,##0.00");
            }
            else
            {
                textBoxPriceDescription.Text = "";
                textBoxPrice.Text = "0.00";
                textBoxTriggerQuantity.Text = "0.00";
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Entities.MstItemPriceEntity newItemPrice = new Entities.MstItemPriceEntity()
            {
                Id = mstItemPriceEntity.Id,
                ItemId = mstItemPriceEntity.ItemId,
                PriceDescription = textBoxPriceDescription.Text,
                Price = Convert.ToDecimal(textBoxPrice.Text),
                TriggerQuantity = Convert.ToDecimal(textBoxTriggerQuantity.Text)
            };

            if (mstItemPriceEntity.Id == 0)
            {
                Controllers.MstItemPriceController mstItemPriceController = new Controllers.MstItemPriceController();
                String[] addItemPrice = mstItemPriceController.AddItemPrice(newItemPrice);

                if (addItemPrice[1].Equals("0") == true)
                {
                    MessageBox.Show(addItemPrice[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    mstItemDetailForm.UpdateItemPriceListDataSource();
                    Close();
                }
            }
            else
            {
                Controllers.MstItemPriceController mstItemPriceController = new Controllers.MstItemPriceController();
                String[] updateItemPrice = mstItemPriceController.UpdateItemPrice(newItemPrice);

                if (updateItemPrice[1].Equals("0") == true)
                {
                    MessageBox.Show(updateItemPrice[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    mstItemDetailForm.UpdateItemPriceListDataSource();
                    Close();
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxPrice_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBoxTriggerQuantity_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBoxPrice_Leave(object sender, EventArgs e)
        {
            textBoxPrice.Text = Convert.ToDecimal(textBoxPrice.Text).ToString("#,##0.00");
        }

        private void textBoxTriggerQuantity_Leave(object sender, EventArgs e)
        {
            textBoxTriggerQuantity.Text = Convert.ToDecimal(textBoxTriggerQuantity.Text).ToString("#,##0.00");
        }
    }
}
