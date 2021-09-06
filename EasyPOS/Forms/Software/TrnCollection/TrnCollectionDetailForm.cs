using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.TrnCollection
{
    public partial class TrnCollectionDetailForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;
        public TrnCollectionListForm trnCollectionListForm;
        public Entities.TrnCollectionEntity trnCollectionEntity;

        public static List<Entities.DgvTrnCollectionLineListEntity> collectionLineData = new List<Entities.DgvTrnCollectionLineListEntity>();
        public static Int32 CollectionLinePageNumber = 1;
        public static Int32 CollectionLinePageSize = 50;
        public PagedList<Entities.DgvTrnCollectionLineListEntity> collectionLinePageList = new PagedList<Entities.DgvTrnCollectionLineListEntity>(collectionLineData, CollectionLinePageNumber, CollectionLinePageSize);
        public BindingSource collectionLineDataSource = new BindingSource();
        public TrnCollectionDetailForm(SysSoftwareForm softwareForm, TrnCollectionListForm collectionListForm, Entities.TrnCollectionEntity collectionEntity)
        {
            InitializeComponent();
            sysSoftwareForm = softwareForm;

            sysUserRights = new Modules.SysUserRightsModule("TrnCollectionDetail");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                trnCollectionListForm = collectionListForm;
                trnCollectionEntity = collectionEntity;

                GetTerminal();
            }

            var id = trnCollectionEntity.Id;

            Controllers.TrnCollectionController trnCollection = new Controllers.TrnCollectionController();
            var detail = trnCollection.DetailCollection(id);

            if (detail != null)
            {
                sysSoftwareForm.displayTimeStamp(detail.EntryUserName, detail.EntryDateTime + " " + detail.EntryTime, detail.UpdatedUserName, detail.UpdateDateTime + " " + detail.UpdateTime);
            }
        }

        public void GetTerminal()
        {
            Controllers.TrnCollectionController trnCollectionController = new Controllers.TrnCollectionController();
            if (trnCollectionController.DropdownListCollectionTerminalId().Any())
            {
                comboBoxTerminal.DataSource = trnCollectionController.DropdownListCollectionTerminalId();
                comboBoxTerminal.ValueMember = "Id";
                comboBoxTerminal.DisplayMember = "Terminal";

                GetCustomer();
            }
        }
        public void GetCustomer()
        {
            Controllers.TrnCollectionController trnCollectionController = new Controllers.TrnCollectionController();
            if (trnCollectionController.DropdownListCollectionCustomer().Any())
            {
                comboBoxCustomer.DataSource = trnCollectionController.DropdownListCollectionCustomer();
                comboBoxCustomer.ValueMember = "Id";
                comboBoxCustomer.DisplayMember = "Customer";

                GetSalesNumber(trnCollectionEntity.CustomerId, trnCollectionEntity.TerminalId);
            }
        }

        public void GetSalesNumber(Int32 customerId, Int32 terminalId)
        {
            Controllers.TrnCollectionController trnCollectionController = new Controllers.TrnCollectionController();
            if (trnCollectionController.DropdownListCollectionSalesNumberByCustomer(customerId, terminalId).Any())
            {
                comboBoxSalesNumber.DataSource = trnCollectionController.DropdownListCollectionSalesNumberByCustomer(customerId, terminalId);
                comboBoxSalesNumber.ValueMember = "Id";
                comboBoxSalesNumber.DisplayMember = "SalesNumber";
            }

            GetUserList();
        }

        public void GetUserList()
        {
            Controllers.TrnCollectionController trnCollectionController = new Controllers.TrnCollectionController();
            if (trnCollectionController.DropdownListCollectionUser().Any())
            {
                comboBoxPreparedBy.DataSource = trnCollectionController.DropdownListCollectionUser();
                comboBoxPreparedBy.ValueMember = "Id";
                comboBoxPreparedBy.DisplayMember = "FullName";

                comboBoxCheckedBy.DataSource = trnCollectionController.DropdownListCollectionUser();
                comboBoxCheckedBy.ValueMember = "Id";
                comboBoxCheckedBy.DisplayMember = "FullName";

                comboBoxApprovedBy.DataSource = trnCollectionController.DropdownListCollectionUser();
                comboBoxApprovedBy.ValueMember = "Id";
                comboBoxApprovedBy.DisplayMember = "FullName";

                GetCollectionDetail();
            }
        }

        public void GetCollectionDetail()
        {
            UpdateComponents(trnCollectionEntity.IsLocked);

            textBoxCollectionNumber.Text = trnCollectionEntity.CollectionNumber;
            dateTimePickerCollectionDate.Value = Convert.ToDateTime(trnCollectionEntity.CollectionDate);
            textBoxManualORNumber.Text = trnCollectionEntity.ManualORNumber;
            comboBoxTerminal.SelectedValue = trnCollectionEntity.TerminalId;
            comboBoxCustomer.SelectedValue = trnCollectionEntity.CustomerId;
            comboBoxSalesNumber.SelectedText = trnCollectionEntity.SalesNumber;
            textBoxSalesBalance.Text = trnCollectionEntity.SalesBalanceAmount.ToString("#,#00.00");
            textBoxTotalCollectionLineAmount.Text = trnCollectionEntity.Amount.ToString("#,#00.00");
            textBoxRemarks.Text = trnCollectionEntity.Remarks;
            comboBoxPreparedBy.SelectedValue = trnCollectionEntity.PreparedBy;
            comboBoxCheckedBy.SelectedValue = trnCollectionEntity.CheckedBy;
            comboBoxApprovedBy.SelectedValue = trnCollectionEntity.ApprovedBy;

            CreateCollectionLineListDataGridView();
        }

        public void UpdateComponents(Boolean isLocked)
        {
            if (sysUserRights.GetUserRights().CanLock == false)
            {
                buttonLock.Enabled = false;
            }
            else
            {
                buttonLock.Enabled = !isLocked;
            }

            if (sysUserRights.GetUserRights().CanUnlock == false)
            {
                buttonUnlock.Enabled = false;
            }
            else
            {
                buttonUnlock.Enabled = isLocked;
            }

            if (sysUserRights.GetUserRights().CanPrint == false)
            {
                buttonPrint.Enabled = false;
            }
            else
            {
                buttonPrint.Enabled = isLocked;
            }

            dateTimePickerCollectionDate.Enabled = !isLocked;
            textBoxManualORNumber.Enabled = !isLocked;
            comboBoxTerminal.Enabled = !isLocked;
            comboBoxCustomer.Enabled = !isLocked;
            comboBoxSalesNumber.Enabled = !isLocked;
            textBoxSalesBalance.Enabled = !isLocked;
            textBoxTotalCollectionLineAmount.Enabled = !isLocked;
            textBoxRemarks.Enabled = !isLocked;
            comboBoxPreparedBy.Enabled = !isLocked;
            comboBoxCheckedBy.Enabled = !isLocked;
            comboBoxApprovedBy.Enabled = !isLocked;

            dataGridViewCollectionLineList.Columns[0].Visible = !isLocked;
            dataGridViewCollectionLineList.Columns[1].Visible = !isLocked;
            dateTimePickerCollectionDate.Focus();
        }

        private void buttonLock_Click(object sender, EventArgs e)
        {
            try
            {
                Controllers.TrnCollectionController trnCollectionController = new Controllers.TrnCollectionController();

                Entities.TrnCollectionEntity newCollectionEntity = new Entities.TrnCollectionEntity()
                {
                    Id = trnCollectionEntity.Id,
                    PeriodId = 1,
                    CollectionDate = dateTimePickerCollectionDate.Value.Date.ToShortDateString(),
                    ManualORNumber = textBoxManualORNumber.Text,
                    TerminalId = Convert.ToInt32(comboBoxTerminal.SelectedValue),
                    CustomerId = Convert.ToInt32(comboBoxCustomer.SelectedValue),
                    SalesId = Convert.ToInt32(comboBoxSalesNumber.SelectedValue),
                    SalesNumber = comboBoxSalesNumber.Text,
                    SalesBalanceAmount = Convert.ToDecimal(textBoxSalesBalance.Text),
                    Amount = Convert.ToDecimal(textBoxTotalCollectionLineAmount.Text),
                    Remarks = textBoxRemarks.Text,
                    PreparedBy = Convert.ToInt32(comboBoxPreparedBy.SelectedValue),
                    CheckedBy = Convert.ToInt32(comboBoxCheckedBy.SelectedValue),
                    ApprovedBy = Convert.ToInt32(comboBoxApprovedBy.SelectedValue),
                };

                String[] lockCollection = trnCollectionController.LockCollection(trnCollectionEntity.Id, newCollectionEntity);
                if (lockCollection[1].Equals("0") == false)
                {
                    UpdateComponents(true);
                    trnCollectionListForm.UpdateCollectionListDataSource();
                }
                else
                {
                    MessageBox.Show(lockCollection[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buttonUnlock_Click(object sender, EventArgs e)
        {
            Controllers.TrnCollectionController trnCollectionController = new Controllers.TrnCollectionController();

            String[] unlockCollection = trnCollectionController.UnlockCollection(trnCollectionEntity.Id);
            if (unlockCollection[1].Equals("0") == false)
            {
                UpdateComponents(false);
                trnCollectionListForm.UpdateCollectionListDataSource();
            }
            else
            {
                MessageBox.Show(unlockCollection[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void UpdateCollectionListDataSource()
        {
            SetCollectionLineListDataSourceAsync();
        }

        public async void SetCollectionLineListDataSourceAsync()
        {
            List<Entities.DgvTrnCollectionLineListEntity> getCollectionLineListData = await GetCollectionLineListDataTask();
            if (getCollectionLineListData.Any())
            {
                collectionLineData = getCollectionLineListData;
                collectionLinePageList = new PagedList<Entities.DgvTrnCollectionLineListEntity>(collectionLineData, CollectionLinePageNumber, CollectionLinePageSize);

                if (collectionLinePageList.PageCount == 1)
                {
                    buttonCollectionLineListPageListFirst.Enabled = false;
                    buttonCollectionLineListPageListPrevious.Enabled = false;
                    buttonCollectionLineListPageListNext.Enabled = false;
                    buttonCollectionLineListPageListLast.Enabled = false;
                }
                else if (CollectionLinePageNumber == 1)
                {
                    buttonCollectionLineListPageListFirst.Enabled = false;
                    buttonCollectionLineListPageListPrevious.Enabled = false;
                    buttonCollectionLineListPageListNext.Enabled = true;
                    buttonCollectionLineListPageListLast.Enabled = true;
                }
                else if (CollectionLinePageNumber == collectionLinePageList.PageCount)
                {
                    buttonCollectionLineListPageListFirst.Enabled = true;
                    buttonCollectionLineListPageListPrevious.Enabled = true;
                    buttonCollectionLineListPageListNext.Enabled = false;
                    buttonCollectionLineListPageListLast.Enabled = false;
                }
                else
                {
                    buttonCollectionLineListPageListFirst.Enabled = true;
                    buttonCollectionLineListPageListPrevious.Enabled = true;
                    buttonCollectionLineListPageListNext.Enabled = true;
                    buttonCollectionLineListPageListLast.Enabled = true;
                }

                textBoxCollectionLineListPageNumber.Text = CollectionLinePageNumber + " / " + collectionLinePageList.PageCount;
                collectionLineDataSource.DataSource = collectionLinePageList;
            }
            else
            {
                buttonCollectionLineListPageListFirst.Enabled = false;
                buttonCollectionLineListPageListPrevious.Enabled = false;
                buttonCollectionLineListPageListNext.Enabled = false;
                buttonCollectionLineListPageListLast.Enabled = false;

                CollectionLinePageNumber = 1;

                collectionLineData = new List<Entities.DgvTrnCollectionLineListEntity>();
                collectionLineDataSource.Clear();
                textBoxCollectionLineListPageNumber.Text = "1 / 1";
            }
        }
        public Task<List<Entities.DgvTrnCollectionLineListEntity>> GetCollectionLineListDataTask()
        {
            Controllers.TrnCollectionLineController trnCollectionLineController = new Controllers.TrnCollectionLineController();

            List<Entities.TrnCollectionLineEntity> listCollectionLine = trnCollectionLineController.ListCollectionLine(trnCollectionEntity.Id);
            if (listCollectionLine.Any())
            {
                var items = from d in listCollectionLine
                            select new Entities.DgvTrnCollectionLineListEntity
                            {
                                ColumnCollectionLineListButtonEdit = "Edit",
                                ColumnCollectionLineListButtonDelete = "Delete",
                                ColumnCollectionLineListId = d.Id,
                                ColumnCollectionLineListCollectionId = d.CollectionId,
                                ColumnCollectionLineListPayTypeId = d.PayTypeId,
                                ColumnCollectionLineListPayType = d.PayType,
                                ColumnCollectionLineListAmount = d.Amount.ToString("#,##0.00"),
                                ColumnCollectionLineListCheckNumber = d.CheckNumber,
                                ColumnCollectionLineListCheckDate = d.CheckDate,
                                ColumnCollectionLineListCheckBank = d.CheckBank,
                                ColumnCollectionLineListCreditCardVerificationCode = d.CreditCardVerificationCode,
                                ColumnCollectionLineListCreditCardNumber = d.CreditCardNumber,
                                ColumnCollectionLineListCreditCardType = d.CreditCardType,
                                ColumnCollectionLineListCreditCardBank = d.CreditCardBank,
                                ColumnCollectionLineListGiftCertificateNumber = d.GiftCertificateNumber,
                                ColumnCollectionLineListOtherInformation = d.OtherInformation,
                                ColumnCollectionLineListSalesReturnSalesId = d.SalesReturnSalesId.ToString(),
                                ColumnCollectionLineListStockInId = d.StockInId.ToString(),
                                ColumnCollectionLineListAccountId = d.AccountId.ToString(),
                                ColumnCollectionLineListCreditCardReferenceNumber = d.CreditCardReferenceNumber,
                                ColumnCollectionLineListCreditCardHolderName = d.CreditCardHolderName,
                                ColumnCollectionLineListCreditCardExpiry = d.CreditCardExpiry
                            };

                Decimal amount = listCollectionLine.Sum(d => d.Amount);
                textBoxTotalCollectionLineAmount.Text = amount.ToString("#,##0.00");

                return Task.FromResult(items.ToList());
            }
            else
            {
                textBoxTotalCollectionLineAmount.Text = "0.00";
                return Task.FromResult(new List<Entities.DgvTrnCollectionLineListEntity>());
            }
        }
        public void CreateCollectionLineListDataGridView()
        {
            UpdateCollectionListDataSource();

            dataGridViewCollectionLineList.AutoGenerateColumns = false;

            dataGridViewCollectionLineList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewCollectionLineList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewCollectionLineList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewCollectionLineList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewCollectionLineList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
            dataGridViewCollectionLineList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewCollectionLineList.DataSource = collectionLineDataSource;
        }
        public void GetCollectionLineListCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void dataGridViewCollectionLineList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetCollectionLineListCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewCollectionLineList.CurrentCell.ColumnIndex == dataGridViewCollectionLineList.Columns["ColumnCollectionLineListButtonEdit"].Index)
            {
                String checkDate = "";

                if (dataGridViewCollectionLineList.Rows[e.RowIndex].Cells[dataGridViewCollectionLineList.Columns["ColumnCollectionLineListCheckDate"].Index].Value != null)
                {
                    checkDate = dataGridViewCollectionLineList.Rows[e.RowIndex].Cells[dataGridViewCollectionLineList.Columns["ColumnCollectionLineListCheckDate"].Index].Value.ToString();
                }

                var id = Convert.ToInt32(dataGridViewCollectionLineList.Rows[e.RowIndex].Cells[dataGridViewCollectionLineList.Columns["ColumnCollectionLineListId"].Index].Value);
                var collectionId = Convert.ToInt32(dataGridViewCollectionLineList.Rows[e.RowIndex].Cells[dataGridViewCollectionLineList.Columns["ColumnCollectionLineListCollectionId"].Index].Value);
                var amount = Convert.ToDecimal(dataGridViewCollectionLineList.Rows[e.RowIndex].Cells[dataGridViewCollectionLineList.Columns["ColumnCollectionLineListAmount"].Index].Value);
                var payTypeId = dataGridViewCollectionLineList.Rows[e.RowIndex].Cells[dataGridViewCollectionLineList.Columns["ColumnCollectionLineListPayTypeId"].Index].Value.ToString();
                var checkNumber = dataGridViewCollectionLineList.Rows[e.RowIndex].Cells[dataGridViewCollectionLineList.Columns["ColumnCollectionLineListCheckNumber"].Index].Value.ToString();
                var checkBank = dataGridViewCollectionLineList.Rows[e.RowIndex].Cells[dataGridViewCollectionLineList.Columns["ColumnCollectionLineListCheckBank"].Index].Value.ToString();
                var verificationCode = dataGridViewCollectionLineList.Rows[e.RowIndex].Cells[dataGridViewCollectionLineList.Columns["ColumnCollectionLineListCreditCardVerificationCode"].Index].Value.ToString();
                var creditCardNumber = dataGridViewCollectionLineList.Rows[e.RowIndex].Cells[dataGridViewCollectionLineList.Columns["ColumnCollectionLineListCreditCardNumber"].Index].Value.ToString();
                var creditCardHolderName = dataGridViewCollectionLineList.Rows[e.RowIndex].Cells[dataGridViewCollectionLineList.Columns["ColumnCollectionLineListCreditCardHolderName"].Index].Value.ToString();
                var creditCardReferenceNumber = dataGridViewCollectionLineList.Rows[e.RowIndex].Cells[dataGridViewCollectionLineList.Columns["ColumnCollectionLineListCreditCardReferenceNumber"].Index].Value.ToString();
                var creditCardExpiry = dataGridViewCollectionLineList.Rows[e.RowIndex].Cells[dataGridViewCollectionLineList.Columns["ColumnCollectionLineListCreditCardExpiry"].Index].Value.ToString();
                var creditCardType = dataGridViewCollectionLineList.Rows[e.RowIndex].Cells[dataGridViewCollectionLineList.Columns["ColumnCollectionLineListCreditCardType"].Index].Value.ToString();
                var creditCardBank = dataGridViewCollectionLineList.Rows[e.RowIndex].Cells[dataGridViewCollectionLineList.Columns["ColumnCollectionLineListCreditCardBank"].Index].Value.ToString();
                var giftCertificateNumber = dataGridViewCollectionLineList.Rows[e.RowIndex].Cells[dataGridViewCollectionLineList.Columns["ColumnCollectionLineListGiftCertificateNumber"].Index].Value.ToString();
                var otherInformation = dataGridViewCollectionLineList.Rows[e.RowIndex].Cells[dataGridViewCollectionLineList.Columns["ColumnCollectionLineListOtherInformation"].Index].Value.ToString();

                Entities.TrnCollectionLineEntity trnCollectionLineEntity = new Entities.TrnCollectionLineEntity()
                {
                    Id = id,
                    CollectionId = collectionId,
                    Amount = amount,
                    PayTypeId = Convert.ToInt32(payTypeId),
                    CheckNumber = checkNumber,
                    CheckDate = checkDate,
                    CheckBank = checkBank,
                    CreditCardVerificationCode = verificationCode,
                    CreditCardNumber = creditCardNumber,
                    CreditCardType = creditCardType,
                    CreditCardBank = creditCardBank,
                    GiftCertificateNumber = giftCertificateNumber,
                    OtherInformation = otherInformation,
                    CreditCardHolderName = creditCardHolderName,
                    CreditCardReferenceNumber = creditCardReferenceNumber,
                    CreditCardExpiry = creditCardExpiry
                };

                TrnCollectionLineDetailForm trnCollectionLineDetailForm = new TrnCollectionLineDetailForm(this, trnCollectionLineEntity);
                trnCollectionLineDetailForm.ShowDialog();
            }

            if (e.RowIndex > -1 && dataGridViewCollectionLineList.CurrentCell.ColumnIndex == dataGridViewCollectionLineList.Columns["ColumnCollectionLineListButtonDelete"].Index)
            {
                DialogResult deleteDialogResult = MessageBox.Show("Delete Collection?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (deleteDialogResult == DialogResult.Yes)
                {
                    var id = Convert.ToInt32(dataGridViewCollectionLineList.Rows[e.RowIndex].Cells[dataGridViewCollectionLineList.Columns["ColumnCollectionLineListId"].Index].Value);

                    Controllers.TrnCollectionLineController trnCollectionLineController = new Controllers.TrnCollectionLineController();
                    String[] deleteCollectionLine = trnCollectionLineController.DeleteCollectionLine(id);
                    if (deleteCollectionLine[1].Equals("0") == false)
                    {
                        CollectionLinePageNumber = 1;
                        UpdateCollectionListDataSource();
                    }
                    else
                    {
                        MessageBox.Show(deleteCollectionLine[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonCollectionLineListPageListFirst_Click(object sender, EventArgs e)
        {
            collectionLinePageList = new PagedList<Entities.DgvTrnCollectionLineListEntity>(collectionLineData, 1, CollectionLinePageSize);
            collectionLineDataSource.DataSource = collectionLinePageList;

            buttonCollectionLineListPageListFirst.Enabled = false;
            buttonCollectionLineListPageListPrevious.Enabled = false;
            buttonCollectionLineListPageListNext.Enabled = true;
            buttonCollectionLineListPageListLast.Enabled = true;

            CollectionLinePageNumber = 1;
            textBoxCollectionLineListPageNumber.Text = CollectionLinePageNumber + " / " + collectionLinePageList.PageCount;
        }

        private void buttonCollectionLineListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (collectionLinePageList.HasPreviousPage == true)
            {
                collectionLinePageList = new PagedList<Entities.DgvTrnCollectionLineListEntity>(collectionLineData, --CollectionLinePageNumber, CollectionLinePageSize);
                collectionLineDataSource.DataSource = collectionLinePageList;
            }

            buttonCollectionLineListPageListNext.Enabled = true;
            buttonCollectionLineListPageListLast.Enabled = true;

            if (CollectionLinePageNumber == 1)
            {
                buttonCollectionLineListPageListFirst.Enabled = false;
                buttonCollectionLineListPageListPrevious.Enabled = false;
            }

            textBoxCollectionLineListPageNumber.Text = CollectionLinePageNumber + " / " + collectionLinePageList.PageCount;
        }

        private void buttonCollectionLineListPageListNext_Click(object sender, EventArgs e)
        {
            if (collectionLinePageList.HasNextPage == true)
            {
                collectionLinePageList = new PagedList<Entities.DgvTrnCollectionLineListEntity>(collectionLineData, CollectionLinePageNumber, CollectionLinePageSize);
                collectionLineDataSource.DataSource = collectionLinePageList;
            }

            buttonCollectionLineListPageListFirst.Enabled = true;
            buttonCollectionLineListPageListPrevious.Enabled = true;

            if (CollectionLinePageNumber == collectionLinePageList.PageCount)
            {
                buttonCollectionLineListPageListNext.Enabled = false;
                buttonCollectionLineListPageListLast.Enabled = false;
            }

            textBoxCollectionLineListPageNumber.Text = CollectionLinePageNumber + " / " + collectionLinePageList.PageCount;
        }

        private void buttonCollectionLineListPageListLast_Click(object sender, EventArgs e)
        {
            collectionLinePageList = new PagedList<Entities.DgvTrnCollectionLineListEntity>(collectionLineData, collectionLinePageList.PageCount, CollectionLinePageSize);
            collectionLineDataSource.DataSource = collectionLinePageList;

            buttonCollectionLineListPageListFirst.Enabled = true;
            buttonCollectionLineListPageListPrevious.Enabled = true;
            buttonCollectionLineListPageListNext.Enabled = false;
            buttonCollectionLineListPageListLast.Enabled = false;

            CollectionLinePageNumber = collectionLinePageList.PageCount;
            textBoxCollectionLineListPageNumber.Text = CollectionLinePageNumber + " / " + collectionLinePageList.PageCount;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            sysSoftwareForm.RemoveTabPage();
        }

        private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCustomer.SelectedItem == null)
            {
                return;
            }

            var selectedItemCustomer = (Entities.MstCustomerEntity)comboBoxCustomer.SelectedItem;
            if (selectedItemCustomer != null)
            {
                comboBoxSalesNumber.Text = "";
                comboBoxSalesNumber.DataSource = new List<Entities.TrnSalesEntity>();

                trnCollectionEntity.SalesId = null;
                textBoxSalesBalance.Text = "0.00";

                Int32 terminalId = Convert.ToInt32(comboBoxTerminal.SelectedValue);
                Int32 customerId = selectedItemCustomer.Id;

                Controllers.TrnCollectionController trnCollectionController = new Controllers.TrnCollectionController();
                if (trnCollectionController.DropdownListCollectionSalesNumberByCustomer(customerId, terminalId).Any())
                {
                    comboBoxSalesNumber.DataSource = trnCollectionController.DropdownListCollectionSalesNumberByCustomer(customerId, terminalId);
                    comboBoxSalesNumber.ValueMember = "Id";
                    comboBoxSalesNumber.DisplayMember = "SalesNumber";

                    comboBoxSalesNumber.Text = trnCollectionController.DropdownListCollectionSalesNumberByCustomer(customerId, terminalId).FirstOrDefault().SalesNumber;
                }
            }
        }

        private void buttonAddCollectionLine_Click(object sender, EventArgs e)
        {
            Entities.TrnCollectionLineEntity trnCollectionLineEntity = new Entities.TrnCollectionLineEntity()
            {
                Id = 0,
                CollectionId = trnCollectionEntity.Id,
                Amount = 0,
                PayTypeId = 1,
                CheckNumber = "",
                CheckDate = "",
                CheckBank = "",
                CreditCardVerificationCode = "",
                CreditCardNumber = "",
                CreditCardType = "",
                CreditCardBank = "",
                GiftCertificateNumber = "",
                OtherInformation = "",
                CreditCardHolderName = "",
                CreditCardReferenceNumber = "",
                CreditCardExpiry = ""
            };

            TrnCollectionLineDetailForm trnCollectionLineDetailForm = new TrnCollectionLineDetailForm(this, trnCollectionLineEntity);
            trnCollectionLineDetailForm.ShowDialog();
        }

        private void comboBoxSalesNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSalesNumber.SelectedItem == null)
            {
                return;
            }

            var selectedSalesNumber = (Entities.TrnSalesEntity)comboBoxSalesNumber.SelectedItem;
            if (selectedSalesNumber != null)
            {
                textBoxSalesBalance.Text = selectedSalesNumber.BalanceAmount.ToString("#,##0.00");
            }
            else
            {
                textBoxSalesBalance.Text = "0.00";
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            new TrnPOS.TrnPOSOfficialReceiptReportForm(Convert.ToInt32(trnCollectionEntity.SalesId), trnCollectionEntity.Id, false, "Dot Matrix");
        }

        private void comboBoxCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                String inputString = comboBoxCustomer.Text;
                comboBoxCustomer.SelectedIndex = comboBoxCustomer.FindString(inputString);
            }

        }
    }
}
