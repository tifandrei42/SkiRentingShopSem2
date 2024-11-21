using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Entities;

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
                INSERT INTO Equipment (Name, Brand, Size, PricePerDay, EquipmentType, ImagePath)
                VALUES (@Name, @Brand, @Size, @PricePerDay, @EquipmentType, @ImagePath);
                SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", equipment.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Brand", equipment.Brand ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Size", equipment.Size ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PricePerDay", equipment.PricePerDay);
                    cmd.Parameters.AddWithValue("@EquipmentType", equipment.EquipmentType ?? (object)DBNull.Value);
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

        public Equipment GetEquipmentById(int id)
        {
            Equipment equipment = null;
            string query = "SELECT * FROM Equipment WHERE Equipment_Id = @Id";

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
                            EquipmentType = reader["EquipmentType"].ToString(),
                            PricePerDay = (decimal)reader["PricePerDay"],
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
        SELECT Equipment_Id, Name, Brand, Size, PricePerDay, EquipmentType, ImagePath
        FROM Equipment";

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
                                EquipmentId = reader["Equipment_Id"] != DBNull.Value ? (int)reader["Equipment_Id"] : 0,
                                Name = reader["Name"] as string,
                                Brand = reader["Brand"] as string,
                                Size = reader["Size"] as string,
                                PricePerDay = reader["PricePerDay"] != DBNull.Value ? (decimal)reader["PricePerDay"] : 0m,
                                EquipmentType = reader["EquipmentType"] as string,
                                ImagePath = reader["ImagePath"] as string
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
                    EquipmentType = @EquipmentType,
                    ImagePath = @ImagePath
                WHERE EquipmentId = @EquipmentId";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", equipment.Name ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Brand", equipment.Brand ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Size", equipment.Size ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PricePerDay", equipment.PricePerDay);
                    cmd.Parameters.AddWithValue("@EquipmentType", equipment.EquipmentType ?? (object)DBNull.Value);
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
                string sql = @"
                DELETE FROM Equipment
                WHERE EquipmentId = @EquipmentId";

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
