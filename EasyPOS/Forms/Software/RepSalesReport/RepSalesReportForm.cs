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

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public RepSalesReportForm(SysSoftwareForm softwareForm)
        {
            InitializeComponent();

            label1.Text = SetLabel(label1.Text);
            label2.Text = SetLabel(label2.Text);
            labelStartDate.Text = SetLabel(labelStartDate.Text);
            labelEndDate.Text = SetLabel(labelEndDate.Text);
            labelTerminal.Text = SetLabel(labelTerminal.Text);
            labelCustomer.Text = SetLabel(labelCustomer.Text);
            labelAgent.Text = SetLabel(labelAgent.Text);
            labelDateAsOf.Text = SetLabel(labelDateAsOf.Text);
            buttonView.Text = SetLabel(buttonView.Text);
            buttonClose.Text = SetLabel(buttonClose.Text);

            //Listbox Change Language
            listBoxSalesReport.BeginUpdate();

            ListBox.ObjectCollection items = listBoxSalesReport.Items;
            int count = items.Count;

            for (int i = 0; i < count; i++)
            {
                // — Update The Item
                items[i] = SetLabel(items[i].ToString());
            }

            listBoxSalesReport.EndUpdate();
            //End Change Language

            sysSoftwareForm = softwareForm;

            GetTerminalList();
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
            GetSalesSupplierList();
        }

        public void GetSalesSupplierList()
        {
            Controllers.RepSalesReportController repSalesReportController = new Controllers.RepSalesReportController();
            if (repSalesReportController.DropdownListSupplier().Any())
            {
                List<Entities.MstSupplierEntity> newSalesSupplierList = new List<Entities.MstSupplierEntity>();
                newSalesSupplierList.Add(new Entities.MstSupplierEntity
                {
                    Id = 0,
                    Supplier = "ALL"
                });

                foreach (var obj in repSalesReportController.DropdownListSupplier())
                {
                    newSalesSupplierList.Add(new Entities.MstSupplierEntity
                    {
                        Id = obj.Id,
                        Supplier = obj.Supplier
                    });
                };

                comboBoxSupplier.DataSource = newSalesSupplierList;
                comboBoxSupplier.ValueMember = "Id";
                comboBoxSupplier.DisplayMember = "Supplier";
            }
            GetSalesItemList();
        }

        public void GetSalesItemList()
        {
            Controllers.RepSalesReportController repSalesReportController = new Controllers.RepSalesReportController();
            if (repSalesReportController.DropdownListSalesItem().Any())
            {
                List<Entities.MstItemEntity> newSalesItemList = new List<Entities.MstItemEntity>();
                newSalesItemList.Add(new Entities.MstItemEntity
                {
                    Id = 0,
                    ItemDescription = "ALL"
                });

                foreach (var obj in repSalesReportController.DropdownListSalesItem())
                {
                    newSalesItemList.Add(new Entities.MstItemEntity
                    {
                        Id = obj.Id,
                        ItemDescription = obj.ItemDescription
                    });
                };

                comboBoxItemFilter.DataSource = newSalesItemList;
                comboBoxItemFilter.ValueMember = "Id";
                comboBoxItemFilter.DisplayMember = "ItemDescription";
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
                    case "销售总结报告":
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

                        labelSupplier.Visible = false;
                        comboBoxSupplier.Visible = false;

                        labelItem.Visible = false;
                        comboBoxItemFilter.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = SetLabel("View");

                        break;
                    case "Sales Detail Report":
                    case "销售明细报表":
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

                        labelSupplier.Visible = true;
                        comboBoxSupplier.Visible = true;

                        labelItem.Visible = true;
                        comboBoxItemFilter.Visible = true;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = SetLabel("View");

                        break;
                    case "80mm Sales Summary Report":
                    case "80mm 销售总结报告":
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

                        labelSupplier.Visible = false;
                        comboBoxSupplier.Visible = false;

                        labelItem.Visible = false;
                        comboBoxItemFilter.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = SetLabel("Print");

                        break;
                    case "80mm Sales Detail Report":
                    case "80mm 销售明细报告":
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

                        labelSupplier.Visible = false;
                        comboBoxSupplier.Visible = false;

                        labelItem.Visible = false;
                        comboBoxItemFilter.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = SetLabel("Print");

                        break;
                    case "80mm Sales Status Report":
                    case "80mm 销售状况报告":
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

                        labelSupplier.Visible = false;
                        comboBoxSupplier.Visible = false;

                        labelItem.Visible = false;
                        comboBoxItemFilter.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = SetLabel("Print");

                        break;
                    case "Collection Summary Report":
                    case "馆藏汇总报告":
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

                        labelSupplier.Visible = false;
                        comboBoxSupplier.Visible = false;

                        labelItem.Visible = false;
                        comboBoxItemFilter.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = SetLabel("View");

                        break;
                    case "Collection Detail Report":
                    case "馆藏详情报告":
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

                        labelSupplier.Visible = false;
                        comboBoxSupplier.Visible = false;

                        labelItem.Visible = false;
                        comboBoxItemFilter.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = SetLabel("View");

                        break;
                    case "80mm Collection Detail Report":
                    case "80 毫米系列详细报告":
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

                        labelSupplier.Visible = false;
                        comboBoxSupplier.Visible = false;

                        labelItem.Visible = false;
                        comboBoxItemFilter.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = SetLabel("Print");

                        break;
                    case "Cancelled Summary Report":
                    case "取消的总结报告":
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

                        labelSupplier.Visible = false;
                        comboBoxSupplier.Visible = false;

                        labelItem.Visible = false;
                        comboBoxItemFilter.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = SetLabel("View");

                        break;
                    case "Stock Withdrawal Report":
                    case "股票提取报告":
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

                        labelSupplier.Visible = false;
                        comboBoxSupplier.Visible = false;

                        labelItem.Visible = false;
                        comboBoxItemFilter.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = SetLabel("View");

                        break;
                    case "Collection Detail Report (Facepay)":
                    case "收款详情报告 (Facepay)":
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

                        labelSupplier.Visible = false;
                        comboBoxSupplier.Visible = false;

                        labelItem.Visible = false;
                        comboBoxItemFilter.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = SetLabel("View");

                        break;
                    case "Top Selling Items Report":
                    case "畅销商品报告":
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

                        labelSupplier.Visible = false;
                        comboBoxSupplier.Visible = false;

                        labelItem.Visible = false;
                        comboBoxItemFilter.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = SetLabel("View");

                        break;
                    case "Sales Return Detail Report":
                    case "销售退货明细报表":
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

                        labelSupplier.Visible = false;
                        comboBoxSupplier.Visible = false;

                        labelItem.Visible = false;
                        comboBoxItemFilter.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = SetLabel("View");

                        break;
                    case "Customer List Report":
                    case "客户名单报告":
                        buttonView.Text = SetLabel("View");

                        break;
                    case "Sales Summary Reward Report":
                    case "销售总结奖励报告":
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

                        labelSupplier.Visible = false;
                        comboBoxSupplier.Visible = false;

                        labelItem.Visible = false;
                        comboBoxItemFilter.Visible = false;

                        buttonView.Text = SetLabel("View");

                        break;
                    case "Net Sales Summary Report - Daily":
                    case "净销售额汇总报告 - 每日":
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

                        labelSupplier.Visible = false;
                        comboBoxSupplier.Visible = false;

                        labelItem.Visible = false;
                        comboBoxItemFilter.Visible = false;

                        buttonView.Text = SetLabel("View");

                        break;
                    case "Net Sales Summary Report - Monthly":
                    case "净销售额汇总报告 - 每月":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;

                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;

                        labelCustomer.Visible = false;
                        comboBoxCustomer.Visible = false;

                        labelAgent.Visible = false;
                        comboBoxSalesAgent.Visible = false;

                        labelSupplier.Visible = false;
                        comboBoxSupplier.Visible = false;

                        labelItem.Visible = false;
                        comboBoxItemFilter.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = SetLabel("View");

                        break;
                    case "Hourly Top Selling Sales Report":
                    case "每小时最畅销销售报告":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;

                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;

                        labelCustomer.Visible = false;
                        comboBoxCustomer.Visible = false;

                        labelAgent.Visible = false;
                        comboBoxSalesAgent.Visible = false;

                        labelSupplier.Visible = false;
                        comboBoxSupplier.Visible = false;

                        labelItem.Visible = false;
                        comboBoxItemFilter.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = SetLabel("View");

                        break;

                    case "Unsold Item Report":
                    case "未售出商品报告":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;

                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;

                        labelCustomer.Visible = false;
                        comboBoxCustomer.Visible = false;

                        labelAgent.Visible = false;
                        comboBoxSalesAgent.Visible = false;

                        labelSupplier.Visible = false;
                        comboBoxSupplier.Visible = false;

                        labelItem.Visible = false;
                        comboBoxItemFilter.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        dateTimePickerStartDate.Focus();
                        buttonView.Text = SetLabel("View");

                        break;
                    case "Cost Of Sales Report":
                    case "销售成本报告":
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

                        labelSupplier.Visible = false;
                        comboBoxSupplier.Visible = false;

                        labelItem.Visible = false;
                        comboBoxItemFilter.Visible = false;

                        labelDateAsOf.Visible = true;
                        dateTimePickerDateAsOf.Visible = true;

                        labelStartDate.Focus();
                        buttonView.Text = SetLabel("View");

                        break;
                    case "Accounts Receivable":
                    case "应收账款":
                        labelStartDate.Visible = false;
                        dateTimePickerStartDate.Visible = false;

                        labelEndDate.Visible = false;
                        dateTimePickerEndDate.Visible = false;

                        labelTerminal.Visible = false;
                        comboBoxTerminal.Visible = false;

                        labelCustomer.Visible = true;
                        comboBoxCustomer.Visible = true;

                        labelAgent.Visible = false;
                        comboBoxSalesAgent.Visible = false;

                        labelItem.Visible = false;
                        comboBoxItemFilter.Visible = false;

                        labelSupplier.Visible = false;
                        comboBoxSupplier.Visible = false;

                        labelDateAsOf.Visible = true;
                        dateTimePickerDateAsOf.Visible = true;

                        labelDateAsOf.Focus();
                        buttonView.Text = "View";

                        break;
                    case "Daily Sales Report (BIR)":
                    //case "应收账款":
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

                        labelItem.Visible = false;
                        comboBoxItemFilter.Visible = false;

                        labelSupplier.Visible = false;
                        comboBoxSupplier.Visible = false;

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        labelDateAsOf.Focus();
                        buttonView.Text = "View";

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

                        labelSupplier.Visible = false;
                        comboBoxSupplier.Visible = false;

                        labelItem.Visible = false;
                        comboBoxItemFilter.Visible = false;

                        dateTimePickerStartDate.Focus();

                        labelDateAsOf.Visible = false;
                        dateTimePickerDateAsOf.Visible = false;

                        buttonView.Text = "View";

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
                        case "销售总结报告":
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
                        case "销售明细报表":
                            sysUserRights = new Modules.SysUserRightsModule("RepSalesDetail");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanView == true)
                                {
                                    RepSalesDetailReportForm repSalesReportSalesDetail = new RepSalesDetailReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue), Convert.ToInt32(comboBoxCustomer.SelectedValue), Convert.ToInt32(comboBoxSalesAgent.SelectedValue), Convert.ToInt32(comboBoxSupplier.SelectedValue), Convert.ToInt32(comboBoxItemFilter.SelectedValue));
                                    repSalesReportSalesDetail.ShowDialog();
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                            break;
                        case "80mm Sales Summary Report":
                        case "80mm 销售总结报告":
                            sysUserRights = new Modules.SysUserRightsModule("RepRestaurantSalesSummary");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanPrint == true)
                                {
                                    new Rep80mmSalesSummaryReportPDFForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue), 0, 0);
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                            break;
                        case "80mm Sales Detail Report":
                        case "80mm 销售明细报告":
                            sysUserRights = new Modules.SysUserRightsModule("RepRestaurantSalesDetail");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanPrint == true)
                                {
                                    new Rep80mmSalesDetailReportPDFForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue), 0, 0);
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                            break;

                        case "80mm Sales Status Report":
                        case "80mm 销售状况报告":
                            sysUserRights = new Modules.SysUserRightsModule("RepRestaurantSalesStatus");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanPrint == true)
                                {
                                    _80mmReport.RepSalesStatusReport80mmForm repSalesStatusReport80MmForm = new _80mmReport.RepSalesStatusReport80mmForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue));
                                    repSalesStatusReport80MmForm.ShowDialog();
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                            break;

                        case "Collection Summary Report":
                        case "馆藏汇总报告":
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
                        case "馆藏详情报告":
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
                        case "80 毫米系列详细报告":
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
                        case "取消的总结报告":
                            sysUserRights = new Modules.SysUserRightsModule("RepCollectionDetail");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanPrint == true)
                                {
                                    new Rep80mmCollectionDetailReportPDFForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue));
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }

                            break;
                        case "Stock Withdrawal Report":
                        case "股票提取报告":
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
                        case "收款详情报告 (Facepay)":
                            String printCollectionDetailReportFacepayFilePath = "";

                            DialogResult folderBrowserDialogCollectionDetailReportFacepayDialoResult = folderBrowserDialogCollectionDetailReportFacepay.ShowDialog();
                            if (folderBrowserDialogCollectionDetailReportFacepayDialoResult == DialogResult.OK)
                            {
                                printCollectionDetailReportFacepayFilePath = folderBrowserDialogCollectionDetailReportFacepay.SelectedPath;
                                new RepCollectionDetailFacepayReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue), printCollectionDetailReportFacepayFilePath);
                            }

                            break;
                        case "Top Selling Items Report":
                        case "畅销商品报告":
                            RepTopSellingItemsReportForm repSalesReportTopSellingItemsReportForm = new RepTopSellingItemsReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date);
                            repSalesReportTopSellingItemsReportForm.ShowDialog();

                            break;
                        case "Sales Return Detail Report":
                        case "销售退货明细报表":
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
                        case "客户名单报告":
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
                        case "销售总结奖励报告":
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
                        case "净销售额汇总报告 - 每日":
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
                        case "净销售额汇总报告 - 每月":
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
                        case "每小时最畅销销售报告":
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
                        case "未售出商品报告":
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
                        case "销售成本报告":
                            sysUserRights = new Modules.SysUserRightsModule("RepSalesDetail");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanView == true)
                                {
                                    RepCostOfSaleReportForm repCostOfSaleReportForm = new RepCostOfSaleReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue), Convert.ToInt32(comboBoxCustomer.SelectedValue), Convert.ToInt32(comboBoxSalesAgent.SelectedValue), Convert.ToInt32(comboBoxSupplier.SelectedValue), Convert.ToInt32(comboBoxItemFilter.SelectedValue));
                                    repCostOfSaleReportForm.ShowDialog();
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            break;

                        case "Accounts Receivable":
                        case "应收账款":
                            sysUserRights = new Modules.SysUserRightsModule("RepSalesDetail");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanView == true)
                                {
                                    RepAccountsReceivableSummaryReportForm repAccountsReceivableSummaryReportForm = new RepAccountsReceivableSummaryReportForm(dateTimePickerDateAsOf.Value.Date, Convert.ToInt32(comboBoxCustomer.SelectedValue));
                                    repAccountsReceivableSummaryReportForm.ShowDialog();
                                }
                                else
                                {
                                    MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            break;

                        case "Daily Sales Report (BIR)":
                        //case "应收账款":
                            sysUserRights = new Modules.SysUserRightsModule("RepSalesDetail");

                            if (sysUserRights.GetUserRights() == null)
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                if (sysUserRights.GetUserRights().CanView == true)
                                {
                                    RepDailySalesBIRReportPDFForm repDailySalesBIRReportPDFForm = new RepDailySalesBIRReportPDFForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue));
                                    repDailySalesBIRReportPDFForm.ShowDialog();
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

    }
}
