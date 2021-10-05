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
    public partial class MstItemComponentDetailForm : Form
    {
        MstItemDetailForm mstItemDetailForm;
        Entities.MstItemComponentEntity mstItemComponentEntity;
        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public MstItemComponentDetailForm(MstItemDetailForm itemDetailForm, Entities.MstItemComponentEntity itemComponentEntity)
        {
            InitializeComponent();

            buttonClose.Text = SetLabel(buttonClose.Text);
            buttonSave.Text = SetLabel(buttonSave.Text);
            label1.Text = SetLabel(label1.Text);
            label2.Text = SetLabel(label2.Text);
            label3.Text = SetLabel(label3.Text);
            label4.Text = SetLabel(label4.Text);
            label6.Text = SetLabel(label6.Text);
            label7.Text = SetLabel(label7.Text);
            label8.Text = SetLabel(label8.Text);

            mstItemDetailForm = itemDetailForm;
            mstItemComponentEntity = itemComponentEntity;

            GetItemComponentList();
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

        public void GetItemComponentList()
        {
            Controllers.MstItemComponentController mstItemComponentController = new Controllers.MstItemComponentController();
            var items = mstItemComponentController.DropdownListItem(mstItemComponentEntity.ItemId);
            if (items.Any())
            {
                comboBoxItemComponent.DataSource = items;
                comboBoxItemComponent.ValueMember = "Id";
                comboBoxItemComponent.DisplayMember = "ItemDescription";
            }

            LoadItemComponent();
        }

        public void LoadItemComponent()
        {
            comboBoxItemComponent.SelectedValue = mstItemComponentEntity.ComponentItemId;
            textBoxUnit.Text = mstItemComponentEntity.Unit;
            textBoxQuantity.Text = mstItemComponentEntity.Quantity.ToString("#,##0.00");
            textBoxCost.Text = mstItemComponentEntity.Cost.ToString("#,##0.00");
            textBoxAmount.Text = mstItemComponentEntity.Amount.ToString("#,##0.00");
            textBoxOnHantQty.Text = mstItemComponentEntity.OnHandQuantity.ToString("#,##0.00");

            computeAmount();
        }

        private void comboBoxItemComponent_Click(object sender, EventArgs e)
        {
            GetItemComponentList();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (mstItemDetailForm.mstItemEntity.IsInventory == false)
                {
                    Entities.MstItemComponentEntity newItemComponent = new Entities.MstItemComponentEntity()
                    {
                        Id = mstItemComponentEntity.Id,
                        ItemId = mstItemComponentEntity.ItemId,
                        ComponentItemId = Convert.ToInt32(comboBoxItemComponent.SelectedValue),
                        Quantity = Convert.ToDecimal(textBoxQuantity.Text),
                        Cost = Convert.ToDecimal(textBoxCost.Text),
                        Amount = Convert.ToDecimal(textBoxAmount.Text),
                    };

                    if (mstItemComponentEntity.Id == 0)
                    {
                        Controllers.MstItemComponentController mstItemComponentController = new Controllers.MstItemComponentController();
                        String[] addItemComponent = mstItemComponentController.AddItemComponent(newItemComponent);

                        if (addItemComponent[1].Equals("0") == true)
                        {
                            MessageBox.Show(addItemComponent[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            mstItemDetailForm.UpdateItemComponentListDataSource();
                            Close();
                        }
                    }
                    else
                    {
                        Controllers.MstItemComponentController mstItemComponentController = new Controllers.MstItemComponentController();
                        String[] updateItemComponent = mstItemComponentController.UpdateItemComponent(newItemComponent);

                        if (updateItemComponent[1].Equals("0") == true)
                        {
                            MessageBox.Show(updateItemComponent[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            mstItemDetailForm.UpdateItemComponentListDataSource();
                            Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Cannot add component to Non-Inventory item.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxQuantity_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBoxQuantity_Leave(object sender, EventArgs e)
        {
            textBoxQuantity.Text = Convert.ToDecimal(textBoxQuantity.Text).ToString("#,##0.00");
            computeAmount();
        }

        private void comboBoxItemComponent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxItemComponent.SelectedItem == null)
            {
                return;
            }

            var selectedItemItemComponent = (Entities.MstItemEntity)comboBoxItemComponent.SelectedItem;
            if (selectedItemItemComponent != null)
            {
                textBoxUnit.Text = selectedItemItemComponent.Unit;
                textBoxCost.Text = selectedItemItemComponent.Cost.ToString("#,##0.00");
                textBoxOnHantQty.Text = selectedItemItemComponent.OnhandQuantity.ToString("#,##0.00");

                if (String.IsNullOrEmpty(textBoxQuantity.Text) == false)
                {
                    computeAmount();
                }
            }
        }

        public void computeAmount()
        {
            Decimal quantity = Convert.ToDecimal(textBoxQuantity.Text);
            Decimal cost = Convert.ToDecimal(textBoxCost.Text);
            Decimal amount = quantity * cost;

            textBoxAmount.Text = amount.ToString("#,##0.00");
        }
    }
}
