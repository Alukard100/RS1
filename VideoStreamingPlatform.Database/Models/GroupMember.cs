using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class GroupMember
    {
        public int GroupMemberId { get; set; }
        public int GroupId { get; set; }
        public int UserId { get; set; }

        public virtual Synchronization Group { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
