using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.User;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.User;


namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface IUserService
    {
        CommonResponse CreateUser(CreateUserRequest request);
        List<GetUserResponse> GetUser(GetUserRequest request);
        CommonResponse DeleteUser(CommonDeleteRequest request);
        CommonResponse UpdateUser(UpdateUserRequest request);


    }
}
