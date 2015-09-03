using System;
using System.ComponentModel.Composition.Hosting;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Exam.Api.Filters;
using Exam.Api.Framework;

namespace Exam.Api
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);

            var catalog = new DirectoryCatalog(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath);
            var solver = new MefDependencySolver(catalog);
            GlobalConfiguration.Configuration.DependencyResolver = solver;

            GlobalConfiguration.Configuration.Formatters.Clear();
            GlobalConfiguration.Configuration.Formatters.Add(new ApiJsonMediaTypeFormatter());

            GlobalConfiguration.Configuration.MessageHandlers.Add(new RequestTransHandler());

            var config = GlobalConfiguration.Configuration;
            config.Services.Replace(typeof(IHttpControllerSelector), new ExamHttpControllerSelector(config));

            config.Services.Replace(typeof(IHttpActionSelector), new ExamHttpActionSelector());
            //GlobalConfiguration.Configuration.Services.Remove(typeof(IHttpActionInvoker), GlobalConfiguration.Configuration.Services.GetActionInvoker());
            //GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpActionInvoker), new AuthorizeHandler());
        }
    }
}