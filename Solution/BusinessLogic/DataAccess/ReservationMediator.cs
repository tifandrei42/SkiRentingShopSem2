using BusinessLogic.Entities;
using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BusinessLogic.DataAccess
{
    public class ReservationMediator : DbAccess
    {
        public ReservationMediator() : base() { }

        // Create Reservation with Quantity
        public void CreateReservation(Reservation reservation)
        {
            string query = @"
                INSERT INTO Reservation (Customer_Id, ReservationDate, TotalPrice, Status)
                VALUES (@CustomerId, @ReservationDate, @TotalPrice, @Status);
                SELECT SCOPE_IDENTITY();";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", reservation.CustomerId);
                    cmd.Parameters.AddWithValue("@ReservationDate", reservation.ReservationDate);
                    cmd.Parameters.AddWithValue("@TotalPrice", reservation.TotalPrice);
                    cmd.Parameters.AddWithValue("@Status", reservation.Status.ToString());

                    connection.Open();
                    int reservationId = Convert.ToInt32(cmd.ExecuteScalar());

                    AddReservationEquipment(reservationId, reservation.Equipment.EquipmentId, reservation.Quantity);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in CreateReservation: {ex.Message}");
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        // Get Reservation by ID
        public Reservation? GetReservationById(int reservationId)
        {
            Reservation? reservation = null;
            string query = @"
                SELECT 
                    r.Reservation_Id,
                    r.Customer_Id,
                    r.ReservationDate,
                    r.TotalPrice,
                    r.Status,
                    e.Equipment_Id,
                    e.Name AS EquipmentName,
                    e.Brand,
                    e.Size,
                    e.PricePerDay,
                    e.EquipmentType,
                    e.ImagePath,
                    e.Category,
                    re.Quantity
                FROM Reservation r
                INNER JOIN ReservationEquipment re ON r.Reservation_Id = re.Reservation_Id
                INNER JOIN Equipment e ON re.Equipment_Id = e.Equipment_Id
                WHERE r.Reservation_Id = @ReservationId";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ReservationId", reservationId);
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            reservation = new Reservation
                            {
                                ReservationId = (int)reader["Reservation_Id"],
                                CustomerId = (int)reader["Customer_Id"],
                                ReservationDate = (DateTime)reader["ReservationDate"],
                                TotalPrice = (decimal)reader["TotalPrice"],
                                Status = (Status)Enum.Parse(typeof(Status), reader["Status"].ToString()),
                                Quantity = (int)reader["Quantity"],

                                Equipment = new Equipment
                                {
                                    EquipmentId = (int)reader["Equipment_Id"],
                                    Name = reader["EquipmentName"].ToString(),
                                    Brand = reader["Brand"].ToString(),
                                    Size = reader["Size"].ToString(),
                                    PricePerDay = (decimal)reader["PricePerDay"],
                                    Category = (EquipmentCategory)Enum.Parse(typeof(EquipmentCategory), reader["Category"].ToString()),
                                    ImagePath = reader["ImagePath"].ToString()
                                }
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in GetReservationById: {ex.Message}");
                throw;
            }
            finally
            {
                connection.Close();
            }

            return reservation;
        }

        // Get Reservations by Customer ID
        public List<Reservation> GetReservationsByCustomerId(int customerId)
        {
            List<Reservation> reservations = new List<Reservation>();

            string query = @"
                SELECT 
                    r.Reservation_Id,
                    r.Customer_Id,
                    r.ReservationDate,
                    r.TotalPrice,
                    r.Status,
                    e.Equipment_Id,
                    e.Name AS EquipmentName,
                    e.Brand,
                    e.Size,
                    e.PricePerDay,
                    e.EquipmentType,
                    e.ImagePath,
                    e.Category,
                    re.Quantity
                FROM Reservation r
                INNER JOIN ReservationEquipment re ON r.Reservation_Id = re.Reservation_Id
                INNER JOIN Equipment e ON re.Equipment_Id = e.Equipment_Id
                WHERE r.Customer_Id = @CustomerId";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reservations.Add(new Reservation
                            {
                                ReservationId = (int)reader["Reservation_Id"],
                                CustomerId = (int)reader["Customer_Id"],
                                ReservationDate = (DateTime)reader["ReservationDate"],
                                TotalPrice = (decimal)reader["TotalPrice"],
                                Status = (Status)Enum.Parse(typeof(Status), reader["Status"].ToString()),
                                Quantity = (int)reader["Quantity"],

                                Equipment = new Equipment
                                {
                                    EquipmentId = (int)reader["Equipment_Id"],
                                    Name = reader["EquipmentName"].ToString(),
                                    Brand = reader["Brand"].ToString(),
                                    Size = reader["Size"].ToString(),
                                    PricePerDay = (decimal)reader["PricePerDay"],
                                    Category = (EquipmentCategory)Enum.Parse(typeof(EquipmentCategory), reader["Category"].ToString()),
                                    ImagePath = reader["ImagePath"].ToString()
                                }
                            });
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in GetReservationsByCustomerId: {ex.Message}");
                throw;
            }
            finally
            {
                connection.Close();
            }

            return reservations;
        }

        // Update Reservation Status
        public void UpdateReservationStatus(int reservationId, string newStatus)
        {
            string query = @"
                UPDATE Reservation
                SET Status = @Status
                WHERE Reservation_Id = @ReservationId";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Status", newStatus);
                    cmd.Parameters.AddWithValue("@ReservationId", reservationId);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in UpdateReservationStatus: {ex.Message}");
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        // Delete Reservation
        public void DeleteReservation(int reservationId)
        {
            string query = "DELETE FROM Reservation WHERE Reservation_Id = @ReservationId";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ReservationId", reservationId);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in DeleteReservation: {ex.Message}");
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        // Add Reservation Equipment
        public void AddReservationEquipment(int reservationId, int equipmentId, int quantity)
        {
            string query = @"
                INSERT INTO ReservationEquipment (Reservation_Id, Equipment_Id, Quantity)
                VALUES (@ReservationId, @EquipmentId, @Quantity)";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ReservationId", reservationId);
                    cmd.Parameters.AddWithValue("@EquipmentId", equipmentId);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in AddReservationEquipment: {ex.Message}");
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<Reservation> GetReservations()
        {
            List<Reservation> reservations = new List<Reservation>();

            string query = @"
        SELECT 
            r.Reservation_Id,
            r.Customer_Id,
            r.ReservationDate,
            r.TotalPrice,
            r.Status,
            e.Equipment_Id,
            e.Name AS EquipmentName,
            e.Brand,
            e.Size,
            e.PricePerDay,
            e.EquipmentType,
            e.ImagePath,
            e.Category,
            re.Quantity
        FROM Reservation r
        LEFT JOIN ReservationEquipment re ON r.Reservation_Id = re.Reservation_Id
        LEFT JOIN Equipment e ON re.Equipment_Id = e.Equipment_Id";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reservations.Add(new Reservation
                            {
                                ReservationId = (int)reader["Reservation_Id"],
                                CustomerId = (int)reader["Customer_Id"],
                                ReservationDate = (DateTime)reader["ReservationDate"],
                                TotalPrice = (decimal)reader["TotalPrice"],
                                Status = (Status)Enum.Parse(typeof(Status), reader["Status"].ToString()),
                                Quantity = (int)reader["Quantity"], // Fetch quantity

                                Equipment = new Equipment
                                {
                                    EquipmentId = (int)reader["Equipment_Id"],
                                    Name = reader["EquipmentName"].ToString(),
                                    Brand = reader["Brand"].ToString(),
                                    Size = reader["Size"].ToString(),
                                    PricePerDay = (decimal)reader["PricePerDay"],
                                    Category = (EquipmentCategory)Enum.Parse(typeof(EquipmentCategory), reader["Category"].ToString()),
                                    ImagePath = reader["ImagePath"].ToString()
                                }
                            });
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in GetReservations: {ex.Message}");
                throw;
            }
            finally
            {
                connection.Close();
            }

            return reservations;
        }

    }
}
