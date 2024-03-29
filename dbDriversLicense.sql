
/****** Object:  Table [dbo].[tblDriversLicenseDetail]    Script Date: 11-11-2019 20:53:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDriversLicenseDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[License_number] [nvarchar](20) NULL,
	[Issue_date] [datetime] NULL,
	[Expiry_date] [datetime] NULL,
	[PersonId] [int] NULL,
	[LicenseTypeId] [int] NOT NULL,
 CONSTRAINT [PK_tblDriversLicenseDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblLicenseTypes]    Script Date: 11-11-2019 20:53:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblLicenseTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LicenseTypeName] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblLicenseTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblPersonDetail]    Script Date: 11-11-2019 20:53:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPersonDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[LastUpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_tblPersonDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblDriversLicenseDetail] ON 
GO
INSERT [dbo].[tblDriversLicenseDetail] ([Id], [License_number], [Issue_date], [Expiry_date], [PersonId], [LicenseTypeId]) VALUES (7, N'PB58 20100002125', CAST(N'2019-11-11T00:00:00.000' AS DateTime), CAST(N'2032-11-11T00:00:00.000' AS DateTime), 9, 4)
GO
INSERT [dbo].[tblDriversLicenseDetail] ([Id], [License_number], [Issue_date], [Expiry_date], [PersonId], [LicenseTypeId]) VALUES (8, N'Pb58 1200003452', CAST(N'2019-11-11T00:00:00.000' AS DateTime), CAST(N'2032-11-11T00:00:00.000' AS DateTime), 10, 5)
GO
SET IDENTITY_INSERT [dbo].[tblDriversLicenseDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[tblLicenseTypes] ON 
GO
INSERT [dbo].[tblLicenseTypes] ([Id], [LicenseTypeName]) VALUES (1, N'Learning License')
GO
INSERT [dbo].[tblLicenseTypes] ([Id], [LicenseTypeName]) VALUES (2, N'Permanent License')
GO
INSERT [dbo].[tblLicenseTypes] ([Id], [LicenseTypeName]) VALUES (3, N'International Driving License')
GO
INSERT [dbo].[tblLicenseTypes] ([Id], [LicenseTypeName]) VALUES (4, N'Light Motor Vehicle License')
GO
INSERT [dbo].[tblLicenseTypes] ([Id], [LicenseTypeName]) VALUES (5, N'Heavy Motor Vehicle License')
GO
SET IDENTITY_INSERT [dbo].[tblLicenseTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[tblPersonDetail] ON 
GO
INSERT [dbo].[tblPersonDetail] ([Id], [FirstName], [LastName], [PhoneNumber], [Address], [CreatedDate], [LastUpdatedDate]) VALUES (9, N'Puneet', N'Sharma', N'7009212750', N'New Zealand', CAST(N'2019-11-11T12:19:01.620' AS DateTime), CAST(N'2019-11-11T12:19:01.620' AS DateTime))
GO
INSERT [dbo].[tblPersonDetail] ([Id], [FirstName], [LastName], [PhoneNumber], [Address], [CreatedDate], [LastUpdatedDate]) VALUES (10, N'Rajan', N'Sharma', N'7009212751', N'New Zealand', CAST(N'2019-11-11T12:20:08.870' AS DateTime), CAST(N'2019-11-11T12:38:22.813' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[tblPersonDetail] OFF
GO
ALTER TABLE [dbo].[tblDriversLicenseDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblDriversLicenseDetail_tblLicenseTypes] FOREIGN KEY([LicenseTypeId])
REFERENCES [dbo].[tblLicenseTypes] ([Id])
GO
ALTER TABLE [dbo].[tblDriversLicenseDetail] CHECK CONSTRAINT [FK_tblDriversLicenseDetail_tblLicenseTypes]
GO
ALTER TABLE [dbo].[tblDriversLicenseDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblDriversLicenseDetail_tblPersonDetail] FOREIGN KEY([PersonId])
REFERENCES [dbo].[tblPersonDetail] ([Id])
GO
ALTER TABLE [dbo].[tblDriversLicenseDetail] CHECK CONSTRAINT [FK_tblDriversLicenseDetail_tblPersonDetail]
GO
