using System.Net.Http;
using System.Web.Http.Filters;
using Exam.Api.Framework;
using Exam.Repository;
using Exam.Model;
using System.Web;
using System;
using Exam.Api.Models;

namespace Exam.Api.Filters
{
    public class LoginActionFilter : ActionFilterAttribute
    {
        LoginUser _user;
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.Request != null)
            {
                _user = actionContext.Request.Content.ReadAsAsync<LoginUser>().Result;
            }
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response != null)
            {
                User user = actionExecutedContext.Response.Content.ReadAsAsync<User>().Result;
                if (user != null)
                {
                    UserHelper.SetUserSession(
                        new UserInfo()
                        {
                            UserID = user.UserID,
                            UserSysNo = user.SysNo,
                            UserName = user.UserName,
                            ExpiredDate = DateTime.Now.AddHours(1)
                        });
                    actionExecutedContext.Response.Content.Headers.Add("user-authorize",
                        UserHelper.CreateUserToken(_user.UserID, _user.Password));
                }
            }
        }
    }
}