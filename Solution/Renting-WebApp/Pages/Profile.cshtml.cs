using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogic.Managers;
using BusinessLogic.DataAccess;
using BusinessLogic.Interfaces;
using BusinessLogic.Entities;

namespace Renting_Website.Pages
{
    [Authorize(Roles = "Customer")]  
    public class CustomerProfileModel : PageModel
    {
        private readonly UserManager _userManager;
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public User Customer { get; private set; }

        public CustomerProfileModel(IUserMediator userMediator)
        {
            _userManager = new(userMediator);
        }

        public void OnGet()
        {
            Customer = _userManager.GetUserById(Id);
        }
    }
}
