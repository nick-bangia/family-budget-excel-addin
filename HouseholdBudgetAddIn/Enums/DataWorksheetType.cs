using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HouseholdBudget.Data.Attributes;

namespace HouseholdBudget.Enums
{
    public enum DataWorksheetType
    {
        [FriendlyName("Pending")]
        PENDING = 0,

        [FriendlyName("Future")]
        FUTURE = 1,

        [FriendlyName("Search Results")]
        SEARCH_RESULTS = 2,

        [FriendlyName("New Entries")]
        NEW_ENTRIES = 3,

        [FriendlyName("Import Results")]
        IMPORT_RESULTS = 4
    }
}
