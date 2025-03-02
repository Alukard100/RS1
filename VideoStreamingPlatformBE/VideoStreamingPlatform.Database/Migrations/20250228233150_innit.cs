using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoStreamingPlatform.Database.Migrations
{
    /// <inheritdoc />
    public partial class innit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "activePromoCodes",
                columns: table => new
                {
                    promoCodeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    codeValue = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    isUsed = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    balance = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activePromoCodes", x => x.promoCodeID);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    categoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.categoryID);
                });

            migrationBuilder.CreateTable(
                name: "NotificationType",
                columns: table => new
                {
                    notificationTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationType", x => x.notificationTypeID);
                });

            migrationBuilder.CreateTable(
                name: "ReportType",
                columns: table => new
                {
                    ReportTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportType", x => x.ReportTypeID);
                });

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    typeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.typeID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    userID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    surname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    userName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    birthDate = table.Column<DateTime>(type: "date", nullable: true),
                    profilePicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    subscriberCount = table.Column<int>(type: "int", nullable: true),
                    typeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.userID);
                    table.ForeignKey(
                        name: "FK_User_UserType_typeID",
                        column: x => x.typeID,
                        principalTable: "UserType",
                        principalColumn: "typeID");
                });

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    blogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: true),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    pictureURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.blogID);
                    table.ForeignKey(
                        name: "FK_Blog_User_userID",
                        column: x => x.userID,
                        principalTable: "User",
                        principalColumn: "userID");
                });

            migrationBuilder.CreateTable(
                name: "CardPayments",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNumber = table.Column<string>(type: "varchar(16)", unicode: false, maxLength: 16, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "date", nullable: false),
                    CardholderName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    userID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardPayments", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_CardPayments_User_userID",
                        column: x => x.userID,
                        principalTable: "User",
                        principalColumn: "userID");
                });

            migrationBuilder.CreateTable(
                name: "Membership",
                columns: table => new
                {
                    membershipID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: true),
                    startDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    endDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membership", x => x.membershipID);
                    table.ForeignKey(
                        name: "FK_Membership_User_userID",
                        column: x => x.userID,
                        principalTable: "User",
                        principalColumn: "userID");
                });

            migrationBuilder.CreateTable(
                name: "MessageBody",
                columns: table => new
                {
                    messagebodyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    msgSenderID = table.Column<int>(type: "int", nullable: false),
                    msgRecieverID = table.Column<int>(type: "int", nullable: false),
                    body = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    timeSent = table.Column<DateTime>(type: "datetime", nullable: false),
                    seen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => new { x.messagebodyID, x.msgSenderID, x.msgRecieverID });
                    table.ForeignKey(
                        name: "FK_MessageBody_User_msgRecieverID",
                        column: x => x.msgRecieverID,
                        principalTable: "User",
                        principalColumn: "userID");
                    table.ForeignKey(
                        name: "FK_MessageBody_User_msgSenderID",
                        column: x => x.msgSenderID,
                        principalTable: "User",
                        principalColumn: "userID");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    notificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    recipientUserID = table.Column<int>(type: "int", nullable: true),
                    senderUserID = table.Column<int>(type: "int", nullable: true),
                    notificationTypeId = table.Column<int>(type: "int", nullable: true),
                    isRead = table.Column<bool>(type: "bit", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.notificationID);
                    table.ForeignKey(
                        name: "FK_Notifications_NotificationType_notificationTypeId",
                        column: x => x.notificationTypeId,
                        principalTable: "NotificationType",
                        principalColumn: "notificationTypeID");
                    table.ForeignKey(
                        name: "FK_Notifications_User_recipientUserID",
                        column: x => x.recipientUserID,
                        principalTable: "User",
                        principalColumn: "userID");
                    table.ForeignKey(
                        name: "FK_Notifications_User_senderUserID",
                        column: x => x.senderUserID,
                        principalTable: "User",
                        principalColumn: "userID");
                });

            migrationBuilder.CreateTable(
                name: "playlist",
                columns: table => new
                {
                    playlistID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: false),
                    playlistName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValueSql: "('playlist')"),
                    isPublic = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playlist", x => x.playlistID);
                    table.ForeignKey(
                        name: "FK_playlist_User_userID",
                        column: x => x.userID,
                        principalTable: "User",
                        principalColumn: "userID");
                });

            migrationBuilder.CreateTable(
                name: "support",
                columns: table => new
                {
                    supportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: false),
                    body = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    timeSent = table.Column<DateTime>(type: "datetime", nullable: false),
                    seen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportMessage", x => new { x.supportID, x.userID });
                    table.ForeignKey(
                        name: "FK_support_User_userID",
                        column: x => x.userID,
                        principalTable: "User",
                        principalColumn: "userID");
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                columns: table => new
                {
                    transactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: false),
                    dateOfTransaction = table.Column<DateTime>(type: "datetime", nullable: true),
                    transactionAmount = table.Column<decimal>(type: "money", nullable: false),
                    transactionStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.transactionID);
                    table.ForeignKey(
                        name: "FK_transactions_User_userID",
                        column: x => x.userID,
                        principalTable: "User",
                        principalColumn: "userID");
                });

            migrationBuilder.CreateTable(
                name: "userValues",
                columns: table => new
                {
                    userValuesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: true),
                    email = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    passwordHash = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userValues", x => x.userValuesID);
                    table.ForeignKey(
                        name: "FK_userValues_User_userID",
                        column: x => x.userID,
                        principalTable: "User",
                        principalColumn: "userID");
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    videoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    videoName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    filePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    resolutionType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    uploadDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    durationInSecondes = table.Column<int>(type: "int", nullable: false),
                    isFree = table.Column<bool>(type: "bit", nullable: false),
                    userID = table.Column<int>(type: "int", nullable: false),
                    categoryID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.videoID);
                    table.ForeignKey(
                        name: "FK_Video_Category_categoryID",
                        column: x => x.categoryID,
                        principalTable: "Category",
                        principalColumn: "categoryID");
                    table.ForeignKey(
                        name: "FK_Video_User_userID",
                        column: x => x.userID,
                        principalTable: "User",
                        principalColumn: "userID");
                });

            migrationBuilder.CreateTable(
                name: "wallet",
                columns: table => new
                {
                    walletID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    balance = table.Column<decimal>(type: "money", nullable: true),
                    userID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wallet", x => x.walletID);
                    table.ForeignKey(
                        name: "FK_wallet_User_userID",
                        column: x => x.userID,
                        principalTable: "User",
                        principalColumn: "userID");
                });

            migrationBuilder.CreateTable(
                name: "advertisement",
                columns: table => new
                {
                    advertisementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: false),
                    videoID = table.Column<int>(type: "int", nullable: true),
                    advertisementPictureURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advertisement", x => x.advertisementID);
                    table.ForeignKey(
                        name: "FK_advertisement_User_userID",
                        column: x => x.userID,
                        principalTable: "User",
                        principalColumn: "userID");
                    table.ForeignKey(
                        name: "FK_advertisement_Video_videoID",
                        column: x => x.videoID,
                        principalTable: "Video",
                        principalColumn: "videoID");
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: false),
                    videoID = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    postedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_User_userID",
                        column: x => x.userID,
                        principalTable: "User",
                        principalColumn: "userID");
                    table.ForeignKey(
                        name: "FK_Comments_Video_videoID",
                        column: x => x.videoID,
                        principalTable: "Video",
                        principalColumn: "videoID");
                });

            migrationBuilder.CreateTable(
                name: "EmojiShow",
                columns: table => new
                {
                    EmojiShowID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    emojiName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    videoID = table.Column<int>(type: "int", nullable: true),
                    ClickCounter = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmojiShow", x => x.EmojiShowID);
                    table.ForeignKey(
                        name: "FK_EmojiShow_Video_videoID",
                        column: x => x.videoID,
                        principalTable: "Video",
                        principalColumn: "videoID");
                });

            migrationBuilder.CreateTable(
                name: "playlistGroup",
                columns: table => new
                {
                    playlistID = table.Column<int>(type: "int", nullable: false),
                    videoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_playlistGroup_Video_videoID",
                        column: x => x.videoID,
                        principalTable: "Video",
                        principalColumn: "videoID");
                    table.ForeignKey(
                        name: "FK_playlistGroup_playlist_playlistID",
                        column: x => x.playlistID,
                        principalTable: "playlist",
                        principalColumn: "playlistID");
                });

            migrationBuilder.CreateTable(
                name: "ratingSystemVideo",
                columns: table => new
                {
                    ratingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    videoID = table.Column<int>(type: "int", nullable: false),
                    likeCount = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    dislikeCount = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ratingSystemVideo", x => x.ratingID);
                    table.ForeignKey(
                        name: "FK_ratingSystemVideo_Video_videoID",
                        column: x => x.videoID,
                        principalTable: "Video",
                        principalColumn: "videoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "report",
                columns: table => new
                {
                    reportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: false),
                    videoID = table.Column<int>(type: "int", nullable: false),
                    reportTypeId = table.Column<int>(type: "int", nullable: true),
                    reportText = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    dateOfReport = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report", x => x.reportID);
                    table.ForeignKey(
                        name: "FK_report_ReportType_reportTypeId",
                        column: x => x.reportTypeId,
                        principalTable: "ReportType",
                        principalColumn: "ReportTypeID");
                    table.ForeignKey(
                        name: "FK_report_User_userID",
                        column: x => x.userID,
                        principalTable: "User",
                        principalColumn: "userID");
                    table.ForeignKey(
                        name: "FK_report_Video_videoID",
                        column: x => x.videoID,
                        principalTable: "Video",
                        principalColumn: "videoID");
                });

            migrationBuilder.CreateTable(
                name: "sessionTable",
                columns: table => new
                {
                    sessionTableID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: false),
                    videoID = table.Column<int>(type: "int", nullable: false),
                    timeStamp = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sessionTable", x => x.sessionTableID);
                    table.ForeignKey(
                        name: "FK_sessionTable_User_userID",
                        column: x => x.userID,
                        principalTable: "User",
                        principalColumn: "userID");
                    table.ForeignKey(
                        name: "FK_sessionTable_Video_videoID",
                        column: x => x.videoID,
                        principalTable: "Video",
                        principalColumn: "videoID");
                });

            migrationBuilder.CreateTable(
                name: "synchronization",
                columns: table => new
                {
                    GroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SyncOwnerID = table.Column<int>(type: "int", nullable: false),
                    VideoID = table.Column<int>(type: "int", nullable: false),
                    GroupCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_synchronization", x => x.GroupID);
                    table.ForeignKey(
                        name: "FK_synchronization_User_SyncOwnerID",
                        column: x => x.SyncOwnerID,
                        principalTable: "User",
                        principalColumn: "userID");
                    table.ForeignKey(
                        name: "FK_synchronization_Video_VideoID",
                        column: x => x.VideoID,
                        principalTable: "Video",
                        principalColumn: "videoID");
                });

            migrationBuilder.CreateTable(
                name: "thumbnailInfo",
                columns: table => new
                {
                    thumbnailInfoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    thumbnailPicture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    videoID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thumbnailInfo", x => x.thumbnailInfoID);
                    table.ForeignKey(
                        name: "FK_thumbnailInfo_Video_videoID",
                        column: x => x.videoID,
                        principalTable: "Video",
                        principalColumn: "videoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoStatistics",
                columns: table => new
                {
                    videoStatisticsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    videoID = table.Column<int>(type: "int", nullable: true),
                    clickCounter = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoStatistics", x => x.videoStatisticsId);
                    table.ForeignKey(
                        name: "FK_VideoStatistics_Video_videoID",
                        column: x => x.videoID,
                        principalTable: "Video",
                        principalColumn: "videoID");
                });

            migrationBuilder.CreateTable(
                name: "ratingSystemComment",
                columns: table => new
                {
                    ratingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    commentID = table.Column<int>(type: "int", nullable: false),
                    likeCount = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    dislikeCount = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ratingSy__2D290D49CF9B9C07", x => x.ratingID);
                    table.ForeignKey(
                        name: "FK_ratingSystemComment_Comments_commentID",
                        column: x => x.commentID,
                        principalTable: "Comments",
                        principalColumn: "CommentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupMembers",
                columns: table => new
                {
                    GroupMemberID = table.Column<int>(type: "int", nullable: false),
                    GroupID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMembers", x => x.GroupMemberID);
                    table.ForeignKey(
                        name: "FK_GroupMembers_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "userID");
                    table.ForeignKey(
                        name: "FK_GroupMembers_synchronization_GroupID",
                        column: x => x.GroupID,
                        principalTable: "synchronization",
                        principalColumn: "GroupID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_advertisement_userID",
                table: "advertisement",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_advertisement_videoID",
                table: "advertisement",
                column: "videoID");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_userID",
                table: "Blog",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_CardPayments_userID",
                table: "CardPayments",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_userID",
                table: "Comments",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_videoID",
                table: "Comments",
                column: "videoID");

            migrationBuilder.CreateIndex(
                name: "IX_EmojiShow_videoID",
                table: "EmojiShow",
                column: "videoID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_GroupID",
                table: "GroupMembers",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMembers_UserID",
                table: "GroupMembers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Membership_userID",
                table: "Membership",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_MessageBody_msgRecieverID",
                table: "MessageBody",
                column: "msgRecieverID");

            migrationBuilder.CreateIndex(
                name: "IX_MessageBody_msgSenderID",
                table: "MessageBody",
                column: "msgSenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_notificationTypeId",
                table: "Notifications",
                column: "notificationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_recipientUserID",
                table: "Notifications",
                column: "recipientUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_senderUserID",
                table: "Notifications",
                column: "senderUserID");

            migrationBuilder.CreateIndex(
                name: "IX_playlist_userID",
                table: "playlist",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_playlistGroup_playlistID",
                table: "playlistGroup",
                column: "playlistID");

            migrationBuilder.CreateIndex(
                name: "IX_playlistGroup_videoID",
                table: "playlistGroup",
                column: "videoID");

            migrationBuilder.CreateIndex(
                name: "IX_ratingSystemComment_commentID",
                table: "ratingSystemComment",
                column: "commentID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ratingSystemVideo_videoID",
                table: "ratingSystemVideo",
                column: "videoID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_report_reportTypeId",
                table: "report",
                column: "reportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_report_userID",
                table: "report",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_report_videoID",
                table: "report",
                column: "videoID");

            migrationBuilder.CreateIndex(
                name: "IX_sessionTable_userID",
                table: "sessionTable",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_sessionTable_videoID",
                table: "sessionTable",
                column: "videoID");

            migrationBuilder.CreateIndex(
                name: "IX_support_userID",
                table: "support",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_synchronization_SyncOwnerID",
                table: "synchronization",
                column: "SyncOwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_synchronization_VideoID",
                table: "synchronization",
                column: "VideoID");

            migrationBuilder.CreateIndex(
                name: "IX_thumbnailInfo_videoID",
                table: "thumbnailInfo",
                column: "videoID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_transactions_userID",
                table: "transactions",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_User_typeID",
                table: "User",
                column: "typeID");

            migrationBuilder.CreateIndex(
                name: "IX_userValues_userID",
                table: "userValues",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_Video_categoryID",
                table: "Video",
                column: "categoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Video_userID",
                table: "Video",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_VideoStatistics_videoID",
                table: "VideoStatistics",
                column: "videoID",
                unique: true,
                filter: "[videoID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_wallet_userID",
                table: "wallet",
                column: "userID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "activePromoCodes");

            migrationBuilder.DropTable(
                name: "advertisement");

            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "CardPayments");

            migrationBuilder.DropTable(
                name: "EmojiShow");

            migrationBuilder.DropTable(
                name: "GroupMembers");

            migrationBuilder.DropTable(
                name: "Membership");

            migrationBuilder.DropTable(
                name: "MessageBody");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "playlistGroup");

            migrationBuilder.DropTable(
                name: "ratingSystemComment");

            migrationBuilder.DropTable(
                name: "ratingSystemVideo");

            migrationBuilder.DropTable(
                name: "report");

            migrationBuilder.DropTable(
                name: "sessionTable");

            migrationBuilder.DropTable(
                name: "support");

            migrationBuilder.DropTable(
                name: "thumbnailInfo");

            migrationBuilder.DropTable(
                name: "transactions");

            migrationBuilder.DropTable(
                name: "userValues");

            migrationBuilder.DropTable(
                name: "VideoStatistics");

            migrationBuilder.DropTable(
                name: "wallet");

            migrationBuilder.DropTable(
                name: "synchronization");

            migrationBuilder.DropTable(
                name: "NotificationType");

            migrationBuilder.DropTable(
                name: "playlist");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "ReportType");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserType");
        }
    }
}
