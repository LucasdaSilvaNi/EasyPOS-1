using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Entities
{
    class SysReadingPrintCount
    {
        public Int32 Id { get; set; }
        public DateTime PrintDate { get; set; }
        public Int32 PrintCount { get; set; }
        public String PrintType { get; set; }
        public Int32 UserId { get; set; }
    }
}
