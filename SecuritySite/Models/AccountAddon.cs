namespace SecuritySite.Models
{
    public class AccountAddon
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public string Code { get; set; } = "";
        public string FeatureName { get; set; } = "";
        public string Description { get; set; } = "";
        public bool Commercial { get; set; }
        public bool BasePlan { get; set; }
        public DateTime DateAdded { get; set; }

        public static explicit operator AccountAddon(AccountFeature addon)
        {
            return new AccountAddon()
            {
                Id = addon.Id,
                Price = addon.Price,
                Code = addon.Code,
                FeatureName = addon.FeatureName,
                Description = addon.Description,
                Commercial = addon.Commercial,
                BasePlan = addon.BasePlan,
                DateAdded = DateTime.UtcNow
            };
        }
    }
}
