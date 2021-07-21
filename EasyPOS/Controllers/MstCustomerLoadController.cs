using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Controllers
{
    class MstCustomerLoadController
    {
        // ============
        // Data Context
        // ============
        public Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

        // ==================
        // List Customer Load
        // ==================
        public List<Entities.MstCustomerLoadEntity> ListCustomerLoad(Int32 customerId)
        {
            var customerLoads = from d in db.MstCustomerLoads
                                where d.CustomerId == customerId
                                select new Entities.MstCustomerLoadEntity
                                {
                                    Id = d.Id,
                                    CustomerId = d.CustomerId,
                                    CardNumber = d.CardNumber,
                                    LoadDate = d.LoadDate.ToShortDateString(),
                                    Type = d.Type,
                                    Amount = d.Amount,
                                    Remarks = d.Remarks
                                };

            return customerLoads.OrderByDescending(d => d.Id).ToList();
        }

        // =================
        // Add Customer Load
        // =================
        public String[] AddCustomerLoad(Entities.MstCustomerLoadEntity objCustomerLoad)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                Data.MstCustomerLoad addCustomerLoad = new Data.MstCustomerLoad()
                {
                    CustomerId = objCustomerLoad.CustomerId,
                    CardNumber = objCustomerLoad.CardNumber,
                    LoadDate = Convert.ToDateTime(objCustomerLoad.LoadDate),
                    Type = objCustomerLoad.Type,
                    Amount = objCustomerLoad.Amount,
                    Remarks = objCustomerLoad.Remarks,
                };

                db.MstCustomerLoads.InsertOnSubmit(addCustomerLoad);
                db.SubmitChanges();

                var customer = from d in db.MstCustomers
                               where d.Id == objCustomerLoad.CustomerId
                               select d;

                if (customer.Any())
                {
                    Decimal totalLoads = 0;

                    var customerLoads = from d in db.MstCustomerLoads
                                        where d.CustomerId == objCustomerLoad.CustomerId
                                        select d;

                    if (customerLoads.Any())
                    {
                        totalLoads = customerLoads.Sum(d => d.Amount);
                    }

                    var updateCustomer = customer.FirstOrDefault();
                    updateCustomer.LoadAmount = totalLoads;
                    db.SubmitChanges();
                }

                String newObject = Modules.SysAuditTrailModule.GetObjectString(addCustomerLoad);

                Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                {
                    UserId = currentUserLogin.FirstOrDefault().Id,
                    AuditDate = DateTime.Now,
                    TableInformation = "MstCustomerLoad",
                    RecordInformation = "",
                    FormInformation = newObject,
                    ActionInformation = "AddCustomerLoad"
                };
                Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                return new String[] { "", "" };
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ====================
        // Update Customer Load
        // ====================
        public String[] UpdateCustomerLoad(Entities.MstCustomerLoadEntity objCustomerLoad)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var customerLoad = from d in db.MstCustomerLoads
                                   where d.Id == objCustomerLoad.Id
                                   select d;

                if (customerLoad.Any())
                {
                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(customerLoad.FirstOrDefault());

                    var updateCustomerLoad = customerLoad.FirstOrDefault();
                    updateCustomerLoad.LoadDate = Convert.ToDateTime(objCustomerLoad.LoadDate);
                    updateCustomerLoad.Type = objCustomerLoad.Type;
                    updateCustomerLoad.Amount = objCustomerLoad.Amount;
                    updateCustomerLoad.Remarks = objCustomerLoad.Remarks;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(customerLoad.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "MstCustomerLoad",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "UpdateCustomerLoad"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "" };
                }
                else
                {
                    return new String[] { "Item price not found!", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ====================
        // Delete Customer Load
        // ====================
        public String[] DeleteCustomerLoad(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var customerLoad = from d in db.MstCustomerLoads
                                   where d.Id == id
                                   select d;

                if (customerLoad.Any())
                {
                    var deleteCustomerLoad = customerLoad.FirstOrDefault();
                    db.MstCustomerLoads.DeleteOnSubmit(deleteCustomerLoad);

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(customerLoad.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "MstCustomerLoad",
                        RecordInformation = oldObject,
                        FormInformation = "",
                        ActionInformation = "DeleteCustomerLoad"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    db.SubmitChanges();

                    return new String[] { "", "" };
                }
                else
                {
                    return new String[] { "Item price not found!", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }
    }
}
