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
        public bool UpdateStock(int equipmentId, int quantity)
        {
            string query = "UPDATE Stock SET Quantity = @Quantity, LastUpdated = @LastUpdated WHERE Equipment_Id = @EquipmentId";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@EquipmentId", equipmentId);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@LastUpdated", DateTime.Now);

                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; 
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in UpdateStock: {ex.Message}");
                throw; 
            }
            finally
            {
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
        public List<Stock> GetAllStock()
        {
            List<Stock> stockList = new List<Stock>();
            string query = "SELECT s.Equipment_Id, s.Quantity, s.LastUpdated FROM Stock s INNER JOIN Equipment e ON s.Equipment_Id = e.Equipment_Id";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stockList.Add(new Stock
                            {
                                EquipmentId = (int)reader["Equipment_Id"],
                                Quantity = (int)reader["Quantity"],
                                LastUpdated = (DateTime)reader["LastUpdated"]
                            });
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in GetAllStock: {ex.Message}");
                throw;
            }
            finally
            {
                connection.Close();
            }

            return stockList;
        }

        public List<dynamic> GetAllEquipmentWithStock()
        {
            List<dynamic> equipmentWithStock = new List<dynamic>();
            string query = @"
        SELECT e.Equipment_Id, e.Name, e.Brand, e.PricePerDay, e.EquipmentType, s.Quantity
        FROM Equipment e
        INNER JOIN Stock s ON e.Equipment_Id = s.Equipment_Id";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            equipmentWithStock.Add(new
                            {
                                EquipmentId = (int)reader["Equipment_Id"],
                                Name = reader["Name"].ToString(),
                                Brand = reader["Brand"].ToString(),
                                PricePerDay = (decimal)reader["PricePerDay"],
                                EquipmentType = reader["EquipmentType"].ToString(),
                                Quantity = (int)reader["Quantity"]
                            });
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in GetAllEquipmentWithStock: {ex.Message}");
                throw;
            }
            finally
            {
                connection.Close();
            }

            return equipmentWithStock;
        }
    }
}
