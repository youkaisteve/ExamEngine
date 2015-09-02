// 源文件头信息：
// 文 件 名：BaseController.cs
// 类    名：BaseController
// 所属工程：Exam.Api
// 最后修改：游凯
// 最后修改：2014-02-19 11:21:49

using System;
using System.Web.Http;
using System.Web.Mvc;
using TCSCD.Component.Tools;

namespace Exam.Api.Utility
{
    public class BaseApiController : ApiController
    {
        /// <summary>  
        /// 对应api的Url  
        /// </summary>  
        public string ApiUrl
        {
            get;
            protected set;
        }

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
    }
}