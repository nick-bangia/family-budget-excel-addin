using System;
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
        public Subcategory subCategory { get; set; }
    }

    internal class GoalEventArgs : EventArgs
    {
        public Subcategory subCategory { get; set; }
        public decimal GoalAmount { get; set; }
    }

    internal class CategoryControlEventArgs : EventArgs
    {
        public CategoryFormType formType { get; set; }
    }
}
