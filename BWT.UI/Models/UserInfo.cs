using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BWT.UI.Models
{
    public partial class UserInfo
    {
        public int Id { get; set; }
        public int FkAccess { get; set; }
        public string FullNames { get; set; }
        public string LastNames { get; set; }
        public string Gender { get; set; }
        public string NameTag { get; set; }
        public DateTime BirthDay { get; set; }
        public string ImageProfile { get; set; }
    }

}

