using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FamilyBudget.Common.Attributes;

namespace FamilyBudget.AddIn.Enums
{
    internal enum DataWorksheetColumns
    {
        [FriendlyName("ID")]
        UNIQUE_ID = 1,

        [FriendlyName("Date")]
        DATE = 2,

        [FriendlyName("Description")]
        DESCRIPTION = 3,

        [FriendlyName("Amount")]
        AMOUNT = 4,

        [FriendlyName("Category")]
        CATEGORY = 5,

        [FriendlyName("SubCategory")]
        SUBCATEGORY = 6,

        [FriendlyName("Type")]
        TYPE = 7,

        [FriendlyName("Payment Method")]
        PAYMENT_METHOD = 8,

        [FriendlyName("Status")]
        STATUS = 9
    }
}
