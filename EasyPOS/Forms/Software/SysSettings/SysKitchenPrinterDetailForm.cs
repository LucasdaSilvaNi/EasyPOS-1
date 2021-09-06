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

namespace EasyPOS.Forms.Software.SysSettings
{
    public partial class SysKitchenPrinterDetailForm : Form
    {
        SysSettingsForm SysSettingsForm;
        SysKitchenPrinterEntity sysKitchenPrinterEntity;

        public SysKitchenPrinterDetailForm(SysSettingsForm sysSystemTablesForm, SysKitchenPrinterEntity kitchenPrinterEntity)
        {
            InitializeComponent();

            SysSettingsForm = sysSystemTablesForm;
            sysKitchenPrinterEntity = kitchenPrinterEntity;

            LoadAccount();
        }

        public void LoadAccount()
        {
            if (sysKitchenPrinterEntity != null)
            {
                textBoxKitchen.Text = sysKitchenPrinterEntity.Kitchen;
                textBoxKitchenPrinter.Text = sysKitchenPrinterEntity.PrinterName;
                textBoxAlias.Text = sysKitchenPrinterEntity.Alias;
                textBoxDefaultWidth.Text = sysKitchenPrinterEntity.DefaultWidth.ToString();
                textBoxDefaultHeight.Text = sysKitchenPrinterEntity.DefaultHeight.ToString();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (sysKitchenPrinterEntity == null)
            {

            }
            else
            {
                sysKitchenPrinterEntity.Kitchen = textBoxKitchen.Text;
                sysKitchenPrinterEntity.PrinterName = textBoxKitchenPrinter.Text;
                sysKitchenPrinterEntity.Alias = textBoxAlias.Text;
                sysKitchenPrinterEntity.DefaultWidth = Convert.ToInt32(textBoxDefaultWidth.Text);
                sysKitchenPrinterEntity.DefaultHeight = Convert.ToInt32(textBoxDefaultHeight.Text);

                Controllers.SysKitchenPrinterController sysKitchenPrinterController = new Controllers.SysKitchenPrinterController();
                String[] updateKitchenPrinter = sysKitchenPrinterController.UpdateKitchenPrinter(sysKitchenPrinterEntity);
                if (updateKitchenPrinter[1].Equals("0") == true)
                {
                    MessageBox.Show(updateKitchenPrinter[0], "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    SysSettingsForm.UpdateKitchenDataSource();
                    Close();
                }
            }
        }

        private void textBoxDefaultWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxDefaultHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
