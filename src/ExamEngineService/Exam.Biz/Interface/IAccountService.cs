using Exam.Repository;

namespace Exam.Service.Interface
{
    public interface IAccountService
    {
        User Login(string userName, string password);
    }
}