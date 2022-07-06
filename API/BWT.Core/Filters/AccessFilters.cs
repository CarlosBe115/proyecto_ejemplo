using System;
using System.Collections.Generic;
using System.Text;

namespace BWT.Core.Filters
{
    public class AccessFilters
    {
        public int? Id { get; set; }
        public string EmailAddress { get; set; }
        public int? FkRol { get; set; }
        public bool? IsValid { get; set; }
        public DateTime? TimeBan { get; set; }
    }
}
