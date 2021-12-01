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
    public partial class TrnPOSTouchActivityForm : Form
    {
        public SysSoftwareForm sysSoftwareForm;

        public TrnPOSTouchForm trnPOSTouchForm;
        public Entities.TrnSalesEntity trnSalesEntity;

        private Modules.SysUserRightsModule sysUserRights;

        public TrnPOSTouchActivityForm(SysSoftwareForm softwareForm, TrnPOSTouchForm POSTouchForm, Entities.TrnSalesEntity salesEntity)
        {
            InitializeComponent();

            sysSoftwareForm = softwareForm;
            trnPOSTouchForm = POSTouchForm;
            trnSalesEntity = salesEntity;

            sysUserRights = new Modules.SysUserRightsModule("TrnRestaurant");
            if (sysUserRights.GetUserRights() == null)
            {
                MessageBox.Show("No rights!", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (sysUserRights.GetUserRights().CanTender == false)
                {
                    buttonTender.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanPrint == false)
                {
                    buttonReprint.Enabled = false;
                }

                if (sysUserRights.GetUserRights().CanCancel == false)
                {
                    buttonCancel.Enabled = false;
                }

            }

            labelInvoiceNumber.Text = trnSalesEntity.SalesNumber;

            Boolean isLocked = trnSalesEntity.IsLocked;
            Boolean isTendered = trnSalesEntity.IsTendered;
            Boolean isCanclled = trnSalesEntity.IsCancelled;

            if (isLocked == true && isTendered == true && isCanclled == false)
            {
                buttonEditOrder.Enabled = false;
                buttonBillOut.Enabled = false;
                buttonPrintPartialBill.Enabled = false;
                buttonSplitMergeBill.Enabled = false;
                buttonTender.Enabled = false;
                buttonDeliver.Enabled = false;
                buttonDelete.Enabled = false;
            }
            else if (isLocked == true && isTendered == true && isCanclled == true)
            {
                buttonEditOrder.Enabled = false;
                buttonBillOut.Enabled = false;
                buttonPrintPartialBill.Enabled = false;
                buttonSplitMergeBill.Enabled = false;
                buttonTender.Enabled = false;
                buttonCancel.Enabled = false;
                buttonDeliver.Enabled = false;
            }
            else if (isLocked == true)
            {
                buttonDeliver.Enabled = false;
                buttonCancel.Enabled = false;
            }
            else
            {
                buttonCancel.Enabled = false;
            }


        }

        private void buttonEditOrder_Click(object sender, EventArgs e)
        {
            sysSoftwareForm.AddTabPagePOSTouchSalesDetail(trnPOSTouchForm, trnSalesEntity);
            Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonTender_Click(object sender, EventArgs e)
        {
            Boolean isLocked = trnSalesEntity.IsLocked;
            Boolean isTendered = trnSalesEntity.IsTendered;

            if (isTendered == true)
            {
                MessageBox.Show("Already tendered.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
                if (trnPOSSalesController.IsSalesTendered(trnSalesEntity.Id) == true)
                {
                    MessageBox.Show("Already tendered.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Modules.SysSerialPortModule.OpenSerialPort();

                    Entities.TrnSalesEntity newSalesEntity = new Entities.TrnSalesEntity
                    {
                        Id = trnSalesEntity.Id,
                        Amount = trnSalesEntity.Amount,
                        SalesNumber = trnSalesEntity.SalesNumber,
                        SalesDate = trnSalesEntity.SalesDate.ToString(),
                        Customer = trnSalesEntity.Customer,
                        Remarks = trnSalesEntity.Remarks
                    };

                    String line1 = Modules.SysCurrentModule.GetCurrentSettings().CustomerDisplayFirstLineMessage;
                    String line2 = "P " + newSalesEntity.Amount.ToString("#,##0.00");

                    if (newSalesEntity.Amount > 0)
                    {
                        line1 = "TOTAL:";
                    }

                    Modules.SysSerialPortModule.WriteSeralPortMessage(line1, line2);

                    TrnPOSTenderForm trnSalesDetailTenderForm = new TrnPOSTenderForm(sysSoftwareForm, null, null, trnPOSTouchForm, null, newSalesEntity);
                    trnSalesDetailTenderForm.ShowDialog();
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Boolean isCanclled = trnSalesEntity.IsCancelled;
            Boolean isTendered = trnSalesEntity.IsTendered;

            if (isCanclled == true)
            {
                MessageBox.Show("Already Cancelled.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (isTendered == true)
                {
                    TrnPOSCancelRemarksForm trnSalesListCancelRemarksForm = new TrnPOSCancelRemarksForm(sysSoftwareForm, null, trnPOSTouchForm, trnSalesEntity.Id);
                    trnSalesListCancelRemarksForm.ShowDialog();
                }
            }
        }

        private void buttonDeliver_Click(object sender, EventArgs e)
        {
            if (Modules.SysCurrentModule.GetCurrentSettings().EnableEasyShopIntegration == true)
            {
                TrnPOSTouchActivityDeliver trnPOSTouchActivityDeliver = new TrnPOSTouchActivityDeliver(trnPOSTouchForm, this, trnSalesEntity.Id, trnSalesEntity.Remarks);
                trnPOSTouchActivityDeliver.ShowDialog();
            }
            else
            {
                MessageBox.Show("Easyshop Integration is not enabled.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F2:
                    {
                        if (buttonEditOrder.Enabled == true)
                        {
                            buttonEditOrder.PerformClick();
                            Close();
                        }

                        break;
                    }
                case Keys.F3:
                    {
                        if (buttonBillOut.Enabled == true)
                        {
                            buttonBillOut.PerformClick();
                            Close();
                        }

                        break;
                    }
                case Keys.F4:
                    {
                        if (buttonTender.Enabled == true)
                        {
                            buttonTender.PerformClick();
                            Close();
                        }

                        break;
                    }
                case Keys.F5:
                    {
                        if (buttonReprint.Enabled == true)
                        {
                            buttonReprint.PerformClick();
                            Close();
                        }

                        break;
                    }
                case Keys.F6:
                    {
                        if (buttonDelete.Enabled == true)
                        {
                            buttonDelete.PerformClick();
                            Close();
                        }

                        break;
                    }
                case Keys.F7:
                    {
                        if (buttonCancel.Enabled == true)
                        {
                            buttonCancel.PerformClick();
                            Close();
                        }

                        break;
                    }
                case Keys.F8:
                    {
                        if (buttonDeliver.Enabled == true)
                        {
                            buttonDeliver.PerformClick();
                            Close();
                        }

                        break;
                    }
                case Keys.F9:
                    {
                        if (buttonPrintPartialBill.Enabled == true)
                        {
                            buttonPrintPartialBill.PerformClick();
                            Close();
                        }

                        break;
                    }
                case Keys.F10:
                    {
                        if (buttonSplitMergeBill.Enabled == true)
                        {
                            buttonSplitMergeBill.PerformClick();
                            Close();
                        }

                        break;
                    }
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

        private void buttonReprint_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean isLocked = trnSalesEntity.IsLocked;
                Boolean isTendered = trnSalesEntity.IsTendered;

                if (isTendered != true)
                {
                    MessageBox.Show("Not tendered.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (isLocked != true)
                {
                    MessageBox.Show("Not locked.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult cancelDialogResult = MessageBox.Show("Reprint Sales?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (cancelDialogResult == DialogResult.Yes)
                    {
                        DialogResult printDialogResult = printDialogReprintOR.ShowDialog();
                        if (printDialogResult == DialogResult.OK)
                        {
                            Int32 salesId = Convert.ToInt32(trnSalesEntity.Id);
                            Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
                            Int32 collectionId = trnPOSSalesController.GetCollectionId(trnSalesEntity.Id);
                            if (collectionId != 0)
                            {
                                new TrnPOSOfficialReceiptReportForm(salesId, collectionId, true, printDialogReprintOR.PrinterSettings.PrinterName);
                                Close();
                            }
                            else
                            {
                                MessageBox.Show("No collection.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonBillOut_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean isLocked = trnSalesEntity.IsLocked;

                if (isLocked != true)
                {
                    MessageBox.Show("Not locked.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Int32 salesId = Convert.ToInt32(trnSalesEntity.Id);
                    Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
                    Int32 collectionId = trnPOSSalesController.GetCollectionId(trnSalesEntity.Id);
                    new TrnPOSBillOutForm(salesId);
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Boolean isLocked = trnSalesEntity.IsLocked;
            Boolean isTendered = trnSalesEntity.IsTendered;

            if (isLocked == true)
            {
                MessageBox.Show("Already locked.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (isTendered == true)
            {
                MessageBox.Show("Already tendered.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DialogResult deleteDialogResult = MessageBox.Show("Delete Sales?", "Easy POS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (deleteDialogResult == DialogResult.Yes)
                {
                    Controllers.TrnSalesController trnPOSSalesController = new Controllers.TrnSalesController();
                    String[] deleteSales = trnPOSSalesController.DeleteSales(trnSalesEntity.Id);
                    if (deleteSales[1].Equals("0") == false)
                    {
                        trnPOSTouchForm.UpdateSalesListGridDataSource();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show(deleteSales[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonSplitMergeBill_Click(object sender, EventArgs e)
        {
            TrnPOSTouchActivitySplitMergeForm trnPOSTouchActivitySplitMergeForm = new TrnPOSTouchActivitySplitMergeForm(sysSoftwareForm, trnPOSTouchForm, this, trnSalesEntity);
            trnPOSTouchActivitySplitMergeForm.ShowDialog();
        }
    }
}
