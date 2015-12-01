using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Model
{
    public class ProcessExtendModel : ProcessModel
    {
        public int SysNo { get; set; }
        public string Category { get;set; }
        public string DifficultyLevel { get; set; }
        public string Description { get; set; }
        public string InUser { get; set; }
        public DateTime InDate { get; set; }
        public string LastEditUser { get; set; }
        public DateTime? LastEditDate { get; set; }
    }
}
