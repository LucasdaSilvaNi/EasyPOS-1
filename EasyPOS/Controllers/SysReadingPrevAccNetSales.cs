using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Controllers
{
    class SysReadingPrevAccNetSales
    {
        // ============
        // Data Context
        // ============
        public Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

        public String[] InsertPrevAccNetSales(Entities.SysReadingPrevAccNetSales objPrevAccNetSales)
        {
            Data.SysReadingPrevAccNetSale addPrevAccNetSales = new Data.SysReadingPrevAccNetSale()
            {
                ReadingDate = objPrevAccNetSales.ReadingDate,
                AccumulatedNetSales = objPrevAccNetSales.AccumulatedNetSales,
            };

            db.SysReadingPrevAccNetSales.InsertOnSubmit(addPrevAccNetSales);
            db.SubmitChanges();

            return new string[] { "", "" };
        }

        public String[] UpdatePrevAccNetSales(DateTime readingDate, Entities.SysReadingPrevAccNetSales objPrevAccNetSales)
        {
            try
            {
                var prevAccNetSales = from d in db.SysReadingPrevAccNetSales
                           where d.ReadingDate == readingDate
                           select d;

                if (prevAccNetSales.Any())
                {
                    var updatedPreAccNetSales = prevAccNetSales.FirstOrDefault();
                    updatedPreAccNetSales.AccumulatedNetSales = objPrevAccNetSales.AccumulatedNetSales;
                    db.SubmitChanges();
                }

                return new String[] { "", "" };
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }
    }
}
