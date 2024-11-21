using BusinessLogic.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataAccess
{
    public class StaffMemberMediator : DbAccess
    {
        public int CreateStaffMember(StaffMember staffMember)
        {
            int newStaffId = 0;

            try
            {
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO StaffMember (FirstName, LastName, Email, Role) OUTPUT INSERTED.Staff_Id " +
                    "VALUES (@FirstName, @LastName, @Email, @Role)", connection))
                {
                    cmd.Parameters.AddWithValue("@FirstName", staffMember.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", staffMember.LastName);
                    cmd.Parameters.AddWithValue("@Email", staffMember.Email);
                    cmd.Parameters.AddWithValue("@Role", staffMember.Role);

                    if (connection.State == System.Data.ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    newStaffId = (int)cmd.ExecuteScalar();
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine("Database error: " + sqlEx.Message);
            }
            catch (InvalidOperationException invEx)
            {
                Console.WriteLine("Invalid operation: " + invEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }

            return newStaffId;
        }

        public StaffMember GetStaffMemberById(int staffId)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT Staff_Id, FirstName, LastName, Email, Role FROM StaffMember WHERE Staff_Id = @Staff_Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Staff_Id", staffId);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new StaffMember
                            {
                                StaffId = (int)reader["Staff_Id"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Role = reader["Role"].ToString()
                            };
                        }
                    }
                }
            }

            catch (SqlException sqlEx)
            {
                Console.WriteLine("Database error: " + sqlEx.Message);
            }
            catch (InvalidOperationException invEx)
            {
                Console.WriteLine("Invalid operation: " + invEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }

            finally
            {
                connection.Close();
            }
            return null;
        }

        public void UpdateStaffMember(StaffMember staffMember)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE StaffMember SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Role = @Role " +
                    "WHERE Staff_Id = @Staff_Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Staff_Id", staffMember.StaffId);
                    cmd.Parameters.AddWithValue("@FirstName", staffMember.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", staffMember.LastName);
                    cmd.Parameters.AddWithValue("@Email", staffMember.Email);
                    cmd.Parameters.AddWithValue("@Role", staffMember.Role);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            catch (SqlException sqlEx)
            {
                Console.WriteLine("Database error: " + sqlEx.Message);
            }
            catch (InvalidOperationException invEx)
            {
                Console.WriteLine("Invalid operation: " + invEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }

            finally
            {
                connection.Close();
            }
        }

        public void DeleteStaffMember(int staffId)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM StaffMember WHERE Staff_Id = @Staff_Id", connection))
                {
                    cmd.Parameters.AddWithValue("@Staff_Id", staffId);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            catch (SqlException sqlEx)
            {
                Console.WriteLine("Database error: " + sqlEx.Message);
            }
            catch (InvalidOperationException invEx)
            {
                Console.WriteLine("Invalid operation: " + invEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }

            finally
            {
                connection.Close();
            }
        }

        public List<StaffMember> GetAllStaffMembers()
        {
            List<StaffMember> staffMembers = new List<StaffMember>();

            try
            {
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT Staff_Id, FirstName, LastName, Email, Role FROM StaffMember", connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StaffMember staffMember = new StaffMember
                            {
                                StaffId = (int)reader["Staff_Id"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Role = reader["Role"].ToString()
                            };

                            staffMembers.Add(staffMember);
                        }
                    }
                }
            }

            catch (SqlException sqlEx)
            {
                Console.WriteLine("Database error: " + sqlEx.Message);
            }
            catch (InvalidOperationException invEx)
            {
                Console.WriteLine("Invalid operation: " + invEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }

            finally
            {
                connection.Close();
            }

            return staffMembers;
        }

        public StaffMember GetStaffMemberByEmail(string email)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT sm.Staff_Id, sm.Role, u.User_Id, u.FirstName, u.LastName, u.Email, u.Password " +
                    "FROM StaffMember sm " +
                    "INNER JOIN [User] u ON sm.Staff_Id = u.User_Id " +
                    "WHERE u.Email = @Email", connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new StaffMember
                            {
                                StaffId = (int)reader["Staff_Id"],
                                Role = reader["Role"].ToString(),
                                UserId = (int)reader["User_Id"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Password = reader["Password"].ToString()
                            };
                        }
                    }
                }
            }

            catch (SqlException sqlEx)
            {
                Console.WriteLine("Database error: " + sqlEx.Message);
            }
            catch (InvalidOperationException invEx)
            {
                Console.WriteLine("Invalid operation: " + invEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }

            finally
            {
                connection.Close();
            }

            return null;
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
    }
}
