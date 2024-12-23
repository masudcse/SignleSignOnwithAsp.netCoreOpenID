
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;

namespace SSOExample.Services
{
    public class TokenService : ITokenService
    {
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public async Task<string> GetAccessTokenAsync()
        {
            string[] scopes = new[] { "User.Read", "profile", "email" };
            return await _tokenAcquisition.GetAccessTokenForUserAsync(scopes);
        }

        public async Task<AuthenticationResult> GetAuthenticationResultAsync()
        {
            string[] scopes = new[] { "User.Read", "profile", "email" };
            return await _tokenAcquisition.GetAuthenticationResultForUserAsync(scopes);
        }

        public async Task<string> GetValidAccessTokenAsync()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User;
            if (currentUser == null || !currentUser.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
            return await GetAccessTokenAsync();
        }
    }
}
