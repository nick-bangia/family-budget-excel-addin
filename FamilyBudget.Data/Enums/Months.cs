using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FamilyBudget.Data.Attributes;

namespace FamilyBudget.Data.Enums
{
    public enum Months
    {
        [FriendlyName("January")]
        JANUARY = 1,

        [FriendlyName("February")]
        FEBRUARY = 2,

        [FriendlyName("March")]
        MARCH = 3,

        [FriendlyName("April")]
        APRIL = 4,

        [FriendlyName("May")]
        MAY = 5,

        [FriendlyName("June")]
        JUNE = 6,

        [FriendlyName("July")]
        JULY = 7,

        [FriendlyName("August")]
        AUGUST = 8,

        [FriendlyName("September")]
        SEPTEMBER = 9,

        [FriendlyName("October")]
        OCTOBER = 10,

        [FriendlyName("November")]
        NOVEMBER = 11,

        [FriendlyName("December")]
        DECEMBER = 12
    }
}
