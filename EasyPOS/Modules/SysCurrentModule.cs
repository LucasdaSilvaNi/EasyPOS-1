using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace EasyPOS.Modules
{
    class SysCurrentModule
    {
        public static String path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Settings/SysCurrent.json");

        // ====================
        // Get Current Settings 
        // ====================
        public static Entities.SysCurrentEntity GetCurrentSettings()
        {
            String json;
            using (StreamReader trmRead = new StreamReader(path)) { json = trmRead.ReadToEnd(); }

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            Entities.SysCurrentEntity sysCurrentEntity = javaScriptSerializer.Deserialize<Entities.SysCurrentEntity>(json);

            return sysCurrentEntity;
        }

        // ===============================
        // Update Current Settings - Login
        // ===============================
        public static void UpdateCurrentSettingsLogin(Int32 currentUserId, String userName, String loginDate, Boolean IsLoginDate)
        {
            var currentSettings = GetCurrentSettings();

            DateTime currentDate = DateTime.Today;
            if (IsLoginDate == true)
            {
                currentDate = Convert.ToDateTime(loginDate);
            }

            Entities.SysCurrentEntity newEntities = new Entities.SysCurrentEntity()
            {
                CompanyName = currentSettings.CompanyName,
                Address = currentSettings.Address,
                ContactNo = currentSettings.ContactNo,
                TIN = currentSettings.TIN,
                AccreditationNo = currentSettings.AccreditationNo,
                SerialNo = currentSettings.SerialNo,
                PermitNo = currentSettings.PermitNo,
                MachineNo = currentSettings.MachineNo,
                DeclareRate = currentSettings.DeclareRate,
                ReceiptFooter = currentSettings.ReceiptFooter,
                InvoiceFooter = currentSettings.InvoiceFooter,
                LicenseCode = currentSettings.LicenseCode,
                TenantOf = currentSettings.TenantOf,
                CurrentUserId = currentUserId,
                CurrentUserName = userName,
                CurrentVersion = currentSettings.CurrentVersion,
                CurrentDeveloper = currentSettings.CurrentDeveloper,
                CurrentSupport = currentSettings.CurrentSupport,
                CurrentPeriodId = currentSettings.CurrentPeriodId,
                CurrentDate = currentDate.ToShortDateString(),
                TerminalId = currentSettings.TerminalId,
                WalkinCustomerId = currentSettings.WalkinCustomerId,
                DefaultDiscountId = currentSettings.DefaultDiscountId,
                ReturnSupplierId = currentSettings.ReturnSupplierId,
                ORPrintTitle = currentSettings.ORPrintTitle,
                IsTenderPrint = currentSettings.IsTenderPrint,
                IsBarcodeQuantityAlwaysOne = currentSettings.IsBarcodeQuantityAlwaysOne,
                WithCustomerDisplay = currentSettings.WithCustomerDisplay,
                CustomerDisplayPort = currentSettings.CustomerDisplayPort,
                CustomerDisplayBaudRate = currentSettings.CustomerDisplayBaudRate,
                CustomerDisplayFirstLineMessage = currentSettings.CustomerDisplayFirstLineMessage,
                CustomerDisplayIfCounterClosedMessage = currentSettings.CustomerDisplayIfCounterClosedMessage,
                CollectionReport = currentSettings.CollectionReport,
                ZReadingFooter = currentSettings.ZReadingFooter,
                EasypayAPIURL = currentSettings.EasypayAPIURL,
                EasypayDefaultUsername = currentSettings.EasypayDefaultUsername,
                EasypayDefaultPassword = currentSettings.EasypayDefaultPassword,
                EasypayMotherCardNumber = currentSettings.EasypayMotherCardNumber,
                ActivateAuditTrail = currentSettings.ActivateAuditTrail,
                FacepayImagePath = currentSettings.FacepayImagePath,
                POSType = currentSettings.POSType,
                AllowNegativeInventory = currentSettings.AllowNegativeInventory,
                IsLoginDate = IsLoginDate,
                EnableEasyShopIntegration = currentSettings.EnableEasyShopIntegration,
                PromptLoginSales = currentSettings.PromptLoginSales,
                PrinterType = currentSettings.PrinterType,
                SwipeLogin = currentSettings.SwipeLogin,
                WithdrawalFooter = currentSettings.WithdrawalFooter,
                WithdrawalPrintTitle = currentSettings.WithdrawalPrintTitle,
                DateLogin = currentSettings.DateLogin,
                HideSalesAmount = currentSettings.HideSalesAmount,
                HideStockInPriceAndCost = currentSettings.HideStockInPriceAndCost,
                HideSalesItemDetail = currentSettings.HideSalesItemDetail,
                HideItemListBarcode = currentSettings.HideItemListBarcode,
                HideItemListItemCode = currentSettings.HideItemListItemCode,
                AllowCancelPreviousSales = currentSettings.AllowCancelPreviousSales,
                LockAutoSales = currentSettings.LockAutoSales,
                ShowCustomerInfo = currentSettings.ShowCustomerInfo,
                ChoosePrinter = currentSettings.ChoosePrinter,
                IsTriggeredQuantity = currentSettings.IsTriggeredQuantity,
                EnableEditPrice = currentSettings.EnableEditPrice,
                SalesOrderPrinterType = currentSettings.SalesOrderPrinterType,
                DisableSalesCustomerSelection = currentSettings.DisableSalesCustomerSelection,
                Language = currentSettings.Language,
                SingleColumnBarcodePrintOut = currentSettings.SingleColumnBarcodePrintOut,
                RestrictCashin = currentSettings.RestrictCashin,
                DisableLockTender = currentSettings.DisableLockTender,
                PopupCustomerInfo = currentSettings.PopupCustomerInfo,
                BodegaTransaction = currentSettings.BodegaTransaction
            };

            String newJson = new JavaScriptSerializer().Serialize(newEntities);
            File.WriteAllText(path, newJson);
        }

        // ===============================
        // Update Current Settings - Login
        // ===============================
        public static void UpdateCurrentSettings(Entities.SysCurrentEntity objSysCurrentEntity)
        {
            var currentSettings = GetCurrentSettings();

            Entities.SysCurrentEntity newSysCurrentEntities = new Entities.SysCurrentEntity()
            {
                CompanyName = objSysCurrentEntity.CompanyName,
                Address = objSysCurrentEntity.Address,
                ContactNo = objSysCurrentEntity.ContactNo,
                TIN = objSysCurrentEntity.TIN,
                AccreditationNo = objSysCurrentEntity.AccreditationNo,
                SerialNo = objSysCurrentEntity.SerialNo,
                PermitNo = objSysCurrentEntity.PermitNo,
                MachineNo = objSysCurrentEntity.MachineNo,
                DeclareRate = objSysCurrentEntity.DeclareRate,
                ReceiptFooter = objSysCurrentEntity.ReceiptFooter,
                InvoiceFooter = objSysCurrentEntity.InvoiceFooter,
                LicenseCode = objSysCurrentEntity.LicenseCode,
                TenantOf = objSysCurrentEntity.TenantOf,
                CurrentUserId = currentSettings.CurrentUserId,
                CurrentUserName = currentSettings.CurrentUserName,
                CurrentVersion = objSysCurrentEntity.CurrentVersion,
                CurrentDeveloper = objSysCurrentEntity.CurrentDeveloper,
                CurrentSupport = objSysCurrentEntity.CurrentSupport,
                CurrentPeriodId = objSysCurrentEntity.CurrentPeriodId,
                CurrentDate = currentSettings.CurrentDate,
                TerminalId = objSysCurrentEntity.TerminalId,
                WalkinCustomerId = objSysCurrentEntity.WalkinCustomerId,
                DefaultDiscountId = objSysCurrentEntity.DefaultDiscountId,
                ReturnSupplierId = objSysCurrentEntity.ReturnSupplierId,
                ORPrintTitle = objSysCurrentEntity.ORPrintTitle,
                IsTenderPrint = objSysCurrentEntity.IsTenderPrint,
                IsBarcodeQuantityAlwaysOne = objSysCurrentEntity.IsBarcodeQuantityAlwaysOne,
                WithCustomerDisplay = objSysCurrentEntity.WithCustomerDisplay,
                CustomerDisplayPort = objSysCurrentEntity.CustomerDisplayPort,
                CustomerDisplayBaudRate = objSysCurrentEntity.CustomerDisplayBaudRate,
                CustomerDisplayFirstLineMessage = objSysCurrentEntity.CustomerDisplayFirstLineMessage,
                CustomerDisplayIfCounterClosedMessage = objSysCurrentEntity.CustomerDisplayIfCounterClosedMessage,
                CollectionReport = objSysCurrentEntity.CollectionReport,
                ZReadingFooter = objSysCurrentEntity.ZReadingFooter,
                EasypayAPIURL = objSysCurrentEntity.EasypayAPIURL,
                EasypayDefaultUsername = objSysCurrentEntity.EasypayDefaultUsername,
                EasypayDefaultPassword = objSysCurrentEntity.EasypayDefaultPassword,
                EasypayMotherCardNumber = objSysCurrentEntity.EasypayMotherCardNumber,
                ActivateAuditTrail = objSysCurrentEntity.ActivateAuditTrail,
                FacepayImagePath = currentSettings.FacepayImagePath,
                POSType = objSysCurrentEntity.POSType,
                AllowNegativeInventory = objSysCurrentEntity.AllowNegativeInventory,
                IsLoginDate = currentSettings.IsLoginDate,
                EnableEasyShopIntegration = objSysCurrentEntity.EnableEasyShopIntegration,
                PromptLoginSales = objSysCurrentEntity.PromptLoginSales,
                PrinterType = objSysCurrentEntity.PrinterType,
                SwipeLogin = objSysCurrentEntity.SwipeLogin,
                WithdrawalFooter = objSysCurrentEntity.WithdrawalFooter,
                WithdrawalPrintTitle = objSysCurrentEntity.WithdrawalPrintTitle,
                DateLogin = objSysCurrentEntity.DateLogin,
                HideSalesAmount = objSysCurrentEntity.HideSalesAmount,
                HideStockInPriceAndCost = objSysCurrentEntity.HideStockInPriceAndCost,
                HideSalesItemDetail = objSysCurrentEntity.HideSalesItemDetail,
                HideItemListBarcode = objSysCurrentEntity.HideItemListBarcode,
                HideItemListItemCode = objSysCurrentEntity.HideItemListItemCode,
                AllowCancelPreviousSales = objSysCurrentEntity.AllowCancelPreviousSales,
                LockAutoSales = objSysCurrentEntity.LockAutoSales,
                ShowCustomerInfo = objSysCurrentEntity.ShowCustomerInfo,
                ChoosePrinter = objSysCurrentEntity.ChoosePrinter,
                IsTriggeredQuantity = objSysCurrentEntity.IsTriggeredQuantity,
                EnableEditPrice = objSysCurrentEntity.EnableEditPrice,
                SalesOrderPrinterType = objSysCurrentEntity.SalesOrderPrinterType,
                DisableSalesCustomerSelection = objSysCurrentEntity.DisableSalesCustomerSelection,
                Language = objSysCurrentEntity.Language,
                SingleColumnBarcodePrintOut = objSysCurrentEntity.SingleColumnBarcodePrintOut,
                RestrictCashin = objSysCurrentEntity.RestrictCashin,
                DisableLockTender = objSysCurrentEntity.DisableLockTender,
                PopupCustomerInfo = objSysCurrentEntity.PopupCustomerInfo,
                BodegaTransaction = objSysCurrentEntity.BodegaTransaction
            };

            String newJson = new JavaScriptSerializer().Serialize(newSysCurrentEntities);
            File.WriteAllText(path, newJson);
        }


        // ======================================
        // Update Current Settings - License Code
        // ======================================
        public static void UpdateCurrentSettingsLicenseCode(String licenseCode)
        {
            var currentSettings = GetCurrentSettings();
            
            Entities.SysCurrentEntity newEntities = new Entities.SysCurrentEntity()
            {
                CompanyName = currentSettings.CompanyName,
                Address = currentSettings.Address,
                ContactNo = currentSettings.ContactNo,
                TIN = currentSettings.TIN,
                AccreditationNo = currentSettings.AccreditationNo,
                SerialNo = currentSettings.SerialNo,
                PermitNo = currentSettings.PermitNo,
                MachineNo = currentSettings.MachineNo,
                DeclareRate = currentSettings.DeclareRate,
                ReceiptFooter = currentSettings.ReceiptFooter,
                InvoiceFooter = currentSettings.InvoiceFooter,
                LicenseCode = licenseCode,
                TenantOf = currentSettings.TenantOf,
                CurrentUserId = currentSettings.CurrentUserId,
                CurrentUserName = currentSettings.CurrentUserName,
                CurrentVersion = currentSettings.CurrentVersion,
                CurrentDeveloper = currentSettings.CurrentDeveloper,
                CurrentSupport = currentSettings.CurrentSupport,
                CurrentPeriodId = currentSettings.CurrentPeriodId,
                CurrentDate = currentSettings.CurrentDate,
                TerminalId = currentSettings.TerminalId,
                WalkinCustomerId = currentSettings.WalkinCustomerId,
                DefaultDiscountId = currentSettings.DefaultDiscountId,
                ReturnSupplierId = currentSettings.ReturnSupplierId,
                ORPrintTitle = currentSettings.ORPrintTitle,
                IsTenderPrint = currentSettings.IsTenderPrint,
                IsBarcodeQuantityAlwaysOne = currentSettings.IsBarcodeQuantityAlwaysOne,
                WithCustomerDisplay = currentSettings.WithCustomerDisplay,
                CustomerDisplayPort = currentSettings.CustomerDisplayPort,
                CustomerDisplayBaudRate = currentSettings.CustomerDisplayBaudRate,
                CustomerDisplayFirstLineMessage = currentSettings.CustomerDisplayFirstLineMessage,
                CustomerDisplayIfCounterClosedMessage = currentSettings.CustomerDisplayIfCounterClosedMessage,
                CollectionReport = currentSettings.CollectionReport,
                ZReadingFooter = currentSettings.ZReadingFooter,
                EasypayAPIURL = currentSettings.EasypayAPIURL,
                EasypayDefaultUsername = currentSettings.EasypayDefaultUsername,
                EasypayDefaultPassword = currentSettings.EasypayDefaultPassword,
                EasypayMotherCardNumber = currentSettings.EasypayMotherCardNumber,
                ActivateAuditTrail = currentSettings.ActivateAuditTrail,
                FacepayImagePath = currentSettings.FacepayImagePath,
                POSType = currentSettings.POSType,
                AllowNegativeInventory = currentSettings.AllowNegativeInventory,
                IsLoginDate = currentSettings.IsLoginDate,
                EnableEasyShopIntegration = currentSettings.EnableEasyShopIntegration,
                PromptLoginSales = currentSettings.PromptLoginSales,
                PrinterType = currentSettings.PrinterType,
                SwipeLogin = currentSettings.SwipeLogin,
                WithdrawalFooter = currentSettings.WithdrawalFooter,
                WithdrawalPrintTitle = currentSettings.WithdrawalPrintTitle,
                DateLogin = currentSettings.DateLogin,
                HideSalesAmount = currentSettings.HideSalesAmount,
                HideStockInPriceAndCost = currentSettings.HideStockInPriceAndCost,
                HideSalesItemDetail = currentSettings.HideSalesItemDetail,
                HideItemListBarcode = currentSettings.HideItemListBarcode,
                HideItemListItemCode = currentSettings.HideItemListItemCode,
                AllowCancelPreviousSales = currentSettings.AllowCancelPreviousSales,
                LockAutoSales = currentSettings.LockAutoSales,
                ShowCustomerInfo = currentSettings.ShowCustomerInfo,
                ChoosePrinter = currentSettings.ChoosePrinter,
                IsTriggeredQuantity = currentSettings.IsTriggeredQuantity,
                EnableEditPrice = currentSettings.EnableEditPrice,
                SalesOrderPrinterType = currentSettings.SalesOrderPrinterType,
                DisableSalesCustomerSelection = currentSettings.DisableSalesCustomerSelection,
                Language = currentSettings.Language,
                SingleColumnBarcodePrintOut = currentSettings.SingleColumnBarcodePrintOut,
                RestrictCashin = currentSettings.RestrictCashin,
                DisableLockTender = currentSettings.DisableLockTender,
                PopupCustomerInfo = currentSettings.PopupCustomerInfo,
                BodegaTransaction = currentSettings.BodegaTransaction
            };

            String newJson = new JavaScriptSerializer().Serialize(newEntities);
            File.WriteAllText(path, newJson);
        }
    }
}
