using System.Web.Http;

namespace Exam.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();
            config.Routes.MapHttpRoute(
                "DefaultApi", "api/{controller}/{action}/{id}", new {id = RouteParameter.Optional});
        }
    }
}