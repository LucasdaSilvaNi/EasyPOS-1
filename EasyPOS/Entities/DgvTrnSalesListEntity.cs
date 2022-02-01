using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Entities
{
    public class DgvTrnSalesListEntity
    {
        public String ColumnEdit { get; set; }
        public String ColumnDelete { get; set; }
        public Int32 ColumnId { get; set; }
        public String ColumnTerminal { get; set; }
        public String ColumnSalesDate { get; set; }
        public String ColumnSalesNumber { get; set; }
        public String ColumnManualSalesNumber { get; set; }
        public String ColumnRececiptInvoiceNumber { get; set; }
        public String ColumnCustomerCode { get; set; }
        public String ColumnCustomer { get; set; }
        public String ColumnSalesAgent { get; set; }
        public String ColumnCashier { get; set; }
        public String ColumnDelivery { get; set; }
        public String ColumnAmount { get; set; }
        public String ColumnSpace { get; set; }
        public Boolean ColumnIsLocked { get; set; }
        public Boolean ColumnIsTendered { get; set; }
        public Boolean ColumnIsCancelled { get; set; }
        public String ColumnTable { get; set; }
        public String ColumnRemarks { get; set; }
        public String ColumnTenderedAmount { get; set; }
    }
}
