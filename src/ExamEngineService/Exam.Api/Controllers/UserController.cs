using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Http;
using Component.Tools.Exceptions;
using Exam.Api.Framework;
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
        public ApiResponse MyTeamUsers([FromUri] int sysNo)
        {
            List<User> teamUsers = userService.GetUserByTeamSysNo(sysNo);
            return ApiOk(teamUsers);
        }

        [HttpPost]
        public ApiResponse ImportUser([FromBody] object file)
        {
            throw new BusinessException("开发中");
        }
    }
}