using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using BusinessLogic.Managers;
using BusinessLogic.Entities;
using System.Text.Json;

namespace Renting_Website.Pages
{
    public class ReserveEquipmentModel : PageModel
    {
        private readonly ReservationManager _reservationManager;
        private readonly EquipmentManager _equipmentManager;
        private readonly AvailabilityManager _availabilityManager;

        public ReserveEquipmentModel(EquipmentManager equipmentManager)
        {
            _reservationManager = new ReservationManager();
            _equipmentManager = equipmentManager;
            _availabilityManager = new AvailabilityManager();
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }  

        [BindProperty]
        public DateTime ReservationDate { get; set; }
        public Equipment Equipment { get; set; }
        public List<BasketEquipment> Basket { get; set; } = new();

        public IActionResult OnGet()
        {
            Equipment = _equipmentManager.GetEquipmentById(Id);

            if (Equipment == null)
            {
                return RedirectToPage("/Error", new { errorMessage = "The requested equipment was not found." });
            }

            ReservationDate = DateTime.Today;

            var basketJson = HttpContext.Session.GetString("Basket");
            if (!string.IsNullOrEmpty(basketJson))
            {
                Basket = JsonSerializer.Deserialize<List<BasketEquipment>>(basketJson);
            }

            return Page();
        }

        public IActionResult OnPostAddToBasket()
        {
            Equipment = _equipmentManager.GetEquipmentById(Id);

            if (Equipment == null)
            {
                return RedirectToPage("/Error", new { errorMessage = "Equipment not found." });
            }

            if (!_availabilityManager.CheckAvailability(ReservationDate, Equipment))
            {
                ModelState.AddModelError(string.Empty, "The equipment is not available on the selected date.");
                return Page();
            }

            var basketJson = HttpContext.Session.GetString("Basket");
            List<BasketEquipment> basket = string.IsNullOrEmpty(basketJson)
                ? new List<BasketEquipment>()
                : JsonSerializer.Deserialize<List<BasketEquipment>>(basketJson);

            basket.Add(new BasketEquipment
            {
                EquipmentId = Equipment.EquipmentId,
                Name = Equipment.Name,
                Date = ReservationDate,
                PricePerDay = Equipment.PricePerDay
            });

            HttpContext.Session.SetString("Basket", JsonSerializer.Serialize(basket));

            return RedirectToPage("/Basket");
        }
    }
}
