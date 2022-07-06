using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BWT.UI.Models
{
    public partial class Partners : BaseEntity
    {
        public int FkUserInfo { get; set; }
        public string DescriptionPartners { get; set; }

        public virtual UserInfo FkUserInfoNavigation { get; set; }
    }
}
