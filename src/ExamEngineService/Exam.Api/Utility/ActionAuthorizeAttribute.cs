// 源文件头信息：
// 文 件 名：ActionAuthorizeAttribute.cs
// 类    名：ActionAuthorizeAttribute
// 所属工程：Exam.Api
// 最后修改：游凯
// 最后修改：2014-05-06 10:25:36

using System.Web.Mvc;

namespace Exam.Api.Utility
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