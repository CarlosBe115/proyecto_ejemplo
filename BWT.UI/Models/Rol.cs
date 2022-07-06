using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BWT.UI.Models
{
    public class Rol : BaseEntity
    {
        public Rol()
        {
            TbAccess = new HashSet<Access>();
        }

        public string NameRol { get; set; }

        public virtual ICollection<Access> TbAccess { get; set; }
    }
}
