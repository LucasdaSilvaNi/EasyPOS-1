using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Entities
{
    public class TrnPOSTouchPrintOrderDetailEntity
    {
        public Int32 Id { get; set; }
        public Int32 SalesId { get; set; }
        public Int32 ItemId { get; set; }
        public String ItemDescription { get; set; }
        public Boolean? IsPrinted { get; set; }
    }
}
