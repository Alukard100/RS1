using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class UserValue
    {
        public int UserValuesId { get; set; }
        public int? UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? PasswordHash { get; set; }

        public virtual User? User { get; set; }
    }
}
