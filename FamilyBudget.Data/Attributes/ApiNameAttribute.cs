using System;

namespace FamilyBudget.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ApiNameAttribute : Attribute
    {
        private string _apiName;

        public ApiNameAttribute(String apiName)
        {
            this._apiName = apiName;
        }

        public string Name
        {
            get
            {
                return this._apiName;
            }
        }
    }
}
