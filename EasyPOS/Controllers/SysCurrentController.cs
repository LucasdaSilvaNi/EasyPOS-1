using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Controllers
{
    class SysCurrentController
    {
        // ============
        // Data Context
        // ============
        public Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

        // ===============
        // Terminal - List
        // ===============
        public List<Entities.MstTerminalEntity> DropDownTerminalList()
        {
            var terminals = from d in db.MstTerminals
                            select new Entities.MstTerminalEntity
                            {
                                Id = d.Id,
                                Terminal = d.Terminal
                            };

            return terminals.ToList();
        }

        // =============
        // Period - List
        // =============
        public List<Entities.MstPeriodEntity> DropDownPeriodList()
        {
            var periods = from d in db.MstPeriods
                          select new Entities.MstPeriodEntity
                          {
                              Id = d.Id,
                              Period = d.Period
                          };

            return periods.ToList();
        }

        // ===============
        // Customer - List
        // ===============
        public List<Entities.MstCustomerEntity> DropDownCustomerList()
        {
            var customers = from d in db.MstCustomers
                            select new Entities.MstCustomerEntity
                            {
                                Id = d.Id,
                                Customer = d.Customer
                            };

            return customers.OrderByDescending(d => d.Id).ToList();
        }

        // ===============
        // Discount - List
        // ===============
        public List<Entities.MstDiscountEntity> DropDownDiscountList()
        {
            var discounts = from d in db.MstDiscounts
                            select new Entities.MstDiscountEntity
                            {
                                Id = d.Id,
                                Discount = d.Discount
                            };

            return discounts.OrderByDescending(d => d.Id).ToList();
        }

        // ===============
        // Supplier - List
        // ===============
        public List<Entities.MstSupplierEntity> DropDownSupplierList()
        {
            var suppliers = from d in db.MstSuppliers
                            select new Entities.MstSupplierEntity
                            {
                                Id = d.Id,
                                Supplier = d.Supplier
                            };

            return suppliers.OrderByDescending(d => d.Id).ToList();
        }

        // ===============
        // Language - List
        // ===============
        public List<Entities.SysLanguageEntity> DropDownLanguageList()
        {
            var language = from d in db.SysLabels
                            select new Entities.SysLanguageEntity
                            {
                                Id = d.Id,
                                Label = d.Label,
                                DisplayedLabel = d.DisplayedLabel,
                                Language = d.Language
                            };

            return language.OrderByDescending(d => d.Id).ToList();
        }

        // ===================
        // SysCurrent - Update
        // ===================
        public String[] UpdateSysCurrent(Entities.SysCurrentEntity objSysCurrent)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;

                if (currentUserLogin.Any())
                {
                    Modules.SysCurrentModule.UpdateCurrentSettings(objSysCurrent);
                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Current login user not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ============
        // Get Currents
        // ============
        public Entities.SysCurrentEntity GetSysCurrent()
        {
            return Modules.SysCurrentModule.GetCurrentSettings();
        }
    }
}
