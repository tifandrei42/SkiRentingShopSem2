using BusinessLogic.Entities;
using BusinessLogic.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Renting_Website.Pages
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminPageModel : PageModel
    {
        private readonly UserManager _userManager;
        private readonly ReservationManager _reservationManager;

        public List<User> Customers { get; set; } // List of customers
        public Dictionary<int, CustomerSummary> CustomerSummaries { get; set; } // Customer summary data

        public AdminPageModel(UserManager userManager, ReservationManager reservationManager)
        {
            _userManager = userManager;
            _reservationManager = reservationManager;
        }

        public void OnGet()
        {
            Customers = _userManager.GetUsersByRole("Customer");

            CustomerSummaries = new Dictionary<int, CustomerSummary>();
            foreach (var customer in Customers)
            {
                var reservations = _reservationManager.GetReservationsByCustomerId(customer.User_Id);

                var finishedCount = reservations.Count(r => r.Status.ToString() == "Finished");
                var pendingCount = reservations.Count(r => r.Status.ToString() == "Pending");
                var totalPrice = reservations.Sum(r => r.TotalPrice);

                CustomerSummaries[customer.User_Id] = new CustomerSummary
                {
                    FinishedCount = finishedCount,
                    PendingCount = pendingCount,
                    TotalPrice = totalPrice
                };
            }
        }
    }
}
