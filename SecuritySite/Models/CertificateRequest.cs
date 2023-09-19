using System.ComponentModel.DataAnnotations;

namespace SecuritySite.Models
{
    public class CertificateRequest
    { 
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string InsuranceAgency { get; set; } = "";
        public string AgentName { get; set; } = "";
        public string AgentEmail { get; set; } = "";
        public string PolicyNumber { get; set; } = "";
        public string AgencyAddress { get; set; } = "";
        public string? AgencySecondAddress { get; set; } = "";
        public string AgencyCity { get; set; } = "";
        public string AgencyState { get; set; } = "";
        public string AgencyZipCode { get; set; } = "";
        public string AgencyCountry { get; set; } = "";
        public string AgencyPhone { get; set; } = "";
        public bool ApplyingForFireCertificate { get; set; }
        public string? MoreInfo { get; set; } = "";
        public bool Approved { get; set; }
        public string AccountOwner { get; set; } = "";
        public DateTime? Submitted { get; set; }
    }
}
