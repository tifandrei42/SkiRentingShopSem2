using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Entities;
using BusinessLogic.Managers;

namespace Renting_Website.Pages
{
    public class BasketModel : PageModel
    {
        public List<BasketEquipment> Basket { get; set; } = new();
        public ReservationManager reservationManager;

        public BasketModel() 
        {
            reservationManager = new ReservationManager();
        }
        
        public void OnGet()
        {
            var basketJson = HttpContext.Session.GetString("Basket");
            if (!string.IsNullOrEmpty(basketJson))
            {
                Basket = JsonSerializer.Deserialize<List<BasketEquipment>>(basketJson);
            }
        }

        public IActionResult OnPostRemove(int id)
        {
            var basketJson = HttpContext.Session.GetString("Basket");
            if (!string.IsNullOrEmpty(basketJson))
            {
                var basket = JsonSerializer.Deserialize<List<BasketEquipment>>(basketJson);
                basket.RemoveAll(item => item.EquipmentId == id);
                HttpContext.Session.SetString("Basket", JsonSerializer.Serialize(basket));
            }

            return RedirectToPage();
        }

        public IActionResult OnPostClear()
        {
            HttpContext.Session.Remove("Basket");
            return RedirectToPage();
        }

        public IActionResult OnPostConfirm()
        {
            var basketJson = HttpContext.Session.GetString("Basket");
            if (!string.IsNullOrEmpty(basketJson))
            {
                var basket = JsonSerializer.Deserialize<List<BasketEquipment>>(basketJson);
                

                foreach (var item in basket)
                {
                    reservationManager.CreateReservation(new Reservation
                    {
                        Equipment = item.EquipmentId,
                        CustomerId = 1, // Replace with actual customer ID
                        ReservationDate = item.Date,
                        Status = "Pending"
                    });
                }

                HttpContext.Session.Remove("Basket");
            }

            return RedirectToPage("/Reservations");
        }
    }
}

