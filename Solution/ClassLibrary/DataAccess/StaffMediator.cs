using ClassLibrary.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DataAccess
{
    public class StaffMemberMediator : DbAccess
    {
        public int CreateStaffMember(StaffMember staffMember)
        {
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

                    connection.Open();
                    int newStaffId = (int)cmd.ExecuteScalar();
                    return newStaffId;  // Return the new Staff ID
                }
            }
            finally
            {
                connection.Close();
            }
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
                    "INNER JOIN [User] u ON sm.Staff_Id = u.User_Id " + // Corrected the JOIN
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
                                UserId = (int)reader["User_Id"], // from User table
                                FirstName = reader["FirstName"].ToString(), // from User table
                                LastName = reader["LastName"].ToString(), // from User table
                                Email = reader["Email"].ToString(), // from User table
                                Password = reader["Password"].ToString() // from User table
                            };
                        }
                    }
                }
            }
            finally
            {
                connection.Close();
            }

            return null;  // Staff member not found
        }
    }
}
