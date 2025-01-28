using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.Comment
{
    public class UpdateCommentRequest
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
    }
}
