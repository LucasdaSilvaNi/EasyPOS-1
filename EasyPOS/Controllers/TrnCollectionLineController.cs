using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Controllers
{
    class TrnCollectionLineController
    {
        // ============
        // Data Context
        // ============
        public Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

        // ====================
        // List Collection Line
        // ====================
        public List<Entities.TrnCollectionLineEntity> ListCollectionLine(Int32 collectionId)
        {
            var collectionLine = from d in db.TrnCollectionLines
                                 where d.CollectionId == collectionId
                                 select new Entities.TrnCollectionLineEntity
                                 {
                                     Id = d.Id,
                                     CollectionId = d.CollectionId,
                                     PayTypeId = d.PayTypeId,
                                     PayType = d.MstPayType.PayType,
                                     Amount = d.Amount,
                                     CheckNumber = d.CheckNumber,
                                     CheckDate = Convert.ToDateTime(d.CheckDate).ToShortDateString(),
                                     CheckBank = d.CheckBank,
                                     CreditCardVerificationCode = d.CreditCardVerificationCode,
                                     CreditCardNumber = d.CreditCardNumber,
                                     CreditCardType = d.CreditCardType,
                                     CreditCardBank = d.CreditCardBank,
                                     GiftCertificateNumber = d.GiftCertificateNumber,
                                     OtherInformation = d.OtherInformation,
                                     SalesReturnSalesId = d.SalesReturnSalesId,
                                     StockInId = d.StockInId,
                                     AccountId = d.AccountId,
                                     CreditCardReferenceNumber = d.CreditCardReferenceNumber,
                                     CreditCardHolderName = d.CreditCardHolderName,
                                     CreditCardExpiry = d.CreditCardExpiry
                                 };

            return collectionLine.OrderByDescending(d => d.Id).ToList();
        }

        // =======================
        // Dropdown List Pay Types 
        // =======================
        public List<Entities.MstPayTypeEntity> DropdownListPayType()
        {
            var payTypes = from d in db.MstPayTypes
                           select new Entities.MstPayTypeEntity
                           {
                               Id = d.Id,
                               PayTypeCode = d.PayTypeCode,
                               PayType = d.PayType,
                               SortNumber = d.SortNumber
                           };

            return payTypes.OrderBy(d => d.SortNumber).ToList();
        }

        // ===================
        // Add Collection Line
        // ===================
        public String[] AddCollectionLine(Entities.TrnCollectionLineEntity objCollectionLine)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var collection = from d in db.TrnCollections
                                 where d.Id == objCollectionLine.CollectionId
                                 select d;

                if (collection.Any() == false)
                {
                    return new String[] { "Collection transaction not found.", "0" };
                }

                var payType = from d in db.MstPayTypes
                              where d.Id == objCollectionLine.PayTypeId
                              select d;

                if (payType.Any() == false)
                {
                    return new String[] { "Pay type not found.", "0" };
                }

                var account = from d in db.MstAccounts
                              select d;

                if (account.Any() == false)
                {
                    return new String[] { "Account not found.", "0" };
                }

                Data.TrnCollectionLine newCollectionLine = new Data.TrnCollectionLine()
                {
                    CollectionId = objCollectionLine.CollectionId,
                    Amount = objCollectionLine.Amount,
                    PayTypeId = objCollectionLine.PayTypeId,
                    CheckNumber = objCollectionLine.CheckNumber,
                    CheckDate = Convert.ToDateTime(objCollectionLine.CheckDate),
                    CheckBank = objCollectionLine.CheckBank,
                    CreditCardVerificationCode = objCollectionLine.CreditCardVerificationCode,
                    CreditCardNumber = objCollectionLine.CreditCardNumber,
                    CreditCardType = objCollectionLine.CreditCardType,
                    CreditCardBank = objCollectionLine.CreditCardBank,
                    CreditCardExpiry = objCollectionLine.CreditCardExpiry,
                    CreditCardHolderName = objCollectionLine.CreditCardHolderName,
                    CreditCardReferenceNumber = objCollectionLine.CreditCardReferenceNumber,
                    GiftCertificateNumber = objCollectionLine.GiftCertificateNumber,
                    OtherInformation = objCollectionLine.OtherInformation,
                    StockInId = null,
                    AccountId = account.FirstOrDefault().Id,
                };

                db.TrnCollectionLines.InsertOnSubmit(newCollectionLine);
                db.SubmitChanges();

                String newObject = Modules.SysAuditTrailModule.GetObjectString(newCollectionLine);

                Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                {
                    UserId = currentUserLogin.FirstOrDefault().Id,
                    AuditDate = DateTime.Now,
                    TableInformation = "TrnCollectionLine",
                    RecordInformation = "",
                    FormInformation = newObject,
                    ActionInformation = "AddCollectionLine"
                };
                Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                return new String[] { "", "1" };
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ==========================
        // Update Purchase Order Line
        // ==========================
        public String[] UpdateCollectionLine(Int32 id, Entities.TrnCollectionLineEntity objCollectionLine)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var collectionLine = from d in db.TrnCollectionLines
                                     where d.Id == id
                                     select d;

                if (collectionLine.Any())
                {
                    var collectionLines = from d in db.TrnCollectionLines
                                          where d.Id == objCollectionLine.CollectionId
                                          select d;

                    if (collectionLine.Any() == false)
                    {
                        return new String[] { "Collection transaction not found.", "0" };
                    }

                    var payType = from d in db.MstPayTypes
                                  where d.Id == objCollectionLine.PayTypeId
                                  select d;

                    if (payType.Any() == false)
                    {
                        return new String[] { "Pay type not found.", "0" };
                    }

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(collectionLine.FirstOrDefault());

                    var updateCollectionLine = collectionLine.FirstOrDefault();
                    updateCollectionLine.Amount = objCollectionLine.Amount;
                    updateCollectionLine.PayTypeId = objCollectionLine.PayTypeId;
                    updateCollectionLine.CheckNumber = objCollectionLine.CheckNumber;
                    updateCollectionLine.CheckDate = Convert.ToDateTime(objCollectionLine.CheckDate);
                    updateCollectionLine.CheckBank = objCollectionLine.CheckBank;
                    updateCollectionLine.CreditCardVerificationCode = objCollectionLine.CreditCardVerificationCode;
                    updateCollectionLine.CreditCardNumber = objCollectionLine.CreditCardNumber;
                    updateCollectionLine.CreditCardType = objCollectionLine.CreditCardType;
                    updateCollectionLine.CreditCardBank = objCollectionLine.CreditCardBank;
                    updateCollectionLine.CreditCardExpiry = objCollectionLine.CreditCardExpiry;
                    updateCollectionLine.CreditCardHolderName = objCollectionLine.CreditCardHolderName;
                    updateCollectionLine.CreditCardReferenceNumber = objCollectionLine.CreditCardReferenceNumber;
                    updateCollectionLine.GiftCertificateNumber = objCollectionLine.GiftCertificateNumber;
                    updateCollectionLine.OtherInformation = objCollectionLine.OtherInformation;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(collectionLine.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "TrnCollectionLine",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "UpdateCollectionLine"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Purchase Order line not found.  " + id, "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ======================
        // Delete Collection Line
        // ======================
        public String[] DeleteCollectionLine(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var collectionLine = from d in db.TrnCollectionLines
                                     where d.Id == id
                                     select d;

                if (collectionLine.Any())
                {
                    var deleteCollectionLine = collectionLine.FirstOrDefault();
                    db.TrnCollectionLines.DeleteOnSubmit(deleteCollectionLine);

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(collectionLine.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "TrnCollectionLine",
                        RecordInformation = oldObject,
                        FormInformation = "",
                        ActionInformation = "DeleteCollectionLine"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    db.SubmitChanges();

                    return new String[] { "", "1" };
                }
                else
                {
                    return new String[] { "Collection line not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }
    }
}
