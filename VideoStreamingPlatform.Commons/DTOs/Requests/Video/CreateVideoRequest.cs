using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.Video
{
    public class CreateVideoRequest
    {
        public IFormFile file {  get; set; }
        public string VideoName { get; set; }
        public string Description { get; set; }
        public bool IsFree { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
    }
}
