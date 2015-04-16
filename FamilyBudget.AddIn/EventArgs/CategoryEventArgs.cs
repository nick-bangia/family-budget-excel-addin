using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FamilyBudget.AddIn.Enums;
using FamilyBudget.Data.Domain;

namespace FamilyBudget.AddIn.Events
{
    internal class CategoryEventArgs : EventArgs
    {
        public Category category { get; set; }
    }

    internal class SubCategoryEventArgs : EventArgs
    {
        public SubCategory subCategory { get; set; }
    }

    internal class GoalEventArgs : EventArgs
    {
        public SubCategory subCategory { get; set; }
        public decimal GoalAmount { get; set; }
    }

    internal class CategoryControlEventArgs : EventArgs
    {
        public CategoryFormType formType { get; set; }
    }
}
