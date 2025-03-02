using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.Advertisement
{
    public class CreateAdvertisementRequest
    {
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public IFormFile AdvertisementPicture { get; set; }
    }
}
