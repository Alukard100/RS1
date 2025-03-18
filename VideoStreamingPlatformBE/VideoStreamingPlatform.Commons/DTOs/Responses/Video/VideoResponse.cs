using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStreamingPlatform.Commons.DTOs.Responses.Video
{
    public class VideoResponse
    {
        public int VideoId { get; set; }
        public string VideoName { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
        public int CategoryId { get; set; }
        public string CategoryNmae { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int ClickCounter { get; set; }
        public int ThumbnailInfoId { get; set; }
        public byte[] ThumbnailPicture { get; set; }
    }
}
