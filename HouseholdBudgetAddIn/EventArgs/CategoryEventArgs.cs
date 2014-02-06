using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HouseholdBudget.Enums;
using HouseholdBudget.Data.Domain;

namespace HouseholdBudget.Events
{
    internal class CategoryEventArgs : EventArgs
    {
        public Category category { get; set; }
    }

    internal class SubCategoryEventArgs : EventArgs
    {
        public SubCategory subCategory { get; set; }
    }

    internal class CategoryControlEventArgs : EventArgs
    {
        public CategoryFormType formType { get; set; }
    }
}
