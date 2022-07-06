using System;

namespace BWT.Core.Entities
{
    public partial class UserClan:BaseEntity
    {

        public int FkUser { get; set; }
        public int FkClan { get; set; }
        public int FkUcrol { get; set; }
        public DateTime DateRegister { get; set; }
        public bool IsValid { get; set; }

        public virtual Clans FkClanNavigation { get; set; }
        public virtual UCRol FkUcrolNavigation { get; set; }
        public virtual UserInfo FkUserNavigation { get; set; }
    }
}
