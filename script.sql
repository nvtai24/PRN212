USE [master]
GO
/****** Object:  Database [PRN212]    Script Date: 3/25/2025 12:11:22 AM ******/
CREATE DATABASE [PRN212]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PRN212', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\PRN212.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PRN212_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\PRN212_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PRN212] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PRN212].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PRN212] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PRN212] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PRN212] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PRN212] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PRN212] SET ARITHABORT OFF 
GO
ALTER DATABASE [PRN212] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [PRN212] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PRN212] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PRN212] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PRN212] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PRN212] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PRN212] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PRN212] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PRN212] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PRN212] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PRN212] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PRN212] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PRN212] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PRN212] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PRN212] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PRN212] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PRN212] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PRN212] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PRN212] SET  MULTI_USER 
GO
ALTER DATABASE [PRN212] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PRN212] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PRN212] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PRN212] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PRN212] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PRN212] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PRN212] SET QUERY_STORE = ON
GO
ALTER DATABASE [PRN212] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [PRN212]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 3/25/2025 12:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[NotificationID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Message] [text] NOT NULL,
	[PlateNumber] [varchar](15) NULL,
	[SentDate] [datetime] NULL,
	[IsRead] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[NotificationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reports]    Script Date: 3/25/2025 12:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reports](
	[ReportID] [int] IDENTITY(1,1) NOT NULL,
	[ReporterID] [int] NOT NULL,
	[ViolationType] [varchar](50) NOT NULL,
	[Description] [text] NOT NULL,
	[PlateNumber] [varchar](15) NOT NULL,
	[ImageURL] [text] NULL,
	[VideoURL] [text] NULL,
	[Location] [varchar](255) NOT NULL,
	[ReportDate] [datetime] NULL,
	[Status] [varchar](10) NULL,
	[ProcessedBy] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ReportID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/25/2025 12:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [varchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Password] [varchar](255) NOT NULL,
	[Role] [varchar](20) NOT NULL,
	[Phone] [varchar](15) NOT NULL,
	[Address] [text] NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicles]    Script Date: 3/25/2025 12:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicles](
	[VehicleID] [int] IDENTITY(1,1) NOT NULL,
	[PlateNumber] [varchar](15) NOT NULL,
	[OwnerID] [int] NOT NULL,
	[Brand] [varchar](50) NULL,
	[Model] [varchar](50) NULL,
	[ManufactureYear] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[VehicleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Violations]    Script Date: 3/25/2025 12:11:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Violations](
	[ViolationID] [int] IDENTITY(1,1) NOT NULL,
	[ReportID] [int] NOT NULL,
	[PlateNumber] [varchar](15) NOT NULL,
	[ViolatorID] [int] NULL,
	[FineAmount] [decimal](10, 2) NOT NULL,
	[FineDate] [datetime] NULL,
	[PaidStatus] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ViolationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Notifications] ON 

INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (2, 2, N'Your violation report has been approved', N'29B-67890', CAST(N'2025-03-16T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (3, 3, N'You have received a traffic violation penalty notice for your vehicle 30A-12345', N'30A-12345', CAST(N'2025-03-17T23:48:54.397' AS DateTime), 0)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (4, 4, N'You have received a traffic violation penalty notice for your vehicle 51B-67890', N'51B-67890', CAST(N'2025-03-17T23:48:54.397' AS DateTime), 1)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (5, 3, N'You have received a traffic violation penalty notice for your vehicle 43C-11111', N'43C-11111', CAST(N'2025-03-17T23:48:54.397' AS DateTime), 0)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (6, 4, N'You have received a traffic violation penalty notice for your vehicle 29A-12345 due to [Tai nan]', N'92D-22222', CAST(N'2025-03-17T23:48:54.397' AS DateTime), 1)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (7, 5, N'You have received a traffic violation penalty notice for your vehicle 29A-12345 due to [sdfdsf]', N'29E-33333', CAST(N'2025-03-17T23:48:54.397' AS DateTime), 0)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (15, 3, N'You have received a traffic violation penalty notice for your vehicle 43C-11111', N'43C-11111', CAST(N'2025-03-20T15:29:40.817' AS DateTime), 0)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (16, 1, N'You have received a traffic violation penalty notice for your vehicle 29A-12345 due to ngu', N'29A-12345', CAST(N'2025-03-20T15:35:31.493' AS DateTime), 1)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (17, 1, N'You have received a traffic violation penalty notice for your vehicle 29A-12345 due to [sdfsdf]', N'29A-12345', CAST(N'2025-03-20T15:36:22.050' AS DateTime), 1)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (18, 1, N'Your report has been successfully sent to the vehicle with plate number: 29A-12345', N'29A-12345', CAST(N'2025-03-20T18:21:03.263' AS DateTime), 1)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (21, 1, N'Your report has been successfully sent to the vehicle with plate number: sdfsdf', N'sdfsdf', CAST(N'2025-03-20T18:24:40.997' AS DateTime), 1)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (22, 5, N'Your report has been successfully sent to the vehicle with plate number: 29A-12345', N'29A-12345', CAST(N'2025-03-20T19:07:16.600' AS DateTime), 0)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (23, 1, N'You have received a traffic violation penalty notice for your vehicle 29A-12345 due to [Tai nan]', N'29A-12345', CAST(N'2025-03-20T19:08:01.567' AS DateTime), 1)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (24, 1, N'Your report has been successfully sent to the vehicle with plate number: fdgfdg', N'fdgfdg', CAST(N'2025-03-20T21:07:45.773' AS DateTime), 1)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (25, 1, N'Your report has been successfully sent to the vehicle with plate number: dsfdsfdsfsdf', N'dsfdsfdsfsdf', CAST(N'2025-03-21T08:43:58.960' AS DateTime), 1)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (26, 1, N'You have received a traffic violation penalty notice for your vehicle 29A-12345 due to [Tai nan]', N'29A-12345', CAST(N'2025-03-21T08:54:50.697' AS DateTime), 0)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (27, 1, N'You have received a traffic violation penalty notice for your vehicle 29A-12345 due to [Boc dauf]', N'29A-12345', CAST(N'2025-03-21T09:14:06.923' AS DateTime), 0)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (28, 3, N'You have received a traffic violation penalty notice for your vehicle 30A-12345 due to [Boc dau]', N'30A-12345', CAST(N'2025-03-21T09:14:39.537' AS DateTime), 0)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (29, 4, N'You have received a traffic violation penalty notice for your vehicle 92D-22222 due to [di xe ngu]', N'92D-22222', CAST(N'2025-03-21T09:15:57.607' AS DateTime), 0)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (30, 1, N'You have received a traffic violation penalty notice for your vehicle 29A-12345 due to [sdfdsf]', N'29A-12345', CAST(N'2025-03-21T09:36:57.150' AS DateTime), 0)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (31, 4, N'You have received a traffic violation penalty notice for your vehicle 92D-22222 due to [dssdfsdf]', N'92D-22222', CAST(N'2025-03-21T09:41:17.067' AS DateTime), 0)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (32, 1, N'You have received a traffic violation penalty notice for your vehicle 29A-12345 due to [asdsad]', N'29A-12345', CAST(N'2025-03-21T09:43:34.647' AS DateTime), 0)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (33, 3, N'You have received a traffic violation penalty notice for your vehicle 30A-12345 due to [Boc dau]', N'30A-12345', CAST(N'2025-03-21T09:46:00.020' AS DateTime), 0)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (34, 3, N'You have received a traffic violation penalty notice for your vehicle 30A-12345 due to [Boc dau]', N'30A-12345', CAST(N'2025-03-21T09:48:04.553' AS DateTime), 0)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (35, 2, N'You have received a traffic violation penalty notice for your vehicle 29B-67890 due to [Tai nan]', N'29B-67890', CAST(N'2025-03-21T09:49:02.240' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Notifications] OFF
GO
SET IDENTITY_INSERT [dbo].[Reports] ON 

INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (1, 1, N'Speeding', N'Exceeded speed limit on the highway', N'29A-12345', N'http://example.com/image.jpg', N'http://example.com/video.mp4', N'Hanoi Highway', CAST(N'2025-03-16T11:04:51.990' AS DateTime), N'Rejected', NULL)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (2, 6, N'Parking Violation', N'Parking in a no-parking zone', N'29B-67890', N'http://example.com/image2.jpg', N'http://example.com/video2.mp4', N'Downtown Hanoi', CAST(N'2025-03-16T11:04:51.990' AS DateTime), N'Approved', 2)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (3, 6, N'Speeding', N'Exceeded speed limit on the highway', N'30A-12345', N'image1.jpg', N'video1.mp4', N'Thach That', CAST(N'2025-03-17T23:48:54.397' AS DateTime), N'Approved', 2)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (4, 6, N'Wrong Lane', N'Wrong Lane', N'51B-67890', N'image2.jpg', NULL, N'Thach That', CAST(N'2025-03-17T23:48:54.397' AS DateTime), N'Rejected', NULL)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (5, 5, N'Red Light', N'Red Light', N'43C-11111', N'image3.jpg', N'video3.mp4', N'Thach That', CAST(N'2025-03-17T23:48:54.397' AS DateTime), N'Approved', 2)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (6, 5, N'No Helmet', N'No Helmet', N'92D-22222', N'image4.jpg', NULL, N'Thach That', CAST(N'2025-03-17T23:48:54.397' AS DateTime), N'Pending', NULL)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (7, 6, N'Parking', N'Parking in a no-parking zone', N'29E-33333', N'image5.jpg', N'video5.mp4', N'Thach That', CAST(N'2025-03-17T23:48:54.397' AS DateTime), N'Pending', NULL)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (9, 1, N'No Helmet', N'123', N'29E-33333', N'Images\460624155_122143864088291290_4040010554300510664_n.jpg', N'Videos\6412874533358.mp4', N'YB', CAST(N'2025-03-18T10:06:11.603' AS DateTime), N'Rejected', NULL)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (12, 1, N'Bruh', N'No Helmet', N'30A-12345', NULL, NULL, N'Thach That', CAST(N'2025-03-18T17:18:12.583' AS DateTime), N'Rejected', NULL)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (13, 1, N'di xe ngu', N'huhu ngu', N'92D-22222', N'Images\460624155_122143864088291290_4040010554300510664_n.jpg', NULL, N'Ha Noi', CAST(N'2025-03-18T17:25:16.617' AS DateTime), N'Approved', 2)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (14, 1, N'Khong doi mux', N'dddxxx', N'43C-11111', N'Images\7006288841-1526100974306459281938.jpg', N'Videos\6412874533358.mp4', N'Yen Bai', CAST(N'2025-03-18T17:40:11.553' AS DateTime), N'Approved', 2)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (15, 1, N'Di xe cham', N'cham', N'51B-67890', N'Images\IMG_9714.JPG', N'Videos\6412874533358.mp4', N'Yen Bai', CAST(N'2025-03-18T21:42:22.090' AS DateTime), N'Pending', NULL)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (16, 1, N'Tai nan', N'dd', N'29B-67890', NULL, NULL, N'Hoa Lac', CAST(N'2025-03-18T21:47:11.263' AS DateTime), N'Approved', 2)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (17, 1, N'Boc dau', N'sss', N'30A-12345', NULL, NULL, N'Thai Nguyen', CAST(N'2025-03-18T21:49:28.973' AS DateTime), N'Approved', 2)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (18, 1, N'Boc dau', N'sss', N'30A-12345', N'Images\pngtree-vector-healthy-heart-icon-png-image_319655.jpg', NULL, N'Thai Nguyen', CAST(N'2025-03-18T21:49:39.740' AS DateTime), N'Approved', 2)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (19, 1, N'Boc dau', N'sss', N'30A-12345', N'Images\pngtree-vector-healthy-heart-icon-png-image_319655.jpg', N'Videos\6412874533358.mp4', N'Thai Nguyen', CAST(N'2025-03-18T21:49:44.813' AS DateTime), N'Approved', 2)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (20, 1, N'asdsad', N'sadasd', N'29A-12345', N'Images\bai-tap-gym-elle-man-featured-image.jpg', NULL, N'dsadasd', CAST(N'2025-03-18T21:50:04.207' AS DateTime), N'Approved', 2)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (21, 1, N'sdfdsf', N'sdfsdf', N'29A-12345', N'Images\amazon-kindle-paperwhite-2011-vs-2015-3-scaled.jpg', N'Videos\6412874533358.mp4', N'fsdfsdf', CAST(N'2025-03-18T21:50:19.980' AS DateTime), N'Approved', 2)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (22, 1, N'Boc dauf', N'nguff', N'29A-12345', N'Images\amazon-kindle-paperwhite-2011-vs-2015-3-scaled.jpg', N'Videos\6412874533358.mp4', N'Thai Nguyen', CAST(N'2025-03-18T21:50:57.527' AS DateTime), N'Approved', 2)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (23, 1, N'sdfsdf', N'sdfsdf', N'29A-12345', N'Images\bai-tap-gym-elle-man-featured-image.jpg', N'Videos\6412874533358.mp4', N'fdsf', CAST(N'2025-03-18T22:07:20.600' AS DateTime), N'Approved', 2)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (24, 1, N'fdsfdsf', N'sdfsdf', N'29A-12345', NULL, NULL, N'fdsfds', CAST(N'2025-03-18T22:07:33.457' AS DateTime), N'Rejected', NULL)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (25, 1, N'dsfsdf', N'dsfdsf', N'29A-12345', NULL, NULL, N'dsfdsf', CAST(N'2025-03-18T22:07:45.267' AS DateTime), N'Rejected', NULL)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (26, 1, N'ngu', N'ngu', N'29A-12345', NULL, NULL, N'ngu', CAST(N'2025-03-18T22:43:51.387' AS DateTime), N'Approved', 2)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (27, 1, N'dssdfsdf', N'ddfdf', N'92D-22222', NULL, NULL, N'fsddsf', CAST(N'2025-03-19T10:31:56.030' AS DateTime), N'Approved', 2)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (28, 1, N'Vuot den do', N'vuot', N'51B-67890', N'Images\7006288841-1526100974306459281938.jpg', N'Videos\6412874533358.mp4', N'Thach That', CAST(N'2025-03-19T10:35:38.003' AS DateTime), N'Approved', 2)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (31, 1, N'fdfdf', N'ddf', N'123445', NULL, NULL, N'gfgfgf', CAST(N'2025-03-19T11:16:19.817' AS DateTime), N'Rejected', NULL)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (32, 1, N'cxvxcv', N'xcvcxvcxv', N'cvcxvcxv', NULL, NULL, N'vcxv', CAST(N'2025-03-19T11:17:56.270' AS DateTime), N'Approved', 2)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (33, 1, N'Boc dau', N'di xe sai', N'29A-12345', NULL, NULL, N'Yen Bai', CAST(N'2025-03-20T18:21:01.650' AS DateTime), N'Pending', NULL)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (36, 1, N'sdfsdf', N'sdfdsf', N'sdfdsf', NULL, NULL, N'fsdf', CAST(N'2025-03-20T18:22:45.420' AS DateTime), N'Pending', NULL)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (37, 1, N'dsf', N'fsdfsdfdsf', N'29B-67890', N'Images\460624155_122143864088291290_4040010554300510664_n.jpg', NULL, N'sdfdsf', CAST(N'2025-03-20T18:23:01.310' AS DateTime), N'Pending', NULL)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (38, 1, N'dsf', N'fsdfsdfdsf', N'dsfdsfsd', N'Images\460624155_122143864088291290_4040010554300510664_n.jpg', N'Videos\6412874533358.mp4', N'sdfdsf', CAST(N'2025-03-20T18:23:07.170' AS DateTime), N'Pending', NULL)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (39, 1, N'fsdf', N'dsfdsf', N'29B-67890', N'Images\7006288841-1526100974306459281938.jpg', N'Videos\6412874533358.mp4', N'fdsfdsxx', CAST(N'2025-03-20T18:24:40.137' AS DateTime), N'Pending', NULL)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (40, 5, N'Tai nan', N'43434', N'29A-12345', N'Images\460624155_122143864088291290_4040010554300510664_n.jpg', N'Videos\6412874533358.mp4', N'Thach That', CAST(N'2025-03-20T19:07:15.763' AS DateTime), N'Approved', 2)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (41, 1, N'fdgfdg', N'fdgdfg', N'fdgfdg', NULL, NULL, N'gfdfg', CAST(N'2025-03-20T21:07:44.783' AS DateTime), N'Pending', NULL)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (42, 1, N'fdsfdsfdsfdsf', N'sdfsdfdsf', N'dsfdsfdsfsdf', NULL, NULL, N'ffdsfds', CAST(N'2025-03-21T08:43:58.117' AS DateTime), N'Pending', NULL)
SET IDENTITY_INSERT [dbo].[Reports] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [FullName], [Email], [Password], [Role], [Phone], [Address], [Status]) VALUES (1, N'Nguyen Tai', N'a', N'a', N'Citizen', N'0987654321', N'123 Main St, Hanoi', 1)
INSERT [dbo].[Users] ([UserID], [FullName], [Email], [Password], [Role], [Phone], [Address], [Status]) VALUES (2, N'Pham Minh', N'b', N'b', N'TrafficPolice', N'0912345678', N'456 Elm St, Hanoi', 1)
INSERT [dbo].[Users] ([UserID], [FullName], [Email], [Password], [Role], [Phone], [Address], [Status]) VALUES (3, N'John Admin', N'admin@gmail.com', N'123456', N'Admin', N'0901234567', N'Ha Noi', 1)
INSERT [dbo].[Users] ([UserID], [FullName], [Email], [Password], [Role], [Phone], [Address], [Status]) VALUES (4, N'Mary Police', N'police@gmail.com', N'123456', N'TrafficPolice', N'0901234568', N'Ho Chi Minh', 1)
INSERT [dbo].[Users] ([UserID], [FullName], [Email], [Password], [Role], [Phone], [Address], [Status]) VALUES (5, N'Peter Parker', N'peter@gmail.com', N'123456', N'Citizen', N'0901234569', N'Da Nang', 1)
INSERT [dbo].[Users] ([UserID], [FullName], [Email], [Password], [Role], [Phone], [Address], [Status]) VALUES (6, N'Tony Stark', N'tony@gmail.com', N'123456', N'Citizen', N'0901234570', N'Hai Phong', 1)
INSERT [dbo].[Users] ([UserID], [FullName], [Email], [Password], [Role], [Phone], [Address], [Status]) VALUES (7, N'Steve Rogers', N'steve@gmail.com', N'123456', N'TrafficPolice', N'0901234571', N'Can Tho', 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Vehicles] ON 

INSERT [dbo].[Vehicles] ([VehicleID], [PlateNumber], [OwnerID], [Brand], [Model], [ManufactureYear]) VALUES (1, N'29A-12345', 1, N'Toyota', N'Camry', 2020)
INSERT [dbo].[Vehicles] ([VehicleID], [PlateNumber], [OwnerID], [Brand], [Model], [ManufactureYear]) VALUES (2, N'29B-67890', 2, N'Honda', N'Civic', 2018)
INSERT [dbo].[Vehicles] ([VehicleID], [PlateNumber], [OwnerID], [Brand], [Model], [ManufactureYear]) VALUES (3, N'30A-12345', 3, N'Toyota', N'Camry', 2020)
INSERT [dbo].[Vehicles] ([VehicleID], [PlateNumber], [OwnerID], [Brand], [Model], [ManufactureYear]) VALUES (4, N'51B-67890', 4, N'Honda', N'Civic', 2021)
INSERT [dbo].[Vehicles] ([VehicleID], [PlateNumber], [OwnerID], [Brand], [Model], [ManufactureYear]) VALUES (5, N'43C-11111', 3, N'Ford', N'Ranger', 2019)
INSERT [dbo].[Vehicles] ([VehicleID], [PlateNumber], [OwnerID], [Brand], [Model], [ManufactureYear]) VALUES (6, N'92D-22222', 4, N'Hyundai', N'Tucson', 2022)
INSERT [dbo].[Vehicles] ([VehicleID], [PlateNumber], [OwnerID], [Brand], [Model], [ManufactureYear]) VALUES (7, N'29E-33333', 5, N'Mazda', N'CX-5', 2021)
INSERT [dbo].[Vehicles] ([VehicleID], [PlateNumber], [OwnerID], [Brand], [Model], [ManufactureYear]) VALUES (8, N'30A-10001', 1, N'Ford', N'Focus', 2022)
INSERT [dbo].[Vehicles] ([VehicleID], [PlateNumber], [OwnerID], [Brand], [Model], [ManufactureYear]) VALUES (9, N'30A-10002', 1, N'Chevrolet', N'Malibu', 2021)
INSERT [dbo].[Vehicles] ([VehicleID], [PlateNumber], [OwnerID], [Brand], [Model], [ManufactureYear]) VALUES (10, N'30A-10003', 2, N'BMW', N'X5', 2020)
INSERT [dbo].[Vehicles] ([VehicleID], [PlateNumber], [OwnerID], [Brand], [Model], [ManufactureYear]) VALUES (11, N'30A-10004', 2, N'Mercedes-Benz', N'C-Class', 2019)
INSERT [dbo].[Vehicles] ([VehicleID], [PlateNumber], [OwnerID], [Brand], [Model], [ManufactureYear]) VALUES (12, N'30A-10005', 3, N'Audi', N'A4', 2020)
INSERT [dbo].[Vehicles] ([VehicleID], [PlateNumber], [OwnerID], [Brand], [Model], [ManufactureYear]) VALUES (13, N'30A-10006', 3, N'Toyota', N'Camry', 2021)
INSERT [dbo].[Vehicles] ([VehicleID], [PlateNumber], [OwnerID], [Brand], [Model], [ManufactureYear]) VALUES (14, N'30A-10007', 4, N'Honda', N'Accord', 2019)
INSERT [dbo].[Vehicles] ([VehicleID], [PlateNumber], [OwnerID], [Brand], [Model], [ManufactureYear]) VALUES (15, N'30A-10008', 4, N'Kia', N'Optima', 2022)
INSERT [dbo].[Vehicles] ([VehicleID], [PlateNumber], [OwnerID], [Brand], [Model], [ManufactureYear]) VALUES (16, N'30A-10009', 5, N'Nissan', N'Altima', 2021)
INSERT [dbo].[Vehicles] ([VehicleID], [PlateNumber], [OwnerID], [Brand], [Model], [ManufactureYear]) VALUES (17, N'30A-10010', 6, N'Volkswagen', N'Jetta', 2020)
INSERT [dbo].[Vehicles] ([VehicleID], [PlateNumber], [OwnerID], [Brand], [Model], [ManufactureYear]) VALUES (18, N'30A-10011', 7, N'Mazda', N'CX-5', 2021)
SET IDENTITY_INSERT [dbo].[Vehicles] OFF
GO
SET IDENTITY_INSERT [dbo].[Violations] ON 

INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (2, 2, N'29B-67890', 2, CAST(200.00 AS Decimal(10, 2)), CAST(N'2025-03-16T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (3, 1, N'30A-12345', 3, CAST(1500000.00 AS Decimal(10, 2)), CAST(N'2025-03-17T23:48:54.397' AS DateTime), 1)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (4, 2, N'51B-67890', 4, CAST(750000.00 AS Decimal(10, 2)), CAST(N'2025-03-17T23:48:54.397' AS DateTime), 0)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (5, 3, N'43C-11111', 3, CAST(2000000.00 AS Decimal(10, 2)), CAST(N'2025-03-17T23:48:54.397' AS DateTime), 0)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (6, 4, N'92D-22222', 4, CAST(500000.00 AS Decimal(10, 2)), CAST(N'2025-03-17T23:48:54.397' AS DateTime), 1)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (7, 5, N'29E-33333', 5, CAST(1000000.00 AS Decimal(10, 2)), CAST(N'2025-03-17T23:48:54.397' AS DateTime), 0)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (8, 28, N'51B-67890', 4, CAST(3000.00 AS Decimal(10, 2)), CAST(N'2025-03-20T15:03:20.797' AS DateTime), 1)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (17, 14, N'43C-11111', 3, CAST(6666.00 AS Decimal(10, 2)), CAST(N'2025-03-20T15:29:40.817' AS DateTime), 0)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (18, 26, N'29A-12345', 1, CAST(5000.00 AS Decimal(10, 2)), CAST(N'2025-03-20T15:35:31.493' AS DateTime), 1)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (19, 23, N'29A-12345', 1, CAST(9999.00 AS Decimal(10, 2)), CAST(N'2025-03-20T15:36:22.050' AS DateTime), 1)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (20, 40, N'29A-12345', 1, CAST(4567.00 AS Decimal(10, 2)), CAST(N'2025-03-20T19:08:01.567' AS DateTime), 1)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (21, 40, N'29A-12345', 1, CAST(4567.00 AS Decimal(10, 2)), CAST(N'2025-03-21T08:54:50.697' AS DateTime), 0)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (22, 22, N'29A-12345', 1, CAST(6000.00 AS Decimal(10, 2)), CAST(N'2025-03-21T09:14:06.923' AS DateTime), 0)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (23, 19, N'30A-12345', 3, CAST(55555.00 AS Decimal(10, 2)), CAST(N'2025-03-21T09:14:39.537' AS DateTime), 1)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (24, 13, N'92D-22222', 4, CAST(888888.00 AS Decimal(10, 2)), CAST(N'2025-03-21T09:15:57.607' AS DateTime), 1)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (25, 21, N'29A-12345', 1, CAST(6666.00 AS Decimal(10, 2)), CAST(N'2025-03-21T09:36:57.150' AS DateTime), 1)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (26, 27, N'92D-22222', 4, CAST(6666.00 AS Decimal(10, 2)), CAST(N'2025-03-21T09:41:17.067' AS DateTime), 0)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (27, 20, N'29A-12345', 1, CAST(7777.00 AS Decimal(10, 2)), CAST(N'2025-03-21T09:43:34.647' AS DateTime), 1)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (28, 18, N'30A-12345', 3, CAST(6666.00 AS Decimal(10, 2)), CAST(N'2025-03-21T09:46:00.020' AS DateTime), 1)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (29, 17, N'30A-12345', 3, CAST(7777.00 AS Decimal(10, 2)), CAST(N'2025-03-21T09:48:04.553' AS DateTime), 0)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (30, 16, N'29B-67890', 2, CAST(5555.00 AS Decimal(10, 2)), CAST(N'2025-03-21T09:49:02.240' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Violations] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A9D105342C086912]    Script Date: 3/25/2025 12:11:23 AM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Vehicles__036926246DF238AB]    Script Date: 3/25/2025 12:11:23 AM ******/
ALTER TABLE [dbo].[Vehicles] ADD UNIQUE NONCLUSTERED 
(
	[PlateNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Notifications] ADD  DEFAULT (getdate()) FOR [SentDate]
GO
ALTER TABLE [dbo].[Notifications] ADD  DEFAULT ((0)) FOR [IsRead]
GO
ALTER TABLE [dbo].[Reports] ADD  DEFAULT (getdate()) FOR [ReportDate]
GO
ALTER TABLE [dbo].[Reports] ADD  DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[Violations] ADD  DEFAULT (getdate()) FOR [FineDate]
GO
ALTER TABLE [dbo].[Violations] ADD  DEFAULT ((0)) FOR [PaidStatus]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Reports]  WITH CHECK ADD FOREIGN KEY([ProcessedBy])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Reports]  WITH CHECK ADD FOREIGN KEY([ReporterID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD FOREIGN KEY([OwnerID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Violations]  WITH CHECK ADD FOREIGN KEY([PlateNumber])
REFERENCES [dbo].[Vehicles] ([PlateNumber])
GO
ALTER TABLE [dbo].[Violations]  WITH CHECK ADD FOREIGN KEY([ReportID])
REFERENCES [dbo].[Reports] ([ReportID])
GO
ALTER TABLE [dbo].[Violations]  WITH CHECK ADD FOREIGN KEY([ViolatorID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Reports]  WITH CHECK ADD CHECK  (([Status]='Rejected' OR [Status]='Approved' OR [Status]='Pending'))
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD CHECK  (([Role]='Admin' OR [Role]='TrafficPolice' OR [Role]='Citizen'))
GO
USE [master]
GO
ALTER DATABASE [PRN212] SET  READ_WRITE 
GO
