using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Playlist;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.Playlist;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface IPlaylistService
    {
        CommonResponse CreatePlaylist(CreatePlaylistRequest request);
        List<GetPlaylistResponse> GetPlaylists(GetPlaylistsRequest request);
        CommonResponse UpdatePlaylist(UpdatePlaylistRequest request);
        CommonResponse DeletePlaylist(CommonDeleteRequest request);

    }
}
