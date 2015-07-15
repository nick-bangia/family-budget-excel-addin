using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FamilyBudget.Common.Utilities
{
    public static class StringUtil
    {
        public static string ToDelimitedString(this IEnumerable<string> list, char delimiter)
        {
            StringBuilder sb = new StringBuilder();

            // enumerate through a list of strings and build a delimited list
            // start with the first one
            int i = 0;

            // loop through the list and continue appending elements with the delimiter until the last element
            do
            {
                sb.Append(list.ElementAt(i));
                // only append the delimiter if we are in the middle of the list, and there is more than one element
                if ((list.Count() - 1) != i)
                {
                    sb.Append(delimiter);
                }

                // increment the counter
                i += 1;
            }
            while ((list.Count() - 1) >= i);

            return sb.ToString();
        }

        public static long GetStringChecksum(this string input)
        {
            // prepare to get the checksum
            char[] charArray = input.ToCharArray();
            long checksum = 0;

            // loop through the character array, and sum up the
            // character value of each character
            foreach (char character in charArray)
            {
                checksum += (long)character;
            }

            return checksum;
        }
    }
}
