using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Responses.Support
{
    public class GetSupportResponse
    {
        public int UserId { get; set; }
        public string Body { get; set; } = null!;
        public DateTime TimeSent { get; set; }
        public bool Seen { get; set; }
    }
}
