using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Responses.NotificationType;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface INotificationTypeService
    {
        GetNotificationTypeResponse GetNotificationType(CommonDeleteRequest request);
    }
}
