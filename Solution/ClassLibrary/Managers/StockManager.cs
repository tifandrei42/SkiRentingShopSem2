using BusinessLogic.DataAccess;
using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Managers
{
    public class StockManager
    {
        private readonly StockMediator _stockMediator;

        public StockManager()
        {
            _stockMediator = new StockMediator();
        }

        public void UpdateStock(int equipmentId, int quantity)
        {
            _stockMediator.UpdateStock(equipmentId, quantity);
        }

        public Stock GetStockByEquipmentId(int equipmentId)
        {
            return _stockMediator.GetStockByEquipmentId(equipmentId);
        }

        public int GetNumberOfStockByEquipmentId(int equipmentId)
        {
            return _stockMediator.GetNumberOfStockByEquipmentId(equipmentId);
        }
    }
}
