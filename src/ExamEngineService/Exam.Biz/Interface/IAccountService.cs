using Exam.Repository;

namespace Exam.Service.Interface
{
    public interface IAccountService
    {
        User Logon(string userName, string password);
    }
}