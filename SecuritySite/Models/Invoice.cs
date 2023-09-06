using Microsoft.Identity.Client;
using System.Drawing;

namespace SecuritySite.Models
{
    public class Invoice
    {
        public int IvoiceId { get; set; }
        public int InvoiceNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public float Price { get; set; }
        public int Accounts { get; set; }
        public string CardEnding { get; set; }
        public DateTime Date { get; set; }
    }
}
