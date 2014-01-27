using System;

namespace HouseholdBudget.Data.Domain
{
    public class Category
    {
        public Guid CategoryKey { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryPrefix { get; set; }
        public bool IsActive { get; set; }
    }
}
