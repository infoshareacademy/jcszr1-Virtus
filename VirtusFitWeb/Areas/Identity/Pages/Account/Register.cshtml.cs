﻿using System;
using BLL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using VirtusFitApi.Models;
using VirtusFitWeb.DAL;

namespace VirtusFitWeb.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IProductRepository _productRepository;
        private readonly IHttpClientFactory _httpClientFactory;


        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IProductRepository productRepository,
            IHttpClientFactory httpClientFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _productRepository = productRepository;
            _httpClientFactory = httpClientFactory;
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

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var client = _httpClientFactory.CreateClient();
                    var action = CreateAction(user.UserName, UserAccountActionType.AccountCreated);
                    await client.PostAsync("https://localhost:5001/VirtusFit/user",
                        new StringContent(JsonSerializer.Serialize(action), Encoding.UTF8, "application/json"));

                    var accountStatus = CreateAction(user.UserName, UserAccountActionType.UserUnlocked);
                    await client.PostAsync("https://localhost:5001/VirtusFit/user",
                        new StringContent(JsonSerializer.Serialize(accountStatus), Encoding.UTF8, "application/json"));

                    var passwordStatus = CreateAction(user.UserName, UserAccountActionType.PasswordChanged);
                    await client.PostAsync("https://localhost:5001/VirtusFit/user",
                        new StringContent(JsonSerializer.Serialize(passwordStatus), Encoding.UTF8, "application/json"));

                    var seedData = ProductLoader.GetProductsFromFile();
                    foreach (var product in seedData)
                    {
                        _productRepository.InsertProduct(new Product
                        {
                            ProductNo = product.ProductId,
                            ProductName = product.ProductName,
                            Energy = product.Energy,
                            Fat = product.Fat,
                            Carbohydrates = product.Carbohydrates,
                            Protein = product.Protein,
                            Salt = product.Salt,
                            Fiber = product.Fiber,
                            Sugar = product.Sugar,
                            Quantity = product.Quantity,
                            PortionQuantity = product.PortionQuantity,
                            PortionUnit = product.PortionUnit,
                            IsFavorite = product.IsFavorite,
                            UserId = user.Id
                        });
                    }

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                   if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
