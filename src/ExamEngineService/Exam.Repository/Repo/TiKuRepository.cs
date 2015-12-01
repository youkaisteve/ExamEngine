using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Repository.Repo
{
    [Export(typeof(TiKuMasterRepository))]
    public class TiKuMasterRepository : ExamRepositoryBase<TiKuMaster, int>
    {
    }

    [Export(typeof(TiKuDetailRepository))]
    public class TiKuDetailRepository : ExamRepositoryBase<TiKuDetail, int>
    { 
    }
}
