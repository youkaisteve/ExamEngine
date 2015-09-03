using System.ComponentModel.Composition;

namespace Exam.Repository.Repo
{
    [Export(typeof (UserRepository))]
    public class UserRepository : ExamRepositoryBase<User, int>
    {
    }
}