using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests.Video;
using VideoStreamingPlatform.Commons.DTOs.Responses.Video;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface IVideoService
    {
        public Video CreateVideo(CreateVideoRequest request, HttpContext httpContext);
        public bool DeleteVideo(int VideoId);
        public List<VideoResponse> GetVideo(int VideoId);
        public Stream StreamVideo(int VideoId);
        public Video UpdateVideo(UpdateVideoRequest request);
    }
}
