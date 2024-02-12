using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.UserType;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.UserType;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface IUserTypeService
    {
        CommonResponse CreateUserType(CreateUserTypeRequest request);
        CommonResponse DeleteUserType(CommonDeleteRequest request);
        IEnumerable<GetUserTypeResponse> GetUserType();
    }
}