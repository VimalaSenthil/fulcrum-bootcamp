using System.Threading.Tasks;
using Xlent.Lever.Libraries2.Core.Platform.Authentication;

namespace Api.Service.Dal
{
    public interface IAuthenticationService
    {
        Task<AuthenticationToken> GetTokenForTenant(AuthenticationCredentials credentials);
    }
}