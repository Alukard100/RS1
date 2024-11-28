using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Commons.DTOs.Responses.Report
{
    public class GetReportResponse
    {
        public int ReportId { get; set; }
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public int? ReportTypeId { get; set; }
        public string? ReportText { get; set; }
        public DateTime? DateOfReport { get; set; }

        public virtual Database.Models.ReportType? ReportType { get; set; }
        public virtual Database.Models.User User { get; set; } = null!;
        public virtual Video Video { get; set; } = null!;
    }
}
