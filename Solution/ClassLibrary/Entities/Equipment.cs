using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class Equipment
    {
        public int EquipmentId { get; set; }
        public required string? Name { get; set; }
        public required string? Brand { get; set; }
        public string? Size { get; set; }
        public decimal PricePerDay { get; set; }
        public required string? EquipmentType { get; set; }
        public string? ImagePath { get; set; }

        public Equipment() { }

        public Equipment(int equipmentId, string name, string brand, string size, decimal pricePerDay, string equipmentType, string imagePath)
        {
            EquipmentId = equipmentId;
            Name = name;
            Brand = brand;
            Size = size;
            PricePerDay = pricePerDay;
            EquipmentType = equipmentType;
            ImagePath = imagePath;
        }

        public Equipment(string name, string brand, string size, decimal pricePerDay, string equipmentType, string imagePath)
        {
            Name = name;
            Brand = brand;
            Size = size;
            PricePerDay = pricePerDay;
            EquipmentType = equipmentType;
            ImagePath = imagePath;
        }
    }
}
