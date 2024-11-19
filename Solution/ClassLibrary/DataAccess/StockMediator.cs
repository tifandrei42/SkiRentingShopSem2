using BusinessLogic.Entities;
using ClassLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataAccess
{
    public class StockMediator : DbAccess
    {
        public void UpdateStock(int equipmentId, int quantity)
        {
            string query = @"
                UPDATE Stock
                SET Quantity = Quantity + @Quantity, LastUpdated = GETDATE()
                WHERE Equipment_Id = @EquipmentId";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@EquipmentId", equipmentId);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public Stock GetStockByEquipmentId(int equipmentId)
        {
            Stock? stock = null;
            string query = "SELECT * FROM Stock WHERE Equipment_Id = @EquipmentId";

            using (SqlCommand cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@EquipmentId", equipmentId);

                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        stock = new Stock
                        {
                            StockId = (int)reader["Stock_Id"],
                            EquipmentId = (int)reader["Equipment_Id"],
                            Quantity = (int)reader["Quantity"],
                            LastUpdated = (DateTime)reader["LastUpdated"]
                        };
                    }
                }
                connection.Close();
            }

            if (stock == null)
            {
                throw new InvalidOperationException($"No stock found for Equipment ID {equipmentId}");
            }

            return stock;
        }
    }
}
