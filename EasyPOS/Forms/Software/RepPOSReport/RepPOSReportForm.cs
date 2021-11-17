using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.RepPOSReport
{
    public partial class RepPOSReportForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public RepPOSReportForm(SysSoftwareForm softwareForm)
        {
            InitializeComponent();
            sysSoftwareForm = softwareForm;

            GetTerminalList();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            sysSoftwareForm.RemoveTabPage();
        }

        public void GetTerminalList()
        {
            Controllers.RepPOSReportController repPOSReportController = new Controllers.RepPOSReportController();
            if (repPOSReportController.DropdownListTerminal().Any())
            {
                comboBoxTerminal.DataSource = repPOSReportController.DropdownListTerminal();
                comboBoxTerminal.ValueMember = "Id";
                comboBoxTerminal.DisplayMember = "Terminal";

                GetUserList();
            }
        }

        public void GetUserList()
        {
            Controllers.RepPOSReportController repPOSReportController = new Controllers.RepPOSReportController();
            if (repPOSReportController.DropdownListUser().Any())
            {
                comboBoxUser.DataSource = repPOSReportController.DropdownListUser();
                comboBoxUser.ValueMember = "Id";
                comboBoxUser.DisplayMember = "FullName";
            }
        }

        private void listBoxPOSReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPOSReport.SelectedItem != null)
            {
                String selectedItem = listBoxPOSReport.SelectedItem.ToString();
                switch (selectedItem)
                {
                    case "Z Reading Report":
                        labelTerminal.Visible = true;
                        comboBoxTerminal.Visible = true;

                        dateTimePickerDate.Visible = true;
                        labelDate.Visible = true;

                        comboBoxUser.Visible = false;
                        labelUser.Visible = false;

                        dateTimePickerStartDate.Visible = false;
                        labelStartDate.Visible = false;

                        dateTimePickerEndDate.Visible = false;
                        labelEndDate.Visible = false;
                        comboBoxTerminal.Focus();
                        break;
                    case "X Reading Report":
                        labelTerminal.Visible = true;
                        comboBoxTerminal.Visible = true;

                        dateTimePickerDate.Visible = true;
                        labelDate.Visible = true;

                        comboBoxUser.Visible = true;
                        labelUser.Visible = true;

                        dateTimePickerStartDate.Visible = false;
                        labelStartDate.Visible = false;

                        dateTimePickerEndDate.Visible = false;
                        labelEndDate.Visible = false;
                        comboBoxTerminal.Focus();
                        break;
                    case "E-Journal Report (ejournal.txt)":
                        labelTerminal.Visible = true;
                        comboBoxTerminal.Visible = true;

                        dateTimePickerDate.Visible = false;
                        labelDate.Visible = false;

                        comboBoxUser.Visible = false;
                        labelUser.Visible = false;

                        dateTimePickerStartDate.Visible = true;
                        labelStartDate.Visible = true;

                        dateTimePickerEndDate.Visible = true;
                        labelEndDate.Visible = true;
                        comboBoxTerminal.Focus();
                        break;
                    case "E-Sales Report (esales.csv)":
                        labelTerminal.Visible = true;
                        comboBoxTerminal.Visible = true;

                        dateTimePickerDate.Visible = false;
                        labelDate.Visible = false;

                        comboBoxUser.Visible = false;
                        labelUser.Visible = false;

                        dateTimePickerStartDate.Visible = true;
                        labelStartDate.Visible = true;

                        dateTimePickerEndDate.Visible = true;
                        labelEndDate.Visible = true;
                        comboBoxTerminal.Focus();
                        break;
                    case "Collection Register (collectionregister.csv)":
                        labelTerminal.Visible = true;
                        comboBoxTerminal.Visible = true;

                        dateTimePickerDate.Visible = false;
                        labelDate.Visible = false;

                        comboBoxUser.Visible = false;
                        labelUser.Visible = false;

                        dateTimePickerStartDate.Visible = true;
                        labelStartDate.Visible = true;

                        dateTimePickerEndDate.Visible = true;
                        labelEndDate.Visible = true;
                        comboBoxTerminal.Focus();
                        break;

                    case "E-Sales Journal":
                        labelTerminal.Visible = true;
                        comboBoxTerminal.Visible = true;

                        dateTimePickerDate.Visible = false;
                        labelDate.Visible = false;

                        comboBoxUser.Visible = false;
                        labelUser.Visible = false;

                        dateTimePickerStartDate.Visible = true;
                        labelStartDate.Visible = true;

                        dateTimePickerEndDate.Visible = true;
                        labelEndDate.Visible = true;
                        comboBoxTerminal.Focus();
                        break;

                    default:
                        labelTerminal.Visible = false;
                        comboBoxTerminal.Visible = false;

                        dateTimePickerDate.Visible = false;
                        labelDate.Visible = false;

                        comboBoxUser.Visible = false;
                        labelUser.Visible = false;

                        dateTimePickerStartDate.Visible = false;
                        labelStartDate.Visible = false;

                        dateTimePickerEndDate.Visible = false;
                        labelEndDate.Visible = false;
                        comboBoxTerminal.Focus();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Please select a report.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
            {
                yield return day;
            }
        }

        private void buttonView_Click(object sender, EventArgs e)
        {
            if (listBoxPOSReport.SelectedItem != null)
            {
                Decimal declareRate = Modules.SysCurrentModule.GetCurrentSettings().DeclareRate;

                String selectedItem = listBoxPOSReport.SelectedItem.ToString();
                switch (selectedItem)
                {
                    case "Z Reading Report":
                        sysUserRights = new Modules.SysUserRightsModule("RepPOS (Z Reading)");

                        if (sysUserRights.GetUserRights() == null)
                        {
                            MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (sysUserRights.GetUserRights().CanView == true)
                            {
                                RepZReadingReportForm repZReadingReportForm = new RepZReadingReportForm(this, Convert.ToInt32(comboBoxTerminal.SelectedValue), Convert.ToDateTime(dateTimePickerDate.Value.ToShortDateString()));
                                repZReadingReportForm.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }

                        break;
                    case "X Reading Report":
                        sysUserRights = new Modules.SysUserRightsModule("RepPOS (X Reading)");

                        if (sysUserRights.GetUserRights() == null)
                        {
                            MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (sysUserRights.GetUserRights().CanView == true)
                            {
                                RepXReadingReportForm repXReadingReportForm = new RepXReadingReportForm(this, Convert.ToInt32(comboBoxTerminal.SelectedValue), Convert.ToDateTime(dateTimePickerDate.Value.ToShortDateString()), Convert.ToInt32(comboBoxUser.SelectedValue));
                                repXReadingReportForm.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }

                        break;
                    case "E-Journal Report (ejournal.txt)":
                        DialogResult dialogResult = folderBrowserDialogSaveEJournal.ShowDialog();
                        if (dialogResult == DialogResult.OK)
                        {
                            String folderPath = folderBrowserDialogSaveEJournal.SelectedPath;

                            FileStream fs1 = new FileStream(folderPath + "\\ejournal" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
                            StreamWriter writer = new StreamWriter(fs1);

                            Controllers.RepPOSReportController repPOSReportController = new Controllers.RepPOSReportController();
                            if (repPOSReportController.ListCollections(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue)).Any())
                            {
                                var systemCurrent = Modules.SysCurrentModule.GetCurrentSettings();
                                foreach (var collection in repPOSReportController.ListCollections(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue)))
                                {
                                    writer.Write(systemCurrent.CompanyName + "\n");
                                    writer.Write(systemCurrent.Address + "\n");

                                    writer.Write("TIN: " + systemCurrent.TIN + "\n");
                                    writer.Write("S/N: " + systemCurrent.SerialNo + "\n");
                                    writer.Write("MIN: " + systemCurrent.MachineNo + "\n");
                                    writer.Write(systemCurrent.ORPrintTitle + "\n");

                                    String isCancelled = "";
                                    if (collection.IsCancelled == true)
                                    {
                                        isCancelled = "-CANCELLED";
                                    }

                                    writer.Write(collection.CollectionNumber + isCancelled + "\n");
                                    writer.Write("---------------------------------------------------------\n");

                                    if (collection.SalesId != null)
                                    {
                                        Decimal VATSales = 0;
                                        Decimal VATAmount = 0;
                                        Decimal NonVAT = 0;
                                        Decimal VATExempt = 0;
                                        Decimal VATZeroRated = 0;

                                        if (repPOSReportController.ListSalesLines(Convert.ToInt32(collection.SalesId)).Any())
                                        {
                                            Decimal totalSales = 0;
                                            Decimal totalNumberOfItems = 0;
                                            Decimal totalDiscount = 0;

                                            foreach (var salesLine in repPOSReportController.ListSalesLines(Convert.ToInt32(collection.SalesId)))
                                            {
                                                if (salesLine.MstTax.Code == "VAT")
                                                {
                                                    VATSales += (salesLine.Price * salesLine.Quantity) - ((salesLine.Price * salesLine.Quantity) / (1 + (salesLine.MstItem.MstTax1.Rate / 100)) * (salesLine.MstItem.MstTax1.Rate / 100));
                                                    VATAmount += salesLine.TaxAmount;
                                                }
                                                else if (salesLine.MstTax.Code == "NONVAT")
                                                {
                                                    NonVAT += salesLine.Price * salesLine.Quantity;
                                                }
                                                else if (salesLine.MstTax.Code == "EXEMPTVAT")
                                                {
                                                    if (salesLine.MstItem.MstTax1.Rate > 0)
                                                    {
                                                        VATExempt += (salesLine.Price * salesLine.Quantity) - ((salesLine.Price * salesLine.Quantity) / (1 + (salesLine.MstItem.MstTax1.Rate / 100)) * (salesLine.MstItem.MstTax1.Rate / 100));
                                                    }
                                                    else
                                                    {
                                                        VATExempt += salesLine.Price * salesLine.Quantity;
                                                    }
                                                }
                                                else if (salesLine.MstTax.Code == "ZEROVAT")
                                                {
                                                    VATZeroRated += salesLine.Amount;
                                                }

                                                String itemDescription = salesLine.MstItem.ItemDescription + " " + salesLine.Quantity.ToString("#,##0.00") + " " + salesLine.MstUnit.Unit + " @ " + salesLine.Price.ToString("#,#00.00") + " \t\t" + salesLine.Amount.ToString("#,#00.00");
                                                if (salesLine.MstItem.ItemDescription.Length >= 20)
                                                {
                                                    itemDescription = salesLine.MstItem.ItemDescription.Substring(0, 20) + " " + salesLine.Quantity.ToString("#,##0.00") + " " + salesLine.MstUnit.Unit + " @ " + salesLine.Price.ToString("#,#00.00") + " \t" + salesLine.Amount.ToString("#,#00.00");
                                                }
                                                else
                                                {
                                                    if (salesLine.MstItem.ItemDescription.Length <= 12)
                                                    {
                                                        itemDescription = salesLine.MstItem.ItemDescription + " " + salesLine.Quantity.ToString("#,##0.00") + " " + salesLine.MstUnit.Unit + " @ " + salesLine.Price.ToString("#,#00.00") + " \t\t\t" + salesLine.Amount.ToString("#,#00.00");
                                                    }
                                                    if (salesLine.MstItem.ItemDescription.Length <= 4)
                                                    {
                                                        itemDescription = salesLine.MstItem.ItemDescription + " " + salesLine.Quantity.ToString("#,##0.00") + " " + salesLine.MstUnit.Unit + " @ " + salesLine.Price.ToString("#,#00.00") + " \t\t\t\t" + salesLine.Amount.ToString("#,#00.00");
                                                    }
                                                }

                                                writer.Write(itemDescription);
                                                writer.Write("\n");

                                                totalSales += salesLine.Amount;
                                                totalNumberOfItems += 1;
                                                totalDiscount += salesLine.DiscountAmount;
                                            }

                                            writer.Write("---------------------------------------------------------\n");
                                            writer.Write("Total Sales: \t\t\t\t\t" + (totalSales * declareRate).ToString("#,#00.00"));
                                            writer.Write("\n");
                                            writer.Write("Total No. of Item(s): \t\t\t\t" + totalNumberOfItems.ToString("#,#00.00"));
                                            writer.Write("\n");
                                            writer.Write("Total Discount: \t\t\t\t" + (totalDiscount * declareRate).ToString("#,#00.00"));
                                            writer.Write("\n");
                                            writer.Write("---------------------------------------------------------\n");
                                        }

                                        if (repPOSReportController.ListCollectionLines(Convert.ToInt32(collection.Id)).Any())
                                        {
                                            foreach (var collectionLine in repPOSReportController.ListCollectionLines(Convert.ToInt32(collection.Id)))
                                            {
                                                writer.Write(collectionLine.PayType + ": \t\t\t\t\t\t" + (collectionLine.Amount * declareRate).ToString("#,##0.00"));
                                                writer.Write("\n");
                                            }
                                        }

                                        writer.Write("---------------------------------------------------------\n");
                                        writer.Write("VAT ANALYSIS\n");

                                        writer.Write("VAT Sales: \t\t\t\t\t" + (VATSales * declareRate).ToString("#,#00.00"));
                                        writer.Write("\n");
                                        writer.Write("VAT Amount: \t\t\t\t\t" + (VATAmount * declareRate).ToString("#,#00.00"));
                                        writer.Write("\n");
                                        writer.Write("Non-VAT: \t\t\t\t\t" + (NonVAT * declareRate).ToString("#,#00.00"));
                                        writer.Write("\n");
                                        writer.Write("VAT Exempt: \t\t\t\t\t" + (VATExempt * declareRate).ToString("#,#00.00"));
                                        writer.Write("\n");
                                        writer.Write("VAT Zero Rated: \t\t\t\t" + (VATZeroRated * declareRate).ToString("#,#00.00"));
                                        writer.Write("\n");

                                        writer.Write("---------------------------------------------------------\n");
                                        writer.Write("Cashier: " + collection.PreparedByUserName);
                                        writer.Write("\n");
                                        writer.Write("---------------------------------------------------------\n");
                                        writer.Write("Customer Name: \n");
                                        writer.Write("Address: \n");
                                        writer.Write("TIN: \n");
                                        writer.Write("Business Style: \n");
                                        writer.Write("---------------------------------------------------------\n");

                                        writer.Write(systemCurrent.ReceiptFooter);
                                    }

                                    writer.Write("\n\n\n\n\n");
                                }
                            }

                            writer.Close();
                        }

                        break;
                    case "E-Sales Report (esales.csv)":
                        DialogResult dialogResultESales = folderBrowserDialogESales.ShowDialog();
                        if (dialogResultESales == DialogResult.OK)
                        {
                            StringBuilder csv = new StringBuilder();
                            String[] header = {
                                "Terminal",
                                "Date",
                                "Gross Sales (Net of VAT)",
                                "Regular Discount",
                                "Senior Discount",
                                "PWD Discount",
                                "Sales Return",
                                "Net Sales",
                                "Total Collection",
                                "VAT Sales",
                                "VAT Amount",
                                "Non-VAT",
                                "VAT Exempt",
                                "VAT Zero Rated",
                                "Counter ID Start",
                                "Counter ID End",
                                "Cancelled Tx",
                                "Cancelled Amount",
                                "Accumulated Gross Sales (Net of VAT) - Previous Reading",
                                "Accumulated Gross Sales (Net of VAT) - Gross Sales",
                                "Accumulated Gross Sales (Net of VAT) - Running Total",
                                "Accumulated Net Sales - Previous Reading",
                                "Accumulated Net Sales - Net Sales",
                                "Accumulated Net Sales - Running Total",
                                "Z-Reading Counter"
                            };
                            csv.AppendLine(String.Join(",", header));

                            foreach (DateTime date in EachDay(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date))
                            {
                                Controllers.RepPOSReportController repPOSReportController = new Controllers.RepPOSReportController();
                                if (repPOSReportController.ZReadingESalesDataSource(Convert.ToInt32(comboBoxTerminal.SelectedValue), date) != null)
                                {
                                    var objData = repPOSReportController.ZReadingESalesDataSource(Convert.ToInt32(comboBoxTerminal.SelectedValue), date);

                                    String[] data = {
                                        objData.Terminal,
                                        date.ToShortDateString(),
                                        (objData.TotalGrossSales * declareRate).ToString("#,##0.00").Replace("," , ""),
                                        (objData.TotalRegularDiscount * declareRate).ToString("#,##0.00").Replace("," , ""),
                                        (objData.TotalSeniorDiscount * declareRate).ToString("#,##0.00").Replace("," , ""),
                                        (objData.TotalPWDDiscount * declareRate).ToString("#,##0.00").Replace("," , ""),
                                        (objData.TotalSalesReturn * declareRate).ToString("#,##0.00").Replace("," , ""),
                                        (objData.TotalNetSales * declareRate).ToString("#,##0.00").Replace("," , ""),
                                        (objData.TotalCollection * declareRate).ToString("#,##0.00").Replace("," , ""),
                                        (objData.TotalVATSales * declareRate).ToString("#,##0.00").Replace("," , ""),
                                        (objData.TotalVATAmount * declareRate).ToString("#,##0.00").Replace("," , ""),
                                        (objData.TotalNonVAT * declareRate).ToString("#,##0.00").Replace("," , ""),
                                        (objData.TotalVATExempt * declareRate).ToString("#,##0.00").Replace("," , ""),
                                        (objData.TotalVATZeroRated * declareRate).ToString("#,##0.00").Replace("," , ""),
                                        objData.CounterIdStart,
                                        objData.CounterIdEnd,
                                        objData.TotalCancelledTrnsactionCount.ToString("#,##0.00").Replace("," , ""),
                                        (objData.TotalCancelledAmount * declareRate).ToString("#,##0.00").Replace("," , ""),
                                        (objData.GrossSalesTotalPreviousReading * declareRate).ToString("#,##0.00").Replace("," , ""),
                                        (objData.TotalGrossSales * declareRate).ToString("#,##0.00").Replace("," , ""),
                                        (objData.GrossSalesRunningTotal * declareRate).ToString("#,##0.00").Replace("," , ""),
                                        (objData.NetSalesTotalPreviousReading * declareRate).ToString("#,##0.00").Replace("," , ""),
                                        (objData.TotalNetSales * declareRate).ToString("#,##0.00").Replace("," , ""),
                                        (objData.NetSalesRunningTotal * declareRate).ToString("#,##0.00").Replace("," , ""),
                                        objData.ZReadingCounter
                                    };
                                    csv.AppendLine(String.Join(",", data));
                                }
                            }

                            String executingUser = WindowsIdentity.GetCurrent().Name;

                            DirectorySecurity securityRules = new DirectorySecurity();
                            securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                            securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                            DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogESales.SelectedPath, securityRules);
                            File.WriteAllText(createDirectorySTCSV.FullName + "\\esales" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("iso-8859-1"));

                            MessageBox.Show("Generate CSV Successful!", "Generate CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                    case "Collection Register (collectionregister.csv)":
                        DialogResult dialogResultCollectionRegister = folderBrowserDialogCollectionRegister.ShowDialog();
                        if (dialogResultCollectionRegister == DialogResult.OK)
                        {
                            StringBuilder csv = new StringBuilder();
                            String[] header = {
                                "Terminal",
                                "Date",
                                "Collection Number",
                                "Customer Code",
                                "Customer",
                                "Amount",
                                "VAT Sales",
                                "VAT Amount",
                                "Non-VAT",
                                "VAT Exempt",
                                "VAT Zero Rated"
                            };
                            csv.AppendLine(String.Join(",", header));

                            Controllers.RepPOSReportController repPOSReportController = new Controllers.RepPOSReportController();
                            if (repPOSReportController.ListCollectionRegister(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue)).Any())
                            {
                                foreach (var collectionRegister in repPOSReportController.ListCollectionRegister(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue)))
                                {
                                    String customerCode = "";
                                    if (collectionRegister.CustomerCode != null)
                                    {
                                        customerCode = collectionRegister.CustomerCode.Replace(",", " ");
                                    }

                                    String[] data = {
                                        collectionRegister.Terminal,
                                        collectionRegister.CollectionDate,
                                        collectionRegister.CollectionNumber,
                                        customerCode,
                                        collectionRegister.Customer.Replace("," , ""),
                                        (collectionRegister.Amount * declareRate).ToString("#,#00.00").Replace("," , ""),
                                        (collectionRegister.VATSales * declareRate).ToString("#,#00.00").Replace("," , ""),
                                        (collectionRegister.VATAmount * declareRate).ToString("#,#00.00").Replace("," , ""),
                                        (collectionRegister.NonVAT * declareRate).ToString("#,#00.00").Replace("," , ""),
                                        (collectionRegister.VATExempt * declareRate).ToString("#,#00.00").Replace("," , ""),
                                        (collectionRegister.VATZeroRated * declareRate).ToString("#,#00.00").Replace("," , ""),
                                    };
                                    csv.AppendLine(String.Join(",", data));
                                }
                            }

                            String executingUser = WindowsIdentity.GetCurrent().Name;

                            DirectorySecurity securityRules = new DirectorySecurity();
                            securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                            securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                            DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogCollectionRegister.SelectedPath, securityRules);
                            File.WriteAllText(createDirectorySTCSV.FullName + "\\collectionregister" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("iso-8859-1"));

                            MessageBox.Show("Generate CSV Successful!", "Generate CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                    case "E-Sales Journal":
                        new RepPOSReportESalesJournalPDFForm(Convert.ToInt32(comboBoxTerminal.SelectedValue), dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date);
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
    }
}
