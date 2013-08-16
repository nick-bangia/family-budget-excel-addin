using System;
using HouseholdBudget.Attributes;
using System.Reflection;

namespace HouseholdBudget.Utilities
{
    internal class EnumUtil
    {
        public static string GetFriendlyName(Enum anEnum)
        {
            string friendlyName = anEnum.ToString();

            MemberInfo memInfo = (anEnum.GetType().GetMember(anEnum.ToString()))[0];
            foreach (Attribute attr in memInfo.GetCustomAttributes(typeof(FriendlyNameAttribute), false))
            {
                // if in this loop, a friendly name should exist, so get it
                FriendlyNameAttribute fra = (FriendlyNameAttribute)attr;
                friendlyName = fra.Name;
            }

            // return the name, which is defaulted to the value of ToString() on the enum, if no FriendlyName attribute exists
            return friendlyName;
        }
    }
}
