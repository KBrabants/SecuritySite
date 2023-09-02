using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace SecuritySite.Pages.Account
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }

        public PartialViewResult OnGetPageView(int id)
        {
            switch (id)
            {
                case 0:
                    return Partial("Pages/Account/Info/_Overview.cshtml");
                case 1:
                    return Partial("Pages/Account/Info/_Locations.cshtml");
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
