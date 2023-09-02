namespace SecuritySite.Models
{
    public class AccountFeature
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public string FeatureName { get; set; } = "";
        public string Description { get; set; } = "";
        public bool Commercial { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
