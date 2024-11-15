using ClassLibrary.ObjectClasses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.DataAccess
{
    public class CustomerMediator : DbAccess
    {
        public CustomerMediator() : base() { }

        public void CreateCustomer(Customer customer)
        {
            try
            {
                string userSql = @"
            INSERT INTO [User] (FirstName, LastName, Username, Email, Password)
            OUTPUT INSERTED.User_Id  -- Retrieve the newly inserted User_Id
            VALUES (@FirstName, @LastName, @Username, @Email, @Password)";

                int userId;

                using (SqlCommand cmd = new SqlCommand(userSql, connection))
                {
                   
                    cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                    cmd.Parameters.AddWithValue("@Username", customer.Username);
                    cmd.Parameters.AddWithValue("@Email", customer.Email);
                    cmd.Parameters.AddWithValue("@Password", customer.Password);  

                    connection.Open();
                    userId = (int)cmd.ExecuteScalar();
                }

                string customerSql = @"
            INSERT INTO [Customer] (Customer_Id, Phone_Number, Address)
            VALUES (@Customer_Id, @Phone_Number, @Address)";

                using (SqlCommand cmd = new SqlCommand(customerSql, connection))
                {
                    cmd.Parameters.AddWithValue("@Customer_Id", userId);
                    cmd.Parameters.AddWithValue("@Phone_Number", customer.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Address", customer.Address);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }


        public SqlDataReader GetCustomerById(int customerId)
        {
            SqlDataReader reader = null;
            try
            {
                string sql = "SELECT u.userId, u.userName, u.email, u.dateOfBirth, c.phoneNumber, c.address " +
                             "FROM User u " +
                             "JOIN Customer c ON u.userId = c.customerId " +
                             "WHERE u.userId = @CustomerId";

                using (var cmd = new SqlCommand(sql, this.connection))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);
                    connection.Open();
                    reader = cmd.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return reader; 
        }

       
        public void UpdateCustomer(int customerId, string phoneNumber, string address)
        {
            try
            {
                string sql = "UPDATE Customer SET phoneNumber = @PhoneNumber, address = @Address WHERE customerId = @CustomerId";

                using (var cmd = new SqlCommand(sql, this.connection))
                {
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        
        public void DeleteCustomer(int customerId)
        {
            try
            {
                string sql = "DELETE FROM Customer WHERE customerId = @CustomerId;" +
                             "DELETE FROM User WHERE userId = @CustomerId";

                using (var cmd = new SqlCommand(sql, this.connection))
                {
                    cmd.Parameters.AddWithValue("@CustomerId", customerId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            Customer customer = null;
            string query = "SELECT User_Id, Username, FirstName, LastName, Email, Password FROM [User] WHERE Email = @Email";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    await connection.OpenAsync(); 

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())  
                    {
                        if (await reader.ReadAsync())  
                        {
                            customer = new Customer
                            {
                                UserId = (int)reader["User_Id"],
                                Username = reader["Username"].ToString(),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Password = reader["Password"].ToString()  
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            finally
            {
                await connection.CloseAsync();  
            }

            return customer;
        }

        public List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            string query = "SELECT User_Id, Username, FirstName, LastName, Email, Password FROM [User]";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer
                            {
                                UserId = (int)reader["User_Id"],
                                Username = reader["Username"].ToString(),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Password = reader["Password"].ToString()
                            };
                            customers.Add(customer);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return customers;
        }
    }
}
