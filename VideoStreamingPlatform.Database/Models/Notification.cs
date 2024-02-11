using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class Notification
    {
        public int NotificationId { get; set; }
        public int? RecipientUserId { get; set; }
        public int? SenderUserId { get; set; }
        public int? NotificationTypeId { get; set; }
        public bool? IsRead { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual NotificationType? NotificationType { get; set; }
        public virtual User? RecipientUser { get; set; }
        public virtual User? SenderUser { get; set; }
    }
}
