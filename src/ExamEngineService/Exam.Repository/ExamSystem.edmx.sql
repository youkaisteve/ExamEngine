
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/09/2015 20:25:49
-- Generated from EDMX file: E:\Git\ExamEngine\src\ExamEngineService\Exam.Repository\ExamSystem.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ExamSystem];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Role];
GO
IF OBJECT_ID(N'[dbo].[RoleUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleUser];
GO
IF OBJECT_ID(N'[dbo].[StandardAnwser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StandardAnwser];
GO
IF OBJECT_ID(N'[dbo].[Team]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Team];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[UserAnwser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserAnwser];
GO
IF OBJECT_ID(N'[dbo].[UserTeam]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserTeam];
GO
IF OBJECT_ID(N'[dbo].[WorkflowTeamRelation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WorkflowTeamRelation];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Role'
CREATE TABLE [dbo].[Role] (
    [SysNo] int IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(50)  NOT NULL,
    [Status] int  NOT NULL,
    [InDate] datetime  NOT NULL,
    [InUse] nvarchar(100)  NOT NULL,
    [EditDate] datetime  NULL,
    [EditUser] nvarchar(100)  NULL
);
GO

-- Creating table 'RoleUser'
CREATE TABLE [dbo].[RoleUser] (
    [SysNo] int IDENTITY(1,1) NOT NULL,
    [RoleSysNo] int  NOT NULL,
    [UserSysNo] int  NOT NULL,
    [Status] int  NOT NULL,
    [InDate] datetime  NOT NULL,
    [InUser] nvarchar(100)  NOT NULL,
    [EditDate] datetime  NULL,
    [EditUse] nvarchar(100)  NULL
);
GO

-- Creating table 'StandardAnwser'
CREATE TABLE [dbo].[StandardAnwser] (
    [SysNo] int IDENTITY(1,1) NOT NULL,
    [TemplateData] nvarchar(4000)  NOT NULL,
    [TemplateName] nvarchar(100)  NOT NULL,
    [InDate] datetime  NOT NULL,
    [InUser] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Team'
CREATE TABLE [dbo].[Team] (
    [SysNo] int IDENTITY(1,1) NOT NULL,
    [TeamName] nvarchar(50)  NOT NULL,
    [InDate] datetime  NOT NULL,
    [InUser] nvarchar(50)  NOT NULL,
    [EditDate] datetime  NULL,
    [EditUser] nvarchar(50)  NULL
);
GO

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [SysNo] int IDENTITY(1,1) NOT NULL,
    [UserID] nvarchar(100)  NOT NULL,
    [UserName] nvarchar(20)  NOT NULL,
    [Password] nvarchar(128)  NOT NULL,
    [Status] int  NOT NULL,
    [InDate] datetime  NOT NULL,
    [InUser] nvarchar(100)  NOT NULL,
    [EditDate] datetime  NULL,
    [EditUser] nvarchar(100)  NULL
);
GO

-- Creating table 'UserAnwser'
CREATE TABLE [dbo].[UserAnwser] (
    [SysNo] int IDENTITY(1,1) NOT NULL,
    [UserSysNo] int  NOT NULL,
    [TempData] nvarchar(4000)  NOT NULL,
    [TemplateName] nvarchar(100)  NOT NULL,
    [NodeName] nvarchar(100)  NULL,
    [NodeID] int  NULL,
    [TokenID] int  NULL,
    [ProcessInstanceID] int  NULL,
    [WorkflowName] nvarchar(100)  NULL,
    [InDate] datetime  NOT NULL
);
GO

-- Creating table 'UserTeam'
CREATE TABLE [dbo].[UserTeam] (
    [SysNo] int IDENTITY(1,1) NOT NULL,
    [UserSysNo] int  NOT NULL,
    [TeamSysNo] int  NOT NULL,
    [InUser] nvarchar(50)  NOT NULL,
    [InDate] datetime  NOT NULL
);
GO

-- Creating table 'WorkflowTeamRelation'
CREATE TABLE [dbo].[WorkflowTeamRelation] (
    [SysNo] int  NOT NULL,
    [TeamSysNo] int  NOT NULL,
    [InDate] datetime  NOT NULL,
    [InUser] nvarchar(50)  NOT NULL,
    [EditDate] datetime  NULL,
    [EditUser] nvarchar(50)  NULL,
    [ProcessName] nvarchar(100)  NOT NULL,
    [NodeName] nvarchar(100)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [SysNo] in table 'Role'
ALTER TABLE [dbo].[Role]
ADD CONSTRAINT [PK_Role]
    PRIMARY KEY CLUSTERED ([SysNo] ASC);
GO

-- Creating primary key on [SysNo] in table 'RoleUser'
ALTER TABLE [dbo].[RoleUser]
ADD CONSTRAINT [PK_RoleUser]
    PRIMARY KEY CLUSTERED ([SysNo] ASC);
GO

-- Creating primary key on [SysNo] in table 'StandardAnwser'
ALTER TABLE [dbo].[StandardAnwser]
ADD CONSTRAINT [PK_StandardAnwser]
    PRIMARY KEY CLUSTERED ([SysNo] ASC);
GO

-- Creating primary key on [SysNo] in table 'Team'
ALTER TABLE [dbo].[Team]
ADD CONSTRAINT [PK_Team]
    PRIMARY KEY CLUSTERED ([SysNo] ASC);
GO

-- Creating primary key on [SysNo] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([SysNo] ASC);
GO

-- Creating primary key on [SysNo] in table 'UserAnwser'
ALTER TABLE [dbo].[UserAnwser]
ADD CONSTRAINT [PK_UserAnwser]
    PRIMARY KEY CLUSTERED ([SysNo] ASC);
GO

-- Creating primary key on [SysNo] in table 'UserTeam'
ALTER TABLE [dbo].[UserTeam]
ADD CONSTRAINT [PK_UserTeam]
    PRIMARY KEY CLUSTERED ([SysNo] ASC);
GO

-- Creating primary key on [SysNo] in table 'WorkflowTeamRelation'
ALTER TABLE [dbo].[WorkflowTeamRelation]
ADD CONSTRAINT [PK_WorkflowTeamRelation]
    PRIMARY KEY CLUSTERED ([SysNo] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------