using Exam.Model;
using Exam.Repository;
using Exam.Repository.Repo;
using Component.Tools;
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

        [Import]
        private ProcessInfoRepository ProcessInfoRepo { get; set; }

        [Import]
        private TiKuMasterRepository TiKuRepo { get; set; }

        [Import]
        private TiKuDetailRepository TiKuDetailRepo { get; set; }

        public void CreateProcessInfo(ProcessExtendModel pInfo)
        {
            var data = PublicFunc.EntityMap<ProcessExtendModel, ProcessInfo>(pInfo);
            if (data != null)
            {
                ProcessInfoRepo.Insert(data);
            }
        }

        public void UpdateProcessInfo(ProcessExtendModel pInfo)
        {
            var data = PublicFunc.EntityMap<ProcessExtendModel, ProcessInfo>(pInfo);
            if (data != null)
            {
                ProcessInfoRepo.Update(data);
            }
        }

        public ProcessExtendModel GetProcessInfo(int id)
        {
            var pInfo = ProcessInfoRepo.Entities.FirstOrDefault(m => m.SysNo == id);
            return PublicFunc.EntityMap<ProcessInfo, ProcessExtendModel>(pInfo);
        }

        public ProcessExtendModel GetProcessInfo(string name)
        {
            var pInfo = ProcessInfoRepo.Entities.FirstOrDefault(m => m.ProcessName == name);
            return PublicFunc.EntityMap<ProcessInfo, ProcessExtendModel>(pInfo);
        }

        public List<ProcessExtendModel> GetAllProcess()
        {
            return ProcessInfoRepo.Entities.Select(data => PublicFunc.EntityMap<ProcessInfo, ProcessExtendModel>(data)).ToList();
        }

        public List<TiKuMasterModel> GetAllTiKu()
        {
            var query = from master in TiKuRepo.Entities
                        select new TiKuMasterModel
                        {
                            SysNo = master.SysNo,
                            TiKuName = master.TiKuName,
                            InUser = master.InUser,
                            InDate = master.InDate,
                            LastEditUser = master.LastEditUser,
                            LastEditDate = master.LastEditDate,
                            Status = master.Status,
                            Details = TiKuDetailRepo.Entities.Where(m => m.MasterSysNo == master.SysNo).Select(detail => new TiKuDetailModel
                                      {
                                          SysNo = detail.SysNo,
                                          MasterSysNo = detail.MasterSysNo,
                                          NodeName = detail.NodeName,
                                          ProcessInfoSysNo = detail.ProcessInfoSysNo,
                                          TeamName = detail.TeamName
                                      }).ToList()
                        };
            if (query != null)
            {
                return query.ToList<TiKuMasterModel>();
            }
            return null;
        }

        public void CreateTiKu(TiKuMasterModel master)
        {
            if (master != null)
            {
                var insertCount = TiKuRepo.Insert(PublicFunc.EntityMap<TiKuMasterModel, TiKuMaster>(master), false);
                if (insertCount > 0)
                {
                    var details = PublicFunc.EntityMap<List<TiKuDetailModel>, List<TiKuDetail>>(master.Details);
                    if (details != null)
                    {
                        TiKuDetailRepo.Insert(details, false);
                    }
                }

                UnitOfWork.Submit();
            }
        }

        public void UpdateTiKu(TiKuMasterModel master)
        {
            if (master != null)
            {
                var updateCount = TiKuRepo.Update(PublicFunc.EntityMap<TiKuMasterModel, TiKuMaster>(master), false);
                if (updateCount > 0)
                {
                    var details = PublicFunc.EntityMap<List<TiKuDetailModel>, List<TiKuDetail>>(master.Details);
                    if (details != null)
                    {
                        foreach (var detail in details)
                        {
                            TiKuDetailRepo.Update(detail, false);
                        }
                    }
                }

                UnitOfWork.Submit();
            }
        }
    }
}
