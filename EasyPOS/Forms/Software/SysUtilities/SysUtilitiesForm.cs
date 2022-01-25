using PagedList;
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

namespace EasyPOS.Forms.Software.SysUtilities
{
    public partial class SysUtilitiesForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;

        public static List<Entities.DgvSysSysUtilitiesAuditTrailListEntity> auditTrailListData = new List<Entities.DgvSysSysUtilitiesAuditTrailListEntity>();
        public static Int32 pageNumber = 1;
        public static Int32 pageSize = 50;
        public PagedList<Entities.DgvSysSysUtilitiesAuditTrailListEntity> auditTrailListPageList = new PagedList<Entities.DgvSysSysUtilitiesAuditTrailListEntity>(auditTrailListData, pageNumber, pageSize);
        public BindingSource audiTrailListDataSource = new BindingSource();

        public static List<Entities.DgvSysUtilitiesBarcodePrintingItemList> itemListData = new List<Entities.DgvSysUtilitiesBarcodePrintingItemList>();
        public static Int32 itemListPageNumber = 1;
        public static Int32 itemListPageSize = 50;
        public PagedList<Entities.DgvSysUtilitiesBarcodePrintingItemList> itemListPageList = new PagedList<Entities.DgvSysUtilitiesBarcodePrintingItemList>(itemListData, pageNumber, pageSize);
        public BindingSource itemListDataSource = new BindingSource();
        public static List<Entities.DgvMstItemListEntity> itemLineData = new List<Entities.DgvMstItemListEntity>();
        public RepInventoryReport.RepInventoryItemListReportForm _mstItemForm;
        public SysUtilitiesForm(SysSoftwareForm softwareForm, RepInventoryReport.RepInventoryItemListReportForm mstItemListForm)
        {
            InitializeComponent();
            sysSoftwareForm = softwareForm;
            _mstItemForm = mstItemListForm;

            GetUserList();
            CreateItemListDataGridView();
        }
        public List<Entities.MstItemEntity> itemList = new List<Entities.MstItemEntity>();
        public List<Entities.MstCustomerEntity> CustomerList = new List<Entities.MstCustomerEntity>();

        public void UpdateAuditTrailListDataSource()
        {
            DateTime startDateFilter = dateTimePickerSysAuditTrailListStartDateFilter.Value.Date;
            DateTime endDateFilter = dateTimePickerSysAuditTrailListEndDateFilter.Value.Date;
            Int32 userId = 0;
            if (comboBoxUserFilter.SelectedValue != null)
            {
                userId = Convert.ToInt32(comboBoxUserFilter.SelectedValue);
            }

            SetAuditTrailListDataSourceAsync(startDateFilter, endDateFilter, userId);
        }

        public void GetUserList()
        {
            Controllers.SysAuditTrailController sysAuditTrailController = new Controllers.SysAuditTrailController();
            var users = sysAuditTrailController.DropDownUserList();
            if (users.Any())
            {
                comboBoxUserFilter.DataSource = users;
                comboBoxUserFilter.ValueMember = "Id";
                comboBoxUserFilter.DisplayMember = "FullName";

                CreateAuditTrailListDataGridView();
            }
        }

