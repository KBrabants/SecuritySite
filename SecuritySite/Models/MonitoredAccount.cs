using Microsoft.AspNetCore.Identity;

namespace SecuritySite.Models
{
    public class MonitoredAccount
    {
        public int MonitoredAccountId { get; set; }
        public string AccountOwner { get; set; } = "";
        public string locationName { get; set; } = "";
        public string durressCode { get; set; } = "";
        public string emergencyContact { get; set; } = "";
        public string emergencyContactPhoneNumber { get; set; } = "";

        [ProtectedPersonalData]
        public string address { get; set; } = "";
        public string city { get; set; } = "";
        public string county { get; set; } = "";
        public string state { get; set; } = "";
        public string zipcode { get; set; } = "";

        [ProtectedPersonalData]
        public string alarmPassword { get; set; } = "";

        [ProtectedPersonalData]
        public string installerCode { get; set; } = "";
        public string additionalInfo { get; set; } = "";
        public bool deleted { get; set; }
        public bool accepted { get; set; }
        public DateTime created { get; set; }
        public DateTime lastUpdated { get; set; }
        public IEnumerable<AccountFeature> features { get; set; } = new List<AccountFeature>();

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

        public static MonitoredAccount BaseDIYResidential { get; }= new MonitoredAccount
        {
            features = new List<AccountFeature>
            {
                new AccountFeature {Price=20, FeatureName="BasePlan"}
            }
        };
        public static MonitoredAccount BaseResidentialPlan { get; } = new MonitoredAccount
        {
            features = new List<AccountFeature>
            {
                new AccountFeature {Price=30, FeatureName="BasePlan"}
            }
        };
        public static MonitoredAccount BaseCameraResidentialPlan { get; } = new MonitoredAccount
        {
            features = new List<AccountFeature>
            {
                new AccountFeature {Price=40, FeatureName="BasePlan"}
            }
        };
        public static MonitoredAccount BaseDIYCommercial { get; } = new MonitoredAccount
        {
            features = new List<AccountFeature>
            {
                new AccountFeature {Price=30, FeatureName="BasePlan"}
            }
        };
        public static MonitoredAccount BaseCommercialPlan { get; } = new MonitoredAccount
        {
            features = new List<AccountFeature>
            {
                new AccountFeature {Price=45, FeatureName="BasePlan"}
            }
        };
        public static MonitoredAccount BaseCameraCommercialPlan { get; } = new MonitoredAccount
        {
            features = new List<AccountFeature>
            {
                new AccountFeature {Price=65, FeatureName="BasePlan"}
            }
        };

    }
}
