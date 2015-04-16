﻿using System;

namespace FamilyBudget.Data.Domain
{
    public class SubCategory
    {
        public Guid SubCategoryKey { get; set; }
        public Guid CategoryKey { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryPrefix { get; set; }
        public string AccountName { get; set; }
        public bool IsActive { get; set; }
        public bool IsGoal { get; set; }
    }
}