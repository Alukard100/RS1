using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class ThumbnailInfo
    {
        public int ThumbnailInfoId { get; set; }
        public byte[]? ThumbnailPicture { get; set; }
        public int VideoId { get; set; }
        [JsonIgnore]
        public virtual Video Video { get; set; } = null!;
    }
}
