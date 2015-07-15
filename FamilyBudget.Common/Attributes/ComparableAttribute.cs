using System;

namespace FamilyBudget.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ComparableAttribute : Attribute
    {        
        // if attribute is used, then the field is comparable
        public ComparableAttribute()
        {}
    }
}
