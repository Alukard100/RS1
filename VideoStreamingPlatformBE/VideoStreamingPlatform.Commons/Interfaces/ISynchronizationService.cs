using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Synchronization;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.Synchronization;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface ISynchronizationService
    {
        CommonResponse CreateSynchronization(CreateSynchronizationRequest request);
        List<GetSynchronizationResponse> GetSynchronizations(GetSynchronizationsRequest request);
        CommonResponse UpdateSynchronization(UpdateSynchronizationRequest request);
        CommonResponse DeleteSynchronization(CommonDeleteRequest request);

    }
}
