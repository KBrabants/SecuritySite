using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecuritySite.Models;

namespace SecuritySite.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MonitoredAccount> MonitoredAccounts { get; set; }
        public DbSet<AccountFeature> AccountFeatures { get; set; }
        public DbSet<CertificateRequest> CertificateRequests { get; set; }

    }

}
