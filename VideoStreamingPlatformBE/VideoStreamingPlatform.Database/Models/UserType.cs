using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class UserType
    {
        public UserType()
        {
            Users = new HashSet<User>();
        }

        public int TypeId { get; set; }
        public string? Type { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
