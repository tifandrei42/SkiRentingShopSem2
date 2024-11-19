using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.ObjectClasses
{
    public class Reservation
    {
        private int reservationId;
        private int equipmentId;
        private int customerId;
        private DateTime reservationDate;
        private DateTime startDate;
        private DateTime endDate;
        private decimal totalPrice;
        private string status;

        public int ReservationId
        {
            get => reservationId;
            set => reservationId = value;
        }

        public int EquipmentId
        {
            get => equipmentId;
            set => equipmentId = value;
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

        public DateTime StartDate
        {
            get => startDate;
            set => startDate = value;
        }

        public DateTime EndDate
        {
            get => endDate;
            set => endDate = value;
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
            reservationDate = DateTime.Now;
            status = "Pending";
        }

        public Reservation(int reservationId, int equipmentId, int customerId, DateTime startDate, DateTime endDate, decimal totalPrice)
        {
            this.reservationId = reservationId;
            this.equipmentId = equipmentId;
            this.customerId = customerId;
            this.reservationDate = DateTime.Now;
            this.startDate = startDate;
            this.endDate = endDate;
            this.totalPrice = totalPrice;
            this.status = "Pending";
        }

        public Reservation(int reservationId, int equipmentId, int customerId, DateTime reservationDate, DateTime startDate, DateTime endDate, decimal totalPrice, string status)
        {
            this.reservationId = reservationId;
            this.equipmentId = equipmentId;
            this.customerId = customerId;
            this.reservationDate = reservationDate;
            this.startDate = startDate;
            this.endDate = endDate;
            this.totalPrice = totalPrice;
            this.status = status;
        }
    }
}
