using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BWT.UI.Models
{
    public class UCRol : BaseEntity
    {
        public UCRol()
        {
            TbUserClan = new HashSet<UserClan>();
        }

        public string NameRol { get; set; }

        public virtual ICollection<UserClan> TbUserClan { get; set; }
    }
    //public int Id { get; set; }
    //public string NameRol { get; set; }
}
