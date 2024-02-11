using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class VideoStatistic
    {
        public int VideoStatisticsId { get; set; }
        public int? VideoId { get; set; }
        public int? ClickCounter { get; set; }

        public virtual Video? Video { get; set; }
    }
}
