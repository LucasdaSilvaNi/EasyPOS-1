using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Controllers
{
    class MstItemPriceController
    {
        // ============
        // Data Context
        // ============
        public Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

        // ===============
        // List Item Price
        // ===============
        public List<Entities.MstItemPriceEntity> ListItemPrice(Int32 itemId)
        {
            var itemPrices = from d in db.MstItemPrices
                             where d.ItemId == itemId
                             select new Entities.MstItemPriceEntity
                             {
                                 Id = d.Id,
                                 PriceDescription = d.PriceDescription,
                                 Price = d.Price,
                                 TriggerQuantity = d.TriggerQuantity
                             };

            return itemPrices.OrderByDescending(d => d.Id).ToList();
        }
        // ===============
        // Triggered Item Price
        // ===============
        public List<Entities.MstItemPriceEntity> TriggeredItemPrice(Int32 itemId, Decimal quantity)
        {
            var itemPrices = from d in db.MstItemPrices
                             where d.ItemId == itemId
                             && d.TriggerQuantity <= quantity
                             select new Entities.MstItemPriceEntity
                             {
                                 Id = d.Id,
                                 PriceDescription = d.PriceDescription,
                                 Price = d.Price,
                                 TriggerQuantity = d.TriggerQuantity
                             };

            return itemPrices.OrderByDescending(d => d.Id).ToList();
        }

        // ==============
        // Add Item Price
        // ==============
        public String[] AddItemPrice(Entities.MstItemPriceEntity objItemPrice)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                Data.MstItemPrice addItemPrice = new Data.MstItemPrice()
                {
                    ItemId = objItemPrice.ItemId,
                    PriceDescription = objItemPrice.PriceDescription,
                    Price = objItemPrice.Price,
                    TriggerQuantity = objItemPrice.TriggerQuantity
                };

                db.MstItemPrices.InsertOnSubmit(addItemPrice);
                db.SubmitChanges();

                String newObject = Modules.SysAuditTrailModule.GetObjectString(addItemPrice);

                Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                {
                    UserId = currentUserLogin.FirstOrDefault().Id,
                    AuditDate = DateTime.Now,
                    TableInformation = "MstItemPrice",
                    RecordInformation = "",
                    FormInformation = newObject,
                    ActionInformation = "AddItemPrice"
                };
                Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                return new String[] { "", "" };
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // =================
        // Update Item Price
        // =================
        public String[] UpdateItemPrice(Entities.MstItemPriceEntity objItemPrice)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var itemPrice = from d in db.MstItemPrices
                                where d.Id == objItemPrice.Id
                                select d;

                if (itemPrice.Any())
                {
                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(itemPrice.FirstOrDefault());

                    var updateItemPrice = itemPrice.FirstOrDefault();
                    updateItemPrice.PriceDescription = objItemPrice.PriceDescription;
                    updateItemPrice.Price = objItemPrice.Price;
                    updateItemPrice.TriggerQuantity = objItemPrice.TriggerQuantity;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(itemPrice.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "MstItemPrice",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "UpdateItemPrice"
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

        // =================
        // Delete Item Price
        // =================
        public String[] DeleteItemPrice(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var itemPrice = from d in db.MstItemPrices
                                where d.Id == id
                                select d;

                if (itemPrice.Any())
                {
                    var deleteItemPrice = itemPrice.FirstOrDefault();
                    db.MstItemPrices.DeleteOnSubmit(deleteItemPrice);

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(itemPrice.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "MstItemPrice",
                        RecordInformation = oldObject,
                        FormInformation = "",
                        ActionInformation = "DeleteItemPrice"
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
