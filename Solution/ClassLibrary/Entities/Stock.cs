using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Entities
{
    public class Stock
    {
        private int stockId;
        private int equipmentId;
        private int quantity;
        private DateTime lastUpdated;

        public int StockId { get; set; }
        public int EquipmentId { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }

        public Stock(int stockId, int equipmentId, int quantity, DateTime lastUpdated) 
        {
            this.stockId = stockId;
            this.equipmentId = equipmentId;
            this.quantity = quantity;
            this.lastUpdated = lastUpdated;
        }
        public Stock() { }
    }
}
