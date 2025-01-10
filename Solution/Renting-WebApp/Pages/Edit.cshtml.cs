using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using BusinessLogic.Managers;
using BusinessLogic.Strategies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Renting_Website.Pages
{
    [Authorize(Roles = "Customer")]
    public class EditModel : PageModel
    {
        private readonly UserManager _userManager;
        private readonly LoginService _loginService;

        [BindProperty]
        public User Customer { get; set; }

        public EditModel(IUserMediator userMediator)
        {
            _userManager = new(userMediator);
            EncriptionManager encriptionManager = new EncriptionManager();
            _loginService = new LoginService(_userManager, encriptionManager);
        }

        public void OnGet(int id)
        {
            Customer = _userManager.GetUserById(id);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _loginService.Update(Customer, Customer.Password);
            return RedirectToPage("/Profile", new { id = Customer.User_Id });
        }
    }
}
