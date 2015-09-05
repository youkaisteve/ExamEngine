using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using Exam.Api.Framework;

namespace Exam.Api.Controllers
{
    [Export(typeof(TaskController))]
    public class TaskController : BaseApiController
    {

    }
}