        public async void SetAuditTrailListDataSourceAsync(DateTime startDate, DateTime endDate, Int32 userId)
        {
            List<Entities.DgvSysSysUtilitiesAuditTrailListEntity> getAuditTrailListData = await GetAuditTrailListDataTask(startDate, endDate, userId);
            if (getAuditTrailListData.Any())
            {
                auditTrailListData = getAuditTrailListData;
                auditTrailListPageList = new PagedList<Entities.DgvSysSysUtilitiesAuditTrailListEntity>(auditTrailListData, pageNumber, pageSize);

                if (auditTrailListPageList.PageCount == 1)
                {
                    buttonAuditTrailListPageListFirst.Enabled = false;
                    buttonAuditTrailListPageListPrevious.Enabled = false;
                    buttonAuditTrailListPageListNext.Enabled = false;
                    buttonAuditTrailListPageListLast.Enabled = false;
                }
                else if (pageNumber == 1)
                {
                    buttonAuditTrailListPageListFirst.Enabled = false;
                    buttonAuditTrailListPageListPrevious.Enabled = false;
                    buttonAuditTrailListPageListNext.Enabled = true;
                    buttonAuditTrailListPageListLast.Enabled = true;
                }
                else if (pageNumber == auditTrailListPageList.PageCount)
                {
                    buttonAuditTrailListPageListFirst.Enabled = true;
                    buttonAuditTrailListPageListPrevious.Enabled = true;
                    buttonAuditTrailListPageListNext.Enabled = false;
                    buttonAuditTrailListPageListLast.Enabled = false;
                }
                else
                {
                    buttonAuditTrailListPageListFirst.Enabled = true;
                    buttonAuditTrailListPageListPrevious.Enabled = true;
                    buttonAuditTrailListPageListNext.Enabled = true;
                    buttonAuditTrailListPageListLast.Enabled = true;
                }

                textBoxAuditTrailListPageNumber.Text = pageNumber + " / " + auditTrailListPageList.PageCount;
                audiTrailListDataSource.DataSource = auditTrailListPageList;
            }
            else
            {
                buttonAuditTrailListPageListFirst.Enabled = false;
                buttonAuditTrailListPageListPrevious.Enabled = false;
                buttonAuditTrailListPageListNext.Enabled = false;
                buttonAuditTrailListPageListLast.Enabled = false;

                pageNumber = 1;

                auditTrailListData = new List<Entities.DgvSysSysUtilitiesAuditTrailListEntity>();
                audiTrailListDataSource.Clear();
                textBoxAuditTrailListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvSysSysUtilitiesAuditTrailListEntity>> GetAuditTrailListDataTask(DateTime startDate, DateTime endDate, Int32 userId)
        {


            Controllers.SysAuditTrailController sysAuditTrailController = new Controllers.SysAuditTrailController();

            List<Entities.SysAuditTrailEntity> listAuditTrail = sysAuditTrailController.ListAuditTrail(startDate, endDate, userId);
            if (listAuditTrail.Any())
            {
                var auditTrail = from d in listAuditTrail
                                 select new Entities.DgvSysSysUtilitiesAuditTrailListEntity
                                 {
                                     ColumnAuditTrailListId = d.Id,
                                     ColumnAuditTrailListUserId = d.UserId,
                                     ColumnAuditTrailListUser = d.User,
                                     ColumnAuditTrailListAuditDate = d.AuditDate.ToString(),
                                     ColumnAuditTrailListTableInformation = d.TableInformation,
                                     ColumnAuditTrailListRecordInformation = d.RecordInformation,
                                     ColumnAuditTrailListFormInformation = d.FormInformation,
                                     ColumnAuditTrailListActionInformation = d.ActionInformation
                                 };
                return Task.FromResult(auditTrail.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvSysSysUtilitiesAuditTrailListEntity>());
            }
        }

        public void CreateAuditTrailListDataGridView()
        {
            UpdateAuditTrailListDataSource();

            dataGridViewAuditTrailList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewAuditTrailList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewAuditTrailList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewAuditTrailList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewAuditTrailList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewAuditTrailList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewAuditTrailList.DataSource = audiTrailListDataSource;

            GetItemBarcodeList();
        }


        private void buttonClose_Click(object sender, EventArgs e)
        {
            sysSoftwareForm.RemoveTabPage();
        }

        private void dateTimePickerStockOutListFilter_ValueChanged(object sender, EventArgs e)
        {
            UpdateAuditTrailListDataSource();
        }

        private void textBoxStockOutListFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateAuditTrailListDataSource();
            }
        }

        private void buttonAudiTrailListPageListLastFirst_Click(object sender, EventArgs e)
        {
            auditTrailListPageList = new PagedList<Entities.DgvSysSysUtilitiesAuditTrailListEntity>(auditTrailListData, 1, pageSize);
            audiTrailListDataSource.DataSource = auditTrailListPageList;

            buttonAuditTrailListPageListFirst.Enabled = false;
            buttonAuditTrailListPageListPrevious.Enabled = false;
            buttonAuditTrailListPageListNext.Enabled = true;
            buttonAuditTrailListPageListLast.Enabled = true;

            pageNumber = 1;
            textBoxAuditTrailListPageNumber.Text = pageNumber + " / " + auditTrailListPageList.PageCount;
        }

