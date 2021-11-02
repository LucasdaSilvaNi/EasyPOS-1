using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Entities
{
    public class DgvStockLevelReportEntity
    {
        public String ColumnItemListButtonEdit { get; set; }
        public String ColumnItemListButtonDelete { get; set; }
        public Int32 ColumnItemListId { get; set; }
        public String ColumnItemListBarcode { get; set; }
        public String ColumnItemListCode { get; set; }
        public String ColumnItemListDescription { get; set; }
        public String ColumnItemListUnit { get; set; }
        public String ColumnItemListCategory { get; set; }
        public String ColumnItemListCost { get; set; }
        public String ColumnItemListPrice { get; set; }
        public String ColumnItemListStockLevelQuantity { get; set; }
        public String ColumnItemListOnHandQuantity { get; set; }
        public Boolean ColumnItemListIsInventory { get; set; }
        public Boolean ColumnItemListIsLocked { get; set; }
        public String ColumnSupplier { get; set; }
    }
}
