using Exam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exam.Model.QueryFilters;
using Component.Data;

namespace Exam.Service.Interface
{
    public interface ITiKuService
    {
        void CreateProcessInfo(ProcessExtendModel pInfo);
        void UpdateProcessInfo(ProcessExtendModel pInfo);

        ProcessExtendModel GetProcessInfo(int id);
        ProcessExtendModel GetProcessInfo(string name);

        List<ProcessExtendModel> GetAllProcess();

        QueryResult<ProcessExtendModel> GetProcessByCondition(ProcessQueryFilter filter);

        List<TiKuMasterModel> GetAllTiKu();

        QueryResult<TiKuMasterModel> GetTiKuByCondition(TiKuQueryFilter filter);
        void CreateTiKu(TiKuMasterModel master);

        void UpdateTiKuStatus(TiKuUpdateModel master);

        void DeleteTiKu(TiKuUpdateModel model);

        List<dynamic> GetExportProcess();
    }
}
