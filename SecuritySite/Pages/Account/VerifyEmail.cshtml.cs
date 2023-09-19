using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecuritySite.Data;
using SecuritySite.Services;

namespace SecuritySite.Pages.Account
{
    public class VerifyEmailModel : PageModel
    {
        public AccountUpdateService ac_updates { get; }
        public EmailingService _email { get; }
        UserManager<ApplicationUser> _manager { get; }
        public VerifyEmailModel(UserManager<ApplicationUser> manager,AccountUpdateService accountUpdateService, EmailingService email)
        {
            ac_updates = accountUpdateService;
            _email = email;
            _manager = manager;
        }
        public void OnGet()
        {
        }
        [BindProperty]
        public string email { get; set; }
        public IActionResult OnPost() 
        {
            var user = _manager.FindByEmailAsync(email).Result;
            if(user.EmailConfirmed)
            {
                return RedirectToPage("Login");
            }
            else
            {
                Task.Run(() =>
                {
                    string token = ac_updates.GenerateEmailConfirmationToken(user).Result;
                    _email.EmailVerification(email, token);
                });

            }

            return Page();
        }
    }
}
