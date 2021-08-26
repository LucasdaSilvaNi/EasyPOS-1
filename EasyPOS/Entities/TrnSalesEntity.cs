using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Entities
{
    public class TrnSalesEntity
    {
        public Int32 Id { get; set; }
        public Int32 PeriodId { get; set; }
        public String Period { get; set; }
        public String SalesDate { get; set; }
        public String SalesNumber { get; set; }
        public String ManualInvoiceNumber { get; set; }
        public String CollectionNumber { get; set; }
        public Decimal Amount { get; set; }
        public Int32? TableId { get; set; }
        public String Table { get; set; }
        public Int32 TableStatus { get; set; }
        public Int32 CustomerId { get; set; }
        public String CustomerCode { get; set; }
        public String Customer { get; set; }
        public String CustomerAddress { get; set; }
        public Int32 AccountId { get; set; }
        public Int32 TermId { get; set; }
        public String Term { get; set; }
        public Int32? DiscountId { get; set; }
        public Decimal DiscountRate { get; set; }
        public Decimal DiscountAmount { get; set; }
        public String SeniorCitizenId { get; set; }
        public String SeniorCitizenName { get; set; }
        public Int32? SeniorCitizenAge { get; set; }
        public String Remarks { get; set; }
        public Int32? SalesAgent { get; set; }
        public String SalesAgentUserName { get; set; }
        public Int32 TerminalId { get; set; }
        public String Terminal { get; set; }
        public Int32? DeliveryId { get; set; }
        public String DeliveryDriver { get; set; }
        public Int32 PreparedBy { get; set; }
        public String PreparedByUserName { get; set; }
        public Int32 CheckedBy { get; set; }
        public String CheckedByUserName { get; set; }
        public Int32 ApprovedBy { get; set; }
        public String ApprovedByUserName { get; set; }
        public Boolean IsLocked { get; set; }
        public Boolean IsTendered { get; set; }
        public Boolean IsCancelled { get; set; }
        public Boolean IsDispatched { get; set; }
        public Boolean IsReturned { get; set; }
        public String ReturnApplication { get; set; }
        public Decimal PaidAmount { get; set; }
        public Decimal CreditAmount { get; set; }
        public Decimal DebitAmount { get; set; }
        public Decimal BalanceAmount { get; set; }
        public Int32 EntryUserId { get; set; }
        public String EntryUserName { get; set; }
        public String EntryUserUserName { get; set; }
        public String EntryDateTime { get; set; }
        public String EntryTime { get; set; }
        public Int32 UpdateUserId { get; set; }
        public String UpdatedUserName { get; set; }
        public String UpdatedUserUserName { get; set; }
        public String UpdateTime { get; set; }
        public String UpdateDateTime { get; set; }
        public Int32? Pax { get; set; }
        public Int32? DiscountedPax { get; set; }
        public String PostCode { get; set; }
        public Decimal NumberOfItems { get; set; }
        public Decimal NumberOfItemsPrepared { get; set; }
        public String Status { get; set; }
    }

    public class SysDeliverDriver
    {
        public List<SysDeliverDriverData> data { get; set; }
    }

    public class SysDeliverDriverData
    {
        public Int32 id { get; set; }
        public String type { get; set; }
        public SysDeliverDriverAttributes attributes { get; set; }
    }
    public class SysDeliverDriverAttributes
    {
        public Int32 id { get; set; }
        public String uuid { get; set; }
        public String first_name { get; set; }
        public String last_name { get; set; }
        public String status { get; set; }
        public String gender { get; set; }
    }

    public class SysDriver
    {
        public Int32 Id { get; set; }
        public String FullName { get; set; }
    }

    public class SysDelivery
    {
        public SysDeliveryOrder delivery { get; set; }
    }

    public class SysDeliveryOrder
    {
        public String branch_code { get; set; }
        public String order_id { get; set; }
        public String total { get; set; }
        public String driver_id { get; set; }
    }
}
