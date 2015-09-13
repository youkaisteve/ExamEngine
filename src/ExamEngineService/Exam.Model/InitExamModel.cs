using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Model
{
    public class InitExamModel : ModelBase
    {
        public string ProcessName { get; set; }

        public List<NodeTeamModel> NodeTeams { get; set; } 
    }
}
