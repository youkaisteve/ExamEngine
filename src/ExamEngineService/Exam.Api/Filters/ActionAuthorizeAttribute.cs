using System.Web.Mvc;

namespace Exam.Api.Filters
{
    public class ActionAuthorizeAttribute : AuthorizeAttribute
    {
        public virtual string ModuleId { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }
    }
}