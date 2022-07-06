using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BWT.UI.Models
{
    public class Access
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string EmailPassword { get; set; }
        public string AccessKey { get; set; }
        public int FkRol { get; set; }
        public bool IsValid { get; set; }
        public DateTime? TimeBan { get; set; }
        public string TokenValidation { get; set; }
    }
}
