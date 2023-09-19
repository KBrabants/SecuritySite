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
        public AccountUpdateService _update { get; set; }
        public IndexModel(AccountQueryService query, UserManager<ApplicationUser> users, AccountUpdateService update)
        {
            _query = query;
            _update = update;
        }
        public List<MonitoredAccount> MonitoredAccounts { get; set; } = new List<MonitoredAccount>();

        [BindProperty]
        public AccountInfo accountInfo { get; set; }
        public string PageView {  get; set; }
        public object pageModel { get;set; }
        public void OnGet()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string guid = _query.GetAccountGuid(id);

            MonitoredAccounts = _query.GetMonitoredAccounts(guid).ToList();
            foreach(var monitoredAccount in MonitoredAccounts)
            {
                _update.update_AccountPrice(monitoredAccount);
            }
            if (!_query.GetAccountInfo(id).Completed())
            {
                PageView = "/Pages/Account/Info/_Overview.cshtml";
                pageModel = _query.GetAccountInfo(id);
            }
            else
            {
                PageView = "/Pages/Account/Info/_Locations.cshtml";
                pageModel = _query.GetMonitoredAccounts(guid).ToList();
            }
        }
        private AccountInfo account { get;set; }
        public PartialViewResult OnGetPageView(int id)
        {


            switch (id)
            {
                case 0: {
                        //List<MonitoredAccount> accounts = _query.GetMonitoredAccounts(User.FindFirstValue(ClaimTypes.NameIdentifier)).ToList();

                        account = _query.GetAccountInfo(User.FindFirstValue(ClaimTypes.NameIdentifier));
                        return Partial("Pages/Account/Info/_Overview.cshtml", account);
                    }
                case 1:
                    string guid = _query.GetAccountGuid(User.FindFirstValue(ClaimTypes.NameIdentifier));

                    List<MonitoredAccount> locations = _query.GetMonitoredAccounts(guid).ToList();


                    PageView = "/Pages/Account/Info/_Locations.cshtml";
                    pageModel = _query.GetMonitoredAccounts(guid).ToList();
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
        public void OnPostAccountInfo()
        {
            var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _update.update_AccountInfo(id, accountInfo);
            PageView = "/Pages/Account/Info/_Overview.cshtml";
            pageModel = _query.GetAccountInfo(id);
        }
    }
}
