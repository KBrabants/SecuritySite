using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SecuritySite.Pages
{
    public class cartModel : PageModel
    {
        public Dictionary<string, int> priceDirectory { get; } = new Dictionary<string, int>();


        [BindProperty(SupportsGet = true)]
        public string monitoringCode { get; set; }

        public Input MonitoringInfo { get; set; }
        public ActionResult OnGet()
        {
            if (priceDirectory[monitoringCode] != null)
            {
                MonitoringInfo.cost = priceDirectory[monitoringCode];
                return Page();
            }

           return RedirectToPage("/");
        }


    }

    public class Input
    {
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public int cost { get; set; }
    }
}
