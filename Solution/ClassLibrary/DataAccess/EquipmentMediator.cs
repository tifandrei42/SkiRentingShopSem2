using ClassLibrary.ObjectClasses;
using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DataAccess
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
                    // Execute the insert command and get the generated EquipmentId
                    int equipmentId = Convert.ToInt32(cmd.ExecuteScalar());
                    equipment.EquipmentId = equipmentId;  // Set the EquipmentId in the object
                }
            }
            catch (SqlException ex)
            {
                // Log or handle the exception
                Console.WriteLine($"SQL Error in CreateEquipment: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        // Read equipment by ID
        public Equipment GetEquipmentById(int equipmentId)
        {
            Equipment? equipment = null;
            try
            {
                string sql = @"
                SELECT EquipmentId, Name, Brand, Size, PricePerDay, EquipmentType, ImagePath
                FROM Equipment
                WHERE EquipmentId = @EquipmentId";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@EquipmentId", equipmentId);
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            equipment = new Equipment
                            {
                                EquipmentId = (int)reader["EquipmentId"],
                                Name = reader["Name"] as string,
                                Brand = reader["Brand"] as string,
                                Size = reader["Size"] as string,
                                PricePerDay = (decimal)reader["PricePerDay"],
                                EquipmentType = reader["EquipmentType"] as string,
                                ImagePath = reader["ImagePath"] as string
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in GetEquipmentById: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return equipment;
        }

        public List<Equipment> GetAllEquipment()
        {
            List<Equipment> equipmentList = new List<Equipment>();
            try
            {
                string sql = @"
                SELECT EquipmentId, Name, Brand, Size, PricePerDay, EquipmentType, ImagePath
                FROM Equipment";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Equipment equipment = new Equipment
                            {
                                EquipmentId = (int)reader["EquipmentId"],
                                Name = reader["Name"] as string,
                                Brand = reader["Brand"] as string,
                                Size = reader["Size"] as string,
                                PricePerDay = (decimal)reader["PricePerDay"],
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
