// 源文件头信息：
// 文 件 名：BaseController.cs
// 类    名：BaseController
// 所属工程：Exam.Api
// 最后修改：游凯
// 最后修改：2014-02-19 11:21:49

using System;
using System.Web.Mvc;
using TCSCD.Component.Tools;

namespace Exam.Api.Utility
{
    public class BaseController : Controller
    {
        private const string errorFormat = "Source:{0}\nMessage:{1}\nStackTrace:{2}";
        /// <summary>
        /// 记录条数，有特别要求的页面可重新该字段
        /// </summary>
        protected virtual int PageSize
        {
            get { return int.Parse(PublicFunc.GetConfigByKey_AppSettings("PageSize")); }
        }

        /// <summary>
        /// 在新建时，需要加载的表单列表的每页数量
        /// </summary>
        public virtual int HeadPageSize
        {
            get { return 5; }
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            //记录异常日志
            if (filterContext.Exception != null)
            {
                Exception ex = filterContext.Exception.InnerException ?? filterContext.Exception;

                string message = string.Format(
                    errorFormat,
                    ex.Source,
                    ex.Message,
                    ex.StackTrace);
                LogHelper.Instanse.WriteError(message);
                //LogService.WriteErrorLog(message);
            }
        }
    }
}