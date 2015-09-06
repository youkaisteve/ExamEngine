using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.Service.Interface;

namespace Exam.Service.Implement
{
    [Export(typeof(ITaskService))]
    public class TaskService : ServiceBase, ITaskService
    {
        protected override string ModuleName
        {
            get { return "Task"; }
        }

        public List<dynamic> GetUserTasks(QueryFilters.UserTaskQueryFilter filter)
        {
            //first:get all tasks by userid
            return null;
        }
    }
}
