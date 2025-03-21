using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.UserValues;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.UserValues;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface IUserValuesService
    {
        CommonResponse CreateUserValues(CreateUserValuesRequest request);
        CommonResponse DeleteUserValues(CommonDeleteRequest request);
        GetUserValuesResponse GetUserValues(GetUserValuesRequest request);
        CommonResponse  UpdateUserValues(UpdateUserValuesRequest request);

        LoginResponse LoginUser(LoginRequest request);

    }
}
