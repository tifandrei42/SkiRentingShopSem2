using ClassLibrary.DataAccess;
using ClassLibrary.Managers;
using ClassLibrary.ObjectClasses;
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
        public int Id { get; set; }  // Equipment ID from route

        public Equipment Equipment { get; set; }

        public IActionResult OnGet()
        {
            Equipment = _equipmentManager.GetEquipmentById(Id);

            // If no equipment is found, redirect back to the equipment list page
            if (Equipment == null)
            {
                return RedirectToPage("/Content");
            }

            return Page();
        }
    }
}
