using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;

namespace BusinessLogic.DataAccess
{
    public class ReservationMediator : DbAccess, IReservationMediator
    {
        private bool connectionOpen;

        public ReservationMediator() : base() { connectionOpen = false; }

        private void OpenConnection() 
        {
            if (!connectionOpen)
            {
                connection.Open();
                connectionOpen = true;
            } 
        }
        private void CloseConnection()
        {
            if (connectionOpen)
            {
                connection.Close();
                connectionOpen = false;
            }
        }


        public void CreateReservation(Reservation reservation)
        {
            string query = @"
        INSERT INTO Reservation (Customer_Id, ReservationDate, CreationDate, TotalPrice, Status)
        VALUES (@CustomerId, @ReservationDate, @CreationDate, @TotalPrice, @Status);
        SELECT SCOPE_IDENTITY();";

            // Open connection and start a transaction
            OpenConnection();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                // Execute the reservation insert within the transaction
                using (SqlCommand cmd = new SqlCommand(query, connection, transaction))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", reservation.CustomerId);
                    cmd.Parameters.AddWithValue("@ReservationDate", reservation.ReservationDate.Date);
                    cmd.Parameters.AddWithValue("@CreationDate", reservation.CreationDate.Date);
                    cmd.Parameters.AddWithValue("@TotalPrice", reservation.TotalPrice);
                    cmd.Parameters.AddWithValue("@Status", reservation.Status.ToString());

                    // Get the new reservation ID
                    int reservationId = Convert.ToInt32(cmd.ExecuteScalar());

                    // Pass the transaction to AddReservationEquipment
                    AddReservationEquipment(reservationId, reservation.Equipment.EquipmentId, reservation.Quantity, transaction);
                }

                // Commit the transaction if all operations succeed
                transaction.Commit();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in CreateReservation: {ex.Message}");

                // Rollback the transaction in case of failure
                transaction.Rollback();
                throw;
            }
            finally
            {
                CloseConnection(); // Always close the connection
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
                    r.CreationDate,
                    r.TotalPrice,
                    r.Status,
                    e.Equipment_Id,
                    e.Name AS EquipmentName,
                    e.Brand,
                    e.Size,
                    e.PricePerDay,
                    e.ImagePath,
                    e.Category_Id AS Category,
                    re.Quantity
                FROM Reservation r
                INNER JOIN ReservationEquipment re ON r.Reservation_Id = re.Reservation_Id
                INNER JOIN Equipment e ON re.Equipment_Id = e.Equipment_Id
                WHERE r.Reservation_Id = @ReservationId;";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ReservationId", reservationId);
                    OpenConnection();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            reservation = new Reservation
                            {
                                ReservationId = (int)reader["Reservation_Id"],
                                CustomerId = (int)reader["Customer_Id"],
                                ReservationDate = (DateTime)reader["ReservationDate"],
                                CreationDate = (DateTime)reader["CreationDate"],
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
                CloseConnection();
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
                    r.CreationDate,
                    r.TotalPrice,
                    r.Status,
                    e.Equipment_Id,
                    e.Name AS EquipmentName,
                    e.Brand,
                    e.Size,
                    e.PricePerDay,
                    e.ImagePath,
                    e.Category_Id AS Category,
                    re.Quantity
                FROM Reservation r
                INNER JOIN ReservationEquipment re ON r.Reservation_Id = re.Reservation_Id
                INNER JOIN Equipment e ON re.Equipment_Id = e.Equipment_Id
                WHERE r.Customer_Id = @CustomerId;";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    OpenConnection();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reservations.Add(new Reservation
                            {
                                ReservationId = (int)reader["Reservation_Id"],
                                CustomerId = (int)reader["Customer_Id"],
                                ReservationDate = (DateTime)reader["ReservationDate"],
                                CreationDate = (DateTime)reader["CreationDate"],
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
                CloseConnection();
            }

            return reservations;
        }

        public void UpdateReservationStatus(int reservationId, string newStatus)
        {
            string query = @"
                UPDATE Reservation
                SET Status = @Status
                WHERE Reservation_Id = @ReservationId;";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Status", newStatus);
                    cmd.Parameters.AddWithValue("@ReservationId", reservationId);
                    OpenConnection();
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
                CloseConnection();
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
                    OpenConnection();
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
                CloseConnection();
            }
        }

        public void AddReservationEquipment(int reservationId, int equipmentId, int quantity, SqlTransaction transaction = null)
        {
            string query = @"
        INSERT INTO ReservationEquipment (Reservation_Id, Equipment_Id, Quantity)
        VALUES (@ReservationId, @EquipmentId, @Quantity)";

            try
            {
                // Execute the equipment insert within the provided transaction
                using (SqlCommand cmd = new SqlCommand(query, connection, transaction))
                {
                    cmd.Parameters.AddWithValue("@ReservationId", reservationId);
                    cmd.Parameters.AddWithValue("@EquipmentId", equipmentId);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in AddReservationEquipment: {ex.Message}");
                throw;
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
                    r.CreationDate,
                    r.TotalPrice,
                    r.Status,
                    e.Equipment_Id,
                    e.Name AS EquipmentName,
                    e.Brand,
                    e.Size,
                    e.PricePerDay,
                    e.Category_Id AS Category, -- Fixed column name
                    e.ImagePath,
                    re.Quantity
                FROM Reservation r
                LEFT JOIN ReservationEquipment re ON r.Reservation_Id = re.Reservation_Id
                LEFT JOIN Equipment e ON re.Equipment_Id = e.Equipment_Id";
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    OpenConnection();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reservations.Add(new Reservation
                            {
                                ReservationId = (int)reader["Reservation_Id"],
                                CustomerId = (int)reader["Customer_Id"],
                                CreationDate = (DateTime)reader["CreationDate"],
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
                CloseConnection();
            }

            return reservations;
        }

        public void UpdateGroupStatus(int customerId, DateTime reservationDate, string newStatus)
        {
            string query = @"
                UPDATE Reservation
                SET Status = @Status
                WHERE Customer_Id = @CustomerId AND ReservationDate = @ReservationDate";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Status", newStatus);
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    cmd.Parameters.AddWithValue("@ReservationDate", reservationDate);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating reservation status: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }


        public List<ReservationGroup> GetGroupedReservations()
        {
            List<ReservationGroup> groups = new List<ReservationGroup>();

            string query = @"
                SELECT 
                    r.Customer_Id,
                    r.ReservationDate,
                    COUNT(*) AS TotalReservations,
                    SUM(r.TotalPrice) AS TotalPrice,
                    STRING_AGG(e.Name, ', ') AS EquipmentNames,
                    STRING_AGG(r.Status, ', ') AS Statuses
                FROM Reservation r
                INNER JOIN ReservationEquipment re ON r.Reservation_Id = re.Reservation_Id
                INNER JOIN Equipment e ON re.Equipment_Id = e.Equipment_Id
                GROUP BY r.Customer_Id, r.ReservationDate";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            groups.Add(new ReservationGroup
                            {
                                CustomerId = (int)reader["Customer_Id"],
                                ReservationDate = (DateTime)reader["ReservationDate"],
                                TotalReservations = (int)reader["TotalReservations"],
                                TotalPrice = (decimal)reader["TotalPrice"],
                                EquipmentNames = reader["EquipmentNames"].ToString(),
                                Statuses = reader["Statuses"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching grouped reservations: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return groups;
        }

        public List<Reservation> GetReservationsByStatus(string status)
        {
            throw new NotImplementedException();
        }
    }
}
