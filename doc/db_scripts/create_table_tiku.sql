create table dbo.Tiku
(
	SysNo Int Primary key NOT NULL Identity(1,1)
	,ProcessName nvarchar(100) NOT NULL
	,NodeName nvarchar(100) NOT NULL
	,CategorySysNo INT NOT NULL
	,DifficultyLevel NVARCHAR(50) NOT NULL
)

create table dbo.ProcessCategory
(
	SysNo Int Primary key NOT NULL Identity(1,1)
	,Code varchar(20) NOT NULL
	,[Level] INT NOT NULL
	,ParentSysNo
)