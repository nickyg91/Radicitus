USE [master]
GO

/****** Object:  Database [LeafGreen-Dev]    Script Date: 1/18/2018 9:10:56 PM ******/
CREATE DATABASE [LeafGreen-Dev]
GO

ALTER DATABASE [LeafGreen-Dev] SET COMPATIBILITY_LEVEL = 130
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LeafGreen-Dev].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [LeafGreen-Dev] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [LeafGreen-Dev] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [LeafGreen-Dev] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [LeafGreen-Dev] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [LeafGreen-Dev] SET ARITHABORT OFF 
GO

ALTER DATABASE [LeafGreen-Dev] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [LeafGreen-Dev] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [LeafGreen-Dev] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [LeafGreen-Dev] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [LeafGreen-Dev] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [LeafGreen-Dev] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [LeafGreen-Dev] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [LeafGreen-Dev] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [LeafGreen-Dev] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [LeafGreen-Dev] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [LeafGreen-Dev] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO

ALTER DATABASE [LeafGreen-Dev] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [LeafGreen-Dev] SET READ_COMMITTED_SNAPSHOT ON 
GO

ALTER DATABASE [LeafGreen-Dev] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [LeafGreen-Dev] SET  MULTI_USER 
GO

ALTER DATABASE [LeafGreen-Dev] SET DB_CHAINING OFF 
GO

ALTER DATABASE [LeafGreen-Dev] SET ENCRYPTION ON
GO

ALTER DATABASE [LeafGreen-Dev] SET QUERY_STORE = ON
GO

ALTER DATABASE [LeafGreen-Dev] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 7), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 10, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO)
GO

ALTER DATABASE [LeafGreen-Dev] SET  READ_WRITE 
GO




USE [LeafGreen-Dev]
GO
/****** Object:  User [radsqluser]    Script Date: 1/18/2018 9:09:30 PM ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'radsqluser')
CREATE USER [radsqluser] FOR LOGIN [radsqluser] WITH DEFAULT_SCHEMA=[rad]
GO
/****** Object:  DatabaseRole [rRadicitusRole]    Script Date: 1/18/2018 9:09:31 PM ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'rRadicitusRole' AND type = 'R')
CREATE ROLE [rRadicitusRole]
GO
ALTER ROLE [rRadicitusRole] ADD MEMBER [radsqluser]
GO
ALTER ROLE [db_owner] ADD MEMBER [radsqluser]
GO
/****** Object:  Schema [rad]    Script Date: 1/18/2018 9:09:33 PM ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'rad')
EXEC sys.sp_executesql N'CREATE SCHEMA [rad]'

GO
/****** Object:  Table [rad].[Grid]    Script Date: 1/18/2018 9:09:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[rad].[Grid]') AND type in (N'U'))
BEGIN
CREATE TABLE [rad].[Grid](
	[GridId] [int] IDENTITY(1,1) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[GridName] [varchar](50) NULL,
	[CostPerSquare] [int] NOT NULL,
 CONSTRAINT [PK_Grid] PRIMARY KEY CLUSTERED 
(
	[GridId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
END
GO
/****** Object:  Table [rad].[RadGridNumber]    Script Date: 1/18/2018 9:09:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[rad].[RadGridNumber]') AND type in (N'U'))
BEGIN
CREATE TABLE [rad].[RadGridNumber](
	[RadNumberId] [int] IDENTITY(1,1) NOT NULL,
	[GridId] [int] NOT NULL,
	[GridNumber] [int] NOT NULL,
	[RadMemberName] [nvarchar](250) NOT NULL,
	[HasPaid] [bit] NOT NULL,
 CONSTRAINT [PK_RadGridNumber] PRIMARY KEY CLUSTERED 
(
	[RadNumberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)
END
GO
SET IDENTITY_INSERT [rad].[Grid] ON 

GO
INSERT [rad].[Grid] ([GridId], [DateCreated], [GridName], [CostPerSquare]) VALUES (1, CAST(N'2017-12-24T23:33:00.8133333' AS DateTime2), N'Test1', 500)
GO
INSERT [rad].[Grid] ([GridId], [DateCreated], [GridName], [CostPerSquare]) VALUES (2, CAST(N'2017-12-24T23:33:04.9833333' AS DateTime2), N'Test2', 500)
GO
INSERT [rad].[Grid] ([GridId], [DateCreated], [GridName], [CostPerSquare]) VALUES (3, CAST(N'2017-12-24T23:33:07.8766667' AS DateTime2), N'Test3', 500)
GO
INSERT [rad].[Grid] ([GridId], [DateCreated], [GridName], [CostPerSquare]) VALUES (4, CAST(N'2017-12-24T23:33:10.7800000' AS DateTime2), N'Test4', 500)
GO
INSERT [rad].[Grid] ([GridId], [DateCreated], [GridName], [CostPerSquare]) VALUES (5, CAST(N'2017-12-24T23:33:12.4066667' AS DateTime2), N'Test5', 500)
GO
INSERT [rad].[Grid] ([GridId], [DateCreated], [GridName], [CostPerSquare]) VALUES (6, CAST(N'2017-12-24T23:33:16.2366667' AS DateTime2), N'Test6', 500)
GO
INSERT [rad].[Grid] ([GridId], [DateCreated], [GridName], [CostPerSquare]) VALUES (8, CAST(N'2017-12-25T01:52:14.7533333' AS DateTime2), N'Red Test', 500)
GO
INSERT [rad].[Grid] ([GridId], [DateCreated], [GridName], [CostPerSquare]) VALUES (9, CAST(N'2017-12-25T01:52:26.8966667' AS DateTime2), N'Red Test', 500)
GO
INSERT [rad].[Grid] ([GridId], [DateCreated], [GridName], [CostPerSquare]) VALUES (10, CAST(N'2017-12-25T02:41:00.8200000' AS DateTime2), N'BlueTest', 85)
GO
INSERT [rad].[Grid] ([GridId], [DateCreated], [GridName], [CostPerSquare]) VALUES (11, CAST(N'2017-12-25T02:41:24.7433333' AS DateTime2), N'GreenTest', 85)
GO
INSERT [rad].[Grid] ([GridId], [DateCreated], [GridName], [CostPerSquare]) VALUES (12, CAST(N'2017-12-25T02:43:26.5900000' AS DateTime2), N'Add another', 500)
GO
INSERT [rad].[Grid] ([GridId], [DateCreated], [GridName], [CostPerSquare]) VALUES (13, CAST(N'2017-12-25T02:50:05.3200000' AS DateTime2), N'This Has A Name', 500)
GO
INSERT [rad].[Grid] ([GridId], [DateCreated], [GridName], [CostPerSquare]) VALUES (14, CAST(N'2018-01-08T00:17:27.7133333' AS DateTime2), N'This is another ', 500)
GO
SET IDENTITY_INSERT [rad].[Grid] OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[rad].[DF_RadGridNumber_HasPaid]') AND type = 'D')
BEGIN
ALTER TABLE [rad].[RadGridNumber] ADD  CONSTRAINT [DF_RadGridNumber_HasPaid]  DEFAULT ((0)) FOR [HasPaid]
END

GO
