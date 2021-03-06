using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VirtusFitApi.Models;
using AppContext = VirtusFitWeb.DAL.AppContext;


namespace VirtusFitWeb.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginModel(SignInManager<IdentityUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<IdentityUser> userManager,
            AppContext context,
            IHttpClientFactory httpClientFactory)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
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

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (_context.BlockedUsers.Any(b=>b.Email==Input.Email))
            {

                ModelState.AddModelError(string.Empty, "Your account has been blocked. Contact administrator");
                return Page();
            }

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                var client = _httpClientFactory.CreateClient();
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    var action = CreateAction(Input.Email, UserAccountActionType.SuccessfulLogonAttempt);
                    await client.PostAsync("https://localhost:5001/VirtusFit/user",
                        new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    var action = CreateAction(Input.Email, UserAccountActionType.FailedLogonAttempt);
                    await client.PostAsync("https://localhost:5001/VirtusFit/user",
                        new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    var action = CreateAction(Input.Email, UserAccountActionType.FailedLogonAttempt);
                    await client.PostAsync("https://localhost:5001/VirtusFit/user",
                        new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
