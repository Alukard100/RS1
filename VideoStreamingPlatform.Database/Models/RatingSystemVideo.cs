using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class RatingSystemVideo
    {
        public int RatingId { get; set; }
        public int VideoId { get; set; }
        public int? LikeCount { get; set; }
        public int? DislikeCount { get; set; }

        public virtual Video Video { get; set; } = null!;
    }
}
