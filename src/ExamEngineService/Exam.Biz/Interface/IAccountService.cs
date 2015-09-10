using Exam.Repository;

namespace Exam.Service.Interface
{
    public interface IAccountService
    {
        dynamic Login(string userName, string password);
    }
}