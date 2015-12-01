using Exam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Service.Interface
{
    public interface ITiKuService
    {
        void CreateProcessInfo(ProcessExtendModel pInfo);
        void UpdateProcessInfo(ProcessExtendModel pInfo);

        ProcessExtendModel GetProcessInfo(int id);
        ProcessExtendModel GetProcessInfo(string name);

        List<ProcessExtendModel> GetAllProcess();


        List<TiKuMasterModel> GetAllTiKu();
        void CreateTiKu(TiKuMasterModel master);

        void UpdateTiKu(TiKuMasterModel master);
    }
}
