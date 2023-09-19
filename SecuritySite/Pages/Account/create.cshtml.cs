using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecuritySite.Data;
using SecuritySite.Models;
using SecuritySite.Services;

namespace SecuritySite.Pages.Account
{
    public class createModel : PageModel
    {
        public AccountUpdateService ac_updates { get; }
        public EmailingService _email { get; }
        public createModel(AccountUpdateService accountUpdateService, EmailingService email) 
        { 
            ac_updates = accountUpdateService;
            _email = email;
        }


        public void OnGet()
        {
        }

        [BindProperty]
        public IEnumerable<IdentityError>? errors { get; set; }
        [BindProperty]
        public InputModel Input { get; set; } =new InputModel();
        public IActionResult OnPost()
        {
            ApplicationUser newUser = new ApplicationUser()
            {
                UserName = Input.firstName.Replace(" ", "-"),
                Email = Input.emailAddress,
                PhoneNumber = Input.phoneNumber,
            };

            IEnumerable<IdentityError>? error;
            ac_updates.new_Account(newUser, Input.password, out error);

            if(error == null)
            {
                Task.Run(() =>
                {
                    string token = ac_updates.GenerateEmailConfirmationToken(newUser).Result;
                    _email.EmailVerification(Input.emailAddress, token);
                    _email.EmailVoltic("New Account Created", $"{Input.firstName} created their account");
                });

             
                return RedirectToPage("VerifyEmail");
            }
            else
            {
                errors = error; 
                return Page();
            }

        }

    }
    public class InputModel
    {
        public string firstName { get; set; } = "";
        public string password { get; set; } = "";
        public string emailAddress { get; set; } = "";
        public string phoneNumber { get; set; } = "";
    }
}
