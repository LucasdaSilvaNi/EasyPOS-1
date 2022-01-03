using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Entities
{
    class SysReadingPrevAccNetSales
    {
        public Int32 Id { get; set; }
        public DateTime ReadingDate { get; set; }
        public Decimal AccumulatedNetSales { get; set; }
    }
}
