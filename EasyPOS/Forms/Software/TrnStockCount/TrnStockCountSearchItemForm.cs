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

namespace EasyPOS.Forms.Software.TrnStockCount
{
    public partial class TrnStockCountSearchItemForm : Form
    {
        public TrnStockCountDetailForm trnStockCountDetailForm;
        public Entities.TrnStockCountEntity trnStockCountEntity;

        public static List<Entities.DgvTrnStockCountSearchItemListEntity> searchItemListData = new List<Entities.DgvTrnStockCountSearchItemListEntity>();
        public static Int32 pageNumber = 1;
        public static Int32 pageSize = 50;
        public PagedList<Entities.DgvTrnStockCountSearchItemListEntity> searchItemListPageList = new PagedList<Entities.DgvTrnStockCountSearchItemListEntity>(searchItemListData, pageNumber, pageSize);
        public BindingSource searchItemListDataSource = new BindingSource();

        public List<Entities.SysLanguageEntity> sysLanguageEntities = new List<Entities.SysLanguageEntity>();

        public TrnStockCountSearchItemForm(TrnStockCountDetailForm stockOutDetailForm, Entities.TrnStockCountEntity stockOutEntity)
        {
            InitializeComponent();

            label1.Text = SetLabel(label1.Text);
            buttonClose.Text = SetLabel(buttonClose.Text);
            buttonSearchItemListPageListFirst.Text = SetLabel(buttonSearchItemListPageListFirst.Text);
            buttonSearchItemListPageListPrevious.Text = SetLabel(buttonSearchItemListPageListPrevious.Text);
            buttonSearchItemListPageListNext.Text = SetLabel(buttonSearchItemListPageListNext.Text);
            buttonSearchItemListPageListLast.Text = SetLabel(buttonSearchItemListPageListLast.Text);
            dataGridViewSearchItemList.Columns[1].HeaderText = SetLabel(dataGridViewSearchItemList.Columns[1].HeaderText);
            dataGridViewSearchItemList.Columns[2].HeaderText = SetLabel(dataGridViewSearchItemList.Columns[2].HeaderText);
            dataGridViewSearchItemList.Columns[3].HeaderText = SetLabel(dataGridViewSearchItemList.Columns[3].HeaderText);
            dataGridViewSearchItemList.Columns[4].HeaderText = SetLabel(dataGridViewSearchItemList.Columns[4].HeaderText);
            dataGridViewSearchItemList.Columns[11].HeaderText = SetLabel(dataGridViewSearchItemList.Columns[11].HeaderText);
            dataGridViewSearchItemList.Columns[12].HeaderText = SetLabel(dataGridViewSearchItemList.Columns[12].HeaderText);

            trnStockCountDetailForm = stockOutDetailForm;
            trnStockCountEntity = stockOutEntity;
            if (Modules.SysCurrentModule.GetCurrentSettings().HideItemListBarcode == true)
            {
                ColumnSearchItemListBarCode.Visible = false;
            }
            else
            {
                ColumnSearchItemListBarCode.Visible = true;
            }
            CreateSearchItemListDataGridView();
            textBoxSearchItemListFilter.Focus();
            
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

        public void UpdateSearchItemListDataSource()
        {
            SetSearchItemListDataSourceAsync();
        }

        public async void SetSearchItemListDataSourceAsync()
        {
            List<Entities.DgvTrnStockCountSearchItemListEntity> getSearchItemListData = await GetSearchItemListDataTask();
            if (getSearchItemListData.Any())
            {
                searchItemListData = getSearchItemListData;
                searchItemListPageList = new PagedList<Entities.DgvTrnStockCountSearchItemListEntity>(searchItemListData, pageNumber, pageSize);

                if (searchItemListPageList.PageCount == 1)
                {
                    buttonSearchItemListPageListFirst.Enabled = false;
                    buttonSearchItemListPageListPrevious.Enabled = false;
                    buttonSearchItemListPageListNext.Enabled = false;
                    buttonSearchItemListPageListLast.Enabled = false;
                }
                else if (pageNumber == 1)
                {
                    buttonSearchItemListPageListFirst.Enabled = false;
                    buttonSearchItemListPageListPrevious.Enabled = false;
                    buttonSearchItemListPageListNext.Enabled = true;
                    buttonSearchItemListPageListLast.Enabled = true;
                }
                else if (pageNumber == searchItemListPageList.PageCount)
                {
                    buttonSearchItemListPageListFirst.Enabled = true;
                    buttonSearchItemListPageListPrevious.Enabled = true;
                    buttonSearchItemListPageListNext.Enabled = false;
                    buttonSearchItemListPageListLast.Enabled = false;
                }
                else
                {
                    buttonSearchItemListPageListFirst.Enabled = true;
                    buttonSearchItemListPageListPrevious.Enabled = true;
                    buttonSearchItemListPageListNext.Enabled = true;
                    buttonSearchItemListPageListLast.Enabled = true;
                }

                textBoxSearchItemListPageNumber.Text = pageNumber + " / " + searchItemListPageList.PageCount;
                searchItemListDataSource.DataSource = searchItemListPageList;
            }
            else
            {
                buttonSearchItemListPageListFirst.Enabled = false;
                buttonSearchItemListPageListPrevious.Enabled = false;
                buttonSearchItemListPageListNext.Enabled = false;
                buttonSearchItemListPageListLast.Enabled = false;

                pageNumber = 1;

                searchItemListData = new List<Entities.DgvTrnStockCountSearchItemListEntity>();
                searchItemListDataSource.Clear();
                textBoxSearchItemListPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvTrnStockCountSearchItemListEntity>> GetSearchItemListDataTask()
        {
            String filter = textBoxSearchItemListFilter.Text;
            Controllers.TrnStockCountLineController trnStockCountLineController = new Controllers.TrnStockCountLineController();

            List<Entities.MstItemEntity> listSearchItem = trnStockCountLineController.ListSearchItem(filter);
            if (listSearchItem.Any())
            {
                var items = from d in listSearchItem
                            select new Entities.DgvTrnStockCountSearchItemListEntity
                            {
                                ColumnSearchItemListId = d.Id,
                                ColumnSearchItemListBarCode = d.BarCode,
                                ColumnSearchItemListDescription = d.ItemDescription,
                                ColumnSearchItemListGenericName = d.GenericName,
                                ColumnSearchItemListCost = d.Cost.ToString("#,##0.00"),
                                ColumnSearchItemListOutTaxId = d.OutTaxId,
                                ColumnSearchItemListOutTax = d.OutTax,
                                ColumnSearchItemListOutTaxRate = d.OutTaxRate.ToString("#,##0.00"),
                                ColumnSearchItemListUnitId = d.UnitId,
                                ColumnSearchItemListUnit = d.Unit,
                                ColumnSearchItemListPrice = d.Price.ToString("#,##0.00"),
                                ColumnSearchItemListOnhandQuantity = d.OnhandQuantity.ToString("#,##0.00"),
                                ColumnSearchItemListButtonPick = SetLabel("Pick")
                            };

                return Task.FromResult(items.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvTrnStockCountSearchItemListEntity>());
            }
        }

        public void CreateSearchItemListDataGridView()
        {
            UpdateSearchItemListDataSource();

            dataGridViewSearchItemList.Columns[12].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewSearchItemList.Columns[12].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewSearchItemList.Columns[12].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewSearchItemList.DataSource = searchItemListDataSource;
        }

        public void GetSearchItemListCurrentSelectedCell(Int32 rowIndex)
        {

        }


        private void dataGridViewSearchItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewSearchItemList.CurrentCell.ColumnIndex == dataGridViewSearchItemList.Columns["ColumnSearchItemListButtonPick"].Index)
            {
                var stockOutId = trnStockCountEntity.Id;
                var itemId = Convert.ToInt32(dataGridViewSearchItemList.Rows[e.RowIndex].Cells[dataGridViewSearchItemList.Columns["ColumnSearchItemListId"].Index].Value);
                var itemDescription = dataGridViewSearchItemList.Rows[e.RowIndex].Cells[dataGridViewSearchItemList.Columns["ColumnSearchItemListDescription"].Index].Value.ToString();
                var cost = Convert.ToDecimal(dataGridViewSearchItemList.Rows[e.RowIndex].Cells[dataGridViewSearchItemList.Columns["ColumnSearchItemListCost"].Index].Value);
                var unitId = Convert.ToInt32(dataGridViewSearchItemList.Rows[e.RowIndex].Cells[dataGridViewSearchItemList.Columns["ColumnSearchItemListUnitId"].Index].Value);
                var unit = dataGridViewSearchItemList.Rows[e.RowIndex].Cells[dataGridViewSearchItemList.Columns["ColumnSearchItemListUnit"].Index].Value.ToString();
                var price = Convert.ToDecimal(dataGridViewSearchItemList.Rows[e.RowIndex].Cells[dataGridViewSearchItemList.Columns["ColumnSearchItemListPrice"].Index].Value);

                Entities.TrnStockCountLineEntity trnStockCountLineEntity = new Entities.TrnStockCountLineEntity()
                {
                    Id = 0,
                    StockCountId = stockOutId,
                    ItemId = itemId,
                    ItemDescription = itemDescription,
                    UnitId = unitId,
                    Unit = unit,
                    Quantity = 1,
                    Cost = cost,
                    Amount = 0
                };

                TrnStockCountLineItemDetailForm trnStockCountLineItemDetailForm = new TrnStockCountLineItemDetailForm(trnStockCountDetailForm, trnStockCountLineEntity);
                trnStockCountLineItemDetailForm.ShowDialog();
            }
        }

        private void textBoxSearchItemListFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateSearchItemListDataSource();
                pageNumber = 1;
                CreateSearchItemListDataGridView();
            }
        }

