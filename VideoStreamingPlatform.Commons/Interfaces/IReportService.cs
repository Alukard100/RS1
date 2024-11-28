using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Report;
using VideoStreamingPlatform.Commons.DTOs.Responses;
using VideoStreamingPlatform.Commons.DTOs.Responses.Report;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface IReportService
    {
        CommonResponse CreateReport(CreateReportRequest request);
        List<GetReportResponse> GetReports(GetReportsRequest request);
        CommonResponse UpdateReport(UpdateReportRequest request);
        CommonResponse DeleteReport(CommonDeleteRequest request);

    }
}
