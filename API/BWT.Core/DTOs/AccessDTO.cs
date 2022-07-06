using System;

namespace BWT.Core.DTOs
{
    public class AccessDTO
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
