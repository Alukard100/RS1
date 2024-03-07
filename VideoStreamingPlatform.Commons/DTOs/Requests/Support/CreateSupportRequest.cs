using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.Support
{
    public class CreateSupportRequest
    {
        public int UserId { get; set; }
        public string Body { get; set; }
        public DateTime TimeSent { get; set; }
    }
}
