using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using BusinessLogic.Managers;
using BusinessLogic.Entities;

namespace Renting_Website.Pages
{
    public class ReserveEquipmentModel : PageModel
    {
        private readonly ReservationManager _reservationManager;
        private readonly EquipmentManager _equipmentManager;

        public ReserveEquipmentModel(EquipmentManager equipmentManager)
        {
            _reservationManager = new ReservationManager();
            _equipmentManager = equipmentManager;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }  

        [BindProperty]
        public DateTime ReservationDate { get; set; }
        public Equipment Equipment { get; set; }

        public IActionResult OnGet()
        {
            Equipment = _equipmentManager.GetEquipmentById(Id);
            if (Equipment == null)
            {
                return RedirectToPage("/Error", new { errorMessage = "The requested equipment was not found." });
            }

            ReservationDate = DateTime.Today;

            return Page();
        }

        public IActionResult OnPost()
        {
            Equipment = _equipmentManager.GetEquipmentById(Id); 

            var userId = User.FindFirst("UserId")?.Value;

            if (Equipment == null)
            {
                return RedirectToPage("/Error", new { errorMessage = "The requested equipment was not found." });
            }

            if (userId == null)
            {
                return Unauthorized();
            }
            int customerId = int.Parse(userId);

            return Page();
            //try
            //{
            //    //var reservation = new Reservation
            //    //{
            //    //    EquipmentId = Id,
            //    //    CustomerId = customerId,
            //    //    CreationDate = c,
            //    //    Status = "Pending"
            //    //};

            //    //bool success = _reservationManager.CreateReservation(reservation);

            //    //if (!success)
            //    //{
            //    //    ModelState.AddModelError(string.Empty, "The equipment is not available for the selected dates.");
            //    //    return Page();
            //    //}

            //    return RedirectToPage("/Reservations", new { reservationId = reservation.ReservationId });
            //}
            //catch (Exception ex)
            //{
            //    ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
            //    return Page();
            //}
        }
    }
}
