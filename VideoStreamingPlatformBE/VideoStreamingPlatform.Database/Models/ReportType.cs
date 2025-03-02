using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class ReportType
    {
        public ReportType()
        {
            Reports = new HashSet<Report>();
        }

        public int ReportTypeId { get; set; }
        public string? Type { get; set; }

        public virtual ICollection<Report> Reports { get; set; }
    }
}
