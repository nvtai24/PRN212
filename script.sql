USE [PRN212]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 3/19/2025 11:04:36 AM ******/
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
/****** Object:  Table [dbo].[Reports]    Script Date: 3/19/2025 11:04:36 AM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 3/19/2025 11:04:36 AM ******/
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
/****** Object:  Table [dbo].[Vehicles]    Script Date: 3/19/2025 11:04:36 AM ******/
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
/****** Object:  Table [dbo].[Violations]    Script Date: 3/19/2025 11:04:36 AM ******/
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

INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (1, 1, N'You have a new violation report for your vehicle 29A-12345', N'29A-12345', CAST(N'2025-03-16T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (2, 2, N'Your violation report has been approved', N'29B-67890', CAST(N'2025-03-16T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (3, 3, N'B?n có m?t vi ph?m giao thông m?i c?n x? lý', N'30A-12345', CAST(N'2025-03-17T23:39:39.353' AS DateTime), 1)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (4, 4, N'Xe c?a b?n dã b? báo cáo vi ph?m', N'51B-67890', CAST(N'2025-03-17T23:39:39.353' AS DateTime), 1)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (5, 3, N'Nh?c nh?: H?n n?p ph?t c?a b?n s?p d?n', N'43C-11111', CAST(N'2025-03-17T23:39:39.353' AS DateTime), 1)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (6, 4, N'Vi ph?m c?a b?n dã du?c x? lý', N'92D-22222', CAST(N'2025-03-17T23:39:39.353' AS DateTime), 1)
INSERT [dbo].[Notifications] ([NotificationID], [UserID], [Message], [PlateNumber], [SentDate], [IsRead]) VALUES (7, 5, N'B?n có thông báo m?i v? vi ph?m giao thông', N'29E-33333', CAST(N'2025-03-17T23:39:39.353' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Notifications] OFF
GO
SET IDENTITY_INSERT [dbo].[Reports] ON 

INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (1, 1, N'Speeding', N'Exceeded speed limit on the highway', N'29A-12345', N'http://example.com/image.jpg', N'http://example.com/video.mp4', N'Hanoi Highway', CAST(N'2025-03-16T11:08:27.120' AS DateTime), N'Pending', NULL)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (2, 2, N'Parking Violation', N'Parking in a no-parking zone', N'29B-67890', N'http://example.com/image2.jpg', N'http://example.com/video2.mp4', N'Downtown Hanoi', CAST(N'2025-03-16T11:08:27.120' AS DateTime), N'Approved', 2)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (3, 2, N'Speeding', N'Vu?t quá t?c d? 60km/h trong khu dân cu', N'30A-12345', N'image1.jpg', N'video1.mp4', N'Ðu?ng Láng, Hà N?i', CAST(N'2025-03-17T23:39:39.353' AS DateTime), N'Approved', 1)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (4, 2, N'Wrong Lane', N'Ði không dúng làn du?ng quy d?nh', N'51B-67890', N'image2.jpg', NULL, N'Nguy?n Van Linh, HCMC', CAST(N'2025-03-17T23:39:39.353' AS DateTime), N'Pending', NULL)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (5, 5, N'Red Light', N'Vu?t dèn d?', N'43C-11111', N'image3.jpg', N'video3.mp4', N'Lê Du?n, Ðà N?ng', CAST(N'2025-03-17T23:39:39.353' AS DateTime), N'Approved', 1)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (6, 5, N'No Helmet', N'Không d?i mu b?o hi?m', N'92D-22222', N'image4.jpg', NULL, N'Tr?n Phú, H?i Phòng', CAST(N'2025-03-17T23:39:39.353' AS DateTime), N'Rejected', 1)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (7, 2, N'Parking', N'Ð? xe sai quy d?nh', N'29E-33333', N'image5.jpg', N'video5.mp4', N'Nguy?n Hu?, HCMC', CAST(N'2025-03-17T23:39:39.353' AS DateTime), N'Pending', NULL)
INSERT [dbo].[Reports] ([ReportID], [ReporterID], [ViolationType], [Description], [PlateNumber], [ImageURL], [VideoURL], [Location], [ReportDate], [Status], [ProcessedBy]) VALUES (8, 3, N'abc hihi', N'ádfasdfasdfasdf', N'29A-12345', N'Images\wallpaperflare.com_wallpaper.jpg', NULL, N'ádfklaskjdfkl;ádf', CAST(N'2025-03-19T11:01:41.430' AS DateTime), N'Pending', NULL)
SET IDENTITY_INSERT [dbo].[Reports] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [FullName], [Email], [Password], [Role], [Phone], [Address], [Status]) VALUES (1, N'Nguyen Tai', N'tai.nguyen@example.com', N'password123', N'Citizen', N'0987654321', N'Nghi Loc, Nghe An 12', 1)
INSERT [dbo].[Users] ([UserID], [FullName], [Email], [Password], [Role], [Phone], [Address], [Status]) VALUES (2, N'Pham Minh 12123', N'minh.pham@example.com', N'password123', N'Citizen', N'0912345678', N'456 Elm St, Hanoi', 0)
INSERT [dbo].[Users] ([UserID], [FullName], [Email], [Password], [Role], [Phone], [Address], [Status]) VALUES (3, N'John Admin', N'admin@gmail.com', N'123456', N'Admin', N'0901234567', N'Hanoi', 0)
INSERT [dbo].[Users] ([UserID], [FullName], [Email], [Password], [Role], [Phone], [Address], [Status]) VALUES (4, N'Mary Police', N'police@gmail.com', N'123456', N'Citizen', N'0901234568', N'HoChiMinh City', 0)
INSERT [dbo].[Users] ([UserID], [FullName], [Email], [Password], [Role], [Phone], [Address], [Status]) VALUES (5, N'Peter Parker', N'peter@gmail.com', N'123456', N'Citizen', N'0901234569', N'Da Nang', 0)
INSERT [dbo].[Users] ([UserID], [FullName], [Email], [Password], [Role], [Phone], [Address], [Status]) VALUES (6, N'Tony Stark', N'tony@gmail.com', N'123456', N'TrafficPolice', N'0901234570', N'Hai Phong', 1)
INSERT [dbo].[Users] ([UserID], [FullName], [Email], [Password], [Role], [Phone], [Address], [Status]) VALUES (7, N'Steve Rogers 12', N'steve@gmail.com', N'123456', N'TrafficPolice', N'0901234571', N'Can Tho c12', 0)
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
SET IDENTITY_INSERT [dbo].[Vehicles] OFF
GO
SET IDENTITY_INSERT [dbo].[Violations] ON 

INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (1, 1, N'29A-12345', 1, CAST(500.00 AS Decimal(10, 2)), CAST(N'2025-03-16T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (2, 2, N'29B-67890', 2, CAST(200.00 AS Decimal(10, 2)), CAST(N'2025-03-16T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (3, 1, N'30A-12345', 3, CAST(1500000.00 AS Decimal(10, 2)), CAST(N'2025-03-17T23:39:39.353' AS DateTime), 1)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (4, 2, N'51B-67890', 4, CAST(750000.00 AS Decimal(10, 2)), CAST(N'2025-03-17T23:39:39.353' AS DateTime), 0)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (5, 3, N'43C-11111', 3, CAST(2000000.00 AS Decimal(10, 2)), CAST(N'2025-03-17T23:39:39.353' AS DateTime), 0)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (6, 4, N'92D-22222', 4, CAST(500000.00 AS Decimal(10, 2)), CAST(N'2025-03-17T23:39:39.353' AS DateTime), 1)
INSERT [dbo].[Violations] ([ViolationID], [ReportID], [PlateNumber], [ViolatorID], [FineAmount], [FineDate], [PaidStatus]) VALUES (7, 5, N'29E-33333', 5, CAST(1000000.00 AS Decimal(10, 2)), CAST(N'2025-03-17T23:39:39.353' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Violations] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A9D1053434F8B744]    Script Date: 3/19/2025 11:04:36 AM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Vehicles__03692624E860A32E]    Script Date: 3/19/2025 11:04:36 AM ******/
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
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Status]  DEFAULT ((1)) FOR [Status]
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
