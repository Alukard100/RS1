using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class Playlist
    {
        public int PlaylistId { get; set; }
        public int UserId { get; set; }
        public string? PlaylistName { get; set; }
        public bool? IsPublic { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
