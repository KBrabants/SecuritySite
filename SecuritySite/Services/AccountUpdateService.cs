﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SecuritySite.Data;
using SecuritySite.Models;

namespace SecuritySite.Services
{
    public class AccountUpdateService
    {
        private AppDbContext _context { get; }
        //private EmailingService _emails { get; }
        private UserManager<ApplicationUser> _userManager { get; }
        public AccountUpdateService(AppDbContext context,  UserManager<ApplicationUser> userManager)//EmailingService emails,
        {
            _context = context;
       //     _emails = emails;
            _userManager = userManager;
        }
        public void new_CertificateRequest(CertificateRequest certRequest, MonitoredAccount account)
        {
            account.lastUpdated = DateTime.UtcNow;
        //    _emails.EmailCertificateRequest().Start();
            certRequest.Submitted = DateTime.UtcNow;
            _context.CertificateRequests.Add(certRequest);
            _context.SaveChangesAsync().Start();
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

            var result =  _userManager.CreateAsync(newAccount, password);
            result.Wait();

            _context.SaveChanges();


            if(result.Result.Succeeded) {
                //    _emails.EmailVerification();
                //    _emails.EmailVoltic("New Account Signed Up");
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
    }
}