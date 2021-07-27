using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.RepSalesReport
{
    public partial class RepSalesReportForm : Form
    {
        SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public RepSalesReportForm(SysSoftwareForm softwareForm)
        {
            InitializeComponent();
            sysSoftwareForm = softwareForm;

            GetTerminalList();
        }

        public void GetTerminalList()
        {
            Controllers.RepSalesReportController repSalesReportController = new Controllers.RepSalesReportController();
            if (repSalesReportController.DropdownListTerminal().Any())
            {
                comboBoxTerminal.DataSource = repSalesReportController.DropdownListTerminal();
                comboBoxTerminal.ValueMember = "Id";
                comboBoxTerminal.DisplayMember = "Terminal";
            }

            GetCustomerList();

        }

        public void GetCustomerList()
        {
            Controllers.RepSalesReportController repSalesReportController = new Controllers.RepSalesReportController();
            if (repSalesReportController.DropdownListCustomer().Any())
            {
                List<Entities.MstCustomerEntity> newCustomerList = new List<Entities.MstCustomerEntity>();
                newCustomerList.Add(new Entities.MstCustomerEntity
                {
                    Id = 0,
                    Customer = "ALL"
                });

                foreach (var obj in repSalesReportController.DropdownListCustomer())
                {
                    newCustomerList.Add(new Entities.MstCustomerEntity
                    {
                        Id = obj.Id,
                        Customer = obj.Customer
                    });
                };

                comboBoxCustomer.DataSource = newCustomerList;
                comboBoxCustomer.ValueMember = "Id";
                comboBoxCustomer.DisplayMember = "Customer";
            }
            GetSalesAgentList();
        }
        public void GetSalesAgentList()
        {
            Controllers.RepSalesReportController repSalesReportController = new Controllers.RepSalesReportController();
            if (repSalesReportController.DropdownListAgent().Any())
            {
                List<Entities.MstUserEntity> newSalesAgentList = new List<Entities.MstUserEntity>();
                newSalesAgentList.Add(new Entities.MstUserEntity
                {
                    Id = 0,
                    FullName = "ALL"
                });

                foreach (var obj in repSalesReportController.DropdownListAgent())
                {
                    newSalesAgentList.Add(new Entities.MstUserEntity
                    {
                        Id = obj.Id,
                        FullName = obj.FullName
                    });
                };

                comboBoxSalesAgent.DataSource = newSalesAgentList;
                comboBoxSalesAgent.ValueMember = "Id";
                comboBoxSalesAgent.DisplayMember = "FullName";
            }
        }

        private void listBoxSalesReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSalesReport.SelectedItem != null)
            {
                String selectedItem = listBoxSalesReport.SelectedItem.ToString();
                switch (selectedItem)
                {
                    case "Sales Summary Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;

                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;

                        labelTerminal.Visible = true;
                        comboBoxTerminal.Visible = true;

                        labelCustomer.Visible = true;
                        comboBoxCustomer.Visible = true;

                        labelAgent.Visible = true;
                        comboBoxSalesAgent.Visible = true;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = "View";

                        buttonPreview.Visible = false;

                        break;
                    case "Sales Detail Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;

                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;

                        labelTerminal.Visible = true;
                        comboBoxTerminal.Visible = true;

                        labelCustomer.Visible = true;
                        comboBoxCustomer.Visible = true;

                        labelAgent.Visible = true;
                        comboBoxSalesAgent.Visible = true;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = "View";

                        buttonPreview.Visible = false;

                        break;
                    case "80mm Sales Summary Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;

                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;

                        labelTerminal.Visible = true;
                        comboBoxTerminal.Visible = true;

                        labelCustomer.Visible = false;
                        comboBoxCustomer.Visible = false;

                        labelAgent.Visible = false;
                        comboBoxSalesAgent.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = "Print";

                        buttonPreview.Visible = true;

                        break;
                    case "80mm Sales Detail Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;

                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;

                        labelTerminal.Visible = true;
                        comboBoxTerminal.Visible = true;

                        labelCustomer.Visible = false;
                        comboBoxCustomer.Visible = false;

                        labelAgent.Visible = false;
                        comboBoxSalesAgent.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = "Print";

                        buttonPreview.Visible = true;

                        break;
                    case "80mm Sales Status Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;

                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;

                        labelTerminal.Visible = true;
                        comboBoxTerminal.Visible = true;

                        labelCustomer.Visible = false;
                        comboBoxCustomer.Visible = false;

                        labelAgent.Visible = false;
                        comboBoxSalesAgent.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = "Print";

                        buttonPreview.Visible = true;

                        break;
                    case "Collection Summary Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;

                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;

                        labelTerminal.Visible = true;
                        comboBoxTerminal.Visible = true;

                        labelCustomer.Visible = false;
                        comboBoxCustomer.Visible = false;

                        labelAgent.Visible = false;
                        comboBoxSalesAgent.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = "View";

                        buttonPreview.Visible = false;

                        break;
                    case "Collection Detail Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;

                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;

                        labelTerminal.Visible = true;
                        comboBoxTerminal.Visible = true;

                        labelCustomer.Visible = false;
                        comboBoxCustomer.Visible = false;

                        labelAgent.Visible = false;
                        comboBoxSalesAgent.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = "View";

                        buttonPreview.Visible = false;

                        break;
                    case "80mm Collection Detail Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;

                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;

                        labelTerminal.Visible = true;
                        comboBoxTerminal.Visible = true;

                        labelCustomer.Visible = false;
                        comboBoxCustomer.Visible = false;

                        labelAgent.Visible = false;
                        comboBoxSalesAgent.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = "Print";

                        buttonPreview.Visible = true;

                        break;
                    case "Cancelled Summary Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;

                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;

                        labelTerminal.Visible = true;
                        comboBoxTerminal.Visible = true;

                        labelCustomer.Visible = false;
                        comboBoxCustomer.Visible = false;

                        labelAgent.Visible = false;
                        comboBoxSalesAgent.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = "View";

                        buttonPreview.Visible = false;

                        break;
                    case "Stock Withdrawal Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;

                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;

                        labelTerminal.Visible = true;
                        comboBoxTerminal.Visible = true;

                        labelCustomer.Visible = true;
                        comboBoxCustomer.Visible = true;

                        labelAgent.Visible = false;
                        comboBoxSalesAgent.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = "View";

                        buttonPreview.Visible = false;

                        break;
                    case "Collection Detail Report (Facepay)":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;

                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;

                        labelTerminal.Visible = true;
                        comboBoxTerminal.Visible = true;

                        labelCustomer.Visible = false;
                        comboBoxCustomer.Visible = false;

                        labelAgent.Visible = false;
                        comboBoxSalesAgent.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = "View";

                        buttonPreview.Visible = false;

                        break;
                    case "Top Selling Items Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;

                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;

                        labelTerminal.Visible = false;
                        comboBoxTerminal.Visible = false;

                        labelCustomer.Visible = false;
                        comboBoxCustomer.Visible = false;

                        labelAgent.Visible = false;
                        comboBoxSalesAgent.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = "View";

                        buttonPreview.Visible = false;

                        break;
                    case "Sales Return Detail Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;

                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;

                        labelTerminal.Visible = true;
                        comboBoxTerminal.Visible = true;

                        labelCustomer.Visible = false;
                        comboBoxCustomer.Visible = false;

                        labelAgent.Visible = false;
                        comboBoxSalesAgent.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = "View";

                        buttonPreview.Visible = false;

                        break;
                    case "Customer List Report":
                        buttonView.Text = "View";
                        buttonPreview.Visible = false;

                        break;
                    case "Sales Summary Reward Report":
                        labelStartDate.Visible = false;
                        dateTimePickerStartDate.Visible = false;

                        labelEndDate.Visible = false;
                        dateTimePickerEndDate.Visible = false;

                        labelTerminal.Visible = false;
                        comboBoxTerminal.Visible = false;

                        labelCustomer.Visible = true;
                        comboBoxCustomer.Visible = true;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        labelAgent.Visible = false;
                        comboBoxSalesAgent.Visible = false;

                        buttonView.Text = "View";

                        buttonPreview.Visible = false;

                        break;
                    case "Net Sales Summary Report - Daily":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;

                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;

                        dateTimePickerStartDate.Focus();

                        labelCustomer.Visible = false;
                        comboBoxCustomer.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        labelAgent.Visible = false;
                        comboBoxSalesAgent.Visible = false;

                        buttonView.Text = "View";

                        buttonPreview.Visible = false;

                        break;
                    case "Net Sales Summary Report - Monthly":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;

                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;

                        labelCustomer.Visible = false;
                        comboBoxCustomer.Visible = false;

                        labelAgent.Visible = false;
                        comboBoxSalesAgent.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = "View";

                        buttonPreview.Visible = false;

                        break;
                    case "Hourly Top Selling Sales Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;

                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;

                        labelCustomer.Visible = false;
                        comboBoxCustomer.Visible = false;

                        labelAgent.Visible = false;
                        comboBoxSalesAgent.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = "View";

                        buttonPreview.Visible = false;

                        break;

                    case "Unsold Item Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;

                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;

                        labelCustomer.Visible = false;
                        comboBoxCustomer.Visible = false;

                        labelAgent.Visible = false;
                        comboBoxSalesAgent.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = "View";

                        buttonPreview.Visible = false;

                        break;
                    case "Cost Of Sales Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;

                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;

                        labelTerminal.Visible = true;
                        comboBoxTerminal.Visible = true;

                        labelCustomer.Visible = true;
                        comboBoxCustomer.Visible = true;

                        labelAgent.Visible = false;
                        comboBoxSalesAgent.Visible = false;

                        labelDateAsOf.Visible = true;
                        dateTimePickerDateAsOf.Visible = true;

                        labelStartDate.Focus();
                        buttonView.Text = "View";

                        buttonPreview.Visible = false;

                        break;
                    case "Accounts Receivable":
                        labelStartDate.Visible = false;
                        dateTimePickerStartDate.Visible = false;

                        labelEndDate.Visible = false;
                        dateTimePickerEndDate.Visible = false;

                        labelTerminal.Visible = false;
                        comboBoxTerminal.Visible = false;

                        labelCustomer.Visible = false;
                        comboBoxCustomer.Visible = false;

                        labelAgent.Visible = false;
                        comboBoxSalesAgent.Visible = false;

                        labelDateAsOf.Visible = true;
                        dateTimePickerDateAsOf.Visible = true;

                        labelDateAsOf.Focus();
                        buttonView.Text = "View";

                        buttonPreview.Visible = false;

                        break;
                    default:
                        labelStartDate.Visible = false;
                        dateTimePickerStartDate.Visible = false;

                        labelEndDate.Visible = false;
                        dateTimePickerEndDate.Visible = false;

                        labelTerminal.Visible = false;
                        comboBoxTerminal.Visible = false;

                        labelCustomer.Visible = false;
                        comboBoxCustomer.Visible = false;

                        labelAgent.Visible = false;
                        comboBoxSalesAgent.Visible = false;

                        dateTimePickerStartDate.Focus();

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        buttonView.Text = "View";

                        buttonPreview.Visible = false;

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

        private void buttonView_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (listBoxSalesReport.SelectedItem != null)
                {
                    String selectedItem = listBoxSalesReport.SelectedItem.ToString();
                    switch (selectedItem)
                    {
                        case "Sales Summary Report":
                            sysUserRights = new Modules.SysUserRightsModule("RepSalesSummary");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanView == true)
                                {
                                    RepSalesSummaryReportForm repSalesSummaryReport = new RepSalesSummaryReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue), Convert.ToInt32(comboBoxCustomer.SelectedValue), Convert.ToInt32(comboBoxSalesAgent.SelectedValue));
                                    repSalesSummaryReport.ShowDialog();
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                            break;

                        case "Sales Detail Report":
                            sysUserRights = new Modules.SysUserRightsModule("RepSalesDetail");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanView == true)
                                {
                                    RepSalesDetailReportForm repSalesReportSalesDetail = new RepSalesDetailReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue), Convert.ToInt32(comboBoxCustomer.SelectedValue), Convert.ToInt32(comboBoxSalesAgent.SelectedValue));
                                    repSalesReportSalesDetail.ShowDialog();
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                            break;
                        case "80mm Sales Summary Report":
                            sysUserRights = new Modules.SysUserRightsModule("RepRestaurantSalesSummary");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanPrint == true)
                                {
                                    _80mmReport.RepSalesSummaryReport80mmForm repSalesSummaryReport80MmForm = new _80mmReport.RepSalesSummaryReport80mmForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue), false, "");
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                            break;
                        case "80mm Sales Detail Report":
                            sysUserRights = new Modules.SysUserRightsModule("RepRestaurantSalesDetail");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanPrint == true)
                                {
                                    _80mmReport.RepSalesDetailReport80mmForm repSalesDetailReport80MmForm = new _80mmReport.RepSalesDetailReport80mmForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue), false, "");
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                            break;

                        case "80mm Sales Status Report":
                            sysUserRights = new Modules.SysUserRightsModule("RepRestaurantSalesStatus");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanPrint == true)
                                {
                                    _80mmReport.RepSalesStatusReport80mmForm repSalesStatusReport80MmForm = new _80mmReport.RepSalesStatusReport80mmForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue), false, "");
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                            break;

                        case "Collection Summary Report":
                            sysUserRights = new Modules.SysUserRightsModule("RepCollectionSummary");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanView == true)
                                {
                                    RepCollectionSummaryReport reportCollectionReport = new RepCollectionSummaryReport(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue));
                                    reportCollectionReport.ShowDialog();
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                            break;
                        case "Collection Detail Report":
                            sysUserRights = new Modules.SysUserRightsModule("RepCollectionDetail");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanView == true)
                                {
                                    RepCollectionDetailReportForm reportCollectionDetailReportForm = new RepCollectionDetailReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue));
                                    reportCollectionDetailReportForm.ShowDialog();
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                            break;
                        case "Cancelled Summary Report":
                            sysUserRights = new Modules.SysUserRightsModule("RepSalesCancelledSummary");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanView == true)
                                {
                                    RepCancelSalesSummaryReportForm repCancelSalesSummaryReport = new RepCancelSalesSummaryReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue));
                                    repCancelSalesSummaryReport.ShowDialog();
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                            break;
                        case "80mm Collection Detail Report":
                            sysUserRights = new Modules.SysUserRightsModule("RepCollectionDetail");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanPrint == true)
                                {
                                    _80mmReport.RepCollectionDetailReport80mmForm repCollectionDetailReport80MmForm = new _80mmReport.RepCollectionDetailReport80mmForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue), false, "");
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                            break;
                        case "Stock Withdrawal Report":
                            sysUserRights = new Modules.SysUserRightsModule("RepCollectionDetail");
                            String printFilePath = "";
                            DialogResult folderBrowserDialoResult = folderBrowserDialogStockWithdrawalReport.ShowDialog();

                            if (folderBrowserDialoResult == DialogResult.OK)
                            {
                                printFilePath = folderBrowserDialogStockWithdrawalReport.SelectedPath;

                                if (String.IsNullOrEmpty(printFilePath) == true)
                                {
                                    MessageBox.Show("Empty file path", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    Controllers.RepSalesReportController repSalesReportController = new Controllers.RepSalesReportController();
                                    if (repSalesReportController.StockWithdrawalReport(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue), Convert.ToInt32(comboBoxCustomer.SelectedValue)).Any())
                                    {
                                        var collectionList = repSalesReportController.StockWithdrawalReport(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue), Convert.ToInt32(comboBoxCustomer.SelectedValue));
                                        new Software.TrnPOS.TrnPOSDeliveryReceiptReportForm(printFilePath + "\\", collectionList, false, false, false);

                                        MessageBox.Show("Generate PDF Successful!", "Generate CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }

                            break;
                        case "Collection Detail Report (Facepay)":
                            String printCollectionDetailReportFacepayFilePath = "";

                            DialogResult folderBrowserDialogCollectionDetailReportFacepayDialoResult = folderBrowserDialogCollectionDetailReportFacepay.ShowDialog();
                            if (folderBrowserDialogCollectionDetailReportFacepayDialoResult == DialogResult.OK)
                            {
                                printCollectionDetailReportFacepayFilePath = folderBrowserDialogCollectionDetailReportFacepay.SelectedPath;
                                new RepCollectionDetailFacepayReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue), printCollectionDetailReportFacepayFilePath);
                            }

                            break;
                        case "Top Selling Items Report":
                            RepTopSellingItemsReportForm repSalesReportTopSellingItemsReportForm = new RepTopSellingItemsReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date);
                            repSalesReportTopSellingItemsReportForm.ShowDialog();

                            break;
                        case "Sales Return Detail Report":
                            sysUserRights = new Modules.SysUserRightsModule("RepSalesDetail");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanView == true)
                                {
                                    RepSalesReturnDetailReportForm repSalesReturnDetailReportForm = new RepSalesReturnDetailReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue));
                                    repSalesReturnDetailReportForm.ShowDialog();
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                            break;
                        case "Customer List Report":
                            sysUserRights = new Modules.SysUserRightsModule("RepSalesDetail");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanView == true)
                                {
                                    RepCustomerListReportForm repCustomerListReportForm = new RepCustomerListReportForm();
                                    repCustomerListReportForm.ShowDialog();
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                            break;
                        case "Sales Summary Reward Report":
                            sysUserRights = new Modules.SysUserRightsModule("RepSalesDetail");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanView == true)
                                {
                                    RepSalesSummaryRewardReportForm repSalesSummaryRewardReportForm = new RepSalesSummaryRewardReportForm(Convert.ToInt32(comboBoxCustomer.SelectedValue));
                                    repSalesSummaryRewardReportForm.ShowDialog();
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            break;
                        case "Net Sales Summary Report - Daily":
                            sysUserRights = new Modules.SysUserRightsModule("RepSalesDetail");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanView == true)
                                {
                                    RepNetSalesSummaryReportForm repNetSalesSummaryReport = new RepNetSalesSummaryReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date);
                                    repNetSalesSummaryReport.ShowDialog();
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            break;
                        case "Net Sales Summary Report - Monthly":
                            sysUserRights = new Modules.SysUserRightsModule("RepSalesDetail");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanView == true)
                                {
                                    RepNetSalesSummaryReportMonthlyForm repNetSalesSummaryMonthlyReport = new RepNetSalesSummaryReportMonthlyForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date);
                                    repNetSalesSummaryMonthlyReport.ShowDialog();
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            break;
                        case "Hourly Top Selling Sales Report":
                            sysUserRights = new Modules.SysUserRightsModule("RepSalesDetail");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanView == true)
                                {
                                    RepHourlyTopSellingSalesReportForm repTopSalesSummaryMonthlyReport = new RepHourlyTopSellingSalesReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date);
                                    repTopSalesSummaryMonthlyReport.ShowDialog();
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            break;

                        case "Unsold Item Report":
                            sysUserRights = new Modules.SysUserRightsModule("RepSalesDetail");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanView == true)
                                {
                                    RepUnsoldItemReportForm repUnsoldItemReport = new RepUnsoldItemReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date);
                                    repUnsoldItemReport.ShowDialog();
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            break;
                        case "Cost Of Sales Report":
                            sysUserRights = new Modules.SysUserRightsModule("RepSalesDetail");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanView == true)
                                {
                                    RepCostOfSaleReportForm repCostOfSaleReportForm = new RepCostOfSaleReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue), Convert.ToInt32(comboBoxCustomer.SelectedValue), Convert.ToInt32(comboBoxSalesAgent.SelectedValue));
                                    repCostOfSaleReportForm.ShowDialog();
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            break;

                        case "Accounts Receivable":
                            sysUserRights = new Modules.SysUserRightsModule("RepSalesDetail");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanView == true)
                                {
                                    RepAccountsReceivableSummaryReportForm repAccountsReceivableSummaryReportForm = new RepAccountsReceivableSummaryReportForm(dateTimePickerDateAsOf.Value.Date);
                                    repAccountsReceivableSummaryReportForm.ShowDialog();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {

            try
            {
                if (listBoxSalesReport.SelectedItem != null)
                {
                    String selectedItem = listBoxSalesReport.SelectedItem.ToString();
                    switch (selectedItem)
                    {
                        case "80mm Sales Summary Report":
                            sysUserRights = new Modules.SysUserRightsModule("RepRestaurantSalesSummary");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                DialogResult printDialogResult = printDialog80mmReport.ShowDialog();
                                if (printDialogResult == DialogResult.OK)
                                {
                                    if (sysUserRights.GetUserRights().CanPrint == true)
                                    {
                                        String printerName = printDialog80mmReport.PrinterSettings.PrinterName;
                                        _80mmReport.RepSalesSummaryReport80mmForm repSalesSummaryReport80MmForm = new _80mmReport.RepSalesSummaryReport80mmForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue), true, printerName);
                                    }
                                    else
                                    {
                                        MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }

                            break;
                        case "80mm Sales Detail Report":
                            sysUserRights = new Modules.SysUserRightsModule("RepRestaurantSalesDetail");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                DialogResult printDialogResult = printDialog80mmReport.ShowDialog();
                                if (printDialogResult == DialogResult.OK)
                                {
                                    if (sysUserRights.GetUserRights().CanPrint == true)
                                    {
                                        String printerName = printDialog80mmReport.PrinterSettings.PrinterName;
                                        _80mmReport.RepSalesDetailReport80mmForm repSalesSummaryReport80MmForm = new _80mmReport.RepSalesDetailReport80mmForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue), true, printerName);
                                    }
                                    else
                                    {
                                        MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }

                            break;
                        case "80mm Sales Status Report":
                            sysUserRights = new Modules.SysUserRightsModule("RepRestaurantSalesStatus");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                DialogResult printDialogResult = printDialog80mmReport.ShowDialog();
                                if (printDialogResult == DialogResult.OK)
                                {
                                    if (sysUserRights.GetUserRights().CanPrint == true)
                                    {
                                        String printerName = printDialog80mmReport.PrinterSettings.PrinterName;
                                        _80mmReport.RepSalesStatusReport80mmForm repSalesSummaryReport80MmForm = new _80mmReport.RepSalesStatusReport80mmForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue), true, printerName);
                                    }
                                    else
                                    {
                                        MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }

                            break;
                        case "80mm Collection Detail Report":
                            sysUserRights = new Modules.SysUserRightsModule("RepCollectionDetail");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                DialogResult printDialogResult = printDialog80mmReport.ShowDialog();
                                if (printDialogResult == DialogResult.OK)
                                {
                                    if (sysUserRights.GetUserRights().CanPrint == true)
                                    {
                                        String printerName = printDialog80mmReport.PrinterSettings.PrinterName;
                                        _80mmReport.RepCollectionDetailReport80mmForm repSalesSummaryReport80MmForm = new _80mmReport.RepCollectionDetailReport80mmForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue), true, printerName);
                                    }
                                    else
                                    {
                                        MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
