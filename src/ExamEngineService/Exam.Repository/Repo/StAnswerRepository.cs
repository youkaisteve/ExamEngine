using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Repository.Repo
{
    [Export(typeof(StAnswerRepository))]
    public class StAnswerRepository : ExamRepositoryBase<StandardAnwser, int>
    {
    }
}
