using System;
using System.Collections.Generic;

namespace BWT.Core.Entities
{
    public partial class UserInfo:BaseEntity
    {
        public UserInfo()
        {
            TbPartners = new HashSet<Partners>();
            TbSocialNetworks = new HashSet<SocialNetworks>();
            TbUserClan = new HashSet<UserClan>();
        }

        public int FkAccess { get; set; }
        public string FullNames { get; set; }
        public string LastNames { get; set; }
        public string Gender { get; set; }
        public string NameTag { get; set; }
        public DateTime? BirthDay { get; set; }
        public string ImageProfile { get; set; }

        public virtual Access FkAccessNavigation { get; set; }
        public virtual ICollection<Partners> TbPartners { get; set; }
        public virtual ICollection<SocialNetworks> TbSocialNetworks { get; set; }
        public virtual ICollection<UserClan> TbUserClan { get; set; }
    }
}
