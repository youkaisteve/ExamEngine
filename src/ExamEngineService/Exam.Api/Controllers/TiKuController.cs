using System.ComponentModel.Composition;
using System.Web.Http;
using Exam.Api.Filters;
using Exam.Api.Framework;
using Exam.Model;
using Exam.Model.QueryFilters;
using Exam.Service.Interface;

namespace Exam.Api.Controllers
{
    [Export(typeof(TiKuController))]
    public class TiKuController : BaseApiController
    {
        [Import]
        private ITiKuService tiKuService { get; set; }

        [HttpPost]
        [BaseAuthoriizeFilter]
        public ApiResponse ImportTiku(TiKuMasterModel model)
        {
            tiKuService.CreateTiKu(model);
            return ApiOk();
        }

        [HttpPost]
        [BaseAuthoriizeFilter]
        public ApiResponse CreateProcessInfo(ProcessExtendModel model)
        {
            tiKuService.CreateProcessInfo(model);
            return ApiOk();
        }

        [HttpPost]
        [BaseAuthoriizeFilter]
        public ApiResponse UpdateProcessInfo(ProcessExtendModel model)
        {
            tiKuService.UpdateProcessInfo(model);
            return ApiOk();
        }

        [HttpPost]
        public ApiResponse GetAllProcess()
        {
            var result = tiKuService.GetAllProcess();
            return ApiOk(result);
        }

        [HttpPost]
        public ApiResponse GetAllTiKu()
        {
            var result = tiKuService.GetAllTiKu();
            return ApiOk(result);
        }

        [HttpPost]
        public ApiResponse GetTiKuByCondition(TiKuQueryFilter filter)
        {
            var result = tiKuService.GetTiKuByCondition(filter);
            return ApiOk(result);
        }
    }
}