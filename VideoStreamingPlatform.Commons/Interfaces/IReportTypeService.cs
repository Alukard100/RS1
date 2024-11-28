using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.ReportType;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.ReportType;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface IReportTypeService
    {
        CommonResponse CreateReportType(CreateReportTypeRequest request);
        List<GetReportTypeResponse> GetReportTypes(GetReportTypesRequest request);
        CommonResponse UpdateReportType(UpdateReportTypeRequest request);
        CommonResponse DeleteReportType(CommonDeleteRequest request);

    }
}
