using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.Notifications
{
    public class GetNotificationsRequest
    {
        public int? RecipientUserId { get; set; }
        public int? NotificationTypeId { get; set; }
        public bool? IsRead { get; set; }
    }
}
