using System.Collections.Generic;
using System.ComponentModel.Composition;
using Component.Tools;
using Exam.Model;
using Exam.Model.QueryFilters;
using Exam.Repository;
using Exam.Repository.Repo;
using Exam.Service.Interface;
using WorkflowCallWapper;
using WorkflowCallWapper.Models;

namespace Exam.Service.Implement
{
    [Export(typeof(ITaskService))]
    public class TaskService : ServiceBase, ITaskService
    {
        [Import]
        public TeamRepository teamRepo;
        [Import]
        private UserRepository userRepo;
        protected override string ModuleName
        {
            get { return "Task"; }
        }

        public dynamic GetUserTasks(UserTaskQueryFilter filter)
        {
            var proxy = new WorkflowProxy();
            QueryTaskView queryTasks = proxy.GetUnProcessTaskByUser(filter.UserId ?? PublicFunc.GetConfigByKey_AppSettings("mock_user"),
                filter.PageInfo.PageIndex,
                filter.PageInfo.PageSize);

            return queryTasks;
        }

        public void BeginExam(BeginExamModel data)
        {
            WorkflowProxy proxy = new WorkflowProxy();

            var processName = data.ProcessName;
            //foreach (string processName in data.ProcessNames)
            //{
            ProcessInstance processInstance = new ProcessInstance
            {
                Actor = data.UserId,
                ActorName = data.UserName,
                ProcessName = processName
            };

            //获取下一步的NodeName，用于获取该Node对应的Team和用户列表
            string nodeName = proxy.GetFirstNodeName(processName);

            //获取node对应的用户
            var users = teamRepo.GetUsersByNodeName(nodeName);

            var choosedUsers = new List<string>();

            foreach (var user in users)
            {
                //TODO:这里需要返回用户角色(目前默认写成Student)
                User choosenUser = GetRandomUserId(users, choosedUsers);
                choosedUsers.Add(choosenUser.UserID);

                TaskUser taskUser = new TaskUser();
                taskUser.UserId = choosenUser.UserID;
                taskUser.UserName = choosenUser.UserName;
                //TODO:给用户的角色赋值
                taskUser.UserRole = "Student";
                processInstance.IncludeActors.Add(taskUser);

                processInstance = proxy.CreateProcessInstance(processInstance);
            }
        }
        //}
    }
}