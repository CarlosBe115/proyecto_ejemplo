using System.Collections.Generic;

namespace BWT.Core.Entities
{
    public class UCRol:BaseEntity
    {
        public UCRol()
        {
            TbUserClan = new HashSet<UserClan>();
        }

        public string NameRol { get; set; }

        public virtual ICollection<UserClan> TbUserClan { get; set; }
    }
}
