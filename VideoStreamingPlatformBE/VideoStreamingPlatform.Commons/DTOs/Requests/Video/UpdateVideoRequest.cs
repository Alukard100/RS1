using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.Video
{
    public class UpdateVideoRequest
    {
        public int VideoId { get; set; }
        public string VideoName { get; set; }
        public string Description { get; set; }
    }
}
