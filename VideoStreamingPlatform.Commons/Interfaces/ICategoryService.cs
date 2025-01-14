using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests.Category;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface ICategoryService
    {
        public Category CreateCategory(string CategoryName);
        public Category UpdateCategory(UpdateCategoryRequest request);
        public bool DeleteCategory(int CategoryId);
        public Category GetCategory(int CategoryId);
    }
}
