using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using BusinessLogic.Managers;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using BusinessLogic.Strategies;
using BusinessLogic.DataAccess;
using Newtonsoft.Json; // For serialization

namespace Renting_Website.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        private readonly LoginService loginService;

        public string PageTitle { get; private set; } = "Login";

        public LoginModel()
        {
            IUserMediator userMediator = new UserMediator();
            UserManager userManager = new UserManager(userMediator);
            EncriptionManager encriptionManager = new EncriptionManager();
            loginService = new LoginService(userManager, encriptionManager);
        }

        public IActionResult OnGet(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            User customer = loginService.Login(Email, Password);

            if (customer == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password.");
                return Page();
            }

            // Initialize an empty basket
            Basket basket = new Basket();

            // Serialize the basket into JSON for storage in claims
            string basketJson = JsonConvert.SerializeObject(basket);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, customer.UserName),
                new Claim(ClaimTypes.Email, customer.Email),
                new Claim("FullName", $"{customer.FirstName} {customer.LastName}"),
                new Claim(ClaimTypes.Role, "Customer"),
                new Claim("UserId", customer.User_Id.ToString()),
                new Claim("Basket", basketJson) // Store the basket in claims
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddHours(1)
                });

            returnUrl = returnUrl ?? Url.Page("/Index");
            return LocalRedirect(returnUrl);
        }
    }
}
