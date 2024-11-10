using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Renting_Website.Pages
{
    [Authorize(Roles = "Customer")]  
    public class CustomerProfileModel : PageModel
    {
        public void OnGet()
        {
            
        }
    }
}
