using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Filters;
using System.Web.Script.Serialization;
using Component.Tools.Exceptions;
using Exam.Api.Configuration.API_CONFIG;
using Exam.Api.Controllers;
using Exam.Api.Framework;
using Exam.Api.Models;
using Exam.Service.Interface;
using Newtonsoft.Json;

namespace Exam.Api.Filters
{
    [Export]
    public class RequestTransHandler : DelegatingHandler
    {
        [Import]
        private IAccountService accountService;
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //var content = request.Content;
            //string jsonContent = content.ReadAsStringAsync().Result;
            //ApiRequestData data = JsonConvert.DeserializeObject<ApiRequestData>(jsonContent);
            var actions = request.Headers.GetValues("action");
            if (actions == null || !actions.Any())
            {
                throw new BusinessException("无法识别的请求");
            }
            string action = actions.FirstOrDefault();
            var actionConfig = ApiConfigurationMgr.Instanse.GetByKey(action);
            if (actionConfig == null)
            {
                throw new BusinessException("未知请求");
            }

            var routeData = request.GetRouteData();
            routeData.Values["controller"] = actionConfig.Controller;
            routeData.Values["action"] = actionConfig.Action;

            request.SetRouteData(routeData);

            //if (actionConfig.NeedAuthorize)
            //{
            //    var userToken = request.Headers.Authorization.Parameter;
            //    if (string.IsNullOrEmpty(userToken))
            //    {
            //        throw new UnAuthorizedException();
            //    }
            //    var array = UserHelper.GetUserTokenString(userToken);
            //    if (array == null || array.Length == 0)
            //    {
            //        throw new UnAuthorizedException();
            //    }
            //}



            return base.SendAsync(request, cancellationToken);
        }
    }
}