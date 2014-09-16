using System;

namespace HouseholdBudget.Data.Domain
{
    public class SubCategory
    {
        public Guid SubCategoryKey { get; set; }
        public Guid CategoryKey { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryPrefix { get; set; }
        public bool IsActive { get; set; }
    }
}
