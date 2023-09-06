using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecuritySite.Models;
using SecuritySite.Services;

namespace SecuritySite.Pages.Account.New
{
    public class FeaturesModel : PageModel
    {
        public AccountQueryService _query {get;}
        public AccountUpdateService _update { get;}
        public FeaturesModel(AccountQueryService query, AccountUpdateService update) 
        {
            _query = query;
            _update = update;

        }


        [BindProperty(SupportsGet =true)]
        public int accountId {  get; set; }
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
            }else if(account.completed == true)
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

            return RedirectToPage("Review", new { accountId = account.MonitoredAccountId});
        }
    }
}
