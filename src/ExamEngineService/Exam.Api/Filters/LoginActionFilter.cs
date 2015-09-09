using System.Net.Http;
using System.Web.Http.Filters;
using Exam.Api.Framework;
using Exam.Repository;

namespace Exam.Api.Filters
{
    public class LoginActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response != null)
            {
                User user = actionExecutedContext.Response.Content.ReadAsAsync<User>().Result;
                if (user != null)
                {
                    actionExecutedContext.Response.Content.Headers.Add("user-authorize",
                        UserHelper.CreateUserToken(user.UserName));
                }
            }
        }
    }
}