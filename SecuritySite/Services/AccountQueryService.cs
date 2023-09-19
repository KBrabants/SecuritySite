using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging.Abstractions;
using SecuritySite.Data;
using SecuritySite.Models;
using System.Collections.Immutable;
using System.Security.Claims;
using System.Security.Principal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace SecuritySite.Services
{
    public class AccountQueryService
    {
        private AppDbContext _context { get; }
        private UserManager<ApplicationUser> _userManager { get; }
        public AccountQueryService(AppDbContext context, UserManager<ApplicationUser> userMg) 
        { 
             _context = context;
            _userManager = userMg;
        }

        public IEnumerable<MonitoredAccount> GetMonitoredAccounts(string OwnerGUID)
        {
            var list = _context.MonitoredAccounts
                 .Where(a => a.AccountOwner == OwnerGUID);

           return list.ToList();
        }
        public MonitoredAccount GetMonitoredAccount(int accountId)
        {
            var account = _context.MonitoredAccounts.Where(a => a.MonitoredAccountId == accountId).FirstOrDefault();
                
            if(account == null)
            {
                return new MonitoredAccount();
            }
            else
            {
                return account;
            }

        }

        public IEnumerable<CertificateRequest> GetCertificateRequests(string OwnerGUID)
        {
            return _context.CertificateRequests
                .Where(c =>  c.AccountOwner == OwnerGUID)
                .ToList();
        }
        public AccountInfo GetAccountInfo(string Id)
        {
            ApplicationUser user = _userManager.FindByIdAsync(Id).Result;

            int accounts = _context.MonitoredAccounts.Where(a => a.AccountOwner == user.AccountGUID).Count();

            AccountInfo accountInfo = new AccountInfo()
            {
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Name = user.UserName.Replace("-", " "),
                City = user.City,
                County = user.County,
                ZipCode = user.ZipCode,
                State = user.State,
                Accounts = accounts,
            };

            return accountInfo;

        }


        public string GetAccountGuid(string Id)
        {
            ApplicationUser user = _userManager.FindByIdAsync(Id).Result;

            return user.AccountGUID;

        }
        public IEnumerable<string> GetAccountFeatures(MonitoredAccount monitoredAccount)
        {
            var con = _context.MonitoredAccounts
                .Where(a => a == monitoredAccount).FirstOrDefault();

             var features = con.Features.Split(",");

            return features;

        }
        public void GetAccountFeatures(MonitoredAccount monitoredAccount, out IEnumerable<AccountFeature> Features)
        {
            List<AccountFeature> features = new List<AccountFeature>();

            foreach(string feat in monitoredAccount.Features.Split(","))
            {
              features.Add(_context.AccountFeatures.Where(f => f.Code == feat).FirstOrDefault());
            }

            Features = features;
        }
        public IEnumerable<AccountFeature> GetFeatures()
        {
            return _context.AccountFeatures.AsEnumerable();
        }
        public AccountFeature GetFeature(string code)
        {
            return _context.AccountFeatures
                .Where(f => f.Code == code)
                .First();
        }
        public IEnumerable<AccountFeature> GetFeatures(bool basePlan)
        {
            return _context.AccountFeatures
                .Where(f => f.BasePlan == basePlan)
                .OrderBy(f => f.Id)
                .AsEnumerable();
        }
        public IEnumerable<AccountFeature> GetFeatures(bool isCommercial, bool basePlan)
        {
            return _context.AccountFeatures
                .Where(f => f.Commercial == isCommercial)
                .Where(f => f.BasePlan == basePlan)
                .OrderBy(f => f.Price)
                .AsEnumerable();
        }
        public List<AccountFeature> GetFeatures(string accountFeatures)
        {
            List<AccountFeature> features = new List<AccountFeature>();
            IEnumerable<string> codes = accountFeatures.Split(',');
            foreach (string s in codes)
            {
                features.Add( _context.AccountFeatures
                    .Where(f => f.Code == s)
                    .First());
            }
            return features;
        }
        public IEnumerable<AccountFeature> GetFeatures(IEnumerable<string> codes)
        {
            List<AccountFeature> features = new();
            foreach (string s in codes)
            {
                if(s != null && s.Replace(" ","") != "")
                    features.Add(_context.AccountFeatures.Where(f => f.Code == s).First());
            }
            return features;
        }

        public IEnumerable<AccountFeature> GetPlans(bool basePlan)
        {
            return _context.AccountFeatures.Where(f => f.BasePlan == basePlan).AsEnumerable();
        }

        /// <summary>
        /// Returns true when the account owner is authenticated and owns the current account ID
        /// </summary>
        /// <param name="User"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool VerifyOwner(ClaimsPrincipal User, MonitoredAccount account)
        {
            if (User != null && User.Identity.IsAuthenticated)
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ApplicationUser user = _userManager.FindByIdAsync(userId).Result;
                if(user != null && account != null && user.AccountGUID == account.AccountOwner)
                {
                    return true;
                }
            }
            return false;
        }
        public float GetMonitoredAccountPrice(MonitoredAccount account)
        {
            float price = 0;
            foreach (string feature in account.Features.Split(","))
            {
                price += GetFeature(feature).Price;
            }
            return price;
        }
        public AccountFeature GetBasePlan(MonitoredAccount account)
        {
            foreach (var feat in GetFeatures(account.Features))
            {
                if(feat.BasePlan)
                    return feat;
            }

            return null;
        }
        public IEnumerable<AccountFeature> FilterExisting(string accountFeatures, IEnumerable<AccountFeature> features)
        {
            List<AccountFeature> filtered = new();
            foreach (var feature in features)
            {
                if (!accountFeatures.Contains(feature.Code))
                {
                   filtered.Add(feature);
                }
            }
            return filtered;
        }
    }
}
