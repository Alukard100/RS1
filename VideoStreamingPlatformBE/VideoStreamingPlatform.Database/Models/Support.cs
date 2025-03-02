using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class Support
    {
        public int SupportId { get; set; }
        public int UserId { get; set; }
        public string Body { get; set; } = null!;
        public DateTime TimeSent { get; set; }
        public bool Seen { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
