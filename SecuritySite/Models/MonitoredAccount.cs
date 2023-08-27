using Microsoft.AspNetCore.Identity;

namespace SecuritySite.Models
{
    public class MonitoredAccount
    {
        int MonitoredAccountId { get; set; }
        public string emergencyContact { get; set; }
        public string emergencyContactPhoneNumber { get; set; }

        [ProtectedPersonalData]
        public string address { get; set; }
        public string city { get; set; }
        public string county { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }

        [ProtectedPersonalData]
        public string alarmPassword { get; set; }

        [ProtectedPersonalData]
        public string installerCode { get; set; }
        public string additionalInfo { get; set; }
        public int monthlyCost { get; set; }
        private List<AccountFeature> features { get; set; } = new List<AccountFeature>();

        public List<AccountFeature> GetFeatures()
        {
            return features;
        }
        public void AddFeature(int price, string name)
        {
            monthlyCost += price;
            features.Add(new AccountFeature { price=price, featureName=name});
        }
        public void RemoveFeature(int price, string name)
        {
            monthlyCost -= price;
            for(int i = 0; i < features.Count; i++)
            {
                if (features[i].featureName == name)
                {
                    features.RemoveAt(i);
                    return;
                }
            }
        }

        public static MonitoredAccount BaseDIYResidential { get; }= new MonitoredAccount
        {
            monthlyCost = 20,
            features = new List<AccountFeature>
            {
                new AccountFeature {price=20, featureName="BasePlan"}
            }
        };
        public static MonitoredAccount BaseResidentialPlan { get; } = new MonitoredAccount
        {
            monthlyCost = 30,
            features = new List<AccountFeature>
            {
                new AccountFeature {price=30, featureName="BasePlan"}
            }
        };
        public static MonitoredAccount BaseCameraResidentialPlan { get; } = new MonitoredAccount
        {
            monthlyCost = 40,
            features = new List<AccountFeature>
            {
                new AccountFeature {price=40, featureName="BasePlan"}
            }
        };
        public static MonitoredAccount BaseDIYCommercial { get; } = new MonitoredAccount
        {
            monthlyCost = 30,
            features = new List<AccountFeature>
            {
                new AccountFeature {price=30, featureName="BasePlan"}
            }
        };
        public static MonitoredAccount BaseCommercialPlan { get; } = new MonitoredAccount
        {
            monthlyCost = 45,
            features = new List<AccountFeature>
            {
                new AccountFeature {price=45, featureName="BasePlan"}
            }
        };

        public static MonitoredAccount BaseCameraCommercialPlan { get; } = new MonitoredAccount
        {
            monthlyCost = 65,
            features = new List<AccountFeature>
            {
                new AccountFeature {price=65, featureName="BasePlan"}
            }
        };

    }
}
