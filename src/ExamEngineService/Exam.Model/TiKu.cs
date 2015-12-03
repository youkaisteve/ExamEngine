using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Model
{
    public class TiKuMasterModel : ModelBase
    {
        public int SysNo { get; set; }
        public string TiKuName { get; set; }
        public string InUser { get; set; }
        public DateTime InDate { get; set; }
        public string LastEditUser { get; set; }
        public DateTime? LastEditDate { get; set; }
        public int Status { get; set; }

        public List<TiKuDetailModel> Details { get; set; }
    }

    public class TiKuDetailModel
    {
        public int SysNo { get; set; }
        public int MasterSysNo { get; set; }
        public string ProcessName { get; set; }
        public string NodeName { get; set; }
        public string TeamName { get; set; }
    }

    public class TiKuUpdateModel : ModelBase
    {
        public List<TiKuMasterModel> List { get; set; }
    }
}
