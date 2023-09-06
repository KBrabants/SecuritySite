using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecuritySite.Data;
using SecuritySite.Migrations;
using SecuritySite.Models;
using SecuritySite.Services;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace SecuritySite.Pages.Account
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public AccountQueryService _query { get; set; }
        public IndexModel(AccountQueryService query, UserManager<ApplicationUser> users)
        {
            _query = query;
        }
        public List<MonitoredAccount> MonitoredAccounts { get; set; } = new List<MonitoredAccount>();
        public void OnGet()
        {
            string guid = _query.GetAccountGuid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            MonitoredAccounts = _query.GetMonitoredAccounts(guid).ToList();
        }
        private AccountInfo account { get;set; }
        public PartialViewResult OnGetPageView(int id)
        {
            switch (id)
            {
                case 0: {

                        //List<MonitoredAccount> accounts = _query.GetMonitoredAccounts(User.FindFirstValue(ClaimTypes.NameIdentifier)).ToList();

                        account = _query.GetAccountInfo(User.FindFirstValue(ClaimTypes.NameIdentifier));
                        account.BillDate = DateTime.Now;
                        account.LastBill = 0;
                        return Partial("Pages/Account/Info/_Overview.cshtml", account);
                    }
                case 1:
                    string guid = _query.GetAccountGuid(User.FindFirstValue(ClaimTypes.NameIdentifier));

                    List<MonitoredAccount> locations = _query.GetMonitoredAccounts(guid).ToList();

                    return Partial("Pages/Account/Info/_Locations.cshtml", locations);
                case 2:
                    return Partial("Pages/Account/Info/_Invoicing.cshtml");
                case 3:
                    return Partial("Pages/Account/Info/_Notifications.cshtml");
                case 4:
                    return Partial("Pages/Account/Info/_Requests.cshtml");
                case 5:
                    return Partial("Pages/Account/Info/_Settings.cshtml");
                default:
                    return Partial("Pages/Account/Info/_Overview.cshtml");
            }

        }
    }
}
