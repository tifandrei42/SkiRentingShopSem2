using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using BusinessLogic.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Managers
{
    public class EquipmentManager
    {
        private readonly IEquipmentMediator _equipmentMediator;

        public EquipmentManager(IEquipmentMediator equipmentMediator)
        {
            _equipmentMediator = equipmentMediator;
        }

        public void AddEquipment(Equipment equipment)
        {
            if (string.IsNullOrWhiteSpace(equipment.Name))
            {
                throw new ArgumentException("Equipment name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(equipment.Brand))
            {
                throw new ArgumentException("Equipment brand is required.", nameof(equipment));
            }

            if (equipment.CategoryId <= 0)
            {
                throw new ArgumentException("Invalid category selected.");
            }

            _equipmentMediator.CreateEquipment(equipment);
        }

        public Equipment GetEquipmentById(int equipmentId)
        {
            return _equipmentMediator.GetEquipmentById(equipmentId);
        }

        public List<Equipment> GetAllEquipment()
        {
            return _equipmentMediator.GetAllEquipment();
        }

        public void UpdateEquipment(Equipment equipment)
        {
            var existingEquipment = _equipmentMediator.GetEquipmentById(equipment.EquipmentId);
            if (existingEquipment == null)
            {
                throw new KeyNotFoundException($"Equipment with ID {equipment.EquipmentId} does not exist.");
            }

            if (equipment.EquipmentId <= 0)
            {
                throw new ArgumentException("Invalid Equipment ID.");
            }

            if (string.IsNullOrWhiteSpace(equipment.Name))
            {
                throw new ArgumentException("Equipment name cannot be empty.");
            }

            if (equipment.CategoryId <= 0)
            {
                throw new ArgumentException("Invalid category selected.");
            }

            _equipmentMediator.UpdateEquipment(equipment);
        }

        public void DeleteEquipment(int equipmentId)
        {
            var equipment = _equipmentMediator.GetEquipmentById(equipmentId);
            if (equipment == null)
                throw new KeyNotFoundException($"Equipment with ID {equipmentId} does not exist.");

            _equipmentMediator.DeleteEquipment(equipmentId);
        }

        public List<Equipment> GetFilteredEquipment(string brandFilter, string nameFilter, decimal? minPrice, decimal? maxPrice)
        {
            var allEquipment = _equipmentMediator.GetAllEquipment();

            if (!string.IsNullOrEmpty(brandFilter))
            {
                allEquipment = allEquipment.Where(e => e.Brand.Contains(brandFilter, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(nameFilter))
            {
                allEquipment = allEquipment.Where(e => e.Name.Contains(nameFilter, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (minPrice.HasValue)
            {
                allEquipment = allEquipment.Where(e => e.PricePerDay >= minPrice.Value).ToList();
            }

            if (maxPrice.HasValue)
            {
                allEquipment = allEquipment.Where(e => e.PricePerDay <= maxPrice.Value).ToList();
            }

            return allEquipment;
        }

        public bool IsDuplicateEquipment(string name, string imagePath)
        {
            return _equipmentMediator.IsDuplicateEquipment(name, imagePath);
        }

    }
}
