using System.Web.Mvc;
using System.Web.Routing;

namespace Exam.Api
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("RedirectToAuthError", "AuthorityError",
                new {controller = "Error", action = "AuthorityError"});

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new {controller = "api", action = "welcome", id = UrlParameter.Optional}
                );
        }
    }
}