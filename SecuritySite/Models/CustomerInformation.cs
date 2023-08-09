namespace SecuritySite.Models
{
    public class CustomerInformation
    {
        public string monitoringCode { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string emailAddress { get; set; }
        public string emergencyContact { get; set; }
        public string emergencyContactPhoneNumber { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string county { get; set; }
        public string state { get; set; }
        public string zipcode { get; set; }
        public string alarmPassword { get; set; }
        public string installerCode { get; set; }
        public string additionalInfo { get; set; }
    }
}
