using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HouseholdBudget.Enums;

namespace HouseholdBudget.Events
{
    internal class CategoryEventArgs : EventArgs
    {
        public string CategoryName { get; set; }
    }

    internal class SubCategoryEventArgs : EventArgs
    {
        public Guid CategoryKey { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryPrefix { get; set; }
        public bool IsActive { get; set; }
    }

    internal class CategoryControlEventArgs : EventArgs
    {
        public CategoryFormType formType { get; set; }
    }
}
