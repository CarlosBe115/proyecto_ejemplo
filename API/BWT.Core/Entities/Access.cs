using System;
using System.Collections.Generic;

namespace BWT.Core.Entities
{
    public partial class Access:BaseEntity
    {
        public Access()
        {
            TbUserInfo = new HashSet<UserInfo>();
        }

        public string EmailAddress { get; set; }
        public string EmailPassword { get; set; }
        public string AccessKey { get; set; }
        public int FkRol { get; set; }
        public bool IsValid { get; set; }
        public DateTime? TimeBan { get; set; }

        public virtual Rol FkRolNavigation { get; set; }
        public virtual ICollection<UserInfo> TbUserInfo { get; set; }
    }
}
