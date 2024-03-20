using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.UserValues;
using VideoStreamingPlatform.Commons.DTOs.Responses;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface IUserValuesService
    {
        CommonResponse CreateUserValues(CreateUserValuesRequest request);
        CommonResponse DeleteUserValues(CommonDeleteRequest request);
    }
}
