using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.Blog
{
    public class CreateBlogRequest
    {
        public int? UserId { get; set; }
        public string? Title { get; set; }
        public byte[]? Picture { get; set; }
        public string? Content { get; set; }
    }
}
