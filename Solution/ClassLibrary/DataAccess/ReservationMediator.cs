using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataAccess
{
    public class ReservationMediator : DbAccess
    {
        public ReservationMediator() : base() { }

        public void CreateReservation(Reservation reservation)
        {
            string query = @"
                INSERT INTO Reservation (Customer_Id, ReservationDate, TotalPrice, Status)
                VALUES (@CustomerId, @ReservationDate, @TotalPrice, @Status)";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", reservation.CustomerId);
                    cmd.Parameters.AddWithValue("@ReservationDate", reservation.ReservationDate);
                    cmd.Parameters.AddWithValue("@TotalPrice", reservation.TotalPrice);
                    cmd.Parameters.AddWithValue("@Status", reservation.Status);

                    connection.Open();
                    cmd.ExecuteNonQuery();
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


        public Reservation? GetReservationById(int reservationId)
        {
            Reservation reservation = null;
            string query = @"
                SELECT Reservation_Id, Equipment_Id, Customer_Id, ReservationDate, CreationDate, TotalPrice, Status
                FROM Reservation
                WHERE Reservation_Id = @ReservationId";

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
                                EquipmentId = (int)reader["Equipment_Id"],
                                CustomerId = (int)reader["Customer_Id"],
                                ReservationDate = (DateTime)reader["ReservationDate"],
                                CreationDate = (DateTime)reader["CreationDate"],
                                TotalPrice = (decimal)reader["TotalPrice"],
                                Status = reader["Status"].ToString()
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

        public List<Reservation> GetReservationsByCustomerId(int customerId)
        {
            List<Reservation> reservations = new List<Reservation>();
            string query = @"
                SELECT Reservation_Id, Customer_Id, ReservationDate, CreationDate, TotalPrice, Status
                FROM Reservation
                WHERE Customer_Id = @CustomerId";

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
                                EquipmentId = (int)reader["Equipment_Id"],
                                CustomerId = (int)reader["Customer_Id"],
                                ReservationDate = (DateTime)reader["ReservationDate"],
                                CreationDate = (DateTime)reader["CreationDate"],
                                TotalPrice = (decimal)reader["TotalPrice"],
                                Status = reader["Status"].ToString()
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

        public List<Reservation> GetReservations()
        {
            List<Reservation> reservations = new List<Reservation>();

            string query = @"
                SELECT *
                FROM Reservation";

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
                                EquipmentId = (int)reader["Equipment_Id"],
                                CustomerId = (int)reader["Customer_Id"],
                                ReservationDate = (DateTime)reader["ReservationDate"],
                                CreationDate = (DateTime)reader["CreationDate"],
                                TotalPrice = (decimal)reader["TotalPrice"],
                                Status = reader["Status"].ToString()
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

        public List<Tuple<int, int, int>> GetReservationEquipmentByReservationId(int reservationId)
        {
            List<Tuple<int, int, int>> output = new List<Tuple<int, int, int>>();

            string query = @"
                SELECT Reservation_Id, Equipment_Id, Quantity
                FROM ReservationEquipment
                WHERE Reservation_Id = @ReservationId";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ReservationId", reservationId);
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int resId = (int)reader["Reservation_Id"];
                            int equipmentId = (int)reader["Equipment_Id"];
                            int quantity = (int)reader["Quantity"];
                            Tuple<int, int, int> tuple = Tuple.Create(resId, equipmentId, quantity);

                            output.Add(tuple);
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

            return output;
        }

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

        public int GetLastInsertedReservationId()
        {
            int lastId = 0; 

            string query = "SELECT IDENT_CURRENT('Reservation') AS LastId";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open(); 

                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        lastId = Convert.ToInt32(result);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in GetLastInsertedReservationId: {ex.Message}");
                throw;
            }
            finally
            {
                connection.Close(); // Ensure the connection is closed
            }

            return lastId; // Return the last inserted ID
        }
    }
}
