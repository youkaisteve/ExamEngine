using System.ComponentModel.Composition;
using System.Web.Http;
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
        public ApiResponse GetUserTasks([FromBody] UserTaskQueryFilter filter)
        {
            filter.PageInfo = new QueryPageInfo()
            {
                PageIndex = 0,
                PageSize = 10000
            };

            dynamic result = taskService.GetUserTasks(filter);

            return ApiOk(result);
        }

        [HttpPost]
        public ApiResponse GetTaskDetail([FromBody] dynamic data)
        {
            var result = taskService.GetTaskDetail(data.InstanceId.Value, data.TokenId.Value);
            return ApiOk(result);
        }

        [HttpPost]
        public ApiResponse BeginExam([FromBody] BeginExamModel data)
        {
            taskService.BeginExam(data);
            return ApiOk();
        }

        [HttpPost]
        public ApiResponse InitExam([FromBody] InitExamModel data)
        {
            taskService.InitExam(data);
            return ApiOk();
        }

        [HttpPost]
        public ApiResponse Process([FromBody] dynamic data)
        {
            taskService.Process(data.InstanceId.Value, data.TokenId.Value, data.TransitionName.Value, data.FormData.Value);
            return ApiOk();
        }
    }
}