using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStreamingPlatform.Commons.DTOs.Requests;
using VideoStreamingPlatform.Commons.DTOs.Requests.Comment;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Commons.Interfaces
{
    public interface ICommentService
    {
        public Comment CreateComment(CreateCommentRequest request);
        public Comment GetComment(int CommentId);
        public bool DeleteComment(CommonDeleteRequest request);
        public Comment UpdateComment(UpdateCommentRequest request);
    }
}
