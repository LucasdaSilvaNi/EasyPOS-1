using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software
{
    public partial class SysSoftwareForm : Form
    {
        public SysSoftwareForm()
        {
            InitializeComponent();
            InitializeDefaultForm();

            String terminal = "";
            Controllers.SysSoftwareController sysSoftwareController = new Controllers.SysSoftwareController();
            if (String.IsNullOrEmpty(sysSoftwareController.GetCurrentTerminal(Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().TerminalId))) == false)
            {
                terminal = sysSoftwareController.GetCurrentTerminal(Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().TerminalId));
            }

            String currentDate = DateTime.Today.ToShortDateString() + "\t\t";
            if (Modules.SysCurrentModule.GetCurrentSettings().IsLoginDate == true)
            {
                currentDate = Modules.SysCurrentModule.GetCurrentSettings().CurrentDate + "\t\t";
            }

            String currentUserName = Modules.SysCurrentModule.GetCurrentSettings().CurrentUserName + "\t\t";
            if (Modules.SysCurrentModule.GetCurrentSettings().PromptLoginSales == true)
            {
                currentUserName = Modules.SysCurrentModule.GetCurrentSettings().CurrentUserName + "\t\t";
            }

            String currentTerminal = terminal;

            labelCurrentUser.Text = "Date: " + currentDate + "     User: " + currentUserName + "     Terminal: " + currentTerminal;
            panelSidebarMenu.Visible = false;

            labelVersion.Text = "EasyPOS Version: " + Modules.SysCurrentModule.GetCurrentSettings().CurrentVersion;
            labelSupport.Text = "Support: Easyfis Corporation " + Modules.SysCurrentModule.GetCurrentSettings().CurrentSupport;
        }

        public TabPage tabPageItemList = new TabPage { Name = "tabPageItemList", Text = "Setup - Item List" };
        public TabPage tabPageItemDetail = new TabPage { Name = "tabPageItemDetail", Text = "Setup - Item Detail" };
        public TabPage tabPageCustomerList = new TabPage { Name = "tabPageCustomerList", Text = "Setup - Customer List" };
        public TabPage tabPageCustomerDetail = new TabPage { Name = "tabPageCustomerDetail", Text = "Setup - Customer Detail" };
        public TabPage tabPageDiscountingList = new TabPage { Name = "tabPageDiscountingList", Text = "Setup - Discounting List" };
        public TabPage tabPageDiscountingDetail = new TabPage { Name = "tabPageDiscountingDetail", Text = "Setup - Discounting Detail" };
        public TabPage tabPageUserList = new TabPage { Name = "tabPageUserList", Text = "Setup - User List" };
        public TabPage tabPageUserDetail = new TabPage { Name = "tabPageUserDetail", Text = "Setup - User Detail" };
        public TabPage tabPageItemGroupList = new TabPage { Name = "tabPageItemGroupList", Text = "Setup - Item Group List" };
        public TabPage tabPageItemGroupDetail = new TabPage { Name = "tabPageItemGroupDetail", Text = "Setup - Item Group Detail" };
        public TabPage tabPageTableGroupList = new TabPage { Name = "tabPageTableGroupList", Text = "Setup - Table Group List" };
        public TabPage tabPageTableGroupDetail = new TabPage { Name = "tabPageTableGroupDetail", Text = "Setup - Table Group Detail" };
        public TabPage tabPagePOSBarcode = new TabPage { Name = "tabPagePOSBarcode", Text = "Activity - POS Barcode" };
        public TabPage tabPagePOSBarcodeDetail = new TabPage { Name = "tabPagePOSBarcodeDetail", Text = "Activity - POS Barcode Detail" };
        public TabPage tabPagePOSTouch = new TabPage { Name = "tabPagePOSTouch", Text = "Activity - POS Touch" };
        public TabPage tabPagePOSTouchDetail = new TabPage { Name = "tabPagePOSTouchDetail", Text = "Activity - POS Touch Detail" };
        public TabPage tabPageStockInList = new TabPage { Name = "tabPageStockInList", Text = "Activity - Stock-In List" };
        public TabPage tabPageStockInDetail = new TabPage { Name = "tabPageStockInDetail", Text = "Activity - Stock-In Detail" };
        public TabPage tabPageStockOutList = new TabPage { Name = "tabPageStockOutList", Text = "Activity - Stock-Out List" };
        public TabPage tabPageStockOutDetail = new TabPage { Name = "tabPageStockOutDetail", Text = "Activity - Stock-Out Detail" };
        public TabPage tabPageStockCountList = new TabPage { Name = "tabPageStockCountList", Text = "Activity - Stock-Count List" };
        public TabPage tabPageStockCountDetail = new TabPage { Name = "tabPageStockCountDetail", Text = "Activity - Stock-Count Detail" };
        public TabPage tabPageDisbursementList = new TabPage { Name = "tabPageDisbursementList", Text = "Activity - Remittance List" };
        public TabPage tabPageDisbursementDetail = new TabPage { Name = "tabPageDisbursementDetail", Text = "Activity - Remittance Detail" };
        public TabPage tabPagePOSReport = new TabPage { Name = "tabPagePOSReport", Text = "Report - POS Report" };
        public TabPage tabPageSalesReports = new TabPage { Name = "tabPageSalesReports ", Text = "Report - Sales Report" };
        public TabPage tabPageInventoryReports = new TabPage { Name = "tabPageInventoryReports ", Text = "Report - Inventory Report" };
        public TabPage tabPageRemittanceReports = new TabPage { Name = "tabPageRemittanceReports ", Text = "Report - Remittance Report" };
        public TabPage tabPageSystemTables = new TabPage { Name = "tabPageSystemTables", Text = "System - System Tables" };
        public TabPage tabPageSystemUtilities = new TabPage { Name = "tabPageSystemUtilities", Text = "System - Utilities" };
        public TabPage tabPageSettings = new TabPage { Name = "tabPageSettings", Text = "Settings" };
        public TabPage tabPageKitchenDisplay = new TabPage { Name = "tabPageKitchenDisplay", Text = "Kitchen Display" };
        public TabPage tabPageDispatchStation = new TabPage { Name = "tabPageDispatchStation", Text = "Dispatch Station" };
        public TabPage tabPagePurchaseOrderList = new TabPage { Name = "tabPagePurchaseOrderList", Text = "Purchase Order List" };
        public TabPage tabPagePurchaseOrderDetail = new TabPage { Name = "tabPagePurchaseOrderDetail", Text = "Purchase Order Detail" };
        public TabPage tabPageCollectionList = new TabPage { Name = "tabPageCollectionList", Text = "Collection List" };
        public TabPage tabPageCollectionDetail = new TabPage { Name = "tabPageCollectionDetail", Text = "Collection Detail" };

        public MstItem.MstItemListForm mstItemListForm = null;
        public MstItem.MstItemDetailForm mstItemDetailForm = null;
        public MstCustomer.MstCustomerListForm mstCustomerListForm = null;
        public MstCustomer.MstCustomerDetailForm mstCustomerDetailForm = null;
        public MstDiscounting.MstDiscountingListForm mstDiscountingListForm = null;
        public MstDiscounting.MstDiscountingDetailForm mstDiscountingDetailForm = null;
        public MstUser.MstUserListForm mstUserListForm = null;
        public MstUser.MstUserDetailForm mstUserDetailForm = null;
        public MstItemGroup.MstItemGroupListForm mstItemGroupListForm = null;
        public MstItemGroup.MstItemGroupDetailForm mstItemGroupDetailForm = null;
        public MstTableGroup.MstTableGroupListForm mstTableGroupListForm = null;
        public MstTableGroup.MstTableGroupDetailForm mstTableGroupDetailForm = null;
        public TrnPOS.TrnPOSBarcodeForm trnSalesListForm = null;
        public TrnPOS.TrnPOSBarcodeDetailForm trnSalesDetailForm = null;
        public TrnPOS.TrnPOSTouchForm trnPOSTouchForm = null;
        public TrnPOS.TrnPOSTouchDetailForm trnPOSTouchDetailForm = null;
        public TrnStockIn.TrnStockInListForm trnStockInListForm = null;
        public TrnStockIn.TrnStockInDetailForm trnStockInDetailForm = null;
        public TrnStockOut.TrnStockOutListForm trnStockOutListForm = null;
        public TrnStockOut.TrnStockOutDetailForm trnStockOutDetailForm = null;
        public TrnStockCount.TrnStockCountListForm trnStockCountListForm = null;
        public TrnStockCount.TrnStockCountDetailForm trnStockCountDetailForm = null;
        public TrnDisbursement.TrnDisbursementListForm trnDisbursementListForm = null;
        public TrnDisbursement.TrnDisbursementDetailForm trnDisbursementDetailForm = null;
        public RepPOSReport.RepPOSReportForm repPOSReportForm = null;
        public RepSalesReport.RepSalesReportForm repSalesReportForm = null;
        public RepInventoryReport.RepInventoryForm repInventoryReportForm = null;
        public RepRemittanceReport.RepRemittanceForm repRemittanceReportForm = null;
        public SysUtilities.SysUtilitiesForm sysUtilitiesListForm = null;
        public SysSystemTables.SysSystemTablesForm sysSystemTablesForm = null;
        public SysSettings.SysSettingsForm sysSettingsForm = null;
        public SysKitchenDisplay.SysKitchenDisplayForm sysKitchenDisplayForm = null;
        public SysDispatchStation.SysDispatchStationForm sysDispatchStationForm = null;
        public TrnPurchaseOrder.TrnPurchaseOrderListForm trnPurchaseOrderListForm = null;
        public TrnPurchaseOrder.TrnPurchaseOrderDetailForm trnPurchaseOrderDetailForm = null;

        public TrnCollection.TrnCollectionListForm trnCollectionListForm = null;
        public TrnCollection.TrnCollectionDetailForm trnCollectionDetailForm = null;

        public void InitializeDefaultForm()
        {
            SysMenu.SysMenuForm sysMenuForm = new SysMenu.SysMenuForm(this)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageSysMenu.Controls.Add(sysMenuForm);
        }

        public void displayTimeStamp(String createdBy, String createdDate, String updatedBy, String updatedDate)
        {
            labelCreatedByUser.Text = createdBy;
            labelCreatedDate.Text = createdDate;
            labelUpdatedByUser.Text = updatedBy;
            labelUpdatedDate.Text = updatedDate;
        }

        public void AddTabPageRemittanceReports()
        {
            tabPageRemittanceReports.Controls.Remove(repRemittanceReportForm);

            repRemittanceReportForm = new RepRemittanceReport.RepRemittanceForm(this)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageRemittanceReports.Controls.Add(repRemittanceReportForm);

            if (tabControlSoftware.TabPages.Contains(tabPageRemittanceReports) == true)
            {
                tabControlSoftware.SelectTab(tabPageRemittanceReports);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageRemittanceReports);
                tabControlSoftware.SelectTab(tabPageRemittanceReports);
            }
        }

        public void AddTabPageItemList()
        {
            tabPageItemList.Controls.Remove(mstItemListForm);

            mstItemListForm = new MstItem.MstItemListForm(this)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageItemList.Controls.Add(mstItemListForm);

            if (tabControlSoftware.TabPages.Contains(tabPageItemList) == true)
            {
                tabControlSoftware.SelectTab(tabPageItemList);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageItemList);
                tabControlSoftware.SelectTab(tabPageItemList);
            }
        }

        public void AddTabPageItemDetail(MstItem.MstItemListForm itemListForm, Entities.MstItemEntity itemEntity)
        {
            tabPageItemDetail.Controls.Remove(mstItemDetailForm);

            mstItemDetailForm = new MstItem.MstItemDetailForm(this, itemListForm, itemEntity)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageItemDetail.Controls.Add(mstItemDetailForm);

            if (tabControlSoftware.TabPages.Contains(tabPageItemDetail) == true)
            {
                tabControlSoftware.SelectTab(tabPageItemDetail);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageItemDetail);
                tabControlSoftware.SelectTab(tabPageItemDetail);
            }
        }

        public void AddTabPageCustomerList()
        {
            tabPageCustomerList.Controls.Remove(mstCustomerListForm);

            mstCustomerListForm = new MstCustomer.MstCustomerListForm(this)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageCustomerList.Controls.Add(mstCustomerListForm);

            if (tabControlSoftware.TabPages.Contains(tabPageCustomerList) == true)
            {
                tabControlSoftware.SelectTab(tabPageCustomerList);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageCustomerList);
                tabControlSoftware.SelectTab(tabPageCustomerList);
            }
        }

        public void AddTabPageCustomerDetail(MstCustomer.MstCustomerListForm itemListForm, Entities.MstCustomerEntity itemEntity)
        {
            tabPageCustomerDetail.Controls.Remove(mstCustomerDetailForm);

            mstCustomerDetailForm = new MstCustomer.MstCustomerDetailForm(this, itemListForm, itemEntity)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageCustomerDetail.Controls.Add(mstCustomerDetailForm);

            if (tabControlSoftware.TabPages.Contains(tabPageCustomerDetail) == true)
            {
                tabControlSoftware.SelectTab(tabPageCustomerDetail);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageCustomerDetail);
                tabControlSoftware.SelectTab(tabPageCustomerDetail);
            }
        }

        public void AddTabPageDiscountingList()
        {
            tabPageDiscountingList.Controls.Remove(mstDiscountingListForm);

            mstDiscountingListForm = new MstDiscounting.MstDiscountingListForm(this)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageDiscountingList.Controls.Add(mstDiscountingListForm);

            if (tabControlSoftware.TabPages.Contains(tabPageDiscountingList) == true)
            {
                tabControlSoftware.SelectTab(tabPageDiscountingList);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageDiscountingList);
                tabControlSoftware.SelectTab(tabPageDiscountingList);
            }
        }

        public void AddTabPageDiscountingDetail(MstDiscounting.MstDiscountingListForm discountingListForm, Entities.MstDiscountEntity discountingEntity)
        {
            tabPageDiscountingDetail.Controls.Remove(mstDiscountingDetailForm);

            mstDiscountingDetailForm = new MstDiscounting.MstDiscountingDetailForm(this, discountingListForm, discountingEntity)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageDiscountingDetail.Controls.Add(mstDiscountingDetailForm);

            if (tabControlSoftware.TabPages.Contains(tabPageDiscountingDetail) == true)
            {
                tabControlSoftware.SelectTab(tabPageDiscountingDetail);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageDiscountingDetail);
                tabControlSoftware.SelectTab(tabPageDiscountingDetail);
            }
        }

        public void AddTabPageUserList()
        {
            tabPageUserList.Controls.Remove(mstUserListForm);

            mstUserListForm = new MstUser.MstUserListForm(this)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageUserList.Controls.Add(mstUserListForm);

            if (tabControlSoftware.TabPages.Contains(tabPageUserList) == true)
            {
                tabControlSoftware.SelectTab(tabPageUserList);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageUserList);
                tabControlSoftware.SelectTab(tabPageUserList);
            }
        }

        public void AddTabPageUserDetail(MstUser.MstUserListForm userListForm, Entities.MstUserEntity userEntity)
        {
            tabPageUserDetail.Controls.Remove(mstUserDetailForm);

            mstUserDetailForm = new MstUser.MstUserDetailForm(this, userListForm, userEntity)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageUserDetail.Controls.Add(mstUserDetailForm);

            if (tabControlSoftware.TabPages.Contains(tabPageUserDetail) == true)
            {
                tabControlSoftware.SelectTab(tabPageUserDetail);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageUserDetail);
                tabControlSoftware.SelectTab(tabPageUserDetail);
            }
        }

        public void AddTabPagePOSSalesList()
        {
            tabPagePOSBarcode.Controls.Remove(trnSalesListForm);

            trnSalesListForm = new TrnPOS.TrnPOSBarcodeForm(this)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPagePOSBarcode.Controls.Add(trnSalesListForm);

            if (tabControlSoftware.TabPages.Contains(tabPagePOSBarcode) == true)
            {
                tabControlSoftware.SelectTab(tabPagePOSBarcode);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPagePOSBarcode);
                tabControlSoftware.SelectTab(tabPagePOSBarcode);
            }
        }

        public void AddTabPagePOSSalesDetail(TrnPOS.TrnPOSBarcodeForm salesListForm, Entities.TrnSalesEntity salesEntity)
        {
            tabPagePOSBarcodeDetail.Controls.Remove(trnSalesDetailForm);

            trnSalesDetailForm = new TrnPOS.TrnPOSBarcodeDetailForm(this, salesListForm, salesEntity)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPagePOSBarcodeDetail.Controls.Add(trnSalesDetailForm);

            if (tabControlSoftware.TabPages.Contains(tabPagePOSBarcodeDetail) == true)
            {
                tabControlSoftware.SelectTab(tabPagePOSBarcodeDetail);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPagePOSBarcodeDetail);
                tabControlSoftware.SelectTab(tabPagePOSBarcodeDetail);
            }
        }

        public void AddTabPageStockInList()
        {
            tabPageStockInList.Controls.Remove(trnStockInListForm);

            trnStockInListForm = new TrnStockIn.TrnStockInListForm(this)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageStockInList.Controls.Add(trnStockInListForm);

            if (tabControlSoftware.TabPages.Contains(tabPageStockInList) == true)
            {
                tabControlSoftware.SelectTab(tabPageStockInList);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageStockInList);
                tabControlSoftware.SelectTab(tabPageStockInList);
            }
        }

        public void AddTabPageStockInDetail(TrnStockIn.TrnStockInListForm stockInListForm, Entities.TrnStockInEntity stockInEntity)
        {
            tabPageStockInDetail.Controls.Remove(trnStockInDetailForm);

            trnStockInDetailForm = new TrnStockIn.TrnStockInDetailForm(this, stockInListForm, stockInEntity)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageStockInDetail.Controls.Add(trnStockInDetailForm);

            if (tabControlSoftware.TabPages.Contains(tabPageStockInDetail) == true)
            {
                tabControlSoftware.SelectTab(tabPageStockInDetail);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageStockInDetail);
                tabControlSoftware.SelectTab(tabPageStockInDetail);
            }
        }

        public void AddTabPageStockOutList()
        {
            tabPageStockOutList.Controls.Remove(trnStockOutListForm);

            trnStockOutListForm = new TrnStockOut.TrnStockOutListForm(this)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageStockOutList.Controls.Add(trnStockOutListForm);

            if (tabControlSoftware.TabPages.Contains(tabPageStockOutList) == true)
            {
                tabControlSoftware.SelectTab(tabPageStockOutList);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageStockOutList);
                tabControlSoftware.SelectTab(tabPageStockOutList);
            }
        }

        public void AddTabPageStockOutDetail(TrnStockOut.TrnStockOutListForm stockOutListForm, Entities.TrnStockOutEntity stockOutEntity)
        {
            tabPageStockOutDetail.Controls.Remove(trnStockOutDetailForm);

            trnStockOutDetailForm = new TrnStockOut.TrnStockOutDetailForm(this, stockOutListForm, stockOutEntity)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageStockOutDetail.Controls.Add(trnStockOutDetailForm);

            if (tabControlSoftware.TabPages.Contains(tabPageStockOutDetail) == true)
            {
                tabControlSoftware.SelectTab(tabPageStockOutDetail);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageStockOutDetail);
                tabControlSoftware.SelectTab(tabPageStockOutDetail);
            }
        }

        public void AddTabPageStockCountList()
        {
            tabPageStockCountList.Controls.Remove(trnStockCountListForm);

            trnStockCountListForm = new TrnStockCount.TrnStockCountListForm(this)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageStockCountList.Controls.Add(trnStockCountListForm);

            if (tabControlSoftware.TabPages.Contains(tabPageStockCountList) == true)
            {
                tabControlSoftware.SelectTab(tabPageStockCountList);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageStockCountList);
                tabControlSoftware.SelectTab(tabPageStockCountList);
            }
        }

        public void AddTabPageStockCountDetail(TrnStockCount.TrnStockCountListForm stockCountListForm, Entities.TrnStockCountEntity stockCountEntity)
        {
            tabPageStockCountDetail.Controls.Remove(trnStockCountDetailForm);

            trnStockCountDetailForm = new TrnStockCount.TrnStockCountDetailForm(this, stockCountListForm, stockCountEntity)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageStockCountDetail.Controls.Add(trnStockCountDetailForm);

            if (tabControlSoftware.TabPages.Contains(tabPageStockCountDetail) == true)
            {
                tabControlSoftware.SelectTab(tabPageStockCountDetail);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageStockCountDetail);
                tabControlSoftware.SelectTab(tabPageStockCountDetail);
            }
        }

        public void AddTabPageDisbursementList()
        {
            tabPageDisbursementList.Controls.Remove(trnDisbursementListForm);

            trnDisbursementListForm = new TrnDisbursement.TrnDisbursementListForm(this)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageDisbursementList.Controls.Add(trnDisbursementListForm);

            if (tabControlSoftware.TabPages.Contains(tabPageDisbursementList) == true)
            {
                tabControlSoftware.SelectTab(tabPageDisbursementList);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageDisbursementList);
                tabControlSoftware.SelectTab(tabPageDisbursementList);
            }
        }

        public void AddTabPageDisbursementDetail(TrnDisbursement.TrnDisbursementListForm disbursementListForm, Entities.TrnDisbursementEntity disbursementEntity)
        {
            tabPageDisbursementDetail.Controls.Remove(trnDisbursementDetailForm);

            trnDisbursementDetailForm = new TrnDisbursement.TrnDisbursementDetailForm(this, disbursementListForm, disbursementEntity)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageDisbursementDetail.Controls.Add(trnDisbursementDetailForm);

            if (tabControlSoftware.TabPages.Contains(tabPageDisbursementDetail) == true)
            {
                tabControlSoftware.SelectTab(tabPageDisbursementDetail);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageDisbursementDetail);
                tabControlSoftware.SelectTab(tabPageDisbursementDetail);
            }
        }

        public void AddTabPagePOSReport()
        {
            tabPagePOSReport.Controls.Remove(repPOSReportForm);

            repPOSReportForm = new RepPOSReport.RepPOSReportForm(this)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPagePOSReport.Controls.Add(repPOSReportForm);

            if (tabControlSoftware.TabPages.Contains(tabPagePOSReport) == true)
            {
                tabControlSoftware.SelectTab(tabPagePOSReport);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPagePOSReport);
                tabControlSoftware.SelectTab(tabPagePOSReport);
            }
        }

        public void AddTabPageInventoryReports()
        {
            tabPageInventoryReports.Controls.Remove(repInventoryReportForm);

            repInventoryReportForm = new RepInventoryReport.RepInventoryForm(this)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageInventoryReports.Controls.Add(repInventoryReportForm);

            if (tabControlSoftware.TabPages.Contains(tabPageInventoryReports) == true)
            {
                tabControlSoftware.SelectTab(tabPageInventoryReports);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageInventoryReports);
                tabControlSoftware.SelectTab(tabPageInventoryReports);
            }
        }

        public void AddTabPageSystemTables()
        {
            tabPageSystemTables.Controls.Remove(sysSystemTablesForm);

            sysSystemTablesForm = new SysSystemTables.SysSystemTablesForm(this)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageSystemTables.Controls.Add(sysSystemTablesForm);

            if (tabControlSoftware.TabPages.Contains(tabPageSystemTables) == true)
            {
                tabControlSoftware.SelectTab(tabPageSystemTables);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageSystemTables);
                tabControlSoftware.SelectTab(tabPageSystemTables);
            }
        }

        public void AddTabPageSettings()
        {
            tabPageSettings.Controls.Remove(sysSettingsForm);

            if (sysSettingsForm == null)
            {
                sysSettingsForm = new SysSettings.SysSettingsForm(this)
                {
                    TopLevel = false,
                    Visible = true,
                    Dock = DockStyle.Fill
                };
            }

            tabPageSettings.Controls.Add(sysSettingsForm);

            if (tabControlSoftware.TabPages.Contains(tabPageSettings) == true)
            {
                tabControlSoftware.SelectTab(tabPageSettings);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageSettings);
                tabControlSoftware.SelectTab(tabPageSettings);
            }
        }

        public void AddTabPageUtilities()
        {
            tabPageSystemUtilities.Controls.Remove(sysUtilitiesListForm);

            sysUtilitiesListForm = new SysUtilities.SysUtilitiesForm(this, null)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageSystemUtilities.Controls.Add(sysUtilitiesListForm);

            if (tabControlSoftware.TabPages.Contains(tabPageSystemUtilities) == true)
            {
                tabControlSoftware.SelectTab(tabPageSystemUtilities);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageSystemUtilities);
                tabControlSoftware.SelectTab(tabPageSystemUtilities);
            }
        }

        public void AddTabPageSalesReport()
        {
            tabPageSalesReports.Controls.Remove(repSalesReportForm);

            repSalesReportForm = new RepSalesReport.RepSalesReportForm(this)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageSalesReports.Controls.Add(repSalesReportForm);

            if (tabControlSoftware.TabPages.Contains(tabPageSalesReports) == true)
            {
                tabControlSoftware.SelectTab(tabPageSalesReports);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageSalesReports);
                tabControlSoftware.SelectTab(tabPageSalesReports);
            }
        }

        public void AddTabPagePOSTouchSalesList()
        {
            tabPagePOSTouch.Controls.Remove(trnPOSTouchForm);

            trnPOSTouchForm = new TrnPOS.TrnPOSTouchForm(this)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPagePOSTouch.Controls.Add(trnPOSTouchForm);

            if (tabControlSoftware.TabPages.Contains(tabPagePOSTouch) == true)
            {
                tabControlSoftware.SelectTab(tabPagePOSTouch);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPagePOSTouch);
                tabControlSoftware.SelectTab(tabPagePOSTouch);
            }
        }

        public void AddTabPagePOSTouchSalesDetail(TrnPOS.TrnPOSTouchForm POSTouchForm, Entities.TrnSalesEntity salesEntity)
        {
            tabPagePOSTouchDetail.Controls.Remove(trnPOSTouchDetailForm);

            trnPOSTouchDetailForm = new TrnPOS.TrnPOSTouchDetailForm(this, POSTouchForm, salesEntity)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPagePOSTouchDetail.Controls.Add(trnPOSTouchDetailForm);

            if (tabControlSoftware.TabPages.Contains(tabPagePOSTouchDetail) == true)
            {
                tabControlSoftware.SelectTab(tabPagePOSTouchDetail);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPagePOSTouchDetail);
                tabControlSoftware.SelectTab(tabPagePOSTouchDetail);
            }
        }

        public void AddTabPageItemGroupList()
        {
            tabPageItemGroupList.Controls.Remove(mstItemGroupListForm);

            mstItemGroupListForm = new MstItemGroup.MstItemGroupListForm(this)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageItemGroupList.Controls.Add(mstItemGroupListForm);

            if (tabControlSoftware.TabPages.Contains(tabPageItemGroupList) == true)
            {
                tabControlSoftware.SelectTab(tabPageItemGroupList);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageItemGroupList);
                tabControlSoftware.SelectTab(tabPageItemGroupList);
            }
        }

        public void AddTabPageItemGroupDetail(MstItemGroup.MstItemGroupListForm discountingListForm, Entities.MstItemGroupEntity itemGroupEntity)
        {
            tabPageItemGroupDetail.Controls.Remove(mstItemGroupDetailForm);

            mstItemGroupDetailForm = new MstItemGroup.MstItemGroupDetailForm(this, discountingListForm, itemGroupEntity)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageItemGroupDetail.Controls.Add(mstItemGroupDetailForm);

            if (tabControlSoftware.TabPages.Contains(tabPageItemGroupDetail) == true)
            {
                tabControlSoftware.SelectTab(tabPageItemGroupDetail);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageItemGroupDetail);
                tabControlSoftware.SelectTab(tabPageItemGroupDetail);
            }
        }

        public void AddTabPageTableGroupList()
        {
            tabPageTableGroupList.Controls.Remove(mstTableGroupListForm);

            mstTableGroupListForm = new MstTableGroup.MstTableGroupListForm(this)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageTableGroupList.Controls.Add(mstTableGroupListForm);

            if (tabControlSoftware.TabPages.Contains(tabPageTableGroupList) == true)
            {
                tabControlSoftware.SelectTab(tabPageTableGroupList);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageTableGroupList);
                tabControlSoftware.SelectTab(tabPageTableGroupList);
            }
        }

        public void AddTabPageTableGroupDetail(MstTableGroup.MstTableGroupListForm discountingListForm, Entities.MstTableGroupEntity tableGroupEntity)
        {
            tabPageTableGroupDetail.Controls.Remove(mstTableGroupDetailForm);

            mstTableGroupDetailForm = new MstTableGroup.MstTableGroupDetailForm(this, discountingListForm, tableGroupEntity)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageTableGroupDetail.Controls.Add(mstTableGroupDetailForm);

            if (tabControlSoftware.TabPages.Contains(tabPageTableGroupDetail) == true)
            {
                tabControlSoftware.SelectTab(tabPageTableGroupDetail);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageTableGroupDetail);
                tabControlSoftware.SelectTab(tabPageTableGroupDetail);
            }
        }

        public void AddTabPageKitchenDisplay()
        {
            tabPageKitchenDisplay.Controls.Remove(sysKitchenDisplayForm);

            sysKitchenDisplayForm = new SysKitchenDisplay.SysKitchenDisplayForm(this)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageKitchenDisplay.Controls.Add(sysKitchenDisplayForm);

            if (tabControlSoftware.TabPages.Contains(tabPageKitchenDisplay) == true)
            {
                tabControlSoftware.SelectTab(tabPageKitchenDisplay);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageKitchenDisplay);
                tabControlSoftware.SelectTab(tabPageKitchenDisplay);
            }
        }

        public void AddTabPageDispatchStation()
        {
            tabPageDispatchStation.Controls.Remove(sysDispatchStationForm);

            sysDispatchStationForm = new SysDispatchStation.SysDispatchStationForm(this)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageDispatchStation.Controls.Add(sysDispatchStationForm);

            if (tabControlSoftware.TabPages.Contains(tabPageDispatchStation) == true)
            {
                tabControlSoftware.SelectTab(tabPageDispatchStation);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageDispatchStation);
                tabControlSoftware.SelectTab(tabPageDispatchStation);
            }
        }

        public void AddTabPagePurchaseOrderList()
        {
            tabPagePurchaseOrderList.Controls.Remove(trnPurchaseOrderListForm);

            trnPurchaseOrderListForm = new TrnPurchaseOrder.TrnPurchaseOrderListForm(this)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPagePurchaseOrderList.Controls.Add(trnPurchaseOrderListForm);

            if (tabControlSoftware.TabPages.Contains(tabPagePurchaseOrderList) == true)
            {
                tabControlSoftware.SelectTab(tabPagePurchaseOrderList);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPagePurchaseOrderList);
                tabControlSoftware.SelectTab(tabPagePurchaseOrderList);
            }
        }
        public void AddTabPagePurchaseOrderDetail(TrnPurchaseOrder.TrnPurchaseOrderListForm purchaseOrderListForm, Entities.TrnPurchaseOrderEntity purchaseOrderEntity)
        {
            tabPagePurchaseOrderDetail.Controls.Remove(trnPurchaseOrderDetailForm);

            trnPurchaseOrderDetailForm = new TrnPurchaseOrder.TrnPurchaseOrderDetailForm(this, purchaseOrderListForm, purchaseOrderEntity)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPagePurchaseOrderDetail.Controls.Add(trnPurchaseOrderDetailForm);

            if (tabControlSoftware.TabPages.Contains(tabPagePurchaseOrderDetail) == true)
            {
                tabControlSoftware.SelectTab(tabPagePurchaseOrderDetail);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPagePurchaseOrderDetail);
                tabControlSoftware.SelectTab(tabPagePurchaseOrderDetail);
            }
        }
        public void AddTabPageCollectionList()
        {
            tabPageCollectionList.Controls.Remove(trnCollectionListForm);

            trnCollectionListForm = new TrnCollection.TrnCollectionListForm(this)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageCollectionList.Controls.Add(trnCollectionListForm);

            if (tabControlSoftware.TabPages.Contains(tabPageCollectionList) == true)
            {
                tabControlSoftware.SelectTab(tabPageCollectionList);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageCollectionList);
                tabControlSoftware.SelectTab(tabPageCollectionList);
            }
        }
        public void AddTabPageCollectionDetail(TrnCollection.TrnCollectionListForm collectionListForm, Entities.TrnCollectionEntity collectionEntity)
        {
            tabPageCollectionDetail.Controls.Remove(trnCollectionDetailForm);

            trnCollectionDetailForm = new TrnCollection.TrnCollectionDetailForm(this, collectionListForm, collectionEntity)
            {
                TopLevel = false,
                Visible = true,
                Dock = DockStyle.Fill
            };

            tabPageCollectionDetail.Controls.Add(trnCollectionDetailForm);

            if (tabControlSoftware.TabPages.Contains(tabPageCollectionDetail) == true)
            {
                tabControlSoftware.SelectTab(tabPageCollectionDetail);
            }
            else
            {
                tabControlSoftware.TabPages.Add(tabPageCollectionDetail);
                tabControlSoftware.SelectTab(tabPageCollectionDetail);
            }
        }

        public void RemoveTabPage()
        {
            tabControlSoftware.TabPages.Remove(tabControlSoftware.SelectedTab);
            tabControlSoftware.SelectTab(tabControlSoftware.TabPages.Count - 1);
        }

        private void SysSoftwareForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Account.SysLogin.SysLoginForm sysLogin = new Account.SysLogin.SysLoginForm(null, null, null, null, false); ;
            sysLogin.Show();
        }

        private void tabControlSoftware_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlSoftware.SelectedTab == tabPageCustomerList)
            {
                mstCustomerListForm.Focus();
            }

            if (tabControlSoftware.SelectedTab == tabPageCustomerDetail)
            {
                mstCustomerDetailForm.Focus();
            }
        }

        private void buttonOpenSidebarMenu_Click(object sender, EventArgs e)
        {
            if (panelSidebarMenu.Visible == false)
            {
                panelSidebarMenu.Visible = true;
            }
            else
            {
                panelSidebarMenu.Visible = false;
            }
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlSoftware.SelectTab(tabPageSysMenu);
        }

        private void itemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTabPageItemList();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTabPageCustomerList();
        }

        private void discountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTabPageDiscountingList();
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTabPageUserList();
        }

        private void pOSToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddTabPagePOSSalesList();
        }

        private void pOSTouchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTabPagePOSTouchSalesList();
        }

        private void remittanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTabPageDisbursementList();
        }

        private void stockInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTabPageStockInList();
        }

        private void stockOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTabPageStockOutList();
        }

        private void stockCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTabPageStockCountList();
        }

        private void systemTablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTabPageSystemTables();
        }

        private void utilitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTabPageUtilities();
        }

        private void pOSReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTabPagePOSReport();
        }

        private void salesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTabPageSalesReport();
        }

        private void remittanceReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTabPageRemittanceReports();
        }

        private void inventoryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTabPageInventoryReports();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTabPageSettings();
        }

        private void toolStripMenuItemTabeGroup_Click(object sender, EventArgs e)
        {
            AddTabPageTableGroupList();
        }

        private void toolStripMenuItemItemGroup_Click(object sender, EventArgs e)
        {
            AddTabPageItemGroupList();
        }

        private void toolStripMenuItemKitchenDisplay_Click(object sender, EventArgs e)
        {
            AddTabPageKitchenDisplay();
        }

        private void toolStripMenuItemDispatchStation_Click(object sender, EventArgs e)
        {
            AddTabPageDispatchStation();
        }

        private void purchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTabPagePurchaseOrderList();
        }

        private void collectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddTabPageCollectionList();
        }
    }
}
