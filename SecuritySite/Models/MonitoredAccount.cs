using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509;
using System.ComponentModel.DataAnnotations;

namespace SecuritySite.Models
{
    public class MonitoredAccount
    {
        public MonitoredAccount()
        {
            AccountOwner = Guid.NewGuid().ToString();
            created = DateTime.UtcNow;
            lastUpdated = created;
            accepted = false;
        }
        public int MonitoredAccountId { get; set; }
        public string AccountOwner { get; set; } = "";
        [Required]
        [Display(Description = "Location Name:")]
        [StringLength(100, ErrorMessage = "Location Name Needed")]
        [MaxLength(40)]
        public string locationName { get; set; } = "";
        [Required]
        [StringLength(100, ErrorMessage = "Durress Code Needed")]
        [Display(Description = "Durress Code:")]
        public string durressCode { get; set; } = "";
        [Required]
        [StringLength(100, ErrorMessage = "Emergency Contact Needed")]
        [Display(Description = "Emergency Contact:")]
        public string emergencyContact { get; set; } = "";
        [Required]
        [StringLength(100, ErrorMessage = "Emergency Contact Needed")]
        [Display(Description = "Emergency Contact Name:")]
        public string emergencyContactPhoneNumber { get; set; } = "";

        public string additionalInfo { get; set; } = "";
        [ProtectedPersonalData]
        [Required]
        [StringLength(100, ErrorMessage = "Address Needed")]
        [Display(Description = "Address:")]
        public string address { get; set; } = "";
        [Required]
        [StringLength(100, ErrorMessage = "City Needed")]
        [Display(Description = "City:")]
        public string city { get; set; } = "";
        [Required]
        [StringLength(100, ErrorMessage = "County Needed")]
        [Display(Description = "County:")]
        public string county { get; set; } = "";
        [Required]
        [StringLength(100, ErrorMessage = "State Needed")]
        [Display(Description = "State:")]
        public string state { get; set; } = "";
        [Required]
        [StringLength(100, ErrorMessage = "Zipcode Needed")]
        [Display(Description = "Zip Code:")]
        public string zipcode { get; set; } = "";

        [ProtectedPersonalData]
        [Required]
        [StringLength(100, ErrorMessage = "False Alarm Password Needed")]
        [Display(Description = "Alarm Password:")]
        public string alarmPassword { get; set; } = "";

        [ProtectedPersonalData]
        [Display(Description = "Installer Code:")]
        public string installerCode { get; set; } = "";
        public bool completed { get; set; }
        public bool deleted { get; set; }
        private bool accepted { get; set; }

        [Display(Description = "Is Commercial:")]
        public bool commercial { get; set; }
        public float MonthlyCost { get; set; }
        public DateTime created { get; set; }
        public DateTime lastUpdated { get; set; }
        public DateTime billed { get; set; }
        public string? Features { get; set; } = "";

        public bool IsValid()
        {
            if(locationName.Replace(" ", "") == "" || locationName == null)
            {
                return false;
            }
            if (emergencyContact.Replace(" ", "") == "" || emergencyContact == null)
            {
                return false;
            }
            if (emergencyContactPhoneNumber.Replace(" ", "") == "" || emergencyContactPhoneNumber == null)
            {
                return false;
            }
            if (address.Replace(" ", "") == "" || address == null)
            {
                return false;
            }
            if (city.Replace(" ", "") == "" || city == null)
            {
                return false;
            }
            if (county.Replace(" ", "") == "" || county == null)
            {
                return false;
            }
            if (state.Replace(" ", "") == "" || state == null)
            {
                return false;
            }
            if (zipcode.Replace(" ", "") == "" || zipcode == null)
            {
                return false;
            }
            if (alarmPassword.Replace(" ", "") == "" || alarmPassword == null)
            {
                return false;
            }
            return true;
        }

    }
}
