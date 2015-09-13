using System.Web.Http.Controllers;

namespace Exam.Api.Framework
{
    public class ExamHttpActionSelector : ApiControllerActionSelector
    {
        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            return base.SelectAction(controllerContext);
        }
    }
}