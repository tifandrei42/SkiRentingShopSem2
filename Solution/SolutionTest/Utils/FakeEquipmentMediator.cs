using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionTest.Utils
{
    public class FakeEquipmentMediator : IEquipmentMediator
    {
        private readonly Dictionary<int, Equipment> _equipmentData;
        private int _nextId;

        public FakeEquipmentMediator()
        {
            _equipmentData = new Dictionary<int, Equipment>();
            _nextId = 1;
        }

        public void CreateEquipment(Equipment equipment)
        {
            equipment.EquipmentId = _nextId++;
            _equipmentData.Add(equipment.EquipmentId, equipment);
        }

        public Equipment? GetEquipmentById(int equipmentId)
        {
           _equipmentData.TryGetValue(equipmentId, out Equipment equipment);
            return equipment;
        }

        public List<Equipment> GetAllEquipment()
        {
            return new List<Equipment>(_equipmentData.Values);
        }

        public void UpdateEquipment(Equipment equipment)
        {
            if (_equipmentData.ContainsKey(equipment.EquipmentId))
            {
                _equipmentData[equipment.EquipmentId] = equipment;
            }
        }

        public void DeleteEquipment(int equipmentId)
        {
            _equipmentData.Remove(equipmentId);
        }
        public bool IsDuplicateEquipment(string name, string imagePath)
        {
            throw new NotImplementedException();
        }
    }
}
