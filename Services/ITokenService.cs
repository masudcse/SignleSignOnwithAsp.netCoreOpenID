using Microsoft.Identity.Client;

namespace SSOExample.Services
{
    public interface ITokenService
    {
        Task<string> GetAccessTokenAsync();
        Task<AuthenticationResult> GetAuthenticationResultAsync();
        Task<string> GetValidAccessTokenAsync();
    }
}
