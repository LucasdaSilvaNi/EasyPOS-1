using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Entities
{
    public class RepPOSReportXReadingReportEntity
    {
        public String SalesAgent { get; set; }
        public String Date { get; set; }
        public Decimal TotalGrossSales { get; set; }
        public Decimal TotalRegularDiscount { get; set; }
        public Decimal TotalSeniorDiscount { get; set; }
        public Decimal TotalPWDDiscount { get; set; }
        public Decimal TotalSalesReturn { get; set; }
        public Decimal TotalNetSales { get; set; }
        public List<TrnCollectionLineEntity> CollectionLines { get; set; }
        public Decimal TotalRefund { get; set; }
        public Decimal TotalCollection { get; set; }
        public Decimal TotalVATSales { get; set; }
        public Decimal TotalVATAmount { get; set; }
        public Decimal TotalNonVAT { get; set; }
        public Decimal TotalVATExclusive { get; set; }
        public Decimal TotalVATExempt { get; set; }
        public Decimal TotalVATZeroRated { get; set; }
        public String CounterIdStart { get; set; }
        public String CounterIdEnd { get; set; }
        public Decimal TotalCancelledTrnsactionCount { get; set; }
        public Decimal TotalCancelledAmount { get; set; }
        public Decimal TotalNumberOfTransactions { get; set; }
        public Decimal TotalNumberOfSKU { get; set; }
        public Decimal TotalQuantity { get; set; }
        public Decimal TotalPreviousReading { get; set; }
        public Decimal RunningTotal { get; set; }
        public Int32 XPrintCount { get; set; }
    }
}
