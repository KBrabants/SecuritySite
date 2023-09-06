using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using SecuritySite.Data;
using SecuritySite.Models;
using System.Security.Policy;
using System.Security.Principal;

namespace SecuritySite.Services
{
    public class AccountUpdateService
    {
        private AppDbContext _context { get; }
        private UserManager<ApplicationUser> _userManager { get; }
        public AccountUpdateService(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public Task new_CertificateRequest(CertificateRequest certRequest, MonitoredAccount account)
        {
            account.lastUpdated = DateTime.UtcNow;
            certRequest.Submitted = DateTime.UtcNow;
            _context.CertificateRequests.Add(certRequest);
            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }
        public bool new_Account(ApplicationUser newAccount, string password, out IEnumerable<IdentityError>? errors)
        {
            int guidMatching = 0;
            do
            {
                guidMatching = _userManager.Users.Where(u => u.AccountGUID == newAccount.AccountGUID).Count();
                if (guidMatching == 0)
                    break;

                    newAccount.AccountGUID = Guid.NewGuid().ToString();

            } while (guidMatching > 0);

                var result = _userManager.CreateAsync(newAccount, password);

                if (!result.IsCompleted)
                {
                    result.Wait();
                }
                if (result.Result.Succeeded)
                {
                    _context.SaveChanges();
                    errors = null;
                    return true;
                }
                else
                {
                    errors = result.Result.Errors;
                    return false;
                    //error logging
                }
            

        }
        public Task new_Location(MonitoredAccount newAccount)
        {
            Random r = new Random();
            //set account id
            do
            {
                newAccount.MonitoredAccountId = r.Next(0, int.MaxValue);
            } while (_context.MonitoredAccounts.Where(a => a.MonitoredAccountId == newAccount.MonitoredAccountId).Any());


            _context.MonitoredAccounts.Add(newAccount);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
        public async Task<string> GenerateEmailConfirmationToken(ApplicationUser user)
        {
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return token;

        }
        internal void add_Features(MonitoredAccount account, IEnumerable<string> features)
        {
            if (account == null)
            {
                return;
            }
            string commaSeperate = "";
            foreach (string feature in features)
            {
                if (feature == null || feature.Replace(" ", "") == "")
                    continue;

                commaSeperate = commaSeperate + "," + feature;
            }
            account.Features = account.Features + commaSeperate;

            _context.SaveChanges();
        }
        public void save_Changes() { 
            _context.SaveChanges();
        }
        public void change_BasePlan(MonitoredAccount account, AccountFeature oldPlan, AccountFeature newPlan)
        {
            account.Features.Replace(oldPlan.Code, newPlan.Code);
            _context.SaveChanges(true);
        }

        internal void remove_AccountFeature(MonitoredAccount monitoredAccount, AccountFeature feature)
        {
            monitoredAccount.Features.Replace(feature.Code + ",", "");
            _context.SaveChanges(true);
        }
    }
}
