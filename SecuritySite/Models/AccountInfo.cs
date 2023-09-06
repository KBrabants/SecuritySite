using System.Security.Permissions;

namespace SecuritySite.Models
{
    public class AccountInfo
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public int Accounts { get; set; }
        public float MonthlyCost { get; set; }
        public float LastBill { get; set; }
        public DateTime BillDate { get; set; }
        public Invoice Invoice { get; set; }
    }
}
