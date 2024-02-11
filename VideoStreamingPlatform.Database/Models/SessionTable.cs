using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class SessionTable
    {
        public int SessionTableId { get; set; }
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public int? TimeStamp { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Video Video { get; set; } = null!;
    }
}
