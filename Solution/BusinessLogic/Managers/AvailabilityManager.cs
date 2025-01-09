using BusinessLogic.DataAccess;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLogic.Managers
{
    public class AvailabilityManager
    {
        private ReservationManager _reservationManager;

        public AvailabilityManager(ReservationManager reservationManager) 
        {
            _reservationManager = reservationManager ?? throw new ArgumentNullException(nameof(reservationManager));
        }

        public List<DateTime> GetUnavailableDates(Equipment equipment, List<Reservation> basket, int qt)
        {
            List<DateTime> unavailableDates = new List<DateTime>();

            // Get all interesting dates
            List<DateTime> interestingDates = GetInteresingDates(basket);

            // Verify Dates
            foreach (var date in interestingDates)
            {
                // Check if the equipment is unavailable on this date
                if (!CheckAvailability(date, equipment, basket, qt)) // If not available
                {
                    unavailableDates.Add(date); // Add to unavailable dates
                }
            }

            return unavailableDates;
        }

        private List<DateTime> GetInteresingDates(List<Reservation> basket)
        {
            // Get existing reservations
            List<Reservation> existingReservations = _reservationManager.GetReservations();

            // Extract dates from both lists
            List<DateTime> existingDates = GetDates(existingReservations);
            List<DateTime> basketDates = GetDates(basket);

            // Union ensures no duplicates
            return existingDates.Union(basketDates).ToList();
        }

        private List<DateTime> GetDates(List<Reservation> existingReservations)
        {
            List<DateTime> reservationDates = new List<DateTime>();

            foreach (Reservation reservation in existingReservations) 
            {
                reservationDates.Add(reservation.ReservationDate.Date);
            }
            return reservationDates;
        }

        public bool CheckAvailability(DateTime reservationDate, Equipment equipment, List<Reservation> basket, int qt)
        {
            var currentCount = GetCurrentReservationCount(reservationDate, equipment)
                             + GetBasketCount(reservationDate, equipment, basket);

            var maximumCount = equipment.Quantity;

            if (currentCount + qt > maximumCount)
            {
                return false;
            }
            return true;
        }

        private int GetBasketCount(DateTime reservationDate, Equipment equipment, List<Reservation> basket)
        {
            if (basket == null || basket.Count == 0)
                return 0;

            return basket
                .Where(r =>
                    r.Equipment.EquipmentId == equipment.EquipmentId &&
                    r.ReservationDate.Date == reservationDate.Date &&
                    r.Status != Enums.Status.Canceled)
                .Sum(r => r.Quantity);
        }

        private int GetCurrentReservationCount(DateTime reservationDate, Equipment equipment)
        {
            // Get only reservations for this date 
            List<Reservation> reservations = _reservationManager.GetReservations();

            return reservations
                .Where(r =>
                    r.Equipment.EquipmentId == equipment.EquipmentId &&
                    r.ReservationDate.Date == reservationDate.Date &&
                    r.Status != Enums.Status.Canceled)
                .Sum(r => r.Quantity);
        }
    }
}
