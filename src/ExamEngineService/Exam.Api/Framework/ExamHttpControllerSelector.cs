using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace Exam.Api.Framework
{
    public class ExamHttpControllerSelector : DefaultHttpControllerSelector
    {
        public ExamHttpControllerSelector(HttpConfiguration configuration)
            : base(configuration)
        {
        }

        public override string GetControllerName(HttpRequestMessage request)
        {
            string str = base.GetControllerName(request);
            return str;
        }
    }
}