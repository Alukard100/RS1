namespace VideoStreamingPlatform.Commons.DTOs.Requests.Report
{
    public class GetReportsRequest
    {
        public int ReportId { get; set; }
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public int? ReportTypeId { get; set; }
        public string? ReportText { get; set; }
        public DateTime? DateOfReport { get; set; }

    }
}
