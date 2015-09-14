using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Application.Framework.Common;
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
        private AssignedUserRepository assignedUserRepo;
        [Import]
        private TeamRepository teamRepo;
        [Import]
        private UserRepository userRepo;
        [Import]
        private WorkflowTeamRepository workflowTeamRepo;
        [Import]
        private UserAnswerRepository userAnswerRepo;

        protected override string ModuleName
        {
            get { return "Task"; }
        }

        public dynamic GetUserTasks(UserTaskQueryFilter filter)
        {
            var proxy = new WorkflowProxy();
            QueryTaskView queryTasks =
                proxy.GetUnProcessTaskByUser(filter.UserId ?? PublicFunc.GetConfigByKey_AppSettings("mock_user"),
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
            var proxy = new WorkflowProxy();

            string processName = data.ProcessName;
            //foreach (string processName in data.ProcessNames)
            //{
            var processInstance = new ProcessInstance
            {
                Actor = data.UserId,
                ActorName = data.UserName,
                ProcessName = processName
            };

            //获取下一步的NodeName，用于获取该Node对应的Team和用户列表
            string nodeName = proxy.GetFirstNodeName(processName);

            //获取node对应的用户
            List<User> users = teamRepo.GetUsersByNodeName(processName, nodeName);

            var choosedUsers = new List<string>();

            foreach (User user in users)
            {
                //TODO:这里需要返回用户角色(目前默认写成Student)
                User choosenUser = GetRandomUserId(users, choosedUsers);
                choosedUsers.Add(choosenUser.UserID);

                var taskUser = new TaskUser();
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
            foreach (NodeTeamModel nodeTeam in data.NodeTeams)
            {
                wtr = new WorkflowTeamRelation
                {
                    InDate = DateTime.Now,
                    InUser = data.User.UserID,
                    NodeName = nodeTeam.NodeName,
                    ProcessName = data.ProcessName,
                    TeamName = nodeTeam.TeamName
                };
                workflowTeamRepo.Insert(wtr, false);
            }

            UnitOfWork.Submit();
        }

        /// <summary>
        /// </summary>
        /// <param name="instanceid">流程ID</param>
        /// <param name="tokenid">节点ID</param>
        /// <param name="transitionName">按钮名称（离开当前节点的TransitionName）</param>
        public void Process(ProcessModel data)
        {
            var proxy = new WorkflowProxy();

            var processInstance = new ProcessInstance();
            processInstance.InstanceID = data.InstanceId;
            processInstance.TokenID = data.TokenId;
            processInstance.RouterName = data.TransitionName;

            //判断登记表中有没有该人员，如果没有，则写入（需要传入表单Json串）
            var item = new VariableInstance();
            if (processInstance.RouterName == "到是否参加社会保险")
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

            //获取下一个节点名并启动流程
            var list = proxy.GetTransitionNextNodeRoles(data.DefineName, data.TokenName, data.TransitionName);
            if (list != null && list.Count > 0)
            {

                string nodeName = list[0];
                List<User> users = teamRepo.GetUsersByNodeName(data.DefineName, nodeName);
                User choosenUser = GetRandomUserId(users);
                if (choosenUser == null)
                {
                    throw new BusinessException("找不到下一步处理人");
                }

                assignedUserRepo.Insert(new AssignedUser
                {
                    InDate = DateTime.Now,
                    InstanceID = data.InstanceId,
                    UserID = choosenUser.UserID,
                    Nodename = nodeName,
                    ProcessName = data.DefineName,
                    InUser = data.User.UserID
                });

                var user = new TaskUser { UserId = choosenUser.UserID, UserName = choosenUser.UserName };
                processInstance.IncludeActors.Add(user);
            }

            proxy.ProcessExecuter(processInstance);

            if (!string.IsNullOrEmpty(data.TemplateData))
            {
                userAnswerRepo.Insert(new UserAnwser()
                {
                    TemplateData = data.TemplateData,
                    TemplateName = data.TemplateName,
                    InDate = DateTime.Now,
                    ProcessName = data.DefineName,
                    NodeName = data.TokenName,
                    UserID = data.User.UserID,
                    InstanceID = data.InstanceId,
                    TokenID = data.TokenId
                });
            }
        }
    }
}