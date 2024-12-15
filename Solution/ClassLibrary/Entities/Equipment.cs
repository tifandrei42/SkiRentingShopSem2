using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class Equipment
    {
        public int CategoryId
        {
            get; set;
        }

        public int EquipmentId { get; set; }
        public required string? Name { get; set; }
        public required string? Brand { get; set; }
        public string? Size { get; set; }
        public decimal PricePerDay { get; set; }
        public EquipmentCategory Category
        {
            get => (EquipmentCategory)CategoryId; 
            set => CategoryId = (int)value;       
        }
        public string? ImagePath { get; set; }
        public int Quantity { get; set; }

        public Equipment() { }

        public Equipment(int equipmentId, string name, string brand, string size, decimal pricePerDay, EquipmentCategory category, string imagePath, int quantity)
        {
            EquipmentId = equipmentId;
            Name = name;
            Brand = brand;
            Size = size;
            PricePerDay = pricePerDay;
            Category = category;
            ImagePath = imagePath;
            Quantity = quantity;
        }

        public Equipment(string name, string brand, string size, decimal pricePerDay, EquipmentCategory equipmentCategory, string imagePath, int quantity)
        {
            Name = name;
            Brand = brand;
            Size = size;
            PricePerDay = pricePerDay;
            Category = equipmentCategory;
            ImagePath = imagePath;
            Quantity = quantity;
        }
    }
}
