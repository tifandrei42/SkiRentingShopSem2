using BusinessLogic.Entities;
using BusinessLogic.Managers;
using BusinessLogic.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Renting_Website.Pages
{
    public class DetailedProductViewModel : PageModel
    {
        private readonly EquipmentManager _equipmentManager;

        public DetailedProductViewModel(EquipmentManager equipmentManager)
        {
            _equipmentManager = equipmentManager;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; } // Equipment ID from route

        public Equipment Equipment { get; private set; }

        public IActionResult OnGet()
        {
            Equipment = _equipmentManager.GetEquipmentById(Id);

            if (Equipment == null)
            {
                return RedirectToPage("/Error", new { errorMessage = "The requested equipment was not found." });
            }

            return Page();
        }
    }
}
