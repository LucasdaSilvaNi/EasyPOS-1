using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Entities
{
    public class SysCurrentEntity
    {
        public String CompanyName { get; set; }
        public String Address { get; set; }
        public String ContactNo { get; set; }
        public String TIN { get; set; }
        public String AccreditationNo { get; set; }
        public String SerialNo { get; set; }
        public String PermitNo { get; set; }
        public String MachineNo { get; set; }
        public Decimal DeclareRate { get; set; }
        public String ReceiptFooter { get; set; }
        public String InvoiceFooter { get; set; }
        public String LicenseCode { get; set; }
        public String TenantOf { get; set; }
        public Int32 CurrentUserId { get; set; }
        public String CurrentUserName { get; set; }
        public String CurrentVersion { get; set; }
        public String CurrentDeveloper { get; set; }
        public String CurrentSupport { get; set; }
        public Int32 CurrentPeriodId { get; set; }
        public String CurrentDate { get; set; }
        public Int32 TerminalId { get; set; }
        public Int32 WalkinCustomerId { get; set; }
        public Int32 DefaultDiscountId { get; set; }
        public Int32 ReturnSupplierId { get; set; }
        public String ORPrintTitle { get; set; }
        public Boolean IsTenderPrint { get; set; }
        public Boolean IsBarcodeQuantityAlwaysOne { get; set; }
        public Boolean WithCustomerDisplay { get; set; }
        public String CustomerDisplayPort { get; set; }
        public Int32 CustomerDisplayBaudRate { get; set; }
        public String CustomerDisplayFirstLineMessage { get; set; }
        public String CustomerDisplayIfCounterClosedMessage { get; set; }
        public String CollectionReport { get; set; }
        public String ZReadingFooter { get; set; }
        public String EasypayAPIURL { get; set; }
        public String EasypayDefaultUsername { get; set; }
        public String EasypayDefaultPassword { get; set; }
        public String EasypayMotherCardNumber { get; set; }
        public Boolean ActivateAuditTrail { get; set; }
        public String FacepayImagePath { get; set; }
        public String POSType { get; set; }
        public Boolean AllowNegativeInventory { get; set; }
        public Boolean IsLoginDate { get; set; }
        public Boolean EnableEasyShopIntegration { get; set; }
        public Boolean PromptLoginSales { get; set; }
        public String PrinterType { get; set; }
        public Boolean SwipeLogin { get; set; }
        public String WithdrawalFooter { get; set; }
        public String WithdrawalPrintTitle { get; set; }
        public Boolean DateLogin { get; set; }
        public Boolean HideSalesAmount { get; set; }
        public Boolean HideStockInPriceAndCost { get; set; }
        public Boolean HideSalesItemDetail { get; set; }
        public Boolean HideItemListBarcode { get; set; }
        public Boolean HideItemListItemCode { get; set; }
        public Boolean AllowCancelPreviousSales { get; set; }
        public Boolean LockAutoSales { get; set; }
        public Boolean ShowCustomerInfo { get; set; }
        public Boolean ChoosePrinter { get; set; }
        public Boolean IsTriggeredQuantity { get; set; }
        public Boolean EnableEditPrice { get; set; }
        public String SalesOrderPrinterType { get; set; }
        public Boolean DisableSalesCustomerSelection { get; set; }
        public String Language { get; set; }
        public Boolean SingleColumnBarcodePrintOut { get; set; }
    }
}