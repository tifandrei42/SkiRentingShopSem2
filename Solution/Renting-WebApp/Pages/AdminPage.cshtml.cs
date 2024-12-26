using BusinessLogic.Entities;
using BusinessLogic.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Renting_Website.Pages
{
    [Authorize(Policy = "AdminOnly")] // Only admins can access this page
    public class AdminPageModel : PageModel
    {
        private readonly UserManager _userManager;
        private readonly ReservationManager _reservationManager;

        public List<User> Customers { get; set; } // List of all customers
        public Dictionary<int, List<Reservation>> CustomerReservations { get; set; } // Reservations per customer

        public AdminPageModel(UserManager userManager, ReservationManager reservationManager)
        {
            _userManager = userManager;
            _reservationManager = reservationManager;
        }

        public void OnGet()
        {
            
            Customers = _userManager.GetUsersByRole("Customer");

            // Fetch reservations for each customer
            CustomerReservations = new Dictionary<int, List<Reservation>>();
            foreach (var customer in Customers)
            {
                var reservations = _reservationManager.GetReservationsByCustomerId(customer.User_Id);
                CustomerReservations[customer.User_Id] = reservations;
            }
        }
    }
}
