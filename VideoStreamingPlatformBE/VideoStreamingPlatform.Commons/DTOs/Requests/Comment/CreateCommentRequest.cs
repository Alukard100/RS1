using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.Comment
{
    public class CreateCommentRequest
    {
        public int VideoId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
    }
}
