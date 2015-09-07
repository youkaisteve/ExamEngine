using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Web.Http;
using Exam.Api.Filters;
using Exam.Api.Framework;
using Exam.Api.Models;
using Exam.Repository;
using Exam.Service.Interface;

namespace Exam.Api.Controllers
{
    [Export]
    public class AccountController : BaseApiController
    {
        [Import] private IAccountService _accountService;

        [Import] private IUserService userService;

        [HttpPost]
        [LoginActionFilter]
        public ApiResponse Login([FromBody] LoginUser user)
        {
            User returnUser = _accountService.Login(user.UserName, user.Password);
            return ApiOk(returnUser);
        }

        [HttpPost]
        public ApiResponse MyTeamUsers([FromUri] int sysNo)
        {
            List<User> teamUsers = userService.GetUserByTeamSysNo(sysNo);
            return ApiOk(teamUsers);
        }
    }
}