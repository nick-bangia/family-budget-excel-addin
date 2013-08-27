using System;

namespace HouseholdBudget.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class FriendlyNameAttribute : Attribute
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
