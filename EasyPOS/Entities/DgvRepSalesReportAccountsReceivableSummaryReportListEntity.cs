using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Entities
{
    public class DgvRepSalesReportAccountsReceivableSummaryReportListEntity
    {
        public String ColumnCustomer { get; set; }
        public String ColumnTerm { get; set; }
        public String ColumnCreditLimit { get; set; }
        public String ColumnSalesNumber { get; set; }
        public String ColumnSalesDate { get; set; }
        public String ColumnSalesAmount { get; set; }
        public String ColumnPaymentAmount { get; set; }
        public String ColumnBalanceAmount { get; set; }
        public String ColumnDueDate { get; set; }
        public String ColumnCurrent { get; set; }
        public String Column30Days { get; set; }
        public String Column60Days { get; set; }
        public String Column90Days { get; set; }
        public String Column120Days { get; set; }
    }
}
