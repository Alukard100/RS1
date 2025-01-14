using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.Category
{
    public class UpdateCategoryRequest
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
