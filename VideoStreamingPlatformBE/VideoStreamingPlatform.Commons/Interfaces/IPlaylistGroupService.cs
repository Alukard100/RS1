using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.PlaylistGroup;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.PlaylistGroup;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface IPlaylistGroupService
    {
        CommonResponse CreatePlaylistGroup(CreatePlaylistGroupRequest request);
        List<GetPlaylistGroupResponse> GetPlaylistGroups(GetPlaylistGroupsRequest request);
        CommonResponse UpdatePlaylistGroup(UpdatePlaylistGroupRequest request);
        CommonResponse DeletePlaylistGroup(int playlistId, int videoId);

    }
}
