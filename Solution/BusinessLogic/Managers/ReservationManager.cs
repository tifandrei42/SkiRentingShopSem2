using BusinessLogic.DataAccess;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using System;
using System.Collections.Generic;

namespace BusinessLogic.Managers
{
    public class ReservationManager
    {
        private readonly ReservationMediator _reservationMediator;

        public ReservationManager()
        {
            _reservationMediator = new ReservationMediator();
        }

        public bool CreateReservation(Reservation reservation)
        {

            _reservationMediator.CreateReservation(reservation);

            return true; 
        }


        public void CancelReservation(int reservationId)
        {
            _reservationMediator.UpdateReservationStatus(reservationId, Status.Canceled.ToString());
        }

        public List<Reservation> GetReservationsByCustomerId(int customerId)
        {
            return _reservationMediator.GetReservationsByCustomerId(customerId);
        }
        public Reservation? GetReservationById(int reservationId)
        {
            return _reservationMediator.GetReservationById(reservationId);
        }

        public List<Reservation> GetReservations()
        {
            return _reservationMediator.GetReservations();
        }
    }
}
