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
        private StockMediator _stockMediator;

        public EquipmentManager(IEquipmentMediator equipmentMediator)
        {
            _equipmentMediator = equipmentMediator;
            _stockMediator = new StockMediator();
        }

        public void AddEquipment(Equipment equipment)
        {
            if (string.IsNullOrWhiteSpace(equipment.Name))
            {
                throw new ArgumentException("Equipment name cannot be empty.");
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
            _equipmentMediator.UpdateEquipment(equipment);
        }

        public void DeleteEquipment(int equipmentId)
        {
            _equipmentMediator.DeleteEquipment(equipmentId);
        }
        public List<dynamic> GetAllEquipmentWithStock()
        {
            return _stockMediator.GetAllEquipmentWithStock();
        }

    }
}
