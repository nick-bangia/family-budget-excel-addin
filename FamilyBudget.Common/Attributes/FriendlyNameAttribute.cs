using System;

namespace FamilyBudget.Common.Attributes
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
