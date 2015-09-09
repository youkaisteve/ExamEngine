using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Repository.Repo
{
    [Export(typeof(TeamRepository))]
    public class TeamRepository : ExamRepositoryBase<Team, int>
    {
        [Import]
        private UserRepository userRepo;

        [Import]
        private UserTeamRepository userTeamRepo;

        [Import]
        private WorkflowTeamRepository workflowTeamRepo;

        public List<User> GetUsersByNodeName(string nodeName)
        {
            var query = from user in userRepo.Entities
                        join userTeam in userTeamRepo.Entities
                        on user.SysNo equals userTeam.UserSysNo
                        join wTeam in workflowTeamRepo.Entities
                        on userTeam.TeamSysNo equals wTeam.TeamSysNo
                        where wTeam.NodeName == nodeName
                        select user;
            return query.Distinct().ToList();
        }
    }
}
