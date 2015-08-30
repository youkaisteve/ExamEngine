// 源文件头信息：
// 文 件 名：RoleAuthorizeAttribute.cs
// 类    名：RoleAuthorizeAttribute
// 所属工程：Exam.Api
// 最后修改：游凯
// 最后修改：2013-10-16 09:39:57

using System;
using System.Web.Mvc;

namespace Exam.Api.Utility
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