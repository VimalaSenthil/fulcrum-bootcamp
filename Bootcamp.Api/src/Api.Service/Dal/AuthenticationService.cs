using System.Threading.Tasks;
using Xlent.Lever.Authentication.Sdk;
using Xlent.Lever.Libraries2.Core.MultiTenant.Model;
using Xlent.Lever.Libraries2.Core.Platform.Authentication;

namespace Api.Service.Dal
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthenticationManager _authenticationManager;

        public AuthenticationService(string baseUrl, Tenant tenant, AuthenticationCredentials credentials)
        {
            _authenticationManager = new AuthenticationManager(tenant, baseUrl, credentials);
        }

        public async Task<AuthenticationToken> GetTokenForTenant(AuthenticationCredentials credentials)
        {
            IAuthenticationCredentials tokenCredentials = new AuthenticationCredentials
            {
                ClientId = credentials.ClientId,
                ClientSecret = credentials.ClientSecret
            };
            return await _authenticationManager.GetJwtTokenAsync(tokenCredentials);
        }
    }
}