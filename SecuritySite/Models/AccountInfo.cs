using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace SecuritySite.Models
{
    public class AccountInfo
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Name is required")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Email is required")]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Phone Number is required")]
        public string Address { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Address is required")]
        public string City { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "City is required")]
        public string County { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "County is required")]
        public string ZipCode { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "State is required")]
        public string State { get; set; }
        public int Accounts { get; set; }
        public float MonthlyCost { get; set; }
        public float LastBill { get; set; }
        public DateTime BillDate { get; set; }
        public Invoice Invoice { get; set; }

        public bool Completed()
        {
            if(this == null)
                return false;
            if (Email == null || ReplaceSpace(Email) == "")
                return false;
            if (PhoneNumber == null || ReplaceSpace(PhoneNumber) == "")
                return false;
            if (Address == null || ReplaceSpace(Address) == "")
                return false;
            if (City == null || ReplaceSpace(City) == "")
                return false;
            if (County == null || ReplaceSpace(County) == "")
                return false;
            if (ZipCode == null || ReplaceSpace(ZipCode) == "")
                return false;
            if (State == null || ReplaceSpace(State) == "")
                return false;

            return true;
        }

        private string ReplaceSpace(string s)
        {
            return s.Replace(" ", "");
        }
    }


}
