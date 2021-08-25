using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Controllers
{
    class SysKitchenPrinterController
    {
        // ============
        // Data Context
        // ============
        public Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

        // ============
        // List Kitchen
        // ============
        public List<Entities.SysKitchenPrinterEntity> ListKitchen()
        {
            var kitchen = from d in db.SysKitchenPrinters
                        select new Entities.SysKitchenPrinterEntity
                        {
                            Id = d.Id,
                            Kitchen = (int)d.Kitchen,
                            Printer = d.Printer,
                        };

            return kitchen.OrderByDescending(d => d.Id).ToList();


        }
    }
       
}
