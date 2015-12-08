using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
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
using Exam.Model.QueryFilters;
using Component.Data;
using WorkflowCallWapper;

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
        private ProcessInfoRepository processInfoRepo;

        [Import]
        private TiKuMasterRepository tiKuRepo;

        [Import]
        private TiKuDetailRepository tiKuDetailRepo;

        [Import]
        private WorkflowTeamRepository workflowTeamRepo;

        [Import("Exam")]
        private IAdoNetWrapper adonetWrapper;

        [Import]
        private ITaskService taskService;
        [Import]
        private IUserService userService;

        public void CreateProcessInfo(ProcessExtendModel pInfo)
        {
            var data = PublicFunc.EntityMap<ProcessExtendModel, ProcessInfo>(pInfo);
            if (data != null)
            {
                data.InUser = pInfo.User.UserID;
                data.InDate = DateTime.Now;
                data.LastEditUser = pInfo.User.UserID;
                data.LastEditDate = DateTime.Now;
                processInfoRepo.Insert(data);
            }
        }

        public void UpdateProcessInfo(ProcessExtendModel pInfo)
        {
            var data = PublicFunc.EntityMap<ProcessExtendModel, ProcessInfo>(pInfo);
            if (data != null)
            {
                data.LastEditUser = pInfo.User.UserID;
                data.LastEditDate = DateTime.Now;
                processInfoRepo.Update(data);
            }
        }

        public ProcessExtendModel GetProcessInfo(int id)
        {
            var pInfo = processInfoRepo.Entities.FirstOrDefault(m => m.SysNo == id);
            return PublicFunc.EntityMap<ProcessInfo, ProcessExtendModel>(pInfo);
        }

        public ProcessExtendModel GetProcessInfo(string name)
        {
            var pInfo = processInfoRepo.Entities.FirstOrDefault(m => m.ProcessName == name);
            return PublicFunc.EntityMap<ProcessInfo, ProcessExtendModel>(pInfo);
        }

        public QueryResult<ProcessExtendModel> GetProcessByCondition(ProcessQueryFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }
            var queryResult = new QueryResult<ProcessExtendModel>()
            {
                Page = new PageInfo()
                {
                    PageIndex = filter.PageInfo.PageIndex <= 0 ? 1 : filter.PageInfo.PageIndex,
                    PageSize = filter.PageInfo.PageSize
                }
            };

            queryResult.Page.Total = processInfoRepo.Entities.Count();
            var query = processInfoRepo.Entities.OrderByDescending(m => m.LastEditDate)
                .Skip(filter.PageInfo.PageSize * (filter.PageInfo.PageIndex - 1))
                .Take(filter.PageInfo.PageSize);
            queryResult.Result = PublicFunc.EntityMap<List<ProcessInfo>, List<ProcessExtendModel>>(query.ToList());
            return queryResult;
        }

        public List<ProcessExtendModel> GetAllProcess()
        {
            return processInfoRepo.Entities.Select(data => PublicFunc.EntityMap<ProcessInfo, ProcessExtendModel>(data)).ToList();
        }

        public List<TiKuMasterModel> GetAllTiKu()
        {
            var query = from master in tiKuRepo.Entities
                        select new TiKuMasterModel
                        {
                            SysNo = master.SysNo,
                            TiKuName = master.TiKuName,
                            InUser = master.InUser,
                            InDate = master.InDate,
                            LastEditUser = master.LastEditUser,
                            LastEditDate = master.LastEditDate,
                            Status = master.Status,
                            Details = tiKuDetailRepo.Entities.Where(m => m.MasterSysNo == master.SysNo).Select(detail => new TiKuDetailModel
                                      {
                                          SysNo = detail.SysNo,
                                          MasterSysNo = detail.MasterSysNo,
                                          NodeName = detail.NodeName,
                                          ProcessName = detail.ProcessName,
                                          TeamName = detail.TeamName
                                      }).ToList()
                        };
            return query.ToList();
        }

        public QueryResult<TiKuMasterModel> GetTiKuByCondition(TiKuQueryFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }

            var queryResult = new QueryResult<TiKuMasterModel>()
            {
                Page = new PageInfo()
                {
                    PageIndex = filter.PageInfo.PageIndex <= 0 ? 1 : filter.PageInfo.PageIndex,
                    PageSize = filter.PageInfo.PageSize
                }
            };

            queryResult.Page.Total = tiKuRepo.Entities.Count();
            var query = tiKuRepo.Entities.OrderByDescending(m => m.LastEditDate)
                .Skip(filter.PageInfo.PageSize * (filter.PageInfo.PageIndex - 1))
                .Take(filter.PageInfo.PageSize);
            queryResult.Result = PublicFunc.EntityMap<List<TiKuMaster>, List<TiKuMasterModel>>(query.ToList());
            return queryResult;
        }

        public void CreateTiKu(TiKuMasterModel master)
        {
            if (master != null)
            {
                var parameter = new SqlParameter()
                {
                    DbType = DbType.String,
                    ParameterName = "@TiKuName",
                    Value = master.TiKuName
                };
                adonetWrapper.ExecuteSqlCommand(
                        @"declare @masterSysNo int
                          select @masterSysNo = SysNo FROM dbo.TiKuMaster where TiKuName=@TiKuName
                          delete from dbo.TiKuMaster where SysNo = @masterSysNo;
                          delete from dbo.TiKuDetail where MasterSysNo = @masterSysNo"
                        , parameter);

                var insertCount = tiKuRepo.Insert(PublicFunc.EntityMap<TiKuMasterModel, TiKuMaster>(master));
                if (insertCount > 0)
                {
                    var masterSysNo = tiKuRepo.Entities.FirstOrDefault(m => m.TiKuName == master.TiKuName).SysNo;
                    var details = PublicFunc.EntityMap<List<TiKuDetailModel>, List<TiKuDetail>>(master.Details);
                    if (details != null)
                    {
                        details.ForEach((detail) =>
                        {
                            detail.MasterSysNo = masterSysNo;
                        });

                        tiKuDetailRepo.Insert(details);

                        var names = details.Select(m => m.ProcessName).Distinct();
                        foreach (var name in names)
                        {
                            var matchDetails = details.Where(m => m.ProcessName == name);
                            if (matchDetails != null)
                            {
                                var initExam = new InitExamModel();
                                initExam.User = master.User;
                                initExam.ProcessName = name;
                                initExam.NodeTeams = new List<NodeTeamModel>();

                                foreach (var mDetail in matchDetails)
                                {
                                    initExam.NodeTeams.Add(new NodeTeamModel()
                                    {
                                        NodeName = mDetail.NodeName,
                                        TeamName = mDetail.TeamName
                                    });
                                }
                                taskService.InitExam(initExam);
                            }
                        }
                    }
                }
            }
        }

        public void ActiveTiKu(TiKuUpdateModel masters)
        {
            if (masters != null)
            {
                foreach (var master in masters.List)
                {
                    var entity = tiKuRepo.Entities.FirstOrDefault(m => m.SysNo == master.SysNo);
                    if (entity != null)
                    {
                        entity.Status = master.Status;
                        entity.LastEditDate = DateTime.Now;
                        entity.LastEditUser = masters.User.UserID;
                        tiKuRepo.Update(entity);

                        var names = tiKuDetailRepo.Entities.Where(m => m.MasterSysNo == entity.SysNo).Select(m => m.ProcessName).Distinct();
                        foreach (var name in names)
                        {
                            taskService.BeginExam(new BeginExamModel()
                            {
                                ProcessName = name,
                                UserId = masters.User.UserID,
                                UserName = masters.User.UserName
                            });
                        }
                    }
                }
            }
        }

        public void DeleteTiKu(TiKuUpdateModel model)
        {
            foreach (var master in model.List)
            {
                var parameter = new SqlParameter()
                {
                    DbType = DbType.Int32,
                    ParameterName = "@SysNo",
                    Value = master.SysNo
                };
                adonetWrapper.ExecuteSqlCommand(
                        @"delete from dbo.TiKuMaster where SysNo = @SysNo;
                      delete from dbo.TiKuDetail where MasterSysNo = @SysNo"
                        , parameter);
            }
        }

        public List<dynamic> GetExportProcess()
        {
            List<dynamic> result = new List<dynamic>();
            var proxy = new WorkflowProxy();
            var processList = processInfoRepo.Entities.ToList();

            foreach (var p in processList)
            {
                var tasks = proxy.GetProcessAllTask(p.ProcessName);
                foreach (var task in tasks)
                {
                    result.Add(new
                    {
                        p.ProcessName,
                        NodeName = task,
                        TeamName = "",
                        p.Category,
                        p.DifficultyLevel,
                        p.Description,
                        p.InDate,
                        p.InUser,
                        LastEditDate = p.LastEditDate.Value,
                        p.LastEditUser
                    });
                }
            }

            return result;
        }
    }
}
