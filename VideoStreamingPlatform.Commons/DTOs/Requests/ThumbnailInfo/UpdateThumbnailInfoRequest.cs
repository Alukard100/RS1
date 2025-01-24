using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.ThumbnailInfo
{
    public class UpdateThumbnailInfoRequest
    {
        public IFormFile file {  get; set; }
        public int ThumbnailInfoId { get; set; }
    }
}
