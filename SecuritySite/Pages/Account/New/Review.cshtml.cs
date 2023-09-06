using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using SecuritySite.Migrations;
using SecuritySite.Models;
using SecuritySite.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SecuritySite.Pages.Account.New
{
    public class ReviewModel : PageModel
    {
        public AccountQueryService _query { get; set; }
        public AccountUpdateService _update { get; set; }
        public EmailingService _emailing { get; set; }
        public ReviewModel(AccountQueryService query, AccountUpdateService update, EmailingService email)
        {
            _query = query;
            _update = update;
            _emailing = email;
        }
        [BindProperty]
        public MonitoredAccount LocationInfo { get; set; }

        [BindProperty(SupportsGet = true)]
        public int accountId { get; set; }
        public List<AccountFeature> accountFeatures { get; set; }
        public IActionResult OnGet()
        {
            MonitoredAccount account = _query.GetMonitoredAccount(accountId);
            if (account == null || !_query.VerifyOwner(User, account))
            {
                return RedirectToPage("error");
            }
            else if (account.completed == true)
            {
                return RedirectToPage("/Account/Index");
            }
            LocationInfo = account;
            accountFeatures = _query.GetFeatures(account.Features);
            return Page();



        }
        public IActionResult OnPost()
        {
            MonitoredAccount account = _query.GetMonitoredAccount(accountId);
            if (account == null || !_query.VerifyOwner(User, account))
            {
                return RedirectToPage("error");
            }
            else if (account.completed == true)
            {
                return RedirectToPage("/Account/Index");
            }

            account.completed = true;
            _emailing.EmailVoltic("New Location Created", $"{LocationInfo.MonitoredAccountId} was created");
            _update.save_Changes();

            return RedirectToPage("/Account/Index");
        }

    }
}
