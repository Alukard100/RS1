using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.MessageBody;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.MessageBody;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface IMessageBodyService
    {
        CommonResponse CreateMessageBody(CreateMessageBodyRequest request);
        CommonResponse DeleteMessageBody(CommonDeleteRequest request);
        CommonResponse UpdateMessageBody(UpdateMessageBodyRequest request);
        List<GetMessageBodyResponse> GetMessageBody(GetMessageBodyRequest request);
        
    }
}
