using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Entities
{
    public class TrnCollectionEntity
    {
        public Int32 Id { get; set; }
        public Int32 PeriodId { get; set; }
        public String Period { get; set; }
        public String CollectionDate { get; set; }
        public String CollectionNumber { get; set; }
        public Int32 TerminalId { get; set; }
        public String Terminal { get; set; }
        public String CancelledCollectionNumber { get; set; }
        public String ManualORNumber { get; set; }
        public Int32 CustomerId { get; set; }
        public String CustomerCode { get; set; }
        public String Customer { get; set; }
        public String Remarks { get; set; }
        public Int32? SalesId { get; set; }
        public String SalesNumber { get; set; }
        public Decimal SalesBalanceAmount { get; set; }
        public Decimal Amount { get; set; }
        public Decimal TenderAmount { get; set; }
        public Decimal ChangeAmount { get; set; }
        public Int32 PreparedBy { get; set; }
        public String PreparedByUserName { get; set; }
        public Int32 CheckedBy { get; set; }
        public String CheckedByUserName { get; set; }
        public Int32 ApprovedBy { get; set; }
        public String ApprovedByUserName { get; set; }
        public Boolean IsCancelled { get; set; }
        public Boolean IsLocked { get; set; }
        public Int32 EntryUserId { get; set; }
        public String EntryUserName { get; set; }
        public String EntryDateTime { get; set; }
        public String EntryTime { get; set; }
        public Int32 UpdateUserId { get; set; }
        public String UpdatedUserName { get; set; }
        public String UpdateDateTime { get; set; }
        public String UpdateTime { get; set; }
        public List<TrnCollectionLineEntity> CollectionLines { get; set; }
        public Decimal VATSales { get; set; }
        public Decimal VATAmount { get; set; }
        public Decimal NonVAT { get; set; }
        public Decimal VATExempt { get; set; }
        public Decimal VATZeroRated { get; set; }
    }
}
