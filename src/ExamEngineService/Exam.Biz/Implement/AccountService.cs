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

        protected override string ModuleName
        {
            get { return "Account"; }
        }

        public User Login(string userName, string password)
        {
            if (userRepo.Entities.Any(m => m.UserName == userName && m.Password == password))
            {
                return userRepo.Entities.FirstOrDefault(m => m.UserName == userName && m.Password == password);
            }
            throw new BusinessException("用户名或密码错误");
        }
    }
}