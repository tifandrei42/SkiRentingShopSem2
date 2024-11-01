using ClassLibrary.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DataAccess
{
    public class EquipmentMediator: DbAccess
    {
        public EquipmentMediator(): base() { }


        public List<Equipment> GetAllEquipment()
        {
            List<Equipment> equipmentList = new List<Equipment>();

            using (SqlConnection connection = new SqlConnection("YourConnectionString"))
            {
                string query = "SELECT EquipmentId, Name, Brand, Size, PricePerDay, IsAvailable, EquipmentType, ImagePath FROM Equipment";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Equipment equipment = new Equipment
                        {
                            EquipmentId = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Brand = reader.GetString(2),
                            Size = reader.GetString(3),
                            PricePerDay = reader.GetDecimal(4),
                            IsAvailable = reader.GetBoolean(5),
                            EquipmentType = reader.GetString(6),
                            ImagePath = reader.IsDBNull(7) ? null : reader.GetString(7) // Get ImagePath
                        };
                        equipmentList.Add(equipment);
                    }
                }
            }

            return equipmentList;
        }

    }
}
