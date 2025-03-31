using System.Collections.Generic;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.MessageBody;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.MessageBody;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface IMessageBodyService
    {
        Task<CommonResponse> CreateMessageBody(CreateMessageBodyRequest request);
        Task<CommonResponse> DeleteMessageBody(CommonDeleteRequest request);
        Task<CommonResponse> UpdateMessageBody(UpdateMessageBodyRequest request);
        Task<List<GetMessageBodyResponse>> GetMessageBody(GetMessageBodyRequest request);
    }
}
