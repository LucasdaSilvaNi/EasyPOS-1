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

namespace EasyPOS.Forms.Software.TrnPOS
{
    
    public partial class TrnPOSSearchItemForm : Form
    {
        public TrnPOSBarcodeDetailForm trnSalesDetailForm;
        public TrnPOSTouchDetailForm trnPOSTouchDetailForm;
        public Entities.TrnSalesEntity trnSalesEntity;
        public List<Entities.DgvTrnSalesSearchItemListEntity> searchItemList;
        public PagedList<Entities.DgvTrnSalesSearchItemListEntity> pageList;
        public BindingSource dataSearchItemListSource = new BindingSource();

        public Int32 pageNumber = 1;
        public Int32 pageSize = 50;

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();


        public TrnPOSSearchItemForm(TrnPOSBarcodeDetailForm salesDetailForm, TrnPOSTouchDetailForm POSTouchDetailForm, Entities.TrnSalesEntity salesEntity)
        {
            InitializeComponent();

            Controllers.SysLanguageController sysLabel = new Controllers.SysLanguageController();
            if (sysLabel.ListLanguage("").Any())
            {
                sysLanguageEntities = sysLabel.ListLanguage("");
                var language = Modules.SysCurrentModule.GetCurrentSettings().Language;
                if (language != "English")
                {
                    buttonClose.Text = SetLabel(buttonClose.Text);
                    label1.Text = SetLabel(label1.Text);
                    dataGridViewSearchItemList.Columns[1].HeaderText = SetLabel(dataGridViewSearchItemList.Columns[1].HeaderText);
                    dataGridViewSearchItemList.Columns[2].HeaderText = SetLabel(dataGridViewSearchItemList.Columns[2].HeaderText);
                    dataGridViewSearchItemList.Columns[9].HeaderText = SetLabel(dataGridViewSearchItemList.Columns[9].HeaderText);
                    dataGridViewSearchItemList.Columns[10].HeaderText = SetLabel(dataGridViewSearchItemList.Columns[10].HeaderText);
                    dataGridViewSearchItemList.Columns[11].HeaderText = SetLabel(dataGridViewSearchItemList.Columns[11].HeaderText);
                    dataGridViewSearchItemList.Columns[12].HeaderText = SetLabel(dataGridViewSearchItemList.Columns[12].HeaderText);
                    buttonPageListFirst.Text = SetLabel(buttonPageListFirst.Text);
                    buttonPageListPrevious.Text = SetLabel(buttonPageListPrevious.Text);
                    buttonPageListNext.Text = SetLabel(buttonPageListNext.Text);
                    buttonPageListLast.Text = SetLabel(buttonPageListLast.Text);
                }
            }

            trnSalesDetailForm = salesDetailForm;
            trnPOSTouchDetailForm = POSTouchDetailForm;

            trnSalesEntity = salesEntity;

            if (Modules.SysCurrentModule.GetCurrentSettings().HideItemListBarcode == true)
            {
                ColumnSearchItemBarcode.Visible = false;
            }
            else
            {
                ColumnSearchItemBarcode.Visible = true;
            }

            dataGridViewSearchItemList.Focus();

            GetListSearchItemDataSource("");
            GetDataGridViewListSearchItemSource();
            
        }

