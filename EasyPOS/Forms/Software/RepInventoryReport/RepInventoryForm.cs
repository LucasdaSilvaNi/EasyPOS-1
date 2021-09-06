using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.RepInventoryReport
{
    public partial class RepInventoryForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public RepInventoryForm(SysSoftwareForm softwareForm)
        {
            InitializeComponent();
            sysSoftwareForm = softwareForm;


            checkBoxFilter.Visible = false;
            labelFilter.Visible = false;
            GetItemList();
            HideComboBox();

        }

        public void GetItemList()
        {
            Controllers.RepInventoryReportController repInventoryReportController = new Controllers.RepInventoryReportController();
            if (repInventoryReportController.DropdownListItem().Any())
            {
                List<Entities.MstItemEntity> newItemList = new List<Entities.MstItemEntity>();
                newItemList.Add(new Entities.MstItemEntity
                {
                    Id = 0,
                    ItemDescription = "ALL"
                });

                foreach (var obj in repInventoryReportController.DropdownListItem())
                {
                    newItemList.Add(new Entities.MstItemEntity
                    {
                        Id = obj.Id,
                        ItemDescription = obj.ItemDescription
                    });
                };

                comboBoxItem.DataSource = newItemList;
                comboBoxItem.ValueMember = "Id";
                comboBoxItem.DisplayMember = "ItemDescription";
            }
            if (repInventoryReportController.DropdownListItemCategory().Any())
            {
                List<Entities.MstItemEntity> newItemList = new List<Entities.MstItemEntity>();
                newItemList.Add(new Entities.MstItemEntity
                {
                    Id = 0,
                    Category = "ALL"
                });

                foreach (var obj in repInventoryReportController.DropdownListItemCategory())
                {
                    newItemList.Add(new Entities.MstItemEntity
                    {
                        Id = obj.Id,
                        Category = obj.Category
                    });
                };
                comboBoxCategory.DataSource = newItemList;
                comboBoxCategory.ValueMember = "Category";
                comboBoxCategory.DisplayMember = "Category";
            }
        }

           
    private void listBoxSalesReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxInventoryReport.SelectedItem != null)
            {
                String selectedItem = listBoxInventoryReport.SelectedItem.ToString();
                
                switch (selectedItem)
                {
                    case "Inventory Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;
                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;
                        checkBoxFilter.Visible = true;
                        labelFilter.Visible = true;
                        comboBoxItem.Visible = false;
                        labelItem.Visible = false;
                        checkBoxFilter.Checked = false;
                        dateTimePickerStartDate.Focus();
                        buttonView.Text = "View";

                        break;
                    case "Item List Report":
                        checkBoxFilter.Visible = false;
                        labelFilter.Visible = false;
                        comboBoxItem.Visible = false;
                        labelItem.Visible = false;
                        buttonView.Text = "View";

                        break;
                    case "Stock Card":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;
                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;
                        checkBoxFilter.Visible = false;
                        labelFilter.Visible = false;
                        comboBoxItem.Visible = true;
                        labelItem.Visible = true;
                        comboBoxCategory.Visible = false;
                        labelCategory.Visible = false;
                        dateTimePickerStartDate.Focus();
                        buttonView.Text = "View";

                        break;
                    case "Stock In Detail Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;
                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;
                        checkBoxFilter.Visible = false;
                        labelFilter.Visible = false;
                        comboBoxCategory.Visible = false;
                        labelCategory.Visible = false;
                        comboBoxItem.Visible = false;
                        labelItem.Visible = false;
                        dateTimePickerStartDate.Focus();
                        buttonView.Text = "View";

                        break;
                    case "Stock Out Detail Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;
                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;
                        checkBoxFilter.Visible = false;
                        labelFilter.Visible = false;
                        comboBoxCategory.Visible = false;
                        labelCategory.Visible = false;
                        comboBoxItem.Visible = false;
                        labelItem.Visible = false;
                        dateTimePickerStartDate.Focus();
                        buttonView.Text = "View";

                        break;
                    case "Stock Count Detail Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;
                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;
                        checkBoxFilter.Visible = false;
                        labelFilter.Visible = false;
                        comboBoxCategory.Visible = false;
                        labelCategory.Visible = false;
                        comboBoxItem.Visible = false;
                        labelItem.Visible = false;
                        dateTimePickerStartDate.Focus();
                        buttonView.Text = "View";

                        break;
                    case "Item Expiry Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;
                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;
                        checkBoxFilter.Visible = false;
                        labelFilter.Visible = false;
                        comboBoxCategory.Visible = false;
                        labelCategory.Visible = false;
                        comboBoxItem.Visible = false;
                        labelItem.Visible = false;
                        dateTimePickerStartDate.Focus();
                        buttonView.Text = "View";

                        break;
                    case "80mm Inventory Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;
                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;
                        checkBoxFilter.Visible = true;
                        labelFilter.Visible = true;
                        comboBoxItem.Visible = false;
                        labelItem.Visible = false;
                        checkBoxFilter.Checked = false;
                        dateTimePickerStartDate.Focus();
                        buttonView.Text = "Print";
                        break;
                    default:
                        labelStartDate.Visible = false;
                        dateTimePickerStartDate.Visible = false;
                        labelEndDate.Visible = false;
                        dateTimePickerEndDate.Visible = false;
                        checkBoxFilter.Visible = false;
                        labelFilter.Visible = false;
                        comboBoxCategory.Visible = false;
                        labelCategory.Visible = false;
                        comboBoxItem.Visible = false;
                        labelItem.Visible = false;
                        buttonView.Text = "View";

                        break;
                    
                }
            }
            else
            {
                MessageBox.Show("Please select a report.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonView_OnClick(object sender, EventArgs e)
        {
            if (listBoxInventoryReport.SelectedItem != null)
            {
                String selectedItem = listBoxInventoryReport.SelectedItem.ToString();
                switch (selectedItem)
                {
                    case "Inventory Report":
                        sysUserRights = new Modules.SysUserRightsModule("RepInventory");
                        if (sysUserRights.GetUserRights() == null)
                        {
                            MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (sysUserRights.GetUserRights().CanView == true)
                            {
                                RepInventoryReportForm repInventoryReportInventoryReport = new RepInventoryReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToString(comboBoxCategory.SelectedValue), Convert.ToInt32(comboBoxItem.SelectedValue));
                                repInventoryReportInventoryReport.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    case "Item List Report":
                        sysUserRights = new Modules.SysUserRightsModule("RepInventoryItemList");
                        if (sysUserRights.GetUserRights() == null)
                        {
                            MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (sysUserRights.GetUserRights().CanView == true)
                            {
                                RepInventoryItemListReportForm repInventoryListReportInventoryReport = new RepInventoryItemListReportForm();
                                repInventoryListReportInventoryReport.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    case "Stock Card":
                        sysUserRights = new Modules.SysUserRightsModule("RepInventoryStockCard");
                        if (sysUserRights.GetUserRights() == null)
                        {
                            MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (sysUserRights.GetUserRights().CanView == true)
                            {
                                RepStockCardForm repStockCardForm = new RepStockCardForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxItem.SelectedValue)/*, Convert.ToString(comboBoxCategory.SelectedValue)*/);
                                repStockCardForm.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    case "Stock In Detail Report":
                        sysUserRights = new Modules.SysUserRightsModule("RepInventoryStockInDetail");
                        if (sysUserRights.GetUserRights() == null)
                        {
                            MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (sysUserRights.GetUserRights().CanView == true)
                            {
                                RepStockInDetailReportForm reportStockInDetailReport = new RepStockInDetailReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date);
                                reportStockInDetailReport.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    case "Stock Out Detail Report":
                        sysUserRights = new Modules.SysUserRightsModule("RepInventoryStockOutDetail");
                        if (sysUserRights.GetUserRights() == null)
                        {
                            MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (sysUserRights.GetUserRights().CanView == true)
                            {
                                RepStockOutDetailReportForm repInventoryReportStockOut = new RepStockOutDetailReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date);
                                repInventoryReportStockOut.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    case "Stock Count Detail Report":
                        sysUserRights = new Modules.SysUserRightsModule("RepInventoryStockCountDetail");
                        if (sysUserRights.GetUserRights() == null)
                        {
                            MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (sysUserRights.GetUserRights().CanView == true)
                            {
                                RepStockCountDetailReportForm repInventoryReportStockCount = new RepStockCountDetailReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date);
                                repInventoryReportStockCount.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                       
                        break;
                    case "Item Expiry Report":
                        sysUserRights = new Modules.SysUserRightsModule("RepInventoryItemExpiry");
                        if (sysUserRights.GetUserRights() == null)
                        {
                            MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (sysUserRights.GetUserRights().CanView == true)
                            {
                                RepItemExpiryReportForm repItemExpiryReport = new RepItemExpiryReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date);
                                repItemExpiryReport.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    case "80mm Inventory Report":
                        sysUserRights = new Modules.SysUserRightsModule("RepRestaurantInventory");
                        if (sysUserRights.GetUserRights() == null)
                        {
                            MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (sysUserRights.GetUserRights().CanPrint == true)
                            {
                                _80mm_Report.RepInventoryReport80mmForm repInventoryReport80mm = new _80mm_Report.RepInventoryReport80mmForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToString(comboBoxCategory.SelectedValue), Convert.ToInt32(comboBoxItem.SelectedValue));
                                repInventoryReport80mm.ShowDialog();
                            }
                            else
                            {
                                
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        break;
                    default:
                        MessageBox.Show("Please select a report.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        break;
                }
            }
            else
            {
                MessageBox.Show("Please select a report.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_OnClick(object sender, EventArgs e)
        {
            sysSoftwareForm.RemoveTabPage();
        }

        private void checkBoxFilter_CheckedChanged(object sender, EventArgs e)
        {
            HideComboBox();
        }
        public void HideComboBox()
        {
            if(checkBoxFilter.Checked == true)
            {
               comboBoxCategory.Visible = true;
               comboBoxItem.Visible = true;
               labelItem.Visible = true;
               labelCategory.Visible = true;
                comboBoxCategory.Text = "ALL";
                comboBoxItem.Text = "ALL";
            }
            else
            {
                comboBoxItem.Visible = false;
                comboBoxCategory.Visible = false;
                labelItem.Visible = false;
                labelCategory.Visible = false;
                comboBoxCategory.Text = "ALL";
                comboBoxItem.Text = "ALL";
            }
        }

       
    }
}
