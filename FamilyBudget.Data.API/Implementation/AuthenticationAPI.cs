using FamilyBudget.Common.Interfaces;
using FamilyBudget.Data.API.Domain;
using FamilyBudget.Data.API.Utilities;
using FamilyBudget.Common.Config;

namespace FamilyBudget.Data.API.Implementation
{
    public class AuthenticationAPI : IAuthentication
    {
        public bool Authenticate()
        {
            ApiToken apiToken = APIUtil.Login();
            if (apiToken.accessToken != null)
            {
                AddInConfiguration.APIConfiguration.AccessToken = apiToken.accessToken;
                return true;
            }

            return false;
        }
    }        
}
