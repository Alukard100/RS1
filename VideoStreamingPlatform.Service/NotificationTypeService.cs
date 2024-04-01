using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Responses.NotificationType;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class NotificationTypeService : INotificationTypeService
    {
        VideoStreamingPlatformContext _db = new VideoStreamingPlatformContext();

        public GetNotificationTypeResponse GetNotificationType(CommonDeleteRequest request)
        {
            var notificationType= _db.NotificationTypes.Where(x=>x.NotificationTypeId==request.Id).FirstOrDefault();
            if (notificationType != null )
            {
                var returnVal= new GetNotificationTypeResponse() { Value=notificationType.Value };
                return returnVal;
            }
            else
            {
            throw new InvalidOperationException("Notification type with provided ID does not exist.");
            }
        }
    }
}
