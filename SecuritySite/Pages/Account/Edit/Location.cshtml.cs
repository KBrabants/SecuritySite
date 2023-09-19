using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using SecuritySite.Models;
using SecuritySite.Services;

namespace SecuritySite.Pages.Account.Edit
{
    [Authorize]
    public class LocationModel : PageModel
    {
        public AccountQueryService _query { get; }
        public LocationModel(AccountQueryService query) 
        { 
         _query = query;
        }
        [BindProperty(SupportsGet =true)]
        public int id { get; set; }
        public MonitoredAccount LocationInfo { get; set; }
        public IActionResult OnGet()
        {
            try
            {
                if (_query.VerifyOwner(User, _query.GetMonitoredAccount(id)))
                {
                    LocationInfo = _query.GetMonitoredAccount(id);
                }
            }
            catch
            {
                return RedirectToPage("error");
            }

            if (LocationInfo == null)
            {
                return RedirectToPage("error");
            }
            else if (!LocationInfo.completed)
            {
                return RedirectToPage("/Account/new/Features", new { accountId = LocationInfo.MonitoredAccountId });
            }
            return Page();
        }
    }
}
