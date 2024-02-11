using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class PlaylistGroup
    {
        public int PlaylistId { get; set; }
        public int VideoId { get; set; }

        public virtual Playlist Playlist { get; set; } = null!;
        public virtual Video Video { get; set; } = null!;
    }
}
