using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private enum status;

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

        public string Status
        {
            get => status;
            set => status = value;
        }
        public Reservation()
        {
        }

        public Reservation(int reservationId, Equipment equipment, int customerId, DateTime reservationDate, decimal totalPrice)
        {
            this.reservationId = reservationId;
            this.equipment = equipment;
            this.customerId = customerId;
            creationDate = DateTime.Now;
            this.reservationDate = reservationDate;
            this.totalPrice = totalPrice;
            status = "Pending";
        }

        public Reservation(int reservationId, Equipment equipment, int customerId, DateTime reservationDate, DateTime creationDate, decimal totalPrice, string status)
        {
            this.reservationId = reservationId;
            this.equipment = equipment;
            this.customerId = customerId;
            this.reservationDate = reservationDate;
            this.creationDate = creationDate;
            this.totalPrice = totalPrice;
            this.status = status;
        }
    }
}
