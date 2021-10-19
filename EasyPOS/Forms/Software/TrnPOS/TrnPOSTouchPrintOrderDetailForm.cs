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
    public partial class TrnPOSTouchPrintOrderDetailForm : Form
    {
        public Entities.TrnSalesLineEntity trnSalesLineEntity;
        public Entities.TrnSalesEntity trnSalesEntity;

        public TrnPOSTouchPrintOrderDetailForm(Entities.TrnSalesEntity salesEntity)
        {
            InitializeComponent();

            trnSalesEntity = salesEntity;
            GetSalesLineList();
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if (Modules.SysCurrentModule.GetCurrentSettings().ChoosePrinter == true)
            {
                DialogResult printDialogResult = printDialogSelectPrinter.ShowDialog();
                if (printDialogResult == DialogResult.OK)
                {
                    if (trnSalesEntity.IsReturned == true)
                    {
                        new TrnPOSReturnReportForm(trnSalesEntity.Id);
                    }
                    else
                    {
                        if (Modules.SysCurrentModule.GetCurrentSettings().SalesOrderPrinterType == "Label Printer")
                        {
                            new TrnPOSSalesOrderReportFormLabelPrinter(trnSalesEntity.Id, printDialogSelectPrinter.PrinterSettings.PrinterName);
                        }
                        else
                        {
                            new TrnPOSSalesOrderReportForm(trnSalesEntity.Id, printDialogSelectPrinter.PrinterSettings.PrinterName);
                        }
                    }
                }
            }
            else
            {
                if (trnSalesEntity.IsReturned == true)
                {
                    new TrnPOSReturnReportForm(trnSalesEntity.Id);
                }
                else if (Modules.SysCurrentModule.GetCurrentSettings().SalesOrderPrinterType == "Kitchen Printer")
                {
                    new TrnPOSTouchOrderReportFormKitchenPrinter(trnSalesEntity.Id, "", dataGridViewPrintOrderSalesLineList);
                }
                else
                {
                    new TrnPOSSalesOrderReportForm(trnSalesEntity.Id, "");
                }
            }
        }

        public void GetSalesLineList()
        {

            dataGridViewPrintOrderSalesLineList.Rows.Clear();
            dataGridViewPrintOrderSalesLineList.Refresh();

            Controllers.TrnSalesLineController trnPOSSalesLineController = new Controllers.TrnSalesLineController();

            var salesLineList = trnPOSSalesLineController.ListSalesLine(trnSalesEntity.Id);
            if (salesLineList.Any())
            {

                foreach (var objSalesLineList in salesLineList)
                {
                    Boolean isPrinted = false;
                    if (objSalesLineList.IsPrinted != null)
                    {
                        isPrinted = Convert.ToBoolean(objSalesLineList.IsPrinted);
                    }

                    dataGridViewPrintOrderSalesLineList.Rows.Add(
                        objSalesLineList.Id,
                        objSalesLineList.SalesId,
                        objSalesLineList.ItemId,
                        isPrinted,
                        objSalesLineList.ItemDescription
                    );
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        //public Task<List<Entities.DgvTrnPOSTouchPrintOrderDetailEntity>> GetItemGroupItemListDataTask(Int32 salesId)
        //{
        //    Controllers.TrnSalesLineController trnPOSSalesLineController = new Controllers.TrnSalesLineController();

        //    Entities.TrnPOSTouchPrintOrderDetailEntity listPrintOrderItem = new Entities.TrnPOSTouchPrintOrderDetailEntity();
        //    if (listPrintOrderItem.Any())
        //    {
        //        var items = from d in listPrintOrderItem
        //                    select new Entities.DgvTrnPOSTouchPrintOrderDetailEntity
        //                    {
        //                        ColumnSalesLineListPrintOrderId = d.Id,
        //                        ColumnSalesLinetListPrintOrderItemId = d.ItemId,
        //                        ColumnSalesLineListPrintOrderSalesId = d.SalesId,
        //                        ColumnSalesLineListPrintOrderItemDescription = d.ItemDescription,
        //                        ColumnSalesLineListPrintOrderPrinted = d.IsPrinted != null ? Convert.ToBoolean(d.IsPrinted) : false
        //                    };

        //        return Task.FromResult(items.ToList());
        //    }
        //    else
        //    {
        //        return Task.FromResult(new List<Entities.DgvTrnPOSTouchPrintOrderDetailEntity>());
        //    }
        //}
    }
}
