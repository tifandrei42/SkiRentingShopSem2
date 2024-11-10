using ClassLibrary.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IEquipmentMediator
    {
        void CreateEquipment(Equipment equipment);
        Equipment GetEquipmentById(int equipmentId);
        List<Equipment> GetAllEquipment();
        void UpdateEquipment(Equipment equipment);
        void DeleteEquipment(int equipmentId);
    }
}
