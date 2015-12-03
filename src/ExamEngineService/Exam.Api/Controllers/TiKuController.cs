using System.ComponentModel.Composition;
using System.Web.Http;
using Exam.Api.Filters;
using Exam.Api.Framework;
using Exam.Model;
using Exam.Model.QueryFilters;
using Exam.Service.Interface;
using DCommon.LinqToNPOI;
using DCommon.LinqToNPOI.Writer;
using System.Net.Http;
using System.IO;
using System.Net;
using System.Net.Http.Headers;

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

        [HttpGet]
        public HttpResponseMessage ExportProcessInfo()
        {
            var data = tiKuService.GetExportProcess();
            var table = Utility.ToDataTable(data);
            var stream = Utility.DataTableToExcel(table);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "题库.xls"
            };
            
            return result;
        }

        public ApiResponse ImportTiKu()
        {
            string uploadPath = Path.Combine(
                PublicFunc.GetDeployDirectory(),
                PublicFunc.GetConfigByKey_AppSettings("Upload_Path"));
            HttpPostedFile file = HttpContext.Current.Request.Files[0];
            string strPath = Path.Combine(uploadPath, file.FileName);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            file.SaveAs(strPath);

            var ds = Utility.ExcelToDataSet(strPath, "select * from [Sheet1$]");
            TiKuMasterModel model = new TiKuMasterModel();

            if (File.Exists(strPath))
            {
                File.Delete(strPath);
            }
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
        public ApiResponse QueryProcess(ProcessQueryFilter filter)
        {
            var result = tiKuService.GetProcessByCondition(filter);
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