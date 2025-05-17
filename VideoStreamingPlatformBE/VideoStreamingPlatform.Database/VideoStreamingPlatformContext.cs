using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VideoStreamingPlatform.Database.Models;

namespace VideoStreamingPlatform.Database
{
    public partial class VideoStreamingPlatformContext : DbContext
    {

        public VideoStreamingPlatformContext(DbContextOptions<VideoStreamingPlatformContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ActivePromoCode> ActivePromoCodes { get; set; } = null!;
        public virtual DbSet<Advertisement> Advertisements { get; set; } = null!;
        public virtual DbSet<Blog> Blogs { get; set; } = null!;
        public virtual DbSet<CardPayment> CardPayments { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<EmojiShow> EmojiShows { get; set; } = null!;
        public virtual DbSet<GroupMember> GroupMembers { get; set; } = null!;
        public virtual DbSet<Membership> Memberships { get; set; } = null!;
        public virtual DbSet<MessageBody> MessageBodies { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<NotificationType> NotificationTypes { get; set; } = null!;
        public virtual DbSet<Playlist> Playlists { get; set; } = null!;
        public virtual DbSet<PlaylistGroup> PlaylistGroups { get; set; } = null!;
        public virtual DbSet<RatingSystemComment> RatingSystemComments { get; set; } = null!;
        public virtual DbSet<RatingSystemVideo> RatingSystemVideos { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<ReportType> ReportTypes { get; set; } = null!;
        public virtual DbSet<SessionTable> SessionTables { get; set; } = null!;
        public virtual DbSet<Support> Supports { get; set; } = null!;
        public virtual DbSet<Synchronization> Synchronizations { get; set; } = null!;
        public virtual DbSet<ThumbnailInfo> ThumbnailInfos { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserType> UserTypes { get; set; } = null!;
        public virtual DbSet<UserValue> UserValues { get; set; } = null!;
        public virtual DbSet<Video> Videos { get; set; } = null!;
        public virtual DbSet<VideoStatistic> VideoStatistics { get; set; } = null!;
        public virtual DbSet<Wallet> Wallets { get; set; } = null!;
        public virtual DbSet<UserViews> UserViews { get; set; } = null!;
        public virtual DbSet<UserLikes> UserLikes { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActivePromoCode>(entity =>
            {
                entity.HasKey(e => e.PromoCodeId);

                entity.ToTable("activePromoCodes");

                entity.Property(e => e.PromoCodeId).HasColumnName("promoCodeID");

                entity.Property(e => e.Balance)
                    .HasColumnType("money")
                    .HasColumnName("balance");

                entity.Property(e => e.CodeValue)
                    .HasMaxLength(20)
                    .HasColumnName("codeValue");

                entity.Property(e => e.IsUsed)
                    .HasColumnName("isUsed")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Advertisement>(entity =>
            {
                entity.ToTable("advertisement");

                entity.Property(e => e.AdvertisementId).HasColumnName("advertisementID");

                entity.Property(e => e.AdvertisementPictureURL).HasColumnName("advertisementPictureURL");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.VideoId).HasColumnName("videoID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Advertisements)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Video)
                    .WithMany(p => p.Advertisements)
                    .HasForeignKey(d => d.VideoId);
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.ToTable("Blog");

                entity.Property(e => e.BlogId).HasColumnName("blogID");

                entity.Property(e => e.Content)
                    .HasMaxLength(500)
                    .HasColumnName("content");

                entity.Property(e => e.PictureURL).HasColumnName("pictureURL");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<CardPayment>(entity =>
            {
                entity.HasKey(e => e.PaymentId);

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.CardNumber)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.CardholderName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ExpirationDate).HasColumnType("date");

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CardPayments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");
                entity.Property(e => e.CategoryId).ValueGeneratedOnAdd();
                entity.Property(e => e.CategoryId).HasColumnName("categoryID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .HasColumnName("categoryName");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.CommentId).ValueGeneratedOnAdd();

                entity.Property(e => e.Content)
                    .HasMaxLength(300)
                    .HasColumnName("content");

                entity.Property(e => e.PostedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("postedDate");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.VideoId).HasColumnName("videoID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Video)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.VideoId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<EmojiShow>(entity =>
            {
                entity.ToTable("EmojiShow");

                entity.Property(e => e.EmojiShowId).HasColumnName("EmojiShowID");

                entity.Property(e => e.ClickCounter).HasDefaultValueSql("((0))");

                entity.Property(e => e.EmojiName)
                    .HasMaxLength(100)
                    .HasColumnName("emojiName");

                entity.Property(e => e.VideoId).HasColumnName("videoID");

                entity.HasOne(d => d.Video)
                    .WithMany(p => p.EmojiShows)
                    .HasForeignKey(d => d.VideoId);
            });

            modelBuilder.Entity<GroupMember>(entity =>
            {
                entity.Property(e => e.GroupMemberId)
                    .ValueGeneratedNever()
                    .HasColumnName("GroupMemberID");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupMembers)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupMembers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Membership>(entity =>
            {
                entity.ToTable("Membership");

                entity.Property(e => e.MembershipId).HasColumnName("membershipID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("endDate");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("startDate");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Memberships)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<MessageBody>(entity =>
            {
                entity.HasKey(e => new { e.MessagebodyId, e.MsgSenderId, e.MsgRecieverId })
                    .HasName("PK_Message");

                entity.ToTable("MessageBody");

                entity.Property(e => e.MessagebodyId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("messagebodyID");

                entity.Property(e => e.MsgSenderId).HasColumnName("msgSenderID");

                entity.Property(e => e.MsgRecieverId).HasColumnName("msgRecieverID");

                entity.Property(e => e.Body)
                    .HasMaxLength(255)
                    .HasColumnName("body");

                entity.Property(e => e.Seen).HasColumnName("seen");

                entity.Property(e => e.TimeSent)
                    .HasColumnType("datetime")
                    .HasColumnName("timeSent");

                entity.HasOne(d => d.MsgReciever)
                    .WithMany(p => p.MessageBodyMsgRecievers)
                    .HasForeignKey(d => d.MsgRecieverId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.MsgSender)
                    .WithMany(p => p.MessageBodyMsgSenders)
                    .HasForeignKey(d => d.MsgSenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.NotificationId).HasColumnName("notificationID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("createdAt");

                entity.Property(e => e.IsRead).HasColumnName("isRead");

                entity.Property(e => e.NotificationTypeId).HasColumnName("notificationTypeId");

                entity.Property(e => e.RecipientUserId).HasColumnName("recipientUserID");

                entity.Property(e => e.SenderUserId).HasColumnName("senderUserID");

                entity.HasOne(d => d.NotificationType)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.NotificationTypeId);

                entity.HasOne(d => d.RecipientUser)
                    .WithMany(p => p.NotificationRecipientUsers)
                    .HasForeignKey(d => d.RecipientUserId);

                entity.HasOne(d => d.SenderUser)
                    .WithMany(p => p.NotificationSenderUsers)
                    .HasForeignKey(d => d.SenderUserId);
            });

            modelBuilder.Entity<NotificationType>(entity =>
            {
                entity.ToTable("NotificationType");

                entity.Property(e => e.NotificationTypeId).HasColumnName("notificationTypeID");

                entity.Property(e => e.Value)
                    .HasMaxLength(100)
                    .HasColumnName("value");
            });

            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.ToTable("playlist");

                entity.Property(e => e.PlaylistId).HasColumnName("playlistID");

                entity.Property(e => e.IsPublic).HasColumnName("isPublic");

                entity.Property(e => e.PlaylistName)
                    .HasMaxLength(50)
                    .HasColumnName("playlistName")
                    .HasDefaultValueSql("('playlist')");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Playlists)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<PlaylistGroup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("playlistGroup");

                entity.Property(e => e.PlaylistId).HasColumnName("playlistID");

                entity.Property(e => e.VideoId).HasColumnName("videoID");

                entity.HasOne(d => d.Playlist)
                    .WithMany()
                    .HasForeignKey(d => d.PlaylistId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Video)
                    .WithMany()
                    .HasForeignKey(d => d.VideoId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<RatingSystemComment>(entity =>
            {
                entity.HasKey(e => e.RatingId)
                    .HasName("PK__ratingSy__2D290D49CF9B9C07");

                entity.ToTable("ratingSystemComment");

                entity.Property(e => e.RatingId).HasColumnName("ratingID");

                entity.Property(e => e.CommentId).HasColumnName("commentID");

                entity.Property(e => e.DislikeCount)
                    .HasColumnName("dislikeCount")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LikeCount)
                    .HasColumnName("likeCount")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Comment)
                    .WithOne(p => p.RatingSystemComments)
                    .HasForeignKey<RatingSystemComment>(d => d.CommentId);
            });

            modelBuilder.Entity<RatingSystemVideo>(entity =>
            {
                entity.HasKey(e => e.RatingId);

                entity.ToTable("ratingSystemVideo");

                entity.Property(e => e.RatingId).HasColumnName("ratingID");

                entity.Property(e => e.DislikeCount)
                    .HasColumnName("dislikeCount")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.LikeCount)
                    .HasColumnName("likeCount")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.VideoId).HasColumnName("videoID");

                entity.HasOne(d => d.Video)
                    .WithOne(p => p.RatingSystemVideos)
                    .HasForeignKey<RatingSystemVideo>(d => d.VideoId);
            });


            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("report");

                entity.Property(e => e.ReportId).HasColumnName("reportID");

                entity.Property(e => e.DateOfReport)
                    .HasColumnType("datetime")
                    .HasColumnName("dateOfReport");

                entity.Property(e => e.ReportText)
                    .HasMaxLength(150)
                    .HasColumnName("reportText");

                entity.Property(e => e.ReportTypeId).HasColumnName("reportTypeId");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.VideoId).HasColumnName("videoID");

                entity.HasOne(d => d.ReportType)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.ReportTypeId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Video)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.VideoId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ReportType>(entity =>
            {
                entity.ToTable("ReportType");

                entity.Property(e => e.ReportTypeId).HasColumnName("ReportTypeID");

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<SessionTable>(entity =>
            {
                entity.ToTable("sessionTable");

                entity.Property(e => e.SessionTableId).HasColumnName("sessionTableID");

                entity.Property(e => e.TimeStamp).HasColumnName("timeStamp");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.VideoId).HasColumnName("videoID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SessionTables)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Video)
                    .WithMany(p => p.SessionTables)
                    .HasForeignKey(d => d.VideoId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Support>(entity =>
            {
                entity.HasKey(e => new { e.SupportId, e.UserId })
                    .HasName("PK_SupportMessage");

                entity.ToTable("support");

                entity.Property(e => e.SupportId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("supportID");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.Body)
                    .HasMaxLength(255)
                    .HasColumnName("body");

                entity.Property(e => e.Seen).HasColumnName("seen");

                entity.Property(e => e.TimeSent)
                    .HasColumnType("datetime")
                    .HasColumnName("timeSent");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Supports)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Synchronization>(entity =>
            {
                entity.HasKey(e => e.GroupId);

                entity.ToTable("synchronization");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.GroupCode).HasMaxLength(10);

                entity.Property(e => e.SyncOwnerId).HasColumnName("SyncOwnerID");

                entity.Property(e => e.VideoId).HasColumnName("VideoID");

                entity.HasOne(d => d.SyncOwner)
                    .WithMany(p => p.Synchronizations)
                    .HasForeignKey(d => d.SyncOwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Video)
                    .WithMany(p => p.Synchronizations)
                    .HasForeignKey(d => d.VideoId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ThumbnailInfo>(entity =>
            {
                entity.ToTable("thumbnailInfo");

                entity.Property(e => e.ThumbnailInfoId).HasColumnName("thumbnailInfoID");

                entity.Property(e => e.ThumbnailPicture).HasColumnName("thumbnailPicture");

                entity.Property(e => e.VideoId).HasColumnName("videoID");

                entity.HasOne(d => d.Video)
                    .WithOne(p => p.ThumbnailInfos)
                    .HasForeignKey<ThumbnailInfo>(d => d.VideoId);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("transactions");

                entity.Property(e => e.TransactionId).HasColumnName("transactionID");

                entity.Property(e => e.DateOfTransaction)
                    .HasColumnType("datetime")
                    .HasColumnName("dateOfTransaction");

                entity.Property(e => e.TransactionAmount)
                    .HasColumnType("money")
                    .HasColumnName("transactionAmount");

                entity.Property(e => e.TransactionStatus).HasColumnName("transactionStatus");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("birthDate");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .HasColumnName("country");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");

                entity.Property(e => e.ProfilePicture).HasColumnName("profilePicture");

                entity.Property(e => e.SubscriberCount).HasColumnName("subscriberCount");

                entity.Property(e => e.Surname)
                    .HasMaxLength(20)
                    .HasColumnName("surname");

                entity.Property(e => e.TypeId).HasColumnName("typeID");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("userName");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.ToTable("UserType");

                entity.Property(e => e.TypeId).HasColumnName("typeID");

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<UserValue>(entity =>
            {
                entity.HasKey(e => e.UserValuesId);

                entity.ToTable("userValues");

                entity.Property(e => e.UserValuesId).HasColumnName("userValuesID");

                entity.Property(e => e.Email)
                    .HasMaxLength(80)
                    .HasColumnName("email");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(64)
                    .HasColumnName("passwordHash");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserValues)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Video>(entity =>
            {
                entity.ToTable("Video");

                entity.Property(e => e.VideoId).ValueGeneratedOnAdd();

                entity.Property(e => e.VideoId).HasColumnName("videoID");

                entity.Property(e => e.CategoryId).HasColumnName("categoryID");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.DurationInSecondes).HasColumnName("durationInSecondes");

                entity.Property(e => e.FilePath).HasColumnName("filePath");

                entity.Property(e => e.IsFree).HasColumnName("isFree");

                entity.Property(e => e.ResolutionType)
                    .HasMaxLength(10)
                    .HasColumnName("resolutionType");

                entity.Property(e => e.UploadDate)
                    .HasColumnType("datetime")
                    .HasColumnName("uploadDate");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.VideoName)
                    .HasMaxLength(255)
                    .HasColumnName("videoName");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Videos)
                    .HasForeignKey(d => d.CategoryId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Videos)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<VideoStatistic>(entity =>
            {
                entity.HasKey(e => e.VideoStatisticsId);

                entity.Property(e => e.VideoStatisticsId).HasColumnName("videoStatisticsId");

                entity.Property(e => e.ClickCounter).HasColumnName("clickCounter");

                entity.Property(e => e.VideoId).HasColumnName("videoID");

                entity.HasOne(d => d.Video)
                    .WithOne(p => p.VideoStatistics)
                    .HasForeignKey<VideoStatistic>(d => d.VideoId);
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("wallet");

                entity.Property(e => e.WalletId).HasColumnName("walletID");

                entity.Property(e => e.Balance)
                    .HasColumnType("money")
                    .HasColumnName("balance");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Wallets)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<UserViews>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.VideoId });

                entity.HasOne(e => e.User)
                        .WithMany(u => u.UserViews)
                        .HasForeignKey(e => e.UserId)
                        .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Video)
                        .WithMany(v => v.UserViews)
                        .HasForeignKey(e => e.VideoId)
                        .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<UserLikes>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.VideoId });

                entity.HasOne(e => e.User)
                        .WithMany(u => u.UserLikes)
                        .HasForeignKey(e => e.UserId)
                        .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Video)
                        .WithMany(v => v.UserLikes)
                        .HasForeignKey(e => e.VideoId)
                        .OnDelete(DeleteBehavior.Cascade);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
