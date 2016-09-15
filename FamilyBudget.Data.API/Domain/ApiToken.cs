using System;

namespace FamilyBudget.Data.API.Domain
{
    public class ApiToken
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }   
        public DateTime accessExpiresOn { get; set; }
        public DateTime refreshExpiresOn { get; set; }
        public string message { get; set; }

        public ApiToken(string accessToken, DateTime accessExpiresOn, string refreshToken, DateTime refreshExpiresOn)
        {
            this.accessToken = accessToken;
            this.accessExpiresOn = accessExpiresOn;
            this.refreshToken = refreshToken;
            this.refreshExpiresOn = refreshExpiresOn;
        }

        public ApiToken(string failureMessage)
        {
            this.message = failureMessage;
            this.accessToken = null;
        }
    }
}
