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
        public List<Entities.SysKitchenPrinterEntity> ListKitchen(String filter)
        {
            var kitchen = from d in db.SysKitchenPrinters
                          where d.Kitchen.Contains(filter) || d.PrinterName.Contains(filter)
                          select new Entities.SysKitchenPrinterEntity
                          {
                              Id = d.Id,
                              Kitchen = d.Kitchen,
                              PrinterName = d.PrinterName,
                              Alias = d.Alias,
                              DefaultWidth = d.DefaultWidth,
                              DefaultHeight = d.DefaultHeight
                          };

            return kitchen.OrderByDescending(d => d.Id).ToList();
        }

        public List<Entities.SysKitchenPrinterEntity> KitchenPerKitchenNumber(String kitchenNumber)
        {
            var kitchen = from d in db.SysKitchenPrinters
                          where d.Kitchen == kitchenNumber
                          select new Entities.SysKitchenPrinterEntity
                          {
                              Id = d.Id,
                              Kitchen = d.Kitchen,
                              PrinterName = d.PrinterName,
                              Alias = d.Alias,
                              DefaultWidth = d.DefaultWidth,
                              DefaultHeight = d.DefaultHeight
                          };

            return kitchen.OrderByDescending(d => d.Id).ToList();
        }

        // ======================
        // Update Kitchen Printer
        // ======================
        public String[] UpdateKitchenPrinter(Entities.SysKitchenPrinterEntity objKitchenPrinter)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current user not found.", "0" };
                }

                var kitchenPrinter = from d in db.SysKitchenPrinters
                                     where d.Id == objKitchenPrinter.Id
                                     select d;

                if (kitchenPrinter.Any())
                {
                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(kitchenPrinter.FirstOrDefault());

                    var updateKitchenPrinter = kitchenPrinter.FirstOrDefault();
                    updateKitchenPrinter.PrinterName = objKitchenPrinter.PrinterName;
                    updateKitchenPrinter.Alias = objKitchenPrinter.Alias;
                    updateKitchenPrinter.DefaultWidth = objKitchenPrinter.DefaultWidth;
                    updateKitchenPrinter.DefaultHeight = objKitchenPrinter.DefaultHeight;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(kitchenPrinter.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "SysKitchenPrinter",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "UpdateKitchenPrinter"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "" };
                }
                else
                {
                    return new String[] { "KitchenPrinter not found!", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }
    }
}
