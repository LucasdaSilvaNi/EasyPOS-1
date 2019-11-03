﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyPOS.Forms.Software.RepSalesReport
{
    public partial class RepSalesReportForm : Form
    {
        SysSoftwareForm sysSoftwareForm;
        public RepSalesReportForm(SysSoftwareForm softwareForm)
        {
            InitializeComponent();

            sysSoftwareForm = softwareForm;
        }

        public void GetTerminalList()
        {
            Controllers.RepSalesReportController repSalesReportController = new Controllers.RepSalesReportController();
            if (repSalesReportController.DropdownListTerminal().Any())
            {
                comboBoxTerminal.DataSource = repSalesReportController.DropdownListTerminal();
                comboBoxTerminal.ValueMember = "Id";
                comboBoxTerminal.DisplayMember = "Terminal";
            }
        }

        private void listBoxSalesReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSalesReport.SelectedItem != null)
            {
                String selectedItem = listBoxSalesReport.SelectedItem.ToString();
                switch (selectedItem)
                {
                    case "Sales Summary Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;
                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;
                        comboBoxTerminal.Visible = false;
                        labelTerminal.Visible = false;
                        break;
                    case "Sales Detail Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;
                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;
                        comboBoxTerminal.Visible = false;
                        labelTerminal.Visible = false;
                        break;
                    case "Cancel Sales Summary Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;
                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;
                        comboBoxTerminal.Visible = false;
                        labelTerminal.Visible = false;
                        break;
                    case "Collection Summary Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;
                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;
                        GetTerminalList();
                        comboBoxTerminal.Visible = true;
                        labelTerminal.Visible = true;
                        break;
                    case "Collection Detail Report":
                        labelStartDate.Visible = true;
                        dateTimePickerStartDate.Visible = true;
                        labelEndDate.Visible = true;
                        dateTimePickerEndDate.Visible = true;
                        GetTerminalList();
                        comboBoxTerminal.Visible = true;
                        labelTerminal.Visible = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Please select a report.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_OnClick(object sender, EventArgs e)
        {
            sysSoftwareForm.RemoveTabPage();
        }

        private void buttonView_OnClick(object sender, EventArgs e)
        {
            if (listBoxSalesReport.SelectedItem != null)
            {
                String selectedItem = listBoxSalesReport.SelectedItem.ToString();
                switch (selectedItem)
                {
                    case "Sales Summary Report":
                        RepSalesReportSalesSummaryReportForm repSalesSummaryReport = new RepSalesReportSalesSummaryReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date);
                        repSalesSummaryReport.ShowDialog();
                        break;
                    case "Sales Detail Report":
                        RepSalesReportSalesDetailReportForm repSalesReportSalesDetail = new RepSalesReportSalesDetailReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date);
                        repSalesReportSalesDetail.ShowDialog();
                        break;
                    case "Cancel Sales Summary Report":
                        RepSalesReportCancelSalesSummaryReportForm repCancelSalesSummaryReport = new RepSalesReportCancelSalesSummaryReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date);
                        repCancelSalesSummaryReport.ShowDialog();
                        break;
                    case "Collection Summary Report":
                        RepSalesReportCollectionSummaryReport reportCollectionReport = new RepSalesReportCollectionSummaryReport(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue));
                        reportCollectionReport.ShowDialog();
                        break;
                    case "Collection Detail Report":
                        RepSalesReportCollectionDetailReportForm reportCollectionDetailReportForm = new RepSalesReportCollectionDetailReportForm(dateTimePickerStartDate.Value.Date, dateTimePickerEndDate.Value.Date, Convert.ToInt32(comboBoxTerminal.SelectedValue));
                        reportCollectionDetailReportForm.ShowDialog();
                        break;

                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Please select a report.", "Easy POS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}