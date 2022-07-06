using System;

namespace BWT.Core.DTOs
{
    public class UserClanDTO
    {
        public int Id { get; set; }
        public int FkUser { get; set; }
        public int FkClan { get; set; }
        public int FkUcrol { get; set; }
        public DateTime DateRegister { get; set; }
        public bool IsValid { get; set; }
    }
}
