using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Http;
using Component.Tools;
using Exam.Api.Filters;
using Exam.Api.Framework;
using Exam.Model;
using Exam.Repository;
using Exam.Service.Interface;
using Exam.Service.Interfave;
using WebGrease.Css.Extensions;

namespace Exam.Api.Controllers
{
    [Export]
    public class UserController : BaseApiController
    {
        [Import]
        private IUserService userService;

        [Import]
        private ISettingService settingService;

        [HttpPost]
        [BaseAuthoriizeFilter]
        public ApiResponse MyTeamUsers([FromUri] string name)
        {
            List<User> teamUsers = userService.GetUserByTeamName(name);
            return ApiOk(teamUsers);
        }

        [HttpPost]
        [BaseAuthoriizeFilter]
        public ApiResponse ImportUser()
        {
            string uploadPath = Path.Combine(
                PublicFunc.GetCurrentDirectory(),
                PublicFunc.GetConfigByKey_AppSettings("Upload_Path"));
            HttpPostedFile file = HttpContext.Current.Request.Files[0];
            string strPath = Path.Combine(uploadPath, file.FileName);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            file.SaveAs(strPath);

            var model = new TramUserImportListModel()
            {
                Lists = new List<TeamUserImportModel>(),
                User = new UserInfo()
                {
                    //上传文件时，body为空，在ApiJsonMediaTypeFormatter中ReadFromStreamAsync时要出错
                    //在解决该问题之前，现在这里设置一下登录用户的UserID
                    UserID = ActionContext.Request.Content.Headers.GetValues("UserID").FirstOrDefault()
                }
            };
            var list = model.Lists;
            //using (var sr = new StreamReader(strPath, Encoding.UTF8))
            //{
            //    string lineContent = sr.ReadLine();
            //    while (!string.IsNullOrEmpty(lineContent = sr.ReadLine()))
            //    {
            //        string[] splitValues = lineContent.Split(',');
            //        list.Add(new TeamUserImportModel
            //        {
            //            TeamName = splitValues[0],
            //            UserId = splitValues[1],
            //            UserName = splitValues[2]
            //        });
            //    }
            //}

            var ds = Utility.ExcelToDataSet(strPath, "select * from [Sheet1$]");
            if (ds != null && ds.Tables[0] != null)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    list.Add(new TeamUserImportModel
                                {
                                    TeamName = row[0].ToString(),
                                    UserId = row[1].ToString(),
                                    UserName = row[2].ToString()
                                });
                }
            }

            if (list.Count > 0)
            {
                userService.ImportTeamUser(model);
            }
            if (File.Exists(strPath))
            {
                File.Delete(strPath);
            }
            return ApiOk(list);
        }

        [HttpPost]
        public ApiResponse GetAllProcess()
        {
            var result = userService.GetAllProcess();
            return ApiOk(result);
        }

        [HttpPost]
        public ApiResponse GetScoreStatistics()
        {
            var data = userService.GetScoreStatistics();
            return ApiOk(data);
        }

        [HttpPost]
        [BaseAuthoriizeFilter]
        public ApiResponse SaveStandardAnswer(StandardAnwserModel model)
        {
            settingService.SaveStandardAnswer(model);
            return ApiOk();
        }

        [HttpPost]
        public ApiResponse LoadFormList()
        {
            var returnFiles = new List<string>();
            var formDir = Path.Combine(PublicFunc.GetDeployDirectory(), PublicFunc.GetConfigByKey_AppSettings("Form_Path"));
            if (!string.IsNullOrEmpty(formDir) && Directory.Exists(formDir))
            {
                var files = Directory.GetFiles(formDir, "*.html", SearchOption.AllDirectories);
                files.ForEach(m => returnFiles.Add(PublicFunc.GetConfigByKey_AppSettings("TemplatePrefix") + Path.GetFileName(m)));
            }
            return ApiOk(returnFiles);
        }

        [HttpPost]
        [BaseAuthoriizeFilter]
        public ApiResponse LoadForm([FromBody] StandardAnwserModel model)
        {
            return ApiOk(settingService.LoadForm(model.TemplateName));
        }

        [HttpPost]
        [BaseAuthoriizeFilter]
        public ApiResponse GetProcessImage(ProcessModel process)
        {
            var imageData = settingService.GetProcessImage(process.DefineName);
            return ApiOk(new { Image = imageData });
        }

        [HttpPost]
        [BaseAuthoriizeFilter]
        public ApiResponse GetCurrentTokenImage(ProcessModel process)
        {
            var imageData = settingService.GetCurrentTokenImage(process.InstanceId, process.TokenId);
            return ApiOk(new { Image = imageData });
        }

        [HttpPost]
        public ApiResponse UploadForm()
        {
            string uploadPath = Path.Combine(
                PublicFunc.GetCurrentDirectory(),
                PublicFunc.GetConfigByKey_AppSettings("Form_Path"));
            HttpFileCollection files = HttpContext.Current.Request.Files;
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            foreach (HttpPostedFile file in files)
            {
                string strPath = Path.Combine(uploadPath, file.FileName);
                file.SaveAs(strPath);
            }

            return ApiOk();
        }

        [HttpPost]
        public ApiResponse TerminateAllUnFinishProcess()
        {
            userService.TerminateAllUnFinishProcess();
            return ApiOk();
        }

        public ApiResponse CleanData()
        {
            settingService.CleanData();
            return ApiOk();
        }
    }
}