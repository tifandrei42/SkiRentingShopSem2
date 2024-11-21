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
        private readonly StockManager _stockManager;

        public DetailedProductViewModel(EquipmentManager equipmentManager)
        {
            _equipmentManager = equipmentManager;
            _stockManager = new StockManager();
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; } // Equipment ID from route

        public Equipment Equipment { get; private set; }
        public int Stock { get; private set; }

        public IActionResult OnGet()
        {
            Equipment = _equipmentManager.GetEquipmentById(Id);

            if (Equipment == null)
            {
                return RedirectToPage("/Error", new { errorMessage = "The requested equipment was not found." });
            }

            Stock = _stockManager.GetNumberOfStockByEquipmentId(Id); 

            return Page();
        }
    }
}
