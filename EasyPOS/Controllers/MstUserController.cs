using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Controllers
{
    class MstUserController
    {
        // ============
        // Data Context
        // ============
        public Data.easyposdbDataContext db = new Data.easyposdbDataContext(Modules.SysConnectionStringModule.GetConnectionString());

        // ==========
        // List Users
        // ==========
        public List<Entities.MstUserEntity> ListUser(String filter)
        {
            var users = from d in db.MstUsers
                        where d.UserName.Contains(filter)
                        || d.FullName.Contains(filter)
                        select new Entities.MstUserEntity
                        {
                            Id = d.Id,
                            UserName = d.UserName,
                            Password = d.Password,
                            FullName = d.FullName,
                            UserCardNumber = d.UserCardNumber,
                            EntryUserId = d.EntryUserId,
                            EntryUserName = d.UserName,
                            EntryDateTime = d.EntryDateTime.ToShortDateString(),
                            UpdateUserId = d.UpdateUserId,
                            UpdatedUserName = d.UserName,
                            UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                            IsLocked = d.IsLocked
                        };

            return users.OrderBy(d => d.Id).ToList();
        }

        // ===========
        // Detail User
        // ===========
        public Entities.MstUserEntity DetailUser(Int32 id)
        {
            var users = from d in db.MstUsers
                        where d.Id == id
                        select new Entities.MstUserEntity
                        {
                            Id = d.Id,
                            UserName = d.UserName,
                            Password = d.Password,
                            FullName = d.FullName,
                            UserCardNumber = d.UserCardNumber,
                            EntryUserId =d.EntryUserId,
                            EntryUserName = GetUser(d.EntryUserId),
                            EntryUserUserName = d.UserName,
                            EntryDateTime = d.EntryDateTime.ToShortDateString(),
                            EntryTime = d.EntryDateTime.ToShortTimeString(),
                            UpdateUserId = d.UpdateUserId,
                            UpdatedUserName = GetUser(d.UpdateUserId),
                            UpdatedUserUserName = d.UserName,
                            UpdateDateTime = d.UpdateDateTime.ToShortDateString(),
                            UpdateTime = d.UpdateDateTime.ToShortTimeString(),
                            IsLocked = d.IsLocked
                        };

            return users.FirstOrDefault();
        }

        public String GetUser(Int32 UserId)
        {
            var userName = "";
            var currentUser = from d in db.MstUsers
                              where d.Id == UserId
                              select d;
            if (currentUser.Any())
            {
                userName = currentUser.FirstOrDefault().UserName;
            }
            return userName;
        }

        // ========
        // Add User
        // ========
        public String[] AddUser()
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                Data.MstUser newUser = new Data.MstUser()
                {
                    UserName = "NA",
                    Password = "NA",
                    FullName = "NA",
                    UserCardNumber = "NA",
                    EntryUserId = currentUserLogin.FirstOrDefault().Id,
                    EntryDateTime = DateTime.Now,
                    UpdateUserId = currentUserLogin.FirstOrDefault().Id,
                    UpdateDateTime = DateTime.Now,
                    IsLocked = false
                };

                db.MstUsers.InsertOnSubmit(newUser);
                db.SubmitChanges();

                String newObject = Modules.SysAuditTrailModule.GetObjectString(newUser);

                Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                {
                    UserId = currentUserLogin.FirstOrDefault().Id,
                    AuditDate = DateTime.Now,
                    TableInformation = "MstUser",
                    RecordInformation = "",
                    FormInformation = newObject,
                    ActionInformation = "AddUser"
                };
                Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                return new String[] { "", newUser.Id.ToString() };
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // =========
        // Lock User
        // =========
        public String[] LockUser(Int32 id, Entities.MstUserEntity objUser)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var existingUser = from d in db.MstUsers
                                   where d.UserName == objUser.UserName
                                   && d.IsLocked == true
                                   select d;

                if (existingUser.Any())
                {
                    return new String[] { "Username is already taken.", "0" };
                }

                var user = from d in db.MstUsers
                           where d.Id == id
                           select d;

                if (user.Any())
                {
                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(user.FirstOrDefault());

                    var lockUser = user.FirstOrDefault();
                    lockUser.UserName = objUser.UserName;
                    lockUser.Password = objUser.Password;
                    lockUser.FullName = objUser.FullName;
                    lockUser.UserCardNumber = objUser.UserCardNumber;
                    lockUser.UpdateUserId = currentUserLogin.FirstOrDefault().Id;
                    lockUser.UpdateDateTime = DateTime.Now;
                    lockUser.IsLocked = true;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(user.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "MstUser",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "LockUser"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "" };
                }
                else
                {
                    return new String[] { "User not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ===========
        // Unlock User
        // ===========
        public String[] UnlockUser(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var user = from d in db.MstUsers
                           where d.Id == id
                           select d;

                if (user.Any())
                {
                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(user.FirstOrDefault());

                    var unlockUser = user.FirstOrDefault();
                    unlockUser.IsLocked = false;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(user.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "MstUser",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "UnlockUser"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "" };
                }
                else
                {
                    return new String[] { "User not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }

        // ===========
        // Delete User
        // ===========
        public String[] DeleteUser(Int32 id)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var user = from d in db.MstUsers
                           where d.Id == id
                           select d;

                if (user.Any())
                {
                    var deleteUser = user.FirstOrDefault();
                    db.MstUsers.DeleteOnSubmit(deleteUser);

                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(user.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "MstUser",
                        RecordInformation = oldObject,
                        FormInformation = "",
                        ActionInformation = "DeleteUser"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    db.SubmitChanges();

                    return new String[] { "", "" };
                }
                else
                {
                    return new String[] { "User not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }
        // =========
        // Save User
        // =========
        public String[] SaveUser(Int32 id, Entities.MstUserEntity objUser)
        {
            try
            {
                var currentUserLogin = from d in db.MstUsers where d.Id == Convert.ToInt32(Modules.SysCurrentModule.GetCurrentSettings().CurrentUserId) select d;
                if (currentUserLogin.Any() == false)
                {
                    return new String[] { "Current login user not found.", "0" };
                }

                var existingUser = from d in db.MstUsers
                                   where d.UserName == objUser.UserName
                                   && d.IsLocked == true
                                   select d;

                if (existingUser.Any())
                {
                    return new String[] { "Username is already taken.", "0" };
                }

                var user = from d in db.MstUsers
                           where d.Id == id
                           select d;

                if (user.Any())
                {
                    String oldObject = Modules.SysAuditTrailModule.GetObjectString(user.FirstOrDefault());

                    var saveUser = user.FirstOrDefault();
                    saveUser.UserName = objUser.UserName;
                    saveUser.Password = objUser.Password;
                    saveUser.FullName = objUser.FullName;
                    saveUser.UserCardNumber = objUser.UserCardNumber;
                    saveUser.UpdateUserId = currentUserLogin.FirstOrDefault().Id;
                    saveUser.UpdateDateTime = DateTime.Now;
                    db.SubmitChanges();

                    String newObject = Modules.SysAuditTrailModule.GetObjectString(user.FirstOrDefault());

                    Entities.SysAuditTrailEntity newAuditTrail = new Entities.SysAuditTrailEntity()
                    {
                        UserId = currentUserLogin.FirstOrDefault().Id,
                        AuditDate = DateTime.Now,
                        TableInformation = "MstUser",
                        RecordInformation = oldObject,
                        FormInformation = newObject,
                        ActionInformation = "SaveUser"
                    };
                    Modules.SysAuditTrailModule.InsertAuditTrail(newAuditTrail);

                    return new String[] { "", "" };
                }
                else
                {
                    return new String[] { "User not found.", "0" };
                }
            }
            catch (Exception e)
            {
                return new String[] { e.Message, "0" };
            }
        }
    }
}
