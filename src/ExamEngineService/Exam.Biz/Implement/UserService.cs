using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Exam.Repository;
using Exam.Repository.Repo;
using Exam.Service.Interface;
using System.ComponentModel.Composition;

namespace Exam.Service.Implement
{
    [Export(typeof(IUserService))]
    public class UserService : ServiceBase, IUserService
    {
        [Import]
        private UserRepository userRepo;
        [Import]
        private UserTeamRepository userTeamRepo;
        [Import]
        private TeamRepository teamRepo;

        protected override string ModuleName
        {
            get { return "User"; }
        }

        public List<dynamic> GetUserByTeamSysNo(int sysNo)
        {
            var query = from user in userRepo.Entities
                        join ut in userTeamRepo.Entities
                            on user.SysNo equals ut.UserSysNo
                        where ut.TeamSysNo == sysNo
                        select new { user.UserID, user.UserName, ut };
            return query.ToList<dynamic>();
        }

        public List<dynamic> GetAllTeamsWithUser()
        {
            var query = from user in userRepo.Entities
                        join ut in userTeamRepo.Entities
                            on user.SysNo equals ut.UserSysNo
                        join t in teamRepo.Entities
                            on ut.TeamSysNo equals t.SysNo
                        select new { t, user };
            var result = new List<dynamic>();
            query.ToList().ForEach((item) =>
            {
                if (result.All(m => m.TeamSysNo != item.t.SysNo))
                {
                    result.Add(new
                    {
                        item.t.SysNo,
                        item.t.TeamName,
                        Users = new List<dynamic>()
                    });
                }
                else
                {
                    var firstOrDefault = result.FirstOrDefault(m => m.TeamSysNo == item.t.SysNo);
                    if (firstOrDefault != null)
                        firstOrDefault.Users.Add(item.user);
                }
            });

            return result;
        }
    }
}
