using System;
using System.Collections.Generic;
using System.Reflection;
using FamilyBudget.Data.Attributes;
using FamilyBudget.Data.Domain;

namespace FamilyBudget.Data.Utilities
{
    public static class EnumUtil
    {
        public static string GetApiName(Enum anEnum)
        {
            string apiName = anEnum.ToString();

            // get the friendly name of this enum, if it exists
            object[] apiNameAttributes = GetCustomAttributes(typeof(ApiNameAttribute), anEnum);
            if (apiNameAttributes.Length > 0)
            {
                apiName = ((ApiNameAttribute)apiNameAttributes[0]).Name;
            }

            // return the name, which is defaulted to the value of ToString() on the enum, if no FriendlyName attribute exists
            return apiName;
        }
        
        public static string GetFriendlyName(Enum anEnum)
        {
            string friendlyName = anEnum.ToString();

            // get the friendly name of this enum, if it exists
            object[] friendlyNameAttributes = GetCustomAttributes(typeof(FriendlyNameAttribute), anEnum);
            if (friendlyNameAttributes.Length > 0)
            {
                friendlyName = ((FriendlyNameAttribute)friendlyNameAttributes[0]).Name;
            }

            // return the name, which is defaulted to the value of ToString() on the enum, if no FriendlyName attribute exists
            return friendlyName;
        }

        public static bool IsComparable(this Enum anEnum)
        {
            object[] comparables = GetCustomAttributes(typeof(ComparableAttribute), anEnum);

            // if the object array has any elements, then this enumeration is a comparable
            return comparables.Length > 0;
        }

        public static EnumListMember[] GetEnumMemberArray(Type enumType)
        {
            FieldInfo[] fields = enumType.GetFields();
            List<EnumListMember> enumMembers = new List<EnumListMember>();

            foreach (var field in fields)
            {
                string friendlyName = field.Name;

                // pass over fields that are not enumeration fields
                if (field.Name.Equals("value__")) continue;

                // get the friendly name
                foreach (Attribute attr in field.GetCustomAttributes(typeof(FriendlyNameAttribute), false))
                {
                    // if in this loop, a friendly name should exist, so get it
                    FriendlyNameAttribute fra = (FriendlyNameAttribute)attr;
                    friendlyName = fra.Name;
                }

                enumMembers.Add(new EnumListMember(friendlyName, (int)field.GetRawConstantValue()));
            }

            // return the object array to the caller
            return enumMembers.ToArray();
        }

        public static EnumListMember GetEnumMember(Enum anEnum)
        {
            string enumeration = anEnum.ToString();
            EnumListMember match = null;
            EnumListMember[] enumArray = GetEnumMemberArray(anEnum.GetType());

            foreach (EnumListMember member in enumArray)
            {
                if (member.DisplayValue == enumeration)
                {
                    match = member;
                }
            }

            return match;            
        }

        private static object[] GetCustomAttributes(Type type, Enum anEnum)
        {
            // use reflection to get an array of attributes of type that decorate the given enumeration
            MemberInfo memInfo = (anEnum.GetType().GetMember(anEnum.ToString()))[0];
            return memInfo.GetCustomAttributes(type, false);
        }
    }
}
