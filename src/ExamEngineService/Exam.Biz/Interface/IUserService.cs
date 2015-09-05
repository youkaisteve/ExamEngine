using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.Repository;

namespace Exam.Service.Interface
{
    public interface IUserService
    {
        List<dynamic> GetUserByTeamSysNo(int sysNo);
    }
}
