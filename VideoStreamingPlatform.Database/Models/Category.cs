using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public virtual ICollection<Video> Videos { get; set; }
    }
}