        public string SetLabel(string label)
        {
            if (sysLanguageEntities.Any())
            {
                foreach (var displayedLabel in sysLanguageEntities)
                {
                    if (displayedLabel.Label == label)
                    {
                        return displayedLabel.DisplayedLabel;
                    }
                }
            }
            return label;
        }
        public void resetCursor()
        {
            textBoxFilter.TabIndex = 0;

            textBoxFilter.Focus();
            textBoxFilter.SelectAll();

            GetListSearchItemDataSource(textBoxFilter.Text);
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        //public void GetSearchItemList()
        //{
        //    dataGridViewSearchItemList.Rows.Clear();
        //    dataGridViewSearchItemList.Refresh();

        //    Controllers.TrnSalesLineController trnPOSSalesLineController = new Controllers.TrnSalesLineController();

        //    var itemList = trnPOSSalesLineController.ListSearchItem(textBoxFilter.Text);
        //    if (itemList.Any())
        //    {
        //        dataGridViewSearchItemList.Columns[12].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
        //        dataGridViewSearchItemList.Columns[12].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
        //        dataGridViewSearchItemList.Columns[12].DefaultCellStyle.ForeColor = Color.White;

        //        foreach (var objItemList in itemList)
        //        {
        //            dataGridViewSearchItemList.Rows.Add(
        //                objItemList.Id,
        //                objItemList.BarCode,
        //                objItemList.ItemDescription,
        //                objItemList.GenericName,
        //                objItemList.OutTaxId,
        //                objItemList.OutTax,
        //                objItemList.OutTaxRate.ToString("#,##0.00"),
        //                objItemList.UnitId,
        //                objItemList.Unit,
        //                objItemList.Price.ToString("#,##0.00"),
        //                objItemList.OnhandQuantity.ToString("#,##0.00"),
        //                objItemList.IsInventory,
        //                "Pick"
        //            );
        //        }
        //    }
        //}

        public List<Entities.DgvTrnSalesSearchItemListEntity> GetSearchItemList(String filter)
        {
            List<Entities.DgvTrnSalesSearchItemListEntity> rowList = new List<Entities.DgvTrnSalesSearchItemListEntity>();

            Controllers.TrnSalesLineController trnPOSSalesLineController = new Controllers.TrnSalesLineController();

            var itemList = trnPOSSalesLineController.ListSearchItem(filter);
            if (itemList.Any())
            {
                dataGridViewSearchItemList.Columns[12].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
                dataGridViewSearchItemList.Columns[12].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
                dataGridViewSearchItemList.Columns[12].DefaultCellStyle.ForeColor = Color.White;
                var row = from d in itemList
                          select new Entities.DgvTrnSalesSearchItemListEntity
                          {
                              ColumnSearchItemId = d.Id,
                              ColumnSearchItemBarcode = d.BarCode,
                              ColumnSearchItemDescription = d.ItemDescription,
                              ColumnSearchItemGenericName = d.GenericName,
                              ColumnSearchItemOutTaxId = d.OutTaxId,
                              ColumnSearchItemOutTax = d.OutTax,
                              ColumnSearchItemOutTaxRate = d.OutTaxRate.ToString("#,##0.00"),
                              ColumnSearchItemUnitId = d.UnitId,
                              ColumnSearchItemUnit = d.Unit,
                              ColumnSearchItemPrice = d.Price.ToString("#,##0.00"),
                              ColumnSearchItemOnHandQuantity = d.OnhandQuantity.ToString("#,##0.00"),
                              ColumnSearchItemIsInventory = d.IsInventory,
                              ColumnSearchItemButtonPick = "Pick"
                          };

                rowList = row.ToList();
            }

            return rowList;
        }

        public void GetListSearchItemDataSource(String filter)
        {
            searchItemList = GetSearchItemList(filter);
            if (searchItemList.Any())
            {
                pageList = new PagedList<Entities.DgvTrnSalesSearchItemListEntity>(searchItemList, pageNumber, pageSize);

                if (pageList.PageCount == 1)
                {
                    buttonPageListFirst.Enabled = false;
                    buttonPageListPrevious.Enabled = false;
                    buttonPageListNext.Enabled = false;
                    buttonPageListLast.Enabled = false;
                }
                else if (pageNumber == 1)
                {
                    buttonPageListFirst.Enabled = false;
                    buttonPageListPrevious.Enabled = false;
                    buttonPageListNext.Enabled = true;
                    buttonPageListLast.Enabled = true;
                }
                else if (pageNumber == pageList.PageCount)
                {
                    buttonPageListFirst.Enabled = true;
                    buttonPageListPrevious.Enabled = true;
                    buttonPageListNext.Enabled = false;
                    buttonPageListLast.Enabled = false;
                }
                else
                {
                    buttonPageListFirst.Enabled = true;
                    buttonPageListPrevious.Enabled = true;
                    buttonPageListNext.Enabled = true;
                    buttonPageListLast.Enabled = true;
                }

                textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
                dataSearchItemListSource.DataSource = pageList;
            }
            else
            {
                buttonPageListFirst.Enabled = false;
                buttonPageListPrevious.Enabled = false;
                buttonPageListNext.Enabled = false;
                buttonPageListLast.Enabled = false;

                dataSearchItemListSource.Clear();
                textBoxPageNumber.Text = "0 / 0";
            }
        }
        public void GetDataGridViewListSearchItemSource()
        {
            dataGridViewSearchItemList.DataSource = dataSearchItemListSource;
        }


        private void textBoxFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetListSearchItemDataSource(textBoxFilter.Text);
            }
        }
        