        private void buttonAudiTrailListPageListLastPrevious_Click(object sender, EventArgs e)
        {
            if (auditTrailListPageList.HasPreviousPage == true)
            {
                auditTrailListPageList = new PagedList<Entities.DgvSysSysUtilitiesAuditTrailListEntity>(auditTrailListData, --pageNumber, pageSize);
                audiTrailListDataSource.DataSource = auditTrailListPageList;
            }

            buttonAuditTrailListPageListNext.Enabled = true;
            buttonAuditTrailListPageListLast.Enabled = true;

            if (pageNumber == 1)
            {
                buttonAuditTrailListPageListFirst.Enabled = false;
                buttonAuditTrailListPageListPrevious.Enabled = false;
            }

            textBoxAuditTrailListPageNumber.Text = pageNumber + " / " + auditTrailListPageList.PageCount;
        }

        private void buttonAudiTrailListPageListLastNext_Click(object sender, EventArgs e)
        {
            if (auditTrailListPageList.HasNextPage == true)
            {
                auditTrailListPageList = new PagedList<Entities.DgvSysSysUtilitiesAuditTrailListEntity>(auditTrailListData, ++pageNumber, pageSize);
                audiTrailListDataSource.DataSource = auditTrailListPageList;
            }

            buttonAuditTrailListPageListFirst.Enabled = true;
            buttonAuditTrailListPageListPrevious.Enabled = true;

            if (pageNumber == auditTrailListPageList.PageCount)
            {
                buttonAuditTrailListPageListNext.Enabled = false;
                buttonAuditTrailListPageListLast.Enabled = false;
            }

            textBoxAuditTrailListPageNumber.Text = pageNumber + " / " + auditTrailListPageList.PageCount;
        }

        private void buttonAudiTrailListPageListLastLast_Click(object sender, EventArgs e)
        {
            auditTrailListPageList = new PagedList<Entities.DgvSysSysUtilitiesAuditTrailListEntity>(auditTrailListData, auditTrailListPageList.PageCount, pageSize);
            audiTrailListDataSource.DataSource = auditTrailListPageList;

            buttonAuditTrailListPageListFirst.Enabled = true;
            buttonAuditTrailListPageListPrevious.Enabled = true;
            buttonAuditTrailListPageListNext.Enabled = false;
            buttonAuditTrailListPageListLast.Enabled = false;

            pageNumber = auditTrailListPageList.PageCount;
            textBoxAuditTrailListPageNumber.Text = pageNumber + " / " + auditTrailListPageList.PageCount;
        }

        private void AuditTrailList_Filter(object sender, EventArgs e)
        {
            UpdateAuditTrailListDataSource();
        }

        private void dateTimePickerSysAuditTrailListEndDateFilter_ValueChanged(object sender, EventArgs e)
        {
            UpdateAuditTrailListDataSource();
        }

        private void dateTimePickerSysAuditTrailListStartDateFilter_ValueChanged(object sender, EventArgs e)
        {
            UpdateAuditTrailListDataSource();
        }

        private void buttonView_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = folderBrowserDialogGenerateCSV.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {


                    StringBuilder csv = new StringBuilder();
                    String[] header = { "Date", "User", "Module", "Action Taken", "Old Value", "New Value" };
                    csv.AppendLine(String.Join(",", header));

                    if (auditTrailListPageList.Any())
                    {
                        foreach (var auditTrail in auditTrailListPageList)
                        {
                            String[] data = {auditTrail.ColumnAuditTrailListAuditDate,
                                        auditTrail.ColumnAuditTrailListUser.Replace("," , ""),
                                        auditTrail.ColumnAuditTrailListTableInformation.Replace("," , "-"),
                                        auditTrail.ColumnAuditTrailListRecordInformation.Replace("," , "-"),
                                        auditTrail.ColumnAuditTrailListFormInformation.Replace("," , "-"),
                                        auditTrail.ColumnAuditTrailListActionInformation.Replace("," , "-")
                            };

                            csv.AppendLine(String.Join(",", data));
                        }
                    }

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\AuditSummaryReport_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("iso-8859-1"));

