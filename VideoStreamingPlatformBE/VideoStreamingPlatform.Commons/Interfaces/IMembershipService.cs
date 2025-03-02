using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Membership;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.Membership;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface IMembershipService
    {
        CommonResponse CreateMembership(CreateMembershipRequest request);
        CommonResponse DeleteMembership(CommonDeleteRequest request);
        CommonResponse UpdateMembership(CreateMembershipRequest request);
        List<GetMembershipResponse> GetMembership(GetMembershipRequest request);

    }
}
