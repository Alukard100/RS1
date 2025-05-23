USE [master]
GO
/****** Object:  Database [VideoStreamingPlatform]    Script Date: 05-Sep-24 12:09:05 AM ******/
CREATE DATABASE [VideoStreamingPlatform]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VideoStreamingPlatform', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\VideoStreamingPlatform.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'VideoStreamingPlatform_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\VideoStreamingPlatform_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [VideoStreamingPlatform] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VideoStreamingPlatform].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VideoStreamingPlatform] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VideoStreamingPlatform] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VideoStreamingPlatform] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VideoStreamingPlatform] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VideoStreamingPlatform] SET ARITHABORT OFF 
GO
ALTER DATABASE [VideoStreamingPlatform] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [VideoStreamingPlatform] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VideoStreamingPlatform] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VideoStreamingPlatform] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VideoStreamingPlatform] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VideoStreamingPlatform] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VideoStreamingPlatform] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VideoStreamingPlatform] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VideoStreamingPlatform] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VideoStreamingPlatform] SET  ENABLE_BROKER 
GO
ALTER DATABASE [VideoStreamingPlatform] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VideoStreamingPlatform] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VideoStreamingPlatform] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VideoStreamingPlatform] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VideoStreamingPlatform] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VideoStreamingPlatform] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VideoStreamingPlatform] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VideoStreamingPlatform] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [VideoStreamingPlatform] SET  MULTI_USER 
GO
ALTER DATABASE [VideoStreamingPlatform] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VideoStreamingPlatform] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VideoStreamingPlatform] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VideoStreamingPlatform] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VideoStreamingPlatform] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VideoStreamingPlatform] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [VideoStreamingPlatform] SET QUERY_STORE = OFF
GO
USE [VideoStreamingPlatform]
GO
/****** Object:  Table [dbo].[activePromoCodes]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[activePromoCodes](
	[promoCodeID] [int] IDENTITY(1,1) NOT NULL,
	[codeValue] [nvarchar](20) NOT NULL,
	[isUsed] [bit] NULL,
	[balance] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[promoCodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[codeValue] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[advertisement]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[advertisement](
	[advertisementID] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NOT NULL,
	[videoID] [int] NULL,
	[advertisementPictureURL] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[advertisementID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blog]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog](
	[blogID] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NULL,
	[title] [nvarchar](50) NULL,
	[pictureURL] [varchar](500) NULL,
	[content] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[blogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CardPayments]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CardPayments](
	[PaymentID] [int] IDENTITY(1,1) NOT NULL,
	[CardNumber] [varchar](16) NOT NULL,
	[ExpirationDate] [date] NOT NULL,
	[CardholderName] [varchar](255) NOT NULL,
	[Amount] [money] NOT NULL,
	[TransactionDate] [datetime] NOT NULL,
	[userID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[categoryID] [int] IDENTITY(1,1) NOT NULL,
	[categoryName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[categoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[commentID] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NOT NULL,
	[videoID] [int] NOT NULL,
	[content] [nvarchar](300) NOT NULL,
	[postedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[commentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmojiShow]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmojiShow](
	[EmojiShowID] [int] IDENTITY(1,1) NOT NULL,
	[emojiName] [nvarchar](100) NULL,
	[videoID] [int] NULL,
	[ClickCounter] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EmojiShowID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupMembers]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupMembers](
	[GroupMemberID] [int] NOT NULL,
	[GroupID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupMemberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Membership]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Membership](
	[membershipID] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NULL,
	[startDate] [datetime] NOT NULL,
	[endDate] [datetime] NULL,
	[createdAt] [datetime] NULL,
	[isActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[membershipID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageBody]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageBody](
	[messagebodyID] [int] IDENTITY(1,1) NOT NULL,
	[msgSenderID] [int] NOT NULL,
	[msgRecieverID] [int] NOT NULL,
	[body] [nvarchar](255) NOT NULL,
	[timeSent] [datetime] NOT NULL,
	[seen] [bit] NOT NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[messagebodyID] ASC,
	[msgSenderID] ASC,
	[msgRecieverID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[notificationID] [int] IDENTITY(1,1) NOT NULL,
	[recipientUserID] [int] NULL,
	[senderUserID] [int] NULL,
	[notificationTypeId] [int] NULL,
	[isRead] [bit] NULL,
	[createdAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[notificationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificationType]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationType](
	[notificationTypeID] [int] IDENTITY(1,1) NOT NULL,
	[value] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[notificationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[playlist]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[playlist](
	[playlistID] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NOT NULL,
	[playlistName] [nvarchar](50) NULL,
	[isPublic] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[playlistID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[playlistGroup]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[playlistGroup](
	[playlistID] [int] NOT NULL,
	[videoID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ratingSystemComment]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ratingSystemComment](
	[ratingID] [int] IDENTITY(1,1) NOT NULL,
	[commentID] [int] NOT NULL,
	[likeCount] [int] NULL,
	[dislikeCount] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ratingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ratingSystemVideo]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ratingSystemVideo](
	[ratingID] [int] IDENTITY(1,1) NOT NULL,
	[videoID] [int] NOT NULL,
	[likeCount] [int] NULL,
	[dislikeCount] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ratingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[report]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[report](
	[reportID] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NOT NULL,
	[videoID] [int] NOT NULL,
	[reportTypeId] [int] NULL,
	[reportText] [nvarchar](150) NULL,
	[dateOfReport] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[reportID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReportType]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportType](
	[ReportTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ReportTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sessionTable]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sessionTable](
	[sessionTableID] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NOT NULL,
	[videoID] [int] NOT NULL,
	[timeStamp] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[sessionTableID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[support]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[support](
	[supportID] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NOT NULL,
	[body] [nvarchar](255) NOT NULL,
	[timeSent] [datetime] NOT NULL,
	[seen] [bit] NOT NULL,
 CONSTRAINT [PK_SupportMessage] PRIMARY KEY CLUSTERED 
(
	[supportID] ASC,
	[userID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[synchronization]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[synchronization](
	[GroupID] [int] IDENTITY(1,1) NOT NULL,
	[SyncOwnerID] [int] NOT NULL,
	[VideoID] [int] NOT NULL,
	[GroupCode] [nvarchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[GroupCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[thumbnailInfo]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[thumbnailInfo](
	[thumbnailInfoID] [int] IDENTITY(1,1) NOT NULL,
	[thumbnailPicture] [varbinary](max) NULL,
	[videoID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[thumbnailInfoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[transactions]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transactions](
	[transactionID] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NOT NULL,
	[dateOfTransaction] [datetime] NULL,
	[transactionAmount] [money] NOT NULL,
	[transactionStatus] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[transactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[userID] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](20) NOT NULL,
	[surname] [nvarchar](20) NOT NULL,
	[userName] [nvarchar](50) NOT NULL,
	[birthDate] [date] NULL,
	[profilePicture] [varbinary](max) NULL,
	[country] [nvarchar](50) NULL,
	[subscriberCount] [int] NULL,
	[typeID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[userID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserType](
	[typeID] [int] IDENTITY(1,1) NOT NULL,
	[type] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[typeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[userValues]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[userValues](
	[userValuesID] [int] IDENTITY(1,1) NOT NULL,
	[userID] [int] NULL,
	[email] [nvarchar](80) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[passwordHash] [nvarchar](64) NULL,
PRIMARY KEY CLUSTERED 
(
	[userValuesID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Video]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Video](
	[videoID] [int] IDENTITY(1,1) NOT NULL,
	[videoName] [nvarchar](255) NOT NULL,
	[filePath] [nvarchar](max) NULL,
	[description] [nvarchar](500) NULL,
	[resolutionType] [nvarchar](10) NULL,
	[uploadDate] [datetime] NULL,
	[durationInSecondes] [int] NULL,
	[isFree] [bit] NOT NULL,
	[userID] [int] NOT NULL,
	[categoryID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[videoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VideoStatistics]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VideoStatistics](
	[videoStatisticsId] [int] IDENTITY(1,1) NOT NULL,
	[videoID] [int] NULL,
	[clickCounter] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[videoStatisticsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[wallet]    Script Date: 05-Sep-24 12:09:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[wallet](
	[walletID] [int] IDENTITY(1,1) NOT NULL,
	[balance] [money] NULL,
	[userID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[walletID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[activePromoCodes] ADD  DEFAULT ((0)) FOR [isUsed]
GO
ALTER TABLE [dbo].[EmojiShow] ADD  DEFAULT ((0)) FOR [ClickCounter]
GO
ALTER TABLE [dbo].[MessageBody] ADD  DEFAULT ((0)) FOR [seen]
GO
ALTER TABLE [dbo].[playlist] ADD  DEFAULT ('playlist') FOR [playlistName]
GO
ALTER TABLE [dbo].[ratingSystemComment] ADD  DEFAULT ((0)) FOR [likeCount]
GO
ALTER TABLE [dbo].[ratingSystemComment] ADD  DEFAULT ((0)) FOR [dislikeCount]
GO
ALTER TABLE [dbo].[ratingSystemVideo] ADD  DEFAULT ((0)) FOR [likeCount]
GO
ALTER TABLE [dbo].[ratingSystemVideo] ADD  DEFAULT ((0)) FOR [dislikeCount]
GO
ALTER TABLE [dbo].[support] ADD  DEFAULT ((0)) FOR [seen]
GO
ALTER TABLE [dbo].[advertisement]  WITH CHECK ADD FOREIGN KEY([userID])
REFERENCES [dbo].[User] ([userID])
GO
ALTER TABLE [dbo].[advertisement]  WITH CHECK ADD FOREIGN KEY([videoID])
REFERENCES [dbo].[Video] ([videoID])
GO
ALTER TABLE [dbo].[Blog]  WITH CHECK ADD FOREIGN KEY([userID])
REFERENCES [dbo].[User] ([userID])
GO
ALTER TABLE [dbo].[CardPayments]  WITH CHECK ADD FOREIGN KEY([userID])
REFERENCES [dbo].[User] ([userID])
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD FOREIGN KEY([userID])
REFERENCES [dbo].[User] ([userID])
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD FOREIGN KEY([videoID])
REFERENCES [dbo].[Video] ([videoID])
GO
ALTER TABLE [dbo].[EmojiShow]  WITH CHECK ADD FOREIGN KEY([videoID])
REFERENCES [dbo].[Video] ([videoID])
GO
ALTER TABLE [dbo].[GroupMembers]  WITH CHECK ADD FOREIGN KEY([GroupID])
REFERENCES [dbo].[synchronization] ([GroupID])
GO
ALTER TABLE [dbo].[GroupMembers]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([userID])
GO
ALTER TABLE [dbo].[Membership]  WITH CHECK ADD FOREIGN KEY([userID])
REFERENCES [dbo].[User] ([userID])
GO
ALTER TABLE [dbo].[MessageBody]  WITH CHECK ADD FOREIGN KEY([msgRecieverID])
REFERENCES [dbo].[User] ([userID])
GO
ALTER TABLE [dbo].[MessageBody]  WITH CHECK ADD FOREIGN KEY([msgSenderID])
REFERENCES [dbo].[User] ([userID])
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD FOREIGN KEY([notificationTypeId])
REFERENCES [dbo].[NotificationType] ([notificationTypeID])
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD FOREIGN KEY([recipientUserID])
REFERENCES [dbo].[User] ([userID])
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD FOREIGN KEY([senderUserID])
REFERENCES [dbo].[User] ([userID])
GO
ALTER TABLE [dbo].[playlist]  WITH CHECK ADD FOREIGN KEY([userID])
REFERENCES [dbo].[User] ([userID])
GO
ALTER TABLE [dbo].[playlistGroup]  WITH CHECK ADD FOREIGN KEY([playlistID])
REFERENCES [dbo].[playlist] ([playlistID])
GO
ALTER TABLE [dbo].[playlistGroup]  WITH CHECK ADD FOREIGN KEY([videoID])
REFERENCES [dbo].[Video] ([videoID])
GO
ALTER TABLE [dbo].[ratingSystemComment]  WITH CHECK ADD FOREIGN KEY([commentID])
REFERENCES [dbo].[Comments] ([commentID])
GO
ALTER TABLE [dbo].[ratingSystemVideo]  WITH CHECK ADD FOREIGN KEY([videoID])
REFERENCES [dbo].[Video] ([videoID])
GO
ALTER TABLE [dbo].[report]  WITH CHECK ADD FOREIGN KEY([reportTypeId])
REFERENCES [dbo].[ReportType] ([ReportTypeID])
GO
ALTER TABLE [dbo].[report]  WITH CHECK ADD FOREIGN KEY([userID])
REFERENCES [dbo].[User] ([userID])
GO
ALTER TABLE [dbo].[report]  WITH CHECK ADD FOREIGN KEY([videoID])
REFERENCES [dbo].[Video] ([videoID])
GO
ALTER TABLE [dbo].[sessionTable]  WITH CHECK ADD FOREIGN KEY([userID])
REFERENCES [dbo].[User] ([userID])
GO
ALTER TABLE [dbo].[sessionTable]  WITH CHECK ADD FOREIGN KEY([videoID])
REFERENCES [dbo].[Video] ([videoID])
GO
ALTER TABLE [dbo].[support]  WITH CHECK ADD FOREIGN KEY([userID])
REFERENCES [dbo].[User] ([userID])
GO
ALTER TABLE [dbo].[synchronization]  WITH CHECK ADD FOREIGN KEY([SyncOwnerID])
REFERENCES [dbo].[User] ([userID])
GO
ALTER TABLE [dbo].[synchronization]  WITH CHECK ADD FOREIGN KEY([VideoID])
REFERENCES [dbo].[Video] ([videoID])
GO
ALTER TABLE [dbo].[thumbnailInfo]  WITH CHECK ADD FOREIGN KEY([videoID])
REFERENCES [dbo].[Video] ([videoID])
GO
ALTER TABLE [dbo].[transactions]  WITH CHECK ADD FOREIGN KEY([userID])
REFERENCES [dbo].[User] ([userID])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([typeID])
REFERENCES [dbo].[UserType] ([typeID])
GO
ALTER TABLE [dbo].[userValues]  WITH CHECK ADD FOREIGN KEY([userID])
REFERENCES [dbo].[User] ([userID])
GO
ALTER TABLE [dbo].[Video]  WITH CHECK ADD FOREIGN KEY([categoryID])
REFERENCES [dbo].[Category] ([categoryID])
GO
ALTER TABLE [dbo].[Video]  WITH CHECK ADD FOREIGN KEY([userID])
REFERENCES [dbo].[User] ([userID])
GO
ALTER TABLE [dbo].[VideoStatistics]  WITH CHECK ADD FOREIGN KEY([videoID])
REFERENCES [dbo].[Video] ([videoID])
GO
ALTER TABLE [dbo].[wallet]  WITH CHECK ADD FOREIGN KEY([userID])
REFERENCES [dbo].[User] ([userID])
GO
USE [master]
GO
ALTER DATABASE [VideoStreamingPlatform] SET  READ_WRITE 
GO
