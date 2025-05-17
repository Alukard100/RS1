using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class UserViews
    {
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public DateTime Timestamp { get; set; }
        public virtual User User { get; set; } = null;
        public virtual Video Video { get; set; } = null;

    }
}
