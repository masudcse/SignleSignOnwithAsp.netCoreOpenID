using Microsoft.AspNetCore.Mvc;
using SSOExample.Models;
using SSOExample.Services;
using System.Text.Json;

namespace SSOExample.Controllers
{
    public class AccountController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly IHttpClientFactory _httpClientFactory;
        public AccountController(ITokenService tokenService, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Profile()
        {
            try
            {
                var accessToken = await _tokenService.GetValidAccessTokenAsync();
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                var response = await client.GetAsync("https://graph.microsoft.com/v1.0/me");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var profile = JsonSerializer.Deserialize<UserProfile>(content);
                    return View(profile);
                }

                return View(new UserProfile());
            }
            catch (Exception ex)
            {
              //  _logger.LogError(ex, "Error getting user profile");
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
