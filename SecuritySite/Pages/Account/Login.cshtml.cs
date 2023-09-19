using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Org.BouncyCastle.Bcpg.OpenPgp;
using SecuritySite.Data;
using SecuritySite.Migrations;
using SecuritySite.Models;
using System.Web;

namespace SecuritySite.Pages.Account
{
    public class LoginModel : PageModel
    {
        public Dictionary<string, int> priceDirectory { get; } = new Dictionary<string, int>();
        public Dictionary<string, string> planDirectory { get; } = new Dictionary<string, string>();
        public UserManager<ApplicationUser> _userManager { get; }
        public SignInManager<ApplicationUser> _signInManager { get; }
        public int Cost { get; set; }
        public LoginModel(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            Input = new();
            _userManager = userManager;
            _signInManager = signInManager;
        }


        //[BindProperty(SupportsGet = true)]
        //public string MonitoringCode { get; set; }

        [BindProperty]
        public Input Input { get; set; }

        public ActionResult OnGet()
        {
               return Page();
        }

        public IActionResult OnGetVerification(string token, string email)
        {
            try
            {
                ApplicationUser user = _userManager.FindByEmailAsync(email).Result!;
                var result = _userManager.ConfirmEmailAsync(user, token);
                if (!result.IsCompleted)
                    result.Wait();

                if (result.Result.Succeeded)
                {
                    _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToPage("/Account/Index");

                }
                else
                {
                    return RedirectToPage("Index");
                }

            }
            catch
            {
                return RedirectToPage("create");
            }
        }

        public IActionResult OnPost()
        {
           var result = _userManager.FindByEmailAsync(Input.emailAddress);

            if(!result.IsCompleted)
                result.Wait();
            if (!result.Result.EmailConfirmed)
                RedirectToPage("VerifyEmail");

            var user = result.Result;
           _signInManager.PasswordSignInAsync(user, Input.password, Input.rememberSignin, user.AccessFailedCount > 20).Wait();

            return RedirectToPage("/Account/Index");

        }
    }

    public class Input
    {
        public string password { get; set; } = "";
        public string emailAddress { get; set; } = "";
        public bool rememberSignin { get; set; }
    }
}
