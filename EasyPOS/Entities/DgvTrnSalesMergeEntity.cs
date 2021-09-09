using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Entities
{
    public class DgvTrnSalesMergeEntity
    {
        public Boolean ColumnMergeCheckbox { get; set; }
        public Int32 ColumnMergeSalesId { get; set; }
        public String ColumnMergeSalesNumber { get; set; }
        public Int32 ColumnMergeTableId { get; set; }
        public String ColumnMergeTableCode { get; set; }
        public String ColumnMergeSalesAmount { get; set; }
        public String ColumnMergeSalesUser { get; set; }
    }
}
