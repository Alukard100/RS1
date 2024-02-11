using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class RatingSystemComment
    {
        public int RatingId { get; set; }
        public int CommentId { get; set; }
        public int? LikeCount { get; set; }
        public int? DislikeCount { get; set; }

        public virtual Comment Comment { get; set; } = null!;
    }
}
