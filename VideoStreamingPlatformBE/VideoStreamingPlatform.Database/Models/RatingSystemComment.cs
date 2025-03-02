using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class RatingSystemComment
    {
        public int RatingId { get; set; }
        public int CommentId { get; set; }
        public int? LikeCount { get; set; }
        public int? DislikeCount { get; set; }
        [JsonIgnore]
        public virtual Comment Comment { get; set; } = null!;
    }
}
