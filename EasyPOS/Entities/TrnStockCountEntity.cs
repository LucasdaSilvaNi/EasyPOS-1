using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Entities
{
    public class TrnStockCountEntity
    {
        public Int32 Id { get; set; }
        public Int32 PeriodId { get; set; }
        public String StockCountDate { get; set; }
        public String StockCountNumber { get; set; }
        public String Remarks { get; set; }
        public Int32 PreparedBy { get; set; }
        public Int32 CheckedBy { get; set; }
        public Int32 ApprovedBy { get; set; }
        public Boolean IsLocked { get; set; }
        public Int32 EntryUserId { get; set; }
        public String EntryUserUserName { get; set; }
        public String EntryDateTime { get; set; }
        public String EntryTime { get; set; }
        public Int32 UpdateUserId { get; set; }
        public String UpdateUserUserName { get; set; }
        public String UpdateDateTime { get; set; }
        public String UpdateTime { get; set; }
    }
}
