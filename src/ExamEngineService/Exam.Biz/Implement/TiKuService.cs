using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.Interface
{
    [Export(typeof(ITiKuService))]
    public class TiKuService : ServiceBase, ITiKuService
    {
        protected override string ModuleName
        {
            get { return "TiKu"; }
        }

        public void CreateOrUpdateProcessInfo(Repository.ProcessInfo pInfo)
        {
            throw new NotImplementedException();
        }

        public Repository.ProcessInfo GetProcessInfo(int id)
        {
            throw new NotImplementedException();
        }

        public Repository.ProcessInfo GetProcessInfo(string name)
        {
            throw new NotImplementedException();
        }

        public List<Repository.ProcessInfo> GetAllProcess()
        {
            throw new NotImplementedException();
        }

        public List<Repository.TiKuMaster> GetAllTiKu()
        {
            throw new NotImplementedException();
        }

        public void CreateOrUpdateTiKu(Repository.TiKuMaster master)
        {
            throw new NotImplementedException();
        }
    }
}
