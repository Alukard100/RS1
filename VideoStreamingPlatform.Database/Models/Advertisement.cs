using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class Advertisement
    {
        public int AdvertisementId { get; set; }
        public int UserId { get; set; }
        public int? VideoId { get; set; }
        public string AdvertisementPictureURL { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Video? Video { get; set; }
    }
}
