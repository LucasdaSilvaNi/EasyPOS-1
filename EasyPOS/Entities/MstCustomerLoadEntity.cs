using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Entities
{
    public class MstCustomerLoadEntity
    {
        public Int32 Id { get; set; }
        public Int32 CustomerId { get; set; }
        public String CardNumber { get; set; }
        public String LoadDate { get; set; }
        public String Type { get; set; }
        public Decimal Amount { get; set; }
        public String Remarks { get; set; }
    }
}