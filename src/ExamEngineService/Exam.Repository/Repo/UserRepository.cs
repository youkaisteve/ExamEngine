using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Exam.Repository.Repo
{
    [Export(typeof(UserRepository))]
    public class UserRepository : ExamRepositoryBase<User, int>
    {
        [Import]
        private UserTeamRepository userTeamRepo;

        public List<User> GetUserByTeamSysNo(int sysNo)
        {
            var query = from user in this.Entities
                        join ut in userTeamRepo.Entities
                            on user.SysNo equals ut.UserSysNo
                        where ut.TeamSysNo == sysNo
                        select user;
            return query.ToList();
        }
    }
}