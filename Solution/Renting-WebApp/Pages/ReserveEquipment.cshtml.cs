using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using BusinessLogic.Managers;
using BusinessLogic.Entities;
using System.Text.Json;
using BusinessLogic.Enums;

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
        public Basket? Basket { get; set; } = new();

        public IActionResult OnGet()
        {
            // Retrieve equipment details
            Equipment = _equipmentManager.GetEquipmentById(Id);

            if (Equipment == null)
            {
                return RedirectToPage("/Error", new { errorMessage = "The requested equipment was not found." });
            }

            ReservationDate = DateTime.Today;

            // Load basket from session
            var basketJson = HttpContext.Session.GetString("Basket");
            if (!string.IsNullOrEmpty(basketJson))
            {
                Basket = JsonSerializer.Deserialize<Basket>(basketJson);
            }

            return Page();
        }

        public IActionResult OnPostAddToBasket()
        {
            // Retrieve the selected equipment
            Equipment = _equipmentManager.GetEquipmentById(Id);

            if (Equipment == null)
            {
                return RedirectToPage("/Error", new { errorMessage = "Equipment not found." });
            }

            // Check availability
            if (!_availabilityManager.CheckAvailability(ReservationDate, Equipment, Basket, 1))
            {
                ModelState.AddModelError(string.Empty, "The equipment is not available on the selected date.");
                return Page();
            }

            var basketJson = HttpContext.Session.GetString("Basket");
            Basket? basket = string.IsNullOrEmpty(basketJson)
                ? new Basket()
                : JsonSerializer.Deserialize<Basket>(basketJson);

            var customerIdClaim = User.FindFirst("UserId"); 
            int customerId = customerIdClaim != null ? int.Parse(customerIdClaim.Value) : 0;

            basket.AddReservation(new Reservation
            {
                ReservationId = -1,
                Equipment = Equipment,
                CustomerId = customerId,
                ReservationDate = ReservationDate,
                CreationDate = DateTime.Now,
                TotalPrice = Equipment.PricePerDay,
                Status = Status.Pending
            });

            HttpContext.Session.SetString("Basket", JsonSerializer.Serialize(basket));

            return RedirectToPage("/Basket");
        }

        private List<DateTime> GetUnavailableDates(int equipmentId)
        {
            var unavailableDates = new List<DateTime>();

            // Get all reservations for this equipment
            var reservations = _reservationManager.GetReservations();
            foreach (var reservation in reservations)
            {
                if (reservation.Equipment.EquipmentId == equipmentId)
                {
                    unavailableDates.Add(reservation.ReservationDate.Date); 
                }
            }

            return unavailableDates;
        }
    }
}
