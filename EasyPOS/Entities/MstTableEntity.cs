using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Entities
{
    public class MstTableEntity
    {
        public Int32 Id { get; set; }
        public String TableCode { get; set; }
        public Int32 TableGroupId { get; set; }
        public String TableGroup { get; set; }
        public Int32? TopLocation { get; set; }
        public Int32? LeftLocation { get; set; }
        public Boolean HasSales { get; set; }
    }
}
