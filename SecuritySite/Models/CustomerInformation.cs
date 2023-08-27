using Microsoft.AspNetCore.Identity;

namespace SecuritySite.Models
{
    public class CustomerInformation
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string emergencyContact { get; set; }
        public string emergencyContactPhoneNumber { get; set; }

        [ProtectedPersonalData]
        public string address { get; set; }
        public string city { get; set; }
        public string county { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
    }
}
