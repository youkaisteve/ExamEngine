using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam.Api.Models
{
    public class ApiRequestData
    {
        public string Action { get; set; }
        public object Params { get; set; }
    }
}