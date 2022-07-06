using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BWT.UI.Models
{
    public partial class SocialNetworks : BaseEntity
    {
        public int FkUserInfo { get; set; }
        public string Twitch { get; set; }
        public string Youtube { get; set; }
        public string Facebook { get; set; }
        public string Instragram { get; set; }
        public string Tiktok { get; set; }
        public string Twitter { get; set; }

        public virtual UserInfo FkUserInfoNavigation { get; set; }
    }
}
