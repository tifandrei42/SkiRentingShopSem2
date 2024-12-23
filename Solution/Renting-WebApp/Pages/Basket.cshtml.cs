using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogic.Entities;
using Newtonsoft.Json; // For JSON serialization
using System.Collections.Generic;

namespace Renting_Website.Pages
{
    public class BasketModel : PageModel
    {
        public List<Reservation>? Basket { get; set; } = new();

        // Load the basket when the page loads
        public void OnGet()
        {
            var basketJson = HttpContext.Session.GetString("Basket"); // Get JSON string
            if (!string.IsNullOrEmpty(basketJson))
            {
                Console.WriteLine($"Basket JSON: {basketJson}");

                Basket = JsonConvert.DeserializeObject<List<Reservation>>(basketJson); // Deserialize it
            }
        }

        // Remove an item by ID
        public IActionResult OnPostRemove(int id)
        {
            var basketJson = HttpContext.Session.GetString("Basket");
            if (!string.IsNullOrEmpty(basketJson))
            {
                var basket = JsonConvert.DeserializeObject<List<Reservation>>(basketJson);
                basket.RemoveAll(r => r.Equipment.EquipmentId == id); // Remove item
                HttpContext.Session.SetString("Basket", JsonConvert.SerializeObject(basket)); // Save updated basket
            }
            return RedirectToPage();
        }

        // Clear all items in the basket
        public IActionResult OnPostClear()
        {
            HttpContext.Session.Remove("Basket");
            return RedirectToPage();
        }

    }
}
