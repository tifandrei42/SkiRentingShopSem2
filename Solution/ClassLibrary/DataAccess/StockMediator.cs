using BusinessLogic.Entities;
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
        public StockMediator() : base() { }
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

        public int GetNumberOfStockByEquipmentId(int equipmentId)
        {
            if (connection == null)
            {
                throw new InvalidOperationException("Database connection is not initialized.");
            }

            int stock = 0;
            string query = "SELECT Quantity FROM Stock WHERE Equipment_Id = @EquipmentId";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@EquipmentId", equipmentId);

                    connection.Open();
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        stock = Convert.ToInt32(result);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw;
            }
            finally
            {
                connection.Close();
            }

            return stock;
        }

    }
}
