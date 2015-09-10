--创建功能
INSERT INTO dbo.Functions(FunctionName,FunctionKey,Description,Status,InDate,InUser)VALUES('发起流程','StartProcess','发起流程',1,GETDATE(),'007')
INSERT INTO dbo.Functions(FunctionName,FunctionKey,Description,Status,InDate,InUser)VALUES('答案设置','AnswerSetting','答案设置',1,GETDATE(),'007')
INSERT INTO dbo.Functions(FunctionName,FunctionKey,Description,Status,InDate,InUser)VALUES('成绩查看','ViewScore','成绩查看',1,GETDATE(),'007')
INSERT INTO dbo.Functions(FunctionName,FunctionKey,Description,Status,InDate,InUser)VALUES('学生导入','ImportStu','学生导入',1,GETDATE(),'007')

--功能与角色的关系