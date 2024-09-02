using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.EmojiShow;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.EmojiShow;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface IEmojiShowService
    {
        CommonResponse CreateEmojiShow(CreateEmojiShowRequest request);
        List<GetEmojiShowResponse> GetEmojiShows(GetEmojiShowsRequest request);
        CommonResponse UpdateEmojiShow(UpdateEmojiShowRequest request);
        CommonResponse DeleteEmojiShow(CommonDeleteRequest request);
    }
}
