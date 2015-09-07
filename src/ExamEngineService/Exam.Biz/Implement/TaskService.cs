using System.ComponentModel.Composition;
using Component.Tools;
using Exam.Model;
using Exam.Model.QueryFilters;
using Exam.Service.Interface;
using WorkflowCallWapper;
using WorkflowCallWapper.Models;

namespace Exam.Service.Implement
{
    [Export(typeof (ITaskService))]
    public class TaskService : ServiceBase, ITaskService
    {
        protected override string ModuleName
        {
            get { return "Task"; }
        }

        public dynamic GetUserTasks(UserTaskQueryFilter filter)
        {
            var proxy = new WorkflowProxy();
            QueryTaskView queryTasks = proxy.GetUnProcessTaskByUser(PublicFunc.GetConfigByKey_AppSettings("mock_user"),
                filter.PageInfo.PageIndex,
                filter.PageInfo.PageSize);

            return queryTasks;
        }

        public void BeginExam(BeginExamModel data)
        {
            
        }
    }
}