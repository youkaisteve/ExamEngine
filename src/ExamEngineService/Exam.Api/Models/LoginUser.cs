using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam.Api.Models
{
    [Serializable]
    public class LoginUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}