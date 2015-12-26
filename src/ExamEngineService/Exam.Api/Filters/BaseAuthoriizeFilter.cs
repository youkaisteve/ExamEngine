using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Component.Tools.Exceptions;
using Exam.Api.Framework;
using Exam.Model;

namespace Exam.Api.Filters
{
    public class BaseAuthoriizeFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            IEnumerable<string> auth;
            actionContext.Request.Headers.TryGetValues("user-authorize", out auth);
            if (auth == null || !auth.Any())
            {
                throw new UnAuthorizedException();
            }
            string userToken = auth.FirstOrDefault();
            if (string.IsNullOrEmpty(userToken) || userToken == "null")
            {
                throw new UnAuthorizedException();
            }

            UserInfo userInfo = UserHelper.GetCredentialsToUserInfo(userToken);
            if (userInfo == null)
            {
                throw new UnAuthorizedException();
            }

            //UserInfo userInfo = UserHelper.GetUserSession(array[0]);

            //if (userInfo == null || userInfo.ExpiredDate < DateTime.Now)
            //{
            //    throw new AuthorizeExpiredException();
            //}
            //userInfo.ExpiredDate = DateTime.Now.AddHours(1);
            //UserHelper.SetUserSession(userInfo);

            actionContext.Request.Content.Headers.Add("UserID", userInfo.UserID);
            actionContext.Request.Content.Headers.Add("UserName", userInfo.UserName);
            actionContext.Request.Content.Headers.Add("UserSysNo", userInfo.UserSysNo.ToString());
        }
    }
}