using System.ComponentModel.Composition;
using System.Linq;
using Component.Tools.Exceptions;
using Exam.Repository.Repo;
using Exam.Service.Interface;

namespace Exam.Service.Implement
{
    [Export(typeof(IAccountService))]
    public class AccountService : ServiceBase, IAccountService
    {
        [Import]
        private RoleRepository roleRepo;
        [Import]
        private RoleUserRepository roleUserRepo;
        [Import]
        private UserRepository userRepo;

        protected override string ModuleName
        {
            get { return "Account"; }
        }

        public dynamic Login(string userID, string password)
        {
            if (userRepo.Entities.Any(m => m.UserID == userID && m.Password == password))
            {
                //return userRepo.Entities.FirstOrDefault(m => m.UserName == userName && m.Password == password);
                var query = from user in userRepo.Entities
                            join roleUser in roleUserRepo.Entities
                                on user.SysNo equals roleUser.UserSysNo
                            join role in roleRepo.Entities
                                on roleUser.RoleSysNo equals role.SysNo
                            where user.UserID == userID
                            select new { user.UserID, user.UserName, user.SysNo, user.Password, user.Status, role.AuthID, role.AuthName };
                return query.FirstOrDefault();
            }
            throw new BusinessException("用户名或密码错误");
        }
    }
}