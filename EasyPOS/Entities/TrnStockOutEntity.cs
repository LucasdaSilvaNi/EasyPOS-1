using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Entities
{
    public class TrnStockOutEntity
    {
        public Int32 Id { get; set; }
        public Int32 PeriodId { get; set; }
        public String StockOutDate { get; set; }
        public String StockOutNumber { get; set; }
        public String ManualStockOutNumber { get; set; }
        public Int32 AccountId { get; set; }
        public String Account { get; set; }
        public String Remarks { get; set; }
        public Int32 PreparedBy { get; set; }
        public Int32 CheckedBy { get; set; }
        public Int32 ApprovedBy { get; set; }
        public Boolean IsLocked { get; set; }
        public Int32 EntryUserId { get; set; }
        public String EntryUserName { get; set; }
        public String EntryDateTime { get; set; }
        public String EntryTime { get; set; }
        public Int32 UpdateUserId { get; set; }
        public String UpdateUserName { get; set; }
        public String UpdateDateTime { get; set; }
        public String UpdateTime { get; set; }
    }
}
