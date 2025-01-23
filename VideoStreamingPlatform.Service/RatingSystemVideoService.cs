using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class RatingSystemVideoService : IRatingSystemVideoService
    {
        private readonly VideoStreamingPlatformContext _db;
        public RatingSystemVideoService(VideoStreamingPlatformContext dbContext)
        {
            _db = dbContext;
        }

        public RatingSystemVideo CreateRSV(int VideoId)
        {
            var checkVideo = _db.Videos.FirstOrDefault(v => v.VideoId == VideoId);
            if (checkVideo != null) 
            {
                var newRSV = new RatingSystemVideo()
                {
                    VideoId = VideoId,
                    LikeCount = 0,
                    DislikeCount = 0,
                    Video = checkVideo
                };
                _db.RatingSystemVideos.Add(newRSV);
                _db.SaveChanges();
                return newRSV;
            } return null;
        }

        public bool DeleteRSV(RatingSystemVideo RatingId)
        {
            if (RatingId != null && RatingId.RatingId > 0)
            {
                _db.RatingSystemVideos.Remove(RatingId);
                _db.SaveChanges();
                return true;
            } return true;
        }

        public RatingSystemVideo GetRSV(int RatingId)
        {
            var checkedRating = _db.RatingSystemVideos.FirstOrDefault(r => r.RatingId == RatingId);
            if (checkedRating != null)
            {
                return checkedRating;
            } return null;
        }
    }
}
