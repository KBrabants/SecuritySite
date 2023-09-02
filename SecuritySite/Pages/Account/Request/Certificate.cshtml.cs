using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using SecuritySite.Models;

namespace SecuritySite.Pages.Account.Request
{
    public class CertificateModel : PageModel
    {
        public HttpContext _context { get; }
        public CertificateModel(HttpContext context) {
         _context = context;
        }
        public void OnGet()
        {
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
               // Account = GetUserAccount(Input.Account, ),
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
            //Save info to database :)
            return RedirectToPage("processing");
        }
    }

    public class InputModel
    {
        [Required]
        [Display(Name = "Select Location")]
        public string Account { get; set; } = "";

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
