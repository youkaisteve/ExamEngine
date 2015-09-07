using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Model
{
    public class BeginExamModel
    {
        public string WorkflowId { get; set; }
        public string UserId { get; set; }
        public List<WorkflowNodeTeam> NodeTeams { get; set; }
    }

    public class WorkflowNodeTeam
    {
        public string NodeName { get; set; }
        public int TeamId { get; set; }
    }
}
