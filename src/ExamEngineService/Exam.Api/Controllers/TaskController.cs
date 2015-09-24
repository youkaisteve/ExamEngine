using System.ComponentModel.Composition;
using System.Web.Http;
using Exam.Api.Filters;
using Exam.Api.Framework;
using Exam.Model;
using Exam.Model.QueryFilters;
using Exam.Service.Interface;

namespace Exam.Api.Controllers
{
    [Export(typeof(TaskController))]
    public class TaskController : BaseApiController
    {
        [Import]
        public ITaskService taskService;

        [HttpPost]
        [BaseAuthoriizeFilter]
        public ApiResponse GetUserTasks([FromBody] UserTaskQueryFilter filter)
        {
            filter.PageInfo = new QueryPageInfo
            {
                PageIndex = 0,
                PageSize = 10000
            };

            dynamic result = taskService.GetUserTasks(filter);

            return ApiOk(result);
        }

        [HttpPost]
        [BaseAuthoriizeFilter]
        public ApiResponse GetTaskDetail([FromBody] dynamic data)
        {
            dynamic result = taskService.GetTaskDetail(data.InstanceId.Value, data.TokenId.Value);
            return ApiOk(result);
        }

        [HttpPost]
        [BaseAuthoriizeFilter]
        public ApiResponse BeginExam([FromBody] BeginExamModel data)
        {
            taskService.BeginExam(data);
            return ApiOk();
        }

        [HttpPost]
        [BaseAuthoriizeFilter]
        public ApiResponse InitExam([FromBody] InitExamModel data)
        {
            taskService.InitExam(data);
            BeginExamModel begin = new BeginExamModel()
            {
                ProcessName = data.ProcessName,
                UserId = data.User.UserID,
                UserName = data.User.UserName
            };
           taskService.BeginExam(begin);

            return ApiOk();
        }

        [HttpPost]
        [BaseAuthoriizeFilter]
        public ApiResponse Process([FromBody] ProcessModel data)
        {
            taskService.Process(data);
            return ApiOk();
        }

        [HttpPost]
        [BaseAuthoriizeFilter]
        public ApiResponse UnFinishProcess([FromBody] QueryFilter filter)
        {
            var data = taskService.GetUnFinishProcess(filter);
            return ApiOk(data);
        }

    }
}