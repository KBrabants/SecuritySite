using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SecuritySite.Pages.Account.Request
{
    public class processingModel : PageModel
    {
        [Authorize]
        public void OnGet()
        {
        }
    }
}
