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
        public ReservationManager reservationManager;

        public AvailabilityManager() 
        {
            reservationManager = new ReservationManager();
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
            if (basket == null || !basket.Any())
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
            List<Reservation> reservations = reservationManager.GetReservations();

            return reservations
                .Where(r =>
                    r.Equipment.EquipmentId == equipment.EquipmentId &&
                    r.ReservationDate.Date == reservationDate.Date &&
                    r.Status != Enums.Status.Canceled)
                .Sum(r => r.Quantity);
        }

        private List<Reservation> GetReservationsByDate(DateTime reservationDate)
        {
            List<Reservation> input = reservationManager.GetReservations();
            List<Reservation> output = new List<Reservation>();

            foreach (Reservation reservation in input) 
            {
                if (reservation.ReservationDate == reservationDate)
                {
                    output.Add(reservation);
                }
            }
            return output;
        }
    }
}
