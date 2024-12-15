using BusinessLogic.DataAccess;
using BusinessLogic.Entities;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Managers
{
    public class ReservationManager
    {
        private readonly ReservationMediator _reservationMediator;
        private readonly EquipmentMediator _equipmentMediator;

        public ReservationManager()
        {
            _reservationMediator = new ReservationMediator();
            _equipmentMediator = new EquipmentMediator();
        }

        public bool CreateReservation(Reservation reservation)
        {
            Equipment equipment = _equipmentMediator.GetEquipmentById(reservation.EquipmentId);
            if (equipment == null)
            {
                throw new Exception("Equipment not found.");
            }

            bool isAvailable = _reservationMediator.IsEquipmentAvailable(reservation.EquipmentId, reservation.StartDate, reservation.EndDate);
            if (!isAvailable)
            {
                return false;
            }

            int rentalDays = (reservation.EndDate - reservation.StartDate).Days;
            reservation.TotalPrice = rentalDays * equipment.PricePerDay;

            _reservationMediator.CreateReservation(reservation);
            return true;
        }

        public void CancelReservation(int reservationId)
        {
            _reservationMediator.UpdateReservationStatus(reservationId, "Cancelled");
        }

        public List<Reservation> GetReservationsByCustomerId(int customerId)
        {
            return _reservationMediator.GetReservationsByCustomerId(customerId);
        }
    }
}
