USE [master]
GO
/****** Object:  Database [PRN212]    Script Date: 3/16/2025 7:51:38 PM ******/
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
/****** Object:  Table [dbo].[Notifications]    Script Date: 3/16/2025 7:51:38 PM ******/
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
/****** Object:  Table [dbo].[Reports]    Script Date: 3/16/2025 7:51:38 PM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 3/16/2025 7:51:38 PM ******/
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
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicles]    Script Date: 3/16/2025 7:51:38 PM ******/
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
/****** Object:  Table [dbo].[Violations]    Script Date: 3/16/2025 7:51:38 PM ******/
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

INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (1, 1, N'You have a new violation report for your vehicle 29A-12345', N'29A-12345', CAST(N'2025-03-16T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (2, 2, N'Your violation report has been approved', N'29B-67890', CAST(N'2025-03-16T00:00:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Notifications] OFF
GO
SET IDENTITY_INSERT [dbo].[Reports] ON 

INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (1, 1, N'Speeding', N'Exceeded speed limit on the highway', N'29A-12345', N'http://example.com/image.jpg', N'http://example.com/video.mp4', N'Hanoi Highway', CAST(N'2025-03-16T11:04:51.990' AS DateTime), N'Pending', NULL)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (2, 2, N'Parking Violation', N'Parking in a no-parking zone', N'29B-67890', N'http://example.com/image2.jpg', N'http://example.com/video2.mp4', N'Downtown Hanoi', CAST(N'2025-03-16T11:04:51.990' AS DateTime), N'Approved', 2)
SET IDENTITY_INSERT [dbo].[Reports] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [FullName], [Email], [Password], [Role], [Phone], [Address]) VALUES (1, N'Nguyen Tai', N'tai@gmail.com', N'123', N'Citizen', N'0987654321', N'123 Main St, Hanoi')
INSERT [dbo].[Users] ([UserID], [FullName], [Email], [Password], [Role], [Phone], [Address]) VALUES (2, N'Pham Minh', N'minh@gmail.com', N'123', N'TrafficPolice', N'0912345678', N'456 Elm St, Hanoi')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Vehicles] ON 

INSERT [dbo].[Vehicles] ([VehicleID], [PlateNumber], [OwnerID], [Brand], [Model], [ManufactureYear]) VALUES (1, N'29A-12345', 1, N'Toyota', N'Camry', 2020)
INSERT [dbo].[Vehicles] ([VehicleID], [PlateNumber], [OwnerID], [Brand], [Model], [ManufactureYear]) VALUES (2, N'29B-67890', 2, N'Honda', N'Civic', 2018)
SET IDENTITY_INSERT [dbo].[Vehicles] OFF
GO
SET IDENTITY_INSERT [dbo].[Violations] ON 

INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (1, 1, N'29A-12345', 1, CAST(500.00 AS Decimal(10, 2)), CAST(N'2025-03-16T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (2, 2, N'29B-67890', 2, CAST(200.00 AS Decimal(10, 2)), CAST(N'2025-03-16T00:00:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Violations] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A9D1053422F4A42C]    Script Date: 3/16/2025 7:51:38 PM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Vehicles__03692624CA507D8E]    Script Date: 3/16/2025 7:51:38 PM ******/
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
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD FOREIGN KEY([PlateNumber])
REFERENCES [dbo].[Vehicles] ([PlateNumber])
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[Reports]  WITH CHECK ADD FOREIGN KEY([PlateNumber])
REFERENCES [dbo].[Vehicles] ([PlateNumber])
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
