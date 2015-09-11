using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Component.Tools.Exceptions;
using Exam.Api.Framework;
using System.Web;
using System;

namespace Exam.Api.Filters
{
    public class BaseAuthoriizeFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            IEnumerable<string> auth = actionContext.Request.Headers.GetValues("user-authorize");
            if (auth == null || !auth.Any())
            {
                throw new UnAuthorizedException();
            }
            string userToken = auth.FirstOrDefault();
            if (string.IsNullOrEmpty(userToken))
            {
                throw new UnAuthorizedException();
            }
            string[] array = UserHelper.GetCredentials(userToken);
            if (array == null || array.Length == 0)
            {
                throw new UnAuthorizedException();
            }

            var userInfo = UserHelper.GetUserSession(array[0]);

            if (userInfo == null || userInfo.ExpiredDate < DateTime.Now)
            {
                throw new AuthorizeExpiredException();
            }

            userInfo.ExpiredDate = DateTime.Now;
            UserHelper.SetUserSession(userInfo);
        }
    }
}