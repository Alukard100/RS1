using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Requests.Advertisement
{
    public class GetAdvertisementsRequest
    {
        public int UserId { get; set; }
        public int VideoId { get; set; }
    }
}
