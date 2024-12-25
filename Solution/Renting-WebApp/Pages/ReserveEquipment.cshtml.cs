using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using BusinessLogic.Managers;
using BusinessLogic.Entities;
using Newtonsoft.Json; // Use Newtonsoft for better JSON handling
using BusinessLogic.Enums;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Renting_Website.Pages
{
    [Authorize(Roles = "Customer")]
    public class ReserveEquipmentModel : PageModel
    {
        private readonly ReservationManager _reservationManager;
        private readonly EquipmentManager _equipmentManager;
        private readonly AvailabilityManager _availabilityManager;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Quantity { get; set; }

        [BindProperty]
        public DateTime ReservationDate { get; set; }

        public Equipment Equipment { get; set; }
        public List<Reservation> Basket { get; set; } = new();
        public List<DateTime> UnavailableDates { get; set; } = new();


        public ReserveEquipmentModel(EquipmentManager equipmentManager)
        {
            _reservationManager = new ReservationManager();
            _equipmentManager = equipmentManager;
            _availabilityManager = new AvailabilityManager();
        }

        public IActionResult OnGet()
        {
            // Retrieve Equipment
            Equipment = _equipmentManager.GetEquipmentById(Id);
           

            if (Equipment == null)
            {
                return RedirectToPage("/Error", new { errorMessage = "The requested equipment was not found." });
            }

            if (Quantity <= 0 || Quantity > Equipment.Quantity)
            {
                return RedirectToPage("/Error", new { errorMessage = "Invalid quantity selected." });
            }
            ReservationDate = DateTime.Today;

            // Load the basket
            var basketJson = HttpContext.Session.GetString("Basket");
            if (!string.IsNullOrEmpty(basketJson))
            {
                try
                {
                    // Deserialize basket as a List
                    Basket = JsonConvert.DeserializeObject<List<Reservation>>(basketJson) ?? new List<Reservation>();
                }
                catch (JsonSerializationException ex)
                {
                    Console.WriteLine($"JSON Deserialization Error: {ex.Message}");
                    Basket = new List<Reservation>(); // Reset basket if corrupted
                }
            }
            UnavailableDates = GetUnavailableDates(Equipment);

            return Page();
        }

        public IActionResult OnPostAddToBasket()
        {
            // Retrieve Equipment
            Equipment = _equipmentManager.GetEquipmentById(Id);

            if (Equipment == null)
            {
                return RedirectToPage("/Error", new { errorMessage = "Equipment not found." });
            }

            if (!_availabilityManager.CheckAvailability(ReservationDate, Equipment, Basket, Quantity))
            {
                ModelState.AddModelError(string.Empty, "The equipment is not available on the selected date.");
                return Page();
            }

            // Load basket from session
            var basketJson = HttpContext.Session.GetString("Basket");
            List<Reservation>? basket = string.IsNullOrEmpty(basketJson)
                ? new List<Reservation>()
                : JsonConvert.DeserializeObject<List<Reservation>>(basketJson);

            var customerIdClaim = User.FindFirst("UserId");
            int customerId = customerIdClaim != null ? int.Parse(customerIdClaim.Value) : 0;


            basket.Add(new Reservation
            {
                ReservationId = -1,
                Equipment = Equipment,
                CustomerId = customerId,
                ReservationDate = ReservationDate,
                CreationDate = DateTime.Today.Date,
                TotalPrice = Equipment.PricePerDay,
                Status = Status.Pending,
                Quantity = Quantity
            });

            // Save updated basket as JSON
            HttpContext.Session.SetString("Basket", JsonConvert.SerializeObject(basket));


            return RedirectToPage("/Content");
        }

        private List<DateTime> GetUnavailableDates(Equipment equipment)
        {
            return _availabilityManager.GetUnavailableDates(equipment, Basket, Quantity);
        }

    }
}
