using Microsoft.AspNetCore.Identity;
using System;

namespace Assignment.Data.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsAdmin { get; set; }
    }
}
