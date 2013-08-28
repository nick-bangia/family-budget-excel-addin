using System;

namespace HouseholdBudget.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class FriendlyNameAttribute : Attribute
    {
        private string _friendlyName;

        public FriendlyNameAttribute(String friendlyName)
        {
            this._friendlyName = friendlyName;
        }

        public string Name
        {
            get
            {
                return this._friendlyName;
            }
        }
    }
}
