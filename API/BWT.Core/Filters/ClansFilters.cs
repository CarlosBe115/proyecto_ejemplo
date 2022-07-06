using System;
using System.Collections.Generic;

namespace BWT.Core.Filters
{
    public class ClansFilters
    {
        public int? FkGames { get; set; }
        public string NameClan { get; set; }
        public string Abbreviation { get; set; }
        public DateTime? CreationClan { get; set; }
        public string Ranked { get; set; }
        public int? FkUserCreator { get; set; }
    }
}
