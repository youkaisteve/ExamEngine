using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Repository.Repo
{
    [Export(typeof(RoleRepository))]
    public class RoleRepository : ExamRepositoryBase<Role,int>
    {
    }

    [Export(typeof(RoleUserRepository))]
    public class RoleUserRepository : ExamRepositoryBase<RoleUser, int>
    {
    }
}
