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
        private readonly EquipmentMediator _equipmentMediator;

        public EquipmentManager()
        {
            _equipmentMediator = new EquipmentMediator();
        }

        public void AddEquipment(Equipment equipment)
        {
            _equipmentMediator.CreateEquipment(equipment);
        }

        public Equipment GetEquipment(int equipmentId)
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
