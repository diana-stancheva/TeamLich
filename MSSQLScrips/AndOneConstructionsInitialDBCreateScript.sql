USE [master]
GO
/****** Object:  Database [AndOneConstructions]    Script Date: 8/26/2014 1:53:03 PM ******/
CREATE DATABASE [AndOneConstructions]
GO
ALTER DATABASE [AndOneConstructions] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AndOneConstructions].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AndOneConstructions] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AndOneConstructions] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AndOneConstructions] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AndOneConstructions] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AndOneConstructions] SET ARITHABORT OFF 
GO
ALTER DATABASE [AndOneConstructions] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AndOneConstructions] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AndOneConstructions] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AndOneConstructions] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AndOneConstructions] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AndOneConstructions] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AndOneConstructions] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AndOneConstructions] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AndOneConstructions] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AndOneConstructions] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AndOneConstructions] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AndOneConstructions] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AndOneConstructions] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AndOneConstructions] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AndOneConstructions] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AndOneConstructions] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AndOneConstructions] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AndOneConstructions] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AndOneConstructions] SET  MULTI_USER 
GO
ALTER DATABASE [AndOneConstructions] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AndOneConstructions] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AndOneConstructions] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AndOneConstructions] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [AndOneConstructions] SET DELAYED_DURABILITY = DISABLED 
GO
USE [AndOneConstructions]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 8/26/2014 1:53:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[AddressId] [int] IDENTITY(1,1) NOT NULL,
	[Details] [nvarchar](100) NOT NULL,
	[TownId] [int] NOT NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Buildings]    Script Date: 8/26/2014 1:53:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Buildings](
	[BuildingId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ConstructionSiteId] [int] NOT NULL,
 CONSTRAINT [PK_Buildings] PRIMARY KEY CLUSTERED 
(
	[BuildingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Clients]    Script Date: 8/26/2014 1:53:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[ClientId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ConstructionSites]    Script Date: 8/26/2014 1:53:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConstructionSites](
	[ConstructionSiteId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[AddressId] [int] NOT NULL,
 CONSTRAINT [PK_ConstructionSites] PRIMARY KEY CLUSTERED 
(
	[ConstructionSiteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Departments]    Script Date: 8/26/2014 1:53:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employees]    Script Date: 8/26/2014 1:53:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentId] [int] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EmployeesProjects]    Script Date: 8/26/2014 1:53:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeesProjects](
	[EmployeeId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
 CONSTRAINT [PK_EmployeesProjects] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC,
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Projects]    Script Date: 8/26/2014 1:53:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[ProjectId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[ClientId] [int] NOT NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Towns]    Script Date: 8/26/2014 1:53:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Towns](
	[TownId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Towns] PRIMARY KEY CLUSTERED 
(
	[TownId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Addresses] ON 

INSERT [dbo].[Addresses] ([AddressId], [Details], [TownId]) VALUES (1, N'Alexander Malinov 33 blv.', 1)
INSERT [dbo].[Addresses] ([AddressId], [Details], [TownId]) VALUES (4, N'Serdika 18 str.', 1)
INSERT [dbo].[Addresses] ([AddressId], [Details], [TownId]) VALUES (6, N'Gen. Skobelev 18 str.', 2)
INSERT [dbo].[Addresses] ([AddressId], [Details], [TownId]) VALUES (8, N'Alexander Stamboliyski 10 str.', 3)
SET IDENTITY_INSERT [dbo].[Addresses] OFF
SET IDENTITY_INSERT [dbo].[Buildings] ON 

INSERT [dbo].[Buildings] ([BuildingId], [Name], [ConstructionSiteId]) VALUES (1, N'Warehouse Main', 2)
INSERT [dbo].[Buildings] ([BuildingId], [Name], [ConstructionSiteId]) VALUES (2, N'Warehouse Office', 2)
INSERT [dbo].[Buildings] ([BuildingId], [Name], [ConstructionSiteId]) VALUES (3, N'House one', 5)
INSERT [dbo].[Buildings] ([BuildingId], [Name], [ConstructionSiteId]) VALUES (4, N'Main Car Garage', 1)
INSERT [dbo].[Buildings] ([BuildingId], [Name], [ConstructionSiteId]) VALUES (5, N'Repair Garage', 1)
INSERT [dbo].[Buildings] ([BuildingId], [Name], [ConstructionSiteId]) VALUES (6, N'Personel Building', 1)
SET IDENTITY_INSERT [dbo].[Buildings] OFF
SET IDENTITY_INSERT [dbo].[Clients] ON 

INSERT [dbo].[Clients] ([ClientId], [Name]) VALUES (1, N'Bamboo Foods LTD')
INSERT [dbo].[Clients] ([ClientId], [Name]) VALUES (2, N'SnD Cars Associates')
INSERT [dbo].[Clients] ([ClientId], [Name]) VALUES (3, N'Stankov lawyers And Co')
SET IDENTITY_INSERT [dbo].[Clients] OFF
SET IDENTITY_INSERT [dbo].[ConstructionSites] ON 

INSERT [dbo].[ConstructionSites] ([ConstructionSiteId], [Name], [ProjectId], [AddressId]) VALUES (1, N'Garage Site ', 1, 4)
INSERT [dbo].[ConstructionSites] ([ConstructionSiteId], [Name], [ProjectId], [AddressId]) VALUES (2, N'Warehouse Site', 3, 6)
INSERT [dbo].[ConstructionSites] ([ConstructionSiteId], [Name], [ProjectId], [AddressId]) VALUES (5, N'Office Site', 5, 1)
SET IDENTITY_INSERT [dbo].[ConstructionSites] OFF
SET IDENTITY_INSERT [dbo].[Departments] ON 

INSERT [dbo].[Departments] ([DepartmentId], [Name]) VALUES (1, N'Architecture')
INSERT [dbo].[Departments] ([DepartmentId], [Name]) VALUES (2, N'Engineering')
INSERT [dbo].[Departments] ([DepartmentId], [Name]) VALUES (3, N'Finance')
SET IDENTITY_INSERT [dbo].[Departments] OFF
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([EmployeeId], [DepartmentId], [FirstName], [LastName]) VALUES (2, 1, N'Peter', N'Ivanov')
INSERT [dbo].[Employees] ([EmployeeId], [DepartmentId], [FirstName], [LastName]) VALUES (3, 2, N'Ivan', N'Stoyanov')
INSERT [dbo].[Employees] ([EmployeeId], [DepartmentId], [FirstName], [LastName]) VALUES (4, 2, N'Mariya', N'Petkova')
INSERT [dbo].[Employees] ([EmployeeId], [DepartmentId], [FirstName], [LastName]) VALUES (5, 3, N'Yana', N'Zhekova')
SET IDENTITY_INSERT [dbo].[Employees] OFF
INSERT [dbo].[EmployeesProjects] ([EmployeeId], [ProjectId]) VALUES (2, 1)
INSERT [dbo].[EmployeesProjects] ([EmployeeId], [ProjectId]) VALUES (2, 3)
INSERT [dbo].[EmployeesProjects] ([EmployeeId], [ProjectId]) VALUES (3, 1)
INSERT [dbo].[EmployeesProjects] ([EmployeeId], [ProjectId]) VALUES (3, 5)
INSERT [dbo].[EmployeesProjects] ([EmployeeId], [ProjectId]) VALUES (4, 3)
INSERT [dbo].[EmployeesProjects] ([EmployeeId], [ProjectId]) VALUES (4, 5)
INSERT [dbo].[EmployeesProjects] ([EmployeeId], [ProjectId]) VALUES (5, 1)
SET IDENTITY_INSERT [dbo].[Projects] ON 

INSERT [dbo].[Projects] ([ProjectId], [Name], [StartDate], [EndDate], [ClientId]) VALUES (1, N'Industrial Garage Construction', CAST(N'2013-10-23' AS Date), NULL, 2)
INSERT [dbo].[Projects] ([ProjectId], [Name], [StartDate], [EndDate], [ClientId]) VALUES (3, N'Goods Warehouse', CAST(N'2012-10-23' AS Date), CAST(N'2014-01-23' AS Date), 1)
INSERT [dbo].[Projects] ([ProjectId], [Name], [StartDate], [EndDate], [ClientId]) VALUES (5, N'Office Building', CAST(N'2013-02-01' AS Date), NULL, 3)
SET IDENTITY_INSERT [dbo].[Projects] OFF
SET IDENTITY_INSERT [dbo].[Towns] ON 

INSERT [dbo].[Towns] ([TownId], [Name]) VALUES (1, N'Sofia')
INSERT [dbo].[Towns] ([TownId], [Name]) VALUES (2, N'Plovdiv')
INSERT [dbo].[Towns] ([TownId], [Name]) VALUES (3, N'Burgas')
SET IDENTITY_INSERT [dbo].[Towns] OFF
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Towns] FOREIGN KEY([TownId])
REFERENCES [dbo].[Towns] ([TownId])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_Towns]
GO
ALTER TABLE [dbo].[Buildings]  WITH CHECK ADD  CONSTRAINT [FK_Buildings_ConstructionSites] FOREIGN KEY([ConstructionSiteId])
REFERENCES [dbo].[ConstructionSites] ([ConstructionSiteId])
GO
ALTER TABLE [dbo].[Buildings] CHECK CONSTRAINT [FK_Buildings_ConstructionSites]
GO
ALTER TABLE [dbo].[ConstructionSites]  WITH CHECK ADD  CONSTRAINT [FK_ConstructionSites_Addresses] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Addresses] ([AddressId])
GO
ALTER TABLE [dbo].[ConstructionSites] CHECK CONSTRAINT [FK_ConstructionSites_Addresses]
GO
ALTER TABLE [dbo].[ConstructionSites]  WITH CHECK ADD  CONSTRAINT [FK_ConstructionSites_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([ProjectId])
GO
ALTER TABLE [dbo].[ConstructionSites] CHECK CONSTRAINT [FK_ConstructionSites_Projects]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Departments] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([DepartmentId])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Departments]
GO
ALTER TABLE [dbo].[EmployeesProjects]  WITH CHECK ADD  CONSTRAINT [FK_EmployeesProjects_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([EmployeeId])
GO
ALTER TABLE [dbo].[EmployeesProjects] CHECK CONSTRAINT [FK_EmployeesProjects_Employees]
GO
ALTER TABLE [dbo].[EmployeesProjects]  WITH CHECK ADD  CONSTRAINT [FK_EmployeesProjects_Projects] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([ProjectId])
GO
ALTER TABLE [dbo].[EmployeesProjects] CHECK CONSTRAINT [FK_EmployeesProjects_Projects]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Projects_Clients] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Clients] ([ClientId])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Projects_Clients]
GO
USE [master]
GO
ALTER DATABASE [AndOneConstructions] SET  READ_WRITE 
GO
