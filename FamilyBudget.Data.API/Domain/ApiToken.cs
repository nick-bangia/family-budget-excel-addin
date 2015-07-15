using System;

namespace FamilyBudget.Data.API.Domain
{
    public class ApiToken
    {
        public string accessToken { get; set; }
        public DateTime expiresOn { get; set; }
        public string message { get; set; }

        public ApiToken(string accessToken, DateTime expiresOn)
        {
            this.accessToken = accessToken;
            this.expiresOn = expiresOn;
        }

        public ApiToken(string failureMessage)
        {
            this.message = failureMessage;
            this.accessToken = null;
        }
    }
}
