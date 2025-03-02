using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Support;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.Support;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface ISupportService
    {
        CommonResponse CreateSupport(CreateSupportRequest request);
        CommonResponse DeleteSupport(CommonDeleteRequest request);
        List<GetSupportResponse> GetSupport(GetSupportRequest request);
    }
}
