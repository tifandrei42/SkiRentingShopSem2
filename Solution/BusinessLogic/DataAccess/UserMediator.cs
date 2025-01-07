using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BusinessLogic.DataAccess
{
    public class UserMediator : DbAccess, IUserMediator
    {
        public UserMediator() : base() { }

        public void AddUser(User user)
        {
            string query = @"
                INSERT INTO [dbo].[User] (UserName, FirstName, LastName, Email, Password, DateOfBirth, RoleId, PhoneNumber, Address)
                VALUES (@UserName, @FirstName, @LastName, @Email, @Password, @DateOfBirth, @RoleId, @PhoneNumber, @Address);
            ";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@UserName", user.UserName);
                    cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", user.LastName);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", user.Address);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                    cmd.Parameters.AddWithValue("@RoleId", (int)user.Role);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error adding user: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        public void DeleteUser(int userId)
        {
            string query = "DELETE FROM [dbo].[User] WHERE User_Id = @UserId;";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error deleting user: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            string query = @"SELECT User_Id, UserName, FirstName, LastName, Email, Password, PhoneNumber, Address, DateOfBirth, RoleId FROM [dbo].[User]";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new()
                            {
                                User_Id = reader.GetInt32(reader.GetOrdinal("User_Id")),
                                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Password = reader.GetString(reader.GetOrdinal("Password")),
                                PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                Role = (UserRole)reader.GetInt32(reader.GetOrdinal("RoleId"))
                            };
                            users.Add(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error retrieving all users: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return users;
        }

        public User GetUserById(int userId)
        {
            User user = null;
            string query = @"
                SELECT User_Id, UserName, FirstName, LastName, Email, Password, DateOfBirth, RoleId 
                FROM [dbo].[User] 
                WHERE User_Id = @UserId;
            ";

            try
            {
                using (SqlCommand cmd = new(query, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                User_Id = reader.GetInt32(reader.GetOrdinal("User_Id")),
                                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Password = reader.GetString(reader.GetOrdinal("Password")),
                                PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                Role = (UserRole)reader.GetInt32(reader.GetOrdinal("RoleId"))
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error retrieving user by ID: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            if (user == null)
            {
                throw new InvalidOperationException($"No user found for User ID {userId}");
            }

            return user;
        }

        public void UpdateUser(User user)
        {
            string query = @"
                UPDATE [dbo].[User]
                SET UserName = @UserName,
                    FirstName = @FirstName,
                    LastName = @LastName,
                    Email = @Email,
                    Password = @Password,
                    DateOfBirth = @DateOfBirth,
                    PhoneNumber = @PhoneNumber,
                    Address = @Address,
                    RoleId = @RoleId
                WHERE User_Id = @UserId;
            ";

            try
            {
                using (SqlCommand cmd = new(query, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", user.User_Id);
                    cmd.Parameters.AddWithValue("@UserName", user.UserName);
                    cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", user.LastName);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", user.Address);
                    cmd.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                    cmd.Parameters.AddWithValue("@RoleId", (int)user.Role);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error updating user: " + ex.Message);
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        public User? GetUserByEmail(string email)
        {
            User? user = null;

            // SQL query with correct column names
            string query = @"
        SELECT 
            User_Id, 
            UserName, 
            FirstName, 
            LastName, 
            Email, 
            Password, 
            DateOfBirth, 
            RoleId, 
            Address, 
            PhoneNumber
        FROM [dbo].[User] 
        WHERE Email = @Email;
    ";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            user = new User
                            {
                                // Map database fields to the User object
                                User_Id = reader.GetInt32(reader.GetOrdinal("User_Id")),
                                UserName = reader["UserName"] as string ?? string.Empty,
                                FirstName = reader["FirstName"] as string ?? string.Empty,
                                LastName = reader["LastName"] as string ?? string.Empty,
                                Email = reader["Email"] as string ?? string.Empty,
                                Address = reader["Address"] as string ?? string.Empty,
                                PhoneNumber = reader["PhoneNumber"] as string ?? string.Empty,
                                Password = reader["Password"] as string ?? string.Empty,
                                DateOfBirth = reader.IsDBNull(reader.GetOrdinal("DateOfBirth"))
                                    ? DateTime.MinValue 
                                    : reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                Role = reader.IsDBNull(reader.GetOrdinal("RoleId"))
                                    ? UserRole.None
                                    : (UserRole)reader.GetInt32(reader.GetOrdinal("RoleId"))
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error retrieving user by email: {ex.Message}");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            return user;
        }

        public List<User> GetUsersByRole(string roleName)
        {
            List<User> users = new List<User>();

            string query = @"
                SELECT u.User_Id, u.Username, u.Email, u.PhoneNumber, u.Address
                FROM [User] u -- Escaped table name
                JOIN UserRoles r ON u.RoleId = r.RoleId
                WHERE r.RoleName = @RoleName;";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@RoleName", roleName);
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                User_Id = (int)reader["User_Id"],
                                UserName = reader["Username"].ToString(),
                                Email = reader["Email"].ToString(),
                                PhoneNumber = reader["PhoneNumber"].ToString(),
                                Address = reader["Address"].ToString()
                            });
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in GetUsersByRole: {ex.Message}");
                throw;
            }
            finally
            {
                connection.Close();
            }

            return users;
        }
    }
}
