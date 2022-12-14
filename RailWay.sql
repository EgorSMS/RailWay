USE [RailWay]
GO
/****** Object:  Table [dbo].[Cities]    Script Date: 04.06.2022 22:26:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[ID_City] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[PlatformCount] [int] NOT NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED 
(
	[ID_City] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doljnost]    Script Date: 04.06.2022 22:26:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doljnost](
	[ID_Doljnost] [int] IDENTITY(1,1) NOT NULL,
	[NameOfDolj] [varchar](max) NOT NULL,
	[Salary] [money] NOT NULL,
 CONSTRAINT [PK_Doljnost] PRIMARY KEY CLUSTERED 
(
	[ID_Doljnost] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 04.06.2022 22:26:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[ID_Role] [int] IDENTITY(1,1) NOT NULL,
	[NameOfRole] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID_Role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Route]    Script Date: 04.06.2022 22:26:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Route](
	[ID_Route] [int] IDENTITY(1,1) NOT NULL,
	[ID_City_Departure] [int] NOT NULL,
	[ID_City_Arrival] [int] NOT NULL,
	[PlatformDeparture] [int] NOT NULL,
	[PlatformArrival] [int] NOT NULL,
 CONSTRAINT [PK_Route] PRIMARY KEY CLUSTERED 
(
	[ID_Route] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 04.06.2022 22:26:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[ID_Staff] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [varchar](max) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Firdname] [varchar](max) NULL,
	[Snils] [varchar](11) NOT NULL,
	[INN] [varchar](11) NOT NULL,
	[SeriaPass] [varchar](4) NOT NULL,
	[NumberPass] [varchar](6) NOT NULL,
	[Gender] [bit] NOT NULL,
	[ID_Doljnost] [int] NOT NULL,
 CONSTRAINT [PK_Staff] PRIMARY KEY CLUSTERED 
(
	[ID_Staff] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StaffOfTeam]    Script Date: 04.06.2022 22:26:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StaffOfTeam](
	[ID_SOT] [int] IDENTITY(1,1) NOT NULL,
	[ID_Staff1] [int] NOT NULL,
	[ID_Staff2] [int] NOT NULL,
	[ID_Train] [int] NOT NULL,
 CONSTRAINT [PK_StaffOfTeam] PRIMARY KEY CLUSTERED 
(
	[ID_SOT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stops]    Script Date: 04.06.2022 22:26:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stops](
	[ID_Stop] [int] IDENTITY(1,1) NOT NULL,
	[ID_City] [int] NOT NULL,
	[ID_TimeTable] [int] NOT NULL,
	[TimeOfStop] [datetime] NOT NULL,
	[Platform] [int] NOT NULL,
 CONSTRAINT [PK_Stops] PRIMARY KEY CLUSTERED 
(
	[ID_Stop] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeTable]    Script Date: 04.06.2022 22:26:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeTable](
	[ID_TimeTable] [int] IDENTITY(1,1) NOT NULL,
	[DateTimeArrived] [datetime] NOT NULL,
	[DateTimeDeparted] [datetime] NOT NULL,
	[ID_Train] [int] NOT NULL,
	[ID_Route] [int] NOT NULL,
 CONSTRAINT [PK_TimeTable] PRIMARY KEY CLUSTERED 
(
	[ID_TimeTable] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Train]    Script Date: 04.06.2022 22:26:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Train](
	[ID_Train] [int] IDENTITY(1,1) NOT NULL,
	[ID_TypeOfTrain] [int] NOT NULL,
	[NumberOfTrain] [int] NOT NULL,
 CONSTRAINT [PK_Train] PRIMARY KEY CLUSTERED 
(
	[ID_Train] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeOfTrain]    Script Date: 04.06.2022 22:26:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeOfTrain](
	[ID_TypeOfTrain] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[MaxSpeed] [varchar](max) NOT NULL,
	[Capacity] [int] NOT NULL,
 CONSTRAINT [PK_TypeOfTrain] PRIMARY KEY CLUSTERED 
(
	[ID_TypeOfTrain] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 04.06.2022 22:26:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID_User] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [varchar](max) NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[Firdname] [varchar](max) NULL,
	[Snils] [varchar](11) NOT NULL,
	[INN] [varchar](11) NOT NULL,
	[SeriaPass] [varchar](4) NOT NULL,
	[NumberPass] [varchar](6) NOT NULL,
	[Gender] [bit] NOT NULL,
	[Login] [varchar](max) NOT NULL,
	[Password] [varchar](max) NOT NULL,
	[ID_Role] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID_User] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Route]  WITH CHECK ADD  CONSTRAINT [FK_Cities_Route_Arrival] FOREIGN KEY([ID_City_Arrival])
REFERENCES [dbo].[Cities] ([ID_City])
GO
ALTER TABLE [dbo].[Route] CHECK CONSTRAINT [FK_Cities_Route_Arrival]
GO
ALTER TABLE [dbo].[Route]  WITH CHECK ADD  CONSTRAINT [FK_Cities_Route_Departure] FOREIGN KEY([ID_City_Departure])
REFERENCES [dbo].[Cities] ([ID_City])
GO
ALTER TABLE [dbo].[Route] CHECK CONSTRAINT [FK_Cities_Route_Departure]
GO
ALTER TABLE [dbo].[Staff]  WITH CHECK ADD  CONSTRAINT [FK_Doljnost_Staff] FOREIGN KEY([ID_Doljnost])
REFERENCES [dbo].[Doljnost] ([ID_Doljnost])
GO
ALTER TABLE [dbo].[Staff] CHECK CONSTRAINT [FK_Doljnost_Staff]
GO
ALTER TABLE [dbo].[StaffOfTeam]  WITH CHECK ADD  CONSTRAINT [FK_Staff1_StaffOfTeam] FOREIGN KEY([ID_Staff1])
REFERENCES [dbo].[Staff] ([ID_Staff])
GO
ALTER TABLE [dbo].[StaffOfTeam] CHECK CONSTRAINT [FK_Staff1_StaffOfTeam]
GO
ALTER TABLE [dbo].[StaffOfTeam]  WITH CHECK ADD  CONSTRAINT [FK_Staff2_StaffOfTeam] FOREIGN KEY([ID_Staff2])
REFERENCES [dbo].[Staff] ([ID_Staff])
GO
ALTER TABLE [dbo].[StaffOfTeam] CHECK CONSTRAINT [FK_Staff2_StaffOfTeam]
GO
ALTER TABLE [dbo].[StaffOfTeam]  WITH CHECK ADD  CONSTRAINT [FK_Train_StaffOfTeam] FOREIGN KEY([ID_Train])
REFERENCES [dbo].[Train] ([ID_Train])
GO
ALTER TABLE [dbo].[StaffOfTeam] CHECK CONSTRAINT [FK_Train_StaffOfTeam]
GO
ALTER TABLE [dbo].[Stops]  WITH CHECK ADD  CONSTRAINT [FK_Cities_Stops] FOREIGN KEY([ID_City])
REFERENCES [dbo].[Cities] ([ID_City])
GO
ALTER TABLE [dbo].[Stops] CHECK CONSTRAINT [FK_Cities_Stops]
GO
ALTER TABLE [dbo].[Stops]  WITH CHECK ADD  CONSTRAINT [FK_TimeTable_Stops] FOREIGN KEY([ID_TimeTable])
REFERENCES [dbo].[TimeTable] ([ID_TimeTable])
GO
ALTER TABLE [dbo].[Stops] CHECK CONSTRAINT [FK_TimeTable_Stops]
GO
ALTER TABLE [dbo].[TimeTable]  WITH CHECK ADD  CONSTRAINT [FK_Route_TimeTable] FOREIGN KEY([ID_Route])
REFERENCES [dbo].[Route] ([ID_Route])
GO
ALTER TABLE [dbo].[TimeTable] CHECK CONSTRAINT [FK_Route_TimeTable]
GO
ALTER TABLE [dbo].[TimeTable]  WITH CHECK ADD  CONSTRAINT [FK_Train_TimeTable] FOREIGN KEY([ID_Train])
REFERENCES [dbo].[Train] ([ID_Train])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TimeTable] CHECK CONSTRAINT [FK_Train_TimeTable]
GO
ALTER TABLE [dbo].[Train]  WITH CHECK ADD  CONSTRAINT [FK_TypeOfTrain_Train] FOREIGN KEY([ID_TypeOfTrain])
REFERENCES [dbo].[TypeOfTrain] ([ID_TypeOfTrain])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Train] CHECK CONSTRAINT [FK_TypeOfTrain_Train]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_Role_User] FOREIGN KEY([ID_Role])
REFERENCES [dbo].[Role] ([ID_Role])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_Role_User]
GO
