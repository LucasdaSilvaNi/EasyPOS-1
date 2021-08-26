using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyPOS.Entities
{
    public class MstUserEntity
    {
        public Int32 Id { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public String FullName { get; set; }
        public String UserCardNumber { get; set; }
        public Int32 EntryUserId { get; set; }
        public String EntryUserName { get; set; }
        public String EntryUserUserName { get; set; }
        public String EntryDateTime { get; set; }
        public String EntryTime { get; set; }
        public Int32 UpdateUserId { get; set; }
        public String UpdatedUserName { get; set; }
        public String UpdatedUserUserName { get; set; }
        public String UpdateDateTime { get; set; }
        public String UpdateTime { get; set; }
        public Boolean IsLocked { get; set; }
    }
}
