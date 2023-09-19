using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecuritySite.Models;
using SecuritySite.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SecuritySite.Pages.Account.edit
{
        [Authorize]
    public class FeaturesModel : PageModel
    {
        AccountUpdateService _update { get;set; }
        public AccountQueryService _query { get; set; }
        public FeaturesModel(AccountQueryService query, AccountUpdateService update) 
        {
            _update = update;
            _query = query;
        }

        [BindProperty(SupportsGet = true)]
        public int accountId { get; set; }
        public MonitoredAccount account { get; set; }
        public List<AccountFeature> accountFeatures { get; set; }
        public List<AccountFeature> availableFeatures { get; set; }
        public List<AccountFeature> basePlans { get;set; }
        public IActionResult OnGet()
        {
            try
            {
                if (_query.VerifyOwner(User, _query.GetMonitoredAccount(accountId)))
                {
                    account = _query.GetMonitoredAccount(accountId);
                }
            }
            catch
            {
                return RedirectToPage("error");
            }

            if (account == null)
            {
                return RedirectToPage("error");
            }
            else if (account.completed != true)
            {
                return RedirectToPage("/Account/new/Features", new {accountId = this.accountId});
            }
            accountFeatures = _query.GetFeatures(account.Features);
            availableFeatures = _query.FilterExisting( account.Features , _query.GetFeatures(account.commercial, false)).ToList();
            basePlans = _query.FilterExisting(account.Features, _query.GetFeatures(true)).ToList();

            return Page();
        }
        [BindProperty]
        public string test { get; set; }
        [BindProperty]
        public List<string> selectedFeatures { get; set; }
        public void OnPost()
        {

        }

        [BindProperty]
        public string RemoveCode { get; set; }
        public IActionResult OnPostRemoveFeature()
        {
            if (_query.VerifyOwner(User, _query.GetMonitoredAccount(accountId)))
            {
                account = _query.GetMonitoredAccount(accountId);
            }
            if (account == null)
            {
                return RedirectToPage("error");
            }
            else if (account.completed != true)
            {
                return RedirectToPage("/Account/new/Features", new { accountId = this.accountId });
            }

            AccountFeature feature = _query.GetFeature(RemoveCode);
            _update.remove_AccountFeature(account, feature);

            accountFeatures = _query.GetFeatures(account.Features);
            availableFeatures = _query.GetFeatures(account.commercial, false).ToList();
            basePlans = _query.FilterExisting(account.Features, _query.GetFeatures(true)).ToList();
            return Page();
            

        }
        [BindProperty]
        public List<string> addFeatures { get; set; }
        int x = 929;
        public IActionResult OnPostAddFeature()
        {
            if (_query.VerifyOwner(User, _query.GetMonitoredAccount(accountId)))
            {
                account = _query.GetMonitoredAccount(accountId);
            }
            if (account == null)
            {
                return RedirectToPage("error");
            }
            else if (account.completed != true)
            {
                return RedirectToPage("/Account/new/Features", new { accountId = this.accountId });
            }
            List<AccountFeature> features = _query.GetFeatures(addFeatures).ToList();

            foreach (AccountFeature feature in features)
            {
                if (feature == null) continue;
                if (feature.BasePlan)
                {
                    var oldPlan = _query.GetBasePlan(account);
                    _update.change_BasePlan(account, oldPlan, feature);

                    accountFeatures = _query.GetFeatures(account.Features);
                    availableFeatures = _query.FilterExisting(account.Features, _query.GetFeatures(account.commercial, false)).ToList();
                    basePlans = _query.FilterExisting(account.Features, _query.GetFeatures(true)).ToList();


                    return Page();
                }
            }
            _update.add_Features(account, addFeatures);

            accountFeatures = _query.GetFeatures(account.Features);
            availableFeatures = _query.FilterExisting(account.Features, _query.GetFeatures(account.commercial, false)).ToList();
            basePlans = _query.FilterExisting(account.Features, _query.GetFeatures(true)).ToList();
            return Page();
        }
    }
}
