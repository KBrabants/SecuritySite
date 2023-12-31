﻿namespace SecuritySite.Models
{
    public class AccountFeature
    {
        public int Id { get; set; }
        public float Price { get; set; }
        public string Code { get; set; } = "";
        public string FeatureName { get; set; } = "";
        public string Description { get; set; } = "";
        public bool Commercial { get; set; }
        public bool BasePlan { get; set; }

        public static explicit operator AccountFeature(AccountAddon addon)
        {
            return new AccountFeature()
            {
                Id = addon.Id,
                Price = addon.Price,
                Code = addon.Code,
                FeatureName = addon.FeatureName,
                Description = addon.Description,
                Commercial = addon.Commercial,
                BasePlan = addon.BasePlan
            };
        }
    }
}
