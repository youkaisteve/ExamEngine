using System.ComponentModel.Composition;
using System.Web.Http;
using Component.Tools.Exceptions;
using Exam.Api.Filters;
using Exam.Api.Framework;
using Exam.Api.Models;
using Exam.Service.Interface;

namespace Exam.Api.Controllers
{
    [Export]
    public class AccountController : BaseApiController
    {
        [Import]
        private IAccountService _accountService;


        [HttpPost]
        [LoginActionFilter]
        public ApiResponse Login([FromBody] LoginUser user)
        {
            if (user == null)
            {
                throw new BusinessException("无效用户");
            }

            dynamic returnUser = _accountService.Login(user.UserID, user.Password);
            return ApiOk(returnUser);
        }

        [HttpPost]
        [LoginActionFilter]
        public ApiResponse GetUserInfoByToken([FromBody] CommonModel token)
        {
            var userInfo = UserHelper.GetCredentialsToUserInfo(token.token);
            return ApiOk(_accountService.GetUserInfoByUserId(userInfo.UserID));
        }
    }
}