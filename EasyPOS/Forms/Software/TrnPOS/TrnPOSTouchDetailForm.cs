using EasyPOS.Data;
using EasyPOS.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.TrnPOS
{
    public partial class TrnPOSTouchDetailForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;
        private Modules.SysUserRightsModule sysUserRights;

        public TrnPOSTouchForm trnPOSTouchForm;
        public Entities.TrnSalesEntity trnSalesEntity;
        public Entities.TrnSalesLineEntity salesLineEntity;

        private List<Entities.MstItemGroupEntity> listItemGroups = new List<Entities.MstItemGroupEntity>();
        private ToolTip itemGroupToolTip = new ToolTip();
        private const int itemGroupNoOfButtons = 6;
        private int itemGroupPages;
        private int itemGroupPage = 1;
        private Int32 selectedItemGroupId;

        private List<Entities.MstItemGroupItemEntity> listItemGroupItems = new List<Entities.MstItemGroupItemEntity>();
        private ToolTip itemGroupItemToolTip = new ToolTip();
        private const int itemGroupItemNoOfButtons = 30;
        private int itemGroupItemPages;
        private int itemGroupItemPage = 1;
        Button[] itemGroupItemButtons;

        public TrnPOSTouchDetailForm(SysSoftwareForm softwareForm, TrnPOSTouchForm POSTouchForm, Entities.TrnSalesEntity salesEntity)
        {
            InitializeComponent();

            sysSoftwareForm = softwareForm;
            trnPOSTouchForm = POSTouchForm;
            trnSalesEntity = salesEntity;

            sysUserRights = new Modules.SysUserRightsModule("TrnSalesDetail");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (sysUserRights.GetUserRights().CanLock == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonLock.Enabled = false;
                    }
                    else
                    {
                        buttonLock.Enabled = true;
                    }
                }
                else
                {
                    buttonLock.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanUnlock == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonUnlock.Enabled = true;
                    }
                    else
                    {
                        buttonUnlock.Enabled = false;
                    }
                }
                else
                {
                    buttonUnlock.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanReturn == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonReturn.Enabled = false;
                    }
                    else
                    {
                        buttonReturn.Enabled = true;
                    }
                }
                else
                {
                    buttonReturn.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanDiscount == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonDiscount.Enabled = false;
                    }
                    else
                    {
                        buttonDiscount.Enabled = true;
                    }
                }
                else
                {
                    buttonDiscount.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanTender == true)
                {
                    buttonTender.Enabled = true;
                }
                else
                {
                    buttonTender.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanPrint == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonPrint.Enabled = false;
                    }
                    else
                    {
                        buttonPrint.Enabled = true;
                    }
                }
                else
                {
                    buttonPrint.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanAdd == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonBarcode.Enabled = false;
                        textBoxBarcode.Enabled = false;
                        buttonSearchItem.Enabled = false;
                        buttonDownload.Enabled = false;
                    }
                    else
                    {
                        buttonBarcode.Enabled = true;
                        textBoxBarcode.Enabled = true;
                        buttonSearchItem.Enabled = true;
                        buttonDownload.Enabled = true;
                    }
                }
                else
                {
                    buttonBarcode.Enabled = false;
                    textBoxBarcode.Enabled = false;
                    buttonSearchItem.Enabled = false;
                    buttonDownload.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanEdit == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        dataGridViewSalesLineList.Columns[0].Visible = false;
                    }
                    else
                    {
                        dataGridViewSalesLineList.Columns[0].Visible = true;
                    }
                }
                else
                {
                    dataGridViewSalesLineList.Columns[0].Visible = true;
                }

                if (sysUserRights.GetUserRights().CanDelete == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        dataGridViewSalesLineList.Columns[1].Visible = false;
                    }
                    else
                    {
                        dataGridViewSalesLineList.Columns[1].Visible = true;
                    }
                }
                else
                {
                    dataGridViewSalesLineList.Columns[1].Visible = false;
                }

                GetSalesDetail();
                GetSalesLineList();
            }
            var id = trnSalesEntity.Id;

            Controllers.TrnSalesController newSales = new Controllers.TrnSalesController();
            var detail = newSales.DetailSales(id);

            if (detail != null)
            {
                sysSoftwareForm.displayTimeStamp(detail.EntryUserUserName, detail.EntryDateTime + " " + detail.EntryTime, detail.UpdatedUserUserName, detail.UpdateDateTime + " " + detail.UpdateTime);
            }

            Controllers.TrnSalesController trnSalesController = new Controllers.TrnSalesController();
            listItemGroups = trnSalesController.ListItemGroup();
            itemGroupPages = listItemGroups.Count();

            itemGroupItemButtons = new Button[] {
                buttonItemGroupItem1,
                buttonItemGroupItem2,
                buttonItemGroupItem3,
                buttonItemGroupItem4,
                buttonItemGroupItem5,
                buttonItemGroupItem6,
                buttonItemGroupItem7,
                buttonItemGroupItem8,
                buttonItemGroupItem9,
                buttonItemGroupItem10,
                buttonItemGroupItem11,
                buttonItemGroupItem12,
                buttonItemGroupItem13,
                buttonItemGroupItem14,
                buttonItemGroupItem15,
                buttonItemGroupItem16,
                buttonItemGroupItem17,
                buttonItemGroupItem18,
                buttonItemGroupItem19,
                buttonItemGroupItem20,
                buttonItemGroupItem21,
                buttonItemGroupItem22,
                buttonItemGroupItem23,
                buttonItemGroupItem24,
                buttonItemGroupItem25,
                buttonItemGroupItem26,
                buttonItemGroupItem27,
                buttonItemGroupItem28,
                buttonItemGroupItem29,
                buttonItemGroupItem30
            };

            for (int i = 0; i < itemGroupItemNoOfButtons; i++)
            {
                itemGroupItemButtons[i].Click += new EventHandler(buttonItemGroupItem_Click);

                if (trnSalesEntity.IsLocked == true)
                {
                    itemGroupItemButtons[i].Enabled = false;
                }
                else
                {
                    itemGroupItemButtons[i].Enabled = true;
                }
            }

            FillItemGroup();
        }

        private void FillItemGroup()
        {
            try
            {
                Button[] itemGroupButtons = new Button[] {
                    buttonItemGroup1,
                    buttonItemGroup2,
                    buttonItemGroup3,
                    buttonItemGroup4,
                    buttonItemGroup5,
                    buttonItemGroup6
                };

                for (int i = 0; i < itemGroupNoOfButtons; i++)
                {
                    itemGroupToolTip.SetToolTip(itemGroupButtons[i], "");
                    itemGroupButtons[i].Text = "";
                }

                var listItemGroupPage = listItemGroups.Skip((itemGroupPage - 1) * itemGroupNoOfButtons).Take(itemGroupNoOfButtons).ToList();
                if (listItemGroupPage.Any())
                {
                    for (int i = 0; i < listItemGroupPage.Count(); i++)
                    {
                        itemGroupToolTip.SetToolTip(itemGroupButtons[i], listItemGroupPage[i].Id.ToString());
                        itemGroupButtons[i].Text = listItemGroupPage[i].ItemGroup;
                    }

                    selectedItemGroupId = listItemGroupPage[0].Id;
                    FillItemGroupItem(listItemGroupPage[0].Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillItemGroupItem(Int32 itemGroupItemGroupId)
        {
            try
            {
                Controllers.TrnSalesController trnSalesController = new Controllers.TrnSalesController();

                listItemGroupItems = trnSalesController.ListItemGroupItem(itemGroupItemGroupId);
                itemGroupItemPages = listItemGroupItems.Count();

                for (int i = 0; i < itemGroupItemNoOfButtons; i++)
                {
                    itemGroupItemToolTip.SetToolTip(itemGroupItemButtons[i], "");
                    itemGroupItemButtons[i].Text = "";
                }

                var listItemGroupItemPage = listItemGroupItems.Skip((itemGroupItemPage - 1) * itemGroupItemNoOfButtons).Take(itemGroupItemNoOfButtons).ToList();
                if (listItemGroupItemPage.Any())
                {
                    for (int i = 0; i < listItemGroupItemPage.Count(); i++)
                    {
                        itemGroupItemToolTip.SetToolTip(itemGroupItemButtons[i], listItemGroupItemPage[i].Barcode.ToString());
                        itemGroupItemButtons[i].Text = listItemGroupItemPage[i].Alias;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonItemGroupItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Modules.SysCurrentModule.GetCurrentSettings().HideSalesItemDetail == false)
                {
                    Button b = sender as Button;
                    String barcode = itemGroupItemToolTip.GetToolTip(b);

                    Controllers.TrnSalesLineController trnPOSSalesLineController = new Controllers.TrnSalesLineController();

                    Entities.MstItemEntity detailItem = trnPOSSalesLineController.DetailItem(barcode);
                    if (detailItem != null)
                    {
                        Int32 ItemId = detailItem.Id;
                        Int32 SalesId = trnSalesEntity.Id;
                        String ItemDescription = detailItem.ItemDescription;
                        Int32 TaxId = detailItem.OutTaxId;
                        String Tax = detailItem.OutTax;
                        Decimal TaxRate = detailItem.OutTaxRate;
                        Decimal TaxAmount = detailItem.Price / (1 + (TaxRate / 100)) * (TaxRate / 100);
                        Int32 UnitId = detailItem.UnitId;
                        String Unit = detailItem.Unit;
                        Decimal Price = detailItem.Price;
                        Int32 DiscountId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().DefaultDiscountId);
                        Int32 UserId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId);

                        Entities.TrnSalesLineEntity trnSalesLineEntity = new Entities.TrnSalesLineEntity()
                        {
                            Id = 0,
                            SalesId = SalesId,
                            ItemId = ItemId,
                            ItemDescription = ItemDescription,
                            UnitId = UnitId,
                            Unit = Unit,
                            Price = Price,
                            DiscountId = DiscountId,
                            Discount = "",
                            DiscountRate = 0,
                            DiscountAmount = 0,
                            NetPrice = Price,
                            Quantity = 1,
                            Amount = Price,
                            TaxId = TaxId,
                            Tax = Tax,
                            TaxRate = TaxRate,
                            TaxAmount = TaxAmount,
                            SalesAccountId = 159,
                            AssetAccountId = 255,
                            CostAccountId = 238,
                            TaxAccountId = 87,
                            SalesLineTimeStamp = DateTime.Now.Date.ToShortDateString(),
                            UserId = UserId,
                            Preparation = "",
                            Price1 = 0,
                            Price2 = 0,
                            Price2LessTax = 0,
                            PriceSplitPercentage = 0,
                        };

                        TrnPOSSalesItemDetailForm trnSalesDetailSalesItemDetailForm = new TrnPOSSalesItemDetailForm(null, this, trnSalesLineEntity, null);
                        trnSalesDetailSalesItemDetailForm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Item not found.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    Button b = sender as Button;
                    String barcode = itemGroupItemToolTip.GetToolTip(b);
                    Controllers.TrnSalesLineController trnPOSSalesLineController = new Controllers.TrnSalesLineController();
                    trnPOSSalesLineController.BarcodeSalesLine(trnSalesEntity.Id, barcode);
                    GetSalesLineList();
                }


                //if (Modules.SysCurrentModule.GetCurrentSettings().IsBarcodeQuantityAlwaysOne == "True")
                //{
                //    trnPOSSalesLineController.BarcodeSalesLine(trnSalesEntity.Id, barcode);
                //    GetSalesLineList();
                //}
                //else
                //{

                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GetSalesDetail()
        {
            textBoxTotalSalesAmount.Text = trnSalesEntity.Amount.ToString("#,##0.00");
            labelInvoiceNumber.Text = trnSalesEntity.SalesNumber;
            labelInvoiceDate.Text = trnSalesEntity.SalesDate;
            labelCustomerCode.Text = trnSalesEntity.CustomerCode;
            labelCustomer.Text = trnSalesEntity.Customer;
            labelRemarks.Text = trnSalesEntity.Remarks;
            //labelTableNumber.Text = trnSalesEntity.Table;
        }

        private void buttonSearchItem_Click(object sender, EventArgs e)
        {
            TrnPOSSearchItemForm trnSalesDetailSearchItemForm = new TrnPOSSearchItemForm(null, this, trnSalesEntity);
            trnSalesDetailSearchItemForm.ShowDialog();
        }

        private void buttonBarcode_Click(object sender, EventArgs e)
        {
            textBoxBarcode.Focus();
            textBoxBarcode.SelectAll();
        }

        private void buttonTender_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(textBoxTotalSalesAmount.Text) > 0)
            {
                Entities.TrnSalesEntity newSalesEntity = new Entities.TrnSalesEntity
                {
                    Id = trnSalesEntity.Id,
                    Amount = Convert.ToDecimal(textBoxTotalSalesAmount.Text),
                    SalesNumber = trnSalesEntity.SalesNumber,
                    SalesDate = trnSalesEntity.SalesDate,
                    CustomerId = trnSalesEntity.CustomerId,
                    CustomerCode = trnSalesEntity.CustomerCode,
                    Customer = trnSalesEntity.Customer,
                    Remarks = trnSalesEntity.Remarks
                };

                TrnPOSTenderForm trnSalesDetailTenderForm = new TrnPOSTenderForm(sysSoftwareForm, null, null, trnPOSTouchForm, this, newSalesEntity);
                trnSalesDetailTenderForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Cannot tender zero amount.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDiscount_Click(object sender, EventArgs e)
        {
            Decimal salesAmount = 0;
            List<Entities.TrnSalesLineEntity> listSalesLine = new List<Entities.TrnSalesLineEntity>();

            foreach (DataGridViewRow row in dataGridViewSalesLineList.Rows)
            {
                Decimal price = Convert.ToDecimal(row.Cells[dataGridViewSalesLineList.Columns["ColumnSalesLinePrice"].Index].Value);
                Decimal quantity = Convert.ToDecimal(row.Cells[dataGridViewSalesLineList.Columns["ColumnSalesLineQuantity"].Index].Value);

                salesAmount += price * quantity;

                Int32 ItemId = Convert.ToInt32(row.Cells[4].Value);
                String ItemDescription = row.Cells[5].Value.ToString();
                Decimal Price = Convert.ToDecimal(row.Cells[9].Value);
                Decimal Quantity = Convert.ToDecimal(row.Cells[6].Value);

                listSalesLine.Add(new Entities.TrnSalesLineEntity()
                {
                    Id = 0,
                    SalesId = 0,
                    ItemId = ItemId,
                    ItemDescription = ItemDescription,
                    UnitId = 0,
                    Unit = "",
                    Price = Price,
                    DiscountId = 0,
                    Discount = "",
                    DiscountRate = 0,
                    DiscountAmount = 0,
                    NetPrice = 0,
                    Quantity = Quantity,
                    Amount = 0,
                    TaxId = 0,
                    Tax = "",
                    TaxRate = 0,
                    TaxAmount = 0,
                    SalesAccountId = 159,
                    AssetAccountId = 255,
                    CostAccountId = 238,
                    TaxAccountId = 87,
                    SalesLineTimeStamp = DateTime.Now.Date.ToShortDateString(),
                    UserId = 0,
                    Preparation = "",
                    Price1 = 0,
                    Price2 = 0,
                    Price2LessTax = 0,
                    PriceSplitPercentage = 0
                });
            }

            if (listSalesLine.Any())
            {
                var groupedSalesLine = from d in listSalesLine.ToList()
                                       group d by new
                                       {
                                           d.ItemId,
                                           d.ItemDescription
                                       } into g
                                       select new Entities.TrnSalesLineEntity()
                                       {
                                           Id = 0,
                                           SalesId = 0,
                                           ItemId = g.Key.ItemId,
                                           ItemDescription = g.Key.ItemDescription,
                                           UnitId = 0,
                                           Unit = "",
                                           Price = 0,
                                           DiscountId = 0,
                                           Discount = "",
                                           DiscountRate = 0,
                                           DiscountAmount = 0,
                                           NetPrice = 0,
                                           Quantity = 1,
                                           Amount = g.Sum(i => i.Price * i.Quantity),
                                           TaxId = 0,
                                           Tax = "",
                                           TaxRate = 0,
                                           TaxAmount = 0,
                                           SalesAccountId = 159,
                                           AssetAccountId = 255,
                                           CostAccountId = 238,
                                           TaxAccountId = 87,
                                           SalesLineTimeStamp = DateTime.Now.Date.ToShortDateString(),
                                           UserId = 0,
                                           Preparation = "",
                                           Price1 = 0,
                                           Price2 = 0,
                                           Price2LessTax = 0,
                                           PriceSplitPercentage = 0
                                       };

                TrnPOSDiscountForm trnSalesDetailDiscountForm = new TrnPOSDiscountForm(null, this, salesAmount, groupedSalesLine.ToList());
                trnSalesDetailDiscountForm.ShowDialog();
            }
            else
            {
                TrnPOSDiscountForm trnSalesDetailDiscountForm = new TrnPOSDiscountForm(null, this, salesAmount, new List<Entities.TrnSalesLineEntity>());
                trnSalesDetailDiscountForm.ShowDialog();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
            sysSoftwareForm.RemoveTabPage();
        }

        private void textBoxBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Controllers.TrnSalesLineController trnPOSSalesLineController = new Controllers.TrnSalesLineController();

                if (Modules.SysCurrentModule.GetCurrentSettings().IsBarcodeQuantityAlwaysOne == true)
                {
                    trnPOSSalesLineController.BarcodeSalesLine(trnSalesEntity.Id, textBoxBarcode.Text);
                    GetSalesLineList();
                }
                else
                {
                    Entities.MstItemEntity detailItem = trnPOSSalesLineController.DetailItem(textBoxBarcode.Text);
                    if (detailItem != null)
                    {
                        Int32 ItemId = detailItem.Id;
                        Int32 SalesId = trnSalesEntity.Id;
                        String ItemDescription = detailItem.ItemDescription;
                        Int32 TaxId = detailItem.OutTaxId;
                        String Tax = detailItem.OutTax;
                        Decimal TaxRate = detailItem.OutTaxRate;
                        Int32 UnitId = detailItem.UnitId;
                        String Unit = detailItem.Unit;
                        Decimal Price = detailItem.Price;
                        Int32 DiscountId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().DefaultDiscountId);
                        Int32 UserId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId);

                        Entities.TrnSalesLineEntity trnSalesLineEntity = new Entities.TrnSalesLineEntity()
                        {
                            Id = 0,
                            SalesId = SalesId,
                            ItemId = ItemId,
                            ItemDescription = ItemDescription,
                            UnitId = UnitId,
                            Unit = Unit,
                            Price = Price,
                            DiscountId = DiscountId,
                            Discount = "",
                            DiscountRate = 0,
                            DiscountAmount = 0,
                            NetPrice = Price,
                            Quantity = 1,
                            Amount = Price,
                            TaxId = TaxId,
                            Tax = Tax,
                            TaxRate = TaxRate,
                            TaxAmount = 0,
                            SalesAccountId = 159,
                            AssetAccountId = 255,
                            CostAccountId = 238,
                            TaxAccountId = 87,
                            SalesLineTimeStamp = DateTime.Now.Date.ToShortDateString(),
                            UserId = UserId,
                            Preparation = "",
                            Price1 = 0,
                            Price2 = 0,
                            Price2LessTax = 0,
                            PriceSplitPercentage = 0
                        };

                        TrnPOSSalesItemDetailForm trnSalesDetailSalesItemDetailForm = new TrnPOSSalesItemDetailForm(null, this, trnSalesLineEntity, null);
                        trnSalesDetailSalesItemDetailForm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Item not found.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                textBoxBarcode.SelectAll();
            }
        }

        public void GetSalesLineList()
        {
            Decimal totalSalesAmount = 0;

            dataGridViewSalesLineList.Rows.Clear();
            dataGridViewSalesLineList.Refresh();

            Controllers.TrnSalesLineController trnPOSSalesLineController = new Controllers.TrnSalesLineController();

            var salesLineList = trnPOSSalesLineController.ListSalesLine(trnSalesEntity.Id);
            if (salesLineList.Any())
            {
                dataGridViewSalesLineList.Columns[0].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
                dataGridViewSalesLineList.Columns[0].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
                dataGridViewSalesLineList.Columns[0].DefaultCellStyle.ForeColor = Color.White;

                dataGridViewSalesLineList.Columns[1].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F34F1C");
                dataGridViewSalesLineList.Columns[1].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#F34F1C");
                dataGridViewSalesLineList.Columns[1].DefaultCellStyle.ForeColor = Color.White;

                foreach (var objSalesLineList in salesLineList)
                {
                    totalSalesAmount += objSalesLineList.Amount;

                    dataGridViewSalesLineList.Rows.Add(
                        "Edit",
                        "Delete",                        
                        objSalesLineList.Id,
                        objSalesLineList.SalesId,
                        objSalesLineList.ItemId,
                        objSalesLineList.ItemDescription,
                        objSalesLineList.Quantity.ToString("#,##0.00"),
                        objSalesLineList.UnitId,
                        objSalesLineList.Unit,
                        objSalesLineList.Price.ToString("#,##0.00"),
                        objSalesLineList.DiscountId,
                        objSalesLineList.Discount,
                        objSalesLineList.DiscountRate.ToString("#,##0.00"),
                        objSalesLineList.DiscountAmount.ToString("#,##0.00"),
                        objSalesLineList.NetPrice.ToString("#,##0.00"),
                        objSalesLineList.Amount.ToString("#,##0.00"),
                        objSalesLineList.TaxId,
                        objSalesLineList.Tax,
                        objSalesLineList.TaxRate.ToString("#,##0.00"),
                        objSalesLineList.TaxAmount.ToString("#,##0.00"),
                        objSalesLineList.SalesAccountId,
                        objSalesLineList.AssetAccountId,
                        objSalesLineList.CostAccountId,
                        objSalesLineList.TaxAccountId,
                        objSalesLineList.SalesLineTimeStamp,
                        objSalesLineList.UserId,
                        objSalesLineList.Preparation,
                        objSalesLineList.Price1.ToString("#,##0.00"),
                        objSalesLineList.Price2.ToString("#,##0.00"),
                        objSalesLineList.Price2LessTax.ToString("#,##0.00"),
                        objSalesLineList.PriceSplitPercentage.ToString("#,##0.00")
                    );
                }
            }

            textBoxTotalSalesAmount.Text = totalSalesAmount.ToString("#,##0.00");
            trnSalesEntity.Amount = totalSalesAmount;

            String line1 = Modules.SysCurrentModule.GetCurrentSettings().CustomerDisplayFirstLineMessage;
            String line2 = "P " + textBoxTotalSalesAmount.Text;

            if (totalSalesAmount > 0)
            {
                line1 = "TOTAL:";
            }

            Modules.SysSerialPortModule.WriteSeralPortMessage(line1, line2);
            Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
            textBoxLastChange.Text = trnPOSSalesController.GetLastChange(Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().TerminalId)).ToString("#,##0.00");
        }

        private void dataGridViewSalesLineList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && dataGridViewSalesLineList.CurrentCell.ColumnIndex == dataGridViewSalesLineList.Columns["ColumnSalesLineEdit"].Index)
            {
                Int32 Id = Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[2].Value);
                Int32 SalesId = Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[3].Value);
                Int32 ItemId = Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[4].Value);
                String ItemDescription = dataGridViewSalesLineList.Rows[e.RowIndex].Cells[5].Value.ToString();
                Decimal Quantity = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[6].Value);
                Int32 UnitId = Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[7].Value);
                String Unit = dataGridViewSalesLineList.Rows[e.RowIndex].Cells[8].Value.ToString();
                Decimal Price = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[9].Value);
                Int32 DiscountId = Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[10].Value);
                String Discount = dataGridViewSalesLineList.Rows[e.RowIndex].Cells[11].Value.ToString(); ;
                Decimal DiscountRate = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[12].Value);
                Decimal DiscountAmount = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[13].Value);
                Decimal NetPrice = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[14].Value);
                Decimal Amount = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[15].Value);
                Int32 TaxId = Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[16].Value);
                String Tax = dataGridViewSalesLineList.Rows[e.RowIndex].Cells[17].Value.ToString();
                Decimal TaxRate = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[18].Value);
                Decimal TaxAmount = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[19].Value);
                Int32 SalesAccountId = Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[20].Value);
                Int32 AssetAccountId = Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[21].Value);
                Int32 CostAccountId = Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[22].Value);
                Int32 TaxAccountId = Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[23].Value);
                String SalesLineTimeStamp = dataGridViewSalesLineList.Rows[e.RowIndex].Cells[24].Value.ToString();
                Int32 UserId = Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[25].Value);
                String Preparation = dataGridViewSalesLineList.Rows[e.RowIndex].Cells[26].Value.ToString();
                Decimal Price1 = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[27].Value);
                Decimal Price2 = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[28].Value);
                Decimal Price2LessTax = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[29].Value);
                Decimal PriceSplitPercentage = Convert.ToDecimal(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[30].Value);
                

                Entities.TrnSalesLineEntity trnSalesLineEntity = new Entities.TrnSalesLineEntity()
                {
                    Id = Id,
                    SalesId = SalesId,
                    ItemId = ItemId,
                    ItemDescription = ItemDescription,
                    UnitId = UnitId,
                    Unit = Unit,
                    Price = Price,
                    DiscountId = DiscountId,
                    Discount = Discount,
                    DiscountRate = DiscountRate,
                    DiscountAmount = DiscountAmount,
                    NetPrice = NetPrice,
                    Quantity = Quantity,
                    Amount = Amount,
                    TaxId = TaxId,
                    Tax = Tax,
                    TaxRate = TaxRate,
                    TaxAmount = TaxAmount,
                    SalesAccountId = SalesAccountId,
                    AssetAccountId = AssetAccountId,
                    CostAccountId = CostAccountId,
                    TaxAccountId = TaxAccountId,
                    SalesLineTimeStamp = SalesLineTimeStamp,
                    UserId = UserId,
                    Preparation = Preparation,
                    Price1 = Price1,
                    Price2 = Price2,
                    Price2LessTax = Price2LessTax,
                    PriceSplitPercentage = PriceSplitPercentage,
                };

                TrnPOSSalesItemDetailForm trnSalesDetailSalesItemDetailForm = new TrnPOSSalesItemDetailForm(null, this, trnSalesLineEntity, null);
                trnSalesDetailSalesItemDetailForm.ShowDialog();
            }

            if (e.RowIndex > -1 && dataGridViewSalesLineList.CurrentCell.ColumnIndex == dataGridViewSalesLineList.Columns["ColumnSalesLineDelete"].Index)
            {
                DialogResult deleteDialogResult = MessageBox.Show("Delete Sales?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (deleteDialogResult == DialogResult.Yes)
                {
                    Controllers.TrnSalesLineController trnPOSSalesLineController = new Controllers.TrnSalesLineController();

                    String[] deleteSalesLine = trnPOSSalesLineController.DeleteSalesLine(Convert.ToInt32(dataGridViewSalesLineList.Rows[e.RowIndex].Cells[2].Value));
                    if (deleteSalesLine[1].Equals("0") == false)
                    {
                        GetSalesLineList();
                    }
                    else
                    {
                        MessageBox.Show(deleteSalesLine[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void LockComponents(Boolean isLocked)
        {
            buttonSearchItem.Enabled = !isLocked;
            buttonDownload.Enabled = !isLocked;
            buttonPrint.Enabled = !isLocked;
            buttonLock.Enabled = !isLocked;
            buttonUnlock.Enabled = isLocked;
            buttonReturn.Enabled = !isLocked;
            buttonDiscount.Enabled = !isLocked;
            buttonBarcode.Enabled = !isLocked;
            textBoxBarcode.Enabled = !isLocked;

            dataGridViewSalesLineList.Columns[0].Visible = !isLocked;
            dataGridViewSalesLineList.Columns[1].Visible = !isLocked;

            for (int i = 0; i < itemGroupItemNoOfButtons; i++)
            {
                itemGroupItemButtons[i].Enabled = !isLocked;
            }

            trnPOSTouchForm.UpdateSalesListGridDataSource();
        }

        private void buttonLock_Click(object sender, EventArgs e)
        {
            TrnPOSLockSalesForm trnPOSLockSalesForm = new TrnPOSLockSalesForm(null, this, trnSalesEntity);
            trnPOSLockSalesForm.ShowDialog();
        }

        private void buttonUnlock_Click(object sender, EventArgs e)
        {
            Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
            String[] lockSales = trnPOSSalesController.UnlockSales(trnSalesEntity.Id);
            if (lockSales[1].Equals("0") == false)
            {
                LockComponents(false);
            }
            else
            {
                MessageBox.Show(lockSales[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdatePOSTouchSalesListDataSource()
        {
            trnPOSTouchForm.UpdateSalesListGridDataSource();
        }

        private void buttonItemGroupPrevious_Click(object sender, EventArgs e)
        {
            itemGroupPage--;
            if (itemGroupPage == 0)
            {
                itemGroupPage = 1;
            }

            itemGroupItemPage = 1;

            FillItemGroup();
        }

        private void buttonItemGroupNext_Click(object sender, EventArgs e)
        {
            itemGroupPage++;

            Int32 modulosPage = itemGroupPages % itemGroupNoOfButtons;
            Int32 maximumNoOfPages = (itemGroupPages - modulosPage) / itemGroupNoOfButtons;

            if (modulosPage > 0)
            {
                maximumNoOfPages += 1;
            }

            if (itemGroupPage > maximumNoOfPages)
            {
                itemGroupPage = maximumNoOfPages;
            }

            itemGroupItemPage = 1;

            FillItemGroup();
        }

        private void buttonItemGroupItemPrevious_Click(object sender, EventArgs e)
        {
            itemGroupItemPage--;
            if (itemGroupItemPage == 0)
            {
                itemGroupItemPage = 1;
            }

            FillItemGroupItem(selectedItemGroupId);
        }

        private void buttonItemGroupItemNext_Click(object sender, EventArgs e)
        {
            itemGroupItemPage++;

            Int32 modulosPage = itemGroupItemPages % itemGroupItemNoOfButtons;
            Int32 maximumNoOfPages = (itemGroupItemPages - modulosPage) / itemGroupItemNoOfButtons;

            if (modulosPage > 0)
            {
                maximumNoOfPages += 1;
            }

            if (itemGroupItemPage > maximumNoOfPages)
            {
                itemGroupItemPage = maximumNoOfPages;
            }

            FillItemGroupItem(selectedItemGroupId);
        }

        private void buttonItemGroup1_Click(object sender, EventArgs e)
        {
            itemGroupItemPage = 1;

            if (itemGroupToolTip.GetToolTip(buttonItemGroup1) != "")
            {
                Int32 itemGroupId = Convert.ToInt32(itemGroupToolTip.GetToolTip(buttonItemGroup1));
                selectedItemGroupId = itemGroupId;
                FillItemGroupItem(itemGroupId);
            }
        }

        private void buttonItemGroup2_Click(object sender, EventArgs e)
        {
            itemGroupItemPage = 1;

            if (itemGroupToolTip.GetToolTip(buttonItemGroup2) != "")
            {
                Int32 itemGroupId = Convert.ToInt32(itemGroupToolTip.GetToolTip(buttonItemGroup2));
                selectedItemGroupId = itemGroupId;
                FillItemGroupItem(itemGroupId);
            }
        }

        private void buttonItemGroup3_Click(object sender, EventArgs e)
        {
            itemGroupItemPage = 1;

            if (itemGroupToolTip.GetToolTip(buttonItemGroup3) != "")
            {
                Int32 itemGroupId = Convert.ToInt32(itemGroupToolTip.GetToolTip(buttonItemGroup3));
                selectedItemGroupId = itemGroupId;
                FillItemGroupItem(itemGroupId);
            }
        }

        private void buttonItemGroup4_Click(object sender, EventArgs e)
        {
            itemGroupItemPage = 1;

            if (itemGroupToolTip.GetToolTip(buttonItemGroup4) != "")
            {
                Int32 itemGroupId = Convert.ToInt32(itemGroupToolTip.GetToolTip(buttonItemGroup4));
                selectedItemGroupId = itemGroupId;
                FillItemGroupItem(itemGroupId);
            }
        }

        private void buttonItemGroup5_Click(object sender, EventArgs e)
        {
            itemGroupItemPage = 1;

            if (itemGroupToolTip.GetToolTip(buttonItemGroup5) != "")
            {
                Int32 itemGroupId = Convert.ToInt32(itemGroupToolTip.GetToolTip(buttonItemGroup5));
                selectedItemGroupId = itemGroupId;
                FillItemGroupItem(itemGroupId);
            }
        }

        private void buttonItemGroup6_Click(object sender, EventArgs e)
        {
            itemGroupItemPage = 1;

            if (itemGroupToolTip.GetToolTip(buttonItemGroup6) != "")
            {
                Int32 itemGroupId = Convert.ToInt32(itemGroupToolTip.GetToolTip(buttonItemGroup6));
                selectedItemGroupId = itemGroupId;
                FillItemGroupItem(itemGroupId);
            }
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            TrnPOSDownloadItemsForm TrnPOSDownloadItemsForm = new TrnPOSDownloadItemsForm(sysSoftwareForm, null, this, trnSalesEntity.Id);
            TrnPOSDownloadItemsForm.ShowDialog();
        }

        private void buttonReturn_Click(object sender, EventArgs e)
        {
            TrnPOSReturn trnPOSReturn = new TrnPOSReturn(null, this);
            trnPOSReturn.ShowDialog();
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            TrnPOSTouchPrintOrderDetailForm order = new TrnPOSTouchPrintOrderDetailForm(trnSalesEntity);
            order.Show();
            //if (Modules.SysCurrentModule.GetCurrentSettings().ChoosePrinter == true)
            //{
            //    DialogResult printDialogResult = printDialogSelectPrinter.ShowDialog();
            //    if (printDialogResult == DialogResult.OK)
            //    {
            //        if (trnSalesEntity.IsReturned == true)
            //        {
            //            new TrnPOSReturnReportForm(trnSalesEntity.Id);
            //        }
            //        else
            //        {
            //            if (Modules.SysCurrentModule.GetCurrentSettings().SalesOrderPrinterType == "Label Printer")
            //            {
            //                new TrnPOSSalesOrderReportFormLabelPrinter(trnSalesEntity.Id, printDialogSelectPrinter.PrinterSettings.PrinterName);
            //            }
            //            else
            //            {
            //                new TrnPOSSalesOrderReportForm(trnSalesEntity.Id, printDialogSelectPrinter.PrinterSettings.PrinterName);
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    if (trnSalesEntity.IsReturned == true)
            //    {
            //        new TrnPOSReturnReportForm(trnSalesEntity.Id);
            //    }
            //    else if (Modules.SysCurrentModule.GetCurrentSettings().SalesOrderPrinterType == "Kitchen Printer")
            //    {
            //        new TrnPOSTouchOrderReportFormKitchenPrinter(trnSalesEntity.Id, "");
            //    }
            //    else
            //    {
            //        new TrnPOSSalesOrderReportForm(trnSalesEntity.Id, "");
            //    }
            //}
        }

        private void buttonOverRide_Click(object sender, EventArgs e)
        {
            Account.SysLogin.SysLoginForm login = new Account.SysLogin.SysLoginForm(null, null, null, this, true);
            login.ShowDialog();
        }
        public void OverrideSales(Int32 overrideUserId)
        {
            sysUserRights.OverrideUserRights(overrideUserId, "TrnSalesDetail");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (sysUserRights.GetUserRights().CanLock == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonLock.Enabled = false;
                    }
                    else
                    {
                        buttonLock.Enabled = true;
                    }
                }
                else
                {
                    buttonLock.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanUnlock == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonUnlock.Enabled = true;
                    }
                    else
                    {
                        buttonUnlock.Enabled = false;
                    }
                }
                else
                {
                    buttonUnlock.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanReturn == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonReturn.Enabled = false;
                    }
                    else
                    {
                        buttonReturn.Enabled = true;
                    }
                }
                else
                {
                    buttonReturn.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanDiscount == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonDiscount.Enabled = false;
                    }
                    else
                    {
                        buttonDiscount.Enabled = true;
                    }
                }
                else
                {
                    buttonDiscount.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanTender == true)
                {
                    buttonTender.Enabled = true;
                }
                else
                {
                    buttonTender.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanPrint == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonPrint.Enabled = true;
                    }
                    else
                    {
                        buttonPrint.Enabled = false;
                    }
                }
                else
                {
                    buttonPrint.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanAdd == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        buttonBarcode.Enabled = false;
                        textBoxBarcode.Enabled = false;
                        buttonSearchItem.Enabled = false;
                        buttonDownload.Enabled = false;
                    }
                    else
                    {
                        buttonBarcode.Enabled = true;
                        textBoxBarcode.Enabled = true;
                        buttonSearchItem.Enabled = true;
                        buttonDownload.Enabled = true;
                    }
                }
                else
                {
                    buttonBarcode.Enabled = false;
                    textBoxBarcode.Enabled = false;
                    buttonSearchItem.Enabled = false;
                    buttonDownload.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanEdit == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        dataGridViewSalesLineList.Columns[0].Visible = false;
                    }
                    else
                    {
                        dataGridViewSalesLineList.Columns[0].Visible = true;
                    }
                }
                else
                {
                    dataGridViewSalesLineList.Columns[0].Visible = true;
                }

                if (sysUserRights.GetUserRights().CanDelete == true)
                {
                    if (trnSalesEntity.IsLocked == true)
                    {
                        dataGridViewSalesLineList.Columns[1].Visible = false;
                    }
                    else
                    {
                        dataGridViewSalesLineList.Columns[1].Visible = true;
                    }
                }
                else
                {
                    dataGridViewSalesLineList.Columns[1].Visible = false;
                }

                GetSalesDetail();
                GetSalesLineList();
            }
        }

        private void buttonHideItems_Click(object sender, EventArgs e)
        {
            if (panelItems.Visible == true)
            {
                panelItems.Visible = false;
            }
            else
            {
                panelItems.Visible = true;
            }
        }

        private void buttonChangeTable_Click(object sender, EventArgs e)
        {
            TrnPOSTouchChangeTableForm trnPOSTouchChangeTableForm = new TrnPOSTouchChangeTableForm(sysSoftwareForm, trnPOSTouchForm, this, trnSalesEntity, Convert.ToDateTime(trnSalesEntity.SalesDate));
            trnPOSTouchChangeTableForm.ShowDialog();
        }
    }
}
