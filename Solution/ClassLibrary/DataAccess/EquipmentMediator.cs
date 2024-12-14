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
                INSERT INTO Equipment (Name, Brand, Size, PricePerDay, Category_Id, ImagePath)
                VALUES (@Name, @Brand, @Size, @PricePerDay, @CategoryId, @ImagePath);
                SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", equipment.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Brand", equipment.Brand ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Size", equipment.Size ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PricePerDay", equipment.PricePerDay);
                    cmd.Parameters.AddWithValue("@CategoryId", (int)equipment.Category); // Store enum as an integer referencing Category_Id
                    cmd.Parameters.AddWithValue("@ImagePath", equipment.ImagePath ?? (object)DBNull.Value);

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
            SELECT e.Equipment_Id, e.Name, e.Brand, e.Size, e.PricePerDay, e.Category_Id, c.CategoryName, e.ImagePath
            FROM Equipment e
            INNER JOIN Category c ON e.Category_Id = c.Category_Id
            WHERE e.Equipment_Id = @Id";

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
                            Name = reader["Name"].ToString(),
                            Brand = reader["Brand"].ToString(),
                            Size = reader["Size"].ToString(),
                            PricePerDay = (decimal)reader["PricePerDay"],
                            Category = (EquipmentCategory)(int)reader["Category_Id"], // Map Category_Id to the enum
                            ImagePath = reader["ImagePath"].ToString()
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
            SELECT e.Equipment_Id, e.Name, e.Brand, e.Size, e.PricePerDay, e.Category_Id, c.CategoryName, e.ImagePath
            FROM Equipment e
            INNER JOIN Category c ON e.Category_Id = c.Category_Id";

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Equipment equipment = new Equipment
                            {
                                EquipmentId = (int)reader["Equipment_Id"],
                                Name = reader["Name"].ToString(),
                                Brand = reader["Brand"].ToString(),
                                Size = reader["Size"].ToString(),
                                PricePerDay = (decimal)reader["PricePerDay"],
                                Category = (EquipmentCategory)(int)reader["Category_Id"], // Map Category_Id to the enum
                                ImagePath = reader["ImagePath"].ToString()
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
            catch (Exception ex)
            {
                Console.WriteLine($"General Error in GetAllEquipment: {ex.Message}");
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
                    ImagePath = @ImagePath
                WHERE Equipment_Id = @EquipmentId";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", equipment.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Brand", equipment.Brand ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Size", equipment.Size ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PricePerDay", equipment.PricePerDay);
                    cmd.Parameters.AddWithValue("@CategoryId", (int)equipment.Category); // Store enum as integer
                    cmd.Parameters.AddWithValue("@ImagePath", equipment.ImagePath ?? (object)DBNull.Value);
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
    }
}
