using System;
using System.Collections.Generic;
using System.Text;

namespace BWT.Core.Entities
{
    public class Rol:BaseEntity
    {
        public Rol()
        {
            TbAccess = new HashSet<Access>();
        }

        public string NameRol { get; set; }

        public virtual ICollection<Access> TbAccess { get; set; }
    }
}
