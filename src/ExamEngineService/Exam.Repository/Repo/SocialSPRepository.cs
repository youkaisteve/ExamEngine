using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Repository.Repo
{
    [Export(typeof(SocialSPRepository))]
    public class SocialSPRepository : ExamRepositoryBase<SocialSecurityPersonnel, int>
    {
    }
}
