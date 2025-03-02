using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Category;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly VideoStreamingPlatformContext _db;
        public CategoryService(VideoStreamingPlatformContext dbContext)
        {
            _db = dbContext;
        }

        public Category CreateCategory(string categoryName)
        {
            var existingCategory = _db.Categories.FirstOrDefault(c => c.CategoryName.ToUpper() == categoryName.ToUpper());
            if (existingCategory != null) 
            {
                return null;
            }
            var newCategory = new Category { CategoryName = categoryName };
            _db.Categories.Add(newCategory);
            _db.SaveChanges();
            return newCategory;
        }

        public bool DeleteCategory(int categoryId)
        {
            var existingCategory = _db.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (existingCategory != null)
            {
                _db.Categories.Remove(existingCategory);
                _db.SaveChanges();
                return true;
            } return false;
        }

        public Category GetCategory(int categoryId)
        {
            var existingCategory = _db.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (existingCategory != null) 
            {
                return existingCategory;
            } return null;
        }

        public Category UpdateCategory(UpdateCategoryRequest request)
        {
            var category = _db.Categories.FirstOrDefault(c => c.CategoryId == request.CategoryId);
            if (category != null) 
            {
                category.CategoryName = request.CategoryName;

                _db.Categories.Update(category);
                _db.SaveChanges();
                return category;
            } return null;
            
        }
    }
}
