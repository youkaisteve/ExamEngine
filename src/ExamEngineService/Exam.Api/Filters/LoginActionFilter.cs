using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using Exam.Repository;
using Exam.Api.Framework;

namespace Exam.Api.Filters
{
    public class LoginActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var user = actionExecutedContext.Response.Content.ReadAsAsync<User>().Result;
            if (user != null)
            {
                actionExecutedContext.Response.Content.Headers.Add("user-authorize", UserHelper.CreateUserToken(user.UserName));
            }
        }
    }
}