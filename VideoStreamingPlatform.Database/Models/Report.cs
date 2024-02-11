using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class Report
    {
        public int ReportId { get; set; }
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public int? ReportTypeId { get; set; }
        public string? ReportText { get; set; }
        public DateTime? DateOfReport { get; set; }

        public virtual ReportType? ReportType { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual Video Video { get; set; } = null!;
    }
}
