using System;
using System.Collections.Generic;

namespace BWT.Core.Entities
{
    public partial class Games:BaseEntity
    {
        public Games()
        {
            TbClans = new HashSet<Clans>();
        }

        public string NameGame { get; set; }
        public string DescriptionGame { get; set; }
        public string ImageGame { get; set; }
        public int LimitUserGames { get; set; }

        public virtual ICollection<Clans> TbClans { get; set; }
    }
}
