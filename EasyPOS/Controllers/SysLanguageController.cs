using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Controllers
{
    class SysLanguageController
    {
        // ============
        // Data Context
        // ============
        public Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

        // =============
        // List Language
        // =============
        public List<Entities.SysLanguageEntity> ListLanguage(String filter)
        {
            var units = from d in db.SysLabels
                        where d.Label.Contains(filter)
                        select new Entities.SysLanguageEntity
                        {
                            Id = d.Id,
                            Label = d.Label,
                            DisplayedLabel = d.DisplayedLabel
                        };

            return units.OrderByDescending(d => d.Id).ToList();
        }

        // ============
        // Add Language
        // ============
        public String[] AddLanguage(Entities.SysLanguageEntity objLanguage)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                Data.SysLabel addLanguage = new Data.SysLabel()
                {
                    Label = objLanguage.Label,
                    DisplayedLabel = objLanguage.DisplayedLabel
                };

                db.SysLabels.InsertOnSubmit(addLanguage);
                db.SubmitChanges();

                String newObject = Modules.SysAuditTrailModule.GetObjectString(addLanguage);

                Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                {
                    UserId = currentUserLogin.FirstOrDefault().Id,
                    AuditDate = DateTime.Now,
                    TableInformation = "SysLabel",
                    RecordInformation = "",
                    FormInformation = newObject,
                    ActionInformation = "AddLanguage"
                };
                Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                return new String[] { "", "" };
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ===============
        // Update Language
        // ===============
        public String[] UpdateLanguage(Entities.SysLanguageEntity objLanguage)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var language = from d in db.SysLabels
                           where d.Id == objLanguage.Id
                           select d;

                if (language.Any())
                {
                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(language.FirstOrDefault());

                    var updateLanguage = language.FirstOrDefault();
                    updateLanguage.Label = objLanguage.Label;
                    updateLanguage.DisplayedLabel = objLanguage.DisplayedLabel;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(language.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "SysLabel",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "UpdateLanguage"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "" };
                }
                else
                {
                    return new String[] { "Language not found!", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ===========
        // Delete Unit
        // ===========
        public String[] DeleteLanguage(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var language = from d in db.SysLabels
                           where d.Id == id
                           select d;

                if (language.Any())
                {
                    var deleteLanguage = language.FirstOrDefault();
                    db.SysLabels.DeleteOnSubmit(deleteLanguage);

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(language.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "SysLabel",
                        RecordInformation = oldObject,
                        FormInformation = "",
                        ActionInformation = "DeleteLanguage"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    db.SubmitChanges();

                    return new String[] { "", "" };
                }
                else
                {
                    return new String[] { "Language not found!", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

    }
}
