using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Responses.Blog
{
    public class GetBlogResponse
    {
        public int BlogId { get; set; }
        public int? UserId { get; set; }
        public string? Title { get; set; }
        public byte[]? Picture { get; set; }
        public string? Content { get; set; }
    }
}
