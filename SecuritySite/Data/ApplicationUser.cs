using Microsoft.AspNetCore.Identity;
using SecuritySite.Models;
using System.Diagnostics;

namespace SecuritySite.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string AccountGUID { get; set; } = Guid.NewGuid().ToString();
        public bool IsAdmin { get; set; }
    }
}
