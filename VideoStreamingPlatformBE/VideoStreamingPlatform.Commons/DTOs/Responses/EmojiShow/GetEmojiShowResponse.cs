using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Responses.EmojiShow
{
    public class GetEmojiShowResponse
    {
        public int EmojiShowId { get; set; }
        public string? EmojiName { get; set; }
    }
}
