using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Entities
{
    public class DgvTrnPurchaseOrderLineListEntity
    {
        public String ColumnPurchaseOrderLineListButtonEdit { get; set; }
        public String ColumnPurchaseOrderLineListButtonDelete { get; set; }
        public Int32 ColumnPurchaseOrderLineListId { get; set; }
        public Int32 ColumnPurchaseOrderLineListPurchaseOrderId { get; set; }
        public Int32 ColumnPurchaseOrderLineListItemId { get; set; }
        public String ColumnPurchaseOrderLineListItemDescription { get; set; }
        public Int32 ColumnPurchaseOrderLineListUnitId { get; set; }
        public String ColumnPurchaseOrderLineListUnit { get; set; }
        public Decimal ColumnPurchaseOrderLineListQuantity { get; set; }
        public Decimal ColumnPurchaseOrderLineListCost { get; set; }
        public Decimal ColumnPurchaseOrderLineListAmount { get; set; }
        public Decimal ColumnPurchaseOrderLineListReceivedQuantity { get; set; }
    }
}
