using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime? PostedDate { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; } = null!;
        [JsonIgnore]
        public virtual Video Video { get; set; } = null!;
        public virtual RatingSystemComment RatingSystemComments { get; set; } = null!;
    }
}
