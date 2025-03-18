using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class VideoStatistic
    {
        public int VideoStatisticsId { get; set; }
        public int? VideoId { get; set; }
        public int ClickCounter { get; set; }
        [JsonIgnore]
        public virtual Video? Video { get; set; }
    }
}
