using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Controllers
{
    class SysReadingPrintCountController
    {
        // ============
        // Data Context
        // ============
        public Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

        public String[] InsertPrintCount(Entities.SysReadingPrintCount objPrintCount)
        {
            Data.SysReadingPrintCount addPrintCount = new Data.SysReadingPrintCount()
            {
                PrintDate = objPrintCount.PrintDate,
                PrintCount = objPrintCount.PrintCount,
                PrintType = objPrintCount.PrintType,
                UserId = objPrintCount.UserId
            };

            db.SysReadingPrintCounts.InsertOnSubmit(addPrintCount);
            db.SubmitChanges();

            return new string[] { "", "" };
        }
    }
}
