using Microsoft.AspNetCore.Identity;
using SecuritySite.Models;
using System.Diagnostics;

namespace SecuritySite.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string AccountGUID { get; set; } = Guid.NewGuid().ToString();
        public bool IsAdmin { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
    }
}
