using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Model
{
    public class TramUserImportListModel : ModelBase
    {
        public List<TeamUserImportModel> Lists { get; set; }
    }
    /// <summary>
    /// 学生及组的导入实体
    /// </summary>
    public class TeamUserImportModel
    {
        public string TeamName { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
    }
}
