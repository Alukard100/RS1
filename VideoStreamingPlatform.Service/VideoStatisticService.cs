using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database.Models;
using static MediaToolkit.Model.Metadata;

namespace VideoStreamingPlatform.Service
{
    public class VideoStatisticService : IVideoStatisticService
    {
        private readonly VideoStreamingPlatformContext _db;
        public VideoStatisticService(VideoStreamingPlatformContext dbContext)
        {
            _db = dbContext;
        }
        public VideoStatistic CreateStatistic(int VideoId)
        {
            var checkVideo = _db.Videos.Where(v => v.VideoId == VideoId).FirstOrDefault();
            if (checkVideo != null)
            {
                var newStatistic = new VideoStatistic()
                {
                    VideoId = VideoId,
                    ClickCounter = 0,
                    Video = checkVideo
                };
                _db.VideoStatistics.Add(newStatistic);
                _db.SaveChanges();
                return newStatistic;
            } return null;
        }

        public bool DeleteStatistic(VideoStatistic VideoStatistic)
        {
            if (VideoStatistic != null && VideoStatistic.VideoId > 0)
            {
                _db.VideoStatistics.Remove(VideoStatistic);
                _db.SaveChanges();
                return true;
            } return true;
        }

        public VideoStatistic GetStatistic(int VideoStatisticId)
        {
            var checkStatistic = _db.VideoStatistics.Where(v => v.VideoStatisticsId == VideoStatisticId).FirstOrDefault();
            if (checkStatistic != null)
            {
                return checkStatistic;
            }
            return null;
        }
    }
}
