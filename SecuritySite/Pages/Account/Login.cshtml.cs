using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SecuritySite.Models;

namespace SecuritySite.Pages.Account
{
    public class LoginModel : PageModel
    {
        public Dictionary<string, int> priceDirectory { get; } = new Dictionary<string, int>();
        public Dictionary<string, string> planDirectory { get; } = new Dictionary<string, string>();
        public int Cost { get; set; }
        public LoginModel()
        {
            priceDirectory["diy-r-s"] = 20;
            priceDirectory["diy-r-g"] = 25;
            priceDirectory["diy-r-cam-s"] = 32;

            priceDirectory["pro-r-s"] = 25;
            priceDirectory["pro-r-g"] = 30;

            priceDirectory["pro-r-cam-s"] = 40;
            priceDirectory["pro-r-cam-g"] = 45;

            priceDirectory["diy-c-s"] = 30;
            priceDirectory["diy-c-g"] = 35;
            priceDirectory["diy-c-cam-s"] = 45;

            priceDirectory["pro-c-s"] = 45;
            priceDirectory["pro-c-g"] = 55;

            priceDirectory["pro-c-cam-s"] = 65;
            priceDirectory["pro-c-cam-g"] = 70;

            //plan directory

            planDirectory["diy-r-s"] = "Residential DIY Silver Monitoring";
            planDirectory["diy-r-g"] = "Residential DIY Gold Monitoring";
            planDirectory["diy-r-cam-s"] = "Residential DIY Monitoring With Cameras";

            planDirectory["pro-r-s"] = "Residential Silver Monitoring";
            planDirectory["pro-r-g"] = "Residential Gold Monitoring";

            planDirectory["pro-r-cam-s"] = "Residential Monitoring With Cameras (Silver)";
            planDirectory["pro-r-cam-g"] = "Residentail Monitoring With Cameras (Gold)";

            planDirectory["diy-c-s"] = "Commercial DIY Silver Monitoring";
            planDirectory["diy-c-g"] = "Commercial DIY Gold Monitoring";
            planDirectory["diy-c-cam-s"] = "Commercial Monitoring With Cameras";

            planDirectory["pro-c-s"] = "Commercial Monitoring (Silver)";
            planDirectory["pro-c-g"] = "Commercial Monitoring (Gold)"; ;

            planDirectory["pro-c-cam-s"] = "Commercial Monitoring With Cameras (Silver)";
            planDirectory["pro-c-cam-g"] = "Commercial Monitoring With Cameras (Gold)";

            Input = new();
        }


        //[BindProperty(SupportsGet = true)]
        //public string MonitoringCode { get; set; }

        public Input Input { get; set; }

        public ActionResult OnGet()
        {
            //if (MonitoringCode == null)
            //    return RedirectToPage("index");

            //if (priceDirectory.TryGetValue(MonitoringCode, out int cost))
            //{
            //    Cost = cost;
               return Page();
            //}

            //return RedirectToPage("index");
        }


    }

    public class Input
    {
        public string password { get; set; } = "";
        public string emailAddress { get; set; } = "";
        public bool rememberSignin { get; set; }
    }
}
