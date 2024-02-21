using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests.ActivePromoCodes;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.ActivePromoCodes;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface IActivePromoCodesService
    {
        CommonResponse GeneratePromoCodes(GeneratePromoCodesRequest request);
        List<GetListOfActiveCodesResponse> GetListOfActiveCodes(GetListOfActiveCodesRequest request);
        CommonResponse DeleteUsedPromoCodes();
    }
}