        private void buttonSearchItemListPageListFirst_Click(object sender, EventArgs e)
        {
            searchItemListPageList = new PagedList<Entities.DgvTrnStockCountSearchItemListEntity>(searchItemListData, 1, pageSize);
            searchItemListDataSource.DataSource = searchItemListPageList;

            buttonSearchItemListPageListFirst.Enabled = false;
            buttonSearchItemListPageListPrevious.Enabled = false;
            buttonSearchItemListPageListNext.Enabled = true;
            buttonSearchItemListPageListLast.Enabled = true;

            pageNumber = 1;
            textBoxSearchItemListPageNumber.Text = pageNumber + " / " + searchItemListPageList.PageCount;
        }

        private void buttonSearchItemListPageListPrevious_Click(object sender, EventArgs e)
        {
            if (searchItemListPageList.HasPreviousPage == true)
            {
                searchItemListPageList = new PagedList<Entities.DgvTrnStockCountSearchItemListEntity>(searchItemListData, --pageNumber, pageSize);
                searchItemListDataSource.DataSource = searchItemListPageList;
            }

            buttonSearchItemListPageListNext.Enabled = true;
            buttonSearchItemListPageListLast.Enabled = true;

            if (pageNumber == 1)
            {
                buttonSearchItemListPageListFirst.Enabled = false;
                buttonSearchItemListPageListPrevious.Enabled = false;
            }

            textBoxSearchItemListPageNumber.Text = pageNumber + " / " + searchItemListPageList.PageCount;
        }

