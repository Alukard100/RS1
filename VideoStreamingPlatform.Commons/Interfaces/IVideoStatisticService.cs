using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface IVideoStatisticService
    {
        public VideoStatistic CreateStatistic(int VideoId);
        public bool DeleteStatistic(VideoStatistic VideoStatistic);
        public VideoStatistic GetStatistic(int VideoStatisticId);
    }
}
