using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Component.Tools.Exceptions;
using Exam.Api.Configuration.API_CONFIG;
using Exam.Service.Interface;

namespace Exam.Api.Framework
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
            IEnumerable<string> actions;
            request.Headers.TryGetValues("action", out actions);
            if (actions != null && actions.Any())
            {
                string action = actions.FirstOrDefault();
                var actionConfig = ApiConfigurationMgr.Instanse.GetByKey(action);
                if (actionConfig != null)
                {
                    var routeData = request.GetRouteData();
                    routeData.Values["controller"] = actionConfig.Controller;
                    routeData.Values["action"] = actionConfig.Action;
                    request.SetRouteData(routeData);
                }
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}