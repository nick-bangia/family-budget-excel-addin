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

        [FriendlyName("Tax Deductible?")]
        TAX_DEDUCTIBLE = 5,

        [FriendlyName("Category")]
        CATEGORY = 6,

        [FriendlyName("SubCategory")]
        SUBCATEGORY = 7,

        [FriendlyName("Type")]
        TYPE = 8,

        [FriendlyName("Payment Method")]
        PAYMENT_METHOD = 9,

        [FriendlyName("Status")]
        STATUS = 10
    }
}
