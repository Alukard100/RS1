using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Responses.Playlist
{
    public class GetPlaylistResponse
    {
        public int PlaylistId { get; set; }
        public int UserId { get; set; }
        public string? PlaylistName { get; set; }
        public bool? IsPublic { get; set; }
    }
}
