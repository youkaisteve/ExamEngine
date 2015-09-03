using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Component.Tools.Configurations;

namespace Exam.Api.Configuration.API_CONFIG
{
    public class ApiConfiguration : ConfigBase
    {
        public string Controller { get; set; }

        public string Action { get; set; }

        public bool NeedAuthorize { get; set; }
    }
}