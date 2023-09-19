using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NpgsqlTypes;
using SecuritySite.Models;
using SecuritySite.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SecuritySite.Pages.New
{
    [Authorize]
    public class LocationModel : PageModel
    {
        AccountUpdateService _updates { get; }
        AccountQueryService _query { get; }
        public LocationModel(AccountUpdateService updates, AccountQueryService query)
        {
            _updates = updates;
            _query = query;
            Features = _query.GetFeatures(true).ToList();
        }

        [BindProperty]
        public InputModel LocationInfo { get; set; } = new InputModel();

        [BindProperty]
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Base Plan Rquired")]
        public string BasePlan { get; set; }
        public List<AccountFeature> Features { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            MonitoredAccount newLocation = new()
            {
                locationName = LocationInfo.locationName,
                durressCode = LocationInfo.durressCode,
                emergencyContact = LocationInfo.emergencyContact,
                emergencyContactPhoneNumber = LocationInfo.emergencyContactPhoneNumber,
                additionalInfo = LocationInfo.additionalInfo,
                address = LocationInfo.address,
                city = LocationInfo.city,
                county = LocationInfo.county,
                state = LocationInfo.state,
                zipcode = LocationInfo.zipcode,
                alarmPassword = LocationInfo.alarmPassword,installerCode = LocationInfo.installerCode,
            };
            newLocation.AccountOwner = _query.GetAccountGuid(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            newLocation.Features = BasePlan;
            newLocation.commercial = _query.GetFeature(BasePlan).Commercial;
            _updates.new_Location(newLocation).Wait();
            return RedirectToPage("Features", new { accountId = newLocation.MonitoredAccountId });
        }
    }
    public class InputModel
    {
        [Required]
        [StringLength(100,MinimumLength = 1, ErrorMessage = "Location Name Needed")]
        [Display(Name = "Location Name")]
        public string locationName { get; set; } = "";
        [Required]
        [StringLength(100, ErrorMessage = "Durress Code Needed")]
        [Display(Name = "Durress Code")]
        public string durressCode { get; set; } = "";
        [Required]
        [StringLength(100, ErrorMessage = "Emergency Contact Needed")]
        [Display(Name = "Emergency Contact")]
        public string emergencyContact { get; set; } = "";

        [Required]
        [StringLength(100, ErrorMessage = "Emergency Contact Needed")]
        [Display(Name = "Emergency Contact Name")]
        public string emergencyContactPhoneNumber { get; set; } = "";

        public string additionalInfo { get; set; } = "";
        [Required]
        [StringLength(100, ErrorMessage = "Address Needed")]
        [Display(Name = "Address")]
        public string address { get; set; } = "";
        [Required]
        [StringLength(100, ErrorMessage = "City Needed")]
        [Display(Name = "City")]
        public string city { get; set; } = "";
        [Required]
        [StringLength(100, ErrorMessage = "County Needed")]
        [Display(Name = "County")]
        public string county { get; set; } = "";
        [Required]
        [StringLength(100, ErrorMessage = "State Needed")]
        [Display(Name = "State")]
        public string state { get; set; } = "";
        [Required]
        [StringLength(100, ErrorMessage = "Zipcode Needed")]
        [Display(Name = "Zip Code")]
        public string zipcode { get; set; } = "";

        [Required]
        [StringLength(100, ErrorMessage = "False Alarm Password Needed")]
        [Display(Name = "Alarm Password")]
        public string alarmPassword { get; set; } = "";

        [Display(Name = "Installer Code")]
        public string installerCode { get; set; } = "";

    }
}
