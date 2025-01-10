using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class Basket
    {
        private List<Reservation> reservations;

        public Basket()
        {
            reservations = new List<Reservation>();
        }
    }
}
