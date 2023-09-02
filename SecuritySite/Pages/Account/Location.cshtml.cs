using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecuritySite.Models;

namespace SecuritySite.Pages.Account
{
    public class LocationModel : PageModel
    {
        public List<AccountFeature> AccountFeatures { get; set; }
        public LocationModel()
        {
            AccountFeatures = new List<AccountFeature>();
            AccountFeatures.Add(new AccountFeature {FeatureName= "Hourly Test Timer", Price = 2,Description ="Your system will be sent a daily test timer. When a test fails, you will be sent an email informing you that it has failed."});
            AccountFeatures.Add(new AccountFeature {FeatureName = "Two Way Communication", Price = 6, Description = "Compatible systems will be able to communicate to central station through the systems control panel." });
            AccountFeatures.Add(new AccountFeature {FeatureName = "Doorbell Camera", Price = 6, Description = "Adding this plan will allow you to be able to add a doorbell to your app and view it remotely at any time." });
            AccountFeatures.Add(new AccountFeature {FeatureName = "Add Video Surveillance", Price = 10, Description = "Your system will be allowed to add 4 video cameras to your app and view it remotely at any time. Doorbells will take one of the 4 slots." });
            AccountFeatures.Add(new AccountFeature {FeatureName = "Stream Video Recorder SVR", Price = 6, Description = "Enables you to add an SVR to your Alarm.com video system (for accounts with Alarm.com only)." });
        }
        public void OnGet()
        {
        }
        [BindProperty]
        public MonitoredAccount LocationInfo { get;set; } = new MonitoredAccount();
        public void OnPostLocationInfo()
        {
            if (LocationInfo.IsValid())
            {
                //update
            }

        }
        public void OnPostRemoveFeature()
        {

        }
        public void OnDelete() { }

    }

}
