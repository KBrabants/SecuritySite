using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecuritySite.Models;
using SecuritySite.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SecuritySite.Pages.Account.edit
{
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
            else if (account.completed == true)
            {
                return RedirectToPage("/Account/Index");
            }
            accountFeatures = _query.GetFeatures(account.Features);
            availableFeatures = _query.GetFeatures(account.commercial, false).ToList();

            return Page();
        }
        [BindProperty]
        public string test { get; set; }
        [BindProperty]
        public List<string> selectedFeatures { get; set; }
        public IActionResult OnPost()
        {
            if (_query.VerifyOwner(User, _query.GetMonitoredAccount(accountId)))
            {
                account = _query.GetMonitoredAccount(accountId);
            }
            if (account == null)
            {
                return RedirectToPage("error");
            }
            else if (account.completed == true)
            {
                return RedirectToPage("/Account/Index");
            }

            selectedFeatures.RemoveAll(x => x == null);
            account.MonthlyCost = 0;
            account.MonthlyCost = _query.GetMonitoredAccountPrice(account);
            _update.add_Features(account, selectedFeatures);

            return RedirectToPage("Review", new { accountId = account.MonitoredAccountId });
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
            else if (account.completed == true)
            {
                return RedirectToPage("/Account/Index");
            }

            AccountFeature feature = _query.GetFeature(RemoveCode);
            _update.remove_AccountFeature(account, feature);
            return Page();
            

        }
        [BindProperty]
        public List<string> addFeatures { get; set; }
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
            else if (account.completed == true)
            {
                return RedirectToPage("/Account/Index");
            }
            List<AccountFeature> features = _query.GetFeatures(addFeatures).ToList();

            foreach (AccountFeature feature in features)
            {
                if (feature == null) continue;
                if (feature.BasePlan)
                {
                    var oldPlan = _query.GetFeature(RemoveCode);
                    _update.change_BasePlan(account, oldPlan, feature);
                    addFeatures.Remove(feature.Code);
                }
            }
            _update.add_Features(account, addFeatures);
            return Page();
        }
    }
}
