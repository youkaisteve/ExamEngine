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

        /// <summary>
        /// 初始化流程配置
        /// </summary>
        /// <param name="data">流程节点和Team的对应实体</param>
        void InitExam(InitExamModel data);
    }
}