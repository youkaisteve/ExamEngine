using Exam.Model;
using Exam.Model.QueryFilters;

namespace Exam.Service.Interface
{
    /// <summary>
    ///     处理我的任务的服务，包括任务列表，任务详情等
    /// </summary>
    public interface ITaskService
    {
        dynamic GetUserTasks(UserTaskQueryFilter filter);

        void BeginExam(BeginExamModel data);
    }
}