using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Http;
using Component.Tools;
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
        public ApiResponse MyTeamUsers([FromUri] string name)
        {
            List<User> teamUsers = userService.GetUserByTeamName(name);
            return ApiOk(teamUsers);
        }

        [HttpPost]
        public ApiResponse ImportUser()
        {
            string uploadPath = PublicFunc.GetConfigByKey_AppSettings("upload_path");
            HttpPostedFile file = HttpContext.Current.Request.Files[0];
            string strPath = Path.Combine(uploadPath, file.FileName);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            file.SaveAs(strPath);

            var list = new List<TeamUserImportModel>();
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
                userService.ImportTeamUser(list);
            }
            return ApiOk(list);
        }
    }
}