using BusinessLogic.Entities;
using BusinessLogic.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Renting_Website.Pages
{
    public class ReservationsModel : PageModel
    {
        private readonly ReservationManager _reservationManager;

        public ReservationsModel()
        {
            _reservationManager = new ReservationManager();
        }

        public List<Reservation> Reservations { get; private set; }

        public void OnGet()
        {
            int customerId = 1; // Replace with actual logged-in user ID
            Reservations = _reservationManager.GetReservationsByCustomerId(customerId);
        }

        public IActionResult OnPost(int id)
        {
            try
            {
                _reservationManager.CancelReservation(id);
                return RedirectToPage("/MyReservations");
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Failed to cancel reservation.");
                return Page();
            }
        }
    }
}
