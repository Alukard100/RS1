using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests.ThumbnailInfo;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface IThumbnailInfoService
    {
        public ThumbnailInfo CreateThumbnail(int VideoId);
        public ThumbnailInfo UpdateThumbnail(UpdateThumbnailInfoRequest request);
        public bool DeleteThumbnail(ThumbnailInfo Thumbnail);
        public ThumbnailInfo GetThumbnail(int ThumbnailId);
    }
}
