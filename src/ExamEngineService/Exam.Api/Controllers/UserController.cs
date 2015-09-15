using System.Collections.Generic;
using System.ComponentModel.Composition;
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

namespace Exam.Api.Controllers
{
    [Export]
    public class UserController : BaseApiController
    {
        [Import]
        private IUserService userService;

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
            using (var sr = new StreamReader(strPath, Encoding.UTF8))
            {
                string lineContent = sr.ReadLine();
                while (!string.IsNullOrEmpty(lineContent = sr.ReadLine()))
                {
                    string[] splitValues = lineContent.Split(',');
                    list.Add(new TeamUserImportModel
                    {
                        TeamName = splitValues[0],
                        UserId = splitValues[1],
                        UserName = splitValues[2]
                    });
                }
            }

            if (list.Count > 0)
            {
                userService.ImportTeamUser(model);
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
    }
}