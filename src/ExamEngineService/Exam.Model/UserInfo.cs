using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Model
{
    public class UserInfo
    {
        public int UserSysNo { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }

        public DateTime ExpiredDate { get; set; }
    }
}
