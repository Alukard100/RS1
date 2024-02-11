using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class Category
    {
        public Category()
        {
            Videos = new HashSet<Video>();
        }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public virtual ICollection<Video> Videos { get; set; }
    }
}
