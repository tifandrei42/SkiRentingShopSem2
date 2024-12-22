using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class BasketEquipment
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public decimal PricePerDay { get; set; }
    }
}
