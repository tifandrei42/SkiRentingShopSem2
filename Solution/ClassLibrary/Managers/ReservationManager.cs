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

        public bool CreateReservation(Reservation reservation, List<Tuple<int, int>> equipmentList)
        {
            decimal totalPrice = 0;

            foreach (var item in equipmentList)
            {
                int equipmentId = item.Item1;
                int quantity = item.Item2;

                Equipment equipment = _equipmentMediator.GetEquipmentById(equipmentId);

                if (equipment == null)
                {
                    throw new Exception($"Equipment with ID {equipmentId} not found.");
                }

                // Calculate price (for a single day)
                totalPrice += equipment.PricePerDay * quantity;
            }

            reservation.TotalPrice = totalPrice;

            _reservationMediator.CreateReservation(reservation);

            // Get the reservation ID
            int reservationId = _reservationMediator.GetLastInsertedReservationId();

            // Link equipment to reservation
            foreach (var item in equipmentList)
            {
                int equipmentId = item.Item1;
                int quantity = item.Item2;

                _reservationMediator.AddReservationEquipment(reservationId, equipmentId, quantity);
            }

            return true; // Reservation created
        }


        public void CancelReservation(int reservationId)
        {
            _reservationMediator.UpdateReservationStatus(reservationId, "Cancelled");
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

        public List<Tuple<int,int,int>> GetReservationEquipmentByReservationId(int reservationId)
        {
            return _reservationMediator.GetReservationEquipmentByReservationId(reservationId);
        }
    }
}
