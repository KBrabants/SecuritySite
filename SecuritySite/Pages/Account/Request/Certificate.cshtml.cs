using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using SecuritySite.Models;
using SecuritySite.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SecuritySite.Pages.Account.Request
{
    [Authorize]
    public class CertificateModel : PageModel
    {
        public AccountQueryService _query { get;}
        public AccountUpdateService _update { get; }
        public EmailingService _email { get; }
        public CertificateModel(AccountQueryService query, AccountUpdateService update, EmailingService email) {
            _query = query;
            _update = update;
            _email = email;
        }

        public List<MonitoredAccount> accounts { get; set; }
        public void OnGet()
        {
            accounts = _query.GetMonitoredAccounts(_query.GetAccountGuid(User.FindFirstValue(ClaimTypes.NameIdentifier))).ToList();
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public IActionResult OnPost() 
        {
            if (!ModelState.IsValid)
            { 
             return Page();
            }
            CertificateRequest request = new CertificateRequest {
                AccountId = Input.AccountId,
                InsuranceAgency = Input.InsuranceAgency,
                AgentName = Input.AgentName,
                PolicyNumber = Input.PolicyNumber,
                AgencyAddress = Input.AgencyAddress,
                AgencySecondAddress = Input.AgencySecondAddress,
                AgencyCity = Input.AgencyCity,
                AgencyState = Input.AgencyState,
                AgencyCountry = Input.AgencyCountry,
                AgencyZipCode = Input.AgencyZipCode,
                AgencyPhone = Input.AgencyPhone,
                MoreInfo = Input.MoreInfo,
                ApplyingForFireCertificate = Input.ApplyingForFireCertificate,
                AgentEmail = Input.AgentEmail,
            };

            _update.new_CertificateRequest(request, _query.GetMonitoredAccount(request.AccountId));
            _email.EmailCertificateRequest(_query.GetAccountInfo(User.FindFirstValue(ClaimTypes.NameIdentifier)).Email);
            return RedirectToPage("processing");
        }
    }

    public class InputModel
    {
        [Required]
        [Display(Name = "Select Location")]
        public int AccountId { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Insurance Agency is Required")]
        [Display(Name = "Insurance Agency Name")]
        public string InsuranceAgency { get; set; } = "";

        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Rep. Name is Required")]
        [Display(Name = "Insurance Representative Name")]
        public string AgentName { get; set; } = "";

        [Required]
        [EmailAddress]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Rep. Email Required")]
        [Display(Name = "Representative Email")]
        public string AgentEmail { get; set; } = "";

        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Policy Number Required")]
        [Display(Name = "Policy Number (Temporary Policy Number Allowed)")]
        public string PolicyNumber { get; set; } = "";

        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Address is Required")]
        [Display(Name = "Insurance Agency Address")]
        public string AgencyAddress { get; set; } = "";

        [Display(Name = "Second Address")]
        public string? AgencySecondAddress { get; set; } = "";

        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Insurance Agency City")]
        [Display(Name = "Agency City")]
        public string AgencyCity { get; set; } = "";

        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Insurance Agency State")]
        [Display(Name = "Agency State")]
        public string AgencyState { get; set; } = "";

        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Insurance Agency Zipcode")]
        [Display(Name = "Agency Zip/Postal Code")]
        public string AgencyZipCode { get; set; } = "";

        [Required]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Insurance Agency Country")]
        [Display(Name = "Agency Country")]
        public string AgencyCountry { get; set; } = "";
        [Required]
        [Phone]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Insurance Agency Phone Number")]
        [Display(Name = "Agency Phone Number")]
        public string AgencyPhone { get; set; } = "";

        [Display(Name = "Include fire certificate")]
        public bool ApplyingForFireCertificate { get; set; }
        [Display(Name = "More Info")]
        public string? MoreInfo { get; set; } = "";
    }
}
