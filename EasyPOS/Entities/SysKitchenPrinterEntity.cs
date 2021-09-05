using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Entities
{
    public class SysKitchenPrinterEntity
    {
        public Int32 Id { get; set; }
        public String Kitchen { get; set; }
        public String PrinterName { get; set; }
        public String Alias { get; set; }
        public Int32 DefaultWidth { get; set; }
        public Int32 DefaultHeight { get; set; }
    }
}