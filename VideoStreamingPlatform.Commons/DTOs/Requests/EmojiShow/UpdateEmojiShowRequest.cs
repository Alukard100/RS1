using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.EmojiShow
{
    public class UpdateEmojiShowRequest
    {
        public int EmojiShowId { get; set; }
        public string? EmojiName { get; set; }
        public byte[]? Icon { get; set; }
    }
}
