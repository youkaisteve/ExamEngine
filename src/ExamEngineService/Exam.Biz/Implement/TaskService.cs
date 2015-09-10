using System;
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
        private TeamRepository teamRepo;
        [Import]
        private UserRepository userRepo;
        [Import]
        private WorkflowTeamRepository workflowTeamRepo;

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

        public dynamic GetTaskDetail(string instanceId, string tokenId)
        {
            var proxy = new WorkflowProxy();
            List<Transition> transitions = proxy.GetTransitions(instanceId, tokenId);
            VariableInstance page = proxy.GetCurrentTaskSetPage(instanceId, tokenId);
            return new { Transitions = transitions, Page = page.Value };
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
            //}
        }

        public void InitExam(InitExamModel data)
        {
            WorkflowTeamRelation wtr;
            foreach (var nodeTeam in data.NodeTeams)
            {
                wtr = new WorkflowTeamRelation()
                {
                    InDate = DateTime.Now,
                    InUser = data.UserId,
                    NodeName = nodeTeam.NodeName,
                    ProcessName = data.ProcessName,
                    TeamSysNo = nodeTeam.TeamSysNo
                };
                workflowTeamRepo.Insert(wtr, false);
            }

            UnitOfWork.Submit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instanceid">流程ID</param>
        /// <param name="tokenid">节点ID</param>
        /// <param name="transitionName">按钮名称（离开当前节点的TransitionName）</param>
        public void Process(string instanceid, string tokenid, string transitionName)
        {
            WorkflowProxy proxy = new WorkflowProxy();

            var processInstance = new ProcessInstance();
            processInstance.InstanceID = instanceid;
            processInstance.TokenID = tokenid;
            processInstance.RouterName = transitionName;

            //获取下一步的代办人
            TaskUser user = new TaskUser {UserId = "007", UserName = "007"};
            processInstance.IncludeActors.Add(user);

            //判断登记表中有没有该人员，如果没有，则写入（需要传入表单Json串）
            VariableInstance item = new VariableInstance();
            if (processInstance.RouterName == "是否参加社会保险")
            {
                item.VariableName = "isexit";
                item.Value = int.Parse(PublicFunc.GetConfigByKey_AppSettings("flag"));
                processInstance.Variables.Add(item);
            }
            else
            {
                item.VariableName = "flag";
                item.Value = int.Parse(PublicFunc.GetConfigByKey_AppSettings("flag"));
                processInstance.Variables.Add(item);
            }

            var process = proxy.ProcessExecuter(processInstance);
        }
    }
}