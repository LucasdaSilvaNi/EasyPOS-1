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
    public partial class TrnPOSTouchActivitySplitMergeTableCodeForm : Form
    {
        public TrnPOSTouchActivitySplitMergeForm trnPOSTouchActivitySplitMergeForm;
        public DataGridView mstDataGridViewSalesItemSplitMerge;
        public Entities.TrnSalesEntity trnSalesEntity;
        public Boolean isTableGroupSelected = false;
        public Decimal originalQuantity = 0;

        public TrnPOSTouchActivitySplitMergeTableCodeForm(TrnPOSTouchActivitySplitMergeForm POSTouchActivitySplitMergeForm, DataGridView dataGridViewSalesItemSplitMerge, Entities.TrnSalesEntity SalesEntity, Decimal quantity)
        {
            InitializeComponent();

            trnPOSTouchActivitySplitMergeForm = POSTouchActivitySplitMergeForm;
            mstDataGridViewSalesItemSplitMerge = dataGridViewSalesItemSplitMerge;
            trnSalesEntity = SalesEntity;
            originalQuantity = quantity;

            textBoxQuantity.Text = quantity.ToString("#,##0.00");

            GetTableGroupList();
        }

        public void GetTableGroupList()
        {
            try
            {
                Controllers.TrnSalesController trnSalesController = new Controllers.TrnSalesController();
                if (trnSalesController.ListTableGroup().Any())
                {
                    comboBoxTableGroup.DataSource = trnSalesController.ListTableGroup();
                    comboBoxTableGroup.ValueMember = "Id";
                    comboBoxTableGroup.DisplayMember = "TableGroup";

                    getTableCodeList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void getTableCodeList()
        {
            try
            {
                Int32 tableGroupId = Convert.ToInt32(comboBoxTableGroup.SelectedValue);
                DateTime salesDate = Convert.ToDateTime(trnSalesEntity.SalesDate);

                Controllers.TrnSalesController trnSalesController = new Controllers.TrnSalesController();
                if (trnSalesController.ListTable(tableGroupId, salesDate).Any())
                {
                    comboBoxTableCode.DataSource = trnSalesController.ListTable(tableGroupId, salesDate).Where(d => d.Id == trnSalesEntity.TableId || d.HasSales == false).ToList();
                    comboBoxTableCode.ValueMember = "Id";
                    comboBoxTableCode.DisplayMember = "TableCode";
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

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(textBoxQuantity.Text) > originalQuantity)
            {
                MessageBox.Show("The provided quantity must not be higher than the original quantity.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SetTable();
            }
        }

        public void SetTable()
        {
            try
            {
                if (mstDataGridViewSalesItemSplitMerge.Rows.Contains(mstDataGridViewSalesItemSplitMerge.CurrentRow))
                {
                    Decimal quantity = Convert.ToDecimal(textBoxQuantity.Text);
                    Int32 tableId = Convert.ToInt32(comboBoxTableCode.SelectedValue);

                    var selectedItemTable = (Entities.MstTableEntity)comboBoxTableCode.SelectedItem;
                    String tableCode = selectedItemTable.TableCode;

                    mstDataGridViewSalesItemSplitMerge.CurrentRow.Cells[3].Value = quantity.ToString("#,##0.00");
                    mstDataGridViewSalesItemSplitMerge.CurrentRow.Cells[5].Value = tableId;
                    mstDataGridViewSalesItemSplitMerge.CurrentRow.Cells[6].Value = tableCode;
                }

                mstDataGridViewSalesItemSplitMerge.Refresh();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxQuantity_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBoxQuantity_Leave(object sender, EventArgs e)
        {
            textBoxQuantity.Text = Convert.ToDecimal(textBoxQuantity.Text).ToString("#,##0.00");
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

        private void comboBoxTableGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isTableGroupSelected == true)
            {
                getTableCodeList();
            }
            else
            {
                isTableGroupSelected = true;
            }
        }
    }
}
