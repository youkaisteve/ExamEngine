using System.ComponentModel.Composition;
using System.Web.Http;
using Component.Tools.Exceptions;
using Exam.Api.Framework;
using Exam.Repository;
using Exam.Service.Interface;
using Exam.Api.Models;
using Exam.Api.Filters;

namespace Exam.Api.Controllers
{
    [Export]
    public class AccountController : BaseApiController
    {
        [Import]
        private IAccountService _accountService;

        [Import]
        private IUserService userService;

        [HttpPost]
        [LoginActionFilter]
        public ApiResponse Login([FromBody]LoginUser user)
        {
            var returnUser = _accountService.Login(user.UserName, user.Password);
            return ApiOk(returnUser);
        }

        public ApiResponse MyTeamUsers([FromUri]int sysNo)
        {
            var teamUsers = userService.GetUserByTeamSysNo(sysNo);
            return ApiOk(teamUsers);
        }
    }
}