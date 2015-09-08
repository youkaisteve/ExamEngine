using System.Linq;
using System.Web.Http.Filters;

namespace Exam.Api.Filters
{
    public class eEnableCorsAttribute : ActionFilterAttribute
    {
        private const string accessControlAllowOrigin = "Access-Control-Allow-Origin";
        private const string origin = "Origin";

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Request.Headers.Contains(origin))
            {
                var originHeader = actionExecutedContext.Request.Headers.GetValues(origin).FirstOrDefault();

                if (!string.IsNullOrEmpty(originHeader))
                {
                    actionExecutedContext.Response.Headers.Add(accessControlAllowOrigin, originHeader);
                }
            }
        }
    }
}