using Microsoft.EntityFrameworkCore;
using SecuritySite.Data;
using SecuritySite.Models;

namespace SecuritySite.Services
{
    public class AccountQueryService
    {
        private AppDbContext _context { get; }
        public AccountQueryService(AppDbContext context) 
        { 
             _context = context;
        }

        public IEnumerable<MonitoredAccount> GetMonitoredAccounts(string OwnerGUID)
        {
           return _context.MonitoredAccounts
                .Where(a => a.AccountOwner == OwnerGUID)
                .Include(a => a)
                .ToList();
        }

        public IEnumerable<CertificateRequest> GetCertificateRequests(string OwnerGUID)
        {
            return _context.CertificateRequests
                .Where(c =>  c.AccountOwner == OwnerGUID)
                .Include(c => c)
                .ToList();
        }
        public IEnumerable<AccountFeature> GetAccountFeatures(MonitoredAccount monitoredAccount)
        {
            var con = _context.MonitoredAccounts
                .Where(a => a == monitoredAccount)
                .Include(a=> a.features) 
                .ToList();

            return (IEnumerable<AccountFeature>)con;


        }
    }
}
