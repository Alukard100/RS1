using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface IRatingSystemCommentService
    {
        public RatingSystemComment CreateRSC(int CommentId);
        public bool DeleteRSC(RatingSystemComment RatingId);
        public RatingSystemComment GetRSC(int RatingId);
    }
}
