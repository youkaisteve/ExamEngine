using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Exam.Api.Framework;
using Exam.Api.Models;
using Exam.Model;
using Exam.Repository;

namespace Exam.Api.Filters
{
    public class LoginActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response != null)
            {
                ApiResponse response = actionExecutedContext.Response.Content.ReadAsAsync<ApiResponse>().Result;
                dynamic user = response.Data;
                if (user != null)
                {
                    var sysNo = user.GetType().GetProperty("SysNo").GetValue(user);
                    if (sysNo != null && sysNo > 0)
                    {
                        var userInfo = new UserInfo
                        {
                            UserID = user.GetType().GetProperty("UserID").GetValue(user),
                            UserSysNo = sysNo,
                            UserName = user.GetType().GetProperty("UserName").GetValue(user),
                            ExpiredDate = DateTime.Now.AddHours(1)
                        };
                        UserHelper.SetUserSession(userInfo);

                        actionExecutedContext.Response.Content.Headers.Add("user-authorize",
                            UserHelper.CreateUserToken(userInfo.UserID,
                                user.GetType().GetProperty("Password").GetValue(user).ToString()));
                    }
                }
            }
        }
    }
}