using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Renting_Website.Pages
{
    public class AccessDeniedModel : PageModel
    {
        public string ReturnUrl { get; set; }

        public void OnGet()
        {
            ReturnUrl = HttpContext.Request.Query["ReturnUrl"];
        }
    }
}
