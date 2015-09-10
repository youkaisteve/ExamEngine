using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Exam.Repository;
using Exam.Repository.Repo;
using Exam.Service.Interface;
using Exam.Model;
using Component.Tools.Exceptions;

namespace Exam.Service.Implement
{
    [Export(typeof (IUserService))]
    public class UserService : ServiceBase, IUserService
    {
        [Import] private TeamRepository teamRepo;
        [Import] private UserRepository userRepo;
        [Import] private UserTeamRepository userTeamRepo;

        protected override string ModuleName
        {
            get { return "User"; }
        }

        public List<User> GetUserByTeamSysNo(int sysNo)
        {
            return userRepo.GetUserByTeamSysNo(sysNo);
        }

        public List<dynamic> GetAllTeamsWithUser()
        {
            var query = from user in userRepo.Entities
                join ut in userTeamRepo.Entities
                    on user.SysNo equals ut.UserSysNo
                join t in teamRepo.Entities
                    on ut.TeamSysNo equals t.SysNo
                select new {t, user};
            var result = new List<dynamic>();
            query.ToList().ForEach(item =>
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
                    dynamic firstOrDefault = result.FirstOrDefault(m => m.TeamSysNo == item.t.SysNo);
                    if (firstOrDefault != null)
                        firstOrDefault.Users.Add(item.user);
                }
            });

            return result;
        }

        public void ImportTeamUser(List<TeamUserImportModel> data)
        {
            if (data == null)
            {
                throw new BusinessException("无数据");
            }
        }
    }
}