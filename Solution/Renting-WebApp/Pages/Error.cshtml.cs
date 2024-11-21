using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Renting_Website.Pages
{
    public class ErrorModel : PageModel
    {
        public string ErrorMessage { get; private set; }

        public void OnGet(string errorMessage = null)
        {
            ErrorMessage = errorMessage;
        }
    }
}
