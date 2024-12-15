using BusinessLogic.DataAccess;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public bool CheckAvailability(DateTime reservationDate, Equipment equipment)
        {
            var currentCount = GetCurrentReservationCount(reservationDate, equipment);
            var maximumCount = equipment.Quantity;

            if (currentCount >= maximumCount) 
            {
                return false;
            }
            return true;
        }

        private int GetCurrentReservationCount(DateTime reservationDate, Equipment equipment)
        {
            // Get only reservations for this date 
            List<Reservation> reservations = GetReservationsByDate(reservationDate);

            int count = 0;

            foreach (Reservation reservation in reservations)
            {
                //Filter by reservationId
                List<Tuple<int,int,int>> reservationEquipment = reservationManager.GetReservationEquipmentByReservationId(reservation.ReservationId);

                foreach (Tuple<int, int, int> element in reservationEquipment) 
                {
                    int rId = element.Item1;
                    int eId = element.Item2;
                    int qt = element.Item3;

                    //Filter by equipment
                    if (eId == equipment.EquipmentId) 
                    {
                        count += qt;
                    }
                }
            }
            return count;
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
