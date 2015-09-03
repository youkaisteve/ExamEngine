using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;

namespace Exam.Api.Filters
{
    public class ExamHttpControllerSelector : DefaultHttpControllerSelector
    {
        public ExamHttpControllerSelector(HttpConfiguration configuration)
            : base(configuration)
        { }
        public override string GetControllerName(HttpRequestMessage request)
        {
            var str = base.GetControllerName(request);
            return str;
        }
    }
}