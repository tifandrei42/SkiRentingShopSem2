using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DataAccess
{
    public class CategoryMediator : DbAccess, ICategoryMediator
    {
        public CategoryMediator() : base() { }

        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            string query = "SELECT Category_Id, CategoryName FROM Category";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new Category
                            {
                                CategoryId = (int)reader["Category_Id"],
                                CategoryName = reader["CategoryName"].ToString()
                            });
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in GetAllCategories: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return categories;
        }

        public Category? GetCategoryById(int categoryId)
        {
            Category category = null;
            string query = "SELECT Category_Id, CategoryName FROM Category WHERE Category_Id = @CategoryId";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            category = new Category
                            {
                                CategoryId = (int)reader["Category_Id"],
                                CategoryName = reader["CategoryName"].ToString()
                            };
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in GetCategoryById: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return category;
        }

        public void AddCategory(Category category)
        {
            string query = "INSERT INTO Category (CategoryName) VALUES (@CategoryName);";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in AddCategory: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateCategory(Category category)
        {
            string query = "UPDATE Category SET CategoryName = @CategoryName WHERE Category_Id = @CategoryId";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CategoryId", category.CategoryId);
                    cmd.Parameters.AddWithValue("@CategoryName", category.CategoryName);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in UpdateCategory: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        public void DeleteCategory(int categoryId)
        {
            string query = "DELETE FROM Category WHERE Category_Id = @CategoryId";

            try
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error in DeleteCategory: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
