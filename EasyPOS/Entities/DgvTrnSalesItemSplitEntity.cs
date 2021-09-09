using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Entities
{
    public class DgvTrnSalesItemSplitEntity
    {
        public Int32 ColumnSplitSalesItemId { get; set; }
        public String ColumnSplitSalesItemDescription { get; set; }
        public String ColumnSalesItemUnit { get; set; }
        public String ColumnSalesItemQuantity { get; set; }
        public String ColumnSalesItemButtonPickTable { get; set; }
        public Int32 ColumnSplitSalesTableId { get; set; }
        public String ColumnSalesItemTableCode { get; set; }
        public Int32 ColumnSalesLineId { get; set; }
    }
}
