using System.ComponentModel.Composition;
using System.Linq;
using Component.Tools.Exceptions;
using Exam.Repository;
using Exam.Repository.Repo;
using Exam.Service.Interface;

namespace Exam.Service.Implement
{
    [Export(typeof (IAccountService))]
    public class AccountService : ServiceBase, IAccountService
    {
        [Import] private UserRepository userRepo;
        [Import]
        private RoleRepository roleRepo;
        [Import]
        private RoleUserRepository roleUserRepo;
        protected override string ModuleName
        {
            get { return "Account"; }
        }

        public dynamic Login(string userName, string password)
        {
            if (userRepo.Entities.Any(m => m.UserName == userName && m.Password == password))
            {
                //return userRepo.Entities.FirstOrDefault(m => m.UserName == userName && m.Password == password);
                var query = from user in userRepo.Entities
                    join roleUser in roleUserRepo.Entities
                        on user.SysNo equals roleUser.UserSysNo
                    join role in roleRepo.Entities
                        on roleUser.RoleSysNo equals role.SysNo
                    where user.UserName == userName
                    select new {user.UserID, user.UserName, user.SysNo, user.Status, role.AuthFunction};
                return query.FirstOrDefault();
            }
            throw new BusinessException("用户名或密码错误");
        }
    }
}