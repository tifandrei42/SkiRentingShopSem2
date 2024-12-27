using BusinessLogic.DataAccess;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using BusinessLogic.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Renting_Website.Pages
{
    [Authorize(Policy = "CustomerOnly")]
    public class ReservationsModel : PageModel
    {
        private readonly ReservationManager _reservationManager;
        private readonly UserManager userManager;

        public User Customer { get; private set; }
        private int customerId;
        public ReservationsModel(IUserMediator userMediator)
        {
            IReservationMediator reservationMediator = new ReservationMediator();
            _reservationManager = new ReservationManager(reservationMediator);
            userManager = new(userMediator);
        }

        public List<Reservation> Reservations { get; private set; }

        public void OnGet()
        {
            LoadReservations();
        }

        private void LoadReservations()
        {
            var customerIdClaim = User.FindFirst("UserId");
            int customerId = customerIdClaim != null ? int.Parse(customerIdClaim.Value) : 0;

            Customer = userManager.GetUserById(customerId);
            Reservations = _reservationManager.GetReservationsByCustomerId(Customer.User_Id) ?? new List<Reservation>();
        }


        public IActionResult OnPost(int id)
        {
            try
            {
                _reservationManager.CancelReservation(id);
                LoadReservations();
                return Page();
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Failed to cancel reservation.");
                LoadReservations();
                return Page();
            }
        }

        public IActionResult OnPostCancel(int id)
        {
            try
            {
                var reservation = _reservationManager.GetReservationById(id);

                if ((reservation.ReservationDate - DateTime.Today).Days >= 3)
                {
                    
                    _reservationManager.CancelReservation(id);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Reservations can only be canceled at least 3 days in advance.");
                    LoadReservations();
                    return Page();
                }
                LoadReservations();
                return Page();
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Failed to cancel reservation.");
                LoadReservations();
                return Page();
            }
        }
    }
}
