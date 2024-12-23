using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class Basket
    {
        private List<Reservation> reservations;

        public Basket()
        {
            reservations = new List<Reservation>();
        }

        public void AddReservation(Reservation reservation)
        {
            reservations.Add(reservation);
        }

        public void RemoveReservation(int reservationId)
        {
            reservations.RemoveAll(r => r.ReservationId == reservationId);
        }

        public List<Reservation> GetReservations()
        {
            return reservations;
        }

        public int GetBasketCount(DateTime reservationDate, Equipment equipment)
        {
            return reservations.Count(r => r.ReservationDate.Date == reservationDate.Date &&
                                           r.Equipment.EquipmentId == equipment.EquipmentId);
        }

        public decimal CalculateTotalPrice()
        {
            return reservations.Sum(r => r.TotalPrice);
        }
    }
}
