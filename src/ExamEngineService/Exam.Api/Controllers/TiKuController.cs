using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Web;
using System.Web.Http;
using Component.Tools;
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
using System.Web.Http.ModelBinding;

namespace Exam.Api.Controllers
{
    [Export(typeof(TiKuController))]
    public class TiKuController : BaseApiController
    {
        [Import]
        private ITiKuService tiKuService { get; set; }

        [HttpGet]
        public HttpResponseMessage ExportProcessInfo()
        {
            var data = tiKuService.GetExportProcess();
            var table = Utility.ToDataTable(data);
            var stream = Utility.DataTableToExcel(table, "tiku");
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


        [HttpPost]
        [BaseAuthoriizeFilter]
        public ApiResponse ImportTiKu()
        {
            string uploadPath = Path.Combine(
                PublicFunc.GetDeployDirectory(),
                PublicFunc.GetConfigByKey_AppSettings("Upload_Path"));
            HttpPostedFile file = HttpContext.Current.Request.Files[0];
            //var file = new { FileName = "题库.xls" };
            string strPath = Path.Combine(uploadPath, file.FileName);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            file.SaveAs(strPath);

            var baseModel = GetModelBase();

            var ds = Utility.ExcelToDataSet(strPath, "select * from [Sheet0$]");
            var model = new TiKuMasterModel();
            model.User = baseModel.User;
            if (ds != null && ds.Tables[0] != null)
            {
                model.TiKuName = file.FileName.Substring(0, file.FileName.LastIndexOf('.'));
                model.InDate = DateTime.Now;
                model.InUser = baseModel.User.UserID;
                model.LastEditDate = DateTime.Now;
                model.LastEditUser = baseModel.User.UserID;
                model.Status = (int)TiKuStatus.WaitForActive;
                model.Details = new List<TiKuDetailModel>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    model.Details.Add(new TiKuDetailModel
                    {
                        TeamName = row["TeamName"].ToString(),
                        ProcessName = row["ProcessName"].ToString(),
                        NodeName = row["NodeName"].ToString()
                    });
                }
            }

            tiKuService.CreateTiKu(model);

            if (File.Exists(strPath))
            {
                File.Delete(strPath);
            }
            return ApiOk();
        }

        [HttpPost]
        [BaseAuthoriizeFilter]
        public ApiResponse ActiveTiKu(TiKuUpdateModel model)
        {
            foreach(var item in model.List)
            {
                item.Status = (int)TiKuStatus.Actived;
            }
            tiKuService.ActiveTiKu(model);
            return ApiOk();
        }

        [HttpPost]
        [BaseAuthoriizeFilter]
        public ApiResponse DeleteTiKu(TiKuUpdateModel model)
        {
            tiKuService.DeleteTiKu(model);
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