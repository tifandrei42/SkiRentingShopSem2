using BusinessLogic.Enums;
using System;

namespace BusinessLogic.Entities
{
    public class Reservation
    {
        private int reservationId;
        private Equipment equipment;
        private int customerId;
        private DateTime reservationDate;
        private DateTime creationDate;
        private decimal totalPrice;
        private Status status;
        private int quantity; 

        public int ReservationId
        {
            get => reservationId;
            set => reservationId = value;
        }

        public Equipment Equipment
        {
            get => equipment;
            set => equipment = value;
        }

        public int CustomerId
        {
            get => customerId;
            set => customerId = value;
        }

        public DateTime ReservationDate
        {
            get => reservationDate;
            set => reservationDate = value;
        }

        public DateTime CreationDate
        {
            get => creationDate;
            set => creationDate = value;
        }

        public decimal TotalPrice
        {
            get => totalPrice;
            set => totalPrice = value;
        }

        public Status Status
        {
            get => status;
            set => status = value;
        }

        public int Quantity 
        {
            get => quantity;
            set => quantity = value;
        }

        public Reservation()
        {
        }

        public Reservation(int reservationId, Equipment equipment, int customerId, DateTime reservationDate, decimal totalPrice, int quantity)
        {
            this.reservationId = reservationId;
            this.equipment = equipment;
            this.customerId = customerId;
            this.creationDate = DateTime.Now;
            this.reservationDate = reservationDate;
            this.totalPrice = totalPrice;
            this.quantity = quantity; 
            this.status = Status.Pending;
        }
    }
}