        private void dataGridViewSearchItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewSearchItemList.CurrentCell.ColumnIndex == dataGridViewSearchItemList.Columns["ColumnSearchItemButtonPick"].Index)
            {
                Int32 ItemId = Convert.ToInt32(dataGridViewSearchItemList.Rows[e.RowIndex].Cells[0].Value);
                Int32 SalesId = trnSalesEntity.Id;
                String ItemDescription = dataGridViewSearchItemList.Rows[e.RowIndex].Cells[2].Value.ToString();
                Int32 TaxId = Convert.ToInt32(dataGridViewSearchItemList.Rows[e.RowIndex].Cells[4].Value);
                String Tax = dataGridViewSearchItemList.Rows[e.RowIndex].Cells[5].Value.ToString();
                Decimal TaxRate = Convert.ToDecimal(dataGridViewSearchItemList.Rows[e.RowIndex].Cells[6].Value);
                Int32 UnitId = Convert.ToInt32(dataGridViewSearchItemList.Rows[e.RowIndex].Cells[7].Value);
                String Unit = dataGridViewSearchItemList.Rows[e.RowIndex].Cells[8].Value.ToString();
                Decimal Price = Convert.ToDecimal(dataGridViewSearchItemList.Rows[e.RowIndex].Cells[9].Value);
                Int32 DiscountId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().DefaultDiscountId);
                Int32 UserId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId);

                Decimal TaxAmount = 0;
                if (TaxRate > 0)
                {
                    TaxAmount = (Price / (1 + (TaxRate / 100))) * (TaxRate / 100);
                }

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
                    Preparation = "NA",
                    Price1 = 0,
                    Price2 = 0,
                    Price2LessTax = 0,
                    PriceSplitPercentage = 0
                };
                TrnPOSSalesItemDetailForm trnSalesDetailSalesItemDetailForm = new TrnPOSSalesItemDetailForm(trnSalesDetailForm, trnPOSTouchDetailForm, trnSalesLineEntity,this);
                trnSalesDetailSalesItemDetailForm.ShowDialog();

            }
        }

        private void dataGridViewSearchItemList_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGridViewSearchItemList.SelectedRows.Count == 0)
            {
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                Int32 ItemId = Convert.ToInt32(dataGridViewSearchItemList.Rows[dataGridViewSearchItemList.CurrentCell.RowIndex].Cells[0].Value);
                Int32 SalesId = trnSalesEntity.Id;
                String ItemDescription = dataGridViewSearchItemList.Rows[dataGridViewSearchItemList.CurrentCell.RowIndex].Cells[2].Value.ToString();
                Int32 TaxId = Convert.ToInt32(dataGridViewSearchItemList.Rows[dataGridViewSearchItemList.CurrentCell.RowIndex].Cells[4].Value);
                String Tax = dataGridViewSearchItemList.Rows[dataGridViewSearchItemList.CurrentCell.RowIndex].Cells[5].Value.ToString();
                Decimal TaxRate = Convert.ToDecimal(dataGridViewSearchItemList.Rows[dataGridViewSearchItemList.CurrentCell.RowIndex].Cells[6].Value);
                Int32 UnitId = Convert.ToInt32(dataGridViewSearchItemList.Rows[dataGridViewSearchItemList.CurrentCell.RowIndex].Cells[7].Value);
                String Unit = dataGridViewSearchItemList.Rows[dataGridViewSearchItemList.CurrentCell.RowIndex].Cells[8].Value.ToString();
                Decimal Price = Convert.ToDecimal(dataGridViewSearchItemList.Rows[dataGridViewSearchItemList.CurrentCell.RowIndex].Cells[9].Value);
                Int32 DiscountId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().DefaultDiscountId);
                Int32 UserId = Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId);

                Decimal TaxAmount = 0;
                if (TaxRate > 0)
                {
                    TaxAmount = (Price / (1 + (TaxRate / 100))) * (TaxRate / 100);
                }

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
                    Preparation = "NA",
                    Price1 = 0,
                    Price2 = 0,
                    Price2LessTax = 0,
                    PriceSplitPercentage = 0
                };
                TrnPOSSalesItemDetailForm trnSalesDetailSalesItemDetailForm = new TrnPOSSalesItemDetailForm(trnSalesDetailForm, trnPOSTouchDetailForm, trnSalesLineEntity,this);
                trnSalesDetailSalesItemDetailForm.ShowDialog();

                resetCursor();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    {
                        Close();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void buttonPageListFirst_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvTrnSalesSearchItemListEntity>(searchItemList, 1, pageSize);
            dataSearchItemListSource.DataSource = pageList;

            buttonPageListFirst.Enabled = false;
            buttonPageListPrevious.Enabled = false;
            buttonPageListNext.Enabled = true;
            buttonPageListLast.Enabled = true;

            pageNumber = 1;
            textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
        }

        private void buttonPageListPrevious_Click(object sender, EventArgs e)
        {
            if (pageList.HasPreviousPage == true)
            {
                pageList = new PagedList<Entities.DgvTrnSalesSearchItemListEntity>(searchItemList, --pageNumber, pageSize);
                dataSearchItemListSource.DataSource = pageList;
            }

            buttonPageListNext.Enabled = true;
            buttonPageListLast.Enabled = true;

            if (pageNumber == 1)
            {
                buttonPageListFirst.Enabled = false;
                buttonPageListPrevious.Enabled = false;
            }

            textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
        }

        private void buttonPageListNext_Click(object sender, EventArgs e)
        {
            if (pageList.HasNextPage == true)
            {
                pageList = new PagedList<Entities.DgvTrnSalesSearchItemListEntity>(searchItemList, ++pageNumber, pageSize);
                dataSearchItemListSource.DataSource = pageList;
            }

            buttonPageListFirst.Enabled = true;
            buttonPageListPrevious.Enabled = true;

            if (pageNumber == pageList.PageCount)
            {
                buttonPageListNext.Enabled = false;
                buttonPageListLast.Enabled = false;
            }

            textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
        }

        private void buttonPageListLast_Click(object sender, EventArgs e)
        {
            pageList = new PagedList<Entities.DgvTrnSalesSearchItemListEntity>(searchItemList, pageList.PageCount, pageSize);
            dataSearchItemListSource.DataSource = pageList;

            buttonPageListFirst.Enabled = true;
            buttonPageListPrevious.Enabled = true;
            buttonPageListNext.Enabled = false;
            buttonPageListLast.Enabled = false;

            pageNumber = pageList.PageCount;
            textBoxPageNumber.Text = pageNumber + " / " + pageList.PageCount;
        }
    }
}
