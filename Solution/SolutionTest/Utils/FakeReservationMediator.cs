using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionTest.Utils
{
    public class FakeReservationMediator : IReservationMediator
    {
        private readonly List<Reservation> _reservations = new();

        public void CreateReservation(Reservation reservation)
        {
            reservation.ReservationId = _reservations.Count + 1;
            _reservations.Add(reservation);
        }

        public void UpdateReservationStatus(int reservationId, string newStatus)
        {
            var reservation = _reservations.FirstOrDefault(r => r.ReservationId == reservationId);
            if (reservation != null)
            {
                reservation.Status = Enum.Parse<Status>(newStatus);
            }
        }

        public List<Reservation> GetReservationsByCustomerId(int customerId)
        {
            return _reservations.Where(r => r.CustomerId == customerId).ToList();
        }

        public Reservation? GetReservationById(int reservationId)
        {
            return _reservations.FirstOrDefault(r => r.ReservationId == reservationId);
        }

        public List<Reservation> GetReservations()
        {
            return _reservations.ToList();
        }

        public List<Reservation> GetReservationsByStatus(string status)
        {
            return _reservations.Where(r => r.Status.ToString() == status).ToList();
        }

        public void UpdateGroupStatus(int customerId, DateTime reservationDate, string newStatus)
        {
            foreach (var reservation in _reservations.Where(r => r.CustomerId == customerId && r.ReservationDate.Date == reservationDate.Date))
            {
                reservation.Status = Enum.Parse<Status>(newStatus);
            }
        }

        public List<ReservationGroup> GetGroupedReservations()
        {
            return _reservations
                .GroupBy(r => new { r.CustomerId, r.ReservationDate })
                .Select(g => new ReservationGroup
                {
                    CustomerId = g.Key.CustomerId,
                    ReservationDate = g.Key.ReservationDate,
                    Reservations = g.ToList()
                })
                .ToList();
        }
    }
}
