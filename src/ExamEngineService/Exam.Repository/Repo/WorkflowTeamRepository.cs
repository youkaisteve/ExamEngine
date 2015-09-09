using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Repository.Repo
{
    [Export(typeof(WorkflowTeamRepository))]
    public class WorkflowTeamRepository : ExamRepositoryBase<WorkflowTeamRelation, int>
    {
    }
}
