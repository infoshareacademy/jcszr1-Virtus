using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using VirtusFitApi.Models;

namespace VirtusFitWeb.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public LogoutModel(SignInManager<IdentityUser> signInManager, ILogger<LogoutModel> logger, IHttpClientFactory httpClientFactory)
        {
            _signInManager = signInManager;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public void OnGet()
        {
        }

        public CreateUserAccountAction CreateAction(string userName, UserAccountActionType type)
        {
            var action = new CreateUserAccountAction
            {
                UserId = userName,
                ActionType = type,
                Created = DateTime.UtcNow
            };

            return action;

        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            var client = _httpClientFactory.CreateClient();
            var action = CreateAction(_signInManager.Context.User.Identity.Name, UserAccountActionType.UserLoggedOut);
            await client.PostAsync("https://localhost:5001/VirtusFit/user",
                new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));


            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
