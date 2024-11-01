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
        public required string Name { get; set; }
        public required string Brand { get; set; }
        public string Size { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }
        public required string EquipmentType { get; set; }
        public string? ImagePath { get; set; }

    }
}
