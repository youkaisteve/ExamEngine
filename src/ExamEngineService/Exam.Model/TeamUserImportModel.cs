using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Model
{
    /// <summary>
    /// 学生及组的导入实体
    /// </summary>
    public class TeamUserImportModel
    {
        public string TeamId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
