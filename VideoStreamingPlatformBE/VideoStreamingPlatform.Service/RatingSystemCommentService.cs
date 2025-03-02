using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database;
using VideoStreamingPlatform.Database.Models;
using static MediaToolkit.Model.Metadata;

namespace VideoStreamingPlatform.Service
{
    public class RatingSystemCommentService : IRatingSystemCommentService
    {
        private readonly VideoStreamingPlatformContext _db;
        public RatingSystemCommentService(VideoStreamingPlatformContext dbContext)
        {
            _db = dbContext;
        }

        public RatingSystemComment CreateRSC(int CommentId)
        {
            var checkedComment = _db.Comments.FirstOrDefault(c => c.CommentId == CommentId);
            if (checkedComment != null)
            {
                var newRSC = new RatingSystemComment()
                {
                    CommentId = CommentId,
                    LikeCount = 0,
                    DislikeCount = 0,
                    Comment = checkedComment                   
                };
                _db.RatingSystemComments.Add(newRSC);
                _db.SaveChanges();
                return newRSC;
            }
            return null;
        }

        public bool DeleteRSC(RatingSystemComment RatingId)
        {
            if (RatingId != null && RatingId.RatingId > 0)
            {
                _db.RatingSystemComments.Remove(RatingId);
                _db.SaveChanges();
                return true;
            }
            return true;
        }

        public RatingSystemComment GetRSC(int RatingId)
        {
            var checkedRating = _db.RatingSystemComments.FirstOrDefault(r => r.RatingId == RatingId);
            if (checkedRating != null)
            {
                return checkedRating;
            }
            return null;
        }
    }
}
