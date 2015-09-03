using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Exam.Api.Framework;

namespace Exam.Api.Filters
{
    public class BaseAuthoriizeAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //string authStr = actionContext.Request.Headers.GetValues("exam-auth").First();
            //if (authStr != null)
            //{
            //    string[] credArray = GetCredentials(authStr);
            //    string userName = credArray[0];
            //    string password = credArray[1];
            //    if (true) //尝试登录并返回结果
            //    {
            //        return;
            //    }
            //}

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