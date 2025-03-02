using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Comment;
using VideoStreamingPlatform.Commons.Interfaces;
using VideoStreamingPlatform.Database;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Service
{
    public class CommentService : ICommentService
    {
        private readonly VideoStreamingPlatformContext _db;
        private readonly IRatingSystemCommentService _ratingSystemCommentService;
        public CommentService(VideoStreamingPlatformContext dbContext, IRatingSystemCommentService ratingSystemCommentService)
        {
            _db = dbContext;
            _ratingSystemCommentService = ratingSystemCommentService;
        }

        public Comment CreateComment(CreateCommentRequest request)
        {
            if (request.VideoId == null)
            {
                return null;
            }
            var checkVideo = _db.Videos.FirstOrDefault(v => v.VideoId == request.VideoId);
            if (checkVideo == null) { return null; }

            if (request.Content.IsNullOrEmpty())
            {
                return null;
            }

            var comment = new Comment
            {
                UserId = request.UserId,
                VideoId = request.VideoId,
                Content = request.Content,
                PostedDate = DateTime.UtcNow,           
            };
            _db.Comments.Add(comment);
            _db.SaveChanges();

            _ratingSystemCommentService.CreateRSC(comment.CommentId);

            return comment;
        }

        public bool DeleteComment(CommonDeleteRequest request)
        {
            var comment = _db.Comments.Include(c => c.RatingSystemComments)
                                       .Where(c => c.CommentId == request.Id)
                                       .FirstOrDefault();
            if (comment != null)
            {
                if (_ratingSystemCommentService.DeleteRSC(comment.RatingSystemComments))
                {
                    _db.Comments.Remove(comment);
                    _db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public Comment GetComment(int CommentId)
        {
            var comment = _db.Comments.FirstOrDefault(c => c.CommentId == CommentId);
            if (comment != null) 
            {
                return comment;
            } return null;
        }

        public Comment UpdateComment(UpdateCommentRequest request)
        {
            var comment = _db.Comments.FirstOrDefault(c => c.CommentId == request.CommentId);
            if (comment != null) 
            {
                comment.Content = request.Content;
                
                _db.Update(comment);
                _db.SaveChanges();

                return comment;
            } return null;
        }
    }
}
