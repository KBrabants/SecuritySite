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
        public MonitoredAccount LocationInfo { get; set; }
        [BindProperty]
        [Required]
        public string BasePlan { get; set; }
        public List<AccountFeature> Features { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            MonitoredAccount newLocation = LocationInfo;
            newLocation.AccountOwner = _query.GetAccountGuid(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            newLocation.Features = BasePlan;
            _updates.new_Location(newLocation).Wait();
            return RedirectToPage("Features", new { accountId = newLocation.MonitoredAccountId });
        }
    }
    public class InputModel
    {
        
    }
}
