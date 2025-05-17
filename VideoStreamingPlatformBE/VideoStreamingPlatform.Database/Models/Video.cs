using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class Video
    {
        public Video()
        {
            Advertisements = new HashSet<Advertisement>();
            Comments = new HashSet<Comment>();
            EmojiShows = new HashSet<EmojiShow>();
            Reports = new HashSet<Report>();
            SessionTables = new HashSet<SessionTable>();
            Synchronizations = new HashSet<Synchronization>();
            UserViews = new HashSet<UserViews>();
            UserLikes = new HashSet<UserLikes>();

        }

        public int VideoId { get; set; }
        public string VideoName { get; set; } = null!;
        public string FilePath { get; set; }
        public string Description { get; set; }
        public string ResolutionType { get; set; }
        public DateTime UploadDate { get; set; }
        public int DurationInSecondes { get; set; }
        public bool IsFree { get; set; }
        public int UserId { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Advertisement> Advertisements { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<EmojiShow> EmojiShows { get; set; }
        public virtual RatingSystemVideo RatingSystemVideos { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<SessionTable> SessionTables { get; set; }
        public virtual ICollection<Synchronization> Synchronizations { get; set; }
        public virtual ThumbnailInfo ThumbnailInfos { get; set; }
        public virtual VideoStatistic VideoStatistics { get; set; }
        public virtual ICollection<UserViews> UserViews { get; set; } 
        public virtual ICollection<UserLikes> UserLikes { get; set; }
    }
}
