using System.Net;
using System.Net.Http;

namespace Exam.Api.Utility
{
    public class BasicAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization != null)
            {
                //string userInfo = Encoding.Default.GetString(Convert.FromBase64String(actionContext.Request.Headers.Authorization.Parameter));
                //用户验证逻辑
                if (UserHelper.IsValid(actionContext.Request.Headers.Authorization.Parameter))
                {
                    IsAuthorized(actionContext);
                }
                else
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
            else
            {
                HandleUnauthorizedRequest(actionContext);
            }
        }

        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var host = actionContext.Request.RequestUri.DnsSafeHost;
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Authenticate", string.Format("Basic realm=\"{0}\"", host));  
        }
       
    }
}