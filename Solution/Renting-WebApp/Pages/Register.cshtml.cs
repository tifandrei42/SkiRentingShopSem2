using ClassLibrary.Managers;
using ClassLibrary.ObjectClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Renting_Website.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public Customer Customer { get; set; } 

        public CustomerManager CustomerManager { get; set; }

        public string PageTitle { get; private set; } = "Register";

        public RegisterModel()
        {
            CustomerManager = new CustomerManager();  
        }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (CustomerManager.CheckEmail(Customer))
                {
                    ModelState.AddModelError("Customer.Email", "The email address is already taken.");
                    return Page();
                }

                Customer.Password = CustomerManager.HashPassword(Customer.Password);

                CustomerManager.AddCustomer(Customer);
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
