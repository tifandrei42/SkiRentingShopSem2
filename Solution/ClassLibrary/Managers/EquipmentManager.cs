using BusinessLogic.Interfaces;
using ClassLibrary.DataAccess;
using ClassLibrary.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Managers
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

    }
}
