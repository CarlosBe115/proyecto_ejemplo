using System;
using System.Collections.Generic;

namespace BWT.Core.DTOs
{
    public class ClansDTO
    {
        public int Id { get; set; }
        public int FkGames { get; set; }
        public string NameClan { get; set; }
        public string Abbreviation { get; set; }
        public string DescriptionClan { get; set; }
        public DateTime CreationClan { get; set; }
        public int LimitUser { get; set; }
        public int CurrentUser { get; set; }
        public int FkUserCreator { get; set; }
        public string Ranked { get; set; }
    }
}
