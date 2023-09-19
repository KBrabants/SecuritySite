using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SecuritySite.Pages
{
    [Authorize]
    public class TermsOfServiceModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
