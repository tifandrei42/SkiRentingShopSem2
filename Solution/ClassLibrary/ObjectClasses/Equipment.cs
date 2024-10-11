using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.ObjectClasses
{
    public class Equipment
    {
        public int EquipmentId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Size { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
        public string EquipmentType { get; set; }

        public bool CheckAvailability(DateTime startDate, DateTime endDate)
        {
            return true;
        }

        public decimal CalculateRentalPrice(int days)
        {
            return PricePerDay * days;
        }
    }
}
