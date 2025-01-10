using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogic.Managers;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Renting_Website.Pages
{
    [Authorize(Roles = "Customer")]
    public class DeleteAccountModel : PageModel
    {
        private readonly UserManager _userManager;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public DeleteAccountModel(IUserMediator userMediator)
        {
            _userManager = new(userMediator);
        }

        public IActionResult OnPost()
        {
            _userManager.DeleteUser(Id);
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }
    }
}
