using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Notifications;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.Notifications;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface INotificationService
    {
        CommonResponse CreateNotification(CreateNotificationRequest request);
        CommonResponse DeleteNotification(CommonDeleteRequest request);
        List<GetNotificationsResponse> GetNotification(GetNotificationsRequest request);
    }
}
