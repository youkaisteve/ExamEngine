USE [master]
GO
/****** Object:  Database [ExamSystem]    Script Date: 09/05/2015 21:21:15 ******/
CREATE DATABASE [ExamSystem] ON  PRIMARY 
( NAME = N'ExamSystem', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\ExamSystem.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ExamSystem_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\ExamSystem_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ExamSystem] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ExamSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ExamSystem] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [ExamSystem] SET ANSI_NULLS OFF
GO
ALTER DATABASE [ExamSystem] SET ANSI_PADDING OFF
GO
ALTER DATABASE [ExamSystem] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [ExamSystem] SET ARITHABORT OFF
GO
ALTER DATABASE [ExamSystem] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [ExamSystem] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [ExamSystem] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [ExamSystem] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [ExamSystem] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [ExamSystem] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [ExamSystem] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [ExamSystem] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [ExamSystem] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [ExamSystem] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [ExamSystem] SET  DISABLE_BROKER
GO
ALTER DATABASE [ExamSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [ExamSystem] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [ExamSystem] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [ExamSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [ExamSystem] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [ExamSystem] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [ExamSystem] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [ExamSystem] SET  READ_WRITE
GO
ALTER DATABASE [ExamSystem] SET RECOVERY FULL
GO
ALTER DATABASE [ExamSystem] SET  MULTI_USER
GO
ALTER DATABASE [ExamSystem] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [ExamSystem] SET DB_CHAINING OFF
GO
USE [ExamSystem]
GO
/****** Object:  Table [dbo].[WorkCategory]    Script Date: 09/05/2015 21:21:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkCategory](
	[SysNo] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_WORKCATEGORY] PRIMARY KEY CLUSTERED 
(
	[SysNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作业分类，例如社保，公司，银行等' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WorkCategory'
GO
/****** Object:  Table [dbo].[UserTeam]    Script Date: 09/05/2015 21:21:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTeam](
	[SysNo] [int] IDENTITY(1,1) NOT NULL,
	[UserSysNo] [int] NOT NULL,
	[TeamSysNo] [int] NOT NULL,
	[InUser] [nvarchar](50) NOT NULL,
	[InDate] [datetime] NOT NULL,
 CONSTRAINT [PK_USERTEAM] PRIMARY KEY CLUSTERED 
(
	[SysNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户和小组的关系表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserTeam'
GO
/****** Object:  Table [dbo].[UserFunction]    Script Date: 09/05/2015 21:21:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserFunction](
	[SysNo] [int] IDENTITY(1001,1) NOT NULL,
	[UserSysNo] [int] NOT NULL,
	[FunctionSysNo] [int] NOT NULL,
	[InDate] [datetime] NOT NULL,
	[InUserName] [nvarchar](100) NOT NULL,
	[InUserSysNo] [int] NOT NULL,
	[EditDate] [datetime] NULL,
	[EditUserName] [nvarchar](100) NULL,
	[EditUserSysNo] [int] NULL,
 CONSTRAINT [PK_UserFunctions] PRIMARY KEY CLUSTERED 
(
	[SysNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAnwser]    Script Date: 09/05/2015 21:21:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAnwser](
	[SysNo] [int] IDENTITY(1,1) NOT NULL,
	[UserSysNo] [int] NOT NULL,
	[FieldName] [nvarchar](100) NOT NULL,
	[TemplateName] [nvarchar](100) NOT NULL,
	[WorkFlowInstanceID] [int] NULL,
	[UserAnwser] [nvarchar](500) NOT NULL,
	[InDate] [datetime] NOT NULL,
 CONSTRAINT [PK_USERANWSER] PRIMARY KEY CLUSTERED 
(
	[SysNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流程实例ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserAnwser', @level2type=N'COLUMN',@level2name=N'WorkFlowInstanceID'
GO
/****** Object:  Table [dbo].[User]    Script Date: 09/05/2015 21:21:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[SysNo] [int] IDENTITY(1001,1) NOT NULL,
	[UserID] [nvarchar](100) NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[UserAuthCode] [nvarchar](100) NOT NULL,
	[Status] [int] NOT NULL,
	[InDate] [datetime] NOT NULL,
	[InUserName] [nvarchar](100) NOT NULL,
	[InUserSysNo] [int] NOT NULL,
	[EditDate] [datetime] NULL,
	[EditUserName] [nvarchar](100) NULL,
	[EditUserSysNo] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[SysNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Team]    Script Date: 09/05/2015 21:21:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Team](
	[SysNo] [int] IDENTITY(1,1) NOT NULL,
	[TeamName] [nvarchar](50) NOT NULL,
	[InDate] [datetime] NOT NULL,
	[InUser] [nvarchar](50) NOT NULL,
	[EditDate] [datetime] NULL,
	[EditUser] [nvarchar](50) NULL,
 CONSTRAINT [PK_TEAM] PRIMARY KEY CLUSTERED 
(
	[SysNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'小组表，可设置小组名，例如计网一班一小组' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Team'
GO
/****** Object:  Table [dbo].[StandardAnwser]    Script Date: 09/05/2015 21:21:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StandardAnwser](
	[SysNo] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Anwser] [nvarchar](500) NOT NULL,
	[FieldName] [nvarchar](100) NOT NULL,
	[TemplateName] [nvarchar](100) NOT NULL,
	[InDate] [datetime] NOT NULL,
	[InUser] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_STANDARDANWSER] PRIMARY KEY CLUSTERED 
(
	[SysNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'该字段需要自动以规则，例如ID_Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StandardAnwser', @level2type=N'COLUMN',@level2name=N'FieldName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模板名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'StandardAnwser', @level2type=N'COLUMN',@level2name=N'TemplateName'
GO
/****** Object:  Table [dbo].[RoleUser]    Script Date: 09/05/2015 21:21:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleUser](
	[SysNo] [int] IDENTITY(1001,1) NOT NULL,
	[RoleSysNo] [int] NOT NULL,
	[UserSysNo] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[InDate] [datetime] NOT NULL,
	[InUserName] [nvarchar](100) NOT NULL,
	[InUserSysNo] [int] NOT NULL,
	[EditDate] [datetime] NULL,
	[EditUserName] [nvarchar](100) NULL,
	[EditUserSysNo] [int] NULL,
 CONSTRAINT [PK_RoleUsers] PRIMARY KEY CLUSTERED 
(
	[SysNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleFunction]    Script Date: 09/05/2015 21:21:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleFunction](
	[SysNo] [int] IDENTITY(1001,1) NOT NULL,
	[RoleSysNo] [int] NOT NULL,
	[FunctionSysNo] [int] NOT NULL,
	[InDate] [datetime] NOT NULL,
	[InUserName] [nvarchar](100) NOT NULL,
	[InUserSysNo] [int] NOT NULL,
	[EditDate] [datetime] NULL,
	[EditUserName] [nvarchar](100) NULL,
	[EditUserSysNo] [int] NULL,
 CONSTRAINT [PK_RoleFunctions] PRIMARY KEY CLUSTERED 
(
	[SysNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 09/05/2015 21:21:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[SysNo] [int] IDENTITY(1001,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[Status] [int] NOT NULL,
	[InDate] [datetime] NOT NULL,
	[InUserName] [nvarchar](100) NOT NULL,
	[InUserSysNo] [int] NOT NULL,
	[EditDate] [datetime] NULL,
	[EditUserName] [nvarchar](100) NULL,
	[EditUserSysNo] [int] NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[SysNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Function]    Script Date: 09/05/2015 21:21:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Function](
	[SysNo] [int] IDENTITY(1001,1) NOT NULL,
	[FunctionName] [nvarchar](200) NULL,
	[FunctionKey] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[Status] [int] NULL,
	[InDate] [datetime] NOT NULL,
	[InUserName] [nvarchar](100) NOT NULL,
	[InUserSysNo] [int] NOT NULL,
	[EditDate] [datetime] NULL,
	[EditUserName] [nvarchar](100) NULL,
	[EditUserSysNo] [int] NULL,
 CONSTRAINT [PK_Functions] PRIMARY KEY CLUSTERED 
(
	[SysNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
