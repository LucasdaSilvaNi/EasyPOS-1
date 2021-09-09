using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.TrnPOS
{
    public partial class TrnPOSTouchActivitySplitMergeForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;

        public TrnPOSTouchForm trnPOSTouchForm;
        public TrnPOSTouchActivityForm trnPOSTouchActivityForm;
        public Entities.TrnSalesEntity trnSalesEntity;

        public static Int32 pageNumber = 1;
        public static Int32 pageSize = 50;

        public static List<Entities.DgvTrnSalesItemSplitEntity> salesItemListSplitData = new List<Entities.DgvTrnSalesItemSplitEntity>();
        public PagedList<Entities.DgvTrnSalesItemSplitEntity> salesItemListSplitPageList = new PagedList<Entities.DgvTrnSalesItemSplitEntity>(salesItemListSplitData, pageNumber, pageSize);
        public BindingSource salesItemListSplitDataSource = new BindingSource();

        public static List<Entities.DgvTrnSalesMergeEntity> salesListMergeData = new List<Entities.DgvTrnSalesMergeEntity>();
        public PagedList<Entities.DgvTrnSalesMergeEntity> salesListMergePageList = new PagedList<Entities.DgvTrnSalesMergeEntity>(salesListMergeData, pageNumber, pageSize);
        public BindingSource salesListMergeDataSource = new BindingSource();

        public Boolean isTableGroupSelected = false;

        public TrnPOSTouchActivitySplitMergeForm(SysSoftwareForm softwareForm, TrnPOSTouchForm POSTouchForm, TrnPOSTouchActivityForm POSTouchActivityForm, Entities.TrnSalesEntity salesEntity)
        {
            InitializeComponent();

            sysSoftwareForm = softwareForm;
            trnPOSTouchForm = POSTouchForm;
            trnPOSTouchActivityForm = POSTouchActivityForm;
            trnSalesEntity = salesEntity;

            Boolean isLocked = trnSalesEntity.IsLocked;
            Boolean isTendered = trnSalesEntity.IsTendered;
            Boolean isCanclled = trnSalesEntity.IsCancelled;

            CreateSalesItemSplitDataGridView();
            GetTableGroupList();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void UpdateSalesItemSplitDataSource()
        {
            SetSalesItemSplitDataSourceAsync();
        }

        public async void SetSalesItemSplitDataSourceAsync()
        {
            List<Entities.DgvTrnSalesItemSplitEntity> getSalesItemSplitData = await GetSalesItemSplitDataTask();
            if (getSalesItemSplitData.Any())
            {
                salesItemListSplitData = getSalesItemSplitData;
                salesItemListSplitPageList = new PagedList<Entities.DgvTrnSalesItemSplitEntity>(salesItemListSplitData, pageNumber, pageSize);

                if (salesItemListSplitPageList.PageCount == 1)
                {
                    buttonSalesItemSplitPageListFirst.Enabled = false;
                    buttonSalesItemSplitPageListPrevious.Enabled = false;
                    buttonSalesItemSplitPageListNext.Enabled = false;
                    buttonSalesItemSplitPageListLast.Enabled = false;
                }
                else if (pageNumber == 1)
                {
                    buttonSalesItemSplitPageListFirst.Enabled = false;
                    buttonSalesItemSplitPageListPrevious.Enabled = false;
                    buttonSalesItemSplitPageListNext.Enabled = true;
                    buttonSalesItemSplitPageListLast.Enabled = true;
                }
                else if (pageNumber == salesItemListSplitPageList.PageCount)
                {
                    buttonSalesItemSplitPageListFirst.Enabled = true;
                    buttonSalesItemSplitPageListPrevious.Enabled = true;
                    buttonSalesItemSplitPageListNext.Enabled = false;
                    buttonSalesItemSplitPageListLast.Enabled = false;
                }
                else
                {
                    buttonSalesItemSplitPageListFirst.Enabled = true;
                    buttonSalesItemSplitPageListPrevious.Enabled = true;
                    buttonSalesItemSplitPageListNext.Enabled = true;
                    buttonSalesItemSplitPageListLast.Enabled = true;
                }

                textBoxSalesItemSplitPageNumber.Text = pageNumber + " / " + salesItemListSplitPageList.PageCount;
                salesItemListSplitDataSource.DataSource = salesItemListSplitPageList;
            }
            else
            {
                buttonSalesItemSplitPageListFirst.Enabled = false;
                buttonSalesItemSplitPageListPrevious.Enabled = false;
                buttonSalesItemSplitPageListNext.Enabled = false;
                buttonSalesItemSplitPageListLast.Enabled = false;

                pageNumber = 1;

                salesItemListSplitData = new List<Entities.DgvTrnSalesItemSplitEntity>();
                salesItemListSplitDataSource.Clear();
                textBoxSalesItemSplitPageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvTrnSalesItemSplitEntity>> GetSalesItemSplitDataTask()
        {
            Controllers.TrnSalesLineController trnSalesLineController = new Controllers.TrnSalesLineController();
            List<Entities.TrnSalesLineEntity> listSalesItemSplitItems = trnSalesLineController.ListSalesLine(trnSalesEntity.Id);

            if (listSalesItemSplitItems.Any())
            {
                var returnItemss = from d in listSalesItemSplitItems
                                   select new Entities.DgvTrnSalesItemSplitEntity
                                   {
                                       ColumnSplitSalesItemId = d.ItemId,
                                       ColumnSplitSalesItemDescription = d.ItemDescription,
                                       ColumnSalesItemUnit = d.Unit,
                                       ColumnSalesItemQuantity = d.Quantity.ToString("#,##0.00"),
                                       ColumnSalesItemButtonPickTable = "Pick Table",
                                       ColumnSplitSalesTableId = d.TableId != null ? Convert.ToInt32(d.TableId) : 0,
                                       ColumnSalesItemTableCode = d.TableCode,
                                       ColumnSalesLineId = d.Id
                                   };

                return Task.FromResult(returnItemss.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvTrnSalesItemSplitEntity>());
            }
        }

        public void CreateSalesItemSplitDataGridView()
        {
            UpdateSalesItemSplitDataSource();

            dataGridViewSalesItemSplitItems.Columns[4].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewSalesItemSplitItems.Columns[4].DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#01A6F0");
            dataGridViewSalesItemSplitItems.Columns[4].DefaultCellStyle.ForeColor = Color.White;

            dataGridViewSalesItemSplitItems.DataSource = salesItemListSplitDataSource;
        }

        public void GetSalesItemSplitCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void buttonSalesItemSplitPageListFirst_Click(object sender, EventArgs e)
        {
            salesItemListSplitPageList = new PagedList<Entities.DgvTrnSalesItemSplitEntity>(salesItemListSplitData, 1, pageSize);
            salesItemListSplitDataSource.DataSource = salesItemListSplitPageList;

            buttonSalesItemSplitPageListFirst.Enabled = false;
            buttonSalesItemSplitPageListPrevious.Enabled = false;
            buttonSalesItemSplitPageListNext.Enabled = true;
            buttonSalesItemSplitPageListLast.Enabled = true;

            pageNumber = 1;
            textBoxSalesItemSplitPageNumber.Text = pageNumber + " / " + salesItemListSplitPageList.PageCount;
        }

        private void buttonSalesItemSplitPageListPrevious_Click(object sender, EventArgs e)
        {
            if (salesItemListSplitPageList.HasPreviousPage == true)
            {
                salesItemListSplitPageList = new PagedList<Entities.DgvTrnSalesItemSplitEntity>(salesItemListSplitData, --pageNumber, pageSize);
                salesItemListSplitDataSource.DataSource = salesItemListSplitPageList;
            }

            buttonSalesItemSplitPageListNext.Enabled = true;
            buttonSalesItemSplitPageListLast.Enabled = true;

            if (pageNumber == 1)
            {
                buttonSalesItemSplitPageListFirst.Enabled = false;
                buttonSalesItemSplitPageListPrevious.Enabled = false;
            }

            textBoxSalesItemSplitPageNumber.Text = pageNumber + " / " + salesItemListSplitPageList.PageCount;
        }

        private void buttonSalesItemSplitPageListNext_Click(object sender, EventArgs e)
        {
            if (salesItemListSplitPageList.HasNextPage == true)
            {
                salesItemListSplitPageList = new PagedList<Entities.DgvTrnSalesItemSplitEntity>(salesItemListSplitData, ++pageNumber, pageSize);
                salesItemListSplitDataSource.DataSource = salesItemListSplitPageList;
            }

            buttonSalesItemSplitPageListFirst.Enabled = true;
            buttonSalesItemSplitPageListPrevious.Enabled = true;

            if (pageNumber == salesItemListSplitPageList.PageCount)
            {
                buttonSalesItemSplitPageListNext.Enabled = false;
                buttonSalesItemSplitPageListLast.Enabled = false;
            }

            textBoxSalesItemSplitPageNumber.Text = pageNumber + " / " + salesItemListSplitPageList.PageCount;
        }

        private void buttonSalesItemSplitPageListLast_Click(object sender, EventArgs e)
        {
            salesItemListSplitPageList = new PagedList<Entities.DgvTrnSalesItemSplitEntity>(salesItemListSplitData, salesItemListSplitPageList.PageCount, pageSize);
            salesItemListSplitDataSource.DataSource = salesItemListSplitPageList;

            buttonSalesItemSplitPageListFirst.Enabled = true;
            buttonSalesItemSplitPageListPrevious.Enabled = true;
            buttonSalesItemSplitPageListNext.Enabled = false;
            buttonSalesItemSplitPageListLast.Enabled = false;

            pageNumber = salesItemListSplitPageList.PageCount;
            textBoxSalesItemSplitPageNumber.Text = pageNumber + " / " + salesItemListSplitPageList.PageCount;
        }

        private void dataGridViewSalesItemSplit_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetSalesItemSplitCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewSalesItemSplitItems.CurrentCell.ColumnIndex == dataGridViewSalesItemSplitItems.Columns["ColumnSalesItemButtonPickTable"].Index)
            {
                Decimal quantity = Convert.ToDecimal(dataGridViewSalesItemSplitItems.Rows[dataGridViewSalesItemSplitItems.CurrentCell.RowIndex].Cells[dataGridViewSalesItemSplitItems.Columns["ColumnSalesItemQuantity"].Index].Value);

                TrnPOSTouchActivitySplitMergeTableCodeForm trnPOSTouchActivitySplitMergeTableCodeForm = new TrnPOSTouchActivitySplitMergeTableCodeForm(this, dataGridViewSalesItemSplitItems, trnSalesEntity, quantity);
                trnPOSTouchActivitySplitMergeTableCodeForm.ShowDialog();
            }
        }

        private void buttonSplit_Click(object sender, EventArgs e)
        {
            DialogResult splitDialogResult = MessageBox.Show("Split bill? ", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (splitDialogResult == DialogResult.Yes)
            {
                if (dataGridViewSalesItemSplitItems.Rows.Count > 0)
                {
                    List<Entities.DgvTrnSalesItemSplitEntity> listSplitMergeItems = new List<Entities.DgvTrnSalesItemSplitEntity>();

                    for (var i = 0; i < dataGridViewSalesItemSplitItems.Rows.Count; i++)
                    {
                        listSplitMergeItems.Add(new Entities.DgvTrnSalesItemSplitEntity()
                        {
                            ColumnSplitSalesItemId = Convert.ToInt32(dataGridViewSalesItemSplitItems.Rows[i].Cells[dataGridViewSalesItemSplitItems.Columns["ColumnSplitSalesItemId"].Index].Value),
                            ColumnSplitSalesItemDescription = dataGridViewSalesItemSplitItems.Rows[i].Cells[dataGridViewSalesItemSplitItems.Columns["ColumnSplitSalesItemDescription"].Index].Value.ToString(),
                            ColumnSalesItemUnit = dataGridViewSalesItemSplitItems.Rows[i].Cells[dataGridViewSalesItemSplitItems.Columns["ColumnSalesItemUnit"].Index].Value.ToString(),
                            ColumnSalesItemQuantity = dataGridViewSalesItemSplitItems.Rows[i].Cells[dataGridViewSalesItemSplitItems.Columns["ColumnSalesItemQuantity"].Index].Value.ToString(),
                            ColumnSalesItemButtonPickTable = dataGridViewSalesItemSplitItems.Rows[i].Cells[dataGridViewSalesItemSplitItems.Columns["ColumnSalesItemButtonPickTable"].Index].Value.ToString(),
                            ColumnSplitSalesTableId = Convert.ToInt32(dataGridViewSalesItemSplitItems.Rows[i].Cells[dataGridViewSalesItemSplitItems.Columns["ColumnSplitSalesTableId"].Index].Value),
                            ColumnSalesItemTableCode = dataGridViewSalesItemSplitItems.Rows[i].Cells[dataGridViewSalesItemSplitItems.Columns["ColumnSalesItemTableCode"].Index].Value.ToString(),
                            ColumnSalesLineId = Convert.ToInt32(dataGridViewSalesItemSplitItems.Rows[i].Cells[dataGridViewSalesItemSplitItems.Columns["ColumnSalesLineId"].Index].Value)
                        });
                    }

                    Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
                    String[] splitSales = trnPOSSalesController.SplitSales(trnSalesEntity, listSplitMergeItems);
                    if (splitSales[1].Equals("0") == false)
                    {
                        MessageBox.Show("Split successful.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        trnPOSTouchForm.UpdateSalesListGridDataSource();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show(splitSales[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void GetTableGroupList()
        {
            try
            {
                Controllers.TrnSalesController trnSalesController = new Controllers.TrnSalesController();
                if (trnSalesController.ListTableGroup().Any())
                {
                    comboBoxTableGroup.DataSource = trnSalesController.ListTableGroup();
                    comboBoxTableGroup.ValueMember = "Id";
                    comboBoxTableGroup.DisplayMember = "TableGroup";

                    getTableCodeList();
                }

                CreateSalesMergeDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxTableGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isTableGroupSelected == true)
            {
                getTableCodeList();
            }
            else
            {
                isTableGroupSelected = true;
            }
        }

        public void getTableCodeList()
        {
            try
            {
                Int32 tableGroupId = Convert.ToInt32(comboBoxTableGroup.SelectedValue);
                DateTime salesDate = Convert.ToDateTime(trnSalesEntity.SalesDate);

                Controllers.TrnSalesController trnSalesController = new Controllers.TrnSalesController();
                if (trnSalesController.ListTable(tableGroupId, salesDate).Any())
                {
                    List<Int32> tableIds = new List<Int32>();

                    for (var i = 0; i < dataGridViewMergeSalesList.Rows.Count; i++)
                    {
                        Boolean selectedToMerge = Convert.ToBoolean(dataGridViewMergeSalesList.Rows[i].Cells[dataGridViewMergeSalesList.Columns["ColumnMergeCheckbox"].Index].Value);
                        if (selectedToMerge == true)
                        {
                            Int32 tableId = Convert.ToInt32(dataGridViewMergeSalesList.Rows[i].Cells[dataGridViewMergeSalesList.Columns["ColumnMergeTableId"].Index].Value);
                            tableIds.Add(tableId);
                        }
                    }

                    List<Entities.MstTableEntity> listTables = new List<Entities.MstTableEntity>();
                    foreach (var table in trnSalesController.ListTable(tableGroupId, salesDate).ToList())
                    {
                        if (tableIds.Contains(table.Id) == true)
                        {
                            listTables.Add(new Entities.MstTableEntity()
                            {
                                Id = table.Id,
                                TableCode = table.TableCode,
                                HasSales = table.HasSales
                            });
                        }
                    }

                    comboBoxTableCode.DataSource = listTables;
                    comboBoxTableCode.ValueMember = "Id";
                    comboBoxTableCode.DisplayMember = "TableCode";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateSalesMergeDataSource()
        {
            SetSalesMergeDataSourceAsync();
        }

        public async void SetSalesMergeDataSourceAsync()
        {
            List<Entities.DgvTrnSalesMergeEntity> getSalesMergeData = await GetSalesMergeDataTask();
            if (getSalesMergeData.Any())
            {
                salesListMergeData = getSalesMergeData;
                salesListMergePageList = new PagedList<Entities.DgvTrnSalesMergeEntity>(salesListMergeData, pageNumber, pageSize);

                if (salesListMergePageList.PageCount == 1)
                {
                    buttonSalesMergePageListFirst.Enabled = false;
                    buttonSalesMergePageListPrevious.Enabled = false;
                    buttonSalesMergePageListNext.Enabled = false;
                    buttonSalesMergePageListLast.Enabled = false;
                }
                else if (pageNumber == 1)
                {
                    buttonSalesMergePageListFirst.Enabled = false;
                    buttonSalesMergePageListPrevious.Enabled = false;
                    buttonSalesMergePageListNext.Enabled = true;
                    buttonSalesMergePageListLast.Enabled = true;
                }
                else if (pageNumber == salesListMergePageList.PageCount)
                {
                    buttonSalesMergePageListFirst.Enabled = true;
                    buttonSalesMergePageListPrevious.Enabled = true;
                    buttonSalesMergePageListNext.Enabled = false;
                    buttonSalesMergePageListLast.Enabled = false;
                }
                else
                {
                    buttonSalesMergePageListFirst.Enabled = true;
                    buttonSalesMergePageListPrevious.Enabled = true;
                    buttonSalesMergePageListNext.Enabled = true;
                    buttonSalesMergePageListLast.Enabled = true;
                }

                textBoxSalesMergePageNumber.Text = pageNumber + " / " + salesListMergePageList.PageCount;
                salesListMergeDataSource.DataSource = salesListMergePageList;
            }
            else
            {
                buttonSalesMergePageListFirst.Enabled = false;
                buttonSalesMergePageListPrevious.Enabled = false;
                buttonSalesMergePageListNext.Enabled = false;
                buttonSalesMergePageListLast.Enabled = false;

                pageNumber = 1;

                salesListMergeData = new List<Entities.DgvTrnSalesMergeEntity>();
                salesListMergeDataSource.Clear();
                textBoxSalesMergePageNumber.Text = "1 / 1";
            }
        }

        public Task<List<Entities.DgvTrnSalesMergeEntity>> GetSalesMergeDataTask()
        {
            Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
            var salesList = trnPOSSalesController.POSTTouchListSales(Convert.ToDateTime(trnSalesEntity.SalesDate), trnSalesEntity.TerminalId, "");

            if (salesList.Any())
            {
                var returnItemss = from d in salesList
                                   where d.IsLocked == true
                                   && d.IsTendered == false
                                   && d.IsCancelled == false
                                   select new Entities.DgvTrnSalesMergeEntity
                                   {
                                       ColumnMergeCheckbox = false,
                                       ColumnMergeSalesId = d.Id,
                                       ColumnMergeSalesNumber = d.SalesNumber,
                                       ColumnMergeTableId = d.TableId != null ? Convert.ToInt32(d.TableId) : 0,
                                       ColumnMergeTableCode = d.TableId != null ? d.Table : "",
                                       ColumnMergeSalesAmount = d.Amount.ToString("#,##0.00"),
                                       ColumnMergeSalesUser = d.SalesAgentUserName
                                   };

                return Task.FromResult(returnItemss.ToList());
            }
            else
            {
                return Task.FromResult(new List<Entities.DgvTrnSalesMergeEntity>());
            }
        }

        public void CreateSalesMergeDataGridView()
        {
            UpdateSalesMergeDataSource();

            dataGridViewMergeSalesList.DataSource = salesListMergeDataSource;
        }

        public void GetSalesMergeCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void buttonSalesMergePageListFirst_Click(object sender, EventArgs e)
        {
            salesListMergePageList = new PagedList<Entities.DgvTrnSalesMergeEntity>(salesListMergeData, 1, pageSize);
            salesListMergeDataSource.DataSource = salesListMergePageList;

            buttonSalesMergePageListFirst.Enabled = false;
            buttonSalesMergePageListPrevious.Enabled = false;
            buttonSalesMergePageListNext.Enabled = true;
            buttonSalesMergePageListLast.Enabled = true;

            pageNumber = 1;
            textBoxSalesMergePageNumber.Text = pageNumber + " / " + salesListMergePageList.PageCount;
        }

        private void buttonSalesMergePageListPrevious_Click(object sender, EventArgs e)
        {
            if (salesListMergePageList.HasPreviousPage == true)
            {
                salesListMergePageList = new PagedList<Entities.DgvTrnSalesMergeEntity>(salesListMergeData, --pageNumber, pageSize);
                salesListMergeDataSource.DataSource = salesListMergePageList;
            }

            buttonSalesMergePageListNext.Enabled = true;
            buttonSalesMergePageListLast.Enabled = true;

            if (pageNumber == 1)
            {
                buttonSalesMergePageListFirst.Enabled = false;
                buttonSalesMergePageListPrevious.Enabled = false;
            }

            textBoxSalesMergePageNumber.Text = pageNumber + " / " + salesListMergePageList.PageCount;
        }

        private void buttonSalesMergePageListNext_Click(object sender, EventArgs e)
        {
            if (salesListMergePageList.HasNextPage == true)
            {
                salesListMergePageList = new PagedList<Entities.DgvTrnSalesMergeEntity>(salesListMergeData, ++pageNumber, pageSize);
                salesListMergeDataSource.DataSource = salesListMergePageList;
            }

            buttonSalesMergePageListFirst.Enabled = true;
            buttonSalesMergePageListPrevious.Enabled = true;

            if (pageNumber == salesListMergePageList.PageCount)
            {
                buttonSalesMergePageListNext.Enabled = false;
                buttonSalesMergePageListLast.Enabled = false;
            }

            textBoxSalesMergePageNumber.Text = pageNumber + " / " + salesListMergePageList.PageCount;
        }

        private void buttonSalesMergePageListLast_Click(object sender, EventArgs e)
        {
            salesListMergePageList = new PagedList<Entities.DgvTrnSalesMergeEntity>(salesListMergeData, salesListMergePageList.PageCount, pageSize);
            salesListMergeDataSource.DataSource = salesListMergePageList;

            buttonSalesMergePageListFirst.Enabled = true;
            buttonSalesMergePageListPrevious.Enabled = true;
            buttonSalesMergePageListNext.Enabled = false;
            buttonSalesMergePageListLast.Enabled = false;

            pageNumber = salesListMergePageList.PageCount;
            textBoxSalesMergePageNumber.Text = pageNumber + " / " + salesListMergePageList.PageCount;
        }

        private void dataGridViewSalesMerge_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                GetSalesMergeCurrentSelectedCell(e.RowIndex);
            }

            if (e.RowIndex > -1 && dataGridViewMergeSalesList.CurrentCell.ColumnIndex == dataGridViewMergeSalesList.Columns["ColumnMergeCheckbox"].Index)
            {
                Boolean selectedToMerge = Convert.ToBoolean(dataGridViewMergeSalesList.Rows[dataGridViewMergeSalesList.CurrentCell.RowIndex].Cells[dataGridViewMergeSalesList.Columns["ColumnMergeCheckbox"].Index].Value);

                if (selectedToMerge == false)
                {
                    dataGridViewMergeSalesList.CurrentRow.Cells[0].Value = true;
                }
                else
                {
                    dataGridViewMergeSalesList.CurrentRow.Cells[0].Value = false;
                }

                getTableCodeList();
            }
        }

        private void buttonMerge_Click(object sender, EventArgs e)
        {
            DialogResult splitDialogResult = MessageBox.Show("Merge bills? ", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (splitDialogResult == DialogResult.Yes)
            {
                if (dataGridViewMergeSalesList.Rows.Count > 0)
                {
                    List<Int32> salesIds = new List<Int32>();

                    for (var i = 0; i < dataGridViewMergeSalesList.Rows.Count; i++)
                    {
                        Boolean selectedToMerge = Convert.ToBoolean(dataGridViewMergeSalesList.Rows[i].Cells[dataGridViewMergeSalesList.Columns["ColumnMergeCheckbox"].Index].Value);
                        if (selectedToMerge == true)
                        {
                            Int32 salesId = Convert.ToInt32(dataGridViewMergeSalesList.Rows[i].Cells[dataGridViewMergeSalesList.Columns["ColumnMergeSalesId"].Index].Value);
                            salesIds.Add(salesId);
                        }
                    }

                    Int32 tableId = Convert.ToInt32(comboBoxTableCode.SelectedValue);

                    Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
                    String[] splitSales = trnPOSSalesController.MergeSales(salesIds, tableId);
                    if (splitSales[1].Equals("0") == false)
                    {
                        MessageBox.Show("Merge successful.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        trnPOSTouchForm.UpdateSalesListGridDataSource();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show(splitSales[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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
    }
}
