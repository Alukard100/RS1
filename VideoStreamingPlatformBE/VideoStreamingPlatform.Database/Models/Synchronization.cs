using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class Synchronization
    {
        public Synchronization()
        {
            GroupMembers = new HashSet<GroupMember>();
        }

        public int GroupId { get; set; }
        public int SyncOwnerId { get; set; }
        public int VideoId { get; set; }
        public string GroupCode { get; set; } = null!;

        public virtual User SyncOwner { get; set; } = null!;
        public virtual Video Video { get; set; } = null!;
        public virtual ICollection<GroupMember> GroupMembers { get; set; }
    }
}
