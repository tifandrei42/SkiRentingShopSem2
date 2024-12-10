using BusinessLogic.DataAccess;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using BusinessLogic.Managers;
using BusinessLogic.Strategies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Renting_Website.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }


        private readonly IAuthenticationStrategy authenticationService;

        public string PageTitle { get; private set; } = "Register";

        public RegisterModel()
        {
            IUserMediator userMediator = new UserMediator();
            UserManager userManager = new UserManager(userMediator);
            EncriptionManager encriptionManager = new EncriptionManager();

            authenticationService = new CustomerStrategy(userManager, encriptionManager);
        }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                User.Role = UserRole.Customer;
                authenticationService.Register(User, User.Password);
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
