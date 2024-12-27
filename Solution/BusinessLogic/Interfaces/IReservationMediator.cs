using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IReservationMediator
    {
        void CreateReservation(Reservation reservation);

        void UpdateReservationStatus(int reservationId, string newStatus);

        List<Reservation> GetReservationsByCustomerId(int customerId);

        Reservation? GetReservationById(int reservationId);

        List<Reservation> GetReservations();

        List<Reservation> GetReservationsByStatus(string status);

        void UpdateGroupStatus(int customerId, DateTime reservationDate, string newStatus);

        List<ReservationGroup> GetGroupedReservations();
    }
}
