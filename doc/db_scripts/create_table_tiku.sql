create table dbo.ProcessInfo
(
	SysNo Int Primary key NOT NULL Identity(1,1)
	,ProcessName NVARCHAR(100) NOT NULL
	,Category NVARCHAR(50) NOT NULL
	,DifficultyLevel NVARCHAR(50) NOT NULL
	,Description NVARCHAR(500) NULL
	,InDate DATETIME DEFAULT(getdate()) NOT NULL
	,InUser NVARCHAR(100) NOT NULL
	,LastEditDate DATETIME NULL
	,LastEditUser NVARCHAR(100) NULL
)

create table dbo.TiKuMaster
(
	SysNo Int Primary key NOT NULL Identity(1,1)
	,TiKuName NVARCHAR(100) NOT NULL
	,InDate DATETIME DEFAULT(getdate()) NOT NULL
	,InUser NVARCHAR(100) NOT NULL
	,LastEditDate DATETIME NULL
	,LastEditUser NVARCHAR(100) NULL
	,Status INT NOT NULL DEFAULT(0)--1:待激活；2：已删除；3-已激活
)

create table dbo.TiKuDetail
(
	SysNo Int Primary key NOT NULL Identity(1,1)
	,MasterSysNo INT NOT NULL
	,ProcessInfoSysNo INT NOT NULL
	,NodeName NVARCHAR(100) NOT NULL
	,TeamName NVARCHAR(50) NOT NULL
)