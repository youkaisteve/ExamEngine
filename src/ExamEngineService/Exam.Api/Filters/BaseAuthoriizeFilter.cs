using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Component.Tools.Exceptions;
using Exam.Api.Framework;

namespace Exam.Api.Filters
{
    public class BaseAuthoriizeFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var auth = actionContext.Request.Headers.GetValues("user-authorize");
            if (auth == null || !auth.Any())
            {
                throw new BusinessException("该接口需要用户认证");
            }
            var userToken = auth.FirstOrDefault();
            if (string.IsNullOrEmpty(userToken))
            {
                throw new UnAuthorizedException();
            }
            var array = UserHelper.GetCredentials(userToken);
            if (array == null || array.Length == 0)
            {
                throw new UnAuthorizedException();
            }
            base.OnAuthorization(actionContext);
        }
    }
}