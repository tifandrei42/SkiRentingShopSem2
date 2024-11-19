using ClassLibrary.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Managers
{
    public class ReservationManager
    {
        private readonly ReservationMediator _reservationMediator;
        private readonly StockManager _stockManager;

        public ReservationManager()
        {
            _reservationMediator = new ReservationMediator();
            _stockManager = new StockManager();
        }

        public bool CreateReservation(Reservation reservation)
        {
            var stock = _stockManager.GetStockByEquipmentId(reservation.EquipmentId);

            if (stock.Quantity <= 0)
            {
                throw new InvalidOperationException("Insufficient stock for this equipment.");
            }

            // Deduct stock
            _stockManager.UpdateStock(reservation.EquipmentId, -1);

            // Save reservation
            _reservationMediator.CreateReservation(reservation);

            return true;
        }
    }
}
