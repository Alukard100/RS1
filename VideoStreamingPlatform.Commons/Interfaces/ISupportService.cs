using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests.Support;
using VideoStreamingPlatform.Commons.DTOs.Responses;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface ISupportService
    {
        CommonResponse CreateSupport(CreateSupportRequest request);
    }
}
