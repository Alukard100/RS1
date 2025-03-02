using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class RatingSystemVideo
    {
        public int RatingId { get; set; }
        public int VideoId { get; set; }
        public int? LikeCount { get; set; }
        public int? DislikeCount { get; set; }
        [JsonIgnore]
        public virtual Video Video { get; set; }
    }
}
