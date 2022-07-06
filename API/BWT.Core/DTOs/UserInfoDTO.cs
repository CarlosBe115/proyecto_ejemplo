using System;
using System.Collections.Generic;

namespace BWT.Core.DTOs
{
    public class UserInfoDTO
    {
        public int Id { get; set; }
        public int FkAccess { get; set; }
        public string FullNames { get; set; }
        public string LastNames { get; set; }
        public string Gender { get; set; }
        public string NameTag { get; set; }
        public DateTime? BirthDay { get; set; }
        public string ImageProfile { get; set; }
    }
}
