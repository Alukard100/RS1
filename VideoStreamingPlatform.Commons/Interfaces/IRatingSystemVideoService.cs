using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface IRatingSystemVideoService
    {
        public RatingSystemVideo CreateRSV(int VideoId);
        public bool DeleteRSV(RatingSystemVideo RatingId);
        public RatingSystemVideo GetRSV(int RatingId);
    }
}
