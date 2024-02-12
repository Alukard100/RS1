using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Advertisement;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.Advertisement;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface IAdvertisementService
    {
        CommonResponse CreateAdvertisement(CreateAdvertisementRequest request);
        List<GetAdvertisementResponse> GetAdvertisements(GetAdvertisementsRequest request);
        CommonResponse UpdateAdvertisement(UpdateAdvertisementRequest request);
        CommonResponse DeleteAdvertisement(CommonDeleteRequest request);

    }
}
