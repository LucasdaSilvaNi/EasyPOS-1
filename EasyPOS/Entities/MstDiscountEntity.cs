using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Entities
{
    public class MstDiscountEntity
    {
        public Int32 Id { get; set; }
        public String Discount { get; set; }
        public Decimal DiscountRate { get; set; }
        public Decimal DiscountAmount { get; set; }
        public Boolean IsVatExempt { get; set; }
        public Boolean IsDateScheduled { get; set; }
        public String DateStart { get; set; }
        public String DateEnd { get; set; }
        public Boolean IsTimeScheduled { get; set; }
        public String TimeStart { get; set; }
        public String TimeEnd { get; set; }
        public Boolean IsDayScheduled { get; set; }
        public Boolean DayMon { get; set; }
        public Boolean DayTue { get; set; }
        public Boolean DayWed { get; set; }
        public Boolean DayThu { get; set; }
        public Boolean DayFri { get; set; }
        public Boolean DaySat { get; set; }
        public Boolean DaySun { get; set; }
        public Int32 EntryUserId { get; set; }
        public String EntryUserName { get; set; }
        public String EntryUserUserName { get; set; }
        public String EntryDateTime { get; set; }
        public String EntryTime { get; set; }
        public Int32 UpdateUserId { get; set; }
        public String UpdatedUserName { get; set; }
        public String UpdatedUserUserName { get; set; }
        public String UpdateDateTime { get; set; }
        public String UpdateTime { get; set; }
        public Boolean IsLocked { get; set; }
    }
}
