using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class Comment
    {
        public Comment()
        {
            RatingSystemComments = new HashSet<RatingSystemComment>();
        }

        public int CommentId { get; set; }
        public int UserId { get; set; }
        public int VideoId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime? PostedDate { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Video Video { get; set; } = null!;
        public virtual ICollection<RatingSystemComment> RatingSystemComments { get; set; }
    }
}
