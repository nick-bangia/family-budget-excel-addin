using FamilyBudget.Data.Enums;

namespace FamilyBudget.Data.Domain
{
    public class ApiHealth
    {
        public ApiHealthState healthState { get; set; }
        public string healthReason { get; set; }

        public ApiHealth(ApiHealthState state, string reason)
        {
            this.healthState = state;
            this.healthReason = reason;
        }
    }
}
