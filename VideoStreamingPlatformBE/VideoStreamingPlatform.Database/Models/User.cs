using System;
using System.Collections.Generic;

namespace VideoStreamingPlatform.Database.Models
{
    public partial class User
    {
        public User()
        {
            Advertisements = new HashSet<Advertisement>();
            Blogs = new HashSet<Blog>();
            CardPayments = new HashSet<CardPayment>();
            Comments = new HashSet<Comment>();
            GroupMembers = new HashSet<GroupMember>();
            Memberships = new HashSet<Membership>();
            MessageBodyMsgRecievers = new HashSet<MessageBody>();
            MessageBodyMsgSenders = new HashSet<MessageBody>();
            NotificationRecipientUsers = new HashSet<Notification>();
            NotificationSenderUsers = new HashSet<Notification>();
            Playlists = new HashSet<Playlist>();
            Reports = new HashSet<Report>();
            SessionTables = new HashSet<SessionTable>();
            Supports = new HashSet<Support>();
            Synchronizations = new HashSet<Synchronization>();
            Transactions = new HashSet<Transaction>();
            UserValues = new HashSet<UserValue>();
            Videos = new HashSet<Video>();
            Wallets = new HashSet<Wallet>();
            UserViews = new HashSet<UserViews>();
            UserLikes = new HashSet<UserLikes>();
        }

        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public string? Country { get; set; }
        public int? SubscriberCount { get; set; }
        public int TypeId { get; set; }

        public virtual UserType Type { get; set; } = null!;
        public virtual ICollection<Advertisement> Advertisements { get; set; }
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<CardPayment> CardPayments { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<GroupMember> GroupMembers { get; set; }
        public virtual ICollection<Membership> Memberships { get; set; }
        public virtual ICollection<MessageBody> MessageBodyMsgRecievers { get; set; }
        public virtual ICollection<MessageBody> MessageBodyMsgSenders { get; set; }
        public virtual ICollection<Notification> NotificationRecipientUsers { get; set; }
        public virtual ICollection<Notification> NotificationSenderUsers { get; set; }
        public virtual ICollection<Playlist> Playlists { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<SessionTable> SessionTables { get; set; }
        public virtual ICollection<Support> Supports { get; set; }
        public virtual ICollection<Synchronization> Synchronizations { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<UserValue> UserValues { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
        public virtual  ICollection<UserViews> UserViews { get; set; }
        public virtual ICollection<UserLikes> UserLikes { get; set; }
    }
}
