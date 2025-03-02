using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Responses.Notifications
{
    public class GetNotificationsResponse
    {
        public int? RecipientUserId { get; set; }
        public int? SenderUserId { get; set; }
        public int? NotificationTypeId { get; set; }
        public bool? IsRead { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
