using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.Advertisement
{
    public class UpdateAdvertisementRequest
    {
        public int AdvertisementId { get; set; }
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public byte[] AdvertisementPicture { get; set; }

    }
}
