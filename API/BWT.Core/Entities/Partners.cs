using System;
using System.Collections.Generic;

namespace BWT.Core.Entities
{
    public partial class Partners:BaseEntity
    {
        public int FkUserInfo { get; set; }
        public string DescriptionPartners { get; set; }

        public virtual UserInfo FkUserInfoNavigation { get; set; }
    }
}
