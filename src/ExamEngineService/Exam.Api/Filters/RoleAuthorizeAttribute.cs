using System.Web.Mvc;

namespace Exam.Api.Filters
{
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        public string ModuleId { get; set; }
        
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
           base.OnAuthorization(filterContext);
        }
    }
}