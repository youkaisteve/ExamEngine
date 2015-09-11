using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Component.Tools.Exceptions
{
    public class AuthorizeExpiredException : BusinessException
    {
        public override int ExceptionCode
        {
            get
            {
                return 3;
            }
        }
        public AuthorizeExpiredException()
            : base("授权已过期")
        {
        }
    }
}
