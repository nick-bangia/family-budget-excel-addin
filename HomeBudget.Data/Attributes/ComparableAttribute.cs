using System;

namespace HouseholdBudget.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ComparableAttribute : Attribute
    {        
        // if attribute is used, then the field is comparable
        public ComparableAttribute()
        {}
    }
}
