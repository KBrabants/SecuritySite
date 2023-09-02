using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SecuritySite.Pages.Info
{
    public class IndexModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string subject { get; set; } = "";
        public IActionResult OnGet()
        {
            if (subject != null)
            {
                return Page();

            }
            else {
                return RedirectToPage("/Info/General");
            }
        }
    }
}
