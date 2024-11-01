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
        public EquipmentManager() 
        {

        }
        public List<Equipment> GetAllEquipment()
        {
            
            return new EquipmentMediator().GetAllEquipment();
        }


    }
}
