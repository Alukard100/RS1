using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class ThumbnailInfo
    {
        public int ThumbnailInfoId { get; set; }
        public byte[]? ThumbnailPicture { get; set; }
        public int VideoId { get; set; }

        public virtual Video Video { get; set; } = null!;
    }
}
