using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Entities
{
    public class DgvTrnPOSTouchPrintOrderDetailEntity
    {
        public Int32 ColumnSalesLineListPrintOrderId { get; set; }
        public Int32 ColumnSalesLineListPrintOrderSalesId { get; set; }
        public Int32 ColumnSalesLinetListPrintOrderItemId { get; set; }
        public Boolean ColumnSalesLineListPrintOrderPrinted { get; set; }
        public String ColumnSalesLineListPrintOrderItemDescription { get; set; }
    }
}
