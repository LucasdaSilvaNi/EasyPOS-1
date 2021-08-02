using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Entities
{
    public class DgvMstCustomerLoadListEntity
    {
        public Int32 ColumnCustomerLoadId { get; set; }
        public String ColumnCustomerLoadCustomerId { get; set; }
        public String ColumnCustomerLoadCardNumber { get; set; }
        public String ColumnCustomerLoadLoadDate { get; set; }
        public String ColumnCustomerLoadType { get; set; }
        public String ColumnCustomerLoadAmount { get; set; }
        public String ColumnCustomerLoadRemarks { get; set; }
    }
}