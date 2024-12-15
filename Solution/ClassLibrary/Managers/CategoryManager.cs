using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Managers
{
    public class CategoryManager
    {
        private readonly ICategoryMediator _categoryMediator;

        public CategoryManager(ICategoryMediator categoryMediator)
        {
            _categoryMediator = categoryMediator;
        }

        public List<Category> GetAllCategories()
        {
            return _categoryMediator.GetAllCategories();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _categoryMediator.GetCategoryById(categoryId);
        }

        public void AddCategory(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                throw new ArgumentException("Category name cannot be empty.");
            }

            _categoryMediator.AddCategory(category);
        }

        public void UpdateCategory(Category category)
        {
            if (category.CategoryId <= 0)
            {
                throw new ArgumentException("Invalid Category ID.");
            }

            if (string.IsNullOrWhiteSpace(category.CategoryName))
            {
                throw new ArgumentException("Category name cannot be empty.");
            }

            _categoryMediator.UpdateCategory(category);
        }

        public void DeleteCategory(int categoryId)
        {
            if (categoryId <= 0)
            {
                throw new ArgumentException("Invalid Category ID.");
            }

            _categoryMediator.DeleteCategory(categoryId);
        }
    }
}
