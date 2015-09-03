using System.ComponentModel.Composition;
using System.Web.Http;
using Component.Tools.Exceptions;
using Exam.Api.Framework;
using Exam.Repository;
using Exam.Service.Interface;
using Exam.Api.Models;

namespace Exam.Api.Controllers
{
    [Export]
    public class AccountController : BaseApiController
    {
        [Import]
        private IAccountService _accountService;

        [HttpPost]
        public ApiResponse Login(LoginUser user)
        {
            throw new BusinessException("aaa");
            return ApiOk("测试");
        }
    }
}