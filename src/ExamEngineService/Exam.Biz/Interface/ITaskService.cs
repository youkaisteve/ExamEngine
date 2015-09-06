using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Component.Data;
using Exam.Service.QueryFilters;

namespace Exam.Service.Interface
{
    /// <summary>
    /// 处理我的任务的服务，包括任务列表，任务详情等
    /// </summary>
    public interface ITaskService
    {
        List<dynamic> GetUserTasks(UserTaskQueryFilter filter);
    }
}
