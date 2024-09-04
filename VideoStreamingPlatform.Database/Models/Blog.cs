using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class Blog
    {
        public int BlogId { get; set; }
        public int? UserId { get; set; }
        public string? Title { get; set; }
        public string PictureURL { get; set; }
        public string? Content { get; set; }

        public virtual User? User { get; set; }
    }
}
