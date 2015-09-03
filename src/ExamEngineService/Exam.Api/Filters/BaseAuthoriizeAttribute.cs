using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Component.Tools.Exceptions;
using Exam.Api.Framework;

namespace Exam.Api.Filters
{
    public class BaseAuthoriizeAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var userToken = actionContext.Request.Headers.Authorization.Parameter;
            if (string.IsNullOrEmpty(userToken))
            {
                throw new UnAuthorizedException();
            }
            var array = UserHelper.GetUserTokenString(userToken);
            if (array == null || array.Length == 0)
            {
                throw new UnAuthorizedException();
            }

            //HandleUnauthorizedRequest(actionContext);
            base.OnAuthorization(actionContext);
        }

        private string[] GetCredentials(string authStr)
        {
            return SymmetricEncryption.Decrypt(authStr).Split(':');
        }

        private void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

            actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='exam'");
        }
    }
}