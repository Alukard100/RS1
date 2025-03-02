using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class EmojiShow
    {
        public int EmojiShowId { get; set; }
        public string? EmojiName { get; set; }
        public int? VideoId { get; set; }
        public int? ClickCounter { get; set; }
        public string? altName { get; set; }
        public virtual Video? Video { get; set; }
    }
}
