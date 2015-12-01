using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Model
{
    public class TiKuMaster : ModelBase
    {
        public int SysNo { get; set; }
        public string TiKuName { get; set; }
        public string InUser { get; set; }
        public DateTime InDate { get; set; }
        public string LastEditUser { get; set; }
        public DateTime? LastEditDate { get; set; }
        public int Status { get; set; }
    }

    public class TiKuDetail
    {
        public int SysNo { get; set; }
        public int MasterSysNo { get; set; }
        public int ProcessInfoSysNo { get; set; }
        public string NodeName { get; set; }
        public string TeamName { get; set; }
    }
}
