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

        [Import]
        private AssignedUserRepository assignedUserRepo;

        [Import]
        private RoleUserRepository roleUserRepository;

        [Import]
        private RoleRepository roleRepository;

        public User GetNextUserByNodeName(string processName, string nodeName)
        {
            var query = from user in userRepo.Entities
                        join userTeam in userTeamRepo.Entities
                            on user.UserID equals userTeam.UserID
                        join wTeam in workflowTeamRepo.Entities
                            on userTeam.TeamName equals wTeam.TeamName
                        join roleUser in roleUserRepository.Entities
                        on user.UserID equals roleUser.UserID
                        join role in roleRepository.Entities
                        on roleUser.RoleSysNo equals role.SysNo
                        where wTeam.NodeName == nodeName && wTeam.ProcessName == processName
                        select user;

            var choosedUsers = assignedUserRepo.Entities.GroupBy(m => m.UserID).Select(g => new { g.Key, Count = g.Count() });

            var joinQuery = from q in query
                            join c in choosedUsers
                            on q.UserID equals c.Key into temp
                            from t in temp.DefaultIfEmpty()
                            orderby t.Count
                            select q;

            return joinQuery.FirstOrDefault();
        }

        public List<User> GetAllUsersByNodeName(string processName, string nodeName)
        {
            var query = from user in userRepo.Entities
                        join userTeam in userTeamRepo.Entities
                            on user.UserID equals userTeam.UserID
                        join wTeam in workflowTeamRepo.Entities
                            on userTeam.TeamName equals wTeam.TeamName
                        where wTeam.NodeName == nodeName && wTeam.ProcessName == processName
                        select user;

            return query.ToList();
        }
    }
}
