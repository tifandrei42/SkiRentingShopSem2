using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BusinessLogic.DataAccess
{
    public class EquipmentMediator : DbAccess, IEquipmentMediator
    {
        public EquipmentMediator() : base() { }

        public void CreateEquipment(Equipment equipment)
        {
            try
            {
                string sql = @"
                INSERT INTO Equipment (Name, Brand, Size, PricePerDay, Category_Id, ImagePath, Quantity)
                VALUES (@Name, @Brand, @Size, @PricePerDay, @CategoryId, @ImagePath, @Quantity);
                SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", equipment.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Brand", equipment.Brand ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Size", equipment.Size ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PricePerDay", equipment.PricePerDay);
                    cmd.Parameters.AddWithValue("@CategoryId", equipment.CategoryId);
                    cmd.Parameters.AddWithValue("@ImagePath", equipment.ImagePath ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Quantity", equipment.Quantity);

                    connection.Open();
                    int equipmentId = Convert.ToInt32(cmd.ExecuteScalar());
                    equipment.EquipmentId = equipmentId;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in CreateEquipment: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        public Equipment? GetEquipmentById(int id)
        {
            Equipment equipment = null;
            string query = @"
                            SELECT 
                e.Equipment_Id,
                e.Name,
                e.Brand,
                e.Size,
                e.PricePerDay,
                e.Category_Id,
                e.ImagePath,
                e.Quantity
            FROM 
                Equipment e
            WHERE 
                e.Equipment_Id = @Id";

            using (var cmd = new SqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        equipment = new Equipment
                        {
                            EquipmentId = (int)reader["Equipment_Id"],
                            Name = reader["Name"]?.ToString(),
                            Brand = reader["Brand"]?.ToString(),
                            Size = reader["Size"]?.ToString(),
                            PricePerDay = (decimal)reader["PricePerDay"],
                            Category = (EquipmentCategory)(int)reader["Category_Id"],
                            ImagePath = reader["ImagePath"]?.ToString(),
                            Quantity = (int)reader["Quantity"]
                        };
                    }
                }
                connection.Close();
            }
            return equipment;
        }

        public List<Equipment> GetAllEquipment()
        {
            List<Equipment> equipmentList = new List<Equipment>();
            string sql = @"
            SELECT 
                e.Equipment_Id, e.Name, e.Brand, e.Size, e.PricePerDay, e.Category_Id, c.CategoryName, e.ImagePath, e.Quantity
            FROM 
                Equipment e
            INNER JOIN 
                Category c ON e.Category_Id = c.Category_Id";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Equipment equipment = new()
                            {
                                EquipmentId = (int)reader["Equipment_Id"],
                                Name = reader["Name"].ToString(),
                                Brand = reader["Brand"].ToString(),
                                Size = reader["Size"].ToString(),
                                PricePerDay = (decimal)reader["PricePerDay"],
                                Category = (EquipmentCategory)(int)reader["Category_Id"],
                                ImagePath = reader["ImagePath"].ToString(),
                                Quantity = (int)reader["Quantity"]
                            };
                            equipmentList.Add(equipment);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in GetAllEquipment: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return equipmentList;
        }

        public void UpdateEquipment(Equipment equipment)
        {
            try
            {
                string sql = @"
                UPDATE Equipment
                SET Name = @Name,
                    Brand = @Brand,
                    Size = @Size,
                    PricePerDay = @PricePerDay,
                    Category_Id = @CategoryId,
                    ImagePath = @ImagePath,
                    Quantity = @Quantity
                WHERE Equipment_Id = @EquipmentId";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", equipment.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Brand", equipment.Brand ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Size", equipment.Size ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PricePerDay", equipment.PricePerDay);
                    cmd.Parameters.AddWithValue("@CategoryId", equipment.CategoryId);
                    cmd.Parameters.AddWithValue("@ImagePath", equipment.ImagePath ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Quantity", equipment.Quantity);
                    cmd.Parameters.AddWithValue("@EquipmentId", equipment.EquipmentId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in UpdateEquipment: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeleteEquipment(int equipmentId)
        {
            try
            {
                string sql = "DELETE FROM Equipment WHERE Equipment_Id = @EquipmentId";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@EquipmentId", equipmentId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in DeleteEquipment: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        public bool IsDuplicateEquipment(string name, string imagePath)
        {
            bool isDuplicate = false;
            string sql = @"
                SELECT COUNT(1) 
                FROM Equipment 
                WHERE Name = @Name AND ImagePath = @ImagePath";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@ImagePath", imagePath);

                    connection.Open();
                    int count = (int)cmd.ExecuteScalar();
                    isDuplicate = count > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in IsDuplicateEquipment: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return isDuplicate;
        }
    }
}
