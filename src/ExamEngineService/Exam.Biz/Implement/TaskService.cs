using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Dynamic;
using System.Linq;
using Component.Tools;
using Exam.Model;
using Exam.Model.QueryFilters;
using Exam.Repository;
using Exam.Repository.Repo;
using Exam.Service.Interface;
using WorkflowCallWapper;
using WorkflowCallWapper.Models;
using Component.Tools.Exceptions;
using System.IO;

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

        [Import]
        private SocialSPRepository socialSpRepo;

        [Import]
        private StAnswerRepository stAnswerRepo;

        protected override string ModuleName
        {
            get { return "Task"; }
        }

        public dynamic GetUserTasks(UserTaskQueryFilter filter)
        {
            var proxy = new WorkflowProxy();
            QueryTaskView queryTasks =
                proxy.GetUnProcessTaskByUser(filter.UserId,
                    filter.PageInfo.PageIndex,
                    filter.PageInfo.PageSize);

            return queryTasks;
        }

        public dynamic GetUnFinishProcess(QueryFilter filter)
        {
            var proxy = new WorkflowProxy();
            QueryInstanceView queryProcesses =
                proxy.GetFinishedProcess(filter.PageInfo.PageIndex,
                    filter.PageInfo.PageSize);

            if (queryProcesses != null)
            {
                return new { queryProcesses.TotalCount, Processes = queryProcesses.Historys };
            }

            return queryProcesses;
        }

        public dynamic GetTaskDetail(string instanceId, string tokenId)
        {
            string pageDesc = "";
            string pageValue = "";
            var proxy = new WorkflowProxy();
            List<Transition> transitions = proxy.GetTransitions(instanceId, tokenId);
            VariableInstance page = proxy.GetCurrentTaskSetPage(instanceId, tokenId);
            if (page != null)
            {
                //获取当前流程实例的所有变量，查找和当前节点的pageName相同的变量，取出变量值，该值就是表单的数据
                List<VariableInstance> vars = proxy.GetVariables(instanceId, "");
                var findVar = vars.FirstOrDefault(m => m.VariableName == page.Value);
                pageValue = findVar == null ? page.Value.ToString() : findVar.Value.ToString();

                var shortName = Path.GetFileName(page.Value.ToString());
                //获取page desc
                var firstOrDefault = stAnswerRepo.Entities.FirstOrDefault(m => m.TemplateName == shortName);
                if (firstOrDefault != null)
                {
                    pageDesc = firstOrDefault.TemplateDesc;
                }
            }

            return new { Transitions = transitions, Page = page.Value, PageDesc = pageDesc, PageData = pageValue };
        }

        public void BeginExam(BeginExamModel data)
        {
            var proxy = new WorkflowProxy();

            string processName = data.ProcessName;

            //获取下一步的NodeName，用于获取该Node对应的Team和用户列表
            string nodeName = proxy.GetFirstNodeName(processName);

            //获取node对应的用户
            List<User> users = teamRepo.GetAllUsersByNodeName(processName, nodeName);

            foreach (User user in users)
            {
                var processInstance = new ProcessInstance
                {
                    ProcessName = processName,
                    Actor = user.UserID,
                    ActorName = user.UserName
                };

                var taskUser = new TaskUser();
                taskUser.UserId = user.UserID;
                taskUser.UserName = user.UserName;
                taskUser.UserRole = "Student";
                processInstance.IncludeActors.Add(taskUser);
                processInstance = proxy.CreateProcessInstance(processInstance);
                LogHelper.Instanse.WriteInfo(
                    string.Format("发起流程，用户-{0},InstanceID-{1}", taskUser.UserId, processInstance.InstanceID));
            }
        }

        public void InitExam(InitExamModel data)
        {
            workflowTeamRepo.Delete(m => m.ProcessName == data.ProcessName);
            var list = new List<WorkflowTeamRelation>();
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
                list.Add(wtr);
            }

            workflowTeamRepo.Insert(list);
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

            var item = new VariableInstance();
            if (processInstance.RouterName == "到是否参加社会保险")
            {
                int flag = 0;
                if (!string.IsNullOrWhiteSpace(data.TemplateData))
                {
                    dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<object>(data.TemplateData);
                    string idCardNumber = jsonObj.ShenFenZhengHaoMa;
                    if (socialSpRepo.Entities.Any(m => m.IDCardNumber == idCardNumber))
                    {
                        flag = 1;
                    }
                    else
                    {
                        flag = 0;
                        socialSpRepo.Insert(new SocialSecurityPersonnel()
                        {
                            IDCardNumber = idCardNumber
                        });
                    }
                }
                item.VariableName = "flag";
                item.Value = flag;
                processInstance.Variables.Add(item);
            }

            //将表单数据保存至流程的变量中，以便在审核环节能够读出表单的数据(变量值不能超过2000)
            if (!string.IsNullOrWhiteSpace(data.TemplateData))
            {
                VariableInstance pageUrl = proxy.GetCurrentTaskSetPage(processInstance.InstanceID, processInstance.TokenID);
                item = new VariableInstance();
                item.Value = data.TemplateData;
                item.VariableName = pageUrl.Value.ToString();
                processInstance.Variables.Add(item);
            }

            //获取下一个节点名并启动流程
            string userId = "";
            var list = proxy.GetTransitionNextNodeRoles(data.DefineName, data.TokenName, data.TransitionName);
            if (list != null && list.Count > 0)
            {

                string nodeName = list[0];
                User choosenUser = teamRepo.GetNextUserByNodeName(data.DefineName, nodeName);
                //var choosenUser = GetRandomUserId(users);
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
                userId = choosenUser.UserID;
                processInstance.IncludeActors.Add(user);
            }
            proxy.ProcessExecuter(processInstance);

            LogHelper.Instanse.WriteInfo(
                string.Format("InstanceID:-{0},TokenID:-{1},推给了-{2}",
                    processInstance.InstanceID, processInstance.TokenID, userId));

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