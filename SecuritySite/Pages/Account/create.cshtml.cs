using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecuritySite.Models;

namespace SecuritySite.Pages.Account
{
    public class createModel : PageModel
    {
        public void OnGet()
        {
        }

        public InputModel Input { get; set; }
        public void OnPost()
        {

        }

    }
    public class InputModel
    {
        public string firstName { get; set; }
        public string password { get; set; }
        public string emailAddress { get; set; }
        public string phoneNumber { get; set; }
    }
}
