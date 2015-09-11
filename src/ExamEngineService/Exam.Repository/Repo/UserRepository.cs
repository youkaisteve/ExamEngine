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

        public List<User> GetUserByTeamName(string name)
        {
            var query = from user in this.Entities
                        join ut in userTeamRepo.Entities
                            on user.UserID equals ut.UserID
                        where ut.TeamName == name
                        select user;
            return query.ToList();
        }
    }
}