                    MessageBox.Show("Generate CSV Successful!", "Generate CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateItemListDataSource()
        {
            SetItemListDataSourceAsync();
        }

        public async void SetItemListDataSourceAsync()
        {
            List<Entities.DgvSysUtilitiesBarcodePrintingItemList> getItemListData = await GetItemListDataTask();
            if (getItemListData.Any())
            {
                itemListData = getItemListData;
                itemListPageList = new PagedList<Entities.DgvSysUtilitiesBarcodePrintingItemList>(itemListData, itemListPageNumber, itemListPageSize);

                if (itemListPageList.PageCount == 1)
                {
                    buttonItemListPageListFirst.Enabled = false;
                    buttonItemListPageListPrevious.Enabled = false;
                    buttonItemListPageListNext.Enabled = false;
                    buttonItemListPageListLast.Enabled = false;
                }
                else if (itemListPageNumber == 1)
                {
                    buttonItemListPageListFirst.Enabled = false;
                    buttonItemListPageListPrevious.Enabled = false;
                    buttonItemListPageListNext.Enabled = true;
                    buttonItemListPageListLast.Enabled = true;
                }
                else if (itemListPageNumber == itemListPageList.PageCount)
                {
                    buttonItemListPageListFirst.Enabled = true;
                    buttonItemListPageListPrevious.Enabled = true;
                    buttonItemListPageListNext.Enabled = false;
                    buttonItemListPageListLast.Enabled = false;
                }
                else
                {
                    buttonItemListPageListFirst.Enabled = true;
                    buttonItemListPageListPrevious.Enabled = true;
                    buttonItemListPageListNext.Enabled = true;
                    buttonItemListPageListLast.Enabled = true;
                }

                textBoxItemListPageNumber.Text = itemListPageNumber + " / " + itemListPageList.PageCount;
                itemListDataSource.DataSource = itemListPageList;
            }
            else
            {
                buttonItemListPageListFirst.Enabled = false;
                buttonItemListPageListPrevious.Enabled = false;
                buttonItemListPageListNext.Enabled = false;
                buttonItemListPageListLast.Enabled = false;

                itemListPageNumber = 1;

                itemListData = new List<Entities.DgvSysUtilitiesBarcodePrintingItemList>();
                itemListDataSource.Clear();
                textBoxItemListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvSysUtilitiesBarcodePrintingItemList>> GetItemListDataTask()
        {
            String filter = textBoxItemListFilter.Text;
            Controllers.MstItemController mstItemController = new Controllers.MstItemController();

            List<Entities.MstItemEntity> listItem = mstItemController.UtilitiesListItem(filter);
            if (listItem.Any())
            {
                var items = from d in listItem
                            select new Entities.DgvSysUtilitiesBarcodePrintingItemList
                            {
                                ColumnItemListButtonPick = "Pick",
                                ColumnItemListId = d.Id,
                                ColumnItemListCode = d.ItemCode,
                                ColumnItemListDescription = d.ItemDescription,
                                ColumnItemListBarcode = d.BarCode,
                                ColumnItemListUnit = d.Unit,
                                ColumnItemListCategory = d.Category,
                                ColumnItemListAlias = d.Alias,
                                ColumnItemListPrice = d.Price.ToString("#,##0.00"),
                                ColumnItemListIsInventory = d.IsInventory,
                                ColumnItemListIsLocked = d.IsLocked
                            };

                return Task.FromResult(items.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvSysUtilitiesBarcodePrintingItemList>());
            }
        }

        public void CreateItemListDataGridView()
        {
            UpdateItemListDataSource();

            dataGridViewItemList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewItemList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewItemList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewItemList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewItemList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewItemList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewItemList.DataSource = itemListDataSource;

            GetItemBarcodeList();
        }

        public void GetItemListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void dataGridViewItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetItemListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewItemList.CurrentCell.ColumnIndex == dataGridViewItemList.Columns["ColumnItemListButtonPick"].Index)
            {
                Int32 itemId = Convert.ToInt32(dataGridViewItemList.Rows[dataGridViewItemList.CurrentCell.RowIndex].Cells[dataGridViewItemList.Columns["ColumnItemListId"].Index].Value);
                SysBarcodePrintingQuantity sysUtilitiesBarcodePrintingQuantity = new SysBarcodePrintingQuantity(itemId);
                sysUtilitiesBarcodePrintingQuantity.ShowDialog();
            }
        }

        private void textBoxItemListFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateItemListDataSource();
            }
        }

        private void buttonItemListPageListFirst_Click(object sender, EventArgs e)
        {
            itemListPageList = new PagedList<Entities.DgvSysUtilitiesBarcodePrintingItemList>(itemListData, 1, itemListPageSize);
            itemListDataSource.DataSource = itemListPageList;

            buttonItemListPageListFirst.Enabled = false;
            buttonItemListPageListPrevious.Enabled = false;
            buttonItemListPageListNext.Enabled = true;
            buttonItemListPageListLast.Enabled = true;

            itemListPageNumber = 1;
            textBoxItemListPageNumber.Text = itemListPageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonItemListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (itemListPageList.HasPreviousPage == true)
            {
                itemListPageList = new PagedList<Entities.DgvSysUtilitiesBarcodePrintingItemList>(itemListData, --itemListPageNumber, itemListPageSize);
                itemListDataSource.DataSource = itemListPageList;
            }

            buttonItemListPageListNext.Enabled = true;
            buttonItemListPageListLast.Enabled = true;

            if (itemListPageNumber == 1)
            {
                buttonItemListPageListFirst.Enabled = false;
                buttonItemListPageListPrevious.Enabled = false;
            }

            textBoxItemListPageNumber.Text = itemListPageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonItemListPageListNext_Click(object sender, EventArgs e)
        {
            if (itemListPageList.HasNextPage == true)
            {
                itemListPageList = new PagedList<Entities.DgvSysUtilitiesBarcodePrintingItemList>(itemListData, ++itemListPageNumber, itemListPageSize);
                itemListDataSource.DataSource = itemListPageList;
            }

            buttonItemListPageListFirst.Enabled = true;
            buttonItemListPageListPrevious.Enabled = true;

            if (itemListPageNumber == itemListPageList.PageCount)
            {
                buttonItemListPageListNext.Enabled = false;
                buttonItemListPageListLast.Enabled = false;
            }

            textBoxItemListPageNumber.Text = itemListPageNumber + " / " + itemListPageList.PageCount;
        }

        private void buttonItemListPageListLast_Click(object sender, EventArgs e)
        {
            itemListPageList = new PagedList<Entities.DgvSysUtilitiesBarcodePrintingItemList>(itemListData, itemListPageList.PageCount, itemListPageSize);
            itemListDataSource.DataSource = itemListPageList;

            buttonItemListPageListFirst.Enabled = true;
            buttonItemListPageListPrevious.Enabled = true;
            buttonItemListPageListNext.Enabled = false;
            buttonItemListPageListLast.Enabled = false;

            itemListPageNumber = itemListPageList.PageCount;
            textBoxItemListPageNumber.Text = itemListPageNumber + " / " + itemListPageList.PageCount;
        }
        //==============================
        //Upload Item Data Export Format
        //==============================
        private void buttonExport_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = folderBrowserDialogGenerateCSV.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    StringBuilder csv = new StringBuilder();
                    String[] header = {
                        "Barcode",
                        "Item Description",
                        "Alias",
                        "Category",
                        "Unit",
                        "Default Supplier",
                        "Cost",
                        "Mark Up",
                        "Price",
                        "Stock Level Quantity",
                        "On-hand Quantity",
                        "IsInventory",
                        "Generic Name",
                        "Sales VAT",
                        "Remarks",
                        "Package",
                        "Expiry Date",
                        "Lot Number",
                    };

                    csv.AppendLine(String.Join(",", header));

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\ItemUploadFormat" + ".csv", csv.ToString(), Encoding.GetEncoding("iso-8859-1"));

                    MessageBox.Show("Generate CSV Successful!", "Generate CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //=========================
        //Upload Data Export Format
        //=========================
        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult openFileDialogImportCSVResult = openFileDialogImportCSV.ShowDialog();
                if (openFileDialogImportCSVResult == DialogResult.OK)
                {
                    itemList = new List<Entities.MstItemEntity>();

                    textBoxFileName.Text = openFileDialogImportCSV.FileName;

                    string[] lines = File.ReadAllLines(textBoxFileName.Text);
                    if (lines.Length > 0)
                    {
                        for (int i = 1; i < lines.Length; i++)
                        {
                            string[] dataWords = lines[i].Split(',');

                            itemList.Add(new Entities.MstItemEntity()
                            {
                                BarCode = dataWords[0],
                                ItemDescription = dataWords[1],
                                Alias = dataWords[2],
                                Category = dataWords[3],
                                Unit = dataWords[4],
                                Supplier = dataWords[5],
                                Cost = Convert.ToDecimal(dataWords[6]),
                                MarkUp = Convert.ToDecimal(dataWords[7]),
                                Price = Convert.ToDecimal(dataWords[8]),
                                ReorderQuantity = Convert.ToDecimal(dataWords[9]),
                                OnhandQuantity = Convert.ToDecimal(dataWords[10]),
                                IsInventory = Convert.ToBoolean(dataWords[11]),
                                GenericName = dataWords[12],
                                OutTax = dataWords[13],
                                Remarks = dataWords[14],
                                IsPackage = Convert.ToBoolean(dataWords[15]),
                                ExpiryDate = dataWords[16],
                                LotNumber = dataWords[17],
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            try
            {
                Controllers.MstItemController mstItemController = new Controllers.MstItemController();
                String[] addItem = mstItemController.ImportItem(itemList);
                if (addItem[1].Equals("0") == false)
                {
                    MessageBox.Show("Upload Successfully");

                }
                else
                {
                    MessageBox.Show(addItem[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //==================================
        //Upload Customer Data Export Format
        //==================================
        private void buttonExportCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = folderBrowserDialogGenerateCSV.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    StringBuilder csv = new StringBuilder();
                    String[] header = {
                        "Code",
                        "Customer",
                        "Address",
                        "Contact Person",
                        "Contact No.",
                        "Credit Limit",
                        "Term",
                        "TIN",
                        "With Reward",
                        "Reward No.",
                        "Reward Conversion",
                        "Default Price",
                    };

                    csv.AppendLine(String.Join(",", header));

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\CustomerUploadFormat" + ".csv", csv.ToString(), Encoding.GetEncoding("iso-8859-1"));

                    MessageBox.Show("Generate CSV Successful!", "Generate CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //=========================
        //Upload Data Export Format
        //=========================
        private void buttonOpenCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult openFileDialogImportCSVResult = openFileDialogImportCSV.ShowDialog();
                if (openFileDialogImportCSVResult == DialogResult.OK)
                {
                    CustomerList = new List<Entities.MstCustomerEntity>();

                    textBoxFileNameCustomer.Text = openFileDialogImportCSV.FileName;

                    string[] lines = File.ReadAllLines(textBoxFileNameCustomer.Text);
                    if (lines.Length > 0)
                    {
                        for (int i = 1; i < lines.Length; i++)
                        {
                            string[] dataWords = lines[i].Split(',');

                            CustomerList.Add(new Entities.MstCustomerEntity()
                            {
                                CustomerCode = dataWords[0],
                                Customer = dataWords[1],
                                Address = dataWords[2],
                                ContactPerson = dataWords[3],
                                ContactNumber = dataWords[4],
                                CreditLimit = Convert.ToDecimal(dataWords[5]) ,
                                Term = dataWords[6],
                                TIN = dataWords[7],
                                WithReward = Convert.ToBoolean(dataWords[8]),
                                RewardNumber = dataWords[9],
                                RewardConversion = Convert.ToDecimal(dataWords[10]),
                                DefaultPriceDescription = dataWords[11],
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCustomerImport_Click(object sender, EventArgs e)
        {
            try
            {
                Controllers.MstCustomerController mstCustomerController = new Controllers.MstCustomerController();
                String[] addCustomer = mstCustomerController.ImportCustomer(CustomerList);
                if (addCustomer[1].Equals("0") == false)
                {
                    MessageBox.Show("Upload Successfully");
                }
                else
                {
                    MessageBox.Show(addCustomer[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUpdateExport_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = folderBrowserDialogGenerateCSV.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    StringBuilder csv = new StringBuilder();
                    String[] header = {
                        "Barcode",
                        "Item Description",
                        "Unit",
                        "Cost",
                        "Price"
                    };

                    csv.AppendLine(String.Join(",", header));

                    Controllers.RepInventoryReportController repInvetoryReportController = new Controllers.RepInventoryReportController();

                    var inventoryListReport = repInvetoryReportController.GetInventoryListReport();


                    //dog + "cat"
                    if (inventoryListReport.Any())
                    {
                        foreach (var stockInLine in inventoryListReport)
                        {
                            
                            String[] data = {
                              //"\""+stockInLine.BarCode.ToString()+"\"",
                              "="+"\""+stockInLine.BarCode+"\"",
                              stockInLine.ItemDescription.Replace(",", ""),
                              stockInLine.Unit,
                              Convert.ToString(stockInLine.Cost).Replace(",", ""),
                              Convert.ToString(stockInLine.Price).Replace(",", ""),
                            };

                            csv.AppendLine(String.Join(",", data));
                        }
                    }

                    String executingUser = WindowsIdentity.GetCurrent().Name;

                    DirectorySecurity securityRules = new DirectorySecurity();
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.Read, AccessControlType.Allow));
                    securityRules.AddAccessRule(new FileSystemAccessRule(executingUser, FileSystemRights.FullControl, AccessControlType.Allow));

                    DirectoryInfo createDirectorySTCSV = Directory.CreateDirectory(folderBrowserDialogGenerateCSV.SelectedPath, securityRules);
                    File.WriteAllText(createDirectorySTCSV.FullName + "\\ExportedItem" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".csv", csv.ToString(), Encoding.GetEncoding("iso-8859-1"));

                    MessageBox.Show("Generate CSV Successful!", "Generate CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonOpenUpdatePrice_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult openFileDialogImportCSVResult = openFileDialogImportCSV.ShowDialog();
                if (openFileDialogImportCSVResult == DialogResult.OK)
                {
                    itemList = new List<Entities.MstItemEntity>();

                    textBoxUpdatePrice.Text = openFileDialogImportCSV.FileName;

                    string[] lines = File.ReadAllLines(textBoxUpdatePrice.Text);
                    if (lines.Length > 0)
                    {
                        for (int i = 1; i < lines.Length; i++)
                        {
                            string[] dataWords = lines[i].Split(',');

                            itemList.Add(new Entities.MstItemEntity()
                            {
                                BarCode = dataWords[0],
                                ItemDescription = dataWords[1],
                                Unit = dataWords[2],
                                Cost = Convert.ToDecimal(dataWords[3]),
                                Price = Convert.ToDecimal(dataWords[4]),
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                Controllers.MstItemController mstItemController = new Controllers.MstItemController();
                String[] updateItemPrice = mstItemController.UpdateItemPrice(itemList);
                if (updateItemPrice[1].Equals("0") == false)
                {
                    MessageBox.Show("Updated Successfully", "Easy POS", MessageBoxButtons.OK,MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show(updateItemPrice[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void GetItemBarcodeList()
        {
            Controllers.MstItemController mstItemController = new Controllers.MstItemController();
            var items = mstItemController.DropdownListRecalculateItemInventoryList();
            if (items.Any())
            {
                cboBarcode.DataSource = items;
                cboBarcode.ValueMember = "Id";
                cboBarcode.DisplayMember = "BarCode";
            }

            GetItemDescriptionList();
        }
        public void GetItemDescriptionList()
        {
            Controllers.MstItemController mstItemController= new Controllers.MstItemController();
            var itemdesc = mstItemController.DropdownListRecalculateItemInventoryList();
            if (itemdesc.Any())
            {
                cboItemDescription.DataSource = itemdesc;
                cboItemDescription.ValueMember = "Id";
                cboItemDescription.DisplayMember = "ItemDescription";
            }
        }
        private void buttonRecalculate_Click(object sender, EventArgs e)
        {
            try
            {
                Controllers.MstItemController mstItemController = new Controllers.MstItemController();
                String[] updateItemInventory = mstItemController.UpdateItemInventory(Convert.ToInt32(cboBarcode.SelectedValue));
                if (updateItemInventory[1].Equals("0") == false)
                {
                    MessageBox.Show("Updated Successfully", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show(updateItemInventory[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboBarcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboBarcode.SelectedIndex > -1)
            {
                cboItemDescription.SelectedValue = cboBarcode.SelectedValue;
            }
        }

        private void cboItemDescription_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboItemDescription.SelectedIndex > -1)
            {
                cboBarcode.SelectedValue = cboItemDescription.SelectedValue;
            }
        }
    }
}
