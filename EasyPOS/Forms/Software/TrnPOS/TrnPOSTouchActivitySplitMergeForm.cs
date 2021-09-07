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
    public partial class TrnPOSTouchActivitySplitMergeForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;

        public TrnPOSTouchForm trnPOSTouchForm;
        public TrnPOSTouchActivityForm trnPOSTouchActivityForm;
        public Entities.TrnSalesEntity trnSalesEntity;

        public static Int32 pageNumber = 1;
        public static Int32 pageSize = 50;

        public static List<Entities.DgvTrnSalesItemSplitEntity> returnData = new List<Entities.DgvTrnSalesItemSplitEntity>();
        public PagedList<Entities.DgvTrnSalesItemSplitEntity> returnPageList = new PagedList<Entities.DgvTrnSalesItemSplitEntity>(returnData, pageNumber, pageSize);
        public BindingSource returnDataSource = new BindingSource();

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
                returnData = getSalesItemSplitData;
                returnPageList = new PagedList<Entities.DgvTrnSalesItemSplitEntity>(returnData, pageNumber, pageSize);

                if (returnPageList.PageCount == 1)
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
                else if (pageNumber == returnPageList.PageCount)
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

                textBoxSalesItemSplitPageNumber.Text = pageNumber + " / " + returnPageList.PageCount;
                returnDataSource.DataSource = returnPageList;
            }
            else
            {
                buttonSalesItemSplitPageListFirst.Enabled = false;
                buttonSalesItemSplitPageListPrevious.Enabled = false;
                buttonSalesItemSplitPageListNext.Enabled = false;
                buttonSalesItemSplitPageListLast.Enabled = false;

                pageNumber = 1;

                returnData = new List<Entities.DgvTrnSalesItemSplitEntity>();
                returnDataSource.Clear();
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
                                       ColumnSplitSalesTableId = 0,
                                       ColumnSalesItemTableCode = "",
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

            dataGridViewSalesItemSplitItems.DataSource = returnDataSource;
        }

        public void GetSalesItemSplitCurrentSelectedCell(Int32 rowIndex)
        {

        }

        private void buttonSalesItemSplitPageListFirst_Click(object sender, EventArgs e)
        {
            returnPageList = new PagedList<Entities.DgvTrnSalesItemSplitEntity>(returnData, 1, pageSize);
            returnDataSource.DataSource = returnPageList;

            buttonSalesItemSplitPageListFirst.Enabled = false;
            buttonSalesItemSplitPageListPrevious.Enabled = false;
            buttonSalesItemSplitPageListNext.Enabled = true;
            buttonSalesItemSplitPageListLast.Enabled = true;

            pageNumber = 1;
            textBoxSalesItemSplitPageNumber.Text = pageNumber + " / " + returnPageList.PageCount;
        }

        private void buttonSalesItemSplitPageListPrevious_Click(object sender, EventArgs e)
        {
            if (returnPageList.HasPreviousPage == true)
            {
                returnPageList = new PagedList<Entities.DgvTrnSalesItemSplitEntity>(returnData, --pageNumber, pageSize);
                returnDataSource.DataSource = returnPageList;
            }

            buttonSalesItemSplitPageListNext.Enabled = true;
            buttonSalesItemSplitPageListLast.Enabled = true;

            if (pageNumber == 1)
            {
                buttonSalesItemSplitPageListFirst.Enabled = false;
                buttonSalesItemSplitPageListPrevious.Enabled = false;
            }

            textBoxSalesItemSplitPageNumber.Text = pageNumber + " / " + returnPageList.PageCount;
        }

        private void buttonSalesItemSplitPageListNext_Click(object sender, EventArgs e)
        {
            if (returnPageList.HasNextPage == true)
            {
                returnPageList = new PagedList<Entities.DgvTrnSalesItemSplitEntity>(returnData, ++pageNumber, pageSize);
                returnDataSource.DataSource = returnPageList;
            }

            buttonSalesItemSplitPageListFirst.Enabled = true;
            buttonSalesItemSplitPageListPrevious.Enabled = true;

            if (pageNumber == returnPageList.PageCount)
            {
                buttonSalesItemSplitPageListNext.Enabled = false;
                buttonSalesItemSplitPageListLast.Enabled = false;
            }

            textBoxSalesItemSplitPageNumber.Text = pageNumber + " / " + returnPageList.PageCount;
        }

        private void buttonSalesItemSplitPageListLast_Click(object sender, EventArgs e)
        {
            returnPageList = new PagedList<Entities.DgvTrnSalesItemSplitEntity>(returnData, returnPageList.PageCount, pageSize);
            returnDataSource.DataSource = returnPageList;

            buttonSalesItemSplitPageListFirst.Enabled = true;
            buttonSalesItemSplitPageListPrevious.Enabled = true;
            buttonSalesItemSplitPageListNext.Enabled = false;
            buttonSalesItemSplitPageListLast.Enabled = false;

            pageNumber = returnPageList.PageCount;
            textBoxSalesItemSplitPageNumber.Text = pageNumber + " / " + returnPageList.PageCount;
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
                            ColumnSalesItemTableCode = dataGridViewSalesItemSplitItems.Rows[i].Cells[dataGridViewSalesItemSplitItems.Columns["ColumnSalesItemTableCode"].Index].Value.ToString()
                        });
                    }


                }
            }
        }
    }
}
