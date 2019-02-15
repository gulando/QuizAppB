USE [master]
GO
/****** Object:  Database [QuizDB]    Script Date: 2/15/2019 2:48:35 PM ******/
CREATE DATABASE [QuizDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuizDB', FILENAME = N'C:\Users\STC Armenia\QuizDB.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QuizDB_log', FILENAME = N'C:\Users\STC Armenia\QuizDB_log.ldf' , SIZE = 816KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [QuizDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuizDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuizDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuizDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuizDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuizDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuizDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuizDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [QuizDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuizDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuizDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuizDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuizDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuizDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuizDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuizDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuizDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QuizDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuizDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuizDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuizDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuizDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuizDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuizDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuizDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QuizDB] SET  MULTI_USER 
GO
ALTER DATABASE [QuizDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuizDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuizDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuizDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [QuizDB] SET DELAYED_DURABILITY = DISABLED 
GO
USE [QuizDB]
GO
/****** Object:  Table [dbo].[Answers]    Script Date: 2/15/2019 2:48:35 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answers](
	[AnswerID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionID] [int] NOT NULL,
	[AnswerTypeID] [int] NOT NULL,
	[AnswerText] [nvarchar](max) NULL,
 CONSTRAINT [Answers_pk] PRIMARY KEY NONCLUSTERED 
(
	[AnswerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AnswerTypes]    Script Date: 2/15/2019 2:51:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnswerTypes](
	[AnswerTypeID] [int] IDENTITY(1,1) NOT NULL,
	[QuizID] [int] NOT NULL,
	[QuestionTypeID] [int] NOT NULL,
	[AnswerTypeName] [nvarchar](max) NULL,
 CONSTRAINT [AnswerType_pk] PRIMARY KEY NONCLUSTERED 
(
	[AnswerTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Questions]    Script Date: 2/15/2019 2:51:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questions](
	[QuestionID] [int] IDENTITY(1,1) NOT NULL,
	[QuizID] [int] NULL,
	[QuizThemeID] [int] NOT NULL,
	[AnswerTypeID] [int] NULL,
	[QuestionTypeID] [int] NULL,
	[QuestionImage] [image] NULL,
	[CorrectAnswer] [nvarchar](max) NULL,
 CONSTRAINT [Questions_pk] PRIMARY KEY NONCLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionTypes]    Script Date: 2/15/2019 2:51:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionTypes](
	[QuestionTypeID] [int] IDENTITY(1,1) NOT NULL,
	[QuizID] [int] NULL,
	[QuestionTypeName] [nvarchar](max) NULL,
 CONSTRAINT [QuestionType_pk] PRIMARY KEY NONCLUSTERED 
(
	[QuestionTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quizes]    Script Date: 2/15/2019 2:51:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quizes](
	[QuizID] [int] IDENTITY(1,1) NOT NULL,
	[QuizName] [nvarchar](max) NULL,
 CONSTRAINT [Quiz_pk] PRIMARY KEY NONCLUSTERED 
(
	[QuizID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuizThemes]    Script Date: 2/15/2019 2:51:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuizThemes](
	[QuizThemeID] [int] IDENTITY(1,1) NOT NULL,
	[QuizID] [int] NOT NULL,
	[QuizThemeName] [nvarchar](max) NULL,
 CONSTRAINT [QuizThemes_pk] PRIMARY KEY NONCLUSTERED 
(
	[QuizThemeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rights]    Script Date: 2/15/2019 2:51:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rights](
	[RightID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Rights] PRIMARY KEY CLUSTERED 
(
	[RightID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 2/15/2019 2:51:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2/15/2019 2:51:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[VwQuizThemes]    Script Date: 2/15/2019 2:51:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VwQuizThemes]
AS
SELECT        dbo.Quizes.QuizID, dbo.Quizes.QuizName, dbo.QuizThemes.QuizThemeID, dbo.QuizThemes.QuizThemeName
FROM            dbo.Quizes INNER JOIN
                         dbo.QuizThemes ON dbo.Quizes.QuizID = dbo.QuizThemes.QuizID
GO
/****** Object:  Index [Answers_AnswerID_uindex]    Script Date: 2/15/2019 2:51:41 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [Answers_AnswerID_uindex] ON [dbo].[Answers]
(
	[AnswerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [AnswerType_AnswerTypeID_uindex]    Script Date: 2/15/2019 2:51:41 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [AnswerType_AnswerTypeID_uindex] ON [dbo].[AnswerTypes]
(
	[AnswerTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Questions_QuestionID_uindex]    Script Date: 2/15/2019 2:51:41 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [Questions_QuestionID_uindex] ON [dbo].[Questions]
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [QuestionType_QuestionTypeID_uindex]    Script Date: 2/15/2019 2:51:41 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [QuestionType_QuestionTypeID_uindex] ON [dbo].[QuestionTypes]
(
	[QuestionTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Quiz_QuizID_uindex]    Script Date: 2/15/2019 2:51:41 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [Quiz_QuizID_uindex] ON [dbo].[Quizes]
(
	[QuizID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [QuizThemes_QuizThemeID_uindex]    Script Date: 2/15/2019 2:51:41 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [QuizThemes_QuizThemeID_uindex] ON [dbo].[QuizThemes]
(
	[QuizThemeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Answers]  WITH CHECK ADD  CONSTRAINT [Answers_Questions__fk] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Questions] ([QuestionID])
GO
ALTER TABLE [dbo].[Answers] CHECK CONSTRAINT [Answers_Questions__fk]
GO
ALTER TABLE [dbo].[AnswerTypes]  WITH CHECK ADD  CONSTRAINT [AnswerTypes_QuestionTypes_fk] FOREIGN KEY([QuestionTypeID])
REFERENCES [dbo].[QuestionTypes] ([QuestionTypeID])
GO
ALTER TABLE [dbo].[AnswerTypes] CHECK CONSTRAINT [AnswerTypes_QuestionTypes_fk]
GO
ALTER TABLE [dbo].[AnswerTypes]  WITH CHECK ADD  CONSTRAINT [Quizes_AnswerTypes__fk] FOREIGN KEY([QuizID])
REFERENCES [dbo].[Quizes] ([QuizID])
GO
ALTER TABLE [dbo].[AnswerTypes] CHECK CONSTRAINT [Quizes_AnswerTypes__fk]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [Questions_AnswerTypes__fk] FOREIGN KEY([AnswerTypeID])
REFERENCES [dbo].[AnswerTypes] ([AnswerTypeID])
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [Questions_AnswerTypes__fk]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [Questions_QuestionTypes__fk] FOREIGN KEY([QuestionTypeID])
REFERENCES [dbo].[QuestionTypes] ([QuestionTypeID])
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [Questions_QuestionTypes__fk]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [Questions_Quizes__fk] FOREIGN KEY([QuizID])
REFERENCES [dbo].[Quizes] ([QuizID])
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [Questions_Quizes__fk]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [Questions_QuizThemes__fk] FOREIGN KEY([QuizThemeID])
REFERENCES [dbo].[QuizThemes] ([QuizThemeID])
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [Questions_QuizThemes__fk]
GO
ALTER TABLE [dbo].[QuestionTypes]  WITH CHECK ADD  CONSTRAINT [QuestionTypes_Quizes__fk] FOREIGN KEY([QuizID])
REFERENCES [dbo].[Quizes] ([QuizID])
GO
ALTER TABLE [dbo].[QuestionTypes] CHECK CONSTRAINT [QuestionTypes_Quizes__fk]
GO
ALTER TABLE [dbo].[QuizThemes]  WITH CHECK ADD  CONSTRAINT [Quizes_QuizThemes__fk] FOREIGN KEY([QuizID])
REFERENCES [dbo].[Quizes] ([QuizID])
GO
ALTER TABLE [dbo].[QuizThemes] CHECK CONSTRAINT [Quizes_QuizThemes__fk]
GO
/****** Object:  StoredProcedure [dbo].[GetQuizThemesSummary]    Script Date: 2/15/2019 2:51:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetQuizThemesSummary]
AS
BEGIN

	SELECT 
			QuizThemes.QuizThemeID AS QuizThemeID, 
			Quizes.QuizID,
			Quizes.QuizName,
			QuizThemes.QuizThemeName
	FROM Quizes
	INNER JOIN
	QuizThemes ON Quizes.QuizID = QuizThemes.QuizID
	ORDER BY Quizes.QuizID
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Quizes"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 238
               Right = 214
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "QuizThemes"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 119
               Right = 428
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VwQuizThemes'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VwQuizThemes'
GO
USE [master]
GO
ALTER DATABASE [QuizDB] SET  READ_WRITE 
GO
