using System;
using System.Collections.Generic;
using System.Linq;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.GroupMembers;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.GroupMembers;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface IGroupMemberService
    {
        CommonResponse CreateGroupMembers(CreateGroupMembersRequest request);
        List<GetGroupMemberResponse> GetGroupMembers(GetGroupMembersRequest request);
        CommonResponse UpdateGroupMembers(UpdateGroupMembersRequest request);
        CommonResponse DeleteGroupMembers(CommonDeleteRequest request);

    }
}