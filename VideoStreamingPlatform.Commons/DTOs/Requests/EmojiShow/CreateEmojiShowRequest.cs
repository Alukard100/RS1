using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.EmojiShow
{
    public class CreateEmojiShowRequest
    {
        public string EmojiName { get; set; }
        public byte[] Icon { get; set; }
    }
}
