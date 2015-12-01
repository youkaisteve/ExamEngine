using Exam.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.Interface
{
    public interface ITiKuService
    {
        void CreateOrUpdateProcessInfo(ProcessInfo pInfo);

        ProcessInfo GetProcessInfo(int id);
        ProcessInfo GetProcessInfo(string name);

        List<ProcessInfo> GetAllProcess();


        List<TiKuMaster> GetAllTiKu();
        void CreateOrUpdateTiKu(TiKuMaster master);
    }
}
