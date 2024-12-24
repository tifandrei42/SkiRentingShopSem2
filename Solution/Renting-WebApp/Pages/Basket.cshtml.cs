using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessLogic.Entities;
using BusinessLogic.Managers;
using BusinessLogic.Enums;
using Newtonsoft.Json;

namespace Renting_Website.Pages
{
    public class BasketModel : PageModel
    {
        private readonly ReservationManager _reservationManager;
        private readonly AvailabilityManager _availabilityManager;

        [BindProperty]
        public List<Reservation> Basket { get; set; } = new();

        public BasketModel(ReservationManager reservationManager, AvailabilityManager availabilityManager)
        {
            _reservationManager = reservationManager;
            _availabilityManager = availabilityManager;
        }

        public void OnGet()
        {
            var basketJson = HttpContext.Session.GetString("Basket");
            if (!string.IsNullOrEmpty(basketJson))
            {
                Basket = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Reservation>>(basketJson) ?? new List<Reservation>();
            }
        }

        public IActionResult OnPostRemove(int id)
        {
            var basketJson = HttpContext.Session.GetString("Basket");
            if (!string.IsNullOrEmpty(basketJson))
            {
                var basket = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Reservation>>(basketJson);

                var itemToRemove = basket.FirstOrDefault(r => r.Equipment.EquipmentId == id);

                if (itemToRemove != null)
                {
                    basket.Remove(itemToRemove);
                }

                HttpContext.Session.SetString("Basket", Newtonsoft.Json.JsonConvert.SerializeObject(basket));
            }

            return RedirectToPage();
        }

        public IActionResult OnPostClear()
        {
            HttpContext.Session.Remove("Basket");
            return RedirectToPage();
        }

        public IActionResult OnPostCompleteReservation()
        {
            var customerIdClaim = User.FindFirst("UserId");
            int customerId = customerIdClaim != null ? int.Parse(customerIdClaim.Value) : 0;

            if (customerId == 0)
            {
                ModelState.AddModelError(string.Empty, "You must be logged in to complete reservations.");
                return Page();
            }

            // Deserialize the basket from session
            var basketJson = HttpContext.Session.GetString("Basket");
            var basket = string.IsNullOrEmpty(basketJson)
                ? new List<Reservation>()
                : JsonConvert.DeserializeObject<List<Reservation>>(basketJson);

            // Validate basket
            if (basket == null || !basket.Any())
            {
                ModelState.AddModelError(string.Empty, "Your basket is empty.");
                return Page();
            }


            // Check each reservation in the basket
            var toBeRemoved = new List<Reservation>();
            bool reservationsUnavailable = false;
            foreach (var reservation in basket)
            {
                // Check availability for each item
                if (!_availabilityManager.CheckAvailability(
                        reservation.ReservationDate,
                        reservation.Equipment,
                        basket,
                        0))
                {
                    toBeRemoved.Add(reservation);
                    reservationsUnavailable = true;
                }
            }
            // Remove unavailable reservations
            if (reservationsUnavailable)
            {
                // remove
                basket.RemoveAll(x => toBeRemoved.Contains(x));
                // sync with serializable object
                HttpContext.Session.SetString("Basket", Newtonsoft.Json.JsonConvert.SerializeObject(basket));
                //inform user
                ModelState.AddModelError(string.Empty, $"Some reservations have become unavailable. Basket updated!");

                return Page();
            }

            // Process each reservation in the basket
            foreach (var reservation in basket)
            {
                reservation.CustomerId = customerId;
                reservation.CreationDate = DateTime.Today.Date;
                reservation.Status = BusinessLogic.Enums.Status.Pending;
                reservation.TotalPrice = reservation.Equipment.PricePerDay * reservation.Quantity;

                try
                {
                    _reservationManager.CreateReservation(reservation);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Failed to create reservation for '{reservation.Equipment.Name}'. Error: {ex.Message}");
                    return Page();
                }
            }

            // Clear the basket after successful reservation
            HttpContext.Session.Remove("Basket");
            return RedirectToPage("/Reservations");
        }

    }
}
