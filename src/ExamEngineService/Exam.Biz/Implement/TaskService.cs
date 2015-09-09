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
            ProcessInstance processInstance = new ProcessInstance
            {
                Actor = data.UserId,
                ActorName = data.UserName,
                ProcessName = data.ProcessName
            };
            foreach (var workflowNodeTeam in data.NodeTeams)
            {
                var choosedUsers = new List<string>();
                //TODO:这里需要返回用户角色
                var teamUsers = userRepo.GetUserByTeamSysNo(workflowNodeTeam.TeamId);
                User choosenUser = GetRandomUserId(teamUsers, choosedUsers);
                choosedUsers.Add(choosenUser.UserID);

                TaskUser user = new TaskUser();
                user.UserId = choosenUser.UserID;
                user.UserName = choosenUser.UserName;
                //TODO:获取用户的角色
                user.UserRole = "Student";
                processInstance.IncludeActors.Add(user);

                processInstance = proxy.CreateProcessInstance(processInstance);

                if (processInstance != null && processInstance.InstanceID != string.Empty)
                {
                    //TODO:获取流程的RouterName
                    processInstance.RouterName = "提交审核";
                }
            }
        }
    }
}