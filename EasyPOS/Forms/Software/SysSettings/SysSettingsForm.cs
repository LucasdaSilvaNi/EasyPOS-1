using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.SysSettings
{
    public partial class SysSettingsForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;


        public Boolean isIntegrationStarted = false;
        public Int32 logMessageCount = 0;

        public String logLocation = "";

        public static Int32 pageSize = 50;

        public static List<Entities.DgvSysSettingsKitchenEntity> kitchenData = new List<Entities.DgvSysSettingsKitchenEntity>();
        public PagedList<Entities.DgvSysSettingsKitchenEntity> kitchenPageList = new PagedList<Entities.DgvSysSettingsKitchenEntity>(kitchenData, kitchenPageNumber, pageSize);
        public BindingSource kitchenDataSource = new BindingSource();
        public static Int32 kitchenPageNumber = 1;

        public SysSettingsForm(SysSoftwareForm softwareForm)
        {
            InitializeComponent();
            sysSoftwareForm = softwareForm;
            sysUserRights = new Modules.SysUserRightsModule("SysSettings");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                getSettings();
                GetComboBoxDropDownList();
                CreateKitchenDataGridView();
            }

        }

        public void getSettings()
        {
            txtLogs.Text = "Press start button to integrate. \r\n\n\r\n\n";

            isIntegrationStarted = false;

            btnStartIntegration.Enabled = true;
            btnStopIntegration.Enabled = false;

            dtpIntegrationDate.Enabled = true;

            Controllers.IntCloudSettingsController intCloudSettingsController = new Controllers.IntCloudSettingsController();
            var cloudSettings = intCloudSettingsController.DetailCloudSettings();
            if (cloudSettings != null)
            {
                txtDomain.Text = cloudSettings.Domain;
                txtBranchCode.Text = cloudSettings.BranchCode;
                txtUserCode.Text = cloudSettings.UserCode;
                cbxUseItemPrice.Checked = cloudSettings.UseItemPrice;
                logLocation = cloudSettings.LogFileLocation;
            }
        }

        private void backgroundWorkerEasyfisIntegration_DoWork(object sender, DoWorkEventArgs e)
        {
            Controllers.IntCloudSettingsController intCloudSettingsController = new Controllers.IntCloudSettingsController();

            while (isIntegrationStarted)
            {
                String apiUrlHost = txtDomain.Text;

                var cloudSettings = intCloudSettingsController.DetailCloudSettings();
                if (cloudSettings != null)
                {
                    var branchCode = cloudSettings.BranchCode;
                    var userCode = cloudSettings.UserCode;
                    var useItemPrice = cloudSettings.UseItemPrice;

                    if (cloudSettings.Application == "Easyfis")
                    {
                        Task taskCustomer = Task.Run(() =>
                        {
                            EasyFISIntegration.Controllers.EasyPOSMstCustomerController objMstCustomer = new EasyFISIntegration.Controllers.EasyPOSMstCustomerController(this, dtpIntegrationDate.Text);
                            objMstCustomer.SyncCustomer(apiUrlHost);
                        });
                        taskCustomer.Wait();

                        if (taskCustomer.IsCompleted)
                        {
                            Task taskSupplier = Task.Run(() =>
                            {
                                EasyFISIntegration.Controllers.EasyPOSMstSupplierController objMstSupplier = new EasyFISIntegration.Controllers.EasyPOSMstSupplierController(this, dtpIntegrationDate.Text);
                                objMstSupplier.SyncSupplier(apiUrlHost);
                            });
                            taskSupplier.Wait();

                            if (taskSupplier.IsCompleted)
                            {
                                Task taskItem = Task.Run(() =>
                                {
                                    EasyFISIntegration.Controllers.EasyPOSMstItemController objMstItem = new EasyFISIntegration.Controllers.EasyPOSMstItemController(this, dtpIntegrationDate.Text);
                                    objMstItem.SyncItem(apiUrlHost);
                                });
                                taskItem.Wait();

                                if (taskItem.IsCompleted)
                                {
                                    Task taskReceivingReceipt = Task.Run(() =>
                                    {
                                        EasyFISIntegration.Controllers.EasyPOSTrnReceivingReceiptController objTrnReceivingReceipt = new EasyFISIntegration.Controllers.EasyPOSTrnReceivingReceiptController(this, dtpIntegrationDate.Text);
                                        objTrnReceivingReceipt.SyncReceivingReceipt(apiUrlHost, branchCode);
                                    });
                                    taskReceivingReceipt.Wait();

                                    if (taskReceivingReceipt.IsCompleted)
                                    {
                                        Task taskStockIn = Task.Run(() =>
                                        {
                                            EasyFISIntegration.Controllers.EasyPOSTrnStockInController objTrnStockIn = new EasyFISIntegration.Controllers.EasyPOSTrnStockInController(this, dtpIntegrationDate.Text);
                                            objTrnStockIn.SyncStockIn(apiUrlHost, branchCode);
                                        });
                                        taskStockIn.Wait();

                                        if (taskStockIn.IsCompleted)
                                        {
                                            Task taskStockOut = Task.Run(() =>
                                            {
                                                EasyFISIntegration.Controllers.EasyPOSTrnStockOutController objTrnStockOut = new EasyFISIntegration.Controllers.EasyPOSTrnStockOutController(this, dtpIntegrationDate.Text);
                                                objTrnStockOut.SyncStockOut(apiUrlHost, branchCode);

                                            });
                                            taskStockOut.Wait();

                                            if (taskStockOut.IsCompleted)
                                            {
                                                Task taskStockTransferIn = Task.Run(() =>
                                                {
                                                    EasyFISIntegration.Controllers.EasyPOSTrnStockTransferInController objTrnStockTransferIn = new EasyFISIntegration.Controllers.EasyPOSTrnStockTransferInController(this, dtpIntegrationDate.Text);
                                                    objTrnStockTransferIn.SyncStockTransferIN(apiUrlHost, branchCode);

                                                });
                                                taskStockTransferIn.Wait();

                                                if (taskStockTransferIn.IsCompleted)
                                                {
                                                    Task taskStockTransferOut = Task.Run(() =>
                                                    {
                                                        EasyFISIntegration.Controllers.EasyPOSTrnStockTransferOutController objTrnStockTransferOut = new EasyFISIntegration.Controllers.EasyPOSTrnStockTransferOutController(this, dtpIntegrationDate.Text);
                                                        objTrnStockTransferOut.SyncStockTransferOT(apiUrlHost, branchCode);
                                                    });
                                                    taskStockTransferOut.Wait();

                                                    if (taskStockTransferOut.IsCompleted)
                                                    {
                                                        Task taskSalesOrder = Task.Run(() =>
                                                        {
                                                            EasyFISIntegration.Controllers.EasyPOSTrnSalesOrderController objTrnSalesOrder = new EasyFISIntegration.Controllers.EasyPOSTrnSalesOrderController(this, dtpIntegrationDate.Text);
                                                            objTrnSalesOrder.SyncSalesOrder(apiUrlHost, branchCode);
                                                        });
                                                        taskSalesOrder.Wait();

                                                        if (taskSalesOrder.IsCompleted)
                                                        {
                                                            Task taskCollection = Task.Run(() =>
                                                            {
                                                                EasyFISIntegration.Controllers.EasyPOSTrnSalesController objTrnCollection = new EasyFISIntegration.Controllers.EasyPOSTrnSalesController(this);
                                                                objTrnCollection.SyncSales(apiUrlHost, branchCode, userCode);
                                                            });
                                                            taskCollection.Wait();

                                                            if (taskCollection.IsCompleted)
                                                            {
                                                                Task taskSalesReturn = Task.Run(() =>
                                                                {
                                                                    EasyFISIntegration.Controllers.EasyPOSTrnSalesReturnController objTrnSalesReturn = new EasyFISIntegration.Controllers.EasyPOSTrnSalesReturnController(this);
                                                                    objTrnSalesReturn.SyncSalesReturn(apiUrlHost, branchCode, userCode);
                                                                });
                                                                taskSalesReturn.Wait();

                                                                if (useItemPrice)
                                                                {
                                                                    if (taskSalesReturn.IsCompleted)
                                                                    {
                                                                        Task taskItemPrice = Task.Run(() =>
                                                                        {
                                                                            EasyFISIntegration.Controllers.EasyPOSTrnItemPriceController objTrnItemPrice = new EasyFISIntegration.Controllers.EasyPOSTrnItemPriceController(this, dtpIntegrationDate.Text);
                                                                            objTrnItemPrice.SyncItemPrice(apiUrlHost, branchCode);

                                                                        });
                                                                        taskItemPrice.Wait();
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (cloudSettings.Application == "Liteclerk")
                    {
                        Task taskLiteclerkCustomer = Task.Run(() =>
                        {
                            LiteclerkIntegration.Controllers.LiteclerkMstArticleCustomerController objLiteclerkCustomer = new LiteclerkIntegration.Controllers.LiteclerkMstArticleCustomerController(this, dtpIntegrationDate.Text);
                            objLiteclerkCustomer.SyncCustomer(apiUrlHost);
                        });
                        taskLiteclerkCustomer.Wait();

                        if (taskLiteclerkCustomer.IsCompleted)
                        {
                            Task taskLiteclerkSupplier = Task.Run(() =>
                            {
                                LiteclerkIntegration.Controllers.LiteclerkMstArticleSupplierController objLiteclerkSupplier = new LiteclerkIntegration.Controllers.LiteclerkMstArticleSupplierController(this, dtpIntegrationDate.Text);
                                objLiteclerkSupplier.SyncSupplier(apiUrlHost);
                            });
                            taskLiteclerkSupplier.Wait();

                            if (taskLiteclerkSupplier.IsCompleted)
                            {
                                Task taskLiteclerkItem = Task.Run(() =>
                                {
                                    LiteclerkIntegration.Controllers.LiteclerkMstArticleItemController objLiteclerkItem = new LiteclerkIntegration.Controllers.LiteclerkMstArticleItemController(this, dtpIntegrationDate.Text);
                                    objLiteclerkItem.SyncItem(apiUrlHost);
                                });
                                taskLiteclerkItem.Wait();

                                if (taskLiteclerkItem.IsCompleted)
                                {
                                    Task taskLiteclerkStockIn = Task.Run(() =>
                                    {
                                        LiteclerkIntegration.Controllers.LiteclerkTrnStockInController objLiteclerkStockIn = new LiteclerkIntegration.Controllers.LiteclerkTrnStockInController(this, dtpIntegrationDate.Text);
                                        objLiteclerkStockIn.SyncStockIn(apiUrlHost, branchCode);
                                    });
                                    taskLiteclerkStockIn.Wait();

                                    if (taskLiteclerkStockIn.IsCompleted)
                                    {
                                        Task taskLiteclerkSalesOrder = Task.Run(() =>
                                        {
                                            LiteclerkIntegration.Controllers.LiteclerkTrnSalesOrderController objLiteclerkSalesOrder = new LiteclerkIntegration.Controllers.LiteclerkTrnSalesOrderController(this, dtpIntegrationDate.Text);
                                            objLiteclerkSalesOrder.SyncSalesOrder(apiUrlHost, branchCode);
                                        });
                                        taskLiteclerkSalesOrder.Wait();

                                        if (taskLiteclerkSalesOrder.IsCompleted)
                                        {
                                            Task taskLiteclerkSales = Task.Run(() =>
                                            {
                                                LiteclerkIntegration.Controllers.LiteclerkTrnPointOfSaleController objLiteclerkSales = new LiteclerkIntegration.Controllers.LiteclerkTrnPointOfSaleController(this);
                                                objLiteclerkSales.SyncSales(apiUrlHost, branchCode, userCode);
                                            });
                                            taskLiteclerkSales.Wait();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                System.Threading.Thread.Sleep(5000);
            }
        }

        private void btnStartIntegration_Click(object sender, EventArgs e)
        {
            if (buttonLock.Enabled == true)
            {
                MessageBox.Show("Please lock the settings first.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                isIntegrationStarted = true;

                btnStartIntegration.Enabled = false;
                btnStopIntegration.Enabled = true;

                dtpIntegrationDate.Enabled = false;

                logMessages("Started! \r\n\nTime Stamp: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "\r\n\n\r\n\n");

                if (backgroundWorkerEasyfisIntegration.IsBusy != true)
                {
                    backgroundWorkerEasyfisIntegration.RunWorkerAsync();
                }
            }
        }

        private void btnStopIntegration_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to stop integration?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                isIntegrationStarted = false;

                btnStartIntegration.Enabled = true;
                btnStopIntegration.Enabled = false;

                dtpIntegrationDate.Enabled = true;

                logMessages("Stopped! \r\n\nTime Stamp: " + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt") + "\r\n\n\r\n\n");
            }
        }

        public void logMessages(String message)
        {
            BeginInvoke((MethodInvoker)delegate
            {
                String displayMessage = message;

                logMessageCount++;
                if (logMessageCount >= 1000)
                {
                    logMessageCount = 0;

                    if (!txtLogs.Text.Equals(""))
                    {
                        File.WriteAllText(logLocation + "\\" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".txt", txtLogs.Text);
                        txtLogs.Text = "";
                    }
                }

                txtLogs.Text += displayMessage;

                txtLogs.Focus();
                txtLogs.SelectionStart = txtLogs.Text.Length;
                txtLogs.ScrollToCaret();
            });
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (isIntegrationStarted == true)
            {
                MessageBox.Show("Please stop the integration first.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                sysSoftwareForm.RemoveTabPage();
            }
        }

        // ==========
        // SysCurrent 
        // ==========
        public void GetComboBoxDropDownList()
        {
            //if (sysUserRights.GetUserRights().CanLock == false)
            //{
            //    buttonLock.Enabled = false;
            //}
            //else
            //{
            //    buttonLock.Enabled = true;
            //}

            //if (sysUserRights.GetUserRights().CanUnlock == false)
            //{
            //    buttonUnlock.Enabled = false;
            //}
            //else
            //{
            //    buttonUnlock.Enabled = true;
            //}

            Controllers.SysCurrentController sysSettingsController = new Controllers.SysCurrentController();

            var terminals = sysSettingsController.DropDownTerminalList();
            if (terminals.Any())
            {

                comboBoxTerminal.DataSource = terminals;
                comboBoxTerminal.ValueMember = "Id";
                comboBoxTerminal.DisplayMember = "Terminal";
            }

            var periods = sysSettingsController.DropDownPeriodList();
            if (periods.Any())
            {
                comboBoxCurrentPeriod.DataSource = periods;
                comboBoxCurrentPeriod.ValueMember = "Id";
                comboBoxCurrentPeriod.DisplayMember = "Period";
            }

            var customers = sysSettingsController.DropDownCustomerList();
            if (customers.Any())
            {
                comboBoxWalkinCustomer.DataSource = customers;
                comboBoxWalkinCustomer.ValueMember = "Id";
                comboBoxWalkinCustomer.DisplayMember = "Customer";
            }

            var discounts = sysSettingsController.DropDownDiscountList();
            if (discounts.Any())
            {
                comboBoxDefaultDiscount.DataSource = discounts;
                comboBoxDefaultDiscount.ValueMember = "Id";
                comboBoxDefaultDiscount.DisplayMember = "Discount";
            }

            var supplier = sysSettingsController.DropDownSupplierList();
            if (supplier.Any())
            {
                comboBoxReturnSupplier.DataSource = supplier;
                comboBoxReturnSupplier.ValueMember = "Id";
                comboBoxReturnSupplier.DisplayMember = "Supplier";
            }

            //var language = sysSettingsController.DropDownLanguageList();
            //if (language.Any())
            //{
            //    comboBoxLanguage.DataSource = language;
            //    comboBoxLanguage.ValueMember = "Id";
            //    comboBoxLanguage.DisplayMember = "Language";
            //}

            List<String> language = new List<String>
            {
                "English",
                "Chinese"
            };

            comboBoxLanguage.DataSource = language;

            List<String> collectionReports = new List<String>
            {
                "Official Receipt",
                "Delivery Receipt",
                "Withdrawal Slip"
            };

            comboBoxCollectionReport.DataSource = collectionReports;

            List<String> posTypes = new List<String>
            {
                "POS Barcode",
                "POS Touch"
            };

            comboBoxPOSType.DataSource = posTypes;

            List<String> printerType = new List<String>
            {
                "Dot Matrix Printer",
                "Thermal Printer",
                "58mm Printer"
            };

            comboBoxPrinterType.DataSource = printerType;

            List<String> salesOrderPrinterType = new List<String>
            {
                "Default Printer",
                "Label Printer",
                "Letter Printer",
                "Kitchen Printer"
            };

            comboBoxSalesOrderPrinterType.DataSource = salesOrderPrinterType;

            getSysCurrentDetail();
        }

        public void getSysCurrentDetail()
        {
            Controllers.SysCurrentController sysSettingsController = new Controllers.SysCurrentController();
            var sysCurrent = sysSettingsController.GetSysCurrent();

            if (sysCurrent != null)
            {
                textBoxCompanyName.Text = sysCurrent.CompanyName;
                textBoxAddress.Text = sysCurrent.Address;
                textBoxContactNo.Text = sysCurrent.ContactNo;
                textBoxTIN.Text = sysCurrent.TIN;
                textBoxAccreditationNo.Text = sysCurrent.AccreditationNo;
                textBoxSerialNo.Text = sysCurrent.SerialNo;
                textBoxPermitNo.Text = sysCurrent.PermitNo;
                textBoxMachineNo.Text = sysCurrent.MachineNo;
                textBoxReceiptFooter.Text = sysCurrent.ReceiptFooter;
                textBoxInvoiceFooter.Text = sysCurrent.InvoiceFooter;
                textBoxLicenseCode.Text = sysCurrent.LicenseCode;
                textBoxTenantOf.Text = sysCurrent.TenantOf;
                textBoxCurrentVersion.Text = sysCurrent.CurrentVersion;
                textBoxCurrentDeveloper.Text = sysCurrent.CurrentDeveloper;
                textBoxCurrentSupport.Text = sysCurrent.CurrentSupport;
                comboBoxCurrentPeriod.SelectedValue = Convert.ToInt32(sysCurrent.CurrentPeriodId);
                comboBoxTerminal.SelectedValue = Convert.ToInt32(sysCurrent.TerminalId);
                comboBoxWalkinCustomer.SelectedValue = Convert.ToInt32(sysCurrent.WalkinCustomerId);
                comboBoxDefaultDiscount.SelectedValue = Convert.ToInt32(sysCurrent.DefaultDiscountId);
                comboBoxReturnSupplier.SelectedValue = Convert.ToInt32(sysCurrent.ReturnSupplierId);
                textBoxORPrintTitle.Text = sysCurrent.ORPrintTitle;
                checkBoxIsTenderPrint.Checked = Convert.ToBoolean(sysCurrent.IsTenderPrint);
                checkBoxIsBarcodeQuantityAlwaysOne.Checked = Convert.ToBoolean(sysCurrent.IsBarcodeQuantityAlwaysOne);
                checkBoxWithCustomerDisplay.Checked = Convert.ToBoolean(sysCurrent.WithCustomerDisplay);
                textBoxCustomerDisplayPort.Text = sysCurrent.CustomerDisplayPort;
                textBoxCustomerDisplayBaudRate.Text = sysCurrent.CustomerDisplayBaudRate.ToString();
                textBoxCustomerDisplayFirstLineMessage.Text = sysCurrent.CustomerDisplayFirstLineMessage;
                textBoxCustomerDisplayIfCounterClosedMessage.Text = sysCurrent.CustomerDisplayIfCounterClosedMessage;
                comboBoxCollectionReport.Text = sysCurrent.CollectionReport;
                textBoxZReadingFooter.Text = sysCurrent.ZReadingFooter;
                textBoxEasypayAPIURL.Text = sysCurrent.EasypayAPIURL;
                textBoxEasypayDefaultUsername.Text = sysCurrent.EasypayDefaultUsername;
                textBoxEasypayDefaultPassword.Text = sysCurrent.EasypayDefaultPassword;
                textBoxEasypayMotherCardNumber.Text = sysCurrent.EasypayMotherCardNumber;
                checkBoxActivateAuditTrail.Checked = Convert.ToBoolean(sysCurrent.ActivateAuditTrail);
                comboBoxPOSType.Text = sysCurrent.POSType;
                checkBoxAllowNegativeInventory.Checked = Convert.ToBoolean(sysCurrent.AllowNegativeInventory);
                checkBoxEnableEasyShopIntegration.Checked = Convert.ToBoolean(sysCurrent.EnableEasyShopIntegration);
                checkBoxPromptLoginSales.Checked = Convert.ToBoolean(sysCurrent.PromptLoginSales);
                comboBoxPrinterType.Text = sysCurrent.PrinterType;
                checkBoxSwipeLogin.Checked = Convert.ToBoolean(sysCurrent.SwipeLogin);
                textBoxWithdrawalFooter.Text = sysCurrent.WithdrawalFooter;
                textBoxWithdrawalPrintTitle.Text = sysCurrent.WithdrawalPrintTitle;
                checkBoxDateLogin.Checked = Convert.ToBoolean(sysCurrent.DateLogin);
                checkBoxHideSalesAmount.Checked = Convert.ToBoolean(sysCurrent.HideSalesAmount);
                checkBoxStockInPriceAndCost.Checked = Convert.ToBoolean(sysCurrent.HideStockInPriceAndCost);
                checkBoxHideTouchSalesItemDetail.Checked = Convert.ToBoolean(sysCurrent.HideSalesItemDetail);
                checkBoxHideItemListBarcode.Checked = Convert.ToBoolean(sysCurrent.HideItemListBarcode);
                checkBoxHideItemListItemCode.Checked = Convert.ToBoolean(sysCurrent.HideItemListItemCode);
                checkBoxAllowCancelPreviousSales.Checked = Convert.ToBoolean(sysCurrent.AllowCancelPreviousSales);
                checkBoxAutoSales.Checked = Convert.ToBoolean(sysCurrent.LockAutoSales);
                checkBoxShowCustomerInfo.Checked = Convert.ToBoolean(sysCurrent.ShowCustomerInfo);
                checkBoxChoosePrinter.Checked = Convert.ToBoolean(sysCurrent.ChoosePrinter);
                checkBoxIsTriggeredQuantity.Checked = Convert.ToBoolean(sysCurrent.IsTriggeredQuantity);
                checkBoxEnableEditPrice.Checked = Convert.ToBoolean(sysCurrent.EnableEditPrice);
                comboBoxSalesOrderPrinterType.Text = sysCurrent.SalesOrderPrinterType;
                checkBoxDisableSalesCustomerSelection.Checked = Convert.ToBoolean(sysCurrent.DisableSalesCustomerSelection);
                comboBoxLanguage.Text = sysCurrent.Language;
                checkBoxSingleColumnBarcodePrintOut.Checked = Convert.ToBoolean(sysCurrent.SingleColumnBarcodePrintOut);
                checkBoxRestrictCashin.Checked = Convert.ToBoolean(sysCurrent.RestrictCashin);
                checkBoxDisableLockTender.Checked = Convert.ToBoolean(sysCurrent.DisableLockTender);
                checkBoxPopupCustomer.Checked = Convert.ToBoolean(sysCurrent.PopupCustomerInfo);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "System Current") { textBoxCompanyName.Focus(); }
            if (tabControl1.SelectedTab.Name == "Easyfis Integration") { dtpIntegrationDate.Focus(); }
        }

        private void buttonLock_Click(object sender, EventArgs e)
        {
            buttonLock.Enabled = false;
            buttonUnlock.Enabled = true;

            var currentSettings = Modules.SysCurrentModule.GetCurrentSettings();

            Controllers.SysCurrentController sysSettingsController = new Controllers.SysCurrentController();
            Entities.SysCurrentEntity sysCurrentEntity = new Entities.SysCurrentEntity()
            {
                CompanyName = textBoxCompanyName.Text,
                Address = textBoxAddress.Text,
                ContactNo = textBoxContactNo.Text,
                TIN = textBoxTIN.Text,
                AccreditationNo = textBoxAccreditationNo.Text,
                SerialNo = textBoxSerialNo.Text,
                PermitNo = textBoxPermitNo.Text,
                MachineNo = textBoxMachineNo.Text,
                DeclareRate = currentSettings.DeclareRate,
                ReceiptFooter = textBoxReceiptFooter.Text,
                InvoiceFooter = textBoxInvoiceFooter.Text,
                LicenseCode = textBoxLicenseCode.Text,
                TenantOf = textBoxTenantOf.Text,
                CurrentUserId = currentSettings.CurrentUserId,
                CurrentUserName = currentSettings.CurrentUserName,
                CurrentVersion = textBoxCurrentVersion.Text,
                CurrentDeveloper = textBoxCurrentDeveloper.Text,
                CurrentSupport = textBoxCurrentSupport.Text,
                CurrentPeriodId = Convert.ToInt32(comboBoxCurrentPeriod.SelectedValue != null ? comboBoxCurrentPeriod.SelectedValue : 0),
                CurrentDate = currentSettings.CurrentDate,
                TerminalId = Convert.ToInt32(comboBoxTerminal.SelectedValue != null ? comboBoxTerminal.SelectedValue : 0),
                WalkinCustomerId = Convert.ToInt32(comboBoxWalkinCustomer.SelectedValue != null ? comboBoxWalkinCustomer.SelectedValue : 0),
                DefaultDiscountId = Convert.ToInt32(comboBoxDefaultDiscount.SelectedValue != null ? comboBoxDefaultDiscount.SelectedValue : 0),
                ReturnSupplierId = Convert.ToInt32(comboBoxReturnSupplier.SelectedValue != null ? comboBoxReturnSupplier.SelectedValue : 0),
                ORPrintTitle = textBoxORPrintTitle.Text,
                IsTenderPrint = checkBoxIsTenderPrint.Checked,
                IsBarcodeQuantityAlwaysOne = checkBoxIsBarcodeQuantityAlwaysOne.Checked,
                WithCustomerDisplay = checkBoxWithCustomerDisplay.Checked,
                CustomerDisplayPort = textBoxCustomerDisplayPort.Text,
                CustomerDisplayBaudRate = Convert.ToInt32(textBoxCustomerDisplayBaudRate.Text),
                CustomerDisplayFirstLineMessage = textBoxCustomerDisplayFirstLineMessage.Text,
                CustomerDisplayIfCounterClosedMessage = textBoxCustomerDisplayIfCounterClosedMessage.Text,
                CollectionReport = comboBoxCollectionReport.Text,
                ZReadingFooter = textBoxZReadingFooter.Text,
                EasypayAPIURL = textBoxEasypayAPIURL.Text,
                EasypayDefaultUsername = textBoxEasypayDefaultUsername.Text,
                EasypayDefaultPassword = textBoxEasypayDefaultPassword.Text,
                EasypayMotherCardNumber = textBoxEasypayMotherCardNumber.Text,
                ActivateAuditTrail = checkBoxActivateAuditTrail.Checked,
                FacepayImagePath = currentSettings.FacepayImagePath,
                POSType = comboBoxPOSType.Text,
                AllowNegativeInventory = checkBoxAllowNegativeInventory.Checked,
                IsLoginDate = currentSettings.IsLoginDate,
                EnableEasyShopIntegration = checkBoxEnableEasyShopIntegration.Checked,
                PromptLoginSales = checkBoxPromptLoginSales.Checked,
                PrinterType = comboBoxPrinterType.Text,
                SwipeLogin = checkBoxSwipeLogin.Checked,
                WithdrawalFooter = textBoxWithdrawalFooter.Text,
                WithdrawalPrintTitle = textBoxWithdrawalPrintTitle.Text,
                DateLogin = checkBoxDateLogin.Checked,
                HideSalesAmount = checkBoxHideSalesAmount.Checked,
                HideStockInPriceAndCost = checkBoxStockInPriceAndCost.Checked,
                HideSalesItemDetail = checkBoxHideTouchSalesItemDetail.Checked,
                HideItemListBarcode = checkBoxHideItemListBarcode.Checked,
                HideItemListItemCode = checkBoxHideItemListItemCode.Checked,
                AllowCancelPreviousSales = checkBoxAllowCancelPreviousSales.Checked,
                LockAutoSales = checkBoxAutoSales.Checked,
                ShowCustomerInfo = checkBoxShowCustomerInfo.Checked,
                ChoosePrinter = checkBoxChoosePrinter.Checked,
                IsTriggeredQuantity = checkBoxIsTriggeredQuantity.Checked,
                EnableEditPrice = checkBoxEnableEditPrice.Checked,
                SalesOrderPrinterType = comboBoxSalesOrderPrinterType.Text,
                DisableSalesCustomerSelection = checkBoxDisableSalesCustomerSelection.Checked,
                Language = comboBoxLanguage.Text,
                SingleColumnBarcodePrintOut = checkBoxSingleColumnBarcodePrintOut.Checked,
                RestrictCashin = checkBoxRestrictCashin.Checked,
                DisableLockTender = checkBoxDisableLockTender.Checked,
                PopupCustomerInfo = checkBoxPopupCustomer.Checked
            };

            String[] saveSysCurrent = sysSettingsController.UpdateSysCurrent(sysCurrentEntity);
            if (saveSysCurrent[1].Equals("0") == false)
            {
                textBoxCompanyName.Enabled = false;
                textBoxAddress.Enabled = false;
                textBoxContactNo.Enabled = false;
                textBoxTIN.Enabled = false;
                textBoxAccreditationNo.Enabled = false;
                textBoxSerialNo.Enabled = false;
                textBoxPermitNo.Enabled = false;
                textBoxMachineNo.Enabled = false;
                textBoxReceiptFooter.Enabled = false;
                textBoxInvoiceFooter.Enabled = false;
                textBoxLicenseCode.Enabled = false;
                textBoxTenantOf.Enabled = false;
                textBoxCurrentVersion.Enabled = false;
                textBoxCurrentDeveloper.Enabled = false;
                textBoxCurrentSupport.Enabled = false;
                comboBoxCurrentPeriod.Enabled = false;
                comboBoxTerminal.Enabled = false;
                comboBoxWalkinCustomer.Enabled = false;
                comboBoxDefaultDiscount.Enabled = false;
                comboBoxReturnSupplier.Enabled = false;
                textBoxORPrintTitle.Enabled = false;
                checkBoxIsTenderPrint.Enabled = false;
                checkBoxIsBarcodeQuantityAlwaysOne.Enabled = false;
                checkBoxWithCustomerDisplay.Enabled = false;
                textBoxCustomerDisplayPort.Enabled = false;
                textBoxCustomerDisplayBaudRate.Enabled = false;
                textBoxCustomerDisplayFirstLineMessage.Enabled = false;
                textBoxCustomerDisplayIfCounterClosedMessage.Enabled = false;
                comboBoxCollectionReport.Enabled = false;
                textBoxZReadingFooter.Enabled = false;
                textBoxEasypayAPIURL.Enabled = false;
                textBoxEasypayDefaultUsername.Enabled = false;
                textBoxEasypayDefaultPassword.Enabled = false;
                textBoxEasypayMotherCardNumber.Enabled = false;
                checkBoxActivateAuditTrail.Enabled = false;
                comboBoxPOSType.Enabled = false;
                checkBoxAllowNegativeInventory.Enabled = false;
                checkBoxEnableEasyShopIntegration.Enabled = false;
                checkBoxPromptLoginSales.Enabled = false;
                comboBoxPrinterType.Enabled = false;
                checkBoxSwipeLogin.Enabled = false;
                textBoxWithdrawalFooter.Enabled = false;
                textBoxWithdrawalPrintTitle.Enabled = false;
                checkBoxDateLogin.Enabled = false;
                checkBoxHideSalesAmount.Enabled = false;
                checkBoxStockInPriceAndCost.Enabled = false;
                checkBoxHideTouchSalesItemDetail.Enabled = false;
                checkBoxHideItemListBarcode.Enabled = false;
                checkBoxHideItemListItemCode.Enabled = false;
                checkBoxAllowCancelPreviousSales.Enabled = false;
                checkBoxAutoSales.Enabled = false;
                checkBoxShowCustomerInfo.Enabled = false;
                checkBoxChoosePrinter.Enabled = false;
                checkBoxIsTriggeredQuantity.Enabled = false;
                checkBoxEnableEditPrice.Enabled = false;
                comboBoxSalesOrderPrinterType.Enabled = false;
                checkBoxDisableSalesCustomerSelection.Enabled = false;
                comboBoxLanguage.Enabled = false;
                checkBoxSingleColumnBarcodePrintOut.Enabled = false;
                checkBoxRestrictCashin.Enabled = false;
                checkBoxDisableLockTender.Enabled = false;
                checkBoxPopupCustomer.Enabled = false;
            }
            else
            {
                MessageBox.Show(saveSysCurrent[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUnlock_Click(object sender, EventArgs e)
        {
            if (isIntegrationStarted == true)
            {
                MessageBox.Show("Please stop the integration first.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                buttonLock.Enabled = true;
                buttonUnlock.Enabled = false;

                textBoxCompanyName.Enabled = true;
                textBoxAddress.Enabled = true;
                textBoxContactNo.Enabled = true;
                textBoxTIN.Enabled = true;
                textBoxAccreditationNo.Enabled = true;
                textBoxSerialNo.Enabled = true;
                textBoxPermitNo.Enabled = true;
                textBoxMachineNo.Enabled = true;
                textBoxReceiptFooter.Enabled = true;
                textBoxInvoiceFooter.Enabled = true;
                textBoxLicenseCode.Enabled = true;
                textBoxTenantOf.Enabled = true;
                textBoxCurrentVersion.Enabled = true;
                textBoxCurrentDeveloper.Enabled = true;
                textBoxCurrentSupport.Enabled = true;
                comboBoxCurrentPeriod.Enabled = true;
                comboBoxTerminal.Enabled = true;
                comboBoxWalkinCustomer.Enabled = true;
                comboBoxDefaultDiscount.Enabled = true;
                comboBoxReturnSupplier.Enabled = true;
                textBoxORPrintTitle.Enabled = true;
                checkBoxIsTenderPrint.Enabled = true;
                checkBoxIsBarcodeQuantityAlwaysOne.Enabled = true;
                checkBoxWithCustomerDisplay.Enabled = true;

                if (checkBoxWithCustomerDisplay.Checked == true)
                {
                    textBoxCustomerDisplayPort.Enabled = true;
                    textBoxCustomerDisplayBaudRate.Enabled = true;
                    textBoxCustomerDisplayFirstLineMessage.Enabled = true;
                    textBoxCustomerDisplayIfCounterClosedMessage.Enabled = true;
                }
                else
                {
                    textBoxCustomerDisplayPort.Enabled = false;
                    textBoxCustomerDisplayBaudRate.Enabled = false;
                    textBoxCustomerDisplayFirstLineMessage.Enabled = false;
                    textBoxCustomerDisplayIfCounterClosedMessage.Enabled = false;
                }

                comboBoxCollectionReport.Enabled = true;
                textBoxZReadingFooter.Enabled = true;
                textBoxEasypayAPIURL.Enabled = true;
                textBoxEasypayDefaultUsername.Enabled = true;
                textBoxEasypayDefaultPassword.Enabled = true;
                textBoxEasypayMotherCardNumber.Enabled = true;
                checkBoxActivateAuditTrail.Enabled = true;
                comboBoxPOSType.Enabled = true;
                checkBoxAllowNegativeInventory.Enabled = true;
                checkBoxEnableEasyShopIntegration.Enabled = true;
                checkBoxPromptLoginSales.Enabled = true;
                comboBoxPrinterType.Enabled = true;
                checkBoxSwipeLogin.Enabled = true;
                textBoxWithdrawalFooter.Enabled = true;
                textBoxWithdrawalPrintTitle.Enabled = true;
                checkBoxDateLogin.Enabled = true;
                checkBoxHideSalesAmount.Enabled = true;
                checkBoxStockInPriceAndCost.Enabled = true;
                checkBoxHideTouchSalesItemDetail.Enabled = true;
                checkBoxHideItemListBarcode.Enabled = true;
                checkBoxHideItemListItemCode.Enabled = true;
                checkBoxAllowCancelPreviousSales.Enabled = true;
                checkBoxAutoSales.Enabled = true;
                checkBoxShowCustomerInfo.Enabled = true;
                checkBoxChoosePrinter.Enabled = true;
                checkBoxIsTriggeredQuantity.Enabled = true;
                checkBoxEnableEditPrice.Enabled = true;
                comboBoxSalesOrderPrinterType.Enabled = true;
                checkBoxDisableSalesCustomerSelection.Enabled = true;
                comboBoxLanguage.Enabled = true;
                checkBoxSingleColumnBarcodePrintOut.Enabled = true;
                checkBoxRestrictCashin.Enabled = true;
                checkBoxDisableLockTender.Enabled = true;
                checkBoxPopupCustomer.Enabled = true;
            }
        }

        private void checkBoxWithCustomerDisplay_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxWithCustomerDisplay.Checked == true)
            {
                if (buttonLock.Enabled == true)
                {
                    textBoxCustomerDisplayPort.Enabled = true;
                    textBoxCustomerDisplayBaudRate.Enabled = true;
                    textBoxCustomerDisplayFirstLineMessage.Enabled = true;
                    textBoxCustomerDisplayIfCounterClosedMessage.Enabled = true;
                }
                else
                {
                    textBoxCustomerDisplayPort.Enabled = false;
                    textBoxCustomerDisplayBaudRate.Enabled = false;
                    textBoxCustomerDisplayFirstLineMessage.Enabled = false;
                    textBoxCustomerDisplayIfCounterClosedMessage.Enabled = false;
                }
            }
            else
            {
                textBoxCustomerDisplayPort.Enabled = false;
                textBoxCustomerDisplayBaudRate.Enabled = false;
                textBoxCustomerDisplayFirstLineMessage.Enabled = false;
                textBoxCustomerDisplayIfCounterClosedMessage.Enabled = false;
            }
        }


        private void SysSettingsForm_Load(object sender, EventArgs e)
        {

        }

        public void UpdateKitchenDataSource()
        {
            SetKitchenDataSourceAsync();
        }

        public async void SetKitchenDataSourceAsync()
        {
            List<Entities.DgvSysSettingsKitchenEntity> getKitchenData = await GetKitchenDataTask();
            if (getKitchenData.Any())
            {
                kitchenData = getKitchenData;
                kitchenPageList = new PagedList<Entities.DgvSysSettingsKitchenEntity>(kitchenData, kitchenPageNumber, pageSize);

                if (kitchenPageList.PageCount == 1)
                {
                    buttonKitchenPageListFirst.Enabled = false;
                    buttonKitchenPageListPrevious.Enabled = false;
                    buttonKitchenPageListNext.Enabled = false;
                    buttonKitchenPageListLast.Enabled = false;
                }
                else if (kitchenPageNumber == 1)
                {
                    buttonKitchenPageListFirst.Enabled = false;
                    buttonKitchenPageListPrevious.Enabled = false;
                    buttonKitchenPageListNext.Enabled = true;
                    buttonKitchenPageListLast.Enabled = true;
                }
                else if (kitchenPageNumber == kitchenPageList.PageCount)
                {
                    buttonKitchenPageListFirst.Enabled = true;
                    buttonKitchenPageListPrevious.Enabled = true;
                    buttonKitchenPageListNext.Enabled = false;
                    buttonKitchenPageListLast.Enabled = false;
                }
                else
                {
                    buttonKitchenPageListFirst.Enabled = true;
                    buttonKitchenPageListPrevious.Enabled = true;
                    buttonKitchenPageListNext.Enabled = true;
                    buttonKitchenPageListLast.Enabled = true;
                }

                textBoxKitchenPageNumber.Text = kitchenPageNumber + " / " + kitchenPageList.PageCount;
                kitchenDataSource.DataSource = kitchenPageList;
            }
            else
            {
                buttonKitchenPageListFirst.Enabled = false;
                buttonKitchenPageListPrevious.Enabled = false;
                buttonKitchenPageListNext.Enabled = false;
                buttonKitchenPageListLast.Enabled = false;

                kitchenPageNumber = 1;

                kitchenData = new List<Entities.DgvSysSettingsKitchenEntity>();
                kitchenDataSource.Clear();
                textBoxKitchenPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvSysSettingsKitchenEntity>> GetKitchenDataTask()
        {
            String filter = textBoxKitchenFilter.Text;
            Controllers.SysKitchenPrinterController sysKichenPrinterController = new Controllers.SysKitchenPrinterController();

            List<Entities.SysKitchenPrinterEntity> listKitchen = sysKichenPrinterController.ListKitchen(filter);
            if (listKitchen.Any())
            {
                var accounts = from d in listKitchen
                               select new Entities.DgvSysSettingsKitchenEntity
                               {
                                   ColumnKitchenButtonEdit = "Edit",
                                   ColumnKitchenId = d.Id,
                                   ColumnKitchenNumber = d.Kitchen,
                                   ColumnPrinterName = d.PrinterName,
                                   ColumnAlias = d.Alias,
                                   ColumnDefaultWidth = d.DefaultWidth,
                                   ColumnDefaultHeight = d.DefaultHeight
                               };

                return Task.FromResult(accounts.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvSysSettingsKitchenEntity>());
            }
        }

        public void CreateKitchenDataGridView()
        {
            UpdateKitchenDataSource();

            dataGridViewKitchen.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewKitchen.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewKitchen.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewKitchen.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewKitchen.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewKitchen.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewKitchen.DataSource = kitchenDataSource;
        }

        public void GetKitchenCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void buttonKitchenPageListFirst_Click(object sender, EventArgs e)
        {
            kitchenPageList = new PagedList<Entities.DgvSysSettingsKitchenEntity>(kitchenData, 1, pageSize);
            kitchenDataSource.DataSource = kitchenPageList;

            buttonKitchenPageListFirst.Enabled = false;
            buttonKitchenPageListPrevious.Enabled = false;
            buttonKitchenPageListNext.Enabled = true;
            buttonKitchenPageListLast.Enabled = true;

            kitchenPageNumber = 1;
            textBoxKitchenPageNumber.Text = kitchenPageNumber + " / " + kitchenPageList.PageCount;
        }

        private void buttonKitchenPageListPrevious_Click(object sender, EventArgs e)
        {
            if (kitchenPageList.HasPreviousPage == true)
            {
                kitchenPageList = new PagedList<Entities.DgvSysSettingsKitchenEntity>(kitchenData, --kitchenPageNumber, pageSize);
                kitchenDataSource.DataSource = kitchenPageList;
            }

            buttonKitchenPageListNext.Enabled = true;
            buttonKitchenPageListLast.Enabled = true;

            if (kitchenPageNumber == 1)
            {
                buttonKitchenPageListFirst.Enabled = false;
                buttonKitchenPageListPrevious.Enabled = false;
            }

            textBoxKitchenPageNumber.Text = kitchenPageNumber + " / " + kitchenPageList.PageCount;
        }

        private void buttonKitchenPageListNext_Click(object sender, EventArgs e)
        {
            if (kitchenPageList.HasNextPage == true)
            {
                kitchenPageList = new PagedList<Entities.DgvSysSettingsKitchenEntity>(kitchenData, ++kitchenPageNumber, pageSize);
                kitchenDataSource.DataSource = kitchenPageList;
            }

            buttonKitchenPageListFirst.Enabled = true;
            buttonKitchenPageListPrevious.Enabled = true;

            if (kitchenPageNumber == kitchenPageList.PageCount)
            {
                buttonKitchenPageListNext.Enabled = false;
                buttonKitchenPageListLast.Enabled = false;
            }

            textBoxKitchenPageNumber.Text = kitchenPageNumber + " / " + kitchenPageList.PageCount;
        }

        private void buttonKitchenPageListLast_Click(object sender, EventArgs e)
        {
            kitchenPageList = new PagedList<Entities.DgvSysSettingsKitchenEntity>(kitchenData, kitchenPageList.PageCount, pageSize);
            kitchenDataSource.DataSource = kitchenPageList;

            buttonKitchenPageListFirst.Enabled = true;
            buttonKitchenPageListPrevious.Enabled = true;
            buttonKitchenPageListNext.Enabled = false;
            buttonKitchenPageListLast.Enabled = false;

            kitchenPageNumber = kitchenPageList.PageCount;
            textBoxKitchenPageNumber.Text = kitchenPageNumber + " / " + kitchenPageList.PageCount;
        }

        private void dataGridViewKitchen_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetKitchenCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewKitchen.CurrentCell.ColumnIndex == dataGridViewKitchen.Columns["ColumnKitchenButtonEdit"].Index)
            {
                Entities.SysKitchenPrinterEntity kitchenPrinter = new Entities.SysKitchenPrinterEntity
                {
                    Id = Convert.ToInt32(dataGridViewKitchen.Rows[e.RowIndex].Cells[1].Value),
                    Kitchen = dataGridViewKitchen.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    PrinterName = dataGridViewKitchen.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    Alias = dataGridViewKitchen.Rows[e.RowIndex].Cells[4].Value.ToString(),
                    DefaultWidth = Convert.ToInt32(dataGridViewKitchen.Rows[e.RowIndex].Cells[5].Value.ToString()),
                    DefaultHeight = Convert.ToInt32(dataGridViewKitchen.Rows[e.RowIndex].Cells[6].Value.ToString())
                };

                SysSettings.SysKitchenPrinterDetailForm sysKitchenPrinterDetailForm = new SysKitchenPrinterDetailForm(this, kitchenPrinter);
                sysKitchenPrinterDetailForm.ShowDialog();
            }
        }

        private void textBoxKitchenFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateKitchenDataSource();
            }
        }
    }
}
