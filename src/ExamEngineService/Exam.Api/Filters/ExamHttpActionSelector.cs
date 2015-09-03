using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;

namespace Exam.Api.Filters
{
    public class ExamHttpActionSelector : ApiControllerActionSelector
    {
        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            var action =  base.SelectAction(controllerContext);
            return action;
        }
    }
}