        private void buttonSearchItemListPageListNext_Click(object sender, EventArgs e)
        {
            if (searchItemListPageList.HasNextPage == true)
            {
                searchItemListPageList = new PagedList<Entities.DgvTrnStockCountSearchItemListEntity>(searchItemListData, ++pageNumber, pageSize);
                searchItemListDataSource.DataSource = searchItemListPageList;
            }

            buttonSearchItemListPageListFirst.Enabled = true;
            buttonSearchItemListPageListPrevious.Enabled = true;

            if (pageNumber == searchItemListPageList.PageCount)
            {
                buttonSearchItemListPageListNext.Enabled = false;
                buttonSearchItemListPageListLast.Enabled = false;
            }

            textBoxSearchItemListPageNumber.Text = pageNumber + " / " + searchItemListPageList.PageCount;
        }

        private void buttonSearchItemListPageListLast_Click(object sender, EventArgs e)
        {
            searchItemListPageList = new PagedList<Entities.DgvTrnStockCountSearchItemListEntity>(searchItemListData, searchItemListPageList.PageCount, pageSize);
            searchItemListDataSource.DataSource = searchItemListPageList;

            buttonSearchItemListPageListFirst.Enabled = true;
            buttonSearchItemListPageListPrevious.Enabled = true;
            buttonSearchItemListPageListNext.Enabled = false;
            buttonSearchItemListPageListLast.Enabled = false;

            pageNumber = searchItemListPageList.PageCount;
            textBoxSearchItemListPageNumber.Text = pageNumber + " / " + searchItemListPageList.PageCount;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
