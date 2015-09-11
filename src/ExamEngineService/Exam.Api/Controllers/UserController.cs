using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using Component.Tools;
using Component.Tools.Exceptions;
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
            HttpPostedFile file = HttpContext.Current.Request.Files[0];
            string strPath = Path.Combine(PublicFunc.GetConfigByKey_AppSettings("upload_path"), file.FileName);
            file.SaveAs(strPath);

            List<TeamUserImportModel> list = new List<TeamUserImportModel>();
            using (StreamReader sr = new StreamReader(strPath))
            {
                sr.ReadLine();
                var lineContent = "";
                while ((lineContent = sr.ReadLine()) != "") ;
                {
                    var splitValues = lineContent.Split('\t');
                    list.Add(new TeamUserImportModel()
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