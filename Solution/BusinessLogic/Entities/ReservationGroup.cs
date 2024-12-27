using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class ReservationGroup
    {
        public int CustomerId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int TotalReservations { get; set; }
        public decimal TotalPrice { get; set; }
        public string EquipmentNames { get; set; }
        public string Statuses { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
