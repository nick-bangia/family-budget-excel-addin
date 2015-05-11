using FamilyBudget.Data.Attributes;

namespace FamilyBudget.Data.Enums
{
    public enum DayOfWeek
    {
        [FriendlyName("Sunday")]
        SUNDAY = 1,

        [FriendlyName("Monday")]
        MONDAY = 2,

        [FriendlyName("Tuesday")]
        TUESDAY = 3,

        [FriendlyName("Wednesday")]
        WEDNESDAY = 4,

        [FriendlyName("Thursday")]
        THURSDAY = 5,

        [FriendlyName("Friday")]
        FRIDAY = 6,

        [FriendlyName("Saturday")]
        SATURDAY = 7
    }
}
