using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.SysMenu
{
    public partial class SysMenuForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public SysMenuForm(SysSoftwareForm softwareForm)
        {
            InitializeComponent();

            buttonItem.Text = SetLabel(buttonItem.Text);
            buttonDiscounting.Text = SetLabel(buttonDiscounting.Text);
            buttonCustomer.Text = SetLabel(buttonCustomer.Text);
            buttonUser.Text = SetLabel(buttonUser.Text);
            buttonPOS.Text = SetLabel(buttonPOS.Text);
            buttonDisbursement.Text = SetLabel(buttonDisbursement.Text);
            buttonStockIn.Text = SetLabel(buttonStockIn.Text);
            buttonStockOut.Text = SetLabel(buttonStockOut.Text);
            buttonSalesReport.Text = SetLabel(buttonSalesReport.Text);
            buttonRemittanceReport.Text = SetLabel(buttonRemittanceReport.Text);
            buttonInventory.Text = SetLabel(buttonInventory.Text);
            buttonStockCount.Text = SetLabel(buttonStockCount.Text);
            buttonPOSReport.Text = SetLabel(buttonPOSReport.Text);
            buttonSettings.Text = SetLabel(buttonSettings.Text);
            buttonUtilities.Text = SetLabel(buttonUtilities.Text);
            buttonSystemTables.Text = SetLabel(buttonSystemTables.Text);

            sysSoftwareForm = softwareForm;

            sysUserRights = new Modules.SysUserRightsModule("SysMenu");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            var sysCurrent = Modules.SysCurrentModule.GetCurrentSettings();
            if (sysCurrent.POSType == "POS Touch")
            {
                buttonPOS.ImageIndex = 14;
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

        private void buttonItem_Click(object sender, EventArgs e)
        {
            sysUserRights = new Modules.SysUserRightsModule("MstItem");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                sysSoftwareForm.AddTabPageItemList();
                buttonItem.Focus();
            }
        }

        private void buttonPOS_Click(object sender, EventArgs e)
        {
            sysUserRights = new Modules.SysUserRightsModule("TrnSales");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var sysCurrent = Modules.SysCurrentModule.GetCurrentSettings();
                if (sysCurrent.POSType == "POS Touch")
                {
                    sysSoftwareForm.AddTabPagePOSTouchSalesList();
                }
                else
                {
                    sysSoftwareForm.AddTabPagePOSSalesList();
                }
            }
        }

        private void buttonDiscounting_Click(object sender, EventArgs e)
        {
            sysUserRights = new Modules.SysUserRightsModule("MstDiscount");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                sysSoftwareForm.AddTabPageDiscountingList();
            }
        }

        private void buttonPOSReport_Click(object sender, EventArgs e)
        {
            sysUserRights = new Modules.SysUserRightsModule("RepPOS");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                sysSoftwareForm.AddTabPagePOSReport();
            }
        }

        private void buttonUser_Click(object sender, EventArgs e)
        {
            sysUserRights = new Modules.SysUserRightsModule("MstUser");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                sysSoftwareForm.AddTabPageUserList();
            }
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            sysUserRights = new Modules.SysUserRightsModule("SysSettings");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                sysSoftwareForm.AddTabPageSettings();
            }
        }

        private void buttonSalesReport_OnClick(object sender, EventArgs e)
        {
            sysUserRights = new Modules.SysUserRightsModule("RepSales");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                sysSoftwareForm.AddTabPageSalesReport();
            }
        }

        private void buttonCustomer_Click(object sender, EventArgs e)
        {
            sysUserRights = new Modules.SysUserRightsModule("MstCustomer");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                sysSoftwareForm.AddTabPageCustomerList();
            }
        }

        private void buttonStockIn_Click(object sender, EventArgs e)
        {
            sysUserRights = new Modules.SysUserRightsModule("TrnStockIn");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                sysSoftwareForm.AddTabPageStockInList();
            }
        }

        private void buttonStockOut_Click(object sender, EventArgs e)
        {
            sysUserRights = new Modules.SysUserRightsModule("TrnStockOut");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                sysSoftwareForm.AddTabPageStockOutList();
            }
        }

        private void buttonDisbursement_Click(object sender, EventArgs e)
        {
            sysUserRights = new Modules.SysUserRightsModule("TrnDisbursement");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                sysSoftwareForm.AddTabPageDisbursementList();
            }
        }

        private void buttonSystemTables_Click(object sender, EventArgs e)
        {
            sysUserRights = new Modules.SysUserRightsModule("SysTables");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                sysSoftwareForm.AddTabPageSystemTables();
            }
        }

        private void buttonInventory_Click(object sender, EventArgs e)
        {
            sysUserRights = new Modules.SysUserRightsModule("RepInventory");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                sysSoftwareForm.AddTabPageInventoryReports();
            }
        }

        private void buttonRemittanceReport_Click(object sender, EventArgs e)
        {
            sysUserRights = new Modules.SysUserRightsModule("RepDisbursement");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                sysSoftwareForm.AddTabPageRemittanceReports();
            }
        }

        private void buttonStockCount_Click(object sender, EventArgs e)
        {
            sysUserRights = new Modules.SysUserRightsModule("TrnStockCount");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                sysSoftwareForm.AddTabPageStockCountList();
            }
        }

        private void buttonUtilities_Click(object sender, EventArgs e)
        {
            sysUserRights = new Modules.SysUserRightsModule("SysUtilities");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                sysSoftwareForm.AddTabPageUtilities();
            }
            
        }

    }
}
